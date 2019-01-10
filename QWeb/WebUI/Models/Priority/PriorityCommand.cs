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
    public class PriorityCommand : IQCommand
    {
        public string TaskId { get; set; }
        public string PriorityValue { get; set; }
        public QUploadFileHandler FileHandler { get; set; }
        public event Action TaskFinishsed;
        
        public string PreResult { get; }
        

        public string BorrowTable(string userLogin)
        {
            return ManagerFileUpload.TakeTable(TaskId, userLogin);
        }

        public string GetPreRes()
        {
            List<OracleParameter> args = new List<OracleParameter>() {
                new OracleParameter("priority_value", OracleDbType.Varchar2, PriorityValue, ParameterDirection.Input)
            };
            return ManagerPlProc.ExecFunc("q_prior_pack.check_prior", OracleDbType.Varchar2, args);
        }

        public void Act()
        {
            if (FillTable())
            {
                WaiterStart();
                UpdatePriority();
            }
        }

        public bool FillTable()
        {
            string[] arr = new string[] { "deal_id" };
            return ManagerDbQuery.FillTable(this.TaskId, arr, this.FileHandler.GetData());
        }

        private void WaiterStart()
        {
            QTaskConroller qTaskConroller = new QTaskConroller(TaskId);
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
            string user = ManagerPlProc.ExecFunc("q_prior_pack.set_prior", OracleDbType.Varchar2, args);
        }
    }
}