using DbLayer;
using DbLayer.Infrsrt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models.Home;
//using WebUI.Models.Home;
using WebUI.Models.QEntities;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        ISelectable _entity = null;
        public int pageSize = 30;

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

        //[HttpPost]
        //public ActionResult SearchCreditor(string orgName)
        //{
        //    var model = ActualCreditor.GetCreditorList().Where(cr => cr.OrgName.Contains(orgName));
        //    return View(model);
        //}

        public ActionResult Regs(string creditorId)
        {
            ViewBag.Message = "creditorId - " + creditorId;
            var model = CreditorReg.GetRegList(creditorId);
            return PartialView(model.OrderByDescending(r => r.RegId).ToList());
        }

        public ActionResult Pins(string regId, int page = 1)
        {
            List<Pin> list = Pin.GetPinsByKey(PinSearhKey.RegId, regId);

            PinListViewModel model = new PinListViewModel
            {
                PinSet = list.OrderBy(pin => pin.BusinessN).
                              Skip((page - 1) * pageSize).
                              Take(pageSize).ToList(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,                    
                    TotalItems = list.Count(),                    
                }
            };

            if (model.PagingInfo.TotalItems < 100)
                model.PagingInfo.ItemsPerPage = 30;
            else if (model.PagingInfo.TotalItems < 1000)
                model.PagingInfo.ItemsPerPage = 100;
            else model.PagingInfo.ItemsPerPage = model.PagingInfo.TotalItems / 10;

            return PartialView(model);            
        }

        public ActionResult PinDetails(string pin)
        {
            var model = Pin.GetPinsByKey(PinSearhKey.Pin, pin);
            return PartialView(model[0]);
        }

        public ActionResult Search(Pin pin)
        {

            List<Pin> model = new List<Pin>();

            if (CheckInputData(pin))
            {
                string valuePin = pin.BusinessN;
                string valueDog = pin.DebtDogovorN;
                string valueInn = pin.Inn;

                if (valuePin != null)
                    model = Pin.GetPinsByKey(PinSearhKey.Pin, valuePin);
                else if (valueDog != null)
                    model = Pin.GetPinsByKey(PinSearhKey.DebtDogovorN, valueDog);
                else
                    model = Pin.GetPinsByKey(PinSearhKey.Inn, valueInn);

                return PartialView(model);
            }
            else
            {
                ViewBag.Search = "Некорректные данные для поиска";
                return PartialView(null);
            }            
        }

        private bool CheckInputData(Pin pin)
        {
            int count = 0;
            if (pin.BusinessN != null) count++;
            if (pin.DebtDogovorN != null) count++;
            if (pin.Inn != null) count++;

            if (count == 1)
                return true;
            else
            {
                return false;
            }
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