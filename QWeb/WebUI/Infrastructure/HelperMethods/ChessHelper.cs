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
            string result = "";         
            

                
            TagBuilder t = new TagBuilder("input");
            t.MergeAttribute("type", "submit");
            t.MergeAttribute("class", "btn");
            string style = "width: 50px; height: 50px; border: inherit; border-color:black; ";

            //if (row < 2)
            //{
            //    style += side == ChessSide.white? "background-color: black;" : "background-color: white;";
            //}
            //if (row > 1 && row < 6)
            //{
            //    style += "background-color: bisque;";
            //}


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



            //if (row % 2 == 1)
            //{


            //    //style += side == ChessSide.white ? "background-color: red;" : "background-color: green;";
            //    style += "background-color: red;";
            //}
            //else
            //{

            //}

            if (row > 5)
            {
                style += side == ChessSide.white ? "background-color: white;" : "background-color: black;";
            }
            t.MergeAttribute("style", style);
            t.MergeAttribute("value", "");
            result = t.ToString(TagRenderMode.Normal);
          

            return MvcHtmlString.Create(result);



        }
    }
}