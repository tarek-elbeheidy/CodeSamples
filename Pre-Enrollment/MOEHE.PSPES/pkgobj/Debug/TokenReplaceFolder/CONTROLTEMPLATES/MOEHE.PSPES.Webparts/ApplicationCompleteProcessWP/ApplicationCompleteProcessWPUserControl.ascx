<%@ Assembly Name="MOEHE.PSPES, Version=1.0.0.0, Culture=neutral, PublicKeyToken=677b80a9c9c0da8c" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApplicationCompleteProcessWPUserControl.ascx.cs" Inherits="MOEHE.PSPES.Webparts.ApplicationCompleteProcessWP.ApplicationCompleteProcessWPUserControl" %>


	<%--<script src="/_layouts/15/MOEHE.PSPES/assets/js/jquery.min.js" type="text/javascript"></script>--%>
		<script src="/_layouts/15/MOEHE.PSPES/assets/js/chosen.jquery.js" type="text/javascript"></script>
		<script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>

<script  type="text/javascript">

    function checkbox1Check() {



        if (document.getElementById('<%=chkSupportingDocuments.ClientID%>').checked) {
            $('.checkbox1').fadeIn('slow');
        }
        else {
            $('.checkbox1').fadeOut('slow');
        }


    }

    function checkbox2Check() {



        if (document.getElementById('<%=chkTest.ClientID%>').checked) {
            $('.checkbox2').fadeIn('slow');
        }
        else {
            $('.checkbox2').fadeOut('slow');
        }


    }

    function checkbox3Check() {



        if (document.getElementById('<%=chkInterview.ClientID%>').checked) {
            $('.checkbox3').fadeIn('slow'); s
        }
        else {
            $('.checkbox3').fadeOut('slow');
        }


    }




</script>





