<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%--<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.PA/PAAdd_Clarification.ascx" TagPrefix="uc1" TagName="Add_Clarification" %>--%>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PAClarRequestsListing.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.ClarRequestsListing" %>

<div class="col-md-12 no-padding margin-bottom-25">

    <asp:LinkButton ID="lnk_AddNewClarPopUp" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, AddNewClarRequest %>" OnClick="lnk_AddNewClarPopUp_Click" CssClass="btn moe-btn pull-right" Visible="false" />
</div>

<div class="row">
    <div class="col-md-12">
        <h4 class="font-weight-600 font-size-18 text-right">
    <asp:Label ID="lbl_NoOfRequests" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, NoOfRequests %>" Visible="false"></asp:Label>
</h4>
    </div>
</div>

<asp:GridView ID="grd_ClarRequests" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="grd_ClarRequests_PageIndexChanging"
    ShowHeaderWhenEmpty="true" EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>" OnRowDataBound="grd_ClarRequests_RowDataBound" CssClass="table moe-table table-striped result-table">
    <Columns>
       <%-- <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, RequestID %>">
            <ItemTemplate>
                
                <asp:Label ID="lbl_RequestID" runat="server" Text='<%#  Eval("RequestID")%>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>--%>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, ClarRequestedDate %>">
            <ItemTemplate>
                <asp:HiddenField ID="hdn_AssignedTo" runat="server" Value='<%#  Eval("AssignedTo")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_ID" runat="server" Value='<%#  Eval("ID")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_RequestID" runat="server" Value='<%#  Eval("RequestID")%>'></asp:HiddenField>
                <asp:Label ID="lbl_ClarRequestedDate" runat="server" Text='<%# Convert.ToDateTime(Eval("RequestClarificationDate"))!=DateTime.MinValue ? Convert.ToDateTime(Eval("RequestClarificationDate")).ToShortDateString():string.Empty %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, ClarRequestSender %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ClarRequestSender" runat="server" Text='<%# Eval("RequestSender") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, ClarRequested %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ClarRequested" runat="server" Text='<%#  Eval("RequestedClarification")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, ClarReply %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ClarReply" runat="server" Text='<%#  Eval("ClarificationReply")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, ReplyDate %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ReplyDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ReplyDate"))!=DateTime.MinValue ? Convert.ToDateTime(Eval("ReplyDate")).ToShortDateString():string.Empty %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, Action %>">
            <ItemTemplate>
                <asp:LinkButton ID="lnk_ReplytoClarification" runat="server"  OnClick="lnk_ReplytoClarification_Click" CssClass="edit-icon fa fa-reply" ToolTip="<%$Resources:ITWORX_MOEHEWF_PA, View %>"></asp:LinkButton>
                <asp:LinkButton ID="lnk_View" runat="server" OnClick="lnk_View_Click" CssClass="display-icon fa fa-eye" ToolTip="<%$Resources:ITWORX_MOEHEWF_PA, View %>"></asp:LinkButton>
                <%--<asp:LinkButton ID="lnk_View" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, View %>" OnClick="lnk_View_Click" CssClass="edit-icon fa fa-eye-o" ToolTip="<%$Resources:ITWORX_MOEHEWF_PA, View %>">></asp:LinkButton>--%>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

