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
            if (_con.State == ConnectionState.Closed)
            {
                _con.Open();
                OracleGlobalization info = _con.GetSessionInfo();
                info.NumericCharacters = ",.";
                info.DateFormat = "dd.mm.yyyy";
                info.Language = "UKRAINIAN";
                _con.SetSessionInfo(info);
            }            
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


        //public static string ExecFunc(string procName, List<ProcParam> args = null)
        //{
        //    try
        //    {
        //        OpenConnect();

        //        using (OracleCommand cmd = new OracleCommand(procName, _con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            foreach (var item in args)
        //            {
        //                cmd.Parameters.Add(item.Name, item.Type, item.Direction).Value = item.Value;
        //            }

        //            cmd.ExecuteNonQuery();
        //            return cmd.Parameters[0].Value.ToString();
        //        }

        //    }
        //    catch (Exception ex )
        //    {
        //        throw;
        //    }
        //    finally { _con.Close(); }
        //}




        public static string ExecFunc(string procName, OracleDbType returnType, List<OracleParameter> args = null, int returnSize = 32767)
        {
            try
            {

                OpenConnect();

                using (OracleCommand cmd = new OracleCommand(procName, _con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("x", OracleDbType.Varchar2, returnSize).Direction = ParameterDirection.ReturnValue;

                    foreach (var item in args)
                    {
                        cmd.Parameters.Add(item);// new OracleParameter(item) item.ParameterName, item.OracleDbTypeType, item.Size).Value = item.Value;
                    }

                    cmd.ExecuteNonQuery();

                    string res = cmd.Parameters["x"].Value.ToString();

                    return res;
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
