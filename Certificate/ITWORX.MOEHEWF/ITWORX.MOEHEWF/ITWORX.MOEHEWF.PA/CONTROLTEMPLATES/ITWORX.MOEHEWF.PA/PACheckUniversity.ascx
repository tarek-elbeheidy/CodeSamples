<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/DropdownWithTextbox.ascx" TagPrefix="uc1" TagName="DropdownWithTextbox" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PACheckUniversity.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.CheckUniversity" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<style>
span[id*='checkUniversity_lblUniversity']:after  {
    content: "*";
    color: red;
    padding: 0 5px;
    font-size: 15px;
    font-weight: normal;
}

div#pnlUniversities h6 {
    margin-bottom: 15px !important;
}
	</style>--%>
<div class="row no-padding margin-bottom-25 margin-2">
    <h5 class="font-size-18 margin-bottom-0 margin-top-0 instruction-details underline">
        <asp:Label ID="lblPAText" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, preApprovalTxt %>"></asp:Label>
    </h5>
</div>
<div class="moe-min-height margin-bottom-25">
    <div class="row unheighlighted-section">

        <!--Year Of Study-->
        <div class="col-md-4 col-sm-6 col-xs-12 no-padding  padd-2">
            <div class="data-container table-display moe-full-width">
                <div class="form-group">
                    <h6 class="font-size-16 margin-bottom-15 margin-top-0">
                        <asp:Label ID="lblYear" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, Year %>"></asp:Label>
                        <span class="astrik error-msg">*</span>
                    </h6>

                    <form>

                        <asp:TextBox ID="txtYear" runat="server" ClientIDMode="Static" CssClass="datepicker moe-full-width input-height-42 border-box moe-input-padding" MaxLength="4"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqYear" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RequiredYear  %>" ControlToValidate="txtYear" CssClass="moe-full-width error-msg" ValidationGroup="Submit" Display="Dynamic"></asp:RequiredFieldValidator>
                        <%--                        <asp:RegularExpressionValidator ID="regYear" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RegNumbersOnly %>" ValidationExpression="^(?:20(?:1[5-9]|[2-9][0-9])|2[1-9][0-9][0-9]|[3-9][0-9][0-9][0-9])$" ControlToValidate="txtYear" CssClass="moe-full-width error-msg" ValidationGroup="Submit" Display="Dynamic"></asp:RegularExpressionValidator>                        --%>
                        <asp:RegularExpressionValidator ID="regYear" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RegNumbersOnly %>" ValidationExpression="^[0-9]{4}$" ControlToValidate="txtYear" CssClass="moe-full-width error-msg" ValidationGroup="Submit" Display="Dynamic"></asp:RegularExpressionValidator>
                        <label id="lblYearMsg" class="moe-full-width error-msg" style="display: none">
                            <asp:Literal runat="server" Text="&lt;%$Resources:ITWORX_MOEHEWF_PA,YearMsg%&gt;" />
                        </label>

                        <%--<asp:Label ID="lblYearMsg" runat="server" CssClass="moe-full-width error-msg" Text="<%$Resources:ITWORX_MOEHEWF_PA, Year %>" style="display:none;"></asp:Label>--%>
                    </form>
                </div>
            </div>
        </div>

        <asp:Panel ID="pnlUniversities" runat="server" ClientIDMode="Static">
            <!--Country Of Study-->
            <div class="col-md-4 col-sm-6 col-xs-12 no-padding">
                <div class="data-container table-display moe-full-width">

                    <div class="form-group" id="ddlCountry">
                        <uc1:DropdownWithTextbox runat="server" id="ddlCalcSectionCountry" />
                    </div>
                </div>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12  no-padding">
                <div class="data-container table-display moe-full-width">

                    <div class="form-group" id="ddlUni">

                        <uc1:DropdownWithTextbox runat="server" id="ddlCalcSectionUniversity" />

                        <span id="spanReqUniversity" style="display: none" class="moe-full-width error-msg">
                            <asp:Literal Text="<%$Resources:ITWORX_MOEHEWF_PA,RequiredUniversities %>" runat="server"> </asp:Literal>
                        </span>
                    </div>
                </div>
            </div>

            <!--Universities List-->
            <div class="col-md-4 col-sm-6 col-xs-12 no-padding" style="display: none">
                <div class="data-container table-display moe-full-width">

                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                            <asp:Label ID="lblUniversityList" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, UniversitiesLists  %>"></asp:Label></h6>

                        <asp:TextBox ID="txtUniversityListValue" runat="server" ClientIDMode="Static" ReadOnly="true" CssClass="disabled moe-full-width input-height-42 moe-input-padding"></asp:TextBox>

                        <span id="requestFailed" style="display: none" class="moe-full-width error-msg"></span>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</div>
<asp:Button ID="btnHdn" runat="server" Text="Button" Style="display: none;" />
 <cc1:ModalPopupExtender ID="modalPopUpConfirmation" runat="server"
        TargetControlID="btnHdn"
        PopupControlID="pnlConfirmation" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlConfirmation" runat="server" Style="display: none;" CssClass="modalPopup">
         <asp:Label ID="lblSubmissionMsg" runat="server" Font-Bold="true" ForeColor="Green" ></asp:Label>
     
        <br />
        <br />

         <asp:Button ID="btnModalOK" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, Ok %>" OnClick="btnModalOK_Click" CssClass="btn moe-btn" />
    </asp:Panel>
<script type="text/javascript">

    function hideOther() {
     
        $('select[id*=ddlCalcSectionCountry]').find('option:last-child').remove();
        $('select[id*=ddlCalcSectionUniversity]').find('option:last-child').remove();
    }

    $(document).ready(function () {

        //hideOther();

        var oldYear = $('#txtYear').val();

        $("#dropUniversity").change(function () {

            if ($("#dropUniversity option:selected").val() == "") {
                $("#spanReqUniversity").show();
                $("#txtUniversityListValue").val("");
                $("#hdnUniversityListValue").val("");
                return false;
            }
            else {
                $("#spanReqUniversity").hide();
            }
        });

        function ValidateUniversities() {
            var returnValue = true;
            if ($("#pnlUniversities").css("display") == "block") {
                if ($("#dropUniversity option:selected").val() == "") {
                    $("#spanReqUniversity").show();

                }
                else {
                    $("#spanReqUniversity").hide();

                }
            }

            if (($("#spanReqUniversity").css("display") != "none" && $("#spanReqUniversity").css("visibility") == "visible") ||
                ($("#reqCountry").css("display") != "none" && $("#reqCountry").css("visibility") == "visible")) {
                returnValue = false;

            }
            return returnValue;
        }

        $('#txtYear').focusout(function (e) {

            if (oldYear != '') {

                setTimeout('__doPostBack(\'<%=ddlCalcSectionCountry.DropWithNewOption.UniqueID%>\',\'\')', 0)

            }
        })
        $('#txtYear').blur(function (e) {
            var nextYear = (new Date().getFullYear() + 1);
            var currentYears = new Date().getFullYear();
            var txt_year = $('#txtYear').val();
            if (txt_year != '') {
                var NYear = Number(txt_year);
                if (NYear != NaN) {
                    if (NYear == currentYears || NYear == nextYear) {

                        $('#lblYearMsg').hide();
                    }
                    else {
                        $('#lblYearMsg').show();
                        return false;

                    }
                }
            }
        });

    });
</script>