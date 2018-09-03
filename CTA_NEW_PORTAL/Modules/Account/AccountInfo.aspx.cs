using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using CTA_NEW_PORTAL.App_Code;
using CTA_NEW_PORTAL.EB;
using Telerik.Web.UI;


public struct TheListItem
{
    public string Value;
    public string Text;
    public string Validation;
    public string Message;
}

namespace CTA_NEW_PORTAL.Modules.Account
{
    public partial class AccountInfo : System.Web.UI.Page
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

        #endregion


        #region PageLoad and PageInit

        protected void Page_Init(object sender, EventArgs e)
        {
            //Rep_General general = new Rep_General();

            //if (Request.QueryString[general.ObfuscateQueryString("AccIDEnc")] == null)
            //{
            //    //Response.Redirect("~/Modules/Account/AccountSearch.aspx");

            //}
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Get_Navegation_Panel();

           
            //try
            //{
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

                        CategoryFlag.Value = "ALL";

                        AccIDEnc.Value = Request.QueryString[general.ObfuscateQueryString("accID")].ToString();
                        DataTable User = Session["user"] != null ? (DataTable)Session["user"] : new DataTable();
                        if (User.Rows.Count > 0)
                        {


                            UserID.Value = User.Rows[0]["UserID"].ToString();



                            //AccIDEnc.Value = Request.QueryString[general.ObfuscateQueryString("accID")].ToString();
                            lblAccID.Text = general.DecryptIds(int.Parse(AccIDEnc.Value)).ToString();

                            //string AccIDEnc = string.Empty;
                            //if (Request.QueryString[general.ObfuscateQueryString("AccIDEnc")] == null)
                            //{
                            AccIDDec.Value = lblAccID.Text;
                            //}
                            //else
                            //{
                            //    string value = Request.QueryString[general.ObfuscateQueryString("AccIDEnc")].ToString();
                            //    AccIDEnc = general.DecryptIds(int.Parse(value)).ToString();
                            //}





                            AccType.Value = GetAccountType().Rows.Count > 0
                                ? GetAccountType().Rows[0]["AccCategory"].ToString()
                                : string.Empty;
                            //Get_ddls();

                            Get_InfoType_List();
                            //FillAccountInfoCategoriesGRD();
                            //lblAccIDEnc.Text = general.DecryptIds(int.Parse(lblAccIDEnc)).ToString();
                            //Select_Account_Crdt();






                            string texttaccNo = Request.QueryString[general.ObfuscateQueryString("accNo")] != null
                                ? Request.QueryString[general.ObfuscateQueryString("accNo")].ToString()
                                : "";
                            accNo.Text = texttaccNo != string.Empty ? general.DecryptIds(int.Parse(texttaccNo)).ToString() : "";





                            Rep_Account account = new Rep_Account();

                            DataTable accTable = account.SelectAcc(int.Parse(UserID.Value), int.Parse(AccIDDec.Value));


                            accName.Text = accTable.Rows.Count > 0 ? accTable.Rows[0]["AccName"].ToString() : string.Empty;






                            if (DataList.Items.Count > 0)
                            {
                              
                                FillAccountInfoGRD();
                                // AccountInfoCategoriesGRD_ItemCommand()
                            }

                            //if (AccountInfoCategoriesGRD.Items.Count > 0)
                            //{
                            //    AccountInfoCategoriesGRD.MasterTableView.Items[0]["List"].Focus();
                            //    InfoType.Value = AccountInfoCategoriesGRD.MasterTableView.Items[0]["InfoTypeCategory"].Text;
                            //    FillAccountInfoGRD();
                            //    // AccountInfoCategoriesGRD_ItemCommand()
                            //}


                            DisableAdd();
                            DisableEdit();
                           

                            //hidnViewFlag.Value = "View";
                            //    lblSubject.Text = "View";



                        }
                        else
                        {
                            B_11_OnClick(null, null);
                        }


                       

                    }


                    //get Roles after check the flag value
                    Btn_Fields_Roles();
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
            //}
            //catch (Exception ex)
            //{

