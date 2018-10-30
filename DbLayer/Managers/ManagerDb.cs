using DbLayer.Connect;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLayer
{
    public class ManagerDb
    {        
        static OracleConnection _con = DbConnect.GetDBConnection();

        public  static int GetCount(string tableName)
        {
            try
            {
                _con.Open();
                int count = -1;
                string query = "select count(*) from " + tableName + " t";
                OracleDataReader reader = DbConnect.GetReader(query);
                while (reader.Read())
                {
                    count = Convert.ToInt32(reader[0]);
                }
                return count;
            }
            catch (Exception)
            {
                throw;
            }
            finally { _con.Close(); }            
        }

        public static List<string> GetDataById(string tableName, string fieldName, int id, string idName = "id")
        {
            try
            {
                _con.Open();

                List<string> list = new List<string>();

                string query = "select " + fieldName + " from " + tableName + " where " + idName + " = " + id;

                OracleDataReader reader = DbConnect.GetReader(query);
                while (reader.Read())
                {
                    list.Add(reader[0].ToString());
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
            finally { _con.Close(); }
        }

        public static List<string> GetFieldList(string tableName, string fieldName)
        {
            try
            {
                _con.Open();

                List<string> list = new List<string>();

                string query = "select " + fieldName + " from " + tableName;

                OracleDataReader reader = DbConnect.GetReader(query);
                while (reader.Read())
                {
                    list.Add(reader[0].ToString());
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
            finally { _con.Close(); }
        }


    }
}
