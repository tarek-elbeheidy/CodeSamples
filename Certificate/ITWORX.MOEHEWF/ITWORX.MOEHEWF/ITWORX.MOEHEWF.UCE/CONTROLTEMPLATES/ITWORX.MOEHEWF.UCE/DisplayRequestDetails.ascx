<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="uc1" TagName="FileUpload" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DisplayRequestDetails.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.DisplayRequestDetails" %>


<%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>
<link rel="stylesheet" href='<%= ResolveUrl ("~/Style%20Library/CSS/jquery-ui.css") %>'>
<script type="text/javascript" src='<%= ResolveUrl ("~/Style%20Library/Scripts/jquery-1.12.4.js") %>'></script>
<script type="text/javascript" src='<%= ResolveUrl ("~/Style%20Library/Scripts/jquery-ui.js") %>'></script>
<script type="text/javascript" src='<%= ResolveUrl ("~/Style%20Library/Scripts/jquery.cookie.js") %>'></script>--%>
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

	<h1 class="section-title text-center font-weight-500 margin-bottom-25">
		<asp:Label ID="lblRequestDetails" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RequestDetails %>"></asp:Label>
	</h1>
	<div id="divRequestDetails" class="container heighlighted-section margin-bottom-50  test-display margin-0">
       <%-- <div class="row margin-bottom-15">
             <div class="col-md-6 col-sm-12 auto-height">
			<div class="data-container">
				<h6 class="font-size-16 margin-bottom-0">
				    <asp:Label ID="lblApplicantName" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ApplicantName %>" Font-Bold="true"></asp:Label>
				</h6>
				<h5 class="font-size-20 margin-top-5">
					 <asp:Label ID="lblApplicantNameValue" runat="server" Text=""></asp:Label>
				</h5>
			</div>
		</div>
            <div class="col-md-6 col-sm-12 auto-height">
			<div class="data-container">
				<h6 class="font-size-16 margin-bottom-0">
				   <asp:Label ID="lblPersonalID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PersonalID %>" Font-Bold="true"></asp:Label>
				</h6>
				<h5 class="font-size-20 margin-top-5">
				    <asp:Label ID="lblPersonalIDValue" runat="server" Text=""></asp:Label>
				</h5>
			</div>
		</div>
        </div>--%>
		<div class="row margin-bottom-15">
            <div class="col-md-4 col-sm-6 auto-height">
			<div class="data-container">
				<h6 class="font-size-16 margin-bottom-0">
					<asp:Label ID="lblRequestNumber" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RequestNumber %>"></asp:Label>
				</h6>
				<h5 class="font-size-20 margin-top-5">
					<asp:Label ID="lblRequestNumberValue" runat="server"></asp:Label>
				</h5>
			</div>
		</div>

		<div class="col-md-4 col-sm-6 auto-height">
			<div class="data-container">
				<h6 class="font-size-16 margin-bottom-0">
					<asp:Label ID="lblRequestCreationDate" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RequestCreationDate %>"></asp:Label>
				</h6>
				<h5 class="font-size-20 margin-top-5">

					<asp:Label ID="lblRequestCreationDateValue" runat="server"></asp:Label>
				</h5>
			</div>
		</div>

		<div class="col-md-4 col-sm-6 auto-height">
			<div class="data-container">
				<h6 class="font-size-16 margin-bottom-0">

					<asp:Label ID="lblRequestSubmitDate" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RequestSubmitDate %>"></asp:Label>
				</h6>
				<h5 class="font-size-20 margin-top-5">
					<asp:Label ID="lblRequestSubmitDateValue" runat="server"></asp:Label>
				</h5>
			</div>
		</div>
		</div>

		<div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12 margin-top-20">
			<div class="data-container">
				<h6 class="font-size-16 margin-bottom-0">
					<asp:Label ID="lblAcademicDegreeForEquivalence" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, AcademicDegree %>"></asp:Label>
				</h6>
				<h5 class="font-size-20 margin-top-5">

					<asp:Label ID="lblAcademicDegreeForEquivalenceValue" runat="server"></asp:Label>
				</h5>
			</div>
		</div>
		</div>

	</div>



	<div class="row unheighlighted-section test-display">
		<!--Start of Accordion-->
		<div class="accordion UCEaccordion">
			<!--First Tab-->
			<h3>
				<asp:Button ID="btnDegreeRequiredToBEquivalentData" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, DegreeRequiredToBEquivalentData %>" CssClass="popdate accordion-input"></asp:Button>
			</h3>
			<div class="container margin-top-25">
				<asp:Panel ID="pnlDegreeRequiredToBEquivalentData" runat="server">

					<div class="row margin-top-15">
						<div class="accordion-inner">
							<!--1st-->

							<h3 class="inner-accordion">
								<asp:Label ID="lblCertificatesData" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CertificatesData %>" Font-Bold="true"></asp:Label>
							</h3>

							<div id="divCertificatesData" class="display-mode">

								<div class="row margin-top-0">
									<div class="col-md-4 col-sm-6 no-padding auto-height">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblCerticateAcademicDegree" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CertificateAcademicDegree %>" runat="server" Font-Bold="true"></asp:Label>
												</h6>


												<h5 class="font-size-18 margin-bottom-0 margin-top-0">

													<asp:Label ID="lblCerticateAcademicDegreeValue" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hdnCertificateAcademic" runat="server" />
                                                       <asp:HiddenField ID="hdnCertificateAcademicTxt" runat="server" />


												</h5>

											</div>
										</div>
									</div>

									<div class="col-md-6 col-sm-6 no-padding auto-height">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblCertificateThroughScholarship" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CertificateThroughScholarship %>" Font-Bold="true"></asp:Label>
												</h6>
												<h5 class="font-size-18 margin-bottom-0 margin-top-0">

													<asp:Label ID="lblCertificateThroughScholarshipValue" runat="server"></asp:Label>

												</h5>
											</div>
										</div>
									</div>

                                  </div>

                                <div class="row">
                                    <div class="col-md-4 col-xs-12 no-padding auto-height">
                                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                            <div class="form-group">
                                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                    <asp:Label ID="lblHavePA" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, HavePA %>"></asp:Label>
                                                </h6>
                                                <h5 class="font-size-20 margin-bottom-0 margin-top-0">

                                                    <asp:Label ID="lblHavePAValue" runat="server"></asp:Label>

                                                </h5>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 col-sm-12 col-xs-12 no-padding test-file">
                                        <div class="data-container table-display moe-width-85">
                                            <div class="form-group">
                                                <div class="form">
                                                    <uc1:FileUpload runat="server" id="fileUploadHavePA" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                     <div class="col-md-4 col-sm-12 col-xs-12 no-padding test-file" >
                                        <div class="data-container table-display moe-width-85">
                                            <div class="form-group">
                                                <div class="form">
                                                    <uc1:FileUpload runat="server" id="fileUploadNoHavePA" />
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
                                <div class="row margin-top-15">
									

									<div class="col-md-4 col-sm-6 no-padding auto-height">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblEntityProvidingStudy" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EntityProvidingStudy %>" Visible="false"></asp:Label>
												</h6>
												<h5 class="font-size-18 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblEntityProvidingStudyValue" runat="server"></asp:Label>
												</h5>
											</div>
										</div>
									</div>
                                    
                                
                                    <div class="col-md-4 col-sm-6 no-padding auto-height" id="entityProviding" style="display: none">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblEntityProvidingOther" runat="server" Font-Bold="true" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Other %>"></asp:Label>
												</h6>
												<h5 class="font-size-18 margin-bottom-0 margin-top-0">

													<asp:Label ID="lblEntityProvidingOtherValue" runat="server" Visible="false"></asp:Label>
												</h5>
											</div>
										</div>
									</div>

								

								</div>
							</div>

                        	<%--<div class="col-md-4 col-sm-6 no-padding">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblCampusStudy" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CampusStudy %>" Font-Bold="true"></asp:Label>
												</h6>
												<h5 class="font-size-18 margin-bottom-0 margin-top-0">

													<asp:Label ID="lblCampusStudyValue" runat="server"></asp:Label>
												</h5>
											</div>
										</div>
									</div>--%>
									<%--<div class="col-md-4 col-sm-6 no-padding" style="display: none;" id="studyLocationContent">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblStudyLocation" runat="server" Font-Bold="true" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, StudyLocation %>"></asp:Label>

												</h6>
												<h5 class="font-size-18 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblStudyLocationValue" runat="server" Visible="false"> </asp:Label>
												</h5>
											</div>
										</div>
									</div>--%>
									<%--<div class="col-md-4 col-sm-6 no-padding" style="display: none;" id="examLocationSection">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblCampusExam" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CampusExam %>"></asp:Label>
												</h6>
												<h5 class="font-size-18 margin-bottom-0 margin-top-0">

													<asp:Label ID="lblCampusExamValue" runat="server"></asp:Label>

												</h5>
											</div>
										</div>
									</div>--%>
                                    <%--
									<div class="col-md-4 col-sm-6 no-padding">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblExamLocation" runat="server" Font-Bold="true" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ExamLocationDisplay %>"></asp:Label>
												</h6>
												<h5 class="font-size-18 margin-bottom-0 margin-top-0">

													<asp:Label ID="lblExamLocationValue" runat="server" Visible="false"></asp:Label>
												</h5>
											</div>
										</div>
									</div>--%>




							<!--2nd-->
							<h3 class="inner-accordion">
								<asp:Label ID="lblUniversitiesDetails" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversitiesDetails %>" Font-Bold="true"></asp:Label>
							</h3>




							<div id="divUniversitiesDetails" class="display-mode">
								<div class="row margin-top-15">
									<div class="col-md-4 col-sm-6 no-padding auto-height">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblCountries" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CountryOfStudy %>" Font-Bold="true"></asp:Label>

												</h6>
												<h5 class="font-size-18 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblCountriesValue" runat="server"></asp:Label>
												</h5>
											</div>
										</div>
									</div>

									<div class="col-md-4 col-sm-6 no-padding auto-height">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblUniversity" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, University %>"></asp:Label>

												</h6>
												<h5 class="font-size-18 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblUniversityValue" runat="server"></asp:Label>
													<asp:Label ID="lblUniversityNotFoundValue" runat="server" Visible="false"></asp:Label>
                                                    
                                       
                                       
                                                
                                                </h5>
											</div>
										</div>
									</div>

                                    <div class="col-md-4 col-sm-6 no-padding auto-height">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblFaculty" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Faculty %>"></asp:Label>

												</h6>
												<h5 class="font-size-18 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblFacultyValue" runat="server"></asp:Label>
													<asp:Label ID="lblFacultyNotFoundValue" runat="server" Visible="false"></asp:Label>
												</h5>
											</div>
										</div>
									</div>

							

                                    </div>
                                	<div class="row margin-top-15">
											<%--<div class="col-md-4 col-sm-6 no-padding test auto-height" style="display: none;">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblUniversityList" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversitiesLists %>" Font-Bold="true" Visible="false"></asp:Label>
												</h6>
												<h5 class="font-size-18 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblUniversityListValue" runat="server" Visible="false"></asp:Label>
												</h5>
											</div>
										</div>
									</div>--%>

									<div class="col-md-4 col-sm-6 no-padding auto-height">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblSpecialization" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Specialization %>"></asp:Label>

												</h6>
												<h5 class="font-size-18 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblSpecializationValue" runat="server"></asp:Label>
													<asp:Label ID="lblSpecializationNotFoundValue" runat="server" Visible="false"></asp:Label>

												</h5>
											</div>
										</div>
									</div>
                                        <div class="col-md-4 col-xs-12 no-padding auto-height" id="divUniversityType" runat="server">
                                            <div class="data-container table-display moe-width-85  moe-sm-full-width">
                                                <div class="form-group">
                                                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                        <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityType %>"></asp:Label>

                                                    </h6>
                                                    <h5 class="font-size-20 margin-bottom-0 margin-top-0">
                                                        <asp:Label ID="lblUniversityTypeValue" runat="server"></asp:Label>


                                                    </h5>
                                                </div>
                                            </div>
                                        </div>



                                    </div>
                                <div class="row">
                                    <div class="col-md-12 col-xs-12 no-padding-imp">
                                        <h5 class="font-size-14 margin-bottom-0 margin-top-0">
                                            <asp:Label ID="lblUniversityCHED" runat="server" CssClass="warning instruction-details underline color-black font-family-sans" Visible="false"></asp:Label>
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
									<div class="col-md-4 col-sm-6 no-padding auto-height">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblStudyLanguage" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, StudyLanguage %>" Font-Bold="true"></asp:Label>

												</h6>
												<h5 class="font-size-18 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblStudyingLanguageValue" runat="server"></asp:Label>
												</h5>
											</div>
										</div>
									</div>

								<%--	<div class="col-md-4 col-sm-6 no-padding">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblStudingType" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, StudyType %>" Font-Bold="true"></asp:Label>

												</h6>
												<h5 class="font-size-18 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblStudyingTypeValue" runat="server"></asp:Label>
												</h5>
											</div>
										</div>
									</div>--%>

									<div class="col-md-4 col-sm-6 no-padding auto-height">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblStudyingSystem" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, StudySystem %>" Font-Bold="true"></asp:Label>

												</h6>
												<h5 class="font-size-18 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblStudyingSystemValue" runat="server"></asp:Label>
												</h5>
											</div>
										</div>
									</div>

									<div class="col-md-4 col-sm-6 no-padding auto-height">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblStartDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, StudyStartDate %>" Font-Bold="true"></asp:Label>

												</h6>
												<h5 class="font-size-18 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblStartDateValue" runat="server" />
												</h5>
											</div>
										</div>
									</div>

                                    </div>
                                <div class="row margin-top-15">
									<div class="col-md-4 col-sm-6 no-padding auto-height">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblGraduationDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, GraduationDate %>" Font-Bold="true"></asp:Label>

												</h6>
												<h5 class="font-size-18 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblGraduationDateValue" runat="server" />
												</h5>
											</div>
										</div>
									</div>

								<%--	<div class="col-md-4 col-sm-6 no-padding">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblStudyPeriod" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, AcademicProgramPeriod %>" Font-Bold="true"></asp:Label>

												</h6>
												<h5 class="font-size-18 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblStudyPeriodValue" runat="server"></asp:Label>
												</h5>
											</div>
										</div>
									</div>--%>

