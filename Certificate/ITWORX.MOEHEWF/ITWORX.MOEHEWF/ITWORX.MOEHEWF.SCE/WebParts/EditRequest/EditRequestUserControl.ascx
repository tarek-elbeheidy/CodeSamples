<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditRequestUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.WebParts.EditRequest.EditRequestUserControl" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/DDLWithTXTWithNoPostback.ascx" TagPrefix="uc1" TagName="DDLWithTXTWithNoPostback" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/ClientSideFileUpload.ascx" TagPrefix="uc1" TagName="ClientSideFileUpload" %>


<asp:HiddenField ID="OLevel_HF" runat="server" />
<asp:HiddenField ID="ALevel_HF" runat="server" />
<asp:HiddenField ID="IBList_HF" runat="server" />
<asp:HiddenField ID="MOIAddress_hdf" ClientIDMode="Static" runat="server" />
<asp:HiddenField ID="stdNationality_hf" ClientIDMode="Static" runat="server" />
<asp:HiddenField ID="stdGender_hf" ClientIDMode="Static" runat="server" />
<asp:HiddenField ID="hdnBirthDate" ClientIDMode="Static" runat="server" />
<asp:HiddenField ID="hdnStudentName" ClientIDMode="Static" runat="server" />
       <div class="school-collapse dashboard">
                     
<div class="row">
    <div class="col-md-9 col-xs-9">
        <h4>
            <asp:Literal ID="ltrlSaveText" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, EditNote %>"></asp:Literal>
        </h4>
    </div>
</div>
<div class="row">
    <div class="col-xs-12">
        <h2 class="tab-title margin-top-0 margin-bottom-0">
            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, EditRequestTitle %>"></asp:Literal>
        </h2>
    </div>
</div>

<%--<div class="row">
    <div class="accordion panel-group" id="accordion">
        <div class="panel panel-default">
            <div class="panel-heading active">
                <h4 class="panel-title">
                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne">
                        <asp:Label ID="lblContactsData" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ContactsData %>"></asp:Label><em></em>
                    </a>
                </h4>
            </div>--%>
