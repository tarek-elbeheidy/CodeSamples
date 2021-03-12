<%@ Assembly Name="ITWORX.MOEHEWF.UCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=883afb4c05a35fe5" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/DropdownWithTextbox.ascx" TagPrefix="uc1" TagName="DropdownWithTextbox" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="uc1" TagName="FileUpload" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RequestDetails.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.RequestDetails" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
 <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
 <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>--%>

<%--<link rel="stylesheet" href='<%= ResolveUrl ("~/Style%20Library/CSS/jquery-ui.css") %>'>--%>
<%--<script type="text/javascript" src='<%= ResolveUrl ("~/Style%20Library/Scripts/jquery-1.12.4.js") %>'></script>--%>
<%--<script type="text/javascript" src='<%= ResolveUrl ("~/Style%20Library/Scripts/jquery-ui.js") %>'></script>--%>
<%--<script type="text/javascript" src='<%= ResolveUrl ("~/Style%20Library/Scripts/jquery.cookie.js") %>'></script>--%>

<style type="text/css">
    .modalBackground {
        background-color: Black;
        filter: alpha(opacity=90);
        opacity: 0.8;
    }

    .instruction-details {
        font-family: sans-serif;
        line-height: 1.7;
        color: #333;
        display: block;
    }

    #divDelegationTemplates h6.height-auto {
        height: auto;
    }
    .instruction-details .fa.fa-exclamation-triangle{
        display:none!important;
    }
    /*.modalPopup {
        background-color: #fff;
        border: 3px solid #ccc;
        padding: 10px;
        width: 300px;
    }*/
