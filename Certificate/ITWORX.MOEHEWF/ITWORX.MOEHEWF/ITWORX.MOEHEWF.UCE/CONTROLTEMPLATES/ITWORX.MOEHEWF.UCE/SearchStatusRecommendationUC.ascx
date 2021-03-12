<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="UploadUC.ascx" TagPrefix="uc1" TagName="UploadUC" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="MOEHE" TagName="FileUpload" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="uc2" TagName="FileUpload" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.UCE/CommityDecision.ascx" TagPrefix="MOEHE" TagName="CommityDecision" %>





<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchStatusRecommendationUC.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.SearchStatusRecommendationUC" %>
<style>
    .moe-radioBtn.moe-radioBtn-row tbody>tr{
        width: auto !important;
        margin-left:50px;
    }
    .moe-section-title{
        color: #8d163a;
    }
    .moe-section-cntnr{
        border: 1px solid #eaebed;
        padding: 10px;
    }
    .moe-fieldset legend{
        text-align:right !important;
    }
  
</style>
<script>
    function changeDecisionTemp() {
        if ($("#<%=rdbtn_EmpRecommendation.ClientID%> input:checked").val() == "1") {
            $("#lbl_Approve").css("display", "none");
            $("#lbl_Reject").css("display", "block");
        }
        else {
            $("#lbl_Approve").css("display", "block");
            $("#lbl_Reject").css("display", "none");
        }
    }

   

    $(function () {

        $('#txt_bookDate').attr('readonly', true); 
        $('#txt_bookDate').datepicker({
            dateFormat: "dd/mm/yy", 
            changeYear: true,
            changeMonth: true 

        });
    })

    function setFormSubmitToFalse() {
        setTimeout(function () { _spFormOnSubmitCalled = false; }, 3000);
        return true;
    }
</script> 
<asp:HiddenField ID="hdn_RequestNumber" runat="server" />



