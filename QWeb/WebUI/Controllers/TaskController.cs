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

        public ActionResult Notes(string projectId, string startDate, string stopDate, string regId, string regName, string credId, string credName)
        {
            string start = startDate;
            string stop = stopDate;

            if (start == null) return View();

            if (start.Contains("-"))
            {
                start = ReFormatDate(startDate);
                stop = ReFormatDate(stopDate);
            }

            ViewBag.RegId = regId;
            ViewBag.RegName = regName;
            ViewBag.CreditorId = credId;
            ViewBag.CrediotrName = credName;

            List<Note> model = Note.GetNotes(projectId, start, stop);

            return PartialView(model);
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