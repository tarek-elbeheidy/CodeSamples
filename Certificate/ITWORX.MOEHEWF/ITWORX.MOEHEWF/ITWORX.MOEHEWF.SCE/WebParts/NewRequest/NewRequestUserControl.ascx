<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/TermsAndConditions.ascx" TagPrefix="uc1" TagName="TermsAndConditions" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/ApplicantDetails.ascx" TagPrefix="uc1" TagName="ApplicantDetails" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/DDLWithTXTWithNoPostback.ascx" TagPrefix="uc1" TagName="DDLWithTXTWithNoPostback" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.SCE/DisplayRequestDetails.ascx" TagPrefix="uc1" TagName="DisplayRequestDetails" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/MOEHE_TC.ascx" TagPrefix="uc1" TagName="MOEHE_TC" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/ClientSideFileUpload.ascx" TagPrefix="uc1" TagName="ClientSideFileUpload" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewRequestUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.WebParts.NewRequest.NewRequestUserControl" %>

<asp:HiddenField ID="OLevel_HF" runat="server" />
<asp:HiddenField ID="ALevel_HF" runat="server" />
<asp:HiddenField ID="IBList_HF" runat="server" />
<asp:HiddenField ID="MOIAddress_hdf" ClientIDMode="Static" runat="server" />
<asp:HiddenField ID="stdNationality_hf" ClientIDMode="Static" runat="server" />
<asp:HiddenField ID="stdGender_hf" ClientIDMode="Static" runat="server" />
<asp:HiddenField ID="hdnBirthDate" ClientIDMode="Static" runat="server" />
<asp:HiddenField ID="hdnStudentName" ClientIDMode="Static" runat="server" />
<asp:HiddenField ID="hdnRequestId" ClientIDMode="Static" runat="server" />

<style>
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
    .moe-file-name .form-group{
        margin-top:-10px !important;
        padding-top:0 !important;
    }
    .moe-file-name{
        padding-bottom:10px;
    }
    .moe-file-name .moe-2nd-label{
        margin-top: 10px;
    }
    .moe-file-upload .moe-file-btn{
        margin-top:8px;
    }
</style>

<div id="requestHeaderDiv" runat="server" visible="false" class="school-applicant  dark-bg margin-top-10">
    <div class="row">
        <div class="col-md-4 col-sm-6 col-xs-12">
            <div class="form-group">
                <label>
                    <asp:Label ID="lbl_requestNum" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequestNumber %>"></asp:Label>
                </label>
                <asp:TextBox ID="txt_requestNum" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-4 col-sm-6 col-xs-12">
            <div class="form-group">
                <label>
                    <asp:Label ID="lbl_creationDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequestCreationDate %>"></asp:Label>
                </label>
                <asp:TextBox ID="txt_creationDate" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-4 col-sm-6 col-xs-12">
            <div class="form-group">
                <label>
                    <asp:Label ID="lbl_submitionDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequestSendDate %>"></asp:Label></label>
                <asp:TextBox ID="txt_submitionDate" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    </div>
</div>

<div id="divSaveButton" runat="server" class="school-saveBtn" style="" visible="false">
    <div class="row">
        <div class="col-md-9 col-xs-9">
            <h4>
                <asp:Literal ID="ltrlSaveText" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SaveText %>"></asp:Literal>
            </h4>
        </div>
        <div class="col-md-3 cpl-xs-3 text-right">
            <asp:Button ID="btnSaveRequest" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Save %>" OnClick="btnSaveRequest_Click" />
        </div>
    </div>
</div>

<!-- submit popup -->
<cc1:ModalPopupExtender ID="modalPopUpConfirmation" runat="server"
    TargetControlID="btnHdn"
    PopupControlID="pnlConfirmation" BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlConfirmation" runat="server" Style="display: none;" CssClass="modalPopup">
    <asp:Label ID="lblSuccess" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>
    <br />
    <br />
    <asp:Button ID="btnModalOK" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Ok %>" OnClick="btnModalOK_Click" />
</asp:Panel>
<asp:Button ID="btnHdn" runat="server" Text="Button" Style="display: none;" />

<!-- save popup -->
<cc1:ModalPopupExtender ID="modalSavePopup" runat="server"
    TargetControlID="btnSaveHdn"
    PopupControlID="pnlSaveConfirmation" BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlSaveConfirmation" runat="server" Style="display: none;" CssClass="modalPopup">
    <asp:Label ID="lblSaveSuccess" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>
    <br />
    <br />
    <asp:Button ID="btnSaveOk" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Ok %>" OnClick="btnSaveOk_Click" />
</asp:Panel>
<asp:Button ID="btnSaveHdn" runat="server" Text="Button" Style="display: none;" />

<div class="row margin-top-25 titleAndBtn schooling-request-title">
    <div class="col-md-9 col-xs-7">
        <h1 class="section-title font-weight-400 no-margin margin-top-0 margin-bottom-0-imp">
            <asp:Literal ID="ltrlEquivalenceTitle" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SchoolEquivalency %>"></asp:Literal>
        </h1>
    </div>
    <div class="col-md-3 col-xs-5 no-padding text-right">
        <asp:Button ID="btn_goToMenu" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, goToMenu %>" OnClick="btn_goToMenu_Click" UseSubmitBehavior="false"/>
    </div>
</div>

