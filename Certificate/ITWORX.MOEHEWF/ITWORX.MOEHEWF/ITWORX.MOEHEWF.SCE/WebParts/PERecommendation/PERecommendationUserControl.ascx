<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/ClientSideFileUpload.ascx" TagPrefix="uc1" TagName="ClientSideFileUpload" %>



<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PERecommendationUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.WebParts.PERecommendation.PERecommendationUserControl" %>
<style>
    .moe-recommend-upload .moe-file-btn{
        padding-top: 19px;
    }
</style>
<asp:HiddenField runat="server"  ID="hdnTextDecision" ClientIDMode="Static"/>
<div class="tab-pane tab-padd" id="div_Edit" role="tabpanel" runat="server" visible="false">
    <div class="row">
        <div class="col-xs-12">
            <h2 class="tab-title">
                <asp:Label ID="Label1" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, searchStatusReco %>"></asp:Label>
            </h2>
        </div>
    </div>

    <div class="row margin-top-25 margin-bottom-15">
        <div class="col-xs-12">
            <div class="form-group">
                <label>
                    <asp:Label ID="Label2" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, searchStatus %>"></asp:Label></label><span class="error-msg"> *</span>
                <asp:TextBox ID="txt_SearchStatus" CssClass="form-control text-area" TextMode="MultiLine" Rows="10" Columns="20" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, searchStatusRequired %>" CssClass="error-msg" ValidationGroup="RecommendationGroup" ControlToValidate="txt_SearchStatus"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="clearfix"></div>


        <div class="col-md-4 col-sm-6">
            <div class="form-group">
                <label>
                    <asp:Label ID="Label3" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, recommendation %>"></asp:Label></label>
                <span class="error-msg">*</span>

                <asp:DropDownList ID="ddl_Recommendation" runat="server" ClientIDMode="Static" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42" AutoPostBack="true" OnSelectedIndexChanged="ddl_Recommendation_SelectedIndexChanged"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, recommendationRequired %>" CssClass="error-msg" ValidationGroup="RecommendationGroup" ControlToValidate="ddl_Recommendation" InitialValue="-1"></asp:RequiredFieldValidator>

            </div>
        </div>




        <div class="col-md-5 ">
            <div class="form-group">
                <label>
                    <asp:Label ID="Label4" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, attendTo %>"></asp:Label></label>

                <asp:DropDownList ID="ddl_ScholasticLevel" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
            </div>
        </div>

        <div class="col-md-3 col-sm-6" id="assignRejected" style="display: none;">
            <div class="form-group">
                <label>
                    <asp:Label ID="Label5" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, convertTo %>"></asp:Label></label>
                <span class="error-msg">*</span>

                <asp:DropDownList ID="ddl_assignRejected" ClientIDMode="Static" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42" runat="server"></asp:DropDownList>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, convertToRequired %>" ValidationGroup="RecommendationGroup" CssClass="error-msg" OnServerValidate="Custom_ServerValidate" ClientValidationFunction="Custom_ClientValidate"></asp:CustomValidator>
            </div>
        </div>
        <div class="clearfix"></div>
        <div class="col-xs-12 margin-top-15">
            <div class="form-group">
                <label>
                    <asp:Label ID="Label6" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, decisionText %>"></asp:Label></label><span class="error-msg"> *</span>


                <asp:TextBox ID="txt_decision" ClientIDMode="Static" TextMode="MultiLine" Rows="10" Columns="20" CssClass="summernote form-control text-area" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv_decision" ClientIDMode="Static" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, decisionTextRequired %>" CssClass="error-msg" ValidationGroup="RecommendationGroup" ControlToValidate="txt_decision"></asp:RequiredFieldValidator>
                 <%--<asp:CustomValidator ID="CustomValidator2" runat="server"  ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, decisionTextRequired %>" ValidationGroup="RecommendationGroup" CssClass="error-msg"  ClientValidationFunction="Custom2_ClientValidate"></asp:CustomValidator>--%>
            </div>
        </div>


    </div>

    <div class="row margin-top-5">
        <div class="col-xs-12 text-right">
            <asp:Button ID="btn_Preview" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, preview %>" Enabled="false" CssClass="btn school-btn" OnClick="btn_Preview_Click" OnClientClick="LnkDownload(this.id)" />
        </div>
    </div>




    <div class="margin-top-10 moe-recommend-upload">
        <uc1:ClientSideFileUpload runat="server" id="AddFile" />
    </div>
    <div class="row margin-top-5">
        <div class="col-xs-12 text-right">


                            <asp:Button ID="btn_save" runat="server" CssClass="btn school-btn" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Save %>" OnClick="btnSave_Click" ValidationGroup="RecommendationGroup" ClientIDMode="Static" />
                             

                            <asp:Button ID="btn_submit" runat="server" CssClass="btn school-btn" Text="<%$Resources:ITWORX_MOEHEWF_SCE, submit %>" OnClick="btnSubmit_Click" ValidationGroup="RecommendationGroup"  ClientIDMode="Static" />
                    	</div>
                    </div>


</div>




