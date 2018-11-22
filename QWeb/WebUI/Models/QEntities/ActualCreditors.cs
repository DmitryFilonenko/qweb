using DbLayer.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace WebUI.Models.QEntities
{
    public class ActualCreditor
    {
        public string OrgName { get; set; }
        public string Alias { get; set; }
        public string OrgIdLong { get; set; }
        public string OrgIdShorty { get; set; }
        //public string Fund { get; set; }
        List<ActualCreditor> _actualCreditorsList = null;
        public List<ActualCreditor> ActualCreditorsList { get { return _actualCreditorsList; } set { _actualCreditorsList = value; } }

        public ActualCreditor() { }

        public ActualCreditor(string path)
        {
            _actualCreditorsList = new List<ActualCreditor>();            
            InitList(path);
        }

        private void InitList(string path)
        {
            //string path = @"~/App_Data/Sql_files/actual_creditors.sql"; //  @"~/Sql_files/actual_creditors.sql";
            string query = File.ReadAllText(path);
            List<string> list = ManagerSqlFiles.GetDataByFilePath(path, 4);

            foreach (var item in list)
            {
                string[] arr = item.Split('#');
                ActualCreditor creditor = new ActualCreditor { OrgName = arr[0], Alias = arr[1], OrgIdLong = arr[2], OrgIdShorty = arr[3]/*, Fund = arr[4]*/ };
                _actualCreditorsList.Add(creditor);
            }
        }
    }
}