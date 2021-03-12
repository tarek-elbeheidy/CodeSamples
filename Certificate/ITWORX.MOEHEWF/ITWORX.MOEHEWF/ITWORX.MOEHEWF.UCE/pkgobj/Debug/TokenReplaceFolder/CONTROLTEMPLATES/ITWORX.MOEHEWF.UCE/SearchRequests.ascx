<%@ Assembly Name="ITWORX.MOEHEWF.UCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=883afb4c05a35fe5" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchRequests.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.SearchRequests" %>

<%--<script type="text/javascript" src='<%= ResolveUrl ("~/Style%20Library/Scripts/jquery.min.js") %>'></script>
 <link rel="stylesheet" href='<%= ResolveUrl ("~/Style%20Library/CSS/jquery-ui.css") %>'>
<script type="text/javascript" src='<%= ResolveUrl ("~/Style%20Library/Scripts/jquery-1.12.4.js") %>'></script>
<script type="text/javascript" src='<%= ResolveUrl ("~/Style%20Library/Scripts/jquery-ui.js") %>'></script>--%>
<style>
    /*button.export-btn{
        background-color: transparent;
        border: none;
    }*/
    button.export-btn:before {
        content: 'd';
    }

    .modalBackground {
        background-color: Black;
        filter: alpha(opacity=90);
        opacity: 0.8;
    }
</style>

