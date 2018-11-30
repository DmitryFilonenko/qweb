using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebUI.Models.QEntities;

namespace WebUI.Models.Home
{
    public class PinListViewModel
    {
        public List<Pin> PinSet { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}