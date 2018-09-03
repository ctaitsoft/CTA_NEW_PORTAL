using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CTA_NEW_PORTAL.EB;

namespace CTA_NEW_PORTAL.Modules.Account
{
    public partial class AccountSales : System.Web.UI.Page
    {

        #region Global Fields

        public SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["GlobalConnection"].ConnectionString);

        #endregion Global Fields

        #region Navegation_Panel

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

        #endregion

        #region PageLoad and PageInit
        protected void Page_Load(object sender, EventArgs e)
        {
            Get_Navegation_Panel();

            if (!IsPostBack)
            {

                Generate_Buttons();
                Rep_General general = new Rep_General();

                if (Request.QueryString[general.ObfuscateQueryString("accID")] != null)
                {
                    if (hidnViewFlag.Value == "Update")
                    {
                        //hidnViewFlag.Value = "Update";
                        lblSubject.Text = "Edit";
                    }
                    else if (hidnViewFlag.Value == "Add")
                    {

                        //hidnViewFlag.Value = "Add";
                        lblSubject.Text = "New";

                    }

                    else
                    {
                        hidnViewFlag.Value = "View";
                        lblSubject.Text = "View";



                        AccIDEnc.Value = Request.QueryString[general.ObfuscateQueryString("accID")].ToString();

                        DataTable User = Session["user"] != null ? (DataTable)Session["user"] : new DataTable();
                        if (User.Rows.Count > 0)
                        {


                            UserID.Value = User.Rows[0]["UserID"].ToString();
                            lblAccID.Text = general.DecryptIds(int.Parse(AccIDEnc.Value)).ToString();
                            AccIDDec.Value = lblAccID.Text;







                            string texttaccNo = Request.QueryString[general.ObfuscateQueryString("accNo")] != null
                                ? Request.QueryString[general.ObfuscateQueryString("accNo")].ToString()
                                : "";
                            accNo.Text = texttaccNo != string.Empty ? general.DecryptIds(int.Parse(texttaccNo)).ToString() : "";

                            Rep_Account account = new Rep_Account();

                            DataTable accTable = account.SelectAcc(int.Parse(UserID.Value), int.Parse(AccIDDec.Value));

                            accName.Text = accTable.Rows.Count > 0 ? accTable.Rows[0]["AccName"].ToString() : string.Empty;


                            FillDDLs();

                            FillAccountSales(GetAccountSales());

                        }
                        else
                        {
                            B_11_OnClick(null, null);
                        }

                    }


                    //get Roles after check the flag value
                    Btn_Fields_Roles();
                    DisableEdit();



                }
                else
                {
                    B_11_OnClick(null, null);
                }

            }
            else
            {
                // FillAccountInfoGRD();
            }
        }

        #endregion PageLoad and PageInit


        #region Header Buttons


        #region Button Roles

        private void Btn_Fields_Roles()
        {
            if (hidnViewFlag.Value == "View")
            {
                //Disable_Fields();
                Buttons_permissions();

                //B_11.Enabled = true;
                //B_12.Enabled = true;
                //B_13.Enabled = true;


                //B_21.Enabled = false;
                //B_22.Enabled = false;
                //B_23.Enabled = false;

                B_29.Enabled = false;
                B_18.Enabled = true;
                B_19.Enabled = false;

                //Additional Buttons
                B_51.Enabled = false;
                //B_61.Enabled = false;
                //B_62.Enabled = false;

                //btnAddCat1.Enabled = false;
                //btnAddCat1.OnClientClick = null;
                //btnAddCat2.Enabled = false;
                //btnAddCat2.OnClientClick = null;
            }
            else if (hidnViewFlag.Value == "Add")
            {
                //Enable_Fields();

                Buttons_permissions();
                B_11.Enabled = true;
                B_12.Enabled = true;
                B_13.Enabled = true;


                B_21.Enabled = false;
                B_22.Enabled = false;
                B_23.Enabled = false;

                B_29.Enabled = false;
                B_18.Enabled = true;
                B_19.Enabled = false;

                //Additional Buttons
                B_61.Enabled = false;
                B_62.Enabled = false;

                //btnAddTitle.Enabled = true;
                //btnAddTitle.OnClientClick = "ShowTitleDialog(); return false;";
            }
            else if (hidnViewFlag.Value == "Update")
            {
                //Enable_Fields();
                Buttons_permissions();
                B_11.Enabled = true;
                B_12.Enabled = true;
                B_13.Enabled = true;

                B_21.Enabled = false;
                B_22.Enabled = false;
                B_23.Enabled = false;

                B_29.Enabled = false;
                B_18.Enabled = true;
                B_19.Enabled = false;

                //Additional Buttons
                B_61.Enabled = false;
                B_62.Enabled = false;

                //btnAddCat1.Enabled = true;
                //btnAddCat1.OnClientClick = "ShowCat_1_Dialog(); return false;";
                //btnAddCat2.Enabled = true;
                //btnAddCat2.OnClientClick = "ShowCat_2_Dialog(); return false;";
            }
        }

