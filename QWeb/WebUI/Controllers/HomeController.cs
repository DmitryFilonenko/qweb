using DbLayer;
using DbLayer.Infrsrt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using WebUI.Models.Home;
using WebUI.Models.QEntities;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        ISelectable _entity = null; 

        public ActionResult Index()
        {
            Pin pinToSeach = new Pin();
            ViewBag.PinToSeach = pinToSeach;

            _entity = new QTask();
            List<IDbEntity> taskList = _entity.GetAllFieldsList(); 
            ViewBag.Tasks = new SelectList(taskList, "Id", "Name");
            var model = ActualCreditor.GetCreditorList();
            
            return View(model);
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
            int count = ManagerDbQuery.GetCount(list);
            ViewBag.Message = "В таблице " + list + " содержится " + count + " записей.";
            return View();
        }

        public ActionResult Regs(string creditorId)
        {
            ViewBag.Message = "creditorId - " + creditorId;
            var model = CreditorReg.GetRegList(creditorId);
            return PartialView(model.OrderByDescending(r=>r.RegId).ToList());
        }

        public ActionResult Pins(string regId)
        {
            var model = Pin.GetPinsByKey(PinSearhKey.RegId, regId);
            return PartialView(model.OrderByDescending(p => p.BusinessN).ToList());
        }

        public ActionResult PinDetails(string pin)
        {
            var model = Pin.GetPinsByKey(PinSearhKey.Pin, pin);
            return PartialView(model[0]);
        }

        public ActionResult Search(Pin pin)
        {
            string valuePin = pin.BusinessN;
            string valueDog = pin.DebtDogovorN;
            List<Pin> model = new List<Pin>();
            if (valuePin != null)
                model = Pin.GetPinsByKey(PinSearhKey.Pin, valuePin);
            else
                model = Pin.GetPinsByKey(PinSearhKey.DebtDogovorN, valueDog);

            return PartialView(model);
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