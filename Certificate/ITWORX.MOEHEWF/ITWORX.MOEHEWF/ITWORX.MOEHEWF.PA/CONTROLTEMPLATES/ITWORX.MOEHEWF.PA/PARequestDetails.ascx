<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/DropdownWithTextbox.ascx" TagPrefix="uc1" TagName="DropdownWithTextbox" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="uc1" TagName="FileUpload" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PARequestDetails.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.PARequestDetails" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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

    html[dir="rtl"] span[id*='PARequestDetails_lbl_ErrorMessage'] {
        border-right: solid 5px #cc0000;
        border-left: 0;
        background: url(/_catalogs/masterpage/MOEHE/common/img/warning.png) right 0 no-repeat #ffe3e3 !important;
    }

    html[dir="ltr"].span[id*='PARequestDetails_lbl_ErrorMessage'] {
        border-left: solid 5px #cc0000;
        border-right: 0;
        background: url(/_catalogs/masterpage/MOEHE/common/img/warning.png) left 0 no-repeat #ffe3e3 !important;
    }

    .request-details .validation-summary span[id*='PARequestDetails_lbl_ErrorMessage'] {
        background-position: right 0 !important;
        margin: 20px -2%;
        background-size: 40px 40px !important;
        padding: 10px 40px;
        display: block;
    }

    span.FancyBox {
        color: #a40136;
    }

    #ddlUniversity h6 {
        margin-top: 0 !important;
        margin-bottom: 0 !important
    }
