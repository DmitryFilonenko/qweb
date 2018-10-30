using DbLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models.Home
{
    public class AppTask
    {
        int Id { get; set; }
        string Name { get; set; }
        public List<AppSubTask> SubTasks;


        public List<AppTask> GetAppTaskList()
        {
            List<AppTask> list = new List<AppTask>();
            AppTaskManager.GetFieldList("tasks", "name");


            return list;
        }
    }


    


}