<%--									<div class="col-md-4 col-sm-6 no-padding">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblActualStudy" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ActualStudingPeriod %>" Font-Bold="true"></asp:Label>

												</h6>
												<h5 class="font-size-18 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblActualStudyValue" runat="server"></asp:Label>
												</h5>
											</div>
										</div>
									</div>--%>

									<%--<div class="col-md-4 col-sm-6 no-padding">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblGainedHours" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NumberOfHoursGained %>"></asp:Label>

												</h6>
												<h5 class="font-size-18 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblGainedHoursValue" runat="server"></asp:Label>
												</h5>
											</div>
										</div>
									</div>--%>

									<%--<div class="col-md-4 col-sm-6 no-padding">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblGPA" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, GPA %>"></asp:Label>

												</h6>
												<h5 class="font-size-18 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblGPAValue" runat="server"></asp:Label>
												</h5>
											</div>
										</div>
									</div>--%>

								<%--	<div class="col-md-4 col-sm-6 no-padding">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblOnlineHours" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NumberOfOnlineHours %>"></asp:Label>

												</h6>
												<h5 class="font-size-18 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblOnlineHoursValue" runat="server"></asp:Label>
												</h5>
											</div>
										</div>
									</div>--%>

<%--									<div class="col-md-4 col-sm-6 no-padding">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblOnlinePercentage" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PercentageOfOnlineHours %>"></asp:Label>

												</h6>
												<h5 class="font-size-18 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblOnlinePercentageValue" runat="server" Visible="false"></asp:Label>
												</h5>
											</div>
										</div>
									</div>
