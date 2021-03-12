<%@ Assembly Name="ITWORX.MOEHEWF.UCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=883afb4c05a35fe5" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="uc1" TagName="FileUpload" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReviewerDisplayRequestDetails.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.ReviewerDisplayRequestDetails" %>


<style>
    .instruction-details .fa.fa-exclamation-triangle{
        display:none!important;
    }
</style>

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

    <h1 class="section-title text-center font-weight-500 margin-bottom-25 testeee">
        <asp:Label ID="lblRequestDetails" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RequestDetails %>"></asp:Label>
    </h1>
    <div id="divRequestDetails" class="row heighlighted-section margin-bottom-50 flex-display flex-wrap">
         <div class="col-md-4 col-sm-6">
			<div class="data-container">
				<h6 class="font-size-16 margin-bottom-15">
					<asp:Label ID="lblName" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Name %>" Font-Bold="true"></asp:Label>

				</h6>
				<h5 class="font-size-20">
					<asp:Label ID="lblNameValue" runat="server" ></asp:Label>
				</h5>
			</div>
		</div>

		<div class="col-md-3 col-sm-6">
			<div class="data-container">
				<h6 class="font-size-16 margin-bottom-15">
					<asp:Label ID="lblPersonalID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PersonalID %>" Font-Bold="true"></asp:Label>

				</h6>
				<h5 class="font-size-20">
					<asp:Label ID="lblPersonalIDValue" runat="server"></asp:Label>
				</h5>
			</div>
		</div>
        <div class="col-md-4 col-xs-12">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lblRequestNumber" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RequestNumber %>"></asp:Label>
                </h6>
                <h5 class="font-size-20">
                    <asp:Label ID="lblRequestNumberValue" runat="server"></asp:Label>
                </h5>
            </div>
        </div>

        <div class="col-md-4 col-xs-12">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lblRequestCreationDate" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RequestCreationDate %>"></asp:Label>
                </h6>
                <h5 class="font-size-20">

                    <asp:Label ID="lblRequestCreationDateValue" runat="server"></asp:Label>
                </h5>
            </div>
        </div>

        <div class="col-md-4 col-xs-12">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">

                    <asp:Label ID="lblRequestSubmitDate" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RequestSubmitDate %>"></asp:Label>
                </h6>
                <h5 class="font-size-20">
                    <asp:Label ID="lblRequestSubmitDateValue" runat="server"></asp:Label>
                </h5>
            </div>
        </div>

        <div class="col-md-12 col-sm-12 col-xs-12">
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



    <div class="row unheighlighted-section margin-2">
        <!--Start of Accordion-->
        <div class="accordion UCEaccordion">
            <!--First Tab-->
            <h3 class="firstAA">
                <asp:Button ID="btnDegreeRequiredToBEquivalentData" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, DegreeRequiredToBEquivalentData %>" CssClass="popdate accordion-input"></asp:Button>
            </h3>
            <div class="row margin-top-25">
                <asp:Panel ID="pnlDegreeRequiredToBEquivalentData" runat="server">

                    <div class="row margin-top-15">
                        <div class="accordion-inner">
                            <!--1st-->

                            <h3 class="inner-accordion">
                                <asp:Label ID="lblCertificatesData" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CertificatesData %>" Font-Bold="true"></asp:Label>
                            </h3>

                            <div id="divCertificatesData" class="display-mode">

                                <div class="row margin-top-15">
                                    <div class="col-md-4 col-xs-12 no-padding auto-height">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblCerticateAcademicDegree" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CertificateAcademicDegree %>" runat="server" Font-Bold="true"></asp:Label>
                                                </h6>


                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">

                                                    <asp:Label ID="lblCerticateAcademicDegreeValue" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hdnCertificateAcademic" runat="server" />

                                                       <asp:HiddenField ID="hdnCertificateAcademicTxt" runat="server" />

                                                </h5>

                                           

                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-xs-12 no-padding auto-height">
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

                                    <div class="col-md-4 col-xs-12 no-padding auto-height">
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



                                </div>
                                <div class="row margin-top-15">
                                    <div class="col-md-4 col-xs-12 no-padding auto-height">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblEntityProvidingStudy" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EntityProvidingStudy %>" Visible="false"></asp:Label>
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblEntityProvidingStudyValue" runat="server"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-4 col-xs-12 no-padding auto-height" id="entityProviding" style="display: none">
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


                                    <div class="col-md-4 col-xs-12 no-padding auto-height" style="display: none;" id="examLocationSection">
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


                                </div>

                                 <div class="row">
							<div class="col-md-7 col-sm-12 col-xs-12 no-padding test-file" >
								<div class="data-container table-display moe-width-85">
									<div class="form-group">
										<div class="form">
											<uc1:FileUpload runat="server" id="fileUploadCertificateEquivalent" />
										</div>
									</div>
								</div>
							</div>
                                      </div>
                                   <div class="row">
							<div class="col-md-7 col-sm-12 col-xs-12 no-padding test-file" >
								<div class="data-container table-display moe-width-85">
									<div class="form-group">
										<div class="form">
											<uc1:FileUpload runat="server" id="fileUploadGrades" />
										</div>
									</div>
								</div>
							</div>
                                      </div>
                            </div>


                            <!--2nd-->
                            <h3 class="inner-accordion">
                                <asp:Label ID="lblUniversitiesDetails" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversitiesDetails %>" Font-Bold="true"></asp:Label>
                            </h3>




                            <div id="divUniversitiesDetails" class="display-mode">
                                <div class="row margin-top-15">
                                    <div class="col-md-4 col-xs-12 no-padding auto-height">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblCountries" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CountryOfStudy %>" Font-Bold="true"></asp:Label>
                                                    <asp:Label ID="lblNewCoutriesAdded" runat="server" Visible="false" CssClass="fa fa-exclamation-circle display-icon" title="<%$Resources:ITWORX_MOEHEWF_UCE, NewCountryAdded %>" />


                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblCountriesValue" runat="server"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-xs-12 no-padding auto-height">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblUniversity" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, University %>"></asp:Label>
                                                    <asp:Label ID="lblNewUniversitiesAdded" runat="server" Visible="false" CssClass="fa fa-exclamation-circle display-icon" title="<%$Resources:ITWORX_MOEHEWF_UCE, NewUniversityAdded %>" />
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblUniversityValue" runat="server"></asp:Label>
                                                    <asp:Label ID="lblUniversityNotFoundValue" runat="server" Visible="false"></asp:Label>

                                                </h5>
                                            </div>
                                        </div>
                                    </div>
                                      <div class="col-md-4 col-xs-12 no-padding test auto-height">
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
                                   



                                </div>
                                <div class="row margin-top-15">
                                     <div class="col-md-4 col-xs-12 no-padding auto-height">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblFaculty" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Faculty %>"></asp:Label>

                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblFacultyValue" runat="server"></asp:Label>
                                                    <asp:Label ID="lblFacultyNotFoundValue" runat="server" Visible="false"></asp:Label>
                                                </h5>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-xs-12 no-padding auto-height">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblSpecialization" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Specialization %>"></asp:Label>

                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblSpecializationValue" runat="server"></asp:Label>
                                                    <asp:Label ID="lblSpecializationNotFoundValue" runat="server" Visible="false"></asp:Label>

                                                </h5>
                                            </div>
                                        </div>
                                    </div>
                                  




                                </div>

                                <div class="row">
                                    <div class="col-md-12 col-xs-12 no-padding-imp">
                                        <h5 class="font-size-14 margin-bottom-0 margin-top-0">
                                            <asp:Label ID="lblUniversityCHED" runat="server" CssClass="warning instruction-details underline color-black font-family-sans"></asp:Label>
                                        </h5>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-12 col-xs-12 no-padding-imp">
                                        <h5 class="font-size-14 margin-bottom-0 margin-top-0">
                                            <asp:Label ID="lblUniversityHEDD" runat="server" CssClass="warning instruction-details underline color-black font-family-sans"></asp:Label>
                                        </h5>
                                    </div>

                                </div>
                            </div>


                            <!--3rd-->
                            <h3 class="inner-accordion">
                                <asp:Label ID="lblStudingDetails" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, StudyDetails %>" Font-Bold="true"></asp:Label>
                            </h3>
                            <div id="divStudyingDetails" class="display-mode">
                                <div class="row margin-top-15">
                                    <div class="col-md-4 col-xs-12 no-padding auto-height">
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


                                    <div class="col-md-4 col-xs-12 no-padding auto-height">
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

                                    <div class="col-md-4 col-xs-12 no-padding auto-height">
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

                                </div>
                                <div class="row margin-top-15">

                                    <div class="col-md-4 col-xs-12 no-padding auto-height">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblGraduationDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, GraduationDate %>" Font-Bold="true"></asp:Label>

                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblGraduationDateValue" runat="server" />
                                                </h5>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </div>


                            <!--4th-->
                            <h3 class="inner-accordion">
                                <asp:Label ID="lblUniversitiesNames" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityNames %>" Font-Bold="true"></asp:Label>

                            </h3>
                            <div id="divUniversities" class="display-mode">

                                <div class="row no-padding">
                                    <asp:Repeater ID="repCalculatedDetailsForCertificate" runat="server">

                                        <HeaderTemplate>
                                            <table class="table table-striped moe-full-width moe-table">
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

                                                    <th scope="col">
                                                        <%=Resources.ITWORX_MOEHEWF_UCE.Actions %>
                                                    </th>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>

                                                <td>

                                                    <asp:Label ID="lblCalcSectionCountry" runat="server" Text='<%#Eval("Country.SelectedTitle") %>'></asp:Label>
                                                    <asp:Label ID="lblCalcSectionOtherCountry" runat="server" Text='<%#Eval("OtherCountry") %>'></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCalcSectionUniversity" runat="server" Text='<%#Eval("Univesrity.SelectedTitle") %>'></asp:Label>
                                                    <asp:Label ID="lblCalcSectionOtherUniversity" runat="server" Text='<%#Eval("OtherUniversity") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCalcSectionFaculty" runat="server" Text='<%#Eval("Faculty") %>'></asp:Label>

                                                </td>

                                                <td>
                                                    <asp:Label ID="lblCalcSectionStudySystem" runat="server" Text='<%#Eval("StudySystem.SelectedTitle") %>'></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:HiddenField ID="hdnCalcSectionID" runat="server" Value='<%#Eval("ID") %>' />
                                                    <asp:LinkButton ID="lnkCalcSectionDisplayAttach" runat="server" OnClick="lnkCalcSectionDisplayAttach_Click" ToolTip="<%$Resources:ITWORX_MOEHEWF_UCE, DisplayAttachments %>" CssClass="display-icon fa fa-eye"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            </table>
                  
                                        </FooterTemplate>

                                    </asp:Repeater>
                                </div>




                                <uc1:FileUpload runat="server" id="fileUploadCalculatedDetails" Visible="false" />
                                <asp:GridView ID="gridUniversitiesNames" runat="server" OnRowDataBound="gridUniversitiesNames_RowDataBound" CssClass="table table-striped moe-full-width moe-table">
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

                                        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, StudyActualPeriod %>">
                                            <ItemTemplate>

                                                <asp:Label ID="lblUniGridStudingPeriod" runat="server"></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>

                                </asp:GridView>
                            </div>


                            <!--5th-->
                            <h3 class="inner-accordion">
                                <asp:Label ID="lblPublishingCertificateUnivesity" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityPublishingCertificateDetails %>" Font-Bold="true"></asp:Label>
                            </h3>

                            <div id="divPublishingCertificate" class="display-mode">
                                <div class="row margin-top-15">
                                    <div class="col-md-4 col-xs-12 no-padding auto-height">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblUniversityMainHeadQuarter" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityMainHeadquarter %>"></asp:Label>
                                                    <asp:Label ID="lblUniversityMainAdded" runat="server" Visible="false" CssClass="fa fa-exclamation-circle display-icon" title="<%$Resources:ITWORX_MOEHEWF_UCE, NewUniversityMainAdded %>" />

                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblUniversityMainHeadQuarterValue" runat="server"></asp:Label>
                                                    <asp:Label ID="lblNewUniversityMainHeadQuarterValue" runat="server" Visible="false"></asp:Label>

                                                </h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-xs-12 no-padding auto-height">
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

                                    <div class="col-md-4 col-xs-12 no-padding auto-height">
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
                        <!--End of 2nd accordion-->
                    </div>
                </asp:Panel>
            </div>

            <!--2nd Tab-->
            <h3>
                <asp:Button ID="btnWorkingDetails" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, WorkingDetails %>" CssClass="popdate accordion-input"></asp:Button>
            </h3>
            <div class="container margin-top-25">
                <asp:Panel ID="pnlWorkingDetails" runat="server">
                    <%-- <asp:Label ID="lblWorkingDetails" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, WorkingDetails %>" Font-Bold="true"></asp:Label>--%>
                    <div id="divWorkingDetails" class="display-mode">
                        <div class="row margin-top-15">
                            <div class="col-md-4 col-xs-12 no-padding auto-height">
                                <div class="data-container table-display moe-width-85">
                                    <div class="form-group">
                                        <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                            <asp:Label ID="lblWorkingOrNot" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, WorkingOrNot %>" Font-Bold="true"></asp:Label>

                                        </h6>
                                        <h5 class="font-size-20  margin-bottom-0 margin-top-0">
                                            <asp:Label ID="lblWorkingOrNotValue" runat="server"></asp:Label>
                                        </h5>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-4 col-xs-12 no-padding auto-height" id="workEntityNeedsEquivalency" runat="server" visible="false">

                                <div class="data-container table-display moe-width-85">
                                    <div class="form-group">
                                        <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                            <asp:Label ID="lblEntityNeedsEquivalency" runat="server" Font-Bold="true" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EntityNeedsEquivalency %>"></asp:Label>


                                        </h6>
                                        <h5 class="font-size-20  margin-bottom-0 margin-top-0">
                                            <asp:Label ID="lblEntityNeedsEquivalencyValue" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblOtherEntityNeedsEquivalencyValue" runat="server" Visible="false"></asp:Label>
                                        </h5>
                                    </div>
                                </div>
                            </div>

                            <div class="row no-padding" id="WorkingFor" runat="server" visible="false">
                                <div class="col-md-4 col-xs-12 no-padding auto-height">
                                    <div class="data-container table-display moe-width-85">
                                        <div class="form-group">
                                            <h6 class="font-size-16 margin-bottom-0 margin-top-0">

                                                <asp:Label ID="lblEntityWorkingFor" runat="server" Font-Bold="true" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EntityWorkingFor %>"></asp:Label>

                                            </h6>
                                            <h5 class="font-size-20  margin-bottom-0 margin-top-0">
                                                <asp:Label ID="lblEntityWorkingForValue" runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="lblOtherEntityWorkingForValue" runat="server" Visible="false"></asp:Label>
                                            </h5>
                                        </div>
                                    </div>
                                </div>
                                <%--<div class="col-md-4 col-xs-12 no-padding"  id="divOccupation"  runat="server" visible="false">
									<div class="data-container table-display moe-width-85">
										<div class="form-group">
											<h6 class="font-size-16 margin-bottom-0 margin-top-0">
												<asp:Label ID="lblOccupation" runat="server" Font-Bold="true" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, OccupationName %>"></asp:Label>

											</h6>
											<h5 class="font-size-20  margin-bottom-0 margin-top-0">
												<asp:Label ID="lblOccupationValue" runat="server" Visible="false"></asp:Label>
												<asp:Label ID="lblOtherOccupationValue" runat="server" Visible="false"></asp:Label>
											</h5>
										</div>
									</div>
								</div>--%>
                            </div>


                            <div class="row margin-top-15">
                                <div class="col-md-6 col-sm-12 col-xs-12 no-padding test-file auto-height" id="fileWorkig">
                                    <div class="data-container table-display moe-width-85">
                                        <div class="form-group">
                                            <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                <asp:Label ID="lblCopyOfOrganizationlLetter" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CopyOfOrganizationlLetter %>" Font-Bold="true"></asp:Label>
                                            </h6>
                                            <div class="form">
                                                <uc1:FileUpload runat="server" id="fileUploadOrgAttach" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6 col-sm-12 col-xs-12 no-padding auto-height" id="filenotWorking">
                                    <div class="row data-container table-display moe-full-width">
                                        <div class="form-group">
                                            <div class="form fileUpload-width">

                                                <uc1:FileUpload runat="server" id="fileUploadNotWorking" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>







                        </div>


                    </div>
                </asp:Panel>
            </div>

            <!--3rf Tab-->
            <h3>
                <asp:Button ID="btnPreviousCertificates" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PreviousCertificates %>" CssClass="popdate accordion-input" />
            </h3>
            <div class="container margin-top-25">
                <asp:Panel ID="pnlPreviousCertificates" runat="server">
                    <div id="divPreviousCertificates" class="display-mode">
                        <div class="row">
                            <div class="col-md-6 col-sm-12 col-xs-12 no-padding margin-top-15 auto-height" id="filenotWorking">
                                <div class="row data-container table-display moe-full-width">
                                    <div class="form-group">
                                        <div class="form fileUpload-width">

                                            <uc1:FileUpload runat="server" id="fileUploadCertificates" />
                                            <%--<uc1:FileUpload runat="server" id="fileUploadGeneralSec" />--%>
                                           <uc1:FileUpload runat="server" id="fileUploadDiploma" Visible="false" />  
                                            <uc1:FileUpload runat="server" id="fileUploadInterDiploma" Visible="false" />

                                            <uc1:FileUpload runat="server" id="fileUploadSequenceCert" />
                                            
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                    </div>
                </asp:Panel>
            </div>

            <!--4th Tab-->
            <h3>
                <asp:Button ID="btnDelegationTemplates" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, DelegationTemplates %>" CssClass="popdate accordion-input"></asp:Button>

            </h3>
            <div class="container margin-top-25">
                <asp:Panel ID="pnlDelegationTemplates" runat="server">
                    <div id="divDelegationTemplates">
                        <div class="row">
                            <div class="col-md-6 col-sm-12 col-xs-12 no-padding margin-top-15 auto-height" id="filenotWorking">
                                <div class="row data-container table-display moe-full-width">
                                    <div class="form-group">
                                        <div class="form fileUpload-width display-mode">

                                            <uc1:FileUpload runat="server" id="fileUploadDelegates" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                    </div>
                </asp:Panel>
            </div>
            <!--5th Tab-->
            <h3>
                <asp:Button ID="btnAttachments" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, DisplayAttachments %>" CssClass="popdate accordion-input"></asp:Button>

            </h3>
            <div class="cotainer margin-top-25 display-mode">
                <asp:Panel ID="pnlAttachments" runat="server">
                    <div id="divAttachments" class="row">
                         <div class="col-md-7 col-sm-12 col-xs-12  auto-height">
                            <h6 class="font-size-18 margin-bottom-0 margin-top-0 font-family-sans" style="color: black">
                                <asp:Label ID="lblCertEquivalent" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CertificateToBeEquivalent %>" Font-Bold="true"></asp:Label>
                            </h6>
                            <uc1:FileUpload runat="server" id="fileEquivalent" />
                        </div>
                         <div class="col-md-7 col-sm-12 col-xs-12  auto-height">
                            <h6 class="font-size-18 margin-bottom-0 margin-top-0 font-family-sans" style="color: black">
                                <asp:Label ID="lblGrades" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Grades %>" Font-Bold="true"></asp:Label>
                            </h6>
                            <uc1:FileUpload runat="server" id="fileGrades" />
                        </div>
                        <div class="col-md-7 col-sm-12 col-xs-12  auto-height">
                            <h6 class="font-size-18 margin-bottom-0 margin-top-0 font-family-sans" style="color: black">
                                <asp:Label ID="lbl_OrgAttach" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, OrganizationlLetterCopy %>" Font-Bold="true"></asp:Label>
                            </h6>
                            <uc1:FileUpload runat="server" id="OrgAttach" />
                        </div>
                        <div class="col-md-7 col-sm-12 col-xs-12  margin-top-15 auto-height">
                            <h6 class="font-size-18 margin-bottom-0 margin-top-0 font-family-sans" style="color: black">
                                <asp:Label ID="lbl_Certificates" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PrevCertificates %>" Font-Bold="true"></asp:Label>
                            </h6>
                            <uc1:FileUpload runat="server" id="Certificates" />
                        </div>

                         <div class="col-md-7 col-sm-12 col-xs-12  margin-top-15 auto-height" id="diploma" runat="server" visible="false">
                            <h6 class="font-size-18 margin-bottom-0 margin-top-0 font-family-sans" style="color: black">
                                <asp:Label ID="lblDiploma" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Diploma %>" Font-Bold="true"></asp:Label>
                            </h6>
                            <uc1:FileUpload runat="server" id="fileDiploma" />
                        </div>
                         <div class="col-md-7 col-sm-12 col-xs-12  margin-top-15 auto-height" id="interDiploma" runat="server" visible="false">
                            <h6 class="font-size-18 margin-bottom-0 margin-top-0 font-family-sans" style="color: black">
                                <asp:Label ID="lblInterDiploma" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, IntermediateDiploma %>" Font-Bold="true"></asp:Label>
                            </h6>
                            <uc1:FileUpload runat="server" id="fileInterDiploma" />
                        </div>

                         <div class="col-md-7 col-sm-12 col-xs-12  margin-top-15 auto-height">
                            <h6 class="font-size-18 margin-bottom-0 margin-top-0 font-family-sans" style="color: black">
                                <asp:Label ID="lblCertificateSequence" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CertificateSequence %>" Font-Bold="true"></asp:Label>
                            </h6>
                            <uc1:FileUpload runat="server" id="CertificateSequence" />
                        </div>
               



                        <div class="col-md-7 col-sm-12 col-xs-12  margin-top-15 auto-height">
                            <h6 class="font-size-18 margin-bottom-0 margin-top-0 font-family-sans" style="color: black">
                                <asp:Label ID="lbl_Delegates" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, DelegationTemplates %>" Font-Bold="true"></asp:Label>
                            </h6>
                            <uc1:FileUpload runat="server" id="Delegates" />
                        </div>

                    </div>
                </asp:Panel>
            </div>

        </div>
        <!--Start of Accordion-->
    </div>
    <!--End of unheighlited section-->










</div>
<div class="row no-padding margin-top-20">
    <h4 class="font-size-18 font-weight-600">
        <asp:Label ID="lblNoRequest" runat="server" Font-Bold="true" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, YouHaveNoRequests %>"></asp:Label>
    </h4>
</div>
<div class="row no-padding margin-top-20">
    <asp:Button ID="btn_Print" runat="server" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Print %>" OnClientClick="Print(this)" CssClass="btn moe-btn pull-right" />
</div>


