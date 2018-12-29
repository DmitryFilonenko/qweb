using DbLayer.Prop;
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

        //private static readonly DbConnect instance = new DbConnect();
        public static OracleConnection _con;// = new OracleConnection(DbLayerProperties.ConnectionString);

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static DbConnect()
        {
            CreateConnect();
            DbLayerProperties.ConnnectionStringChanged += DbLayerProperties_ConnnectionStringChanged;
        }

        private static void CreateConnect()
        {
            _con = new OracleConnection(DbLayerProperties.ConnectionString);
            OpenConnect(ref _con);
        }

        private static void DbLayerProperties_ConnnectionStringChanged(ConnectionType obj)
        {
            if (_con.State == System.Data.ConnectionState.Open)
            {
                _con.Close();
            }
            CreateConnect();
        }

        private static void OpenConnect(ref OracleConnection con)
        {
            con.Open();
            OracleGlobalization info = _con.GetSessionInfo();
            info.NumericCharacters = ",.";
            info.DateFormat = "dd.mm.yyyy";
            info.Language = "UKRAINIAN";
            con.SetSessionInfo(info);
        }

        //private DbConnect()
        //{
        //}

        //public static DbConnect Instance
        //{
        //    get
        //    {
        //        return instance;
        //    }
        //}



        //public static OracleConnection GetDBConnection()
        //{
        //    return _con;
        //}

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
