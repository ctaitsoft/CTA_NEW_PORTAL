
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CTA_NEW_PORTAL.EB;


namespace CTA_NEW_PORTAL.Modules.Home
{
    public partial class Login : System.Web.UI.Page
    {
        protected string IpAddress;
        protected string PublicIpAddress;
        protected string MachineName;
        public static string GetIp4Address()
        {
            string IP4Address = String.Empty;

            foreach (IPAddress IPA in Dns.GetHostAddresses(HttpContext.Current.Request.UserHostAddress))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }

            if (IP4Address != String.Empty)
            {
                return IP4Address;
            }

            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }

            return IP4Address;
        }
        private void Get_Machine_Name()
        {
            string[] computer_name = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName.Split(new Char[] { '.' });
            String ecn = System.Environment.MachineName;
            MachineName = computer_name[0].ToString();
        }
        private void Get_Machine_IP()
        {
            IpAddress = GetIp4Address();
        }
        private void CheckUserLogin()
        {

            Rep_UserAccount user = new Rep_UserAccount
            {
                UserLog = txtEmail.Text,
                UserPass = txtPassword.Text,
                UserIpLan = IpAddress,
                UserIpWan = PublicIpAddress,
                UserDeviceName = MachineName
            };

            user = user.Authenticate();


            if (user != null)
            {
                //To Get more data about user
                //User = User.GetUserSysLvl();

                // Fill User in Session
                Session["user"] = user.UserAcc;

                if (ckb1.Checked)
                {
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
                }
                else
                {
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);

                }
                Response.Cookies["UserName"].Value = txtEmail.Text.Trim();
                Response.Cookies["Password"].Value = txtPassword.Text.Trim();

                Response.Redirect("../Home/Home.aspx");
            }
            else
            {
                lblLoginMsg.Text = "Invalid user name or password !";
                txtEmail.Focus();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblLoginMsg.Text = "";
            if (!IsPostBack)
            {
                if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
                {
                    txtEmail.Text = Request.Cookies["UserName"].Value;
                    txtPassword.Attributes["value"] = Request.Cookies["Password"].Value;
                    ckb1.Checked = true;
                }
            }

            //This Code to NO Cache Sission
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetNoStore();
            Session["user"] = null;
        }

        protected void btnLogin_OnClick(object sender, EventArgs e)
        {
            Get_Machine_IP();
            Get_Machine_Name();
            PublicIpAddress = myText.Text;
            CheckUserLogin();
        }
    }
}