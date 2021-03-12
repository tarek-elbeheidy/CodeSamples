<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="MOEHE" TagName="FileUpload" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Add_NewRecommendation.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.Add_NewRecommendation" %>




<div id="main-content" class="no-margin">
	<div class="row unheighlighted-section">
	<asp:Panel ID="pnl_PresProgRecommend" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PresProgRecommend %>">
		<div class="col-md-4 col-sm-6 margin-top-10 margin-bottom-10">
			<div class="data-container table-display moe-width-85">
				<div class="form-group">
					<h6 class="font-size-16 margin-bottom-10 margin-top-15">
						<asp:Label ID="lbl_Recommendation" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Recommendation %>"></asp:Label>
						<span class="error-msg astrik">* </span>

					</h6>
					<asp:DropDownList ID="drp_Recommendations" runat="server" CssClass="moe-dropdown moe-full-width input-height-42 border-box moe-input-padding">
					</asp:DropDownList>
					<asp:RequiredFieldValidator ID="reqVal_Recommendations" runat="server" ForeColor="Red" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredRecommendations %>"
						ValidationGroup="Approve" ControlToValidate="drp_Recommendations" CssClass="error-msg moe-full-width"></asp:RequiredFieldValidator>

				</div>
			</div>
		</div>


		<div class="col-md-12 col-sm-12 col-xs-12 margin-top-10 margin-bottom-10">
			<div class="data-container table-display moe-full-width">
				<div class="form-group">
					<h6 class="font-size-16 margin-bottom-10 margin-top-15">
						<asp:Label ID="lbl_Comments" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ProcedureComments %>"></asp:Label>
						<span class="error-msg astrik">* </span>

					</h6>
					<asp:TextBox ID="txt_Comments" runat="server" TextMode="MultiLine" CssClass="moe-full-width input-height-42 border-box moe-input-padding text-area"></asp:TextBox>

					<asp:RequiredFieldValidator ID="reqVal_Comments" runat="server" CssClass="error-msg moe-full-width" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredComments %>"
						ValidationGroup="Approve" ControlToValidate="txt_Comments"></asp:RequiredFieldValidator>

				</div>
			</div>
		</div>
       <div class="col-md-8 col-sm-12 col-xs-12 margin-top-10 margin-bottom-10">
			<div class="data-container table-display moe-full-width">
				<div class="form-group">
                    <MOEHE:FileUpload runat="server" id="RecommProgramManagerAttachements" />
                </div>
            </div>
           </div>
		<div class="col-md-12 no-padding">

			<asp:Button ID="btn_ApproveRecommendation" ValidationGroup="Approve" runat="server" OnClick="btn_ApproveRecommendation_Click" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ApproveRecommendation %>" CssClass="btn moe-btn pull-right" />

		</div>

	</asp:Panel>

</div>
</div>
