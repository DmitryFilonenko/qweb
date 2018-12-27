using DbLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebUI.Infrastructure;

namespace WebUI.Models.Priority
{
    public class PriorityCommand : IQCommand
    {
        public string TaskId { get; set; }
        public string PriorityValue { get; set; }
        public string PathToFile { get; set; }
        public string[] Data { get; set; }
        
        public bool CheckData(string[] strArr)
        {
            bool isCorrect = true;

            foreach (var item in strArr)
            {
                if (!item.Any(char.IsDigit))
                {
                    isCorrect = false;
                    break;
                }
            }
            return isCorrect;
        }       

        
        public void FillTable()
        {
            string[] arr = new string[] { "deal_id" };
            ManagerDbQuery.FillTable(this.TaskId, arr, this.Data);           

        }

        public void Act()
        {
            FillTable();
            //UpdatePriority();
        }

        private void UpdatePriority()
        {
            throw new NotImplementedException();
        }
    }
}