<!-- Section: Services Details -->
<section class="form-horizontal">
	<div class="container mt-30 mb-30 pt-0 pb-0 bg-white-theme">
	  <div class="row">
		<!-- BEGIN EXAMPLE TABLE PORTLET-->
		<div class="portlet light ">
			<div class="portlet-body">
			  <div class="row m-0">
				<fieldset class="scheduler-border m-0 mb-5 col-sm-12">
				  <legend class="scheduler-border mb-0"><asp:Literal id="Literal1" runat="server" Text="<%$Resources:MOEHE.PSPES,SearchCrit%>"></asp:Literal></legend>
					<div class="row m-0">
						<!--StudientQID-->
						<div class="col-sm-6">
						  <div class="form-group">
							<label for="StudientQID"><asp:Literal id="Literal2" runat="server" Text="<%$Resources:MOEHE.PSPES,QatariID1%>"></asp:Literal> <span class="required"> * </span></label>
							  <asp:TextBox id = "TxtQID" runat="server"  class="form-control"></asp:TextBox>
							</div>
						</div>
						<!--ApplicationRefNbr-->
						<div class="col-sm-6">
							<div class="form-group">
							<label for="ApplicationRefNbr"><asp:Literal id="Literal3" runat="server" Text="<%$Resources:MOEHE.PSPES,appRefNum%>"></asp:Literal> <span class="required"> * </span></label>
							 <asp:TextBox id = "TxtAppRef" class="form-control" runat="server"></asp:TextBox>
							</div>
						</div>
						
					</div>
					<div class="row mr-10">
						<div class="col-md-12">
							<div class="pull-right">
								<asp:LinkButton ID="SearchLinkButton" runat="server" OnClick="SearchLinkButton_Click" Text="" class="btn btn-default"><i class="fa fa-search pt-0"></i> بحث </asp:LinkButton>
							</div>
						</div>
					</div>
					<section>
						<!-- BEGIN EXAMPLE TABLE PORTLET-->
						<div class="portlet light hidden">
							<div class="portlet-body">
								<table class="table table-striped table-bordered table-hover" id="sample_21">
									<thead>
										<tr>
											<th class="text-center"> <asp:Literal runat="server" ID="Literal54" Text='<%$Resources:MOEHE.PSPES,ApplicationRefNo%>'></asp:Literal> </th>
											<th class="text-center"> <asp:Literal runat="server" ID="Literal55" Text='<%$Resources:MOEHE.PSPES,ApplicationDate%>'></asp:Literal> </th>
											<th class="text-center"> <asp:Literal runat="server" ID="Literal56" Text='<%$Resources:MOEHE.PSPES,QID%>'></asp:Literal> </th>
											<th class="text-center">  <asp:Literal runat="server" ID="Literal57" Text='<%$Resources:MOEHE.PSPES,StudentName%>'></asp:Literal>  </th>
											<th class="text-center"> <asp:Literal runat="server" ID="Literal58" Text='<%$Resources:MOEHE.PSPES,Curriculum%>'></asp:Literal> </th>
											<th class="text-center"><asp:Literal runat="server" ID="Literal59" Text='<%$Resources:MOEHE.PSPES,SchoolName%>'></asp:Literal> </th>
											<th class="text-center"><asp:Literal runat="server" ID="Literal60" Text='<%$Resources:MOEHE.PSPES,EditApplicationTitle%>'></asp:Literal> </th>
											<th class="text-center"><asp:Literal runat="server" ID="Literal61" Text='<%$Resources:MOEHE.PSPES,CompleteApplicationTitle%>'></asp:Literal> </th>
										</tr>
									</thead>
									<tbody>
									   <asp:Repeater ID="ApplicationsDataRepeater" runat="server">
											<HeaderTemplate></HeaderTemplate>
											<ItemTemplate>
												<tr>
													<td> <asp:Label ID="ApplicationRefNoLabel" Enabled="false"   Text='<%# Eval("ApplicationRefNo") %>' Visible="true" runat="server"></asp:Label> </td>
														
													<td>
														 <asp:Label ID="ApplicationDateLabel" Enabled="false"   Text='<%# Eval("ApplicationDate") %>' Visible="true" runat="server"></asp:Label>
														
													</td>
												<td class="center ">
													 <asp:Label ID="QIDLabel" Enabled="false"   Text='<%# Eval("QID") %>' Visible="true" runat="server"></asp:Label>
												</td>

												 <td class="center ">
													 <asp:Label ID="StudentNameLabel" Enabled="false"   Text='<%# Eval("StudentName") %>' Visible="true" runat="server"></asp:Label>
												</td>
												 <td class="center ">
													 <asp:Label ID="CurriculumLabel" Enabled="false"   Text='<%# Eval("Curriculum") %>' Visible="true" runat="server"></asp:Label>
												</td>

												 <td class="center ">
													 <asp:Label ID="SchoolNameLabel" Enabled="false"   Text='<%# Eval("SchoolName") %>' Visible="true" runat="server"></asp:Label>
												</td>
											   
												 <td class="center ">
													 <asp:LinkButton ID="EditApplicationLinkButton" runat="server"   Text='<%$Resources:MOEHE.PSPES,EditApplication%>'></asp:LinkButton>
												</td>
												 <td class="center ">
												  <asp:LinkButton ID="CompleteApplicationLinkButton" runat="server" OnClick="CompleteApplicationLinkButton_Click" Text='<%$Resources:MOEHE.PSPES,CompleteApplication%>'></asp:LinkButton>
												</td>
											</tr>
										</ItemTemplate>
										<FooterTemplate></FooterTemplate>
										</asp:Repeater>
									</tbody>
								</table>
							</div>
						</div>
						<!-- END EXAMPLE TABLE PORTLET-->
   
					</section>
				</fieldset>
				
				<fieldset class="scheduler-border m-0 col-sm-12">
					<legend class="scheduler-border mb-0"><asp:Literal id="Literal4" runat="server" Text="<%$Resources:MOEHE.PSPES,applicantData%>"></asp:Literal></legend>
					<form class="p-15 mt-10" role="form">
						<div class="row m-0">
							<!--StudientName-->
							<div class="col-md-4">
								<div class="form-group">
									<label for="StudientName">
										<asp:Literal id="Literal7" runat="server" Text="<%$Resources:MOEHE.PSPES,studentName%>"></asp:Literal>
									</label>
									<asp:TextBox id = "TxtStudentNm" runat="server" class="aspNetDisabled form-control" disabled="disabled"></asp:TextBox>
								</div>
							</div>
							<!--PreEnrollmentSchool-->
							<div class="col-md-4">
								<div class="form-group">
									<label for="PreEnrollmentSchool">
										<asp:Literal id="Literal8" runat="server" Text="<%$Resources:MOEHE.PSPES,preEnrolScll%>"></asp:Literal>
									</label>
									<asp:TextBox id = "TxtPreEnScl" runat="server" class="aspNetDisabled form-control" disabled="disabled"></asp:TextBox>
								</div>
							</div>
							<!--Grade-->
							<div class="col-md-4">
								<div class="form-group">
									<label for="PreEnrollmentSchool">
										<asp:Literal id="Literal22" runat="server" Text="<%$Resources:MOEHE.PSPES,studentGrade%>"></asp:Literal>
									</label>
									<asp:TextBox id = "TxtPreEnGrade" runat="server" class="aspNetDisabled form-control" disabled="disabled"></asp:TextBox>
								</div>
							</div>
						</div>
						<div class="row m-0">
							<!--StudientGender-->
							<div class="col-md-3">
								<div class="form-group">
									<label for="StudientGender">
										<asp:Literal id="Literal9" runat="server" Text="<%$Resources:MOEHE.PSPES,studentGender%>"></asp:Literal>
									</label>
									<asp:TextBox id = "TxtGender" runat="server"  class="aspNetDisabled form-control" disabled="disabled"></asp:TextBox>
								</div>
							</div>
							<!--StudientNationality-->
							<div class="col-md-3">
								<div class="form-group">
									<label for="StudientNationality">
										<asp:Literal id="Literal10" runat="server" Text="<%$Resources:MOEHE.PSPES,studentNationality%>"></asp:Literal>
									</label>
									<asp:TextBox id = "TxtNationality" runat="server" class="aspNetDisabled form-control" disabled="disabled"></asp:TextBox>
								</div>
							</div>
							<!--StudientNationality-->
							<div class="col-md-3">
								<div class="form-group">
									<label for="StudientNationality">
										<asp:Literal id="Literal23" runat="server" Text="<%$Resources:MOEHE.PSPES,ApplicationStatus%>"></asp:Literal>
									</label>
									<asp:TextBox id = "TxtWListNum" runat="server" disabled="disabled" class="aspNetDisabled form-control"></asp:TextBox>
								</div>
							</div>
							<!--StudientNationality-->
							<div class="col-md-3">
								<div class="form-group">
									<label for="StudientNationality">
										<asp:Literal id="Literal24" runat="server" Text="<%$Resources:MOEHE.PSPES,ApplicationDate%>"></asp:Literal>
									</label>
								<asp:TextBox id = "TxtAppDT" runat="server" class="aspNetDisabled form-control" disabled="disabled"></asp:TextBox>
								</div>
							</div>
						</div>
					</form>
				</fieldset>
				<fieldset class="scheduler-border m-0 mb-10 col-sm-12">
					<div class="row m-0">
						<div class="form-group form-md-checkboxes col-md-12">
							<div class="md-checkbox-inline hidden">
							  <div class="md-checkbox">
								  <input type="checkbox" id="checkbox_1" runat="server" name="checkboxes1[]" value="1" class="md-check">
								  <label for="checkbox_1">
									<span class="inc"></span>
									<span class="check"></span>
									<span class="box"></span><asp:Literal id="Literal12" runat="server" Text="<%$Resources:MOEHE.PSPES,submitMissingdoc%>"></asp:Literal>  </label>
							  </div>
							  <div class="checkbox-inline">
								  <input type="checkbox" id="checkbox_2" runat="server" name="checkboxes2[]" value="1" class="md-check">
								  <label for="checkbox_2">
									<span class="inc"></span>
									<span class="check"></span>
									<span class="box"></span> <asp:Literal id="Literal13" runat="server" Text="<%$Resources:MOEHE.PSPES,test1%>"></asp:Literal>  </label>
							  </div>
							  <div class="checkbox-inline">
								<input type="checkbox" id="checkbox_3" runat="server" name="checkboxes3[]" value="1" class="md-check">
								<label for="checkbox_3">
									<span class="inc"></span>
									<span class="check"></span>
									<span class="box"></span> <asp:Literal id="Literal14" runat="server" Text="<%$Resources:MOEHE.PSPES,interview1%>"></asp:Literal>  </label>
							  </div>
							</div>
							<div class="col-md-4">
								<div class="checkbox-inline">
									<asp:CheckBox runat="server" ID="chkSupportingDocuments"  onclick="javascript:checkbox1Check();" CssClass="checkbox_1  m-0" Text='<%$Resources:MOEHE.PSPES,submitMissingdoc%>' />
								</div>
							</div>
							<div class="col-md-4">
								<div class="checkbox-inline">
									<asp:CheckBox runat="server" ID="chkTest" CssClass="checkbox_2  m-0"  onclick="javascript:checkbox2Check();" Text='<%$Resources:MOEHE.PSPES,test1%>' />
								</div>
							</div>
							<div class="col-md-4">
								<div class="checkbox-inline">
									 <asp:CheckBox runat="server" ID="chkInterview" CssClass="checkbox_3  m-0" onclick="javascript:checkbox3Check();" Text='<%$Resources:MOEHE.PSPES,interview1%>' />
								</div>
							</div>
						</div>  
					</div>
				</fieldset>
				  
				<%--MohammedAlhanafi 18-02-2019 supporitng document part--%>

				<fieldset class="checkbox1 scheduler-border m-0 col-sm-12">
					<legend class="scheduler-border mb-0">
						<asp:Literal id="SupportingDocumentsLiteral" runat="server" Text='<%$Resources:MOEHE.PSPES,SupportingDocuments%>'></asp:Literal>
					</legend>
					<%--GridView--%> 
					<div>
						<asp:GridView ID="gvRequiredDocuments" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" runat="server">
							<Columns>
								<asp:TemplateField>
									<HeaderTemplate><asp:Literal runat="server" ID="Literal69" Text='<%$Resources:MOEHE.PSPES,DocumentName%>'></asp:Literal></HeaderTemplate>
									<ItemTemplate>
										<asp:Label ID="lblRequiredDocumentName" CssClass="control-label ml-10 mb-5" runat="server"></asp:Label>
										<asp:Label ID="ArabicDocumentTypeLabel" runat="server" Visible='<%# Eval("ShowArabic") %>' Text='<%# Eval("ArabicDocumentType") %>' Enabled="false"></asp:Label>
										<asp:Label ID="EnglishDocumentTypeLabel" runat="server" Visible='<%# Eval("ShowEnglish") %>' Text='<%# Eval("EnglishDocumentType") %>' Enabled="false"></asp:Label>
										<asp:HiddenField ID="DocumentTypeIDHiddenField" Value='<%# Eval("DocumentTypeID") %>' runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField>
									<HeaderTemplate><asp:Literal runat="server" ID="Literal69" Text="<%$Resources:MOEHE.PSPES,View%>"></asp:Literal></HeaderTemplate>
									<ItemTemplate>
										<asp:HyperLink ID="ArabicDocumentTypeHyperLink" runat="server"  Visible='<%# Eval("IsUploadedArabic") %>' Text="عرض" NavigateUrl='<%# Eval("DoumentLocation") %>'></asp:HyperLink>
										<asp:HyperLink ID="EnglishDocumentTypeHyperLink" runat="server"  Visible='<%# Eval("IsUploadedEnglish") %>' Text="View" NavigateUrl='<%# Eval("DoumentLocation") %>'></asp:HyperLink>
									</ItemTemplate>
									</asp:TemplateField>
								<asp:TemplateField>
									<HeaderTemplate><asp:Literal runat="server" ID="Literal69" Text="<%$Resources:MOEHE.PSPES,File%>"></asp:Literal></HeaderTemplate>
									<ItemTemplate>
										<asp:FileUpload ID="fuRequiredDocument"  runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
							</Columns>
						</asp:GridView>
						<div class="row m-0">
							<div runat="server" id="dvNoDocuments"  Visible = "false">
								<div class="glyphicon glyphicon-exclamation-sign m-0"></div>
								<asp:Label ID="LblSupportingDocs" Visible = "false" runat="server" Text="<%$Resources:MOEHE.PSPES,NorequiredDocs%>"></asp:Label> 
							</div>

							<asp:Panel ID="PanelDocuments" runat="server">
								<div class="col-md-2 mt-10">
									<asp:LinkButton ID="SaveDocumentsLinkButton" class="btn btn-default" runat="server" OnClick="SaveDocumentsLinkButton_Click"><asp:Literal id="Literal40" runat="server" Text="<%$Resources:MOEHE.PSPES,Save%>"></asp:Literal></asp:LinkButton>
								</div>
							</asp:Panel>
						</div>
					</div>    
				</fieldset>
				<%--MohammedAlhanafi 18-02-2019 supporitng document part--%>
				<fieldset class="checkbox2 scheduler-border m-0 col-sm-12">
					<legend class="scheduler-border mb-0"><asp:Literal id="Literal15" runat="server" Text="<%$Resources:MOEHE.PSPES,CallforTest%>"></asp:Literal></legend>
					<div class="row m-0">
						<div class="form-group form-md-radios mb-10 ml-10">
							<div class="md-radio-inline">
								<!--CallTest-->
								<div class="md-radio">
									<input type="radio" id="radio6" name="radioTest" onclick="TestRadioCheck();"  class="md-radiobtn">
									<asp:HiddenField ID="InviationTestaRadioHiddenField" runat="server" />
									<label for="radio6">
										<span class="inc"></span>
										<span class="check"></span>
										<span class="box"></span><asp:Literal id="Literal5" runat="server" Text="<%$Resources:MOEHE.PSPES,CallforTest%>"></asp:Literal>
									</label>
								</div>
								<!--Rejected-->
								<div class="md-radio">
									<input type="radio" id="radio7" name="radioTest" onclick="TestRadioCheck();"  class="md-radiobtn" >
									<asp:HiddenField ID="RejecttionTestRadioHiddenField" runat="server" />
									<label for="radio7">
										<span class="inc"></span>
										<span class="check"></span>
										<span class="box"></span><asp:Literal id="Literal6" runat="server" Text="<%$Resources:MOEHE.PSPES,RejectedTest%>"></asp:Literal>
									</label>
								</div>
								<!--No action-->
								<div class="md-radio">
									<input type="radio" id="radio8" name="radioTest" onclick="TestRadioCheck();"  class="md-radiobtn" checked="">
									<asp:HiddenField ID="NoactionTestRadioHiddenField" runat="server" />
									<label for="radio8">
										<span class="inc"></span>
										<span class="check"></span>
										<span class="box"></span><asp:Literal id="Literal16" runat="server" Text="<%$Resources:MOEHE.PSPES,noActionTaken%>"></asp:Literal>
									</label>
								</div>
							</div>
						</div>
					</div>
					<div class="row m-0">
						<!--TimeTestReject-->
						<div class='col-md-4'>
							<div class="form-group">
								<label for="TimeTestReject"><asp:Literal id="Literal17" runat="server" Text="<%$Resources:MOEHE.PSPES,TestDate%>"></asp:Literal><span class="required"> * </span></label>
								<div id="filterDate2">
								  <!-- Datepicker as text field -->
								  <div class="input-group date" data-date-format="dd/mm/yyyy">
									<input type="text" class="form-control" name="TestDateTextBox" placeholder="dd/mm/yyyy">
									<div class="input-group-addon">
									  <span class="glyphicon glyphicon-calendar"></span>
									</div>
								  </div>
								</div>
							</div>
						</div>
						<script>
                            $(function () {
                                var date = new Date();
                                date.setDate(date.getDate());
                                $('.input-group.date').datepicker({
                                    format: "dd/mm/yyyy",
                                    startDate: date
                                });
                            });
						</script>
						<!--TimeTest-->
						<div class="col-md-4">
						  <div class="form-group">
							<label for="StudientCountry"><asp:Literal id="Literal20" runat="server" Text="<%$Resources:MOEHE.PSPES,TestTime%>"></asp:Literal><span class="required"> * </span></label>
							<input type="time" id="TestTimeText" name="TestTimeText" class="form-control">
						  </div>
						</div>
						<div class="col-md-2 pt-30 pl-15">
							<asp:LinkButton ID="SendTestInvatationLinkButton" OnClick="SendTestInvatationLinkButton_Click" class="btn btn-default" runat="server" ><i class="fa fa-paper-plane mr-5"></i><asp:Literal id="Literal518" runat="server" Text="<%$Resources:MOEHE.PSPES,SendInvitation%>"></asp:Literal> </asp:LinkButton>
						</div>
						<div class="col-md-2 pt-30 pl-15">
							<asp:LinkButton ID="ShowHideTestSMSHistoryLinkButton" OnClick="ShowHideTestSMSHistoryLinkButton_Click" class="btn btn-secondary" runat="server"><i class="fa fa-history mr-5"></i><asp:Literal id="Literal618" runat="server" Text="<%$Resources:MOEHE.PSPES,InterviwSMShistory%>"></asp:Literal> </asp:LinkButton>
						</div> 
					</div>
					<div class="row m-0 RejectionReasonTest">
						<div class="col-sm-12">
							<!--RejectionReason-->
							<div class="form-group">
							  <asp:Panel ID="TestRejectionReasonPanel" runat="server" Visible="true">
							  <label for="RejectionReason"><asp:Literal id="Literal18" runat="server" Text="<%$Resources:MOEHE.PSPES,rejectReason%>"></asp:Literal><span class="required"> * </span></label>
							  <textarea class="form-control textariaFReg" rows="3" id="TestRejectionReasonText"  name="TestRejectionReasonText" placeholder=""></textarea>
								  </asp:Panel>
							</div>
						</div>
					</div>
					<h4 class="line-bottom-theme-colored-2 mt-10 mb-10 ml-10 pb-0"><asp:Literal id="Literal111" runat="server" Text="<%$Resources:MOEHE.PSPES,TestResult%>"></asp:Literal></h4>
					<div class="row m-0">
					<%--<div class="alert alert-MsgWarning fade in m-0 mb-5 p-10">
								<div class="glyphicon glyphicon-exclamation-sign m-0"></div>
								<asp:Label ID="LbltestResult" Visible = "false" runat="server" Text="<%$Resources:MOEHE.PSPES,NoTestResults%>"></asp:Label> 
						</div>
					--%>
						<asp:Panel ID="PanelTestRslt" runat="server"> 
							<div class="col-sm-2">
								<div class="form-group">
									<!--TestResult-->
									<label for="TestResult"><asp:Literal id="Literal21" runat="server" Text="<%$Resources:MOEHE.PSPES,TestResult%>"></asp:Literal><span class="required"> * </span></label>
									<asp:DropDownList ID="DDLTestResult" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
								</div>
							</div>
							<div class="col-md-3">
								<!--UploadTestResult-->
								<label class="control-label ml-10 mb-5" for="UploadTestResult"><asp:Literal id="Literal25" runat="server" Text="<%$Resources:MOEHE.PSPES,UPLOADTestResult%>"></asp:Literal><span class="required"> * </span></label>
								<div class="form-group">
									<asp:FileUpload ID="FuploadTestReslt" runat="server" CssClass="form-control"/>
								  
								</div>
							</div>
							<div class="col-md-2 pt-30 pl-15">
								<asp:LinkButton ID="SendTestResultLinkButton" OnClick="SendTestResultLinkButton_Click" class="btn btn-default" runat="server"><i class="fa fa-paper-plane mr-5"></i><asp:Literal id="Literal26" runat="server" Text="<%$Resources:MOEHE.PSPES,SendResult%>"></asp:Literal></asp:LinkButton>
							</div>
						</asp:Panel>
						<asp:GridView ID="gvTestRst" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover">
							<Columns>
								<asp:TemplateField>
								  <HeaderTemplate>
									  <asp:Literal ID="Literal69" runat="server" Text="<%$Resources:MOEHE.PSPES,ResultBy%>"></asp:Literal>
								  </HeaderTemplate>
								  <ItemTemplate>
									  <asp:Label ID="lblBy" runat="server" CssClass="control-label ml-10 mb-5" ></asp:Label>
									  <asp:TextBox ID="TxttstResultBy" ReadOnly ="true" runat="server" Text='<%# Eval("USERID") %>'></asp:TextBox>
								  </ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField>
								  <HeaderTemplate>
									  <asp:Literal ID="Literal269" runat="server" Text="<%$Resources:MOEHE.PSPES,Date%>"></asp:Literal>
								  </HeaderTemplate>
								  <ItemTemplate>
									  <asp:Label ID="lblDTTest" runat="server" CssClass="control-label ml-10 mb-5" ></asp:Label>
									  <asp:TextBox ID="TxttstResultTime" runat="server" ReadOnly ="true" Text='<%# Eval("DTTM") %>'></asp:TextBox>
								  </ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField>
								  <HeaderTemplate>
									  <asp:Literal ID="Literal169" runat="server" Text="<%$Resources:MOEHE.PSPES,TestResult%>"></asp:Literal>
								  </HeaderTemplate>
								  <ItemTemplate>
									  <asp:Label ID="lblRequiredDocumentName" runat="server" CssClass="control-label ml-10 mb-5" ></asp:Label>
									  <asp:TextBox ID="TxttstResult" runat="server" ReadOnly ="true" Text='<%# Eval("TestResult1") %>'></asp:TextBox>
								  </ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField>
								  <HeaderTemplate>
									  <asp:Label ID="lbl1" runat="server" CssClass="control-label ml-10 mb-5" Text="<%$Resources:MOEHE.PSPES,View%>"></asp:Label>
								  </HeaderTemplate>
								  <ItemTemplate>
									  <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("ResultDocLocation") %>' Text="Download">Test Result Document</asp:HyperLink>
								  </ItemTemplate>
								</asp:TemplateField>
							</Columns>
						</asp:GridView>
					</div>
						<asp:GridView ID="TestResultSMSGridView" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" runat="server" Visible="False">
							  <Columns>

								  <asp:TemplateField>
									  <HeaderTemplate>
										  <asp:Literal runat="server" ID="Literal69" Text="<%$Resources:MOEHE.PSPES,MessageText%>"></asp:Literal></HeaderTemplate>

									  <ItemTemplate>
										  <asp:Label ID="MessageTextLabel" runat="server" Text='<%# Eval("MsgText") %>' Enabled="false"></asp:Label>


									  </ItemTemplate>

								  </asp:TemplateField>

								  <asp:TemplateField>
									  <HeaderTemplate>
										  <asp:Literal runat="server" ID="Literal69" Text="<%$Resources:MOEHE.PSPES,Number%>"></asp:Literal></HeaderTemplate>

									  <ItemTemplate>
										  <asp:Label ID="MessageTextLabel" runat="server" Text='<%# Eval("MobileNumber") %>' Enabled="false"></asp:Label>
									  </ItemTemplate>

								  </asp:TemplateField>

								  <asp:TemplateField>
									  <HeaderTemplate>
										  <asp:Literal runat="server" ID="Literal69" Text="<%$Resources:MOEHE.PSPES,Date%>"></asp:Literal></HeaderTemplate>

									  <ItemTemplate>
										  <asp:Label ID="StatusLabel" runat="server" Text='<%# Eval("MsgTime") %>' Enabled="false"></asp:Label>
									  </ItemTemplate>

								  </asp:TemplateField>

								  <asp:TemplateField>
									  <HeaderTemplate>
										  <asp:Literal runat="server" ID="Literal69" Text="<%$Resources:MOEHE.PSPES,Sender%>"></asp:Literal></HeaderTemplate>

									  <ItemTemplate>
										  <asp:Label ID="StatusLabel" runat="server" Text='<%# Eval("MsgSender") %>' Enabled="false"></asp:Label>
									  </ItemTemplate>

								  </asp:TemplateField>

								  <asp:TemplateField>
									  <HeaderTemplate>
										  <asp:Literal runat="server" ID="Literal69" Text="<%$Resources:MOEHE.PSPES,Status%>"></asp:Literal></HeaderTemplate>

									  <ItemTemplate>
										  <asp:Label ID="StatusLabel" runat="server" Text='<%# Eval("MsgStatus") %>' Enabled="false"></asp:Label>
									  </ItemTemplate>

								  </asp:TemplateField>

							  </Columns>
						</asp:GridView>

				</fieldset>
				<fieldset class="checkbox3 scheduler-border m-0 col-sm-12">
					<legend class="scheduler-border mb-0"><asp:Literal id="Literal27" runat="server" Text="<%$Resources:MOEHE.PSPES,interviewSection%>"></asp:Literal></legend>
					<div class="row m-0">
						<div class="form-group form-md-radios mb-20">
							<div class="md-radio-inline">
								<!--CallInterview-->
								<div class="md-radio">
									<input type="radio" id="radio1" onclick=" InterviewRadioCheck();"  name="radioInterview" class="md-radiobtn">
									 <asp:HiddenField ID="InterviewInvitationRadioHiddenField" runat="server" />
									<label for="radio1">
										<span class="inc"></span>
										<span class="check"></span>
										<span class="box"></span><asp:Literal id="Literal28" runat="server" Text="<%$Resources:MOEHE.PSPES,Callinterview%>"></asp:Literal></label>
								</div>
								<!--Rejected-->
								<div class="md-radio">
									<input type="radio" id="radio2" onclick=" InterviewRadioCheck();"  name="radioInterview" class="md-radiobtn">
									 <asp:HiddenField ID="InterviewRejectionRadioHiddenField" runat="server" />
									<label for="radio2">
										<span class="inc"></span>
										<span class="check"></span>
										<span class="box"></span><asp:Literal id="Literal29" runat="server" Text="<%$Resources:MOEHE.PSPES,Rejectinterview%>"></asp:Literal></label>
								</div>
								<!--Noaction-->
								<div class="md-radio">
									<input type="radio" id="radio3" onclick=" InterviewRadioCheck();"  name="radioInterview" class="md-radiobtn"  checked="">
									 <asp:HiddenField ID="InterviewNoactionRadioHiddenField" runat="server" />
									<label for="radio3">
										<span class="inc"></span>
										<span class="check"></span>
										<span class="box"></span><asp:Literal id="Literal30" runat="server" Text="<%$Resources:MOEHE.PSPES,Noactioninterview%>"></asp:Literal></label>
								</div>
							</div>
						</div>
					</div>
					<div class="row m-0">
						<!--TimeTestReject2-->
						<div class='col-sm-4'>
							<div class="form-group">
								<label for="TimeTestReject"><asp:Literal id="Literal31" runat="server" Text="<%$Resources:MOEHE.PSPES,interviewDate%>"></asp:Literal>  <span class="required"> * </span></label>
								<div id="TimeTestReject2">
									<!-- Datepicker as text field -->
									<div class="input-group date" data-date-format="dd/mm/yyyy">
										<input type="text" class="form-control" name="InterviewDateText" placeholder="dd/mm/yyyy">
										<div class="input-group-addon">
											<span class="glyphicon glyphicon-calendar"></span>
										</div>
									</div>
								</div>
							</div>
						</div>
						<script>
                            $(function () {
                                var date = new Date();
                                date.setDate(date.getDate());
                                $('.input-group.date').datepicker({
                                    format: "dd/mm/yyyy",
                                    startDate: date
                                });
                            });
						</script>
						<!--TimeTest-->
						<div class="col-sm-4">
							<div class="form-group">
								<label for="StudientCountry"><asp:Literal id="Literal32" runat="server" Text="<%$Resources:MOEHE.PSPES,interviewTime%>"></asp:Literal><span class="required"> * </span></label>
								<input type="time" id="StudientCountry" name="InterviewTimeText" class="form-control">
							</div>
						</div>

						<div class="col-sm-2 col-md-2 pt-30 pl-15">
							<asp:LinkButton ID="SendInterviewSMSLinkButton1"  class="btn btn-default"  OnClick="SendInterviewSMSLinkButton1_Click" runat="server"><i class="fa fa-paper-plane mr-5"></i><asp:Literal id="Literal33" runat="server" Text="<%$Resources:MOEHE.PSPES,SendInvitation%>"></asp:Literal></asp:LinkButton>
						</div>
						<div class="col-sm-2 col-md-2 pt-30 pl-15">
							<asp:LinkButton ID="ShowHideInterviewSMSHistoryLinkButton" OnClick="ShowHideInterviewSMSHistoryLinkButton_Click" class="btn btn-secondary" runat="server"><i class="fa fa-history mr-5"></i><asp:Literal id="Literal34" runat="server" Text="<%$Resources:MOEHE.PSPES,InterviwSMShistory%>"></asp:Literal></asp:LinkButton>
						</div>
						<div class="row m-0">
							<div class="col-sm-12">
								<!--RejectionReason-->
								<div class="form-group RejectionReasonInterview">
									<asp:Panel ID="InterviewRejectionPanel" runat="server" Visible="true">
										<label for="RejectionReason"><asp:Literal id="Literal35" runat="server" Text="<%$Resources:MOEHE.PSPES,InterviwRejectReason%>"></asp:Literal><span class="required"> * </span></label>
										<textarea class="form-control textariaFReg" rows="3" id="RejectionReason" name="InterviewRejectionReasonText" placeholder=""></textarea>
									</asp:Panel>
								</div>
							</div>
						</div>
						<h4 class="line-bottom-theme-colored-2 mt-10 mb-10 ml-10 pb-0"><asp:Literal id="Literal36" runat="server" Text="<%$Resources:MOEHE.PSPES,Interviwresult%>"></asp:Literal></h4>
							<div class="row m-0">
							<%-- <div class="alert alert-MsgWarning fade in m-0 mb-5 p-10">
									<div class="glyphicon glyphicon-exclamation-sign m-0"></div>
									<asp:Label ID="LblInterviewresult" Visible = "false" runat="server" Text="No Interview result exists for this application "></asp:Label>
								</div>
							</div>
							--%>
							<asp:Panel ID="PanelInterviewrslt" runat="server">
							   <div class="col-sm-2">
									<div class="form-group">
										<!--TestResult-->
										<label for="TestResult"><asp:Literal id="Literal37" runat="server" Text="<%$Resources:MOEHE.PSPES,Interviwresult%>"></asp:Literal><span class="required"> * </span></label>
										<asp:DropDownList ID="DDLinterviewReslt" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>
									   
									</div>
								</div>
								<script>
                                    $(function () {
                                        var date = new Date();
                                        date.setDate(date.getDate());
                                        $('.input-group.date').datepicker({
                                            format: "dd/mm/yyyy",
                                            startDate: date
                                        });
                                    });
								</script>
								<div class="col-sm-2 col-md-2 pt-30 pl-15">
									<asp:LinkButton ID="SendInterviewResultLinkButton1"  class="btn btn-default"  OnClick="SendInterviewResultLinkButton1_Click" runat="server"><i class="fa fa-paper-plane mr-5"></i><asp:Literal id="Literal38" runat="server" Text="<%$Resources:MOEHE.PSPES,SendResult%>"></asp:Literal></asp:LinkButton>

								</div>
							</asp:Panel>
							<asp:GridView ID="GVinterviewResult" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover">
								<Columns>
								  <asp:TemplateField>
									  <HeaderTemplate>
										  <asp:Literal ID="Literal469" runat="server" Text="<%$Resources:MOEHE.PSPES,InterviewResultBy%>"></asp:Literal>
									  </HeaderTemplate>
									  <ItemTemplate>
										  <asp:Label ID="lblRequiredDocumentName" runat="server" CssClass="control-label ml-10 mb-5" ></asp:Label>
										  <asp:TextBox ID="TxtIntResultBy" runat="server" Text='<%# Eval("USERID") %>' ReadOnly ="true"></asp:TextBox>
									  </ItemTemplate>
								  </asp:TemplateField>
								   <asp:TemplateField>
									  <HeaderTemplate>
										  <asp:Literal ID="Literal461" runat="server" Text="<%$Resources:MOEHE.PSPES,Date%>"></asp:Literal>
									  </HeaderTemplate>
									  <ItemTemplate>
										  <asp:Label ID="lblDateInterview" runat="server" CssClass="control-label ml-10 mb-5" ></asp:Label>
										  <asp:TextBox ID="TxtIntResultDT" runat="server" ReadOnly ="true" Text='<%# Eval("DTTM") %>'></asp:TextBox>
									  </ItemTemplate>
								  </asp:TemplateField>
								  <asp:TemplateField>
									  <HeaderTemplate>
										  <asp:Label ID="lbl1" runat="server" CssClass="control-label ml-10 mb-5" Text="<%$Resources:MOEHE.PSPES,View%>"></asp:Label>
									  </HeaderTemplate>
									  <ItemTemplate>
										  <asp:TextBox ID="TxtIntResult" runat="server" ReadOnly ="true" Text='<%# Eval("InterviewResult1") %>'></asp:TextBox>
									  </ItemTemplate>
								  </asp:TemplateField>
								</Columns>
							</asp:GridView>
						</div>
						<asp:GridView ID="InterviewSMSHistoryGridView" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" runat="server" Visible="False">
							<Columns>

							  <asp:TemplateField>
								  <HeaderTemplate>
									  <asp:Literal runat="server" ID="Literal69" Text="<%$Resources:MOEHE.PSPES,MessageText%>"></asp:Literal></HeaderTemplate>

								  <ItemTemplate>
									  <asp:Label ID="MessageTextLabel" runat="server" Text='<%# Eval("MsgText") %>' Enabled="false"></asp:Label>


								  </ItemTemplate>

							  </asp:TemplateField>

							  <asp:TemplateField>
								  <HeaderTemplate>
									  <asp:Literal runat="server" ID="Literal69" Text="<%$Resources:MOEHE.PSPES,Number%>"></asp:Literal></HeaderTemplate>

								  <ItemTemplate>
									  <asp:Label ID="MessageTextLabel" runat="server" Text='<%# Eval("MobileNumber") %>' Enabled="false"></asp:Label>
								  </ItemTemplate>

							  </asp:TemplateField>

							  <asp:TemplateField>
								  <HeaderTemplate>
									  <asp:Literal runat="server" ID="Literal69" Text="<%$Resources:MOEHE.PSPES,Date%>"></asp:Literal></HeaderTemplate>

								  <ItemTemplate>
									  <asp:Label ID="StatusLabel" runat="server" Text='<%# Eval("MsgTime") %>' Enabled="false"></asp:Label>
								  </ItemTemplate>

							  </asp:TemplateField>

							  <asp:TemplateField>
								  <HeaderTemplate>
									  <asp:Literal runat="server" ID="Literal69" Text="<%$Resources:MOEHE.PSPES,Sender%>"></asp:Literal></HeaderTemplate>

								  <ItemTemplate>
									  <asp:Label ID="StatusLabel" runat="server" Text='<%# Eval("MsgSender") %>' Enabled="false"></asp:Label>
								  </ItemTemplate>

							  </asp:TemplateField>

							  <asp:TemplateField>
								  <HeaderTemplate>
									  <asp:Literal runat="server" ID="Literal69" Text="<%$Resources:MOEHE.PSPES,Status%>"></asp:Literal></HeaderTemplate>

								  <ItemTemplate>
									  <asp:Label ID="StatusLabel" runat="server" Text='<%# Eval("MsgStatus") %>' Enabled="false"></asp:Label>
								  </ItemTemplate>

							  </asp:TemplateField>

							</Columns>
						</asp:GridView>
					</fieldset>
						<%--MohammedAlhanafi 19-02-2019 pay fees part--%>
						<fieldset class="scheduler-border m-0 col-sm-12">
							<legend class="scheduler-border mb-0"><asp:Literal id="Literal42" runat="server" Text="<%$Resources:MOEHE.PSPES,CallseatReservation%>"></asp:Literal></legend>
							<div class="row m-0">
								<div class="form-group form-md-radios mb-10">
									<div class="md-radio-inline">
										<!--CallInterview-->
										<div class="md-radio">
										  <input type="radio" id="radio10" onclick="PayFeesRadioCheck();"  name="radioAgree" class="md-radiobtn">
										  <label for="radio10">
											  <span class="inc"></span>
											  <span class="check"></span>
											  <span class="box"></span><asp:Literal id="Literal43" runat="server" Text="<%$Resources:MOEHE.PSPES,noActionTaken%>"></asp:Literal></label>
											<asp:HiddenField ID="NoActionTakenRadioHiddenField" runat="server" />
										</div>
										<!--Rejected-->
										<div class="md-radio">
										  <input type="radio" id="radio11" name="radioAgree"  onclick="PayFeesRadioCheck();" class="md-radiobtn">
										  <label for="radio11">
											  <span class="inc"></span>
											  <span class="check"></span>
											  <span class="box"></span><asp:Literal id="Literal44" runat="server" Text="<%$Resources:MOEHE.PSPES,Callforpayment%>"></asp:Literal></label>
											<asp:HiddenField ID="PayFeesRequestRadioHiddenField" runat="server" />

										</div>
									</div>
									<div class="row m-0">
									  <!--TimePay-->
									   <div class="col-sm-4">
										  <div class="form-groups">
											<label for="ReservationFees"> <asp:Literal id="Literal45" runat="server" Text="<%$Resources:MOEHE.PSPES,reservationFees%>"></asp:Literal><span class="required"> * </span></label>
											<input type="number" id="ReservationFeesTextBoxForSMS" class="form-control" name="ReservationFeesTextBoxForSMS">
										  </div>
										</div>
										<div class='col-sm-4'>
											<div class="form-groups">
											  <label for="TimeTestReject"><asp:Literal id="Literal46" runat="server" Text="<%$Resources:MOEHE.PSPES,feesPaidDate%>"></asp:Literal><span class="required"> * </span></label>
											  <div id="TimeTestReject2">
												<!-- Datepicker as text field -->
												<div class="input-group date" data-date-format="dd/mm/yyyy">
												  <input type="text" class="form-control" name="PayFeesOnDateTime" placeholder="dd/mm/yyyy">
												  <div class="input-group-addon">
													<span class="glyphicon glyphicon-calendar"></span>
												  </div>
												</div>
											  </div>
											</div>
										</div>
										<script>
                                            $(function () {
                                                var date = new Date();
                                                date.setDate(date.getDate());
                                                $('.input-group.date').datepicker({
                                                    format: "dd/mm/yyyy",
                                                    startDate: date
                                                });
                                            });
										</script>
										<div class="col-md-2 pt-30 pl-15">
											<asp:LinkButton ID="PayFeesSMSLinkButton" OnClick="PayFeesSMSLinkButton_Click" class="btn btn-default" runat="server"><i class="fa fa-paper-plane mr-5"></i><asp:Literal id="Literal96" runat="server" Text="<%$Resources:MOEHE.PSPES,sendreservpaymentSMS%>"></asp:Literal> </asp:LinkButton>
										</div>
										<div class="col-md-2 pt-30 pl-15">
												<asp:LinkButton ID="ViewPayFeesSMSLinkButton1" OnClick="ViewPayFeesSMSLinkButton1_Click" class="btn btn-secondary" runat="server"><i class="fa fa-history mr-5"></i><asp:Literal id="Literal97" runat="server" Text="<%$Resources:MOEHE.PSPES,showSMSHistory%>"></asp:Literal> </asp:LinkButton>
										</div>
									</div>
									<asp:GridView ID="PayFessMessageHistoryGridView" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" runat="server" Visible="False">
													   
											<Columns>
											   
														 <asp:TemplateField>
													<HeaderTemplate><asp:Literal runat="server" ID="Literal69" Text="<%$Resources:MOEHE.PSPES,MessageText%>"></asp:Literal></HeaderTemplate>

													<ItemTemplate>
														 <asp:Label ID="MessageTextLabel" runat="server" Text='<%# Eval("MsgText") %>' Enabled="false"></asp:Label>
													 
													   
													</ItemTemplate>

												</asp:TemplateField>

												<asp:TemplateField>
													<HeaderTemplate><asp:Literal runat="server" ID="Literal50" Text="<%$Resources:MOEHE.PSPES,Number%>"></asp:Literal></HeaderTemplate>

													<ItemTemplate>
														 <asp:Label ID="Label1" runat="server" Text='<%# Eval("MobileNumber") %>' Enabled="false"></asp:Label>
													</ItemTemplate>

												</asp:TemplateField>
												
												   <asp:TemplateField>
													<HeaderTemplate><asp:Literal runat="server" ID="Literal51" Text="<%$Resources:MOEHE.PSPES,Date%>"></asp:Literal></HeaderTemplate>

													<ItemTemplate>
														 <asp:Label ID="StatusLabel" runat="server" Text='<%# Eval("MsgTime") %>' Enabled="false"></asp:Label>
													</ItemTemplate>

												</asp:TemplateField>

																		 <asp:TemplateField>
													<HeaderTemplate><asp:Literal runat="server" ID="Literal52" Text="<%$Resources:MOEHE.PSPES,Sender%>"></asp:Literal></HeaderTemplate>

													<ItemTemplate>
														 <asp:Label ID="Label2" runat="server" Text='<%# Eval("MsgSender") %>' Enabled="false"></asp:Label>
													</ItemTemplate>

												</asp:TemplateField>

												   <asp:TemplateField>
													<HeaderTemplate><asp:Literal runat="server" ID="Literal53" Text="<%$Resources:MOEHE.PSPES,Status%>"></asp:Literal></HeaderTemplate>

													<ItemTemplate>
														 <asp:Label ID="Label3" runat="server" Text='<%# Eval("MsgStatus") %>' Enabled="false"></asp:Label>
													</ItemTemplate>

												</asp:TemplateField>

											</Columns>
									</asp:GridView>
								</div>
							</div>
						</fieldset>
						<fieldset class="scheduler-border m-0 col-sm-12">
							<legend class="scheduler-border mb-0"><asp:Literal id="Literal47" runat="server" Text="<%$Resources:MOEHE.PSPES,feesInfo%>"></asp:Literal></legend>
							<div class="row m-0">
								<!--ReservationFees-->
								<div class="col-sm-4">
								  <div class="form-group">
									<label for="ReservationFees"><asp:Literal id="Literal48" runat="server" Text="<%$Resources:MOEHE.PSPES,reservationFees%>"></asp:Literal> <span class="required"> * </span></label>
									<asp:TextBox ID="PaidFeesTextBox" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
								  </div>
								</div>
								<div class="col-sm-4">
								  <div class="form-group">
									<label for="TimeTestReject"><asp:Literal id="Literal49" runat="server" Text="<%$Resources:MOEHE.PSPES,feesPaidDate%>"></asp:Literal><span class="required"> * </span></label>
									<div id="TimeTestReject2">
									  <!-- Datepicker as text field -->
										<div class="input-group date" data-date-format="dd/mm/yyyy">
											<input type="text" class="form-control" name="PaidFeesDateTime" placeholder="dd/mm/yyyy">
											<div class="input-group-addon">
											  <span class="glyphicon glyphicon-calendar"></span>
											</div>
										</div>
									</div>
								  </div>
								</div>
								<script>
                                          $(function () {
                                              var date = new Date();
                                              date.setDate(date.getDate());
                                              $('.input-group.date').datepicker({
                                                  format: "dd/mm/yyyy",
                                                  startDate: date
                                              });
                                          });
								</script>
								<div class="col-md-2 pt-30 pl-15">
									<asp:LinkButton ID="CLinkButton" OnClick="FeesPaidConfirmationLinkButton_Click" class="btn btn-default" runat="server" ><i class="fa fa-paper-plane mr-5"></i><asp:Literal id="Literal98" runat="server" Text="<%$Resources:MOEHE.PSPES,confirmreservComplete%>"></asp:Literal></asp:LinkButton>
								</div>
								<div class="col-md-2 pt-30 pl-15">
									<asp:LinkButton ID="ViewFeesPaidConfirmationLLinkButton" OnClick="ViewFeesPaidConfirmationLLinkButton_Click" class="btn btn-secondary" runat="server"><i class="fa fa-history mr-5"></i><asp:Literal id="Literal99" runat="server" Text="<%$Resources:MOEHE.PSPES,showSMSHistory%>"></asp:Literal></asp:LinkButton>
								</div>
								<asp:GridView ID="PayFessConfirmationMessageHistoryGridView" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" runat="server" Visible="False">
									<Columns>
									   
												 <asp:TemplateField>
											<HeaderTemplate><asp:Literal runat="server" ID="Literal69" Text="<%$Resources:MOEHE.PSPES,MessageText%>"></asp:Literal></HeaderTemplate>

											<ItemTemplate>
												 <asp:Label ID="MessageTextLabel" runat="server" Text='<%# Eval("MsgText") %>' Enabled="false"></asp:Label>
											 
											   
											</ItemTemplate>

										</asp:TemplateField>

										<asp:TemplateField>
											<HeaderTemplate><asp:Literal runat="server" ID="Literal69" Text="<%$Resources:MOEHE.PSPES,Number%>"></asp:Literal></HeaderTemplate>

											<ItemTemplate>
												 <asp:Label ID="MessageTextLabel" runat="server" Text='<%# Eval("MobileNumber") %>' Enabled="false"></asp:Label>
											</ItemTemplate>

										</asp:TemplateField>
										
										   <asp:TemplateField>
											<HeaderTemplate><asp:Literal runat="server" ID="Literal69" Text="<%$Resources:MOEHE.PSPES,Date%>"></asp:Literal></HeaderTemplate>

											<ItemTemplate>
												 <asp:Label ID="StatusLabel" runat="server" Text='<%# Eval("MsgTime") %>' Enabled="false"></asp:Label>
											</ItemTemplate>

										</asp:TemplateField>

																 <asp:TemplateField>
											<HeaderTemplate><asp:Literal runat="server" ID="Literal69" Text="<%$Resources:MOEHE.PSPES,Sender%>"></asp:Literal></HeaderTemplate>

											<ItemTemplate>
												 <asp:Label ID="StatusLabel" runat="server" Text='<%# Eval("MsgSender") %>' Enabled="false"></asp:Label>
											</ItemTemplate>

										</asp:TemplateField>

										   <asp:TemplateField>
											<HeaderTemplate><asp:Literal runat="server" ID="Literal69" Text="<%$Resources:MOEHE.PSPES,Status%>"></asp:Literal></HeaderTemplate>

											<ItemTemplate>
												 <asp:Label ID="StatusLabel" runat="server" Text='<%# Eval("MsgStatus") %>' Enabled="false"></asp:Label>
											</ItemTemplate>

										</asp:TemplateField>

									</Columns>
								</asp:GridView>
							</div>
						</fieldset>
						<%--MohammedAlhanafi 19-02-2019 pay fees part--%>
					</div>
				</div>
			</div>
			<!-- END EXAMPLE TABLE PORTLET-->
		</div>
	</div>
