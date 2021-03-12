<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PAProcedure_AssistSecretaryDetails.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.PAProcedure_AssistSecretaryDetails" %>


<asp:HiddenField ID="hdn_ID" runat="server" Value='<%# Eval("ID") %>' />

<div class="row heighlighted-section margin-bottom-50 flex-display flex-wrap">

	<div class="col-md-4 col-sm-6">
		<div class="data-container">
			<h6 class="font-size-16 margin-bottom-15">
				<asp:Label ID="lbl_RequestID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RequestID %>"></asp:Label>
			</h6>
			<h5 class="font-size-20">
				<asp:Label ID="lbl_RequestIDVal" runat="server"></asp:Label>
			</h5>
		</div>
	</div>

	<div class="col-md-4 col-sm-6">
		<div class="data-container">
			<h6 class="font-size-16 margin-bottom-15">
				<asp:Label ID="lbl_Procedure" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Procedure %>"></asp:Label>
			</h6>
			<h5 class="font-size-20">
				<asp:Label ID="lbl_ProcedureVal" runat="server"></asp:Label>
			</h5>
		</div>
	</div>

	<div class="col-md-4 col-sm-6">
		<div class="data-container">
			<h6 class="font-size-16 margin-bottom-15">
				<asp:Label ID="lbl_RejectionReason" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RejectionReason %>" Visible="false"></asp:Label>
			</h6>
			<h5 class="font-size-20">
				<asp:Label ID="lbl_RejectionReasonVal" runat="server" Visible="false"></asp:Label>
			</h5>
		</div>
	</div>

	<div class="col-md-4 col-sm-6">
		<div class="data-container">
			<h6 class="font-size-16 margin-bottom-15">
				<asp:Label ID="lbl_ProcedureDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ProcedureDate %>"></asp:Label>
			</h6>
			<h5 class="font-size-20">
				<asp:Label ID="lbl_ProcedureDateVal" runat="server"></asp:Label>
			</h5>
		</div>
	</div>

	<div class="col-md-4 col-sm-6">
		<div class="data-container">
			<h6 class="font-size-16 margin-bottom-15">
				<asp:Label ID="lbl_ProcedureComments" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ProcedureComments %>"></asp:Label>
			</h6>
			<h5 class="font-size-20">
				<asp:Label ID="lbl_ProcedureCommentsVal" runat="server"></asp:Label>
			</h5>
		</div>
	</div>

	<div class="col-md-4 col-sm-6">
		<div class="data-container">
			<h6 class="font-size-16 margin-bottom-15">
				<asp:Label ID="lbl_ProcedureCreatedBy" runat="server" Text=" <%$Resources:ITWORX_MOEHEWF_UCE, ProcedureCreatedBy %>"></asp:Label>
			</h6>
			<h5 class="font-size-20">
				<asp:Label ID="lbl_ProcedureCreatedByVal" runat="server"></asp:Label>
			</h5>
		</div>
	</div>
</div>
<div class="row no-padding">
	<asp:Button ID="btn_Close" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Close %>" OnClick="btn_Close_Click" CssClass="btn moe-btn pull-right" />

</div>