--%>

							<%--		<div class="col-md-4 col-sm-6 no-padding">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblCompExam" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, IsThereComprehensiveExam %>"></asp:Label>

												</h6>
												<h5 class="font-size-18 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblCompExamValue" runat="server" />
												</h5>
											</div>
										</div>
									</div>--%>

<%--									<div class="col-md-4 col-sm-6 no-padding">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblAcceptExam" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, IsThereAcceptanceExam %>"></asp:Label>
												</h6>
												<h5 class="font-size-18 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblAcceptExamValue" runat="server" />
												</h5>
											</div>
										</div>
									</div>
--%>

								</div>
							</div>


							<!--4th-->
							<h3 class="inner-accordion">
								<asp:Label ID="lblUniversitiesNames" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityNames %>" Font-Bold="true"></asp:Label>

							</h3>
							<div id="divUniversities">

								<div class="col-md-12 no-padding margin-bottom-10">
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
													<%--<th scope="col">
														<%=Resources.ITWORX_MOEHEWF_UCE.StudyType %>
													</th>--%>
													<th scope="col">
														<%=Resources.ITWORX_MOEHEWF_UCE.StudySystem %>
													</th>
													<%--<th scope="col">
														<%=Resources.ITWORX_MOEHEWF_UCE.StudyActualPeriod %>
													</th>--%>
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
												<%--<td>
													<asp:Label ID="lblCalcSectionStudyType" runat="server" Text='<%#Eval("StudyType.SelectedTitle") %>'></asp:Label>
												</td>--%>
												<td>
													<asp:Label ID="lblCalcSectionStudySystem" runat="server" Text='<%#Eval("StudySystem.SelectedTitle") %>'></asp:Label>
												</td>
												<%--<td>
													<asp:Label ID="lblCalcSectionStudingPeriod" runat="server" Text='<%#Eval("StudyingPeriod") %>'></asp:Label>
												</td>--%>
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




