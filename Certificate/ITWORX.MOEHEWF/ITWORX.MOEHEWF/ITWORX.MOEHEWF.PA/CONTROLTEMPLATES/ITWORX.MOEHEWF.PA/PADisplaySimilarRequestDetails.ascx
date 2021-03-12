<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PADisplaySimilarRequestDetails.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.DisplaySimilarPARequestDetails" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="uc1" TagName="FileUpload" %>



<%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>--%>
<script type="text/javascript">
    function Print(a) {
        var row = $(a).closest("tr").clone(true);
        var printWin = window.open('', '', 'left=0", ",top=0,width=1000,height=600,status=0');
        var table = $("[id*=divformControls]").clone(true);
        $("tr", table).not($("tr:first-child", table)).remove();
        table.append(row);
        $("tr td:last,tr th:last", table).remove();
        var dv = $("<div />");
        dv.append(table);
        printWin.document.write(dv.html());
        printWin.document.close();
        printWin.focus();
        printWin.print();
        printWin.close();
    }
</script>


<div id="divformControls" runat="server">
    <div id="divUser" class="row no-padding">
        <h1 class="section-title text-center font-weight-500 margin-bottom-25">
            <asp:Label ID="lblWelcomeUser" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, WelcomeUser %>"></asp:Label>
            <asp:Label ID="lblUserName" runat="server"></asp:Label>
        </h1>
    </div>


    <div id="divRequestDetails" class="row heighlighted-section margin-bottom-50 flex-display flex-wrap">
        <h1 class="section-title text-center font-weight-500 margin-bottom-25">
            <asp:Label ID="lblRequestDetails" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RequestDetails %>"></asp:Label>
        </h1>

        <div class="col-md-4 col-sm-6">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lblRequestNumber" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RequestNumber %>"></asp:Label>
                </h6>
                <h5 class="font-size-20">
                    <asp:Label ID="lblRequestNumberValue" runat="server"></asp:Label>
                </h5>
            </div>
        </div>

        <div class="col-md-4 col-sm-6">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lblRequestCreationDate" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RequestCreationDate %>"></asp:Label>
                </h6>
                <h5 class="font-size-20">
                    <asp:Label ID="lblRequestCreationDateValue" runat="server"></asp:Label>

                </h5>
            </div>
        </div>

        <div class="col-md-4 col-sm-6">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lblRequestSubmitDate" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RequestSubmitDate %>"></asp:Label>
                </h6>
                <h5 class="font-size-20">
                    <asp:Label ID="lblRequestSubmitDateValue" runat="server"></asp:Label>
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
    </div>


    <div class="row unheighlighted-section test-display">
        <!--Start of Accordion-->
        <div class="accordion PAaccordion">
            <!--First Tab-->
            <h3>
                <asp:Button ID="btnDegreeRequiredToBEquivalentData" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, DegreeRequiredToBEquivalentData %>"></asp:Button>
            </h3>
            <div class="row margin-top-25">
                <!--Start of First Tab-->
                <asp:Panel ID="pnlDegreeRequiredToBEquivalentData" runat="server">
                    <div class="row margin-top-15">

                        <div class="accordion-inner">
                            <h3 class="inner-accordion">
                                <asp:Label ID="lblCertificatesData" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CertificatesData %>" Font-Bold="true"></asp:Label>

                            </h3>
                            <div id="divCertificatesData" class="display-mode">
                                <div class="row margin-top-15">
                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblCerticateAcademicDegree" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CertificateAcademicDegree %>" runat="server" Font-Bold="true"></asp:Label>
                                                   
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblCerticateAcademicDegreeValue" runat="server"></asp:Label>

                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">


                                                    <asp:Label ID="lblCertificateThroughScholarship" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CertificateThroughScholarship %>" Font-Bold="true"></asp:Label>
                                                  
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblCertificateThroughScholarshipValue" runat="server"></asp:Label>


                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">

                                                    <asp:Label ID="lblEntityProvidingStudy" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EntityProvidingStudy %>"></asp:Label>
                                                    
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblEntityProvidingStudyValue" runat="server"></asp:Label>

                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">

                                                    <asp:Label ID="lblEntityProvidingOther" runat="server" Font-Bold="true" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Other %>"></asp:Label>
                                                    
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblEntityProvidingOtherValue" runat="server" Visible="false"></asp:Label>


                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">

                                                    <asp:Label ID="lblCampusStudy" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CampusStudy %>" Font-Bold="true"></asp:Label>
                                                  
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblCampusStudyValue" runat="server"></asp:Label>

                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">

                                                    <asp:Label ID="lblStudyLocation" runat="server" Font-Bold="true" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, StudyLocation %>">  </asp:Label>
                                                    
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblStudyLocationValue" runat="server" Visible="false"> </asp:Label>


                                                   
                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblCampusExam" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CampusExam %>"></asp:Label>
                                                   
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblCampusExamValue" runat="server"></asp:Label>

                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">

                                                    <asp:Label ID="lblExamLocation" runat="server" Font-Bold="true" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ExamLocation %>"></asp:Label>
                                              
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblExamLocationValue" runat="server" Visible="false"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                           
                                

                            </div>

                            <h3 class="inner-accordion">
                                <asp:Label ID="lblUniversitiesDetails" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversitiesDetails %>" Font-Bold="true"></asp:Label>
                            </h3>
                            <div id="divUniversitiesDetails" class="display-mode">
                                <div class="row margin-top-15">

                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblCountries" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CountryOfStudy %>" Font-Bold="true"></asp:Label>
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblCountriesValue" runat="server"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblUniversity" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, University %>"></asp:Label>
                                                   
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblUniversityValue" runat="server"></asp:Label>
                                                
                                                </h5>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblUniversityNotFound" runat="server" Font-Bold="true" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EnterNewUniversity %>"></asp:Label>
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblUniversityNotFoundValue" runat="server" Visible="false"></asp:Label>

                                                </h5>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblUniversityList" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversitiesLists %>" Font-Bold="true" Visible="false"></asp:Label>
                                                   
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblUniversityListValue" runat="server" Visible="false"></asp:Label>
                                                
                                                </h5>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                 
                                                    <asp:Label ID="lblSpecialization" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Specialization %>"></asp:Label>
                                                
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblSpecializationValue" runat="server"></asp:Label>
                                                    
                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblSpecializationNotFound" runat="server" Font-Bold="true" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EnterNewSpecialization %>"></asp:Label>
                                                  
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblSpecializationNotFoundValue" runat="server" Visible="false"></asp:Label>
                                                   
                                                </h5>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                           
                                                    <asp:Label ID="lblFaculty" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Faculty %>"></asp:Label>
                                                   
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblFacultyValue" runat="server"></asp:Label>
                                                    
                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblFacultyNotFound" runat="server" Font-Bold="true" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EnterNewFaculty %>"></asp:Label>
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblFacultyNotFoundValue" runat="server" Visible="false"></asp:Label>

                                                </h5>
                                            </div>
                                        </div>
                                    </div>


                                 


                                </div>

                                

                            </div>

                            <h3 class="inner-accordion">
                                <asp:Label ID="lblStudingDetails" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, StudyDetails %>" Font-Bold="true"></asp:Label>
                            </h3>
                            <div id="divStudyingDetails" class="display-mode">
                                <div class="row margin-top-15">
                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblStudyLanguage" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, StudyLanguage %>" Font-Bold="true"></asp:Label>
                                                    
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblStudyingLanguageValue" runat="server"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">


                                                    <asp:Label ID="lblStudingType" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, StudyType %>" Font-Bold="true"></asp:Label>
                                                  
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblStudyingTypeValue" runat="server"></asp:Label>

                                                  
                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblStudyingSystem" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, StudySystem %>" Font-Bold="true"></asp:Label>
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblStudyingSystemValue" runat="server" Visible="false"></asp:Label>

                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblStartDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, StudyStartDate %>" Font-Bold="true"></asp:Label>
                                                    
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblStartDateValue" runat="server" />
                                                  
                                                </h5>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                
                                                    <asp:Label ID="lblGraduationDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, GraduationDate %>" Font-Bold="true"></asp:Label>
                                                    
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblGraduationDateValue" runat="server" Visible="false" />
                                                    
                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                 
                                                    <asp:Label ID="lblStudyPeriod" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, AcademicProgramPeriod %>" Font-Bold="true"></asp:Label>
                                                    
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblStudyPeriodValue" runat="server"></asp:Label>
                                                    
                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">

                                                    <asp:Label ID="lblActualStudy" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ActualStudingPeriod %>" Font-Bold="true"></asp:Label>
                                                    
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblActualStudyValue" runat="server"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblGPA" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, GPA %>"></asp:Label>
                                                   
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblGPAValue" runat="server"></asp:Label>
                                                   
                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblOnlineHours" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NumberOfOnlineHours %>"></asp:Label>
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblOnlineHoursValue" runat="server"></asp:Label>

                                                </h5>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblGainedHours" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NumberOfHoursGained %>"></asp:Label>

                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblGainedHoursValue" runat="server"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblOnlinePercentage" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PercentageOfOnlineHours %>"></asp:Label>
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblOnlinePercentageValue" runat="server" Visible="false"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblCompExam" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, IsThereComprehensiveExam %>"></asp:Label>
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblCompExamValue" runat="server" />
                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblAcceptExam" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, IsThereAcceptanceExam %>"></asp:Label>
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblAcceptExamValue" runat="server" />
                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                    
                                   

                                    
                                </div>
                            

                            <h3 class="inner-accordion">
                                <asp:Label ID="lblUniversitiesNames" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityNames %>" Font-Bold="true"></asp:Label>
                            </h3>
                            <div id="divUniversities" class="display-mode">

                                <div class="row margin-top-15">

                                    <asp:Repeater ID="repCalculatedDetailsForCertificate" runat="server">

                                        <HeaderTemplate>
                                            <table cellspacing="0" rules="all" border="1">
                                                <tr>
                                                    <th scope="col" style="width: 80px">
                                                        <%=Resources.ITWORX_MOEHEWF_UCE.Country %>
                                                    </th>
                                                    <th scope="col" style="width: 80px">
                                                        <%=Resources.ITWORX_MOEHEWF_UCE.UniversityName %>
                                                    </th>
                                                    <th scope="col" style="width: 80px">
                                                        <%=Resources.ITWORX_MOEHEWF_UCE.FacultyName %>
                                                    </th>
                                                    <th scope="col" style="width: 80px">
                                                        <%=Resources.ITWORX_MOEHEWF_UCE.StudyType %>
                                                    </th>
                                                    <th scope="col" style="width: 80px">
                                                        <%=Resources.ITWORX_MOEHEWF_UCE.StudySystem %>
                                                    </th>
                                                    <th scope="col" style="width: 80px">
                                                        <%=Resources.ITWORX_MOEHEWF_UCE.StudyActualPeriod %>
                                                    </th>
                                                    <th scope="col" style="width: 80px">
                                                        <%=Resources.ITWORX_MOEHEWF_UCE.Actions %>
                                                    </th>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>

                                                <td>

                                                    <asp:Label ID="lblCalcSectionCountry" runat="server" Text='<%#Eval("Country.SelectedTitle") %>'></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCalcSectionUniversity" runat="server" Text='<%#Eval("Univesrity.SelectedTitle") %>'></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCalcSectionFaculty" runat="server" Text='<%#Eval("Faculty.SelectedTitle") %>'></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCalcSectionStudyType" runat="server" Text='<%#Eval("StudyType.SelectedTitle") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCalcSectionStudySystem" runat="server" Text='<%#Eval("StudySystem.SelectedTitle") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCalcSectionStudingPeriod" runat="server" Text='<%#Eval("StudyingPeriod") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:HiddenField ID="hdnCalcSectionID" runat="server" Value='<%#Eval("ID") %>' />
                                                    <asp:LinkButton ID="lnkCalcSectionDisplayAttach" runat="server" OnClick="lnkCalcSectionDisplayAttach_Click" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Display %>"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                        <FooterTemplate>

                                            </table>

                                        </FooterTemplate>

                                    </asp:Repeater>

                                    <uc1:FileUpload runat="server" id="fileUploadCalculatedDetails" Visible="false" />
                                    <asp:GridView ID="gridUniversitiesNames" runat="server" OnRowDataBound="gridUniversitiesNames_RowDataBound" CssClass="table moe-table table-striped result-table">
                                        <Columns>
                                            <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, Country %>">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblUniGridCountry" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityName %>">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblUniGridUniversity" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, FacultyName %>">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblUniGridFaculty" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, StudySystem %>">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblUniGridStudyingSystem" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, StudyType %>">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblUniGridStudyingType" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, StudyActualPeriod %>">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblUniGridStudingPeriod" runat="server"></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--     <asp:TemplateField>
                                                <ItemTemplate>


                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>


                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>

                                    </asp:GridView>
                                </div>
                            </div>

                            <h3 class="inner-accordion">
                                <asp:Label ID="lblPublishingCertificateUnivesity" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityPublishingCertificateDetails %>" Font-Bold="true"></asp:Label>
                            </h3>
                            <div id="divPublishingCertificate" class="display-mode">
                                <div class="row margin-top-15">

                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblUniversityMainHeadQuarter" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityMainHeadquarter %>"></asp:Label>
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblUniversityMainHeadQuarterValue" runat="server"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblNewUniversityMainHeadQuarter" runat="server" Font-Bold="true" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EnterNewCountry %>"></asp:Label>
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblNewUniversityMainHeadQuarterValue" runat="server" Visible="false"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblUniversityAddress" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityAddress %>"></asp:Label>
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblAddressValue" runat="server"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-sm-6 no-padding">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblUniversityEmail" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityEmail %>" Font-Bold="true"></asp:Label>
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblUniversityEmailValue" runat="server"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                             </div>


                        </div>


                        </div>

