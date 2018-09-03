<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="AccountSales.aspx.cs" Inherits="CTA_NEW_PORTAL.Modules.Account.AccountSales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .radioBtns {
            /*position: relative;*/
            /*display: inline-block;*/
            Width: auto;
            /*margin-right: -55px;*/
            /*border:solid 1px #000;*/
            /*margin-bottom: -6px;*/
            /*text-align: left;*/
        }

            .radioBtns input[type="radio"] {
                width: 20px;
            }

            .radioBtns label {
                /*display: inline;*/
                /*border:solid 1px #f00;*/
                margin-left: -1px;
                margin-right: 10px;
            }

    </style>

    <asp:UpdatePanel ID="AccountUpdatePanel_Sales" runat="server">

        <ContentTemplate>
        
        <script type="text/javascript">

            var opt = {
                autoOpen: false,
                modal: true,
                width: 'auto',
                appendTo: $("#ContentPlaceHolder1_AccountUpdatePanel_Sales"),
                buttons: {
                    //Ok: function () {
                    //    $(this).dialog("close");
                    //}
                },
                draggable: true,
                resizable: false,
                close: function (event, ui) {
                    //$('a.Theme').removeClass('ui-state-focus ui-state-hover ui-state-active');
                    //$('a.Theme').blur();
                }

            };

            function ShowcurrencyDialog() {
                $('#modalPopup_Currency').dialog(opt).dialog("open");
            }

            function ClosecurrencyDialog() {
                $('#modalPopup_Currency').dialog('close');
            }

            function ShowpartsDialog() {
                $('#modalPopup_Parts').dialog(opt).dialog("open");
            }

            function ShowserviceDialog() {
                $('#modalPopup_Service').dialog(opt).dialog("open");
            }

        </script>

            <div id="Sub_Content">

                <div class="subject-row">
                    <h4>Accounts &nbsp;<i class="fa fa-caret-right" aria-hidden="true"></i>&nbsp;
                        <asp:Label ID="lblSubject" runat="server">View</asp:Label></h4>
                    <!-- Start: Header Buttons -->
                    <asp:Panel runat="server" ID="btnsPanel" CssClass="btns_Panel">
                        <asp:LinkButton runat="server" ID="B_11" Text="Inquiry" CssClass="mainBtns" CausesValidation="False" Enabled="False" OnClick="B_11_OnClick" /><%--Inquiry 1--%>
                        <asp:LinkButton runat="server" ID="B_12" Text="View" CssClass="mainBtns" Enabled="False" CausesValidation="False" OnClick="B_12_OnClick" /><%--View 2--%>
                        <asp:LinkButton runat="server" ID="B_13" Text="Print" CssClass="mainBtns" Enabled="False" /><%--Print 3--%>
                        <span style="width: 30px; display: inline-block"></span>
                        <asp:LinkButton runat="server" ID="B_21" Text="New" CssClass="mainBtns" CausesValidation="False" Enabled="False" OnClick="B_21_OnClick" /><%--New 4--%>
                        <asp:LinkButton runat="server" ID="B_22" Text="Update" CssClass="mainBtns" Enabled="False" OnClick="B_22_OnClick" /><%--Update 5--%>
                        <asp:LinkButton runat="server" ID="B_23" Text="Cancel" CssClass="mainBtns" Enabled="False" /><%--Cancel 6--%>
                        <asp:LinkButton runat="server" ID="B_29" Text="Save" CssClass="mainBtns" OnClick="B_29_OnClick" /><%--Save 7--%>
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

                <div id="accHeader" class="row">
                    <div class="row-content" style="border-bottom: 1px solid #d4d4d4;">
                        <div class="rowItem" style="min-height: 25px; margin-left: 5px;width: 46%">
                            <table>
                                <tr>
                                    <td class="label-cell-width-x3">Account Code:</td>
                                    <td style="text-indent: 20px">
                                        <asp:Label runat="server" ID="accNo"></asp:Label></td>
                                </tr>
                            </table>
                        </div>
                        <div class="rowItem" style="min-height: 25px;width: 47.5%">
                            <table>
                                <tr>
                                    <td class="label-cell-width-x3">Account Name:</td>
                                    <td style="text-indent: 15px">
                                        <asp:Label runat="server" ID="accName"></asp:Label></td>
                                </tr>

                            </table>
                        </div>
                    </div>
                </div>

                <br />

                <%-- Currncy & price list --%>
                <div class="row">
                    <div class="row-content">
                        <table class="table border-top-none" border="0">
                            <tr>
                                <td class="label-cell-width-x3">Currency</td>
                                <td class="Field-cell-width">
                                    <asp:DropDownList runat="server" ID="dllCurrency" CssClass="form-control"></asp:DropDownList>
                                </td>

                                <td style="width: 110px; min-width: 110px; max-width: 110px">
                                    <asp:LinkButton runat="server" ID="B_51" CssClass="btn btn-default" OnClick="B_51_OnClick"><i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton>
                                </td>

                                <td class="label-cell-width-x3">Price List</td>
                                <td class="Field-cell-width">
                                    <asp:DropDownList runat="server" ID="ddlpriceList" CssClass="form-control"></asp:DropDownList>
                                </td>
                                <td style="width: 60px; min-width: 60px; max-width: 60px">
                                   
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

                <%-- Discount --%>
                <div class="row">
                    <div class="row-content">
                        <table class="table" border="0">
                            <thead>
                                <tr>
                                    <th colspan="6">Discount</th>
                                </tr>
                            </thead>

                            <tbody>
                                <tr>
                                    <td class="label-cell-width-x3"></td>

                                    <td class="Field-cell-width">
                                        <asp:TextBox runat="server" ID="TextBox1" CssClass="form-control visbl"></asp:TextBox></td>

                                    <td style="width: 110px; min-width: 110px; max-width: 110px"></td>

                                    <td class="label-cell-width-x3">General</td>

                                    <td class="Field-cell-width">
                                        <asp:TextBox runat="server" ID="txtGeneral" CssClass="form-control"></asp:TextBox>
                                        <asp:RangeValidator runat="server" Type="Double" MinimumValue="0" MaximumValue="100" ControlToValidate="txtGeneral" ForeColor="red" ErrorMessage="Value must be between 0 and 100" />
                                    </td>

                                    <td style="border: 0 !important; width: 50px">
                                        <asp:LinkButton runat="server" ID="btnAccessGeneral" CssClass="btn btn-default"><i class="fa fa-key" aria-hidden="true"></i></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label-cell-width-x3">Type</td>

                                    <td class="Field-cell-width">
                                        <asp:DropDownList runat="server" ID="ddlDiscountType" CssClass="form-control"></asp:DropDownList></td>

                                    <td style="width: 110px; min-width: 110px; max-width: 110px">&nbsp;</td>

                                    <td class="label-cell-width-x3">Parts</td>

                                    <td class="Field-cell-width">
                                        <asp:TextBox runat="server" ID="txtParts" CssClass="form-control"></asp:TextBox>
                                        <asp:RangeValidator runat="server" Type="Double" MinimumValue="0" MaximumValue="100" ControlToValidate="txtParts" ForeColor="red" ErrorMessage="Value must be between 0 and 100" />
                                    </td>
                                    <td style="border: 0 !important; width: 50px">
                                        <asp:LinkButton runat="server" ID="B_61" CssClass="btn btn-default" OnClick="B_61_OnClick"><i class="fa fa-key" aria-hidden="true"></i></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label-cell-width-x3">Model</td>

                                    <td class="Field-cell-width">
                                        <asp:DropDownList runat="server" ID="ddlDiscountModel" CssClass="form-control"></asp:DropDownList></td>

                                    <td style="width: 110px; min-width: 110px; max-width: 110px">&nbsp;</td>

                                    <td class="label-cell-width-x3">Service</td>

                                    <td class="Field-cell-width">
                                        <asp:TextBox runat="server" ID="txtService" CssClass="form-control"></asp:TextBox>
                                        <asp:RangeValidator runat="server" Type="Double" MinimumValue="0" MaximumValue="100" ControlToValidate="txtService" ForeColor="red" ErrorMessage="Value must be between 0 and 100" />
                                    </td>

                                    <td style="border: 0 !important; width: 50px">
                                        <asp:LinkButton runat="server" ID="B_62" CssClass="btn btn-default" OnClick="B_62_OnClick"><i class="fa fa-key" aria-hidden="true"></i></asp:LinkButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>

                <%-- Tax --%>
                <div class="row">
                    <div class="row-content">
                        <table class="table" border="0">
                            <thead>
                                <tr>
                                    <th colspan="6">Tax</th>
                                </tr>
                            </thead>

                            <tbody>
                                <tr>
                                    <td class="label-cell-width-x3">Taxable</td>
                                    <td class="Field-cell-width">
                                        <asp:RadioButtonList ID="rblTaxable" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table" runat="server" CssClass="radioBtns" OnTextChanged="rblTaxable_OnTextChanged" AutoPostBack="True">
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>


                                    <td style="width: 110px; min-width: 110px; max-width: 110px">&nbsp;</td>

                                    <td class="label-cell-width-x3">Code</td>
                                    <td class="Field-cell-width">
                                        <asp:DropDownList runat="server" ID="ddlCode" CssClass="form-control"></asp:DropDownList></td>
                                    <td style="width: 60px; min-width: 60px; max-width: 60px">&nbsp;</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>

                <br />

                <%-- Payment Type & Responsabil Person --%>
                <div class="row">
                    <div class="row-content">
                        <table class="table" border="0">
                            <tr>
                                <td class="label-cell-width-x3">Payment Type</td>
                                <td class="Field-cell-width">
                                    <asp:DropDownList runat="server" ID="ddlPaymentType" CssClass="form-control"></asp:DropDownList>
                                </td>

                                <td style="width: 110px; min-width: 110px; max-width: 110px">&nbsp;</td>

                                <td class="label-cell-width-x3">Resp. Person</td>

                                <td class="Field-cell-width">
                                    <asp:DropDownList runat="server" ID="ddlRespPerson" CssClass="form-control"></asp:DropDownList>
                                </td>
                                <td style="width: 60px; min-width: 60px; max-width: 60px">&nbsp;</td>
                            </tr>
                        </table>
                    </div>
                </div>

                <%-- Memo --%>
                <div class="row">
                    <div class="row-content">
                        <table class="table" border="0">
                            <tr>
                                <td class="label-cell-width-x3">Memo</td>
                                <td>
                                    <textarea id="txtMemo" runat="server" class="form-control" maxlength="500"></textarea>
                                </td>
                                <td style="width: 50px">&nbsp;</td>
                            </tr>
                        </table>
                    </div>
                </div>

            </div>

            <div id="Navegation_Panel">
                <div id="btn_Group_Panel" class="btn-group btn-breadcrumb" runat="server">
                    <a href="../Home/Home.aspx" class="btn btn-default"><i class="glyphicon glyphicon-home"></i></a>

                </div>
            </div>
                
            <!-- Modal---start Currency-->
            <div id="modalPopup_Currency" title="Add new currency" style="display: none">
                <table class="table">
                    <tr>
                        <td>Currency Name:</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="txtCurrency" CssClass="form-control txt-letter-uppercase-form-control" MaxLength="3"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator_txtCurrency" ControlToValidate="txtCurrency" Display="Dynamic" ValidationGroup="addCurrency" ErrorMessage=" * This Field is Required" ForeColor="Red" />
                            <asp:Label runat="server" ID="lblCurrencyMsg" ForeColor="red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td dir="rtl">
                            <asp:LinkButton runat="server" ID="btnSaveCurrency" CssClass="f-x6" Text="<i class='fa fa-floppy-o' aria-hidden='true'></i>" OnClick="btnSaveCurrency_OnClick" ValidationGroup="addCurrency" ToolTip="Save"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
            <!-- Modal---End -->
        
            <!-- Modal---start Parts-->
            <div id="modalPopup_Parts" title="Parts" style="display: none">
                <table class="table">
                    <tr>
                        <td>Parts:</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="txtParts_2" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtParts_2" Display="Dynamic" ValidationGroup="addParts" ErrorMessage=" * This Field is Required" ForeColor="Red" />
                            <asp:RangeValidator runat="server" Type="Double" MinimumValue="0" MaximumValue="100" ControlToValidate="txtParts_2" ForeColor="red" Display="Dynamic" ValidationGroup="addParts" ErrorMessage="Value must be between 0 and 100" />
                        </td>
                    </tr>
                    <tr>
                        <td dir="rtl">
                            <asp:LinkButton runat="server" ID="lBSave_Parts" CssClass="f-x6" Text="<i class='fa fa-floppy-o' aria-hidden='true'></i>" OnClick="lBSave_Parts_OnClick" ValidationGroup="addParts" ToolTip="Save"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
            <!-- Modal---End -->
        
            <!-- Modal---start Service-->
            <div id="modalPopup_Service" title="Service" style="display: none">
                <table class="table">
                    <tr>
                        <td>Service:</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox runat="server" ID="txtService_2" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtService_2" Display="Dynamic" ValidationGroup="addService" ErrorMessage=" * This Field is Required" ForeColor="Red" />
                            <asp:RangeValidator runat="server" Type="Double" MinimumValue="0" MaximumValue="100" ControlToValidate="txtService_2" ForeColor="red" Display="Dynamic" ValidationGroup="addService" ErrorMessage="Value must be between 0 and 100" />
                        </td>
                    </tr>
                    <tr>
                        <td dir="rtl">
                            <asp:LinkButton runat="server" ID="lbSave_Service" CssClass="f-x6" Text="<i class='fa fa-floppy-o' aria-hidden='true'></i>" OnClick="lbSave_Service_OnClick" ValidationGroup="addService" ToolTip="Save"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
            <!-- Modal---End -->

            <asp:Label runat="server" ID="lblAccID" CssClass="hidn"></asp:Label>
            <asp:HiddenField ID="accCheckName" runat="server" />
            <asp:HiddenField ID="hidnMoveFlag" runat="server" />
            <asp:HiddenField ID="hidnViewFlag" runat="server" />
            <asp:HiddenField ID="AccIDEnc" runat="server" />
            <asp:HiddenField ID="AccIDDec" runat="server" />
            <asp:HiddenField ID="UserID" runat="server" />
            <asp:HiddenField ID="AccType" runat="server" />
            <asp:HiddenField ID="InfoType" runat="server" />
            <asp:HiddenField ID="InfoTypeID" runat="server" />




        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>