</style>
<div class="request-details">
    <asp:Panel ID="pnlformControls" runat="server">
        <div class="row margin-2">
            <div class="col-md-12 margin-top-15 margin-bottom-25 sm-width-90 col-xs-12">
                <h5 class="font-size-18 margin-bottom-0 margin-top-0 instruction-details underline color-black font-family-sans">
                    <asp:Label ID="Label6" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, AttachmentNote %>"></asp:Label>
                </h5>
            </div>
        </div>

        <div id="divPARequestDetails" class="row heighlighted-section margin-bottom-50 flex-display flex-wrap">

            <div class="col-md-6 col-sm-6">
                <div class="data-container">
                    <h6 class="font-size-16 margin-bottom-15">
                        <asp:Label ID="lblRequestNumber" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, RequestNumber %>"></asp:Label>
                    </h6>
                    <h5 class="font-size-22">

                        <asp:Label ID="lblRequestNumberValue" runat="server"></asp:Label>
                    </h5>
                </div>
            </div>

            <div class="col-md-6 col-sm-6">
                <div class="data-container">
                    <h6 class="font-size-16 margin-bottom-15">
                        <asp:Label ID="lblRequestCreationDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, RequestCreationDate %>"></asp:Label>
                    </h6>
                    <h5 class="font-size-22">
                        <asp:Label ID="lblRequestCreationDateValue" runat="server"></asp:Label>
                    </h5>
                </div>
            </div>
        </div>

        <div class="row unheighlighted-section margin-2">
            <!--Start of Accordion-->
            <div class="PAaccordion accordion">

                <!--Start of Third Accordion-->
                <h3>
                    <!--btnPreviousCertificates-->
                    <a class="popdate accordion-input"><%=Resources.ITWORX_MOEHEWF_PA.WantedCertificateDegree %></a>
                    <%--  <asp:Button ID="btnPreviousCertificates" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, WantedCertificateDegree %>" CssClass="popdate accordion-input" />--%>
                </h3>
                <div>
                    <!--Start of pnlPreviousCertificates-->
                    <asp:Panel ID="pnlPreviousCertificates" runat="server">

                        <!--Start of divUniversities-->
                        <div id="divUniversities" class="margin-top-25 container">

                            <div class="row margin-bottom-15">
                                <div class="col-md-4 col-xs-12 no-padding ">
                                    <div class="data-container table-display moe-width-85">
                                        <div class="form-group">
                                            <h6 class="font-size-16 margin-bottom-0 margin-top-0 ">
                                                <asp:Label ID="lblWantedCertificateDegree" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, WantedAcademicDegree %>"></asp:Label>
                                                <span class="astrik error-msg">*</span>
                                            </h6>

                                            <div class="form">
                                                <asp:DropDownList ID="ddlWantedCertificateDegree" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42 moe-dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlWantedCertificateDegree_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqWantedCertificateDegree" runat="server" CssClass="moe-full-width error-msg" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RequiredDegreeDetails %>" ValidationGroup="Submit" ControlToValidate="ddlWantedCertificateDegree" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 col-xs-12 no-padding">
                                    <div class="data-container table-display moe-width-85">
                                        <div class="form-group">
                                            <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                <asp:Label ID="lblCalcSectionCountry" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, Country %>"></asp:Label>
                                                <span class="astrik error-msg">*</span>
                                            </h6>

                                            <div class="form">
                                                <div class="uc-dropdown  uc-with-title">
                                                    <asp:TextBox ID="txtCountriesValue" runat="server" ReadOnly="true" CssClass="moe-full-width input-height-42 border-box moe-input-padding disabled"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-4 col-xs-12 no-padding">
                                    <div class="data-container table-display moe-width-85">
                                        <div class="form-group">
                                            <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                <asp:Label ID="lblCalcSectionUniversity" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, UniversityName %>"></asp:Label>
                                                <span class="astrik error-msg">*</span>
                                            </h6>
                                            <div class="form">
                                                <div class="uc-dropdown  uc-with-title">

                                                    <asp:TextBox ID="txtUniversityValue" runat="server" ReadOnly="true" CssClass="moe-full-width input-height-42 border-box moe-input-padding disabled"></asp:TextBox>
                                                    <asp:HiddenField ID="hdnUniversityID" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row margin-bottom-15">

                                <div class="col-md-4 col-xs-12 no-padding">
                                    <div class="data-container table-display moe-width-85">
                                        <div class="form-group">
                                            <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                <asp:Label ID="lblCalcSectionFaculty" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, FacultyName %>"></asp:Label>
                                            </h6>

                                            <div class="form">
                                                <div class="uc-dropdown uc-with-title">

                                                    <div id="calcFaculty">
                                                        <asp:TextBox ID="ddlCalcSectionFaculty" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 col-xs-12 no-padding">
                                    <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                        <div class="form-group">
                                            <div class="form uc-dropdown">
                                                <div id="requiredSpecialization">
                                                    <uc1:DropdownWithTextbox runat="server" id="ddlProgramSpecialization" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 col-xs-12 no-padding">
                                    <div class="data-container table-display moe-width-85">
                                        <div class="form-group">
                                            <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                <asp:Label runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, StudySystem %>"></asp:Label>
                                                <span class="astrik error-msg">*</span>
                                            </h6>

                                            <div class="form">

                                                <asp:DropDownList ID="dropCalcSectionStudyingSystem" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42 moe-dropdown"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="StudyingSystemValidator" runat="server" CssClass="moe-full-width error-msg" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RequiredStudySystem %>" ValidationGroup="Submit" ControlToValidate="dropCalcSectionStudyingSystem" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row margin-bottom-15">
                                <div class="col-md-4 col-xs-12 no-padding">
                                    <div class="data-container table-display moe-width-85">
                                        <div class="form-group">
                                            <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                <asp:Label ID="lblActualStudyPeriod" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, AcademicNumberOfYears %>"></asp:Label>
                                                <span class="astrik error-msg">*</span>
                                            </h6>

                                            <div class="form">
                                                <asp:TextBox ID="txtCalcSectionAcademicNumberOfYears" runat="server" TextMode="Number" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                                                <asp:RangeValidator ID="regAcademicNumberOfYears" Text="<%$Resources:ITWORX_MOEHEWF_PA, RangeAcademicNumberOfYears %>" runat="server" CssClass="error-msg moe-full-width" ControlToValidate="txtCalcSectionAcademicNumberOfYears" MinimumValue="1" MaximumValue="99" ValidationGroup="Submit" Display="Dynamic" />
                                                <asp:RequiredFieldValidator ID="rqrdAcademicNumberOfYears" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RequiredAcademicNumberOfYears  %>" ControlToValidate="txtCalcSectionAcademicNumberOfYears" runat="server" CssClass="error-msg moe-full-width" ValidationGroup="Submit" Display="Dynamic" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 col-xs-12 no-padding">
                                    <div class="data-container table-display moe-width-85">
                                        <div class="form-group">
                                            <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                <asp:Label ID="lblAcademicStartDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, AcademicStartDate %>"></asp:Label>
                                                <span class="astrik error-msg">*</span>
                                            </h6>

                                            <div class="form">
                                                <input type="text" id="dtAcademicStartDate" readonly="readonly" runat="server" class="moe-full-width moe-input-padding moe-select input-height-42">
                                                <asp:RequiredFieldValidator ID="rqrddtAcademicStartDate" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RequiredAcademicStartDate  %>" ControlToValidate="dtAcademicStartDate" runat="server" CssClass="error-msg moe-full-width" ValidationGroup="Submit" Display="Dynamic" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-4 col-xs-12 no-padding">
                                    <div class="data-container table-display moe-width-85">
                                        <div class="form-group">
                                            <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                <asp:Label ID="lblAcademicEndDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, AcademicEndDate %>"></asp:Label>
                                                <span class="astrik error-msg">*</span>
                                            </h6>

                                            <div class="form">
                                                <input type="text" id="dtAcademicEndDate" readonly="readonly" runat="server" class="moe-full-width moe-input-padding moe-select input-height-42">
                                                <asp:RequiredFieldValidator ID="rqrddtAcademicEndDate" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RequiredAcademicEndDate  %>" ControlToValidate="dtAcademicEndDate" runat="server" CssClass="error-msg moe-full-width" ValidationGroup="Submit" Display="Dynamic" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row margin-bottom-15">
                                <asp:HiddenField ID="hdnCalcSectionItem" runat="server" />
                                <%-- <div class="col-md-8 col-sm-12 col-xs-12 no-padding margin-bottom-15 sm-width-90">
                                    <div class="row data-container table-display moe-full-width">
                                        <div class="form-group">
                                            <div class="form">
                                                <div class="uc-dropdown">

                                                    <uc1:FileUpload runat="server" id="fileUploadCalcDetails" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>--%>
                            </div>
                            <div class="row margin-top-15">
                                <div class="col-md-6 col-sm-12 col-xs-12 no-padding ">

                                    <uc1:FileUpload runat="server" id="fileUploadAdmissionLetter" />
                                </div>
                            </div>
                            <div class="row margin-top-15">
                                <div class="col-md-6 col-sm-12 col-xs-12 no-padding ">

                                    <uc1:FileUpload runat="server" id="fileUploadStudyPlan" />
                                </div>
                            </div>
                            <%--<div class="row margin-bottom-15" id="joinedOtherUni">
                                <div class="col-md-6 col-sm-12 col-xs-12 no-padding margin-bottom-15 sm-width-90">
                                    <div class="data-container table-display moe-full-width">
                                        <div class="form-group">
                                            <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                <asp:Label ID="lblJoinedOtheUniversities" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, JoinedOtherUniversities %>"></asp:Label>
                                                <span class="astrik error-msg">*</span>
                                            </h6>
                                            <div class="form">
                                                <asp:RadioButtonList ID="rdbJoinedOtherUni" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdbJoinedOtherUni_SelectedIndexChanged" ClientIDMode="Static" CssClass="moe-radioBtn radio_cont"></asp:RadioButtonList>
                                                <asp:CustomValidator runat="server" ID="custJoinedOtherUni" EnableClientScript="true" OnServerValidate="custJoinedOtherUni_ServerValidate" ValidationGroup="Submit" ClientValidationFunction="custJoinedOtherUni_ServerValidate" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RequiredJoinedOtherUniversities %>"></asp:CustomValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form">

                                                <div class="uc-dropdown notemsg">
                                                    <uc1:FileUpload runat="server" id="fileUploadGrades" Visible="false" CssClass="test" />
                                                </div>
                                                <div class="uc-dropdown notemsg">
                                                    <uc1:FileUpload runat="server" id="fileUploadAcceptedHours" Visible="false" />
                                                </div>
                                            </div>
                                </div>
                            </div>--%>
                        </div>
                        <!--End of divUniversities-->
                    </asp:Panel>
                    <!--End of pnlPreviousCertificates-->
                </div>
                <!--End of Third Accordion-->

                <!--Start of First Accordion-->
                <h3>
                    <!--btnDegreeRequiredToBEquivalentData-->
                    <%--<asp:Button ID="btnDegreeRequiredToBEquivalentData" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, HighestDegreeHeader %>"
                        CssClass="popdate accordion-input"></asp:Button>--%>

                    <a class="popdate accordion-input"><%=Resources.ITWORX_MOEHEWF_PA.HighestDegreeHeader %></a>
                </h3>
                <div class="cotainer margin-top-25">
                    <!--Inner Accordion-->
                    <div class="row margin-bottom-15">
                        <div class="col-md-4 col-xs-12 no-padding margin-top-25">
                            <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                <div class="form-group">
                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                        <asp:Label ID="lblHighestCertificate" Text="<%$Resources:ITWORX_MOEHEWF_PA, HigherCertificate %>" runat="server"></asp:Label>
                                        <span class="astrik error-msg">*</span>
                                    </h6>
                                    <div class="form">
                                        <asp:DropDownList ID="ddlHighestCertificate" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42 moe-dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlHighestCertificate_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqHighestCertificate" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RequiredHighestCertificate %>" ControlToValidate="ddlHighestCertificate" CssClass="error-msg moe-full-width" ValidationGroup="Submit" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-4 col-xs-12 no-padding margin-top-25">
                            <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                <div class="form-group">
                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                        <asp:Label ID="lblCountries" Text="<%$Resources:ITWORX_MOEHEWF_PA, CountryOfStudy %>" runat="server"></asp:Label>
                                        <span class="astrik error-msg">*</span>
                                    </h6>
                                    <div class="form">
                                        <asp:DropDownList ID="dropCountries" runat="server" OnSelectedIndexChanged="dropCountries_SelectedIndexChanged" AutoPostBack="true" CssClass="moe-full-width moe-input-padding moe-select input-height-42 moe-dropdown"></asp:DropDownList>
                                        <asp:Label ID="lblCountriesValue" runat="server"></asp:Label>
                                        <asp:RequiredFieldValidator ID="reqCountries" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RequiredCountry %>" ControlToValidate="dropCountries" CssClass="error-msg moe-full-width" ValidationGroup="Submit" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-4 col-xs-12 no-padding margin-top-25">
                            <div class="data-container table-display moe-width-85">
                                <div class="form-group">
                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                        <asp:Label ID="lblCertificateDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, CertificateDate %>" ClientIDMode="Static"></asp:Label>
                                        <span class="astrik error-msg">*</span>
                                    </h6>
                                    <div class="form">
                                        <input type="text" id="txtCertificateDate" readonly="readonly" runat="server" class="moe-full-width moe-input-padding moe-select input-height-42" />
                                        <asp:DropDownList ID="ddlCertificateYears" Visible="false" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42 moe-dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlCertificateYears_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqCertificateDate" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RequiredCertificateDate  %>" ControlToValidate="txtCertificateDate" runat="server" CssClass="error-msg moe-full-width" ValidationGroup="Submit" Display="Dynamic" InitialValue="-1" />
                                        <asp:RequiredFieldValidator ID="reqtxtCertificateDate" runat="server" ControlToValidate="txtCertificateDate" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RequiredCertificateDate  %>" CssClass="error-msg moe-full-width" ValidationGroup="Submit"  Enabled="false" Visible="false"></asp:RequiredFieldValidator>
                                        </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row margin-top-15">

                        <div class="col-md-6 col-sm-12 col-xs-12 no-padding ">
                            <uc1:FileUpload runat="server" id="FUDequivalentHours" />
                        </div>
                    </div>
                    <div>
                        <!--Start of pnlDelegationTemplates-->

                        <!--End of pnlDelegationTemplates-->
                    </div>
                    <div id="sectionSecondaryCertificate" runat="server" class="row margin-top-15">

                        <div class="col-md-12 margin-bottom-25 sm-width-90">
                            <h1 class="font-weight-700 font-size-18 color-black font-family-sans margin-top-0 margin-bottom-0 underline">
                                <asp:Literal Text="<%$Resources:ITWORX_MOEHEWF_PA, SecondaryCertificateIfFound %>" runat="server" />
                            </h1>
                        </div>
                        <div class="sm-width-90">
                            <div class="col-md-6 col-sm-12 col-xs-12 no-padding ">

                                <uc1:FileUpload runat="server" id="schoolDocuments" />

                                <uc1:FileUpload runat="server" id="fileUploadNationalService" />
                            </div>
                        </div>
                    </div>
                    <!--divUniversitiesDetails Label-->

                    <!--Start of divUniversitiesDetails-->
                    <div id="divUniversitiesDetails" runat="server" class="row margin-bottom-15">
                        <div class="col-md-12 margin-bottom-25 sm-width-90">
                            <h1 class="font-weight-700 font-size-18 color-black font-family-sans margin-top-0 margin-bottom-0 underline">
                                <asp:Literal Text="<%$Resources:ITWORX_MOEHEWF_PA, HighestCertificateIfFound %>" runat="server" /></h1>
                        </div>

                        <div class="col-md-4 col-xs-12 no-padding">
                            <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                <div class="form-group">

                                    <div class="form uc-dropdown">
                                        <div id="ddlUniversityDiv" runat="server">
                                        </div>
                                        <uc1:DropdownWithTextbox runat="server" id="ddlUniversity" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-4 col-xs-12 no-padding">
                            <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                <div class="form-group">
                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                        <asp:Label ID="facultylbl" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, FacultyName %>"></asp:Label>
                                        <span class="astrik error-msg">*</span>
                                    </h6>
                                    <div class="form Iwidth-100">
                                        <asp:TextBox ID="ddlFaculty" runat="server" />
                                        <asp:RequiredFieldValidator ID="rqrdFaculty" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, FacultyRequired %>" ControlToValidate="ddlFaculty" CssClass="error-msg moe-full-width" ValidationGroup="Submit" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-4 col-xs-12 no-padding">
                            <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                <div class="form-group">
                                    <div class="form uc-dropdown">
                                        <div id="specialization">
                                            <uc1:DropdownWithTextbox runat="server" id="ddlSpecialization" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-4 col-xs-12 no-padding" style="display: none">
                            <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                <div class="form-group">
                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                        <asp:Label ID="lblUniversityList" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, UniversitiesLists %>"></asp:Label>
                                    </h6>
                                    <div class="form">
                                        <asp:TextBox ID="txtUniversityListValue" runat="server" ReadOnly="true" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 sm-width-90 col-xs-12 margin-top-15">
                            <div class="col-md-6 no-padding">
                                <uc1:FileUpload runat="server" id="universityDocuments" />
                            </div>
                        </div>

                        <!--End of row-->
                    </div>
                    <!--End of divUniversitiesDetails-->
                </div>
                <!--End of First Accordion-->
                <!--lblUniversitiesNames Label-->
                <h3>
                    <a class="popdate accordion-input"><%=Resources.ITWORX_MOEHEWF_PA.PrevHoursIfExist %></a>
                    <%-- <asp:Button ID="lblUniversitiesNames" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, PrevHoursIfExist %>" CssClass="popdate accordion-input"></asp:Button>--%>
                </h3>
                <!--Start of divUniversities-->

                <div class="container">
                    <div class="row margin-bottom-15">
                        <div class="col-md-4 col-sm-12 col-xs-12  no-padding">
                            <div class="data-container table-display moe-width-85">
                                <div class="form-group">
                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                        <asp:Label ID="Label2" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, Country %>"></asp:Label>
                                    </h6>

                                    <div class="form">
                                        <div class="uc-dropdown  uc-with-title">
                                            <uc1:DropdownWithTextbox runat="server" id="ddlCalcSectionCountry" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-4 col-sm-12 col-xs-12  no-padding">
                            <div class="data-container table-display moe-width-85">
                                <div class="form-group">
                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                        <asp:Label ID="Label3" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, UniversityName %>"></asp:Label>
                                    </h6>
                                    <div class="form">
                                        <div class="uc-dropdown  uc-with-title">

                                            <uc1:DropdownWithTextbox runat="server" id="ddlCalcSectionUniversity" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-4 col-sm-12 col-xs-12  no-padding">
                            <div class="data-container table-display moe-width-85">
                                <div class="form-group">
                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                        <asp:Label ID="Label4" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, FacultyName %>"></asp:Label>
                                    </h6>
                                    <div class="form">

                                        <asp:TextBox ID="txtCalcSectionFaculty" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row margin-bottom-15">
                        <div class="col-md-4 col-sm-6 no-padding small-height">
                            <div class="data-container table-display moe-width-85 moe-sm-95-width pull-sm-right margin-sm-0">
                                <div class="form-group">
                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                        <asp:Label runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, StudySystem %>"></asp:Label>
                                    </h6>

                                    <div class="form">

                                        <asp:DropDownList ID="dropStudyingSystem" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4 col-sm-6 no-padding small-height">
                            <div class="data-container table-display moe-width-85 moe-sm-95-width pull-sm-right margin-sm-0">
                                <div class="form-group">
                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                        <asp:Label runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, ConvertedHours %>"></asp:Label>
                                    </h6>

                                    <div class="form">
                                        <asp:TextBox ID="txt_AcceptedHours" runat="server" TextMode="Number" ClientIDMode="Static" CssClass="moe-full-width moe-input-padding moe-select input-height-42" />

                                        <asp:RegularExpressionValidator ID="regAcceptedHours" runat="server" ControlToValidate="txt_AcceptedHours" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RegGreaterThanZero %>" ValidationExpression="^[1-9][0-9]*$" CssClass="error-msg moe-full-width" ValidationGroup="Submit" Display="Dynamic"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <div class="row  margin-bottom-15 sm-width-90">
                        <div class="col-md-12 col-sm-12 col-xs-12 data-container table-display margin-bottom-0-imp no-padding-imp file-category">

                            <div id="fileCalc" class="col-md-12 col-sm-12 col-xs-12 margin-top-15">

                                <div class="col-md-8 col-sm-12 col-xs-12 no-padding-imp ">

                                    <uc1:FileUpload runat="server" id="FUADegreeList" />

                                    <div class="col-md-12 col-sm-12 col-xs-12 no-padding-imp">

                                        <uc1:FileUpload runat="server" id="FUAcceptedHoursList" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="container margin-top-15">
                        <div class="row">
                            <asp:LinkButton ID="lnkAddCalcDetails" Text="<%$Resources:ITWORX_MOEHEWF_PA, AddNewUniversitiesandFaculties %>" runat="server" OnClick="lnkAddCalcDetails_Click" CssClass="btn moe-btn popdate pull-right" ValidationGroup="AddCalc"></asp:LinkButton>
                            <asp:LinkButton ID="lnkUpdateCalcDetails" Text="<%$Resources:ITWORX_MOEHEWF_PA, UpateUniversitiesandFaculties %>" runat="server" OnClick="lnkUpdateCalcDetails_Click" CssClass="btn moe-btn popdate pull-right" ValidationGroup="AddCalc" Visible="false"></asp:LinkButton>
                        </div>
                    </div>

                    <div class="container margi-top-25 no-padding repeater-wrap">
                        <asp:Repeater ID="repCalculatedDetailsForCertificate" runat="server" OnItemDataBound="repCalculatedDetailsForCertificate_ItemDataBound">

                            <HeaderTemplate>
                                <table class="table table-striped moe-full-width moe-table margin-top-25">

                                    <tr>
                                        <th scope="col">
                                            <%=Resources.ITWORX_MOEHEWF_PA.Country %>
                                        </th>
                                        <th scope="col">
                                            <%=Resources.ITWORX_MOEHEWF_PA.UniversityName %>
                                        </th>
                                        <th scope="col">
                                            <%=Resources.ITWORX_MOEHEWF_PA.FacultyName %>
                                        </th>
                                        <th scope="col">
                                            <%=Resources.ITWORX_MOEHEWF_PA.StudySystem %>
                                        </th>
                                        <th scope="col">
                                            <%=Resources.ITWORX_MOEHEWF_PA.ConvertedHoursTH %>
                                        </th>
                                        <th scope="col" style="min-width: 120px">
                                            <%=Resources.ITWORX_MOEHEWF_PA.Actions %>
                                        </th>
                                    </tr>
                            </HeaderTemplate>

                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCalcSectionCountry" runat="server" Text='<%#Eval("Country.SelectedTitle") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnCalcSectionCountry" runat="server" Value='<%#Eval("Country.SelectedID") %>' />
                                        <asp:Label ID="lblCalcSectionOtherCountry" runat="server" Text='<%#Eval("OtherCountry") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCalcSectionUniversity" runat="server" Text='<%#Eval("Univesrity.SelectedTitle") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnCalcSectionUniversity" runat="server" Value='<%#Eval("Univesrity.SelectedID") %>' />
                                        <asp:Label ID="lblCalcSectionOtherUniversity" runat="server" Text='<%#Eval("OtherUniversity") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCalcSectionFaculty" runat="server" Text='<%#Eval("Faculty") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCalcSectionStudySystem" runat="server" Text='<%#Eval("StudySystem.SelectedTitle") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnCalcSectionStudySystem" runat="server" Value='<%#Eval("StudySystem.SelectedID") %>' />
                                        <asp:HiddenField ID="hdnId" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblConvertedHours" runat="server" Text='<%#Eval("AcceptedHours") %>'></asp:Label>
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:HiddenField ID="hdnCalcSectionID" runat="server" Value='<%#Eval("ID") %>' />
                                        <asp:HiddenField ID="hdnVirtualID" runat="server" />
                                        <asp:LinkButton ID="lnkCalcSectionDelete" runat="server" OnClick="lnkCalcSectionDelete_Click" CssClass="fa fa-trash"></asp:LinkButton>
                                        <asp:LinkButton ID="lnkCalcSectionEdit" runat="server" OnClick="lnkCalcSectionEdit_Click" CssClass="fa fa-edit"></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>

                            <FooterTemplate>
                                </table>
                  <div id="dvNoRecords" runat="server" visible="false" style="text-align: center; color: Red;">
                      <%=Resources.ITWORX_MOEHEWF_PA.DataEmpty %>
                      <%-- <i>No records to display.</i>--%>
                  </div>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>

                    <cc1:ModalPopupExtender ID="modalAddCalcSection" runat="server"
                        TargetControlID="btnHdnCalc"
                        PopupControlID="pnlAddCalcSection" BackgroundCssClass="modalBackground">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="pnlAddCalcSection" runat="server" Style="display: none;" CssClass="modalPopup">

                        <asp:Label ID="lblDeleteMsg" runat="server" ForeColor="Green" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_PA, DeleteMessage %>"></asp:Label>

                        <asp:Button ID="btn_CalcSecOK" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, Ok %>" />
                    </asp:Panel>
                    <asp:Button ID="btnHdnCalc" runat="server" Text="Button" Style="display: none;" />
                </div>

                <!--End of divUniversities-->
                <!--Start of Second Accordion-->
                <h3>
                    <!--btnWorkingDetails-->
                    <a class="popdate accordion-input"><%=Resources.ITWORX_MOEHEWF_PA.WorkingDetails %></a>
                    <%--   <asp:Button ID="btnWorkingDetails" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, WorkingDetails %>" CssClass="popdate accordion-input"></asp:Button>--%>
                </h3>
                <div>
                    <!--Start of pnlWorkingDetails-->
                    <asp:Panel ID="pnlWorkingDetails" runat="server">
                        <div id="divWorkingDetails">

                            <div class="row margin-top-25">
                                <!--Start of row-->

                                <div class="col-md-4 col-xs-12 no-padding">
                                    <div class="data-container table-display moe-width-85">
                                        <div class="form-group">
                                            <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                <asp:Label ID="lblWorkingOrNot" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, WorkingOrNot %>"></asp:Label>
                                                <span class="astrik error-msg">*</span>
                                            </h6>

                                            <div class="form">
                                                <asp:RadioButtonList ID="rdbWorkingOrNot" runat="server" ClientIDMode="Static" CssClass="moe-radioBtn radio_cont" AutoPostBack="true" OnSelectedIndexChanged="rdbWorkingOrNot_SelectedIndexChanged"></asp:RadioButtonList>
                                                <asp:CustomValidator runat="server" ID="DoYouWorkCheckBoxRequired" EnableClientScript="true" OnServerValidate="DoYouWorkCheckBoxRequired_ServerValidate" ValidationGroup="Submit" ClientValidationFunction="DoYouWorkCheckBoxRequired_ServerValidate"><asp:literal text="<%$Resources:ITWORX_MOEHEWF_PA, RequiredWorkingOrNot %>" runat="server" /></asp:CustomValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div id="ShowHideWorkingForAndOccupation" runat="server">
                                    <div class="col-md-4 col-xs-12 no-padding" id="entityWorkingFor">
                                        <%--<div class="data-container table-display moe-width-85">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="Label1" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, EntityWorkingFor %>"></asp:Label><span class="astrik error-msg">*</span> </h6>
                                                <div class="form Iwidth-100">
                                                    <asp:DropDownList runat="server" ID="ddlEntityWorkingFor" CssClass="moe-full-width moe-input-padding moe-select input-height-42 moe-dropdown"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rqrdddlEntityWorkingFor" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RequiredEntityWorkingFor %>" ValidationGroup="Submit" ControlToValidate="ddlEntityWorkingFor" CssClass="error-msg moe-full-width" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>--%>

                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">

                                                <div class="form uc-dropdown">
                                                    <div id="RequiredEntityWorkingFor" runat="server">
                                                    </div>
                                                    <uc1:DropdownWithTextbox runat="server" id="ddlEntityWorkingFor" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="clearfix"></div>
                                    <div class="col-md-8 col-xs-12 no-padding margin-top-15 ">
                                        <div class="row data-container table-display sm-width-90">
                                            <div class="form-group">
                                                <div class="form">
                                                    <div class="uc-dropdown">
                                                        <uc1:FileUpload runat="server" id="fileUploadWorking" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div id="ShowHideNotWorking" runat="server">

                                    <div class="col-md-8 col-sm-12 col-xs-12 no-padding margin-bottom-15 sm-width-90">
                                        <div class="row data-container table-display moe-full-width">
                                            <div class="form-group">
                                                <div class="form">
                                                    <div class="uc-dropdown">
                                                        <uc1:FileUpload runat="server" id="fileUploadNotWorking" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--Start of row-->
                        </div>
                    </asp:Panel>
                    <!--End of pnlWorkingDetails-->
                </div>
                <!--End of Second Accordion-->

                <!--Start of Fourth Accordion-->
                <h3>
                    <!--btnDelegationTemplates-->

                    <a class="popdate accordion-input"><%=Resources.ITWORX_MOEHEWF_PA.HeaderDelegationTemplates %></a>
                </h3>

                <div>
                    <!--Start of pnlDelegationTemplates-->

                    <asp:Panel ID="pnlDelegationTemplates" runat="server">
                        <div id="divDelegationTemplates" class="edit-mode">

                            <div class="row margin-top-25 margin-bottom-25">
                                <asp:Repeater ID="repDelegatesTemplates" runat="server">

                                    <HeaderTemplate>
                                        <table class="table divDelegationTemplatesTable">

                                            <tr>
                                                <th scope="col">
                                                    <%=Resources.ITWORX_MOEHEWF_PA.DelegationTemplates %>
                                                </th>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>

                                                <asp:HiddenField ID="hdnFileExtension" runat="server" Value='<%# Eval("FileExtension") %>' />
                                                <asp:LinkButton ID="lnkDelegateDisplay" runat="server" Text='<%# Eval("FileTitle") %>' CommandArgument='<%# Eval("ID") %>' OnClick="lnkDelegateDisplay_Click" OnClientClick="return setFormSubmitToFalse();"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>

                                <div class="col-md-12 col-xs-12">
                                    <h6 class="font-size-20 margin-bottom-0 margin-top-0 height-auto  instruct-title">
                                        <asp:Label ID="lblInstructions" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, Instructions %>"></asp:Label>
                                    </h6>
                                    <h5 class="font-size-18 margin-bottom-0 margin-top-0 instruction-details underline color-black font-family-sans">
                                        <asp:Label ID="lblDelegationInstruction" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, DelegationInstructions %>"></asp:Label>
                                    </h5>
                                </div>
                            </div>

                            <div class="row margin-top-15">

                                <div class="col-md-6 col-xs-12">
                                    <uc1:FileUpload runat="server" id="fileUploadDelegates" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <!--End of pnlDelegationTemplates-->
                </div>

                <!--End of Fourth Accordion-->
            </div>
            <!--End of Accordion-->
        </div>

        <div class="row no-padding margin-2">

            <h4 class=" font-weight-600 margin-top-15 checkBox-cont" id="checktext">
                <asp:CheckBox ID="chkConfirmationBox" ClientIDMode="static" CssClass="termsandcond" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, Admission %>" AutoPostBack="False" />
            </h4>
        </div>
        <div class="row validation-summary">
            <asp:ValidationSummary ID="ValidSummaryPARequest" runat="server" ValidationGroup="Submit" Enabled="true" ShowSummary="true" />
        </div>
    </asp:Panel>
