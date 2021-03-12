<%@ Assembly Name="ITWORX.MOEHEWF.SCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7c6ec0a86ef11fff" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewRequests.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE.NewRequests" %>



<style>
    .disp { display:block !important
    }
</style>

<div class="row">
    <div class="col-md-12">
        <h4 class="font-weight-600 font-size-18 text-right">
    <asp:Label ID="lbl_NoOfRequests" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, NoOfRequests %>"></asp:Label>
</h4>
    </div>
</div>

<asp:Label ID="lblNewRequests" runat="server"  Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequestsListing %>" Font-Bold="true"></asp:Label>
<asp:GridView ID="grd_SCENewRequests" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="grd_SCENewRequests_PageIndexChanging" 
    ShowHeaderWhenEmpty="true" EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>" OnRowDataBound="grd_SCENewRequests_RowDataBound" CssClass="table table-striped moe-full-width moe-table result-table">
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
           <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, SubmitDate %>">
            <ItemTemplate>
                <asp:Label ID="lbl_RequestSubmitDate" runat="server" Text='<%# Convert.ToDateTime(Eval("SubmitDate"))!=DateTime.MinValue ? Convert.ToDateTime(Eval("SubmitDate")).ToShortDateString():string.Empty %>'></asp:Label>
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
         <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateHolderQatarID %>">
            <ItemTemplate >
                <asp:Label ID="lbl_HolderQatarID" runat="server"  Text='<%#  Eval("CertificateHolderQatarID")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, ApplicantNameAccToCert %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ApplicantNameFromCert" runat="server"  Text='<%#  Eval("StudentNameAccToCert")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
          <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, NationalityCategory %>">
            <ItemTemplate>
                <asp:Label ID="lbl_NationalityCategory" runat="server"  Text='<%#  Eval("NationalityCategory")%>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateSource %>">
            <ItemTemplate> 
                <asp:Label ID="lbl_CertificateSource" runat="server" Text='<%#  Eval("CertificateResource")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, SchoolName %>">
            <ItemTemplate>
                <asp:Label ID="lbl_SchoolName" runat="server" Text='<%#  Eval("School")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, ApplicantMobileNumber %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ApplicantMobileNumber" runat="server" Text='<%#  Eval("ApplicantMobileNumber")%>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
      
     
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, Action %>">
            <ItemTemplate>
                <asp:LinkButton ID="lnk_Edit" runat="server" OnClick="lnk_Edit_Click" CssClass="edit-icon fa fa-pencil-square-o" ToolTip="<%$Resources:ITWORX_MOEHEWF_SCE, Edit %>"></asp:LinkButton>
               </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView> 



