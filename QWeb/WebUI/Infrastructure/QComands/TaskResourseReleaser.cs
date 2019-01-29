using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUI.Infrastructure.QComands
{
    public class TaskResourseReleaser
    {
        public int TaskId { get; set; }
        abstract public string PathToUploadFile { get; }
    }
}
