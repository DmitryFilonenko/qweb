using DbLayer.Managers;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebUI.Infrastructure.QComands
{
    public class QTaskHandler
    {
        public string TaskId { get; private set; }

        public QTaskHandler(string taskId)
        {
            TaskId = taskId;
        }

        public string BorrowTable(string userLogin)
        {
            List<OracleParameter> args = new List<OracleParameter> {
                    new OracleParameter("task_id", OracleDbType.Varchar2, TaskId, ParameterDirection.Input ),
                    new OracleParameter("user_name", OracleDbType.Varchar2, userLogin, ParameterDirection.Input )
            };

            return ManagerPlSql.ExecFunc("Q_TASKS_PACK.use_table", args);
        }

        public void ReleaseTable()
        {
            List<OracleParameter> args = new List<OracleParameter>() {
                new OracleParameter("task_id", OracleDbType.Varchar2, TaskId, ParameterDirection.Input)
            };

            ManagerPlSql.ExecFunc("Q_TASKS_PACK.free_table", args);
        }
    }
}