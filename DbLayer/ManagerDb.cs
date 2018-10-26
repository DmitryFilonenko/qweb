using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLayer
{
    public class ManagerDb
    {
        #region connectSupport
        private static readonly ManagerDb instance = new ManagerDb();
        private static readonly OracleConnection _con = new OracleConnection(Properties.ConnectionString);

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static ManagerDb()
        {
        }

        private ManagerDb()
        {
        }

        public static ManagerDb Instance
        {
            get
            {
                return instance;
            }
        }

        public OracleConnection GetDBConnection()
        {
            return _con;
        }

        static OracleCommand _cmd;

        static void ExecCommand(string query)
        {
            try
            {
                _cmd = new OracleCommand(query, _con);
                _cmd.ExecuteNonQuery();
                _cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static OracleDataReader GetReader(string query)
        {
            try
            {
                _cmd = new OracleCommand(query, _con);
                OracleDataReader reader = _cmd.ExecuteReader();
                _cmd.Dispose();
                return reader;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        public static int GetCount(string tableName)
        {
            _con.Open();
            int count = -1;
            string query = "select count(*) from " + tableName + " t";
            OracleDataReader reader = GetReader(query);
            while (reader.Read())
            {
                count = Convert.ToInt32(reader[0]);
            }
            _con.Close();
            return count;
        }
    }
}
