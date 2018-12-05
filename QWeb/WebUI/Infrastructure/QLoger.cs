using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Windows;

namespace WebUI.Infrastructure
{
    public static class QLoger
    {
        public static void AddRecordToLog(string taskName, string message)
        {
            try
            {
                if (String.IsNullOrEmpty(message))
                {
                    return;
                }

                string pathToDir = HostingEnvironment.MapPath(@"~/App_Data/Logs");
                string path = Path.Combine(pathToDir, (taskName + ".log"));                
                
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                }

                string fileText = File.ReadAllText(path);

                if (!fileText.Contains(message))
                {
                    string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

                    string textToAdd = Environment.NewLine + DateTime.Now + "\t" + userName + Environment.NewLine + message + Environment.NewLine;
                    File.AppendAllText(path, textToAdd);                    
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
