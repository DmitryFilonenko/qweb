using DbLayer;
using DbLayer.Managers;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace WebUI.Models.Chess
{
    public class ChessResultModel
    {
        string pathToLeadersFile = HostingEnvironment.MapPath(@"~/App_Data/Sql_files\Chess\Leaders.sql");
        string pathToLastsFile = HostingEnvironment.MapPath(@"~/App_Data/Sql_files\Chess\Lasts.sql");

        public List<ChessResult> Leaders { get; set; }
        public List<ChessResult> Lasts { get; set; }

        public ChessResultModel()
        {
            Leaders = GetData(pathToLeadersFile);
            Lasts = GetData(pathToLastsFile);
        }

        private List<ChessResult> GetData(string path)
        {
            List<ChessResult> list = new List<ChessResult>(); 
            string query = File.ReadAllText(path);
            List<string> res = ManagerDbQuery.GetItems(query, 5);
            foreach (var item in res)
            {
                string[] arr = item.Split('#');

                ChessResult cr = new ChessResult()
                {
                    Player = arr[0],
                    DeskSide = arr[1],
                    Result = arr[2],
                    Field = arr[3],
                    Dt = arr[4]
                };
                list.Add(cr);
            }
            return list;
        }

        public static void AddRecord(ChessResult result)
        {
            string side = result.DeskSide == "1" ? "За белых" : "За черных";

            List<OracleParameter> args = new List<OracleParameter>() {
                new OracleParameter("side", OracleDbType.Varchar2, side, ParameterDirection.Input),
                new OracleParameter("field", OracleDbType.Varchar2, result.Field, ParameterDirection.Input),
                new OracleParameter("user_name", OracleDbType.Varchar2, result.Player, ParameterDirection.Input),
                new OracleParameter("time_res", OracleDbType.Varchar2, result.Result, ParameterDirection.Input),
            };
            ManagerPlSql.ExecProc1("q_chess_pack.add_record", args);
        }        
    }
}