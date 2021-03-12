<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExceptionRequests.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE.ExceptionRequests" %>
<%@ Assembly Name="ITWORX.MOEHE.Utilities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=942948f6a64aa526" %>
<%@ Import Namespace="ITWORX.MOEHE.Utilities" %>


<div class="row margin-top-15">
    <div class="col-xs-12 warningMsg">
        <h5 class="font-size-14 margin-bottom-0 margin-top-0 instruction-details color-black font-family-sans">
            <asp:Label ID="lblExceptionMessage" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ExceptionRequestsMessage %>"></asp:Label>

        </h5>
    </div>
</div>

<div class="row margin-top-15">
    <div class="col-sm-6">
        
        <h4 class="font-weight-600 font-size-18 text-left ">
            <asp:Label ID="lblNewRequests" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequestsListing %>" Font-Bold="true"></asp:Label>
        </h4>

    </div>
    <div class="col-sm-6 text-right">
        <asp:HyperLink ID="hypNewRequest" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, NewRequest %>" CssClass="school-btn btn pull-right" />
    </div>
     <div class="col-sm-12">
            <h4 class="font-weight-600 font-size-18 text-right numOfReuq">
                <asp:Label ID="lbl_NoOfRequests" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, NoOfRequests %>"></asp:Label></h4>
        </div>
</div>



<asp:GridView ID="grd_SCExceptionRequests" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="grd_SCExceptionRequests_PageIndexChanging"
    ShowHeaderWhenEmpty="true" EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>" OnRowDataBound="grd_SCExceptionRequests_RowDataBound" CssClass="table table-striped moe-full-width moe-table result-table">
    <Columns>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, RequestNumber %>"  HeaderStyle-Width="100">
            <ItemTemplate>
                <asp:HiddenField ID="hdn_RequestStatusId" runat="server" Value='<%#  Eval("RequestStatusId")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_IsClosed" runat="server" Value='<%#  Eval("IsRequestClosed")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_AssignedTo" runat="server" Value='<%#  Eval("AssignedTo")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_ID" runat="server" Value='<%#  Eval("RequestID")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_QID" runat="server" Value='<%#  Eval("QID")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_DelayedDays" runat="server" Value='<%#  Eval("DelayedDays")%>'></asp:HiddenField>
                <asp:Label ID="lbl_RequestID" runat="server" Text='<%#  Eval("RequestNumber")%>'></asp:Label>
           
                </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, RecievedDate %>">
            <ItemTemplate>
                <asp:Label ID="lbl_RequestRecievedDate" runat="server" Text='<%# Convert.ToDateTime(Eval("RecievedDate"))!=DateTime.MinValue ?  Convert.ToDateTime(Eval("RecievedDate")).QatarFormatedDate() +Convert.ToDateTime(Eval("RecievedDate")).QatarFormatedDateReturnTime():string.Empty %>' ToolTip='<%# Convert.ToDateTime(Eval("RecievedDate"))!=DateTime.MinValue ?  Convert.ToDateTime(Eval("RecievedDate")).QatarFormatedDate() +Convert.ToDateTime(Eval("RecievedDate")).QatarFormatedDateReturnTime():string.Empty %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <%--<asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateHolderQatarID %>">
            <ItemTemplate>
                <asp:Label ID="lbl_HolderQatarID" runat="server" Text='<%#  Eval("CertificateHolderQatarID")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>--%>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, ExcepApplicantName %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ApplicantNameFromCert" runat="server" Text='<%#  Eval("ApplicantName")%>' ToolTip='<%#  Eval("ApplicantName")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, Nationality%>">
            <ItemTemplate>
                <asp:Label ID="lbl_Nationality" runat="server" Text='<%#  Eval("Nationality")%>' ToolTip='<%#  Eval("Nationality")%>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateSource %>">
            <ItemTemplate>
                <asp:Label ID="lbl_CertificateSource" runat="server" Text='<%#  Eval("CertificateResource")%>' ToolTip='<%#  Eval("CertificateResource")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, LastGrade%>">
            <ItemTemplate>
                <asp:Label ID="lbl_LastGrade" runat="server" Text='<%#  Eval("SchoolLastGrade")%>' ToolTip='<%#  Eval("SchoolLastGrade")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, RequestStatus %>">
            <ItemTemplate>
                <asp:Label ID="lbl_RequestStatus" runat="server" Text='<%#  Eval("RequestStatus")%>' ToolTip='<%#  Eval("RequestStatus")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, Action %>">
            <ItemTemplate>
                <asp:LinkButton ID="lnk_Edit" runat="server" OnClick="lnk_Edit_Click" CssClass="edit-icon fa fa-pencil-square-o" ToolTip="<%$Resources:ITWORX_MOEHEWF_SCE, Edit %>"></asp:LinkButton>
                <%-- <asp:LinkButton ID="lnkDisplay" runat="server" ToolTip="<%$Resources:ITWORX_MOEHEWF_SCE, Display %>"  OnClick="lnkDisplay_Click" CssClass="display-icon fa fa-eye"></asp:LinkButton>--%>
                <asp:Literal ID="litExc" runat="server" Visible="false">
                    <i  class="fa fa-exclamation-circle" style="color:red" ></i>
                </asp:Literal>
                 <asp:LinkButton ID="lnkDelete" runat="server" ToolTip="<%$Resources:ITWORX_MOEHEWF_SCE, Delete %>" OnClick="lnkDelete_Click"  OnClientClick="return ConfirmOnDelete();" CssClass="delete-icon fa fa-trash" ></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
       
<div class="row margin-bottom-25">
    <div class="col-xs-12 text-right">
        <asp:Button ID="btnExportExcel" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, ExportExcel %>" OnClick="btnExportExcel_Click" OnClientClick="return setFormSubmitToFalse();" ClientIDMode="Static" CssClass="btn school-btn" />
    </div>
</div>

<script type="text/javascript">
    function ConfirmOnDelete() {
        var dialogText = "<%= DeleteConfirmation %>";

         if (confirm(dialogText) == true)
             return true;
         else
             return false;
     }
</script>