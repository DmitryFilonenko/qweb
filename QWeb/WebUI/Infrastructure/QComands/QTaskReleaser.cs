using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebUI.Infrastructure.QComands
{
    public class QCurrentTask
    {
        public bool IsNeedToStop { get; set; }
        public string PathToFile { get; set; }
        public void DeleteFile()
        {
            if (PathToFile != null)
            {
                try
                {
                    File.Delete(PathToFile);
                }
                catch (Exception)
                {
                    throw;
                }                
            }
        }

        internal void StopTask()
        {
            throw new NotImplementedException();
        }
    }
}