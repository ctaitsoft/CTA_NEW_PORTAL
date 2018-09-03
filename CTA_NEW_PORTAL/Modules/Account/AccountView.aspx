<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="AccountView.aspx.cs" Inherits="CTA_NEW_PORTAL.Modules.Account.AccountView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        $(document).ready(function () {
            //$("#ifrmWizard").attr("src","AccountViews/CrdtInfo.aspx");
        });
    </script>

    <style>
        .RadPanelBar .rpImage {
            height: 19px;
        }
        .RadPanelBar .rpLevel1 .rpImage {
            height: 16px;
        }
        .RadPanelBar_Bootstrap .rpRootLink .rpExpandHandle {
            padding: 0px !important;
            border-radius: 4px;
        }
    </style>


    <div id="Sub_Content">
        <div class="subject-row">
            <h4>Account View</h4>
        </div>

        <div class="row">
           <%-- Account No. 17555466   Account Name: Ahmad Fayez Alqaruoti--%>
        </div>

        <div class="row">
            <div class="row-content">
                
                   <%-- <iframe src="AccountViews/CrdtInfo.aspx" id="ifrmWizard" width="1000" height="450" frameBorder="0" scrolling="no">

                    </iframe> --%> 

                <telerik:RadPanelBar RenderMode="Lightweight" runat="server" ID="RadPanelBar1" Height="600px" Width="100%" ExpandMode="FullExpandedItem">
                    <Items>
                        <telerik:RadPanelItem Text="Credit Info" ImageUrl="~/Images/PNG/edit.png" Expanded="True">
                            <ContentTemplate>
                                <div id="Crdt_Info" runat="server" class="vertual-row">

                                </div>
                            </ContentTemplate>
                        </telerik:RadPanelItem>

                        <telerik:RadPanelItem Text="Occ 2" ImageUrl="~/Images/PNG/edit.png">
                            <ContentTemplate>
                                <asp:Button runat="server" ID="btntst" Text="btn" />
                            </ContentTemplate>
                        </telerik:RadPanelItem>

                        <telerik:RadPanelItem Text="Occ 3" ImageUrl="~/Images/PNG/edit.png">
                            <ContentTemplate>

                            </ContentTemplate>
                        </telerik:RadPanelItem>

                        <telerik:RadPanelItem Text="Occ 4" ImageUrl="~/Images/PNG/edit.png">
                            <ContentTemplate>

                            </ContentTemplate>
                        </telerik:RadPanelItem>
                    </Items>
                </telerik:RadPanelBar>

            </div>
        </div>

    </div>

</asp:Content>
