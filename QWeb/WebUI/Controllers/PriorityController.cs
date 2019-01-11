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
using WebUI.Models.Home.User;

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
            var fileName = Guid.NewGuid().ToString(); 
            var path = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
            uploadfile.SaveAs(path);
            return path;
        }

        [HttpPost]
        public ActionResult PreChange(string priorityValue, HttpPostedFileBase uploadfile)
        {
            PreReport model = new PreReport() { PriorValue = priorityValue };
            string path = SaveFile(uploadfile);
            QUploadFileHandler fileHandler = new QUploadFileHandler(path);
            PriorityCommand qCommand = new PriorityCommand(priorityValue, Session["TaskId"].ToString(), path);
            try
            {
                string whoUses = qCommand.TaskHandler.BorrowTable(UserModel.GetUserLogin());
                if (whoUses != "ok")
                {                       // если таблица занята другим пользователем
                    Session["Message"] = String.Format("Таблица занята пользвотелем {0}.", whoUses);
                    return RedirectToAction("Priority", new { taskId = Session["TaskId"] });
                }

                if (!qCommand.FileHandler.CheckIsDigits())
                {                       // если в файле поданы не только числовые значения (ожидается список пинов)
                    Session["Message"] = "В файле поданы некорректные данные.";
                    return RedirectToAction("Priority", new { taskId = Session["TaskId"] });
                }

                if (!qCommand.FillTable())
                {
                                        // если произошла ошибка при заливке темповой таблицы
                    Session["Message"] = "Ошибка при заполнении таблицы.";
                    return RedirectToAction("Priority", new { taskId = Session["TaskId"] });
                }
                model.FileCount = qCommand.FileHandler.GetCount();
                model.WillUpdCount = qCommand.GetPreRes();
                Session["Command"] = qCommand;
            }
            catch (Exception ex)
            {
                QLoger.AddRecordToLog(UserModel.GetUserLogin(), "PreChange()", ex.Message, MessageType.Exception);
            }
            finally
            {
                qCommand.FileHandler.DeleteFile();
            }
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Change()
        {            
            try
            {
                PriorityCommand command = Session["PriorityCommand"] as PriorityCommand;
                Session["Command"] = null;

                command.TaskFinishsed += () =>
                {
                    command.FileHandler.DeleteFile();
                    command.TaskHandler.ReleaseTable();
                };


            }
            catch (Exception ex)
            {
                QLoger.AddRecordToLog(UserModel.GetUserLogin(), "Change()", ex.Message, MessageType.Exception);
            }
            return PartialView();
        }
    }
}