<asp:Wizard ID="NewRequest_Wizard" runat="server" ActiveStepIndex="0" CssClass="wizardTable" OnNextButtonClick="wizardNewRequest_NextButtonClick" OnPreviousButtonClick="wizardNewRequest_PreviousButtonClick" OnFinishButtonClick="NewRequest_Wizard_FinishButtonClick" DisplaySideBar="false" OnPreRender="wizardNewRequest_PreRender">
    <HeaderTemplate>
        <div class="schooling-proccess">
            <div class="steps">
                <ul id="wizHeader">
                    <asp:Repeater ID="SideBarList" runat="server">
                        <ItemTemplate>
                            <li>
                                <a class="<%# GetClassForWizardStep(Container.DataItem) %>" title="<%#Eval("Name")%>">
                                    <div class="title">
                                        <span class="step-icon">
                                            <i class="fa fa-book" aria-hidden="true"></i>
                                        </span>
                                        <span class="step-text"><%# Eval("Name")%></span>
                                    </div>
                                </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
        <div class="warningMsgWrap" id="fileNote">
            <div class="row margin-top-15 margin-bottom-15 warningMsg" style="">
                <h5 class="font-size-18 margin-bottom-0 margin-top-0 instruction-details color-black font-family-sans">
                    <%--<asp:Literal ID="Literal1" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, TermsAttachmentNote %>"></asp:Literal>--%>
                    <asp:Literal ID="litTermsAttachNote" runat="server"></asp:Literal>

                    
                </h5>
            </div>
            <div class="row margin-top-15 margin-bottom-15 warningMsg finalMsg" style="display: none;">
                <h5 class="font-size-18 margin-bottom-0 margin-top-0 instruction-details color-black font-family-sans">
                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, TermsDataNote %>"></asp:Literal>
                </h5>
            </div>
        </div>
    </HeaderTemplate>

    <%--Steps Start--%>
    <WizardSteps>

        <%--First Step (Terms and Conditions)--%>
        <asp:WizardStep ID="terms_Stp" runat="server" Title="<%$Resources:ITWORX_MOEHEWF_SCE, TermsConditions %>">
            <div class="termsAndCondition stepOne">
                <uc1:MOEHE_TC runat="server" id="MOEHE_TC" />
            </div>
        </asp:WizardStep>

        <%--Second Step (Contact Information)--%>
        <asp:WizardStep ID="ContactData_Stp" runat="server" Title="<%$Resources:ITWORX_MOEHEWF_SCE, ContactsData %>">
            <div class="school-collapse stepTwo margin-top-10">
                <div class="row">
                    <div class="accordion panel-group" id="accordion">
                        <div class="panel panel-default">
                            <div class="panel-heading active">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne">
                                        <asp:Label ID="lblContactsData" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ContactsData %>"></asp:Label><em></em>
                                    </a>
                                </h4>
                            </div>
                            <%-- /.panel-heading --%>
                            <div id="collapseOne" class="panel-collapse collapse in">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lblApplicantName" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, NameOfficialRecords %>"></asp:Label>
                                                    <span class="error-msg">*</span>
                                                </label>
                                                <asp:TextBox ID="txtApplicantName" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="txtApplicantName" ValidationGroup="Submit" runat="server" CssClass="error-msg" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, NameOfficialRecordsValidation %>"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div>
                                                </div>
                                                <label class="block-display">
                                                    <asp:Label ID="lblMobileNumber" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, MobileNumber %>"></asp:Label>
                                                    <span class="error-msg">*</span>
                                                </label>
                                                <asp:TextBox ID="txtMobileNumber" runat="server" ClientIDMode="Static" CssClass="form-control" placeholder="00974xxx" Style="width: 59%; display: inline;float: right;" ></asp:TextBox>
                                                <input type="text" value="00974" id="txtMobileCode" class="form-control" style="width: 40%; float: left;" disabled="disabled">
                                                <asp:RequiredFieldValidator ControlToValidate="txtMobileNumber" ValidationGroup="Submit" runat="server" CssClass="error-msg" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, MobileNumberValidation %>"></asp:RequiredFieldValidator>
                                                <div>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, WrongMobileNumber %>" CssClass="error-msg" ValidationGroup="Submit" ValidationExpression="^[0-9]{8}$" runat="server" ControlToValidate="txtMobileNumber"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lblEmail" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Email %>"></asp:Label>
                                                    <span class="error-msg">*</span>
                                                </label>
                                                <asp:TextBox ID="txtEmail" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="txtEmail" ValidationGroup="Submit" runat="server" CssClass="error-msg" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, EmailValidation %>"></asp:RequiredFieldValidator>
                                                <div>
                                                    <asp:RegularExpressionValidator ID="regEmailValidator" ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, WrongEmailAddress %>" CssClass="error-msg" ValidationGroup="Submit" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" runat="server" ControlToValidate="txtEmail"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:WizardStep>

        <%--Third Step (Applicant Details)--%>
        <asp:WizardStep ID="student_Stp" runat="server" Title="<%$Resources:ITWORX_MOEHEWF_SCE, StudentData %>">
            <div class="school-collapse stepThree margin-top-10">
                <div class="row">
                    <div class="accordion panel-group" id="accordion">
                        <div class="panel panel-default">
                            <div class="panel-heading active">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseFour">
                                        <asp:Label ID="lblStudentData" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentData %>"></asp:Label><em></em>
                                    </a>
                                </h4>
                            </div>
                            <%-- /.panel-heading --%>
                            <div id="collapseFour" class="panel-collapse collapse in">
                                <div class="panel-body">
                                    <div class="row">
                                        <div id="qatariContainer">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>
                                                        <asp:Label ID="lbl_QatarID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, QatarID %>"></asp:Label><span class="error-msg">*</span>
                                                    </label>
                                                    <asp:TextBox ID="txt_QatarID" runat="server" ClientIDMode="Static"   CssClass="form-control"></asp:TextBox>
                                                    <%--<asp:Label ID="lbl_QatarIDValidat" runat="server" ClientIDMode="Static" ForeColor="Red"></asp:Label>--%>
                                                    <asp:CustomValidator ID="QatarIDValidator" runat="server" CssClass="error-msg"  OnServerValidate="QatarIDValidator_ServerValidate" Display="Dynamic" ClientValidationFunction="validateQatarID" ValidationGroup="Submit" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, QatarIDValidation %>"></asp:CustomValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="passportContainer">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>
                                                        <asp:Label ID="lbl_PassPort" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Passport %>" Visible="false"></asp:Label><span class="error-msg">*</span>
                                                    </label>
                                                    <asp:TextBox ID="txt_PassPort" ClientIDMode="Static" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="valPassport" Visible="false" Enabled="false" ControlToValidate="txt_PassPort" ValidationGroup="Submit" runat="server" CssClass="error-msg" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, PassportValidation %>"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>
                                                        <asp:Label ID="lblTempQatarID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, TempQatarID %>" Visible="false"></asp:Label>
                                                    </label>
                                                    <asp:TextBox ID="txtTempQatarID" runat="server" ClientIDMode="Static" Visible="false" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
										                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lbl_birthDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, BirthDate %>"></asp:Label><span class="error-msg">*</span>
                                                </label>
                                                <asp:TextBox ID="txt_birthDate" ClientIDMode="Static" runat="server" CssClass="form-control" Enabled="true" autocomplete="off"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="valBirthDate" Visible="true" Enabled="true" ControlToValidate="txt_birthDate" ValidationGroup="Submit" runat="server" CssClass="error-msg" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, BirthdateValidation %>"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                       <div class="  passportContainer">
                                            <div class="clearfix"></div>
                                        </div>
										 <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lbl_Name" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentName %>"></asp:Label><span class="error-msg">*</span>
                                                </label>
                                                <asp:TextBox ID="txt_Name" ClientIDMode="Static" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="valName" Visible="false" Enabled="false" ControlToValidate="txt_Name" ValidationGroup="Submit" runat="server" CssClass="error-msg" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, NameValidation %>"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div id="" class=" qatariContainer">
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lbl_Nationality" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentNationality %>"></asp:Label><span class="error-msg">*</span>
                                                </label>
                                                <asp:DropDownList ID="ddl_Nationality" runat="server" CssClass="form-control" Enabled="false" ClientIDMode="Static"></asp:DropDownList>
                                                <asp:Label ID="lblNationalityValidation" runat="server" ClientIDMode="Static" CssClass="error-msg"></asp:Label>
                                                <%--<asp:RequiredFieldValidator ControlToValidate="ddl_Nationality" runat="server" ValidationGroup="Submit" InitialValue="-1" CssClass="error-msg"></asp:RequiredFieldValidator>--%>
                                                <asp:CustomValidator ID="NationalityValidator"  runat="server" CssClass="error-msg" ClientValidationFunction="validateNationality" ValidationGroup="Submit" ErrorMessage=""></asp:CustomValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lbl_Gender" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentGender %>"></asp:Label><span class="error-msg">*</span>
                                                </label>
                                                <asp:DropDownList ID="ddl_Gender" runat="server" CssClass="form-control" Enabled="false" ClientIDMode="Static"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="valGender" Visible="false" Enabled="false" ControlToValidate="ddl_Gender" ValidationGroup="Submit" runat="server" CssClass="error-msg" InitialValue="-1" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, GenderValidation %>"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div id="" class=" passportContainer">
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lbl_NationalityCat" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentNationalityType %>"></asp:Label><span class="error-msg">*</span>
                                                </label>
                                                <asp:DropDownList ID="ddl_NatCat" runat="server" CssClass="form-control moe-dropdown"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="NationalityCatValidator" runat="server" ControlToValidate="ddl_NatCat" InitialValue="-1" CssClass="error-msg" ValidationGroup="Submit" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, NationalCatValidation %>"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div id="" class=" qatariContainer">
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lbl_PrintedName" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentNameCert %>"></asp:Label><span class="error-msg">*</span>
                                                </label>
                                                <asp:TextBox ID="txt_PrintedName" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="PrintedNameValidator" runat="server" ControlToValidate="txt_PrintedName" ValidationGroup="Submit" CssClass="error-msg" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, PrintedNameValidation %>"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:WizardStep>

        <%--Fourth Step (Certificate Details)--%>
        <asp:WizardStep ID="certificate_Stp" runat="server" Title="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateDetails %>">
            <div class="school-collapse stepFour margin-top-10">
                <div class="row">
                    <div class="accordion panel-group" id="accordion">
                        <div class="panel panel-default">
                            <div class="panel-heading active">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseFive">
                                        <asp:Label ID="lblCertificateDetails" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateDetails %>"></asp:Label><em></em>
                                    </a>
                                </h4>
                            </div>
                            <%-- /.panel-heading --%>
                            <div id="collapseFive" class="panel-collapse collapse in">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <uc1:DDLWithTXTWithNoPostback runat="server" id="certificateResource" />
                                        </div>
                                        <div class="col-md-4">
                                            <uc1:DDLWithTXTWithNoPostback runat="server" id="schooleType" />
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lbl_PrevSchool" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, PreviousSchool %>"></asp:Label>
                                                </label>
                                                <asp:TextBox ID="txt_PrevSchool" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row margin-top-15">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <%--<label>
                                                    <asp:Label ID="lblSchoolingSystem" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SchoolingSystem %>"></asp:Label>
                                                </label>--%>
                                                <%--<asp:DropDownList ID="ddlSchoolingSystem" runat="server" CssClass="form-control moe-dropdown"></asp:DropDownList>--%>

                                                <uc1:DDLWithTXTWithNoPostback runat="server" id="ddlSchoolingSystem" />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lbl_ScholasticLevel" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, LastSchoolYear %>"></asp:Label><span class="error-msg">*</span>
                                                </label>
                                                <asp:DropDownList ID="ddl_ScholasticLevel" runat="server" CssClass="form-control moe-dropdown"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddl_ScholasticLevel" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, LastSchoolYearValidation %>" ValidationGroup="Submit" InitialValue="-1" runat="server" class="error-msg"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lbl_LastAcademicYear" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, AcademicYear %>"></asp:Label>
                                                    <span class="error-msg">*</span>
                                                </label>
                                                <asp:DropDownList ID="ddl_LastAcademicYear" runat="server" CssClass="form-control moe-dropdown"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddl_LastAcademicYear" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, AcademicYearValidation %>" ValidationGroup="Submit" InitialValue="-1" runat="server" class="error-msg"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row margin-top-15">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lblEquiPurpose" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, EquivalencyPurpose %>"></asp:Label><span class="error-msg">*</span>
                                                </label>
                                                <asp:DropDownList ID="ddlEquiPurpose" runat="server" CssClass="form-control moe-dropdown"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddlEquiPurpose" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, EquivalencyPurposeValidation %>" ValidationGroup="Submit" InitialValue="-1" runat="server" class="error-msg"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lbl_GoingToClass" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, GoToClass %>"></asp:Label><span class="error-msg">*</span>
                                                </label>
                                                <asp:DropDownList ID="ddl_GoingToClass" runat="server" CssClass="form-control moe-dropdown"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="ddl_GoingToClass" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, GoToClassValidation %>" ValidationGroup="Submit" InitialValue="-1" runat="server" class="error-msg"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-4" id="divCertificateType">
                                            <uc1:DDLWithTXTWithNoPostback runat="server" id="certificateType" />
                                            <%--<asp:CustomValidator ID="valCertificateType" runat="server" ClientValidationFunction="validateCertificateType" OnServerValidate="valCertificateType_ServerValidate" Display="Dynamic" ValidationGroup="Submit" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, certificateTypeValidation %>" CssClass="error-msg"></asp:CustomValidator>--%>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div>
                                            <div class="IGCSE_Div " style="display: none">
                                                <%--<asp:CustomValidator ID="OlevelValidator" runat="server" ClientValidationFunction="validateOlevel" OnServerValidate="serverValidateOlevel" ValidationGroup="Submit" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, OLevelValidation %>"></asp:CustomValidator>--%>
                                                <div class="OLevel_Div margin-top-25">
                                                    <h4 class="subject-header IGCSE-subject-header">
                                                        <asp:Label ID="lblOLevel" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, OLevelMessage %>"></asp:Label>
                                                        <span class="error-msg">*</span>
                                                    </h4>
                                                    <div class="row margin-top-15 IGCSE-fields ">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>
                                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectCode %>"></asp:Literal>
                                                                </label>
                                                                <input type="text" class="Ocode_txt form-control" />
                                                                <%--<asp:Label CssClass="error-msg lblIGValidation0" ClientIDMode="Static" Style="display: none" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, oLevelCodeValidation %>"></asp:Label>--%>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label>
                                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectName %>"></asp:Literal>
                                                                </label>
                                                                <input type="text" class="Otitle_txt form-control" />
                                                                <asp:Label CssClass="error-msg lblIGValidation1" ClientIDMode="Static" Style="display: none" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, oLevelTitleValidation %>"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label>
                                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAverage %>"></asp:Literal>
                                                                </label>
                                                                <%--<input type="text" class="OAvrage_txt form-control" />--%>
                                                                <asp:DropDownList ID="ddlOlevelAverage" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                <asp:Label CssClass="error-msg lblIGValidation2" ClientIDMode="Static" Style="display: none" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, oLevelAverageValidation %>"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2 text-right">
                                                            <div class="form-group">
                                                                <label class="visibility-hidden">
                                                                    المعدل
                                                                </label>
                                                                <asp:Button runat="server" CssClass="addOLevel_btn btn moe-btn" type="button" ValidationGroup="SubmitOlevel" Text="<%$Resources:ITWORX_MOEHEWF_SCE, OLevelAdd %>" />
                                                                <%--<input type="button" class="addOLevel_btn btn moe-btn" value="إضافة" />--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <%--<div>
                                                        <asp:Label ID="lblOlevelValidation" CssClass="error-msg levelValidation" ClientIDMode="Static" Style="display: none" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, OlevelTextboxValidation %>"></asp:Label>
                                                    </div>--%>
                                                    <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="validateOlevel" Display="Dynamic" OnServerValidate="serverValidateOlevel" ValidationGroup="Submit" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, OLevelValidation %>" CssClass="error-msg IGCSE-fields"></asp:CustomValidator>
                                                    <div class="row table-wrapper IGCSE-table" id="divIGolevelTable" style="display: none">
                                                        <table class="OLevel_table table school-table table-striped table-bordered">
                                                            <thead>
                                                                <tr>
                                                                    <th class="text-center">
                                                                        <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectNum %>"></asp:Literal>
                                                                    </th>
                                                                    <th class="text-center">
                                                                        <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectCode %>"></asp:Literal>
                                                                    </th>
                                                                    <th class="text-center">
                                                                        <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectName %>"></asp:Literal>
                                                                    </th>
                                                                    <th class="text-center">
                                                                        <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAverage %>"></asp:Literal>
                                                                    </th>
                                                                    <th class="text-center">
                                                                        <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAction %>"></asp:Literal>
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                                <%--<asp:CustomValidator ID="AlevelValidator" runat="server" ClientValidationFunction="validateAlevel" OnServerValidate="serverValidateAlevel" ValidationGroup="Submit" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, ALevelValidation %>"></asp:CustomValidator>--%>
                                                <div class="ALevel_Div">
                                                    <h4 class="subject-header IGCSE-subject-header">
                                                        <asp:Label ID="lblALevel" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ALevelMessage %>"></asp:Label>
                                                        <span class="error-msg">*</span>
                                                    </h4>
                                                    <div class="row IGCSE-fields ">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>
                                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectCode %>"></asp:Literal>
                                                                </label>
                                                                <input type="text" class="Acode_txt form-control" />
                                                                <%--<asp:Label CssClass="error-msg lblIGValidation0" ClientIDMode="Static" Style="display: none" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, oLevelCodeValidation %>"></asp:Label>--%>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label>
                                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectName %>"></asp:Literal>
                                                                </label>
                                                                <input type="text" class="Atitle_txt form-control" />
                                                                <asp:Label CssClass="error-msg lblIGValidation1" ClientIDMode="Static" Style="display: none" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, oLevelTitleValidation %>"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label>
                                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAverage %>"></asp:Literal>
                                                                </label>
                                                                <%--<input type="text" class="Aavrage_txt form-control" />--%>
                                                                <asp:DropDownList ID="ddlAlevelAverage" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                <asp:Label CssClass="error-msg lblIGValidation2" ClientIDMode="Static" Style="display: none" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, oLevelAverageValidation %>"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2 text-right">
                                                            <div class="form-group">
                                                                <label class="visibility-hidden">
                                                                    المعدل
                                                                </label>
                                                                <%--<input type="button" class="addALevel_btn btn moe-btn" value="إضافة" />--%>
                                                                <asp:Button runat="server" CssClass="addALevel_btn btn moe-btn" type="button" Text="<%$Resources:ITWORX_MOEHEWF_SCE, OLevelAdd %>" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblAlevelValidation" CssClass="error-msg levelValidation" ClientIDMode="Static" Style="display: none" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, OlevelTextboxValidation %>"></asp:Label>
                                                    </div>
                                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="validateAlevel" Display="Dynamic" OnServerValidate="serverValidateAlevel" ValidationGroup="Submit" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, ALevelValidation %>" CssClass="error-msg IGCSE-fields"></asp:CustomValidator>
                                                    <div class="row table-wrapper IGCSE-table" id="divIGalevelTable" style="display: none">
                                                        <table class="ALevel_table table school-table table-striped table-bordered">
                                                            <thead>
                                                                <tr>
                                                                    <th class="text-center">
                                                                        <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectNum %>"></asp:Literal>
                                                                    </th>
                                                                    <th class="text-center">
                                                                        <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectCode %>"></asp:Literal>
                                                                    </th>
                                                                    <th class="text-center">
                                                                        <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectName %>"></asp:Literal>
                                                                    </th>
                                                                    <th class="text-center">
                                                                        <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAverage %>"></asp:Literal>
                                                                    </th>
                                                                    <th class="text-center">
                                                                        <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAction %>"></asp:Literal>
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody></tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="IB_Div margin-top-25" style="display: none">
                                                <h4 class="subject-header IB-subject-header">
                                                    <asp:Label ID="lblIBMessage" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, IBMessage %>"></asp:Label>
                                                    <span class="error-msg">*</span>
                                                </h4>
                                                <div class="row IB-fields">
                                                    <div class="col-md-2 col-sm-6 col-xs-12">
                                                        <label>
                                                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectCode %>"></asp:Literal>
                                                        </label>
                                                        <input type="text" class="IBCode_txt form-control" maxlength="100" />
                                                        <%--<asp:Label CssClass="error-msg lblIGValidation0" ClientIDMode="Static" Style="display: none" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ibCodeValidation %>"></asp:Label>--%>
                                                    </div>
                                                    <div class="col-md-3 col-sm-6 col-xs-12">
                                                        <label>
                                                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectName %>"></asp:Literal>
                                                        </label>
                                                        <input type="text" class="IBTitle_txt form-control" maxlength="255" />
                                                        <asp:Label CssClass="error-msg lblIGValidation1" ClientIDMode="Static" Style="display: none" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ibTitleValidation %>"></asp:Label>
                                                    </div>
                                                    <div class="col-md-2 col-sm-6 col-xs-12">
                                                        <label>
                                                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectPoints %>"></asp:Literal>
                                                        </label>
                                                        <input type="number" class="IBPoints_txt form-control" oninput="javascript: if (this.value.length > 10) this.value = this.value.slice(0, 10);" />
                                                        <asp:Label CssClass="error-msg lblIGValidation2" ClientIDMode="Static" Style="display: none" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ibPointsValidation %>"></asp:Label>
                                                    </div>
                                                    <div class="col-md-3 col-sm-6 col-xs-12">
                                                        <label>
                                                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectLevel %>"></asp:Literal>
                                                        </label>
                                                        <asp:DropDownList ID="ddl_IBLevel" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        <asp:Label CssClass="error-msg lblIGValidation3" ClientIDMode="Static" Style="display: none" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ibLevelValidation %>"></asp:Label>
                                                    </div>
                                                    <div class="col-md-2 col-sm-6 col-xs-12 text-right">
                                                        <div class="form-group">
                                                            <label class="visibility-hidden">المعدل</label>
                                                            <asp:Button runat="server" CssClass="addIB_btn btn moe-btn" type="button" Text="<%$Resources:ITWORX_MOEHEWF_SCE, OLevelAdd %>" />
                                                        </div>
                                                    </div>
                                                    <div class="clearfix"></div>
                                                    <asp:CustomValidator ID="IBValidator" runat="server" Display="Dynamic" ClientValidationFunction="validateIB" OnServerValidate="serverValidateIB" ValidationGroup="Submit" CssClass="error-msg IB-fields" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, IBSubjectValidation %>"></asp:CustomValidator>
                                                   <div class="clearfix"></div>
                                                    <asp:CustomValidator ID="custIBSubjects" runat="server" Display="Dynamic" ClientValidationFunction="validateIBSubjects"  ValidationGroup="Submit" CssClass="error-msg IB-fields" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, IBSubjectsValidation %>" OnServerValidate="custIBSubjects_ServerValidate"></asp:CustomValidator>
                                                    <div class="clearfix"></div>
                                                    <div id="divIBTable" class="row table-wrapper IB-table" style="display: none">
                                                        <table class="IB_table table school-table table-striped table-bordered">
                                                            <thead>
                                                                <tr>
                                                                    <th class="text-center">
                                                                        <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectNum %>"></asp:Literal>
                                                                    </th>
                                                                    <th class="text-center">
                                                                        <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectCode %>"></asp:Literal>
                                                                    </th>
                                                                    <th class="text-center">
                                                                        <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectName %>"></asp:Literal>
                                                                    </th>
                                                                    <th class="text-center">
                                                                        <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectPoints %>"></asp:Literal>
                                                                    </th>
                                                                    <th class="text-center">
                                                                        <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectLevel %>"></asp:Literal>
                                                                    </th>
                                                                    <th class="text-center">
                                                                        <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAction %>"></asp:Literal>
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody></tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="accordion panel-group" id="accordion">
                        <div class="panel panel-default">
                            <div class="panel-heading active">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseFive5">
                                        <asp:Label ID="Label16" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, AcademicData %>"></asp:Label><em></em>
                                    </a>
                                </h4>
                            </div>
                            <div id="collapseFive5" class="panel-collapse collapse in">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lblPassedYears" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, PassedYearsTitle %>"></asp:Label><span class="error-msg">*</span>
                                                </label>
                                                <asp:TextBox ID="txtPassedYears" runat="server" CssClass="form-control" Width="25%" TextMode="Number" min="0" MaxLength="2" onkeypress="restrictMinus(event);" oninput="javascript: if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqPassedYears" runat="server" ControlToValidate="txtPassedYears"  ValidationGroup="Submit" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, PassedYearsValidation %>" class="error-msg"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:WizardStep>

        <%--Fifth Step (Attachments)--%>
        <asp:WizardStep ID="attachments_Stp" runat="server" Title="<%$Resources:ITWORX_MOEHEWF_SCE, Attachments %>">
            <div class="">
                <div class="school-collapse stepFive margin-top-10">
                    <div class="row">
                        <div class="accordion panel-group" id="accordion">
                            <div class="panel panel-default">
                                <div class="panel-heading active">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne1">
                                            <asp:Label ID="Label9" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Attachments %>"></asp:Label><em></em>
                                        </a>
                                    </h4>
                                </div>
                                <%-- /.panel-heading --%>
                                <div id="collapseOne1" class="panel-collapse collapse in">
                                    <div class="panel-body">
                                        <div class="row fileUploadContainer">
                                            <div class="col-md-4 col-sm-6 col-xs-8 margin-bottom-10">
                                                <div class="form-group">
                                                    <label class="margin-bottom-0">
                                                        <asp:Label ID="lblFileName" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequiredFiles %>"></asp:Label>
                                                        <asp:Label ID="lbldropFileUpload" runat="server" CssClass="error-msg" Style="display: none;">*</asp:Label>
                                                    </label>
                                                    <%--<asp:DropDownList ID="dropFileUpload" runat="server" CssClass="form-control moe-dropdown"></asp:DropDownList>--%>
                                                    <div class="moe-file-name">
                                                        <uc1:DDLWithTXTWithNoPostback runat="server" id="dropFileUpload" />
                                                    </div>
                                                    <asp:Label ID="lblRequiredDrop" runat="server" Style="display: none;" CssClass="error-msg" Text="<%$Resources:ITWORX_MOEHEWF_SCE, FileNameValidation %>"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div class="schooling-attach-cntnr margin-bottom-25 moe-file-upload">
                                                <uc1:ClientSideFileUpload runat="server" id="FileUp1" />
                                                
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--<asp:ValidationSummary ID="ValidationSummary1" Enabled="true" runat="server" ValidationGroup="Submit" ShowSummary="true" ShowValidationErrors="true" CssClass="validation-summary" />--%>
        </asp:WizardStep>

        <%--Sixth Step (Review and Approval)--%>
        <asp:WizardStep ID="review_Stp" runat="server" Title="<%$Resources:ITWORX_MOEHEWF_SCE, Review %>">
            <div class="school-collapse stepSix displayMode margin-top-10">

                <!--ContactsData-->
                <div class="row">
                    <div class="accordion panel-group" id="accordion">
                        <div class="panel panel-default">
                            <div class="panel-heading active">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo1">
                                        <asp:Label ID="Label2" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ContactsData %>"></asp:Label><em></em>
                                    </a>
                                </h4>
                            </div>
                            <%-- /.panel-heading --%>
                            <div id="collapseTwo1" class="panel-collapse collapse in">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-4  col-xs-12">
                                            <div class="form-group">
                                                <h6>
                                                    <asp:Label ID="lblApplicantOfficialNameText" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, NameOfficialRecords %>"></asp:Label>
                                                </h6>
                                                <h5>
                                                    <asp:Label ID="lblApplicantOfficialNameVal" runat="server"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                        <div class="col-md-4  col-xs-12">
                                            <div class="form-group">
                                                <h6>
                                                    <asp:Label ID="lblMobileNumberText" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, MobileNumber %>"></asp:Label>
                                                </h6>
                                                <h5>
                                                    <asp:Label ID="lblMobileNumberVal" runat="server" ClientIDMode="Static"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                        <div class="col-md-4  col-xs-12">
                                            <div class="form-group">
                                                <h6>
                                                    <asp:Label ID="lblEmailText" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Email %>"></asp:Label>
                                                </h6>
                                                <h5>
                                                    <asp:Label ID="lblEmailVal" runat="server"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!--StudentData-->
                <div class="row">
                    <div class="accordion panel-group" id="accordion">
                        <div class="panel panel-default">
                            <div class="panel-heading active">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne1">
                                        <asp:Label ID="Label1" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentData %>"></asp:Label><em></em>
                                    </a>
                                </h4>
                            </div>
                            <%-- /.panel-heading --%>
                            <div id="collapseOne1" class="panel-collapse collapse in">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-4  col-xs-12" id="divPassportContainer" runat="server" style="display: none">
                                            <div class="form-group">
                                                <h6>
                                                    <asp:Label ID="lblPassPortDisplay" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Passport %>" Visible="false"></asp:Label>
                                                </h6>
                                                <h5>
                                                    <asp:Label ID="lblPassPortVal" ClientIDMode="Static" runat="server" Visible="false"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                        <div class="col-md-4  col-xs-12">
                                            <div class="form-group">
                                                <h6>
                                                    <asp:Label ID="Label10" runat="server" ></asp:Label>
                                                </h6>
                                                <h5>
                                                    <asp:Label ID="lblQatarIDVal" ClientIDMode="Static" runat="server"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                        
                                        <div class="col-md-4  col-xs-12">
                                            <div class="form-group">
                                                <h6>
                                                    <asp:Label ID="Label3" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, BirthDate %>"></asp:Label>
                                                </h6>
                                                <h5>
                                                    <asp:Label ID="lblbirthDateVal" ClientIDMode="Static" runat="server"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                        <div class="col-md-4  col-xs-12">
                                            <div class="form-group">
                                                <h6>
                                                    <asp:Label ID="Label4" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentName %>"></asp:Label>
                                                </h6>
                                                <h5>
                                                    <asp:Label ID="lblNameVal" ClientIDMode="Static" runat="server"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                        <div id="" class=" qatariContainer">
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="col-md-4  col-xs-12">
                                            <div class="form-group">
                                                <h6>
                                                    <asp:Label ID="Label5" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentNationality %>"></asp:Label>
                                                </h6>
                                                <h5>
                                                    <asp:Label ID="lblNationalityVal" runat="server"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                        <div class="col-md-4  col-xs-12">
                                            <div class="form-group">
                                                <h6>
                                                    <asp:Label ID="Label7" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentGender %>"></asp:Label>
                                                </h6>
                                                <h5>
                                                    <asp:Label ID="lblGenderVal" runat="server"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                        <div class="col-md-4  col-xs-12">
                                            <div class="form-group">
                                                <h6>
                                                    <asp:Label ID="Label6" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentNationalityType %>"></asp:Label>
                                                </h6>
                                                <h5>
                                                    <asp:Label ID="lblNatCatVal" runat="server"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                        <div id="" class=" qatariContainer">
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="col-md-4  col-xs-12">
                                            <div class="form-group">
                                                <h6>
                                                    <asp:Label ID="Label8" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentNameCert %>"></asp:Label>
                                                </h6>
                                                <h5>
                                                    <asp:Label ID="lblPrintedNameDisplay" runat="server"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!--CertificateDetails-->
                <div class="row">
                    <div class="accordion panel-group" id="accordion">
                        <div class="panel panel-default">
                            <div class="panel-heading active">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne2">
                                        <asp:Label ID="Label11" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateDetails %>"></asp:Label><em></em>
                                    </a>
                                </h4>
                            </div>
                            <%-- /.panel-heading --%>
                            <div id="collapseOne2" class="panel-collapse collapse in">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-4  col-xs-12">
                                            <div class="form-group">
                                                <h6>
                                                    <asp:Label ID="lblCertResource" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateResource %>"></asp:Label>
                                                </h6>
                                                <h5>
                                                    <asp:Label ID="lblcertificateResource" runat="server" />
                                                </h5>
                                            </div>
                                        </div>
                                        <div class="col-md-4  col-xs-12">
                                            <div class="form-group">
                                                <h6>
                                                    <asp:Label ID="lblSchoolType" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SchoolType %>"></asp:Label>
                                                </h6>
                                                <h5>
                                                    <asp:Label ID="lblSchoolTypeVal" runat="server"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                        <div class="col-md-4  col-xs-12">
                                            <div class="form-group">
                                                <h6>
                                                    <asp:Label ID="Label12" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, PreviousSchool %>"></asp:Label>
                                                </h6>
                                                <h5>
                                                    <asp:Label ID="lblPrevSchool" runat="server"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row margin-top-15">
                                        <div class="col-md-4  col-xs-12">
                                            <div class="form-group">
                                                <h6>
                                                    <asp:Label ID="Label14" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SchoolingSystem %>"></asp:Label>
                                                </h6>
                                                <h5>
                                                    <asp:Label ID="lblSchoolSystemVal" runat="server"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                        <div class="col-md-4  col-xs-12">
                                            <div class="form-group">
                                                <h6>
                                                    <asp:Label ID="Label15" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, LastSchoolYear %>"></asp:Label></h6>
                                                <h5>
                                                    <asp:Label ID="lblScholasticLevel" runat="server"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                        <div class="col-md-4  col-xs-12">
                                            <div class="form-group">
                                                <h6>
                                                    <asp:Label ID="Label17" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, AcademicYear %>"></asp:Label>
                                                </h6>
                                                <h5>
                                                    <asp:Label ID="lblLastAcademicYear" runat="server"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row margin-top-15">
                                        <div class="col-md-4  col-xs-12">
                                            <div class="form-group">
                                                <h6>
                                                    <asp:Label ID="Label19" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, EquivalencyPurpose %>"></asp:Label>
                                                </h6>
                                                <h5>
                                                    <asp:Label ID="lblEquiPurposeVal" runat="server" />
                                                </h5>
                                            </div>
                                        </div>
                                        <div class="col-md-4  col-xs-12">
                                            <div class="form-group">
                                                <h6>
                                                    <asp:Label ID="Label20" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, GoToClass %>"></asp:Label>
                                                </h6>
                                                <h5>
                                                    <asp:Label ID="lblGoingToClass" runat="server"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                        <div class="col-md-4  col-xs-12">
                                            <div class="form-group">
                                                <h6>
                                                    <asp:Label ID="lblCertificateType" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateType %>"></asp:Label>
                                                </h6>
                                                <h5>
                                                    <asp:Label ID="lblCertificateTypeVal" runat="server" />
                                                </h5>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div id="divIG" class="IGCSE-table-view" style="display: none">
                                            <div class="OLevel_Div">
                                                <h4 class="subject-header IGCSE-subject-header">
                                                <asp:Label ID="Label23" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, OLevelMessageReview %>"></asp:Label>
                                                    </h4>
                                                <table id="tblOlevel" class="table moe-table table-striped table-bordered">
                                                    <thead>
                                                        <tr>
                                                            <th class="text-center">
                                                                <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectNum %>"></asp:Literal>
                                                            </th>
                                                            <th class="text-center">
                                                                <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectCode %>"></asp:Literal>
                                                            </th>
                                                            <th class="text-center">
                                                                <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectName %>"></asp:Literal>
                                                            </th>
                                                            <th class="text-center">
                                                                <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAverage %>"></asp:Literal>
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody></tbody>
                                                </table>
                                            </div>
                                            <div class="ALevel_Div margin-top-50">
                                                <h4 class="subject-header IGCSE-subject-header">
                                               <asp:Label ID="Label24" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ALevelMessageReview %>"></asp:Label>
                                                    </h4>
                                                <table id="tblAlevel" class="table moe-table table-striped table-bordered">
                                                    <thead>
                                                        <tr>
                                                            <th class="text-center">
                                                                <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectNum %>"></asp:Literal>
                                                            </th>
                                                            <th class="text-center">
                                                                <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectCode %>"></asp:Literal>
                                                            </th>
                                                            <th class="text-center">
                                                                <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectName %>"></asp:Literal>
                                                            </th>
                                                            <th class="text-center">
                                                                <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAverage %>"></asp:Literal>
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody></tbody>
                                                </table>
                                            </div>
                                        </div>
                                        <div id="divIB" class="IB-table-view" style="display: none">
                                            <div>
                                                <h4 class="subject-header IGCSE-subject-header">
                                               <asp:Label ID="Label25" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, IBMessageReview %>"></asp:Label>
                                                    </h4>
                                                <table id="tblIB" class="table moe-table table-striped table-bordered">
                                                    <thead>
                                                        <tr>
                                                            <th class="text-center">
                                                                <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectNum %>"></asp:Literal>
                                                            </th>
                                                            <th class="text-center">
                                                                <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectCode %>"></asp:Literal>
                                                            </th>
                                                            <th class="text-center">
                                                                <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectName %>"></asp:Literal>
                                                            </th>
                                                            <th class="text-center">
                                                                <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectPoints %>"></asp:Literal>
                                                            </th>
                                                            <th class="text-center">
                                                                <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectLevel %>"></asp:Literal>
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody></tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!--Passed Years-->
                <div class="row">
                    <div class="accordion panel-group" id="accordion">
                        <div class="panel panel-default">
                            <div class="panel-heading active">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseFive1">
                                        <asp:Label ID="Label18" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, AcademicData %>"></asp:Label><em></em>
                                    </a>
                                </h4>
                            </div>
                            <div id="collapseFive1" class="panel-collapse collapse in">
                                <div class="panel-body">
                                    <div class="row margin-top-15">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <h6>
                                                    <asp:Label ID="Label21" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, PassedYearsTitle %>"></asp:Label>
                                                </h6>
                                                <h5>
                                                    <asp:Label ID="lblTotalPassedYears" runat="server"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- attachments -->
                <div class="row">
                    <div class="accordion panel-group" id="accordion">
                        <div class="panel panel-default">
                            <div class="panel-heading active">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo11">
                                        <asp:Label ID="Label22" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Attachments %>"></asp:Label><em></em>
                                    </a>
                                </h4>
                            </div>
                            <%-- /.panel-heading --%>
                            <div id="collapseTwo11" class="panel-collapse collapse in">
                                <div class="panel-body">
                                    <uc1:ClientSideFileUpload runat="server" id="FileUploadDisplay" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- review line -->
                <div class="row">
                    <div class="accordion panel-group" id="accordion">
                        <div class="panel panel-default">
                            <div class="panel-heading active">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo12">
                                        <asp:Label ID="Label13" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ReviewStepTitle %>"></asp:Label><em></em>
                                    </a>
                                </h4>
                            </div>
                            <%-- /.panel-heading --%>
                            <div id="collapseTwo12" class="panel-collapse collapse in">
                                <div class="panel-body">
                                    <asp:CheckBox ID="chkReviewStep" runat="server" ClientIDMode="Static" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ReviewStepText %>" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:WizardStep>

    </WizardSteps>
    <StartNavigationTemplate>
        <div class="margin-lr-30">
            <div class="row">
                <div class="col-sm-6 col-xs-12 text-left"></div>
                <div class="col-sm-6 col-xs-12 text-right">
                    <asp:Button ID="StartNextButton" runat="server" ClientIDMode="Static" ValidationGroup="Submit" CommandName="MoveNext" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Next %>" CssClass="enableStartNextButton" />
                </div>
            </div>
        </div>
    </StartNavigationTemplate>
    <StepNavigationTemplate>
        <div class="margin-lr-30">
            <div class="row">
                <div class="col-sm-6 col-xs-12 text-left">
                    <asp:Button ID="StepPreviousButton" runat="server" CausesValidation="False" CommandName="MovePrevious" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Previous %>" />
                </div>
                <div class="col-sm-6 col-xs-12 text-right">
                    <asp:Button ID="StepNextButton" runat="server" ClientIDMode="Static" ValidationGroup="Submit"  CommandName="MoveNext" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Next %>" />
                </div>
            </div>
        </div>
    </StepNavigationTemplate>
    <FinishNavigationTemplate>
        <div class="margin-lr-30">
            <div class="row">
                <div class="col-sm-6 col-xs-12 text-left">
                    <asp:Button ID="FinishPreviousButton" runat="server" CausesValidation="true" CommandName="MovePrevious" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Previous %>" />
                </div>
                <div class="col-sm-6 col-xs-12 text-right">
                    <asp:Button ID="FinishButton" runat="server" ClientIDMode="Static" ValidationGroup="Submit" CommandName="MoveComplete" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Send %>" CssClass="enableFinishButton" />
                </div>
            </div>
        </div>
    </FinishNavigationTemplate>
