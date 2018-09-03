<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="AccountContact.aspx.cs" Inherits="CTA_NEW_PORTAL.Modules.Account.AccountContact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        #ContentPlaceHolder1_AccountUpdatePanel_Contact {
            width: 100%;
            /*border: solid 1px #f00;*/
        }

        .rgDataDiv {
            height: 320px !important;
        }
    </style>

    <asp:UpdatePanel ID="AccountUpdatePanel_Contact" runat="server">
        <ContentTemplate>

            <div id="Sub_Content">

                <div class="subject-row">
                    <h4>Contact &nbsp;<i class="fa fa-caret-right" aria-hidden="true"></i>&nbsp; View</h4>
                    <asp:Panel runat="server" ID="btnsPanel" CssClass="btns_Panel">
                        <asp:LinkButton runat="server" ID="B_11" Text="Inquiry" CssClass="mainBtns" CausesValidation="False" Enabled="False" OnClick="B_11_OnClick" /><%--Inquiry 1--%>
                        <asp:LinkButton runat="server" ID="B_12" Text="View" CssClass="mainBtns" Enabled="False" OnClick="B_12_OnClick" /><%--View 2--%>
                        <asp:LinkButton runat="server" ID="B_13" Text="Print" CssClass="mainBtns" Enabled="False" /><%--Print 3--%>
                        <span style="width: 30px; display: inline-block"></span>
                        <asp:LinkButton runat="server" ID="B_21" Text="New" CssClass="mainBtns" CausesValidation="False" Enabled="False" OnClick="B_21_OnClick" /><%--New 4--%>
                        <asp:LinkButton runat="server" ID="B_22" Text="Update" CssClass="mainBtns" Enabled="False" /><%--Update 5--%>
                        <asp:LinkButton runat="server" ID="B_23" Text="Cancel" CssClass="mainBtns" Enabled="False" /><%--Cancel 6--%>
                        <asp:LinkButton runat="server" ID="B_29" Text="Save" CssClass="mainBtns" ValidationGroup="save" Enabled="False" /><%--Save 7--%>
                        <span style="width: 30px; display: inline-block"></span>
                        <asp:LinkButton runat="server" ID="B_18" Text="Refresh" CssClass="mainBtns" CausesValidation="False" OnClick="B_19_OnClick" /><%--Refresh 8--%>
                        <asp:LinkButton runat="server" ID="B_19" Text="Clear" CssClass="mainBtns" CausesValidation="False" OnClick="B_19_OnClick" /><%--Clear 9--%>

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
                                    <asp:LinkButton runat="server" ID="B_42" Text="Export" Enabled="False" /></li>
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

                                    <telerik:RadGrid Style="outline: none" ID="ContactsGRD" runat="server" AllowPaging="True" AllowSorting="True"
                                        RenderMode="Lightweight" AutoGenerateColumns="false" OnItemCommand="ContactsGRD_OnItemCommand" OnNeedDataSource="ContactsGRD_OnNeedDataSource">
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

                                                <telerik:GridBoundColumn HeaderText="Main Acc Id" UniqueName="MainAccID" DataField="MainAccID" FilterControlWidth="150px" Display="false">
                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn HeaderText="Contact Type" UniqueName="ContactType" DataField="ContactType"
                                                    FilterControlWidth="150px">
                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn HeaderText="Acc Id" UniqueName="AccID" DataField="AccID" FilterControlWidth="150px" Display="false">
                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                </telerik:GridBoundColumn>

                                                <%--<telerik:GridBoundColumn HeaderText="Contact No." UniqueName="AccNo" DataField="AccNo"
                                                    FilterControlWidth="150px">
                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                </telerik:GridBoundColumn>--%>

                                                <telerik:GridBoundColumn HeaderText="Contact Name" UniqueName="AccName" DataField="AccName"
                                                    FilterControlWidth="150px">
                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn HeaderText="Mobile (1)" UniqueName="AccMobile1" DataField="AccMobile1"
                                                    FilterControlWidth="150px">
                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn HeaderText="Mobile (2)" UniqueName="AccMobile1" DataField="AccMobile1"
                                                    FilterControlWidth="150px">
                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                                </telerik:GridBoundColumn>

                                                <telerik:GridTemplateColumn UniqueName="EditContact" HeaderText="View" AllowFiltering="False" Exportable="false"
                                                    FilterControlAltText="Filter EditContact column">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="EditContactBTN" runat="server" CommandName="EditCommand"><i class="fa fa-pencil-square-o" aria-hidden="true" style="font-size: 16px"></i></asp:LinkButton>
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
                                    </telerik:RadGrid>

                                </telerik:RadAjaxPanel>
                            </telerik:RadCodeBlock>
                        </div>

                    </div>
                </div>

            </div>

            <div id="Navegation_Panel">
                <div id="btn_Group_Panel" class="btn-group btn-breadcrumb" runat="server">
                    <a href="../Home/Home.aspx" class="btn btn-default"><i class="glyphicon glyphicon-home"></i></a>

                </div>
            </div>

        </ContentTemplate>

        <Triggers>
            <%-- for export & import --%>
            <asp:PostBackTrigger ControlID="B_41" />
            <asp:PostBackTrigger ControlID="B_42" />
        </Triggers>

    </asp:UpdatePanel>
</asp:Content>
