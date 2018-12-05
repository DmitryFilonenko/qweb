using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLayer.Infrsrt
{
    public class ProcParam
    {
        public string Name { get; set; }
        public OracleDbType Type { get; set; }
        public ParameterDirection Direction { get; set; }        
        public string Value { get; set; }
    }
}