</section>



<script>

                                    window.onload = function () {
                                        if (document.getElementById('<%=NoActionTakenRadioHiddenField.ClientID %>').value == "true") {
                                                document.getElementById('radio10').checked = true;

                                            }
                                            else if (document.getElementById('<%=PayFeesRequestRadioHiddenField.ClientID %>').value == "true") {
                                                document.getElementById('radio11').checked = true;

                                            }



                                            if (document.getElementById('<%=NoactionTestRadioHiddenField.ClientID %>').value == "true") {
                                                document.getElementById('radio8').checked = true;

                                            }
                                            else if (document.getElementById('<%=InviationTestaRadioHiddenField.ClientID %>').value == "true") {
                                          document.getElementById('radio6').checked = true;

                                      }
                                      else if (document.getElementById('<%=RejecttionTestRadioHiddenField.ClientID %>').value == "true") {
                                                document.getElementById('radio7').checked = true;

                                            }

                                            if (document.getElementById('<%=InterviewInvitationRadioHiddenField.ClientID %>').value == "true") {
                                          document.getElementById('radio1').checked = true;

                                      }
                                      else if (document.getElementById('<%=InterviewNoactionRadioHiddenField.ClientID %>').value == "true") {
                                          document.getElementById('radio3').checked = true;

                                      }
                                      else if (document.getElementById('<%=InterviewRejectionRadioHiddenField.ClientID %>').value == "true") {
                                          document.getElementById('radio2').checked = true;

                                      }





                                    };
                                    function send() {
                                        var genders = document.getElementsByName("gender");
                                        if (genders[0].checked == true) {
                                            alert("Your gender is male");
                                        } else if (genders[1].checked == true) {
                                            alert("Your gender is female");
                                        } else {
                                            // no checked
                                            var msg = '<span style="color:red;">You must select your gender!</span><br /><br />';
                                            document.getElementById('msg').innerHTML = msg;
                                            return false;
                                        }
                                        return true;
                                    }

                                    function PayFeesRadioCheck() {
                                        if (document.getElementById('radio10').checked) {
                                            // NoActionTakenRadioHiddenField checked
                                            document.getElementById('<%=NoActionTakenRadioHiddenField.ClientID %>').value = "true";
                                                document.getElementById('<%=PayFeesRequestRadioHiddenField.ClientID %>').value = "false";



                                            } else if (document.getElementById('radio11').checked) {
                                                document.getElementById('<%=NoActionTakenRadioHiddenField.ClientID %>').value = "false";
                                                document.getElementById('<%=PayFeesRequestRadioHiddenField.ClientID %>').value = "true";




                                            }



                                        }


                                        function TestRadioCheck() {
                                            if (document.getElementById('radio6').checked) {
                                                // NoActionTakenRadioHiddenField checked
                                                document.getElementById('<%=InviationTestaRadioHiddenField.ClientID %>').value = "true";
                                          document.getElementById('<%=RejecttionTestRadioHiddenField.ClientID %>').value = "false";
                                          document.getElementById('<%=NoactionTestRadioHiddenField.ClientID %>').value = "false";



                                      } else if (document.getElementById('radio7').checked) {
                                          document.getElementById('<%=InviationTestaRadioHiddenField.ClientID %>').value = "false";
                                          document.getElementById('<%=RejecttionTestRadioHiddenField.ClientID %>').value = "true";
                                          document.getElementById('<%=NoactionTestRadioHiddenField.ClientID %>').value = "false";




                                      }
                                      else if (document.getElementById('radio8').checked) {
                                          document.getElementById('<%=InviationTestaRadioHiddenField.ClientID %>').value = "false";
                                          document.getElementById('<%=RejecttionTestRadioHiddenField.ClientID %>').value = "false";
                                          document.getElementById('<%=NoactionTestRadioHiddenField.ClientID %>').value = "true";




                                            }



                                        }



                                        function InterviewRadioCheck() {
                                            if (document.getElementById('radio3').checked) {
                                                // NoActionTakenRadioHiddenField checked
                                                document.getElementById('<%=InterviewInvitationRadioHiddenField.ClientID %>').value = "false";
                                          document.getElementById('<%=InterviewRejectionRadioHiddenField.ClientID %>').value = "false";
                                          document.getElementById('<%=InterviewNoactionRadioHiddenField.ClientID %>').value = "true";



                                      } else if (document.getElementById('radio1').checked) {
                                          document.getElementById('<%=InterviewInvitationRadioHiddenField.ClientID %>').value = "true";
                                          document.getElementById('<%=InterviewRejectionRadioHiddenField.ClientID %>').value = "false";
                                          document.getElementById('<%=InterviewNoactionRadioHiddenField.ClientID %>').value = "false";




                                      }
                                      else if (document.getElementById('radio2').checked) {
                                          document.getElementById('<%=InterviewInvitationRadioHiddenField.ClientID %>').value = "false";
                                          document.getElementById('<%=InterviewRejectionRadioHiddenField.ClientID %>').value = "true";
                                          document.getElementById('<%=InterviewNoactionRadioHiddenField.ClientID %>').value = "false";




                                      }



                                  }

