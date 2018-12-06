using DbLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace WebUI.Models.QEntities
{
    public class QActualCreditor
    {
        [Display(Name = "Название")]
        public string OrgName { get; set; }
        [Display(Name = "ID suvd")]
        public string OrgIdLong { get; set; }
        [Display(Name = "ID eadr")]
        public string OrgIdShort { get; set; }
        [Display(Name = "Количество реестров")]
        public int Count { get; set; }

        public static List<QActualCreditor> GetCreditorList()
        {
            List<QActualCreditor> actualCreditorsList = new List<QActualCreditor>();
            string path = HostingEnvironment.MapPath(@"~/App_Data/Sql_files/actual_creditors.sql");
            string query = File.ReadAllText(path);
            List<string> list = ManagerDbQuery.GetItems(query, 3);

            foreach (var item in list)
            {
                string[] arr = item.Split('#');
                QActualCreditor creditor = new QActualCreditor { OrgName = arr[0], OrgIdLong = arr[1], OrgIdShort = arr[2] };
                creditor.Count = ManagerDbQuery.GetCountWhere("suvd.creditor_dogovors", "creditor_id", creditor.OrgIdLong.ToString());
                 actualCreditorsList.Add(creditor);
            }
            return actualCreditorsList;
        }
    }
}