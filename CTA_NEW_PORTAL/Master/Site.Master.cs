using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CTA_NEW_PORTAL.EB;

namespace CTA_NEW_PORTAL.Master
{
    public partial class Site : System.Web.UI.MasterPage
    {
        public static string DBCon = ConfigurationManager.ConnectionStrings["GlobalConnection"].ConnectionString;
        protected string result;
        protected string UserPic;

        private void Check_User_Login()
        {
            //Registration Usr = new Registration();
            if (Session["user"] == null)
            {
                Response.Redirect("~/Modules/Home/Login.aspx");
                UserPanel.Visible = false;
                LoginPanel.Visible = true;
            }
            else
            {
                // Create Object From Session -- Becouse we already filled the session from Class (Control)
                //Registration m = (Registration)Session["user"];

                DataTable User = (DataTable)Session["user"];
                //var collection = (List<string>)Session["user"];
                result += User.Rows[0]["UserName"].ToString();

                UserPic = User.Rows[0]["UserPic"].ToString();

                if (UserPic != "")
                {
                    byte[] bytes = (byte[])User.Rows[0]["UserPic"];
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    UserImage.ImageUrl = "data:image;base64," + base64String;
                    //UserImage.ImageUrl = string.Concat("data:image/png;base64,", Convert.ToBase64String(byteArray));
                }
                else
                {
                    UserImage.ImageUrl = "../Images/Master/User.png";
                }

                lblThemeNo.Text = User.Rows[0]["UserTheme"].ToString();
                //result += "<tr>";
                //result += "<td style='width:55px'><img src = '../Images/User.PNG' width = '35' height = '35' /></td>";
                //result += "<td style='font-family: Open Sans, arial';width:auto>" + User.Rows[0]["UserName"].ToString() + "&nbsp;&nbsp;" + "</td>";
                //result += "<td>" + "<i class='fa fa-caret-down'></i>" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "</td>";
                //result += "</tr>";
                UserPanel.Visible = true;
                LoginPanel.Visible = false;

                if (!this.IsPostBack)
                {
                    DataTable dt = this.GetData();
                    PopulateMenu(dt);
                }
            }
        }

        private void Change_Password()
        {
            try
            {
                Rep_General passwordSave = new Rep_General();
                DataTable user = (DataTable)Session["user"];
                string passMsg = "";

                int userId = int.Parse(user.Rows[0]["UserID"].ToString());
                //TextBox txtBusinessDate = (TextBox)Page.Master.FindControl("txtOldPassword");
                string oldPass = txtOldPassword.Text;
                string newPass = txtNewPassword.Text;

                if (correctFX1.Value != "1")
                {
                    if (txtNewPassword.Text != txtConfirmPassword.Text)
                    {
                        chngPasswordMsg.Text = "New password does not match the confirm password";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Pop", string.Format("ClosepassModal();"), true);
                    }
                    else
                    {
                        passMsg = passwordSave.Password_Save(userId, oldPass, newPass);

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Pop", string.Format("ClosepassModal();"), true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", passMsg), true);
                    }
                }


            }
            catch (Exception ex)
            {
                string error = ex.Message;
                string errText = error.Replace("\'", "");

                string AlertMSG = "";
                AlertMSG += errText;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", AlertMSG), true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Pop", string.Format("ClosepassModal();"), true);
            }
        }

        private DataTable GetData()
        {
            //string query = "SELECT [MenuID], [MenuLevel], [MenuHeader], [MenuSort], [MenuName] FROM [SysMenuRun] order by MenuSort";
            DataTable User = (DataTable)Session["user"];
            string user = User.Rows[0]["UserID"].ToString();

            using (SqlConnection con = new SqlConnection(DBCon))
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SP_SysRole_Menu", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.AddWithValue("@UserID", user);

                da.Fill(dt);
                //using (SqlCommand cmd = new SqlCommand(query))
                //{
                //    using (SqlDataAdapter sda = new SqlDataAdapter())
                //    {
                //        cmd.CommandType = CommandType.Text;
                //        cmd.Connection = con;
                //        sda.SelectCommand = cmd;
                //        sda.Fill(dt);
                //    }
                //}
                return dt;
            }
        }

        private void PopulateMenu(DataTable dt)
        {
            string currentPage = Path.GetFileName(Request.Url.AbsolutePath);
            DataView view = new DataView(dt);
            view.RowFilter = "MenuLevel=1";
            foreach (DataRowView row in view)
            {
                MenuItem menuItem = new MenuItem
                {
                    Value = row["MenuSeq"].ToString(),
                    Text = row["MenuName"].ToString(),
                    NavigateUrl = "~/Modules/" + row["ModulePath"].ToString() + ".aspx",
                    //Selected = row["URL"].ToString().EndsWith(currentPage, StringComparison.CurrentCultureIgnoreCase)
                };
                Menu1.Items.Add(menuItem);
                AddChildItems(dt, menuItem);

            }
        }

        private void AddChildItems(DataTable table, MenuItem menuItem)
        {
            DataView viewItem = new DataView(table);
            viewItem.RowFilter = "MenuHeader=" + menuItem.Value;
            foreach (DataRowView childView in viewItem)
            {
                MenuItem childmenuItem = new MenuItem
                {
                    Value = childView["MenuSeq"].ToString(),
                    Text = childView["MenuName"].ToString(),
                    NavigateUrl = "~/Modules/" + childView["ModulePath"].ToString() + ".aspx"
                };
                menuItem.ChildItems.Add(childmenuItem);
                AddChildItems(table, childmenuItem);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
                Response.Cache.SetNoStore();
            }

            Check_User_Login();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Page.Header.DataBind();
        }

        protected void SignOut_Click(object sender, EventArgs e)
        {
            Session["user"] = null;
            Response.Redirect("~/Modules/Home/Login.aspx");
        }

        protected void btnConfirmPass_OnClick(object sender, EventArgs e)
        {
            Change_Password();
        }

        //protected void Menu1_OnMenuItemClick(object sender, MenuEventArgs e)
        //{
        //    try
        //    {
        //        string selectedItem = (sender as Menu).SelectedItem.Value;
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", selectedItem), true);
        //    }
        //    catch (Exception exception)
        //    {

        //    }

        //}
    }
}