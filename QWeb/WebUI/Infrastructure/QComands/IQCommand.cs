using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Infrastructure
{
    public interface IQCommand
    {
        string PathToFile { get; set; }
        bool CheckData(string[] strArr);
        void FillTable();
        void Act();
    }
}