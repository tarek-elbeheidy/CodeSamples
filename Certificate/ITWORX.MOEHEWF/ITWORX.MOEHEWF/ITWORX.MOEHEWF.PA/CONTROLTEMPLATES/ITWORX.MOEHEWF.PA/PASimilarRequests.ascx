<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PASimilarRequests.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.SimilarRequests" %>

<%--<script type="text/javascript" src='<%= ResolveUrl ("~/Style%20Library/Scripts/jquery.min.js") %>'></script>
 <link rel="stylesheet" href='<%= ResolveUrl ("~/Style%20Library/CSS/jquery-ui.css") %>'>
<script type="text/javascript" src='<%= ResolveUrl ("~/Style%20Library/Scripts/jquery-1.12.4.js") %>'></script>
<script type="text/javascript" src='<%= ResolveUrl ("~/Style%20Library/Scripts/jquery-ui.js") %>'></script>--%>

<div id="SrchControls" runat="server" class="row heighlighted-section margin-bottom-50  edit-mode margin-0">

	<div class=" col-md-12 col-sm-12 col-xs-12  no-padding ">
		<div class="data-container table-display moe-width-85  moe-sm-full-width">
			<div class="form-group">
				<h6 class="font-size-16 margin-bottom-0 margin-top-0 ">
					<asp:Label ID="lbl_AcademicDegree" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, AcademicDegree %>"></asp:Label>
				</h6>

				<div class="form">
					<asp:DropDownList ID="drp_AcademicDegree" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
				</div>
			</div>
		</div>
	</div>



	<div class="col-md-4 col-sm-12 col-xs-12  no-padding ">
		<div class="data-container table-display moe-width-85  moe-sm-full-width">
			<div class="form-group">
				<h6 class="font-size-16 margin-bottom-0 margin-top-0 ">
					<asp:Label ID="lbl_DateFrom" runat="server" Text=" <%$Resources:ITWORX_MOEHEWF_UCE, SubmitDateFrom %> "></asp:Label>
				</h6>

				<div class="form">
					<input type="text" id="dt_DateFrom" readonly="readonly" class="moe-full-width moe-input-padding moe-select input-height-42">
					<asp:HiddenField ID="hdn_DateFrom" runat="server" ClientIDMode="Static" />
				</div>
			</div>
		</div>
	</div>
	<div class="col-md-4 col-sm-12 col-xs-12  no-padding ">
		<div class="data-container table-display moe-width-85  moe-sm-full-width">
			<div class="form-group">
				<h6 class="font-size-16 margin-bottom-0 margin-top-0 ">
					<asp:Label ID="lbl_DateTo" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, SubmitDateTo %> "></asp:Label>
				</h6>

				<div class="form">
					<input type="text" id="dt_DateTo" readonly="readonly" class="moe-full-width moe-input-padding moe-select input-height-42">

					<asp:HiddenField ID="hdn_DateTo" runat="server" ClientIDMode="Static" />
				</div>
			</div>
		</div>
	</div>
	<div class=" col-md-4 col-sm-12 col-xs-12  no-padding ">
		<div class="data-container table-display moe-width-85  moe-sm-full-width">
			<div class="form-group">
				<h6 class="font-size-16 margin-bottom-0 margin-top-0 ">
					<asp:Label ID="lbl_EntityNeedsEquivalency" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EntityNeedsEquivalency %>"></asp:Label>
				</h6>

				<div class="form">
					<asp:DropDownList ID="drp_EntityNeedsEquivalency" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
				</div>
			</div>
		</div>
	</div>
	<div class="col-md-4 col-sm-12 col-xs-12   no-padding ">
		<div class="data-container table-display moe-width-85  moe-sm-full-width">
			<div class="form-group">
				<h6 class="font-size-16 margin-bottom-0 margin-top-0 ">
					<asp:Label ID="lbl_Country" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Country %>"></asp:Label>
				</h6>

				<div class="form">
					<asp:DropDownList ID="drp_Country" runat="server" OnSelectedIndexChanged="drp_Country_SelectedIndexChanged" AutoPostBack="true" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
				</div>
			</div>
		</div>
	</div>
</div>