            //}

        }

        #endregion


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

                dtPermittedBtns = btnGeneral.Permitted_buttons(int.Parse(userId), "AccountInfo");
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

        #endregion Button Permissions

        #region Buttons Events

        #region New
        //New
        protected void B_21_OnClick(object sender, EventArgs e)
        {
            hidnViewFlag.Value = "Add";
            lblSubject.Text = "New";
            Btn_Fields_Roles();
            EnableAdd();
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
        }
        #endregion Edit

        #endregion

        #endregion Header Buttons


        #region Account General (Type/ID......)

        public DataTable GetAccountType()
        {

            DataTable AccountTypeDT = new DataTable();



            try
            {


                connection.Open();
                SqlCommand cmd = new SqlCommand("SP_Account", connection);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@UserID", UserID.Value);
                cmd.Parameters.AddWithValue("@AccID", AccIDDec.Value);

                SqlDataAdapter AccountTypeDA = new SqlDataAdapter(cmd);
                AccountTypeDA.Fill(AccountTypeDT);

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


            return AccountTypeDT;
        }

        #endregion Account General (Type/ID......)


        #region AccountInfoCategoriesGRD

        //protected void AccountInfoCategoriesGRD_ItemCommand(object sender, GridCommandEventArgs e)
        //{


        //    if (e.CommandName == "List")
        //    {

        //        CategoryFlag.Value = "NOTALL";

        //        InfoTypeDLL.Items.Clear();


        //        InfoType.Value = AccountInfoCategoriesGRD.MasterTableView.Items[e.Item.ItemIndex]["InfoTypeCategory"].Text.ToString();





        //        DataTable selectedTable = FillAccountInfoCategoriesDT().AsEnumerable()
        //            .Where(r => r.Field<string>("InfoTypeCategory") == InfoType.Value)
        //            .CopyToDataTable();

        //        TheListItem[] arr = new TheListItem[selectedTable.Rows.Count];
        //        for (int i = 0; i < selectedTable.Rows.Count; i++)
        //        {
        //            TheListItem InfoItem = new TheListItem
        //            {
        //                Text = selectedTable.Rows[i]["InfoTypeName"].ToString(),
        //                Value = selectedTable.Rows[i]["InfoTypeID"].ToString(),
        //                Validation = selectedTable.Rows[i]["ValidationCode"].ToString(),
        //                Message = selectedTable.Rows[i]["ValidationMsg"].ToString()

        //            };

        //            arr[i] = InfoItem;

        //        }

        //        DataTable EnteredInformation = FillAccountInfoDT();
        //        TheListItem[] arr2 = FilterTheArray(arr, EnteredInformation);

        //        Session["TheListItem"] = arr2;
        //        //make sure there aren't any matching names in dt2






        //        DataTable table = ConvertToDT(arr2);




        //        //  DataTable ChoosenDT = rowsOnlyInselectedTable.CopyToDataTable();

        //        InfoTypeDLL.DataSource = table;
        //        InfoTypeDLL.DataTextField = "Text";
        //        InfoTypeDLL.DataValueField = "Value";
        //        InfoTypeDLL.DataBind();
        //        InfoTypeDLL.Items.Insert(0, new ListItem("Select a Type", "-1"));


        //        if (table.Rows.Count > 0)
        //        {
        //            InfoTypeDLL_SelectedIndexChanged(null, null);
        //        }

        //        FillAccountInfoGRD();

        //        //for (int i = 0; i < selectedTable.Rows.Count; i++)
        //        //{
        //        //    ListItem InfoItem = new ListItem
        //        //    {
        //        //        Text = selectedTable.Rows[i]["InfoTypeName"].ToString(),
        //        //        Value = selectedTable.Rows[i]["InfoTypeID"].ToString()
        //        //    };
        //        //    InfoItem.Attributes.Add("Validation", selectedTable.Rows[i]["InfoTypeValidation"].ToString());

        //        //    InfoTypeDLL.Items.Add(InfoItem);
        //        //}

        //        //ListItem test = new ListItem { Text = srText, Value = srValue }
        //        //test.Attributes.Add("data-imagesrc", "xxx");
        //        //test.Attributes.Add("data-description", "xxx");
        //        //dropListUserImages.Items.Add(test);

        //        //InfoTypeDLL.DataSource = selectedTable;

        //        //InfoTypeDLL.DataTextField = "InfoTypeName";
        //        //InfoTypeDLL.DataValueField = "InfoTypeID";
        //        //InfoTypeDLL.DataBind();
        //        // string test1 = "[0][7](7|8|9)\d{7}";

        //        //InfoTXTValidator.ValidationExpression = @"" + InfoTypeDLL.SelectedItem.Attributes["Validation"].ToString() + "";



        //        //InfoTXTValidator.ValidationExpression = @"^[0-9]{1,20}$";
        //        //InfoTXTValidator.ValidationExpression = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
        //        InfoTXT.Text = "";
        //        InfoTXT.Focus();

        //    }



        //    #region Refresh Grid
        //    else if (e.CommandName == "RebindGrid")
        //    {

        //    }
        //    #endregion Refresh Grid





        //}


        public void AfterInfoAddition()
        {

            if (CategoryFlag.Value == "ALL")
            {
                InfoTypeDLL.Items.Clear();

                //InfoType.Value = AccountInfoCategoriesGRD.MasterTableView.Items[e.Item.ItemIndex]["InfoTypeCategory"].Text.ToString();





                DataTable selectedTable = FillAccountInfoCategoriesDT();

                TheListItem[] arr = new TheListItem[selectedTable.Rows.Count];
                for (int i = 0; i < selectedTable.Rows.Count; i++)
                {
                    TheListItem InfoItem = new TheListItem
                    {
                        Text = selectedTable.Rows[i]["InfoTypeName"].ToString(),
                        Value = selectedTable.Rows[i]["InfoTypeID"].ToString(),
                        Validation = selectedTable.Rows[i]["ValidationCode"].ToString(),
                        Message = selectedTable.Rows[i]["ValidationMsg"].ToString()

                    };

                    arr[i] = InfoItem;

                }

                DataTable EnteredInformation = FillAccountInfoDT();
                TheListItem[] arr2 = FilterTheArray(arr, EnteredInformation);

                Session["TheListItem"] = arr2;







                DataTable table = ConvertToDT(arr2);






                InfoTypeDLL.DataSource = table;
                InfoTypeDLL.DataTextField = "Text";
                InfoTypeDLL.DataValueField = "Value";
                InfoTypeDLL.DataBind();
                InfoTypeDLL.Items.Insert(0, new ListItem("Select a Type", "-1"));


                if (table.Rows.Count > 0)
                {
                    InfoTypeDLL_SelectedIndexChanged(null, null);
                }




                InfoTXT.Text = "";
                InfoTXT.Focus();

            }
            else
            {



                InfoTypeDLL.Items.Clear();

                //InfoType.Value = AccountInfoCategoriesGRD.MasterTableView.Items[e.Item.ItemIndex]["InfoTypeCategory"].Text.ToString();





                DataTable selectedTable = FillAccountInfoCategoriesDT().AsEnumerable()
                    .Where(r => r.Field<string>("InfoTypeCategory") == InfoType.Value)
                    .CopyToDataTable();

                TheListItem[] arr = new TheListItem[selectedTable.Rows.Count];
                for (int i = 0; i < selectedTable.Rows.Count; i++)
                {
                    TheListItem InfoItem = new TheListItem
                    {
                        Text = selectedTable.Rows[i]["InfoTypeName"].ToString(),
                        Value = selectedTable.Rows[i]["InfoTypeID"].ToString(),
                        Validation = selectedTable.Rows[i]["ValidationCode"].ToString(),
                        Message = selectedTable.Rows[i]["ValidationMsg"].ToString()

                    };

                    arr[i] = InfoItem;

                }

                DataTable EnteredInformation = FillAccountInfoDT();
                TheListItem[] arr2 = FilterTheArray(arr, EnteredInformation);

                Session["TheListItem"] = arr2;







                DataTable table = ConvertToDT(arr2);






                InfoTypeDLL.DataSource = table;
                InfoTypeDLL.DataTextField = "Text";
                InfoTypeDLL.DataValueField = "Value";
                InfoTypeDLL.DataBind();
                InfoTypeDLL.Items.Insert(0, new ListItem("Select a Type", "-1"));


                if (table.Rows.Count > 0)
                {
                    InfoTypeDLL_SelectedIndexChanged(null, null);
                }




                InfoTXT.Text = "";
                InfoTXT.Focus();

            }

        }

        public void AfterInfoAdditionAll()
        {



            InfoTypeDLL.Items.Clear();

            //InfoType.Value = AccountInfoCategoriesGRD.MasterTableView.Items[e.Item.ItemIndex]["InfoTypeCategory"].Text.ToString();





            DataTable selectedTable = FillAccountInfoCategoriesDT();

            TheListItem[] arr = new TheListItem[selectedTable.Rows.Count];
            for (int i = 0; i < selectedTable.Rows.Count; i++)
            {
                TheListItem InfoItem = new TheListItem
                {
                    Text = selectedTable.Rows[i]["InfoTypeName"].ToString(),
                    Value = selectedTable.Rows[i]["InfoTypeID"].ToString(),
                    Validation = selectedTable.Rows[i]["ValidationCode"].ToString(),
                    Message = selectedTable.Rows[i]["ValidationMsg"].ToString()

                };

                arr[i] = InfoItem;

            }

            DataTable EnteredInformation = FillAccountInfoDT();
            TheListItem[] arr2 = FilterTheArray(arr, EnteredInformation);

            Session["TheListItem"] = arr2;







            DataTable table = ConvertToDT(arr2);






            InfoTypeDLL.DataSource = table;
            InfoTypeDLL.DataTextField = "Text";
            InfoTypeDLL.DataValueField = "Value";
            InfoTypeDLL.DataBind();
            InfoTypeDLL.Items.Insert(0, new ListItem("Select a Type", "-1"));


            if (table.Rows.Count > 0)
            {
                InfoTypeDLL_SelectedIndexChanged(null, null);
            }




            InfoTXT.Text = "";
            InfoTXT.Focus();



        }

        private DataTable ConvertToDT(TheListItem[] arr2)
        {
            DataTable TempDT = new DataTable();

            TempDT.Columns.Add("Text");
            TempDT.Columns.Add("Value");
            TempDT.Columns.Add("Validation");
            TempDT.Columns.Add("Message");

            foreach (var item in arr2)
            {
                TempDT.Rows.Add(item.Text, item.Value, item.Validation, item.Message);
            }

            return TempDT;


        }

        private TheListItem[] FilterTheArray(TheListItem[] arr, DataTable enteredInformation)
        {
            var temp = new List<TheListItem>(arr);



            for (int i = 0; i < temp.Count; i++)
            {
                for (int j = 0; j < enteredInformation.Rows.Count; j++)
                {
                    if (temp[i].Value == enteredInformation.Rows[j]["InfoType"].ToString())
                    {
                        temp.RemoveAt(i);
                        i--;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return temp.ToArray();

        }

        //public void FillAccountInfoCategoriesGRD()
        //{
        //    DataTable Temp = new DataTable();

        //    try
        //    {




        //        DataTable AccountInfoCategoriesDT = FillAccountInfoCategoriesDT();
        //        if (AccountInfoCategoriesDT.Rows.Count > 0)
        //        {

        //            var distinctValues = AccountInfoCategoriesDT.AsEnumerable()
        //                .Select(row => new
        //                {
        //                    InfoTypeCategory = row.Field<string>("InfoTypeCategory"),


        //                })
        //                .Distinct();


        //            var GeneratedDT = new DataTable();

        //            GeneratedDT.Columns.Add("InfoTypeCategory");



        //            foreach (var item in distinctValues)
        //            {

        //                GeneratedDT.Rows.Add(item.InfoTypeCategory.ToString());

        //            }


        //            if (GeneratedDT.Rows.Count > 0)
        //            {
        //                AccountInfoCategoriesGRD.DataSource = GeneratedDT;
        //                AccountInfoCategoriesGRD.DataBind();
        //            }
        //            else
        //            {

        //                AccountInfoCategoriesGRD.DataSource = Temp;
        //                AccountInfoCategoriesGRD.DataBind();

        //            }

        //        }
        //    }


        //    catch (Exception ex)
        //    {

        //    }

        //    finally
        //    {

        //    }
        //}

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

        #endregion AccountInfoCategoriesGRD


        #region AccountInfoGRD

        protected void AccountInfoGRD_ItemCommand(object sender, GridCommandEventArgs e)
        {



            #region Edit
            if (e.CommandName == "Edit")
            {




                GRDLineID.Value = AccountInfoGRD.MasterTableView.Items[e.Item.ItemIndex]["LineID"].Text.ToString();
                GRDInfoType.Value = AccountInfoGRD.MasterTableView.Items[e.Item.ItemIndex]["InfoType"].Text.ToString();
                GRDValidationName.Value = AccountInfoGRD.MasterTableView.Items[e.Item.ItemIndex]["ValidationName"].Text.ToString();
                EditedFieldValue.Value = AccountInfoGRD.MasterTableView.Items[e.Item.ItemIndex]["InfoData"].Text.ToString();
                GRDModuleSaveType.Value = "Update";

                //GridEditFormItem editform = (GridEditFormItem)((Telerik.Web.UI.GridDataItem)(e.Item)).EditFormItem;
                //RadTextBox TheGRDInfoTXT = (RadTextBox)editform.FindControl("GRDInfoTXT");
                //RegularExpressionValidator TheGRDInfoTXTValidator = (RegularExpressionValidator)editform.FindControl("GRDInfoTXTValidator");
                //TheGRDInfoTXTValidator.ControlToValidate = "TheGRDInfoTXT";





                //AccountInfoGRD.DataSource = FillAccountInfoDT();
                //AccountInfoGRD.DataBind();
                FillAccountInfoGRD();






            }
            #endregion Edit

            #region Save
            else if (e.CommandName == "Save")
            {


                if (GRDModuleSaveType.Value == "Update")
                {





                    GridEditFormItem editform = (GridEditFormItem)((Telerik.Web.UI.GridDataItem)(e.Item)).EditFormItem;
                    RadTextBox TheGRDInfoTXT = (RadTextBox)editform.FindControl("GRDInfoTXT");
                    Label TheGRDInfoTXTValidatorLBL = (Label)editform.FindControl("GRDInfoTXTValidatorLBL");



                    if (EditedFieldValue.Value == TheGRDInfoTXT.Text)
                    {
                        AccountInfoGRD.MasterTableView.ClearEditItems();
                    }
                    else
                    {
                        DataTable ValidationDT = GetRecordValidation(GRDValidationName.Value);

                        string Validation = ValidationDT.Rows.Count > 0 ? ValidationDT.Rows[0]["ValidationCode"].ToString() : string.Empty;

                        string Message = ValidationDT.Rows.Count > 0 ? ValidationDT.Rows[0]["ValidationMsg"].ToString() : string.Empty;

                        //TheGRDInfoTXTValidator.ValidationExpression = (Validation == string.Empty) ? @"^[0-9A-Z]([-.\w]*[0-9A-Z])*$" : @"" + Validation;
                        //TheGRDInfoTXTValidator.ErrorMessage = @"" + Message;

                        string EditedFieldPattern = (Validation == string.Empty) ? @"^[0-9A-Z]([-.\w]*[0-9A-Z])*$" : @"" + Validation;
                        TheGRDInfoTXTValidatorLBL.Text = (Message == string.Empty) ? "* Error Data Entry" : @"" + Message;



                        //string FirstNamepattern = @"^[a-zA-Z-ء-ي''-'\s]{2,15}$";
                        //string SecondNamepattern = @"^[a-zA-Z-ء-ي''-'\s]{2,15}$";
                        //string ThirdNamepattern = @"^[a-zA-Z-ء-ي''-'\s]{2,15}$";
                        //string FamilyNamepattern = @"^[a-zA-Z-ء-ي''-'\s]{2,15}$";
                        ////string Phonepattern = @"[7](7|8|9)\d{7}";
                        //string Phonepattern = @"[0][7](7|8|9)\d{7}";



                        //string Emailpattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

                        Regex rgx1 = new Regex(EditedFieldPattern, RegexOptions.IgnoreCase);
                        MatchCollection matches1 = rgx1.Matches(TheGRDInfoTXT.Text);

                        if (matches1.Count > 0)
                        {
                            UpdateInfoData(TheGRDInfoTXT.Text, GRDLineID.Value, GRDInfoType.Value);

                            //AccountInfoGRD.DataSource = FillAccountInfoDT();
                            //AccountInfoGRD.DataBind();
                            FillAccountInfoGRD();


                            AccountInfoGRD.MasterTableView.ClearEditItems();
                        }
                        else
                        {
                            //AccountInfoGRD.MasterTableView.ClearEditItems();
                        }




                    }






                }



            }
            #endregion Save



            #region Cancel
            else if (e.CommandName == "Cancel")
            {
                //AccountInfoGRD.DataSource = FillAccountInfoDT();
                //AccountInfoGRD.DataBind();
                FillAccountInfoGRD();
                AccountInfoGRD.MasterTableView.ClearEditItems();
            }
            #endregion Cancel

            #region Refresh Grid
            else if (e.CommandName == "RebindGrid")
            {
                //AccountInfoGRD.DataSource = FillAccountInfoDT();
                //AccountInfoGRD.DataBind();
                FillAccountInfoGRD();
                AccountInfoGRD.MasterTableView.ClearEditItems();
            }
            #endregion Refresh Grid

            #region Else
            //else if (e.CommandName == "RebindGrid")
            //{
            //AccountInfoGRD.DataSource = FillAccountInfoDT();
            //AccountInfoGRD.DataBind();
            //FillAccountInfoGRD();
            //AccountInfoGRD.MasterTableView.ClearEditItems();
            //}
            #endregion Else



        }

        private DataTable GetRecordValidation(string ValidationName)
        {
            DataTable RecordValidationDT = new DataTable();



            try
            {


                connection.Open();

                SqlCommand cmd = new SqlCommand("SP_SysCode_SysValidation", connection);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@UserID", UserID.Value);
                cmd.Parameters.AddWithValue("@ValidationName", ValidationName);



                SqlDataAdapter RecordValidationDA = new SqlDataAdapter(cmd);
                RecordValidationDA.Fill(RecordValidationDT);

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


            return RecordValidationDT;
        }

        private void UpdateInfoData(string NewInfoData, string LineID, string InfoType)
        {





            try
            {


                connection.Open();

                SqlCommand cmd = new SqlCommand("SP_AccountInfo_Udt", connection);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@UserID", UserID.Value);
                cmd.Parameters.AddWithValue("@AccID", AccIDDec.Value);
                cmd.Parameters.AddWithValue("@LineID", LineID);
                cmd.Parameters.AddWithValue("@InfoType", InfoType);
                cmd.Parameters.AddWithValue("@InfoData", NewInfoData);

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

        public void FillAccountInfoGRD()
        {

            if (CategoryFlag.Value == "ALL")
            {

                try
                {

                    DataTable AccountInfoDT = FillAccountInfoDT();

                    AccountInfoGRD.DataSource = AccountInfoDT;
                    AccountInfoGRD.DataBind();





                }


                catch (Exception ex)
                {

                }

                finally
                {

                }
            }
            else
            {



                DataTable Temp = new DataTable();

                try
                {


                    DataTable AccountInfoDT = FillAccountInfoDT();

                    if (AccountInfoDT.Rows.Count > 0)
                    {

                        var rows = AccountInfoDT.AsEnumerable()
                            .Where(r => r.Field<string>("InfoTypeCategory") == InfoType.Value);
                        //.CopyToDataTable();

                        DataTable selectedTable = rows.Any() ? rows.CopyToDataTable() : Temp;


                        AccountInfoGRD.DataSource = selectedTable;
                        AccountInfoGRD.DataBind();


                    }

                    else
                    {
                        AccountInfoGRD.DataSource = Temp;
                        AccountInfoGRD.DataBind();
                    }


                }


                catch (Exception ex)
                {

                }

                finally
                {

                }
            }
        }



        public DataTable FillAccountInfoDT()
        {

            DataTable AccountInfoDT = new DataTable();



            try
            {


                connection.Open();

                SqlCommand cmd = new SqlCommand("SP_AccountInfo", connection);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@UserID", UserID.Value);
                cmd.Parameters.AddWithValue("@AccID", AccIDDec.Value);



                SqlDataAdapter AccountInfoDA = new SqlDataAdapter(cmd);
                AccountInfoDA.Fill(AccountInfoDT);

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


            return AccountInfoDT;
        }

        #endregion AccountInfoGRD


        #region RegularExpressionValidator

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
                regExpressionValidator.ErrorMessage = "* Mobile number is not correct, Must contain 10 numbers start with 07.. example: 079 000 0000.";
            }
            else if (regexType == "Email")
            {
                regExpressionValidator.ValidationExpression = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                regExpressionValidator.ErrorMessage = "* Email address is not correct ex: Jhon@outlook.com.";
            }
            else if (regexType == "Phone")
            {
                regExpressionValidator.ValidationExpression = @"[0](6|5|3|2)\d{7}";
                regExpressionValidator.ErrorMessage = "* Phone number is not correct, Must contain 9 numbers start with (0).. example: 06 000 0000.";
            }
            else if (regexType == "Date")
            {
                regExpressionValidator.ValidationExpression = @"^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$";
                regExpressionValidator.ErrorMessage = "* Date is not in the correct format, Must be in dd/MM/yyyy.. example: 12/12/2043.";
            }


            return regExpressionValidator;
        }

        #endregion RegularExpressionValidator


        #region InfoTypeDLL and AddInfoBTN


        #region InfoTypeDLL
        protected void InfoTypeDLL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (InfoTypeDLL.SelectedValue != "-1")
            {

                InfoTXT.Enabled = true;
                InfoAddBTN.Enabled = true;

                TheListItem[] arr = (TheListItem[])Session["TheListItem"];

                TheListItem InfoItem = new TheListItem();
                InfoItem = arr.First(item => item.Value == InfoTypeDLL.SelectedValue);
                string Validation = arr.First(item => item.Value == InfoTypeDLL.SelectedValue).Validation;

                string Message = arr.First(item => item.Value == InfoTypeDLL.SelectedValue).Message;

                #region Change Here

                InfoTXTValidator.ValidationExpression = @"" + Validation;
                InfoTXTValidator.ErrorMessage = @"" + Message;

                #endregion


                InfoTXT.Focus();




                //RegularExpressionValidator regExpressionValidator = new RegularExpressionValidator();
                //regExpressionValidator = Get_Regex(Validation);

                //InfoTXTValidator.ValidationExpression = regExpressionValidator.ValidationExpression;
                //InfoTXTValidator.ErrorMessage = regExpressionValidator.ErrorMessage;


                // FillAccountInfoCategoriesGRD();
            }
            else
            {
                InfoTXT.Enabled = false;
                InfoAddBTN.Enabled = false;
            }


        }
        #endregion InfoTypeDLL


        #region AddInfoBTN
        protected void AddInfoBTN_Click(object sender, EventArgs e)
        {

            //hidnMoveFlag.Value = "0";
            if (InfoTypeDLL.SelectedValue != "-1")
            {



                try
                {


                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SP_AccountInfo_Add", connection);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@UserID", UserID.Value);
                    cmd.Parameters.AddWithValue("@AccID", AccIDDec.Value);
                    cmd.Parameters.AddWithValue("@InfoType", InfoTypeDLL.SelectedValue);
                    cmd.Parameters.AddWithValue("@InfoData", InfoTXT.Text);

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

                FillAccountInfoGRD();

                AfterInfoAddition();
            }
            else
            {

            }

        }
        #endregion AddInfoBTN

        #endregion InfoTypeDLL and AddInfoBTN



        #region Enable Disable Add Edit

        public void EnableAdd()
        {
            //AddInfoPNL.Visible = true;
            InfoTypeDLL.Enabled = true;
            InfoAddBTN.Enabled = true;
            InfoTXT.Enabled = true;
            //if (AccountInfoGRD.Items.Count > 0)
            //{
            //    AccountInfoCategoriesGRD.MasterTableView.Items[0]["List"].Focus();
            //    InfoType.Value = CategoryFlag.Value;
            //        AccountInfoCategoriesGRD.MasterTableView.Items[0]["InfoTypeCategory"].Text;
            //    FillAccountInfoGRD();

            //}
            AfterInfoAddition();

        }

        public void DisableAdd()
        {
            // AddInfoPNL.Visible = false;
            InfoTypeDLL.Enabled = false;
            InfoAddBTN.Enabled = false;
            InfoTXT.Enabled = false;

        }


        public void DisableEdit()
        {
            AccountInfoGRD.MasterTableView.GetColumn("Edit").Display = false;


        }

        public void EnableEdit()
        {
            AccountInfoGRD.MasterTableView.GetColumn("Edit").Display = true;

        }

        #endregion Enable Disable Add


        #region Not Used
        protected void lnkbtndntsve_OnClick(object sender, EventArgs e)
        {
            //Select_Account_Crdt();
            //Select_Account_Name();
            hidnViewFlag.Value = "View";
            lblSubject.Text = "View";
            Btn_Fields_Roles();
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

        #endregion Not Used


        #region Info Type List

        private void Get_InfoType_List()
        {
            DataTable Temp = new DataTable();

            DataTable AccountInfoCategoriesDT = FillAccountInfoCategoriesDT();
            if (AccountInfoCategoriesDT.Rows.Count > 0)
            {

                var distinctValues = AccountInfoCategoriesDT.AsEnumerable()
                    .Select(row => new
                    {
                        InfoTypeCategory = row.Field<string>("InfoTypeCategory"),
                        InfoTypeCategoryPIC = row.Field<string>("InfoTypeCategoryPIC"),
                    }).Distinct();

                var GeneratedDT = new DataTable();

                GeneratedDT.Columns.Add("InfoTypeCategory");
                GeneratedDT.Columns.Add("InfoTypeCategoryPIC");

                foreach (var item in distinctValues)
                {
                    string pic = "";
                    GeneratedDT.Rows.Add(item.InfoTypeCategory.ToString(), pic = item.InfoTypeCategoryPIC != null ? "<i class='fa " + item.InfoTypeCategoryPIC + "' aria-hidden='true'></i>" : "<i class='fa fa-info-circle' aria-hidden='true'></i>");
                }


                if (GeneratedDT.Rows.Count > 0)
                {
                    DataList.DataSource = GeneratedDT;
                    DataList.DataBind();
                }
                else
                {
                    DataList.DataSource = Temp;
                    DataList.DataBind();
                }

            }
        }

        protected void DataBTN_Command(object sender, CommandEventArgs e)
        {
            //LinkButton PathName = (LinkButton)sender;
            //string test1 = PathName.ToolTip;
            //double test2 = Convert.ToDouble(e.CommandName);
            ////Session["sysGL"] = s;
            //string command = e.CommandArgument.ToString();




            CategoryFlag.Value = "NOTALL";

            InfoTypeDLL.Items.Clear();

            LinkButton PathName = (LinkButton)sender;
            //string test1 = PathName.ToolTip;
            InfoType.Value = PathName.ToolTip;
                //AccountInfoCategoriesGRD.MasterTableView.Items[e.Item.ItemIndex]["InfoTypeCategory"].Text.ToString();





            DataTable selectedTable = FillAccountInfoCategoriesDT().AsEnumerable()
                .Where(r => r.Field<string>("InfoTypeCategory") == InfoType.Value)
                .CopyToDataTable();

            TheListItem[] arr = new TheListItem[selectedTable.Rows.Count];
            for (int i = 0; i < selectedTable.Rows.Count; i++)
            {
                TheListItem InfoItem = new TheListItem
                {
                    Text = selectedTable.Rows[i]["InfoTypeName"].ToString(),
                    Value = selectedTable.Rows[i]["InfoTypeID"].ToString(),
                    Validation = selectedTable.Rows[i]["ValidationCode"].ToString(),
                    Message = selectedTable.Rows[i]["ValidationMsg"].ToString()

                };

                arr[i] = InfoItem;

            }

            DataTable EnteredInformation = FillAccountInfoDT();
            TheListItem[] arr2 = FilterTheArray(arr, EnteredInformation);

            Session["TheListItem"] = arr2;
            //make sure there aren't any matching names in dt2






            DataTable table = ConvertToDT(arr2);




            //  DataTable ChoosenDT = rowsOnlyInselectedTable.CopyToDataTable();

            InfoTypeDLL.DataSource = table;
            InfoTypeDLL.DataTextField = "Text";
            InfoTypeDLL.DataValueField = "Value";
            InfoTypeDLL.DataBind();
            InfoTypeDLL.Items.Insert(0, new ListItem("Select a Type", "-1"));


            if (table.Rows.Count > 0)
            {
                InfoTypeDLL_SelectedIndexChanged(null, null);
            }

            FillAccountInfoGRD();

            //for (int i = 0; i < selectedTable.Rows.Count; i++)
            //{
            //    ListItem InfoItem = new ListItem
            //    {
            //        Text = selectedTable.Rows[i]["InfoTypeName"].ToString(),
            //        Value = selectedTable.Rows[i]["InfoTypeID"].ToString()
            //    };
            //    InfoItem.Attributes.Add("Validation", selectedTable.Rows[i]["InfoTypeValidation"].ToString());

            //    InfoTypeDLL.Items.Add(InfoItem);
            //}

            //ListItem test = new ListItem { Text = srText, Value = srValue }
            //test.Attributes.Add("data-imagesrc", "xxx");
            //test.Attributes.Add("data-description", "xxx");
            //dropListUserImages.Items.Add(test);

            //InfoTypeDLL.DataSource = selectedTable;

            //InfoTypeDLL.DataTextField = "InfoTypeName";
            //InfoTypeDLL.DataValueField = "InfoTypeID";
            //InfoTypeDLL.DataBind();
            // string test1 = "[0][7](7|8|9)\d{7}";

            //InfoTXTValidator.ValidationExpression = @"" + InfoTypeDLL.SelectedItem.Attributes["Validation"].ToString() + "";



            //InfoTXTValidator.ValidationExpression = @"^[0-9]{1,20}$";
            //InfoTXTValidator.ValidationExpression = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            InfoTXT.Text = "";
            InfoTXT.Focus();






        }

        #endregion


        #region Navegation Panel

        protected void panel_lnkViewInfo_OnClick(object sender, EventArgs e)
        {
            Rep_General general = new Rep_General();

            try
            {
                DataTable user = (DataTable)Session["user"];

                int userId = int.Parse(user.Rows[0]["UserID"].ToString());

                string module = general.SelectModule(userId, "AccountAdd");
                if (module != "Stop")
                {
                    int accId = int.Parse(AccIDEnc.Value);
                    Response.Redirect("~/Modules/" + module + ".aspx?" + general.ObfuscateQueryString("accID") + "=" + accId + "");
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

        protected void panel_lnkAccSales_OnClick(object sender, EventArgs e)
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

        #endregion

    }
}