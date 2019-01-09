using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using DbLayer.Managers;
using WebUI.Infrastructure;
using WebUI.Models.Priority;
using WebUI.Infrastructure.QComands;

namespace WebUI.Controllers
{
    public class PriorityController : Controller
    {

        public ActionResult Priority(string taskId)
        {
            Session["TaskId"] = taskId;
            TodayChanges todayChanges = new TodayChanges();
            var model = todayChanges.ChangesList;

            return PartialView(model.OrderBy(t => t.PriorValue));
        }



        string SaveFile(HttpPostedFileBase uploadfile)
        {
            var fileName = Path.GetFileName(uploadfile.FileName);
            var path = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
            uploadfile.SaveAs(path);
            return path;
        }

        [HttpPost]
        public ActionResult PreChange(string priorityValue, HttpPostedFileBase uploadfile)
        {
            string path = SaveFile(uploadfile);
            QUploadFileHandler fileHandler = new QUploadFileHandler(path);
            
            PriorityCommand qCommand = new PriorityCommand()
            {
                FileHandler = fileHandler,
                PriorityValue = priorityValue,
                PathToFile = path,
                TaskId = Session["TaskId"].ToString(),
                Data = System.IO.File.ReadAllLines(path, System.Text.Encoding.Default)
            };

            string whoUses = qCommand.BorrowTable(User.Identity.Name.Substring(User.Identity.Name.LastIndexOf('\\') + 1));
            if (whoUses != "ok")
            {                       // если таблица занята другим пользователем
                Session["Message"] = String.Format("Таблица занята пользвотелем {0}.", whoUses);
                return RedirectToAction("Priority", new { taskId = Session["TaskId"] });
            }

            if(!qCommand.FileHandler.CheckIsDigits())
            {                       // если в файле поданы не только числовые значения (ожидается список пинов)
                Session["Message"] = "В файле поданы некорректные данные.";
                return RedirectToAction("Priority", new { taskId = Session["TaskId"] });
            }

            qCommand.Act();

            return PartialView();
        }
    }
}