</div>

<asp:Label ID="lblNoRequest" runat="server" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_PA, YouHaveNoRequests %>"></asp:Label>
<asp:Button ID="btnResubmit" runat="server" ValidationGroup="Submit" Text="<%$Resources:ITWORX_MOEHEWF_PA, ReSubmit %>" OnClick="btnResubmit_Click" CssClass="pull-right" />
<cc1:ModalPopupExtender ID="modalPopUpConfirmation" runat="server"
    TargetControlID="btnHdn"
    PopupControlID="pnlConfirmation" BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlConfirmation" runat="server" Style="display: none;" CssClass="modalPopup">

    <asp:Label ID="lblSuccess" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label><br />
    <br />
    <asp:Button ID="btnModalOK" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, Ok %>" OnClick="btnModalOK_Click" />
    <asp:Button ID="btnCancel" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, Cancel %>" Style="display: none" />
</asp:Panel>
<asp:Button ID="btnHdn" runat="server" Text="Button" Style="display: none;" />

<cc1:ModalPopupExtender ID="modalPopupError" runat="server" TargetControlID="btnHdnErrorOk" PopupControlID="pnl_ErrorConfirmed" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>
<asp:Panel ID="pnl_ErrorConfirmed" runat="server" Style="display: none;" CssClass="modalPopup">

    <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Green" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_PA, PopupError %>"></asp:Label>

    <asp:Button ID="btnErrorOk" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, Ok %>" OnClick="btnErrorOk_Click" />
