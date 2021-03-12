<%@ Assembly Name="ITWORX.MOEHEWF.SCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7c6ec0a86ef11fff" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewClarificationSCE.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE.NewClarificationSCE" %>

<div id="main-content">
    <div class="row section-container">
        <div class="row unheighlighted-section margin-bottom-50 flex-display flex-wrap">

            <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10 margin-bottom-10">
                <div class="data-container table-display moe-full-width">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                            <asp:Label ID="lbl_RequestDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequestDate %>"></asp:Label>

                        </h6>
                        <asp:TextBox ID="txt_RequestDate" runat="server" ReadOnly="true"></asp:TextBox>

                    </div>
                </div>
            </div>
            <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10 margin-bottom-10">
                <div class="data-container table-display moe-full-width">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                            <asp:Label ID="lbl_Requester" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequestClarSender %>"></asp:Label>
                        </h6>
                        <asp:TextBox ID="txt_Requester" runat="server" ReadOnly="true"></asp:TextBox>

                    </div>
                </div>
            </div>
            <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10 margin-bottom-10">
                <div class="data-container table-display moe-full-width">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                            <asp:Label ID="lbl_Reason" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequestReason %>"></asp:Label>

                        </h6>
                        <asp:DropDownList ID="ddl_Reasons" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfv_Reasons" runat="server" ControlToValidate="ddl_Reasons" ForeColor="Red" 
                            CssClass="error-msg moe-full-width" ValidationGroup="NewClar" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, Required %>"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>

            <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10 margin-bottom-10">
                <div class="data-container table-display moe-full-width">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                            <asp:Label ID="lbl_NeededData" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, NeededReason %>"></asp:Label>
                        </h6>
                        <asp:TextBox ID="txt_NeededData" runat="server" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_NeededData" ForeColor="Red" 
                            CssClass="error-msg moe-full-width" ValidationGroup="NewClar" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, Required %>"></asp:RequiredFieldValidator>
                  <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txt_NeededData" ID="rev_neededData" ForeColor="Red" 
                            CssClass="error-msg moe-full-width"
                      ValidationExpression = "^[\s\S]{0,1000}$" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, NeededReasonLength %>"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
        </div>

        <asp:Button ID="btn_Send" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SendRequest %>" ValidationGroup="NewClar" OnClick="btn_Send_Click"/>

        <asp:Button ID="btn_Back" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Back %>" OnClick="btn_Back_Click" />
    </div>
</div>

