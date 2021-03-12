<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="MOEHE" TagName="FileUpload" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Procedure_LegalAffairsViewOpinion.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.Procedure_LegalAffairsViewOpinion" %>


<div class="container">
<div class="row">
    <div class="col-md-6 col-sm-12 col-xs-12">
	<div class="data-container">
		<MOEHE:FileUpload runat="server" id="legalAffairsAttachements" LabelDisplayName="legalAffairsAttachements" />
	</div>
</div>
</div>

<div class="row">
	<asp:LinkButton ID="lnk_AddNewOpinionPopUp" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ApproveProcedure %>" OnClick="lnk_AddNewOpinionPopUp_Click" CssClass="btn moe-btn pull-right" />
</div>
<div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
	<div class="data-container">
		<h6 class="font-size-16 margin-bottom-15">

			<asp:Label ID="lbl_InitialOpinion" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, InitialOpinion %>" Visible="false"></asp:Label>

		</h6>
		<h5 class="font-size-22">
			<asp:Label ID="lbl_InitialOpinionVal" runat="server" Visible="false"></asp:Label>
		</h5>
	</div>
</div>
</div>
    </div>
