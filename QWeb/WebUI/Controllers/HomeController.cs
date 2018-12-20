using DbLayer;
using DbLayer.Infrsrt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models.Home;
using WebUI.Models.QEntities;
using WebUI.Models.QEntities.QPins;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        public int pageSize = 30;

        public ActionResult Index()
        {
            QPinBase pinToSeach = new QPinBase();
            ViewBag.PinToSeach = pinToSeach;

            QTask task = new QTask();
            List<QTask> taskList = task.GetTasksByLogin(User.Identity.Name.Substring(User.Identity.Name.LastIndexOf('\\') + 1));// GetAllFieldsList(); 

            HomeModel model = new HomeModel { CreditorList = QActualCreditor.GetCreditorList(), TaskList = taskList };
            return View(model);
        }              

        public ActionResult Regs(string creditorId)
        {
            ViewBag.Message = "creditorId - " + creditorId;
            var model = QCreditorReg.GetRegList(creditorId);
            return PartialView(model.OrderByDescending(r => r.RegId).ToList());
        }

        public ActionResult Pins(string regId, int page = 1)
        {
            List<QPinBase> list = QPinBase.GetPinsByKey(PinSearhKey.RegId, regId);
            List<QPinContact> contList = new List<QPinContact>();
            foreach (var item in list)
            {
                QPinContact cont = new QPinContact(item);
                contList.Add(cont);
            }

            PinListViewModel model = new PinListViewModel
            {
                PinSet = contList.OrderBy(pin => pin.BusinessN).
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
            var list = QPinBase.GetPinsByKey(PinSearhKey.Pin, pin);
            QPinContact model = new QPinContact(list[0]);
            return PartialView(model);
        }

        public ActionResult Search(QPinBase pin)
        { 
            //QPinContact contPin = new QPinContact(pin);
            List<QPinBase> list = new List<QPinBase>();

            if (CheckInputData(pin))
            {
                string valuePin = pin.BusinessN;
                string valueDog = pin.DebtDogovorN;
                string valueInn = pin.Inn;

                if (valuePin != null)
                    list = QPinBase.GetPinsByKey(PinSearhKey.Pin, valuePin);
                else if (valueDog != null)
                    list = QPinBase.GetPinsByKey(PinSearhKey.DebtDogovorN, valueDog);
                else
                    list = QPinBase.GetPinsByKey(PinSearhKey.Inn, valueInn);

                List<QPinContact> model = new List<QPinContact>();
                foreach (var item in list)
                {
                    QPinContact cont = new QPinContact(item);
                    model.Add(cont);
                }

                return PartialView(model);
            }
            else
            {
                ViewBag.Search = "Некорректные данные для поиска";
                return PartialView(null);
            }            
        }

        private bool CheckInputData(QPinBase pin)
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
    }
}