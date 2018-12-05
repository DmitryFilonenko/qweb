using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models.QEntities;

namespace WebUI.Controllers
{
    public class TaskController : Controller
    {
        #region Priority
        public ActionResult Priority( int prior)
        {
            ViewBag.Prior = prior;

            return View();
        }

        [HttpPost]
        public ActionResult Priority(string priorityValue, HttpPostedFileBase uploadfile)
        {            
            uploadfile.SaveAs(Server.MapPath("/Uploads/" + uploadfile.FileName));
            return View();
        }
        #endregion


        #region Notes

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

            var pins = Pin.GetPinsByKey(PinSearhKey.ProjectId, projectId);
            Pin pin = pins.First();

            ViewBag.Pin = pin;

            List<Note> model = Note.GetNotes(NoteSearchKey.ProjectId, projectId, start, stop);

            return PartialView(model);
        }

        public ActionResult NoteEdit(Pin pin, string noteId)
        {
            var notes = Note.GetNotes(NoteSearchKey.NoteId, noteId);
            Note note = notes.First();

            return PartialView(note);
        }

        public ActionResult NoteSave(string BusinessN, string noteId, string Message)
        {
            bool res = Note.SaveNote(noteId, Message);
            
            if (res)
                Session["Message"] = "Успешное сохранение";
            else
                Session["Message"] = "Ошибка при сохранении";
            
            string pin = BusinessN;

            return RedirectToAction("PinDetails", "Home",  new { pin } );
        }

        public ActionResult NoteDelete(string BusinessN, string noteId)
        {
            bool res = Note.DeleteNote(noteId);

            if (res)
                Session["Message"] = "Успешное удаление";
            else
                Session["Message"] = "Ошибка при удалении";

            string pin = BusinessN;

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
    
    //Priority
    //Priority
    //Priority
    //Experiment
    //Draft
    //Stop
    //LetterPinList
    //LetterRegs
    //LetterOther
}