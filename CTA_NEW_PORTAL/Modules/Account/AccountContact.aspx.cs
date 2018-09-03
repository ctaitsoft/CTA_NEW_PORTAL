using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CTA_NEW_PORTAL.EB;
using Telerik.Web.UI;

namespace CTA_NEW_PORTAL.Modules.Account
{
    public partial class AccountContact : System.Web.UI.Page
    {
        protected string ModuleName = "Accounts";

        private void Navegation_Command(string args, string cmdName)
        {
            if (cmdName == "AccountSearch")
            {
                B_11_OnClick(null, null);
            }
            else if (cmdName == "AccountAdd")
            {
                panel_lnkViewInfo_OnClick(null, null);
            }
            else if (cmdName == "AccountInfo")
            {
                panel_lnkAccInfo_OnClick(null, null);
            }
            else if (cmdName == "AccountContact")
            {

            }
            else if (cmdName == "AccountCrdt")
            {
                panel_lnkCrdtInfo_OnClick(null, null);
            }
            else if (cmdName == "AccountCLM")
            {

            }
            else if (cmdName == "AccountSales")
            {
                panel_lnkAccSales_OnClick(null, null);
            }

        }

        protected void DynamicCommand(object sender, CommandEventArgs e)
        {
            string args = e.CommandArgument.ToString(); //Module path
            string cmdName = e.CommandName;

            Navegation_Command(args, cmdName);

        }

        private void Get_Navegation_Panel()
        {
            try
            {
                Rep_General general = new Rep_General();
                DataTable navInfoTable = new DataTable();
                DataTable user = (DataTable)Session["user"];
                int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                navInfoTable = general.NavegationDetails(userId, ModuleName);

                foreach (DataRow dataRow in navInfoTable.Rows)
                {
                    LinkButton linkButton = new LinkButton();

                    linkButton.ID = "lB_" + dataRow["ModuleName"].ToString();
                    linkButton.CssClass = "btn btn-default";
                    linkButton.Text = dataRow["MenuName"].ToString();
                    linkButton.CausesValidation = false;
                    linkButton.CommandArgument = dataRow["ModulePath"].ToString();
                    linkButton.CommandName = dataRow["ModuleName"].ToString();
                    linkButton.Command += new CommandEventHandler(DynamicCommand);

                    btn_Group_Panel.Controls.Add(linkButton);
                }

                LinkButton linkButtonClose = new LinkButton();
                linkButtonClose.Text = "Close";
                linkButtonClose.CssClass = "btn btn-default";
                linkButtonClose.PostBackUrl = "~/Modules/Home/Home.aspx";
                btn_Group_Panel.Controls.Add(linkButtonClose);

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                string errText = error.Replace("\'", "");

                string AlertMSG = " ";
                AlertMSG += errText;
                this.ClientScript.RegisterStartupScript(this.GetType(), this.GetType().Name,
                    string.Format("window.alert('{0}');", AlertMSG), true);
            }

        }

