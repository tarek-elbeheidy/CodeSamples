<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.PA/PARolesNavigationLinks.ascx" TagPrefix="uc1" TagName="PARolesNavigationLinks" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PAAdd_NewRecommendation.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.Add_NewRecommendation" %>

<div id="main-content" class="no-margin">
<div class="row unheighlighted-section margin-bottom-50">

				<asp:Panel ID="pnl_PresProgRecommend" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, PresProgRecommend %>">
					<div class="col-md-4 col-sm-6 margin-top-10 margin-bottom-10">
						<div class="data-container table-display moe-width-85">
							<div class="form-group">
								<h6 class="font-size-16 margin-bottom-10 margin-top-15">
									<asp:Label ID="lbl_Recommendation" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, Recommendation %>"></asp:Label>
									<span class="error-msg astrik">* </span>
								</h6>
								<asp:DropDownList ID="drp_PARecommendations" runat="server" CssClass="moe-dropdown moe-full-width input-height-42 border-box moe-input-padding">
								</asp:DropDownList>
								<asp:RequiredFieldValidator ID="reqVal_PARecommendations" runat="server" ForeColor="Red" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RequiredPARecommendations %>"
									ValidationGroup="Approve" ControlToValidate="drp_PARecommendations" CssClass="error-msg moe-full-width"></asp:RequiredFieldValidator>
							</div>
						</div>
					</div>
					<div class="col-md-12 col-sm-12 col-xs-12 margin-top-10 margin-bottom-10">
						<div class="data-container table-display moe-full-width">
							<div class="form-group">
								<h6 class="font-size-16 margin-bottom-10 margin-top-15">
									<asp:Label ID="lbl_Comments" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, ProcedureComments %>"></asp:Label>
									<span class="error-msg astrik">* </span>
								</h6>
								<asp:TextBox ID="txt_Comments" runat="server" TextMode="MultiLine" CssClass="moe-full-width input-height-42 border-box moe-input-padding text-area"></asp:TextBox>

								<asp:RequiredFieldValidator ID="reqVal_Comment" runat="server" ForeColor="Red" CssClass="error-msg moe-full-width" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RequiredComments %>"
									ValidationGroup="Approve" ControlToValidate="txt_Comments"></asp:RequiredFieldValidator>
							</div>
						</div>
					</div>
					<div class="col-md-12 no-padding">
						<asp:Button ID="btn_ApproveRecommendation" ValidationGroup="Approve" runat="server" OnClick="btn_ApproveRecommendation_Click" Text="<%$Resources:ITWORX_MOEHEWF_PA, ApproveRecommendation %>" CssClass="btn moe-btn pull-right" />
					</div>
				</asp:Panel>


			</div>


    </div>