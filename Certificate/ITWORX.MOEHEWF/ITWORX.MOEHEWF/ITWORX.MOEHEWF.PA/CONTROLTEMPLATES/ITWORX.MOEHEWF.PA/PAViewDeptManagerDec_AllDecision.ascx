<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PAViewDeptManagerDec_AllDecision.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.ViewDeptManagerDec_AllDecision" %>


<div class="col-md-3 col-sm-6">
	<div class="data-container">
		<h6 class="font-size-16 margin-bottom-15">
			<asp:Label ID="lbl_Decision" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, FinalDecision %>" Visible="false"></asp:Label>
		</h6>
		<h5 class="font-size-20">
			<asp:Label ID="lbl_FinalDecisionVal" runat="server"></asp:Label>
		</h5>
	</div>
</div>
<div class="col-md-3 col-sm-6">
	<div class="data-container">
		<h6 class="font-size-16 margin-bottom-15">
			<asp:Label ID="lbl_RejectionReason" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RejectionReason %>" Visible="false"></asp:Label>
		</h6>
		<h5 class="font-size-20">
			<asp:Label ID="lbl_RejectionReasonVal" runat="server" Visible="false"></asp:Label>
		</h5>
	</div>
</div>


<div class="col-md-12 col-sm-12 col-xs-12">
	<div class="data-container">
		<h6 class="font-size-16 margin-bottom-15">
			<asp:Label ID="lbl_Comments" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ProcedureComments %>" Visible="false"></asp:Label>
		</h6>
		<h5 class="font-size-20">
			<asp:Label ID="lbl_CommentsVal" runat="server"></asp:Label>
		</h5>
	</div>
</div>

<div class="col-md-12 no-padding">
	<asp:Button ID="btn_Reject" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ClosedByRejection %>" OnClick="btn_Reject_Click" Visible="false" CssClass="btn moe-btn pull-right clear-btn" />
</div>

<div class="col-md-12 no-padding">
	<asp:Button ID="btn_Approve" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ClosedByAcceptance %>" OnClick="btn_Approve_Click" Visible="false" CssClass="btn moe-btn pull-right" />
</div>

<div class="col-md-12 no-padding">
	<asp:Button ID="btn_PrintFinalDecision" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PrintFinalDecision %>" OnClick="btn_PrintFinalDecision_Click"
		Visible="false" OnClientClick="aspnetForm.target ='_blank';" CssClass="btn moe-btn pull-right" />
</div>

<div class="row no-padding margin-top-15">
	<h5 class="col-md-12 font-size-18 font-weight-600 success-msg">
		<asp:Label ID="lbl_SuccessMsg" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RequestClosed %>" Visible="false"></asp:Label>
	</h5>
</div>
<div class="row no-padding margin-top-20">
	<h5 class="col-md-12 font-size-18 font-weight-600 no-padding text-center">
		<asp:Label ID="lbl_NoResults" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NoResults %>" Visible="false"></asp:Label>
	</h5>
</div>
