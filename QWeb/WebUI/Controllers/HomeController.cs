using DbLayer;
using EFLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models.Home;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            using (var context = new DBContext())
            {
                List<TASK> taskList = context.TASKS.ToList();                
                //List<string> strList = new List<string>();

                //foreach (var item in taskList)
                //{
                //    strList.Add(item.NAME);
                //}
                //SelectList selectList = new SelectList(taskList);
                ViewBag.Tasks = new SelectList(taskList, "ID", "NAME");
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult Index(string list)
        {
            int count = ManagerDb.GetCount(list);
            ViewBag.Message = "В таблице " + list + " содержится " + count + " записей.";
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}