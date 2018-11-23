using DbLayer.Connect;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLayer.Managers
{
    public static class ManagerSqlFiles
    {
        static OracleConnection _con = DbConnect.GetDBConnection();

        public static List<string> GetCountedFieldData(string query, int fieldsCount)
        {
            List<string> list = new List<string>();
            try
            {
                _con.Open();
                //string query = File.ReadAllText(path, Encoding.Default);
                OracleDataReader reader = DbConnect.GetReader(query);
                while (reader.Read())
                {
                    string record = "";
                    for (int i = 0; i < fieldsCount; i++)
                    {
                        record += (reader[i].ToString() + "#");
                    }
                    list.Add(record.TrimEnd('#'));
                }                
            }
            catch (Exception)
            {
                throw;
            }
            finally { _con.Close(); }

            return list;
        }
    }
}
