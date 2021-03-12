<%@ Assembly Name="ITWORX.MOEHEWF.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7b2931724f1d7d1c" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TrackingRequestUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.Common.WebParts.TrackingRequest.TrackingRequestUserControl" %>
<%@ Assembly Name="ITWORX.MOEHE.Utilities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=942948f6a64aa526" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>





<%@ Import Namespace="ITWORX.MOEHE.Utilities"  %>

<%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
 <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
 <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>--%>

<%--<script type="text/javascript" src='<%= ResolveUrl ("~/Style%20Library/Scripts/jquery.min.js") %>'></script>
<link rel="stylesheet" href='<%= ResolveUrl ("~/Style%20Library/CSS/jquery-ui.css") %>'>
<script type="text/javascript" src='<%= ResolveUrl ("~/Style%20Library/Scripts/jquery-1.12.4.js") %>'></script>
<script type="text/javascript" src='<%= ResolveUrl ("~/Style%20Library/Scripts/jquery-ui.js") %>'></script>--%>
<%--<script src="http://code.highcharts.com/highcharts.js" type="text/javascript"></script>--%>


<%-- <div class="row margin-bottom-50 ">
  
   <div class="col-md-12 no-padding chart-container">
        <asp:Literal ID="piechartLiteral" runat="server" ></asp:Literal>
    </div>
</div>--%>

<style>
	/*.clear-btn, input[type=submit].clear-btn:hover, .clear-btn.moe-btn:hover {
		margin-left:10px !important;
		margin-right:10px !important;
	}*/
        .modalBackground {
        background-color: Black;
        filter: alpha(opacity=90);
        opacity: 0.8;
    }

    .modalPopup {
        background-color: #fff;
        border: 3px solid #ccc;
        padding: 10px;
        width: 300px;
    }
</style>

     <div class="row unheighlighted-section margin-bottom-25 margin-2">
         <div class="col-lg-5 col-lg-offset-7 col-md-5 col-md-offset-7 col-sm-8 col-sm-offset-4 col-xs-12">
             <div class="col-md-8 col-sm-7 col-xs-7 no-padding-imp">
                        <asp:DropDownList ID="dropRequestType" runat="server" CssClass="moe-dropdown create-btn pull-right moe-input-padding moe-select input-height-42 "></asp:DropDownList>
                 <asp:RequiredFieldValidator ID="reqRequestType" runat="server" CssClass="error-msg moe-full-width"  ControlToValidate="dropRequestType" Display="Dynamic" ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, RequiredRequestType %>"  ValidationGroup="SelecReq"></asp:RequiredFieldValidator>
                
             </div>
             <div class="col-md-4 col-sm-5 col-xs-5 text-right no-padding-imp">
                 <asp:Button ID="btnRequestType" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, CreateNewRequest %>" OnClick="btnRequestType_Click"  ValidationGroup="SelecReq" CssClass="create-request"/>
             </div>
         </div>
         
    </div>

