<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="AccountCrdt.aspx.cs" Inherits="CTA_NEW_PORTAL.Modules.Account.AccountCrdt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
    </style>

    <asp:UpdatePanel ID="AccountUpdatePanel_Crdt" runat="server">
        <ContentTemplate>

            <script type="text/javascript">
                
                function CheckMove() {
                    document.getElementById('<%= hidnMoveFlag.ClientID %>').value = "1";
                }

                var opt = {
                    autoOpen: false,
                    modal: true,
                    width: 'auto',
                    appendTo: $("#ContentPlaceHolder1_AccountUpdatePanel_Crdt"),
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

                function ShowCheckDialog() {
                    $('#modalPopup_Check').dialog(opt).dialog("open");
                }

                function CloseCheckDialog() {
                    $('#modalPopup_Check').dialog('close');
                }

                function ShowCat_1_Dialog() {
                    $('#modalPopup_Cat1').dialog(opt).dialog("open");
                }

                function ShowCat_2_Dialog() {
                    $('#modalPopup_Cat2').dialog(opt).dialog("open");
                }

                // Warning before leaving the page (back button, or outgoinglink)
                window.onbeforeunload = function () {
                    if (document.getElementById('<%= hidnMoveFlag.ClientID %>').value == "1") {
                        return "Do you really want to leave ?";
                    } else {

                    }
                    //if we return nothing here (just calling return;) then there will be no pop-up question at all
                    //return;
                };


            </script>

            <div id="Sub_Content">
                <div class="subject-row">
                    <h4>Account Credit &nbsp;<i class="fa fa-caret-right" aria-hidden="true"></i>&nbsp; <asp:Label ID="lblSubject" runat="server"></asp:Label></h4>
                    <asp:Panel runat="server" ID="btnsPanel" CssClass="btns_Panel">
                        <asp:LinkButton runat="server" ID="B_11" Text="Inquiry" CssClass="mainBtns" CausesValidation="False" Enabled="False" OnClick="B_11_OnClick" /><%--Inquiry 1--%>
                        <asp:LinkButton runat="server" ID="B_12" Text="View" CssClass="mainBtns" Enabled="False" CausesValidation="False" OnClick="B_12_OnClick" /><%--View 2--%>
                        <asp:LinkButton runat="server" ID="B_13" Text="Print" CssClass="mainBtns" Enabled="False" /><%--Print 3--%>
                        <span style="width: 30px; display: inline-block"></span>
                        <asp:LinkButton runat="server" ID="B_21" Text="New" CssClass="mainBtns" CausesValidation="False" Enabled="False" OnClick="B_21_OnClick" /><%--New 4--%>
                        <asp:LinkButton runat="server" ID="B_22" Text="Update" CssClass="mainBtns" Enabled="False" OnClick="B_22_OnClick" /><%--Update 5--%>
                        <asp:LinkButton runat="server" ID="B_23" Text="Cancel" CssClass="mainBtns" Enabled="False" /><%--Cancel 6--%>
                        <asp:LinkButton runat="server" ID="B_29" Text="Save" CssClass="mainBtns" OnClientClick="if(Page_ClientValidate('save')) ShowconfirmDialog(); return false;" /><%--Save 7--%>
                        <span style="width: 30px; display: inline-block"></span>
                        <asp:LinkButton runat="server" ID="B_18" Text="Refresh" CssClass="mainBtns" CausesValidation="False" OnClick="B_18_OnClick" /><%--Refresh 8--%>
                        <asp:LinkButton runat="server" ID="B_19" Text="Clear" CssClass="mainBtns" CausesValidation="False" OnClick="B_19_OnClick" /><%--Clear 9--%>

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

                </div>

                <div class="row">
                    <div class="row-content" style="border-bottom: 1px solid #d4d4d4;">
                        <div class="rowItem" style="min-height: 25px">
                            <table>
                                <tr>
                                    <td class="label-cell-width-x3">Account Code:</td>
                                    <td style="text-indent: 20px"><asp:Label runat="server" ID="accNo"></asp:Label></td>
                                </tr>
                            </table>
                        </div>
                        <div class="rowItem" style="min-height: 25px">
                            <table>
                                <tr>
                                    <td class="label-cell-width-x3">Account Name:</td>
                                    <td style="text-indent: 25px"><asp:Label runat="server" ID="accName"></asp:Label></td>
                                </tr>
                            </table>
                        </div> 
                    </div>
                </div>

                <div class="row">
                    <div class="row-content">
                        <div class="rowItem">
                            <h5><b>Credit Information</b></h5>
                            <table class="table" border="0">
                                <tr>
                                    <td class="label-cell-width-x3">Credit No.</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtCrdtNo" CssClass="form-control" MaxLength="10" onkeydown="javascript:isNumber2(event,this.id); CheckMove();"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="txt_Validator1" ControlToValidate="txtCrdtNo" Display="Dynamic" ErrorMessage=" * This Field is Required" ForeColor="Red" ValidationGroup="save" />
                                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtCrdtNo" ValidationExpression="^[0-9]{1,20}$" Display="Dynamic" ForeColor="red" ErrorMessage="* Only numbers allowed"></asp:RegularExpressionValidator>
                                        <asp:Label AssociatedControlID="txtCrdtNo" Text="" ForeColor="red" runat="server" CssClass="hidn" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label-cell-width-x3">Category (1)</td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="dllCategory_1" CssClass="form-control" onchange="javascript:CheckMove();"></asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ID="txt_Validator2" ControlToValidate="dllCategory_1" Display="Dynamic" ErrorMessage=" * This Field is Required" ForeColor="Red" InitialValue="0" ValidationGroup="save" />
                                    </td>
                                    <td style="border: 0 !important;width: 50px"><asp:LinkButton runat="server" ID="btnAddCat1" CssClass="btn btn-default" OnClientClick="ShowCat_1_Dialog(); return false;"><i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton></td>
                                </tr>
                                <tr>
                                    <td class="label-cell-width-x3">Category (2)</td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="dllCategory_2" CssClass="form-control" onchange="javascript:CheckMove();"></asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ID="txt_Validator3" ControlToValidate="dllCategory_2" Display="Dynamic" ErrorMessage=" * This Field is Required" ForeColor="Red" InitialValue="0" ValidationGroup="save" />
                                    </td>
                                    <td style="border: 0 !important;width: 50px"><asp:LinkButton runat="server" ID="btnAddCat2" CssClass="btn btn-default" OnClientClick="ShowCat_2_Dialog(); return false;"><i class="fa fa-plus" aria-hidden="true"></i></asp:LinkButton></td>
                                </tr>

                                <tr>
                                    <td class="label-cell-width-x3">Responsible</td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="dllResponsible" CssClass="form-control" onchange="javascript:CheckMove();"></asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ID="txt_Validator4" ControlToValidate="dllResponsible" Display="Dynamic" ErrorMessage=" * This Field is Required" ForeColor="Red" InitialValue="0" ValidationGroup="save" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="rowItem">
                            <h5><b>Credit Limit</b></h5>
                            <table class="table">
                                <tr>
                                    <td class="label-cell-width-x3">Type</td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="dllType" CssClass="form-control" onchange="javascript:CheckMove();"></asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ID="txt_Validator5" ControlToValidate="dllType" Display="Dynamic" ErrorMessage=" * This Field is Required" ForeColor="Red" InitialValue="0" ValidationGroup="save" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label-cell-width-x3">Max</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtMax" CssClass="form-control" MaxLength="10" onkeydown="javascript:isNumber2(event,this.id); CheckMove();"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="txt_Validator6" ControlToValidate="txtMax" Display="Dynamic" ErrorMessage=" * This Field is Required" ForeColor="Red" ValidationGroup="save" />
                                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtMax" ValidationExpression="^[0-9]{1,20}$" Display="Dynamic" ForeColor="red" ErrorMessage="* Only numbers allowed"></asp:RegularExpressionValidator>
                                        <asp:Label AssociatedControlID="txtMax" Text="" ForeColor="red" runat="server" CssClass="hidn" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label-cell-width-x3">Extra</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtExtra" CssClass="form-control" MaxLength="10" onkeydown="javascript:isNumber2(event,this.id); CheckMove();"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="txt_Validator7" ControlToValidate="txtExtra" Display="Dynamic" ErrorMessage=" * This Field is Required" ForeColor="Red" ValidationGroup="save" />
                                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtExtra" ValidationExpression="^[0-9]{1,20}$" Display="Dynamic" ForeColor="red" ErrorMessage="* Only numbers allowed"></asp:RegularExpressionValidator>
                                        <asp:Label AssociatedControlID="txtExtra" Text="" ForeColor="red" runat="server" CssClass="hidn" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label-cell-width-x3">Blocked</td>
                                    <td>
                                        <asp:RadioButtonList ID="RBLBlocked" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table" runat="server" CssClass="radioBtns" onchange="javascript:CheckMove();"></asp:RadioButtonList><br />
                                        <asp:RequiredFieldValidator runat="server" ID="txt_Validator8" ControlToValidate="RBLBlocked" Display="Dynamic" ErrorMessage=" * This Field is Required" ForeColor="Red" InitialValue="" ValidationGroup="save" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="row-content">
                        <table class="table" border="0">
                            <tr>
                                <td class="label-cell-width-x3">Memo</td>
                                <td>
                                    <textarea id="txtMemo" runat="server" class="form-control" maxlength="500"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <%--<asp:Button runat="server" ID="btnUdtCrdt" Text="Save" CssClass="btn btn-default flt-right" OnClick="btnUdtCrdt_OnClick" />--%>
                                    <asp:Label runat="server" ID="lblAccID" CssClass="hidn"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

            </div>

            <div id="Navegation_Panel">
                <div id="btn_Group_Panel" class="btn-group btn-breadcrumb" runat="server">
                    <a href="../Home/Home.aspx" class="btn btn-default"><i class="glyphicon glyphicon-home"></i></a>
<%--                    <a href="../Account/AccountSearch.aspx" class="btn btn-default">Account Search</a>
                    <asp:LinkButton runat="server" CssClass="btn btn-default" Text="Account View" ID="panel_lnkAccView" OnClick="panel_lnkAccView_OnClick"></asp:LinkButton>
                    <a href="#" class="btn btn-default">Details Info</a>
                    <a href="#" class="btn btn-default">Contact Info</a>
                    <asp:LinkButton runat="server" CssClass="btn btn-default" Text="Credit Info" ID="panel_lnkCrdtInfo"></asp:LinkButton>
                    <a href="#" class="btn btn-default">vehicle Info</a>
                    <a href="../Home/Home.aspx" class="btn btn-default">Close</a>--%>
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
                            <asp:LinkButton runat="server" ID="btnSaveCat1" CssClass="f-x6" Text="<i class='fa fa-floppy-o' aria-hidden='true'></i>" ValidationGroup="addCat1" OnClick="btnSaveCat1_OnClick" ToolTip="Save"></asp:LinkButton>
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
                        <asp:LinkButton runat="server" ID="btnSaveCat2" CssClass="f-x6" Text="<i class='fa fa-floppy-o' aria-hidden='true'></i>" ValidationGroup="addCat2" OnClick="btnSaveCat2_OnClick" ToolTip="Save"></asp:LinkButton>
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

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
