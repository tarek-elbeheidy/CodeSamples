<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.UCE/CheckUniversity.ascx" TagPrefix="uc1" TagName="CheckUniversity" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.UCE/ApplicantDetails.ascx" TagPrefix="uc1" TagName="ApplicantDetails" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.UCE/RequestDetails.ascx" TagPrefix="uc1" TagName="RequestDetails" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/TermsAndConditions.ascx" TagPrefix="uc1" TagName="TermsAndConditions" %>
<%--<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.UCE/RolesNavigationLinks.ascx" TagPrefix="uc1" TagName="RolesNavigationLinks" %>--%>
<%@ Register Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewRequestUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.WebParts.NewRequest.NewRequestUserControl" %>

<%--<uc1:RolesNavigationLinks runat="server" id="RolesNavigationLinks" />--%>

<%--<script type="text/javascript" src='<%= ResolveUrl ("~/Style%20Library/Scripts/jquery.min.js") %>'></script>--%>

<style type="text/css">
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
<div class="no-padding row flex-display align-items-center margin-bottom-10">
    <div class="col-md-9 col-xs-7 no-padding">
        <h1 class="section-title font-weight-400 no-margin margin-top-0 margin-bottom-0-imp">
            <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE,createNewRequest%>"></asp:Literal></h1>
        <!--Section Content-->
    </div>
    <div class="col-md-3 col-xs-5 no-padding">
        <asp:Button ID="btnDashboard" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE,DashboardList%>" OnClick="btnDashboard_Click" CssClass="btn moe-btn pull-right" />
    </div>
</div>
<div class="section-container">
<asp:Panel ID="pnlForm" runat="server">
<asp:Wizard ID="wizardNewRequest" runat="server" DisplaySideBar="false" CssClass="moe-full-width" OnNextButtonClick="wizardNewRequest_NextButtonClick" OnPreviousButtonClick="wizardNewRequest_PreviousButtonClick" OnPreRender="wizardNewRequest_PreRender" >
     <HeaderTemplate>
          <div class="row proccess">
              <ul id="wizHeader" class="moe-full-width no-padding flex-display">
                   <asp:Repeater ID="SideBarList" runat="server">
                       <ItemTemplate>
                           <li><a class="<%# GetClassForWizardStep(Container.DataItem) %>" title="<%#Eval("Name")%>">
                               <div class="process-img"></div>
                           <p class="process-des"><%# Eval("Name")%></p>    
                           </a> 

                           </li>
                       </ItemTemplate>
                   </asp:Repeater>
               </ul>
          </div>
 