<div id="SrchControls" runat="server">

    <div class="row no-padding heighlighted-section margin-bottom-50 flex-display flex-wrap">
        <div class="col-md-4 col-sm-6 col-xs-12 no-padding">
            <div class="data-container table-display moe-full-width">
                <div class="form-group">
                    <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                        <asp:Label ID="lbl_RequestID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RequestID %>"></asp:Label>
                    </h6>

                    <div class="form">
                        <asp:TextBox ID="txt_RequestID" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="regVal_NumbersOnly" ControlToValidate="txt_RequestID" ValidationExpression="^[a-zA-Z0-9_.-]*$" ValidationGroup="Srch"
                            Display="Static" EnableClientScript="true" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, NumbersaAndAlphaOnly %>" runat="server" CssClass="error-msg moe-full-width" />
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-4 col-sm-6 col-xs-12 no-padding">
            <div class="data-container table-display moe-full-width">
                <div class="form-group">
                    <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                        <asp:Label ID="lbl_NationalID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, QatariID %>"></asp:Label>
                    </h6>

                    <div class="form">
                        <asp:TextBox ID="txt_NationalID" runat="server" MaxLength="11" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="regVal_NumbersOnlyID" ControlToValidate="txt_NationalID" ValidationExpression="\d+" ValidationGroup="Srch" Display="Static"
                            EnableClientScript="true" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, NumbersOnly %>" runat="server" CssClass="error-msg moe-full-width" />
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-4 col-sm-6 col-xs-12 no-padding">
            <div class="data-container table-display moe-full-width">
                <div class="form-group">
                    <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                        <asp:Label ID="lbl_ApplicantName" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ApplicantName %>"></asp:Label>
                    </h6>

                    <div class="form">

                        <asp:TextBox ID="txt_ApplicantName" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-4 col-sm-6 col-xs-12 no-padding">
            <div class="data-container table-display moe-full-width">
                <div class="form-group">
                    <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                        <asp:Label ID="lbl_Nationality" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Nationality %>  "></asp:Label>
                    </h6>

                    <div class="form">
                        <asp:DropDownList ID="drp_Nationality" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42" EnableViewState="false"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-4 col-sm-6 col-xs-12 no-padding">
            <div class="data-container table-display moe-full-width">
                <div class="form-group">
                    <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                        <asp:Label ID="lbl_DateFrom" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, SubmitDateFrom %>"></asp:Label>
                    </h6>

                    <div class="form">
                        <input type="text" id="dt_DateFrom" readonly="readonly" class="moe-full-width moe-input-padding moe-select input-height-42">
                        <asp:HiddenField ID="hdn_DateFrom" runat="server" ClientIDMode="Static" />
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-4 col-sm-6 col-xs-12 no-padding">
            <div class="data-container table-display moe-full-width">
                <div class="form-group">
                    <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                        <asp:Label ID="lbl_DateTo" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, SubmitDateTo %> "></asp:Label>
                    </h6>

                    <div class="form">
                        <input type="text" id="dt_DateTo" readonly="readonly" class="moe-full-width moe-input-padding moe-select input-height-42">
                        <asp:HiddenField ID="hdn_DateTo" runat="server" ClientIDMode="Static" />
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-4 col-sm-6 col-xs-12 no-padding">
            <div class="data-container table-display moe-full-width">
                <div class="form-group">
                    <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                        <asp:Label ID="lbl_NationCategory" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NationalityCategory %> "></asp:Label>
                    </h6>

                    <div class="form">
                        <asp:DropDownList ID="drp_NationCategory" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42" EnableViewState="false"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-4 col-sm-6 col-xs-12 no-padding">
            <div class="data-container table-display moe-full-width">
                <div class="form-group">
                    <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                        <asp:Label ID="lbl_Certificate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Certificate %>"></asp:Label>
                    </h6>

                    <div class="form">
                        <asp:DropDownList ID="drp_Certificate" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42" EnableViewState="false"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-4 col-sm-6 col-xs-12 no-padding">
            <div class="data-container table-display moe-full-width">
                <div class="form-group">
                    <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                        <asp:Label ID="lbl_Country" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Country %> "></asp:Label>
                    </h6>

                    <div class="form">
                        <asp:DropDownList ID="drp_Country" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42" onchange="ddl_Countrychanged()"  OnSelectedIndexChanged="drp_Country_SelectedIndexChanged" AutoPostBack="true" EnableViewState="false">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-4 col-sm-6 col-xs-12 no-padding">

            <div class="data-container table-display moe-full-width">
                <div class="form-group">

                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                                <asp:Label ID="lbl_University" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityName %>"></asp:Label>
                            </h6>
                            <div class="form">
                                <asp:DropDownList ID="drp_University" runat="server" onchange="ddl_changed()"  AutoPostBack="false" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42" EnableViewState="false"></asp:DropDownList>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="drp_Country" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <div class="col-md-4 col-sm-6 col-xs-12 no-padding">

            <div class="data-container table-display moe-full-width">
                <div class="form-group">

                    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                                     <asp:Label ID="lbl_Faculty" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, FacultyName %> "></asp:Label>
                                </h6>
                                <div class="form">

                <asp:DropDownList ID="drp_Faculty" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drp_Faculty_SelectedIndexChanged" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="drp_University" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
                    <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                        <asp:Label ID="facultylbl" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, FacultyName %>"></asp:Label>
                    </h6>

                    <div class="form">

                        <asp:TextBox ID="ddlFaculty" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-4 col-sm-6 col-xs-12 no-padding">

            <div class="data-container table-display moe-full-width">
                <div class="form-group">

                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                                <asp:Label ID="lbl_Specialization" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Specialization %> "></asp:Label>
                            </h6>
                            <div class="form">
                                <asp:DropDownList ID="drp_Specialization" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42" EnableViewState="false"></asp:DropDownList>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <%--<asp:AsyncPostBackTrigger ControlID="drp_Faculty" EventName="SelectedIndexChanged" />--%>
                            <asp:AsyncPostBackTrigger ControlID="drp_University" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <div class="col-md-4 col-sm-6 col-xs-12 no-padding">
            <div class="data-container table-display moe-full-width">
                <div class="form-group">
                    <h6 class="font-size-16 margin-bottom-10 margin-top-10">

                        <asp:Label ID="lbl_EntityNeedsEquivalency" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EntityNeedsEquivalency %> "></asp:Label>
                    </h6>

                    <div class="form">
                        <asp:DropDownList ID="drp_EntityNeedsEquivalency" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42" EnableViewState="false"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-4 col-sm-6 col-xs-12 no-padding">
            <div class="data-container table-display moe-full-width">
                <div class="form-group">
                    <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                        <asp:Label ID="lbl_RequestStatus" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RequestStatus %>"></asp:Label>
                    </h6>

                    <div class="form">
                        <asp:DropDownList ID="drp_RequestStatus" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-4 col-sm-6 col-xs-12 no-padding">
            <div class="data-container table-display moe-full-width">
                <div class="form-group">
                    <h6 class="font-size-16 margin-bottom-10 margin-top-10">

                        <asp:Label ID="lbl_Employee" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Employee %>"></asp:Label>
                    </h6>

                    <div class="form">
                        <asp:DropDownList ID="drp_Employees" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42" EnableViewState="false"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-12 col-sm-12 col-xs-12  no-padding">
            <asp:Button ID="btn_Cancel" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, ChangeSearchCriteria %>" OnClick="btn_Cancel_Click" CssClass="btn moe-btn pull-right clear-btn" />

            <asp:Button ID="btn_Search" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Search %>" OnClick="btn_Search_Click" ClientIDMode="Static" CssClass="btn moe-btn pull-right margin-right-10" />
            <asp:Button ID="btnExportExcel" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, ExportExcel %>" OnClick="btnExportExcel_Click" OnClientClick="return setFormSubmitToFalse();" ClientIDMode="Static" CssClass="btn moe-btn pull-right margin-right-10 export-btn" />
        </div>
    </div>
