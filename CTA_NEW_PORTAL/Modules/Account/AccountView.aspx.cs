using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace CTA_NEW_PORTAL.Modules.Account
{
    public partial class AccountView : System.Web.UI.Page
    {
        public SqlConnection CustomersConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["GlobalConnection"].ConnectionString);

        private void User_Controls()
        {
            Crdt_Info.Controls.Add(LoadControl("AccountViews/AccountCrdt.ascx"));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            User_Controls();
        }
    }
}