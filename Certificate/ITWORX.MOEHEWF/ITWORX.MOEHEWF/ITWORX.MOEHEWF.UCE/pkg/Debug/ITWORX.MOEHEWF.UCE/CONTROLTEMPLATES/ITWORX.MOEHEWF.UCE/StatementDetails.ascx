<%@ Assembly Name="ITWORX.MOEHEWF.UCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=883afb4c05a35fe5" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="MOEHE" TagName="FileUpload" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StatementDetails.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.StatementDetails" %>

<style>
    .cust_data {
    border: 1px solid #ccc;
    margin: 0;
    padding: 20px 10px;
}

    .cust_title {
    display: inline-block;
    background-color: #a30234;
    padding: 10px 10px 20px 20px;
    color: #fff !important;
    margin-bottom: 0;
}

</style>

<asp:Panel ID="pnl_StatementDetails" runat="server" GroupingText="<%$Resources:ITWORX_MOEHEWF_UCE, StatementDetails %>" CssClass="stateTitle">
	<asp:HiddenField ID="hdn_ID" runat="server" Value='<%# Eval("ID") %>' />
	<!--heighlighted Section-->
	<div class="col-md-12 heighlighted-section margin-bottom-50 flex-display flex-wrap">

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
					<asp:Label ID="lbl_StatDetailsDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, StatementRequestDate %>"></asp:Label>

				</h6>
				<h5 class="font-size-20">
					<asp:Label ID="lbl_StatDetailsDateVal" runat="server"></asp:Label>
				</h5>
			</div>
		</div>

		<div class="col-md-4 col-sm-6">
			<div class="data-container">
				<h6 class="font-size-16 margin-bottom-15">
					<asp:Label ID="lbl_StatDetailsSubject" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, StatementSubject %>"></asp:Label>

				</h6>
				<h5 class="font-size-20">
					<asp:Label ID="lbl_StatDetailsSubjectVal" runat="server"></asp:Label>
				</h5>
			</div>
		</div>

		<div class="col-md-4 col-sm-6">
			<div class="data-container">
				<h6 class="font-size-16 margin-bottom-15">
					<asp:Label ID="lbl_StatDetailsDirectedTo" runat="server" Text=" <%$Resources:ITWORX_MOEHEWF_UCE, StatementDirectedTo %>"></asp:Label>

				</h6>
				<h5 class="font-size-20">
					<asp:Label ID="lbl_StatDetailsDirectedToVal" runat="server"></asp:Label>
				</h5>
			</div>
		</div>

		<div class="col-md-4 col-sm-6">
			<div class="data-container">
				<h6 class="font-size-16 margin-bottom-15">
					<asp:Label ID="lbl_StatDetailsCreatedby" runat="server" Text=" <%$Resources:ITWORX_MOEHEWF_UCE, StatementCreatedBy %>"></asp:Label>

				</h6>
				<h5 class="font-size-20">
					<asp:Label ID="lbl_StatDetailsCreatedbyVal" runat="server"></asp:Label>
				</h5>
			</div>
		</div>

	</div>


	<!--unheighlighted Section-->
	<div class="col-md-12 no-padding unheighlighted-section margin-bottom-50 flex-display flex-wrap" runat="server" id="ReplyControlsdiv">
		<div class="col-md-4 col-sm-6">
			<div class="data-container">
				<h6 class="font-size-16 margin-bottom-15">
					<asp:Label ID="lbl_StatDetailsReplyDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ReplyDate %>"></asp:Label>

				</h6>
				<h5 class="font-size-20">
					<asp:Label ID="lbl_StatDetailsReplyDateVal" runat="server"></asp:Label>
				</h5>
			</div>
		</div>

		<div class="col-md-4 col-sm-6">
			<div class="data-container">
				<h6 class="font-size-16 margin-bottom-15">
					<asp:Label ID="lbl_StatDetailsReplyby" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ReplySender %>"></asp:Label>

				</h6>
				<h5 class="font-size-20">
					<asp:Label ID="lbl_StatDetailsReplybyVal" runat="server"></asp:Label>
				</h5>
			</div>
		</div>

        <div class="col-md-12 col-sm-12 col-xs-12 margin-top-25">
			<div class="data-container">
				<h6 class="font-size-16 margin-bottom-15 cust_title">
					<asp:Label ID="lbl_StatDetailsRequested" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, StatementRequested %>"></asp:Label>

				</h6>
				<h5 class="font-size-20 cust_data">
					<asp:Label ID="lbl_StatDetailsRequestedVal" runat="server"></asp:Label>
				</h5>
			</div>
		</div>

        <div class="col-md-12 col-sm-12 col-xs-12 margin-top-25">
			<div class="data-container">
				<h6 class="font-size-16 margin-bottom-15 cust_title">
					<asp:Label ID="lbl_StatDetailsReply" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, StatementReply %> "></asp:Label>

				</h6>
				<h5 class="font-size-20 cust_data">
					<asp:Label ID="lbl_StatDetailsReplyVal" runat="server"></asp:Label>
				</h5>
			</div>
		</div>

		<div class="col-md-12 col-sm-12 col-xs-12">
			<div class="data-container">
				<h6 class="font-size-16 margin-bottom-15"></h6>
				<h5>
					<MOEHE:FileUpload runat="server" id="StatementReplyAttachements" />
				</h5>
			</div>
		</div>


	</div>


	<div class="col-md-12 no-padding">
		<asp:Button ID="btn_Close" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Close %>" OnClick="btn_Close_Click" CssClass="btn moe-btn pull-right" />
	</div>




</asp:Panel>
