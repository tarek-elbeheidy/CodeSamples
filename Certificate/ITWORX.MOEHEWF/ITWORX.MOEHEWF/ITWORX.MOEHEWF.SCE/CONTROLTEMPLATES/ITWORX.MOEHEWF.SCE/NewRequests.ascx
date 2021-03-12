<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewRequests.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE.NewRequests" %>
<%@ Assembly Name="ITWORX.MOEHE.Utilities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=942948f6a64aa526" %>
<%@ Import Namespace="ITWORX.MOEHE.Utilities"  %>



<div class="row margin-top-15">
    <div class="col-sm-6">
        <h4 class="font-weight-600 font-size-18 text-left "><asp:Label ID="lblNewRequests" runat="server"  Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequestsListing %>" Font-Bold="true"></asp:Label>
        </h4>
    </div>
    <div class="col-sm-6 text-right">
        <h4 class="font-weight-600 font-size-16 text-right numOfReuq"><asp:Label ID="lbl_NoOfRequests" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, NoOfRequests %>"></asp:Label>
                                            
        </h4>
    </div>
</div>




<asp:GridView ID="grd_SCENewRequests" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="grd_SCENewRequests_PageIndexChanging" 
    ShowHeaderWhenEmpty="true" EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>" OnRowDataBound="grd_SCENewRequests_RowDataBound" CssClass="table table-striped moe-full-width moe-table result-table">
    <Columns> 
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, RequestNumber %>"  HeaderStyle-Width="100">
            <ItemTemplate>
                <asp:HiddenField ID="hdn_RequestStatusId" runat="server" Value='<%#  Eval("RequestStatusId")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_IsClosed" runat="server" Value='<%#  Eval("IsRequestClosed")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_AssignedTo" runat="server" Value='<%#  Eval("AssignedTo")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_ID" runat="server" Value='<%#  Eval("ID")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_QID" runat="server" Value='<%#  Eval("QID")%>'></asp:HiddenField>
                <asp:Label ID="lbl_RequestID" runat="server" Text='<%#  Eval("RequestNumber")%>' ToolTip='<%#  Eval("RequestNumber")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
           <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, RecievedDate %>">
            <ItemTemplate>
                <%--<asp:Label ID="lbl_RequestRecievedDate" runat="server" Text='<%# Convert.ToDateTime(Eval("RecievedDate"))!=DateTime.MinValue ?  Convert.ToDateTime(Eval("RecievedDate")).QatarFormatedDate() +Convert.ToDateTime(Eval("RecievedDate")).QatarFormatedDateReturnTime():string.Empty %>'></asp:Label>--%>
                <asp:Label ID="lbl_RequestRecievedDate" runat="server" Text='<%# LCID == (int)Language.English?ConvertDateCalendar(Convert.ToDateTime(Eval("RecievedDate")), "Gregorian", "en-US") + ExtensionMethods.QatarFormatedDateReturnTime(Convert.ToDateTime(Eval("RecievedDate"))):ToArabicDigits(ConvertDateCalendar(Convert.ToDateTime(Eval("RecievedDate")), "Gregorian", "en-US")) + ToArabicDigits(ExtensionMethods.QatarFormatedDateReturnTime(Convert.ToDateTime(Eval("RecievedDate")))).Replace("م", "مساء").Replace("ص", "صباحا") %>' ToolTip='<%# LCID == (int)Language.English?ConvertDateCalendar(Convert.ToDateTime(Eval("RecievedDate")), "Gregorian", "en-US") + ExtensionMethods.QatarFormatedDateReturnTime(Convert.ToDateTime(Eval("RecievedDate"))):ToArabicDigits(ConvertDateCalendar(Convert.ToDateTime(Eval("RecievedDate")), "Gregorian", "en-US")) + ToArabicDigits(ExtensionMethods.QatarFormatedDateReturnTime(Convert.ToDateTime(Eval("RecievedDate")))).Replace("م", "مساء").Replace("ص", "صباحا") %>'></asp:Label>
            
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateHolderQatarID %>">
            <ItemTemplate >
                <asp:Label ID="lbl_HolderQatarID" runat="server"  Text='<%#  Eval("CertificateHolderQatarID")%>' ToolTip='<%#  Eval("CertificateHolderQatarID")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
           <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, ApplicantNameAccToCert %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ApplicantNameFromCert" runat="server"  Text='<%#  Eval("StudentNameAccToCert")%>' ToolTip='<%#  Eval("StudentNameAccToCert")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
          <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, Nationality %>">
            <ItemTemplate>
                <asp:Label ID="lbl_Nationality" runat="server"  Text='<%#  Eval("Nationality")%>' ToolTip='<%#  Eval("Nationality")%>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
          <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateSource %>">
            <ItemTemplate> 
                <asp:Label ID="lbl_CertificateSource" runat="server" Text='<%#  Eval("CertificateResource")%>' ToolTip='<%#  Eval("CertificateResource")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, LastGrade%>">
            <ItemTemplate>
                <asp:Label ID="lbl_LastGrade" runat="server"  Text='<%#  Eval("SchoolLastGrade")%>' ToolTip='<%#  Eval("SchoolLastGrade")%>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, ApplicantMobileNumber %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ApplicantMobileNumber" runat="server" Text='<%#  Eval("ApplicantMobileNumber")%>' ToolTip='<%#  Eval("ApplicantMobileNumber")%>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
      
      <%--  <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, QatariID %>">
            <ItemTemplate>
                <asp:Label ID="lbl_QatariID" runat="server" Text='<%#  Eval("QatariID")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, ApplicantName %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ApplicantName" runat="server" Text='<%# Eval("ApplicantName") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>--%>
        
     
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, Action %>">
            <ItemTemplate>
                <asp:LinkButton ID="lnk_Edit" runat="server" OnClick="lnk_Edit_Click" CssClass="edit-icon fa fa-pencil-square-o" ToolTip="<%$Resources:ITWORX_MOEHEWF_SCE, Edit %>"></asp:LinkButton>
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
    //To handle the freezing of page after downloading a file
    function setFormSubmitToFalse() {
        setTimeout(function () { _spFormOnSubmitCalled = false; }, 3000);
        return true;
    }
</script>