</style>
<div class="request-details">
    <asp:Panel ID="pnlformControls" runat="server">
        

        <%-- <h1 class="section-title text-center font-weight-500 margin-bottom-25">
        <asp:Label ID="lblRequestDetails" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RequestDetails %>"></asp:Label>
    </h1>--%>

        <div id="divRequestDetails" class="row heighlighted-section margin-bottom-50 flex-display flex-wrap">

            <div class="col-md-3 col-sm-6 col-xs-6 auto-height">
                <div class="data-container">
                    <h6 class="font-size-16 margin-bottom-15">
                        <asp:Label ID="lblRequestNumber" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RequestNumber %>"></asp:Label>
                    </h6>
                    <h5 class="font-size-20">

                        <asp:Label ID="lblRequestNumberValue" runat="server"></asp:Label>
                    </h5>
                </div>
            </div>

            <div class="col-md-3 col-sm-6 col-xs-6 auto-height">
                <div class="data-container">
                    <h6 class="font-size-16 margin-bottom-15">
                        <asp:Label ID="lblRequestCreationDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RequestCreationDate %>"></asp:Label>
                    </h6>
                    <h5 class="font-size-20">
                        <asp:Label ID="lblRequestCreationDateValue" runat="server"></asp:Label>
                    </h5>
                </div>
            </div>

            <%--         <div class="col-md-3 col-sm-6">
             <div class="data-container ">
                 <h6 class="font-size-16 margin-bottom-15">
                     <asp:Label ID="lblRequestSubmitDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RequestSubmitDate %>" Visible="false"></asp:Label>
                 </h6>
                 <h5 class="font-size-20">
                        <asp:Label ID="lblRequestSubmitDateValue" runat="server" Visible="false"></asp:Label>
                 </h5>
             </div>
         </div>--%>
        </div>
        <aiv class="row no-padding-imp flex-display margin-2">
             <div class="col-md-12 col-xs-12 margin-top-15 margin-bottom-25">
        <h5 class="font-size-18 margin-bottom-0 margin-top-0 instruction-details underline color-black font-family-sans">
            <asp:Label ID="lblAttachmentNote" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, AttachmentNote %>"></asp:Label>

        </h5>
    </div>
        </aiv>
        <div class="row heighlighted-section margin-bottom-15">
            <div class="col-md-6 no-padding">
                <div class="data-container margin-all-0">
                    <div class="form-group margin-all-0">
                        <h6 class="font-size-16 margin-bottom-15">
                            <asp:Label ID="lblAcademicDegreeForEquivalence" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, AcademicDegree %>"></asp:Label>
                            <span class="astrik error-msg">*</span>
                        </h6>

                        <div class="form">
                            <asp:DropDownList ID="dropAcademicDegreeForEquivalence" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqAcademicDegreeForEquivalence" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredQatariDegreeForEquivalence %>" CssClass="moe-full-width error-msg" ValidationGroup="Submit" ControlToValidate="dropAcademicDegreeForEquivalence" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row unheighlighted-section margin-2">
            <!--Start of Accordion-->
            <div class="accordion UCEaccordion">

                <!--Start of First Accordion-->
                <h3>
                    <!--btnDegreeRequiredToBEquivalentData-->
                    <%-- <asp:LinkButton ID="btnDegreeRequiredToBEquivalentData" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, DegreeRequiredToBEquivalentData %>"
                        CssClass="popdate accordion-input"></asp:LinkButton>--%>
                    <a class="popdate accordion-input"><%=Resources.ITWORX_MOEHEWF_UCE.DegreeRequiredToBEquivalentData %></a>
                </h3>
                <div class="row margin-top-25">
                    <!--Inner Accordion-->

                    <!--Start of pnlDegreeRequiredToBEquivalentData-->
                    <asp:Panel ID="pnlDegreeRequiredToBEquivalentData" runat="server">
                        <div class="row margin-top-15">
                            <div class="accordion-inner">
                                <!--Start of divCertificatesData-->
                                <!--CertificatesData Label-->
                                <h3 class="inner-accordion">
                                    <asp:Label ID="lblCertificatesData" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CertificatesData %>"></asp:Label>
                                </h3>
                                <div id="divCertificatesData">
                                    <div class="row margin-top-15">
                                        <!--CertificatesData "First Field"-->
                                        <div class="col-md-4 col-md-6 col-sm-12 col-xs-12 no-padding ">
                                            <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                                <div class="form-group">
                                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0 ">
                                                        <asp:Label ID="lblCerticateAcademicDegree" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CertificateAcademicDegree %>"></asp:Label>
                                                        <span class="astrik error-msg">*</span>
                                                    </h6>

                                                    <div class="form">
                                                        <asp:DropDownList ID="dropCertificateAcademicDegree" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42" AutoPostBack="true" OnSelectedIndexChanged="dropCertificateAcademicDegree_SelectedIndexChanged"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator  ID="reqCerticateAcademicDegree" runat="server" CssClass="moe-full-width error-msg" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredCertificateAcademicDegree %>" ValidationGroup="Submit" ControlToValidate="dropCertificateAcademicDegree" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--CertificateThroughScholarship "Second Field"-->
                                        <div class="col-md-8 col-sm-12 col-xs-12  no-padding-imp">
                                            <div class="col-md-6 col-sm-12 col-xs-12 no-padding">
                                                <div class="data-container table-display  moe-sm-full-width sm-width-90">
                                                    <div class="form-group">
                                                        <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                            <asp:Label ID="lblCertificateThroughScholarship" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CertificateThroughScholarship %>"></asp:Label>
                                                            <span class="astrik error-msg">*</span>
                                                        </h6>

                                                        <div class="form">
                                                            <asp:RadioButtonList ID="rdbCertificateThroughScholarship" runat="server" ClientIDMode="Static" CssClass="moe-radioBtn"></asp:RadioButtonList>
                                                            <asp:RequiredFieldValidator ID="reqCertificateThroughScholarship" runat="server" CssClass="moe-full-width error-msg" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredCertificateThroughScholarship %>" ValidationGroup="Submit" ControlToValidate="rdbCertificateThroughScholarship" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class=" col-md-6 col-sm-12 col-xs-12 no-padding" id="entityProviding" style="display: none">
                                                <div class="data-container table-display   moe-sm-full-width sm-width-90">
                                                    <div class="form-group">
                                                        <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                            <asp:Label ID="lblEntityProvidingStudy" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EntityProvidingStudy %>"></asp:Label>
                                                            <span class="astrik error-msg">*</span>
                                                        </h6>

                                                        <div class="form">
                                                            <asp:TextBox ID="txtEntityProvidingStudy" runat="server" ClientIDMode="Static" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ClientIDMode="Static" ID="reqEntityProvidingStudy" runat="server" CssClass="moe-full-width error-msg" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiedEntityProvidingStudy %>" ValidationGroup="Submit" ControlToValidate="txtEntityProvidingStudy" Display="Dynamic" Enabled="false"></asp:RequiredFieldValidator>

                                                            <%-- <uc1:DropdownWithTextbox runat="server" id="ddlEntityProvidingStudy" />--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--CampusStudy "3rd Field"-->
                                        <%--   <div class="col-md-12 no-padding-imp">
                                            <div class="col-md-4 col-md-6 col-sm-12 col-xs-12 no-padding">
                                                <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                                    <div class="form-group">
                                                        <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                            <asp:Label ID="lblCampusStudy" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CampusStudy %>"></asp:Label>
                                                            <span class="astrik error-msg">*</span>
                                                        </h6>
                                                        <div class="form">
                                                            <asp:RadioButtonList ID="rdbCampusStudy" runat="server" ClientIDMode="Static" CssClass="moe-radioBtn"></asp:RadioButtonList>
                                                            <asp:RequiredFieldValidator ID="reqCampusStudy" runat="server" CssClass="moe-full-width error-msg" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredCampusStudy %>" ValidationGroup="Submit" ControlToValidate="rdbCampusStudy" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <!--StudyLocation "4th Field"-->

                                            <%-- <div class="col-md-4 col-sm-12 col-xs-12 no-padding" style="display: none;" id="studyLocationContent">
                            <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                <div class="form-group">
                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                 <asp:Label ID="lblStudyLocation" runat="server" ClientIDMode="Static" Text="<%$Resources:ITWORX_MOEHEWF_UCE, StudyLocation %>" Style="display: none">  </asp:Label>
                                 <span class="astrik error-msg"> *</span>
                                        </h6>
                                    <div class="form">
                                         <asp:TextBox ID="txtStudyLocation" runat="server" ClientIDMode="Static" Style="display: none" CssClass="moe-full-width moe-input-padding moe-select input-height-42"> </asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqStudyLocation" runat="server" ClientIDMode="Static" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredStudyLocation %>" CssClass="moe-full-width error-msg" ValidationGroup="Submit" ControlToValidate="txtStudyLocation" Enabled="false" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        </div>--%>
                                        <!--CampusExam "5th Field"-->
                                        <%--  <div class="col-md-12 no-padding-imp">
                                                <div class="col-md-4 col-sm-12 col-xs-12 no-padding">
                                                    <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                                        <div class="form-group">
                                                            <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                                <asp:Label ID="lblCampusExam" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CampusExam %>"></asp:Label>
                                                                <span class="astrik error-msg">*</span>
                                                            </h6>
                                                            <div class="form">
                                                                <asp:RadioButtonList ID="rdbCampusExam" runat="server" ClientIDMode="Static" CssClass="moe-radioBtn"></asp:RadioButtonList>
                                                                <asp:RequiredFieldValidator ID="reqCampusExam" runat="server" CssClass="moe-full-width error-msg" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredCampusExam %>" ValidationGroup="Submit" ControlToValidate="rdbCampusExam" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>--%>
                                        <!--ExamLocation "6th Field"-->
                                        <%--     <div class="col-md-4 col-sm-12 col-xs-12 no-padding" style="display:none;" id="examLocationSection">
                            <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                <div class="form-group">
                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                        <asp:Label ID="lblExamLocation" runat="server" ClientIDMode="Static" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ExamLocation %>" Style="display: none"></asp:Label>
                                    </h6>

                                    <div class="form">
                                         <asp:TextBox ID="txtExamLocation" runat="server" ClientIDMode="Static" Style="display: none" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqExamLocation" runat="server" ClientIDMode="Static" CssClass="moe-full-width error-msg" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredExamLocation %>" ValidationGroup="Submit" ControlToValidate="txtExamLocation" Enabled="false" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                                    </div>
                                    <div class="row margin-top-15">
                                        <div class="col-md-8 col-sm-12 col-xs-12">

                                            <uc1:FileUpload runat="server" id="fileUploadCertificateEquivalent" />

                                        </div>
                                    </div>

                                    <div class="row margin-top-15">
                                        <div class="col-md-8 col-sm-12 col-xs-12">

                                            <uc1:FileUpload runat="server" id="fileUploadGrades" />

                                        </div>
                                    </div>
                                </div>

                                <!--End of divCertificatesData-->

                                <!--divUniversitiesDetails Label-->
                                <h3 class="inner-accordion">
                                    <asp:Label ID="lblUniversitiesDetails" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversitiesDetails %>"></asp:Label>
                                </h3>
                                <!--Start of divUniversitiesDetails-->
                                <div id="divUniversitiesDetails">
                                    <div class="container">
                                        <!--Start of row-->
                                        <div class="row margin-bottom-15">
                                            <div class="col-md-4 col-sm-12 col-xs-12 no-padding">
                                            <div class="data-container table-display moe-width-85">
                                                <div class="form-group">
                                                    <%--<h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                        <asp:Label ID="lblCountries" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CountryOfStudy %>" runat="server"></asp:Label>
                                                        <span class="astrik error-msg" id="spanCountry" runat="server">*</span>
                                                    </h6>--%>
                                                    <div class="form">
                                                        <uc1:DropdownWithTextbox runat="server" id="ddlCountry" />
                                                    </div>

                                                    <h5 class="font-size-20 margin-top-10" style="display: none;">
                                                        <asp:Label ID="lblCountriesValue" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CountryOfStudy %>" Visible="false"></asp:Label></h5>
                                                    <div class="form">
                                                        <asp:TextBox ID="txtCountriesValue" runat="server"  Visible="false" CssClass="moe-full-width input-height-42 border-box moe-input-padding disabled" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                            <div class="col-md-4 col-sm-12 col-xs-12 no-padding test-new">
                                            <div class="data-container table-display moe-width-85">
                                                <div class="form-group">

                                                    <div id="ddlUniversityDiv" runat="server" class="form uc-dropdown">
                                                        <uc1:DropdownWithTextbox runat="server" id="ddlUniversity" />
                                                    </div>

                                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                        <asp:Label ID="lblUniversity" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Universities  %>" Visible="false"></asp:Label>
                                                    </h6>
                                                    <div class="form">
                                                        <asp:TextBox ID="txtUniversityValue" runat="server" ReadOnly="true" Visible="false" CssClass=" moe-full-width input-height-42 border-box moe-input-padding disabled"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                            <div class="col-md-4 col-sm-12 col-xs-12 no-padding">
                                                <div class="data-container table-display moe-width-85">
                                                    <div class="form-group">
                                                        <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                            <asp:Label ID="facultylbl" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, FacultyName %>"></asp:Label><span class="astrik error-msg">*</span>
                                                        </h6>
                                                        <div class="form">
                                                            <%--<uc1:DropdownWithTextbox runat="server" id="ddlFaculty" />--%>
                                                            <%--<uc1:DropDownControl runat="server" id="ddlSpecialization"  />--%>
                                                         
                                                            <asp:TextBox ID="ddlFaculty" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox> 
                                                            <asp:RequiredFieldValidator ID="rqrdFaculty" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, FacultyRequired %>" ControlToValidate="ddlFaculty" CssClass="error-msg moe-full-width" ValidationGroup="Submit" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            
                                        </div>
                                        <div class="row">
                                            
                                            <div class="col-md-4 col-sm-12 col-xs-12 no-padding" id="uniList" runat="server" visible="false">
                                            <div class="data-container table-display moe-width-85">
                                                <div class="form-group">
                                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                        <asp:Label ID="lblUniversityList" runat="server" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversitiesLists %>"></asp:Label>
                                                    </h6>
                                                    <div class="form">
                                                        <asp:TextBox ID="txtUniversityListValue" Visible="false" runat="server" ReadOnly="true" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>

                                                        <%--   <uc1:DropDownControl runat="server" id="ddlUniversity"   />--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                            <div class="col-md-4 col-sm-12 col-xs-12 no-padding">
                                                <div class="data-container table-display moe-width-85">
                                                    <div class="form-group">
                                                        <div class="form uc-dropdown">
                                                            <div id="specialization">
                                                                <uc1:DropdownWithTextbox runat="server" id="ddlSpecialization" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 no-padding-imp" >
                                                  <h5 class="font-size-18 margin-bottom-0 margin-top-0">
                                            <asp:Label ID="lblUniversityHEDD" runat="server" CssClass="instruction-details underline warning"></asp:Label></h5>
                                        </div>
                                           
                                    </div>
                                         <div class="row" >
                                            <div class="col-md-12 no-padding-imp" >
                                                <h5 class="font-size-18 margin-bottom-0 margin-top-0 ">
                                            <asp:Label ID="lblUniversityCHED" runat="server" CssClass="instruction-details underline warning"></asp:Label>
                                                    </h5>
                                        </div>
                                        </div>
                                </div>
                                <!--End of row-->

                                <%--<uc1:DropDownControl runat="server" id="ddlSpecialization"  />--%>
                                <!--specialization "3rd Field"-->

                                <%-- <uc1:DropDownControl runat="server" id="ddlFaculty"  />--%>

                                <%--  <asp:Label ID="lblUniversity" runat="server"></asp:Label>--%>
                                <%-- <uc1:DropDownControl runat="server" id="universityDropdown"  />--%>

                                <%-- <asp:DropDownList ID="dropUniversities" runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqUniversities" runat="server" ControlToValidate="dropUniversities" CssClass="moe-full-width error-msg"  ValidationGroup="Submit"></asp:RequiredFieldValidator>
                            <asp:HyperLink ID="hypNewUniversity" runat="server" ClientIDMode="Static"></asp:HyperLink>
                            <asp:TextBox ID="txtNewUniversity" runat="server" ClientIDMode="Static" Visible="false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqNewUniversity" runat="server"  ClientIDMode="Static" ControlToValidate="txtNewUniversity" CssClass="moe-full-width error-msg" ValidationGroup="Submit" Enabled="false" ></asp:RequiredFieldValidator>
                            <br />

                            <!--lblFaculty "4th Field"-->
                            <asp:Label ID="lblFaculty" runat="server"></asp:Label>
                            <asp:DropDownList ID="dropFaculty" runat="server"></asp:DropDownList>
                           <asp:HyperLink ID="hypFaculty" runat="server" ClientIDMode="Static"></asp:HyperLink>
                            <asp:TextBox ID="txtFaculty" runat="server" ClientIDMode="Static" Visible="false"></asp:TextBox>

                             <br />

                             <!--lblFaculty "5th Field"-->
                             <asp:Label ID="lblSpecialization" runat="server"></asp:Label>
                             <asp:DropDownList ID="dropSpecialization" runat="server"></asp:DropDownList>
                             <asp:HyperLink ID="hypSpecialization" runat="server" ClientIDMode="Static"></asp:HyperLink>
                             <asp:TextBox ID="txtSpecialization" runat="server" ClientIDMode="Static" Visible="false"></asp:TextBox>--%>

                                <!--End of divUniversitiesDetails-->
                                    </div>

                                <!-- divStudyingDetails Label-->
                                <h3 class="inner-accordion">
                                    <asp:Label ID="lblStudingDetails" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, StudyDetails %>"></asp:Label>
                                </h3>
                                <!--Start of divStudyingDetails-->
                                <div id="divStudyingDetails">
                                    <div class="container margin-top-15">
                                        <div class="row margin-bottom-25">
                                            <!--Fisrt Field-->
                                        <div class="col-md-4 col-sm-12 col-xs-12 no-padding">
                                            <div class="data-container table-display moe-width-85 ">
                                                <div class="form-group">
                                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                        <asp:Label ID="lblStudyLanguage" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, StudyLanguage %>"></asp:Label>
                                                        <span class="astrik error-msg">*</span>
                                                    </h6>
                                                    <div class="form">
                                                        <asp:DropDownList ID="dropStudyingLanguage" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="reqStudyingLanguage" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredStudyLanguage %>" ControlToValidate="dropStudyingLanguage" CssClass="moe-full-width error-msg" ValidationGroup="Submit" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                            <!--3rd Field-->
                                        <div class="col-md-4 col-sm-12 col-xs-12 no-padding">
                                            <div class="data-container table-display moe-width-85  ">
                                                <div class="form-group">
                                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                        <asp:Label ID="lblStudyingSystem" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, StudySystem %>"></asp:Label>
                                                    </h6>

                                                    <div class="form">
                                                        <asp:DropDownList ID="dropStudyingSystem" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
                                                        <%--   <asp:RequiredFieldValidator ID="reqStudyingSystem" runat="server"   ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredStudySystem %>" ControlToValidate="dropStudyingSystem" ForeColor="Red"  ValidationGroup="Submit"  ></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                            <!--4th Field-->

                                        <div class="col-md-4 col-sm-12 col-xs-12 no-padding">
                                            <div class="data-container table-display moe-width-85  ">
                                                <div class="form-group">
                                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">

                                                        <asp:Label ID="lblStartDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, StudyStartDate %>"></asp:Label>
                                                        <span class="astrik error-msg">*</span>
                                                    </h6>

                                                    <div class="form">
                                                        <%-- <SharePoint:DateTimeControl ID="dtStartDate" runat="server" DateOnly="true"  />--%>

                                                        <asp:TextBox  ID="txtStartDate" ClientIDMode="Static" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42" ></asp:TextBox>
                                                      
                                                        <%--  <input type="text" id="dtStartDate" readonly="readonly" runat="server" class="moe-full-width moe-input-padding moe-select input-height-42">--%>
                                                             <asp:RequiredFieldValidator ID="reqStartDate" runat="server"   ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredStudyStartDate %>" ControlToValidate="txtStartDate" ForeColor="Red"  ValidationGroup="Submit" CssClass="moe-full-width error-msg"  Display="Dynamic" ></asp:RequiredFieldValidator>
                                                      <%--  <span id="reqStartDate" style="display: none" class="moe-full-width error-msg"><%=Resources.ITWORX_MOEHEWF_UCE.RequiredStudyStartDate %></span>--%>
                                                        <%--<asp:RequiredFieldValidator ID="reqStartDate" runat="server"  ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredStudyStartDate %>" ControlToValidate="dtStartDate$dtStartDateDate" ForeColor="Red"  ValidationGroup="Submit"  ></asp:RequiredFieldValidator>
                   <asp:CompareValidator ID="compStartDate" runat="server" ForeColor="Red"  Type="Date" Operator="DataTypeCheck" ControlToValidate="dtStartDate$dtStartDateDate"  ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, InvalidDate %>" ValidationGroup="Submit"></asp:CompareValidator>
                                                        --%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        </div>
                                        
                                        <div class="row margin-bottom-0">
                                            <!--5th Field-->
                                        <div class="col-md-4 col-sm-12 col-xs-12 no-padding">
                                            <div class="data-container table-display moe-width-85  ">
                                                <div class="form-group">
                                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">

                                                        <asp:Label ID="lblGraduationDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, GraduationDate %>"></asp:Label>
                                                    </h6>

                                                    <div class="form">
                                                        <%-- <SharePoint:DateTimeControl ID="dtGraduationDate" runat="server" DateOnly="true" />--%>
                                                        
                                                          <asp:TextBox  ID="txtGraduationDate" ClientIDMode="Static" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                                                      
                                                      <%--  <input type="text" id="dtGraduationDate" readonly="readonly" runat="server" class="moe-full-width moe-input-padding moe-select input-height-42">--%>

                                                        <%--<span id="reqGraduationDate" style="color:red;display:none" ><%=Resources.ITWORX_MOEHEWF_UCE.RequiredGraduationDate %></span>--%>
                                                        <%-- <asp:RequiredFieldValidator ID="reqGraduationDate" runat="server"  ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredGraduationDate %>" ControlToValidate="dtGraduationDate$dtGraduationDateDate" ForeColor="Red"  ValidationGroup="Submit"  ></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="compGraduationDate" runat="server"  ForeColor="Red"  Type="Date" Operator="DataTypeCheck" ControlToValidate="dtGraduationDate$dtGraduationDateDate"  ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, InvalidDate %>" ValidationGroup="Submit"></asp:CompareValidator>
                <asp:CompareValidator ID="compDates" ControlToCompare="dtStartDate$dtStartDateDate" ControlToValidate="dtGraduationDate$dtGraduationDateDate" Type="Date" Operator="GreaterThanEqual" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, GraduationDateValidation %>" runat="server"  ForeColor="Red" ValidationGroup="Submit"></asp:CompareValidator>
                                                        --%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>



                                        </div>
                                        <div class="row" style="display:none;">
                                            <!--Second Field-->

                                        <%--                        <div class="col-md-4 col-sm-12 col-xs-12 no-padding">
    <div class="data-container table-display moe-width-85">
        <div class="form-group">
            <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                  <asp:Label ID="lblStudingType" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, StudyType %>"></asp:Label>
                <span class="astrik error-msg"> *</span>
            </h6>

            <div class="form">
                        <asp:DropDownList ID="dropStudyingType" runat="server"  CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqStudingType" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredStudyType %>" ControlToValidate="dropStudyingType" CssClass="moe-full-width error-msg" ValidationGroup="Submit" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
    </div>
