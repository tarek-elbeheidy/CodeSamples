<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PAAdd_DepartmentManagerProc.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.Add_DepartmentManagerProc" %>






<div id="main-content">
	<div class="row unheighlighted-section margin-bottom-50 flextxt_Comments-display flex-wrap">
		<div class="col-md-4 col-sm-6 margin-top-10 margin-bottom-10">
			<div class="data-container table-display moe-width-85">
				<div class="form-group">
					<h6 class="font-size-16 margin-bottom-10 margin-top-15">
						<asp:Label ID="lbl_Procedure" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, Procedure %>"></asp:Label>
						<span class="error-msg astrik">* </span>
					</h6>
					<asp:DropDownList ID="drp_Procedure" runat="server" CssClass="moe-dropdown moe-full-width input-height-42 border-box moe-input-padding" OnSelectedIndexChanged="drp_Procedure_SelectedIndexChanged" AutoPostBack="true">
</asp:DropDownList>		
<asp:RequiredFieldValidator ID="reqVal_Procedure" runat="server" CssClass="error-msg moe-full-width" ForeColor="Red" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RequiredProcedure %>"
	ValidationGroup="Approve" ControlToValidate="drp_Procedure"></asp:RequiredFieldValidator>
				</div>
			</div>
		</div>

		<div class="col-md-4 col-sm-6 margin-top-10 margin-bottom-10 no-padding">
			<div class="data-container table-display moe-width-85">
				<div class="form-group">
					<h6 class="font-size-16 margin-bottom-10 margin-top-15">
						<asp:Label ID="lbl_RejectionReason" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, RejectionReason %>" Visible="false"></asp:Label>
					</h6>
					<asp:DropDownList ID="drp_RejectionReason" CssClass="moe-dropdown moe-full-width input-height-42 border-box moe-input-padding" runat="server" Visible="false"></asp:DropDownList>
				</div>
			</div>
		</div>

		<div class="col-md-12 margin-top-10 margin-bottom-10  no-padding">
			<div class="data-container table-display moe-full-width">
				<div class="form-group">
					<h6 class="font-size-16 margin-bottom-10 margin-top-15">
						<asp:Label ID="lbl_Comments" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, ProcedureComments %>"></asp:Label>
						<span class="error-msg astrik">* </span>
					</h6>
					<asp:TextBox ID="txt_Comments" runat="server" CssClass="moe-full-width input-height-42 border-box moe-input-padding" TextMode="MultiLine"></asp:TextBox>
					<asp:RequiredFieldValidator ID="reqVal_Comments" CssClass="error-msg moe-full-width" runat="server" ForeColor="Red" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RequiredComments %>"
	ValidationGroup="Approve" ControlToValidate="txt_Comments"></asp:RequiredFieldValidator>
				</div>
			</div>
		</div>
		
		<div class="col-md-12 no-padding margin-top-10">
			<asp:Button ID="btn_ApproveProcedures" ValidationGroup="Approve" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, ApproveProcedure %>" OnClick="btn_ApproveProcedures_Click" CssClass="btn moe-btn pull-right" />
		</div>
	</div>
</div>
