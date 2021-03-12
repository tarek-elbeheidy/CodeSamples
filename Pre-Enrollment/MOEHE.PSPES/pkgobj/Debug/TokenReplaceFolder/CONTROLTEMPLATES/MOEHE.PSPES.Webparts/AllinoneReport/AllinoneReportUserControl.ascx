<%@ Assembly Name="MOEHE.PSPES, Version=1.0.0.0, Culture=neutral, PublicKeyToken=677b80a9c9c0da8c" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AllinoneReportUserControl.ascx.cs" Inherits="MOEHE.PSPES.Webparts.AllinoneReport.AllinoneReportUserControl" %>
<link href="/_layouts/15/MOEHE.PSPES/assets/css/chosen.css" rel="stylesheet" type="text/css" />
<link href="/_layouts/15/MOEHE.PSPES/assets/css/beautiful-checkbox.css" rel="stylesheet" type="text/css" />
<script src="/_layouts/15/MOEHE.PSPES/scripts/bootstrap-multiselect.js" type="text/javascript"></script>
<link href="/_layouts/15/MOEHE.PSPES/assets/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
<section class="form-horizontal">
    <div class="container mt-30 mb-30 pt-0 pb-0 bg-white-theme">
          <div class="row">
            <!-- BEGIN EXAMPLE TABLE PORTLET-->
				<div class="portlet light ">
					<div class="col-md-12 borde-bottom pb-15">
						<h3 class="caption p-0">
							<asp:Literal ID="ltSearch" runat="server" Text="<%$Resources:MOEHE.PSPES,School%>"></asp:Literal>
						</h3>
						<div class="col-md-12">
							<div class="row">
								<!--Search-->
								<div class="col-md-2">
									<div class="form-group">
										<label for="Term">
											<asp:Literal ID="ltTerm" runat="server" Text="<%$Resources:MOEHE.PSPES,Term%>"></asp:Literal>
										</label>
										<asp:DropDownList runat="server" ID="ddlTerm" AutoPostBack="true" OnSelectedIndexChanged="ddlTerm_SelectedIndexChanged" cssclass="form-control">
										</asp:DropDownList>
									</div>
								</div>
								<!--Search-->
								<div class="col-md-4">
									<div class="form-group">
										<label for="SchoolCode">
											<asp:Literal ID="ltSchoolCode" runat="server" Text="<%$Resources:MOEHE.PSPES,School%>"></asp:Literal>
										</label>
										<asp:DropDownList runat="server" ID="ddlSchool" CssClass="chzn-select form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged"></asp:DropDownList>
									</div>
								</div>
							</div>
							<!--End Row-->
						</div>
					</div>
					<div class="col-md-12">
						<h3 class="caption p-0">
							<asp:Literal ID="ltAcademicInfo" runat="server" Text="<%$Resources:MOEHE.PSPES,AcademicInformation%>"></asp:Literal>
						</h3>
						<div class="col-md-12 borde-bottom pb-15 mb-15">
							<div class="row">
								<div class="col-md-2" id="Grade">
									<div class="form-group">
										<label for="Grade">
											<asp:Literal ID="ltGrade" runat="server" Text="<%$Resources:MOEHE.PSPES,CurrentGrade%>"></asp:Literal>
										</label>
										<asp:DropDownList runat="server" ID="ddlGrade" CssClass="chzn-select form-control"></asp:DropDownList>
									</div>
								</div>
								<div class="col-md-3">
									<div class="form-group">
										<label for="Curriculum">
											<asp:Literal ID="ltCurriculum" runat="server" Text="<%$Resources:MOEHE.PSPES,Curriculum%>"></asp:Literal>
										</label>
										<asp:DropDownList runat="server" ID="ddlCurriculum" CssClass="form-control"></asp:DropDownList>
									</div>
								</div>
                                 <div class="col-sm-2">
                                    <label for="SchoolGender" class="pl-5">
                                        <asp:Literal ID="ltSchoolGender" runat="server" Text="<%$Resources:MOEHE.PSPES,SchoolGender%>"></asp:Literal>
                                    </label>
                                    <div class="col-sm-12">
                                        <asp:DropDownList runat="server" ID="ddlSchoolGender" Width="100%">
                                        </asp:DropDownList>
                                    </div>
                                </div>
								<div class="col-md-2">
									<div class="form-group">
										<label for="Gender">
											<asp:Literal ID="ltGender" runat="server" Text="<%$Resources:MOEHE.PSPES,Gender%>"></asp:Literal>
										</label>
										<asp:DropDownList runat="server" ID="ddlGender" CssClass="form-control"></asp:DropDownList>
									</div>
								</div>
								<div class="col-md-3">
									<div class="form-group">
										<label for="Nationality">
											<asp:Literal ID="ltNationality" runat="server" Text="<%$Resources:MOEHE.PSPES,Nationality%>"></asp:Literal>
										</label>
										<asp:DropDownList runat="server" ID="ddlNationality" CssClass="form-control"></asp:DropDownList>
									</div>
								</div>
								<div class="col-md-2">
									<div class="form-group">
										<label for="Status">
											<asp:Literal ID="ltStatus" runat="server" Text="<%$Resources:MOEHE.PSPES,Status%>"></asp:Literal>
										</label>
										<asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control"></asp:DropDownList>
									</div>
								</div>
							</div>
						</div>
					</div>
					<div class="col-md-12 borde-bottom pb-15">
                        <div class="pure-checkbox">
                            <asp:CheckBox ID="chkCurrentYearInfo" runat="server" CssClass="col-md-12 col-xs-12 col-sm-12 Title" Text="<%$Resources:MOEHE.PSPES,CurrentYearAcademicInformation%>" />
                            <asp:CheckBox ID="chkCurrentYearSchool" runat="server" CssClass="col-md-2" Text="<%$Resources:MOEHE.PSPES,School%>" />
                            <asp:CheckBox ID="chkCurrentYearGrade" runat="server" CssClass="col-md-2" Text="<%$Resources:MOEHE.PSPES,CurrentGrade%>" />
                            <asp:CheckBox ID="chkCurrentYearCurriculum" runat="server" CssClass="col-md-2" Text="<%$Resources:MOEHE.PSPES,Curriculum%>" />
                            <asp:CheckBox ID="chkCurrentYearResults" runat="server" CssClass="col-md-2 " Text="<%$Resources:MOEHE.PSPES,Result%>" />
                        </div>
                        <div class="pure-checkbox">
                            <asp:CheckBox ID="chkPreviousYearInfo" runat="server" ValidationGroup="2" CssClass="col-md-12 col-xs-12 col-sm-12 Title" Text="<%$Resources:MOEHE.PSPES,PRVYearsAcademicInfo%>" />
                            <asp:CheckBox ID="chkPreviousYearSchool" runat="server" ValidationGroup="2" CssClass="col-md-2" Text="<%$Resources:MOEHE.PSPES,School%>" />
                            <asp:CheckBox ID="chkPreviousYearGrade" runat="server" ValidationGroup="2" CssClass="col-md-2" Text="<%$Resources:MOEHE.PSPES,CurrentGrade%>" />
                            <asp:CheckBox ID="chkPreviousYearCurriculum" runat="server" ValidationGroup="2" CssClass="col-md-2" Text="<%$Resources:MOEHE.PSPES,Curriculum%>" />
                            <asp:CheckBox ID="chkPreviousYearResults" runat="server" ValidationGroup="2" CssClass="col-md-2" Text="<%$Resources:MOEHE.PSPES,Result%>" />
                        </div>
					</div>
				   <div class="col-md-12 borde-bottom pb-15">
						<div class="col-md-12">
							<h3 class="caption p-0">
								<asp:Literal ID="ltPersonalInfo" runat="server" Text="<%$Resources:MOEHE.PSPES,PersonalInformation%>"></asp:Literal>
							</h3>
							<div class=" pure-checkbox">
								<asp:CheckBox ID="chkStudentInfo" CssClass="col-md-12 col-xs-12 col-sm-12 Title" runat="server" Text="<%$Resources:MOEHE.PSPES,PersonalInformation%>" />
								<div class="col-md-12 p-0">
									<asp:CheckBox ID="chkStudentName" runat="server" CssClass="col-md-2" Text="<%$Resources:MOEHE.PSPES,StudentName%>" />
									<asp:CheckBox ID="chkRefNo" runat="server" CssClass="col-md-2" Text="<%$Resources:MOEHE.PSPES,ApplicationReferenceNo%>" />
									<asp:CheckBox ID="chkStudentBirthDate" runat="server" CssClass="col-md-2" Text="<%$Resources:MOEHE.PSPES,DOB%>" />
									<asp:CheckBox ID="chkStudentNationality" runat="server" CssClass="col-md-2" Text="<%$Resources:MOEHE.PSPES,studentNationality%>" />
									<asp:CheckBox ID="chkStudentArea" runat="server" CssClass="col-md-2" Text="<%$Resources:MOEHE.PSPES,ResedentialArea%>" />
									<asp:CheckBox ID="chkStudentTransport" runat="server" CssClass="col-md-2" Text="<%$Resources:MOEHE.PSPES,TransporationRequired%>" />
									<asp:CheckBox ID="chkApplicationDate" runat="server" CssClass="col-md-2" Text="<%$Resources:MOEHE.PSPES,ApplicationDate%>" />
									<asp:CheckBox ID="chkStudentQID" runat="server" CssClass="col-md-3" Text="<%$Resources:MOEHE.PSPES,QatariID%>" />
								</div>
							</div>
						</div>
						<div class="col-md-12">
							<div class=" pure-checkbox ">
								<asp:CheckBox ID="chkGuardianInfo" runat="server" CssClass="col-md-12 col-xs-12 col-sm-12 Title" Text="<%$Resources:MOEHE.PSPES,GuardianInformation%>" />
								<asp:CheckBox ID="chkGuardianName" runat="server" CssClass="col-md-2 " Text="<%$Resources:MOEHE.PSPES,GuardianName%>" />
								<asp:CheckBox ID="chkGuardianGender" runat="server" CssClass="col-md-2 " Text="<%$Resources:MOEHE.PSPES,Gender%>" />
								<asp:CheckBox ID="chkGuardianRelation" runat="server" CssClass="col-md-2 " Text="<%$Resources:MOEHE.PSPES,Relationship%>" />
								<asp:CheckBox ID="chkGuardianNationality" runat="server" CssClass="col-md-2 " Text="<%$Resources:MOEHE.PSPES,Nationality%>" />
								<asp:CheckBox ID="chkGuardianMobile" runat="server" CssClass="col-md-2" Text="<%$Resources:MOEHE.PSPES,MobileNumber%>" />
								<asp:CheckBox ID="chkGuardianHome" runat="server" CssClass="col-md-2 " Text="<%$Resources:MOEHE.PSPES,Landline%>" />
								<asp:CheckBox ID="chkGuardianEmail" runat="server" CssClass="col-md-2 " Text="<%$Resources:MOEHE.PSPES,Email%>" />
								<asp:CheckBox ID="chkGuardianSector" runat="server" CssClass="col-md-2 " Text="<%$Resources:MOEHE.PSPES,EmploymentType%>" />
								<asp:CheckBox ID="chkGuardianEmployer" runat="server" CssClass="col-md-2 " Text="<%$Resources:MOEHE.PSPES,Employer%>" />
								<asp:CheckBox ID="chkGuardianQID" runat="server" CssClass="col-md-3 " Text="<%$Resources:MOEHE.PSPES,QatariID%>" />
							</div>
						</div>
					</div>
                 <div class="col-md-12 borde-bottom pb-15">
						<div class="col-md-12">
							<h3 class="caption p-0">
								<asp:Literal ID="ltHealthInfo" runat="server" Text="<%$Resources:MOEHE.PSPES,HealthInformation%>"></asp:Literal>
							</h3>
							<div class="pure-checkbox">
								<asp:CheckBox ID="chkHealthInfo" CssClass="col-md-12 col-xs-12 col-sm-12 Title" runat="server" Text="<%$Resources:MOEHE.PSPES,HealthInformation%>" />
								<asp:CheckBox ID="chkHealthCard" runat="server" CssClass="col-md-2" Text="<%$Resources:MOEHE.PSPES,HCNumber%>" />
								<asp:CheckBox ID="chkHealthCenter" runat="server" CssClass="col-md-2 " Text="<%$Resources:MOEHE.PSPES,HealthCenter%>" />
								<asp:CheckBox ID="chkFit" runat="server" CssClass="col-md-2 " Text="<%$Resources:MOEHE.PSPES,FitForSchooling%>" />
								<asp:CheckBox ID="chkSpecialNeed" runat="server" CssClass="col-md-2 " Text="<%$Resources:MOEHE.PSPES,SpecialNeed%>" />
								<asp:CheckBox ID="chkLearning" runat="server" CssClass="col-md-2 " Text="<%$Resources:MOEHE.PSPES,LearningDifficulties%>" />
								<asp:CheckBox ID="chkKnowIssues" runat="server" CssClass="col-md-2" Text="<%$Resources:MOEHE.PSPES,FIELD_TITLE_HEALTHISSUES%>" />
							</div>
						</div>
                </div>
                 <div class="col-md-12 borde-bottom pb-15">
					<div class="col-md-12">
						<h3 class="caption p-0">
							<asp:Literal ID="ltFinalizationInfo" runat="server" Text="<%$Resources:MOEHE.PSPES,ApplicationCompletionprocess%>"></asp:Literal>
						</h3>
						<div class=" pure-checkbox">
							<asp:CheckBox ID="chkSupportDocInfo" CssClass="col-md-12 Title" runat="server" Text="<%$Resources:MOEHE.PSPES,SupportingDocuments%>" />
							<asp:CheckBox ID="chkDocList" runat="server" CssClass="col-md-2 " Text="<%$Resources:MOEHE.PSPES,RequiredDocuments%>" />
						</div>
						<div class=" pure-checkbox">
							<asp:CheckBox ID="chkTestInfo" runat="server" CssClass="col-md-12 Title" Text="<%$Resources:MOEHE.PSPES,FIELD_TITLE_TESTINFO%>" />
							<asp:CheckBox ID="chkCallTest" runat="server" CssClass="col-md-2 " Visible="false" Text="<%$Resources:MOEHE.PSPES,CallforTest%>" />
							<asp:CheckBox ID="chkTestDate" runat="server" CssClass="col-md-2 " Text="<%$Resources:MOEHE.PSPES,TestDate%>" />
							<asp:CheckBox ID="chkTestRejected" runat="server" CssClass="col-md-2 " Visible="false" Text="<%$Resources:MOEHE.PSPES,RejectedTest%>" />
							<asp:CheckBox ID="chkTestRejectedReason" runat="server" CssClass="col-md-2 " Visible="false" Text="<%$Resources:MOEHE.PSPES,rejectReason%>" />
							<asp:CheckBox ID="chkUploadResults" runat="server" CssClass="col-md-2 " Text="<%$Resources:MOEHE.PSPES,Result%>" />
						</div>
						<div class=" pure-checkbox ">
							<asp:CheckBox ID="chkInterviewInfo" runat="server" CssClass="col-md-12 Title" Text="<%$Resources:MOEHE.PSPES,FIELD_TITLE_INTERVIEWINFO%>" />
							<asp:CheckBox ID="chkCalledForInterview" runat="server" CssClass="col-md-2 " Visible="false" Text="<%$Resources:MOEHE.PSPES,Callinterview%>" />
							<asp:CheckBox ID="chkInterviewDate" runat="server" CssClass="col-md-2 " Text="<%$Resources:MOEHE.PSPES,interviewDate%>" />
							<asp:CheckBox ID="chkInterviewRejected" runat="server" CssClass="col-md-2 " Visible="false" Text="<%$Resources:MOEHE.PSPES,Rejectinterview%>" />
							<asp:CheckBox ID="chkInterviewRejectedReason" runat="server" CssClass="col-md-2 " Visible="false" Text="<%$Resources:MOEHE.PSPES,InterviwRejectReason%>" />
							<asp:CheckBox ID="chkInterviewResults" runat="server" CssClass="col-md-2" Text="<%$Resources:MOEHE.PSPES,Interviwresult%>" />
						</div>
					</div>
                </div>
                <div class="col-md-12 borde-bottom pb-15">
					<div class="col-md-12">
						<h3 class="caption p-0">
                        <asp:Literal ID="ltSeatReservationInfo" runat="server" Text="<%$Resources:MOEHE.PSPES,feesInfo%>"></asp:Literal>
						</h3>
                        <div class=" pure-checkbox ">
                            <asp:CheckBox ID="chkCalledForSeatReservation" CssClass="col-md-12 Title" runat="server" Text="<%$Resources:MOEHE.PSPES,seatReservation%>" />
                            <asp:CheckBox ID="chkCalledPay" runat="server" CssClass="col-md-2 " Visible="false" Text="<%$Resources:MOEHE.PSPES,Callforpayment%>" />
                            <asp:CheckBox ID="chkFee" runat="server" CssClass="col-md-2" Text="<%$Resources:MOEHE.PSPES,reservationFees%>" />
                            <asp:CheckBox ID="chkFeeDate" runat="server" CssClass="col-md-2" Text="<%$Resources:MOEHE.PSPES,feesPaidDate%>" />
                        </div>
                        <div class=" pure-checkbox ">
                            <asp:CheckBox ID="chkPaidFeeInfo" runat="server" CssClass="col-md-12 Title" Text="<%$Resources:MOEHE.PSPES,feesInfo%>" />
                            <asp:CheckBox ID="chkPaidFee" runat="server" CssClass="col-md-2 " Text="<%$Resources:MOEHE.PSPES,reservationFees%>" />
                            <asp:CheckBox ID="chkPaidFeeDate" runat="server" CssClass="col-md-2 " Text="<%$Resources:MOEHE.PSPES,feesPaidDate%>" />
                        </div>
                    </div>
                </div>
                <div class="col-md-12 borde-bottom pb-15">
					<div class="col-md-12">
						<h3 class="caption p-0">
                        <asp:Literal ID="ltReportInfo" runat="server" Text="<%$Resources:MOEHE.PSPES,REPORT_PAGE_REPORTINFO%>"></asp:Literal>
						</h3>
                        <div class="col-md-12">
                            <asp:LinkButton ID="lnkExportExcel" CssClass="btn btn-default col-md-2" Text="<%$Resources:MOEHE.PSPES,BUTTON_TITLE_EXPORTEXCEL%>" runat="server" OnClick="lnkExportExcel_Click"><i class="fa fa-file-excel-o" aria-hidden="true" style="font-size: 15px"></i></asp:LinkButton> 
                        </div>
                        <div class="col-md-2" style="visibility:hidden">
                            <asp:LinkButton ID="lnkExportPDF" CssClass="btn btn-default" Text="<%$Resources:MOEHE.PSPES,BUTTON_TITLE_EXPORTPDF%>" runat="server" OnClick="lnkExportPDF_Click" Enabled="False"></asp:LinkButton>
                        </div>
                    </div>
                </fieldset>
            </div>
            <!-- END EXAMPLE TABLE PORTLET-->
        </div>
    </div>
