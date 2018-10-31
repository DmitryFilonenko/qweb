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
        public static MvcHtmlString UnorderedList(this HtmlHelper helper, string[] items)
        {
            TagBuilder tag = new TagBuilder("ul");
            foreach (var item in items)
            {
                TagBuilder liTag = new TagBuilder("li");
                liTag.SetInnerText(item);
                tag.InnerHtml += liTag.ToString();
            }
            return new MvcHtmlString(tag.ToString());
        }
    }
}