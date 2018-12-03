using DbLayer.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace WebUI.Models.QEntities
{
    public class Note
    {
        #region Fields
        
        public string NoteId { get; set; }

        public string EmployeerId { get; set; }

        [Display(Name = "Начало:")]
        public string StartDate { get; set; }

        [Display(Name = "Окончание:")]
        public string StopDate { get; set; }

        [Display(Name = "Пользователь:")]
        public string EmployeerName { get; set; }

        [Display(Name = "Зметка:")]
        public string Message { get; set; }

        #endregion

        public static List<Note> GetNotes(string projectId, string startDate, string stopDate)
        {
            List<Note> notes = new List<Note>();
            string path = HostingEnvironment.MapPath(@"~/App_Data/Sql_files/notes.sql");
            string text = File.ReadAllText(path);            
            string query = String.Format(" and t.project_id = {0} and trunc(t.result_time) between '{1}' and '{2}'", projectId, startDate, stopDate);

            List<string> list = ManagerSqlFiles.GetItems(query, 6);

            foreach (var item in list)
            {
                string[] arr = item.Split('#');
                Note p = new Note { NoteId = arr[0], EmployeerId = arr[1], StartDate = arr[2], StopDate = arr[3], EmployeerName = arr[4], Message = arr[5] };

                notes.Add(p);
            }
            return notes;
        }
    }
}