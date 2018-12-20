using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Infrastructure;
using WebUI.Models.QEntities;
using WebUI.Models.QEntities.QPins;

namespace WebUI.Controllers
{
    public class NoteController : Controller
    {
        #region Notes

        public ActionResult Note(string id, string taskName)
        {
            ViewBag.TaskName = taskName;
            return PartialView();
        }

        public ActionResult NoteDates(string businessN)
        {
            QPinBase basePin = QPinBase.GetPinsByKey(PinSearhKey.Pin, businessN).First();
            QPinNote pinNote = new QPinNote(basePin);
            return PartialView(pinNote);
        }        

        public ActionResult Notes(string projectId, string startDate, string stopDate)
        {
            string start = startDate;
            string stop = stopDate;

            if (start.Contains("-"))
            {
                start = ReFormatDate(startDate);
                stop = ReFormatDate(stopDate);
            }

            QPinBase pin = QPinBase.GetPinsByKey(PinSearhKey.ProjectId, projectId).First();
            QPinNote notePin = new QPinNote(pin);

            ViewBag.Pin = notePin;

            List<QNote> model = QNote.GetNotes(NoteSearchKey.ProjectId, projectId, start, stop);

            return PartialView(model.OrderByDescending(r => r.StopDate).ToList());
        }

        public ActionResult NoteEdit(string noteId, string startDate, string stopDate)
        {
            var notes = QNote.GetNotes(NoteSearchKey.NoteId, noteId);
            QNote note = notes.First();

            ViewBag.Start = startDate;
            ViewBag.Stop = stopDate;

            return PartialView(note);
        }

        public ActionResult NoteSave(string projectId, string noteId, string startDate, string stopDate, string Message)
        {
            try
            {
                bool res = QNote.SaveNote(noteId, Message);

                if (res)
                {
                    Session["Message"] = "Успешное сохранение";
                    QLoger.AddRecordToLog(User.Identity.Name, "Изменение заметок", "projectId - " + projectId + ", ID заметки - " + noteId, "0");
                }
                else
                    Session["Message"] = "Ошибка при сохранении";
            }
            catch (Exception ex)
            {
                QLoger.AddRecordToLog(User.Identity.Name, "Изменение заметок", "projectId - " + projectId + ", ID заметки - " + noteId + Environment.NewLine + ex.Message, "1");
            }
            return RedirectToAction("Notes", new { projectId, startDate, stopDate });
        }

        public ActionResult NoteDelete(string projectId, string noteId, string startDate, string stopDate)
        {
            try
            {
                bool res = QNote.DeleteNote(noteId);

                if (res)
                {
                    Session["Message"] = "Успешное удаление";
                    QLoger.AddRecordToLog(User.Identity.Name, "Удаление заметок", "projectId - " + projectId + ", ID заметки - " + noteId, "0");
                }
                else
                    Session["Message"] = "Ошибка при удалении";
            }
            catch (Exception ex)
            {
                QLoger.AddRecordToLog(User.Identity.Name, "Удаление заметок", "projectId - " + projectId + ", ID заметки - " + noteId + Environment.NewLine + ex.Message, "1");
            }

            return RedirectToAction("Notes", new { projectId, startDate, stopDate });
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