<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SCEStatementListingUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.WebParts.SCEStatementListing.SCEStatementListingUserControl" %>
<div class="tab-pane fade tab-padd active in" id="statementReq" role="tabpanel">
    <div class="row">
        <div class="col-xs-12 text-right">
            <asp:Button ID="btn_AddSCEStatement" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, addNewStatement %>" CssClass="btn school-btn margin-top-25 margin-bottom-10" OnClick="AddSCEStatement_Click" />
        </div>
    </div>

    <div class="row">
        <div class="col-xs-6">
            <h2 class="tab-title text-left">
                <asp:Label ID="Label1" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StatementList %>"></asp:Label>
            </h2>
        </div>
        <div class="col-xs-6">
            <h2 class="font-weight-600 font-size-18 text-right numOfReuq">
                <asp:Label ID="lbl_NoOfRequests" runat="server" Text=""></asp:Label>
            </h2>
        </div>
    </div>
    <div class="row table-wrapper">
        <asp:GridView ID="grd_StatementRequests" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnRowDataBound="grd_SCEStatRequests_RowDataBound"
            ShowHeaderWhenEmpty="true" EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>" CssClass="table moe-table table-striped result-table">
            <Columns>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, StatementDate %>">
                    <ItemTemplate>
                        <asp:HiddenField ID="hdn_ReqID" runat="server" Value='<%#  Eval("RequestID")%>'></asp:HiddenField>
                        <asp:HiddenField ID="hdn_ID" runat="server" Value='<%#  Eval("ID")%>'></asp:HiddenField>
                        <asp:Label ID="lbl_StatementDate" runat="server" Text='<%# Eval("StatementDate")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, StatementSender %>">
                    <ItemTemplate>
                        <asp:Label ID="lbl_StatementSender" runat="server" Text='<%#  Eval("Sender")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, StatementSubject %>">
                    <ItemTemplate>
                        <asp:Label ID="lbl_StatementSubject" runat="server" Text='<%# Eval("StatementSubject") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, StatementAgency %>">
                    <ItemTemplate>
                        <asp:Label ID="lbl_StatementAgency" runat="server" Text='<%#  Eval("StatementAgency")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, ReplyDate %>">
                    <ItemTemplate>
                        <asp:Label ID="lbl_ReplyDate" runat="server" Text='<%#  Eval("ReplayDate")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, SenderOfReplay %>">
                    <ItemTemplate>
                        <asp:Label ID="lbl_SenderOfReplay" runat="server" Text='<%#  Eval("ReplaySender")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, Action %>">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_ReplytoStatement" runat="server" ToolTip="<%$Resources:ITWORX_MOEHEWF_SCE, ReplytoClarification %>" CssClass="display-icon fa fa-reply" OnClick="lnk_ReplytoStatement_Click"></asp:LinkButton>
                        <asp:LinkButton ID="lnk_View" runat="server" ToolTip="<%$Resources:ITWORX_MOEHEWF_SCE, View %>" CssClass="edit-icon fa fa-eye" OnClick="lnk_View_Click"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>


    </div>


</div>
