using SUDLife_DataRepo;
using System.Data;
using System.Data.SqlClient;

namespace SUDLife_Aadarsh.Datalayer
{
    //Just for reference but give name as per our requirement
    public class UpdateLogs
    {
        private readonly IExectueProcdure _exectueProcdure;

        public UpdateLogs(IExectueProcdure exectueProcdure)
        {
            _exectueProcdure = exectueProcdure;
        }

        public void ExecuteProc()
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter{ParameterName = "",Value =""}
            };
            DataSet ds = new DataSet();
            ds = _exectueProcdure.ExecuteProcedure("SP_ProcName",param);
        }

    }
}
