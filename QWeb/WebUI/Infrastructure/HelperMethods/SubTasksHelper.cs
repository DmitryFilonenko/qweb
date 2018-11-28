using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Infrastructure.HelperMethods
{
    public static class SubTasksHelper
    {
        // Для того чтобы использовать данный вспомогательный метод, необходимо импортировать пространство имен WebUI.Infrastructure.HelperMethods.
        // Пространство имен импортируем с помощью файла Views/web.config
        public static string GetAlertType(this HtmlHelper helper, string isArchive)
        {
            if (isArchive == "-")
                return "alert-success";
            else
                return "alert-warning";
        }
    }
}