</asp:Panel>
<asp:Button ID="btnHdnErrorOk" runat="server" Text="Button" Style="display: none;" />

<script type="text/javascript">

    function PostToNewWindow() {
        originalTarget = document.forms[0].target;
        document.forms[0].target = '_blank';
        window.setTimeout("document.forms[0].target=originalTarget;", 300);
        return true;
    }
    function setFormSubmitToFalse() {
        setTimeout(function () { _spFormOnSubmitCalled = false; }, 3000);
        return true;
    }
    function DoYouWorkCheckBoxRequired_ServerValidate(sender, e) {
        e.IsValid = $('#rdbWorkingOrNot input:checked').is(':checked');
    }

    function custJoinedOtherUni_ServerValidate(sender, e) {
        e.IsValid = $('#rdbJoinedOtherUni input:checked').is(':checked');
    }
    $(document).ready(function () {

        var number = document.getElementById('txt_AcceptedHours');
        // Listen for input event on numInput.
        number.onkeydown = function (e) {
            if (!((e.keyCode > 95 && e.keyCode < 106)
                || (e.keyCode > 47 && e.keyCode < 58)
                || e.keyCode == 8)) {
                return false;
            }
        }
        $(".finishdata.btn.moe-btn.margin-lr-0").attr('disabled', 'disabled');

        $("#chkConfirmationBox").change(function () {
            if ($("#chkConfirmationBox").is(":checked")) {
                $(".finishdata.btn.moe-btn.margin-lr-0").removeAttr('disabled');
            }
            else {
                $(".finishdata.btn.moe-btn.margin-lr-0").attr('disabled', 'disabled');
            }
        });

        $("#chkConfirmationBox").change();

        $('#ShowHideNotWorking').hide();
        $('#<%=dtAcademicStartDate.ClientID %>').datepicker({
            dateFormat: "dd/mm/yy",
            showOn: 'focus',
            showButtonPanel: true,
            closeText: 'Clear',
            changeYear: true,
            changeMonth: true,
            onSelect: function (selected) {
                var dt2 = $('#<%=dtAcademicEndDate.ClientID %>');

                dt2.datepicker("option", "minDate", selected)

            },
            onClose: function () {
                var event = arguments.callee.caller.caller.arguments[0];
                // If "Clear" gets clicked, then really clear it
                if ($(event.delegateTarget).hasClass('ui-datepicker-close')) {
                    $(this).val('');
                }
            }

        });
        $('#<%=dtAcademicEndDate.ClientID %>').datepicker({
            dateFormat: "dd/mm/yy",
            showOn: 'focus',
            showButtonPanel: true,
            minDate: '0',
            changeYear: true,
            changeMonth: true,
            closeText: 'Clear',
            onClose: function () {
                var event = arguments.callee.caller.caller.arguments[0];
                // If "Clear" gets clicked, then really clear it
                if ($(event.delegateTarget).hasClass('ui-datepicker-close')) {
                    $(this).val('');
                }
            }

        });
        $('#<%=txtCertificateDate.ClientID %>').datepicker({
            dateFormat: "dd/mm/yy",
            showOn: 'focus',
            showButtonPanel: true,
            changeYear: true,
            changeMonth: true,
            closeText: 'Clear',
            onClose: function () {
                var event = arguments.callee.caller.caller.arguments[0];
                // If "Clear" gets clicked, then really clear it
                if ($(event.delegateTarget).hasClass('ui-datepicker-close')) {
                    $(this).val('');
                }
            }
        });

        if ($('#rdbCertificateThroughScholarship input:checked').val() == "0") {
            $("#entityProviding").hide();
            var requiredEntityProviding = $("#entityProviding").find('[id$=reqDropWithNewOption]');
            ValidatorEnable(document.getElementById(requiredEntityProviding[0].id), false);
        }

        else if ($('#rdbCertificateThroughScholarship input:checked').val() == "1") {

            $("#entityProviding").show();
        }

        $("#rdbCertificateThroughScholarship").change(function () {
            if ($('#rdbCertificateThroughScholarship input:checked').val() == "0") {

                $("#entityProviding").hide();
                $("#entityProviding").find('[id$=txtNewOption]').hide();
                $("#entityProviding").find('[id$=dropWithNewOption]').val("");
                $("#entityProviding").find('[id$=txtNewOption]').val("");

                //set the validation by false of text and dropdown
                var requiredEntityDrop = $("#entityProviding").find('[id$=reqDropWithNewOption]');
                ValidatorEnable(document.getElementById(requiredEntityDrop[0].id), false);
                var requiredEntityText = $("#entityProviding").find('[id$=reqNewOptionText]');
                ValidatorEnable(document.getElementById(requiredEntityText[0].id), false);

            }
            else {
                $("#entityProviding").show();

            }
        });

        function ValidateControls() {
            var returnform = true;
            var returnValue = true;
            if ($('#<%=dtAcademicStartDate.ClientID %>').val() == "") {
                $("#reqStartDate").show();
                returnform = false;
            }
            else {
                $("#reqStartDate").hide();
            }

            if (
                ($("#reqStudyLocation").css("visibility") == "visible" && $("#reqStudyLocation").css("display") != "none") ||
                ($("#reqExamLocation").css("visibility") == "visible" && $("#reqExamLocation").css("display") != "none")
                ||
                ($("#reqStartDate").css("visibility") != "visible" && $("#reqStartDate").css("display") != "none") ||
                returnform == false) {
                return false;
            }
            return returnValue;
        }
        $('.popdate').click(function () {

        });

    });
</script>