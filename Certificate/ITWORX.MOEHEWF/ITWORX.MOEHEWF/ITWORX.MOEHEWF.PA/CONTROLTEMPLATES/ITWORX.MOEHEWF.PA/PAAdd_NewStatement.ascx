<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="MOEHE" TagName="FileUpload" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PAAdd_NewStatement.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.Add_NewStatement" %>

<style>
    .margin-title h6 {
        margin-bottom: 5px !important;
    }
</style>

<div id="main-content">
    <div class="row section-container margin-bottom-50">
        <asp:Panel ID="pnl_PANewRequest" runat="server" GroupingText="<%$Resources:ITWORX_MOEHEWF_PA, NewStatementRequest %>" CssClass="stateTitle">

            <div class="row heighlighted-section margin-bottom-0 flex-display flex-wrap margin-0">
                <asp:HiddenField ID="hdn_ID" runat="server" />

                <div class="col-md-4 col-sm-12 col-xs-12 margin-top-10 margin-bottom-10 auto-height">
                    <div class="data-container table-display moe-width-85">
                        <div class="form-group">
                            <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                                <asp:Label ID="lbl_StatementDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, StatementRequestDate %>"></asp:Label>
                            </h6>
                            <h5>
                                <asp:Label ID="lbl_StatementDateVal" runat="server"></asp:Label>
                            </h5>
                        </div>
                    </div>
                </div>

                <div class="col-md-4 col-sm-12 col-xs-12 margin-top-10 margin-bottom-10 auto-height">
                    <div class="data-container table-display moe-width-85">
                        <div class="form-group">
                            <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                                <asp:Label ID="lbl_StatementCreatedby" runat="server" Text=" <%$Resources:ITWORX_MOEHEWF_PA, StatementCreatedBy %> "></asp:Label>
                            </h6>
                            <h5>
                                <asp:Label ID="lbl_StatementCreatedbyVal" runat="server"></asp:Label>
                            </h5>
                        </div>
                    </div>
                </div>

                <div class="col-md-4 col-sm-12 col-xs-12 margin-top-10 margin-bottom-10 auto-height">
                    <div class="data-container table-display moe-width-85">
                        <div class="form-group">
                            <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                                <asp:Label ID="lbl_ReqID" runat="server" Text=" <%$Resources:ITWORX_MOEHEWF_PA, RequestID %> "></asp:Label>
                            </h6>
                            <h5>
                                <asp:Label ID="lbl_ReqIDVal" runat="server"></asp:Label>
                            </h5>
                        </div>
                    </div>
                </div>
            </div>

            <%-- Values to be inserted --%>

            <div class="row unheighlighted-section margin-bottom-0 flex-display flex-wrap margin-2">

                <div class="col-md-4 col-sm-12 col-xs-12 margin-top-10 margin-bottom-10 ">
                    <div class="data-container table-display moe-width-85">
                        <div class="form-group">
                            <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                                <asp:Label ID="lbl_StatementSubject" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, StatementSubject %>"></asp:Label>
                                <span class="astrik error-msg">*</span>
                            </h6>
                            <asp:TextBox ID="txt_StatementSubject" runat="server" CssClass="moe-full-width input-height-42 border-box moe-input-padding"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqVal_StatementSubject" runat="server" ForeColor="Red" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RequiredStatementSubject %>"
                                ValidationGroup="SendStatementReq" CssClass="error-msg moe-full-width" ControlToValidate="txt_StatementSubject"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

                <div class="col-md-4 col-sm-12 col-xs-12 margin-top-10 margin-bottom-10" runat="server" id="directTo">
                    <div class="data-container table-display moe-width-85">
                        <div class="form-group">
                            <h6 class="font-size-16 margin-bottom-10 margin-top-15">

                                <asp:Label ID="lbl_DirectedTo" runat="server" Text=" <%$Resources:ITWORX_MOEHEWF_PA, StatementDirectedTo %> "></asp:Label>
                                <span class="astrik error-msg">*</span>
                            </h6>
                            <asp:DropDownList ID="drp_DirectedTo" runat="server" CssClass="moe-dropdown moe-full-width input-height-42 border-box moe-input-padding"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqVal_DirectedTo" runat="server" CssClass="error-msg moe-full-width" ForeColor="Red" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RequiredDirectedTo %>"
                                InitialValue="-1" ValidationGroup="SendStatementReq" ControlToValidate="drp_DirectedTo"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

                <div class="col-md-4 col-sm-12 col-xs-12 margin-top-10 margin-bottom-10" runat="server" id="ReplyControlstxt">
                    <div runat="server" id="Div2">
                        <div class="data-container table-display moe-width-85">
                            <div class="form-group">
                                <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                                    <asp:Label ID="lbl_DirectedtoReply" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, StatementDirectedTo %>"></asp:Label>
                                </h6>
                                <asp:TextBox ID="txt_DirectedTo" runat="server" CssClass="moe-full-width input-height-42 border-box moe-input-padding"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10 margin-bottom-10">
                    <div class="data-container table-display moe-full-width">
                        <div class="form-group">
                            <h6 class="font-size-16 margin-bottom-15 margin-top-0">
                                <asp:Label ID="lbl_StatementRequested" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, StatementRequested %>"></asp:Label>
                                <span class="astrik error-msg">*</span>
                            </h6>
                            <asp:TextBox ID="txt_StatementRequested" runat="server" CssClass="moe-full-width input-height-42 border-box moe-input-padding text-area" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqVal_StatementRequested" runat="server" CssClass="error-msg moe-full-width" ForeColor="Red" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RequiredStatementRequested %>"
                                ValidationGroup="SendStatementReq" ControlToValidate="txt_StatementRequested"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">

                <asp:Button ID="btn_SendStatementReq" runat="server" CssClass="btn moe-btn pull-right" OnClick="btn_SendStatementReq_Click" ValidationGroup="SendStatementReq" Text=" <%$Resources:ITWORX_MOEHEWF_PA, SendStatementReq %>" />
            </div>

            <div class="row">
                <div class="col-md-12 no-padding" runat="server" id="ReplyControlsdiv">
                    <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10 margin-bottom-10">
                        <div class="data-container table-display moe-full-width">
                            <div class="form-group">
                                <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                                    <asp:Label ID="lbl_StatDetailsReply" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, StatementReply %> "></asp:Label>
                                </h6>
                                <asp:TextBox ID="txt_StatDetailsReply" runat="server" CssClass="moe-full-width input-height-42 border-box moe-input-padding text-area" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-12 col-xs-12 no-padding">
                        <div class="form-group">
                            <div class="form margin-title">
                                <MOEHE:FileUpload runat="server" id="StatementReplyAttachements" />
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12 no-padding margin-top-15">
                        <asp:Button ID="btn_Reply" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, Reply %>" OnClick="btn_Reply_Click" CssClass="btn moe-btn pull-right" />
                        <asp:Button ID="btnBack" runat="server" CssClass="btn moe-btn pull-left" OnClick="btnBack_Click" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Back %>" />
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</div>