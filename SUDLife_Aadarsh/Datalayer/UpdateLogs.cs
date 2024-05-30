using SUDLife_DBConnection;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace SUDLife_Aadarsh.Datalayer
{
    //Just for reference but give name as per our requirement
    public class UpdateLogs
    {
        Ado ado = new Ado();

        public void ExecuteProc()
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter(parameterName :"@Name",value:"Mayur"),

            };
            DataSet ds = ado.ExecuteProcedure("StoreLogs", param);
        }

    }
}
