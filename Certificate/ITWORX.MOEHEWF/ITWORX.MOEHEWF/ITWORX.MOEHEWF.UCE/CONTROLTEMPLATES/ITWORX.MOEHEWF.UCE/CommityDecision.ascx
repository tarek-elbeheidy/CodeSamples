<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommityDecision.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.CommityDecision" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="uc1" TagName="FileUpload" %>
<style>
    .moe-radioBtn.moe-radioBtn-row tbody > tr {
        width: auto !important;
        margin-left: 50px;
    }

    .moe-section-title {
        color: #8d163a;
    }

    .moe-section-cntnr {
        border: 1px solid #eaebed;
        padding: 10px;
    }

    .moe-fieldset legend {
        text-align: right !important;
    }
    .dark-color{
        color:#000;
    }
</style>
<script> 
    $(function () {

        $('#txt_CommityDate').attr('readonly', true);
        $('#txt_CommityDate').datepicker({
            dateFormat: "dd/mm/yy",
            changeYear: true,
            changeMonth: true

        });
    });

</script>

<div class="container unheighlighted-section  margin-top-25 flex-display flex-wrap moe-fieldset">
    <asp:Panel ID="pnl_CommityDecision" runat="server" GroupingText="<%$Resources:ITWORX_MOEHEWF_UCE, CommityDecision%>" CssClass="stateTitle moe-full-width">
        <%--<fieldset class="stateTitle moe-full-width">
        <legend>df sdf sf sds  </legend>--%>
        <div class="moe-section-cntnr">
            <div class="row margin-top-15">
                <div class="col-md-6 col-sm-12 col-xs-12 no-padding">
                    <div class="data-container table-display ">
                        <div class="form-group">
                            <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                <asp:Label ID="lbl_RejectionReason" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CommityDecision %>"></asp:Label>
                            </h6>
                            <div class="form">
                                <asp:DropDownList ID="drp_RejectionReason" runat="server" CssClass="moe-dropdown moe-full-width input-height-42 border-box moe-input-padding"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6 col-sm-12 col-xs-12 no-padding">
                    <div class="data-container table-display ">
                        <div class="form-group">
                            <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                                <asp:Label ID="lbl_inDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, inDate %>"></asp:Label>
                                <span class="error-msg">*</span>
                            </h6>
                        </div>
                        <div class="form">
                            <asp:TextBox ID="txt_CommityDate" ClientIDMode="Static" runat="server" CssClass="moe-full-width input-height-42 border-box moe-input-padding"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="" Display="Dynamic"
                                ValidationGroup="Approve" ControlToValidate="txt_CommityDate" CssClass="moe-full-width error-msg"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                </div>
            </div>


            <div id="fileCert" class="row margin-top-15">
                <div class="col-md-5 col-sm-12 col-xs-12">
                    <h5 class="font-size-18 margin-bottom-10 margin-top-0"  >
                          <asp:Label ID="lblFiles" CssClass="dark-color" runat="server" Text="Attachments" ></asp:Label>
                    </h5>
                    <uc1:FileUpload runat="server" id="fileUploadCertificates" />

                    <asp:CustomValidator ID="custValidateFileUploadCertificates"
                        runat="server" CssClass="moe-full-width error-msg" ValidationGroup="Submit"
                        Display="Dynamic" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredAllCertificates %>"
                        OnServerValidate="custValidateFileUploadCertificates_ServerValidate"></asp:CustomValidator>
                </div>
            </div>
        </div>
        <%--  </fieldset>--%>
    </asp:Panel>
</div>



