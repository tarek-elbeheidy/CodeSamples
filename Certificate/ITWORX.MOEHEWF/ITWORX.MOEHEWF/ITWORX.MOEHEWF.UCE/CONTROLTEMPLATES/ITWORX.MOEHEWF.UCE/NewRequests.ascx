<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>


<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewRequests.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.NewRequests" %>
<style>
    .modal-header .close{font-size:32px;}
    .modal-header .close:hover{background-color:transparent;padding:0;}
    .modal-Paragraph{white-space:pre-wrap;font-size:16px;}
</style>
<%--<link rel="stylesheet" href='<%= ResolveUrl ("~/Style%20Library/CSS/jquery-ui.css") %>'>
<script type="text/javascript" src='<%= ResolveUrl ("~/Style%20Library/Scripts/jquery-1.12.4.js") %>'></script>
<script type="text/javascript" src='<%= ResolveUrl ("~/Style%20Library/Scripts/jquery-ui.js") %>'></script>
<script type="text/javascript" src='<%= ResolveUrl ("~/Style%20Library/Scripts/jquery.cookie.js") %>'></script>--%>

<div class="row no-padding">
	<div class="col-xs-12">
			<h4 class="font-size-18 font-weight-600 text-right"><asp:Label ID="lbl_NoOfRequests" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NoOfRequests %>"></asp:Label></h4>
	</div>
</div>

<asp:GridView ID="grd_NewRequests" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="grd_NewRequests_PageIndexChanging" 
    ShowHeaderWhenEmpty="true" EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>" OnRowDataBound="grd_NewRequests_RowDataBound" CssClass="table table-striped moe-full-width moe-table result-table">
    <Columns>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, ChooseValue %>">
            <ItemTemplate>
                <asp:CheckBox ID="chkbox_Select" runat="server" />
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
                <asp:Label ID="lbl_HEDD" runat="server" Text=''></asp:Label>

            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, FacultyName %>">
            <ItemTemplate>
                <asp:Label ID="lbl_Faculty" runat="server" Text='<%# Eval("Faculty") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, SubmitDate %>">
            <ItemTemplate>
                <asp:Label ID="lbl_RequestSubmitDate" runat="server" Text='<%# Convert.ToDateTime(Eval("SubmitDate"))!=DateTime.MinValue ? Convert.ToDateTime(Eval("SubmitDate")).ToShortDateString():string.Empty %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, Action %>">
            <ItemTemplate>
                <!--Without Text-->
                <asp:LinkButton ID="lnk_Edit" runat="server" OnClick="lnk_Edit_Click" OnClientClick="LnkClickNewRequest(this.id)" CssClass="edit-icon fa fa-pencil-square-o" ToolTip="<%$Resources:ITWORX_MOEHEWF_UCE, Edit %>"></asp:LinkButton>
                <asp:LinkButton ID="lnk_CheckOut" runat="server" OnClick="lnk_CheckOut_Click" OnClientClick="LnkClickNewRequest(this.id)" CssClass="fa fa-check-square-o check-icon" ToolTip="<%$Resources:ITWORX_MOEHEWF_UCE, CheckOut %>"></asp:LinkButton>
                <asp:LinkButton ID="lnk_Notes" runat="server" data-note='<%# Eval("Note") %>'  data-toggle="modal" data-target="#NotesModal"   
                    CssClass="moe-btn-notes edit-icon fa fa-info-circle" ToolTip="<%$Resources:ITWORX_MOEHEWF_UCE, Notes %>"></asp:LinkButton>

				<%--<asp:LinkButton ID="lnk_View" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, View %>" OnClick="lnk_View_Click" CssClass="fa fa-check-square-o check-icon" ToolTip="<%$Resources:ITWORX_MOEHEWF_UCE, View %>"></asp:LinkButton>--%>
                <!--With Text-->
                <%--<asp:LinkButton ID="lnk_Edit" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Edit %>" OnClick="lnk_Edit_Click" CssClass="edit-icon fa fa-pencil-square-o"></asp:LinkButton>
                <asp:LinkButton ID="lnk_CheckOut" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CheckOut %>" OnClick="lnk_CheckOut_Click" CssClass="fa fa-check-square-o check-icon"></asp:LinkButton>--%>
<%--                <asp:LinkButton ID="lnk_View" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, View %>" OnClick="lnk_View_Click"></asp:LinkButton>--%>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>



<div runat="server" id="EmpAssignTo" class="row no-padding">

	<div class="col-md-3 col-sm-6 margin-top-10 margin-bottom-10">
				<div class="data-container table-display moe-width-95">
					<div class="form-group">
						<h6 class="font-size-16 margin-bottom-10 margin-top-15">
    <asp:Label ID="lbl_AssignToEmp" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, AssignToEmpName %>"></asp:Label>
 
						</h6>
						  <asp:DropDownList ID="drp_AssignTo" runat="server" CssClass="moe-dropdown moe-full-width input-height-42 border-box moe-input-padding"></asp:DropDownList>
      <asp:RequiredFieldValidator ID="reqVal_RequiredValue" runat="server" CssClass="error-msg moe-full-width" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredValue %>"
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
						 		<asp:LinkButton ID="lnk_AssignTo" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, AssignedTolnk %>" OnClick="lnk_AssignTo_Click" ValidationGroup="AssignTo" CssClass="btn moe-btn pull-left"></asp:LinkButton>
                       
					</div>
				</div>
    </div>
    

</div>


<div class="row no-padding margin-top-15">
	<h5 class="font-size-18 font-weight-600 success-msg">
		<asp:Label ID="lbl_SuccessMsg" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, AssignedSuccessfully %>"></asp:Label>
	</h5>
</div>


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

<input type="hidden" value="" id="__EventTriggerControlIdNew" name="__EventTriggerControlIdNew"/>
<script type="text/javascript">
    <!--
    function LnkClickNewRequest(eventControl)
    {
        debugger;
        var ctlId = document.getElementById("__EventTriggerControlIdNew");
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