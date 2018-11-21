using DbLayer;
using DbLayer.Infrsrt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models.Home;
using WebUI.Models.QEntities;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            QTask task = new QTask();
            List<IDbEntity> taskList = task.GetAllFieldsList(); 
            ViewBag.Tasks = new SelectList(taskList, "Id", "Name");
            return View();
        }


        public ActionResult SubTasks(string id)
        {

            //List<AppSubTask> subTaskList = AppTaskManager.GetSubTaskListById(id);

            //List<AppTask> taskList = AppTaskManager.GetFieldsListById("tasks", new string[] { "name" }, id);

            //ViewBag.TaskName = AppTaskManager.GetFieldsListById  //  context.TASKS.Where(t => t.ID == id).First().NAME;

            return PartialView(/*subTaskList*/);
        }


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