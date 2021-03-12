<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Assembly Name="ITWORX.MOEHE.Utilities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=942948f6a64aa526" %>

<%@ Import Namespace="ITWORX.MOEHE.Utilities"  %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReturnedRequests.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.ReturnedRequests" %>
<style>
    .modal-header .close{font-size:32px;}
    .modal-header .close:hover{background-color:transparent;padding:0;}
    .modal-Paragraph{white-space:pre-wrap;font-size:16px;}
</style>
<script type="text/javascript" src='<%= ResolveUrl ("~/Style%20Library/Scripts/jquery.session.js") %>'></script>
<div class="col-md-12 no-padding">
	<h4 class="font-size-18 font-weight-600 text-right">
		<asp:Label ID="lbl_NoOfRequests" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NoOfRequests %>"></asp:Label>
	</h4>
</div>
<asp:HiddenField runat="server" ID="pbcontrol" />

<asp:GridView ID="grd_ReturnedRequests" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="grd_ReturnedRequests_PageIndexChanging"
     ShowHeaderWhenEmpty="true" EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>" OnRowDataBound="grd_ReturnedRequests_RowDataBound" CssClass="table moe-table table-striped result-table">
    <Columns>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, SerialNumber %>">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, RequestNumber %>">
            <ItemTemplate>
                <asp:HiddenField ID="hdn_RequestStatusId" runat="server" Value='<%#  Eval("RequestStatusId")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_IsClosed" runat="server" Value='<%#  Eval("IsRequestClosed")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_AssignedTo" runat="server" Value='<%#  Eval("AssignedTo")%>'></asp:HiddenField>
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
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, ReturnDate %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ReturnDate" runat="server" Text='<%# Convert.ToDateTime(Eval("RejectionDate"))!=DateTime.MinValue ? Convert.ToDateTime(Eval("RejectionDate")).ToShortDateString():string.Empty %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, ReturnReason %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ReturnReason" runat="server" Text='<%# HelperMethods.LocalizedText("ITWORX_MOEHEWF_UCE", Eval("RejectionReason").ToString(), (uint)LCID) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, ReturnedFrom %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ReturnedFrom" runat="server" Text='<%#  Eval("RejectedFrom")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, Action %>">
            <ItemTemplate>
                <asp:LinkButton ID="lnk_Edit" runat="server" ToolTip="<%$Resources:ITWORX_MOEHEWF_UCE, Edit %>" OnClick="lnk_Edit_Click" OnClientClick="LnkClick(this.id)"  CssClass="edit-icon fa fa-pencil-square-o"></asp:LinkButton>
                <asp:LinkButton ID="lnk_View" runat="server" ToolTip="<%$Resources:ITWORX_MOEHEWF_UCE, View %>"  OnClick="lnk_View_Click"  OnClientClick="LnkClick(this.id)"  CssClass="display-icon fa fa-eye"></asp:LinkButton>
                <asp:LinkButton ID="lnk_Notes" runat="server" data-note='<%# Eval("Note") %>'  data-toggle="modal" data-target="#NotesModal"   
                    CssClass="moe-btn-notes edit-icon fa fa-info-circle" ToolTip="<%$Resources:ITWORX_MOEHEWF_UCE, Notes %>"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<!-- Modal -->
<div class="modal fade" id="NotesModal" tabindex="-1" role="dialog" aria-labelledby="NotesModalLabel">
  <div class="modal-dialog modalPopup" role="document">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title font-size-22" id="NotesModalLabel"><asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Notes %>"></asp:Literal></h4>
      </div>
      <div class="modal-body">
        <p class="modal-Paragraph" class="font-size-16">
           
        </p>
      </div>
  </div>
</div>


<input type="hidden" value="" id="__EventTriggerControlId" name="__EventTriggerControlId"/>
<script type="text/javascript">
<!--
function LnkClick(eventControl)
{
    debugger;
    var ctlId = document.getElementById("__EventTriggerControlId");
    if (ctlId) {
        ctlId.value = eventControl;
    }
    
   
}
// -->
    $(document).ready(function () {
        $('#NotesModal').on('show.bs.modal', function (event) {
            var button = $(event.relatedTarget) // Button that triggered the modal
            var recipient = button.data('note') // Extract info from data-* attributes
            var modal = $(this)
            //console.log(button.data('note'))
            //modal.find('.modal-title').text('New message to ' + recipient)
            modal.find('.modal-body .modal-Paragraph').html(recipient)
        })
    });
</script>