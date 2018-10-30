using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLayer
{
    public static class Properties
    {
        static string _connectionString = "User ID = qira; password=eadrqf; Data Source = CD_WORK";
        public static string ConnectionString { get { return _connectionString; } } 
    }
}