</script>
<script>
                                  $(document).ready(function () {
                                      $('#radio1').change(function () {
                                          if (this.checked)
                                              $('.RejectionReasonInterview').fadeOut('slow');

                                      });
                                      $('#radio2').change(function () {
                                          if (this.checked)
                                              $('.RejectionReasonInterview').fadeIn('slow');
                                          else
                                              $('.RejectionReasonInterview').fadeOut('slow');

                                      });
                                      $('#radio3').change(function () {
                                          if (this.checked)
                                              $('.RejectionReasonInterview').fadeOut('slow');

                                      });
                                      $('#radio6').change(function () {
                                          if (this.checked)
                                              $('.RejectionReasonTest').fadeOut('slow');

                                      });
                                      $('#radio7').change(function () {
                                          if (this.checked)
                                              $('.RejectionReasonTest').fadeIn('slow');
                                          else
                                              $('.RejectionReasonTest').fadeOut('slow');

                                      });
                                      $('#radio8').change(function () {
                                          if (this.checked)
                                              $('.RejectionReasonTest').fadeOut('slow');

                                      });
                                      $('.checkbox_2').change(function () {
                                          if (document.getElementById('<%=chkTest.ClientID%>').checked)
                                              $('.checkbox2').fadeIn('slow');
                                          else
                                              $('.checkbox2').fadeOut('slow');

                                      });
                                      $('.checkbox_3').change(function () {
                                          if (document.getElementById('<%=chkInterview.ClientID%>').checked)
                                              $('.checkbox3').fadeIn('slow');
                                          else
                                              $('.checkbox3').fadeOut('slow');

                                      });

                                      $('.checkbox_1').change(function () {
                                          if (document.getElementById('<%=chkSupportingDocuments.ClientID%>').checked)
                                              $('.checkbox1').fadeIn('slow');
                                          else
                                              $('.checkbox1').fadeOut('slow');

                                      });
                                  });
                                  $(window).load(function () {
                                      // chekbox
                                      if (document.getElementById('<%=chkSupportingDocuments.ClientID%>').checked)
                                          $('.checkbox1').fadeIn('slow');
                                      else
                                          $('.checkbox1').fadeOut('slow');

                                      if (document.getElementById('<%=chkTest.ClientID%>').checked)
                                          $('.checkbox2').fadeIn('slow');
                                      else
                                          $('.checkbox2').fadeOut('slow');

                                      if (document.getElementById('<%=chkInterview.ClientID%>').checked)
                                          $('.checkbox3').fadeIn('slow');
                                      else
                                          $('.checkbox3').fadeOut('slow');
                                      // radio…
                                      if ($('#radio1').is(':checked')) {
                                          $('.RejectionReasonInterview').fadeOut('slow');
                                      }

                                      if ($('#radio2').is(':checked')) {
                                          $('.RejectionReasonInterview').fadeIn('slow');
                                      }
                                      else {
                                          $('.RejectionReasonInterview').fadeOut('slow');
                                      }
                                      if ($('#radio3').is(':checked')) {
                                          $('.RejectionReasonInterview').fadeOut('slow');
                                      }
                                      if ($('#radio6').is(':checked')) {
                                          $('.RejectionReasonTest').fadeOut('slow');
                                      }

                                      if ($('#radio7').is(':checked')) {
                                          $('.RejectionReasonTest').fadeIn('slow');
                                      }
                                      else {
                                          $('.RejectionReasonTest').fadeOut('slow');
                                      }
                                      if ($('#radio8').is(':checked')) {
                                          $('.RejectionReasonTest').fadeOut('slow');
                                      }
                                  });




</script>




