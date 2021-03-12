<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExternalCommListing.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.ExternalCommListing" %>

<div class="row no-padding">
	<asp:LinkButton ID="lnk_AddNewBookPopUp" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, AddNewBook %>" OnClick="lnk_AddNewBookPopUp_Click"  CssClass="btn moe-btn pull-right"/>
</div>

<asp:Panel ID="pnl_Books" runat="server" GroupingText="<%$Resources:ITWORX_MOEHEWF_UCE, ExternalContactsRegistry %>" CssClass="row no-padding margin-top-10">
    <asp:GridView ID="grd_Books"  runat="server" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="grd_Books_PageIndexChanging"
        ShowHeaderWhenEmpty="true" EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>" OnRowDataBound="grd_Books_RowDataBound" CssClass="table table-striped moe-full-width moe-table result-table margin-top-25">
        <Columns>
            <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, BookID %>">
                <ItemTemplate>
                    <asp:HiddenField ID="hdn_RequestID" runat="server" Value='<%# Eval("RequestID") %>' />
                    <asp:HiddenField ID="hdn_ID" runat="server" Value='<%# Eval("ID") %>' />
                    <asp:Label ID="lbl_BookID" runat="server" Text='<%# Eval("BookID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, BookDate %>">
                <ItemTemplate>
            <asp:Label ID="lbl_BookDate" runat="server" Text='<%# Convert.ToDateTime(Eval("BookDate"))!=DateTime.MinValue ? Convert.ToDateTime(Eval("BookDate")).ToShortDateString():string.Empty %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, BookSubject %>">
                <ItemTemplate>
                    <asp:Label ID="lbl_BookSubject" runat="server" Text='<%# Eval("BookSubject") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, BookDirectedTo %>">
                <ItemTemplate>
                    <asp:Label ID="lbl_DirectedTo" runat="server" Text='<%# Eval("BookDirectedTo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, BookAuthor %>">
                <ItemTemplate>
                    <asp:Label ID="lbl_BookAuthor" runat="server" Text='<%# Eval("BookAuthor") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, EntityReplyDate %>">
                <ItemTemplate>
            <asp:Label ID="lbl_OrgReplyDate" runat="server" Text='<%# Convert.ToDateTime(Eval("OrgReplyDate"))!=DateTime.MinValue ? Convert.ToDateTime(Eval("OrgReplyDate")).ToShortDateString():string.Empty %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, EntityReplyBookID %>">
                <ItemTemplate>
                    <asp:Label ID="lbl_OrgReplyBookNo" runat="server" Text='<%# Eval("OrgReplyBookNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, AddOrgReply %>">
                <ItemTemplate>
                    <asp:LinkButton ID="lnk_OrgReply" runat="server" Tooltip="<%$Resources:ITWORX_MOEHEWF_UCE, OrgReply %>" OnClick="lnk_OrgReply_Click" CssClass="display-icon fa fa-reply"/>
                    <asp:LinkButton ID="lnk_ViewDetails" runat="server" Tooltip="<%$Resources:ITWORX_MOEHEWF_UCE, ViewDetails %>" OnClick="lnk_ViewDetails_Click" CssClass="display-icon fa fa-eye"/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

	<div class="row no-padding">
	<h4 class="font-size-18 font-weight-600 pull-right">
		<asp:Label ID="lbl_NoOfRequests" runat="server"></asp:Label>
	</h4>
</div>

</asp:Panel>
