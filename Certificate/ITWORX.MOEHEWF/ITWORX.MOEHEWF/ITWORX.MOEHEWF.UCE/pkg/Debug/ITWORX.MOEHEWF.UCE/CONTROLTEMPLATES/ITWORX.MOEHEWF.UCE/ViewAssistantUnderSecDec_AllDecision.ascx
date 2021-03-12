<%@ Assembly Name="ITWORX.MOEHEWF.UCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=883afb4c05a35fe5" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewAssistantUnderSecDec_AllDecision.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.ViewAssistantUnderSecDec_AllDecision" %>

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
<div class="row no-padding">
	<h4 class="font-size-18 font-weight-600 text-center">
		<asp:Label ID="lbl_NoResults" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NoResults %>" Visible="false"></asp:Label>
	</h4>
</div>