<div class="section-container">
 
   
        <div class="row no-padding unheighlighted-section flex-display align-items-center margin-bottom-25 margin-0">
                  <div class="col-md-6 col-xs-8 no-padding-imp">
                       <h2 class="no-margin font-size-32 font-weight-normal color-black segoe-font margin-top-0 margin-bottom-0"> <asp:Label ID="lblAllRequests" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX.MOEHEWF.Common, AllRequests %>"></asp:Label></h2>
                  </div>

                  
        </div>
    <div id="divSearch" runat="server">


        <div class="row no-padding heighlighted-section margin-bottom-50 flex-display flex-wrap">
            <div class="col-md-3 col-sm-6 col-xs-12 no-padding">
                <div class="data-container table-display moe-full-width">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                            <asp:Label ID="lblRequestNumberSearch" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, RequestNumber %>"></asp:Label>
                        </h6>

                        <div class="form">
                           
                            <asp:TextBox ID="txtRequestNumber" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-3 col-sm-6 col-xs-12 no-padding">
                <div class="data-container table-display moe-full-width">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                            <asp:Label ID="lblSubmitDateFromSearch" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, SubmitDateFrom %>"></asp:Label>
                        </h6>

                        <div class="form">

                            <input type="text" id="dtSubmitDateFrom" readonly="readonly" runat="server" class="moe-full-width moe-input-padding moe-select input-height-42">

                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-3 col-sm-6 col-xs-12 no-padding">
                <div class="data-container table-display moe-full-width">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                            <asp:Label ID="lblSubmitDateTo" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, SubmitDateTo %>"></asp:Label>
                        </h6>

                        <div class="form">

                            <input type="text" id="dtSubmitDateTo" readonly="readonly" runat="server" class="moe-full-width moe-input-padding moe-select input-height-42 ">
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-3 col-sm-6 col-xs-12 no-padding">
                <div class="data-container table-display moe-full-width">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                            <asp:Label ID="lblRequestTypeSearch" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, RequestType %>"></asp:Label>
                        </h6>

                        <div class="form">

                            <asp:DropDownList ID="dropRequestTypeSearch" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-3 col-sm-6 col-xs-12 no-padding">
                <div class="data-container table-display moe-full-width">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                            <asp:Label ID="lblRequestStatusSearch" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, RequestStatus %>"></asp:Label>
                        </h6>

                        <div class="form">

                            <asp:DropDownList ID="dropRequestStatus" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-3 col-sm-6 col-xs-12 no-padding">
                <div class="data-container table-display moe-full-width">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                            <asp:Label ID="lblFinalDecisionSearch" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, FinalDecision %>"></asp:Label>
                        </h6>

                        <div class="form">

                            <asp:DropDownList ID="dropFinalDecision" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-6 col-sm-12 col-xs-12  no-padding">
                 <h6 class="font-size-16 margin-bottom-10 margin-top-10" style="visibility:hidden;">
                          hh
                        </h6>
                <asp:Button ID="btnChangeSearchCriteria" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, ChangeSearchCriteria %>" OnClick="btnChangeSearchCriteria_Click" ClientIDMode="Static" CssClass="btn moe-btn pull-right clear-btn" />
                <asp:Button ID="btnSearch" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Search %>" OnClick="btnSearch_Click" ValidationGroup="Search" ClientIDMode="Static" CssClass="btn moe-btn pull-right margin-right-10" />
            </div>


            <%--<SharePoint:DateTimeControl ID="dtSubmitDateFrom" runat="server"  DateOnly="true" />
 <asp:CompareValidator ID="compSubmitDateFrom" runat="server" ForeColor="Red"  Type="Date" Operator="DataTypeCheck" ControlToValidate="dtSubmitDateFrom$dtSubmitDateFromDate"  ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, InvalidDate %>" ValidationGroup="Search" ></asp:CompareValidator>--%>

            <%--<SharePoint:DateTimeControl ID="dtSubmitDateTo" runat="server" DateOnly="true" />
