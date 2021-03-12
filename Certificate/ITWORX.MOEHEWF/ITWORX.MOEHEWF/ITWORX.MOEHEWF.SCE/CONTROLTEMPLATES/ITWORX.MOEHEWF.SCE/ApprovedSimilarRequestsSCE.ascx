<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApprovedSimilarRequestsSCE.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE.ApprovedSimilarRequestsSCE" %>

 <div class="row">
     <%--   <div class="col-xs-6">
            <h2 class="tab-title text-left  margin-top-0  margin-bottom-5">
                <asp:Label ID="Label1" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ClarificationRequests %>"></asp:Label></h2>
        </div>--%>

        <div class="col-xs-12">
            <h4 class="font-weight-600 font-size-18 text-right numOfReuq">
                <asp:Label ID="lbl_NoOfRequests" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, NoOfRequests %>"></asp:Label></h4>
        </div>
    </div>
<div class="row">
    <div class="col-xs-12">
        <asp:GridView ID="grd_ApprovedResults" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="grd_ApprovedResults_PageIndexChanging"
            ShowHeaderWhenEmpty="true" OnRowDataBound="grd_ApprovedResults_RowDataBound" EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>" CssClass="table moe-table table-striped result-table">
            <Columns>

                <asp:TemplateField>
                    <ItemTemplate>
                     <asp:HiddenField ID="hdn_AppID" runat="server" Value='<%#  Eval("Id")%>'></asp:HiddenField>
                         <asp:HiddenField ID="hdnRequestStatus" runat="server" Value='<%#  Eval("RequestStatusId")%>'></asp:HiddenField>
                        <asp:HiddenField ID="hdnRegisteredLevel" runat="server" Value='<%#  Eval("RegisteredSchoolId")%>' />
                         <%--  <asp:HiddenField ID="hdn_ReqId" runat="server" Value='<%#  Eval("RequestIDId")%>'></asp:HiddenField>--%>
                     
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField  HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, SerialNumber %>">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, RequestNumber %>">
                    <ItemTemplate>
                         <asp:HyperLink ID="lnk_ReqNumber" runat="server" Target="_blank" NavigateUrl='<%#  Eval("RequestUrl")%>' ToolTip='<%#  Eval("RequestNumber")%>'><%#  Eval("RequestNumber")%></asp:HyperLink>
                        <%--<asp:Label ID="lbl_RequestNumber" runat="server" Text='<%#  Eval("RequestNumber")%>'></asp:Label>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, EquationDate %>">
                    <ItemTemplate>
                        <asp:Label ID="lbl_RequestDate" runat="server" Text='<%#  Eval("RequestDate")%>' ToolTip='<%#  Eval("RequestDate")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, StudentNumber %>">
                    <ItemTemplate>
                        <asp:Label ID="lbl_StudentNumber" runat="server" Text='<%# Eval("ApplicantNumber") %>' ToolTip='<%# Eval("ApplicantNumber") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, StudentName %>">
                    <ItemTemplate>
                        <asp:Label ID="lbl_StudentName" runat="server" Text='<%#  Eval("ApplicantName")%>' ToolTip='<%#  Eval("ApplicantName")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, Nationality %>">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Nationality" runat="server" Text='<%#  Eval("Nationality")%>' ToolTip='<%#  Eval("Nationality")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateSource %>">
                    <ItemTemplate>
                        <asp:Label ID="lbl_CertificateSource" runat="server" Text='<%#  Eval("CertificateOrigin")%>' ToolTip='<%#  Eval("CertificateOrigin")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, SchoolType %>">
                    <ItemTemplate>
                        <asp:Label ID="lbl_SchoolType" runat="server" Text='<%#  Eval("SchoolType")%>' ToolTip='<%#  Eval("SchoolType")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateType %>">
                    <ItemTemplate>
                        <asp:Label ID="lbl_CertificateType" runat="server" Text='<%#  Eval("CertificateType")%>' ToolTip='<%#  Eval("CertificateType")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, DecisionCopy %>">
                    <ItemTemplate>                       
                        <asp:LinkButton ID="lnk_View" runat="server" OnClientClick="LnkClickApproved(this.id)" ToolTip="<%$Resources:ITWORX_MOEHEWF_SCE, View %>" OnClick="lnk_View_Click" CssClass="display-icon fa fa-eye"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, ChooseAsAttach %>">
                    <ItemTemplate>                       
                        <asp:CheckBox ID="chk_AppReqs" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
         <div class="text-right">
        <asp:Button ID="btn_RemoveAttached" OnClientClick="LnkClickApproved(this.id)" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RemoveAttachReq %>" OnClick="btn_RemoveAttached_Click" />
             </div>
    </div>
</div>

<input type="hidden" value="" id="__EventTriggerApprovedControl" name="__EventTriggerApprovedControl"/>
<script type="text/javascript">
<!--
function LnkClickApproved(eventControl)
{
  //  debugger;
    var ctlId = document.getElementById("__EventTriggerApprovedControl");
    if (ctlId) {
        ctlId.value = eventControl;
    }
    
    window.WebForm_OnSubmit = function () { return true; };
}
// -->
</script>