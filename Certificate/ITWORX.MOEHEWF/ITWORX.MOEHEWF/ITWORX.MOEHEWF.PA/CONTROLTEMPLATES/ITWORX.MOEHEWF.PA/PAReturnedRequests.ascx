<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PAReturnedRequests.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.PAReturnedRequests" %>

<div class="row no-padding">
	<div class="col-md-12">
        <h4 class="font-size-18 font-weight-600 text-right">
        <asp:Label ID="lbl_NoOfRequests" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, NoOfRequests %>"></asp:Label>
	</h4>
	</div>
</div>

<asp:GridView ID="grd_ReturnedRequests" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="grd_ReturnedRequests_PageIndexChanging"
     ShowHeaderWhenEmpty="true" EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>" OnRowDataBound="grd_ReturnedRequests_RowDataBound" CssClass="table moe-table table-striped result-table">
    <Columns> 
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, RequestNumber %>">
            <ItemTemplate>
                <asp:HiddenField ID="hdn_RequestStatusId" runat="server" Value='<%#  Eval("RequestStatusId")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_IsClosed" runat="server" Value='<%#  Eval("IsRequestClosed")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_AssignedTo" runat="server" Value='<%#  Eval("AssignedTo")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_ID" runat="server" Value='<%#  Eval("ID")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_QID" runat="server" Value='<%#  Eval("QID")%>'></asp:HiddenField>
                <asp:Label ID="lbl_RequestID" runat="server" Text='<%#  Eval("RequestNumber")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, QatariID %>">
            <ItemTemplate>
                <asp:Label ID="lbl_QatariID" runat="server" Text='<%#  Eval("QatariID")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, ApplicantName %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ApplicantName" runat="server" Text='<%# Eval("ApplicantName") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, Nationality %>">
            <ItemTemplate>
                <asp:Label ID="lbl_Nationality" runat="server" Text='<%#  Eval("Nationality")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
<%--        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, Certificate %>">
            <ItemTemplate>
                <asp:Label ID="lbl_AcademicDegree" runat="server" Text='<%#  Eval("AcademicDegreeForEquivalence")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>--%>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, ReturnDate %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ReturnDate" runat="server" Text='<%# Convert.ToDateTime(Eval("RejectionDate"))!=DateTime.MinValue ? Convert.ToDateTime(Eval("RejectionDate")).ToShortDateString():string.Empty %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, ReturnReason %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ReturnReason" runat="server" Text='<%#  Eval("RejectionReason")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, ReturnedFrom %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ReturnedFrom" runat="server" Text='<%#  Eval("RejectedFrom")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, Action %>">
            <ItemTemplate>
                <asp:LinkButton ID="lnk_Edit" runat="server" ToolTip="<%$Resources:ITWORX_MOEHEWF_PA, Edit %>"  CssClass="edit-icon fa fa-pencil-square-o" OnClick="lnk_Edit_Click" OnClientClick="LnkClickReturnRequest(this.id)"></asp:LinkButton>
                <asp:LinkButton ID="lnk_View" runat="server" ToolTip="<%$Resources:ITWORX_MOEHEWF_PA, View %>"  CssClass="display-icon fa fa-eye" OnClick="lnk_View_Click" OnClientClick="LnkClickReturnRequest(this.id)"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>


<input type="hidden" value="" id="__EventTriggerControlIdReturn" name="__EventTriggerControlIdReturn"/>
<script type="text/javascript">
<!--
function LnkClickReturnRequest(eventControl)
{
    debugger;
    var ctlId = document.getElementById("__EventTriggerControlIdReturn");
    if (ctlId) {
        ctlId.value = eventControl;
    }
    
   
}
// -->
</script>