</asp:Panel>

            </div><!--End of First Tab-->
            </div>
        </div>


  

    <h3>
        <asp:Button ID="btnWorkingDetails" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, WorkingDetails %>"></asp:Button>
    </h3>
    <div class="row margin-top-25"><!--Start of 2nd Tab-->
        <asp:Panel ID="pnlWorkingDetails" runat="server">
            <div id="divWorkingDetails" class="display-mode">
                <asp:Label ID="lblWorkingDetails" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, WorkingDetails %>" Font-Bold="true"></asp:Label>
                <br />
                <asp:Label ID="lblWorkingOrNot" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, WorkingOrNot %>" Font-Bold="true"></asp:Label>
                <asp:Label ID="lblWorkingOrNotValue" runat="server"></asp:Label>

                <br />
                <%-- <asp:Label ID="lblEntityNeedsEquivalency" runat="server"></asp:Label>
                dropdown needed for the 3 dropdowns from user control
                <asp:Label ID="lblEntityWorkingFor" runat="server"></asp:Label>
                <asp:Label ID="lblOccupation" runat="server"></asp:Label>--%>
                <asp:Label ID="lblEntityNeedsEquivalency" runat="server" Font-Bold="true" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EntityNeedsEquivalency %>"></asp:Label>
                <asp:Label ID="lblEntityNeedsEquivalencyValue" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblOtherEntityNeedsEquivalency" runat="server" Font-Bold="true" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Other %>"></asp:Label>
                <asp:Label ID="lblOtherEntityNeedsEquivalencyValue" runat="server" Visible="false"></asp:Label>
                <br />
                <asp:Label ID="lblEntityWorkingFor" runat="server" Font-Bold="true" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EntityWorkingFor %>"></asp:Label>
                <asp:Label ID="lblEntityWorkingForValue" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblOtherEntityWorkingFor" runat="server" Font-Bold="true" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Other %>"></asp:Label>
                <asp:Label ID="lblOtherEntityWorkingForValue" runat="server" Visible="false"></asp:Label>
                <br />
                <asp:Label ID="lblOccupation" runat="server" Font-Bold="true" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, OccupationName %>"></asp:Label>
                <asp:Label ID="lblOccupationValue" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblOtherOccupation" runat="server" Font-Bold="true" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Other %>"></asp:Label>
                <asp:Label ID="lblOtherOccupationValue" runat="server" Visible="false"></asp:Label>
                <br />
                <asp:Label ID="lblHiringDate" runat="server" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, HiringDate %>" Font-Bold="true"></asp:Label>
                <asp:Label ID="lblHiringDateValue" runat="server" Visible="false" />
                <br />
                <asp:Label ID="lblWorkPhone" runat="server" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, WorkPhone %>" Font-Bold="true"></asp:Label>
                <asp:Label ID="lblWorkPhoneValue" runat="server" Visible="false"></asp:Label>
                <br />
                <asp:Label ID="lblIncomingNumber" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, IncomingNumber %>" Font-Bold="true"></asp:Label>
                <asp:Label ID="lblIncomingNumberValue" runat="server"></asp:Label>
                <br />
                <asp:Label ID="lblDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Date %>" Font-Bold="true"></asp:Label>
                <asp:Label ID="lblDateValue" runat="server" />
                <br />
                <asp:Label ID="lblBarCode" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, BarCode %>" Font-Bold="true"></asp:Label>
                <asp:Label ID="lblBarCodeValue" runat="server"></asp:Label>
                <br />
                <uc1:FileUpload runat="server" id="fileUploadOrgAttach" />
                <%-- <asp:Label ID="lblCopyOfOrganizationlLetter" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CopyOfOrganizationlLetter %>" Font-Bold="true"></asp:Label>
                <br />
                <asp:GridView ID="gridCopyOfOrganizationLetter" runat="server" AutoGenerateColumns="false">
                    <Columns>


                        <asp:BoundField DataField="FileName" HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, TemplateName %>" />
                        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, DownloadFile %>">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDownloadCopy" Text='<%# Eval("FileName") %>' CommandArgument='<%# Eval("ID") %>' runat="server" OnClick="lnkDownloadCopy_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>--%>
            </div>
        </asp:Panel>
    </div><!--End of 2nd Tab-->
       

    <h3>
        <asp:Button ID="btnPreviousCertificates" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PreviousCertificates %>" />
    </h3>
    <div class="row margin-top-25"><!--Start of 3rd Tab-->
        <asp:Panel ID="pnlPreviousCertificates" runat="server">
            <div id="divPreviousCertificates">
                <uc1:FileUpload runat="server" id="fileUploadCertificates" />
                <%--   <asp:Label ID="lblPreviousCertificates" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PreviousCertificates %>"></asp:Label>
                <br />--%>
                <%--This will be grid of upload--%>
            </div>
        </asp:Panel>
    </div><!--End of 3rd Tab-->
        


    <h3>
        <asp:Button ID="btnDelegationTemplates" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, DelegationTemplates %>"></asp:Button>
    </h3>
    <div class="row margin-top-25"><!--Start of 4th Tab-->
        <asp:Panel ID="pnlDelegationTemplates" runat="server">
            <div id="divDelegationTemplates">
                <uc1:FileUpload runat="server" id="fileUploadDelegationTemplates" />
                <%--  <asp:Label ID="lblDelegationTemplates" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, DelegationTemplates %>" Font-Bold="true"></asp:Label>
                <br />
                <asp:GridView ID="gridDelegationTemplates" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="DelegationTempFileName" HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, TemplateName %>" />
                        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, DownloadFile %>">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDownload" Text='<%# Eval("FileName") %>' CommandArgument='<%# Eval("ID") %>' runat="server" OnClick="lnkDownload_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>


                    </Columns>
                </asp:GridView>--%>
            </div>
        </asp:Panel>
    </div><!--End of 4th Tab-->
      

    </div>




<div class="row no-padding">
    <h4 class="font-size-18 font-weight-600">
        <asp:Label ID="lblNoRequest" runat="server" Font-Bold="true" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, YouHaveNoRequests %>"></asp:Label>
    </h4>
</div>
 <h3>
 <asp:Button ID="btn_Print" runat="server"  Text="<%$Resources:ITWORX_MOEHEWF_PA, Print %>"  OnClientClick="Print(this)" />
</h3>
