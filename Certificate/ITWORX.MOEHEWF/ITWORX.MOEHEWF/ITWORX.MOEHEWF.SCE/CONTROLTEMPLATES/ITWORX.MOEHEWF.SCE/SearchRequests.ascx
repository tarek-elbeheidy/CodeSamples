<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchRequests.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE.SearchRequests" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/DDLWithTXTWithNoPostback.ascx" TagPrefix="uc1" TagName="DDLWithTXTWithNoPostback" %>


<style>
    .modalBackground {
        background-color: Black;
        filter: alpha(opacity=90);
        opacity: 0.8;
    }
</style>

<asp:HiddenField ID="hdnhdn_DateFrom" runat="server" />
<asp:HiddenField ID="hdnhdn_DateTo" runat="server" />

<div id="SrchControls">


    <div class="tab-pane">
                            <div class="row">
                                <div class="col-xs-12">
                                    <h2 class="tab-title">
                                               <asp:Label ID="Label1" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, searchRequests %>"></asp:Label> 
                                                
                                    
                                    
                                    </h2>
                                </div>
                            </div>
                            <div class="dark-bg tab-pane-wrap">
                                <div class="row margin-top-15">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label><asp:Label ID="lbl_RequestID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequestID %>"></asp:Label>
                                            
                                            
                                            </label>
                                            <asp:TextBox ID="txt_RequestID" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="regVal_NumbersOnly" ControlToValidate="txt_RequestID" ValidationExpression="^[a-zA-Z0-9_.-]*$" ValidationGroup="Srch"
                            Display="Dynamic" EnableClientScript="true" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, NumbersaAndAlphaOnly %>" runat="server" CssClass="error-msg moe-full-width" />
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label><asp:Label ID="lbl_QatarID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateHolderQatarID %>"></asp:Label>
                                            
                                            
                                            </label>
                                            <asp:TextBox ID="txt_QatarID" runat="server" MaxLength="11" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="regVal_NumbersOnlyID" ControlToValidate="txt_QatarID" ValidationExpression="\d+" ValidationGroup="Srch" Display="Dynamic"
                            EnableClientScript="true" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, NumbersOnly %>" runat="server" CssClass="error-msg moe-full-width" />
                    
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label><asp:Label ID="lbl_StudentName" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentNameCert %>"></asp:Label>
                                            
                                            
                                            </label>
                                            <asp:TextBox ID="txt_StudentName" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42" ></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row margin-top-15">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="lbl_PhoneNumber" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ExcepPhoneNumber %>  "></asp:Label>
                                            
                                            
                                            </label>
                                            <asp:TextBox ID="txt_PhoneNumber" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42" ></asp:TextBox>
                                            
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="lbl_Nationality" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Nationality %>"></asp:Label>  
                                            
                                            
                                            </label>
                                            <asp:DropDownList ID="drp_Nationality" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42" EnableViewState="false"></asp:DropDownList>                                        </div>
                                    </div>
                                    
                                    <div class="col-md-4">
                                        <div class="form-group"> 
                                            <uc1:DDLWithTXTWithNoPostback runat="server" id="certificateResource" />
                                        </div>
                                    </div>
                                </div>

                                <div class="row margin-top-15">
                                    <div class="col-md-4">
                                        <div class="form-group">
 
                                            <uc1:DDLWithTXTWithNoPostback runat="server" id="schooleType" />


                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">

                                            <uc1:DDLWithTXTWithNoPostback runat="server" id="certificateType" />

                                            </div>
                                    </div>
                                    
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>
                                                
                                            <asp:Label ID="lbl_RequestStatus" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequestStatus %> "></asp:Label>
                                            
                                            </label>
                                            <asp:DropDownList ID="drp_RequestStatus" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42" EnableViewState="false"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="row margin-top-15">
                                    <div class="col-md-4">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label><asp:Label ID="lbl_DateFrom" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubmitDateFrom %>"></asp:Label>
                                                    
                                                    
                                                    </label>
                                                     
                                                                           <input type="text" id="dt_DateFrom" readonly="readonly" class="form-control datepicker moe-srch-datepicker">
                        <asp:HiddenField ID="hdn_DateFrom" runat="server"/>
						
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label><asp:Label ID="lbl_DateTo" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubmitDateTo %> "></asp:Label>
                                                    
                                                    
                                                    </label>
                                                                             <input type="text" id="dt_DateTo" readonly="readonly" class="form-control datepicker moe-full-width moe-input-padding moe-select input-height-42 moe-srch-datepicker">
                        <asp:HiddenField ID="hdn_DateTo" runat="server"/>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label><asp:Label ID="lbl_Employee" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Employee %>"></asp:Label>
                                            
                                            
                                            </label>
                                          <asp:DropDownList ID="drp_Employees" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42" EnableViewState="false"></asp:DropDownList>
                                            </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="row">
                                            <label class="visibility-hidden">Button
                                            
                                            
                                            </label>
                                        </div>
                                        <div class="row text-center">
                                            

                                              <asp:Button ID="btn_Cancel" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, ChangeSearchCriteria %>" OnClick="btn_Cancel_Click" CssClass="btn moe-btn clear-btn" />

                                            <asp:Button ID="btn_Search" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Search %>" OnClick="btn_Search_Click" ClientIDMode="Static" CssClass="btn moe-btn" />
                                        </div>
                                    </div>
                                </div>
                            </div> 
                        </div>

