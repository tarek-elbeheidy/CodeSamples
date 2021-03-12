<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PAClarificationReqDetails.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.PAClarificationReqDetails" %>
<asp:HiddenField ID="hdn_ID" runat="server" Value='<%# Eval("ID") %>' />
<style>
    .heighlighted-section.row>div.col-md-12,
    .heighlighted-section.row>div.auto-height{
        min-height: 1px;
    }
</style>
<div class="row no-padding heighlighted-section margin-bottom-50 flex-display flex-wrap">
    <div class="col-md-4 col-sm-6 col-xs-12 no-padding auto-height">
        <div class="data-container table-display moe-width-85">
            <div class="form-group">
                <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                    <asp:Label ID="lbl_RequestID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, RequestID %>"></asp:Label>
                </h6>

                <h5 class="font-size-22">
                    <asp:Label ID="lbl_RequestIDVal" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
    </div>

    

   

    <div class="col-md-4 col-sm-6 col-xs-12 no-padding auto-height">
        <div class="data-container table-display moe-width-85">
            <div class="form-group">
                <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                    <asp:Label ID="lbl_RequestClarificationDate" runat="server" Text=" <%$Resources:ITWORX_MOEHEWF_PA, ClarRequestedDate %>"></asp:Label>
                </h6>

                <h5 class="font-size-22">
                    <asp:Label ID="lbl_RequestClarificationDateVal" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
    </div>
        <div class="col-md-4 col-sm-6 col-xs-12 no-padding auto-height">
        <div class="data-container table-display moe-width-85">
            <div class="form-group">
                <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                    <asp:Label ID="lbl_ReplyDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, ReplyDate %>" Visible="false"></asp:Label>
                </h6>

                <h5 class="font-size-22">
                    <asp:Label ID="lbl_ReplyDateVal" runat="server" Visible="false"></asp:Label>
                </h5>
            </div>
        </div>
    </div>
   
     <div class="col-md-8 col-sm-6 col-xs-12 no-padding auto-height">
        <div class="data-container table-display moe-width-85">
            <div class="form-group">
                <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                    <asp:Label ID="lbl_RequestSender" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, ClarRequestSender %>"></asp:Label>
                </h6>

                <h5 class="font-size-22">
                    <asp:Label ID="lbl_RequestSenderVal" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
    </div>
      <div class="col-md-4 col-sm-6 col-xs-12 no-padding auto-height">
        <div class="data-container table-display moe-width-85">
            <div class="form-group">
                <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                    <asp:Label ID="lbl_AssignedTo" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, ReplySender %>" Visible="false"></asp:Label>
                </h6>

                <h5 class="font-size-22">
                    <asp:Label ID="lbl_AssignedToVal" runat="server" Visible="false"></asp:Label>
                </h5>
            </div>
        </div>
    </div>
    <div class="col-md-12 col-sm-12 col-xs-12 no-padding auto-height">
        <div class="data-container table-display moe-width-85">
            <div class="form-group">
                <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                    <asp:Label ID="lbl_ClarificationReply" runat="server" Text=" <%$Resources:ITWORX_MOEHEWF_PA, ClarReply %>" Visible="false"></asp:Label>
                </h6>

                <h5 class="font-size-22">
                    <asp:Label ID="lbl_ClarificationReplyVal" runat="server" Visible="false"></asp:Label>
                </h5>
            </div>
        </div>
    </div>



   

    <div class="col-md-12 col-sm-12 col-xs-12 no-padding auto-height">
        <div class="data-container table-display moe-full-width">
            <div class="form-group">
                <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                    <asp:Label ID="lbl_RequestedClarification" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, ClarRequested %>"></asp:Label>
                </h6>

                <h5 class="font-size-18">
                    <asp:Label ID="lbl_RequestedClarificationVal" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
    </div>
</div>

<br />
<div class="col-md-12 no-padding">
    <asp:Button ID="btn_Close" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, Close %>" OnClick="btn_Close_Click" CssClass="btn moe-btn pull-right" />
</div>