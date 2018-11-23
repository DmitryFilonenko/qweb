using DbLayer.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace WebUI.Models.QEntities
{
    public class ActualCreditor
    {
        [Display(Name = "Название")]
        public string OrgName { get; set; }
        [Display(Name = "ID suvd")]
        public string OrgIdLong { get; set; }
        [Display(Name = "ID eadr")]
        public string OrgIdShort { get; set; }

        public static List<ActualCreditor> GetCreditorList()
        {
            List<ActualCreditor> actualCreditorsList = new List<ActualCreditor>();
            string path = HostingEnvironment.MapPath(@"~/App_Data/Sql_files/actual_creditors.sql");
            string query = File.ReadAllText(path);
            List<string> list = ManagerSqlFiles.GetCountedFieldData(query, 3);

            foreach (var item in list)
            {
                string[] arr = item.Split('#');
                ActualCreditor creditor = new ActualCreditor { OrgName = arr[0], OrgIdLong = arr[1], OrgIdShort = arr[2] };
                actualCreditorsList.Add(creditor);
            }
            return actualCreditorsList;
        }
    }
}