<div class="row no-padding edit-mode unheighlighted-section.row">
	<div class="col-md-4 col-sm-12 col-xs-12 no-padding ">
		<div class="data-container table-display moe-width-85  moe-sm-full-width">
			<div class="form-group">
				<h6 class="font-size-16 margin-bottom-0 margin-top-0 ">
					<asp:Label ID="lbl_StudyLanguage" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, StudyLanguage %>"></asp:Label>
				</h6>
				<div class="form">
					<asp:DropDownList ID="drp_StudyLanguage" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
				</div>
			</div>
		</div>
	</div>
	<div class="col-md-4 col-sm-12 col-xs-12 no-padding ">
		<div class="data-container table-display moe-width-85  moe-sm-full-width">
			<div class="form-group">
				<h6 class="font-size-16 margin-bottom-0 margin-top-0 ">
					<asp:Label ID="lbl_StudyType" runat="server" Text=" <%$Resources:ITWORX_MOEHEWF_UCE, StudyType %>"></asp:Label>
				</h6>

				<div class="form">
					<asp:DropDownList ID="drp_StudyType" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
				</div>
			</div>
		</div>
	</div>

	<div class="col-md-4 col-sm-12 col-xs-12 no-padding ">
		<div class="data-container table-display moe-width-85  moe-sm-full-width">
			<div class="form-group">
				<h6 class="font-size-16 margin-bottom-0 margin-top-0 ">
					<asp:Label ID="lbl_StudySystem" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, StudySystem %>"></asp:Label>
				</h6>
				<div class="form">
					<asp:DropDownList ID="drp_StudySystem" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
				</div>
			</div>
		</div>
	</div>
	<div class="col-md-4 col-sm-12 col-xs-12 no-padding ">
		<div class="data-container table-display moe-width-85  moe-sm-full-width">
			<div class="form-group">
				<h6 class="font-size-16 margin-bottom-0 margin-top-0 ">
					<asp:Label ID="lbl_Nationality" runat="server" Text=" <%$Resources:ITWORX_MOEHEWF_UCE, Nationality %> "></asp:Label>
				</h6>

				<div class="form">
					<asp:DropDownList ID="drp_Nationality" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
				</div>
			</div>
		</div>
	</div>
	<div class="col-md-4 col-sm-12 col-xs-12 no-padding ">
		<div class="data-container table-display moe-width-85  moe-sm-full-width">
			<div class="form-group">
				<h6 class="font-size-16 margin-bottom-0 margin-top-0 ">
					<asp:Label ID="lbl_FinalDecision" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, FinalDecision %>"></asp:Label>
				</h6>
				<div class="form">
					<asp:DropDownList ID="drp_FinalDecision" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
				</div>
			</div>
		</div>
	</div>

	<div class="col-md-12 no-padding">
		<asp:Button ID="btn_Cancel" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, ChangeSearchCriteria %>" OnClick="btn_Cancel_Click" CssClass="btn moe-btn clear-btn pull-right" />
		<asp:Button ID="btn_Search" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Search %>" OnClick="btn_Search_Click" ClientIDMode="Static" CssClass="btn moe-btn pull-right" />
	</div>
</div>

         <h4 runat="server" id="searchLimit" visible="false" class="pull-left search-result-filter"><%=Resources.ITWORX.MOEHEWF.Common.SearchLimit%></h4>


<div class="row unheighlighted-section margin-bottom-50 flex-display flex-wrap edit-mode" id="DialogProc">
	<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
		<ContentTemplate>
			<div class="col-md-4 col-sm-12 col-xs-12 no-padding ">
				<div class="data-container table-display moe-width-85  moe-sm-full-width">
					<div class="form-group">
						<h6 class="font-size-16 margin-bottom-0 margin-top-0 ">
							<asp:Label ID="lbl_University" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, University %>" Visible="false"></asp:Label>
						</h6>

						<div class="form">
							<asp:DropDownList ID="drp_University" runat="server" OnSelectedIndexChanged="drp_University_SelectedIndexChanged" AutoPostBack="true" Visible="false" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
						</div>
					</div>
				</div>
			</div>
		</ContentTemplate>
		<Triggers>
			<asp:AsyncPostBackTrigger ControlID="drp_Country" EventName="SelectedIndexChanged" />
		</Triggers>
	</asp:UpdatePanel>
</div>


