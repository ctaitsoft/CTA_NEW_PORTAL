<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="AccountInfo.aspx.cs" Inherits="CTA_NEW_PORTAL.Modules.Account.AccountInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Start: Header Buttons -->
    <!-- End: Header Buttons -->

    <style>
        .radioBtns {
            /*position: relative;*/
            display: inline-block;
            Width: 200px;
            /*border:solid 1px #000;*/
            margin-bottom: -6px;
            text-align: left;
        }

            .radioBtns input[type="radio"] {
                width: 20px;
            }

            .radioBtns label {
                display: inline;
                /*border:solid 1px #f00;*/
                margin-left: -1px;
                margin-right: 10px;
            }

        .gridBtns {
            /*border:solid 1px #f00;*/
            width: 100%;
            padding: 6px;
            display: inline-block;
        }

        .table > thead > tr > th {
            border-bottom: 0px !important;
        }

        .m-l-0 {
            margin-left: -10px !important
        }

        .list-test {
            list-style: none;
            width: 100%;
            /*border: 1px solid #00ff00;*/
            padding: 5px;
        }

            .list-test li {
                display: inline-block;
                padding: 4px;
                width: 130px;
                height: 30px;
                text-align: center;
                /*border: 1px solid #f00;*/
                background-color: #f9f9f9;
                color: #333;
                margin-bottom: 4px;
            }
    </style>

    <asp:UpdatePanel ID="AccountUpdatePanel_Crdt" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="AccountInfoGRD" />
            <asp:PostBackTrigger ControlID="InfoAddBTN" />
        </Triggers>

        <ContentTemplate>

            <script type="text/javascript">

                function InfoTXTEnter(e) {

                    if (e.keyCode === 13) {

                        try {

                            document.getElementById('<%=InfoAddBTN.ClientID %>').focus();

                        }
                        catch (err) {
                            //
                            alert("Error");
                        }
                    }
                }

            </script>

            <div id="Sub_Content">
                <div class="subject-row">
                    <h4>Account Info. &nbsp;<i class="fa fa-caret-right" aria-hidden="true"></i>&nbsp;
                        <asp:Label ID="lblSubject" runat="server"></asp:Label></h4>
                    <!-- Start: Header Buttons -->
                    <asp:Panel runat="server" ID="btnsPanel" CssClass="btns_Panel">
                        <asp:LinkButton runat="server" ID="B_11" Text="Inquiry" CssClass="mainBtns" CausesValidation="False" Enabled="False" OnClick="B_11_OnClick" /><%--Inquiry 1--%>
                        <asp:LinkButton runat="server" ID="B_12" Text="View" CssClass="mainBtns" Enabled="False" CausesValidation="False" OnClick="B_12_OnClick" /><%--View 2--%>
                        <asp:LinkButton runat="server" ID="B_13" Text="Print" CssClass="mainBtns" Enabled="False" /><%--Print 3--%>
                        <span style="width: 30px; display: inline-block"></span>
                        <asp:LinkButton runat="server" ID="B_21" Text="New" CssClass="mainBtns" CausesValidation="False" Enabled="False" OnClick="B_21_OnClick" /><%--New 4--%>
                        <asp:LinkButton runat="server" ID="B_22" Text="Update" CssClass="mainBtns" Enabled="False" OnClick="B_22_OnClick" /><%--Update 5--%>
                        <asp:LinkButton runat="server" ID="B_23" Text="Cancel" CssClass="mainBtns" Enabled="False" /><%--Cancel 6--%>
                        <asp:LinkButton runat="server" ID="B_29" Text="Save" CssClass="mainBtns" /><%--Save 7--%>
                        <span style="width: 30px; display: inline-block"></span>
                        <asp:LinkButton runat="server" ID="B_18" Text="Refresh" CssClass="mainBtns" CausesValidation="False" OnClick="B_18_OnClick" /><%--Refresh 8--%>
                        <asp:LinkButton runat="server" ID="B_19" Text="Clear" CssClass="mainBtns" CausesValidation="False" /><%--Clear 9--%>

                        <div class="btn-group">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">More &nbsp;<span class="caret"></span></a>
                            <ul class="dropdown-menu">
                                <li>
                                    <asp:LinkButton ID="B_31" runat="server" Text="Copy" Enabled="False" /></li>
                                <%--Copy 10--%>
                                <li>
                                    <asp:LinkButton ID="B_32" runat="server" Text="Paste" Enabled="False" /></li>
                                <%--Paste 11--%>
                                <li>
                                    <asp:LinkButton ID="B_41" runat="server" Text="Import" Enabled="False" /></li>
                                <%--Import 12--%>
                                <li>
                                    <asp:LinkButton ID="B_42" runat="server" Text="Export" Enabled="False" /></li>
                                <%--Export 13--%>
                                <li role="separator" class="divider"></li>
                                <li>
                                    <asp:LinkButton ID="B_99" runat="server" Text="Special Fields" Enabled="False" /></li>
                                <%--Special Fields 14--%>
                            </ul>
                        </div>

                    </asp:Panel>
                    <!-- End: Header Buttons -->
                </div>

                <div class="row">
                    <div class="row-content" style="border-bottom: 1px solid #d4d4d4;">
                        <div class="rowItem" style="min-height: 25px">
                            <table>
                                <tr>
                                    <td class="label-cell-width-x3">Account Code:</td>
                                    <td style="text-indent: 20px">
                                        <asp:Label runat="server" ID="accNo"></asp:Label></td>
                                </tr>
                            </table>
                        </div>
                        <div class="rowItem" style="min-height: 25px">
                            <table>
                                <tr>
                                    <td class="label-cell-width-x3">Account Name:</td>
                                    <td style="text-indent: 25px">
                                        <asp:Label runat="server" ID="accName"></asp:Label></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="row-content">

                        <ul class="list-test">
                            <asp:DataList ID="DataList" runat="server" RepeatColumns="10" RepeatDirection="Vertical" RepeatLayout="Flow">
                                <ItemTemplate>
                                    <li>
                                        <asp:LinkButton CommandArgument='<%# Eval("InfoTypeCategory") %>' CommandName='<%# Eval("InfoTypeCategory") %>'
                                            ToolTip='<%# Eval("InfoTypeCategory") %>' ID="ModuleBTN" runat="server" Text='<%# Eval("InfoTypeCategoryPIC") + "&nbsp;&nbsp;" + Eval("InfoTypeCategory") %>'
                                                        OnCommand="DataBTN_Command">
                                        </asp:LinkButton>
                                    </li>
                                </ItemTemplate>
                            </asp:DataList>
                        </ul>
                        
                    </div>
                </div>

                <div class="row">
                    <div class="row-content">
                        <table class="table" border="0">
                            <thead>
                                <tr>
                                    <th style="padding-left: 0 !important">Info.</th>
                                </tr>
                            </thead>
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="AccountInfoGRD" runat="server" OnItemCommand="AccountInfoGRD_ItemCommand"
                                        Style="outline: none" CellSpacing="0" GridLines="None" AutoGenerateColumns="False" ShowHeadersWhenNoRecords="true"
                                        Width="100%" CssClass="m-l-0">
                                        <ClientSettings>
                                            <Selecting AllowRowSelect="False"></Selecting>
                                        </ClientSettings>
                                        <MasterTableView CommandItemDisplay="Top">
                                            <CommandItemSettings ShowAddNewRecordButton="False" ShowRefreshButton="False"></CommandItemSettings>
                                            <Columns>

                                                <telerik:GridBoundColumn DataField="AccID" HeaderText="AccID" Display="False"
                                                    UniqueName="AccID">
                                                    <HeaderStyle Width="80%" />
                                                    <ItemStyle Width="80%" />
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="LineID" HeaderText="LineID" Display="False"
                                                    UniqueName="LineID">
                                                    <HeaderStyle Width="80%" />
                                                    <ItemStyle Width="80%" />
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="InfoType" HeaderText="InfoType" Display="False"
                                                    UniqueName="InfoType">
                                                    <HeaderStyle Width="80%" />
                                                    <ItemStyle Width="80%" />
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="InfoTypeCategory" HeaderText="Category" Display="False"
                                                    UniqueName="InfoTypeCategory">
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle Width="100px" />
                                                </telerik:GridBoundColumn>


                                                <telerik:GridBoundColumn DataField="ValidationName" HeaderText="ValidationName" Display="False"
                                                    UniqueName="ValidationName">
                                                    <HeaderStyle Width="80%" />
                                                    <ItemStyle Width="80%" />
                                                </telerik:GridBoundColumn>


                                                <telerik:GridBoundColumn DataField="InfoTypeName" HeaderText="Info Name"
                                                    UniqueName="InfoTypeName">
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle Width="100px" />
                                                </telerik:GridBoundColumn>


                                                <telerik:GridBoundColumn DataField="InfoData" HeaderText="Info Data"
                                                    UniqueName="InfoData">
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle Width="100px" />
                                                </telerik:GridBoundColumn>



                                                <telerik:GridTemplateColumn UniqueName="Edit" HeaderText="Edit"
                                                    AllowFiltering="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="EditBTN" CommandName="Edit" Font-Size="14px"
                                                            CssClass="mainBtns" CausesValidation="False"><i class="fa fa-pencil-square-o" aria-hidden="true" style="font-size: 16px"></i></asp:LinkButton>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="10px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="15px" />
                                                </telerik:GridTemplateColumn>


                                            </Columns>
                                            <EditFormSettings EditFormType="Template">
                                                <FormTemplate>

                                                    <table border="0">

                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="InfoLBL" runat="server" Width="150px" Text="Info :"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <telerik:RadTextBox ID="GRDInfoTXT" runat="server" Text='<%# Bind("InfoData") %>'
                                                                    Width="200px"
                                                                    LabelCssClass="" LabelWidth="200px"
                                                                    Resize="None">
                                                                    <EmptyMessageStyle Resize="None" />
                                                                    <ReadOnlyStyle Resize="None" />
                                                                    <FocusedStyle Resize="None" />
                                                                    <DisabledStyle Resize="None" />
                                                                    <InvalidStyle Resize="None" />
                                                                    <HoveredStyle Resize="None" />
                                                                    <EnabledStyle Resize="None" />
                                                                </telerik:RadTextBox>

                                                                <%-- <asp:RegularExpressionValidator ID="GRDInfoTXTValidator" runat="server" ControlToValidate="GRDInfoTXT" Display="Dynamic" ForeColor="red" ErrorMessage="* Error Data Entry" ></asp:RegularExpressionValidator>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <asp:Label ID="GRDInfoTXTValidatorLBL" runat="server" Text="" ForeColor="red"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td colspan="2">
                                                                <asp:LinkButton ID="SaveBTN" CommandName="Save" runat="server"><i style="font-size: 18px;margin-top: 5px" class="fa fa-floppy-o" aria-hidden="true" title="Save"></i> </asp:LinkButton>
                                                                &nbsp;&nbsp;
                                                                <asp:LinkButton ID="CancelBTN" CommandName="Cancel" runat="server"><i style="font-size: 18px;margin-top: 5px" class="fa fa-times" aria-hidden="true" title="Cancel"></i></asp:LinkButton>
                                                            </td>
                                                        </tr>

                                                    </table>
                                                </FormTemplate>
                                            </EditFormSettings>
                                        </MasterTableView>
                                        <CommandItemStyle CssClass="hidn" />
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">

                                    <asp:Label runat="server" ID="lblAccID" CssClass="hidn"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

                <div class="row">
                    <div class="row-content">
                        <%--  <asp:Panel ID="AddInfoPNL" runat="server">--%>
                        <div class="rowItem">
                            <h5><b>Add Info.</b></h5>

                            <!-- Start: InfoTypeDLL and AddInfoBTN -->
                            <table class="table">
                                <tr>
                                    <td class="label-cell-width-x3">Info. Type</td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="InfoTypeDLL" CssClass="form-control" OnSelectedIndexChanged="InfoTypeDLL_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>

                                    </td>
                                </tr>
                                <tr>
                                    <td class="label-cell-width-x3">Info</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="InfoTXT" CssClass="form-control" MaxLength="50"></asp:TextBox>

                                        <asp:RegularExpressionValidator ID="InfoTXTValidator" runat="server" ControlToValidate="InfoTXT" Display="Dynamic" ForeColor="red" ErrorMessage="* Error Data Entry"></asp:RegularExpressionValidator>

                                    </td>
                                    <td style="border: 0 !important; width: 50px">
                                        <%--<asp:Button ID="AddInfoBTN" OnClick="AddInfoBTN_Click" Text="add" runat="server"/>--%>
                                        <asp:LinkButton runat="server" ID="InfoAddBTN" CssClass="btn btn-default" OnClick="AddInfoBTN_Click" Text="&lt;i class=&quot;fa fa-plus&quot; aria-hidden=&quot;true&quot;&gt;&lt;/i&gt;"></asp:LinkButton>

                                    </td>

                                </tr>

                            </table>
                            <!-- End: InfoTypeDLL and AddInfoBTN -->
                        </div>
                        <%--</asp:Panel>--%>

                        <div class="rowItem" style="display: none">
                            <h5><b>Info. Category</b></h5>

                            <!-- Start: AccountInfoCategoriesGRD -->

                            <div>
                              <%--  <telerik:RadGrid ID="AccountInfoCategoriesGRD" runat="server" OnItemCommand="AccountInfoCategoriesGRD_ItemCommand" ShowHeader="False"
                                    Style="outline: none" AutoGenerateColumns="False"
                                    Width="100%">
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="False"></Selecting>
                                    </ClientSettings>
                                    <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                    <MasterTableView CommandItemDisplay="Top">
                                        <CommandItemSettings ShowAddNewRecordButton="False" ShowRefreshButton="False"></CommandItemSettings>
                                        <Columns>

                                            <telerik:GridBoundColumn DataField="InfoTypeCategory" HeaderText="Category" Display="False"
                                                UniqueName="InfoTypeCategory">
                                                <HeaderStyle Width="80%" />
                                                <ItemStyle Width="80%" />
                                            </telerik:GridBoundColumn>

                                            <telerik:GridTemplateColumn UniqueName="List" HeaderText="List"
                                                AllowFiltering="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="ListBTN" CommandName="List" Text='<%# Eval("InfoTypeCategory").ToString() %>' Font-Size="14px"
                                                        CssClass="gridBtns" CausesValidation="False" Enabled="True" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="10px" />
                                                <ItemStyle HorizontalAlign="Center" Width="15px" />
                                            </telerik:GridTemplateColumn>

                                        </Columns>
                                        <EditFormSettings>
                                            <EditColumn FilterControlAltText="Filter SelectCommandColumn column">
                                            </EditColumn>
                                        </EditFormSettings>
                                    </MasterTableView>
                                    <CommandItemStyle CssClass="hidn" />
                                </telerik:RadGrid>--%>
                            </div>
                            <!-- End: AccountInfoCategoriesGRD -->
                        </div>

                    </div>
                </div>



            </div>

            <div id="Navegation_Panel">
                <div id="btn_Group_Panel" class="btn-group btn-breadcrumb" runat="server">
                    <a href="../Home/Home.aspx" class="btn btn-default"><i class="glyphicon glyphicon-home"></i></a>

                </div>
            </div>

            <!-- Modal---start -->
            <div id="modalPopup_confirm" title="Message" style="display: none">
                <p>
                    <i class="fa fa-warning fa-2x" style="float: left; padding-right: 7px"></i>
                    Are you sure ?
                </p>
                <br />
                <asp:Button runat="server" ID="btnConfirm" CssClass="btn btn-default" Text="yes" Width="80" OnClick="B_29_OnClick" ValidationGroup="save" />
                <a href="#" class="btn btn-default" style="width: 80px" onclick="CloseconfirmDialog()">No</a>
            </div>
            <!-- Modal---End -->

            <!-- Modal---start -->
            <div id="modalPopup_Cat1" title="Add Category (1)" style="display: none">
                <table class="table">
                    <tr>
                        <td>Category Name:</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="txtCategory1" CssClass="form-control txt-letter-form-control" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_txtCategory1" ControlToValidate="txtCategory1" Display="Dynamic" ValidationGroup="addCat1" ErrorMessage=" * This Field is Required" ForeColor="Red" />
                            <asp:Label runat="server" ID="lblCat1Msg" ForeColor="red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td dir="rtl">
                            <asp:LinkButton runat="server" ID="btnSaveCat1" CssClass="f-x6" Text="<i class='fa fa-floppy-o' aria-hidden='true'></i>" ValidationGroup="addCat1" ToolTip="Save"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
            <!-- Modal---End -->

            <!-- Modal---start -->
            <div id="modalPopup_Cat2" title="Add Category (2)" style="display: none">
                <table class="table">
                    <tr>
                        <td>Category Name:</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="txtAddCategory2" CssClass="form-control txt-letter-form-control" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_txtAddCategory2" ControlToValidate="txtAddCategory2" Display="Dynamic" ValidationGroup="addCat2" ErrorMessage=" * This Field is Required" ForeColor="Red" />
                            <asp:Label runat="server" ID="lblCat2Msg" ForeColor="red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td dir="rtl">
                            <asp:LinkButton runat="server" ID="btnSaveCat2" CssClass="f-x6" Text="<i class='fa fa-floppy-o' aria-hidden='true'></i>" ValidationGroup="addCat2" ToolTip="Save"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
            <!-- Modal---End -->

            <!-- Modal---start Check-->
            <div id="modalPopup_Check" title="Message" style="display: none">
                <p>
                    <i class="fa fa-warning fa-2x" style="float: left; padding-right: 7px"></i>
                    Are you sure ?
                </p>
                <br />
                <asp:Button runat="server" ID="btnConfm" CssClass="btn btn-default" Text="save" OnClick="B_29_OnClick" ValidationGroup="save" />
                <asp:LinkButton ID="lnkbtndntsve" runat="server" CssClass="btn btn-default" Text="Don't save" CausesValidation="False" OnClick="lnkbtndntsve_OnClick"></asp:LinkButton>
                <a href="#" class="btn btn-default" onclick="CloseCheckDialog()">Cancel</a>
            </div>
            <!-- Modal---End -->

            <asp:HiddenField ID="accCheckName" runat="server" />
            <asp:HiddenField ID="hidnMoveFlag" runat="server" />
            <asp:HiddenField ID="hidnViewFlag" runat="server" />
            <asp:HiddenField ID="AccIDEnc" runat="server" />
            <asp:HiddenField ID="AccIDDec" runat="server" />
            <asp:HiddenField ID="UserID" runat="server" />
            <asp:HiddenField ID="AccType" runat="server" />
            <asp:HiddenField ID="InfoType" runat="server" />
            <asp:HiddenField ID="InfoTypeID" runat="server" />


            <asp:HiddenField ID="GRDLineID" runat="server" />
            <asp:HiddenField ID="GRDInfoType" runat="server" />

            <asp:HiddenField ID="GRDModuleSaveType" runat="server" />

            <asp:HiddenField ID="CategoryFlag" runat="server" />

            <asp:HiddenField ID="GRDValidationName" runat="server" />

            <asp:HiddenField ID="EditedFieldValue" runat="server" />


        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>