<div class="col-md-12 no-padding margin-bottom-10">
    								<uc1:FileUpload runat="server" id="fileUploadCalculatedDetails" Visible="false" />

</div>						
                                
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
									<%--	<asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, StudyType %>">
											<ItemTemplate>

												<asp:Label ID="lblUniGridStudyingType" runat="server"></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>--%>
										<asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, StudyActualPeriod %>">
											<ItemTemplate>

												<asp:Label ID="lblUniGridStudingPeriod" runat="server"></asp:Label>

											</ItemTemplate>
										</asp:TemplateField>
										<%--     <asp:TemplateField >
               <ItemTemplate >

               
        </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField >
               <ItemTemplate >

                
        </ItemTemplate>
                </asp:TemplateField>--%>
									</Columns>

								</asp:GridView>
							</div>


							<!--5th-->
							<h3 class="inner-accordion">
								<asp:Label ID="lblPublishingCertificateUnivesity" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityPublishingCertificateDetails %>" Font-Bold="true"></asp:Label>
							</h3>

							<div id="divPublishingCertificate" class="display-mode">
								<div class="row margin-top-15">
									<div class="col-md-4 col-sm-6 no-padding auto-height">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblUniversityMainHeadQuarter" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityMainHeadquarter %>"></asp:Label>


												</h6>
												<h5 class="font-size-18 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblUniversityMainHeadQuarterValue" runat="server"></asp:Label>
													<asp:Label ID="lblNewUniversityMainHeadQuarterValue" runat="server" Visible="false"></asp:Label>

												</h5>
											</div>
										</div>
									</div>

									<div class="col-md-4 col-sm-6 no-padding auto-height">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">

													<asp:Label ID="lblUniversityAddress" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityAddress %>"></asp:Label>


												</h6>
												<h5 class="font-size-18 margin-bottom-0 margin-top-0">
													<asp:Label ID="lblAddressValue" runat="server"></asp:Label>
												</h5>
											</div>
										</div>
									</div>

									<div class="col-md-4 col-sm-6 no-padding auto-height">
										<div class="data-container table-display moe-width-85  moe-sm-full-width">
											<div class="form-group">
												<h6 class="font-size-16 margin-bottom-0 margin-top-0">

													<asp:Label ID="lblUniversityEmail" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityEmail %>" Font-Bold="true"></asp:Label>

												</h6>
												<h5 class="font-size-18 margin-bottom-0 margin-top-0">
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
							<div class="col-md-4 col-sm-6 no-padding auto-height">
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

							<div class="col-md-4 col-sm-6 no-padding auto-height" id="workEntityNeedsEquivalency" runat="server" visible="false">
								
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

                           

							<div class="col-md-4 auto-height col-sm-6 no-padding" id="WorkingFor" runat="server" visible="false">
								
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

								<%--<div class="col-md-4 col-sm-6 no-padding" id="divOccupation"  runat="server" visible="false">
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
								<%--<div class="col-md-4 col-sm-6 no-padding" id="hiringSection" style="display: none">
									<div class="data-container table-display moe-width-85">
										<div class="form-group">
											<h6 class="font-size-16 margin-bottom-0 margin-top-0">

												<asp:Label ID="lblHiringDate" runat="server" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, HiringDate %>" Font-Bold="true"></asp:Label>
											</h6>
											<h5 class="font-size-20  margin-bottom-0 margin-top-0">
												<asp:Label ID="lblHiringDateValue" runat="server" Visible="false" />
											</h5>
										</div>
									</div>
								</div>--%>

								<%--<div class="col-md-4 col-sm-6 no-padding" id="workPhoneSection" style="display: none">
									<div class="data-container table-display moe-width-85">
										<div class="form-group">
											<h6 class="font-size-16 margin-bottom-0 margin-top-0">

												<asp:Label ID="lblWorkPhone" runat="server" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, WorkPhone %>" Font-Bold="true"></asp:Label>

											</h6>
											<h5 class="font-size-20  margin-bottom-0 margin-top-0">
												<asp:Label ID="lblWorkPhoneValue" runat="server" Visible="false"></asp:Label>
											</h5>
										</div>
									</div>
								</div>--%>

							</div>







							<%--<div class="col-md-4 col-sm-6 no-padding">
								<div class="data-container table-display moe-width-85">
									<div class="form-group">
										<h6 class="font-size-16 margin-bottom-0 margin-top-0">

											<asp:Label ID="lblIncomingNumber" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, IncomingNumber %>" Font-Bold="true"></asp:Label>

										</h6>
										<h5 class="font-size-20  margin-bottom-0 margin-top-0">
											<asp:Label ID="lblIncomingNumberValue" runat="server"></asp:Label>
										</h5>
									</div>
								</div>
							</div>--%>

							<%--<div class="col-md-4 col-sm-6 no-padding">
								<div class="data-container table-display moe-width-85">
									<div class="form-group">
										<h6 class="font-size-16 margin-bottom-0 margin-top-0">


											<asp:Label ID="lblDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Date %>" Font-Bold="true"></asp:Label>

										</h6>
										<h5 class="font-size-20  margin-bottom-0 margin-top-0">
											<asp:Label ID="lblDateValue" runat="server" />
										</h5>
									</div>
								</div>
							</div>--%>

							<%--<div class="col-md-4 col-sm-6 no-padding">
								<div class="data-container table-display moe-width-85">
									<div class="form-group">
										<h6 class="font-size-16 margin-bottom-0 margin-top-0">

											<asp:Label ID="lblBarCode" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, BarCode %>" Font-Bold="true"></asp:Label>
										</h6>
										<h5 class="font-size-20  margin-bottom-0 margin-top-0">
											<asp:Label ID="lblBarCodeValue" runat="server"></asp:Label>
										</h5>
									</div>
								</div>
							</div>--%>

                        <div class="row">
							<div class="col-md-7 col-sm-12 col-xs-12 no-padding test-file" id="fileWorkig">
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

                             <div class="col-md-7 col-sm-12 col-xs-12 no-padding margin-top-15" id="filenotWorking"  >
                                    <div class="row data-container table-display moe-full-width">
                                        <div class="form-group">
                                            <div class="form fileUpload-width">

                                                <uc1:FileUpload runat="server" id="fileUploadNotWorking" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>




						



						<%-- <asp:Label ID="lblEntityNeedsEquivalency" runat="server"></asp:Label>
          dropdown needed for the 3 dropdowns from user control
        <asp:Label ID="lblEntityWorkingFor" runat="server"></asp:Label>
          <asp:Label ID="lblOccupation" runat="server"></asp:Label>--%>


						<%-- <asp:Label ID="lblCopyOfOrganizationlLetter" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CopyOfOrganizationlLetter %>" Font-Bold="true"></asp:Label>
        <br />
        <asp:GridView ID="gridCopyOfOrganizationLetter" runat="server" AutoGenerateColumns="false">
            <Columns>


                  <asp:BoundField DataField="FileName"  HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, TemplateName %>"/>
                   <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, DownloadFile %>">  
                                    <ItemTemplate>                      
                                     <asp:LinkButton ID="lnkDownloadCopy" Text= '<%# Eval("FileName") %>' CommandArgument = '<%# Eval("ID") %>' runat="server"  OnClick="lnkDownloadCopy_Click" ></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>  

            </Columns>
        </asp:GridView>--%>
					</div>
				</asp:Panel>
			</div>

			<!--3rf Tab-->
			<h3>
				<asp:Button ID="btnPreviousCertificates" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PreviousCertificates %>" CssClass="popdate accordion-input" />
			</h3>
			<div class="container margin-top-25">
				<asp:Panel ID="pnlPreviousCertificates" runat="server" CssClass="row">
					<div id="divPreviousCertificates" class="display-mode col-md-6 col-xs-12 col-sm-12 col-xs-12">
						<uc1:FileUpload runat="server" id="fileUploadCertificates" />
                        
                         <%--<uc1:FileUpload runat="server" id="fileUploadGeneralSec"  />--%>
                                
                        
                                 
                        <uc1:FileUpload runat="server" id="fileUploadDiploma" Visible="false" />

                  
                    
                        <uc1:FileUpload runat="server" id="fileUploadInterDiploma" Visible="false" />

                        <uc1:FileUpload runat="server" id="fileUploadSequenceCert"/>
						<%--   <asp:Label ID="lblPreviousCertificates" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PreviousCertificates %>"></asp:Label>
            <br />--%>
						<%--This will be grid of upload--%>
					</div>
				</asp:Panel>
			</div>

			<!--4th Tab-->
			<h3>
				<asp:Button ID="btnDelegationTemplates" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, DelegationTemplates %>" CssClass="popdate accordion-input"></asp:Button>

			</h3>
			<div class="container margin-top-25">
				<asp:Panel ID="pnlDelegationTemplates" runat="server" CssClass="row">
					<div id="divDelegationTemplates">
						<%--<h6 class="font-size-16 margin-bottom-0 margin-top-0">
                            <asp:Label ID="lblDelegationTemplates" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, DelegationTemplates %>" Font-Bold="true"></asp:Label>
						</h6>--%>
						
						<div class="col-md-6 col-sm-12 col-xs-12 ">
                            <uc1:FileUpload runat="server" id="fileUploadDelegates" />
						</div>
						<%--  
            <br />
            <asp:GridView ID="gridDelegationTemplates" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="DelegationTempFileName"  HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, TemplateName %>"/>
                   <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, DownloadFile %>">  
                                    <ItemTemplate>                      
                                     <asp:LinkButton ID="lnkDownload" Text= '<%# Eval("FileName") %>' CommandArgument = '<%# Eval("ID") %>' runat="server"  OnClick="lnkDownload_Click" ></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>  

                       
                </Columns>
            </asp:GridView>--%>
					</div>
				</asp:Panel>
			</div>


		</div>
		<!--Start of Accordion-->
	</div>
	<!--End of unheighlited section-->










</div>
<div class="row no-padding">
	<h4 class="font-size-18 font-weight-600">
	<asp:Label ID="lblNoRequest" runat="server" Font-Bold="true" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, YouHaveNoRequests %>"></asp:Label>
</h4>
</div>
<div class="row no-padding">
	<asp:Button ID="btn_Print" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Print %>" OnClientClick="Print(this)" CssClass="btn moe-btn pull-right" />
</div>
