<%@ Assembly Name="ITWORX.MOEHEWF.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7b2931724f1d7d1c" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/UserVerification.ascx" TagPrefix="uc1" TagName="UserVerification" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Import Namespace="Microsoft.SharePoint" %>

<%--<%@ Assembly Name="ITWORX.MOEHEWF.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7b2931724f1d7d1c" %>--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ITWORX.MOEHEWF.Common.Layouts.ITWORX.MOEHEWF.Common.Register" MasterPageFile="~/_layouts/15/ITWORX.MOEHEWF.Common/MoeheLogin.master" %>

<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">

    <%--<link rel="stylesheet" href='<%= ResolveUrl ("~/Style%20Library/CSS/jquery-ui.css") %>'>
<script type="text/javascript" src='<%= ResolveUrl ("~/Style%20Library/Scripts/jquery-1.12.4.js") %>'></script>
<script type="text/javascript" src='<%= ResolveUrl ("~/Style%20Library/Scripts/jquery-ui.js") %>'></script>--%>

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
            text-align: center;
        }
    </style>

    <asp:Panel ID="pnlForm" runat="server" CssClass="user-form">
        <div class="form-header row text-center margin-bottom-50">
            <h3 class="margin-tb-0">
                <asp:Literal runat="server" Text="&lt;%$Resources:ITWORX.MOEHEWF.Common,WelcomeMag%&gt;" />
            </h3>
            <img class="form-logo" src="images/logo1.png">
            <h2 class="margin-tb-0">
                <asp:Literal runat="server" Text="&lt;%$Resources:ITWORX.MOEHEWF.Common,RegistrationHeader%&gt;" />
            </h2>
        </div>

        <div class="form-content row margin-top-50">
            <asp:Wizard ID="wizardRegisteration" runat="server" DisplaySideBar="false" CssClass="moe-full-width" OnNextButtonClick="wizardRegisteration_NextButtonClick" OnPreRender="wizardRegisteration_PreRender" OnFinishButtonClick="wizardRegisteration_FinishButtonClick">
                <HeaderTemplate>
                    <div class="row proccess margin-bottom-25" style="display: none;">
                        <ul id="wizHeader" class="moe-full-width no-padding">

                            <asp:Repeater ID="SideBarList" runat="server">
                                <ItemTemplate>
                                    <%--<li><a class="<%# GetClassForWizardStep(Container.DataItem) %>" title="<%#Eval("Name")%>">--%>

                                    <p class="process-des"><%# Eval("Name")%></p>
                                    </a>
                           </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </HeaderTemplate>

                <WizardSteps>
                    <asp:WizardStep ID="wizardStepMOIValidation" runat="server" Title="<%$Resources:ITWORX.MOEHEWF.Common, MOIUserValidation  %>">
                        <div class="row no-padding">

                            <div class="col-md-12 col-xs-12">
                                <h2 class="instruction">
                                    <asp:Label ID="lblInstructions" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Instructions %>"></asp:Label>
                                </h2>
                                <ul class="instruction-list">
                                    <li>
                                        <asp:Label ID="lblPersonalidInstruction" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, QatarIdInstruction %>"></asp:Label>
                                    </li>
                                    <li>
                                        <asp:Label ID="lblemailmobInstruction" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, PhoneNumberEmailMsg %>"></asp:Label>
                                    </li>
                                    <li><%=Resources.ITWORX.MOEHEWF.Common.PasswordInstructions %>
                                        <ul class="instruction-list">
                                            <li>
                                                <asp:Label ID="lblAccountName" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, AccountName  %>"></asp:Label>
                                                <asp:Label ID="lblCharactersLength" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, CharactersLength  %>"></asp:Label>

                                            </li>
                                            <li>
                                                <asp:Label ID="lblEnglishUppercase" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, EnglishUppercase   %>"></asp:Label>
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
                                    </li>
                                </ul>
                            </div>

                        </div>
                        <div class="row">

                            <div class="col-md-6 col-xs-12">
                                <div class="form-group">
                                    <label>
                                        <asp:Label ID="lblQatarId" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, QatarId %>"></asp:Label><span class="error-msg"> *</span>
                                    </label>
                                    <asp:TextBox ID="tbQatarId" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ReqQatarId" Display="dynamic" CssClass="error-msg moe-full-width" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="tbQatarId" Text="<%$Resources:ITWORX.MOEHEWF.Common, RequiredQatarId %>" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="regQatarID" Display="dynamic" CssClass="error-msg moe-full-width" runat="server" ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, RegPersonalID %>" ControlToValidate="tbQatarId" ValidationExpression="^\d{11}$" ValidationGroup="Submit"></asp:RegularExpressionValidator>
                                    <%--<asp:RequiredFieldValidator ID="ReqQatarId" runat="server" ErrorMessage="ReqQatarId"ForeColor="Red" ControlToValidate="tbQatarId" Text="<%$Resources:ITWORX.MOEHEWF.Common, RequiredQatarId %>" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>

                            <div class="col-md-6 col-xs-12">
                                <div class="form-group">
                                    <label>
                                        <asp:Label ID="lblDateOfBirth" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, DateOfBirth %>"></asp:Label><span class="error-msg"> *</span>
                                    </label>
                                    <input type="text" id="dtDateOfBirth" readonly="readonly" runat="server" class="form-control">
                                    <asp:RequiredFieldValidator ID="ReqDateOfBirth" Display="dynamic" CssClass="error-msg moe-full-width" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="dtDateOfBirth" Text="<%$Resources:ITWORX.MOEHEWF.Common, RequiredDateOfBirth %>" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <!--Email-->
                        <div class="row">
                            <div class="col-md-12 col-xs-12">
                                <div class="form-group">
                                    <label>
                                        <asp:Label ID="lblEmail" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, EmailAddress%>"></asp:Label><span class="error-msg"> * </span>
                                    </label>
                                    <asp:TextBox ID="txtEmail" type="email" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="vldEmailRequired" runat="server" Display="dynamic" CssClass="error-msg moe-full-width" ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, RequiredEmail%>" ControlToValidate="txtEmail" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" Display="dynamic" CssClass="error-msg moe-full-width" runat="server" ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, WrongEmailAddress %>" ControlToValidate="txtEmail" ValidationExpression="^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$" ValidationGroup="Submit"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6 col-xs-12">
                                <div class="form-group">
                                    <label>
                                        <asp:Label ID="lblMobileNumber" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, MobileNumber %>"></asp:Label>
                                        <span style="color: red">* </span>
                                    </label>
                                    <asp:TextBox ID="tbMobileNumber" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqMobileNumber" Display="dynamic" runat="server" ErrorMessage="RequiredFieldValidator" CssClass="error-msg moe-full-width" ControlToValidate="tbMobileNumber" Text="<%$Resources:ITWORX.MOEHEWF.Common, RequiredMobileNumber %>" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="dynamic" CssClass="error-msg moe-full-width" runat="server" ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, RegMobileNumber %>" ControlToValidate="tbMobileNumber" ValidationExpression="^\d{8,13}$" ValidationGroup="Submit"></asp:RegularExpressionValidator>

                                </div>
                            </div>



                            <!--NationCategory-->
                            <div class="col-md-6 col-xs-12">
                                <div class="form-group">
                                    <label>
                                        <asp:Label ID="Label1" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, NationalityCategory %>"></asp:Label><span class="error-msg"> * </span>
                                    </label>
                                    <%--<asp:TextBox ID="txtNationalityCategory" runat="server"></asp:TextBox>--%>
                                    <asp:DropDownList ID="drp_NationCategory" runat="server" CssClass="form-control moe-dropdown"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="RequiredFieldValidator" CssClass="error-msg moe-full-width" InitialValue="-1" ControlToValidate="drp_NationCategory" Text="<%$Resources:ITWORX.MOEHEWF.Common, RequiredNationalityCategory %>" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                        </div>

                        <div id="userPassword" class="row">

                            <div class="col-md-12 col-xs-12">
                                <div class="form-group">
                                    <label>
                                        <SharePoint:EncodedLiteral runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, UserPassword %>" EncodeMethod='HtmlEncode' /><span class="error-msg"> *</span>
                                        <%--<asp:Label ID="lblNewPassword" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, NewPassword %>"></asp:Label><span class="error-msg"> *</span>--%>
                                    </label>
                                    <asp:TextBox ID="tbPassword" runat="server" autocomplete="off" class="ms-inputuserfield" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ReqPassword" Display="dynamic" CssClass="error-msg moe-full-width" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="tbPassword" Text="<%$Resources:ITWORX.MOEHEWF.Common, RequiredUserPassword%>" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ID="RegPassword" Display="dynamic" CssClass="error-msg moe-full-width" runat="server" ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, InvalidPassword %>" ControlToValidate="tbPassword" ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{6,9})$"  ValidationGroup="Submit"></asp:RegularExpressionValidator>
                                   <span class="error-msg">
                                    <asp:Literal ID="litInvalidPass" runat="server"  Visible="false" ></asp:Literal>
                                       </span>
                                    <%--<asp:RequiredFieldValidator ID="ReqQatarId" runat="server" ErrorMessage="ReqQatarId"ForeColor="Red" ControlToValidate="tbQatarId" Text="<%$Resources:ITWORX.MOEHEWF.Common, RequiredQatarId %>" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="col-md-12 col-xs-12">
                                <div class="form-group">
                                    <label>
                                        <SharePoint:EncodedLiteral runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, ConfirmNewPassword %>" EncodeMethod='HtmlEncode' /><span class="error-msg"> *</span>
                                        <%-- <asp:Label ID="lblConfirmNewPassword" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, ConfirmNewPassword %>"></asp:Label><span class="error-msg"> *</span>--%>
                                    </label>
                                    <asp:TextBox ID="tbConfirmPassword" runat="server" TextMode="Password" autocomplete="off" class="ms-inputuserfield" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ReqConfirmPassword" Display="dynamic" CssClass="error-msg moe-full-width" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="tbConfirmPassword" Text="<%$Resources:ITWORX.MOEHEWF.Common, RequiredConfirmNewPassword %>" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidatorPassword" runat="server"
                                        ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, ConfirmUserPasswordError%>"
                                        ControlToValidate="tbConfirmPassword" ForeColor="Red"
                                        ControlToCompare="tbPassword"
                                        Display="Dynamic" Type="String" Operator="Equal" Text="<%$Resources:ITWORX.MOEHEWF.Common, ConfirmUserPasswordError%>" ValidationGroup="Submit">
                                    </asp:CompareValidator>

                                    <%--   <asp:RegularExpressionValidator ID="RegConfirmPassword" Display="dynamic" CssClass="error-msg moe-full-width" runat="server" ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, InvalidPassword %>" ControlToValidate="tbConfirmPassword" ValidationExpression="((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{9,100})" ValidationGroup="Submit"></asp:RegularExpressionValidator>--%>
                                    <%--<asp:RequiredFieldValidator ID="ReqQatarId" runat="server" ErrorMessage="ReqQatarId"ForeColor="Red" ControlToValidate="tbQatarId" Text="<%$Resources:ITWORX.MOEHEWF.Common, RequiredQatarId %>" ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <!--Nationality-->

                            <%--  <div class="col-md-12 col-xs-12">
                                <div class="form-group">
                                    <label>
                                        <asp:Label ID="lblNationality" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Nationality %>"></asp:Label><span class="error-msg"> * </span>
                                    </label>
                                    <%--<asp:Label ID="lblVNationality" runat="server" Text=" "></asp:Label>--%>
                            <%--<asp:TextBox ID="lblVNationality" runat="server"></asp:TextBox>--%>
                            <%-- <asp:DropDownList ID="drp_Nationality" runat="server" CssClass="form-control moe-dropdown"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="-1" ErrorMessage="RequiredFieldValidator" CssClass="error-msg moe-full-width" ControlToValidate="drp_Nationality" Text="<%$Resources:ITWORX.MOEHEWF.Common, RequiredNationality %>" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </div>
                            </div>--%>
                        </div>
                        <asp:Panel runat="server" ID="pnlMOIValidation" Visible="false">
                            <div class="row no-padding">
                                <div class="col-md-12 col-xs-12">
                                    <h6>
                                        <asp:Label ID="lblValidationError" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, MOIValidationError %>" CssClass="error-msg moe-full-width wrong-data"></asp:Label>
                                    </h6>
                                </div>

                            </div>
                        </asp:Panel>
        </div>
        <asp:Label ID="lblVerificationStatus" runat="server" Text=""></asp:Label>
        </asp:WizardStep>


                    <asp:WizardStep ID="wizardStepUserVerification" runat="server" Title="<%$Resources:ITWORX.MOEHEWF.Common, SendCode  %>">
                        <div class="row no-padding">
                            <uc1:UserVerification runat="server" id="userVerification" />
                        </div>
                    </asp:WizardStep>


        </WizardSteps>

                <startnavigationtemplate>
                    <div class="row unhighlighted-section no-padding-imp align-items-baseline">
                        <div class="col-md-6 col-xs-12 no-padding text-left">
                            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click"  Text="<%$Resources:ITWORX.MOEHEWF.Common, Cancel %>"  CssClass="submitdata btn moe-btn margin-lr-0" />
                        </div>
                        <div class="col-md-6 col-xs-12 no-padding text-right">
                            <asp:Button ID="StartNextButton" runat="server" CommandName="MoveNext" Text="<%$Resources:ITWORX.MOEHEWF.Common, Next %>" ValidationGroup="Submit" CssClass="submitdata btn moe-btn margin-lr-0" />
                        </div>
                        
                    </div>
                </startnavigationtemplate>

        <stepnavigationtemplate>
                    <div class="row unhighlighted-section no-padding-imp align-items-baseline">
                        <div class="col-md-6 col-xs-12 no-padding text-left">
                        </div>
                        <div class="col-md-6 col-xs-12 no-padding text-right">
                            <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" Text="<%$Resources:ITWORX.MOEHEWF.Common, Next %>" ValidationGroup="Submit" CssClass="btn moe-btn margin-lr-0" />
                        </div>
                    </div>
                </stepnavigationtemplate>

        <finishnavigationtemplate>
                    <div class="row unhighlighted-section no-padding-imp align-items-baseline">
                        <div class="col-md-6 col-xs-12 no-padding text-left">
                        </div>
                        <div class="col-md-6 col-xs-12 no-padding text-right">
                            <asp:Button ID="FinishButton" runat="server" CommandName="MoveComplete" Text="<%$Resources:ITWORX.MOEHEWF.Common, Submit %>" ValidationGroup="Submit" CssClass="finishdata btn moe-btn margin-lr-0" />
                        </div>
                    </div>
                </finishnavigationtemplate>
        </asp:Wizard>
        </div>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="modalPopUpConfirmation" runat="server"
        TargetControlID="btnHdn"
        PopupControlID="pnlConfirmation" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Button ID="btnHdn" runat="server" Text="Button" Style="display: none;" />
    <asp:Panel ID="pnlConfirmation" runat="server" Style="display: none;" CssClass="modalPopup">

        <asp:Label ID="lblSuccess" runat="server" ForeColor="Green" Font-Bold="true" Text="<%$Resources:ITWORX.MOEHEWF.Common, UserAccountCreationMessage%>"></asp:Label>
        <br />
        <br />

        <asp:Button ID="btnLogin" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Ok %>" OnClick="btnLogin_Click" />
        <%-- <div class="row no-padding text-center">
            <asp:HyperLink ID="LoginNavigationHyperLink" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Login %>" class="btn moe-btn" />
        </div>--%>
        <%--<asp:Button ID="btnCancel" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Cancel %>" />--%>
    </asp:Panel>
    <asp:Panel ID="pnlSuccess" runat="server" Visible="false" CssClass="user-form">

        <div class="form-header row text-center margin-bottom-50">
            <h3 class="margin-tb-0">
                <asp:Literal runat="server" Text="&lt;%$Resources:ITWORX.MOEHEWF.Common,WelcomeMag%&gt;" />
            </h3>
            <img class="form-logo" src="images/logo1.png">
            <h2 class="margin-tb-0">
                <asp:Literal runat="server" Text="&lt;%$Resources:ITWORX.MOEHEWF.Common,RegistrationHeader%&gt;" />
            </h2>
        </div>

        <div class="row heighlighted-section ">
            <h5 class="font-size-20 text-center ">
                <asp:Label runat="server" ForeColor="Green" Font-Bold="true" Text="<%$Resources:ITWORX.MOEHEWF.Common, UserAccountCreationMessage%>"></asp:Label>
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
                    :
                </h6>
                <h5 class="font-size-20 font-weight-600 display-inline-block">
                    <asp:Label runat="server" ID="lblCreatedUserName"></asp:Label>
                </h5>
            </div>

            <%--<div class="col-md-12 col-xs-12 text-left">
                <h6 class="font-size-22 font-weight-600 font-bold display-inline-block moe-color margin-lr-10">
                    <asp:Label runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Password%>"></asp:Label>
                    :
                </h6>
                <h5 class="font-size-20 font-weight-600 display-inline-block">
                    <asp:Label runat="server" ID="lblCreatedUserPassword"></asp:Label>
                </h5>
            </div>--%>
        </div>




        <div class="row no-padding text-center">
            <asp:HyperLink ID="lnkNavigateToLogin" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Login %>" class="btn moe-btn" />
        </div>
        <%--<asp:Button ID="btnCancel" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Cancel %>" />--%>
    </asp:Panel>
    <script type="text/javascript">
        $(document).ready(function () {

            $('#<%=dtDateOfBirth.ClientID %>').datepicker({
                dateFormat: "dd/mm/yy",
                showOn: 'focus',
                showButtonPanel: false,
                maxDate: -1,
                changeYear: true,
                changeMonth: true,
                yearRange: 'c-100:nn'
            });

        });
    </script>
</asp:Content>
