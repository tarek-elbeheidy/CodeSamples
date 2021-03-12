<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReassignRequests.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE.ReassignRequests" %>

<%@ Assembly Name="ITWORX.MOEHE.Utilities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=942948f6a64aa526" %>
<%@ Import Namespace="ITWORX.MOEHE.Utilities"  %>


<div class="row no-padding">
	<div class="col-md-12">
        <h4 class="font-size-18 font-weight-600 text-right">
        <asp:Label ID="lbl_NoOfRequests" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, NoOfRequests %>"></asp:Label>
	</h4>
	</div>
</div>

<asp:GridView ID="grd_ReassignRequests" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="grd_ReassignRequests_PageIndexChanging"

     ShowHeaderWhenEmpty="true" EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>"  CssClass="table moe-table table-striped result-table">
    <Columns> 
        <asp:TemplateField  HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, Choose %>">
             <ItemTemplate>
                 <asp:CheckBox runat="server" ID="chkAssign" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, RequestNumber %>"  HeaderStyle-Width="100">
            <ItemTemplate>
                <asp:HiddenField ID="hdn_RequestStatusId" runat="server" Value='<%#  Eval("RequestStatusId")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_IsClosed" runat="server" Value='<%#  Eval("IsRequestClosed")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_AssignedTo" runat="server" Value='<%#  Eval("AssignedTo")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_ID" runat="server" Value='<%#  Eval("RequestID")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_QID" runat="server" Value='<%#  Eval("QID")%>'></asp:HiddenField>
                <asp:Label ID="lbl_RequestID" runat="server" Text='<%#  Eval("RequestNumber")%>' ToolTip='<%#  Eval("RequestNumber")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, RecievedDate %>">
            <ItemTemplate>
                <asp:Label ID="lbl_RequestRecievedDate" runat="server" ToolTip='<%# Convert.ToDateTime(Eval("RecievedDate"))!=DateTime.MinValue ? Convert.ToDateTime(Eval("RecievedDate")).QatarFormatedDate() +Convert.ToDateTime(Eval("RecievedDate")).QatarFormatedDateReturnTime():string.Empty %>' Text='<%# Convert.ToDateTime(Eval("RecievedDate"))!=DateTime.MinValue ? Convert.ToDateTime(Eval("RecievedDate")).QatarFormatedDate() +Convert.ToDateTime(Eval("RecievedDate")).QatarFormatedDateReturnTime():string.Empty %>'></asp:Label>
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
          <%-- <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, LastGrade%>">
            <ItemTemplate>
                <asp:Label ID="lbl_LastGrade" runat="server"  Text='<%#  Eval("SchoolLastGrade")%>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, ApplicantMobileNumber %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ApplicantMobileNumber" runat="server" Text='<%#  Eval("ApplicantMobileNumber")%>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>--%>

          
          
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, ReturnedFrom %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ReturnedFrom" runat="server"  Text='<%#  Eval("ReturnedFrom")%>' ToolTip='<%#  Eval("ReturnedFrom")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, ReturnReason %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ReturnReason" runat="server" Text='<%#  Eval("ReturnReason")%>' ToolTip='<%#  Eval("ReturnReason")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
       <%-- <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, ReturnDate %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ReturnDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ReturnDate"))!=DateTime.MinValue ? Convert.ToDateTime(Eval("ReturnDate")).ToShortDateString():string.Empty %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>--%>


       
       
    
        
    
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, Action %>">
            <ItemTemplate>
               <%-- <asp:LinkButton ID="lnk_Edit" runat="server" ToolTip="<%$Resources:ITWORX_MOEHEWF_SCE, Edit %>"  CssClass="edit-icon fa fa-pencil-square-o" OnClick="lnk_Edit_Click"></asp:LinkButton>--%>
                <asp:LinkButton ID="lnk_View" runat="server" ToolTip="<%$Resources:ITWORX_MOEHEWF_SCE, View %>"  CssClass="display-icon fa fa-eye" OnClick="lnk_View_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>


<div runat="server" id="EmpAssignTo" class="row no-padding">

	<div class="col-md-3 col-sm-6 margin-top-10 margin-bottom-10">
				<div class="data-container table-display moe-width-95">
					<div class="form-group">
						<h6 class="font-size-16 margin-bottom-10 margin-top-15">
    <asp:Label ID="lbl_AssignToEmp" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, AssignToEmpName %>"></asp:Label>
 
						</h6>
						  <asp:DropDownList ID="drp_AssignTo" runat="server" CssClass="moe-dropdown moe-full-width input-height-42 border-box moe-input-padding"></asp:DropDownList>
      <asp:RequiredFieldValidator ID="reqVal_RequiredValue" runat="server" Display="Dynamic" CssClass="error-msg moe-full-width" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, RequiredValue %>"
        ValidationGroup="AssignTo" ControlToValidate="drp_AssignTo"></asp:RequiredFieldValidator>
					</div>
				</div>
			</div>


    <div class="col-md-3 col-sm-6 margin-top-10 margin-bottom-10">
		<div class="data-container table-display moe-width-95 pull-right">
					<div class="form-group">
						<h6 class="font-size-16 margin-bottom-10 margin-top-15" style="visibility:hidden;">
							dd
						</h6>
						 		<asp:LinkButton ID="lnk_AssignTo" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, AssignedTolnk %>"  ValidationGroup="AssignTo" CssClass="btn moe-btn pull-left" OnClick="lnk_AssignTo_Click"></asp:LinkButton>
                         <asp:CustomValidator ID="custAssignCheck" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, ChooseRequest %>"   ValidationGroup="AssignTo" Display="Dynamic" CssClass="error-msg moe-full-width" OnServerValidate="custAssignCheck_ServerValidate"></asp:CustomValidator>
					</div>
				</div>
    </div>
    

</div>

<div class="row no-padding margin-top-15">
	<h5 class="font-size-18 font-weight-600 success-msg">
		<asp:Label ID="lbl_SuccessMsg" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, AssignedSuccessfully %>"></asp:Label>
	</h5>
</div>


<%--<input type="hidden" value="" id="__EventTriggerControlIdReassign" name="__EventTriggerControlIdReassign"/>
<script type="text/javascript">
<!--
function LnkClickReassignRequest(eventControl)
{
    debugger;
    var ctlId = document.getElementById("__EventTriggerControlIdReassign");
    if (ctlId) {
        ctlId.value = eventControl;
    }
    
   
}
// -->
</script>--%>


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