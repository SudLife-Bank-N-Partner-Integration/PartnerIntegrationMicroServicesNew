using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUDLife_DataRepo
{
    public interface IExectueProcdure
    {
        DataSet ExecuteProcedure(string procName, SqlParameter[] param);
    }
}
