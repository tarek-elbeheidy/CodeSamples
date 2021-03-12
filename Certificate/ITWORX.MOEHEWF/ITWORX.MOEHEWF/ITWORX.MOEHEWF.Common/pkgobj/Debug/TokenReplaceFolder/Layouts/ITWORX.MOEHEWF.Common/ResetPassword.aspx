<%@ Assembly Name="ITWORX.MOEHEWF.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7b2931724f1d7d1c" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="ITWORX.MOEHEWF.Common.Layouts.ITWORX.MOEHEWF.Common.ResetPassword" %>
 
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="SslWarning" style="color: red; display: none">
        <SharePoint:EncodedLiteral runat="server" EncodeMethod="HtmlEncode" ID="ClaimsFormsPageMessage" />
    </div>
    <script language="javascript">
        if (document.location.protocol != 'https:') {
            var SslWarning = document.getElementById('SslWarning');
            SslWarning.style.display = '';
        }
    </script>
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #fff;
            border: 3px solid #ccc;
            padding: 10px;
            width: 300px;
        }
        #main-link.homePage{
            height: 90vh!important;
            
        }
    </style>
    <div id="main-link" class="homePage">
    <asp:Panel ID="pnlForm" runat="server" CssClass="user-form">
        <div class="form-header row text-center margin-bottom-50">
            <h3 class="margin-tb-0">
                <asp:Literal runat="server" Text="&lt;%$Resources:ITWORX.MOEHEWF.Common,WelcomeMag%&gt;" />
            </h3>
            <img class="form-logo" src="images/logo1.png">
            <h2 class="margin-tb-0">
                <asp:Literal runat="server" Text="&lt;%$Resources:ITWORX.MOEHEWF.Common,ChangePassword%&gt;" />
            </h2>
        </div>
        <div class="col-md-12 col-xs-12">
            <h2 class="instruction">
                <asp:Label ID="lblInstructions" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Instructions %>"></asp:Label>
            </h2>
            <ul class="instruction-list">
                <li>
                    <asp:Label ID="lblAccountName" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, AccountName  %>"></asp:Label>
                </li>
                <li>
                    <asp:Label ID="lblCharactersLength" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, CharactersLength  %>"></asp:Label>
                </li>
                 <li>
                    <asp:Label ID="lblEnglishUppercase" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, EnglishUppercase   %>"></asp:Label>
                </li>
                 <li>
                    <asp:Label ID="lblEnglishLowercase" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, EnglishLowercase   %>"></asp:Label>
                </li>
               
                <li>
                    <asp:Label ID="lblDigits" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Digits   %>"></asp:Label>
                </li>
                 <li>
                    <asp:Label ID="lblNonAlphabetic" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, NonAlphabetic   %>"></asp:Label>
                </li>
                   <li>
                    <asp:Label ID="lblPasswordsMustNotEasilyGuessed" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, PasswordsMustNotEasilyGuessed    %>"></asp:Label>
                </li>
            </ul>
        </div>
        <div class="col-md-12 col-xs-12">
            <div class="form-group">
                <label class="row">
                    <asp:Label ID="lblQatarId" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, QatarId %>"></asp:Label><span class="error-msg"> *</span>
                </label>
                <asp:TextBox ID="tbQatarId" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ReqQatarId" Display="dynamic" CssClass="error-msg moe-full-width" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="tbQatarId" Text="<%$Resources:ITWORX.MOEHEWF.Common, RequiredQatarId %>" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regQatarID" Display="dynamic" CssClass="error-msg moe-full-width" runat="server" ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, RegPersonalID %>" ControlToValidate="tbQatarId" ValidationExpression="^\d{11}$" ValidationGroup="Submit"></asp:RegularExpressionValidator>
                <%--<asp:RequiredFieldValidator ID="ReqQatarId" runat="server" ErrorMessage="ReqQatarId"ForeColor="Red" ControlToValidate="tbQatarId" Text="<%$Resources:ITWORX.MOEHEWF.Common, RequiredQatarId %>" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
            </div>
        </div>

        <div class="col-md-12 col-xs-12">
            <div class="form-group">
                <label class="row">
                    <SharePoint:EncodedLiteral runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, OldPassword %>" EncodeMethod='HtmlEncode' /><span class="error-msg"> *</span>
                    <%-- <asp:Label ID="lblOldPassword" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, OldPassword %>"></asp:Label><span class="error-msg"> *</span>--%>
                </label>
                <asp:TextBox ID="tbOldPassword" TextMode="Password" autocomplete="off" runat="server" class="ms-inputuserfield" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ReqOldPassword" Display="dynamic" CssClass="error-msg moe-full-width" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="tbOldPassword" Text="<%$Resources:ITWORX.MOEHEWF.Common, RequiredOldPassword %>" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regOldPassword" Display="dynamic" CssClass="error-msg moe-full-width" runat="server" ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, RegOldPassword %>" ControlToValidate="tbOldPassword" ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{6,9})$" ValidationGroup="Submit"></asp:RegularExpressionValidator>
                <%--<asp:RequiredFieldValidator ID="ReqQatarId" runat="server" ErrorMessage="ReqQatarId"ForeColor="Red" ControlToValidate="tbQatarId" Text="<%$Resources:ITWORX.MOEHEWF.Common, RequiredQatarId %>" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
            </div>
        </div>
        <div class="col-md-12 col-xs-12">
            <div class="form-group">
                <label class="row">
                    <SharePoint:EncodedLiteral runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, NewPassword %>" EncodeMethod='HtmlEncode' /><span class="error-msg"> *</span>
                    <%--<asp:Label ID="lblNewPassword" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, NewPassword %>"></asp:Label><span class="error-msg"> *</span>--%>
                </label>
                <asp:TextBox ID="tbNewPassword" runat="server" TextMode="Password" autocomplete="off" class="ms-inputuserfield" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ReqNewPassword" Display="dynamic" CssClass="error-msg moe-full-width" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="tbNewPassword" Text="<%$Resources:ITWORX.MOEHEWF.Common, RequiredNewPassword%>" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                <asp:RegularExpressionValidator ID="RegNewPassword" Display="dynamic" CssClass="error-msg moe-full-width" runat="server" ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, RegNewPassword %>" ControlToValidate="tbNewPassword" ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{6,9})$" ValidationGroup="Submit"></asp:RegularExpressionValidator>
                <%--<asp:RequiredFieldValidator ID="ReqQatarId" runat="server" ErrorMessage="ReqQatarId"ForeColor="Red" ControlToValidate="tbQatarId" Text="<%$Resources:ITWORX.MOEHEWF.Common, RequiredQatarId %>" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
            </div>
        </div>
        <div class="col-md-12 col-xs-12">
            <div class="form-group">
                <label class="row">
                    <SharePoint:EncodedLiteral runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, ConfirmNewPassword %>" EncodeMethod='HtmlEncode' /><span class="error-msg"> *</span>
                    <%-- <asp:Label ID="lblConfirmNewPassword" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, ConfirmNewPassword %>"></asp:Label><span class="error-msg"> *</span>--%>
                </label>
                <asp:TextBox ID="tbConfirmNewPassword" runat="server" TextMode="Password" autocomplete="off" class="ms-inputuserfield" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ReqConfirmNewPassword" Display="dynamic" CssClass="error-msg moe-full-width" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="tbConfirmNewPassword" Text="<%$Resources:ITWORX.MOEHEWF.Common, RequiredConfirmNewPassword %>" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidatorPassword" runat="server"
                    ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, ConfirmPasswordError%>"
                    ControlToValidate="tbConfirmNewPassword" ForeColor="Red"
                    ControlToCompare="tbNewPassword"
                    Display="Dynamic" Type="String" Operator="Equal" Text="<%$Resources:ITWORX.MOEHEWF.Common, ConfirmPasswordError%>" ValidationGroup="Submit">
                </asp:CompareValidator>

                <asp:RegularExpressionValidator ID="RegConfirmNewPassword" Display="dynamic" CssClass="error-msg moe-full-width" runat="server" ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, RegConfirmNewPassword %>" ControlToValidate="tbConfirmNewPassword" ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{6,9})$" ValidationGroup="Submit"></asp:RegularExpressionValidator>
                <%--<asp:RequiredFieldValidator ID="ReqQatarId" runat="server" ErrorMessage="ReqQatarId"ForeColor="Red" ControlToValidate="tbQatarId" Text="<%$Resources:ITWORX.MOEHEWF.Common, RequiredQatarId %>" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
            </div>
        </div>
        <div class="col-md-12 col-xs-12">
            <div class="form-group">
                <label class="row">
                    <asp:Label ID="lblMessage" runat="server">
                    </asp:Label>
                </label>
            </div>
        </div>
        <div class="row unhighlighted-section no-padding-imp align-items-baseline">
            <div class="col-md-6 col-xs-12 no-padding text-left">
            </div>
            <div class="col-md-6 col-xs-12 no-padding text-right">
                <asp:Button ID="BtnChangePassword" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, ChangePassword %>" ValidationGroup="Submit" CssClass="btn moe-btn margin-lr-0" OnClick="BtnChangePassword_Click" />
            </div>
        </div>
    </asp:Panel>
        </div>
    <cc1:ModalPopupExtender ID="modalPopUpConfirmation" runat="server"
        TargetControlID="btnHdn"
        PopupControlID="pnlConfirmation" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Button ID="btnHdn" runat="server" Text="Button" Style="display: none;" />
    <asp:Panel ID="pnlConfirmation" runat="server" Style="display: none;" CssClass="modalPopup">

        <asp:Label ID="lblSuccess" runat="server" ForeColor="Green" Font-Bold="true" Text="<%$Resources:ITWORX.MOEHEWF.Common, NewPasswordSent%>"></asp:Label>
        <br />
        <br />

        <asp:Button ID="btnLogin" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Ok %>" OnClick="btnLogin_Click" />
        <%--        <asp:Button ID="btnCancel" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Cancel %>" OnClick="btnCancel_Click" />--%>
    </asp:Panel>
    <asp:Panel ID="pnlSuccess" runat="server" Visible="false" CssClass="user-form">
        <div class="row heighlighted-section ">
            <h5 class="font-size-20 text-center ">
                <asp:Label runat="server" ForeColor="Green" Font-Bold="true" Text="<%$Resources:ITWORX.MOEHEWF.Common, NewPasswordSent%>"></asp:Label>
            </h5>
        </div>
        <div class="row unheighlighted-section margin-bottom-25 margin-top-25">
            <h5 class="font-size-20 text-center ">
                <asp:Label runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, LoginInstructions%>"></asp:Label>
            </h5>
        </div>
        <div class="row no-padding margin-top-25 margin-bottom-25 login-msg">
            <div class="col-md-12 col-xs-12 text-left">
                <h6 class="font-size-22 font-weight-600 font-bold display-inline-block moe-color margin-lr-10">
                    <asp:Label runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, UserName%>"></asp:Label>
                </h6>
                <h5 class="font-size-20 font-weight-600 display-inline-block">
                    <asp:Label runat="server" ID="lblCreatedUserName"></asp:Label>
                </h5>
            </div>
            <div class="row no-padding text-center">
                <asp:HyperLink ID="lnkNavigateToLogin" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Login %>" class="btn moe-btn" />
                <asp:Button ID="BtnCancel" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Cancel %>" OnClick="BtnCancel_Click" />
            </div>
        </div>
    </asp:Panel>
</asp:Content>