        #endregion Button Roles

        #region Generate Button

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

        #endregion Generate Button

        #region Button Permissions
        private void Buttons_permissions()
        {
            try
            {
                Rep_General btnGeneral = new Rep_General();
                DataTable dtPermittedBtns = new DataTable();
                DataTable User = (DataTable)Session["user"];
                string userId = User.Rows[0]["UserID"].ToString();

                dtPermittedBtns = btnGeneral.Permitted_buttons(int.Parse(userId), "AccountSales");
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

                string opt1 = B_51.ID.Replace("B_", "").ToString();
                string opt2 = B_61.ID.Replace("B_", "").ToString();
                string opt3 = B_62.ID.Replace("B_", "").ToString();
                string[] options = new string[] { opt1, opt2, opt3 };
                foreach (var str in options)
                {
                    string find = "OptionID=" + str;
                    DataRow[] foundRows = dtPermittedBtns.Select(find);

                    LinkButton linkButton = (LinkButton)AccountUpdatePanel_Sales.FindControl("B_" + str);

                    if (foundRows.Length != 0)
                    {

                        linkButton.Enabled = true;
                    }
                    else
                    {
                        linkButton.Enabled = false;
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

        #endregion Button Permissions

        #region Buttons Events

        #region New
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
                    //int accId = accountSearch.EncryptIds(int.Parse(selectinganItem["AccID"].Text));
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
        #endregion New

        #region Inquiry
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
        #endregion Inquiry

        #region View
        //Inquiry
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
                    //int accId = accountSearch.EncryptIds(int.Parse(selectinganItem["AccID"].Text));
                    Response.Redirect("~/Modules/" + module + ".aspx?" + accountSearch.ObfuscateQueryString("accID") + "=" + AccIDEnc.Value + "");
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
        #endregion View

        #region Save
        //Save
        protected void B_29_OnClick(object sender, EventArgs e)
        {
            string ErrorMSG = "Please Fill";
            //if (hidnViewFlag.Value == "Add")
            //{
            if (rblTaxable.SelectedValue == string.Empty || ddlCode.SelectedValue == "-1")
            {
                if (rblTaxable.SelectedValue == string.Empty)
                {
                    ErrorMSG += " *Taxable or not";
                }

                if (ddlCode.SelectedValue == "-1")
                {
                    ErrorMSG += " *Taxable Code";
                }


                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", ErrorMSG), true);
                return;

            }
            else
            {
                UpdateAccountSales();
                B_29.Enabled = false;
                DisableEdit();
                B_22.Enabled = true;
            }



            //}
            //else
            //{
            //    UpdateAccountSales();
            //}

        }

        private void UpdateAccountSales()
        {
            try
            {


                connection.Open();
                SqlCommand cmd = new SqlCommand("SP_AccountSales_Udt", connection);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@UserID", UserID.Value);
                cmd.Parameters.AddWithValue("@AccID", AccIDDec.Value);
                if (dllCurrency.SelectedValue != "-1")
                {
                    cmd.Parameters.AddWithValue("@AccCurrency", dllCurrency.SelectedValue);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@AccCurrency", string.Empty);
                }

                if (ddlpriceList.SelectedValue != "-1")
                {
                    cmd.Parameters.AddWithValue("@AccPriceList", string.Empty);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@AccPriceList", string.Empty);
                }

                if (ddlDiscountType.SelectedValue != "-1")
                {
                    cmd.Parameters.AddWithValue("@AccDisType", ddlDiscountType.SelectedValue);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@AccDisType", string.Empty);
                }

                if (ddlDiscountModel.SelectedValue != "-1")
                {
                    cmd.Parameters.AddWithValue("@AccDisModel", ddlDiscountModel.SelectedValue);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@AccDisModel", string.Empty);
                }

                if (ddlCode.SelectedValue != "-1")
                {
                    cmd.Parameters.AddWithValue("@AccTaxCode", ddlCode.SelectedValue);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@AccTaxCode", string.Empty);
                }

                if (ddlPaymentType.SelectedValue != "-1")
                {
                    cmd.Parameters.AddWithValue("@AccPayType", ddlPaymentType.SelectedValue);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@AccPayType", string.Empty);
                }

                if (ddlRespPerson.SelectedValue != "-1")
                {
                    cmd.Parameters.AddWithValue("@AccSalesResp", ddlRespPerson.SelectedValue);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@AccSalesResp", string.Empty);
                }

                cmd.Parameters.AddWithValue("@AccDisValue", txtGeneral.Text);
                cmd.Parameters.AddWithValue("@AccDisValueP", txtParts.Text);
                cmd.Parameters.AddWithValue("@AccDisValueS", txtService.Text);
                cmd.Parameters.AddWithValue("@AccTaxable", rblTaxable.SelectedValue);
                cmd.Parameters.AddWithValue("@AccSalesMemo", txtMemo.InnerText);
                cmd.ExecuteNonQuery();




            }


            catch (Exception ex)
            {

                string Error = ex.Message;
                string AlertMSG = "لم تتم العملية , يرجى إعادة المحاولة";
                AlertMSG += "\n" + Error;
                this.ClientScript.RegisterStartupScript(this.GetType(), this.GetType().Name,
                    string.Format("window.alert('{0}');", AlertMSG), true);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void InsertAccountSales()
        {
            try
            {


                connection.Open();
                SqlCommand cmd = new SqlCommand("SP_AccountSales_Udt", connection);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@UserID", UserID.Value);
                cmd.Parameters.AddWithValue("@AccID", AccIDDec.Value);
                cmd.Parameters.AddWithValue("@AccCurrency", dllCurrency.SelectedValue);
                cmd.Parameters.AddWithValue("@AccPriceList", ddlpriceList.SelectedValue);
                cmd.Parameters.AddWithValue("@AccDisType", ddlDiscountType.SelectedValue);
                cmd.Parameters.AddWithValue("@AccDisModel", ddlDiscountModel.SelectedValue);
                cmd.Parameters.AddWithValue("@AccDisValue", txtGeneral.Text);
                cmd.Parameters.AddWithValue("@AccDisValueP", txtParts.Text);
                cmd.Parameters.AddWithValue("@AccDisValueS", txtService.Text);
                cmd.Parameters.AddWithValue("@AccTaxable", rblTaxable.SelectedValue);
                cmd.Parameters.AddWithValue("@AccTaxCode", ddlCode.SelectedValue);
                cmd.Parameters.AddWithValue("@AccPayType", ddlPaymentType.SelectedValue);
                cmd.Parameters.AddWithValue("@AccSalesResp", ddlRespPerson.SelectedValue);
                cmd.Parameters.AddWithValue("@AccSalesMemo", txtMemo.InnerText);
                cmd.ExecuteNonQuery();

                DisableAdd();


            }


            catch (Exception ex)
            {

                string Error = ex.Message;
                string AlertMSG = "لم تتم العملية , يرجى إعادة المحاولة";
                AlertMSG += "\n" + Error;
                this.ClientScript.RegisterStartupScript(this.GetType(), this.GetType().Name,
                    string.Format("window.alert('{0}');", AlertMSG), true);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        #endregion Save

        #region Refresh
        //refresh
        protected void B_18_OnClick(object sender, EventArgs e)
        {
            //FillAccountInfoCategoriesGRD();
            //FillAccountInfoGRD();

            hidnViewFlag.Value = "View";
            lblSubject.Text = "View";
            Page.Response.Redirect(Page.Request.Url.ToString(), true);

            //if (hidnMoveFlag.Value == "1")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("ShowCheckDialog();"), true);
            //}
            //else
            //{

            //}
        }
        #endregion Refresh

        #region Clear
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
        #endregion Clear

        #region Edit
        //Edit
        protected void B_22_OnClick(object sender, EventArgs e)
        {
            hidnViewFlag.Value = "Update";
            lblSubject.Text = "Edit";
            Btn_Fields_Roles();
            EnableEdit();
            B_29.Enabled = true;

        }
        #endregion Edit

        #region Parts
        // Show Parts Dialog
        protected void B_61_OnClick(object sender, EventArgs e)
        {
            //txtParts.Enabled = true;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("ShowpartsDialog();"), true);
        }
        #endregion

        #region Services
        // Show Services Dialog
        protected void B_62_OnClick(object sender, EventArgs e)
        {
            //txtService.Enabled = true;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("ShowserviceDialog();"), true);
        }
        #endregion

        #region Add Currency
        protected void B_51_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("ShowcurrencyDialog();"), true);
        }
        #endregion


        #endregion

        #endregion Header Buttons

        #region Get Account Sales

        public DataTable GetAccountSales()
        {
            DataTable AccountSalesDT = new DataTable();

            try
            {


                connection.Open();

                SqlCommand cmd = new SqlCommand("SP_AccountSales", connection);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@UserID", UserID.Value);
                cmd.Parameters.AddWithValue("@AccID", AccIDDec.Value);




                SqlDataAdapter AccountSalesDA = new SqlDataAdapter(cmd);
                AccountSalesDA.Fill(AccountSalesDT);

            }


            catch (Exception ex)
            {

                string Error = ex.Message;
                string AlertMSG = "لم تتم العملية , يرجى إعادة المحاولة";
                AlertMSG += "\n" + Error;
                this.ClientScript.RegisterStartupScript(this.GetType(), this.GetType().Name,
                    string.Format("window.alert('{0}');", AlertMSG), true);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return AccountSalesDT;

        }


        public void FillAccountSales(DataTable AccountSalesDT)
        {


            // AccID AccCurrency AccPriceList AccDisType  AccDisModel AccDisValue AccDisValueP AccDisValueS    AccTaxable AccTaxCode  AccPayType AccSalesResp    AccSalesMemo
            if (AccountSalesDT.Rows.Count > 0)
            {


                dllCurrency.SelectedValue = !DBNull.Value.Equals(AccountSalesDT.Rows[0]["AccCurrency"]) ? AccountSalesDT.Rows[0]["AccCurrency"].ToString() : string.Empty;

                ddlpriceList.SelectedValue = !DBNull.Value.Equals(AccountSalesDT.Rows[0]["AccPriceList"]) ? AccountSalesDT.Rows[0]["AccPriceList"].ToString() : string.Empty;

                ddlDiscountType.SelectedValue = !DBNull.Value.Equals(AccountSalesDT.Rows[0]["AccDisType"]) ? AccountSalesDT.Rows[0]["AccDisType"].ToString() : string.Empty;

                ddlDiscountModel.SelectedValue = !DBNull.Value.Equals(AccountSalesDT.Rows[0]["AccDisModel"]) ? AccountSalesDT.Rows[0]["AccDisModel"].ToString() : string.Empty;

                //rblTaxable.SelectedValue= !DBNull.Value.Equals(AccountSalesDT.Rows[0]["AccTaxable"]) ? Convert.ToInt32(AccountSalesDT.Rows[0]["AccTaxable"].ToString().ToLower()).ToString()  : string.Empty;

                rblTaxable.SelectedValue = Convert.ToBoolean(AccountSalesDT.Rows[0]["AccTaxable"].ToString()) ? "1" : "0";

                //ddlCode.SelectedValue = !DBNull.Value.Equals(AccountSalesDT.Rows[0]["AccTaxCode"]) ? AccountSalesDT.Rows[0]["AccTaxCode"].ToString() : string.Empty;

                ddlCode.SelectedValue = AccountSalesDT.Rows[0]["AccTaxCode"].ToString();

                ddlPaymentType.SelectedValue = !DBNull.Value.Equals(AccountSalesDT.Rows[0]["AccPayType"]) ? AccountSalesDT.Rows[0]["AccPayType"].ToString() : string.Empty;

                ddlRespPerson.SelectedValue = !DBNull.Value.Equals(AccountSalesDT.Rows[0]["AccSalesResp"]) ? AccountSalesDT.Rows[0]["AccSalesResp"].ToString() : string.Empty;


                txtGeneral.Text = !DBNull.Value.Equals(AccountSalesDT.Rows[0]["AccDisValue"]) ? AccountSalesDT.Rows[0]["AccDisValue"].ToString() : string.Empty;

                txtParts.Text = !DBNull.Value.Equals(AccountSalesDT.Rows[0]["AccDisValueP"]) ? AccountSalesDT.Rows[0]["AccDisValueP"].ToString() : string.Empty;

                txtService.Text = !DBNull.Value.Equals(AccountSalesDT.Rows[0]["AccDisValueS"]) ? AccountSalesDT.Rows[0]["AccDisValueS"].ToString() : string.Empty;

                txtMemo.InnerText = !DBNull.Value.Equals(AccountSalesDT.Rows[0]["AccSalesMemo"]) ? AccountSalesDT.Rows[0]["AccSalesMemo"].ToString() : string.Empty;

            }

        }

        #endregion Get Account Sales


        #region Enable Disable Add Edit

        public void EnableAdd()
        {



        }

        public void DisableAdd()
        {
            dllCurrency.Enabled = false;
            ddlpriceList.Enabled = false;
            ddlDiscountType.Enabled = false;
            ddlDiscountModel.Enabled = false;
            txtGeneral.Enabled = false;
            txtParts.Enabled = false;
            txtService.Enabled = false;
            rblTaxable.Enabled = false;
            ddlCode.Enabled = false;
            ddlPaymentType.Enabled = false;
            ddlRespPerson.Enabled = false;
            txtMemo.Disabled = true;

        }


        public void DisableEdit()
        {
            dllCurrency.Enabled = false;
            ddlpriceList.Enabled = false;
            ddlDiscountType.Enabled = false;
            ddlDiscountModel.Enabled = false;
            txtGeneral.Enabled = false;
            txtParts.Enabled = false;
            txtService.Enabled = false;
            rblTaxable.Enabled = false;
            ddlCode.Enabled = false;
            ddlPaymentType.Enabled = false;
            ddlRespPerson.Enabled = false;
            txtMemo.Disabled = true;
        }

        public void EnableEdit()
        {

            dllCurrency.Enabled = true;
            ddlpriceList.Enabled = true;
            ddlDiscountType.Enabled = true;
            ddlDiscountModel.Enabled = true;
            txtGeneral.Enabled = true;
            txtParts.Enabled = true;
            txtService.Enabled = true;
            rblTaxable.Enabled = true;
            ddlCode.Enabled = true;
            ddlPaymentType.Enabled = true;
            ddlRespPerson.Enabled = true;
            txtMemo.Disabled = false;
        }

        #endregion Enable Disable Add


        #region Fill DDLs

        public void FillDDLs()
        {
            // TaxID Tax TaxValue
            ddlCode.DataSource = DDLsData("SP_SysCode_All_Tax");
            ddlCode.DataTextField = "Tax";
            ddlCode.DataValueField = "TaxID";
            ddlCode.DataBind();
            ddlCode.Items.Insert(0, new ListItem("Select Tax", "-1"));
            // TaxID Tax TaxValue


            // PayTypeID	PayType
            ddlPaymentType.DataSource = DDLsData("SP_SysCode_AccSales_PayType");
            ddlPaymentType.DataTextField = "PayType";
            ddlPaymentType.DataValueField = "PayTypeID";
            ddlPaymentType.DataBind();
            ddlPaymentType.Items.Insert(0, new ListItem("Select Type", "-1"));
            // PayTypeID	PayType

            // UserID	UserName
            ddlRespPerson.DataSource = DDLsData("SP_SysUser_RespSales");
            ddlRespPerson.DataTextField = "UserName";
            ddlRespPerson.DataValueField = "UserID";
            ddlRespPerson.DataBind();
            ddlRespPerson.Items.Insert(0, new ListItem("Select Salesman", "-1"));
            // UserID	UserName

            // CurrencyID	Currency
            dllCurrency.DataSource = DDLsData("SP_SysCode_All_Currency");
            dllCurrency.DataTextField = "Currency";
            dllCurrency.DataValueField = "CurrencyID";
            dllCurrency.DataBind();
            dllCurrency.Items.Insert(0, new ListItem("Select Currency", "-1"));
            // CurrencyID	Currency


            // PriceListID	PriceList
            ddlpriceList.DataSource = DDLsData("SP_Sales_PriceList");
            ddlpriceList.DataTextField = "Text";
            ddlpriceList.DataValueField = "Value";
            ddlpriceList.DataBind();
            ddlpriceList.Items.Insert(0, new ListItem("Select Price List", "-1"));
            // PriceListID	PriceList

            // DisTypeID	DisType
            ddlDiscountType.DataSource = DDLsData("SP_SysCode_AccSales_DisType");
            ddlDiscountType.DataTextField = "DisType";
            ddlDiscountType.DataValueField = "DisTypeID";
            ddlDiscountType.DataBind();
            ddlDiscountType.Items.Insert(0, new ListItem("Select Type", "-1"));
            // DisTypeID	DisType

            // DisModelID	DisModel
            ddlDiscountModel.DataSource = DDLsData("SP_Sales_DisModel");
            ddlDiscountModel.DataTextField = "DisModel";
            ddlDiscountModel.DataValueField = "DisModelID";
            ddlDiscountModel.DataBind();
            ddlDiscountModel.Items.Insert(0, new ListItem("Select Model", "-1"));
            // DisModelID	DisModel






        }

        public DataTable DDLsData(string ProcedureName)
        {

            DataTable DDLsDT = new DataTable();



            try
            {


                connection.Open();

                SqlCommand cmd = new SqlCommand(ProcedureName, connection);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@UserID", UserID.Value);




                SqlDataAdapter DDLsDA = new SqlDataAdapter(cmd);
                DDLsDA.Fill(DDLsDT);

            }


            catch (Exception ex)
            {

                string Error = ex.Message;
                string AlertMSG = "لم تتم العملية , يرجى إعادة المحاولة";
                AlertMSG += "\n" + Error;
                this.ClientScript.RegisterStartupScript(this.GetType(), this.GetType().Name,
                    string.Format("window.alert('{0}');", AlertMSG), true);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }


            return DDLsDT;

            // InfoTypeDLL.Items.Insert(0, new ListItem("Select a Type", "-1"));
        }

        #endregion Fill DDLs


        #region Not Used

        public DataTable FillAccountInfoCategoriesDT()
        {

            DataTable AccountInfoCategoriesDT = new DataTable();

            try
            {


                connection.Open();
                SqlCommand cmd = new SqlCommand("SP_SysCode_AccInfo_Type", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserID.Value);
                cmd.Parameters.AddWithValue("@AccCategory", AccType.Value);
                SqlDataAdapter AccountInfoCategoriesDA = new SqlDataAdapter(cmd);
                AccountInfoCategoriesDA.Fill(AccountInfoCategoriesDT);

            }


            catch (Exception ex)
            {

                string Error = ex.Message;
                string AlertMSG = "لم تتم العملية , يرجى إعادة المحاولة";
                AlertMSG += "\n" + Error;
                this.ClientScript.RegisterStartupScript(this.GetType(), this.GetType().Name,
                    string.Format("window.alert('{0}');", AlertMSG), true);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }


            return AccountInfoCategoriesDT;
        }

        #endregion Not Used

        #region Navegation Panel

        protected void panel_lnkViewInfo_OnClick(object sender, EventArgs e)
        {
            Rep_General general = new Rep_General();

            try
            {
                Rep_General accountSearch = new Rep_General();
                DataTable user = (DataTable)Session["user"];

                int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                string module = accountSearch.SelectModule(userId, "AccountAdd");
                if (module != "Stop")
                {
                    int accId = int.Parse(AccIDEnc.Value);
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

        protected void panel_lnkAccInfo_OnClick(object sender, EventArgs e)
        {
            Rep_General general = new Rep_General();
            if (accNo.Text != "")
            {
                try
                {
                    int accId = int.Parse(AccIDEnc.Value);
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
                    int accId = int.Parse(AccIDEnc.Value);
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

        protected void panel_lnkCrdtInfo_OnClick(object sender, EventArgs e)
        {
            Rep_General general = new Rep_General();

            try
            {

                DataTable user = (DataTable)Session["user"];

                int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                string module = general.SelectModule(userId, "AccountCrdt");
                if (module != "Stop")
                {
                    int accId = int.Parse(AccIDEnc.Value);
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

        #endregion


        protected void rblTaxable_OnTextChanged(object sender, EventArgs e)
        {
            if (rblTaxable.SelectedValue == "0")
            {
                try
                {
                    int x = ddlCode.Items.IndexOf(ddlCode.Items.FindByValue("0")); // If you want to find text by value field.
                    ddlCode.Items.RemoveAt(x);
                }
                catch (Exception exception)
                {

                }


                ddlCode.Items.Insert(1, new ListItem("No Tax", "0"));
                ddlCode.SelectedValue = "0";
                ddlCode.Enabled = false;
                string TaxMSG = "Non Taxable means Tax = 0%";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", TaxMSG), true);
                return;
            }
            else
            {
                int x = ddlCode.Items.IndexOf(ddlCode.Items.FindByValue("0")); // If you want to find text by value field.
                ddlCode.Items.RemoveAt(x);
                ddlCode.SelectedValue = "1";
                ddlCode.Enabled = true;
                return;
            }
        }

        protected void btnSaveCurrency_OnClick(object sender, EventArgs e)
        {
            try
            {
                Rep_Account accCurrencySave = new Rep_Account();
                DataTable user = (DataTable)Session["user"];
                DataTable currencyDataTable = new DataTable();

                int userId = int.Parse(user.Rows[0]["UserID"].ToString());
                string currency = txtCurrency.Text.Replace(" ", string.Empty);

                currencyDataTable = DDLsData("SP_SysCode_All_Currency");

                string find = "Currency='" + currency + "'";

                DataRow[] foundRows = currencyDataTable.Select(find);
                if (foundRows.Length != 0)
                {
                    //currency is exist
                    lblCurrencyMsg.Text = "* This currency isn't allowed. Try again";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("ShowcurrencyDialog();"), true);
                }
                else
                {
                    lblCurrencyMsg.Text = "";
                    accCurrencySave.Acc_Currency_Save(userId, currency);
                    txtCurrency.Text = string.Empty;

                    dllCurrency.DataSource = DDLsData("SP_SysCode_All_Currency");
                    dllCurrency.DataTextField = "Currency";
                    dllCurrency.DataValueField = "CurrencyID";
                    dllCurrency.DataBind();
                    dllCurrency.Items.Insert(0, new ListItem("Select Currency", "-1"));
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

        protected void lBSave_Parts_OnClick(object sender, EventArgs e)
        {
            try
            {
                Rep_Account accPartsSave = new Rep_Account();
                DataTable user = (DataTable)Session["user"];

                int userId = int.Parse(user.Rows[0]["UserID"].ToString());
                string accId = AccIDDec.Value;
                float parts = float.Parse(txtParts_2.Text);

                accPartsSave.Acc_Parts_Save(userId, accId, parts);
                txtParts_2.Text = string.Empty;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", "Saved"), true);
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

        protected void lbSave_Service_OnClick(object sender, EventArgs e)
        {
            try
            {
                Rep_Account accServiceSave = new Rep_Account();
                DataTable user = (DataTable)Session["user"];

                int userId = int.Parse(user.Rows[0]["UserID"].ToString());
                string accId = AccIDDec.Value;
                float service = float.Parse(txtService_2.Text);

                accServiceSave.Acc_Service_Save(userId, accId, service);
                txtService_2.Text = string.Empty;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", "Saved"), true);
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
}