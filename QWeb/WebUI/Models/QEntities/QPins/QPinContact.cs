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
        [Display(Name = "Project ID:")]
        public string ProjectId { get; set; }
        [Display(Name = "Пин:")]
        public string BusinessN { get; set; }
        [Display(Name = "Договор:")]
        public string DebtDogovorN { get; set; }
        public string DebtContactId { get; set; }
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
        [Display(Name = "ИНН:")]
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
            string[] arr;
            try
            {
                string[] fields = new string[] { "name_f", "name_i", "name_o" };
                string rec = ManagerDbQuery.GetFieldsListById("suvd.contacts", fields, DebtContactId).First();
                arr = rec.Split('#');
                NameF = arr[0];
                NameI = arr[1];
                NameO = arr[2];
            }
            catch (Exception)
            {
                throw;
            }            
        }
    }
}