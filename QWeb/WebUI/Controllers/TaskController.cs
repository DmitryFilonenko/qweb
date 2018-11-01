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
            //using (var context = new DBContext())
            //{
            //    List<TASK> taskList = context.TASKS.ToList();
            //    ViewBag.Tasks = new SelectList(taskList, "ID", "NAME");
            //}
            ViewBag.Prior = prior;

            return View();
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
}