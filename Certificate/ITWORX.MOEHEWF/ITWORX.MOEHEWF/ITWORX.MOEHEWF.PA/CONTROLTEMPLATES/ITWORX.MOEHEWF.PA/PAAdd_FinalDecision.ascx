<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.PA/PARolesNavigationLinks.ascx" TagPrefix="uc1" TagName="PARolesNavigationLinks" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PAAdd_FinalDecision.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.Add_FinalDecision" %>

<div id="main-content">
    <div class="row section-container side-nav-container">

        <!-- required for floating -->

        <div class="row unheighlighted-section margin-bottom-50 flex-display flex-wrap">

            <div class="col-md-3 col-sm-6 margin-top-10 margin-bottom-10">
                <div class="data-container table-display moe-width-85">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-15">

                            <asp:Label ID="lbl_Decision" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, FinalDecision %>"></asp:Label>
                        </h6>

                        <asp:DropDownList ID="drp_FinalDecision" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drp_FinalDecision_SelectedIndexChanged" CssClass="moe-full-width input-height-42 border-box moe-input-padding moe-dropdown">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqVal_FinalDecision" runat="server" ForeColor="Red" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RequiredProcedure %>"
                            ValidationGroup="Approve" ControlToValidate="drp_FinalDecision" CssClass="error-msg moe-full-width"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>

            <div class="col-md-3 col-sm-6 margin-top-10 margin-bottom-10">
                <div class="data-container table-display moe-width-85">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                            <asp:Label ID="lbl_RejectionReason" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, RejectionReason %>" Visible="false"></asp:Label>
                        </h6>
                        <asp:DropDownList ID="drp_RejectionReason" runat="server" Visible="false" CssClass="moe-full-width input-height-42 border-box moe-input-padding"></asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="col-md-12 col-sm-12 col-xs-12 margin-top-12 margin-bottom-12">
                <div class="data-container table-display">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                            <asp:Label ID="lbl_Comments" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, ProcedureComments %>"></asp:Label>
                        </h6>

                        <asp:TextBox ID="txt_Comments" runat="server" TextMode="MultiLine" CssClass="moe-full-width input-height-42 border-box moe-input-padding text-area"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqVal_Comments" runat="server" ForeColor="Red" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RequiredComments %>"
                            ValidationGroup="Approve" ControlToValidate="txt_Comments" CssClass="error-msg moe-full-width"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div class="col-md-12 no-padding margin-top-10">
                <asp:Button ID="btn_ApproveDecision" ValidationGroup="Approve" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, ApproveDecision %>" OnClick="btn_ApproveDecision_Click" CssClass="btn moe-btn pull-right" />
                <asp:Button ID="btn_ReviewDecision" runat="server" OnClick="ReviewDecisionClick" Text="<%$Resources:ITWORX_MOEHEWF_PA, review %>" OnClientClick="return setFormSubmitToFalse();" CssClass="btn moe-btn pull-left" />
            </div>
            <div class="col-md-12 no-padding margin-top-10">
            </div>
            <div class="row margin-top-15">
                <div class="col-md-12 no-padding">
                </div>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
 function setFormSubmitToFalse() {
        setTimeout(function () { _spFormOnSubmitCalled = false; }, 3000);
        return true;
    }

 </script>