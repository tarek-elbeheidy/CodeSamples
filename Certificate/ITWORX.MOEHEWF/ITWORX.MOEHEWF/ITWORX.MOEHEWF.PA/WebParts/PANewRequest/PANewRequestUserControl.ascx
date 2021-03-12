<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.PA/PACheckUniversity.ascx" TagPrefix="uc1" TagName="CheckUniversity" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.UCE/ApplicantDetails.ascx" TagPrefix="uc1" TagName="ApplicantDetails" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.PA/PARequestDetails.ascx" TagPrefix="uc1" TagName="PARequestDetails" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/TermsAndConditions.ascx" TagPrefix="uc1" TagName="TermsAndConditions" %>
<%--<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.PA/PARolesNavigationLinks.ascx" TagPrefix="uc1" TagName="PARolesNavigationLinks" %>--%>
<%@ Register Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PANewRequestUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.WebParts.PANewRequest.PANewRequestUserControl" %>

<%--<uc1:PARolesNavigationLinks runat="server" id="PARolesNavigationLinks" />--%>

<style type="text/css">
    .ui-datepicker-buttonpane .ui-priority-secondary {
        opacity: 1
    }

    .no-left-margin {
        margin-left: 0 !important
    }

    .uploadButton h6 {
        display: block !important;
    }

    #calcFaculty input {
        width: 100%;
    }

    .Iwidth-100 input {
        width: 100%
    }

    input[id*="btnUpload"]:hover {
        width: auto !important
    }

    #divUniversities {
        display: inline-block;
    }

    .ui-datepicker .ui-datepicker-buttonpane {
        background: #fff;
        height: 52px;
    }

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

    .pnlForm_layout {
        border: none;
        background: #fff
    }

    tr:nth-child(odd) {
        background: #fff;
    }

    .no-margin-imp {
        margin: 0 !important
    }

    #rdbWorkingOrNot {
        border: none
    }

    .margin-top-50 {
        margin-top: 50px
    }
</style>
<%--<h1 class="section-title font-weight-400 margin-bottom-35">
    <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA,createPANewRequest%>"></asp:Literal></h1>--%>
<!--Section Content-->

<div class="no-padding row flex-display align-items-center margin-bottom-10">
    <div class="col-md-9 col-xs-7 no-padding">
        <h1 class="section-title font-weight-400 no-margin margin-top-0 margin-bottom-0-imp">
            <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA,createPANewRequest%>"></asp:Literal></h1>
        <!--Section Content-->
    </div>
    <div class="col-md-3 col-xs-5 no-padding">
        <asp:Button ID="btnDashboard" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA,DashboardList%>" OnClick="btnDashboard_Click" CssClass="btn moe-btn pull-right" />
    </div>