<div class="row unhighlighted-section no-padding-imp flex-display align-items-baseline xs-align-items-center" id="saveBtn">
                    <div class="col-md-9 col-sm-9 col-xs-7 no-padding">
                        <p class="font-size-16 font-weight-400"><asp:Literal ID="saveLtr" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE,saveMsg%>"></asp:Literal></p>
                    </div>
                    <div class="col-md-3 col-sm-3  col-xs-5 no-padding text-right">
                        <asp:Button ID="btnSave" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, SaveRequest %>" OnClick="btnSave_Click" ClientIDMode="Static" CssClass="savedata btn moe-btn" />
                    </div>
                </div>
            </HeaderTemplate>

            <WizardSteps>
                <asp:WizardStep ID="wizardStepCheckUniversity" runat="server" Title="<%$Resources:ITWORX_MOEHEWF_UCE, CheckUniversity  %>">
                    <uc1:CheckUniversity runat="server" id="checkUniversity" />
                </asp:WizardStep>
                <asp:WizardStep ID="wizardStepConditions" runat="server" Title="<%$Resources:ITWORX_MOEHEWF_UCE, TermsAndConditions  %>">
                    <uc1:TermsAndConditions runat="server" id="termsAndConditions" />
                </asp:WizardStep>
                <asp:WizardStep ID="wizardStepApplicantDetails" runat="server" Title="<%$Resources:ITWORX_MOEHEWF_UCE, ApplicantDetails  %>">
                    <uc1:ApplicantDetails runat="server" id="applicantDetails" />
                </asp:WizardStep>
                <asp:WizardStep ID="wizardStepRequestDetails" runat="server" Title="<%$Resources:ITWORX_MOEHEWF_UCE, RequestDetails  %>">
                    <uc1:RequestDetails runat="server" id="requestDetails" />
                </asp:WizardStep>
                <asp:WizardStep ID="wizardStepPay" runat="server" Title="<%$Resources:ITWORX_MOEHEWF_UCE, Pay  %>">
                    <div class="row heighlighted-section no-padding margin-2">
                        <h6 class="font-size-22 font-weight-bold">
                            <asp:Label ID="lbl_PaymentMsg" runat="server" Text=""></asp:Label>
                        </h6>
                    </div>
                </asp:WizardStep>
            </WizardSteps>

            <StartNavigationTemplate>
                <div class="row unhighlighted-section no-padding-imp flex-display align-items-baseline margin-2">
                    <div class="col-md-6 col-xs-6 no-padding text-left">
                    </div>
                    <div class="col-md-6 col-xs-6 no-padding text-right">
                        <asp:Button ID="StartNextButton" runat="server" CommandName="MoveNext" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Next %>" ValidationGroup="Submit" CssClass="submitdata btn moe-btn margin-lr-0" />
                    </div>
                </div>
            </StartNavigationTemplate>
            <StepNavigationTemplate>
                <div class="row unhighlighted-section no-padding-imp flex-display align-items-baseline margin-2">
                    <div class="col-md-6 col-xs-6 no-padding text-left">
                        <asp:Button ID="StepPreviousButton" runat="server" CommandName="MovePrevious" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Previous %>" CssClass="btn moe-btn margin-lr-0" />
                    </div>
                    <div class="col-md-6 col-xs-6 no-padding text-right">
                        <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Next %>" ValidationGroup="Submit" CssClass="btn moe-btn margin-lr-0 enabledstepnext" />
                    </div>
                </div>
            </StepNavigationTemplate>
            <FinishNavigationTemplate>
                <div class="row unhighlighted-section no-padding-imp flex-display align-items-baseline margin-2">
                    <div class="col-md-6 col-xs-12 no-padding text-left">
                        <asp:Button ID="cancleBTN" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Cancel %>" OnClick="CancelPayment_Click" CssClass="btn moe-btn margin-lr-0" />
                    </div>
                    <div id="divIdForm" class="col-md-6 col-xs-12 no-padding text-right">
                        <button type="button" onclick="postPay();" id="PayBTN" class="finishdata btn moe-btn margin-lr-0">
                            <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Pay %>"></asp:Literal>
                        </button>
                        <cc1:ModalPopupExtender ID="ModalPopupSaveDraft" runat="server" TargetControlID="btnSaveDraft" PopupControlID="pnl_SaveDraft" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>
                        <asp:Panel ID="pnl_SaveDraft" runat="server" Style="display: none;" CssClass="modalPopup">

                            <asp:Label ID="lbl_Success_Save" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, SaveSucceed %>" ForeColor="Green" Font-Bold="true"></asp:Label>

                            <asp:Button ID="okDraft" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Ok %>" OnClick="SaveDraft_OnClick" />
                        </asp:Panel>
                        <asp:Button ID="btnSaveDraft" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Save %>" CssClass="btn moe-btn margin-lr-0" OnClick="btn_ModalShow_Click" />

                    </div>
                </div>
            </FinishNavigationTemplate>
        </asp:Wizard>
    </asp:Panel>

    <asp:Label ID="lblNoRequest" runat="server" Font-Bold="true" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_UCE, YouHaveNoRequests %>"></asp:Label>

    <asp:Button ID="btnHdn" runat="server" Text="Button" Style="display: none;" />
    <cc1:ModalPopupExtender ID="ModalPopupSave" runat="server" TargetControlID="btnHdnSave" PopupControlID="pnl_SaveConfirmed" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>
    <asp:Panel ID="pnl_SaveConfirmed" runat="server" Style="display: none;" CssClass="modalPopup">

        <asp:Label ID="lblSuccess_Save" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>

        <asp:Button ID="btn_ModalSaveOK" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Ok %>" OnClick="btn_ModalSaveOK_Click" />
    </asp:Panel>
    <asp:Button ID="btnHdnSave" runat="server" Text="Button" Style="display: none;" />



     <cc1:ModalPopupExtender ID="modalPopupError" runat="server" TargetControlID="btnHdnErrorOk" PopupControlID="pnl_ErrorConfirmed" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>
    <asp:Panel ID="pnl_ErrorConfirmed" runat="server" Style="display: none;" CssClass="modalPopup">

        <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Green" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PopupError %>"></asp:Label>

        <asp:Button ID="btnErrorOk" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Ok %>" OnClick="btnErrorOk_Click" />
    </asp:Panel>
    <asp:Button ID="btnHdnErrorOk" runat="server" Text="Button" Style="display: none;" />