</section>
<script type="text/javascript">
    $(".chzn-select").chosen();
    $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

    $(document).ready(function () {
        $(".Title label").addClass("caption-radio");
        $(".input-list li span").wrap("<div class='pure-radiobutton'></div>");
        $(".input-list li").addClass("col-md-6 ");
    });
    //Select & Unselect All Current Year
    $('input[id*=chkCurrentYearInfo]').click(function () {
        $('input[id*=chkCurrentYearSchool]').prop('checked', this.checked);
        $('input[id*=chkCurrentYearGrade]').prop('checked', this.checked);
        $('input[id*=chkCurrentYearCurriculum]').prop('checked', this.checked);
        $('input[id*=chkCurrentYearResults]').prop('checked', this.checked);
    });
    //Select & Unselect All Previous Year
    $('input[id*=chkPreviousYearInfo]').click(function () {
        $('input[id*=chkPreviousYearSchool]').prop('checked', this.checked);
        $('input[id*=chkPreviousYearGrade]').prop('checked', this.checked);
        $('input[id*=chkPreviousYearCurriculum]').prop('checked', this.checked);
        $('input[id*=chkPreviousYearResults]').prop('checked', this.checked);
    });
    //Select & Unselect All Student Personal Info
    $('input[id*=chkStudentInfo]').click(function () {
        $('input[id*=chkStudentQID]').prop('checked', this.checked);
        $('input[id*=chkStudentName]').prop('checked', this.checked);
        $('input[id*=chkStudentName]').prop('checked', this.checked);
        $('input[id*=chkStudentGender]').prop('checked', this.checked);
        $('input[id*=chkRefNo]').prop('checked', this.checked);
        $('input[id*=chkStudentBirthDate]').prop('checked', this.checked);
        $('input[id*=chkStudentNationality]').prop('checked', this.checked);
        $('input[id*=chkStudentArea]').prop('checked', this.checked);
        $('input[id*=chkStudentTransport]').prop('checked', this.checked);
        $('input[id*=chkApplicationDate]').prop('checked', this.checked);
    });
    //Select & Unselect All Guardian Personal Info
    $('input[id*=chkGuardianInfo]').click(function () {
        $('input[id*=chkGuardianQID]').prop('checked', this.checked);
        $('input[id*=chkGuardianName]').prop('checked', this.checked);
        $('input[id*=chkGuardianGender]').prop('checked', this.checked);
        $('input[id*=chkGuardianRelation]').prop('checked', this.checked);
        $('input[id*=chkGuardianNationality]').prop('checked', this.checked);
        $('input[id*=chkGuardianMobile]').prop('checked', this.checked);
        $('input[id*=chkGuardianHome]').prop('checked', this.checked);
        $('input[id*=chkGuardianEmail]').prop('checked', this.checked);
        $('input[id*=chkGuardianSector]').prop('checked', this.checked);
        $('input[id*=chkGuardianEmployer]').prop('checked', this.checked);
    });

    //Select & Unselect All Student Health Info
    $('input[id*=chkHealthInfo]').click(function () {
        $('input[id*=chkHealthCard]').prop('checked', this.checked);
        $('input[id*=chkHealthCenter]').prop('checked', this.checked);
        $('input[id*=chkFit]').prop('checked', this.checked);
        $('input[id*=chkSpecialNeed]').prop('checked', this.checked);
        $('input[id*=chkLearning]').prop('checked', this.checked);
        $('input[id*=chkKnowIssues]').prop('checked', this.checked);
    });

    //Select & Unselect All Document Info
    $('input[id*=chkSupportDocInfo]').click(function () {
        $('input[id*=chkDocList]').prop('checked', this.checked);
    });

    //Select & Unselect All Test Info
    $('input[id*=chkTestInfo]').click(function () {
        $('input[id*=chkCallTest]').prop('checked', this.checked);
        $('input[id*=chkTestDate]').prop('checked', this.checked);
        $('input[id*=chkTestRejected]').prop('checked', this.checked);
        $('input[id*=chkTestRejectedReason]').prop('checked', this.checked);
        $('input[id*=chkUploadResults]').prop('checked', this.checked);
    });

    //Select & Unselect All Interview Info
    $('input[id*=chkInterviewInfo]').click(function () {
        $('input[id*=chkCalledForInterview]').prop('checked', this.checked);
        $('input[id*=chkInterviewDate]').prop('checked', this.checked);
        $('input[id*=chkInterviewRejected]').prop('checked', this.checked);
        $('input[id*=chkInterviewRejectedReason]').prop('checked', this.checked);
        $('input[id*=chkInterviewResults]').prop('checked', this.checked);
    });

    //Select & Unselect All SeatReservation Info
    $('input[id*=chkCalledForSeatReservation]').click(function () {
        $('input[id*=chkCalledPay]').prop('checked', this.checked);
        $('input[id*=chkFee]').prop('checked', this.checked);
        $('input[id*=chkFeeDate]').prop('checked', this.checked);
    });

    //Select & Unselect All Interview Info
    $('input[id*=chkPaidFeeInfo]').click(function () {
        $('input[id*=chkPaidFee]').prop('checked', this.checked);
        $('input[id*=chkPaidFeeDate]').prop('checked', this.checked);
    });
</script>

