using DbLayer;
using DbLayer.Infrsrt;
using DbLayer.Managers;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebUI.Models.QEntities
{
    public class QTask : ISelectable<QTask>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string TableName {get; set;}

        public List<QTask> GetAllFieldsList()
        {
            List<QTask> taskList = new List<QTask>();
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

        public int GetCount()
        {
            return ManagerDbQuery.GetCount("q_tasks");
        }

        public List<QTask> GetTasksByLogin(string userName)
        {
            List<OracleParameter> args = new List<OracleParameter> {
                    new OracleParameter("user_login", OracleDbType.Varchar2, userName, ParameterDirection.Input )
            };

            string userRoleId = ManagerPlProc.ExecFunc("q_users_pack.get_role_id", OracleDbType.Varchar2, args);
            string[] fields = new string[] { "task_id" };
            List<QTask> taskList = GetFieldsListById("q_roles_tasks", fields, userRoleId, "role_id");

            return taskList;
        }

        public List<QTask> GetFieldsListById(string tableName, string[] fieldNameArr = null, string idValue = null, string idName = "id")
        {
            List<QTask> taskList = new List<QTask>();
            
            List<string> resultlist = ManagerDbQuery.GetFieldsListById(tableName, fieldNameArr, idValue, idName);
            foreach (var item in resultlist)
            {
                string[] arr = item.Split('#');
                QTask task = new QTask();
                task.Id = arr[0];
                if (arr.Length > 1)
                    task.Name = arr[1];
                else
                    task.Name = ManagerDbQuery.GetSingleRecordById("q_tasks", new string[] { "name" }, arr[0], "id");
                                    
                if (arr.Length > 2)
                    task.TableName = arr[2];
                else
                    task.TableName = ManagerDbQuery.GetSingleRecordById("q_tables", new string[] { "table_name" }, arr[0], "id");                
                taskList.Add(task);
            }
            return taskList;
        }

        public QTask GetSingleRecordById(string idValue)
        {
            string[] fields = new string[] { "Id", "Name" };
            string taskRecord = ManagerDbQuery.GetSingleRecordById("q_tasks", fields, idValue);
            string[] resArr = taskRecord.Split('#');
            QTask task = new QTask { Id = resArr[0], Name = resArr[1] };
            return task;
        }
        

        //public List<QTask> GetFieldsListById(string idValue)
        //{
        //    List<QTask> taskList = new List<QTask>();
        //    string[] fields = new string[] { "id", "name", "table_id" };

        //    List<string> resultlist = ManagerDbQuery.GetFieldsListById("q_tasks", fields, idValue, "id");
        //    //foreach (var item in resultlist)
        //    //{
        //    //    string[] arr = item.Split('#');
        //    //    QTask subTask = new QSubTask { Id = arr[0], Task_id = arr[1], Name = arr[2], Action = arr[3], Param = arr[4] };
        //    //    subTaskList.Add(subTask);
        //    //}
        //    return taskList;
        //}       
    }
}