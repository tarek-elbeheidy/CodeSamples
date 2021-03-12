<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomeRegisterUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.Common.WebParts.HomeRegister.HomeRegisterUserControl" %>
<%--<asp:Button ID="btnlang_switcher" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, LangSwitcher %>" OnClientClick="javascript:moehe_lang_switcher_root();" />--%>
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

<div id="mainDiv" runat="server">
    <div id="main-content" class="homePage">
        <div class="row user-form">
            <div class="form-header row text-center margin-bottom-50">
                <h3 class="margin-tb-0">
                    <asp:Literal runat="server" Text="&lt;%$Resources:ITWORX.MOEHEWF.Common,WelcomeMag%&gt;" />
                </h3>
                <img class="form-logo" src="/_catalogs/masterpage/MOEHE/common/img/logo1.png">
                <h2 class="margin-tb-0" id="equArabic">
                    <asp:Literal runat="server" Text="&lt;%$Resources:ITWORX.MOEHEWF.Common,EquivalenceHome%&gt;" />
                </h2>
            </div>

            <div class="form-content row margin-bottom-15 ">
                <div class="pull-left">
                    <%--  <a href="https://sts.edu.gov.qa/adfs/ls?wa=wsignin1.0&wtrealm=urn%3aSharePoint%3aCert&wctx=https%3a%2f%2fcertificate.edu.gov.qa%2fen%2f_layouts%2f15%2fAuthenticate.aspx%3fSource%3d%252Fen%252FPages%252Fdefault%252Easpx" class="btn moe-btn">
                    <asp:Literal runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Login %>" />
                </a>--%>
                    <asp:Button ID="brnLogin" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Login %>" OnClick="brnLogin_Click" />
                </div>
                <div class="pull-right">
                    <%--  <a href="https://sts.edu.gov.qa/adfs/ls?wa=wsignin1.0&wtrealm=urn%3aSharePoint%3aCert&wctx=https%3a%2f%2fcertificate.edu.gov.qa%2fen%2f_layouts%2f15%2fAuthenticate.aspx%3fSource%3d%252Fen%252FPages%252Fdefault%252Easpx&client-request-id=e2c69255-6013-4339-4917-0080030000e7&RedirectToIdentityProvider=AD+AUTHORITY " class="btn moe-btn">
                    <asp:Literal runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, EmployeeLogin %>" />
                </a>--%>
                    <asp:Button ID="btnEmployeeLogin" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, EmployeeLogin %>" OnClick="btnEmployeeLogin_Click" />
                </div>

                <div class="col-md-12 col-xs-12 margin-bottom-15 text-left">

                    <asp:Button ID="btnForgetPassword" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, ForgetPassword %>" CssClass="btn moe-btn newuser lnk_forget_pass" OnClick="btnForgetPassword_Click" />
                </div>

                <div class="col-md-12 col-xs-12 margin-bottom-0 text-left">
                    <%--   <a href="/_layouts/15/ITWORX.MOEHEWF.Common/Register.aspx" class="btn moe-btn clear-btn" id="newUserBtn">
                    <asp:Literal runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, RegisterNewUser %>" />
                </a>--%>
                    <asp:Button ID="newUserBtn" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, RegisterNewUser %>" CssClass="btn moe-btn newuser lnk_forget_pass" OnClick="newUserBtn_Click" />
                </div>
            </div>
        </div>
    </div>
</div>