</div>--%>
                                        
                                        

                                        

                                        <!--6th Field-->
                                        <%--     <div class="col-md-4 col-sm-12 col-xs-12 no-padding">
    <div class="data-container table-display moe-width-85">
        <div class="form-group">
            <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                 <asp:Label ID="lblStudyPeriod" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, AcademicProgramPeriod %>"></asp:Label>
                <span class="astrik error-msg"> *</span>
            </h6>

            <div class="form">
                 <asp:TextBox ID="txtStudyPeriod" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqStudyPeriod" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredAcademicProgramPeriod %>" ControlToValidate="txtStudyPeriod" CssClass="moe-full-width error-msg" ValidationGroup="Submit" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="regStudyPeriod" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RegNumbersOnly %>" ValidationExpression="^\d+$" ControlToValidate="txtStudyPeriod" CssClass="moe-full-width error-msg" ValidationGroup="Submit" Display="Dynamic"></asp:RegularExpressionValidator>
            </div>
        </div>
    </div>
</div>--%>

                                        <!--7th Field-->
                                        <%--        <div class="col-md-4 col-sm-12 col-xs-12 no-padding">
    <div class="data-container table-display moe-width-85">
        <div class="form-group">
            <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                <asp:Label ID="lblActualStudy" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ActualStudingPeriod %>"></asp:Label>
    <span class="astrik error-msg"> *</span>
            </h6>

            <div class="form">
                   <asp:TextBox ID="txtActualStudy" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqActualStudy" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredActualStudingPeriod %>" ControlToValidate="txtActualStudy" CssClass="moe-full-width error-msg" ValidationGroup="Submit" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="regActualStudy" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RegNumbersOnly %>" ValidationExpression="^\d+$" ControlToValidate="txtActualStudy" CssClass="moe-full-width error-msg" Display="Dynamic"></asp:RegularExpressionValidator>
        </div>
            </div></div></div>--%>

                                        <!--8th Field-->
                                        <%--                    <div class="col-md-4 col-sm-12 col-xs-12 no-padding">
                                            <div class="data-container table-display moe-width-85">
                                                <div class="form-group">
                                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">

                                                        <asp:Label ID="lblGainedHours" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NumberOfHoursGained %>"></asp:Label></h6>
                                                    <div class="form">
                                                        <asp:TextBox ID="txtGainedHours" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="regGainedHours" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RegNumbersOnly %>" ValidationExpression="^\d+$" ControlToValidate="txtGainedHours" CssClass="moe-full-width error-msg" ValidationGroup="Submit" Display="Dynamic"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>--%>
                                        <!--9th Field-->
                                        <%--    <div class="col-md-4 col-sm-12 col-xs-12 no-padding">
                                            <div class="data-container table-display moe-width-85">
                                                <div class="form-group">
                                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">

                                                        <asp:Label ID="lblGPA" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, GPA %>"></asp:Label></h6>

                                                    <div class="form">
                                                        <asp:TextBox ID="txtGPA" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>--%>
                                        <!--10th Field-->
                                        <%--      <div class="col-md-4 col-sm-12 col-xs-12 no-padding">
                                            <div class="data-container table-display moe-width-85">
                                                <div class="form-group">
                                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">

                                                        <asp:Label ID="lblOnlineHours" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NumberOfOnlineHours %>"></asp:Label></h6>

                                                    <div class="form">
                                                        <asp:TextBox ID="txtOnlineHours" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="regOnlineHours" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RegNumbersOnly %>" ValidationExpression="^\d+$" ControlToValidate="txtOnlineHours" CssClass="moe-full-width error-msg" ValidationGroup="Submit" Display="Dynamic"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>--%>
                                        <!--12th Field-->
                                        <%--  <div class="col-md-4 col-sm-12 col-xs-12 no-padding">
                                            <div class="data-container table-display moe-width-85">
                                                <div class="form-group">
                                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                        <asp:Label ID="lblOnlinePercentage" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PercentageOfOnlineHours %>"></asp:Label></h6>

                                                    <div class="form">
                                                        <asp:TextBox ID="txtOnlinePercentage" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="regOnlinePercentage" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RegPercNumbersOnly %>" ValidationExpression="^[0-9]*\.?[0-9]*" runat="server" ControlToValidate="txtOnlinePercentage" CssClass="moe-full-width error-msg" ValidationGroup="Submit" Display="Dynamic"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>--%>

                                        <!--13th Field-->
                                        <%--<div class="col-md-12 col-sm-12 col-xs-12 no-padding">
                                            <div class="data-container table-display moe-width-85">
                                                <div class="form-group">
                                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                        <asp:CheckBox ID="chkCompExam" runat="server" />
                                                        <asp:Label ID="lblCompExam" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, IsThereComprehensiveExam %>"></asp:Label>
                                                    </h6>
                                                </div>
                                            </div>
                                        </div>--%>

                                        <!--14th Field-->
                                        <%--    <div class="col-md-12 col-sm-12 col-xs-12 no-padding">
                                            <div class="data-container table-display moe-width-85">
                                                <div class="form-group">
                                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                        <asp:CheckBox ID="chkAcceptExam" runat="server" />
                                                        <asp:Label ID="lblAcceptExam" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, IsThereAcceptanceExam %>"></asp:Label></h6>
                                                </div>
                                            </div>
                                        </div>
                                        --%>
                                        </div>
                                    </div>
                                </div>
                                <!--End of divStudyingDetails-->
                                <!--lblUniversitiesNames Label-->
                                <h3 class="inner-accordion test-class">
                                    <asp:Label ID="lblUniversitiesNames" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityNames %>"></asp:Label>
                                </h3>
                                <!--Start of divUniversities-->
                                <div id="divUniversities">

                                    <div class="container margin-top-15">
                                        <div class="row margin-bottom-25">
                                            <div class="col-md-4 col-sm-12 col-xs-12 no-padding">
                                            <div class="data-container table-display moe-width-85  ">
                                                <div class="form-group">
                                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                        <asp:Label ID="lblCalcSectionCountry" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Country %>"></asp:Label>
                                                    </h6>

                                                    <div class="form">
                                                        <div class="uc-dropdown  uc-with-title">
                                                            <uc1:DropdownWithTextbox runat="server" id="ddlCalcSectionCountry" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-4 col-sm-12 col-xs-12 no-padding">
                                            <div class="data-container table-display moe-width-85  ">
                                                <div class="form-group">
                                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                        <asp:Label ID="lblCalcSectionUniversity" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityName %>"></asp:Label>
                                                    </h6>
                                                    <div class="form">
                                                        <div class="uc-dropdown  uc-with-title">

                                                            <uc1:DropdownWithTextbox runat="server" id="ddlCalcSectionUniversity" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
 
                                        <div class="col-md-4 col-sm-12 col-xs-12 no-padding">
                                            <div class="data-container table-display moe-width-85  ">
                                                <div class="form-group">
                                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                        <asp:Label ID="lblCalcSectionFaculty" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, FacultyName %>"></asp:Label>
                                                    </h6>
                                                    <div class="form">
 

                                                        <asp:TextBox ID="ddlCalcSectionFaculty" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42" ></asp:TextBox>
 
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        </div>

                                        <div class="row margin-bottom-25">
                                             <div class="col-md-4 col-sm-12 col-xs-12 no-padding small-height">
                                            <div class="data-container table-display moe-width-85  ">
                                                <div class="form-group">
                                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                        <asp:Label runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, StudySystem %>"></asp:Label>
                                                    </h6>

                                                    <div class="form">

                                                        <asp:DropDownList ID="dropCalcSectionStudyingSystem" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        </div>
 
                                        <asp:HiddenField ID="hdnCalcSectionItem" runat="server" />
                                        <div class="row  margin-bottom-15 sm-width-90">
                                            <div class="col-md-12 col-sm-12 col-xs-12 data-container table-display moe-full-width margin-bottom-0-imp no-padding-imp file-category">

                                                <div id="divCalcAttachmentCategory" runat="server" visible="false" class="col-md-8 col-sm-12 col-xs-12 no-padding">
                                                    <div class="data-container table-display moe-sm-full-width pull-sm-left margin-sm-0">
                                                        <div class="form-group">
                                                            <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                                <asp:Label runat="server" ID="lblCalcAttachmentCategory" Text="<%$Resources:ITWORX_MOEHEWF_UCE, AttachmentType %>"></asp:Label>
                                                               <%-- <span class="astrik error-msg">*</span>--%>
                                                                <asp:Label runat="server" ID="lblCalcRequiredFlag" Style="color: Red; display: none;"></asp:Label>
                                                            </h6>

                                                            <div class="form">
                                                                <asp:DropDownList ID="ddlCalcAttachmentCategory" runat="server" ClientIDMode="Static" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator Enabled="false" ID="reqCalcAttachmentCategory" ClientIDMode="Static" runat="server" ControlToValidate="ddlCalcAttachmentCategory"
                                                                    Display="Dynamic" ValidationGroup="Upload" CssClass="error-msg" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE,RequiredAttachmentType %>" ></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div id="fileCalc" class="col-md-8 col-sm-12 col-xs-12 margin-top-15 ">
                                                    <uc1:FileUpload runat="server" id="fileUploadCalcDetails" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="container margin-top-15">
                                        <div class="row sm-width-90">
                                            <asp:LinkButton ID="lnkAddCalcDetails" Text="<%$Resources:ITWORX_MOEHEWF_UCE, AddNewUniversitiesandFaculties %>" runat="server" OnClick="lnkAddCalcDetails_Click" CssClass="btn moe-btn popdate pull-right" ValidationGroup="AddCalc"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkUpdateCalcDetails" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UpateUniversitiesandFaculties %>" runat="server" OnClick="lnkUpdateCalcDetails_Click" CssClass="btn moe-btn popdate pull-right" ValidationGroup="AddCalc" Visible="false"></asp:LinkButton>
                                        </div>
                                        
                                    </div>

                                    <div class="container margi-top-25 no-padding repeater-wrap">
                                        <asp:Repeater ID="repCalculatedDetailsForCertificate" runat="server" OnItemDataBound="repCalculatedDetailsForCertificate_ItemDataBound">

                                            <HeaderTemplate>
                                                <table class="table table-striped moe-full-width moe-table margin-top-25">

                                                    <tr>
                                                        <th scope="col">
                                                            <%=Resources.ITWORX_MOEHEWF_UCE.Country %>
                                                        </th>
                                                        <th scope="col">
                                                            <%=Resources.ITWORX_MOEHEWF_UCE.UniversityName %>
                                                        </th>
                                                        <th scope="col">
                                                            <%=Resources.ITWORX_MOEHEWF_UCE.FacultyName %>
                                                        </th>
                                                        <th scope="col">
                                                            <%=Resources.ITWORX_MOEHEWF_UCE.StudySystem %>
                                                        </th>
 
                                                        <th scope="col" style="min-width: 120px">
                                                            <%=Resources.ITWORX_MOEHEWF_UCE.Actions %>
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
                      <%=Resources.ITWORX_MOEHEWF_UCE.DataEmpty %>
                      <%-- <i>No records to display.</i>--%>
                  </div>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>

                                    <cc1:ModalPopupExtender ID="modalAddCalcSection" runat="server"
                                          TargetControlID="btnHdnCalc" 
                PopupControlID="pnlAddCalcSection" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>
                                     <asp:Panel ID="pnlAddCalcSection" runat="server" Style="display: none;" CssClass="modalPopup">
               
     <asp:Label ID="lblDeleteMsg" runat="server" ForeColor="Green" Font-Bold="true"  Text="<%$Resources:ITWORX_MOEHEWF_UCE, DeleteMessage %>" ></asp:Label>

               
                <asp:Button ID="btn_CalcSecOK" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Ok %>" />    
            
            </asp:Panel>
                <asp:Button ID="btnHdnCalc" runat="server" Text="Button" Style="display: none;" />
 
                                </div>

                                <!--End of divUniversities-->

                                <!--divPublishingCertificate Label-->
                                <h3 class="inner-accordion">
                                    <asp:Label ID="lblPublishingCertificateUnivesity" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityPublishingCertificateDetails %>"></asp:Label>
                                </h3>
                                <!--Start of divPublishingCertificate-->
                                <div id="divPublishingCertificate">

                                    <div class="row margin-top-15">
                                        <div class="col-md-4 col-sm-12 col-xs-12 no-padding">
                                            <div class="data-container table-display moe-width-85  ">
                                                <div class="form-group">


                                                
                                                    <div class="form">
                                                        <asp:TextBox ID="TextBox1" runat="server" ReadOnly="true" Visible="false" CssClass=" moe-full-width input-height-42 border-box moe-input-padding disabled"></asp:TextBox>
                                                    </div>
                                                    <div class="form">
                                                        <div class="uc-dropdown">
                                                            <div id="mainCountry" runat="server">
                                                                <uc1:DropdownWithTextbox runat="server" id="ddlMainCountry" />
                                                               
                                                            </div>
                                                            <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                        <asp:Label ID="lblMainCountry" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityMainHeadquarter  %>" Visible="false"></asp:Label>
                                                    </h6>
                                                    <div class="form">
                                                        <asp:TextBox ID="txtMainCountry" runat="server" ReadOnly="true" Visible="false" CssClass=" moe-full-width input-height-42 border-box moe-input-padding disabled"></asp:TextBox>
                                                    </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-4 col-sm-12 col-xs-12 no-padding">
                                            <div class="data-container table-display moe-width-85  ">
                                                <div class="form-group">
                                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                        <asp:Label ID="lblUniversityAddress" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityAddress %>"></asp:Label>
                                                    </h6>

                                                    <div class="form">
                                                        <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-4 col-sm-12 col-xs-12 no-padding">
                                            <div class="data-container table-display moe-width-85  ">
                                                <div class="form-group">
                                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                        <asp:Label ID="lblUniversityEmail" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityEmail %>"></asp:Label>
                                                    </h6>

                                                    <div class="form">
                                                        <asp:TextBox ID="txtUniversityEmail" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
 
                                                        <asp:RegularExpressionValidator ID="regUniversityEmail" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, WrongEmailAddress %>" ValidationGroup="Submit" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" runat="server" CssClass="moe-full-width error-msg" ControlToValidate="txtUniversityEmail" Display="Dynamic"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!--End of divPublishingCertificate-->
                        </div>
                        </div>
                    </asp:Panel>
                </div>

                <!--End of pnlDegreeRequiredToBEquivalentData-->

                <!--End of First Accordion-->

                <!--Start of Second Accordion-->
                <h3>
                    <!--btnWorkingDetails-->

                    <a class="popdate accordion-input"><%=Resources.ITWORX_MOEHEWF_UCE.WorkingDetails %></a>
                </h3>
                <div>
                    <!--Start of pnlWorkingDetails-->
                    <asp:Panel ID="pnlWorkingDetails" runat="server">
                        <div id="divWorkingDetails" class="edit-mode">

                            <div class="row margin-top-25">
                                <!--Start of row-->


                                <div class="col-md-4 col-sm-12 col-xs-12 no-padding">
                                    <div class="data-container table-display moe-width-85  ">
                                        <div class="form-group">
                                            <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                <asp:Label ID="lblWorkingOrNot" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, WorkingOrNot %>"></asp:Label>
                                                <span class="astrik error-msg">*</span>
                                            </h6>

                                            <div class="form">
                                                <asp:RadioButtonList ID="rdbWorkingOrNot" runat="server" ClientIDMode="Static" CssClass="moe-radioBtn"></asp:RadioButtonList>
                                                <asp:RequiredFieldValidator ID="reqWorkingOrNot" runat="server" CssClass="moe-full-width error-msg" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredWorkingOrNot %>" ControlToValidate="rdbWorkingOrNot" ValidationGroup="Submit" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-4 col-sm-12 col-xs-12 no-padding" id="entityNeedsEquivalency" style="display: none">
                                    <div class="data-container table-display moe-width-85  ">
                                        <div class="form-group">
                                            <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                <asp:Label ID="lblEntityNeedsEquivalency" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EntityNeedsEquivalency %>"></asp:Label>
                                                <span class="astrik error-msg">*</span>
                                            </h6>
                                            <div class="form">

                                                <div class="uc-dropdown">

                                                    <asp:DropDownList ID="dropEntityNeedsEquivalency" runat="server" ClientIDMode="Static" CssClass="special-width moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="reqEntityNeedsEquivalency" ClientIDMode="Static" runat="server" CssClass="moe-full-width error-msg" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredEntityNeedsEquivalency %>" ControlToValidate="dropEntityNeedsEquivalency" ValidationGroup="Submit" Display="Dynamic" Enabled="false"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="col-md-4 col-sm-12 col-xs-12 no-padding" id="entityWorkingFor" style="display: none">

                                    <div class="data-container table-display moe-width-85  ">
                                        <div class="form-group">
                                            <div class="form">
                                                <div class="uc-dropdown">
                                                    <uc1:DropdownWithTextbox runat="server" id="ddlEntityWorkingFor" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <%-- <div class="col-md-4 col-sm-12 col-xs-12 no-padding" id="occupation" style="display: none">
                                        <div class="data-container table-display moe-width-85">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblOccupation" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, OccupationName %>"></asp:Label>
                                                    <span class="astrik error-msg">*</span>
                                                </h6>

                                                <div class="form">
                                                    <asp:TextBox ID="txtOccupation" ClientIDMode="Static" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqOccupation" ClientIDMode="Static" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredOccupationName %>" ValidationGroup="Submit" CssClass="moe-full-width error-msg" ControlToValidate="txtOccupation" Display="Dynamic" Enabled="false"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>

                                <!--Here-->


                                
                            </div>

                            <div class="row margin-top-15" id="fileWorking" style="display: none">
                                <div class="col-md-7 col-sm-12 col-xs-12 no-padding" >
                                    
                                        <div class="form-group">
                                            <div class="form">

                                                <uc1:FileUpload runat="server" id="fileUploadWorking" />
                                            </div>
                                        </div>
                                    
                                </div>
                            </div>
                            <div class="row margin-top-15" id="filenotWorking" style="display: none">
                                <div class="col-md-7 col-sm-12 col-xs-12 no-padding">
                                    <div class="form-group">
                                        <div class="form">

                                            <uc1:FileUpload runat="server" id="fileUploadNotWorking" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--Start of row-->
                         </asp:Panel>
                </div>
   
    <!--End of pnlWorkingDetails-->

    <!--End of Second Accordion-->

    <!--Start of Third Accordion-->
    <h3>
        <!--btnPreviousCertificates-->

        <a class="popdate accordion-input"><%=Resources.ITWORX_MOEHEWF_UCE.PreviousCertificates %></a>

    </h3>
    <div>
        <!--Start of pnlPreviousCertificates-->
        <asp:Panel ID="pnlPreviousCertificates" runat="server">
            <div id="divPreviousCertificates" class="edit-mode ">
                <div class="row margin-top-25 file-category">
                    <div id="divAttachmentCategory" runat="server" visible="false" class="col-md-8 col-sm-12 col-xs-12 no-padding">
                        <div class="data-container table-display moe-sm-full-width pull-sm-left margin-sm-0">
                            <div class="form-group">


                                <div class="form">

                                    <div id="certDrop">
                                        <uc1:DropdownWithTextbox runat="server" id="ddlAttachmentCat" />
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="fileCert" class="col-md-8 col-sm-12 col-xs-12">

                        <uc1:FileUpload runat="server" id="fileUploadCertificates" Visible="false" />

                          <asp:CustomValidator ID="custValidateFileUploadCertificates" runat="server"  CssClass="moe-full-width error-msg"   ValidationGroup="Submit" Display="Dynamic" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredAllCertificates %>"  OnServerValidate="custValidateFileUploadCertificates_ServerValidate"></asp:CustomValidator>
                    </div>
                    <div class="col-md-12">
                        <h6 class="font-size-18">
                            <asp:Label ID="lblPreviousCertificates" runat="server" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PreviousCertificatesMessage %>"></asp:Label>
                        </h6>
                    </div>

                    <%--  <uc1:UploadUC runat="server" id="UploadUC" />--%>

                     <div id="diploma" class="col-md-8 col-sm-12 col-xs-12">

                        <uc1:FileUpload runat="server" id="fileUploadDiploma" Visible="false" />

                    </div>

                     <div id="interDiploma" class="col-md-8 col-sm-12 col-xs-12">

                        <uc1:FileUpload runat="server" id="fileUploadInterDiploma" Visible="false" />

                    </div>



