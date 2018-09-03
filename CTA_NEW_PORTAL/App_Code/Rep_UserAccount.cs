using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CTA_NEW_PORTAL.EB
{
    public class Rep_UserAccount
    {
        public DataTable UserAcc { get; set; }
        public string UserLog { get; set; }
        public string UserPass { get; set; }
        public string UserIpLan { get; set; }
        public string UserIpWan { get; set; }
        public string UserDeviceName { get; set; }


        private readonly SqlConnection _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GlobalConnection"].ConnectionString);


        public Rep_UserAccount Authenticate()
        {
            _connection.Open();
            SqlDataAdapter da = new SqlDataAdapter("SP_SysUser_Info", _connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@UserLog", UserLog);
            da.SelectCommand.Parameters.AddWithValue("@UserPass", UserPass);
            da.SelectCommand.Parameters.AddWithValue("@UserIPLan", UserIpLan);
            da.SelectCommand.Parameters.AddWithValue("@UserIPWan", UserIpWan);
            da.SelectCommand.Parameters.AddWithValue("@UserDeviceName", UserDeviceName);
            da.SelectCommand.Parameters.AddWithValue("@UserMacID", DBNull.Value);
            DataTable dtUser = new DataTable();
            da.Fill(dtUser);
            _connection.Close();

            if (dtUser.Rows.Count > 0)
            {
                return new Rep_UserAccount
                {
                    //UserLog = UserLog,
                    //UserNo = int.Parse(dt.Rows[0]["UserNo"].ToString()),
                    //UserName = (dt.Rows[0]["UserName"].ToString()),
                    UserAcc = dtUser
                };
            }
            else
            {
                return null;
            }

        }
    }
}