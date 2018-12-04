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
    public class ManagerDbQuery
    {        
        static OracleConnection _con = DbConnect.GetDBConnection();

        public  static int GetCount(string tableName)
        {
            try
            {
                OpenConnect();
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

        public static int GetCountWhere(string tableName, string field, string value)
        {
            try
            {
                OpenConnect();
                int count = -1;
                string query = String.Format("select count(*) from {0} where {1} = {2}", tableName, field, value);
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
                OpenConnect();
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
                OpenConnect();
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
                OpenConnect();

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

        private static void OpenConnect()
        {
            _con.Open();
            OracleGlobalization info = _con.GetSessionInfo();
            info.NumericCharacters = ",.";
            info.DateFormat = "dd.mm.yyyy";
            info.Language = "UKRAINIAN";
            _con.SetSessionInfo(info);
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


        public static List<string> GetItems(string query, int fieldsCount)
        {
            List<string> list = new List<string>();
            try
            {
                OpenConnect();
                OracleDataReader reader = DbConnect.GetReader(query);
                while (reader.Read())
                {
                    string record = "";
                    for (int i = 0; i < fieldsCount; i++)
                    {
                        if (reader[i] is System.DBNull)
                            record += " #";
                        else
                            record += reader[i].ToString() + "#";
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


        //public static List<string> CompositeQuery(string[] tables, string[] fieldNameArr, string[] conditions )
        //{
        //    List<string> res = new List<string>();



        //    return res;
        //}




    }
}
