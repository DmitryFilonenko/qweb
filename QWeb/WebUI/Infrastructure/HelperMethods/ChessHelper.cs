using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace WebUI.Infrastructure.HelperMethods
{
    public enum ChessSide { white, black }

    public static class ChessHelper
    {  

        public static MvcHtmlString ChessDeskBuilder(this HtmlHelper html, int row, int col, ChessSide side)
        {
            char[] charArr = new char[] { 'a','b','c','d','e','f','g','h'};

            TagBuilder t = new TagBuilder("input");
            t.MergeAttribute("type", "submit");
            t.MergeAttribute("class", "btn answer");
            string name;
            if (side == ChessSide.white)
            {
                name = charArr[col] + (7 - (row) + 1).ToString();
            }
            else
            {
                name = charArr[7 - col] + ((row) + 1).ToString();
            }            
            t.MergeAttribute("id", name);
            string style = "width: 45px; height: 45px; border: inherit; border-color:black; ";
            

            if (row % 2 == 1)
            {
                if (col % 2 == 1)
                {
                    style += "background-color: beige;";
                }
                else
                {
                    style += "background-color: burlywood;";
                }
            }
            else
            {
                if (col % 2 == 1)
                {
                    style += "background-color: burlywood;";
                }
                else
                {
                    style += "background-color: beige;";
                }
            }

            if (row > 5)
            {
                style += side == ChessSide.white ? "background-color: white;" : "background-color: dimgray;";
            }
            t.MergeAttribute("style", style);
            t.MergeAttribute("value", "");
            string result = t.ToString(TagRenderMode.Normal);
            return MvcHtmlString.Create(result);
        }
    }
}