<div class="tab-pane tab-padd" id="div_View" role="tabpanel" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <h2 class="tab-title">
                <asp:Label ID="Label7" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, searchStatusReco %>"></asp:Label>
            </h2>
        </div>
    </div>

    <div class="row margin-top-25 margin-bottom-15">
        <div class="col-xs-12 margin-bottom-15">
            <div class="form-group">
               <label> <asp:Label ID="Label8" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, searchStatus %>"></asp:Label></label>
                <asp:Label ID="lbl_SearchStatus" runat="server" Text="-----" CssClass="block-display"></asp:Label>
            </div>
        </div>

        <div class="clearfix"></div>

        <div class="col-md-4 col-sm-6 margin-bottom-15">
            <div class="form-group">
                <label><asp:Label ID="Label9" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, recommendation %>"></asp:Label></label>


                <asp:Label ID="lbl_Recommendation" runat="server" Text="-----"  CssClass="block-display"></asp:Label>
            </div>
        </div>

        <div class="col-md-5 ">
            <div class="form-group">
                <label><asp:Label ID="Label10" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, attendTo %>"></asp:Label></label>
                <asp:Label ID="lbl_ScholasticLevel" runat="server" Text="-----"  CssClass="block-display"></asp:Label>
            </div>
        </div>

        <div class="col-md-3 col-sm-6" runat="server" visible="false" id="assignRejectedDiv">
            <div class="form-group">
                <label><asp:Label ID="Label11" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, convertTo %>"></asp:Label></label>

                <asp:Label ID="lbl_assignRejected" runat="server" Text="-----"  CssClass="block-display"></asp:Label>
            </div>
        </div>
        <div class="clearfix"></div>
        <div class="col-xs-12 margin-top-15">
            <div class="form-group">
               <label> <asp:Label ID="Label12" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, decisionText %>"></asp:Label></label>


                <asp:Label ID="lbl_decision" runat="server" Text="-----"  CssClass="block-display"></asp:Label>
            </div>
        </div>


    </div>

    <div class="row">

        <uc1:ClientSideFileUpload runat="server" id="ViewFile" />
    </div>
</div>
<input type="hidden" value="" id="__EventTriggerRecommendControl" name="__EventTriggerRecommendControl" />

<link href="/_layouts/15/ITWORX.MOEHEWF.SCE/js/summernote.css" rel="stylesheet" />
<script src="/_layouts/15/ITWORX.MOEHEWF.SCE/js/summernote.js"></script>
<script src="/_layouts/15/ITWORX.MOEHEWF.SCE/js/summernote-ar-AR.js"></script>

<script type="text/javascript">
    $(document).on("show.bs.modal", '.modal', function (event) {
        $('.summernote').summernote();
    })
    $(document).ready(function () {
        var lcid = _spPageContextInfo.currentLanguage;
        if (lcid == '1025') {
            $('.summernote').summernote({
                lang: "ar-AR",
                disableResizeEditor: true,
                height: 200,

                toolbar: [
                    ['style', ['style']],
                    ['style', ['bold', 'italic', 'underline', 'clear']],
                    // ['fontname', ['fontname']],
                    ['font', ['strikethrough', 'superscript', 'subscript']],
                    ['fontsize', ['fontsize']],
                    ['color', ['color']],
                    ['para', ['ul', 'ol', 'paragraph']],
                    ['height', ['height']],
                    ['link', ['link']],
                    // ['table', ['table']], 
                    ["view", ["fullscreen", "codeview"]]

                ]
            });
        }
        else {
            $('.summernote').summernote({

                disableResizeEditor: true,
                height: 200,

                toolbar: [
                    ['style', ['style']],
                    ['style', ['bold', 'italic', 'underline', 'clear']],
                    // ['fontname', ['fontname']],
                    ['font', ['strikethrough', 'superscript', 'subscript']],
                    ['fontsize', ['fontsize']],
                    ['color', ['color']],
                    ['para', ['ul', 'ol', 'paragraph']],
                    ['height', ['height']],
                    ['link', ['link']],
                    //  ['table', ['table']],
                    ["view", ["fullscreen", "codeview"]]

                ]
            });
        }
        //  $('#txt_decision').val('');
    });

    function ValidateDecision() {
        debugger;
        var valid = false;
        if ($('.summernote').summernote('isEmpty')) {
            $('#txt_decision').val('');
            window.ValidatorValidate(window.rfv_decision);
             valid = window.rfv_decision.isvalid;
           
        }
        else {
            $("#hdnTextDecision").val($('#txt_decision').val());
        
            valid = true;
        }
        return valid;

    }

    $(function () {
        showHide();
    })
    $("#btn_save").click(function () {
        var valid = ValidateDecision();
        if (valid == false)
            return false;
    });
    $("#btn_submit").click(function () {
        var valid = ValidateDecision();
        if (valid == false)
            return false;
    });
    $("#ddl_Recommendation").change(function () {

        showHide();
    })

    function showHide() {
        if ($("#ddl_Recommendation").val() == "1" || $("#ddl_Recommendation").val() == "-1")
            $("#assignRejected").hide();
        else
            $("#assignRejected").show();
    }

    function Custom_ClientValidate(sender, argument) {
        if ($("#ddl_Recommendation").val() == "2" && $("#ddl_assignRejected").val() == "-1") {
            argument.IsValid = false;
        }
    }
    //function Custom2_ClientValidate(sender, argument) {

    //    if ($('.summernote').summernote('isEmpty')) {
    //        $('#txt_decision').val('');
    //        argument.IsValid = false;
    //        // window.ValidatorValidate(window.rfv_decision);
    //        //var valid = window.rfv_decision.isvalid;
    //        //return valid;
    //    }
    //    else {
    //        argument.IsValid = true;
    //    }
    //}

    function LnkDownload(eventControl) {
        //  debugger;
        var ctlId = document.getElementById("__EventTriggerSearchControl");
        if (ctlId) {
            ctlId.value = eventControl;
        }

        window.WebForm_OnSubmit = function () { return true; };
    }

</script>
