using DbLayer;
using DbLayer.Managers;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebUI.Infrastructure.QDbWaiter
{
    public class QTaskConroller
    {
        string _taskId;
        private System.Timers.Timer _aTimer;

        public event Action TaskFinished;

        public QTaskConroller(string taskId)
        {
            _taskId = taskId;
            _aTimer = new System.Timers.Timer();
        }

        public void Wait()
        {            
            _aTimer.Elapsed += ATimer_Elapsed;
            _aTimer.Interval = 1000;             // 1 sec
            _aTimer.Start();
        }

        private void ATimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            List<OracleParameter> args = new List<OracleParameter>() {
                new OracleParameter("task_id", OracleDbType.Varchar2, _taskId, ParameterDirection.Input)
            };
            string user = ManagerPlSql.ExecFunc("Q_TASKS_PACK.who_uses", args);

            if (user == "null")
            {
                _aTimer.Stop();
                TaskFinished?.Invoke();       
            }
        }
    }
}