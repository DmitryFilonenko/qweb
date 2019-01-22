using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebUI.Infrastructure.HelperMethods;

namespace WebUI.Models.Chess
{
    public class ChessResult
    {
        public string Player { get; set; }
        public string Result { get; set; }
        public string Field { get; set; }
        public string DeskSide { get; set; }
        public string Dt { get; set; }
    }
}