</asp:Wizard>

<style>
    #ddl_Gender::-ms-expand {
        display: none;
    }

    #ddl_Gender {
        -webkit-appearance: none;
        appearance: none;
    }

    #ddl_Nationality::-ms-expand {
        display: none;
    }

    #ddl_Nationality {
        -webkit-appearance: none;
        appearance: none;
    }
</style>

<script>

    var oLevel_IGCSEList = [];
    var aLevel_IGCSEList = [];
    var IBList = [];
    var SLCount = 0;
    var HLCount = 0;
    window.onload = function () {
        if (document.getElementById("StartNextButton") != null)
            document.getElementById("StartNextButton").focus();
        else if (document.getElementById("StepNextButton") != null)
            document.getElementById("StepNextButton").focus();
        else if (document.getElementById("FinishButton") != null)
            document.getElementById("FinishButton").focus();
    }


    $('#txt_birthDate').datepicker({
        dateFormat: "dd/mm/yy",
        showOn: 'focus',
        showButtonPanel: false,
        maxDate: -1,
        changeYear: true,
        changeMonth: true,
        yearRange: 'c-100:nn'
    });
    $("#txt_birthDate").keydown(false);

    $('#lblMobileNumberVal').before("00974-");

    function restrictMinus(e) {
        var inputKeyCode = e.keyCode ? e.keyCode : e.which;
        if (inputKeyCode != null) {
            if (inputKeyCode == 45) e.preventDefault();
        }
    }

    if ($("#<%=ddl_ScholasticLevel.ClientID%>").val() >= 10 && $("#<%=ddl_ScholasticLevel.ClientID%>").val() <= 13) {
        $('#divCertificateType').show();
    } else {
        $('#divCertificateType').hide();
    }



    $("#<%=ddl_ScholasticLevel.ClientID%>").change(function () {
        var lastClass = $("#<%=ddl_ScholasticLevel.ClientID%>").val();
        if (lastClass >= 10 && lastClass <= 13) {
            $('#divCertificateType').show();
        } else {
            $('#divCertificateType').hide();
            $("#<%=certificateType.Client_ID%>").val("-1");
            $(".IB_Div").hide();
            $(".IGCSE_Div").hide();
        }
        //if (lastClass == 13) {
        //    $("#divCertificateType .form-group .astrik").show();
        //} else {
        //    $("#divCertificateType .form-group .astrik").hide();
        //}
    });

    LoadSubjects();
    //disable enter key
    $(document).keypress(
        function (event) {
            if (event.which == '13') {
                event.preventDefault();
            }
        });

    function LoadSubjects() {
        if ($("#<%=OLevel_HF.ClientID%>").val() != "") {
            $('#divIG').show();
            oLevel_IGCSEList = JSON.parse($("#<%=OLevel_HF.ClientID%>").val());
            if (oLevel_IGCSEList.length > 0) {
                for (var i = 0; i < oLevel_IGCSEList.length; i++) {
                    var tr;
                    tr = $('<tr id="' + (parseInt(i) + 1) + '" />');
                    tr.append("<td class='text-center'>" + (parseInt(i) + 1) + "</td>");
                    if (oLevel_IGCSEList[i].Code != null) {
                        tr.append("<td class='text-center'>" + oLevel_IGCSEList[i].Code + "</td>");
                    } else {
                        tr.append("<td class='text-center'> </td>");
                    }
                    tr.append("<td class='text-center'>" + oLevel_IGCSEList[i].Title + "</td>");
                    tr.append("<td class='text-center'>" + oLevel_IGCSEList[i].Avrage + "</td>");
                    //tr.append("<td><input type='button' class='DeleteOLevel_btn' value='حذف' onclick='DeleteOLevel(" + (parseInt(i) + 1) + ")'/></td>");
                    $('#tblOlevel').append(tr);
                }
            }
        }
        if ($("#<%=ALevel_HF.ClientID%>").val() != "") {
            $('#divIG').show();
            aLevel_IGCSEList = JSON.parse($("#<%=ALevel_HF.ClientID%>").val());
            if (aLevel_IGCSEList.length > 0) {
                for (var i = 0; i < aLevel_IGCSEList.length; i++) {
                    var tr;
                    tr = $('<tr id="' + (parseInt(i) + 1) + '" />');
                    tr.append("<td class='text-center'>" + (parseInt(i) + 1) + "</td>");
                    if (aLevel_IGCSEList[i].Code != null) {
                        tr.append("<td class='text-center'>" + aLevel_IGCSEList[i].Code + "</td>");
                    } else {
                        tr.append("<td class='text-center'> </td>");
                    }
                    tr.append("<td class='text-center'>" + aLevel_IGCSEList[i].Title + "</td>");
                    tr.append("<td class='text-center'>" + aLevel_IGCSEList[i].Avrage + "</td>");
                    //tr.append("<td><input type='button' class='DeleteALevel_btn' value='حذف' onclick='DeleteALevel(" + (parseInt(i) + 1) + ")'/></td>");
                    $('#tblAlevel').append(tr);
                }
            }
        }
        if ($("#<%=IBList_HF.ClientID%>").val() != "") {
            $('#divIB').show();
            var sumPoints = 0;
            IBList = JSON.parse($("#<%=IBList_HF.ClientID%>").val());
            SLCount = 0;
            HLCount = 0;
            if (IBList.length > 0) {
                $('#divIBTable').show();
                for (var i = 0; i < IBList.length; i++) {
                    if (IBList[i].Level == "SL") {
                        SLCount++;
                    }
                    else if (IBList[i].Level == "HL") {
                        HLCount++;
                    }
                    var tr;
                    tr = $('<tr id="' + (parseInt(i) + 1) + '" />');
                    tr.append("<td class='text-center'>" + (parseInt(i) + 1) + "</td>");
                    if (IBList[i].Code != null) {
                        tr.append("<td class='text-center'>" + IBList[i].Code + "</td>");
                    } else {
                        tr.append("<td class='text-center'> </td>");
                    }
                    tr.append("<td class='text-center'>" + IBList[i].Title + "</td>");
                    tr.append("<td class='text-center'>" + IBList[i].Points + "</td>");
                    tr.append("<td class='text-center'>" + IBList[i].Level + "</td>");
                    //tr.append("<td><input type='button' class='DeleteIBItem_btn' value='حذف' onclick='DeleteIBItem(" + (parseInt(i) + 1) + ")'/></td>");
                    $('#tblIB').append(tr);
                    sumPoints += parseInt(IBList[i].Points);
                }
            }
            $('#tblIB').append(
                '<tfoot><tr>' +
                '<td id="footerIbTxt" class="text-center" colspan="3">' + "<%=Resources.ITWORX_MOEHEWF_SCE.Total %>" + '</td>' +
                '<td id="footerIb" class="text-center">' + sumPoints + '</td>' +
                '</tfoot></tr>'
            );
        }
    }

    $(function () {
        LoadForm();
        $('body').append('<div class="loader-overlay"><div class="loader-msg"><span class="loader-overlay-close">x</span><h1 class="errorDes">MOI Error</h1><img src="/_catalogs/masterpage/MOEHE/common/img/loading.gif" /></div></div>');
        $('.loader-overlay-close').click(function () {
            $('.loader-overlay').fadeOut(100);
            $('body').css('overflow', 'auto');
        });
        $('.loader-overlay').hide();
        $("#txt_QatarID").blur(function () {
            if ($("#txt_birthDate").val() != "") {
                getStudentData();
                $('.loader-overlay').show();
                $('.loader-msg img').show();
                $('.loader-overlay-close').hide();
                $('.errorDes').hide();
            }
            else {
                ClearMOIOutputFileds();
            }

        });
        $("#txt_birthDate").change(function () {
            if ($("#txt_QatarID").val() != "" && $("#txt_birthDate").val() != "") {
                getStudentData();
                $('.loader-overlay').show();
                $('.loader-msg img').show();
                $('.loader-overlay-close').hide();
                $('.errorDes').hide();
            }
            else {
                ClearMOIOutputFileds();
            }
        });

        $(".addIB_btn").click(function (e) {
            debugger;
            e.preventDefault();
            if ($(".IBTitle_txt").val() != "" && $("#ddl_IBLevel").val() != "-1" && $(".IBPoints_txt").val() != "") {
                var id = IBList.length > 0 ? parseInt(IBList[IBList.length - 1].ID) + 1 : 1;
                IBList.push({ ID: id, Title: $(".IBTitle_txt").val(), Code: $(".IBCode_txt").val(), Level: $("#ddl_IBLevel option:selected").text(), LevelTitle: $("#ddl_IBLevel option:selected").text(), Points: $(".IBPoints_txt").val() })


                var sumPoints = 0;
                var tr;
                tr = $('<tr id="' + id + '" />');
                tr.append("<td class='text-center'>" + id + "</td>");
                tr.append("<td class='text-center'>" + $(".IBCode_txt").val() + "</td>");
                tr.append("<td class='text-center'>" + $(".IBTitle_txt").val() + "</td>");
                tr.append("<td class='text-center'>" + $(".IBPoints_txt").val() + "</td>");
                tr.append("<td class='text-center'>" + $("#ddl_IBLevel option:selected").text() + "</td>");
                var isAr = window.location.href.toLowerCase().indexOf("ar") >= 0;
                if (isAr) {
                    tr.append("<td  class='text-center'><a class='DeleteIBItem_btn fa fa-times delete-icon' title='حذف' onclick='DeleteIBItem(" + id + ")'></a></td>");
                } else {
                    tr.append("<td><a class='DeleteIBItem_btn fa fa-times delete-icon' title='Delete' onclick='DeleteIBItem(" + id + ")'></a></td>");
                }
                $('.IB_table').append(tr);
                $('#divIBTable').show();
                SLCount = 0;
                HLCount = 0;
                for (var i = 0; i < IBList.length; i++) {
                    sumPoints = sumPoints + parseInt(IBList[i].Points);
                    if (IBList[i].Level == "SL") {
                        SLCount++;
                    }
                    else if (IBList[i].Level == "HL") {
                        HLCount++;
                    }
                }
                if (id == 1) {
                    $('#footerIbTxt').remove();
                    $('#footerIb').remove();
                    $('.IB_table').append(
                        '<tfoot><tr>' +
                        '<td id="footerIbTxt" class="text-center" colspan="3">' + "<%=Resources.ITWORX_MOEHEWF_SCE.Total %>" + '</td>' +
                        '<td id="footerIb" class="text-center">' + sumPoints + '</td>' +
                        '</tr></tfoot>'
                    );
                } else {
                    $("#footerIb").text(sumPoints);
                }
                $("#<%=IBList_HF.ClientID%>").val(JSON.stringify(IBList));
                validateDivs("IB_Div");
                if ($("#ddl_IBLevel").val() == "-1") {
                    $("#ddl_IBLevel").css('border-color', 'red');
                } else {
                    $("#ddl_IBLevel").css('border-color', '');
                }
                $(".IBTitle_txt").val("");
                $(".IBCode_txt").val("");
                $("#ddl_IBLevel").val("-1");
                $(".IBPoints_txt").val("");
            } else {
                validateDivs("IB_Div");
                if ($("#ddl_IBLevel").val() == "-1") {
                    $("#ddl_IBLevel").css('border-color', 'red');
                    $(".IB_Div .lblIGValidation3").show();

                } else {
                    $("#ddl_IBLevel").css('border-color', '');
                    $(".IB_Div .lblIGValidation3").hide();
                }
            }
        });

        $(".addALevel_btn").click(function (e) {
            e.preventDefault();
            if ($(".Atitle_txt").val() != "" && $("#ddlAlevelAverage").val() != "-1") {
                var id = aLevel_IGCSEList.length > 0 ? parseInt(aLevel_IGCSEList[aLevel_IGCSEList.length - 1].ID) + 1 : 1;
                aLevel_IGCSEList.push({ ID: id, Code: $(".Acode_txt").val(), Title: $(".Atitle_txt").val(), Avrage: $("#ddlAlevelAverage option:selected").text() })
                var isAr = window.location.href.toLowerCase().indexOf("ar") >= 0;
                var tr;
                tr = $('<tr id="2' + id + '" />');
                tr.append("<td class='text-center'>" + id + "</td>");
                tr.append("<td class='text-center'>" + $(".Acode_txt").val() + "</td>");
                tr.append("<td class='text-center'>" + $(".Atitle_txt").val() + "</td>");
                tr.append("<td class='text-center'>" + $("#ddlAlevelAverage option:selected").text() + "</td>");
                if (isAr) {
                    tr.append("<td class='text-center'><a class='DeleteALevel_btn  fa fa-times delete-icon' title='حذف' onclick='DeleteALevel(2" + id + ")'></a></td>");
                } else {
                    tr.append("<td class='text-center'><a class='DeleteALevel_btn  fa fa-times delete-icon' title='Delete' onclick='DeleteALevel(2" + id + ")'></a></td>");
                }
                $('.ALevel_table').append(tr);
                $('#divIGalevelTable').show();
                $("#<%=ALevel_HF.ClientID%>").val(JSON.stringify(aLevel_IGCSEList));
                validateDivs("ALevel_Div");
                $(".Acode_txt").val("");
                $(".Atitle_txt").val("");
                $(".Aavrage_txt").val("");
                $("#ddlAlevelAverage").val("-1");
                $("#ddlAlevelAverage").css('border-color', '');
            } else {
                validateDivs("ALevel_Div");
                if ($("#ddlAlevelAverage").val() == "-1") {
                    $("#ddlAlevelAverage").css('border-color', 'red');
                    $(".ALevel_Div .lblIGValidation2").show();

                } else {
                    $("#ddlAlevelAverage").css('border-color', '');
                    $(".ALevel_Div .lblIGValidation2").hide();
                }
            }
        });

        $(".addOLevel_btn").click(function (e) {
            e.preventDefault();
            if ($(".Otitle_txt").val() != "" && $("#ddlOlevelAverage").val() != "-1") {
                var id = oLevel_IGCSEList.length > 0 ? parseInt(oLevel_IGCSEList[oLevel_IGCSEList.length - 1].ID) + 1 : 1;
                oLevel_IGCSEList.push({ ID: id, Code: $(".Ocode_txt").val(), Title: $(".Otitle_txt").val(), Avrage: $("#ddlOlevelAverage option:selected").text() })
                var isAr = window.location.href.toLowerCase().indexOf("ar") >= 0;
                var tr;
                tr = $('<tr id="1' + id + '" />');
                tr.append("<td class='text-center'>" + id + "</td>");
                tr.append("<td class='text-center'>" + $(".Ocode_txt").val() + "</td>");
                tr.append("<td class='text-center'>" + $(".Otitle_txt").val() + "</td>");
                tr.append("<td class='text-center'>" + $("#ddlOlevelAverage option:selected").text() + "</td>");
                if (isAr) {
                    tr.append("<td class='text-center'><a class='DeleteOLevel_btn fa fa-times delete-icon' title='حذف' onclick='DeleteOLevel(1" + id + ")'></a></td>");
                } else {
                    tr.append("<td class='text-center'><a class='DeleteOLevel_btn fa fa-times delete-icon' title='Delete' onclick='DeleteOLevel(1" + id + ")'></a></td>");
                }
                $('.OLevel_table').append(tr);
                $('#divIGolevelTable').show();
                $("#<%=OLevel_HF.ClientID%>").val(JSON.stringify(oLevel_IGCSEList));
                validateDivs("OLevel_Div");
                $(".Ocode_txt").val("");
                $(".Otitle_txt").val("");
                $("#ddlOlevelAverage").val("-1");
                $("#ddlOlevelAverage").css('border-color', '');
            } else {
                validateDivs("OLevel_Div");
                if ($("#ddlOlevelAverage").val() == "-1") {
                    $("#ddlOlevelAverage").css('border-color', 'red');
                    $(".OLevel_Div .lblIGValidation2").show();

                } else {
                    $("#ddlOlevelAverage").css('border-color', '');
                    $(".OLevel_Div .lblIGValidation2").hide();
                }
            }
        });

        $("#<%=CerTypeClientID%>").change(function () {
            viewHideCert(true);
        });
    })

    function validateDivs(control) {
        $("." + control + " input").each(function (index) {
            if (index != 0) {
                if ($(this).val() == "") {
                    $(this).css('border-color', 'red');
                    //$("." + control + " .levelValidation").show();
                    $("." + control + " .lblIGValidation" + index).show();
                } else {
                    $(this).css('border-color', '');
                    //$("." + control + " .levelValidation").hide();
                    $("." + control + " .lblIGValidation" + index).hide();
                }
            }

        });
    }

    function DeleteALevel(ID) {
        var trimId = ID.toString().substring(1);
        $('table.ALevel_table tr#' + ID).remove();
        for (var i = 0; i < aLevel_IGCSEList.length; i++) {
            if (aLevel_IGCSEList[i].ID == trimId)
                aLevel_IGCSEList.splice($.inArray(aLevel_IGCSEList[i], aLevel_IGCSEList), 1);
        }
        $("#<%=ALevel_HF.ClientID%>").val(JSON.stringify(aLevel_IGCSEList));
        if (aLevel_IGCSEList.length == 0) {
            $('#divIGalevelTable').hide();
        }
    }

    function DeleteOLevel(ID) {
        var trimId = ID.toString().substring(1);
        $('table.OLevel_table tr#' + ID).remove();
        for (var i = 0; i < oLevel_IGCSEList.length; i++) {
            if (oLevel_IGCSEList[i].ID == trimId)
                oLevel_IGCSEList.splice($.inArray(oLevel_IGCSEList[i], oLevel_IGCSEList), 1);
        }
        $("#<%=OLevel_HF.ClientID%>").val(JSON.stringify(oLevel_IGCSEList));
        if (oLevel_IGCSEList.length == 0) {
            $('#divIGolevelTable').hide();
        }
    }

    function DeleteIBItem(ID) {
        debugger;
        $('table.IB_table tr#' + ID).remove();
        for (var i = 0; i < IBList.length; i++) {
            if (IBList[i].ID == ID) {

                IBList.splice($.inArray(IBList[i], IBList), 1);
            }
        }
        SLCount = 0;
        HLCount = 0;
        for (var i = 0; i < IBList.length; i++) {
            if (IBList[i].Level == "SL") {
                SLCount++;
            }
            else if (IBList[i].Level == "HL") {
                HLCount++;
            }
        }
        $("#<%=IBList_HF.ClientID%>").val(JSON.stringify(IBList));
        var sumPoints = 0;
        for (var i = 0; i < IBList.length; i++) {
            sumPoints = sumPoints + parseInt(IBList[i].Points);
        }



        $("#footerIb").empty();
        $("#footerIb").text(sumPoints);
        if (IBList.length == 0) {
            $('#divIBTable').hide();
        }
    }

    function validateAlevel(sender, arguments) {
        var crtTypeVal = $("#<%=CerTypeClientID%>").val();
        if (crtTypeVal == 1 && (aLevel_IGCSEList == null || parseInt(aLevel_IGCSEList.length) < 2)) {
            arguments.IsValid = false;
        }
    }

    function validateOlevel(sender, arguments) {
        var crtTypeVal = $("#<%=CerTypeClientID%>").val();
        if (crtTypeVal == 1 && (oLevel_IGCSEList == null || (parseInt(oLevel_IGCSEList.length) < 5))) {
            arguments.IsValid = false;
        }
    }

    function validateCertificateType(sender, arguments) {
        if ($("#<%=certificateType.Client_ID%>").val() == -1 && $("#<%=ddl_ScholasticLevel.ClientID%>").val() == 13) {
            arguments.IsValid = false;
        } else {
            arguments.IsValid = true;
        }
    }

    function validateIB(sender, arguments) {
        var crtTypeVal = $("#<%=CerTypeClientID%>").val();
        if (crtTypeVal == 2) {
            var points = 0;
            for (var i = 0; i < IBList.length; i++) {
                points = points + parseInt(IBList[i].Points);
            }
            if (points < 24)
                arguments.IsValid = false;
        }
    }
    function validateIBSubjects(sender, arguments) {
        debugger;
        var crtTypeVal = $("#<%=CerTypeClientID%>").val();
        if (crtTypeVal == 2) {
            if ((SLCount == 4 && HLCount == 2) || (SLCount == 3 && HLCount == 3)) {
                arguments.IsValid = true;
            }

            else {
                arguments.IsValid = false;
            }
        }
    }
    function validateQatarID(sender, argument) {
        //debugger;
        if ($("#txt_QatarID").val() == "" || $("#txt_birthDate").val() == "" || $("#txt_Name").val() == "" || $("#ddl_Nationality").val() == "-1" || $("#ddl_Gender").val() == "-1") {
            argument.IsValid = false;
        }
    }

    function validateNationality(sender, argument) {
        if ($("#ddl_Nationality").val() == "-1") {
            var isAr = window.location.href.toLowerCase().indexOf("ar") >= 0;
            if (isAr) {
                $("#lblNationalityValidation").text("برجاء اختيار الجنسية");
            } else {
                $("#lblNationalityValidation").text("Please choose nationality");
            }
            argument.IsValid = false;
        } else {
            argument.IsValid = true;
            $("#lblNationalityValidation").text("");
        }
    }

    function LoadForm() {
        //getStudentData();
        viewHideCert(false);
        if ($("#<%=OLevel_HF.ClientID%>").val() != "") {
            $('#divIGolevelTable').show();
            oLevel_IGCSEList = JSON.parse($("#<%=OLevel_HF.ClientID%>").val());
            var isAr = window.location.href.toLowerCase().indexOf("ar") >= 0;
            if (oLevel_IGCSEList.length > 0) {
                for (var i = 0; i < oLevel_IGCSEList.length; i++) {
                    var tr;
                    tr = $('<tr id="1' + oLevel_IGCSEList[i].ID + '" />');
                    tr.append("<td class='text-center'>" + (parseInt(i) + 1) + "</td>");
                    if (oLevel_IGCSEList[i].Code != null) {
                        tr.append("<td class='text-center'>" + oLevel_IGCSEList[i].Code + "</td>");
                    } else {
                        tr.append("<td class='text-center'> </td>");
                    }
                    tr.append("<td class='text-center'>" + oLevel_IGCSEList[i].Title + "</td>");
                    tr.append("<td class='text-center'>" + oLevel_IGCSEList[i].Avrage + "</td>");
                    if (isAr) {
                        tr.append("<td class='text-center'><a class='DeleteOLevel_btn fa fa-times delete-icon' title='حذف' onclick='DeleteOLevel(1" + oLevel_IGCSEList[i].ID + ")'></a></td>");
                    } else {
                        tr.append("<td class='text-center'><a class='DeleteOLevel_btn fa fa-times delete-icon' title='Delete' onclick='DeleteOLevel(1" + oLevel_IGCSEList[i].ID + ")'></a></td>");
                    }
                    $('.OLevel_table').append(tr);
                }
            }
        }

        if ($("#<%=ALevel_HF.ClientID%>").val() != "") {
            $('#divIGalevelTable').show();
            aLevel_IGCSEList = JSON.parse($("#<%=ALevel_HF.ClientID%>").val());
            var isAr = window.location.href.toLowerCase().indexOf("ar") >= 0;
            if (aLevel_IGCSEList.length > 0) {
                for (var i = 0; i < aLevel_IGCSEList.length; i++) {
                    var tr;
                    tr = $('<tr id="2' + aLevel_IGCSEList[i].ID + '" />');
                    tr.append("<td class='text-center'>" + (parseInt(i) + 1) + "</td>");
                    if (aLevel_IGCSEList[i].Code != null) {
                        tr.append("<td class='text-center'>" + aLevel_IGCSEList[i].Code + "</td>");
                    } else {
                        tr.append("<td class='text-center'> </td>");
                    }
                    tr.append("<td class='text-center'>" + aLevel_IGCSEList[i].Title + "</td>");
                    tr.append("<td class='text-center'>" + aLevel_IGCSEList[i].Avrage + "</td>");
                    if (isAr) {
                        tr.append("<td class='text-center'><a class='DeleteALevel_btn fa fa-times delete-icon' title='حذف' onclick='DeleteALevel(2" + aLevel_IGCSEList[i].ID + ")'></a></td>");
                    } else {
                        tr.append("<td class='text-center'><a class='DeleteALevel_btn fa fa-times delete-icon' title='Delete' onclick='DeleteALevel(2" + aLevel_IGCSEList[i].ID + ")'></a></td>");
                    }
                    $('.ALevel_table').append(tr);
                }
            }
        }

        if ($("#<%=IBList_HF.ClientID%>").val() != "") {
            $('#divIBTable').show();
            IBList = JSON.parse($("#<%=IBList_HF.ClientID%>").val());
            var isAr = window.location.href.toLowerCase().indexOf("ar") >= 0;
            var sumPoints = 0;

            SLCount = 0;
            HLCount = 0;
            if (IBList.length > 0) {
                for (var i = 0; i < IBList.length; i++) {
                    if (IBList[i].Level == "SL") {
                        SLCount++;
                    }
                    else if (IBList[i].Level == "HL") {
                        HLCount++;
                    }
                    var tr;
                    tr = $('<tr id="' + IBList[i].ID + '" />');
                    tr.append("<td class='text-center'>" + (parseInt(i) + 1) + "</td>");
                    if (IBList[i].Code != null) {
                        tr.append("<td class='text-center'>" + IBList[i].Code + "</td>");
                    } else {
                        tr.append("<td class='text-center'> </td>");
                    }
                    tr.append("<td class='text-center'>" + IBList[i].Title + "</td>");
                    tr.append("<td class='text-center'>" + IBList[i].Points + "</td>");
                    tr.append("<td class='text-center'>" + IBList[i].Level + "</td>");
                    if (isAr) {
                        tr.append("<td class='text-center'><a class='DeleteIBItem_btn fa fa-times delete-icon' title='حذف' onclick='DeleteIBItem(" + IBList[i].ID + ")'></a></td>");
                    } else {
                        tr.append("<td class='text-center'><a class='DeleteIBItem_btn fa fa-times delete-icon' title='Delete' onclick='DeleteIBItem(" + IBList[i].ID + ")'></a></td>");
                    }
                    $('.IB_table tbody').append(tr);
                    sumPoints += parseInt(IBList[i].Points);
                }
                $('.IB_table').append(
                    '<tfoot><tr>' +
                    '<td id="footerIbTxt" class="text-center" colspan="3">' +"<%=Resources.ITWORX_MOEHEWF_SCE.Total %>" + '</td>' +
                    '<td id="footerIb" class="text-center">' + sumPoints + '</td>' +
                    '</tr></tfoot>'
                );
            }
        }
    }

    function viewHideCert(changed) {
        var crtTypeVal = $("#<%=CerTypeClientID%>").val();
        if (crtTypeVal == 1) {
            $(".IGCSE_Div").show();
            $(".IB_Div").hide();
            if (changed == true) {
                $('.IB_table').find("tr:gt(0)").remove();
                $("#<%=IBList_HF.ClientID%>").val("");
                IBList.splice(0, IBList.length);
                $("#footerIb").remove();
                $("#footerIbTxt").remove();
                $("#<%=IBValidator.ClientID%>").css("display", "none");
            }
        }
        else if (crtTypeVal == 2) {
            $(".IB_Div").show();
            $(".IGCSE_Div").hide();
            if (changed == true) {
                $('.ALevel_table').find("tr:gt(0)").remove();
                $('.OLevel_table').find("tr:gt(0)").remove();
                $("#<%=ALevel_HF.ClientID%>").val("");
                $("#<%=OLevel_HF.ClientID%>").val("");
                aLevel_IGCSEList.splice(0, aLevel_IGCSEList.length);
                oLevel_IGCSEList.splice(0, oLevel_IGCSEList.length);
                $("#<%=CustomValidator2.ClientID%>").css("display", "none");
                $("#<%=CustomValidator1.ClientID%>").css("display", "none");
            }

        } else {
            $(".IB_Div").hide();
            $(".IGCSE_Div").hide();
            if (changed == true) {
                $('.ALevel_table').find("tr:gt(0)").remove();
                $('.OLevel_table').find("tr:gt(0)").remove();
                $('.IB_table').find("tr:gt(0)").remove();
                $("#<%=IBList_HF.ClientID%>").val("");
                $("#<%=ALevel_HF.ClientID%>").val("");
                $("#<%=OLevel_HF.ClientID%>").val("");
                aLevel_IGCSEList.splice(0, aLevel_IGCSEList.length);
                oLevel_IGCSEList.splice(0, oLevel_IGCSEList.length);
                IBList.splice(0, IBList.length);
                $("#footerIb").remove();
                $("#footerIbTxt").remove();
                $("#<%=CustomValidator2.ClientID%>").css("display", "none");
                $("#<%=CustomValidator1.ClientID%>").css("display", "none");
                $("#<%=IBValidator.ClientID%>").css("display", "none");
            }
        }
    }

    function getStudentData() {
        var isAr = window.location.href.toLowerCase().indexOf("ar") >= 0;
        var url = $("#MOIAddress_hdf").val() + $("#txt_QatarID").val();
        $.ajax({
            url: url,
            type: 'GET',
            crossDomain: true,

            success: function (result) {
                //Validate that the entered BD equal actual DB
                //debugger;
                if (result != null && result.BirthDate != $("#txt_birthDate").val()) {
                    $('.loader-overlay').show();
                    $('.loader-overlay .loader-msg img').hide();
                    $('.loader-overlay-close').show();
                    $("#lbl_NameVal").val("");
                    $("#lbl_NationalityVal").val("");
                    $("#stdGender_hf").val("")
                    if (isAr) {
                        $('.errorDes').text("البيانات المدخلة غير متطابقة مع بيانات السجلات الرسمية الرجاء التأكد من صحة الرقم الشخصي وتاريخ الميلاد");
                    }
                    else {
                        $('.errorDes').text("البيانات المدخلة غير متطابقة مع بيانات السجلات الرسمية الرجاء التأكد من صحة الرقم الشخصي وتاريخ الميلاد");
                    }
                    $('.errorDes').show("Qatari ID is not matched the entered birthdate.");

                    ClearMOIOutputFileds();

                }
                else {
                    $('.loader-overlay').hide();
                    if (result != null) {

                        if (isAr) {
                            $("#txt_Name").val(result.ArabicName);
                            $("#hdnStudentName").val(result.ArabicName);
                        } else {
                            $("#txt_Name").val(result.EnglishName);
                            $("#hdnStudentName").val(result.EnglishName);
                        }
                        //$("#txt_birthDate").val(result.BirthDate);
                        $("#hdnBirthDate").val(result.BirthDate);
                        if (result.Gender.toLowerCase() == 'm') {
                            $("#<%=ddl_Gender.ClientID%>").val("M");
                        $("#stdGender_hf").val('M');
                    } else {
                        $("#<%=ddl_Gender.ClientID%>").val("F");
                        $("#stdGender_hf").val('F');
                    }
                    $("#stdNationality_hf").val(result.Nationality);
                    $.ajax({
                        url: "/_api/web/lists/getbytitle('Nationality')/items?$select=ID,Title,TitleAr&$filter=ISOCode%20eq%20%27" + result.Nationality + "%27",
                        type: 'GET',
                        headers: {
                            "accept": "application / json;odata = verbose",
                        },
                        success: function (nat) {
                            var nationality = nat.d.results[0];
                            if (nationality != undefined) {
                                $("#<%=ddl_Nationality.ClientID%>").val(nationality.ID);
                            }
                        }
                    });
                    $("#lbl_QatarIDValidat").text("");
                } else {
                    $('.loader-overlay').show();
                    $('.loader-overlay .loader-msg img').hide();
                    $('.loader-overlay-close').show();
                    $("#lbl_NameVal").val("");
                    $("#lbl_NationalityVal").val("");
                    if (isAr) {
                        //$("#lbl_QatarIDValidat").text("برجاء إعادة أدخال الرقم الشخصى");
                        $('.errorDes').text("الرجاء إدخال الرقم الشخصي بشكل صحيح");
                        $('.errorDes').show();
                        ClearMOIOutputFileds();

                    } else {
                        //$("#lbl_QatarIDValidat").text("Please re-enter QatarID");
                        $('.errorDes').text("Please re-enter Valid QatarID");
                        $('.errorDes').show();
                        ClearMOIOutputFileds();
                    }
                    $("#txt_QatarID").val("");
                }
                }
            },
            error: function () {
                console.log("MOI Error");
                $('.loader-overlay .loader-msg img').hide();
                $('.loader-overlay-close').show();
                $("#lbl_NameVal").val("");
                $("#lbl_NationalityVal").val("");
                if (isAr) {
                    //$("#lbl_QatarIDValidat").text("برجاء إعادة أدخال الرقم الشخصى");
                    $('.errorDes').text("الخدمة غير متاحة الاّن برجاء التواصل مع الدعم الفنى لوزارة التعليم والتعليم العالى على الخط الساخن 155");
                    $('.errorDes').show();
                } else {
                    //$("#lbl_QatarIDValidat").text("Please re-enter QatarID");
                    $('.errorDes').text("Service unavailable now, please contact ministry of education support on hotline 155");
                    $('.errorDes').show();
                }
                $("#txt_QatarID").val("");
            }
        });
    }

    EnableButton();
    function ClearMOIOutputFileds() {
        $("#txt_Name").val("");
        $("#ddl_Nationality").val("");
        $("#ddl_Gender").val("");
    }
    function EnableButton() {
        if ($("#chkTermsAndConditions").is(':visible')) {
            if ($("#chkTermsAndConditions").is(':checked')) {
                $(".enableStartNextButton").removeAttr("disabled");
                if (document.getElementById("StartNextButton") != null) {
                    document.getElementById("StartNextButton").focus();
                }
            } else {
                $(".enableStartNextButton").attr("disabled", "disabled");
            }
        } else {
            $(".enableStartNextButton").removeAttr("disabled");
            if (document.getElementById("StartNextButton") != null) {
                document.getElementById("StartNextButton").focus();
            }
        }
    }

    $("#chkTermsAndConditions").change(function () {
        EnableButton();
    });

    EnableFinishButton();
    function EnableFinishButton() {
        debugger;
        if ($("#chkReviewStep").is(':visible')) {
            if ($("#chkReviewStep").is(':checked')) {
                $(".enableFinishButton").removeAttr("disabled");
                if (document.getElementById("FinishButton") != null) {
                    document.getElementById("FinishButton").focus();
                }
            } else {
                $(".enableFinishButton").attr("disabled", "disabled");
            }
        } else {
            $(".enableFinishButton").removeAttr("disabled");
            if (document.getElementById("FinishButton") != null) {
                document.getElementById("FinishButton").focus();
            }
        }
    }

    $("#chkReviewStep").change(function () {
        debugger;
        EnableFinishButton();
    });

    //certificate type
    $('#divCertificateType').find($('span[id$=spandrop]')).hide();
</script>