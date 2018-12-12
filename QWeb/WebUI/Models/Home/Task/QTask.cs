using DbLayer;
using DbLayer.Infrsrt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models.QEntities
{
    public class QTask : ISelectable, IDbEntity
    {        
        public string Id { get; set; }
        public string Name { get; set; }

        public int GetCount()
        {
            return ManagerDbQuery.GetCount("q_tasks");
        }

        public List<IDbEntity> GetFieldsListById(string idValue)
        {
            throw new NotImplementedException();
        }

        public List<IDbEntity> GetAllFieldsList()
        {
            List<IDbEntity> taskList = new List<IDbEntity>();
            string[] fields = new string[] { "Id", "Name" };
            List<string> resultlist = ManagerDbQuery.GetFieldsList("q_tasks", fields);
            foreach (var item in resultlist)
            {
                string[] arr = item.Split('#');
                QTask task = new QTask { Id = arr[0], Name = arr[1] };
                taskList.Add(task);
            }
            return taskList;
        }

        public IDbEntity GetSingleRecordById(string idValue)
        {
            string[] fields = new string[] { "Id", "Name" };
            string taskRecord = ManagerDbQuery.GetSingleRecordById("q_tasks", fields, idValue);
            string[] resArr = taskRecord.Split('#');
            QTask task = new QTask { Id = resArr[0], Name = resArr[1] };
            return task;
        }

        int ISelectable.GetCount()
        {
            throw new NotImplementedException();
        }

        List<IDbEntity> ISelectable.GetAllFieldsList()
        {
            List<IDbEntity> taskList = new List<IDbEntity>();
            string[] fields = new string[] { "Id", "Name" };
            List<string> resultlist = ManagerDbQuery.GetFieldsList("q_tasks", fields);
            foreach (var item in resultlist)
            {
                string[] arr = item.Split('#');
                QTask task = new QTask { Id = arr[0], Name = arr[1] };
                taskList.Add(task);
            }
            return taskList;
        }

        IDbEntity ISelectable.GetSingleRecordById(string idValue)
        {
            string[] fields = new string[] { "Id", "Name" };
            string taskRecord = ManagerDbQuery.GetSingleRecordById("q_tasks", fields, idValue);
            string[] resArr = taskRecord.Split('#');
            QTask task = new QTask { Id = resArr[0], Name = resArr[1] };
            return task;
        }

        List<IDbEntity> ISelectable.GetFieldsListById(string idValue)
        {
            throw new NotImplementedException();
        }
    }
}