<%--                    <div id="generalSec" class="col-md-8 col-sm-12 col-xs-12">

                        <uc1:FileUpload runat="server" id="fileUploadGeneralSec" Visible="false" />

                    </div>--%>

                    <div id="sequenceCert" class="col-md-8 col-sm-12 col-xs-12 margin-top-15 ">
                        <div class="data-container">

                        
                        <div class="form-group">
                            <h6 class="font-size-18 margin-bottom-0 margin-top-0 height-auto instruct-title underline">
                                <asp:Label ID="lblSequence" runat="server" Visible="false"  Text="<%$Resources:ITWORX_MOEHEWF_UCE, ConfirmationSequence %>"></asp:Label>
                            </h6>

                            <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                <asp:LinkButton ID="lnkSequences" ClientIDMode="Static" runat="server" Visible="false" CssClass="donwload" OnClick="lnkSequences_Click" OnClientClick="return setFormSubmitToFalse();"></asp:LinkButton>
                            </h6>

                            <div class="form margin-top-25">
                                <uc1:FileUpload runat="server" id="fileUploadSequenceCert" Visible="false" />
                            </div>
                        </div>
                            </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <!--End of pnlPreviousCertificates-->
    </div>
    <!--End of Third Accordion-->

    <!--Start of Fourth Accordion-->
    <h3>
        <!--btnDelegationTemplates-->

        <a class="popdate accordion-input"><%=Resources.ITWORX_MOEHEWF_UCE.DelegationTemplates %></a>
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
                                        <%=Resources.ITWORX_MOEHEWF_UCE.DelegationTemplates %>
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

                    <div class="col-md-12 col-xs-12 col-sm-12 col-xs-12">
                        <h6 class="font-size-20 margin-bottom-0 margin-top-0 height-auto instruct-title">
                            <asp:Label ID="lblInstructions" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Instructions %>"></asp:Label>
                        </h6>
                        <h5 class="font-size-18 margin-bottom-0 margin-top-0 instruction-details underline color-black font-family-sans">
                            <asp:Label ID="lblDelegationInstruction" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, DelegationInstructions %>"></asp:Label>
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

        <div class="row validation-summary">
            <asp:ValidationSummary ID="ValidSummaryRequest" runat="server" ValidationGroup="Submit" Enabled="true" ShowSummary="true" ShowValidationErrors="true" />
        </div>
    </asp:Panel>
