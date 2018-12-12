using DbLayer.Connect;
using DbLayer.Infrsrt;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLayer.Managers
{
    public static class ManagerPlProc
    {
        static OracleConnection _con = DbConnect.GetDBConnection();

        private static void OpenConnect()
        {
            _con.Open();
            OracleGlobalization info = _con.GetSessionInfo();
            info.NumericCharacters = ",.";
            info.DateFormat = "dd.mm.yyyy";
            info.Language = "UKRAINIAN";
            _con.SetSessionInfo(info);
        }

        //public static void ExecProc(string procName)
        //{
        //    try
        //    {
        //        OpenConnect();

        //        using (OracleCommand cmd = new OracleCommand(procName, _con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            //cmd.Parameters.Add("regLongName", OracleDbType.Varchar2).Value = regName;
        //            //cmd.Parameters.Add("payStart", OracleDbType.Date).Value = start;

        //            cmd.ExecuteNonQuery();
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally { _con.Close(); }
        //}

        public static void ExecProc(string procName, List<ProcParam> args = null )
        {
            try
            {
                OpenConnect();

                using (OracleCommand cmd = new OracleCommand(procName, _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (var item in args)
                    {
                        cmd.Parameters.Add(item.Name, item.Type, item.Direction).Value = item.Value;
                    }

                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally { _con.Close(); }
        }

       
        public static string ExecFunc(string procName, List<ProcParam> args = null)
        {
            try
            {
                OpenConnect();

                using (OracleCommand cmd = new OracleCommand(procName, _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    
                    //var param1 = new ProcParam { Name = "user_login", Type = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = "Dima" };
                    //var param2 = new ProcParam { Type = OracleDbType.Varchar2, Direction = ParameterDirection.ReturnValue };

                    foreach (var item in args)
                    {
                        cmd.Parameters.Add(item.Name, item.Type, item.Direction).Value = item.Value;
                    }

                    cmd.ExecuteNonQuery();
                    return cmd.Parameters[0].Value.ToString();
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally { _con.Close(); }
        }
    }
}
