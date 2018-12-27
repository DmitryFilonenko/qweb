using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using DbLayer.Managers;
using WebUI.Infrastructure;
using WebUI.Models.Priority;

namespace WebUI.Controllers
{
    public class PriorityController : Controller
    {
        // GET: Priority
        public ActionResult Priority(string taskId)
        {
            Session["TaskId"] = taskId;
            return PartialView();
        }

        [HttpPost]
        public ActionResult Change(string priorityValue, HttpPostedFileBase uploadfile)
        {
            string whoUses = ManagerFileUpload.TakeTable(Session["TaskId"].ToString(), User.Identity.Name.Substring(User.Identity.Name.LastIndexOf('\\') + 1));
            if (whoUses != "ok")
            {
                Session["Message"] = String.Format("Таблица занята пользвотелем {0}.", whoUses);
                return RedirectToAction("Priority", new { taskId = Session["TaskId"] });
            }

            var fileName = Path.GetFileName(uploadfile.FileName);
            var path = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
            uploadfile.SaveAs(path);

            PriorityCommand qCommand = new PriorityCommand() {
                PriorityValue = priorityValue,
                PathToFile = path,
                TaskId = Session["TaskId"].ToString(),
                Data = System.IO.File.ReadAllLines(path/*, System.Text.Encoding.Default*/)
            }; 

            bool isDataCorrect = qCommand.CheckData(qCommand.Data);

            if(!isDataCorrect)
            {
                Session["Message"] = "В файле поданы некорректные данные.";
                return RedirectToAction("Priority", new { taskId = Session["TaskId"] });
            }

            qCommand.Act();

            return PartialView();
        }
    }
}