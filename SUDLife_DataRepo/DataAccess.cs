using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUDLife_DataRepo
{
    public class DataAccess : IExectueProcdure
    {
        private readonly string _connectionString;

        public DataAccess(string con ) { _connectionString = con; }


        public DataSet ExecuteProcedure(string procName, SqlParameter[] param)
        {
            DataSet dataSet = new DataSet();

            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(procName, connection);
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
