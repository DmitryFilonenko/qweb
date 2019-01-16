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
    public static class ManagerPlSql
    {
        public static void ExecProc(string procName, List<ProcParam> args = null )
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand(procName, DbConnect._con))
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
        }


        public static void ExecProc1(string procName, List<OracleParameter> args = null)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand(procName, DbConnect._con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (args != null)
                    {
                        foreach (var item in args)
                        {
                            cmd.Parameters.Add(item);
                        }
                    }
                    cmd.ExecuteNonQuery();                    
                }
            }
            catch (Exception)
            {
                throw;
            }
        } 

        public static string ExecFunc(string procName, List<OracleParameter> args = null, int returnSize = 32767)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand(procName, DbConnect._con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("x", OracleDbType.Varchar2, returnSize).Direction = ParameterDirection.ReturnValue;

                    if (args != null)
                    {
                        foreach (var item in args)
                        {
                            cmd.Parameters.Add(item);// new OracleParameter(item) item.ParameterName, item.OracleDbTypeType, item.Size).Value = item.Value;
                        }
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
        }

        
    }
}
