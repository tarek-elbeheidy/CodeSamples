<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DepartmentManagerDecision.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.DepartmentManagerDecision" %>

<div class="row no-padding margin-bottom-25 margin-2">
	<asp:LinkButton ID="lnk_AddNewDecisionPopUp" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ApproveDecision %>" OnClick="lnk_AddNewDecisionPopUp_Click" CssClass="btn moe-btn pull-right" />
</div>

<h6 class="font-size-16 margin-bottom-15">
<asp:Label ID="lblPreventAddingDecision" runat="server"  Text="<%$Resources:ITWORX_MOEHEWF_UCE, ApproveDecisionPermission %>" Visible="false"></asp:Label>
</h6>
<div class="container heighlighted-section margin-bottom-50" runat="server" id ="divContainer">


    <div class="row">



<div class="col-md-6 col-sm-6 auto-height">
	<div class="data-container">
		<h6 class="font-size-16 margin-bottom-15">
			<asp:Label ID="lbl_Decision" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, FinalDecision %>" Visible="false"></asp:Label>
		</h6>
		<h5 class="font-size-20">
			<asp:Label ID="lbl_FinalDecisionVal" runat="server"></asp:Label>
		</h5>
	</div>
</div>

<div class="col-md-6 col-sm-6 auto-height">
	<div class="data-container">
		<h6 class="font-size-16 margin-bottom-15">
			<asp:Label ID="lbl_RejectionReason" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RejectionReason %>" Visible="false"></asp:Label>
		</h6>
		<h5 class="font-size-20">
			<asp:Label ID="lbl_RejectionReasonVal" runat="server" Visible="false"></asp:Label>
		</h5>
	</div>
</div>
    </div>
    <div class="row margin-top-15">
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
        </div>
    <div class="row">
<div class="col-md-12 col-sm-12 col-xs-12">
	<div class="data-container">
		<h6 class="font-size-16 margin-bottom-15">
			<asp:Label ID="lbl_SuccessMsg" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RequestClosed %>" Visible="false"></asp:Label>
		</h6>
	
	</div>
</div>
    </div>
	</div>