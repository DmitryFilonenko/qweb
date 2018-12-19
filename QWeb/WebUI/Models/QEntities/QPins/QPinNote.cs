using DbLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace WebUI.Models.QEntities.QPinDecorator
{
    public class QPinNote
    {
        #region Fields

        public string ProjectId { get; set; }
        public string BusinessN { get; set; }
        public string DebtDogovorN { get; set; }
        public string ArchiveFlag { get; set; }
        public string CreditorName { get; set; }
        public string CreditorIdLong { get; set; }
        public string CreditorIdShort { get; set; }
        public string RegName { get; set; }
        public string RegIdLong { get; set; }
        public string RegIdShort { get; set; }

        DateTime notesStart = DateTime.Today.AddDays(-7);
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime NotesStart { get { return notesStart; } set { notesStart = value; } }

        DateTime notesStop = DateTime.Today;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime NotesStop { get { return notesStop; } set { notesStop = value; } }

        #endregion

        public QPinNote(QPinBase pinBase)
        {
            this.ProjectId = pinBase.ProjectId;
            this.BusinessN = pinBase.BusinessN;
            this.DebtDogovorN = pinBase.DebtDogovorN;
            this.ArchiveFlag = pinBase.ArchiveFlag;
            this.CreditorName = pinBase.CreditorName;
            this.CreditorIdLong = pinBase.CreditorIdLong;
            this.CreditorIdShort = pinBase.CreditorIdShort;
            this.RegName = pinBase.RegName;
            this.RegIdLong = pinBase.RegIdLong;
            this.RegIdShort = pinBase.RegIdShort;
        }
    }
}