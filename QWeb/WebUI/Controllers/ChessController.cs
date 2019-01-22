using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class ChessController : Controller
    {
        // GET: Chess
        public ActionResult Chess()
        {
            ViewBag.Random = GetRandomNumber(0, 2);
            return PartialView();
        }

        private static readonly Random getrandom = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom) 
            {
                return getrandom.Next(min, max);
            }
        }

        [HttpPost]
        public ActionResult Result(string field, string deskSide, string time)
        {
            //string buttonValue = Request["submitButton"];
            return PartialView();
        }
    }
}