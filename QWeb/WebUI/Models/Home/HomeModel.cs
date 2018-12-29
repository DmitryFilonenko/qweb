using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebUI.Models.QEntities;

namespace WebUI.Models.Home
{
    public class HomeModel
    {        
        public List<QActualCreditor> CreditorList { get; set; }
        public List<QTask> TaskList { get; set; }
    }
}