using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Infrastructure.HelperMethods
{
    public static class AccessHelper
    {
        public static string OperationAccess(this HtmlHelper html, string userLogin, string creator)
        {
            string str = "";            

            if (userLogin.Substring(userLogin.LastIndexOf('\\') + 1) == creator)
                str = "btn-link";
            else
                str = "disabled";

            return str;
        }
    }
}