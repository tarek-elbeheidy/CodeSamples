<%@ Assembly Name="ITWORX.MOEHEWF.UCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=883afb4c05a35fe5" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClarificationRequests.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.ClarificationRequests" %>

<div class="row no-padding">
	<h4 class="font-size-18 font-weight-600 pull-right">
		<asp:Label ID="lbl_NoOfRequests" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NoOfRequests %>"></asp:Label>
	</h4>
</div>

<asp:GridView ID="grd_ClarRequests" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="grd_ClarRequests_PageIndexChanging"
    ShowHeaderWhenEmpty="true" EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>"  CssClass="table moe-table table-striped result-table margin-top-5">
    <Columns>
          <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, SerialNumber %>">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>

              <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, RequestNumber %>">
            <ItemTemplate>
<%--                   <asp:HiddenField ID="hdn_RequestStatusId" runat="server" Value='<%#  Eval("RequestStatusId")%>'></asp:HiddenField>--%>
<%--                <asp:HiddenField ID="hdn_IsClosed" runat="server" Value='<%#  Eval("IsRequestClosed")%>'></asp:HiddenField>--%>
                <asp:HiddenField ID="hdn_ReqID" runat="server" Value='<%#  Eval("RequestID")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_ID" runat="server" Value='<%#  Eval("ID")%>'></asp:HiddenField>
             <asp:Label ID="lbl_RequestID" runat="server" Text='<%#  Eval("RequestNumber")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, QatariID %>">
            <ItemTemplate>
                <asp:Label ID="lbl_QatariID" runat="server" Text='<%#  Eval("QatariID")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, ApplicantName %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ApplicantName" runat="server" Text='<%# Eval("ApplicantName") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, Nationality %>">
            <ItemTemplate>
                <asp:Label ID="lbl_Nationality" runat="server" Text='<%#  Eval("Nationality")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, Certificate %>">
            <ItemTemplate>
                <asp:Label ID="lbl_AcademicDegree" runat="server" Text='<%#  Eval("AcademicDegree")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, EntityNeedsCertificate %>">
            <ItemTemplate>
                <asp:Label ID="lbl_EntityNeedsCertificate" runat="server" Text='<%# Eval("EntityNeedsEquivalency") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, Country %>">
            <ItemTemplate>
                <asp:Label ID="lbl_Country" runat="server" Text='<%# Eval("Country") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityName %>">
            <ItemTemplate>
                <asp:Label ID="lbl_University" runat="server" Text='<%# Eval("University") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, FacultyName %>">
            <ItemTemplate>
                <asp:Label ID="lbl_Faculty" runat="server" Text='<%# Eval("Faculty") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
 
         <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, ClarRequested %>">
            <ItemTemplate>
                <asp:Label ID="lbl_RequiredClarRequested" runat="server" Text='<%# Eval("RequestedClarification") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, ClarRequestedDate %>">
            <ItemTemplate>
                <asp:Label ID="lbl_RequiredClarDate" runat="server" Text='<%# Convert.ToDateTime(Eval("RequestClarificationDate"))!=DateTime.MinValue ? Convert.ToDateTime(Eval("RequestClarificationDate")).ToShortDateString():string.Empty %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
       <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, Action %>">
            <ItemTemplate>
                <asp:LinkButton ID="lnk_View" runat="server" ToolTip="<%$Resources:ITWORX_MOEHEWF_UCE, View %>" OnClick="lnk_View_Click" OnClientClick="LnkClickClarRequest(this.id)" CssClass="display-icon fa fa-eye"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<input type="hidden" value="" id="__EventTriggerControlIdClar" name="__EventTriggerControlIdClar"/>
<script type="text/javascript">
<!--
function LnkClickClarRequest(eventControl)
{
    debugger;
    var ctlId = document.getElementById("__EventTriggerControlIdClar");
    if (ctlId) {
        ctlId.value = eventControl;
    }
    
   
}
// -->
</script>
