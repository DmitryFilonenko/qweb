using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLayer.Prop
{
    public enum ConnectionType { Work, Test }

    public class DbLayerProperties
    {        
        public static event Action<ConnectionType> ConnnectionStringChanged;

        static DbLayerProperties()
        {
             SetWorkDb();  // Set DB by default
            // SetTestDb();
        } 
        
        public static string ConnectionString { get; set; }
        public static ConnectionType DbType { get; set; }

        public static void SetConnectionType(ConnectionType type)
        {
            switch (type)
            {
                case ConnectionType.Work:
                    SetWorkDb();
                    break;
                case ConnectionType.Test:
                    SetTestDb();
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        private static void SetTestDb()
        {
            DbType = ConnectionType.Test;
            ConnectionString = "User ID = qira; password=eadrqf; Data Source = CD_102";
            ConnnectionStringChanged?.Invoke(ConnectionType.Test);
        }

        private static void SetWorkDb()
        {
            DbType = ConnectionType.Work;
            ConnectionString = "User ID = qira; password=eadrqf; Data Source = CD_WORK";
            ConnnectionStringChanged?.Invoke(ConnectionType.Work);
        }
    }
}
