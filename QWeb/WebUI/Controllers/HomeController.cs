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
        ISelectable _entity = null; 

        public ActionResult Index()
        {
            _entity = new QTask();
            List<IDbEntity> taskList = _entity.GetAllFieldsList(); 
            ViewBag.Tasks = new SelectList(taskList, "Id", "Name");
            return View();
        }
        
        public ActionResult SubTasks(string id)
        {
            _entity = new QTask();
            IDbEntity curTask = _entity.GetSingleRecordById(id);
            QTask task = (QTask)curTask;
            ViewBag.TaskName = task.Name;

            _entity = new QSubTask();
            List<IDbEntity> sTaskList = _entity.GetFieldsListById(id);
            List<QSubTask> subTaskList = new List<QSubTask>(); 
            foreach (var item in sTaskList)
            {
                subTaskList.Add((QSubTask)item);
            }
            return PartialView(subTaskList.OrderBy(s => s.Name).ToList()); 
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