<%-- /.panel-heading --%>
<%-- <div id="collapseOne" class="panel-collapse collapse in">
                <div class="panel-body">
                    <div class="row  margin-bottom-15">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="lblApplicantName" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, NameOfficialRecords %>"></asp:Label>
                                    <span class="error-msg">*</span>
                                </label>
                                <asp:TextBox ID="txtApplicantName" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtApplicantName" ValidationGroup="Submit" runat="server" Display="Dynamic" CssClass="error-msg" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, NameOfficialRecordsValidation %>"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="lblMobileNumber" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, MobileNumber %>"></asp:Label>
                                    <span class="error-msg">*</span>
                                </label>
                                <asp:TextBox ID="txtMobileNumber" runat="server" ClientIDMode="Static" CssClass="form-control" TextMode="Number" placeholder="00974xxx"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtMobileNumber" ValidationGroup="Submit" runat="server"  Display="Dynamic" CssClass="error-msg" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, MobileNumberValidation %>"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="lblEmail" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Email %>"></asp:Label>
                                    <span class="error-msg">*</span>
                                </label>
                                <asp:TextBox ID="txtEmail" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtEmail" ValidationGroup="Submit" runat="server" CssClass="error-msg" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, EmailValidation %>" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="regEmailValidator" ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, WrongEmailAddress %>" ValidationGroup="Submit" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" runat="server" ForeColor="Red" ControlToValidate="txtEmail" Display="Dynamic"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>--%>

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
                        <div class="col-md-6" id="divQatarID" runat="server">
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="lbl_QatarID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, QatarID %>"></asp:Label><span class="error-msg">*</span>
                                </label>
                                <asp:TextBox ID="txt_QatarID" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label ID="lbl_QatarIDValidat" runat="server" ClientIDMode="Static" ForeColor="Red"></asp:Label>
                                <asp:CustomValidator ID="QatarIDValidator" runat="server" CssClass="error-msg" ClientValidationFunction="validateQatarID" ValidationGroup="Submit" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, QatarIDValidation %>"></asp:CustomValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="lbl_birthDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, BirthDate %>"></asp:Label>
                                </label>
                                <asp:TextBox ID="txt_birthDate" ClientIDMode="Static" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="valBirthDate" Visible="false" Enabled="false" ControlToValidate="txt_birthDate" ValidationGroup="Submit" runat="server" CssClass="error-msg" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, BirthdateValidation %>"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div id="passportContainer" runat="server" visible="false">
                            <div class="col-md-6">
                                <div class="form-group">

                                    <asp:Label ID="lblTempQatarID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, TempQatarID %>" Visible="false"></asp:Label>

                                    <asp:TextBox ID="txtTempQatarID" runat="server" ClientIDMode="Static" Visible="false" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">

                                    <asp:Label ID="lbl_PassPort" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Passport %>" Visible="false"></asp:Label>

                                    <asp:TextBox ID="txt_PassPort" ClientIDMode="Static" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="valPassport" Visible="false" Display="Dynamic" Enabled="false" ControlToValidate="txt_PassPort" ValidationGroup="Submit" runat="server" CssClass="error-msg" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, PassportValidation %>"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="lbl_Name" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentName %>"></asp:Label>
                                </label>
                                <asp:TextBox ID="txt_Name" ClientIDMode="Static" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="valName" Visible="false" Enabled="false" ControlToValidate="txt_Name" ValidationGroup="Submit" runat="server" CssClass="error-msg" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, NameValidation %>"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="lbl_Nationality" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentNationality %>"></asp:Label><span class="error-msg">*</span>
                                </label>
                                <asp:DropDownList ID="ddl_Nationality" runat="server" CssClass="form-control moe-dropdown" Enabled="false" ClientIDMode="Static"></asp:DropDownList>
                                <asp:Label ID="lblNationalityValidation" runat="server" ClientIDMode="Static" ForeColor="Red" CssClass="error-msg"></asp:Label>
                                
                                <asp:CustomValidator ID="NationalityValidator"  runat="server" CssClass="error-msg" ClientValidationFunction="validateNationality" ValidationGroup="Submit" ErrorMessage=""></asp:CustomValidator>
                                
                                <%--<asp:RequiredFieldValidator ControlToValidate="ddl_Nationality" runat="server" ValidationGroup="Submit" InitialValue="-1" CssClass="error-msg"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="lbl_Gender" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentGender %>"></asp:Label>
                                </label>
                                <asp:DropDownList ID="ddl_Gender" runat="server" CssClass="form-control moe-dropdown" Enabled="false" ClientIDMode="Static"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="valGender" Visible="false" Enabled="false" ControlToValidate="ddl_Gender" ValidationGroup="Submit" runat="server" CssClass="error-msg" InitialValue="-1" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, GenderValidation %>"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="lbl_NationalityCat" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentNationalityType %>"></asp:Label><span class="error-msg">*</span>
                                </label>
                                <asp:DropDownList ID="ddl_NatCat" runat="server" CssClass="form-control moe-dropdown"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="NationalityCatValidator" runat="server" ControlToValidate="ddl_NatCat" InitialValue="-1" CssClass="error-msg" ValidationGroup="Submit" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, NationalCatValidation %>"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="lbl_PrintedName" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentNameCert %>"></asp:Label><span class="error-msg">*</span>
                                </label>
                                <asp:TextBox ID="txt_PrintedName" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PrintedNameValidator" runat="server" ControlToValidate="txt_PrintedName" ForeColor="Red" ValidationGroup="Submit" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, PrintedNameValidation %>" CssClass="error-msg" Display="Dynamic"></asp:RequiredFieldValidator>
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
                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseFive">
                        <asp:Label ID="lblCertificateDetails" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateDetails %>"></asp:Label><em></em>
                    </a>
                </h4>
            </div>
            <%-- /.panel-heading --%>
            <div id="collapseFive" class="panel-collapse collapse in">
                <div class="panel-body">
                    <div class="row margin-bottom-15 ">
                        <div class="col-md-6">
                            <uc1:ddlwithtxtwithnopostback runat="server" id="certificateResource" />
                        </div>
                        <div class="col-md-6">
                            <uc1:ddlwithtxtwithnopostback runat="server" id="schooleType" />
                        </div>
                    </div>
                    <div class="row margin-bottom-15">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="lbl_PrevSchool" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, PreviousSchool %>"></asp:Label>
                                </label>
                                <asp:TextBox ID="txt_PrevSchool" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <%--<label>
                                    <asp:Label ID="lblSchoolingSystem" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SchoolingSystem %>"></asp:Label>
                                </label>--%>
                                <%--<asp:DropDownList ID="ddlSchoolingSystem" runat="server" CssClass="form-control moe-dropdown"></asp:DropDownList>--%>
                                <uc1:DDLWithTXTWithNoPostback runat="server" id="ddlSchoolingSystem" />
                            </div>
                        </div>
                    </div>
                    <div class="row margin-bottom-15">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="lbl_ScholasticLevel" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, LastSchoolYear %>"></asp:Label><span class="error-msg">*</span>
                                </label>
                                <asp:DropDownList ID="ddl_ScholasticLevel" runat="server" CssClass="form-control moe-dropdown"></asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="ddl_ScholasticLevel" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, LastSchoolYearValidation %>" ValidationGroup="Submit" InitialValue="-1" runat="server" class="error-msg"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
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
                    <div class="row margin-bottom-15">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="lblEquiPurpose" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, EquivalencyPurpose %>"></asp:Label><span class="error-msg">*</span>
                                </label>
                                <asp:DropDownList ID="ddlEquiPurpose" runat="server" CssClass="form-control moe-dropdown"></asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="ddlEquiPurpose" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, EquivalencyPurposeValidation %>" ValidationGroup="Submit" InitialValue="-1" runat="server" class="error-msg"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="lbl_GoingToClass" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, GoToClass %>"></asp:Label><span class="error-msg">*</span>
                                </label>
                                <asp:DropDownList ID="ddl_GoingToClass" runat="server" CssClass="form-control moe-dropdown"></asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="ddl_GoingToClass" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, GoToClassValidation %>" ValidationGroup="Submit" InitialValue="-1" runat="server" class="error-msg"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row margin-bottom-15 " id="divCertificateType">
                        <div class="col-md-6">
                            <uc1:ddlwithtxtwithnopostback runat="server" id="certificateType" />
                            <asp:CustomValidator ID="valCertificateType" runat="server" ClientValidationFunction="validateCertificateType" OnServerValidate="valCertificateType_ServerValidate" Display="Dynamic" ValidationGroup="Submit" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, certificateTypeValidation %>" CssClass="error-msg"></asp:CustomValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div>
                            <div class="IGCSE_Div " style="display: none">
                                <%--<asp:CustomValidator ID="OlevelValidator" runat="server" ClientValidationFunction="validateOlevel" OnServerValidate="serverValidateOlevel" ValidationGroup="Submit" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, OLevelValidation %>"></asp:CustomValidator>--%>
                                <div class="OLevel_Div  margin-top-25">
                                    <h4 class="subject-header IGCSE-subject-header">
                                        <asp:Label ID="lblOLevel" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, OLevelMessage %>"></asp:Label>
                                        <span class="error-msg">*</span>
                                    </h4>
                                    <div class="row margin-top-15 IGCSE-fields">
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
                                                <asp:Button runat="server" CssClass="addOLevel_btn btn moe-btn" type="button" Text="<%$Resources:ITWORX_MOEHEWF_SCE, OLevelAdd %>" />
                                                <%--<input type="button" class="addOLevel_btn btn moe-btn" value="إضافة" />--%>
                                            </div>
                                        </div>
                                    </div>

                                    <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="validateOlevel" OnServerValidate="serverValidateOlevel" ValidationGroup="Submit" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, OLevelValidation %>" CssClass="error-msg"></asp:CustomValidator>
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
                                    <h4 class="subject-header  IGCSE-subject-header">
                                        <asp:Label ID="lblALevel" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ALevelMessage %>"></asp:Label>
                                        <span class="error-msg">*</span>
                                    </h4>
                                    <div class="row IGCSE-fields">

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
                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="validateAlevel" OnServerValidate="serverValidateAlevel" ValidationGroup="Submit" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, ALevelValidation %>" CssClass="error-msg"></asp:CustomValidator>
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
                                <h4 class="subject-header  IB-subject-header">
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
                                    <div class="col-md-2 col-sm-12 col-xs-12 text-right">
                                        <div class="form-group">
                                            <label class="visibility-hidden">المعدل</label>
                                            <asp:Button runat="server" CssClass="addIB_btn btn moe-btn" type="button" Text="<%$Resources:ITWORX_MOEHEWF_SCE, OLevelAdd %>" />
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <asp:CustomValidator ID="IBValidator" runat="server" ClientValidationFunction="validateIB" OnServerValidate="serverValidateIB" ValidationGroup="Submit" CssClass="error-msg IB-fields" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, IBSubjectValidation %>"></asp:CustomValidator>
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
                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseSeven">
                        <asp:Label ID="Label16" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, AcademicData %>"></asp:Label><em></em>
                    </a>
                </h4>
            </div>
            <div id="collapseSeven" class="panel-collapse collapse in">
                <div class="panel-body">
                    <div class="row margin-top-15">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="lblPassedYears" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, PassedYearsTitle %>"></asp:Label><span class="error-msg">*</span>
                                </label>
                                <asp:TextBox ID="txtPassedYears" runat="server" CssClass="form-control" Width="40%" TextMode="Number" min="0" MaxLength="2" onkeypress="restrictMinus(event);" oninput="javascript: if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqPassedYears" runat="server" ControlToValidate="txtPassedYears" ForeColor="Red" ValidationGroup="Submit" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, PassedYearsValidation %>" CssClass="error-msg"></asp:RequiredFieldValidator>
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
                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseSeven1">
                        <asp:Label ID="Label1" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Attachments %>"></asp:Label>
                        <em></em>
                    </a>
                </h4>
            </div>
            <div id="collapseSeven1" class="panel-collapse collapse in">
                <div class="panel-body">
                    <div class="row fileUploadContainer">
                        <div class="col-md-6 col-sm-6 col-xs-8 margin-bottom-10">
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="lblFileName" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequiredFiles %>"></asp:Label>
                                    <asp:Label ID="lbldropFileUpload" runat="server" CssClass="error-msg" Style="display: none;">*</asp:Label>
                                </label>
                                <%--<asp:DropDownList ID="dropFileUpload" runat="server" CssClass="form-control moe-dropdown"></asp:DropDownList>--%>
                                <uc1:DDLWithTXTWithNoPostback runat="server" id="dropFileUpload" />

                                <asp:Label ID="lblRequiredDrop" runat="server" Style="display: none;" ForeColor="Red" Text="<%$Resources:ITWORX_MOEHEWF_SCE, FileNameValidation %>"></asp:Label>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                       <uc1:ClientSideFileUpload runat="server" id="FileUp1" />
                        <%--   <uc1:ClientSideFileUpload runat="server" id="FileUp2" Visible="false" />
                          <uc1:ClientSideFileUpload runat="server" id="FileUp3" Visible="false"  />
                          <uc1:ClientSideFileUpload runat="server" id="FileUp4" Visible="false"  />
                          <uc1:ClientSideFileUpload runat="server" id="FileUp5"  Visible="false"/>
                          <uc1:ClientSideFileUpload runat="server" id="FileUp6"  Visible="false" />
                          <uc1:ClientSideFileUpload runat="server" id="FileUp7"  Visible="false" />
                          <uc1:ClientSideFileUpload runat="server" id="FileUp8"  Visible="false" />
                          <uc1:ClientSideFileUpload runat="server" id="FileUp9"  Visible="false" />
                          <uc1:ClientSideFileUpload runat="server" id="FileUp10"  Visible="false" />
                          <uc1:ClientSideFileUpload runat="server" id="FileUp11"  Visible="false" />
                       --%>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<div class="row">
    <%--<asp:ValidationSummary ID="ValidationSummary1" Enabled="true" runat="server" ValidationGroup="Submit" ShowSummary="true" ShowValidationErrors="true" CssClass="validation-summary" />--%>