<div class="row unheighlighted-section margin-bottom-50 flex-display flex-wrap edit-mode" id="DialogProc">
	<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
		<ContentTemplate>
			<div class="col-md-4 col-sm-12 col-xs-12 no-padding ">
				<div class="data-container table-display moe-width-85  moe-sm-full-width">
					<div class="form-group">
						<h6 class="font-size-16 margin-bottom-0 margin-top-0 ">
							<asp:Label ID="lbl_Faculty" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Faculty %>" Visible="false"></asp:Label>
						</h6>

						<div class="form">
							<asp:DropDownList ID="drp_Faculty" runat="server" OnSelectedIndexChanged="drp_Faculty_SelectedIndexChanged" AutoPostBack="true" Visible="false" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
						</div>
					</div>
				</div>
			</div>
		</ContentTemplate>
		<Triggers>
			<asp:AsyncPostBackTrigger ControlID="drp_University" EventName="SelectedIndexChanged" />
		</Triggers>
	</asp:UpdatePanel>
</div>

<div class="row unheighlighted-section margin-bottom-50 flex-display flex-wrap edit-mode" id="DialogProc">
	<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
		<ContentTemplate>
			<div class="col-md-4 col-sm-12 col-xs-12 no-padding ">
				<div class="data-container table-display moe-width-85  moe-sm-full-width">
					<div class="form-group">
						<h6 class="font-size-16 margin-bottom-0 margin-top-0 ">
							<asp:Label ID="lbl_Specialization" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Specialization %>" Visible="false"></asp:Label>
						</h6>

						<div class="form">
							<asp:DropDownList ID="drp_Specialization" runat="server" Visible="false" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
						</div>
					</div>
				</div>
			</div>
		</ContentTemplate>
		<Triggers>
			<asp:AsyncPostBackTrigger ControlID="drp_Faculty" EventName="SelectedIndexChanged" />
		</Triggers>
	</asp:UpdatePanel>
</div>
</div>
<asp:GridView ID="grd_SimilarRequests" runat="server" AutoGenerateColumns="false" AllowPaging="true" ShowHeaderWhenEmpty="true"
	EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>" OnPageIndexChanging="grd_SimilarRequests_PageIndexChanging" DataKeyNames="ID" CssClass="table table-striped moe-full-width moe-table result-table margin-top-25">
	<Columns>
		<asp:TemplateField>
			<ItemTemplate>
				<asp:CheckBox ID="chkbox_Select" runat="server" />
				<asp:HiddenField ID="hdn_ID" runat="server" Value='<%#  Eval("ID")%>'></asp:HiddenField>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, RequestNumber %>">
			<ItemTemplate>
				<asp:LinkButton ID="lnk_RequestID" runat="server" Text='<%#  Eval("RequestNumber")%>' OnClick="lnk_Request_Click"></asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, QatariID %>">
			<ItemTemplate>
				<asp:Label ID="lbl_QatariID" runat="server" Text='<%#  Eval("QatariID")%>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, ApplicantName %>">
			<ItemTemplate>
				<asp:Label ID="lbl_ApplicantName" runat="server" Text='<%# Eval("ApplicantName") %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, Nationality %>">
			<ItemTemplate>
				<asp:Label ID="lbl_Nationality" runat="server" Text='<%#  Eval("Nationality")%>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, AcademicDegree %>">
			<ItemTemplate>
				<asp:Label ID="lbl_AcademicDegree" runat="server" Text='<%#  Eval("AcademicDegreeForEquivalence")%>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, EntityNeedsEquivalency %>">
			<ItemTemplate>
				<asp:Label ID="lbl_EntityNeedsCertificate" runat="server" Text='<%# Eval("EntityNeedsEquivalency") %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, CountryName %>">
			<ItemTemplate>
				<asp:Label ID="lbl_Country" runat="server" Text='<%# Eval("Country") %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, University %>">
			<ItemTemplate>
				<asp:Label ID="lbl_University" runat="server" Text='<%# Eval("University") %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, FacultyName %>">
			<ItemTemplate>
				<asp:Label ID="lbl_Faculty" runat="server" Text='<%# Eval("Faculty") %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, SubmitDate %>">
			<ItemTemplate>
				<asp:Label ID="lbl_RequestSubmitDate" runat="server" Text='<%# Convert.ToDateTime(Eval("SubmitDate"))!=DateTime.MinValue ? Convert.ToDateTime(Eval("SubmitDate")).ToShortDateString():string.Empty %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<%-- <asp:TemplateField HeaderText="<%$Resources:ITWORX.MOEHEWF.Common, AttachmentName %>">
            <ItemTemplate>
                <asp:HyperLink ID="hypAttachment"   Text='<%#Eval("FileName") %>'  runat="server" NavigateUrl='<%#Eval("AttachmentURL") %>' Target="_blank"></asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>--%>
	</Columns>