<asp:CompareValidator ID="compSubmitDateTo" runat="server" ForeColor="Red"  Type="Date" Operator="DataTypeCheck" ControlToValidate="dtSubmitDateTo$dtSubmitDateToDate"  ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, InvalidDate %>" ValidationGroup="Search"></asp:CompareValidator>
<asp:CompareValidator ID="compDates" ControlToCompare="dtSubmitDateFrom$dtSubmitDateFromDate" ControlToValidate="dtSubmitDateTo$dtSubmitDateToDate" Type="Date" Operator="GreaterThanEqual" ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, SumbitDateValidation %>" runat="server"  ForeColor="Red" ValidationGroup="Search"></asp:CompareValidator>--%>

            <%--<asp:LinkButton ID="lnkRedirectToNewRequest" runat="server"  Text="<%$Resources:ITWORX.MOEHEWF.Common, AddNewRequest %>"  OnClick="lnkRedirectToNewRequest_Click"></asp:LinkButton>--%>

        </div>

                <h4 runat="server" id="searchLimit" visible="false" class="pull-left search-result-filter"><%=Resources.ITWORX.MOEHEWF.Common.SearchLimit%></h4>


        <div class="row no-padding margin-top-15 margin-0">
        <h4 class="font-size-18 font-weight-500 pull-right"> <asp:Label ID="lblNoOfTrackingRequests" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, NoOfTrackingRequests %>"></asp:Label> : 
        <asp:Label ID="lblNoOfTrackingRequestsValue" runat="server"></asp:Label></h4>
    </div>
        <div class="no-padding row unheighlighted-section margin-2">
            <asp:GridView ID="gridRequests" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="gridRequests_PageIndexChanging" ShowHeaderWhenEmpty="true" EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>" OnRowDataBound="gridRequests_RowDataBound" CssClass="table moe-table table-striped result-table">
            <Columns>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX.MOEHEWF.Common, SerialNumber %>">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$Resources:ITWORX.MOEHEWF.Common, RequestType %>">
                    <ItemTemplate>
                        <asp:HiddenField runat="server"  ID="hdnRequestTypeEnum" Value='<%# Eval("RequestTypeEnumVaue") %>' />
                        <asp:HiddenField runat="server" ID="hdnRequestId" Value='<%# Eval("RequestId") %>' />
                        <asp:Label ID="lblRequestType" runat="server" Text='<%#  Eval("RequestType.SelectedTitle")%>'></asp:Label>
                       
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX.MOEHEWF.Common, RequestNumber %>">
                    <ItemTemplate>

                        <asp:LinkButton ID="lblRequestNumber" runat="server" Text='<%#  Eval("RequestNumber")%>' OnClick="lnk_Request_Click"></asp:LinkButton>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX.MOEHEWF.Common, RequestSubmitDate %>">
                    <ItemTemplate>
                        <asp:Label ID="lblRequestSubmitDate" runat="server" Text='<%# Convert.ToDateTime(Eval("SubmitDate")) != DateTime.MinValue ? Convert.ToDateTime(Eval("SubmitDate")).QatarFormatedDate() :string.Empty %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX.MOEHEWF.Common, Certificate %>">
                    <ItemTemplate>
                        <asp:Label ID="lblCertificates" runat="server" Text='<%#  Eval("AcadDegree")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX.MOEHEWF.Common, RequestStatus %>">
                    <ItemTemplate>
                        <asp:Label ID="lblRequestStatus" runat="server" Text='<%# Eval("RequestStatus.SelectedTitle") %>'></asp:Label>
                        <asp:HiddenField ID="hdnRequestStatusId" runat="server" Value='<%# Eval("RequestStatus.SelectedID") %>'></asp:HiddenField>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX.MOEHEWF.Common, RequestPhase %>">
                    <ItemTemplate>
                        <%-- <asp:Label ID="lblRequestPhase" runat="server" Text='<%# Eval("RequestPhase.SelectedTitle") %>'></asp:Label>--%>
                        <asp:Label ID="lblRequestPhase" runat="server" Text='<%# LCID==1033? Eval("RequestStatus.ApplicantRequestPhaseEn") :Eval("RequestStatus.ApplicantRequestPhaseAr")  %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX.MOEHEWF.Common, FinalDecision %>">
                    <ItemTemplate>
                        <asp:Label ID="lblFinalDecision" runat="server" Text='<%# LCID==1033? Eval("RequestStatus.FinalDecisionEn") :Eval("RequestStatus.FinalDecisionAr")  %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX.MOEHEWF.Common, AttachmentName %>">
                    <ItemTemplate>
             <asp:LinkButton ID="lnk_FinalDecisionFile" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, FinalDecision %>" OnClick="lnk_FinalDecisionFile_Click" Visible="false" OnClientClick="return setFormSubmitToFalse();"></asp:LinkButton>

