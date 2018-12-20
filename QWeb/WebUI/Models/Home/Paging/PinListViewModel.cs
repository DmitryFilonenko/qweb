using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebUI.Models.QEntities;
using WebUI.Models.QEntities.QPins;

namespace WebUI.Models.Home
{
    public class PinListViewModel
    {
        public List<QPinContact> PinSet { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}