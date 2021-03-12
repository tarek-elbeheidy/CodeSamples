<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="uc1" TagName="FileUpload" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PADisplayRequestDetails.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.PADisplayPARequestDetails" %>

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

<style>
    /*#divUniversities {
		display: inline-block;
	}

	.align_space {
		padding: 0px 4px;
		margin: 0;
	}*/
</style>

<div id="divformControls" runat="server">

    <h1 class="section-title text-center font-weight-500 margin-bottom-25">
        <asp:Label ID="lblPARequestDetails" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_PA, PARequestDetails %>"></asp:Label>
    </h1>
    <div id="divPARequestDetails" class="container heighlighted-section margin-bottom-50 margin-0">
        <div class="row">
            <div class="col-md-4 col-sm-6">
                <div class="data-container">
                    <h6 class="font-size-16 margin-bottom-15">
                        <asp:Label ID="lblRequestNumber" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_PA, RequestNumber %>"></asp:Label>
                    </h6>
                    <h5 class="font-size-20">
                        <asp:Label ID="lblRequestNumberValue" runat="server"></asp:Label>
                    </h5>
                </div>
            </div>

            <div class="col-md-4 col-sm-6">
                <div class="data-container">
                    <h6 class="font-size-16 margin-bottom-15">
                        <asp:Label ID="lblRequestCreationDate" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_PA, RequestCreationDate %>"></asp:Label>
                    </h6>
                    <h5 class="font-size-20">

                        <asp:Label ID="lblRequestCreationDateValue" runat="server"></asp:Label>
                    </h5>
                </div>
            </div>

            <div class="col-md-4 col-sm-6">
                <div class="data-container">
                    <h6 class="font-size-16 margin-bottom-15">

                        <asp:Label ID="lblRequestSubmitDate" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_PA, RequestSubmitDate %>"></asp:Label>
                    </h6>
                    <h5 class="font-size-20">
                        <asp:Label ID="lblRequestSubmitDateValue" runat="server"></asp:Label>
                    </h5>
                </div>
            </div>
        </div>
    </div>

    <div class="row unheighlighted-section test-display">
        <!--Start of Accordion-->
        <div class="accordion PAaccordion">

            <!--Start of First Accordion-->
            <h3>
                <!--btnPreviousCertificates-->
                <asp:Button ID="btnPreviousCertificates" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, WantedCertificateDegree %>" CssClass="popdate accordion-input" />
            </h3>
            <div class="container margin-top-25">
                <!--Start of pnlPreviousCertificates-->
                <asp:Panel ID="pnlPreviousCertificates" runat="server">

                    <!--Start of divUniversities-->
                    <div id="divUniversities" class="display-mode">
                        <div class="row margin-top-15">
                            <div class="col-md-4 col-sm-6 no-padding ">
                                <div class="data-container table-display moe-width-85">
                                    <div class="form-group">
                                        <h6 class="font-size-16 margin-bottom-0 margin-top-0 ">
                                            <asp:Label ID="lblWantedCertificateDegree" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, WantedAcademicDegree %>"></asp:Label>
                                        </h6>

                                        <h5 class="font-size-18 margin-bottom-0 margin-top-0">
                                            <asp:Label ID="ddlWantedCertificateDegree" Text="N/A" runat="server" />
                                        </h5>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-6 no-padding">
                                <div class="data-container table-display moe-width-85">
                                    <div class="form-group">
                                        <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                            <asp:Label ID="lblCalcSectionCountry" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, Country %>"></asp:Label>
                                        </h6>

                                        <h5 class="font-size-18 margin-bottom-0 margin-top-0">
                                            <asp:Label ID="ddlCalcSectionCountry" Text="N/A" runat="server" />
                                        </h5>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-4 col-sm-6 no-padding">
                                <div class="data-container table-display moe-width-85">
                                    <div class="form-group">
                                        <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                            <asp:Label ID="lblCalcSectionUniversity" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, UniversityName %>"></asp:Label>
                                        </h6>
                                        <h5 class="font-size-18 margin-bottom-0 margin-top-0">
                                            <asp:Label ID="ddlCalcSectionUniversity" Text="N/A" runat="server" />
                                        </h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row margin-top-15">

                            <div class="col-md-4 col-sm-6 no-padding">
                                <div class="data-container table-display moe-width-85">
                                    <div class="form-group">
                                        <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                            <asp:Label ID="lblCalcSectionFaculty" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, FacultyName %>"></asp:Label>
                                        </h6>

                                        <h5 class="font-size-18 margin-bottom-0 margin-top-0">
                                            <%--<asp:Label ID="txtCalcSectionFaculty" Text="N/A" runat="server" />--%>
                                            <div id="calcFaculty">
                                                <asp:Label ID="ddlCalcSectionFaculty" Text="N/A" runat="server" />
                                                <%--<asp:Label id="ddlCalcSectionFacultyOther" Text="N/A" runat="server" Visible="false" />--%>
                                            </div>
                                        </h5>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-6 no-padding">
                                <div class="data-container table-display moe-width-85">
                                    <div class="form-group">
                                        <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                            <asp:Label ID="lblProgramSpecialization" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, specialization %>"></asp:Label>
                                        </h6>

                                        <h5 class="font-size-18 margin-bottom-0 margin-top-0">
                                            <%--<asp:Label ID="txtCalcSectionFaculty" Text="N/A" runat="server" />--%>
                                            <div id="calcspecialization">
                                                <asp:Label ID="lblProgramSpecializationVal" Text="N/A" runat="server" />
                                            </div>
                                        </h5>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-6 no-padding">
                                <div class="data-container table-display moe-width-85">
                                    <div class="form-group">
                                        <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                            <asp:Label runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, StudySystem %>"></asp:Label>
                                        </h6>

                                        <h5 class="font-size-18 margin-bottom-0 margin-top-0">
                                            <asp:Label ID="dropCalcSectionStudyingSystem" Text="N/A" runat="server" />
                                        </h5>
                                    </div>
                                </div>
                            </div>

                            <%--							<div class="col-md-4 col-sm-6 no-padding">
								<div class="data-container table-display moe-width-85">
									<div class="form-group">
										<h6 class="font-size-16 margin-bottom-0 margin-top-0">
											<asp:Label runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, StudyType %>"></asp:Label>
										</h6>

										<h5 class="font-size-18 margin-bottom-0 margin-top-0">
											<asp:Label ID="dropCalcSectionStudyType" Text="N/A" runat="server" />
										</h5>
									</div>
								</div>
							</div>--%>
                        </div>
                        <div class="row margin-top-15">
                            <div class="col-md-4 col-sm-6 no-padding">
                                <div class="data-container table-display moe-width-85">
                                    <div class="form-group">
                                        <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                            <asp:Label ID="lblAcademicNumberOfYears" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, AcademicNumberOfYears %>"></asp:Label>
                                        </h6>

                                        <h5 class="font-size-18 margin-bottom-0 margin-top-0">
                                            <asp:Label ID="txtAcademicNumberOfYears" Text="N/A" runat="server" />
                                        </h5>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-6 no-padding">
                                <div class="data-container table-display moe-width-85">
                                    <div class="form-group">
                                        <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                            <asp:Label ID="lblAcademicStartDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, AcademicStartDate %>"></asp:Label>
                                        </h6>
                                        <h5 class="font-size-18 margin-bottom-0 margin-top-0">
                                            <asp:Label ID="dtAcademicStartDate" Text="N/A" runat="server" />
                                        </h5>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-4 col-sm-6 no-padding">
                                <div class="data-container table-display moe-width-85">
                                    <div class="form-group">
                                        <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                            <asp:Label ID="lblAcademicEndDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, AcademicEndDate %>"></asp:Label>
                                        </h6>

                                        <h5 class="font-size-18 margin-bottom-0 margin-top-0">
                                            <asp:Label ID="dtAcademicEndDate" Text="N/A" runat="server" />
                                        </h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row margin-top-15 ">
                            <div class="col-md-4 col-sm-6 no-padding" style="display: none">
                                <div class="data-container table-display moe-width-85">
                                    <div class="form-group">
                                        <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                            <asp:Label ID="lblActualStudyPeriod" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, StudyPeriod %>"></asp:Label>
                                        </h6>

                                        <h5 class="font-size-18 margin-bottom-0 margin-top-0">
                                            <asp:Label ID="txtCalcSectionStudingPeriod" Text="N/A" runat="server" />
                                        </h5>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdnCalcSectionItem" runat="server" />
                            <%--<div class="col-md-9 col-sm-12 col-xs-12 no-padding margin-bottom-15  sm-width-90">
                                <div class="row data-container table-display moe-full-width  sm-width-90">
                                    <div class="form-group">
                                        <h5 class="font-size-18 margin-bottom-0 margin-top-0">
                                            <%--<asp:Label ID="fileUploadCalcDetails" Text="File Upload" runat="server" />--%>
                            <%-- <div class="form">
                                                <uc1:FileUpload runat="server" id="fileUploadCalcDetails" />
                                            </div>
                                        </h5>
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
                        <%--<div class="row margin-top-15" id="joinedOtherUni">

                            <div class="col-md-6 col-sm-6 no-padding">
                                <div class="data-container table-display moe-width-85">
                                    <div class="form-group">
                                        <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                            <asp:Label ID="lblJoinedOtherUni" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, JoinedOtherUniversities %>"></asp:Label>
                                        </h6>

                                        <h5 class="font-size-18 margin-bottom-0 margin-top-0">
                                            <asp:Label ID="lblJoinedOtherUniValue" Text="N/A" runat="server" />
                                        </h5>

                                        <div class="form margin-top-15">
                                            <uc1:FileUpload runat="server" id="fileUploadGrades" Visible="false" />
                                        </div>
                                        <div class="form margin-top-15">
                                            <uc1:FileUpload runat="server" id="fileUploadAcceptedHours" Visible="false" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                    </div>
                    <!--End of divUniversities-->
                </asp:Panel>
                <!--End of pnlPreviousCertificates-->
            </div>
            <!--End of First Accordion-->

            <!--Start of Second Accordion-->
            <h3>
                <!--btnDegreeRequiredToBEquivalentData-->
                <asp:Button ID="btnDegreeRequiredToBEquivalentData" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, HighestDegreeHeader %>"
                    CssClass="popdate accordion-input"></asp:Button>
            </h3>
            <div class="container margin-top-25">
                <!--Inner Accordion-->
                <div class="row margin-top-0">
                    <div class="col-md-4 col-sm-6 no-padding">
                        <div class="data-container table-display moe-width-85 moe-sm-full-width">
                            <div class="form-group">
                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                    <asp:Label ID="lblHighestCertificate" Text="<%$Resources:ITWORX_MOEHEWF_PA, HigherCertificate %>" runat="server"></asp:Label>
                                </h6>
                                <h5 class="font-size-18 margin-bottom-0 margin-top-0">
                                    <asp:Label ID="ddlHighestCertificate" Text="N/A" runat="server" />
                                </h5>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-4 col-sm-6 no-padding">
                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                            <div class="form-group">
                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                    <asp:Label ID="lblCountries" Text="<%$Resources:ITWORX_MOEHEWF_PA, CountryOfStudy %>" runat="server"></asp:Label>
                                </h6>
                                <h5 class="font-size-18 margin-bottom-0 margin-top-0">
                                    <asp:Label ID="lblCountriesValue" Text="N/A" runat="server"></asp:Label>
                                </h5>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-4 col-sm-6 no-padding">
                        <div class="data-container table-display moe-width-85">
                            <div class="form-group">
                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                    <asp:Label ID="lblCertificateDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, CertificateDate %>" ClientIDMode="Static"></asp:Label>
                                </h6>
                                <h5 class="font-size-18 margin-bottom-0 margin-top-0">
                                    <asp:Label ID="txtCertificateDate" Text="N/A" runat="server" />
                                </h5>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row margin-top-15">
                    <div class="col-md-6 col-sm-12 col-xs-12 no-padding ">

                        <uc1:FileUpload runat="server" id="FUDequivalentHours" />
                    </div>
                </div>

                <div id="sectionSecondaryCertificate" runat="server" class="row margin-top-15">

                    <div class="col-md-12 col-sm-12 col-xs-12 no-padding margin-top-15">

                        <%--<asp:Label ID="schoolDocuments" Text="School File Upload" runat="server" />--%>
                        <div class="form">
                            <uc1:FileUpload runat="server" id="schoolDocuments" />
                            <uc1:FileUpload runat="server" id="fileUploadNationalService" />
                        </div>
                    </div>
                </div>
                <!--divUniversitiesDetails Label-->
                <!--Start of divUniversitiesDetails-->
                <div id="divUniversitiesDetails" runat="server" class="row margin-top-15">

                    <!--Start of row-->
                    <div class="col-md-4 col-sm-6 no-padding">
                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                            <div class="form-group">
                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                    <asp:Label runat="server" Text="Univeristy Name"></asp:Label>
                                    <!--Static data-->
                                </h6>
                                <h5 class="font-size-18 margin-bottom-0 margin-top-0">
                                    <asp:Label ID="ddlUniversity" Text="N/A" runat="server" />
                                </h5>
                            </div>
                        </div>
                    </div>

                    <%--<div class="col-md-4 col-sm-6 no-padding">
								<div class="data-container table-display moe-width-85  moe-sm-full-width">
									<div class="form-group">
										<h6 class="font-size-16 margin-bottom-0 margin-top-0">
											<asp:Label ID="lblUniversityList" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, UniversitiesLists %>"></asp:Label>
										</h6>
										<h5 class="font-size-18 margin-bottom-0 margin-top-0">
											<asp:Label ID="txtUniversityListValue" Text="N/A" runat="server" />
										</h5>
									</div>
								</div>
							</div>--%>

                    <div class="col-md-4 col-sm-6 no-padding">
                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                            <div class="form-group">
                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                    <asp:Label ID="facultylbl" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, FacultyName %>"></asp:Label>
                                </h6>
                                <h5 class="font-size-18 margin-bottom-0 margin-top-0">
                                    <asp:Label ID="ddlFaculty" Text="N/A" runat="server" />
                                    <asp:Label ID="ddlFacultyOther" Text="N/A" runat="server" Visible="false" />
                                </h5>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-4 col-sm-6 no-padding">
                        <div class="data-container table-display moe-width-85  moe-sm-full-width">
                            <div class="form-group">
                                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                    <asp:Label ID="specialization" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, specialization %>"></asp:Label>
                                </h6>
                                <h5 class="font-size-18 margin-bottom-0 margin-top-0">
                                    <asp:Label ID="ddlSpecialization" Text="N/A" runat="server" />
                                </h5>
                            </div>
                        </div>
                    </div>

                    <div class="clearfix"></div>
                    <div class="col-md-8 col-sm-12 col-xs-12 no-padding">
                        <%--<asp:Label ID="universityDocuments" Text="File Upload University" runat="server" />--%>
                        <div class="form">
                            <uc1:FileUpload runat="server" id="universityDocuments" />
                        </div>
                    </div>

                    <!--End of divUniversitiesDetails-->
                </div>
                <!--Start of pnlDegreeRequiredToBEquivalentData-->

                <!--End of pnlDegreeRequiredToBEquivalentData-->
            </div>
            <!--End of Second Accordion-->

            <!--Start of Third Accordion-->

            <h3>
                <asp:Button ID="lblUniversitiesNames" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, PrevHoursIfExist %>" CssClass="popdate accordion-input"></asp:Button>
            </h3>
            <div id="divUniversities" class="display-mode">

                <div class="col-md-12 no-padding">
                    <asp:Repeater ID="repCalculatedDetailsForCertificate" runat="server">

                        <HeaderTemplate>
                            <table class="table table-striped moe-full-width moe-table">
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
                                    <th scope="col">
                                        <%=Resources.ITWORX_MOEHEWF_PA.Actions %>
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
                                    <asp:Label ID="lblConvertedHours" runat="server" Text='<%#Eval("AcceptedHours") %>'></asp:Label>
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
                <uc1:FileUpload runat="server" id="fileUploadCalculatedDetails2" Visible="false" />
                <asp:GridView ID="gridUniversitiesNames" runat="server" OnRowDataBound="gridUniversitiesNames_RowDataBound" CssClass="table table-striped moe-full-width moe-table">
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, Country %>">
                            <ItemTemplate>

                                <asp:Label ID="lblUniGridCountry" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, UniversityName %>">
                            <ItemTemplate>

                                <asp:Label ID="lblUniGridUniversity" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, FacultyName %>">
                            <ItemTemplate>

                                <asp:Label ID="lblUniGridFaculty" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, StudySystem %>">
                            <ItemTemplate>

                                <asp:Label ID="lblUniGridStudyingSystem" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, StudyActualPeriod %>">
                            <ItemTemplate>

                                <asp:Label ID="lblUniGridStudingPeriod" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div id="Div_NoResults" runat="server" visible="false" class="row no-padding">
                    <h4 class="font-size-18 font-weight-600 text-center">
                        <asp:Label ID="lbl_NoResults" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, NoData %>"></asp:Label>
                    </h4>
                </div>
            </div>

            <!--End of Third Accordion-->

            <!--Start of Forth Accordion-->

            <h3>
                <!--WorkingDetails-->
                <asp:Button ID="btnWorkingDetails" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, WorkingDetails %>" CssClass="popdate accordion-input"></asp:Button>
            </h3>
            <div class="container margin-top-25">
                <!--Start of pnlWorkingDetails-->
                <asp:Panel ID="pnlWorkingDetails" runat="server">
                    <div id="divWorkingDetails">
                        <div class="row margin-top-15">
                            <div class="col-md-4 col-sm-6 no-padding">
                                <div class="data-container table-display moe-width-85">
                                    <div class="form-group">
                                        <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                            <asp:Label ID="lblWorkingOrNot" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, WorkingOrNot %>"></asp:Label>
                                        </h6>

                                        <h5 class="font-size-18 margin-bottom-0 margin-top-0">
                                            <asp:Label ID="rdbWorkingOrNot" Text="N/A" runat="server" />
                                        </h5>
                                    </div>
                                </div>
                            </div>
                            <!--should this part presented ?-->

                            <div id="ShowHideWorkingForAndOccupation" runat="server">
                                <div class="col-md-4 col-sm-6 no-padding" id="entityWorkingFor">
                                    <div class="data-container table-display moe-width-85">
                                        <div class="form-group">
                                            <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                                <asp:Label ID="Label1" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, EntityWorkingFor %>"></asp:Label>
                                            </h6>
                                            <h5 class="font-size-18 margin-bottom-0 margin-top-0">
                                                <asp:Label ID="ddlEntityWorkingFor" Text="N/A" runat="server" />
                                            </h5>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-9 col-sm-12 col-xs-12 no-padding">
                                    <%--<asp:Label ID="fileUploadWorking" Text="File Upload Not Working" runat="server" />--%>
                                    <div class="form">
                                        <uc1:FileUpload runat="server" id="fileUploadWorking" />
                                    </div>
                                </div>
                            </div>
                            <div id="ShowHideNotWorking" runat="server">
                                <div class="col-md-9 col-sm-12 col-xs-12 no-padding">
                                    <%--<asp:Label ID="fileUploadNotWorking" Text="File Upload Working" runat="server" />--%>
                                    <div class="form">
                                        <uc1:FileUpload runat="server" id="fileUploadNotWorking" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <!--End of pnlWorkingDetails-->
            </div>

            <!--End of Forth Accordion-->
            <h3>
                <!--btnDelegationTemplates-->

                <asp:Button ID="btnDelegationTemplates" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, HeaderDelegationTemplates %>" CssClass="popdate accordion-input" ></asp:Button>
            </h3>
            <div class="container margin-top-25">
                <div class="row margin-top-15">

                    <div class="col-md-6 col-sm-12 col-xs-12 no-padding">

                        <uc1:FileUpload runat="server" id="fileUploadDelegates" />
                    </div>
                </div>
            </div>
            <!--End of Accordion-->
        </div>
        <!--Start of Accordion-->
    </div>
    <!--End of unheighlited section-->
</div>

<div class="col-md-12">
    <asp:Label ID="lblNoRequest" runat="server" Font-Bold="true" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_PA, YouHaveNoRequests %>"></asp:Label>
</div>