</div>

<h4 runat="server" id="searchLimit" visible="false" class="pull-left search-result-filter"><%=Resources.ITWORX.MOEHEWF.Common.SearchLimit%></h4>

<cc1:ModalPopupExtender ID="modalSearchFilter" runat="server"
    TargetControlID="btnHdn"
    PopupControlID="pnlFilter" BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlFilter" runat="server" Style="display: none;" CssClass="modalPopup">

    <asp:Label ID="lblFilterMsg" runat="server" ForeColor="Green" Font-Bold="true" Text="<%$Resources:ITWORX.MOEHEWF.Common, SearchFilterMsg %>"></asp:Label>

    <asp:Button ID="btnOk" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Ok %>" OnClick="btnOk_Click" />
</asp:Panel>
<asp:Button ID="btnHdn" runat="server" Text="Button" Style="display: none;" />

<div class="margin-2">
    <div class="row no-padding margin-top-15">
        <div class="col-md-12">
            <h4 class="font-size-18 font-weight text-right">
                <asp:Label ID="lbl_NoOfRequests" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NoOfRequests %>"></asp:Label>
            </h4>
        </div>
    </div>
    <asp:GridView ID="grd_Requests" runat="server" AutoGenerateColumns="false" AllowPaging="true" DataKeyNames="ID"
        OnPageIndexChanging="grd_Requests_PageIndexChanging" OnRowDataBound="grd_Requests_RowDataBound" ShowHeaderWhenEmpty="true"
        EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>" CssClass="table moe-table table-striped result-table">
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
                    <asp:HiddenField ID="hdn_AssignedTo" runat="server" Value='<%#  Eval("RowAssignedTo")%>'></asp:HiddenField>
                    <asp:HiddenField ID="hdn_ID" runat="server" Value='<%#  Eval("ID")%>'></asp:HiddenField>
                    <asp:Label ID="lbl_RequestID" runat="server" Text='<%#  Eval("RequestNumber")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, SubmitDate %>">
                <ItemTemplate>
                    <asp:Label ID="lbl_RequestSubmitDate" runat="server" Text='<%# Convert.ToDateTime(Eval("SubmitDate"))!=DateTime.MinValue ? Convert.ToDateTime(Eval("SubmitDate")).ToShortDateString():string.Empty %>'></asp:Label>
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
            <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, CountryName %>">
                <ItemTemplate>
                    <asp:Label ID="lbl_Country" runat="server" Text='<%# Eval("Country") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, responsibleOfficer %>">
                <ItemTemplate>
                    <asp:Label ID="lbl_AssignedTo" runat="server" Text='<%# Eval("AssignedTo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, RequestStatus %>">
                <ItemTemplate>
                    <asp:Label ID="lbl_RequestStatus" runat="server" Text='<%# Eval("RequestStatus") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="<%$Resources:ITWORX.MOEHEWF.Common, AttachmentFinalDecision %>">
                <ItemTemplate>
                    <asp:LinkButton ID="lnk_FinalDecisionFile" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, FinalDecision %>" OnClick="lnk_FinalDecisionFile_Click" Visible="false" OnClientClick="return setFormSubmitToFalse();"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, Action %>">
                <ItemTemplate>
                    <asp:LinkButton ID="lnk_Edit" runat="server" ToolTip="<%$Resources:ITWORX_MOEHEWF_UCE, Edit %>" OnClick="lnk_Edit_Click"  CssClass="edit-icon fa fa-pencil-square-o"></asp:LinkButton>
                    <asp:LinkButton ID="lnk_View" runat="server" ToolTip="<%$Resources:ITWORX_MOEHEWF_UCE, View %>" OnClick="lnk_View_Click"  CssClass="display-icon fa fa-eye"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