</div>
<input id="serviceURL" type="hidden" runat="server" name="serviceURL">
<div id="PaymentDiv">
    <input id="access_key" type="hidden" runat="server" name="access_key">
    <input id="profile_id" type="hidden" runat="server" name="profile_id">
    <input id="transaction_uuid" type="hidden" runat="server" name="transaction_uuid">
    <input id="signed_field_names" type="hidden" runat="server" name="signed_field_names">
    <input id="unsigned_field_names" type="hidden" runat="server" name="unsigned_field_names">
    <input id="signed_date_time" type="hidden" runat="server" name="signed_date_time">
    <input id="locale" type="hidden" runat="server" name="locale">
    <input id="customer_ip_address" type="hidden" runat="server" name="customer_ip_address">
    <input id="merchant_defined_data" type="hidden" runat="server" name="merchant_defined_data1">
    <input id="transaction_type" type="hidden" runat="server" name="transaction_type">
    <input id="reference_number" type="hidden" runat="server" name="reference_number">
    <input id="amount" type="hidden" runat="server" name="amount">
    <input id="currency" type="hidden" runat="server" name="currency">
    <input type="hidden" name="__SCROLLPOSITIONX" id="__SCROLLPOSITIONX" value="0">
    <input type="hidden" name="__SCROLLPOSITIONY" id="__SCROLLPOSITIONY" value="0">
    <input id="signature" type="hidden" runat="server" name="signature">
</div>
<script type="text/javascript">
    function postPay() {
        var serviceURL = $("#<%=serviceURL.ClientID%>").val();
        $("#aspnetForm").attr("action", serviceURL);
        var paymentContent = document.getElementById("PaymentDiv").outerHTML;
        document.getElementById("aspnetForm").innerHTML = paymentContent;
        $("#<%=access_key.ClientID%>").attr("name", "access_key");
        $("#<%=profile_id.ClientID%>").attr("name", "profile_id");
        $("#<%=transaction_uuid.ClientID%>").attr("name", "transaction_uuid");
        $("#<%=signed_field_names.ClientID%>").attr("name", "signed_field_names");
        $("#<%=unsigned_field_names.ClientID%>").attr("name", "unsigned_field_names");
        $("#<%=signed_date_time.ClientID%>").attr("name", "signed_date_time");
        $("#<%=locale.ClientID%>").attr("name", "locale");
        $("#<%=customer_ip_address.ClientID%>").attr("name", "customer_ip_address");
        $("#<%=merchant_defined_data.ClientID%>").attr("name", "merchant_defined_data1");
        $("#<%=transaction_type.ClientID%>").attr("name", "transaction_type");
        $("#<%=reference_number.ClientID%>").attr("name", "reference_number");
        $("#<%=amount.ClientID%>").attr("name", "amount");
        $("#<%=currency.ClientID%>").attr("name", "currency");
        $("#<%=signature.ClientID%>").attr("name", "signature");
        document.getElementById('aspnetForm').submit();
    }
    EnableButton();
    function EnableButton() {
        if ($("#chkTermsAndConditions").is(':visible')) {
            if ($("#chkTermsAndConditions").is(':checked')) {
                $(".enabledstepnext").removeAttr("disabled");

            }
            else {
                $(".enabledstepnext").attr("disabled", "disabled");

            }
        }
        else {
            $(".enabledstepnext").removeAttr("disabled");
        }
    }
    $("#chkTermsAndConditions").change(function () {
        EnableButton();
    });



    $(".enabledstepnext").click(function () {
        var grpVaild = Page_ClientValidate("Submit");
        if (grpVaild) {
            return true;
        }
        else {
            return false;
        }
    });
</script>