<%--                        <asp:HyperLink ID="hypAttachment" Text='<%#Eval("FileName") %>' runat="server" NavigateUrl='<%#Eval("AttachmentURL") %>' Target="_blank"></asp:HyperLink>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX.MOEHEWF.Common, Actions %>">
                    <ItemTemplate>
                        <!--ICONS-->
                        <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEdit_Click" CssClass="edit-icon fa fa-pencil-square-o" ToolTip="<%$Resources:ITWORX_MOEHEWF_UCE, Edit %>"></asp:LinkButton>
                        <asp:LinkButton ID="lnkDelete" runat="server" ToolTip="<%$Resources:ITWORX_MOEHEWF_UCE, Delete %>"  OnClick="lnkDelete_Click" OnClientClick="return ConfirmOnDelete();" CssClass="delete-icon fa fa-trash" ></asp:LinkButton>
                        <asp:LinkButton ID="lnkDisplay" runat="server" ToolTip="<%$Resources:ITWORX_MOEHEWF_UCE, Display %>"  OnClick="lnkDisplay_Click" CssClass="display-icon fa fa-eye"></asp:LinkButton>
                         <!--TEXTS-->
                        <%--<asp:LinkButton ID="lnkEdit" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Edit %>" OnClick="lnkEdit_Click" CssClass="edit-icon fa fa-pencil-square-o"></asp:LinkButton>
                        <asp:LinkButton ID="lnkDelete" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Delete %>" OnClick="lnkDelete_Click" OnClientClick="return ConfirmOnDelete();" CssClass="delete-icon fa fa-trash"></asp:LinkButton>
                        <asp:LinkButton ID="lnkDisplay" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Display %>" OnClick="lnkDisplay_Click" CssClass="display-icon fa fa-eye"></asp:LinkButton>--%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>
       
    </div>
    
     <div class="row no-padding margin-top-15 margin-0">
    <h4 class="font-size-18 font-weight-600">
        <asp:Label ID="lblNoRequests" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>"></asp:Label>

    </h4>
         </div>
</div>
<asp:Button ID="btnHdn" runat="server" Text="Button" Style="display: none;" />
 <cc1:ModalPopupExtender ID="modalPopUpConfirmation" runat="server"
        TargetControlID="btnHdn"
        PopupControlID="pnlConfirmation" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlConfirmation" runat="server" Style="display: none;" CssClass="modalPopup">
         <asp:Label ID="lblSubmissionMsg" runat="server" Font-Bold="true" ForeColor="Green"  Text="<%$Resources:ITWORX.MOEHEWF.Common, QatariPASubmissionMsg %>"></asp:Label>
     
        <br />
        <br />

        <asp:Button ID="btnModalOK" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Ok %>" />
    </asp:Panel>
<script type="text/javascript">
    $(document).ready(function () {


       

        $('#<%=dtSubmitDateFrom.ClientID %>').datepicker({

            dateFormat: "dd/mm/yy",
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
          
                onSelect: function (selected) {
                    var dt2 = $('#<%=dtSubmitDateTo.ClientID %>');
                    dt2.datepicker("option", "minDate", selected)
            
            }
          });

        $('#<%=dtSubmitDateTo.ClientID %>').datepicker({
            dateFormat: "dd/mm/yy",
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

        //if ($("#hdnSubmitDateFrom").val() != "") {
        //    $("#dtSubmitDateFrom").val($("#hdnSubmitDateFrom").val());
        //}
        //if ($("#hdnSubmitDateTo").val() != "") {
        //    $("#dtSubmitDateTo").val($("#hdnSubmitDateTo").val());
        //}
        //$("#btnSearch").click(function () {
        //    if ($("#dtSubmitDateFrom").val() != "") {
        //        $("#hdnSubmitDateFrom").val($("#dtSubmitDateFrom").val());
        //        $("#dtSubmitDateFrom").val($("#hdnSubmitDateFrom").val());

        //    }


        //    if ($("#dtSubmitDateTo").val() != "") {
        //        $("#hdnSubmitDateTo").val($("#dtSubmitDateTo").val());
        //        $("#dtSubmitDateTo").val($("#hdnSubmitDateTo").val());
        //    }
        //});


   <%-- $(function () {
        $('#container').highcharts({
            chart: {
                plotBackgroundColor: null,
                plotBorderWidth: 1,
                plotShadow: false
            },
            title: {
                text: 'Request Status Count'
            },
            tooltip: {
                pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: true,
                        format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                        style: {
                            color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                        },
                        showInLegend: true
                    }
                }
            }, series: [{
                type: 'pie',
                name: 'Count',
                data: <%=chartCountData%>,
         
          }]

          });
      });--%>



    });
    function ConfirmOnDelete() {
        var dialogText = "<%= DeleteConfirmation %>";

        if (confirm(dialogText) == true)
            return true;
        else
            return false;
    }
    function setFormSubmitToFalse() {
        setTimeout(function () { _spFormOnSubmitCalled = false; }, 3000);
        return true;
    } 
</script>


