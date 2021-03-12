<%@ Assembly Name="ITWORX.MOEHEWF.SCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7c6ec0a86ef11fff" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReturnedRequests.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE.ReturnedRequests" %>



<div class="row no-padding">
	<div class="col-md-12">
        <h4 class="font-size-18 font-weight-600 text-right">
        <asp:Label ID="lbl_NoOfRequests" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, NoOfRequests %>"></asp:Label>
	</h4>
	</div>
</div>

<asp:GridView ID="grd_ReturnedRequests" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="grd_ReturnedRequests_PageIndexChanging"
     ShowHeaderWhenEmpty="true" EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>" OnRowDataBound="grd_ReturnedRequests_RowDataBound" CssClass="table moe-table table-striped result-table">
    <Columns> 
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, RequestNumber %>">
            <ItemTemplate>
                <asp:HiddenField ID="hdn_RequestStatusId" runat="server" Value='<%#  Eval("RequestStatusId")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_IsClosed" runat="server" Value='<%#  Eval("IsRequestClosed")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_AssignedTo" runat="server" Value='<%#  Eval("AssignedTo")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_ID" runat="server" Value='<%#  Eval("ID")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_QID" runat="server" Value='<%#  Eval("QID")%>'></asp:HiddenField>
                <asp:Label ID="lbl_RequestID" runat="server" Text='<%#  Eval("RequestNumber")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, QatariID %>">
            <ItemTemplate>
                <asp:Label ID="lbl_QatariID" runat="server" Text='<%#  Eval("QatariID")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, ApplicantName %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ApplicantName" runat="server" Text='<%# Eval("ApplicantName") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, Nationality %>">
            <ItemTemplate>
                <asp:Label ID="lbl_Nationality" runat="server" Text='<%#  Eval("Nationality")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, ReturnDate %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ReturnDate" runat="server" Text='<%# Convert.ToDateTime(Eval("RejectionDate"))!=DateTime.MinValue ? Convert.ToDateTime(Eval("RejectionDate")).ToShortDateString():string.Empty %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, ReturnReason %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ReturnReason" runat="server" Text='<%#  Eval("RejectionReason")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, ReturnedFrom %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ReturnedFrom" runat="server" Text='<%#  Eval("RejectedFrom")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, Action %>">
            <ItemTemplate>
                <asp:LinkButton ID="lnk_Edit" runat="server" ToolTip="<%$Resources:ITWORX_MOEHEWF_SCE, Edit %>"  CssClass="edit-icon fa fa-pencil-square-o" OnClick="lnk_Edit_Click"></asp:LinkButton>
                <asp:LinkButton ID="lnk_View" runat="server" ToolTip="<%$Resources:ITWORX_MOEHEWF_SEC, View %>"  CssClass="display-icon fa fa-eye" OnClick="lnk_View_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