</div>
 

<div class="col-md-12 no-padding">
    <h4 class="font-size-18 font-weight-600">
        <asp:Label ID="lblNoRequest" runat="server" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, YouHaveNoRequests %>"></asp:Label>
    </h4>
</div>
<div class="col-md-12 no-padding">
    <asp:Button ID="btnResubmit" runat="server" ValidationGroup="Submit" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ReSubmit %>" OnClick="btnResubmit_Click" CssClass="btn moe-btn pull-right" />
    <asp:Label ID="lblEditRequired" runat="server" ForeColor="Red" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EditRequest %>"></asp:Label>
</div>
<cc1:ModalPopupExtender ID="modalPopUpConfirmation" runat="server"
    TargetControlID="btnHdn"
    PopupControlID="pnlConfirmation" BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlConfirmation" runat="server" Style="display: none;" CssClass="modalPopup">

    <asp:Label ID="lblSuccess" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>

    <asp:Button ID="btnModalOK" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Ok %>" OnClick="btnModalOK_Click" CssClass="btn moe-btn" />

</asp:Panel>
<asp:Button ID="btnHdn" runat="server" Text="Button" Style="display: none;" />


 <cc1:ModalPopupExtender ID="modalPopupError" runat="server" TargetControlID="btnHdnErrorOk" PopupControlID="pnl_ErrorConfirmed" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>
    <asp:Panel ID="pnl_ErrorConfirmed" runat="server" Style="display: none;" CssClass="modalPopup">

        <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Green" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PopupError %>"></asp:Label>

        <asp:Button ID="btnErrorOk" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Ok %>" OnClick="btnErrorOk_Click" />
    </asp:Panel>
    <asp:Button ID="btnHdnErrorOk" runat="server" Text="Button" Style="display: none;" />