<%--/////  new read only summary --%>
<div class="container">
    <div class="row margin-top-15">

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
            <div class="data-container table-display moe-full-width moe-sm-full-width">
                <div class="form-group">
                    <h3 class="margin-bottom-15 margin-top-0 moe-section-title">
                        <asp:Label ID="Label9" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ApplicantBasicData %>"></asp:Label>
                    </h3>
                    <div class="moe-section-cntnr heighlighted-section flex-display flex-wrap">
                        <div class="col-md-4 col-sm-6">
                            <div class="data-container">
                                <h6 class="font-size-16 margin-bottom-15">
                                    <asp:Label ID="lblPersonalID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PersonalID %>" Font-Bold="true"></asp:Label>

                                </h6>
                                <h5 class="font-size-20">
                                    <asp:Label ID="lblPersonalIDValue" runat="server"></asp:Label>
                                </h5>
                            </div>
                        </div>

                        <div class="col-md-4 col-sm-6">
                            <div class="data-container">
                                <h6 class="font-size-16 margin-bottom-15">
                                    <asp:Label ID="lblName" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ApplicantName %>" Font-Bold="true"></asp:Label>

                                </h6>
                                <h5 class="font-size-20">
                                    <asp:Label ID="lblNameValue" runat="server"></asp:Label>
                                </h5>
                            </div>
                        </div>

                        <div class="col-md-4 col-sm-6">
                            <div class="data-container">
                                <h6 class="font-size-16 margin-bottom-15">
                                    <asp:Label ID="lblAcademicDegreeForEquivalence" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, AcademicDegree %>"></asp:Label>
                                </h6>
                                <h5 class="font-size-20">

                                    <asp:Label ID="lblAcademicDegreeForEquivalenceValue" runat="server"></asp:Label>
                                </h5>
                            </div>
                        </div>



                        <div class="col-md-4 col-sm-12">
                            <div class="data-container">
                                <h6 class="font-size-16 margin-bottom-15">
                                    <asp:Label ID="lblCerticateAcademicDegree" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CertificateAcademicDegree %>"></asp:Label>
                                </h6>
                                <h5 class="font-size-20">

                                    <asp:Label ID="lblCerticateAcademicDegreeValue" runat="server"></asp:Label>
                                </h5>
                            </div>
                        </div>


                        <div class="col-md-4 col-sm-12">
                            <div class="data-container">
                                <h6 class="font-size-16 margin-bottom-15">
                                    <asp:Label ID="lblCountries" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CountryOfStudy %>"></asp:Label>
                                     <asp:Label ID="lblNewCoutriesAdded" runat="server" Visible="false" CssClass="fa fa-exclamation-circle display-icon" title="<%$Resources:ITWORX_MOEHEWF_UCE, NewCountryAdded %>" />

                                </h6>
                                <h5 class="font-size-20">

                                    <asp:Label ID="lblCountriesValue" runat="server"></asp:Label>
                                </h5>
                            </div>
                        </div>


                        <div class="col-md-4 col-sm-12">
                            <div class="data-container">
                                <h6 class="font-size-16 margin-bottom-15">
                                    <asp:Label ID="lblUniversity" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, University %>"></asp:Label>
                                    <asp:Label ID="lblNewUniversitiesAdded" runat="server" Visible="false" CssClass="fa fa-exclamation-circle display-icon" title="<%$Resources:ITWORX_MOEHEWF_UCE, NewUniversityAdded %>" />

                                </h6>
                                <h5 class="font-size-20">
                                    <asp:Label ID="lblUniversityValue" runat="server"></asp:Label>
                                    <asp:Label ID="lblUniversityNotFoundValue" runat="server" Visible="false"></asp:Label>

                                </h5>
                            </div>
                        </div>


                       <%-- university type to be added--%>
                        <div class="col-md-4 col-sm-12">
                            <div class="data-container">
                                <h6 class="font-size-16 margin-bottom-15">
                                    <asp:Label ID="lblUniversityType" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityType %>"></asp:Label>

                                </h6>
                                <h5 class="font-size-20">
                                    <asp:Label ID="lblUniversityTypeValue" runat="server"></asp:Label>

                                </h5>
                            </div>
                        </div>

                        <div class="col-md-4 col-sm-12">
                            <div class="data-container">
                                <h6 class="font-size-16 margin-bottom-15">
                                    <asp:Label ID="lblFaculty" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Faculty %>"></asp:Label>

                                </h6>
                                <h5 class="font-size-20">
                                    <asp:Label ID="lblFacultyValue" runat="server"></asp:Label>
                                    <asp:Label ID="lblFacultyNotFoundValue" runat="server" Visible="false"></asp:Label>


                                </h5>
                            </div>
                        </div>

                        <div class="col-md-4 col-sm-12">
                            <div class="data-container">
                                <h6 class="font-size-16 margin-bottom-15">
                                    <asp:Label ID="lblSpecialization" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Specialization %>"></asp:Label>

                                </h6>
                                <h5 class="font-size-20">
                                    <asp:Label ID="lblSpecializationValue" runat="server"></asp:Label>
                                    <asp:Label ID="lblSpecializationNotFoundValue" runat="server" Visible="false"></asp:Label>



                                </h5>
                            </div>
                        </div>

                        <div class="col-md-4 col-sm-12">
                            <div class="data-container">
                                <h6 class="font-size-16 margin-bottom-15">
                                    <asp:Label ID="lblStartDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, StudyStartDate %>" Font-Bold="true"></asp:Label>

                                </h6>
                                <h5 class="font-size-20">
                                    <asp:Label ID="lblStartDateValue" runat="server" />

                                </h5>
                            </div>
                        </div>

                        <div class="col-md-4 col-sm-12">
                            <div class="data-container">
                                <h6 class="font-size-16 margin-bottom-15">
                                    <asp:Label ID="lblGraduationDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, GraduationDate %>" Font-Bold="true"></asp:Label>

                                </h6>
                                <h5 class="font-size-20">
                                    <asp:Label ID="lblGraduationDateValue" runat="server" />

                                </h5>
                            </div>
                        </div>

                        <div class="col-md-4 col-sm-12">
                            <div class="data-container">
                                <h6 class="font-size-16 margin-bottom-15">
                                    <asp:Label ID="lblUniversityList" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversitiesLists %>" Font-Bold="true" Visible="false"></asp:Label>

                                </h6>
                                <h5 class="font-size-20">
                                    <asp:Label ID="lblUniversityListValue" runat="server" Visible="false"></asp:Label>

                                </h5>
                            </div>
                        </div>

                        <div class="col-md-4 col-sm-12">
                            <div class="data-container">
                                <h6 class="font-size-16 margin-bottom-15">
                                    <asp:Label ID="lblHavePA" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, HavePA %>" Font-Bold="true"></asp:Label>

                                </h6>
                                <h5 class="font-size-20">
                                      <asp:Label ID="lblHavePAValue" runat="server"></asp:Label>

                                </h5>
                            </div>
                        </div>

                        <div class="col-md-4 col-sm-12">
                            <div class="data-container">
                                <h6 class="font-size-16 margin-bottom-15">
                                    <asp:Label ID="lblUniversityHEDD" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityHEDD %>" Font-Bold="true" ></asp:Label>
                                </h6>
                                <h5 class="font-size-20">
                                    <asp:Label ID="lblUniversityHEDDValue" runat="server"></asp:Label>

                                </h5>
                            </div>
                        </div>


                        <%--files--%>

                        <div class="col-md-12 col-xs-12">
                                <div class="form-group">
                    <h6 class="font-size-22 margin-bottom-15 margin-top-10">
                        <asp:Label ID="Label10" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RequestAttachments %>"></asp:Label>
                        <asp:HiddenField ID="hdnCertificateAcademic" runat="server" />
                         <asp:HiddenField ID="hdnCertificateAcademicTxt" runat="server" />
                    </h6>
                     
                <asp:Panel ID="pnlAttachments" runat="server">
                    <div id="divAttachments" class="row">
                         <div class="col-md-7 col-sm-12 col-xs-12  auto-height">
                            <h6 class="font-size-18 margin-bottom-0 margin-top-0 font-family-sans" style="color: black">
                                <asp:Label ID="lblCertEquivalent" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CertificateToBeEquivalent %>" Font-Bold="true"></asp:Label>
                            </h6>
                            <uc2:FileUpload runat="server" id="fileEquivalent" />
                        </div>
                         <div class="col-md-7 col-sm-12 col-xs-12  auto-height">
                            <h6 class="font-size-18 margin-bottom-0 margin-top-0 font-family-sans" style="color: black">
                                <asp:Label ID="lblGrades" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Grades %>" Font-Bold="true"></asp:Label>
                            </h6>
                            <uc2:FileUpload runat="server" id="fileGrades" />
                        </div>
                        <div class="col-md-7 col-sm-12 col-xs-12  auto-height">
                            <h6 class="font-size-18 margin-bottom-0 margin-top-0 font-family-sans" style="color: black">
                                <asp:Label ID="lbl_OrgAttach" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, OrganizationlLetterCopy %>" Font-Bold="true"></asp:Label>
                            </h6>
                            <uc2:FileUpload runat="server" id="OrgAttach" />
                        </div>
                        <div class="col-md-7 col-sm-12 col-xs-12  auto-height">
                            <h6 class="font-size-18 margin-bottom-0 margin-top-0 font-family-sans" style="color: black">
                                <asp:Label ID="lbl_Certificates" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PrevCertificates %>" Font-Bold="true"></asp:Label>
                            </h6>
                            <uc2:FileUpload runat="server" id="Certificates" />
                        </div>

                         <div class="col-md-7 col-sm-12 col-xs-12   auto-height" id="diploma" runat="server" visible="false">
                            <h6 class="font-size-18 margin-bottom-0 margin-top-0 font-family-sans" style="color: black">
                                <asp:Label ID="lblDiploma" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Diploma %>" Font-Bold="true"></asp:Label>
                            </h6>
                            <uc2:FileUpload runat="server" id="fileDiploma" />
                        </div>
                         <div class="col-md-7 col-sm-12 col-xs-12  margin-top-15 auto-height" id="interDiploma" runat="server" visible="false">
                            <h6 class="font-size-18 margin-bottom-0 margin-top-0 font-family-sans" style="color: black">
                                <asp:Label ID="lblInterDiploma" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, IntermediateDiploma %>" Font-Bold="true"></asp:Label>
                            </h6>
                            <uc2:FileUpload runat="server" id="fileInterDiploma" />
                        </div>

                         <div class="col-md-7 col-sm-12 col-xs-12   auto-height">
                            <h6 class="font-size-18 margin-bottom-0 margin-top-0 font-family-sans" style="color: black">
                                <asp:Label ID="lblCertificateSequence" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CertificateSequence %>" Font-Bold="true"></asp:Label>
                            </h6>
                            <uc2:FileUpload runat="server" id="CertificateSequence" />
                        </div>
               



                        <div class="col-md-7 col-sm-12 col-xs-12  auto-height">
                            <h6 class="font-size-18 margin-bottom-0 margin-top-0 font-family-sans" style="color: black">
                                <asp:Label ID="lbl_Delegates" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, DelegationTemplates %>" Font-Bold="true"></asp:Label>
                            </h6>
                            <uc2:FileUpload runat="server" id="Delegates" />
                        </div>
                        <div class="col-md-7 col-sm-12 col-xs-12   auto-height">
                            <h6 class="font-size-18 margin-bottom-0 margin-top-0 font-family-sans" style="color: black">
                                <asp:Label ID="lblHavePAAttachment" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ApplicantHavePA %>" Font-Bold="true"></asp:Label>
                            </h6>
                            <uc2:FileUpload runat="server" id="fileUploadHavePA" />
                        </div>
               <div class="col-md-7 col-sm-12 col-xs-12   auto-height">
                            <h6 class="font-size-18 margin-bottom-0 margin-top-0 font-family-sans" style="color: black">
                                <asp:Label ID="lblHaveNoPA" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, AplicantNoHavePA %>" Font-Bold="true"></asp:Label>
                            </h6>
                            <uc2:FileUpload runat="server" id="fileUploadNoHavePA" />
                        </div>
               

                    </div>
                </asp:Panel>
                    
                    </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</div>
