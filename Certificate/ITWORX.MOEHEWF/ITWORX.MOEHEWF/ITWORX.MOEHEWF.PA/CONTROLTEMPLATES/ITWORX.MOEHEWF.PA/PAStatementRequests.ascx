<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PAStatementRequests.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.PAStatementRequests" %>

<div class="row no-padding">
	<asp:LinkButton ID="lnk_AddNewStatementPopUp" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, AddNewStatementRequest %>" OnClick="lnk_AddNewStatementPopUp_Click" CssClass="btn moe-btn pull-right" Visible="false" />
</div>


<asp:Panel ID="pnl_Requests" runat="server" GroupingText=" <%$Resources:ITWORX_MOEHEWF_PA, PAStatementRequestsRegistry %>" CssClass="row no-padding margin-top-25 margin-bottom-25">
	<div class="row no-padding">
		<div class="col-md-12">
            <h4 class="font-size-18 font-weight-600 text-right">
			<asp:Label ID="lbl_NoOfRequests" runat="server"></asp:Label>
		</h4>
		</div>
	</div>

    <asp:GridView ID="grd_PAStatementRequests" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="grd_PAStatementRequests_PageIndexChanging"
		ShowHeaderWhenEmpty="true" EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>" OnRowDataBound="grd_PAStatementRequests_RowDataBound" CssClass="table table-striped moe-full-width moe-table result-table">
		<Columns>
			<asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, StatementRequestDate %>">
				<ItemTemplate>
					<asp:HiddenField ID="hdn_ID" runat="server" Value='<%# Eval("ID") %>' />
					<asp:HiddenField ID="hdn_RequestID" runat="server" Value='<%# Eval("RequestID") %>' />
					<asp:Label ID="lbl_StatementRequestDate" runat="server" Text='<%# Convert.ToDateTime(Eval("StatementDate"))!=DateTime.MinValue ? Convert.ToDateTime(Eval("StatementDate")).ToShortDateString():string.Empty %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, StatementCreatedBy %>">
				<ItemTemplate>
					<asp:Label ID="lbl_StatementCreatedBy" runat="server" Text='<%# Eval("StatementCreatedby") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, StatementSubject %>">
				<ItemTemplate>
					<asp:Label ID="lbl_StatementSubject" runat="server" Text='<%# Eval("StatementSubject") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, StatementDirectedTo %>">
				<ItemTemplate>
					<asp:Label ID="lbl_StatementDirectedTo" runat="server" Text='<%# Eval("DirectedTo") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, ReplyDate %>">
				<ItemTemplate>
					<asp:Label ID="lbl_ReplyDate" runat="server" Text='<%# Convert.ToDateTime(Eval("StatementReplyDate"))!=DateTime.MinValue ? Convert.ToDateTime(Eval("StatementReplyDate")).ToShortDateString():string.Empty %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, ReplySender %>">
				<ItemTemplate>
					<asp:Label ID="lbl_ReplySender" runat="server" Text='<%# Eval("StatementReplyby") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, Actions %>">
				<ItemTemplate>
					<asp:LinkButton ID="lnk_ViewDetails" runat="server" OnClick="lnk_ViewDetails_Click" Tooltip="<%$Resources:ITWORX_MOEHEWF_PA, ViewDetails %>" CssClass="display-icon fa fa-eye"/>
					<asp:LinkButton ID="lnk_ReplytoStatement" runat="server" Tooltip="<%$Resources:ITWORX_MOEHEWF_PA, ReplytoStatement %>" OnClick="lnk_ReplytoStatement_Click" CssClass="display-icon fa fa-reply"></asp:LinkButton>

				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>

	
</asp:Panel>