        private void Generate_Buttons()
        {
            try
            {
                Rep_General btnGeneral = new Rep_General();

                DataTable dtBtns = new DataTable();
                DataTable dtPermittedBtns = new DataTable();
                DataTable User = (DataTable)Session["user"];
                string userId = User.Rows[0]["UserID"].ToString();

                dtBtns = btnGeneral.BtnsDataTable();
                dtPermittedBtns = btnGeneral.Permitted_buttons(int.Parse(userId), "AccountContact");

                int i = 0;
                foreach (LinkButton lnkButton in btnsPanel.Controls.OfType<LinkButton>())
                {
                    for (i = i; i < dtBtns.Rows.Count;)
                    {
                        //lnkButton.ID = dtBtns.Rows[i]["ID"].ToString();
                        lnkButton.Text = dtBtns.Rows[i]["Text"].ToString();
                        lnkButton.ToolTip = dtBtns.Rows[i]["Title"].ToString();
                        i++;
                        break;
                    }

                    //Add button permission
                    //you should give column name 'OptionID=' or it will give you this error "exception: filter expression does not evaluate to a boolean term"
                    string find = "OptionID=" + lnkButton.ID.ToString().Replace("B_", "");
                    DataRow[] foundRows = dtPermittedBtns.Select(find);

                    if (foundRows.Length != 0)
                    {
                        lnkButton.Enabled = true;
                    }
                    else
                    {
                        //these options we enable it or disable it as needed
                        if (find == "OptionID=18" || find == "OptionID=19" || find == "OptionID=29")
                        {

                        }
                        else
                        {
                            lnkButton.Enabled = false;
                        }
                    }

                    //Add triggers to update panel if needed
                    //if (lnkButton.Enabled == true)
                    //{
                    //    if (lnkButton.ToolTip == "Export" || lnkButton.ToolTip == "Import")
                    //    {
                    //        //Button button = (Button)sender;
                    //        //string buttonId = button.ID;
                    //        //TriggerId = lnkButton.ID;
                    //        ScriptManager.GetCurrent(Page).RegisterPostBackControl(lnkButton);
                    //    }
                    //}


                }
            }

            catch (Exception ex)
            {
                string error = ex.Message;
                string errText = error.Replace("\'", "");

                string AlertMSG = "";
                AlertMSG += errText;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", AlertMSG), true);
            }

        }

        private DataTable SelectContacts()
        {
            DataTable contactsDt = new DataTable();

            try
            {
                Rep_Account accountSearch = new Rep_Account();
                Rep_General general = new Rep_General();

                DataTable User = (DataTable)Session["user"];
                string userId = User.Rows[0]["UserID"].ToString();
                string accId = "";

                if (Request.QueryString[general.ObfuscateQueryString("accID")] != null)
                {
                    string value = Request.QueryString[general.ObfuscateQueryString("accID")].ToString();
                    accId = general.DecryptIds(int.Parse(value)).ToString();
                }

                contactsDt = accountSearch.SelectContactSearch(int.Parse(userId), accId);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                string errText = error.Replace("\'", "");

                string AlertMSG = "Error occurred please try again ";
                AlertMSG += errText;
                this.ClientScript.RegisterStartupScript(this.GetType(), this.GetType().Name,
                    string.Format("window.alert('{0}');", AlertMSG), true);
            }

            return contactsDt;
        }

        private void Select_Account_Name()
        {
            try
            {
                Rep_General general = new Rep_General();
                DataTable User = (DataTable)Session["user"];

                string userId = User.Rows[0]["UserID"].ToString();
                string accId = "";
                if (Request.QueryString[general.ObfuscateQueryString("accID")] != null)
                {
                    string value = Request.QueryString[general.ObfuscateQueryString("accID")].ToString();
                    accId = general.DecryptIds(int.Parse(value)).ToString();
                }
                else
                {
                    //accId = lblAccID.Text;
                }


                Rep_Account account = new Rep_Account();

                DataTable accTable = account.SelectAcc(int.Parse(userId), int.Parse(accId));

                if (accTable.Rows.Count > 0)
                {
                    accNo.Text = accTable.Rows[0]["AccNo"].ToString();
                    accName.Text = accTable.Rows[0]["AccName"].ToString();
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                string errText = error.Replace("\'", "");

                string AlertMSG = "";
                AlertMSG += errText;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", AlertMSG), true);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Generate_Buttons();
            Select_Account_Name();
            Get_Navegation_Panel();
        }

        protected void ContactsGRD_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ContactsGRD.DataSource = SelectContacts();
        }

        protected void ContactsGRD_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "EditCommand")
            {
                //string strKey = e.CommandArgument.ToString();
                string strr = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["AccID"].ToString();
                //string mainAccId = ContactsGRD.MasterTableView.Items[e.Item.ItemIndex]["MainAccID"].Text;
                //Response.Redirect("~/Modules/Account/AccountAdd.aspx?ARXDWB=" + strr + "");

                try
                {
                    Rep_General accountSearch = new Rep_General();
                    DataTable user = (DataTable)Session["user"];

                    int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                    string module = accountSearch.SelectModule(userId, "AccountAdd");
                    if (module != "Stop")
                    {
                        int accId = accountSearch.EncryptIds(int.Parse(strr));
                        Response.Redirect("~/Modules/" + module + ".aspx?" + accountSearch.ObfuscateQueryString("accID") + "=" + accId + "");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", "Access Denied"), true);
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    string errText = error.Replace("\'", "");

                    string AlertMSG = "";
                    AlertMSG += error;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", AlertMSG), true);
                }
            }
        }

