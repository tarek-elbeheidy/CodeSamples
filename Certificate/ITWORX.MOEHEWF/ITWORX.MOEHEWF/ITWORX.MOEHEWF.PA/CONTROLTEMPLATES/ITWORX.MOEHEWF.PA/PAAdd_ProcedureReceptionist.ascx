<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PAAdd_ProcedureReceptionist.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.Add_ProcedureReceptionist" %>

<div id="main-content">

	<div class="row unheighlighted-section margin-bottom-50 flex-display flex-wrap">
		<div class="col-md-12 col-sm-12 col-xs-12 margin-top-10 margin-bottom-10">
			<div class="data-container table-display moe-full-width">
				<div class="form-group">
					<h6 class="font-size-16 margin-bottom-10 margin-top-15">
						<asp:Label ID="lbl_Procedure" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Procedure %>"></asp:Label>
						<span class="error-msg astrik">* </span>

					</h6>
					<asp:TextBox ID="txt_Procedure" runat="server" TextMode="MultiLine" CssClass="moe-full-width input-height-42 border-box moe-input-padding text-area"></asp:TextBox>
					<asp:RequiredFieldValidator ID="reqVal_Procedure" runat="server" ForeColor="Red" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredProcedure %>"
						ValidationGroup="Approve" ControlToValidate="txt_Procedure" CssClass="error-msg moe-full-width"></asp:RequiredFieldValidator>
				</div>
			</div>
		</div>

		<div class="col-md-12 no-padding">
			<asp:Button ID="btn_ApproveProcedures" ValidationGroup="Approve" runat="server" OnClick="btn_ApproveProcedures_Click" Text="<%$Resources:ITWORX_MOEHEWF_UCE, SubmitProcedure %>" CssClass="btn moe-btn pull-right" />

		</div>
	</div>
</div>
