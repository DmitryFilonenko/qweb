using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLayer.Connect
{
    class DbConnect
    {
        #region connectSupport
        private static readonly DbConnect instance = new DbConnect();
        private static readonly OracleConnection _con = new OracleConnection(Properties.ConnectionString);

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static DbConnect()
        {
        }

        private DbConnect()
        {
        }

        public static DbConnect Instance
        {
            get
            {
                return instance;
            }
        }

        public static OracleConnection GetDBConnection()
        {
            return _con;
        }

        static OracleCommand _cmd;

        public static void ExecCommand(string query)
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
    }
}
