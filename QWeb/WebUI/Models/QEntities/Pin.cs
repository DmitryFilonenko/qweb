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
        #region Fields
        [Display(Name = "Фамилия:")]
        public string NameF { get; set; }

        [Display(Name = "Имя:")]
        public string NameI { get; set; }

        [Display(Name = "Отчество:")]
        public string NameO { get; set; }

        [Display(Name = "Инн:")]
        public string Inn { get; set; }

        [Display(Name = "Project ID:")]
        public string ProjectId { get; set; }

        [Display(Name = "Пин:")]
        public string BusinessN { get; set; }

        [Display(Name = "Договор:")]
        public string DebtDogovorN { get; set; }

        [Display(Name = "Просроченная задолженность:")]
        public string DebtRest { get; set; }

        [Display(Name = "Общая задолженность:")]
        public string TotalRest { get; set; }

        [Display(Name = "Архив:")]
        public string ArchiveFlag { get; set; }

        [Display(Name = "АП:")]
        public string Ap { get; set; }

        [Display(Name = "АВР:")]
        public string Avr { get; set; }

        [Display(Name = "АФ:")]
        public string Af { get; set; }

        [Display(Name = "АР:")]
        public string Aw { get; set; }

        [Display(Name = "Маркер1:")]
        public string Marker1 { get; set; }

        [Display(Name = "Период:")]
        public string Period1 { get; set; }

        [Display(Name = "Маркер2:")]
        public string Marker2 { get; set; }

        [Display(Name = "Период:")]
        public string Period2 { get; set; }

        [Display(Name = "Кредитор:")]
        public string CreditorName { get; set; }

        [Display(Name = "suvd ID:")]
        public string CreditorIdLong { get; set; }

        [Display(Name = "eadr ID:")]
        public string CreditorIdShort { get; set; }

        [Display(Name = "Реестр:")]
        public string RegName { get; set; }

        [Display(Name = "suvd ID:")]
        public string IdLong { get; set; }

        [Display(Name = "eadr ID:")]
        public string IdShort { get; set; }

        [Display(Name = "Начало:")]
        public string Start { get; set; }

        [Display(Name = "Окончание:")]
        public string Stop { get; set; }


        #endregion

        public static List<Pin> GetPinsByKey(PinSearhKey searhKey, string value)
        {
            List<Pin> pins = new List<Pin>();
            string path = HostingEnvironment.MapPath(@"~/App_Data/Sql_files/select_pin_by.sql");
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
                case PinSearhKey.Inn:
                    cond = " and c.inn = " + value;
                    break;
                case PinSearhKey.RegId:
                    cond = " and p.dogovor_id = " + value;
                    break;
            }
            string query = String.Format("{0} {1}", text, cond);

            List<string> list = ManagerSqlFiles.GetPins(query, 26);

            foreach (var item in list)
            {
                string[] arr = item.Split('#');
                //Pin p = new Pin { NameF = arr[0], NameI = arr[1], NameO = arr[2], Inn = arr[3], ProjectId = arr[4], BusinessN = arr[5],
                //                  DebtDogovorN = arr[6], DebtRest = arr[7], TotalRest = arr[8], ArchiveFlag = arr[9] == "1"? "+" : "-",
                //                  Ap = arr[10], Avr = arr[11], Af = arr[12], Aw = arr[13], Marker1 = arr[14], Period1 = arr[15], Marker2 = arr[16],
                //                  Period2 = arr[17], CreditorName = arr[18], CreditorIdLong = arr[19], CreditorIdShort = arr[20], RegName = arr[21],
                //                  IdLong = arr[22], IdShort = arr[23], Start = arr[24].Substring(0, 10), Stop = arr[25].Substring(0, 10)
                //};

                Pin p = new Pin();
                p.NameF = arr[0];
                p.NameI = arr[1];
                p.NameO = arr[2];
                p.Inn = arr[3];
                p.ProjectId = arr[4];
                p.BusinessN = arr[5];
                p.DebtDogovorN = arr[6];
                p.DebtRest = arr[7];
                p.TotalRest = arr[8];
                p.ArchiveFlag = arr[9] == "1" ? "+" : "-";
                p.Ap = arr[10];
                p.Avr = arr[11];
                p.Af = arr[12];
                p.Aw = arr[13];
                p.Marker1 = arr[14];
                p.Period1 = arr[15];
                p.Marker2 = arr[16];
                p.Period2 = arr[17];
                p.CreditorName = arr[18];
                p.CreditorIdLong = arr[19];
                p.CreditorIdShort = arr[20];
                p.RegName = arr[21];
                p.IdLong = arr[22];
                p.IdShort = arr[23];
                p.Start = arr[24].Length > 10?  arr[24].Substring(0, 10) : "";
                p.Stop = arr[25].Length > 10 ? arr[25].Substring(0, 10) : "";


                if (p.Period1 == "c  no ")
                    p.Period1 = "";
                pins.Add(p);
            }
            return pins;            
        }
    }
}