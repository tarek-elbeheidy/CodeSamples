<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewClarificationRequestSCE.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE.ViewClarificationRequestSCE" %>


<asp:HiddenField ID="hdn_ReqId" runat="server" />
<div class="view-clarification">
<div class="clarifyRequest">
    <div class="">
        <h4 class="pageTitle">
            <asp:Label ID="Label2" runat="server"></asp:Label></h4>
        <div class="dark-bg clarifyRequestEdit margin-top-25">
            <div class="col-md-4  col-sm-6 col-xs-12">
                <div class="form-group">
                    <label>
                        <asp:Label ID="lbl_RequestDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequestDate %>"></asp:Label>
                    </label>
                    <asp:TextBox ID="txt_RequestDate" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4 col-sm-6 col-xs-12">
                <div class="form-group">
                    <label>
                        <asp:Label ID="lbl_Requester" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequestClarSender %>"></asp:Label></label>
                    <asp:TextBox ID="txt_Requester" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>

                </div>
            </div>

            <div class="col-md-4  col-xs-12">
                <div class="form-group">
                    <label>
                        <asp:Label ID="lbl_Reason" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequestReason %>"></asp:Label></label>
                    <asp:TextBox ID="txt_Reason" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="clearfix">
            </div>



            <div class="col-xs-12 margin-top-15">
                <div class="form-group">
                    <label>
                        <asp:Label ID="lbl_NeededData" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, NeededReason %>"></asp:Label></label>
                    <asp:TextBox ID="txt_ReqClarification" runat="server" TextMode="MultiLine" ReadOnly="true" CssClass="form-control text-area"></asp:TextBox>
                </div>
            </div>
            <div class="clearfix">
            </div>

        </div>
    </div>
    <div id="dv_firstMsg" runat="server" class="">
                <h5 class="font-size-18 margin-bottom-0 margin-top-10 instruction-details color-black  font-family-sans success">

        <asp:Label ID="lbl_firstMsg" runat="server"  Text="<%$Resources:ITWORX_MOEHEWF_SCE, ClarifcationFirstMsg %>"></asp:Label></h5>
    </div>
  
    <div class="margin-top-50" id="dv_replyToClar" runat="server">
        <h4><asp:Label ID="lbl_ReplyToClar" runat="server"  Text="<%$Resources:ITWORX_MOEHEWF_SCE, ReplytoClarification %>"></asp:Label> 	</h4>
        <div class="row dark-bg clarifyRequestView ">
            
            <div class="col-md-3  col-sm-6 col-xs-12"  runat="server" id="dv_Reply" >
                <div class="form-group">
                    <label>
                        <asp:Label ID="lbl_ReplyDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ReplyDate %>"></asp:Label>
                    </label>
                    <asp:TextBox ID="txt_ReplyDate" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
            </div>

            <div class="clearfix">
            </div>
            <div class="col-xs-12 replyText">
                <div class="form-group">
                    <label>
                        <asp:Label ID="lbl_ApplicantReply" runat="server" ></asp:Label>
                        <span runat="server" id="reqReply" class="error-msg">*</span></label>
                    <asp:TextBox ID="txt_ApplicantReply" runat="server" TextMode="MultiLine" ReadOnly="true" CssClass="form-control text-area" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfv_ApplicantReply" runat="server" Visible="false" ControlToValidate="txt_ApplicantReply" CssClass="error-msg" ValidationGroup="ViewClar" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, Required %>"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txt_ApplicantReply" ID="rev_ApplicantReplyLength" CssClass="error-msg"
                        ValidationExpression="^[\s\S]{0,1000}$" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, NeededReasonLength %>"></asp:RegularExpressionValidator>


                </div>
            </div>

        </div>
    </div>
      <div>
        
    </div>

    <div class="" id="dv_secondMsg" runat="server">


        <h5 class="font-size-18 margin-bottom-0 margin-top-10 instruction-details color-black  font-family-sans success">

            <asp:Label ID="lbl_SecondMsg" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ClarifcationSecondMsg %>"></asp:Label>
        </h5>

    </div>

</div>


    </div>
<div class="formBtns clarificatinBtns">
    <div class="row">
        <div class="col-sm-6 col-xs-12 text-left">
            <asp:Button ID="btn_Back" runat="server" CssClass="moe-btn btn" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Back %>" OnClick="btn_Back_Click" />
            
        </div>
     
        <div class="col-sm-6 col-xs-12 text-right">
                        <asp:Button ID="btn_ReturnToReq" runat="server" CssClass="moe-btn btn" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ReturnToRequestPage %>" ValidationGroup="ViewClar" OnClick="btn_ReturnToReq_Click" Visible="false" />

            <asp:Button ID="btn_Send" runat="server" CssClass="moe-btn btn" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SendReply %>" ValidationGroup="ViewClar" OnClick="btn_Send_Click" Visible="false" />
        </div>
    </div>
</div>
<script type="text/javascript">

    $("#<%=txt_ApplicantReply.ClientID%>").attr('maxLength',1000);
</script>