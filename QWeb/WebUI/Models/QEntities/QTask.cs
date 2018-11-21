﻿using DbLayer;
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
            return ManagerDb.GetCount("tasks");
        }

        public List<IDbEntity> GetFieldsListById(string idValue)
        {
            throw new NotImplementedException();
        }

        public List<IDbEntity> GetAllFieldsList()
        {
            List<IDbEntity> taskList = new List<IDbEntity>();
            string[] fields = new string[] { "Id", "Name" };
            List<string> resultlist = ManagerDb.GetFieldsList("tasks", fields);
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
            throw new NotImplementedException();
        }
    }
}