<%--end new read only summary--%>
<%--//////--%>
<%--files--%>


<%--<div class="cotainer display-mode">

     <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
            <div class="data-container table-display moe-full-width moe-sm-full-width">
                
                </div>
         </div>
    </div>--%>


<%--end files--%>
<%--////////////////////--%>
<%--بحث الحالةstart--%>
<div class="container unheighlighted-section margin-bottom-50 margin-top-25 flex-display flex-wrap moe-fieldset" id="DialogProc" >
    <asp:Panel ID="pnl_EmpRecommendation" runat="server" GroupingText="<%$Resources:ITWORX_MOEHEWF_UCE, EmployeeOpinion%>" CssClass="stateTitle moe-full-width">

        <asp:HiddenField ID="hdn_ID" runat="server" />
         
        <div class="moe-section-cntnr">

        <div class="row margin-top-15">
            <div class="col-md-4 col-sm-12 col-xs-12 no-padding">
            <div class="data-container table-display moe-width-85">
                <div class="form-group">
                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">

                        <asp:Label ID="lblGainedHours" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NumberOfHoursGained %>"></asp:Label></h6>
                    <div class="form">
                        <asp:TextBox ID="txtGainedHours" MaxLength="8" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="regGainedHours" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RegNumbersOnly %>" ValidationExpression="^\d+$" ControlToValidate="txtGainedHours" CssClass="moe-full-width error-msg" ValidationGroup="Save" Display="Dynamic"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
        </div>

            <!--10th Field-->
            <div class="col-md-4 col-sm-12 col-xs-12 no-padding">
                <div class="data-container table-display moe-width-85">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-0 margin-top-0">

                            <asp:Label ID="lblOnlineHours" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NumberOfOnlineHours %>"></asp:Label></h6>

                        <div class="form">
                            <asp:TextBox ID="txtOnlineHours" MaxLength="8" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="regOnlineHours" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RegNumbersOnly %>" ValidationExpression="^\d+$" ControlToValidate="txtOnlineHours" CssClass="moe-full-width error-msg" ValidationGroup="Save" Display="Dynamic"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
            </div>
            <!--12th Field-->
            <div class="col-md-4 col-sm-12 col-xs-12 no-padding">
                <div class="data-container table-display">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                            <asp:Label ID="lblOnlinePercentage" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PercentageOfOnlineHours %>"></asp:Label></h6>

                        <div class="form">
                            <asp:TextBox ID="txtOnlinePercentage" MaxLength="8" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="regOnlinePercentage" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RegPercNumbersOnly %>" ValidationExpression="^[0-9]*\.?[0-9]*" runat="server" ControlToValidate="txtOnlinePercentage" CssClass="moe-full-width error-msg" ValidationGroup="Save" Display="Dynamic"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
            </div>
        </div>
         
        
       

        <div class="row margin-top-15">
            <div class="col-md-12 col-sm-12 col-xs-12 no-padding">
            <div class="data-container table-display moe-width-85">
                <div class="form-group">
                    <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="Label4" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, HonoraryDegree %>"></asp:Label>
                </h6>
                    <div class="form">
                        <asp:RadioButtonList ID="rblHonoraryDegree" runat="server" CssClass="moe-radioBtn moe-radioBtn-row" >
                            <asp:ListItem Value="0" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Successful %>"></asp:ListItem>
                            <asp:ListItem Value="1" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Ordinary %>"></asp:ListItem>
                            <asp:ListItem Value="2" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Honor %>"></asp:ListItem>
                        </asp:RadioButtonList>
                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, Required %>"
                            ValidationGroup="Save" ControlToValidate="rblOwners" CssClass="moe-full-width error-msg"></asp:RequiredFieldValidator>
   --%>                 </div>
                </div>
            </div>
        </div>
