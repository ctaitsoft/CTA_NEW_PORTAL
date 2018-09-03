using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using Telerik.Web.UI;
using System.Web.UI;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using CTA_NEW_PORTAL.EB;
using Telerik.Web.UI.GridExcelBuilder;

namespace CTA_NEW_PORTAL.Modules.Account
{
    public partial class AccountSearch : System.Web.UI.Page
    {
        public SqlConnection DbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["GlobalConnection"].ConnectionString);
        //bool WindowsFlag = false;

        protected string ModuleName = "Accounts";
        public string TriggerId
        {
            set
            {
                //AccountUpdatePanel.Triggers.Clear();
                //AccountUpdatePanel.Update();
                PostBackTrigger trigger = new PostBackTrigger();

                trigger.ControlID = value.ToString();
                AccountUpdatePanel.Triggers.Add(trigger);

            }
        }

        private void Navegation_Command(string args, string cmdName)
        {
            if (cmdName == "AccountSearch")
            {

            }
            else if (cmdName == "AccountAdd")
            {
                B_12_OnClick(null, null);
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
                dtPermittedBtns = btnGeneral.Permitted_buttons(int.Parse(userId), "AccountSearch");

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

        private DataTable SelectddlDataTable(string proceduer, string param)
        {
            DataTable ddlTable = new DataTable();

            try
            {
                Rep_Account accountCrdtDdlsDetails = new Rep_Account();
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

        private void Get_ContactType()
        {
            try
            {
                Rep_General general = new Rep_General();
                Rep_Account accountUdt = new Rep_Account();
                DataTable User = (DataTable)Session["user"];
                DataTable accUdtTable = new DataTable();

                string userId = User.Rows[0]["UserID"].ToString();
                string accId = "";
                if (Request.QueryString[general.ObfuscateQueryString("accID")] != null)
                {
                    string value = Request.QueryString[general.ObfuscateQueryString("accID")].ToString();
                    accId = general.DecryptIds(int.Parse(value)).ToString();
                    accUdtTable = accountUdt.SelectAcc(int.Parse(userId), int.Parse(accId));
                }
                else
                {

                }

                ddlContactType.DataSource = SelectddlDataTable("SP_SysCode_AccContact_Type", accUdtTable.Rows[0]["AccCategory"].ToString());
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
            Get_Navegation_Panel();

            Rep_General general = new Rep_General();
            if (!IsPostBack)
            {
                if (Request.QueryString[general.ObfuscateQueryString("accID")] != null)
                {
                    CustomersGRD.MasterTableView.Columns.FindByUniqueName("EditCustomer").Display = false;
                    Get_ContactType();
                    Select_Account_Name();
                    accHeader.Attributes["style"] = @"display: normal;";
                }
                else
                {
                    CustomersGRD.MasterTableView.Columns.FindByUniqueName("LinkCustomer").Display = false;
                }

                //DataTable InitialDT = SelectCustomers();
                //CustomersGRD.DataSource = InitialDT;
                //CustomersGRD.DataBind();

                //AddCustomerWindow.VisibleOnPageLoad = false;
            }
            else
            {
                //if (!WindowsFlag)
                //{
                //    AddCustomerWindow.VisibleOnPageLoad = false;
                //}
                //else
                //{
                //    AddCustomerWindow.VisibleOnPageLoad = true;
                //    WindowsFlag = false;
                //}
            }
        }

        protected void SearchBTN_Click(object sender, EventArgs e)
        {
            if (SearchTXT.Text == string.Empty)
            {
                CustomersGRD.DataSource = SelectCustomers();
                CustomersGRD.DataBind();
            }
            else
            {
                //CustomersGRD.MasterTableView.CommandItemSettings.ShowExportToCsvButton = false;
                ////ShowExportToExcelButton
                //CustomersGRD.DataSource = SelectCustomersView();
                //CustomersGRD.DataBind();
                CustomersGRD.DataSource = SelectCustomers();
                CustomersGRD.DataBind();
            }
        }

        protected void ClearSearchBTN_Click(object sender, EventArgs e)
        {
            SearchTXT.Text = "";
            //DataTable InitialDT = new DataTable();
            //CustomersGRD.DataSource = InitialDT;
            //CustomersGRD.DataBind();
            CustomersGRD.DataSource = SelectCustomers();
            CustomersGRD.DataBind();
        }

        private DataTable SelectCustomers()
        {
            //string SelectCommand = "SELECT  Account.AccID, Account.AccNo, Account.AccName, Account.Cancelled, AccountInfo.LineID, AccountInfo.InfoType, AccountInfo.InfoData, AccountInfo.Cancelled AS Expr1 "
            //                                             + " FROM Account INNER JOIN                                                                     "
            //                                             + " AccountInfo ON Account.AccID = AccountInfo.AccID                                            "
            //                                             + " and Account.Cancelled = 0                                                                   "
            //                                             + " and CONCAT(Account.AccNo ,' ' , Account.AccName ,' ', AccountInfo.InfoData) like @search       ";

            //SqlCommand SqlSelectCommand = new SqlCommand(SelectCommand, CustomersConnection);
            // SqlSelectCommand.Parameters.Add("@SearchText", SqlDbType.NVarChar, 65);
            //     SqlSelectCommand.Parameters.Add("@SearchText", System.Data.SqlDbType.Text);
            //     SqlSelectCommand.Parameters["@SearchText"].Value = SearchTXT.Text;

            //     adapter.InsertCommand.Parameters.Add("@CompanyName",
            //SqlDbType.VarChar, 40, "CompanyName");

            //SqlDataAdapter CustomersDA = new SqlDataAdapter("SP_Account_Search", CustomersConnection);
            //CustomersDA.SelectCommand.CommandType = CommandType.StoredProcedure;
            //CustomersDA.SelectCommand.Parameters.AddWithValue("@UserID", userId);
            //CustomersDA.SelectCommand.Parameters.AddWithValue("@SearchType", "Mobile");
            //CustomersDA.SelectCommand.Parameters.AddWithValue("@ModuleName", "AccountSearch");
            //CustomersDA.SelectCommand.Parameters.AddWithValue("@search", "%" + SearchTXT.Text + "%");
            //CustomersDA.SelectCommand.Parameters.AddWithValue("@SearchText", SearchTXT.Text);
            //Add(SearchTXT.Text);
            //CustomersDA.Fill(CustomersDT);

            DataTable CustomersDT = new DataTable();
            DataTable boundTable = new DataTable();
            try
            {
                Rep_Account accountSearch = new Rep_Account();
                Rep_General general = new Rep_General();

                DataTable User = (DataTable)Session["user"];
                string userId = User.Rows[0]["UserID"].ToString();

                CustomersDT = accountSearch.SelectSearch(int.Parse(userId), "Mobile", SearchTXT.Text, "AccountSearch");

                //if query string 
                if (Request.QueryString[general.ObfuscateQueryString("accID")] != null)
                {
                    DataTable contactsDt = new DataTable();
                    int accId = general.DecryptIds(int.Parse(Request.QueryString[general.ObfuscateQueryString("accID")].ToString()));
                    contactsDt = accountSearch.SelectContactSearch(int.Parse(userId), accId.ToString());

                    //var rows = from r in CustomersDT.AsEnumerable()
                    //    //make sure there aren't any matching names in dt2
                    //    where contactsDt.AsEnumerable().Any(r2 => r["AccID"].ToString() != r2["AccID"].ToString() && r["AccID"].ToString() != accId.ToString())
                    //    select r;

                    //var rows = CustomersDT.AsEnumerable().Where(r => contactsDt.AsEnumerable()
                    //    //make sure there aren't any matching names in dt2
                    //    .Any( r2 => r["AccID"].ToString() != r2["AccID"].ToString() ));

                    //boundTable = rows.CopyToDataTable();

                    if (CustomersDT.Rows.Count > 0)
                    {
                        boundTable = CustomersDT.Rows.OfType<DataRow>()
                            .Where(a => CustomersDT.Rows.OfType<DataRow>().Select(k => Convert.ToInt32(k["AccID"])).Except(contactsDt.Rows.OfType<DataRow>()
                                            .Select(k => Convert.ToInt32(k["AccID"])).ToList()).Contains(Convert.ToInt32(a["AccID"])) && Convert.ToInt32(a["AccID"]) != accId).CopyToDataTable();
                    }
                }
                else
                {
                    if (CustomersDT.Rows.Count > 0)
                    {
                        boundTable = CustomersDT;
                    }

                }
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

            return boundTable;
        }

        private DataTable SelectCustomersView()
        {

            DataTable CustomersDT = new DataTable();


            string SelectCommand = "SELECT  * from AccountSearchMobile where "
                                                         + "  CONCAT(AccNo ,' ' , AccName ,' ', AccMobile1 , ' ' , AccMobile2) like @search";



            //string SelectCommand = "SELECT  Account.AccID, Account.AccNo, Account.AccName, Account.Cancelled, AccountInfo.LineID, AccountInfo.InfoType, AccountInfo.InfoData, AccountInfo.Cancelled AS Expr1 "
            //                                            + " FROM Account INNER JOIN                                                                     "
            //                                            + " AccountInfo ON Account.AccID = AccountInfo.AccID                                            "
            //                                            + " and Account.Cancelled = 0                                                                   "
            //                                            + " and CONCAT(Account.AccNo ,' ' , Account.AccName ,' ', AccountInfo.InfoData) like @search       ";

            try
            {



                //SqlCommand SqlSelectCommand = new SqlCommand(SelectCommand, CustomersConnection);
                // SqlSelectCommand.Parameters.Add("@SearchText", SqlDbType.NVarChar, 65);
                //     SqlSelectCommand.Parameters.Add("@SearchText", System.Data.SqlDbType.Text);
                //     SqlSelectCommand.Parameters["@SearchText"].Value = SearchTXT.Text;

                //     adapter.InsertCommand.Parameters.Add("@CompanyName",
                //SqlDbType.VarChar, 40, "CompanyName");


                SqlDataAdapter CustomersDA = new SqlDataAdapter(SelectCommand, DbConnection);
                CustomersDA.SelectCommand.Parameters.AddWithValue("@search", "%" + SearchTXT.Text + "%");
                // CustomersDA.SelectCommand.Parameters.AddWithValue("@SearchText", SearchTXT.Text);
                //Add(SearchTXT.Text);
                CustomersDA.Fill(CustomersDT);






            }
            catch (Exception ex)
            {
                string Error = ex.Message;
                string AlertMSG = "Error occurred please try again";
                AlertMSG += Error;
                this.ClientScript.RegisterStartupScript(this.GetType(), this.GetType().Name,
                string.Format("window.alert('{0}');", AlertMSG), true);
                return CustomersDT;


            }

            finally
            {
                if (DbConnection.State == ConnectionState.Open)
                {
                    DbConnection.Close();
                }
            }

            return CustomersDT;
        }

        protected void CustomersGRD_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "EditCommand")
            {
                //string strKey = e.CommandArgument.ToString();
                string strr = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["AccID"].ToString();
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

            #region Link
            else if (e.CommandName == "LinkCommand")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("ShowconfirmDialog();"), true);
                lblAccId.Text = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["AccID"].ToString();
            }
            #endregion Link

            #region Refresh Grid
            else if (e.CommandName == "RebindGrid")
            {
                SearchBTN_Click(null, null);
            }
            #endregion Refresh Grid


            #region Else
            else
            {
                SearchBTN_Click(null, null);
            }
            #endregion Else
        }

        protected void AddCustomerBTN_Click(object sender, EventArgs e)
        {
            //WindowsFlag = true;
            //AddCustomerWindow.Visible = true;
            //AddCustomerWindow.VisibleOnPageLoad = true;
            //Response.Redirect("CreateAccount.aspx");

            //Response.Redirect("~/Modules/Account/AccountCreate.aspx");
            //Response.Redirect("~/Modules/Account/AccountCreate.aspx?UZQF=1");

            try
            {
                Rep_General accountSearch = new Rep_General();
                DataTable user = (DataTable)Session["user"];

                int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                string module = accountSearch.SelectModule(userId, "AccountAdd");
                if (module != "Stop")
                {
                    if (Request.QueryString[accountSearch.ObfuscateQueryString("accID")] != null)
                    {
                        string accId = Request.QueryString[accountSearch.ObfuscateQueryString("accID")].ToString();
                        Response.Redirect("~/Modules/" + module + ".aspx?" + accountSearch.ObfuscateQueryString("accID") + "=" + accId + "&" + accountSearch.ObfuscateQueryString("view") + "=1", false);
                    }
                    else
                    {
                        Response.Redirect("~/Modules/" + module + ".aspx", false);
                    }
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

        //protected void IndividualBTN_Click(object sender, EventArgs e)
        //{
        //    WindowsFlag = false;
        //    AddCustomerWindow.Visible = false;
        //    AddCustomerWindow.VisibleOnPageLoad = false;
        //    Response.Redirect("~/Modules/Account/AccountCreate.aspx?UZQF=1");
        //}

        //protected void CorporateBTN_Click(object sender, EventArgs e)
        //{
        //    WindowsFlag = false;
        //    AddCustomerWindow.Visible = false;
        //    AddCustomerWindow.VisibleOnPageLoad = false;
        //    Response.Redirect("~/Modules/Account/AccountCreate.aspx?UZQF=2");
        //}

        protected void CustomersGRD_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //System.Threading.Thread.Sleep(2000);
            CustomersGRD.DataSource = SelectCustomers();
        }

        protected void Export_OnClick(object sender, EventArgs e)
        {
            //CustomersGRD.ExportSettings.ExportOnlyData = true;
            //CustomersGRD.ExportSettings.IgnorePaging = true;
            //CustomersGRD.ExportSettings.OpenInNewWindow = false;
            //CustomersGRD.ExportSettings.UseItemStyles = true;
            ////CustomersGRD.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            //CustomersGRD.ExportSettings.Excel.Format = GridExcelExportFormat.Xlsx;
            ////CustomersGRD.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), "Xlsx");
            ////CustomersGRD.ExportSettings.FileName = "Customers (" + DateTime.Now + ")";
            //CustomersGRD.ExportSettings.FileName = string.Format("Customers_{0}", DateTime.Now);
            //CustomersGRD.MasterTableView.ExportToExcel();

            //PostBackTrigger trigger = new PostBackTrigger();
            //trigger.ControlID = "B_42";

            //AccountUpdatePanel.Triggers.Add(trigger);
            //TriggerId = ID.ToString();



            DataTable expCustomersDt = new DataTable();
            //expCustomersDt = SelectCustomers();

            try
            {
                Rep_Account accountSearch = new Rep_Account();

                DataTable User = (DataTable)Session["user"];
                string userId = User.Rows[0]["UserID"].ToString();

                expCustomersDt = accountSearch.SelectSearch(int.Parse(userId), "Excel", SearchTXT.Text, "AccountSearch");
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

            if (expCustomersDt.Rows.Count <= 0)
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", "No records to export"), true);
            }
            else
            {
                expCustomersDt.Columns.Remove("AccID");

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(expCustomersDt, "Accounts");
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + string.Format("Accounts_{0}", DateTime.Now) + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }

        }

        //Add styles to the exported file from telerik in type ExcelIM
        protected void CustomersGRD_OnExcelMLExportStylesCreated(object sender, GridExportExcelMLStyleCreatedArgs e)
        {
            // To set the Excel cell borders style
            BorderStylesCollection borders = new BorderStylesCollection();

            BorderStyles borderStyle = null;

            for (int i = 1; i <= 4; i++)

            {

                borderStyle = new BorderStyles();

                borderStyle.PositionType = (PositionType)i;

                borderStyle.Color = System.Drawing.Color.Black;

                borderStyle.LineStyle = LineStyle.Continuous;

                borderStyle.Weight = 1.0;

                borders.Add(borderStyle);

            }



            // styles have to set for export excel

            foreach (StyleElement style in e.Styles)
            {

                //For Header style - background color

                if (style.Id == "headerStyle")
                {

                    style.InteriorStyle.Pattern = InteriorPatternType.Solid;

                    style.InteriorStyle.Color = System.Drawing.Color.Gray;

                }

                //For alternate row style - background color

                if (style.Id == "alternatingItemStyle" || style.Id == "alternatingPriceItemStyle" || style.Id == "alternatingPercentItemStyle" || style.Id == "alternatingDateItemStyle")
                {

                    style.InteriorStyle.Pattern = InteriorPatternType.Solid;

                    style.InteriorStyle.Color = System.Drawing.Color.LightGray;

                }

                if

                (

                    style.Id.Contains("itemStyle") || style.Id == "priceItemStyle" || style.Id == "percentItemStyle" || style.Id == "dateItemStyle")
                {

                    style.InteriorStyle.Pattern = InteriorPatternType.Solid;

                    style.InteriorStyle.Color = System.Drawing.Color.White;

                }



                // for each cell border styles

                foreach (BorderStyles border in borders)

                {

                    style.Borders.Add(border);

                }



                // Each cell text wrapping

                style.AlignmentElement.Attributes.Add("ss:WrapText", "1");

            }


        }

        //View
        protected void B_12_OnClick(object sender, EventArgs e)
        {
            foreach (GridDataItem selectinganItem in CustomersGRD.MasterTableView.Items)
            {
                if (selectinganItem.Selected)
                {
                    //Response.Redirect("~/Modules/Account/AccountAdd.aspx?ARXDWB=" + selectinganItem["AccID"].Text + "");
                    try
                    {
                        Rep_General accountSearch = new Rep_General();
                        DataTable user = (DataTable)Session["user"];

                        int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                        string module = accountSearch.SelectModule(userId, "AccountAdd");
                        if (module != "Stop")
                        {
                            int accId = accountSearch.EncryptIds(int.Parse(selectinganItem["AccID"].Text));
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
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", "Please select your record"), true);
                }
            }
        }

        #region Navegation_Panel

        protected void panel_lnkCrdtInfo_OnClick(object sender, EventArgs e)
        {
            Rep_General general = new Rep_General();
            foreach (GridDataItem selectinganItem in CustomersGRD.MasterTableView.Items)
            {
                if (selectinganItem.Selected)
                {
                    try
                    {
                        int accId = general.EncryptIds(int.Parse(selectinganItem["AccID"].Text));
                        int accNo = general.EncryptIds(int.Parse(selectinganItem["AccNo"].Text));

                        Rep_General accountSearch = new Rep_General();
                        DataTable user = (DataTable)Session["user"];

                        int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                        string module = accountSearch.SelectModule(userId, "AccountCrdt");
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
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", "Please select your record"), true);
                }
            }
        }

        protected void panel_lnkContactInfo_OnClick(object sender, EventArgs e)
        {
            foreach (GridDataItem selectinganItem in CustomersGRD.MasterTableView.Items)
            {
                if (selectinganItem.Selected)
                {
                    try
                    {
                        Rep_General accountSearch = new Rep_General();
                        DataTable user = (DataTable)Session["user"];

                        int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                        string module = accountSearch.SelectModule(userId, "AccountContact");
                        if (module != "Stop")
                        {
                            int accId = accountSearch.EncryptIds(int.Parse(selectinganItem["AccID"].Text));
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
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", "Please select your record"), true);
                }
            }
        }

        protected void panel_lnkAccInfo_OnClick(object sender, EventArgs e)
        {
            Rep_General general = new Rep_General();
            foreach (GridDataItem selectinganItem in CustomersGRD.MasterTableView.Items)
            {
                if (selectinganItem.Selected)
                {
                    try
                    {
                        int accId = general.EncryptIds(int.Parse(selectinganItem["AccID"].Text));
                        int accNo = general.EncryptIds(int.Parse(selectinganItem["AccNo"].Text));

                        Rep_General accountSearch = new Rep_General();
                        DataTable user = (DataTable)Session["user"];

                        int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                        string module = accountSearch.SelectModule(userId, "AccountInfo");
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
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", "Please select your record"), true);
                }
            }
        }

        protected void panel_lnkAccSales_OnClick(object sender, EventArgs e)
        {
            Rep_General general = new Rep_General();
            foreach (GridDataItem selectinganItem in CustomersGRD.MasterTableView.Items)
            {
                if (selectinganItem.Selected)
                {
                    try
                    {
                        int accId = general.EncryptIds(int.Parse(selectinganItem["AccID"].Text));
                        int accNo = general.EncryptIds(int.Parse(selectinganItem["AccNo"].Text));

                        Rep_General accountSearch = new Rep_General();
                        DataTable user = (DataTable)Session["user"];

                        int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                        string module = accountSearch.SelectModule(userId, "AccountSales");
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
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("window.alert('{0}');", "Please select your record"), true);
                }
            }
        }

        #endregion

        protected void btnConfirm_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", string.Format("ShowTypeDialog();"), true);
        }

        protected void btnLinkContact_OnClick(object sender, EventArgs e)
        {
            try
            {
                Rep_General general = new Rep_General();
                Rep_Account contactLink = new Rep_Account();
                DataTable user = (DataTable)Session["user"];

                int userId = int.Parse(user.Rows[0]["UserID"].ToString());
                int accId = general.DecryptIds(int.Parse(Request.QueryString[general.ObfuscateQueryString("accID")].ToString()));

                int accContactId = int.Parse(lblAccId.Text);
                byte contactType = byte.Parse(ddlContactType.Text);

                contactLink.Contact_Link(userId, accId, accContactId, contactType);

                //return to contact page
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



    }
}