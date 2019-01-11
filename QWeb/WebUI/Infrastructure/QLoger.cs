using DbLayer.Infrsrt;
using DbLayer.Managers;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web.Hosting;


namespace WebUI.Infrastructure
{
    public enum MessageType { Report, Exception }

    public static class QLoger
    {
        public static void AddRecordToLog(string userName, string taskName, string message, MessageType messageType, [CallerMemberName] string callerName = null)
        {
            try
            {                
                List<ProcParam> args = new List<ProcParam> {
                    new ProcParam { Name = "user_login", Type = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = userName },
                    new ProcParam { Name = "task", Type = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = taskName },
                    new ProcParam { Name = "message_text", Type = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = message },
                    new ProcParam { Name = "is_exception", Type = OracleDbType.Decimal, Direction = ParameterDirection.Input, Value = (messageType == MessageType.Exception? "1":"0") },
                    new ProcParam { Name = "method", Type = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = callerName }
                };

                ManagerPlSql.ExecProc("q_log_pack.add_to_log", args);

            }
            catch (Exception)
            {
                string pathToDir = HostingEnvironment.MapPath(@"~/App_Data/Logs");
                string path = Path.Combine(pathToDir, (taskName + ".log"));

                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                }

                string fileText = File.ReadAllText(path);

                string textToAdd = Environment.NewLine + DateTime.Now + "\t" + userName + Environment.NewLine + (callerName != null ? ("Запись создана методом  " + callerName + "()" + Environment.NewLine) : "") + message + Environment.NewLine;
                File.AppendAllText(path, textToAdd);
            }
        }
    }
}