<%--          <div class="col-md-6 col-sm-12 col-xs-12 no-padding">
                <div class="data-container table-display  moe-sm-full-width sm-width-90">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                            <asp:Label ID="Label2" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityName %>"></asp:Label></h6>
                       

                        <div class="form">
                           <asp:RadioButtonList ID="rblUniversity" runat="server" CssClass="moe-radioBtn">
                                <asp:ListItem Value="0" Text="<%$Resources:ITWORX_MOEHEWF_UCE, universityGovernment %>"></asp:ListItem>
                                <asp:ListItem Value="1" Text="<%$Resources:ITWORX_MOEHEWF_UCE, universityPrivate %>"></asp:ListItem>
                            </asp:RadioButtonList>
                           
                        </div>
                    </div>
                </div>
            </div>--%>

           
          

        </div>
           <div class="row margin-top-15">
                <div class="col-md-6 col-sm-12 col-xs-12 no-padding">
            <div class="data-container table-display moe-width-85">
                <div class="form-group">
                    <h6 class="font-size-16 margin-bottom-0 margin-top-0" style="visibility: hidden;">
                            hidden
                        </h6>
                    <div class="form">
                        <asp:CheckBox ID="ckbHaveException" runat="server"  OnCheckedChanged="ckbHaveException_CheckedChanged" AutoPostBack="true"/>
                           <label> <asp:Label ID="lblckbHaveException" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, HaveException %>"  ></asp:Label></label>
                                     </div>
                </div>
            </div>
        </div>

            <div class="col-md-6 col-sm-12 col-xs-12 no-padding">
            <div class="data-container table-display ">
                <div class="form-group">
                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">

                        <asp:Label ID="lblExceptionFrom" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ExceptionFrom %>"></asp:Label></h6>
                    <div class="form">
                        <asp:TextBox ID="txtExceptionFrom"  runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldExceptionFrom" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredExceptionFrom %>"
                                    ValidationGroup="Save" ControlToValidate="txtExceptionFrom" CssClass="moe-full-width error-msg"></asp:RequiredFieldValidator>
                                 </div>
                </div>
            </div>
        </div>

            
        </div>
        <div class="row margin-top-15">
            <div class="col-md-8 col-xs-12">
                <MOEHE:FileUpload runat="server" id="StatusRecommendAttachements" />
            </div>
      </div>

        <div class="row margin-top-15">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
                    <div class="data-container table-display moe-full-width  moe-sm-full-width">
                        <div class="form-group">
                            <h6 class="font-size-16 margin-bottom-0 margin-top-0 ">
                                
                                <asp:Label ID="lbl_AddEmpOpinion" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EmployeeOpinion  %>"></asp:Label>
                                <span class="error-msg"> *</span>
                            </h6>
                            
                            <div class="form">
                                <asp:TextBox ID="txt_EmpOpinion" runat="server" TextMode="MultiLine" CssClass="moe-full-width input-height-42 border-box moe-input-padding text-area"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqVal_EmpOpinion" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredEmpOpinion %>"
                                    ValidationGroup="Save" ControlToValidate="txt_EmpOpinion" CssClass="moe-full-width error-msg"></asp:RequiredFieldValidator>
                            </div>
                            
                        </div>
                    </div>
                </div>
        </div>
        </div>

        </div>