<asp:HiddenField runat="server" ID="hdf_University" ClientIDMode="Static" />
<input type="hidden" value="" id="__EventTriggerControlIdSearch" name="__EventTriggerControlIdSearch"/>

<script type="text/javascript">

    //function LnkClickSearchRequest(eventControl) {
    //    debugger;
    //    var ctlId = document.getElementById("__EventTriggerControlIdSearch");
    //    if (ctlId) {
    //        ctlId.value = eventControl;
    //    }


    //}
    //To handle the freezing of page after downloading a file
    function setFormSubmitToFalse() {
        setTimeout(function () { _spFormOnSubmitCalled = false; }, 3000);
        return true;
    }
    $(document).ready(function () {

        ddl_changed();
        $("#dt_DateFrom").datepicker({
            dateFormat: "m/d/yy",
            showOn: 'focus',
            showButtonPanel: true,
            changeYear: true,
            changeMonth: true,
            closeText: 'Clear',
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
            dateFormat: "m/d/yy",
            showOn: 'focus',
            showButtonPanel: true,
            changeYear: true,
            changeMonth: true,
            closeText: 'Clear',
            onClose: function () {
                var event = arguments.callee.caller.caller.arguments[0];
                // If "Clear" gets clicked, then really clear it
                if ($(event.delegateTarget).hasClass('ui-datepicker-close')) {
                    $(this).val('');
                }
            }
        });

        if ($("#hdn_DateFrom").val() != "") {
            $("#dt_DateFrom").val($("#hdn_DateFrom").val());
        }
        if ($("#hdn_DateTo").val() != "") {
            $("#dt_DateTo").val($("#hdn_DateTo").val());
        }
        $("#btn_Search").click(function () {
            $("#hdn_DateFrom").val($("#dt_DateFrom").val());
            $("#dt_DateFrom").val($("#hdn_DateFrom").val());

            $("#hdn_DateTo").val($("#dt_DateTo").val());
            $("#dt_DateTo").val($("#hdn_DateTo").val());
        });

    });

    function ddl_changed() {
        var id = $("[id*='drp_University'] :selected").text();
        if (id != null && id != "") {
            $("#hdf_University").val(id);
            //alert($("#hdf_University").val());
        }
        else
            $("#hdf_University").val("");

    };

    function ddl_Countrychanged() {
        var idCountry = $("[id*='drp_Country'] :selected").text();
        if (idCountry != null && idCountry != "") {
            $("#hdf_University").val("");
        }
    };
</script>
