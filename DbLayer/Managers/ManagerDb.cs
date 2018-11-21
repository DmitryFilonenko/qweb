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
                string query = String.Format("select count(*) from {0}", tableName);
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

        public static List<string> GetFieldsListById(string tableName, string[] fieldNameArr, string idValue, string idName = "id")
        {
            try
            {
                _con.Open();
                int countOfFields = fieldNameArr.Length;
                List<string> list = new List<string>();
                string fields = ArrToString(fieldNameArr);

                string query = String.Format("select {0} from {1} where {2} = {3}", fields, tableName, idName, idValue);

                OracleDataReader reader = DbConnect.GetReader(query);
                while (reader.Read())
                {
                    string record = "";
                    for (int i = 0; i < countOfFields; i++)
                    {
                        record += (reader[i].ToString() + "#");
                    }
                    list.Add(record.TrimEnd('#'));
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
            finally { _con.Close(); }
        }

        public static string GetSingleRecordById(string tableName, string[] fieldNameArr, string idValue, string idName = "id")
        {
            try
            {
                _con.Open();
                int countOfFields = fieldNameArr.Length;
                List<string> list = new List<string>();
                string fields = ArrToString(fieldNameArr);

                string query = String.Format("select {0} from {1} where {2} = {3}", fields, tableName, idName, idValue);

                OracleDataReader reader = DbConnect.GetReader(query);
                while (reader.Read())
                {
                    string record = "";
                    for (int i = 0; i < countOfFields; i++)
                    {
                        record += (reader[i].ToString() + "#");
                    }
                    list.Add(record.TrimEnd('#'));
                }
                return list[0];
            }
            catch (Exception)
            {
                throw;
            }
            finally { _con.Close(); }
        }

        public static List<string> GetFieldsList(string tableName, string[] fieldNameArr)
        {
            try
            {
                _con.Open();

                int countOfFields = fieldNameArr.Length;
                List<string> list = new List<string>();
                string fields = ArrToString(fieldNameArr);

                string query = String.Format("select {0} from {1} ", fields, tableName);

                OracleDataReader reader = DbConnect.GetReader(query);
                while (reader.Read())
                {
                    string record = "";
                    for (int i = 0; i < countOfFields; i++)
                    {
                        record += (reader[i].ToString() + "#");
                    }
                    list.Add(record.TrimEnd('#'));
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
            finally { _con.Close(); }
        }

        static string ArrToString(string[] strArr)
        {
            string fields = "";
            foreach (var item in strArr)
            {
                fields += item + ",";
            }            
            return fields.TrimEnd(',');
        }
    }
}