<%--بحث الحالةend --%>
<%--start employee recomendation--%>
    <div class="container moe-fieldset margin-top-25 ">
          <asp:Panel ID="Panel1" runat="server" GroupingText="<%$Resources:ITWORX_MOEHEWF_UCE, EmpRecommendationAndFinalDecision%>" CssClass="stateTitle moe-full-width">
           <!-- </asp:Panel>  -->
         <div class="moe-section-cntnr">
        <div class="row margin-top-15">
                
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
                    <div class="data-container table-display moe-width-85  moe-sm-full-width">
                        <h6 class="font-size-16 margin-bottom-0 margin-top-0 ">
                                <asp:Label ID="Label7" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EmpRecommendation %>"></asp:Label>
                            <span class="error-msg"> *</span>
                        </h6>
                        <div class="form-group">
                            <asp:RadioButtonList ID="rdbtn_EmpRecommendation" runat="server" CssClass="moe-radioBtn moe-radioBtn-row" onclick="changeDecisionTemp()">
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="reqVal_EmpRecommendation" runat="server" ForeColor="Red" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredEmpRecommendation %>"
                                ValidationGroup="Save" ControlToValidate="rdbtn_EmpRecommendation" CssClass="moe-full-width error-msg"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
                    <div class="data-container table-display moe-full-width moe-sm-full-width">
                        <div class="form-group">
                            <h6 class="font-size-16 margin-bottom-0 margin-top-0 ">
                                <asp:Label ID="lbl_DecisiontxtForPrinting" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, DecisiontxtForPrinting2 %>"></asp:Label>
                            </h6>
                            <div class="form recommend-form heighlighted-section">
                               <h1>
                                    <asp:DropDownList ID="drp_Sir" runat="server" CssClass="moe-input-padding moe-select input-height-42">
                                        
                                    </asp:DropDownList>
                                    <%--<label><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","sir") %></label>--%>
                                   <span class="error-msg"> *</span>
                                    <asp:TextBox ID="txtOccupationName" runat="server" CssClass="moe-input-padding moe-select input-height-42"></asp:TextBox>   
                                      <asp:DropDownList ID="drp_recpected" runat="server" CssClass="moe-input-padding moe-select input-height-42"  >
                                        
                                    </asp:DropDownList>
                                    <%--<label><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","recpected") %></label>--%>
                                  
                               </h1>
                                 <asp:RequiredFieldValidator ID="txtOccupationNameValidator" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredOccupationName %>" Display="Dynamic"
                                    ValidationGroup="Save" ControlToValidate="txtOccupationName" CssClass="moe-full-width error-msg"></asp:RequiredFieldValidator>
                                <h1>
                                    <asp:Label ID="lbl_EntityNeedsEquivalency" runat="server" Text=""></asp:Label> 
                                </h1><br />
                                <h1>
                                    <label><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","WelcomText") %></label>
                                </h1>
                                <br />
                                <h1>
                                    <asp:Label ID="lbl_accordingBook" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, accordingBook %>"></asp:Label> 
                                    <span class="error-msg"> *</span>
                                    <asp:TextBox ID="txt_bookNum"  runat="server" CssClass="moe-input-padding moe-select input-height-42"></asp:TextBox>  
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="" Display="Dynamic"
                                    ValidationGroup="Save" ControlToValidate="txt_bookNum" CssClass="moe-full-width error-msg"></asp:RequiredFieldValidator>
                                    <asp:Label ID="lbl_inDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, inDate %>"></asp:Label> 
                                    <span class="error-msg"> *</span>
                                    <asp:TextBox ID="txt_bookDate" ClientIDMode="Static" runat="server" CssClass="moe-input-padding moe-select input-height-42"></asp:TextBox>    
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="" Display="Dynamic"
                                    ValidationGroup="Save" ControlToValidate="txt_bookDate" CssClass="moe-full-width error-msg"></asp:RequiredFieldValidator>
                                    <asp:Label ID="lbl_RemainingDecicionBoby" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RemainingDecicionBoby %>"></asp:Label> 
                                </h1>
                                <h1>
                                    <label id="lbl_Approve"><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","approveDecisionText") %></label>
                                    <label id="lbl_Reject" style="display:none"><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","rejectDecisionText") %></label>
                                    <span class="error-msg"> *</span>
                                    </h1>
                                <asp:TextBox ID="txt_DecisiontxtForPrinting" runat="server" TextMode="MultiLine" CssClass="moe-full-width input-height-42 border-box moe-input-padding text-area"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="req_DecisiontxtForPrinting" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredDecisiontxtForPrinting %>"
                                    ValidationGroup="Save" ControlToValidate="txt_DecisiontxtForPrinting" CssClass="moe-full-width error-msg"></asp:RequiredFieldValidator>
                                <h3>
                                    <label><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","AppreciationText") %></label>
                                </h3> 
                                <h4>
                                    <asp:Label ID="lbl_headManagerName" runat="server" ></asp:Label>  
                                </h4>
                                <h4>
                                    <label><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","EquivalencManager") %></label>
                                </h4> 
                            </div>
                        </div>
                    </div>
                </div>
            <div class="row margin-top-15">
                <div class="col-md-12 no-padding">
                    <asp:Button ID="btn_ReviewDecision" runat="server"  OnClick="ReviewDecisionClick" Text="<%$Resources:ITWORX_MOEHEWF_UCE, review %>" OnClientClick="return setFormSubmitToFalse();"  CssClass="pull-right"  />
                </div>
            </div>
        </div>
        </div>
