using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Infrastructure.HelperMethods;
using WebUI.Models.Chess;
using WebUI.Models.Home.User;

namespace WebUI.Controllers
{
    public class ChessController : Controller
    {
        // GET: Chess
        public ActionResult Chess()
        {
            ViewBag.Random = GetRandomNumber(0, 2);
            ChessResultModel model = new ChessResultModel();
            return PartialView(model);
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
        public ActionResult Result(string isOk, string field, string deskSide, string time)
        {
            if (isOk == "true")
            {
                ChessResult chessResult = new ChessResult
                {
                    DeskSide = deskSide,
                    Field = field,
                    Player = UserModel.GetUserLogin(),
                    Result = time.Contains(".") ? time.Replace(".", ",") : time
                };
                ChessResultModel.AddRecord(chessResult);
            }            
            ChessResultModel model = new ChessResultModel();

            return PartialView(model);
        }
    }
}