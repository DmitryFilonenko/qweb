using DbLayer;
//using EFLayer;
using EFModel;
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
                ViewBag.Tasks = new SelectList(taskList, "ID", "NAME");
            }
            
            return View();
        }


        public ActionResult SubTasks(int? id)
        {
            List<SUBTASK> subTaskList = new List<SUBTASK>();

            using (var context = new DBContext())
            {
                subTaskList = context.SUBTASKS.Where(s => s.TASK_ID == id).OrderBy(a => a.ID).ToList();
                ViewBag.TaskName = context.TASKS.Where(t => t.ID == id).First().NAME;
            }
            return PartialView(subTaskList);
        }

        //private MvcHtmlString CreateHtml(List<SUBTASK> subTasklist)
        //{
        //    TagBuilder ulTag = new TagBuilder("ul");
        //    foreach (var item in subTasklist)
        //    {
        //        TagBuilder liTag = new TagBuilder("li");
        //        TagBuilder aTeg = new TagBuilder("a");
        //        aTeg.MergeAttribute("href", "Home/ConcretTask");
        //        aTeg.MergeAttribute("subTaskId", item.ID.ToString());
        //        aTeg.InnerHtml = item.NAME.ToString();
        //        liTag.InnerHtml+=aTeg.ToString();
        //        ulTag.InnerHtml += liTag.ToString();
        //    }
        //    return new MvcHtmlString(ulTag.ToString());
        //}


        public string ConcretTask(string subTaskId)
        {
            return "ID подзадачи - " + subTaskId;
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