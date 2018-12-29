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
    public class QCreditorReg
    {
        [Display(Name = "Кредитор")]
        public string CreditorName { get; set; }

        [Display(Name = "ID кредитора")]
        public string CreditorId { get; set; }

        [Display(Name = "Реестр")]
        public string RegName { get; set; }

        [Display(Name = "ID реестра")]
        public string RegId { get; set; }

        [Display(Name = "Дата договора")]
        public string ActDate { get; set; }

        [Display(Name = "Дата начала")]
        public string StartDate { get; set; }

        [Display(Name = "Дата окончания")]
        public string StopDate { get; set; }

        [Display(Name = "В работе")]
        public string IsActive { get; set; }

        [Display(Name = "Количество дел")]
        public int Count { get; set; }

        public static List<QCreditorReg> GetRegList(string creditorId)
        {            
            List<QCreditorReg> regs = new List<QCreditorReg>();
            string path = HostingEnvironment.MapPath(@"~/App_Data/Sql_files/Entities/regs_by_creditor_id.sql");
            string text = File.ReadAllText(path);
            string query = String.Format("{0} {1}", text, creditorId); 
            List<string> list = ManagerDbQuery.GetItems(query, 8);

            foreach (var item in list)
            {
                string[] arr = item.Split('#');
                QCreditorReg reg = new QCreditorReg { CreditorName = arr[0],
                                                    CreditorId = arr[1],
                                                    RegName = arr[2],
                                                    RegId = arr[3],
                                                    ActDate = arr[4].Substring(0, 10),
                                                    StartDate = arr[5].Length > 10 ? arr[5].Substring(0, 10) : "",
                                                    StopDate = arr[6].Length > 10 ? arr[6].Substring(0, 10) : "",
                                                    IsActive = arr[7] == "1"? "-" : "+" };

                reg.Count = ManagerDbQuery.GetCountWhere("suvd.projects", "dogovor_id", reg.RegId);
                regs.Add(reg);
            }
            return regs;
        }
    }
}