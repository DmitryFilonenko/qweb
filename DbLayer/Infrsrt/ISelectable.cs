using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLayer.Infrsrt
{
    public interface ISelectable<T>
    {
        int GetCount();
        List<T> GetAllFieldsList();
        T GetSingleRecordById(string idValue);
        List<T> GetFieldsListById(string tableName, string[] fieldNameArr = null, string idValue = null, string idName = "id");  
    }
}
