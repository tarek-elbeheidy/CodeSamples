<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SCEBooksRequestsUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.WebParts.SCEBooksRequests.SCEBooksRequestsUserControl" %>


<style>
    .disp {
        display: block !important
    }
</style>
<div class="tab-pane tab-padd">
    <asp:Label ID="lblExceptionMessage" runat="server" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ExceptionBooksRequestsMessage %>"></asp:Label>
    <div class="row">
        <div class="col-xs-12 text-right">
            <asp:Button ID="hypNewBookRequest" runat="server" OnClick="btn_NewRequest_Click" Text="<%$Resources:ITWORX_MOEHEWF_SCE, NewBook %>" CssClass="btn school-btn margin-top-25 margin-bottom-10"></asp:Button>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-6">
            <h2 class="tab-title text-left">
                <asp:Label ID="Label1" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ExternalComm %>"></asp:Label>
                <asp:Label ID="lblNewBookRequests" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequestsListing %>" Font-Bold="true"></asp:Label>
            </h2>
        </div>
        <div class="col-xs-6">
            <h2 class="font-weight-600 font-size-18 text-right">
                <asp:Label ID="lbl_NoOfRequests" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, NoOfRequests %>"></asp:Label>
            </h2>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <asp:GridView ID="grd_SCEExternalBookRequests" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="SCEExternalBook_PageIndexChanging"
                ShowHeaderWhenEmpty="true" EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>" OnRowDataBound="grd_SCEBookRequests_RowDataBound" CssClass="table table-striped moe-full-width moe-table result-table">
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, SCEBookNumber %>">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdn_BookRequestStatusId" runat="server" Value='<%#  Eval("Bookstatus")%>'></asp:HiddenField>
                            <asp:HiddenField ID="hdn_ID" runat="server" Value='<%#  Eval("ID")%>'></asp:HiddenField>
                            <asp:Label ID="lbl_BookRequestID" runat="server" Text='<%#  Eval("BookNumber")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, SCEBookDate %>">
                        <ItemTemplate>
                            <asp:Label ID="lbl_BookRequestSsendDate" runat="server" Text='<%#  Eval("BookDate")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, SCEBookSubject %>">
                        <ItemTemplate>
                            <asp:Label ID="lbl_BookSubject" runat="server" Text='<%#  Eval("BookSubject")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, SCEAuthority %>">
                        <ItemTemplate>
                            <asp:Label ID="lbl_EAuthorityName" runat="server" Text='<%#  Eval("BookEntityTitle")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, SCEReplyDate %>">
                        <ItemTemplate>
                            <asp:Label ID="lbl_SCEReplyDate" runat="server" Text='<%#Eval("ReplyDate")%> '></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, SCEReplyNumber%>">
                        <ItemTemplate>
                            <asp:Label ID="lbl_SCEReplyNumber" runat="server" Text='<%#  Eval("ReplyNumber")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, Action %>" HeaderStyle-Width="100">
                        <ItemTemplate>

                            <asp:LinkButton ID="lnk_View" runat="server" OnClick="lnk_View_Click" CssClass="edit-icon fa fa-eye" ToolTip="<%$Resources:ITWORX_MOEHEWF_SCE, View %>"></asp:LinkButton>
                            <asp:LinkButton ID="lnk_Edit" runat="server" OnClick="lnk_Edit_Click" CssClass="edit-icon fa fa-pencil-square-o" ToolTip="<%$Resources:ITWORX_MOEHEWF_SCE, Edit %>"></asp:LinkButton>
                            <asp:LinkButton ID="lnk_AddReply" runat="server" OnClick="lnk_AddReply_Click" CssClass="edit-icon fa fa-reply" ToolTip="<%$Resources:ITWORX_MOEHEWF_SCE, AddReply %>"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</div>
