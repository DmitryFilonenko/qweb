using DbLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace WebUI.Models.QEntities.QPins
{
    public class QPinContact
    {
        #region Fields
        public string ProjectId { get; set; }
        public string BusinessN { get; set; }
        public string DebtDogovorN { get; set; }
        public string DebtContactId { get; set; }
        public string ArchiveFlag { get; set; }
        public string CreditorName { get; set; }
        public string CreditorIdLong { get; set; }
        public string CreditorIdShort { get; set; }
        public string RegName { get; set; }
        public string RegIdLong { get; set; }
        public string RegIdShort { get; set; }
        public string Inn { get; set; }

        [Display(Name = "ФИО:")]
        public string NameF { get; set; }               
        public string NameI { get; set; }
        public string NameO { get; set; }
        #endregion

        public QPinContact(QPinBase pinBase)
        {
            this.ProjectId = pinBase.ProjectId;
            this.BusinessN = pinBase.BusinessN;
            this.DebtDogovorN = pinBase.DebtDogovorN;
            this.DebtContactId = pinBase.DebtContactId;
            this.Inn = pinBase.Inn;
            this.ArchiveFlag = pinBase.ArchiveFlag;
            this.CreditorName = pinBase.CreditorName;
            this.CreditorIdLong = pinBase.CreditorIdLong;
            this.CreditorIdShort = pinBase.CreditorIdShort;
            this.RegName = pinBase.RegName;
            this.RegIdLong = pinBase.RegIdLong;
            this.RegIdShort = pinBase.RegIdShort;

            GetDecorFields();
        }

        private void GetDecorFields()
        {
            string[] fields = new string[] { "inn", "name_f", "name_i", "name_o" };
            string rec = ManagerDbQuery.GetFieldsListById("suvd.contacts", fields, DebtContactId).First();
            string[] arr = rec.Split('#');
            Inn = arr[0];
            NameF = arr[1];
            NameI = arr[2];
            NameO = arr[3];
        }
    }
}