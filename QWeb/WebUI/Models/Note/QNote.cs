using DbLayer;
using DbLayer.Infrsrt;
using DbLayer.Managers;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace WebUI.Models.QEntities
{
    public enum NoteSearchKey { ProjectId, NoteId }

    public class QNote
    {
        #region Fields

        [HiddenInput(DisplayValue = false)]
        public string NoteId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string EmployeerId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ProjectId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string BusinessN { get; set; }

        [Display(Name = "Начало:")]
        [ReadOnly(true)]
        public string StartDate { get; set; }

        [Display(Name = "Окончание:")]
        [ReadOnly(true)]
        public string StopDate { get; set; }

        [Display(Name = "Пользователь:")]
        [ReadOnly(true)]
        public string EmployeerName { get; set; }

        [Display(Name = "Логин:")]
        [ReadOnly(true)]
        public string EmployeerLogin { get; set; }

        [Display(Name = "Зметка:")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        #endregion

        public static List<QNote> GetNotes(NoteSearchKey searhKey, string keyValue, string startDate = "", string stopDate = "")
        {
            List<QNote> notes = new List<QNote>();
            string path = HostingEnvironment.MapPath(@"~/App_Data/Sql_files/notes.sql");
            string text = File.ReadAllText(path);

            string query = "";

            switch (searhKey)
            {
                case NoteSearchKey.ProjectId:
                    query = text + String.Format(" and t.project_id = {0} and trunc(t.result_time) between '{1}' and '{2}'", keyValue, startDate, stopDate);
                    break;
                case NoteSearchKey.NoteId:
                    query = text + String.Format(" and t.id = {0}", keyValue);
                    break;
            }            

            List<string> list = ManagerDbQuery.GetItems(query, 9);

            foreach (var item in list)
            {
                string[] arr = item.Split('#');
                QNote p = new QNote { NoteId = arr[0], EmployeerId = arr[1], ProjectId = arr[2], BusinessN = arr[3], StartDate = arr[4].ToString(), StopDate = arr[5], EmployeerName = arr[6], EmployeerLogin = arr[7], Message = arr[8] };
                notes.Add(p);
            }
            return notes;
        }

        public static bool SaveNote(string noteId, string comment)
        {
            bool res = false;
            try
            {
                List<ProcParam> args = new List<ProcParam> {
                    new ProcParam { Name = "note_id", Type = OracleDbType.Decimal, Direction = ParameterDirection.Input, Value = noteId },
                    new ProcParam { Name = "txt", Type = OracleDbType.Varchar2, Direction = ParameterDirection.Input, Value = comment }
                };

                ManagerPlProc.ExecProc("q_note.update_note", args);
                res = true;
            }
            catch (Exception)
            {
                throw;
            }

            return res;
        }

        public static bool DeleteNote(string noteId)
        {
            bool res = false;
            try
            {
                List<ProcParam> args = new List<ProcParam> {
                    new ProcParam { Name = "note_id", Type = OracleDbType.Decimal, Direction = ParameterDirection.Input, Value = noteId }                    
                };

                ManagerPlProc.ExecProc("q_note.delete_note", args);
                res = true;
            }
            catch (Exception)
            {
                throw;
            }

            return res;
        }
    }
}