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
            projectId += "0";          

            return View();
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