<%--     <div class="row margin-top-15" >
               <div class="col-md-6 col-sm-12 col-xs-12 no-padding">
                <div class="data-container table-display moe-width-85">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-0 margin-top-0" style="visibility: hidden;">
                            hidden
                        </h6>
                        <h6 class="font-size-16 margin-bottom-0 margin-top-0" >
                            <asp:CheckBox ID="ckbHavePA" runat="server" />
                            <asp:Label ID="lblckbHavePA" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, HeHavePA %>" ></asp:Label>
                        </h6>
                            
                        </div>
                    </div>
                </div>
        </div>--%>
         
              <MOEHE:CommityDecision runat="server" id="CommityDecisionCtr" />



              <div class="row no-padding margin-top-15">

                <div class="col-md-12 col-sm-12 col-xs-12">
                    <h5 class="font-size-18 font-weight-600 margin-2 underline">
                        <asp:Label ID="lbl_NotificationMsg" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NotifyMsgProcRecommend %>"></asp:Label>
                    </h5>
                </div>
                  </div>
              <div class="row no-padding  margin-bottom-25">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <asp:Button ID="btn_ApproveProcedures" ValidationGroup="Save" runat="server" OnClick="btn_ApproveProcedures_Click" OnClientClick="return setFormSubmitToFalse();" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ApproveProcRecommend %>" CssClass="pull-right " />
                     <asp:Button ID="btn_Save" runat="server" ValidationGroup="Save" OnClick="btn_Save_Click" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Save %>" OnClientClick="return setFormSubmitToFalse();" CssClass="pull-right margin-left-20" />
                </div>
            </div>

          <%--  <div class="row margin-top-15">
             <div class="col-md-12 no-padding">--%>
               
          <%--   </div>
        </div>--%>
       
        <%--    <asp:Button ID="btn_ViewStatus" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ViewStatus %>"/>--%>
        <div class="row margin-top-15">
            <h5 class="col-md-12 font-size-18 font-weight-600 success-msg">
                <asp:Label ID="lbl_SaveSuccess" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, SavedSuccessfully %>" Visible="false" ForeColor="Green"></asp:Label>
            </h5>
        </div>

    </asp:Panel>
