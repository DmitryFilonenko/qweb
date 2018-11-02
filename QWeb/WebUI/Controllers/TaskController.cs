//using EFLayer;
using EFModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class TaskController : Controller
    {
        public ActionResult Priority(int prior)
        {
            ViewBag.Prior = prior;
            return View();
        }

        [HttpPost]
        public ActionResult Priority(string priorityValue, HttpPostedFileBase uploadfile)
        {
            if (uploadfile == null)
            {
               return RedirectToAction("Index", "Home");
            }

            string path = SaveFileWithGuidName(uploadfile);
            try
            {
                using (var context = new DBContext())
                {
                   // context.
                }
               
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                System.IO.File.Delete(path);
            }
            
            return View();
        }

        private string SaveFileWithGuidName(HttpPostedFileBase uploadfile)
        {
            string fileName = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(uploadfile.FileName);
            fileName += extension;
            uploadfile.SaveAs(Server.MapPath("/Uploads/" + fileName));
            return Server.MapPath("/Uploads/" + fileName);
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