<script type="text/javascript">

    function openInNewTab() {
        originalTarget = document.forms[0].target;
        document.forms[0].target = '_blank';
        window.setTimeout("document.forms[0].target=originalTarget;", 300);
        return true;
    }
    function setFormSubmitToFalse() {
        setTimeout(function () { _spFormOnSubmitCalled = false; }, 3000);
        return true;
    }
    $(document).ready(function () {

        $("#lnkSequences").text($("#lnkSequences").text().split('.').slice(0, -1).join('.'));

        $("#fileCert").find('[id$=btnUpload]').click(function () {

            var attachCatDrop = $("#certDrop").find('[id$=dropWithNewOption]');
            var textCat = $("#certDrop").find('[id$=txtNewOption]');
            if (attachCatDrop.val() == "") {
                var requiredCatAttach = $("#certDrop").find('[id$=reqDropWithNewOption]');
                ValidatorEnable(document.getElementById(requiredCatAttach[0].id), true);
                return false;
            }

            else if (attachCatDrop.val() == "New" && textCat.val() == "") {
                var requiredCatAttachText = $("#certDrop").find('[id$=reqNewOptionText]');
                ValidatorEnable(document.getElementById(requiredCatAttachText[0].id), true);
                return false;
            }


        });
        $("#fileCalc").find('[id$=btnUpload]').click(function () {

            if ($("#ddlCalcAttachmentCategory :selected").val() == "") {

                ValidatorEnable(document.getElementById('reqCalcAttachmentCategory'), true);
                return false;
            }

        });
        $('#txtStartDate').attr('readonly', true);
        $('#txtGraduationDate').attr('readonly', true);
        $('#txtStartDate').datepicker({
            dateFormat: "dd/mm/yy",
            maxDate: '0',
            changeYear: true,
            changeMonth: true,
            onSelect: function (selected) {
                var dt2 = $('#txtGraduationDate');
                dt2.datepicker("option", "minDate", selected)

            }

        });

        $('#txtGraduationDate').datepicker({
            dateFormat: "dd/mm/yy",
            showOn: 'focus',
            maxDate: '0',
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


        }

        else if ($('#rdbCertificateThroughScholarship input:checked').val() == "1") {

            $("#entityProviding").show();

        }



        if ($('#rdbWorkingOrNot input:checked').val() == "0") {


            $("#entityNeedsEquivalency").show();
            $("#entityWorkingFor").hide();

            $("#filenotWorking").show();
            $("#fileWorking").hide();
            // $("#occupation").hide();


            var entityWorkingDrop = $("#entityWorkingFor").find('[id$=reqDropWithNewOption]');
            ValidatorEnable(document.getElementById(entityWorkingDrop[0].id), false);
            var entityWorkingText = $("#entityWorkingFor").find('[id$=reqNewOptionText]');
            ValidatorEnable(document.getElementById(entityWorkingText[0].id), false);






        }
        else if ($('#rdbWorkingOrNot input:checked').val() == "1") {

            $("#entityWorkingFor").show();
            //  $("#occupation").show();
            $("#entityNeedsEquivalency").hide();

            $("#dropEntityNeedsEquivalency").val("");
            $("#fileWorking").show();

            $("#filenotWorking").hide();
            ValidatorEnable(document.getElementById('reqEntityNeedsEquivalency'), false);

        }


        $("#rdbCertificateThroughScholarship").change(function () {
            if ($('#rdbCertificateThroughScholarship input:checked').val() == "0") {

                $("#entityProviding").hide();
                $("#txtEntityProvidingStudy").val("");


            }
            else {
                $("#entityProviding").show();


            }
        });

        $("#rdbWorkingOrNot").change(function () {
            if ($('#rdbWorkingOrNot input:checked').val() == "0") {
                //Entity needs equivalency must be appeared and the rest must be disappeared


                $("#entityNeedsEquivalency").show();
                $("#entityWorkingFor").hide();
                $("#entityWorkingFor").find('[id$=dropWithNewOption]').val("");
                $("#entityWorkingFor").find('[id$=txtNewOption]').val("");
                $("#entityWorkingFor").find('[id$=txtNewOption]').hide();
                $("#filenotWorking").show();
                $("#fileWorking").hide();
                var entityWorkingDrop = $("#entityWorkingFor").find('[id$=reqDropWithNewOption]');
                ValidatorEnable(document.getElementById(entityWorkingDrop[0].id), false);
                var entityWorkingText = $("#entityWorkingFor").find('[id$=reqNewOptionText]');
                ValidatorEnable(document.getElementById(entityWorkingText[0].id), false);


                //$("#occupation").hide();
                //$("#txtOccupation").hide().val("");

            }
            else if ($('#rdbWorkingOrNot input:checked').val() == "1") {

                $("#entityWorkingFor").show();
                //$("#occupation").show();
                //$("#txtOccupation").show().val("");
                $("#entityNeedsEquivalency").hide();
                $("#filenotWorking").hide();
                $("#fileWorking").show();
                ValidatorEnable(document.getElementById('reqEntityNeedsEquivalency'), false);

            }
            //Delete Attachments if checked is changed

            var param = {};
            param.working = parseInt($('#rdbWorkingOrNot input:checked').val());

            $.ajax({
                type: "POST",
                url: _spPageContextInfo.siteAbsoluteUrl + "/_layouts/15/ITWORX.MOEHEWF.UCE/CascadingDropdowns.aspx/DeleteOrganizationalAttachment",
                data: JSON.stringify(param),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {

                    console.log(result.d);

                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if (jqXHR.status == 500) {
                        console.log('Internal error: ' + jqXHR.responseText);
                    } else {
                        console.log('Unexpected error.');
                    }
                }
            });
            __doPostBack("Registration", "BindWorkingAttachments");
        });


        function ValidateControls() {
            var returnform = true;
            var returnValue = true;


            if (
                /*($("#reqOccupation").css("visibility") == "visible" && $("#reqOccupation").css("display") != "none") ||*/
                ($("#reqEntityNeedsEquivalency").css("visibility") == "visible" && $("#reqEntityNeedsEquivalency").css("display") != "none") ||
                ($("#reqStudyLocation").css("visibility") == "visible" && $("#reqStudyLocation").css("display") != "none") ||
                ($("#reqExamLocation").css("visibility") == "visible" && $("#reqExamLocation").css("display") != "none") ||
                ($("#reqEntityProvidingStudy").css("visibility") == "visible" && $("#reqEntityProvidingStudy").css("display") != "none") ||

                ($("#reqStartDate").css("visibility") != "visible" && $("#reqStartDate").css("display") != "none") ||
                returnform == false) {
                return false;
            }
            return returnValue;
        }

    });
</script>
