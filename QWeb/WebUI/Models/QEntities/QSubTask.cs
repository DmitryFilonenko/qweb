using DbLayer;
using DbLayer.Infrsrt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models.QEntities
{
    public class QSubTask : ISelectable, IDbEntity
    {
        public string Id { get; set; }
        public string Task_id { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        public string Param { get; set; }
        public string Table_id { get; set; }


        public List<IDbEntity> GetAllFieldsList()
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return ManagerDb.GetCount("subtasks");
        }

        public List<IDbEntity> GetFieldsListById(string idValue)
        {
            List<IDbEntity> subTaskList = new List<IDbEntity>();
            string[] fields = new string[] { "id", "task_id", "name", "action", "param", "table_id" };

            List<string> resultlist = ManagerDb.GetFieldsListById("subtasks", fields, idValue, "task_id");
            foreach (var item in resultlist)
            {
                string[] arr = item.Split('#');
                QSubTask subTask = new QSubTask { Id = arr[0], Task_id = arr[1], Name = arr[2], Action = arr[3], Param = arr[4], Table_id = arr[5] };
                subTaskList.Add(subTask);
            }
            return subTaskList;
        }

        public IDbEntity GetSingleRecordById(string idValue)
        {
            throw new NotImplementedException();
        }
    }
}