using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebUI.Infrastructure.QComands
{
    public class QUploadFileHandler
    {
        string _pathToFile;

        public QUploadFileHandler(string path)
        {
            _pathToFile = path;
        }

        public bool CheckIsDigits()
        {
            bool isCorrect = true;
            string[] strArr = System.IO.File.ReadAllLines(_pathToFile/*, System.Text.Encoding.Default*/);

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

        public void DeleteFile()
        {
            File.Delete(_pathToFile);
        }
    }
}