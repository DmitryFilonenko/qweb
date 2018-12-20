using DbLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace WebUI.Models.QEntities.QPins
{

    public enum PinSearhKey { Pin, DebtDogovorN, RegId, Inn, ProjectId }

    public class QPinBase
    {       
        #region Fields
       
        [Display(Name = "Project ID:")]
        public string ProjectId { get; set; }

        [Display(Name = "Пин:")]
        public string BusinessN { get; set; }

        [Display(Name = "Договор:")]
        public string DebtDogovorN { get; set; }

        public string DebtContactId { get; set; }

        [Display(Name = "Архив:")]
        public string ArchiveFlag { get; set; }
        
        [Display(Name = "Кредитор:")]
        public string CreditorName { get; set; }

        [Display(Name = "suvd ID:")]
        public string CreditorIdLong { get; set; }

        [Display(Name = "eadr ID:")]
        public string CreditorIdShort { get; set; }

        [Display(Name = "Реестр:")]
        public string RegName { get; set; }

        [Display(Name = "suvd ID:")]
        public string RegIdLong { get; set; }

        [Display(Name = "eadr ID:")]
        public string RegIdShort { get; set; }        

        #endregion

        public static List<QPinBase> GetPinsByKey(PinSearhKey searhKey, string value)
        {
            List<QPinBase> pins = new List<QPinBase>();
            string path = HostingEnvironment.MapPath(@"~/App_Data/Sql_files/get_pin_base.sql");
            string text = File.ReadAllText(path);
            string cond = "";
            switch (searhKey)
            {
                case PinSearhKey.Pin:
                    cond = " and p.business_n = " + value;
                    break;
                case PinSearhKey.DebtDogovorN:
                    cond = " and p.debt_dogovor_n = '" + value + "'";
                    break;
                case PinSearhKey.RegId:
                    cond = " and p.dogovor_id = " + value;
                    break;
                case PinSearhKey.ProjectId:
                    cond = " and p.id = " + value;
                    break;
            }
            string query = String.Format("{0} {1}", text, cond);

            List<string> list = ManagerDbQuery.GetItems(query, 11);

            foreach (var item in list)
            {
                string[] arr = item.Split('#');

                QPinBase p = new QPinBase();
                p.ProjectId = arr[0];
                p.BusinessN = arr[1];
                p.DebtDogovorN = arr[2];
                p.DebtContactId = arr[3];
                p.ArchiveFlag = arr[4] == "1" ? "+" : "-";
                p.CreditorName = arr[5];
                p.CreditorIdLong = arr[6];
                p.CreditorIdShort = arr[7];
                p.RegName = arr[8];
                p.RegIdLong = arr[9];
                p.RegIdShort = arr[10];

                pins.Add(p);
            }
            return pins;
        }
    }
    
}