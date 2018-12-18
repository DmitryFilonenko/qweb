using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Infrastructure;
using WebUI.Models.QEntities;

namespace WebUI.Controllers
{
    public class NoteController : Controller
    {
        #region Notes

        public ActionResult TargetPin()
        {
            return PartialView();
        }

        public ActionResult Note(string id, string taskName)
        {
            ViewBag.TaskName = taskName;
            return PartialView();
        }

        public ActionResult Notes(string projectId, string startDate, string stopDate)
        {
            string start = startDate;
            string stop = stopDate;

            if (start == null) return View();

            if (start.Contains("-"))
            {
                start = ReFormatDate(startDate);
                stop = ReFormatDate(stopDate);
            }

            var pins = QPin.GetPinsByKey(PinSearhKey.ProjectId, projectId);
            QPin pin = pins.First();

            ViewBag.Pin = pin;

            List<QNote> model = QNote.GetNotes(NoteSearchKey.ProjectId, projectId, start, stop);

            return PartialView(model);
        }

        public ActionResult NoteEdit(QPin pin, string noteId)
        {
            var notes = QNote.GetNotes(NoteSearchKey.NoteId, noteId);
            QNote note = notes.First();

            return PartialView(note);
        }

        public ActionResult NoteSave(string BusinessN, string noteId, string Message)
        {
            try
            {
                bool res = QNote.SaveNote(noteId, Message);

                if (res)
                {
                    Session["Message"] = "Успешное сохранение";
                    QLoger.AddRecordToLog(User.Identity.Name, "Изменение заметок", "Пин - " + BusinessN + ", ID заметки - " + noteId, "0");
                }
                else
                    Session["Message"] = "Ошибка при сохранении";
            }
            catch (Exception ex)
            {
                QLoger.AddRecordToLog(User.Identity.Name, "Изменение заметок", "Пин - " + BusinessN + ", ID заметки - " + noteId + Environment.NewLine + ex.Message, "1");
            }
            string pin = BusinessN;

            return RedirectToAction("PinDetails", "Home", new { pin });
        }

        public ActionResult NoteDelete(string pin, string noteId)
        {
            try
            {
                bool res = QNote.DeleteNote(noteId);

                if (res)
                {
                    Session["Message"] = "Успешное удаление";
                    QLoger.AddRecordToLog(User.Identity.Name, "Удаление заметок", "Пин - " + pin + ", ID заметки - " + noteId, "0");
                }
                else
                    Session["Message"] = "Ошибка при удалении";
            }
            catch (Exception ex)
            {
                QLoger.AddRecordToLog(User.Identity.Name, "Удаление заметок", "Пин - " + pin + ", ID заметки - " + noteId + Environment.NewLine + ex.Message, "1");
            }

            return RedirectToAction("PinDetails", "Home", new { pin });
        }

        private static string ReFormatDate(string startDate)
        {
            string d = startDate.Substring(startDate.LastIndexOf("-") + 1);
            startDate = startDate.Substring(0, startDate.LastIndexOf("-"));
            string m = startDate.Substring(startDate.LastIndexOf("-") + 1);
            string y = startDate.Substring(0, startDate.LastIndexOf("-"));

            return string.Format("{0}.{1}.{2}", d, m, y);
        }
        #endregion
    }
}