using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALWhiskers
    {
        static string strCon = @"Data Source=sql.bsite.net\MSSQL2016;Initial Catalog=bloodwarrior3_;Persist Security Info=True;User ID=bloodwarrior3_;Password=Aa1234$";
        static SqlConnection con;
        public DALWhiskers()
        {
            con = new SqlConnection(strCon);
        }

    }
}