</asp:GridView>


<div class="row no-padding">
	<asp:Button ID="btn_ApprovedAttachement" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ApprovedAttachement %>" OnClick="btn_ApprovedAttachement_Click" OnClientClick="return ConfirmOnApprove();" CssClass="btn moe-btn pull-right" />
</div>

<div class="row no-padding margin-top-15">
	<h4 class="font-size-18 font-weight-600 text-center success-msg">
		<asp:Label ID="lbl_SuccessMsg" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, SuccessMsg %>"></asp:Label>

	</h4>
</div>

<div class="row no-padding margin-top-15">
	<h4 class="font-size-18 font-weight-600 text-center">
		<asp:Label ID="lbl_NoOfRequests" runat="server"></asp:Label>

	</h4>
</div>

<script type="text/javascript">
	$(document).ready(function () {


		$("#dt_DateFrom").datepicker({
			dateFormat: "m/d/yy",
			showOn: 'focus',
			showButtonPanel: true,
			closeText: 'Clear',
			changeYear: true,
			changeMonth: true,
			onClose: function () {
				var event = arguments.callee.caller.caller.arguments[0];
				// If "Clear" gets clicked, then really clear it
				if ($(event.delegateTarget).hasClass('ui-datepicker-close')) {
					$(this).val('');
				}
			},
			onSelect: function () {
				var dt2 = $('#dt_DateTo');
				var startDate = $(this).datepicker('getDate');
				//add 30 days to selected date
				startDate.setDate(startDate.getDate() + 30);
				var minDate = $(this).datepicker('getDate');
				var dt2Date = dt2.datepicker('getDate');
				//difference in days. 86400 seconds in day, 1000 ms in second
				var dateDiff = (dt2Date - minDate) / (86400 * 1000);

				//dt2 not set or dt1 date is greater than dt2 date
				if (dt2Date == null || dateDiff < 0) {
					dt2.datepicker('setDate', minDate);
				}
				//dt1 date is 30 days under dt2 date
				else if (dateDiff > 30) {
					dt2.datepicker('setDate', startDate);
				}
				//sets dt2 maxDate to the last day of 30 days window
				dt2.datepicker('option', 'maxDate', startDate);
				//first day which can be selected in dt2 is selected date in dt1
				dt2.datepicker('option', 'minDate', minDate);
			}
		});
		$('#dt_DateTo').datepicker({
			dateFormat: "m/d/yy",
			showOn: 'focus',
			showButtonPanel: true,
			closeText: 'Clear',
			changeYear: true,
			changeMonth: true,
			onClose: function () {
				var event = arguments.callee.caller.caller.arguments[0];
				// If "Clear" gets clicked, then really clear it
				if ($(event.delegateTarget).hasClass('ui-datepicker-close')) {
					$(this).val('');
				}
			}
		});

		if ($("#hdn_DateFrom").val() != "") {
			$("#dt_DateFrom").val($("#hdn_DateFrom").val());
		}
		if ($("#hdn_DateTo").val() != "") {
			$("#dt_DateTo").val($("#hdn_DateTo").val());
		}
		$("#btn_Search").click(function () {
			if ($("#dt_DateFrom").val() != "") {
				$("#hdn_DateFrom").val($("#dt_DateFrom").val());
				$("#dt_DateFrom").val($("#hdn_DateFrom").val());

			}
			if ($("#dt_DateTo").val() != "") {
				$("#hdn_DateTo").val($("#dt_DateTo").val());
				$("#dt_DateTo").val($("#hdn_DateTo").val());
			}
		});
	});
	function ConfirmOnApprove() {
		var dialogText;
		if (_spPageContextInfo.webServerRelativeUrl.includes("en") == true) {
			dialogText = "Are you sure you want to Approve ?"

		}
		else {
			dialogText = "هل حقاً تريد الموافقة ?"
		}
		if (confirm(dialogText) == true)
			return true;
		else
			return false;
	}
</script>
<%--<asp:Label ID="lbl_NoResults" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, NoResults %>" Font-Bold="true"></asp:Label>--%>
