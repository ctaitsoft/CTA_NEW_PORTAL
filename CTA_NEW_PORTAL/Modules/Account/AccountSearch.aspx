<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="AccountSearch.aspx.cs" Inherits="CTA_NEW_PORTAL.Modules.Account.AccountSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        if (document.layers) {
            //Capture the MouseDown event.
            document.captureEvents(Event.MOUSEDOWN);

            //Disable the OnMouseDown event handler.
            document.onmousedown = function () {
                return false;
            };
        }
        else {
            //Disable the OnMouseUp event handler.
            document.onmouseup = function (e) {
                if (e != null && e.type == "mouseup") {
                    //Check the Mouse Button which is clicked.
                    if (e.which == 2 || e.which == 3) {
                        //If the Button is middle or right then disable.
                        return false;
                    }
                }
            };
        }

        //Disable the Context Menu event.
        document.oncontextmenu = function () {
            return false;
        };
    </script>

    <script type="text/javascript">
        document.onkeypress = function (event) {
            event = (event || window.event);
            if (event.keyCode == 123) {
                return false;
            }
        }
        document.onmousedown = function (event) {
            event = (event || window.event);
            if (event.keyCode == 123) {
                return false;
            }
        }
        document.onkeydown = function (event) {
            event = (event || window.event);
            if (event.keyCode == 123) {
                return false;
            }
        }
    </script>

    <script type="text/javascript">

        function SearchTXTEnter(e) {
            if (e.keyCode == 13) {
                try {
                    document.getElementById('<%=btnSearch.ClientID %>').focus();
                }
                catch (err) {
                    alert("Error");
                }
            }

        }

        <%-- function CustomerName2TXTEnter(e) {

                if (e.keyCode == 13) {

                    try {
                        document.getElementById('<%=CustomerName3TXT.ClientID %>').focus();
                    }
                    catch (err) {

                        alert("Error");
                    }
                }
            }

            function CustomerName3TXTEnter(e) {

                if (e.keyCode == 13) {

                    try {

                        document.getElementById('<%=CustomerName4TXT.ClientID %>').focus();
                    }
                    catch (err) {
                        alert("Error");
                    }
                }

            }

            function CustomerName4TXTEnter(e) {

                if (e.keyCode == 13) {

                    try {
                        document.getElementById('<%=CellPhoneTXT.ClientID %>').focus();
                    }

                    catch (err) {
                        alert("Error");
                    }
                }
            }

            function CellPhoneTXTEnter(e) {
                if (e.keyCode == 13) {
                    try {
                        document.getElementById('<%=CustEmailTXT.ClientID %>').focus();
                    }

                    catch (err) {
                        alert("Error");
                    }
                }
            }

            function txtCaptchaEnter(e) {

                if (e.keyCode == 13) {
                    try {
                        document.getElementById('<%=BtnSubmit2.ClientID %>').focus();
                    }

                    catch (err) {
                        alert("Error");
                    }
                }
            }

            function dateValidation() {
                var enteredDay = document.getElementById('<%=ddlDay.ClientID %>').value;
                var enteredMonth = document.getElementById('<%=ddlMonth.ClientID %>').value;
                var enteredYear = document.getElementById('<%=ddlYear.ClientID %>').value;
                if (enteredDay != 0 && enteredMonth != 0 && enteredYear != 0) {

                    document.getElementById('<%=DateValidatorTXT.ClientID %>').value = document.getElementById('<%=ddlDay.ClientID %>').value + "/" + document.getElementById('<%=ddlMonth.ClientID %>').value + "/" + document.getElementById('<%=ddlYear.ClientID %>').value;
                    var obj = document.getElementById("<%=DateValidatorTXT.ClientID%>");

                    var day = obj.value.split("/")[0];
                    var month = obj.value.split("/")[1];

                    var year = obj.value.split("/")[2];
                    if ((day < 1 || day > 31) || (month < 1 && month > 12) && (year.length != 4)) {
                        alert("التاريخ المدخل غير صحيح"); return false;

                    }

                    else {
                        var dt = new Date(year, month - 1, day);

                        var today = new Date();
                        if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt > today)) {
                            alert("التاريخ المدخل غير صحيح"); return false;

                        }
                    }
                }

            } --%>
    </script>

    <script type="text/javascript">
        <%--function OnClientClicked(sender, args) {
            var window = $find('<%=AddCustomerWindow.ClientID %>');
            window.close();
        }--%>
    </script>

    <%-- <script type="text/javascript">
          $("#CustomersGRD th[data-field=AccNo]").html("wwwwwwwww");
      </script>--%>

    <style>
        #ContentPlaceHolder1_AccountUpdatePanel {
            width: 100%;
            /*border: solid 1px #f00;*/
        }

        .rgDataDiv {
            height: 320px !important;
        }
    </style>

    <asp:UpdatePanel ID="AccountUpdatePanel" runat="server">
        <ContentTemplate>
        
        <script type="text/javascript">

            var opt = {
                autoOpen: false,
                modal: true,
                width: 'auto',
                appendTo: $("#ContentPlaceHolder1_AccountUpdatePanel"),
                buttons: {
                    //Ok: function () {
                    //    $(this).dialog("close");
                    //}
                },
                draggable: true,
                resizable: false,
                close: function (event, ui) {
                    $('a.Theme').removeClass('ui-state-focus ui-state-hover ui-state-active');
                    $('a.Theme').blur();
                }

            };

            function ShowconfirmDialog() {
                $('#modalPopup_confirm').dialog(opt).dialog("open");
            }

            function CloseconfirmDialog() {
                $('#modalPopup_confirm').dialog('close');
            }

            function ShowTypeDialog() {
                $('#modalPopup_Type').dialog(opt).dialog("open");
            }

        </script>

            <div id="Sub_Content">

                <div class="subject-row">
                    <h4>Accounts &nbsp;<i class="fa fa-caret-right" aria-hidden="true"></i>&nbsp; Search</h4>
                    <asp:Panel runat="server" ID="btnsPanel" CssClass="btns_Panel">
                        <asp:LinkButton runat="server" ID="B_11" Text="Inquiry" CssClass="mainBtns" CausesValidation="False" Enabled="False" /><%--Inquiry 1--%>
                        <asp:LinkButton runat="server" ID="B_12" Text="View" CssClass="mainBtns" Enabled="False" OnClick="B_12_OnClick" /><%--View 2--%>
                        <asp:LinkButton runat="server" ID="B_13" Text="Print" CssClass="mainBtns" Enabled="False" /><%--Print 3--%>
                        <span style="width: 30px; display: inline-block"></span>
                        <asp:LinkButton runat="server" ID="B_21" Text="New" CssClass="mainBtns" CausesValidation="False" Enabled="False" OnClick="AddCustomerBTN_Click" /><%--New 4--%>
                        <asp:LinkButton runat="server" ID="B_22" Text="Update" CssClass="mainBtns" Enabled="False" /><%--Update 5--%>
                        <asp:LinkButton runat="server" ID="B_23" Text="Cancel" CssClass="mainBtns" Enabled="False" /><%--Cancel 6--%>
                        <asp:LinkButton runat="server" ID="B_29" Text="Save" CssClass="mainBtns" ValidationGroup="save" Enabled="False" /><%--Save 7--%>
                        <span style="width: 30px; display: inline-block"></span>
                        <asp:LinkButton runat="server" ID="B_18" Text="Refresh" CssClass="mainBtns" CausesValidation="False" OnClick="SearchBTN_Click" /><%--Refresh 8--%>
                        <asp:LinkButton runat="server" ID="B_19" Text="Clear" CssClass="mainBtns" CausesValidation="False" OnClick="ClearSearchBTN_Click" /><%--Clear 9--%>

                        <div class="btn-group">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">More &nbsp;<span class="caret"></span></a>
                            <ul class="dropdown-menu">
                                <li>
                                    <asp:LinkButton runat="server" ID="B_31" Text="Copy" Enabled="False" /></li>
                                <%--Copy 10--%>
                                <li>
                                    <asp:LinkButton runat="server" ID="B_32" Text="Paste" Enabled="False" /></li>
                                <%--Paste 11--%>
                                <li>
                                    <asp:LinkButton runat="server" ID="B_41" Text="Import" Enabled="False" /></li>
                                <%--Import 12--%>
                                <li>
                                    <asp:LinkButton runat="server" ID="B_42" Text="Export" Enabled="False" OnClick="Export_OnClick" /></li>
                                <%--Export 13--%>
                                <li role="separator" class="divider"></li>
                                <li>
                                    <asp:LinkButton runat="server" ID="B_99" Text="Special Fields" Enabled="False" /></li>
                                <%--Special Fields 14--%>
                            </ul>
                        </div>

                    </asp:Panel>
                </div>

                <div class="row">
                    <div class="row-content">
                        <table class="table table-border table-margin-bottom" border="0">
                            <tr>
                                <td class="label-cell-width">Search ( Account No. | Name | Mobile ) 
                                </td>
                                <td>
                                    <asp:Panel runat="server" DefaultButton="btnSearch" ID="searchPanel">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <asp:TextBox ID="SearchTXT" runat="server" MaxLength="65" onkeypress="return SearchTXTEnter(event)" CssClass="form-control"></asp:TextBox>
                                                <div class="input-group-addon">
                                                    <asp:LinkButton ID="btnSearch" runat="server" OnClick="SearchBTN_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                                                </div>
                                                <%--<div class="input-group-addon">
                                                    <asp:LinkButton ID="btnClearSearch" runat="server" Text="Clear" OnClick="ClearSearchBTN_Click"><i class="fa fa-times" aria-hidden="true"></i></asp:LinkButton></div>--%>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                </td>

                                <%--<td>
                                    <asp:Button ID="AddCustomerBTN" runat="server" Text="Add Customer" OnClick="AddCustomerBTN_Click" CssClass="btn btn-default" />
                                </td>--%>
                            </tr>
                        </table>
                        <asp:Label runat="server" ID="lblAccId" CssClass="hidn"></asp:Label>
                    </div>
                </div>
            
                <div id="accHeader" class="row" runat="server" style="display: none">
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

                        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
                        </telerik:RadAjaxLoadingPanel>

                        <div class="demo-container no-bg">
                            <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">

                                    <telerik:RadGrid Style="outline: none" ID="CustomersGRD" runat="server" AllowPaging="True" AllowSorting="True"
                                        RenderMode="Lightweight" AutoGenerateColumns="false" OnItemCommand="CustomersGRD_ItemCommand" OnNeedDataSource="CustomersGRD_OnNeedDataSource">
                                        <%--OnExcelMLExportStylesCreated="CustomersGRD_OnExcelMLExportStylesCreated"--%>
                                        <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>

                                        <PagerStyle Mode="NextPrevAndNumeric" PageSizeControlType="RadComboBox"></PagerStyle>

                                        <ExportSettings FileName="Accounts"></ExportSettings>

                                        <ClientSettings>
                                            <Selecting AllowRowSelect="True"></Selecting>
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                        </ClientSettings>

                                        <MasterTableView CommandItemDisplay="Top" DataKeyNames="AccID">
                                            <CommandItemSettings ShowAddNewRecordButton="False" ShowExportToCsvButton="False" ShowRefreshButton="False"></CommandItemSettings>
                                            <Columns>

                                                <telerik:GridBoundColumn HeaderText="Acc Id" UniqueName="AccID" DataField="AccID" FilterControlWidth="150px" Display="false">
                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn HeaderText="Account No." UniqueName="AccNo" DataField="AccNo"
                                                    FilterControlWidth="150px">
                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn HeaderText="Account Name" UniqueName="AccName" DataField="AccName"
                                                    FilterControlWidth="150px">
                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                </telerik:GridBoundColumn>

                                                <%--      <telerik:GridBoundColumn HeaderText="LineID" UniqueName="LineID" DataField="LineID"  Display="false"
                                        FilterControlWidth="150px">
                                        <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                                    </telerik:GridBoundColumn>--%>

                                                <telerik:GridBoundColumn HeaderText="Mobile (1)" UniqueName="AccMobile1" DataField="AccMobile1"
                                                    FilterControlWidth="150px">
                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn HeaderText="Mobile (2)" UniqueName="AccMobile2" DataField="AccMobile2"
                                                    FilterControlWidth="150px">
                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                </telerik:GridBoundColumn>

                                                <%--   <telerik:GridBoundColumn HeaderText="InfoType" UniqueName="InfoType" DataField="InfoType"  Display="false"
                                        FilterControlWidth="150px">
                                        <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                                    </telerik:GridBoundColumn>

                                      <telerik:GridBoundColumn HeaderText="Address" UniqueName="InfoData" DataField="InfoData"  
                                        FilterControlWidth="150px">
                                        <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                                    </telerik:GridBoundColumn>--%>

                                                <%--<telerik:GridButtonColumn ButtonType="ImageButton" ImageUrl="~/images/PNG/edit.png" CommandName="Edit" HeaderText="Edit" Exportable="False">
                                            <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                                        </telerik:GridButtonColumn>--%>

                                                <telerik:GridTemplateColumn UniqueName="EditCustomer" HeaderText="View" AllowFiltering="False" Exportable="false"
                                                    FilterControlAltText="Filter EditCustomer column">
                                                    <ItemTemplate>
                                                        <%--<asp:ImageButton ID="EditCustomerBTN" CommandName="EditCommand" ImageUrl="~/images/PNG/edit.png"
                                                runat="server" Width="18px" Height="18px" />--%>
                                                        <asp:LinkButton ID="EditCustomerBTN" runat="server" CommandName="EditCommand"><i class="fa fa-pencil-square-o" aria-hidden="true" style="font-size: 16px"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                </telerik:GridTemplateColumn>
                                                
                                                <telerik:GridTemplateColumn UniqueName="LinkCustomer" HeaderText="Link" AllowFiltering="False" Exportable="false" FilterControlAltText="Filter EditCustomer column">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkCustomerBTN" runat="server" CommandName="LinkCommand"><i class="fa fa-link" aria-hidden="true" style="font-size: 16px"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                </telerik:GridTemplateColumn>

                                            </Columns>
                                            <EditFormSettings EditFormType="Template">
                                                <FormTemplate>
                                                </FormTemplate>
                                            </EditFormSettings>
                                        </MasterTableView>
                                        <CommandItemStyle CssClass="hidn" />
                                    </telerik:RadGrid>

                                </telerik:RadAjaxPanel>

                            </telerik:RadCodeBlock>
                        </div>

                        <%--<telerik:RadWindow ID="AddCustomerWindow" InitialBehaviors="None" Title="Customer Type" runat="server" MaxHeight="100px" MaxWidth="500px" VisibleStatusbar="false" Behaviors="Close,Move" EnableShadow="true" AutoSize="false" AutoSizeBehaviors="Default">

                <ContentTemplate>
                    <telerik:RadButton ID="RadButton1" runat="server" AutoPostBack="false" Visible="false" OnClientClicked="OnClientClicked"></telerik:RadButton>
                    <table>
                        <tr>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="IndividualBTN" Text="Individual" Width="90px" runat="server" OnClick="IndividualBTN_Click" /></td>
                            <td>
                                <asp:Button ID="CorporateBTN" Text="Corporate" Width="90px" runat="server" OnClick="CorporateBTN_Click" /></td>
                        </tr>
                    </table>
                </ContentTemplate>
            </telerik:RadWindow>--%>


                        <asp:Panel runat="server" ID="HiddenPanelPNL">

                            <div style="display: none">
                                <asp:TextBox ID="DateValidatorTXT" runat="server"></asp:TextBox>
                            </div>

                        </asp:Panel>

                    </div>

                </div>

            </div>

            <div id="Navegation_Panel">
                <div id="btn_Group_Panel" class="btn-group btn-breadcrumb" runat="server">
                    <a href="../Home/Home.aspx" class="btn btn-default"><i class="glyphicon glyphicon-home"></i></a>

                </div>
            </div>
        
        
            <!-- Modal---start Confirm-->
            <div id="modalPopup_confirm" title="Message" style="display: none">
                <p>
                    <i class="fa fa-warning fa-2x" style="float: left; padding-right: 7px"></i>
                    Are you sure ?
                </p>
                <br />
                <asp:Button runat="server" ID="btnConfirm" CssClass="btn btn-default" Text="yes" Width="80" OnClick="btnConfirm_OnClick" />
                <a href="#" class="btn btn-default" style="width: 80px" onclick="CloseconfirmDialog()">No</a>
            </div>
            <!-- Modal---End -->
        
            <!-- Modal---start Title-->
            <div id="modalPopup_Type" title="Contact type" style="display: none">
            <table class="table">
                <tr>
                    <td>Contact Type:</td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlContactType" CssClass="form-control txt-letter-form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_ddlContactType" ControlToValidate="ddlContactType" Display="Dynamic" ValidationGroup="ContactType" ErrorMessage=" * This Field is Required" ForeColor="Red" InitialValue="-1" />
                    </td>
                </tr>
                <tr>
                    <td dir="rtl">
                        <asp:LinkButton runat="server" ID="btnLinkContact" CssClass="f-x6" Text="<i class='fa fa-floppy-o' aria-hidden='true'></i>" OnClick="btnLinkContact_OnClick" ValidationGroup="ContactType" ToolTip="Save"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
            <!-- Modal---End -->

        </ContentTemplate>

        <Triggers>
            <%-- for export & import --%>
            <asp:PostBackTrigger ControlID="B_41" />
            <asp:PostBackTrigger ControlID="B_42" />
        </Triggers>

    </asp:UpdatePanel>

</asp:Content>
