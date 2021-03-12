<%@ Assembly Name="ITWORX.MOEHEWF.UCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=883afb4c05a35fe5" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StatementRequests.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.StatementRequests" %>

<div class="row no-padding">
	<asp:LinkButton ID="lnk_AddNewStatementPopUp" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, AddNewStatementRequest %>" OnClick="lnk_AddNewStatementPopUp_Click" CssClass="btn moe-btn pull-right"/>
</div>


<asp:Panel ID="pnl_Requests" runat="server" GroupingText=" <%$Resources:ITWORX_MOEHEWF_UCE, StatementRequestsRegistry %>" CssClass="row no-padding margin-top-25 margin-bottom-25">
    <div class="row no-padding">
	<h4 class="font-size-18 font-weight-600 text-right">
		    <asp:Label ID="lbl_NoOfRequests" runat="server"></asp:Label>
	</h4>
</div>
    <asp:GridView ID="grd_StatementRequests" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="grd_StatementRequests_PageIndexChanging"
    ShowHeaderWhenEmpty="true" EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>" OnRowDataBound="grd_StatementRequests_RowDataBound" CssClass="table table-striped moe-full-width moe-table result-table">
        <Columns>
            <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, StatementRequestDate %>">
                <ItemTemplate>
                    <asp:HiddenField ID="hdn_ID" runat="server" Value='<%# Eval("ID") %>' />
                    <asp:HiddenField ID="hdn_RequestID" runat="server" Value='<%# Eval("RequestID") %>' />
                    <asp:Label ID="lbl_StatementRequestDate" runat="server" Text='<%# Convert.ToDateTime(Eval("StatementDate"))!=DateTime.MinValue ? Convert.ToDateTime(Eval("StatementDate")).ToShortDateString():string.Empty %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, StatementCreatedBy %>">
                <ItemTemplate>
                    <asp:Label ID="lbl_StatementCreatedBy" runat="server" Text='<%# Eval("StatementCreatedby") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, StatementSubject %>">
                <ItemTemplate>
                    <asp:Label ID="lbl_StatementSubject" runat="server" Text='<%# Eval("StatementSubject") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, StatementDirectedTo %>">
                <ItemTemplate>
                    <asp:Label ID="lbl_StatementDirectedTo" runat="server" Text='<%# Eval("DirectedTo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, ReplyDate %>">
                <ItemTemplate>
                    <asp:Label ID="lbl_ReplyDate" runat="server" Text='<%# Convert.ToDateTime(Eval("StatementReplyDate"))!=DateTime.MinValue ? Convert.ToDateTime(Eval("StatementReplyDate")).ToShortDateString():string.Empty %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, ReplySender %>">
                <ItemTemplate>
                    <asp:Label ID="lbl_ReplySender" runat="server" Text='<%# Eval("StatementReplyby") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lnk_ReplytoStatement" runat="server" Tooltip="<%$Resources:ITWORX_MOEHEWF_UCE, ReplytoStatement %>" OnClick="lnk_ReplytoStatement_Click" CssClass="display-icon fa fa-reply"></asp:LinkButton>
					<asp:LinkButton ID="lnk_ViewDetails" runat="server" OnClick="lnk_ViewDetails_Click" Tooltip="<%$Resources:ITWORX_MOEHEWF_UCE, ViewDetails %>" CssClass="display-icon fa fa-eye"/>

                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>



</asp:Panel>
