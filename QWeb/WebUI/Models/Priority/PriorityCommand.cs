using DbLayer;
using DbLayer.Managers;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebUI.Infrastructure;
using WebUI.Infrastructure.QComands;
using WebUI.Infrastructure.QDbWaiter;

namespace WebUI.Models.Priority
{
    public class PriorityCommand 
    {
        string _priorityValue;
        public string PriorityValue { get { return _priorityValue; } }
        QUploadFileHandler _fileHandler; 
        public QUploadFileHandler FileHandler { get { return _fileHandler; } }
        QTaskHandler _taskHandler;
        public QTaskHandler TaskHandler { get { return _taskHandler; } }
        public event Action TaskFinishsed;
        
        //public string PreResult { get; }


        public PriorityCommand(string priority, string taskId, string pathToFile)
        {
            _priorityValue = priority;
            _fileHandler = new QUploadFileHandler(pathToFile);
            _taskHandler = new QTaskHandler(taskId);
        }

        public string GetPreRes()
        {
            List<OracleParameter> args = new List<OracleParameter>() {
                new OracleParameter("priority_value", OracleDbType.Varchar2, PriorityValue, ParameterDirection.Input)
            };
            string s = ManagerPlSql.ExecFunc("q_prior_pack.check_prior", args);
            return s;
        }




        //public void Act()
        //{

        //    if (FillTable())
        //    {

        //       // WaiterStart();
        //       // UpdatePriority();
        //    }
        //}

        public bool FillTable()
        {
            string[] arr = new string[] { "deal_id" };
            return ManagerDbQuery.FillTable(_taskHandler.TaskId, arr, _fileHandler.GetData());
        }

        private void WaiterStart()
        {
            QTaskConroller qTaskConroller = new QTaskConroller(_taskHandler.TaskId);
            qTaskConroller.TaskFinished += QTaskConroller_TaskFinished;
            qTaskConroller.Wait();
        }

        private void QTaskConroller_TaskFinished()
        {            
            TaskFinishsed?.Invoke();
        }
        
        private void UpdatePriority()
        {
            List<OracleParameter> args = new List<OracleParameter>() {
                new OracleParameter("priority_value", OracleDbType.Varchar2, PriorityValue, ParameterDirection.Input)
            };
            string user = ManagerPlSql.ExecFunc("q_prior_pack.set_prior", args);
        }   
        
    }
}