using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLayer.Managers
{
    public class ManagerFileUpload
    {
        public static string TakeTable(string taskId, string userName)
        {
            List<OracleParameter> args = new List<OracleParameter> {
                    new OracleParameter("task_id", OracleDbType.Varchar2, taskId, ParameterDirection.Input ),
                    new OracleParameter("user_name", OracleDbType.Varchar2, userName, ParameterDirection.Input )
             };

            return ManagerPlProc.ExecFunc("Q_TASKS_PACK.use_table", OracleDbType.Varchar2, args);



             //str;
            //return ManagerPlProc.ExecFunc("q_users_pack.check_table_access", OracleDbType.Varchar2, args); // pl-proc returns "Ok" if table free or user login whos uses table            
        }
    }
}
