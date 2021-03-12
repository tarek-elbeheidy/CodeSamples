<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SCEStatementReplyUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.WebParts.SCEStatementReply.SCEStatementReplyUserControl" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/ClientSideFileUpload.ascx" TagPrefix="uc1" TagName="ClientSideFileUpload" %>
<style>
    .moe-statement-upload .moe-file-btn{
        padding-top: 19px;
    }
</style>

<div class="new-clarification">
    <div class="clarifyRequest">
        <div class=""> 
            <h6 class="">
            <asp:Label ID="Label10" runat="server" CssClass="pageTitle" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SendedStatementRequest %>"></asp:Label>
                </h6>
            <div class="dark-bg request-padd margin-top-25">
                <div class="col-md-4">
                    <div class="form-group"> 
                        <label><asp:Label ID="Label9" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StatementDate %>"></asp:Label> </label>
                        <asp:TextBox ID="txt_statementDate" runat="server" Enabled="false" CssClass="form-control" ></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4 col-md-offset-1">
                    <div class="form-group"> 
                       <label> <asp:Label ID="Label8" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StatementSubject %>"></asp:Label> </label>
                        <asp:TextBox ID="txt_statementTopic" runat="server" Enabled="false" CssClass="form-control" ></asp:TextBox>
                    </div>
                </div>
                <div class="clearfix ">
                </div>
              
                
                
                <div class="col-md-4   margin-top-15">
                    <div class="form-group"> 
                        <label><asp:Label ID="Label7" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StatementAgency %>"></asp:Label></label> 
                        <asp:TextBox ID="txt_statementAgency" runat="server" Enabled="false" CssClass="form-control" ></asp:TextBox>
                    </div>
                </div>
                  <div class="col-md-4 col-md-offset-1 margin-top-15">
                    <div class="form-group"> 
                        <label><asp:Label ID="Label6" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StatementSender %>"></asp:Label></label> 
                        <asp:TextBox ID="txt_Sender" runat="server" Enabled="false" CssClass="form-control" ></asp:TextBox>
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="col-xs-12 margin-top-15">
                    <div class="form-group"> 
                        <label><asp:Label ID="Label5" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequiredStatement %>"></asp:Label></label>
                        <asp:TextBox ID="txt_RequiredStatement" runat="server" CssClass="form-control text-area" Enabled="false" TextMode="MultiLine" Rows="10" Columns="20" ></asp:TextBox>                         
                    </div>
                </div>
                <div class="clearfix">
                </div>
            </div>
        </div>
    </div>
</div>
<div class="new-clarification no-padding-top">
  <div class="clarifyRequest">
      <h6 class="">
    <asp:Label ID="Label4" runat="server" CssClass="pageTitle" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StatementReplay %>"></asp:Label>
          </h6>
    <div class="dark-bg request-padd margin-top-25">
        <div class="col-md-4 col-xs-12">
            <div class="form-group"> 
                <label><asp:Label ID="Label3" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ReplyDate %>"></asp:Label> </label>
                <asp:TextBox ID="txt_ReplayDate" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>
            </div>
        </div>
                                 <div class="col-md-4 col-md-offset-1 col-xs-12">
            <div class="form-group"> 
                <label><asp:Label ID="Label2" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SenderOfReplay %>"></asp:Label></label> 
                <asp:TextBox ID="txt_ReplayBy" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="clearfix">
        </div>
        
        <div class="col-xs-12 margin-top-15">
            <div class="form-group"> 
                <label><asp:Label ID="Label1" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StatementReplay %>"></asp:Label></label><span class="error-msg"> *</span>
                 
                <asp:TextBox ID="txt_Replay" runat="server" CssClass="form-control text-area" TextMode="MultiLine" Columns="20" Rows="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, StatementReplayRequired %>" ValidationGroup="StatementGroup" CssClass="error-msg" ControlToValidate="txt_Replay"  Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="clearfix">
        </div>
       <div class="margin-top-10 moe-statement-upload">
            <uc1:ClientSideFileUpload runat="server" id="AddFile" />    		 
    	</div> 
    </div>
</div>
</div>
<div class="formBtns clarificatinBtns">
    <div class="row">
        <div class="col-sm-6 col-xs-12 text-left">
 
            <asp:Button ID="btn_Back" runat="server" CssClass="moe-btn btn" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Back %>" OnClick="btn_Back_Click" />
        </div>
        <div class="col-sm-6 col-xs-12 text-right"> 
            <asp:Button ID="btn_Save" runat="server" CssClass="moe-btn btn" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Save %>" OnClick="btn_Save_Click" ValidationGroup="StatementGroup" />
            <asp:Button ID="btn_Submit" runat="server" CssClass="moe-btn btn" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SendReply %>" OnClick="btn_Submit_Click" ValidationGroup="StatementGroup"/>
 
           
        </div>
    </div>
</div>
<asp:HiddenField ID="RequestID_HF" runat="server" />