        //Inquiry
        protected void B_11_OnClick(object sender, EventArgs e)
        {
            try
            {
                Rep_General accountSearch = new Rep_General();
                DataTable user = (DataTable)Session["user"];

                int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                string module = accountSearch.SelectModule(userId, "AccountSearch");
                if (module != "Stop")
                {
                    Response.Redirect("~/Modules/" + module + ".aspx", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", "Access Denied"), true);
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                string errText = error.Replace("\'", "");

                string AlertMSG = "";
                AlertMSG += error;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", AlertMSG), true);
            }
        }

        //Clear
        protected void B_19_OnClick(object sender, EventArgs e)
        {
            ContactsGRD.DataSource = SelectContacts();
            ContactsGRD.DataBind();
        }

        //View
        protected void B_12_OnClick(object sender, EventArgs e)
        {
            panel_lnkViewInfo_OnClick(null, null);
            //foreach (GridDataItem selectinganItem in ContactsGRD.MasterTableView.Items)
            //{
            //    if (selectinganItem.Selected)
            //    {
            //        //Response.Redirect("~/Modules/Account/AccountAdd.aspx?ARXDWB=" + selectinganItem["AccID"].Text + "");
            //        try
            //        {
            //            Rep_General accountSearch = new Rep_General();
            //            DataTable user = (DataTable)Session["user"];

            //            int userId = int.Parse(user.Rows[0]["UserID"].ToString());

            //            string module = accountSearch.SelectModule(userId, "AccountAdd");
            //            if (module != "Stop")
            //            {
            //                int accId = accountSearch.EncryptIds(int.Parse(selectinganItem["AccID"].Text));
            //                Response.Redirect("~/Modules/" + module + ".aspx?" + accountSearch.ObfuscateQueryString("accID") + "=" + accId + "", false);
            //            }
            //            else
            //            {
            //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", "Access Denied"), true);
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            string error = ex.Message;
            //            string errText = error.Replace("\'", "");

            //            string AlertMSG = "";
            //            AlertMSG += error;
            //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", AlertMSG), true);
            //        }
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", "Please select your record"), true);
            //    }
            //}
        }

        //New
        protected void B_21_OnClick(object sender, EventArgs e)
        {
            try
            {
                Rep_General general = new Rep_General();
                DataTable user = (DataTable)Session["user"];

                int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                string module = general.SelectModule(userId, "AccountSearch");
                if (module != "Stop")
                {
                    string accId = Request.QueryString[general.ObfuscateQueryString("accID")].ToString();
                    Response.Redirect("~/Modules/" + module + ".aspx?" + general.ObfuscateQueryString("accID") + "=" + accId + "", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", "Access Denied"), true);
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                string errText = error.Replace("\'", "");

                string AlertMSG = "";
                AlertMSG += error;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", AlertMSG), true);
            }
        }


        #region Navegation Panel

        protected void panel_lnkViewInfo_OnClick(object sender, EventArgs e)
        {
            Rep_General general = new Rep_General();

            string value = "";

            if (Request.QueryString[general.ObfuscateQueryString("accID")] != null)
            {
                value = Request.QueryString[general.ObfuscateQueryString("accID")].ToString();

                try
                {
                    Rep_General accountSearch = new Rep_General();
                    DataTable user = (DataTable)Session["user"];

                    int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                    string module = accountSearch.SelectModule(userId, "AccountAdd");

                    if (module != "Stop")
                    {
                        int accId = int.Parse(value);
                        Response.Redirect("~/Modules/" + module + ".aspx?" + accountSearch.ObfuscateQueryString("accID") + "=" + accId + "");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", "Access Denied"), true);
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    string errText = error.Replace("\'", "");

                    string AlertMSG = "";
                    AlertMSG += error;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", AlertMSG), true);
                }
            }


        }

        protected void panel_lnkCrdtInfo_OnClick(object sender, EventArgs e)
        {
            Rep_General general = new Rep_General();

            string value = "";

            if (Request.QueryString[general.ObfuscateQueryString("accID")] != null)
            {
                value = Request.QueryString[general.ObfuscateQueryString("accID")].ToString();

                try
                {

                    DataTable user = (DataTable)Session["user"];

                    int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                    string module = general.SelectModule(userId, "AccountCrdt");
                    if (module != "Stop")
                    {
                        int accId = int.Parse(value);
                        int accNoTXT = general.EncryptIds(int.Parse(accNo.Text));
                        Response.Redirect("~/Modules/" + module + ".aspx?" + general.ObfuscateQueryString("accID") + "=" + accId + "&" + general.ObfuscateQueryString("accNo") + "=" + accNoTXT + "", false);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", "Access Denied"), true);
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    string errText = error.Replace("\'", "");

                    string AlertMSG = "";
                    AlertMSG += error;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", AlertMSG), true);
                }
            }
        }

        protected void panel_lnkAccSales_OnClick(object sender, EventArgs e)
        {
            Rep_General general = new Rep_General();
            if (accNo.Text != "")
            {
                try
                {
                    string value = "";
                    if (Request.QueryString[general.ObfuscateQueryString("accID")] != null)
                    {
                        value = Request.QueryString[general.ObfuscateQueryString("accID")].ToString();
                    }

                    int accId = int.Parse(value);
                    int accNoTXT = general.EncryptIds(int.Parse(accNo.Text));

                    DataTable user = (DataTable)Session["user"];

                    int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                    string module = general.SelectModule(userId, "AccountSales");
                    if (module != "Stop")
                    {
                        Response.Redirect("~/Modules/" + module + ".aspx?" + general.ObfuscateQueryString("accID") + "=" + accId + "&" + general.ObfuscateQueryString("accNo") + "=" + accNoTXT + "", false);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", "Access Denied"), true);
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    string errText = error.Replace("\'", "");

                    string AlertMSG = "";
                    AlertMSG += error;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", AlertMSG), true);
                }
            }
        }

        protected void panel_lnkAccInfo_OnClick(object sender, EventArgs e)
        {
            Rep_General general = new Rep_General();
            if (accNo.Text != "")
            {
                try
                {
                    string value = "";
                    if (Request.QueryString[general.ObfuscateQueryString("accID")] != null)
                    {
                        value = Request.QueryString[general.ObfuscateQueryString("accID")].ToString();
                    }

                    int accId = int.Parse(value);
                    int accNoTXT = general.EncryptIds(int.Parse(accNo.Text));


                    DataTable user = (DataTable)Session["user"];

                    int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                    string module = general.SelectModule(userId, "AccountInfo");
                    if (module != "Stop")
                    {
                        Response.Redirect("~/Modules/" + module + ".aspx?" + general.ObfuscateQueryString("accID") + "=" + accId + "&" + general.ObfuscateQueryString("accNo") + "=" + accNoTXT + "", false);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", "Access Denied"), true);
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    string errText = error.Replace("\'", "");

                    string AlertMSG = "";
                    AlertMSG += error;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", AlertMSG), true);
                }
            }
        }

        #endregion


    }
}