</div>
<%--end employee recomndation--%>

<div class="row no-padding margin-top-15">
    <h5 class="font-size-18 font-weight-600 success-msg">
        <asp:Label ID="lbl_Success" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, SuccessMsg %>" Visible="false" ForeColor="Green"></asp:Label>
    </h5>
</div>


<div class="container unheighlighted-section margin-bottom-50 flex-display flex-wrap  margin-top-25" id="DialogProc">
    <div id="viewControls" runat="server" visible="false" class="moe-section-cntnr">
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lbl_EmpOpinion" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EmployeeOpinion %>"></asp:Label>
                </h6>
                <h5 class="font-size-22">
                    <asp:Label ID="lbl_EmpOpinionVal" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lbl_Decisiontxt" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, DecisiontxtForPrinting %>"></asp:Label>
                </h6>
 
                 
                <div class="form recommend-form heighlighted-section">
                       <h1>
                            <label><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","sir") %></label>
                            <asp:Label ID="lbl_OccupationName" runat="server" Text=""></asp:Label>                       
                            <label><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","recpected") %></label>
                       </h1>
                        <h1>
                            <asp:Label ID="lbl_EntityNeedsEquivalencyView" runat="server" Text=""></asp:Label> 
                        </h1><br />
                        <h1>
                            <label><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","WelcomText") %></label>
                        </h1>
                        <br />
                        <h1>
                            <asp:Label ID="lbl_decicionBobyView" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, DecisionBody %>"></asp:Label> 
                        </h1>
                        <h1>
                            <asp:Label ID="lbl_DecisiontxtVal" runat="server"></asp:Label>
                        </h1>
                        <h3>
                            <label><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","AppreciationText") %></label>
                        </h3> 
                        <h4>
                            <asp:Label ID="lbl_headManagerNameView" runat="server" ></asp:Label> 
                        </h4>
                        <h4>
                            <label><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","EquivalencManager") %></label>
                        </h4> 
                    </div>
            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lbl_Recommendation" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EmpRecommendation %>"></asp:Label>
                </h6>
                <h5 class="font-size-22">
                    <asp:Label ID="lbl_RecommendationVal" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
        <div class="col-md-12 no-padding">
            <MOEHE:FileUpload runat="server" id="ViewStatusRecommendAttachements" />

        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="Label1" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NumberOfHoursGained %>"></asp:Label>
                </h6>
                <h5 class="font-size-22">
                    <asp:Label ID="lbltxtGainedHours" Text="N/A" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="Label3" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NumberOfOnlineHours %>"></asp:Label>
                </h6>
                <h5 class="font-size-22">
                    <asp:Label ID="lbltxtOnlineHours" Text="N/A" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="Label5" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PercentageOfOnlineHours %>"></asp:Label>
                </h6>
                <h5 class="font-size-22">
                    <asp:Label ID="lbltxtOnlinePercentage" Text="N/A" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
         <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lblHonoraryDegree" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, HonoraryDegree %>"></asp:Label>
                </h6>
                <h5 class="font-size-22">
                    <asp:Label ID="lblrblHonoraryDegree" Text="N/A" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
         <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="Label6" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, HeHavePA %>"></asp:Label>
                </h6>
                <h5 class="font-size-22">
                    <asp:Label ID="valckbHavePA" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
         <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="Label8" runat="server" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityName %>"></asp:Label>
                </h6>
                <h5 class="font-size-22">
                    <asp:Label ID="lblrblUniversity" Visible="false" Text="N/A" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
        </div>
    </div>
</div>

