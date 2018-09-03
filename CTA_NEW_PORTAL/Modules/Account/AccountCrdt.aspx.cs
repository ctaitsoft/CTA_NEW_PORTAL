using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CTA_NEW_PORTAL.EB;

namespace CTA_NEW_PORTAL.Modules.Account
{
    public partial class AccountCrdt : System.Web.UI.Page
    {
        public SqlConnection DbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["GlobalConnection"].ConnectionString);

        protected string ModuleName = "Accounts";

        private void Navegation_Command(string args, string cmdName)
        {
            if (cmdName == "AccountSearch")
            {
                B_11_OnClick(null, null);
            }
            else if (cmdName == "AccountAdd")
            {
                panel_lnkAccView_OnClick(null, null);
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

        private DataTable SelectddlDataTable(string proceduer, string param)
        {
            DataTable ddlTable = new DataTable();

            try
            {
                Rep_General accountCrdtDdlsDetails = new Rep_General();
                DataTable User = (DataTable)Session["user"];
                string userId = User.Rows[0]["UserID"].ToString();

                ddlTable = accountCrdtDdlsDetails.SelectddlDataTable(int.Parse(userId), proceduer, param);
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

        private void Get_ddls()
        {
            try
            {
                dllCategory_1.DataSource = SelectddlDataTable("SP_SysCode_AccCrdt_Category1", "");
                dllCategory_1.DataValueField = "CategoryID";
                dllCategory_1.DataTextField = "Category";
                dllCategory_1.DataBind();
                dllCategory_1.Items.Insert(0, new ListItem("- Select -", "0"));

                dllCategory_2.DataSource = SelectddlDataTable("SP_SysCode_AccCrdt_Category2", "");
                dllCategory_2.DataValueField = "CategoryID";
                dllCategory_2.DataTextField = "Category";
                dllCategory_2.DataBind();
                dllCategory_2.Items.Insert(0, new ListItem("- Select -", "0"));

                dllResponsible.DataSource = SelectddlDataTable("SP_SysCode_AccCrdt_Resp", "");
                dllResponsible.DataValueField = "UserID";
                dllResponsible.DataTextField = "UserName";
                dllResponsible.DataBind();
                dllResponsible.Items.Insert(0, new ListItem("- Select -", "0"));

                dllType.DataSource = SelectddlDataTable("SP_SysCode_AccCrdt_LimitType", ""); ;
                dllType.DataValueField = "CrdtLimitTypeID";
                dllType.DataTextField = "CrdtLimitType";
                dllType.DataBind();
                dllType.Items.Insert(0, new ListItem("- Select -", "0"));
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
                btnAddCat1.Enabled = false;
                btnAddCat1.OnClientClick = null;
                btnAddCat2.Enabled = false;
                btnAddCat2.OnClientClick = null;
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
                //btnAddTitle.Enabled = true;
                //btnAddTitle.OnClientClick = "ShowTitleDialog(); return false;";
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
                btnAddCat1.Enabled = true;
                btnAddCat1.OnClientClick = "ShowCat_1_Dialog(); return false;";
                btnAddCat2.Enabled = true;
                btnAddCat2.OnClientClick = "ShowCat_2_Dialog(); return false;";
            }
        }

        private void Disable_Fields()
        {
            txtCrdtNo.Enabled = false;
            dllCategory_1.Enabled = false;
            dllCategory_2.Enabled = false;
            dllResponsible.Enabled = false;

            dllType.Enabled = false;
            txtMax.Enabled = false;
            txtExtra.Enabled = false;
            RBLBlocked.Enabled = false;
            txtMemo.Disabled = true;
        }

        private void Enable_Fields()
        {
            txtCrdtNo.Enabled = true;
            dllCategory_1.Enabled = true;
            dllCategory_2.Enabled = true;
            dllResponsible.Enabled = true;

            dllType.Enabled = true;
            txtMax.Enabled = true;
            txtExtra.Enabled = true;
            RBLBlocked.Enabled = true;
            txtMemo.Disabled = false;

            if (hidnViewFlag.Value == "Add")
            {
                //dllAccountType.Enabled = true;
            }
        }

        private void Get_AccCrdt_Blocked()
        {
            RBLBlocked.Items.Insert(0, new ListItem("No", "0"));
            RBLBlocked.Items.Insert(0, new ListItem("Yes", "1"));
            //RBLBlocked.Items.Insert(0, new ListItem("Select", "0"));
            //RBLBlocked.SelectedValue = "0";
        }

        private void Select_Account_Crdt()
        {
            try
            {
                DataTable User = (DataTable)Session["user"];
                Rep_General general = new Rep_General();

                string userId = User.Rows[0]["UserID"].ToString();
                string accId = Request.QueryString[general.ObfuscateQueryString("accID")].ToString();
                int id = general.DecryptIds(int.Parse(accId));
                Rep_Account accountCrdt = new Rep_Account();

                DataTable accCrdtTable = accountCrdt.SelectCrdt(int.Parse(userId), id);

                if (accCrdtTable.Rows.Count > 0)
                {
                    txtCrdtNo.Text = accCrdtTable.Rows[0]["AccCrdtNo"].ToString();
                    dllCategory_1.Text = accCrdtTable.Rows[0]["AccCrdtGategory1"].ToString();
                    dllCategory_2.Text = accCrdtTable.Rows[0]["AccCrdtGategory2"].ToString();
                    dllResponsible.Text = accCrdtTable.Rows[0]["AccCrdtResp"].ToString();
                    dllType.Text = accCrdtTable.Rows[0]["AccCrdtType"].ToString();
                    txtMax.Text = accCrdtTable.Rows[0]["AccCrdtMax"].ToString();
                    txtExtra.Text = accCrdtTable.Rows[0]["AccCrdtExtra"].ToString();
                    txtMemo.InnerText = accCrdtTable.Rows[0]["AccCrdtMemo"].ToString();
                    RBLBlocked.Text = accCrdtTable.Rows[0]["AccCrdtStop"].ToString() == "True" ? "1" : "2";
                    //RBLBlocked.Items.FindByText(accCrdTable.Rows[0]["AccCrdtStop"].ToString()).Selected = true;
                }
                else
                {

                }

                string texttaccNo = Request.QueryString[general.ObfuscateQueryString("accNo")] != null ? Request.QueryString[general.ObfuscateQueryString("accNo")].ToString() : "";
                accNo.Text = texttaccNo != string.Empty ? general.DecryptIds(int.Parse(texttaccNo)).ToString() : "";

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

        private void Select_Account_Name()
        {
            try
            {
                Rep_General general = new Rep_General();
                DataTable User = (DataTable)Session["user"];

                string userId = User.Rows[0]["UserID"].ToString();
                string accId = "";
                if (Request.QueryString[general.ObfuscateQueryString("accID")] == null)
                {
                    accId = lblAccID.Text;
                }
                else
                {
                    string value = Request.QueryString[general.ObfuscateQueryString("accID")].ToString();
                    accId = general.DecryptIds(int.Parse(value)).ToString();
                }


                Rep_Account account = new Rep_Account();

                DataTable accTable = account.SelectAcc(int.Parse(userId), int.Parse(accId));

                if (accTable.Rows.Count > 0)
                {
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

        private void Udt_Account_Crdt()
        {
            try
            {
                Rep_General general = new Rep_General();
                Rep_Account accountCrdt = new Rep_Account();
                string value = Request.QueryString[general.ObfuscateQueryString("accID")].ToString();
                DataTable user = (DataTable)Session["user"];

                int userId = int.Parse(user.Rows[0]["UserID"].ToString());
                int accId = general.DecryptIds(int.Parse(value));
                int crdtNo = int.Parse(txtCrdtNo.Text);
                byte crdtCategory1 = byte.Parse(dllCategory_1.Text);
                byte crdtCategory2 = byte.Parse(dllCategory_2.Text);
                byte crdtType = byte.Parse(dllType.Text);
                double crdtMax = double.Parse(txtMax.Text);
                double crdtExtra = double.Parse(txtExtra.Text);
                byte crdtStop = byte.Parse(RBLBlocked.Text);
                byte crdtResp = byte.Parse(dllResponsible.Text);
                string crdtMemo = txtMemo.InnerText;

                string aUpdateCrdtccUdt = accountCrdt.UpdateCrdt(userId, accId, crdtNo, crdtCategory1, crdtCategory2, crdtType, crdtMax, crdtExtra, crdtStop, crdtResp, crdtMemo);

                Disable_Fields();
                Select_Account_Crdt();

                hidnViewFlag.Value = "View";
                lblSubject.Text = "View";
                Btn_Fields_Roles();

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", aUpdateCrdtccUdt), true);
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

                dtPermittedBtns = btnGeneral.Permitted_buttons(int.Parse(userId), "AccountCrdt");
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

        protected void Page_Init(object sender, EventArgs e)
        {
            Rep_General general = new Rep_General();

            if (Request.QueryString[general.ObfuscateQueryString("accID")] == null)
            {
                //Response.Redirect("~/Modules/Account/AccountSearch.aspx");
                try
                {
                    DataTable user = (DataTable)Session["user"];

                    int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                    string module = general.SelectModule(userId, "AccountSearch");
                    if (module != "Stop")
                    {
                        Response.Redirect("~/Modules/" + module + ".aspx");
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

        protected void Page_Load(object sender, EventArgs e)
        {
            Generate_Buttons();
            Get_Navegation_Panel();
            Rep_General general = new Rep_General();

            if (!IsPostBack)
            {
                Get_ddls();
                Get_AccCrdt_Blocked();
            }

            if (Request.QueryString[general.ObfuscateQueryString("accID")] != null)
            {
                if (!IsPostBack)
                {
                    string lblaccId = Request.QueryString[general.ObfuscateQueryString("accID")].ToString();
                    lblAccID.Text = general.DecryptIds(int.Parse(lblaccId)).ToString();
                    Select_Account_Crdt();
                    Select_Account_Name();
                    hidnViewFlag.Value = "View";
                    lblSubject.Text = "View";
                }
            }
            else if (hidnViewFlag.Value == "Update")
            {

            }
            else
            {
                if (!IsPostBack)
                {
                    hidnViewFlag.Value = "Add";
                    lblSubject.Text = "New";
                }
            }
            //get Roles after check the flag value
            Btn_Fields_Roles();
        }

        //New
        protected void B_21_OnClick(object sender, EventArgs e)
        {
            try
            {
                Rep_General accountSearch = new Rep_General();
                DataTable user = (DataTable)Session["user"];

                int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                string module = accountSearch.SelectModule(userId, "AccountAdd");
                if (module != "Stop")
                {
                    Response.Redirect("~/Modules/" + module + ".aspx");
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
                    Response.Redirect("~/Modules/" + module + ".aspx");
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
            try
            {
                Rep_General accountSearch = new Rep_General();
                DataTable user = (DataTable)Session["user"];

                int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                string module = accountSearch.SelectModule(userId, "AccountAdd");
                if (module != "Stop")
                {
                    int accId = accountSearch.EncryptIds(int.Parse(lblAccID.Text));
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

        //Save
        protected void B_29_OnClick(object sender, EventArgs e)
        {
            if (hidnViewFlag.Value == "Update")
            {
                Udt_Account_Crdt();
            }
            else if (accCheckName.Value == "1")
            {

            }
            else
            {
                try
                {
                    Udt_Account_Crdt();
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", "Saved"), true);
                    //hidnViewFlag.Value = "View";
                    //lblSubject.Text = "View";
                    //Btn_Fields_Roles();
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    string errText = error.Replace("\'", "");

                    string AlertMSG = "";
                    AlertMSG += errText;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", AlertMSG), true);

                    hidnViewFlag.Value = "View";
                    lblSubject.Text = "View";
                    Btn_Fields_Roles();
                }
            }
        }

        //refresh
        protected void B_18_OnClick(object sender, EventArgs e)
        {
            if (hidnMoveFlag.Value == "1")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("ShowCheckDialog();"), true);
            }
            else
            {

            }
        }

        //clear
        protected void B_19_OnClick(object sender, EventArgs e)
        {
            if (hidnMoveFlag.Value == "1")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("ShowCheckDialog();"), true);
            }
            else
            {

            }
        }

        //Edit
        protected void B_22_OnClick(object sender, EventArgs e)
        {
            hidnViewFlag.Value = "Update";
            lblSubject.Text = "Edit";
            Btn_Fields_Roles();
        }

        protected void btnSaveCat1_OnClick(object sender, EventArgs e)
        {
            try
            {
                Rep_Account crdtCategorySave = new Rep_Account();
                DataTable user = (DataTable)Session["user"];
                DataTable categoryDT = new DataTable();

                int userId = int.Parse(user.Rows[0]["UserID"].ToString());
                string crdtCategory = txtCategory1.Text.Replace(" ", string.Empty);

                categoryDT = SelectddlDataTable("SP_SysCode_AccCrdt_Category1", "");

                string find = "Category='" + crdtCategory + "'";
                //myDataTable.Select("columnName1 like '%" + value + "%'");
                DataRow[] foundRows = categoryDT.Select(find);
                if (foundRows.Length != 0)
                {
                    //Category is exist
                    lblCat1Msg.Text = "* This Category isn't allowed. Try again";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("ShowCat_1_Dialog();"), true);
                }
                else
                {
                    lblCat1Msg.Text = "";
                    crdtCategorySave.Crdt_Category1_Save(userId, crdtCategory);
                    txtCategory1.Text = string.Empty;
                }

                dllCategory_1.DataSource = SelectddlDataTable("SP_SysCode_AccCrdt_Category1", "");
                dllCategory_1.DataValueField = "CategoryID";
                dllCategory_1.DataTextField = "Category";
                dllCategory_1.DataBind();
                dllCategory_1.Items.Insert(0, new ListItem("- Select -", "0"));
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

        protected void btnSaveCat2_OnClick(object sender, EventArgs e)
        {
            try
            {
                Rep_Account crdtCategorySave = new Rep_Account();
                DataTable user = (DataTable)Session["user"];
                DataTable categoryDT = new DataTable();

                int userId = int.Parse(user.Rows[0]["UserID"].ToString());
                string crdtCategory = txtAddCategory2.Text.Replace(" ", string.Empty);

                categoryDT = SelectddlDataTable("SP_SysCode_AccCrdt_Category2", "");

                string find = "Category='" + crdtCategory + "'";
                //myDataTable.Select("columnName1 like '%" + value + "%'");
                DataRow[] foundRows = categoryDT.Select(find);
                if (foundRows.Length != 0)
                {
                    //Category is exist
                    lblCat2Msg.Text = "* This Category isn't allowed. Try again";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("ShowCat_2_Dialog();"), true);
                }
                else
                {
                    lblCat2Msg.Text = "";
                    crdtCategorySave.Crdt_Category2_Save(userId, crdtCategory);
                    txtAddCategory2.Text = string.Empty;
                }

                dllCategory_2.DataSource = SelectddlDataTable("SP_SysCode_AccCrdt_Category2", "");
                dllCategory_2.DataValueField = "CategoryID";
                dllCategory_2.DataTextField = "Category";
                dllCategory_2.DataBind();
                dllCategory_2.Items.Insert(0, new ListItem("- Select -", "0"));
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

        protected void lnkbtndntsve_OnClick(object sender, EventArgs e)
        {
            Select_Account_Crdt();
            Select_Account_Name();
            hidnViewFlag.Value = "View";
            lblSubject.Text = "View";
            Btn_Fields_Roles();
        }

        #region Navegation_Panel

        protected void panel_lnkAccView_OnClick(object sender, EventArgs e)
        {
            B_12_OnClick(null, null);
        }

        protected void panel_lnkAccSales_OnClick(object sender, EventArgs e)
        {
            Rep_General general = new Rep_General();
            if (accNo.Text != "")
            {
                try
                {
                    int accId = general.EncryptIds(int.Parse(lblAccID.Text));
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

                    int accId = general.EncryptIds(int.Parse(lblAccID.Text));
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

        protected void panel_lnkContactInfo_OnClick(object sender, EventArgs e)
        {
            try
            {
                Rep_General general = new Rep_General();
                DataTable user = (DataTable)Session["user"];

                int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                string module = general.SelectModule(userId, "AccountContact");
                if (module != "Stop")
                {
                    int accId = general.EncryptIds(int.Parse(lblAccID.Text));
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

        #endregion

    }
}