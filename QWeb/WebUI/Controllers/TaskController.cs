using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class TaskController : Controller
    {
        
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