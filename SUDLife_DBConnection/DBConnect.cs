

using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SUDLife_DBConnection
{
    public class DBConnect
    {
        private static DBConnect _singleInstance;
        private DBConnect()
        {
            string config = ConfigurationManager.AppSettings["ConnectionStrings:ConnectionString"].ToString();
        }

        public static DBConnect SingleInstance
        {
            get
            {
                if(_singleInstance == null)
                {
                    _singleInstance = new DBConnect();
                }
                return _singleInstance;
            }
        }

        public IDbConnection connection
        {
            get
            {
                return new SqlConnection(ConfigurationManager.AppSettings["ConnectionStrings:ConnectionString"].ToString());
            }
        }
    }
}
