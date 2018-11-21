using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLayer
{
    public class AppTaskManager : ManagerDb
    {
    //    public static List<AppTask> GetTaskList()
    //    {
    //        List<AppTask> taskList = new List<AppTask>();
    //        string[] fields = new string[] { "Id", "Name" };
    //        List<string> resultlist = GetFieldsList("tasks", fields);
    //        foreach (var item in resultlist)
    //        {
    //            string[] arr = item.Split('#');
    //            AppTask task = new AppTask { Id = arr[0], Name = arr[1] };
    //            taskList.Add(task);
    //        }
    //        return taskList;
    //    }

    //    public static List<AppSubTask> GetSubTaskListById(string idValue)
    //    {
    //        List<AppSubTask> subTaskList = new List<AppSubTask>();
    //        string[] fields = new string[] { "Id", "Task_id", "Name", "Action", "Param", "Table_id" };
    //        List<string> resultlist = GetFieldsListById("subtasks", fields, idValue, "Task_id");
    //        foreach (var item in resultlist)
    //        {
    //            string[] arr = item.Split('#');
    //            AppSubTask subTask = new AppSubTask { Id = arr[0], Task_id = arr[1], Name = arr[2], Action = arr[3], Param = arr[4], Table_id =arr[5] };
    //            subTaskList.Add(subTask);
    //        }
    //        return subTaskList;
    //    }
    //}


    //public class AppTask 
    //{
        
    //}

    //public class AppSubTask
    //{   
    //    public string Id { get; set; }
    //    public string Task_id { get; set; }
    //    public string Name { get; set; }
    //    public string Action { get; set; }
    //    public string Param { get; set; }
    //    public string Table_id { get; set; }
    }
}
