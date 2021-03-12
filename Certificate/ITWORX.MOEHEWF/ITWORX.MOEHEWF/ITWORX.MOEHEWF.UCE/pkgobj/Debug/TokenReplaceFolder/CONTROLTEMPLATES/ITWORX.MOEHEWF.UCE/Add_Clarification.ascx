<%@ Assembly Name="ITWORX.MOEHEWF.UCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=883afb4c05a35fe5" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Add_Clarification.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.Add_Clarification" %>

<div id="main-content">
    <div class="row section-container">
        <div class="row unheighlighted-section margin-bottom-50 flex-display flex-wrap">

            <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10 margin-bottom-10">
                <div class="data-container table-display moe-full-width">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                            <asp:Label ID="lbl_ClarRequested" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ClarRequested %>"></asp:Label>
                            <span class="astrik error-msg">*</span>
                        </h6>
                        <asp:TextBox ID="txt_ClarRequested" runat="server" TextMode="MultiLine" CssClass="moe-full-width input-height-42 border-box moe-input-padding text-area"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqVal_ClarRequested" runat="server" CssClass="error-msg moe-full-width" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredClarRequested %>"
                            ValidationGroup="NewClar" ControlToValidate="txt_ClarRequested"></asp:RequiredFieldValidator>
                        <asp:HiddenField ID="hdn_ID" runat="server" />
                    </div>
                </div>
            </div>
            <div class="col-md-12 no-padding margin-top-10">
                <div class="col-md-9 col-sm-6 col-xs-12">
                    <h6 class="font-size-18 margin-bottom-0 margin-top-0 height-auto instruct-title underline">
                        <asp:Label ID="lbl_Warning" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ClarificationWarning %>"></asp:Label>
                    </h6>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <asp:Button ID="btn_AddNewClarification" runat="server" OnClick="btn_AddNewClarification_Click" ValidationGroup="NewClar" Text="<%$Resources:ITWORX_MOEHEWF_UCE, AddClarification %>" CssClass="btn moe-btn pull-right" />
                </div>
            </div>
            <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10 margin-bottom-0">
                <div class="data-container table-display moe-full-width">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                            <asp:Label ID="lbl_ClarReply" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ClarReply %>"></asp:Label>
                        </h6>
                        <asp:TextBox ID="txt_ClarReply" runat="server" TextMode="MultiLine" CssClass="moe-full-width input-height-42 border-box moe-input-padding text-area"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqVal_ClarReply" runat="server" CssClass="error-msg moe-full-width" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredClarReply %>"
                            ValidationGroup="Reply" ControlToValidate="txt_ClarReply"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>

            <div class="col-md-12 col-xs-12 no-padding">
                                    <asp:CustomValidator ID="custReply" runat="server" CssClass="moe-full-width error-msg" ValidationGroup="Reply" Display="Dynamic" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, ClarificationNeededError %>" OnServerValidate="custReply_ServerValidate"></asp:CustomValidator>
                </div>
                <div class="col-md-12 col-sm-12 col-xs-12  no-padding-imp">

                    
                        <asp:LinkButton ID="lnkClarRequestDetails" runat="server" CssClass="btn moe-btn pull-left" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EditRequestDetails %>" OnClick="lnkClarRequestDetails_Click" Visible="false"></asp:LinkButton>
                 

                
                    <asp:Button ID="btn_Reply" runat="server" OnClick="btn_Reply_Click" ValidationGroup="Reply" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Reply %>" CssClass="btn moe-btn pull-right" />
                    <asp:Button ID="btn_Cancel" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, Cancel %>" CssClass="btn moe-btn pull-right f-tbn" OnClick="btn_Cancel_Click" />

                
            </div>
        </div>
    </div>
</div>
