<%@ Assembly Name="ITWORX.MOEHEWF.UCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=883afb4c05a35fe5" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/DropdownWithTextbox.ascx" TagPrefix="uc1" TagName="DropdownWithTextbox" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CheckUniversity.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.CheckUniversity" %>

<%-- <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">  
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>  
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>  --%>

<div class="row no-padding margin-bottom-25 margin-2">
    <h5 class="font-size-18 margin-bottom-0 margin-top-0 instruction-details underline">
        <asp:Label ID="lblYearHint" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, YearHint %>"></asp:Label>
    </h5>

</div>



<div class="moe-min-height margin-bottom-25">


    <div class="row unheighlighted-section">
        <!--Year Of Study-->
        <div class="col-md-4 col-sm-6 col-xs-12 no-padding padd-2">
            <div class="data-container table-display moe-full-width">

                <div class="form-group">
                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                        <asp:Label ID="lblYear" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Year  %>"></asp:Label><span class="astrik error-msg"> *</span>

                    </h6>
                    <form>

                        <asp:TextBox ID="txtYear" runat="server" ClientIDMode="Static" CssClass="datepicker moe-full-width input-height-42 border-box moe-input-padding" MaxLength="4"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqYear" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredYear  %>" ControlToValidate="txtYear" CssClass="moe-full-width error-msg" ValidationGroup="Submit" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="regYear" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RegNumbersOnly %>" ValidationExpression="^\d+$" ControlToValidate="txtYear" CssClass="moe-full-width error-msg" ValidationGroup="Submit" Display="Dynamic"></asp:RegularExpressionValidator>

                    </form>
                </div>
            </div>
        </div>

        <asp:Panel ID="pnlUniversities" runat="server" ClientIDMode="Static" Style="display: none">
            <!--Country Of Study-->
            <div class="col-md-4 col-sm-6 col-xs-12 no-padding">
                <div class="data-container table-display moe-full-width">

                    <div class="form-group" id="ddlCountry">
                        <%--<h6 class="font-size-16 margin-bottom-10 margin-top-15">
                            <asp:Label ID="lblCountry" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CountryOfStudy  %>"></asp:Label>

                        </h6>--%>
                        <%-- <form>--%>
                        <uc1:DropdownWithTextbox runat="server" id="ddlCalcSectionCountry" />
                        <%-- </form>--%>
                    </div>
                </div>
            </div>
            <div class="col-md-4 col-sm-12 col-xs-12  no-padding">
                <div class="data-container table-display moe-full-width">

                    <div class="form-group" id="ddlUni">
                        <%-- <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                            <asp:Label ID="lblUniversity" runat="server"  Text="<%$Resources:ITWORX_MOEHEWF_UCE, Universities  %>"></asp:Label>

                        </h6>--%>
                        <%--  <form>--%>
                        <uc1:DropdownWithTextbox runat="server" id="ddlCalcSectionUniversity" />

                        <span id="spanReqUniversity" style="display: none" class="moe-full-width error-msg"><%=Resources.ITWORX_MOEHEWF_UCE.RequiredUniversities %></span>

                    </div>
                </div>
            </div>


            <!--Universities List-->
            <div class="col-md-4 col-sm-6 col-xs-12 no-padding" style="display: none" id="uniList">
                <div class="data-container table-display moe-full-width">

                    <div class="form-group">

                        <%-- </form>--%>
                    </div>
                </div>
            </div>

        </asp:Panel>
    </div>
    <h2 class="font-size-18 margin-bottom-50 font-family-sans margin-top-0">
        <asp:Label ID="lblUniversityNotListed" runat="server" ClientIDMode="Static" Text=""></asp:Label></h2>
</div>
<script type="text/javascript">


    $(document).ready(function () {

        var oldYear = $('#txtYear').val();
        $("#txtYear").keydown(function (e) {
            // Allow: backspace, delete, tab, escape, enter and .
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
                // Allow: Ctrl+A, Command+A
                (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: home, end, left, right, down, up
                (e.keyCode >= 35 && e.keyCode <= 40)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });

        $('#txtYear').focusout(function (e) {

            if (oldYear != '') {

                setTimeout('__doPostBack(\'<%=ddlCalcSectionCountry.DropWithNewOption.UniqueID%>\',\'\')', 0)
            }
        })

        $('#txtYear').on('input', function (e) {

           <%-- $("#dropUniversity").empty().append($("<option></option>").val("").html("<%= Select %>"));--%>
            if ($('#txtYear').val() == "" || parseInt($('#txtYear').val()) <  <%= StudyYear %> ) {
                //if (parseInt($('#txtYear').val()) < 2015) {
                $('#pnlUniversities').hide();
                var countryDrop = $("#ddlCountry").find('[id$=reqDropWithNewOption]');
                ValidatorEnable(document.getElementById(countryDrop[0].id), false);
                var countryDropText = $("#ddlCountry").find('[id$=reqNewOptionText]');
                ValidatorEnable(document.getElementById(countryDropText[0].id), false);

                var uniDrop = $("#ddlUni").find('[id$=reqDropWithNewOption]');
                ValidatorEnable(document.getElementById(uniDrop[0].id), false);
                var uniDropText = $("#ddlUni").find('[id$=reqNewOptionText]');
                ValidatorEnable(document.getElementById(uniDropText[0].id), false);
            }
            else {
                $('#pnlUniversities').show();
                $('#pnlUniversities').focus();
            }
        });
        if (parseInt($('#txtYear').val()) >=  <%= StudyYear %>) {

            $('#pnlUniversities').show();
            $("#dropUniversity").empty().append($("<option></option>").val("").html("<%= Select %>"));

        }
        if ($("hdnUniversityDropId").val() != "") {
            $("#dropUniversity option:selected").val($("#hdnUniversityDropId").val());
        }



    });

</script>
