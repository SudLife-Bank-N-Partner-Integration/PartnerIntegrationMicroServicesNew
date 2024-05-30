using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUDLife_DBConnection
{
    public class Ado
    {
        DBConnect connection;

        public Ado()
        {
            connection = DBConnect.SingleInstance;
        }

        public DataSet ExecuteProcedure(string procName,SqlParameter[] param)
        {
            DataSet dataSet = new DataSet();

            using (IDbConnection con = connection.connection)
            {
                SqlCommand command = new SqlCommand(procName, (SqlConnection)con);
                command.CommandType = CommandType.StoredProcedure;
                foreach (var item in param)
                {
                    command.Parameters.Add(new SqlParameter { ParameterName = item.ParameterName, Value = item.Value });
                }
                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    da.Fill(dataSet);
                }
            }
            return dataSet;
        }
    }
}
