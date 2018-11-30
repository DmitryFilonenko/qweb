using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebUI.Models.Home;

namespace WebUI.Infrastructure.HelperMethods
{
    public static class PagingHelper
    {
        public static string GetPageClass(this HtmlHelper html, PagingInfo pagingInfo, int currentItem)
        {
            string str = "";

            if (pagingInfo.CurrentPage == currentItem)
                str = "btn btn-primary";
            else
                str = "btn btn-outline-primary";

            return str;
        }
    }
}