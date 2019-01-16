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


        public void Act()
        {
            WaiterStart();
            InsertToReportTable();
            UpdatePriority();
            
            //FreeTable();
        }

        private void InsertToReportTable()
        {
            List<OracleParameter> args = new List<OracleParameter>() {
                new OracleParameter("priority_value", OracleDbType.Varchar2, PriorityValue, ParameterDirection.Input)
            };
            ManagerPlSql.ExecProc1("q_prior_pack.prior_to_report", args);
        }

        //private void FreeTable()
        //{
        //    List<OracleParameter> args = new List<OracleParameter>() {
        //        new OracleParameter("task_id", OracleDbType.Varchar2, TaskHandler.TaskId, ParameterDirection.Input)
        //    };
        //    ManagerPlSql.ExecProc1("Q_TASKS_PACK.free_table", args);
        //}

        public string ChekResult()
        {
            List<OracleParameter> args = new List<OracleParameter>() {
                new OracleParameter("priority_value", OracleDbType.Varchar2, PriorityValue, ParameterDirection.Input)
            };
            return ManagerPlSql.ExecFunc("q_prior_pack.get_updated_prior_count", args);            
        }

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
            ManagerPlSql.ExecProc1("q_prior_pack.set_prior", args);
        }   
        
    }
}