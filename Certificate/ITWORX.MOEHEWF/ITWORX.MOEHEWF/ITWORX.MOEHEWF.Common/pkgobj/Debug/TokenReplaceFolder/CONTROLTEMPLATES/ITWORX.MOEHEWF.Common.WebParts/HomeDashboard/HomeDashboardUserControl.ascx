<%@ Assembly Name="ITWORX.MOEHEWF.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7b2931724f1d7d1c" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomeDashboardUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.Common.WebParts.HomeDashboard.HomeDashboardUserControl" %>
<script>

    if (!$('.ms-webpart-zone').parent().hasClass("tableCol-75")) {
        $('.tableCol-75').hide();
    }
    else {
        $('.tableCol-25').hide();
    }
    $('.main-menu').hide();
    $('nav').addClass("homeNav");
    $('.access-icons').hide();
    $('.search').hide();
</script>
<style>
    .navbar-header, .collapse.navbar-collapse, .access-icons, .search, li.active, .main-menu, .welcome-content, header:after, li.user-name, .access-menu > li:last-child {
        display: none;
    }
    /*.navbar-header,.collapse.navbar-collapse,.access-icons, .search, li.active, .main-menu, .welcome-content
	{
		display:none;
	}*/
    .cell-margin, .ms-webpartzone-cell {
        margin: 0;
    }

    .welcome.blank-wp {
        padding: 0;
    }

    .lnk_forget_pass {
        border: none !important;
        text-decoration: underline;
        font-size: 1em !important;
        margin: 0 !important;
        padding: 0 !important;
        background-color: #fff !important;
        color: #a40136 !important;
    }

        .lnk_forget_pass:hover {
            background-color: #fff !important;
            color: #a40136 !important;
            text-decoration: underline;
        }
</style>

<div id="divLinks" runat="server">
    <div id="main-link" class="homePage">
        <div class="row user-form">
            <div class="form-header row text-center margin-bottom-50">
                <h3 class="margin-tb-0">
                    <asp:Literal runat="server" Text="&lt;%$Resources:ITWORX.MOEHEWF.Common,WelcomeMag%&gt;" />
                </h3>
                <img class="form-logo" src="/_catalogs/masterpage/MOEHE/common/img/logo1.png">
                <h2 class="margin-tb-0">
                    <asp:Literal runat="server" Text="&lt;%$Resources:ITWORX.MOEHEWF.Common,EquivalenceHome%&gt;" />
                </h2>
            </div>

            <div class="form-content row margin-bottom-15 ">
                <%-- <div class="col-xs-12 text-center margin-bottom-10">
                    <asp:HyperLink ID="hprLnkEquivalence" runat="server" CssClass="moe-btn btn"><asp:Literal runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Equivalence %>" /></asp:HyperLink>
                </div>
                <div class="col-xs-12 text-center margin-bottom-10">
                    <asp:HyperLink ID="hprLnkPriorApproval" runat="server" CssClass="moe-btn btn"><asp:Literal runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, PriorApproval %>" /></asp:HyperLink>
                </div>--%>
                <asp:Repeater runat="server" ID="rptDashBoard" OnItemDataBound="rptDashBoard_ItemDataBound">
                    <ItemTemplate>
                        <asp:HiddenField ID="hdnSPGroup" runat="server" Value='<%#Eval("SPGroup") %>' />
                        <asp:HiddenField ID="hdn_ID" runat="server" Value='<%#  Eval("ID")%>'></asp:HiddenField>
                        <div class="col-xs-12 text-center margin-bottom-10" id="divLink" runat="server" visible="false">
                            <asp:LinkButton ID="hylnk_links" CssClass="moe-btn btn" runat="server"  Text='<%# Eval("Title") %>' OnClick="hylnk_links_Click">   </asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</div>