</div>
<div class="section-container">

    <asp:Panel ID="pnlForm" runat="server">
        <asp:Wizard ID="wizardPANewRequest" runat="server" DisplaySideBar="false" CssClass="moe-full-width pnlForm_layout" OnNextButtonClick="wizardPANewRequest_NextButtonClick" OnPreviousButtonClick="wizardPANewRequest_PreviousButtonClick" OnPreRender="wizardPANewRequest_PreRender" OnFinishButtonClick="wizardPANewRequest_FinishButtonClick">
            <HeaderTemplate>
                <div class="row proccess pa">
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
                        <p class="font-size-16 font-weight-400">
                            <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA,saveMsg%>"></asp:Literal>
                        </p>
                    </div>
                    <div class="col-md-3 col-sm-3 col-xs-5 no-padding text-right">
                        <asp:Button ID="btnSave" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, SaveRequest %>" OnClick="btnSave_Click" ClientIDMode="Static" CssClass="savedata btn moe-btn" />
                        <%--<asp:Button ID="btnSave" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, SaveRequest %>" OnClick="btnSave_Click" ClientIDMode="Static" CssClass="savedata btn moe-btn" />--%>
                    </div>
                </div>
            </HeaderTemplate>

            <WizardSteps>
                <asp:WizardStep ID="wizardStepCheckUniversity" runat="server" Title="<%$Resources:ITWORX_MOEHEWF_PA, CheckUniversity  %>">
                    <uc1:CheckUniversity runat="server" id="checkUniversity" />
                </asp:WizardStep>
                <asp:WizardStep ID="wizardStepConditions" runat="server" Title="<%$Resources:ITWORX_MOEHEWF_PA, TermsAndConditions  %>">
                    <uc1:TermsAndConditions runat="server" id="termsAndConditions" />
                </asp:WizardStep>
                <asp:WizardStep ID="wizardStepApplicantDetails" runat="server" Title="<%$Resources:ITWORX_MOEHEWF_PA, ApplicantDetails  %>">
                    <uc1:ApplicantDetails runat="server" id="applicantDetails" />
                </asp:WizardStep>
                <asp:WizardStep ID="wizardStepPARequestDetails" runat="server" Title="<%$Resources:ITWORX_MOEHEWF_PA, PARequestDetails  %>">
                    <uc1:PARequestDetails runat="server" id="PARequestDetails" />
                </asp:WizardStep>
            </WizardSteps>
            <StartNavigationTemplate>
                <div class="row unhighlighted-section no-padding-imp flex-display align-items-baseline margin-2">
                    <div class="col-md-6 col-xs-12 no-padding text-left">
                    </div>
                    <div class="col-md-6 col-xs-12 no-padding text-right">
                        <asp:Button ID="StartNextButton" runat="server" CommandName="MoveNext" Text="<%$Resources:ITWORX_MOEHEWF_PA, Next %>" ValidationGroup="Submit" CssClass="submitdata btn moe-btn margin-lr-0 isVaildYear" />
                    </div>
                </div>
            </StartNavigationTemplate>
            <StepNavigationTemplate>
                <div class="row unhighlighted-section no-padding-imp flex-display align-items-baseline margin-2">
                    <div class="col-md-6 col-xs-6 no-padding text-left">
                        <asp:Button ID="StepPreviousButton" runat="server" CommandName="MovePrevious" Text="<%$Resources:ITWORX_MOEHEWF_PA, Previous %>" CssClass="btn moe-btn margin-lr-0" />
                    </div>
                    <div class="col-md-6 col-xs-6 no-padding text-right">
                        <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" Text="<%$Resources:ITWORX_MOEHEWF_PA, Next %>" ValidationGroup="Submit" CssClass="btn moe-btn margin-lr-0 enabledstepnext" />
                    </div>
                </div>
            </StepNavigationTemplate>
            <FinishNavigationTemplate>
                <div class="row unhighlighted-section no-padding-imp flex-display align-items-baseline margin-2">
                    <div class="col-md-6 col-sm-6 col-xs-12 no-padding text-left">
                        <asp:Button ID="FinishPreviousButton" runat="server" CommandName="MovePrevious" Text="<%$Resources:ITWORX_MOEHEWF_PA, Previous %>" CssClass="btn moe-btn margin-lr-0" />
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12 no-padding text-right">
                        <asp:Button ID="FinishButton" runat="server" CommandName="MoveComplete" Text="<%$Resources:ITWORX_MOEHEWF_PA, Submit %>" ValidationGroup="Submit" CssClass="finishdata btn moe-btn margin-lr-0" />
                    </div>
                </div>
            </FinishNavigationTemplate>
        </asp:Wizard>
    </asp:Panel>

    <asp:Label ID="lblNoRequest" runat="server" Font-Bold="true" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_PA, YouHaveNoRequests %>"></asp:Label>
    <asp:Label ID="lblSubmissionMsg" runat="server" Font-Bold="true" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_PA, QatariPASubmissionMsg %>"></asp:Label>
    <cc1:ModalPopupExtender ID="modalPopUpConfirmation" runat="server"
        TargetControlID="btnHdn"
        PopupControlID="pnlConfirmation" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlConfirmation" runat="server" Style="display: none;" CssClass="modalPopup">

        <asp:Label ID="lblSuccess" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>
        <br />
        <br />

        <asp:Button ID="btnModalOK" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, Ok %>" OnClick="btnModalOK_Click" />
    </asp:Panel>
    <asp:Button ID="btnHdn" runat="server" Text="Button" Style="display: none;" />

    <cc1:ModalPopupExtender ID="modalSavePopup" runat="server"
        TargetControlID="btnSaveHdn"
        PopupControlID="pnlSaveConfirmation" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlSaveConfirmation" runat="server" Style="display: none;" CssClass="modalPopup">

        <asp:Label ID="lblSaveSuccess" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>
        <br />
        <br />

        <asp:Button ID="btnSaveOk" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, Ok %>" OnClick="btnSaveOk_Click" />
    </asp:Panel>
    <asp:Button ID="btnSaveHdn" runat="server" Text="Button" Style="display: none;" />

    <cc1:ModalPopupExtender ID="modalPopupError" runat="server" TargetControlID="btnHdnErrorOk" PopupControlID="pnl_ErrorConfirmed" BackgroundCssClass="modalBackground"></cc1:ModalPopupExtender>
    <asp:Panel ID="pnl_ErrorConfirmed" runat="server" Style="display: none;" CssClass="modalPopup">

        <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Green" Font-Bold="true" Text="<%$Resources:ITWORX_MOEHEWF_PA, PopupError %>"></asp:Label>

        <asp:Button ID="btnErrorOk" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, Ok %>" OnClick="btnErrorOk_Click" />
    </asp:Panel>
    <asp:Button ID="btnHdnErrorOk" runat="server" Text="Button" Style="display: none;" />
</div>
<script type="text/javascript">
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

    $(".isVaildYear").click(function () {
        var nextYear = (new Date().getFullYear() + 1);
        var currentYears = new Date().getFullYear();
        var txt_year = $('#txtYear').val();
        if (txt_year != '') {
            var NYear = Number(txt_year);
            if (NYear != NaN) {
                if (NYear == currentYears || NYear == nextYear) {

                    $('#lblYearMsg').hide();
                    return true;
                }
                else {
                    $('#lblYearMsg').show();
                    return false;

                }
            }
        }
    });
</script>