<h4 runat="server" id="searchLimit" class="pull-left search-result-filter">
    <asp:Label ID="Label2" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, chooseSearchcriteria %>"></asp:Label> 
</h4>
 
 
                      

</div>

<div class="row margin-top-15">
    <div class="col-sm-6">
        <h4 class="font-weight-600 font-size-18 text-left "><asp:Label ID="lblNewRequests" runat="server" Visible="false"  Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequestsListing %>" Font-Bold="true"></asp:Label>
        </h4>
    </div>
    <div class="col-sm-6 text-right">
        <h4 class="font-weight-600 font-size-16 text-right numOfReuq"><asp:Label ID="lbl_NoOfRequests" Visible="false" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, NoOfRequests %>"></asp:Label>
                                            
                                            </h4> 
    </div>
</div>



<asp:GridView ID="grd_Requests" runat="server" AutoGenerateColumns="false" AllowPaging="true" DataKeyNames="ID"
    OnPageIndexChanging="grd_Requests_PageIndexChanging" OnRowDataBound="grd_Requests_RowDataBound" ShowHeaderWhenEmpty="true"
    EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>" CssClass="table moe-table table-striped result-table">
    <Columns> 
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, RequestNumber %>"  HeaderStyle-Width="100">
            <ItemTemplate>
                <asp:HiddenField ID="hdn_RequestStatusId" runat="server" Value='<%#  Eval("RequestStatusId")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_IsClosed" runat="server" Value='<%#  Eval("IsRequestClosed")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_AssignedTo" runat="server" Value='<%#  Eval("AssignedTo")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_ID" runat="server" Value='<%#  Eval("ID")%>'></asp:HiddenField>
                <asp:Label ID="lbl_RequestID" runat="server" Text='<%#  Eval("RequestNumber")%>' ToolTip='<%#  Eval("RequestNumber")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, RecieveRequestDate %>">
            <ItemTemplate>
                <asp:Label ID="lbl_RequestSubmitDate" runat="server" ToolTip='<%# Convert.ToDateTime(Eval("RecievedDate"))!=DateTime.MinValue ? Convert.ToDateTime(Eval("RecievedDate")).ToShortDateString():string.Empty %>' Text='<%# Convert.ToDateTime(Eval("RecievedDate"))!=DateTime.MinValue ? Convert.ToDateTime(Eval("RecievedDate")).ToShortDateString():string.Empty %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateHolderQatarID %>">
            <ItemTemplate>
                <asp:Label ID="lbl_QatariID" runat="server" Text='<%#  Eval("CertificateHolderQatarID")%>' ToolTip='<%#  Eval("CertificateHolderQatarID")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, ExcepApplicantName %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ApplicantName" runat="server" Text='<%#  Eval("StudentNameAccToCert")%>' ToolTip='<%#  Eval("StudentNameAccToCert")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, LastGrade %>">
            <ItemTemplate>
                <asp:Label ID="lbl_Nationality" runat="server" Text='<%#  Eval("SchoolLastGrade")%>' ToolTip='<%#  Eval("SchoolLastGrade")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, Nationality %>">
            <ItemTemplate>
                <asp:Label ID="lbl_Country" runat="server" Text='<%# Eval("Nationality") %>' ToolTip='<%# Eval("Nationality") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, ResposibleEmployee %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ResEmployee" runat="server" Text='<%# Eval("AssignedTo") %>' ToolTip='<%# Eval("Nationality") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, RequestStatus %>">
            <ItemTemplate>
                <asp:Label ID="lbl_Status" runat="server" Text='<%# Eval("RequestStatus") %>' ToolTip='<%# Eval("Nationality") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, ExcepPhoneNumber %>">
            <ItemTemplate>
                <asp:Label ID="lbl_RequestStatus" runat="server" Text='<%#  Eval("ApplicantMobileNumber")%>' ToolTip='<%#  Eval("ApplicantMobileNumber")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField> 
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, Action %>">
            <ItemTemplate>
                <asp:LinkButton ID="lnk_Edit" runat="server" Tootltip="<%$Resources:ITWORX_MOEHEWF_SCE, Edit %>" CssClass="edit-icon fa fa-pencil-square-o" OnClick="lnk_Edit_Click"></asp:LinkButton>
                <asp:LinkButton ID="lnk_View" runat="server" ToolTip="<%$Resources:ITWORX_MOEHEWF_SCE, View %>" CssClass="display-icon fa fa-eye" OnClick="lnk_View_Click"></asp:LinkButton>
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
    $(function () {


        $("#dt_DateFrom").datepicker({
            dateFormat: "d/m/yy",
            showOn: 'focus',
            showButtonPanel: true,
            closeText: 'Clear',
            changeYear: true,
            changeMonth: true,
            onClose: function () {
                var event = arguments.callee.caller.caller.arguments[0];
                // If "Clear" gets clicked, then really clear it
                if ($(event.delegateTarget).hasClass('ui-datepicker-close')) {
                    $(this).val('');
                }
            },
            onSelect: function () {
                var dt2 = $('#dt_DateTo');
                var startDate = $(this).datepicker('getDate');
                //add 30 days to selected date
                startDate.setDate(startDate.getDate() + 30);
                var minDate = $(this).datepicker('getDate');
                //first day which can be selected in dt2 is selected date in dt1
                dt2.datepicker('option', 'minDate', minDate);
            }
        });
        $('#dt_DateTo').datepicker({
            dateFormat: "d/m/yy",
            showOn: 'focus',
            showButtonPanel: true,
            closeText: 'Clear',
            changeYear: true,
            changeMonth: true,
            onClose: function () {
                var event = arguments.callee.caller.caller.arguments[0];
                // If "Clear" gets clicked, then really clear it
                if ($(event.delegateTarget).hasClass('ui-datepicker-close')) {
                    $(this).val('');
                }
            }
        });

        if ($("#<%=hdn_DateFrom.ClientID%>").val() != "") {
            $("#dt_DateFrom").val($("#<%=hdn_DateFrom.ClientID%>").val());
        }
        if ($("#<%=hdn_DateTo.ClientID%>").val() != "") {
                    $("#dt_DateTo").val($("#<%=hdn_DateTo.ClientID%>").val());
                }
                $("#btn_Search").click(function () {
                    $("#<%=hdn_DateFrom.ClientID%>").val($("#dt_DateFrom").val());
            $("#dt_DateFrom").val($("#<%=hdn_DateFrom.ClientID%>").val());

            $("#<%=hdn_DateTo.ClientID%>").val($("#dt_DateTo").val());
            $("#dt_DateTo").val($("#<%=hdn_DateTo.ClientID%>").val());
        });
         
    });
</script>