</div>

<div class="row saveClarBtn margin-bottom-15">
    <div class="col-md-6 text-left">
        <asp:Button ID="btnBack" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Back %>" OnClick="btnBack_Click" />
    </div>
    <div class="col-md-6 text-right">
        <asp:Button ID="btnFinishButton" runat="server" ValidationGroup="Submit" CommandName="MoveComplete" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Finish %>" OnClick="btnFinishButton_Click" CssClass="btn moe-btn" />
    </div>
</div>
</div>

       
<form>

    </form>
<script>
  
    var Group;
    var MaxFileNumbers;
    var MaxFileNumbersOther;
    <%--function BindAttachmentProperties()
    {
        var certificateTypeId = $('#<%=certificateType.Client_ID%>').val();
        var natCatId = $('#<%=ddl_NatCat.ClientID%>').val();
        var countryId = $('#<%=certificateResource.Client_ID%>').val();
        var goingClassId = $('#<%=ddl_GoingToClass.ClientID%>').val();
        debugger
    
        var dropClientID = "<%= DropClientId %>" ;
        var textBoxClientID =  "<%= TextBoxClientID %>";
        var reqDropClientID = "<%= ReqDropClientID %>";
        var labelRequiredDrop = "<%= LabelRequiredDrop %>";
        var language = _spPageContextInfo.currentLanguage;
        
        var param = {};
        param.certificateTypeId = certificateTypeId;
        param.natCatId = natCatId;
        param.countryId = countryId;
        param.goingClassId = goingClassId;
        param.requestId = requestId;
        param.dropClientID = dropClientID;
        param.textBoxClientID = textBoxClientID;
        param.reqDropClientID = reqDropClientID;
        param.labelRequiredDrop = labelRequiredDrop;
        param.language = language;

       
        $.ajax({
            type: "POST",
            url: _spPageContextInfo.siteAbsoluteUrl + "/_layouts/15/ITWORX.MOEHEWF.SCE/ChangeFUProperties.aspx/ChangeProperties",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(param),
            async: false,
            dataType: "json",
            success: function (/*data*/) {
                console.log("Hiiiiii");
                alert("Success");
                debugger;
                $("#ctl00_ctl58_g_173ff186_4584_46a8_8c6f_4a0ced1d38eb_ctl00_FileUp1_hdnGrp").val("<%= Group %>");
            },
            error: function (xhr, status, error) {
                alert(xhr.status);
                alert(xhr.responseText);
            }
        });
    }--%>

    var oLevel_IGCSEList = [];
    var aLevel_IGCSEList = [];
    var IBList = [];
    var SLCount = 0;
    var HLCount = 0;
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
        debugger;
        $("#<%=certificateType.Client_ID%>").val("-1");
        
    }

    $("#<%=ddl_ScholasticLevel.ClientID%>").change(function () {
        var lastClass = $("#<%=ddl_ScholasticLevel.ClientID%>").val();
        if (lastClass >= 10 && lastClass <= 13) {
            $('#divCertificateType').show();
        } else {
            $('#divCertificateType').hide();
            debugger;
            $("#<%=certificateType.Client_ID%>").val("-1");
            $(".IB_Div").hide();
            $(".IGCSE_Div").hide();
        }
        if (lastClass == 13) {
            $("#divCertificateType .form-group .astrik").show();
        } else {
            $("#divCertificateType .form-group .astrik").hide();
        }
    });

    $('#<%=certificateResource.Client_ID%>').change(function () {
        debugger;
       <%-- if ($('#<%=certificateType.Client_ID%>').val()!="" && $('#<%=certificateType.Client_ID%>').val() != "-1") {--%>
            BindAttachmentDDL();
            BindFileUploadProperties();
        //}
    });

    $('#<%=certificateType.Client_ID%>').change(function () {
        BindAttachmentDDL();
        BindFileUploadProperties();
        $('span[id$=custRequiredFile]').hide();
    });

    $('#<%=ddl_GoingToClass.ClientID%>').change(function () {
        debugger;
        BindAttachmentDDL();
        BindFileUploadProperties();
        $('span[id$=custRequiredFile]').hide();
    });

    $('#<%=ddl_ScholasticLevel.ClientID%>').change(function () {
        BindAttachmentDDL();
        BindFileUploadProperties();
        $('span[id$=custRequiredFile]').hide();
    });

    $('#<%=ddl_NatCat.ClientID%>').change(function () {
      
        BindAttachmentDDL();
        BindFileUploadProperties();
        $('span[id$=custRequiredFile]').hide();
    });


    $("#<%=schooleType.Client_ID%>").change(function () {
        
        BindAttachmentDDL();
        BindFileUploadProperties();
        $('span[id$=custRequiredFile]').hide();
       
    });

 
    function GetCertificateTypeTitle() {
        var certType = "";
        $.ajax({
            
            url: "/_api/web/lists/getbytitle('CertificateType')/items(" + $("#<%=certificateType.Client_ID%>").val() + ")",
            type: 'GET',
            async: false,
            headers: { "accept": "application / json;odata = verbose" },
            success: function (data) {
                debugger;

                certType = data.d.Title;



            },
            error: function ajaxError(response) {
                console.log(response.status + ' ' + response.statusText);
                //alert(response.status + ' ' + response.statusText);
            }
        });
        return certType;
    }
    function GetNationalityCategoryTitle() {
        var natCat = "";
        $.ajax({

            url: "/_api/web/lists/getbytitle('NationalityCategory')/items(" + $("#<%=ddl_NatCat.ClientID%>").val() + ")",
            type: 'GET',
            async: false,
            headers: { "accept": "application / json;odata = verbose" },
            success: function (data) {
                debugger;

                natCat = data.d.Title;



            },
            error: function ajaxError(response) {
                console.log(response.status + ' ' + response.statusText);
                //alert(response.status + ' ' + response.statusText);
            }
        });
        return natCat;
      }
    function BindAttachmentDDL() {
        debugger;
        //if class to register is "Not available" and scholastic level is 13
        if ($('#<%=ddl_GoingToClass.ClientID%>').val() == "14" && $('#<%=certificateType.Client_ID%>').val()!="-1" ) {
            var countryId = $('#<%=certificateResource.Client_ID%>').val();
            $.ajax({
                //url: "/_api/web/lists/getbytitle('CountryOfStudy')/items?$select=ID,MOEHECountryType&$filter=ID eq '" + countryId + "'",
                url: "/_api/web/lists/getbytitle('CountryOfStudy')/items(" + countryId + ")",
                type: 'GET',
                async: false,
                headers: { "accept": "application / json;odata = verbose" },
                success: function (countryOfStudy) {
                    debugger;
                    var countryType = countryOfStudy.d.MOEHECountryType;
                    var certificateTypeId = $('#<%=certificateType.Client_ID%>').val();
                    $.ajax({
                        url: "/_api/web/lists/getbytitle('FileName')/items?$select=ID,MOEHECountryType,Title,TitleAr,CertificateType/Id&$expand=CertificateType&$filter=MOEHECountryType eq '" + countryType + "' and CertificateType/Id eq '" + certificateTypeId + "'",
                        type: 'GET',
                        async:false,
                        headers: { "accept": "application / json;odata = verbose" },
                        success: function (fileNameList) {
                            debugger;
                            if (fileNameList.d.results.length == 0) {

                                console.log('No items on the list');
                            } else {
                                $('#<%=dropFileUpload.Client_ID%>').empty();
                                
                               
                                //check  if certificate type is high school, country is arab and school type is not private 
                                //in order to remove the certificate from country option
                                debugger;
                                if ($("#<%=certificateType.Client_ID%> option:selected").text() == "<%=Resources.ITWORX_MOEHEWF_SCE.HighSchool %>"
                                    && countryType == "Arab") {
                                    if ($("#<%=schooleType.Client_ID%>").val() != "-2" && $("#<%=schooleType.Client_ID%> option:selected").text() == "<%=Resources.ITWORX_MOEHEWF_SCE.Private %>") {
                                        Group = GetCertificateTypeTitle() + countryType + "Private";
                                    }
                                    else {
                                        var fileIndex = fileNameList.d.results.findIndex(obj => obj.Title == "<%=Resources.ITWORX_MOEHEWF_SCE.CertificateCopyFromCountry %>" || obj.TitleAr == "<%=Resources.ITWORX_MOEHEWF_SCE.CertificateCopyFromCountry %>");
                                        fileNameList.d.results.splice(fileIndex, 1);
                                        Group = GetCertificateTypeTitle() + countryType;
                                    }
                                }
                                else {
                                     <%--$("#<%=certificateType.Client_ID%> option:selected").text()--%>
                                    Group =GetCertificateTypeTitle() + countryType;
                                }
                                    

                                if (_spPageContextInfo.currentLanguage == 1033) {
                                    $('#<%=dropFileUpload.Client_ID%>').append("<option value='-1'>Select</option>");
                                    $.each(fileNameList.d.results, function (i, fileName) {
                                        $('#<%=dropFileUpload.Client_ID%>').append("<option value='" + fileName.Id + "'>" + fileName.Title + "</option>");
                                    });
                                    <%--$('#<%=dropFileUpload.Client_ID%>').append("<option value='-2'>Other</option>");--%>
                                 
                                    <%--Group = $("#<%=certificateType.Client_ID%> option:selected").text() + countryType;--%>
                                    
                                } else {
                                    debugger;
                                    $('#<%=dropFileUpload.Client_ID%>').append("<option value='-1'>اختار</option>");
                                    $.each(fileNameList.d.results, function (i, fileName) {
                                        $('#<%=dropFileUpload.Client_ID%>').append("<option value='" + fileName.Id + "'>" + fileName.TitleAr + "</option>");
                                    });
                                   <%-- $('#<%=dropFileUpload.Client_ID%>').append("<option value='-2'>أخرى</option>");--%>
                                  <%-- Group = $("#<%=certificateType.Client_ID%> option:selected").text() + countryType;--%>
                                }
                                $('#<%=dropFileUpload.Client_ID%>').append("<option value='-2'><%=Resources.ITWORX_MOEHEWF_SCE.OtherDocuments %></option>");
                                debugger;
                                MaxFileNumbers = fileNameList.d.results.length;
                            }
                        },
                        error: function (error) {
                            console.log(JSON.stringify(error));
                        }
                    });
                },
                error: function (error) {
                    console.log(JSON.stringify(error));
                }
            });
        } else {
            debugger;
            //check the nationality category ddl value
            var nationalityCatId = $('#<%=ddl_NatCat.ClientID%>').val();
            $.ajax({
                url: "/_api/web/lists/getbytitle('FileName')/items?$select=ID,Title,TitleAr,NationalityCategory/Id&$expand=NationalityCategory&$filter=NationalityCategory/Id eq '" + nationalityCatId + "'",
                type: 'GET',
                async: false,
                headers: { "accept": "application / json;odata = verbose" },
                success: function (fileNameList) {
                    if (fileNameList.d.results.length == 0) {
                        console.log('No items on the list');
                    } else {
                        debugger;
                        $('#<%=dropFileUpload.Client_ID%>').empty();
                        if (_spPageContextInfo.currentLanguage == 1033) {
                            $('#<%=dropFileUpload.Client_ID%>').append("<option value='-1'>Select</option>");
                            $.each(fileNameList.d.results, function (i, fileName) {
                                $('#<%=dropFileUpload.Client_ID%>').append("<option value='" + fileName.Id + "'>" + fileName.Title + "</option>");
                            });
                           <%-- $('#<%=dropFileUpload.Client_ID%>').append("<option value='-2'>Other</option>");--%>
                           
                            Group = GetNationalityCategoryTitle();<%--$("#<%=ddl_NatCat.ClientID%> option:selected").text();--%>
                        } else {
                            $('#<%=dropFileUpload.Client_ID%>').append("<option value='-1'>اختار</option>");
                            $.each(fileNameList.d.results, function (i, fileName) {
                                $('#<%=dropFileUpload.Client_ID%>').append("<option value='" + fileName.Id + "'>" + fileName.TitleAr + "</option>");
                            });
                        <%--    $('#<%=dropFileUpload.Client_ID%>').append("<option value='-2'>أخرى</option>");--%>
                            Group = GetNationalityCategoryTitle();<%-- $("#<%=ddl_NatCat.ClientID%> option:selected").text();--%>
                        } 
                        $('#<%=dropFileUpload.Client_ID%>').append("<option value='-2'><%=Resources.ITWORX_MOEHEWF_SCE.OtherDocuments %></option>");
                        MaxFileNumbers = fileNameList.d.results.length;
                    }
                },
                error: function (error) {
                    console.log(JSON.stringify(error));
                }
            });
        }
    }
    function BindFileUploadProperties() {
        debugger;
        
      
       $('input[id$=hdnGrp]').val(Group);
       $('input[id$=hdnMaxFileNo]').val(MaxFileNumbers);
      
       
       if (_spPageContextInfo.currentLanguage == 1033) {
           
           $(".fileNumbers").text("You can't upload more than " + MaxFileNumbers + " ملف");
        }
        else {
           $(".fileNumbers").text(" لايمكن رفع اكثر من" + MaxFileNumbers + " ملف");
       }

        //check if there is sth exist in this group

       var requestId = <%= FileUp1.LookupFieldValue %>;
       var results;
       var requestUri =
           "<%= FileUp1.DocLibWebUrl  %>/_api/web/lists/getByTitle('<%= FileUp1.DocumentLibraryName %>')/items?$select=ID&$filter=(MOEHEDocumentGroup eq '" + Group + "' or MOEHEDocumentGroup eq '" + Group + "Other" + "') and  <%= FileUp1.LookupFieldName %> eq " + requestId;


        requestUri = encodeURI(requestUri);

        var requestHeaders = {
            "accept": "application/json;odata=verbose"
        }

        $.ajax({
            url: requestUri,
            type: 'GET',
            dataType: 'json',
            headers: requestHeaders,
            async: false,
            success: function (data) {
                debugger;
                //results = data.d.results;
                if (data.d.results.length == 0) {
                    $(".attach-cntnr table").empty();
                    $(".attach-cntnr").empty();
                    $('input[id$=hdnUploadCount]').val(0);
                    $('input[id$=hdnUploadCountOther]').val(0);
                    //$("#tableAttach").hide();
                }
               
            },
            error: function (data) {
                console.log("failed");
            }



        });



        debugger;
     
        var requestId = <%= FileUp1.LookupFieldValue %>;
        var results;
        var requestUri =
            "<%= FileUp1.DocLibWebUrl %>/_api/web/lists/getByTitle('<%= FileUp1.DocumentLibraryName %>')/items?$select=ID&$filter=(MOEHEDocumentGroup ne '" + Group + "' and MOEHEDocumentGroup ne '" + Group + "Other" + "') and  <%= FileUp1.LookupFieldName %> eq " + requestId;


        requestUri = encodeURI(requestUri);

        var requestHeaders = {
            "accept": "application/json;odata=verbose"
        }

        $.ajax({
            url: requestUri,
            type: 'GET',
            dataType: 'json',
            headers: requestHeaders,
            async: false,
            success: function (data) {
                debugger;
                results = data.d.results;

            },
            error: function (data) {
                console.log("failed");
            }



        });

        for (i = 0; i < results.length; i++) {

            var requestUri =
                "<%= FileUp1.DocLibWebUrl  %>/_api/web/lists/getByTitle('<%= FileUp1.DocumentLibraryName %>')/items(" + results[i].Id + ")",

                requestUri = encodeURI(requestUri);
            $.ajax({
                url: requestUri,
                type: "POST",
                contentType: "application/json;odata=verbose",
                headers: {
                    "Accept": "application/json;odata=verbose",
                    "X-RequestDigest": $("#__REQUESTDIGEST").val(),
                    "IF-MATCH": "*",
                    "X-HTTP-Method": "DELETE",
                },
                success: function (data) {
                    debugger;
                    console.log("success");

                },
                error: function (data) {
                    console.log("failed");
                }
            });
        }
    }

    $(function () {
        debugger;
        LoadForm();
        $("#txt_QatarID").blur(function () {
            getStudentData();
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
            viewHideCert();
        });
    });

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
        $("#<%=ALevel_HF.ClientID%>").val(JSON.stringify(aLevel_IGCSEList)); debugger;
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
        $('table.IB_table tr#' + ID).remove();
        for (var i = 0; i < IBList.length; i++) {
            if (IBList[i].ID == ID)
                IBList.splice($.inArray(IBList[i], IBList), 1);
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
        if (crtTypeVal == 1 && (parseInt(aLevel_IGCSEList.length) < 2)) {
            arguments.IsValid = false;
        }
    }

    function validateOlevel(sender, arguments) {
        var crtTypeVal = $("#<%=CerTypeClientID%>").val();
        if (crtTypeVal == 1 && (parseInt(oLevel_IGCSEList.length) < 5)) {
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
            if ((SLCount == 4 && HLCount == 2) ||( SLCount == 3 && HLCount == 3)) {
                arguments.IsValid = true;
            }

            else {
                arguments.IsValid = false;
            }
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
        viewHideCert();
        if ($("#<%=OLevel_HF.ClientID%>").val() != "") {
            $('#divIGolevelTable').show();
            oLevel_IGCSEList = JSON.parse($("#<%=OLevel_HF.ClientID%>").val());
            var isAr = window.location.href.toLowerCase().indexOf("ar") >= 0;
            if (oLevel_IGCSEList.length > 0) {
                for (var i = 0; i < oLevel_IGCSEList.length; i++) {
                    var tr;
                    tr = $('<tr id="' + oLevel_IGCSEList[i].ID + '" />');
                    tr.append("<td class='text-center'>" + (parseInt(i) + 1) + "</td>");
                    if (oLevel_IGCSEList[i].Code != null) {
                        tr.append("<td class='text-center'>" + oLevel_IGCSEList[i].Code + "</td>");
                    } else {
                        tr.append("<td class='text-center'> </td>");
                    }
                    tr.append("<td class='text-center'>" + oLevel_IGCSEList[i].Title + "</td>");
                    tr.append("<td class='text-center'>" + oLevel_IGCSEList[i].Avrage + "</td>");
                    if (isAr) {
                        tr.append("<td class='text-center'><a class='DeleteOLevel_btn fa fa-times delete-icon' title='حذف' onclick='DeleteOLevel(" + oLevel_IGCSEList[i].ID + ")'></a></td>");
                    } else {
                        tr.append("<td class='text-center'><a class='DeleteOLevel_btn fa fa-times delete-icon' title='Delete' onclick='DeleteOLevel(" + oLevel_IGCSEList[i].ID + ")'></a></td>");
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
                    tr = $('<tr id="' + aLevel_IGCSEList[i].ID + '" />');
                    tr.append("<td class='text-center'>" + (parseInt(i) + 1) + "</td>");
                    if (aLevel_IGCSEList[i].Code != null) {
                        tr.append("<td class='text-center'>" + aLevel_IGCSEList[i].Code + "</td>");
                    } else {
                        tr.append("<td class='text-center'> </td>");
                    }
                    tr.append("<td class='text-center'>" + aLevel_IGCSEList[i].Title + "</td>");
                    tr.append("<td class='text-center'>" + aLevel_IGCSEList[i].Avrage + "</td>");
                    if (isAr) {
                        tr.append("<td class='text-center'><a class='DeleteALevel_btn fa fa-times delete-icon' title='حذف' onclick='DeleteALevel(" + aLevel_IGCSEList[i].ID + ")'></a></td>");
                    } else {
                        tr.append("<td class='text-center'><a class='DeleteALevel_btn fa fa-times delete-icon' title='Delete' onclick='DeleteALevel(" + aLevel_IGCSEList[i].ID + ")'></a></td>");
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
                    '<td id="footerIbTxt" class="text-center" colspan="3">' + "<%=Resources.ITWORX_MOEHEWF_SCE.Total %>" + '</td>' +
                    '<td id="footerIb" class="text-center">' + sumPoints + '</td>' +
                    '</tr></tfoot>'
                );
            }
        }
    }
    function validateQatarID(sender, argument) {
        if ($("#txt_QatarID").val() == "" || $("#txt_birthDate").val() == "" || $("#txt_Name").val() == "" || $("#ddl_Nationality").val() == "-1" || $("#ddl_Gender").val() == "-1") {
            $("#lbl_QatarIDValidat").text("");
            argument.IsValid = false;
        }
    }
    function viewHideCert() {
        var crtTypeVal = $("#<%=CerTypeClientID%>").val();
        if (crtTypeVal == 1) {
            $(".IGCSE_Div").show();
            $(".IB_Div").hide();
        }
        else if (crtTypeVal == 2) {
            $(".IB_Div").show();
            $(".IGCSE_Div").hide();
        }
        else {
            $(".IB_Div").hide();
            $(".IGCSE_Div").hide();
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
                if (result != null) {
                    if (isAr) {
                        $("#txt_Name").val(result.ArabicName);
                        $("#hdnStudentName").val(result.ArabicName);
                    } else {
                        $("#txt_Name").val(result.EnglishName);
                        $("#hdnStudentName").val(result.EnglishName);
                    }
                    $("#txt_birthDate").val(result.BirthDate);
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
                    $("#lbl_NameVal").val("");
                    $("#lbl_NationalityVal").val("");
                     
                    $("#txt_QatarID").val("");
                }
            },
            error: function () {
                console.log("MOI Error");
                $("#lbl_NameVal").val("");
                $("#lbl_NationalityVal").val("");
                 
                $("#txt_QatarID").val("");
            }
        });
    }
    $(document).ready(function () {
        BindAttachmentDDL();
        BindFileUploadProperties();
    });
</script>
