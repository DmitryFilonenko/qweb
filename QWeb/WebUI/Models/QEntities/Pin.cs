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
    public enum PinSearhKey { Pin, DebtDogovorN, RegId, Inn }

    public class Pin
    {
        [Display(Name = "Фамилия")]
        public string NameF { get; set; }

        [Display(Name = "Имя")]
        public string NameI { get; set; }

        [Display(Name = "Отчество")]
        public string NameO { get; set; }

        [Display(Name = "Инн")]
        public string Inn { get; set; }

        [Display(Name = "ProjectId")]
        public string ProjectId { get; set; }

        [Display(Name = "Пин")]
        public string BusinessN { get; set; }

        [Display(Name = "Номер договора")]
        public string DebtDogovorN { get; set; }

        [Display(Name = "Просроченная задолженность")]
        public string DebtRest { get; set; }

        [Display(Name = "Общая задолженность")]
        public string TotalRest { get; set; }

        [Display(Name = "Архив")]
        public string ArchiveFlag { get; set; }

        public static List<Pin> GetPinsByKey(PinSearhKey searhKey, string value)
        {
            List<Pin> pins = new List<Pin>();
            string path = HostingEnvironment.MapPath(@"~/App_Data/Sql_files/select_pin_by.sql");
            string text = File.ReadAllText(path);
            string cond = "";
            switch (searhKey)
            {
                case PinSearhKey.Pin:
                    cond = " and p.business_n = ";
                    break;
                case PinSearhKey.DebtDogovorN:
                    cond = " and p.debt_dogovor_n = ";
                    break;
                case PinSearhKey.Inn:
                    cond = " and c.inn = ";
                    break;
                case PinSearhKey.RegId:
                    cond = " and p.dogovor_id = ";
                    break;
            }
            string query = String.Format("{0} {1} {2}", text, cond, value);

            List<string> list = ManagerSqlFiles.GetPins(query, 10);

            foreach (var item in list)
            {
                string[] arr = item.Split('#');
                Pin p = new Pin { NameF = arr[0], NameI = arr[1], NameO = arr[2], Inn = arr[3], ProjectId = arr[4], BusinessN = arr[5], DebtDogovorN = arr[6], DebtRest = arr[7], TotalRest = arr[8], ArchiveFlag = arr[9] == "1"? "+" : "-" };
                pins.Add(p);
            }
            return pins;            
        }
    }
}