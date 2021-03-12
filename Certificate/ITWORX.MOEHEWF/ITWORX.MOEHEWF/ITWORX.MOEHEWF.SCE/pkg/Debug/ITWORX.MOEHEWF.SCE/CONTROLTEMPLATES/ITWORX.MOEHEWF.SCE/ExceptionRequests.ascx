<%@ Assembly Name="ITWORX.MOEHEWF.SCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7c6ec0a86ef11fff" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExceptionRequests.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE.ExceptionRequests" %>


<style>
    .disp { display:block !important
    }
</style>
<asp:Label ID="lblExceptionMessage" runat="server"  Text="<%$Resources:ITWORX_MOEHEWF_SCE, ExceptionRequestsMessage %>"></asp:Label>

<asp:Label ID="lblNewRequests" runat="server"  Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequestsListing %>" Font-Bold="true"></asp:Label>
<asp:HyperLink ID="hypNewRequest" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, NewRequest %>"  CssClass="moe-btn btn" />
<asp:GridView ID="grd_SCExceptionRequests" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="grd_SCExceptionRequests_PageIndexChanging" 
    ShowHeaderWhenEmpty="true" EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>" OnRowDataBound="grd_SCExceptionRequests_RowDataBound" CssClass="table table-striped moe-full-width moe-table result-table">
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
         <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateHolderQatarID %>">
            <ItemTemplate >
                <asp:Label ID="lbl_HolderQatarID" runat="server"  Text='<%#  Eval("CertificateHolderQatarID")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, ExcepApplicantName %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ApplicantNameFromCert" runat="server"  Text='<%#  Eval("StudentNameAccToCert")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateSource %>">
            <ItemTemplate> 
                <asp:Label ID="lbl_CertificateSource" runat="server" Text='<%#  Eval("CertificateResource")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
          <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, LastGrade%>">
            <ItemTemplate>
                <asp:Label ID="lbl_LastGrade" runat="server"  Text='<%#  Eval("SchoolLastGrade")%>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
          <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, Nationality%>">
            <ItemTemplate>
                <asp:Label ID="lbl_Nationality" runat="server"  Text='<%#  Eval("Nationality")%>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, ExcepPhoneNumber %>">
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
