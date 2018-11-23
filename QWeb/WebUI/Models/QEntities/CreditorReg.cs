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
    public class CreditorReg
    {
        [Display(Name = "Название реестра")]
        public string RegName { get; set; }

        [Display(Name = "ID")]
        public string RegId { get; set; }

        [Display(Name = "Дата договора")]
        public string ActDate { get; set; }

        [Display(Name = "Дата начала работы")]
        public string StartDate { get; set; }

        [Display(Name = "Дата окончания работы")]
        public string StopDate { get; set; }

        [Display(Name = "В работе")]
        public string IsActive { get; set; }


        public static List<CreditorReg> GetRegList(string creditorId)
        {
            List<CreditorReg> regs = new List<CreditorReg>();
            string path = HostingEnvironment.MapPath(@"~/App_Data/Sql_files/regs_by_creditor_id.sql");
            string text = File.ReadAllText(path);
            string query = String.Format("{0} {1}", text, creditorId); 
            List<string> list = ManagerSqlFiles.GetCountedFieldData(query, 6);

            foreach (var item in list)
            {
                string[] arr = item.Split('#');
                CreditorReg reg = new CreditorReg { RegName = arr[0], RegId = arr[1], ActDate = arr[2], StartDate = arr[3], StopDate = arr[4], IsActive = arr[5] };
                regs.Add(reg);
            }
            return regs;
        }
        


    }
}