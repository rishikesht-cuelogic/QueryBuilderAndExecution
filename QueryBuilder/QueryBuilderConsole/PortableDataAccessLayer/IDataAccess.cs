using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableDataAccessLayer
{
    interface IDataAccess
    {
        object GetDataFromDatabase(string sqlSelectCommond, string[] TableNames = null, ProcReturnType ProcReturnType = ProcReturnType.JSON);
        int SaveDataToDatabase(string sqlInsertCommond);
    }
}
