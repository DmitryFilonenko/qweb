using DbLayer;
using DbLayer.Infrsrt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models.QEntities
{
    public class QSubTask : ISelectable<QSubTask>
    {
        public string Id { get; set; }
        public string Task_id { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        public string Param { get; set; }

        public List<QSubTask> GetAllFieldsList()
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return ManagerDbQuery.GetCount("q_subtasks");
        }

        public List<QSubTask> GetFieldsListById( string idValue)
        {
            List<QSubTask> subTaskList = new List<QSubTask>();
            string[] fields = new string[] { "id", "task_id", "name", "action", "param" };

            List<string> resultlist = ManagerDbQuery.GetFieldsListById("q_subtasks", fields, idValue, "task_id");
            foreach (var item in resultlist)
            {
                string[] arr = item.Split('#');
                QSubTask subTask = new QSubTask { Id = arr[0], Task_id = arr[1], Name = arr[2], Action = arr[3], Param = arr[4] };
                subTaskList.Add(subTask);
            }
            return subTaskList;
        }

        public List<QSubTask> GetFieldsListById(string tableName, string[] fieldNameArr = null, string idValue = null, string idName = "id")
        {
            List<QSubTask> subTaskList = new List<QSubTask>();
            string[] fields = new string[] { "id", "task_id", "name", "action", "param" };

            List<string> resultlist = ManagerDbQuery.GetFieldsListById("q_subtasks", fields, idValue, "task_id");
            foreach (var item in resultlist)
            {
                string[] arr = item.Split('#');
                QSubTask subTask = new QSubTask { Id = arr[0], Task_id = arr[1], Name = arr[2], Action = arr[3], Param = arr[4] };
                subTaskList.Add(subTask);
            }
            return subTaskList;
        }

        //public List<QSubTask> GetFieldsListById(string tableName)
        //{
        //    List<QSubTask> subTaskList = new List<QSubTask>();
        //    string[] fields = new string[] { "id", "task_id", "name", "action", "param" };

        //    List<string> resultlist = ManagerDbQuery.GetFieldsListById("q_subtasks", fields, idValue, "task_id");
        //    foreach (var item in resultlist)
        //    {
        //        string[] arr = item.Split('#');
        //        QSubTask subTask = new QSubTask { Id = arr[0], Task_id = arr[1], Name = arr[2], Action = arr[3], Param = arr[4] };
        //        subTaskList.Add(subTask);
        //    }
        //    return subTaskList;
        //}

        public QSubTask GetSingleRecordById(string idValue)
        {
            throw new NotImplementedException();
        }
    }
}