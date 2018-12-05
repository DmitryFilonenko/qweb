using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLayer.Infrsrt
{
    public interface ISelectable
    {
        int GetCount();
        List<IDbEntity> GetAllFieldsList();
        IDbEntity GetSingleRecordById(string idValue);
        List<IDbEntity> GetFieldsListById(string idValue);   
        

    }
}
