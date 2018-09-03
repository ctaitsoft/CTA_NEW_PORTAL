using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
//using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CTA_NEW_PORTAL.EB;

namespace CTA_NEW_PORTAL.Modules.Account
{
    public partial class AccountAdd : System.Web.UI.Page
    {
        public SqlConnection DbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["GlobalConnection"].ConnectionString);

        protected string AccountId;

        protected string ModuleName = "Accounts";

        private void Navegation_Command(string args, string cmdName)
        {
            //string idParam = ""; // Ex: accId

            //Rep_General general = new Rep_General();
            //string qString = "";


            //if (idParam != "")
            //{
            //    //int accId = general.EncryptIds(int.Parse(lblAccId.Text));
            //    //int accNo = general.EncryptIds(int.Parse(txtAccCode.Text));
            //    qString = "?" + general.ObfuscateQueryString("accID") + "=" + "accId....." + "&" + general.ObfuscateQueryString("accNo") + "=" + "accNo...." + "";
            //    Response.Redirect("~/Modules/" + args + ".aspx" + qString + "", false);
            //}
            //else
            //{
            //    qString = "";
            //    Response.Redirect("~/Modules/" + args + ".aspx" + qString + "", false);
            //}

            if (cmdName == "AccountSearch")
            {
                B_11_OnClick(null, null);
            }
            else if (cmdName == "AccountAdd")
            {

            }
            else if (cmdName == "AccountInfo")
            {
                panel_lnkAccInfo_OnClick(null, null);
            }
            else if (cmdName == "AccountContact")
            {
                panel_lnkContactInfo_OnClick(null, null);
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

        private void Check_User_Login()
        {
            DataTable user = (DataTable)Session["user"];

            if (user != null)
            {
                accUsr.Value = user.Rows[0]["UserID"].ToString();
            }
        }

        private void Check_Name_QueryString()
        {
            Rep_General general = new Rep_General();
            //this code is for (check_name) script method to get the AccID without encryption to check if you are on edit mode OR not
            if (Request.QueryString[general.ObfuscateQueryString("accID")] != null)
            {
                string value = Request.QueryString[general.ObfuscateQueryString("accID")].ToString();
                string qTxt = general.DecryptIds(int.Parse(value)).ToString();
                lblAccQ.Text = qTxt;
            }
            if (Request.QueryString[general.ObfuscateQueryString("view")] != null)
            {
                string value = Request.QueryString[general.ObfuscateQueryString("view")].ToString();
                lblviewQ.Text = value;
            }
        }

        private DataTable SelectddlDataTable(string proceduer, string param)
        {
            DataTable ddlTable = new DataTable();

            try
            {
                Rep_Account accountDdlsDetails = new Rep_Account();
                DataTable User = (DataTable)Session["user"];
                string userId = User.Rows[0]["UserID"].ToString();

                ddlTable = accountDdlsDetails.SelectddlDataTable(int.Parse(userId), proceduer, param);
            }
            catch (Exception ex)
            {
                string Error = ex.Message;
                string AlertMSG = "";
                AlertMSG += Error;
                this.ClientScript.RegisterStartupScript(this.GetType(), this.GetType().Name,
                    string.Format("window.alert('{0}');", AlertMSG), true);

                return ddlTable;
            }

            return ddlTable;
        }

        private RegularExpressionValidator Get_Regex(string regexType)
        {
            RegularExpressionValidator regExpressionValidator = new RegularExpressionValidator();

            regExpressionValidator.EnableClientScript = true;
            regExpressionValidator.ErrorMessage = "";
            regExpressionValidator.ForeColor = Color.Red;
            regExpressionValidator.Display = ValidatorDisplay.Dynamic;

            if (regexType == "Mobile")
            {
                regExpressionValidator.ValidationExpression = @"[0][7](7|8|9)\d{7}";
                regExpressionValidator.Text = "* Mobile number is not correct, Must contain 10 numbers start with 07.. example: 079 000 0000.";
            }
            else if (regexType == "Email")
            {
                regExpressionValidator.ValidationExpression = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                regExpressionValidator.Text = "* Email address is not correct ex: Jhon@outlook.com.";
            }
            else if (regexType == "Phone")
            {
                regExpressionValidator.ValidationExpression = @"[0](6|5|3|2)\d{7}";
                regExpressionValidator.Text = "* Phone number is not correct, Must contain 9 numbers start with (0).. example: 06 000 0000.";
            }
            else if (regexType == "Date")
            {
                regExpressionValidator.ValidationExpression = @"^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$";
                regExpressionValidator.Text = "* Date is not in the correct format, Must be in dd/MM/yyyy.. example: 12/12/2043.";
            }


            return regExpressionValidator;
        }

        private void Get_ddls()
        {
            try
            {
                dllAccountType.DataSource = SelectddlDataTable("SP_SysCode_Acc_Category", "");
                dllAccountType.DataTextField = "AccCategory";
                dllAccountType.DataValueField = "AccCategoryID";
                dllAccountType.DataBind();

                ddlTitle.DataSource = SelectddlDataTable("SP_SysCode_Acc_Title", dllAccountType.Text);
                ddlTitle.DataTextField = "Title";
                ddlTitle.DataValueField = "TitleID";
                ddlTitle.DataBind();
                ddlTitle.Items.Insert(0, new ListItem("Select", "-1"));

                ddlGender.DataSource = SelectddlDataTable("SP_SysCode_Acc_Gender", dllAccountType.Text);
                ddlGender.DataTextField = "Gender";
                ddlGender.DataValueField = "GenderID";
                ddlGender.DataBind();
                ddlGender.Items.Insert(0, new ListItem("Select", "-1"));

                ddlContactType.DataSource = SelectddlDataTable("SP_SysCode_AccContact_Type", dllAccountType.Text);
                ddlContactType.DataTextField = "ContactType";
                ddlContactType.DataValueField = "ContactTypeID";
                ddlContactType.DataBind();
                ddlContactType.Items.Insert(0, new ListItem("Select", "-1"));
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

        private void ddls_validation()
        {
            if (dllAccountType.Text == "1")
            {
                AccName.Attributes.Remove("class");
                //AccName.Attributes["style"] = @"display:normal;";

                txtFullName.Enabled = false;
                txtFullName.Attributes.Add("readonly", "readonly");

                ddlTitle.Enabled = true;
                ddlTitle.Attributes.Remove("readonly");
                ddlTitle.SelectedIndex = -1;
                ddlGender.Enabled = true;
                ddlGender.Attributes.Remove("readonly");
                ddlGender.SelectedIndex = -1;

                btnAddTitle.Enabled = true;
                btnAddTitle.OnClientClick = "ShowTitleDialog(); return false;";

                //Enable these fields to make changes if you are in the update session
                txtFirstName.Enabled = true;
                txtSecondName.Enabled = true;
                txtThirdName.Enabled = true;
                txtForthName.Enabled = true;
                //txtFirstName_Validator.ValidationGroup = "save";
                //txtSecondName_Validator.ValidationGroup = "save";
                //txtThirdName_Validator.ValidationGroup = "save";
                //txtForthName_Validator.ValidationGroup = "save";
            }
            else
            {
                AccName.Attributes.Add("class", "hidn");
                //AccName.Attributes["style"] = @"display:none;";

                txtFullName.Enabled = true;
                txtFullName.Attributes.Remove("readonly");
                txtFullName.Focus();


                ddlTitle.Enabled = false;
                ddlTitle.Attributes.Add("readonly", "readonly");
                ddlTitle.SelectedIndex = 1;
                ddlGender.Enabled = false;
                ddlGender.Attributes.Add("readonly", "readonly");
                ddlGender.SelectedIndex = 1;

                btnAddTitle.Enabled = false;
                btnAddTitle.OnClientClick = null;
                //txtFirstName_Validator.ValidationGroup = "";
                //txtSecondName_Validator.ValidationGroup = "";
                //txtThirdName_Validator.ValidationGroup = "";
                //txtForthName_Validator.ValidationGroup = "";
            }
        }

        private void Account_New()
        {
            txtAccCode.Text = string.Empty;
            txtFullName.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtSecondName.Text = string.Empty;
            txtThirdName.Text = string.Empty;
            txtForthName.Text = string.Empty;

            dllAccountType.SelectedIndex = 0;
            dllAccountType.Enabled = true;
            dllAccountType.Attributes.Remove("readonly");

            AccName.Attributes.Remove("class");

            txtFirstName.Enabled = true;
            txtSecondName.Enabled = true;
            txtThirdName.Enabled = true;
            txtForthName.Enabled = true;
            txtFullName.Enabled = false;
            txtFullName.Attributes.Add("readonly", "readonly");

            ddlTitle.Enabled = true;
            ddlTitle.Attributes.Remove("readonly");
            ddlTitle.SelectedIndex = -1;
            ddlGender.Enabled = true;
            ddlGender.Attributes.Remove("readonly");
            ddlGender.SelectedIndex = -1;

            foreach (Control c in phAccDetails.Controls)
            {
                foreach (Control childc in c.Controls)
                {
                    foreach (Control childc1 in childc.Controls)
                    {
                        if (childc1 is TextBox)
                        {
                            ((TextBox)childc1).Enabled = true;
                            ((TextBox)childc1).Text = string.Empty;
                        }
                        else
                        {
                            //this foreach loop code if we have textbox in HtmlGenericControl
                            foreach (Control childc2 in childc1.Controls)
                            {
                                if (childc2 is TextBox)
                                {
                                    ((TextBox)childc2).Enabled = true;
                                    ((TextBox)childc2).Text = string.Empty;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Disable_Fields()
        {
            txtAccCode.Enabled = false;
            dllAccountType.Enabled = false;
            ddlTitle.Enabled = false;
            txtFirstName.Enabled = false;
            txtSecondName.Enabled = false;
            txtThirdName.Enabled = false;
            txtForthName.Enabled = false;
            txtFullName.Enabled = false;
            ddlGender.Enabled = false;

            foreach (Control c in phAccDetails.Controls)
            {
                foreach (Control childc in c.Controls)
                {
                    foreach (Control childc1 in childc.Controls)
                    {
                        if (childc1 is TextBox)
                        {
                            ((TextBox)childc1).Enabled = false;
                        }
                        else
                        {
                            //this foreach loop code if we have textbox in HtmlGenericControl
                            foreach (Control childc2 in childc1.Controls)
                            {
                                if (childc2 is TextBox)
                                {
                                    ((TextBox)childc2).Enabled = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Btn_Fields_Roles()
        {
            if (hidnViewFlag.Value == "View")
            {
                Disable_Fields();
                Buttons_permissions();

                B_29.Enabled = false;
                B_29.OnClientClick = null;

                B_18.Enabled = false;
                B_19.Enabled = false;
                btnAddTitle.Enabled = false;
                btnAddTitle.OnClientClick = null;
                accContactType.Attributes["style"] = @"display: none;";
                accHeader.Attributes["style"] = @"visibility:hidden;";
            }
            else if (hidnViewFlag.Value == "Add")
            {
                Enable_Fields();
                B_11.Enabled = true;
                B_12.Enabled = false;
                B_13.Enabled = false;
                B_21.Enabled = false;
                B_22.Enabled = false;
                B_23.Enabled = false;

                B_29.Enabled = true;
                B_29.OnClientClick = "if(Page_ClientValidate('save')) ShowconfirmDialog(); return false;";
                B_18.Enabled = true;
                B_19.Enabled = true;
                btnAddTitle.Enabled = true;
                btnAddTitle.OnClientClick = "ShowTitleDialog(); return false;";
                accContactType.Attributes["style"] = @"display: none;";
                accHeader.Attributes["style"] = @"visibility:hidden;";
            }
            else if (hidnViewFlag.Value == "Update")
            {
                Enable_Fields();
                B_11.Enabled = true;
                B_12.Enabled = false;
                B_13.Enabled = false;
                B_21.Enabled = false;
                B_22.Enabled = false;
                B_23.Enabled = false;

                B_29.Enabled = true;
                B_29.OnClientClick = "if(Page_ClientValidate('save')) ShowconfirmDialog(); return false;";
                B_18.Enabled = false;
                B_19.Enabled = true;
                if (dllAccountType.Text == "1")
                {
                    btnAddTitle.Enabled = true;
                    btnAddTitle.OnClientClick = "ShowTitleDialog(); return false;";
                }
                accContactType.Attributes["style"] = @"display: none;";
                accHeader.Attributes["style"] = @"visibility:hidden;";
            }
            else if (hidnViewFlag.Value == "Contact")
            {
                Enable_Contact_Fields();
                B_11.Enabled = true;
                B_12.Enabled = false;
                B_13.Enabled = false;
                B_21.Enabled = false;
                B_22.Enabled = false;
                B_23.Enabled = false;

                B_29.Enabled = true;
                B_29.OnClientClick = "if(Page_ClientValidate('save')) ShowconfirmDialog(); return false;";
                B_18.Enabled = false;
                B_19.Enabled = true;
                if (dllAccountType.Text == "1")
                {
                    btnAddTitle.Enabled = true;
                    btnAddTitle.OnClientClick = "ShowTitleDialog(); return false;";
                }

                btnAddContactTyp.Enabled = true;
                btnAddContactTyp.OnClientClick = "ShowTypeDialog(); return false;";

                ddlContactType.Enabled = true;


                accContactType.Attributes["style"] = @"display: normal;";
                accHeader.Attributes["style"] = @"visibility:visible;";
            }
        }

        private void Enable_Fields()
        {
            if (dllAccountType.Text == "1")
            {
                ddlTitle.Enabled = true;
                txtFirstName.Enabled = true;
                txtSecondName.Enabled = true;
                txtThirdName.Enabled = true;
                txtForthName.Enabled = true;
                ddlGender.Enabled = true;
                txtFullName.Enabled = false;
                txtFullName.Attributes.Add("readonly", "readonly");
            }
            else
            {
                txtFullName.Enabled = true;
                txtFullName.Attributes.Remove("readonly");
            }
            if (hidnViewFlag.Value == "Add")
            {
                dllAccountType.Enabled = true;
            }
        }

        private void Enable_Contact_Fields()
        {
            lblAccNo.Text = lblAccNo.Text != string.Empty ? lblAccNo.Text : txtAccCode.Text;
            lblAccName.Text = lblAccName.Text != string.Empty ? lblAccName.Text : txtFullName.Text;
            lblAccCategory.Text = lblAccCategory.Text != string.Empty ? lblAccCategory.Text : dllAccountType.Text;

            txtAccCode.Enabled = false;
            txtAccCode.Text = string.Empty;

            dllAccountType.Enabled = false;
            dllAccountType.SelectedValue = "1";

            //---------------Add These to prevent out of range Error--------START---------
            ddlTitle.Items.Clear();
            ddlTitle.SelectedValue = "1";
            //---------------Add These to prevent out of range Error--------END---------
            ddlTitle.DataSource = SelectddlDataTable("SP_SysCode_Acc_Title", dllAccountType.Text);
            ddlTitle.DataTextField = "Title";
            ddlTitle.DataValueField = "TitleID";
            ddlTitle.DataBind();
            ddlTitle.Items.Insert(0, new ListItem("Select", "-1"));

            //---------------Add These to prevent out of range Error--------START---------
            ddlGender.Items.Clear();
            ddlGender.SelectedValue = "1";
            //---------------Add These to prevent out of range Error--------END---------
            ddlGender.DataSource = SelectddlDataTable("SP_SysCode_Acc_Gender", dllAccountType.Text);
            ddlGender.DataTextField = "Gender";
            ddlGender.DataValueField = "GenderID";
            ddlGender.DataBind();
            ddlGender.Items.Insert(0, new ListItem("Select", "-1"));

            ddlContactType.DataSource = SelectddlDataTable("SP_SysCode_AccContact_Type", dllAccountType.Text);
            ddlContactType.DataTextField = "ContactType";
            ddlContactType.DataValueField = "ContactTypeID";
            ddlContactType.DataBind();
            ddlContactType.Items.Insert(0, new ListItem("Select", "-1"));

            //get the ddls_validation method to refresh the text boxes depends on dllAccountType cause we had change it here.
            ddls_validation();

            //Clear the dynamic controls to prevent dublicated textbox ID's and reget the Controls to get the dynamic textboxes depends on dllAccountType cause we had change it here.
            phAccDetails.Controls.Clear();
            Get_Acc_Details();

            ddlTitle.Enabled = true;
            ddlTitle.SelectedIndex = -1;

            txtFirstName.Enabled = true;
            txtFirstName.Text = string.Empty;

            txtSecondName.Enabled = true;
            txtSecondName.Text = string.Empty;

            txtThirdName.Enabled = true;
            txtThirdName.Text = string.Empty;

            txtForthName.Enabled = true;
            txtForthName.Text = string.Empty;

            txtFullName.Enabled = false;
            txtFullName.Text = string.Empty;
            txtFullName.Attributes.Add("readonly", "readonly");

            ddlGender.Enabled = true;
            ddlGender.SelectedIndex = -1;

            ddlContactType.SelectedIndex = -1;

            foreach (Control c in phAccDetails.Controls)
            {
                foreach (Control childc in c.Controls)
                {
                    foreach (Control childc1 in childc.Controls)
                    {
                        if (childc1 is TextBox)
                        {
                            ((TextBox)childc1).Enabled = true;
                            ((TextBox)childc1).Text = string.Empty;
                        }
                        else
                        {
                            //this foreach loop code if we have textbox in HtmlGenericControl
                            foreach (Control childc2 in childc1.Controls)
                            {
                                if (childc2 is TextBox)
                                {
                                    ((TextBox)childc2).Enabled = true;
                                    ((TextBox)childc2).Text = string.Empty;
                                }
                            }
                        }
                    }
                }
            }
        }

        #region Account_Update

        private void Select_Account_Udt()
        {
            try
            {
                Rep_General general = new Rep_General();
                DataTable User = (DataTable)Session["user"];

                string userId = User.Rows[0]["UserID"].ToString();
                string accId = "";
                if (Request.QueryString[general.ObfuscateQueryString("accID")] == null)
                {
                    accId = lblAccId.Text;
                }
                else
                {
                    string value = Request.QueryString[general.ObfuscateQueryString("accID")].ToString();
                    accId = general.DecryptIds(int.Parse(value)).ToString();
                }


                Rep_Account accountUdt = new Rep_Account();

                DataTable accUdtTable = accountUdt.SelectAcc(int.Parse(userId), int.Parse(accId));

                if (accUdtTable.Rows.Count > 0)
                {
                    txtAccCode.Text = accUdtTable.Rows[0]["AccNo"].ToString();
                    dllAccountType.Text = accUdtTable.Rows[0]["AccCategory"].ToString();


                    if (dllAccountType.Text == "1")
                    {
                        AccName.Attributes.Remove("class");
                        txtFirstName.Text = accUdtTable.Rows[0]["AccName1"].ToString();
                        txtSecondName.Text = accUdtTable.Rows[0]["AccName2"].ToString();
                        txtThirdName.Text = accUdtTable.Rows[0]["AccName3"].ToString();
                        txtForthName.Text = accUdtTable.Rows[0]["AccName4"].ToString();

                        //Hiddin Fields
                        hidnTitle.Value = ddlTitle.Text;
                        hidnGender.Value = ddlGender.Text;
                    }
                    else
                    {
                        AccName.Attributes.Add("class", "hidn");
                        ddlTitle.DataSource = SelectddlDataTable("SP_SysCode_Acc_Title", dllAccountType.Text);
                        ddlTitle.DataTextField = "Title";
                        ddlTitle.DataValueField = "TitleID";
                        ddlTitle.DataBind();
                        ddlTitle.Items.Insert(0, new ListItem("Select", "-1"));

                        ddlGender.DataSource = SelectddlDataTable("SP_SysCode_Acc_Gender", dllAccountType.Text);
                        ddlGender.DataTextField = "Gender";
                        ddlGender.DataValueField = "GenderID";
                        ddlGender.DataBind();
                        ddlGender.Items.Insert(0, new ListItem("Select", "-1"));

                        ddlContactType.DataSource = SelectddlDataTable("SP_SysCode_AccContact_Type", dllAccountType.Text);
                        ddlContactType.DataTextField = "ContactType";
                        ddlContactType.DataValueField = "ContactTypeID";
                        ddlContactType.DataBind();
                        ddlContactType.Items.Insert(0, new ListItem("Select", "-1"));
                    }

                    ddlTitle.Text = accUdtTable.Rows[0]["AccTitle"].ToString();
                    txtFullName.Text = accUdtTable.Rows[0]["AccName"].ToString();
                    ddlGender.Text = accUdtTable.Rows[0]["AccGender"].ToString();
                    lblAccId.Text = accUdtTable.Rows[0]["AccID"].ToString();

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

        private void Select_AccountInfo_Udt()
        {
            try
            {
                Rep_General general = new Rep_General();
                DataTable User = (DataTable)Session["user"];

                string userId = User.Rows[0]["UserID"].ToString();
                string accId = "";
                if (Request.QueryString[general.ObfuscateQueryString("accID")] == null)
                {
                    accId = lblAccId.Text;
                }
                else
                {
                    string value = Request.QueryString[general.ObfuscateQueryString("accID")].ToString();
                    accId = general.DecryptIds(int.Parse(value)).ToString();
                }

                Rep_Account accountInfoUdt = new Rep_Account();

                DataTable accInfoUdtTable = accountInfoUdt.SelectAccInfo(int.Parse(userId), int.Parse(accId));

                if (accInfoUdtTable.Rows.Count > 0)
                {
                    foreach (Control c in phAccDetails.Controls)
                    {
                        foreach (Control childc in c.Controls)
                        {
                            foreach (Control childc1 in childc.Controls)
                            {
                                if (childc1 is TextBox)
                                {
                                    string find = "InfoType=" + ((TextBox)childc1).ID.ToString().Replace("F_", "");
                                    DataRow[] foundRows = accInfoUdtTable.Select(find);
                                    if (foundRows.Length != 0)
                                    {
                                        if (((TextBox)childc1).ID.ToString().Replace("F_", "") == foundRows[0]["InfoType"].ToString())
                                        {
                                            ((TextBox)childc1).Text = foundRows[0]["InfoData"].ToString();
                                        }
                                        else
                                        {
                                            ((TextBox)childc1).Text = "";
                                        }
                                    }

                                }
                                else
                                {
                                    //this foreach loop code if we have textbox in HtmlGenericControl
                                    foreach (Control childc2 in childc1.Controls)
                                    {
                                        if (childc2 is TextBox)
                                        {
                                            string find = "InfoType=" + ((TextBox)childc2).ID.ToString().Replace("F_", "");
                                            DataRow[] foundRows = accInfoUdtTable.Select(find);
                                            if (foundRows.Length != 0)
                                            {
                                                if (((TextBox)childc2).ID.ToString().Replace("F_", "") == foundRows[0]["InfoType"].ToString())
                                                {
                                                    ((TextBox)childc2).Text = foundRows[0]["InfoData"].ToString();
                                                }
                                                else
                                                {
                                                    ((TextBox)childc2).Text = "";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
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
            }
        }

        private void Account_Udt()
        {
            try
            {
                Rep_Account accountUdt = new Rep_Account();
                Rep_General general = new Rep_General();
                string accId = "";
                if (Request.QueryString[general.ObfuscateQueryString("accID")] == null)
                {
                    accId = lblAccId.Text;
                }
                else
                {
                    string value = Request.QueryString[general.ObfuscateQueryString("accID")].ToString();
                    accId = general.DecryptIds(int.Parse(value)).ToString();
                }
                DataTable user = (DataTable)Session["user"];

                int userId = int.Parse(user.Rows[0]["UserID"].ToString());
                string accName = txtFullName.Text;
                string accName1 = txtFirstName.Text;
                string accName2 = txtSecondName.Text;
                string accName3 = txtThirdName.Text;
                string accName4 = txtForthName.Text;
                string accTitle = ddlTitle.Text;
                string accGender = ddlGender.Text;

                string Account_Udt = accountUdt.UpdateUdt(userId, int.Parse(accId), accName, accName1, accName2, accName3, accName4, accTitle, accGender);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", Account_Udt), true);

                Disable_Fields();
                Select_Account_Udt();

                hidnViewFlag.Value = "View";
                lblSubject.Text = "View";
                Btn_Fields_Roles();
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

        #endregion

        private void Account_Save()
        {
            try
            {
                //System.Threading.Thread.Sleep(2000);
                Rep_Account accountSave = new Rep_Account();
                DataTable user = (DataTable)Session["user"];
                DataTable accountTable = new DataTable();

                int userId = int.Parse(user.Rows[0]["UserID"].ToString());
                string accName = txtFullName.Text;
                string accName1 = txtFirstName.Text;
                string accName2 = txtSecondName.Text;
                string accName3 = txtThirdName.Text;
                string accName4 = txtForthName.Text;
                byte accCategory = byte.Parse(dllAccountType.Text);
                byte accTitle = byte.Parse(ddlTitle.Text);
                byte accGender = byte.Parse(ddlGender.Text);

                accountTable = accountSave.Acc_Save(userId, accName, accName1, accName2, accName3, accName4, accCategory, accTitle, accGender);
                //return AccountId & AccountNo
                AccountId = accountTable.Rows[0]["AccID"].ToString();
                lblAccId.Text = AccountId;
                txtAccCode.Text = accountTable.Rows[0]["AccNo"].ToString();


                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", "Saved"), true);
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

        private void Contact_Save()
        {
            try
            {
                Rep_Account ContactSave = new Rep_Account();
                DataTable user = (DataTable)Session["user"];
                DataTable contactTable = new DataTable();

                int userId = int.Parse(user.Rows[0]["UserID"].ToString());
                string conName = txtFullName.Text;
                string conName1 = txtFirstName.Text;
                string conName2 = txtSecondName.Text;
                string conName3 = txtThirdName.Text;
                string conName4 = txtForthName.Text;
                byte conCategory = byte.Parse(dllAccountType.Text);
                byte conTitle = byte.Parse(ddlTitle.Text);
                byte conGender = byte.Parse(ddlGender.Text);
                byte conType = byte.Parse(ddlContactType.Text);
                int accId = int.Parse(lblAccId.Text);

                contactTable = ContactSave.Contact_Save(userId, conName, conName1, conName2, conName3, conName4, conCategory, conTitle, conGender, conType, accId);
                //return ContactNo
                txtAccCode.Text = contactTable.Rows[0]["AccNo"].ToString();
                lblAccContactId.Text = contactTable.Rows[0]["AccID"].ToString();

                Disable_Fields();
                btnAddTitle.Enabled = false;
                btnAddTitle.OnClientClick = null;

                btnAddContactTyp.Enabled = false;
                btnAddContactTyp.OnClientClick = null;

                ddlContactType.Enabled = false;
                B_29.Enabled = false;
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

        private void Get_Acc_Details()
        {
            try
            {
                Rep_Account accountInfoTypeDetails = new Rep_Account();
                DataTable accInfoTable = new DataTable();
                DataTable user = (DataTable)Session["user"];

                int userId = int.Parse(user.Rows[0]["UserID"].ToString());
                string accountType = dllAccountType.Text;

                accInfoTable = accountInfoTypeDetails.AccInfoTypeDetails(userId, accountType);


                int i = 0;
                //int columnCount = accInfoTable.Columns.Count;
                foreach (DataRow dataRow in accInfoTable.Rows)
                {
                    //Delete rows that has null value in InfoTypeMandatory column 
                    if (dataRow["InfoTypeMandatory"].ToString() != "True")
                    {
                        dataRow.Delete();
                        //Go forward
                        continue;
                    }


                    TableRow tableRow = new TableRow();
                    //TableCell tableCell1 = new TableCell();
                    TableCell tableCell2 = new TableCell();
                    tableCell2.CssClass = "label-cell-width";
                    TableCell tableCell3 = new TableCell();
                    //this cell & checkbox is for mobile field if you will add international number
                    //TableCell tableCell4 = new TableCell();
                    //CheckBox checkBox = new CheckBox();
                    //checkBox.Text = "&nbsp;&nbsp;International";
                    //checkBox.ID = "chkInternational";
                    //checkBox.AutoPostBack = true;
                    //checkBox.CheckedChanged += new EventHandler(chkInternational_Changed);

                    TextBox textBox = new TextBox();
                    textBox.ID = "F_" + dataRow["InfoTypeID"].ToString();
                    textBox.CssClass = "form-control nxt";
                    if (dataRow["InfoTypeValidation"].ToString() == "Mobile" || dataRow["InfoTypeValidation"].ToString() == "Phone" || dataRow["InfoTypeValidation"].ToString() == "Number")
                    {
                        textBox.Attributes.Add("onkeydown", "isNumber(event,this.id); CheckMove();");
                    }
                    else if (dataRow["InfoTypeValidation"].ToString() == "Number")
                    {
                        textBox.MaxLength = 10;
                    }
                    else if (dataRow["InfoTypeValidation"].ToString() == "Text")
                    {
                        textBox.MaxLength = 50;
                    }

                    //Add this HTML code if you need to make check on value using Ajax or to add more design
                    HtmlGenericControl div = new HtmlGenericControl("div");
                    HtmlGenericControl span = new HtmlGenericControl("span");
                    HtmlGenericControl span1 = new HtmlGenericControl("span");
                    div.Attributes.Add("id", "AccMobExist");
                    div.Attributes.Add("class", "form-group has-feedback");
                    span.Attributes.Add("id", "MobExistIcon");
                    span.Attributes.Add("class", "glyphicon form-control-feedback");
                    span1.Attributes.Add("id", "helpBlock3");
                    span1.Attributes.Add("class", "help-block");

                    //Validation
                    RequiredFieldValidator validator = new RequiredFieldValidator();
                    validator.ID = "F_RequiredFieldValidator" + i;
                    validator.ControlToValidate = "F_" + dataRow["InfoTypeID"].ToString();
                    validator.EnableClientScript = true;
                    validator.ErrorMessage = "";
                    validator.Text = "* This field is required";
                    validator.ForeColor = Color.Red;
                    validator.ValidationGroup = "save";
                    validator.Display = ValidatorDisplay.Dynamic;

                    //Regular Expression
                    RegularExpressionValidator regExpressionValidator = new RegularExpressionValidator();
                    regExpressionValidator = Get_Regex(dataRow["InfoTypeValidation"].ToString());
                    regExpressionValidator.ControlToValidate = "F_" + dataRow["InfoTypeID"].ToString();
                    regExpressionValidator.ID = "RegexF_" + dataRow["InfoTypeID"].ToString();
                    regExpressionValidator.ValidationGroup = "save";



                    //Fill Cell's
                    //tableCell1.Text = i.ToString();
                    tableCell2.Text = dataRow["InfoTypeName"].ToString();

                    // Here we need to add some css and html code for Mobile field to use it to check mobile value
                    if (dataRow["InfoTypeValidation"].ToString() == "Mobile")
                    {
                        div.Controls.Add(textBox);
                        div.Controls.Add(span);
                        div.Controls.Add(span1);
                        tableCell3.Controls.Add(div);
                        //tableCell4.Controls.Add(checkBox);
                    }
                    else
                    {
                        tableCell3.Controls.Add(textBox);
                    }

                    if (dataRow["InfoTypeMandatory"].ToString() == "True")
                    {
                        tableCell3.Controls.Add(validator);
                    }
                    if (dataRow["InfoTypeValidation"].ToString() == "Mobile" || dataRow["InfoTypeValidation"].ToString() == "Email" ||
                        dataRow["InfoTypeValidation"].ToString() == "Phone" || dataRow["InfoTypeValidation"].ToString() == "Date")
                    {
                        tableCell3.Controls.Add(regExpressionValidator);
                    }

                    //Fill Row with cell's
                    //tableRow.Controls.Add(tableCell1);
                    tableRow.Controls.Add(tableCell2);
                    tableRow.Controls.Add(tableCell3);
                    //if (dataRow["InfoTypeValidation"].ToString() == "Mobile")
                    //{
                    //    tableRow.Controls.Add(tableCell4);   //Add Checkbox
                    //}

                    //Add the new row
                    phAccDetails.Controls.Add(tableRow);

                    i++;
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

        private void Generate_Buttons()
        {
            try
            {
                Rep_General btnGeneral = new Rep_General();

                DataTable dtBtns = new DataTable();

                dtBtns = btnGeneral.BtnsDataTable();

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

        private void Buttons_permissions()
        {
            try
            {
                Rep_General btnGeneral = new Rep_General();
                DataTable dtPermittedBtns = new DataTable();
                DataTable User = (DataTable)Session["user"];
                string userId = User.Rows[0]["UserID"].ToString();

                dtPermittedBtns = btnGeneral.Permitted_buttons(int.Parse(userId), "AccountAdd");
                foreach (LinkButton lnkButton in btnsPanel.Controls.OfType<LinkButton>())
                {
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

        //protected void Page_Init(object sender, EventArgs e)
        //{

        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            Check_User_Login();
            Check_Name_QueryString();
            Generate_Buttons();
            Get_Navegation_Panel();

            if (!IsPostBack)
            {
                Get_ddls();
                ddls_validation();
            }

            Rep_General general = new Rep_General();
            if (Request.QueryString[general.ObfuscateQueryString("accID")] != null)
            {
                if (!IsPostBack)
                {
                    Select_Account_Udt();
                    hidnViewFlag.Value = "View";
                    lblSubject.Text = "View";
                }
                //get dynamic textbox's after select account data to check the account type then get data of dynamic textbox's "Select_AccountInfo_Udt()";
                Get_Acc_Details();

                if (!IsPostBack)
                {
                    Select_AccountInfo_Udt();
                }
            }
            else if (hidnViewFlag.Value == "Update")
            {
                Get_Acc_Details();
                Select_AccountInfo_Udt();
            }
            else
            {
                if (!IsPostBack)
                {
                    hidnViewFlag.Value = "Add";
                    lblSubject.Text = "New";
                }

                Get_Acc_Details();
            }

            if (!IsPostBack)
            {
                //get Roles after check the flag value
                Btn_Fields_Roles();
            }

            if (lblviewQ.Text != "")
            {
                if (!IsPostBack)
                {
                    panel_lnkContactInfo_OnClick(null, null);
                }
            }


        }

        protected void dllAccountType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlTitle.DataSource = SelectddlDataTable("SP_SysCode_Acc_Title", dllAccountType.Text);
            ddlTitle.DataTextField = "Title";
            ddlTitle.DataValueField = "TitleID";
            ddlTitle.DataBind();
            ddlTitle.Items.Insert(0, new ListItem("Select", "-1"));

            ddlGender.DataSource = SelectddlDataTable("SP_SysCode_Acc_Gender", dllAccountType.Text);
            ddlGender.DataTextField = "Gender";
            ddlGender.DataValueField = "GenderID";
            ddlGender.DataBind();
            ddlGender.Items.Insert(0, new ListItem("Select", "-1"));

            ddlContactType.DataSource = SelectddlDataTable("SP_SysCode_AccContact_Type", dllAccountType.Text);
            ddlContactType.DataTextField = "ContactType";
            ddlContactType.DataValueField = "ContactTypeID";
            ddlContactType.DataBind();
            ddlContactType.Items.Insert(0, new ListItem("Select", "-1"));

            ddls_validation();

            if (hidnViewFlag.Value == "Update")
            {
                if (dllAccountType.Text == "1")
                {
                    if (hidnTitle.Value == "" || hidnGender.Value == "")
                    { }
                    else
                    {
                        ddlTitle.Text = hidnTitle.Value;
                        ddlGender.Text = hidnGender.Value;
                    }
                }
            }
        }

        //New
        protected void B_21_OnClick(object sender, EventArgs e)
        {
            //Account_New();
            //B_21.Enabled = false;
            //B_29.Enabled = true;
            ////refresh dll's 
            //Get_ddls();
            //hidnMoveFlag.Value = string.Empty;
            //hidnViewFlag.Value = string.Empty;

            //PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty(
            //        "IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            //// make collection editable
            //isreadonly.SetValue(this.Request.QueryString, false, null);
            //// remove
            //this.Request.QueryString.Remove(general.ObfuscateQueryString("accID"));

            //Response.Redirect("~/Modules/Account/AccountAdd.aspx");
            try
            {
                Rep_General accountSearch = new Rep_General();
                DataTable user = (DataTable)Session["user"];

                int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                string module = accountSearch.SelectModule(userId, "AccountAdd");
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

        //Save
        protected void B_29_OnClick(object sender, EventArgs e)
        {
            if (hidnViewFlag.Value == "Update")
            {
                Account_Udt();
            }
            else if (hidnViewFlag.Value == "Contact")
            {
                Contact_Save();

                string infoTypeId = "";
                string txtData = "";

                try
                {
                    DataTable user = (DataTable)Session["user"];
                    int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                    foreach (Control c in phAccDetails.Controls)
                    {
                        foreach (Control childc in c.Controls)
                        {
                            foreach (Control childc1 in childc.Controls)
                            {
                                if (childc1 is TextBox)
                                {
                                    txtData = ((TextBox)childc1).Text;
                                    infoTypeId = ((TextBox)childc1).ID.ToString().Replace("F_", "");
                                    //lblAccId.Text += infoTypeId + txtData;
                                    Rep_Account accountInfoTypeDetails = new Rep_Account();
                                    accountInfoTypeDetails.Acc_InfoType_Save(userId, lblAccContactId.Text, infoTypeId, txtData);
                                }
                                else
                                {
                                    //this foreach loop code if we have textbox in HtmlGenericControl
                                    foreach (Control childc2 in childc1.Controls)
                                    {
                                        if (childc2 is TextBox)
                                        {
                                            txtData = ((TextBox)childc2).Text;
                                            infoTypeId = ((TextBox)childc2).ID.ToString().Replace("F_", "");
                                            //lblAccId.Text += infoTypeId + txtData;
                                            Rep_Account accountInfoTypeDetails = new Rep_Account();
                                            accountInfoTypeDetails.Acc_InfoType_Save(userId, lblAccContactId.Text, infoTypeId, txtData);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //return to contact page
                    Rep_General general = new Rep_General();
                    string module = general.SelectModule(userId, "AccountContact");
                    if (module != "Stop")
                    {
                        int value = int.Parse(Request.QueryString[general.ObfuscateQueryString("accID")].ToString());
                        Response.Redirect("~/Modules/" + module + ".aspx?" + general.ObfuscateQueryString("accID") + "=" + value + "", false);
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
                    AlertMSG += errText;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", AlertMSG), true);
                }
            }
            else if (accCheckName.Value == "1")
            {

            }
            else
            {
                Account_Save();

                //TextBox myTextBox = phAccDetails.FindControl("F_11") as TextBox;
                //string txtInfoType = myTextBox.ID.ToString().Replace("F_","");
                string infoTypeId = "";
                string txtData = "";

                try
                {
                    DataTable user = (DataTable)Session["user"];
                    int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                    foreach (Control c in phAccDetails.Controls)
                    {
                        foreach (Control childc in c.Controls)
                        {
                            foreach (Control childc1 in childc.Controls)
                            {
                                if (childc1 is TextBox)
                                {
                                    txtData = ((TextBox)childc1).Text;
                                    infoTypeId = ((TextBox)childc1).ID.ToString().Replace("F_", "");
                                    //lblAccId.Text += infoTypeId + txtData;
                                    Rep_Account accountInfoTypeDetails = new Rep_Account();
                                    accountInfoTypeDetails.Acc_InfoType_Save(userId, lblAccId.Text, infoTypeId, txtData);
                                }
                                else
                                {
                                    //this foreach loop code if we have textbox in HtmlGenericControl
                                    foreach (Control childc2 in childc1.Controls)
                                    {
                                        if (childc2 is TextBox)
                                        {
                                            txtData = ((TextBox)childc2).Text;
                                            infoTypeId = ((TextBox)childc2).ID.ToString().Replace("F_", "");
                                            //lblAccId.Text += infoTypeId + txtData;
                                            Rep_Account accountInfoTypeDetails = new Rep_Account();
                                            accountInfoTypeDetails.Acc_InfoType_Save(userId, lblAccId.Text, infoTypeId, txtData);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", "Saved"), true);

                    hidnViewFlag.Value = "View";
                    lblSubject.Text = "View";
                    Btn_Fields_Roles();
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

        //View
        protected void B_12_OnClick(object sender, EventArgs e)
        {
            if (txtAccCode.Text == string.Empty)
            {

            }
            else
            {
                try
                {
                    Rep_General accountSearch = new Rep_General();
                    DataTable user = (DataTable)Session["user"];

                    int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                    string module = accountSearch.SelectModule(userId, "AccountView");
                    if (module != "Stop")
                    {
                        int accId = accountSearch.EncryptIds(int.Parse(lblAccId.Text));
                        Response.Redirect("~/Modules/" + module + ".aspx?" + accountSearch.ObfuscateQueryString("accID") + "=" + accId + "", false);
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

        //refresh
        protected void B_18_OnClick(object sender, EventArgs e)
        {
            //if (hidnMoveFlag.Value == "1")
            //{
            //    if (txtAccCode.Text == string.Empty)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("ShowCheckDialog();"), true);
            //    }
            //}
            //else
            //{

            //}

            //refresh data
            Get_ddls();
            ddls_validation();
        }

        //clear
        protected void B_19_OnClick(object sender, EventArgs e)
        {
            //New
            if (hidnMoveFlag.Value == "1" && lblAccId.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("ShowCheckDialog();"), true);
            }
            //Not a New
            else if (lblAccId.Text != string.Empty && (hidnViewFlag.Value == "Update" || hidnViewFlag.Value == "Contact"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("ShowCheckDialog();"), true);
            }
        }

        //Title
        protected void btnSaveTitle_OnClick(object sender, EventArgs e)
        {
            try
            {
                Rep_Account accTitleSave = new Rep_Account();
                DataTable user = (DataTable)Session["user"];
                DataTable titleDataTable = new DataTable();

                int userId = int.Parse(user.Rows[0]["UserID"].ToString());
                string accTitle = txtTitle.Text.Replace(" ", string.Empty);
                byte accCategory = byte.Parse(dllAccountType.Text);

                titleDataTable = SelectddlDataTable("SP_SysCode_Acc_Title", dllAccountType.Text);

                string find = "Title='" + accTitle + "'";
                //myDataTable.Select("columnName1 like '%" + value + "%'");
                DataRow[] foundRows = titleDataTable.Select(find);
                if (foundRows.Length != 0)
                {
                    //title is exist
                    lblTitleMsg.Text = "* This title isn't allowed. Try again";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("ShowTitleDialog();"), true);
                }
                else
                {
                    lblTitleMsg.Text = "";
                    accTitleSave.Acc_Title_Save(userId, accCategory, accTitle);
                    txtTitle.Text = string.Empty;
                }

                //B_21_OnClick(null, null);
                ddlTitle.DataSource = SelectddlDataTable("SP_SysCode_Acc_Title", dllAccountType.Text);
                ddlTitle.DataTextField = "Title";
                ddlTitle.DataValueField = "TitleID";
                ddlTitle.DataBind();
                ddlTitle.Items.Insert(0, new ListItem("Select", "-1"));
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

        //Type
        protected void btnSaveType_OnClick(object sender, EventArgs e)
        {
            try
            {
                Rep_Account conTypeSave = new Rep_Account();
                DataTable user = (DataTable)Session["user"];
                DataTable typeDataTable = new DataTable();

                int userId = int.Parse(user.Rows[0]["UserID"].ToString());
                string conType = txtContactType.Text;
                byte accCategory = byte.Parse(dllAccountType.Text);

                typeDataTable = SelectddlDataTable("SP_SysCode_AccContact_Type", dllAccountType.Text);

                string find = "ContactType='" + conType + "'";
                //myDataTable.Select("columnName1 like '%" + value + "%'");
                DataRow[] foundRows = typeDataTable.Select(find);
                if (foundRows.Length != 0)
                {
                    //Type is exist
                    lblTypeMsg.Text = "* This type isn't allowed. Try again";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("ShowTypeDialog();"), true);
                }
                else
                {
                    lblTypeMsg.Text = "";
                    conTypeSave.Contact_Type_Save(userId, accCategory, conType);
                    txtContactType.Text = string.Empty;
                }

                ddlContactType.DataSource = SelectddlDataTable("SP_SysCode_AccContact_Type", dllAccountType.Text);
                ddlContactType.DataTextField = "ContactType";
                ddlContactType.DataValueField = "ContactTypeID";
                ddlContactType.DataBind();
                ddlContactType.Items.Insert(0, new ListItem("Select", "-1"));
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

        //Edit
        protected void B_22_OnClick(object sender, EventArgs e)
        {
            hidnViewFlag.Value = "Update";
            lblSubject.Text = "Edit";
            Btn_Fields_Roles();
        }

        protected void lnkbtndntsve_OnClick(object sender, EventArgs e)
        {
            if (lblAccId.Text != string.Empty)
            {
                Select_Account_Udt();
                Select_AccountInfo_Udt();
                hidnViewFlag.Value = "View";
                lblSubject.Text = "View";
                Btn_Fields_Roles();
            }
            else
            {
                B_21_OnClick(null, null);
            }
        }

        //protected void chkInternational_Changed(object sender, EventArgs e)
        //{
        //    foreach (Control c in phAccDetails.Controls)
        //    {
        //        foreach (Control childc in c.Controls)
        //        {
        //            foreach (Control childc1 in childc.Controls)
        //            {
        //                if (childc1 is TextBox)
        //                {
        //                    if (((TextBox)childc1).ID == "F_11")
        //                    {
        //                        CheckBox checkBox = (CheckBox)childc1.FindControl("chkInternational");
        //                        if (checkBox.Checked)
        //                        {
        //                            RegularExpressionValidator regexValidator = (RegularExpressionValidator)childc1.FindControl("RegexF_11");
        //                            regexValidator.Enabled = false;
        //                            ((TextBox)childc1).Focus();
        //                        }
        //                        else
        //                        {
        //                            RegularExpressionValidator regexValidator = (RegularExpressionValidator)childc1.FindControl("RegexF_11");
        //                            regexValidator.Enabled = true;
        //                            if (!regexValidator.IsValid)
        //                            {
        //                                regexValidator.IsValid = true;
        //                            }
        //                            else
        //                            {
        //                                regexValidator.IsValid = false;
        //                            }

        //                            ((TextBox)childc1).Focus();

        //                        }

        //                    }
        //                }
        //                else
        //                {
        //                    //this foreach loop code if we have textbox in HtmlGenericControl
        //                    foreach (Control childc2 in childc1.Controls)
        //                    {
        //                        if (childc2 is TextBox)
        //                        {
        //                            CheckBox checkBox = (CheckBox)childc2.FindControl("chkInternational");
        //                            if (checkBox.Checked)
        //                            {
        //                                RegularExpressionValidator regexValidator = (RegularExpressionValidator)childc2.FindControl("RegexF_11");
        //                                regexValidator.Enabled = false;
        //                                ((TextBox)childc2).Focus();
        //                            }
        //                            else
        //                            {
        //                                RegularExpressionValidator regexValidator = (RegularExpressionValidator)childc2.FindControl("RegexF_11");
        //                                regexValidator.Enabled = true;
        //                                if (!regexValidator.IsValid)
        //                                {
        //                                    regexValidator.IsValid = true;
        //                                }
        //                                else
        //                                {
        //                                    regexValidator.IsValid = false;
        //                                }

        //                                ((TextBox)childc2).Focus();
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        #region Navegation_Panel
        protected void panel_lnkCrdtInfo_OnClick(object sender, EventArgs e)
        {
            if (lblAccId.Text != string.Empty)
            {
                try
                {
                    Rep_General accountSearch = new Rep_General();
                    DataTable user = (DataTable)Session["user"];

                    int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                    string module = accountSearch.SelectModule(userId, "AccountCrdt");
                    if (module != "Stop")
                    {
                        int accId = accountSearch.EncryptIds(int.Parse(lblAccId.Text));
                        int accNo = accountSearch.EncryptIds(int.Parse(txtAccCode.Text));
                        Response.Redirect("~/Modules/" + module + ".aspx?" + accountSearch.ObfuscateQueryString("accID") + "=" + accId + "&" + accountSearch.ObfuscateQueryString("accNo") + "=" + accNo + "", false);
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

        protected void panel_lnkContactInfo_OnClick(object sender, EventArgs e)
        {
            //if (lblAccId.Text != string.Empty)
            //{
            //    hidnViewFlag.Value = "Contact";
            //    lblSubject.Text = "Contact";
            //    Btn_Fields_Roles();
            //}

            try
            {
                Rep_General general = new Rep_General();
                DataTable user = (DataTable)Session["user"];

                int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                string module = general.SelectModule(userId, "AccountContact");
                if (module != "Stop")
                {
                    int accId = general.EncryptIds(int.Parse(lblAccId.Text));
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

        protected void panel_lnkAccSales_OnClick(object sender, EventArgs e)
        {
            Rep_General general = new Rep_General();
            if (txtAccCode.Text != "")
            {
                try
                {
                    int accId = general.EncryptIds(int.Parse(lblAccId.Text));
                    int accNo = general.EncryptIds(int.Parse(txtAccCode.Text));

                    DataTable user = (DataTable)Session["user"];

                    int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                    string module = general.SelectModule(userId, "AccountSales");
                    if (module != "Stop")
                    {
                        Response.Redirect("~/Modules/" + module + ".aspx?" + general.ObfuscateQueryString("accID") + "=" + accId + "&" + general.ObfuscateQueryString("accNo") + "=" + accNo + "", false);
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
            if (txtAccCode.Text != "")
            {
                try
                {
                    int accId = general.EncryptIds(int.Parse(lblAccId.Text));
                    int accNo = general.EncryptIds(int.Parse(txtAccCode.Text));


                    DataTable user = (DataTable)Session["user"];

                    int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                    string module = general.SelectModule(userId, "AccountInfo");
                    if (module != "Stop")
                    {
                        Response.Redirect("~/Modules/" + module + ".aspx?" + general.ObfuscateQueryString("accID") + "=" + accId + "&" + general.ObfuscateQueryString("accNo") + "=" + accNo + "", false);
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