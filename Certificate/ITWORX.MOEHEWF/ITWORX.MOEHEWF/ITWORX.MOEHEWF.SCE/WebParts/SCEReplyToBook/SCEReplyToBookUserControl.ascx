<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SCEReplyToBookUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.WebParts.SCEReplyToBook.SCEReplyToBookUserControl" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/ClientSideFileUpload.ascx" TagPrefix="uc1" TagName="ClientSideFileUpload" %>
<style>
    .moe-book-upload .moe-file-btn{
        padding-top: 19px;
    }
</style>
<asp:Panel ID="pnlReplyDetails" runat="server">

    <asp:ValidationSummary ID="vsGroup" runat="server" ValidationGroup="BookReply" Visible="false" />
    <asp:Label ID="lblExceptionMessage" runat="server" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ExceptionBooksRequestsMessage %>"></asp:Label>
    <div class="new-clarification">
        <!--Book Details-->
        <div class="clarifyRequest">
            <div class="">
                <h4 class="pageTitle">
                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEReplyHeader %>"></asp:Literal></h4>
                <div class="dark-bg request-padd margin-top-25">
                    <div class="col-md-3 col-xs-12">
                        <div class="form-group">
                            <label><asp:Label runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEBookNumber %>"></asp:Label></label>
                            <asp:TextBox ID="txtBookNumber" runat="server" CssClass="form-control" disabled="disabled"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3 col-md-offset-1 col-xs-12">
                        <div class="form-group">
                            <label><asp:Label runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEBookDate %>"> </asp:Label></label>
                            <asp:TextBox ID="txtBookDate" runat="server" class="form-control" placeholder="12/6/2018" disabled="disabled"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4 col-md-offset-1 col-xs-12">
                        <div class="form-group">
                            <label><asp:Label runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEBookSender %>"> </asp:Label></label>
                            <asp:TextBox ID="txtBookSender" CssClass="form-control" runat="server" disabled="disabled"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>

                    <div class="col-md-7 col-xs-12 margin-top-15">
                        <div class="form-group">
                            <label><asp:Label runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEBookSubject %>"> </asp:Label></label>
                            <asp:TextBox runat="server" ID="txtBookSubject" CssClass="form-control" disabled="disabled"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4 col-md-offset-1 col-xs-12 margin-top-15">
                        <div class="form-group">
                            <label><asp:Label runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEAuthority %>">   </asp:Label></label>
                            <asp:TextBox ID="txtAuthority" runat="server" CssClass="form-control" disabled="disabled">	</asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>

                    <div class="col-md-7 col-xs-12 margin-top-15">


                        <div class="form-group">
                            <label><asp:Label runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEAuthorityAddress %>">  </asp:Label></label>
                            <asp:TextBox Columns="20" Rows="10" TextMode="MultiLine" ID="txtAuthorityAddress" runat="server" CssClass="form-control text-area" disabled="disabled"></asp:TextBox>
                        </div>

                    </div>
                    <div class="col-md-4 col-md-offset-1 col-xs-12 margin-top-15">
                        <div class="form-group">
                            <label><asp:Label runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEAuthorityEmail %>"> </asp:Label></label>
                            <asp:TextBox ID="txtAuthorityEmail" runat="server" CssClass="form-control" disabled="disabled"></asp:TextBox>
                        </div>
                    </div>



                    <div class="clearfix">
                    </div>
                    <div class="col-xs-12 margin-top-15">
                        <div class="form-group">
                            <label><asp:Label runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEBookBody %>">  </asp:Label></label>
                            <asp:TextBox Columns="20" Rows="10" TextMode="MultiLine" ID="txtBookBody" runat="server" CssClass="form-control text-area " disabled="disabled"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="margin-top-10 moe-book-upload">
                       
                            <uc1:ClientSideFileUpload runat="server" id="FileUp1" />


                      
                    </div>





                </div>

            </div>
        </div>
        <!--Book Details-->

        <!--Book Reply-->
        <div class="clarifyRequest">
            <div class="">
                <h4 class="pageTitle">
                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEAuthorityReplyHeader %>"></asp:Literal>
                </h4>
                <div class="dark-bg request-padd margin-top-25">
                    <div class="col-md-3">
                        <div class="form-group">
                             <label><asp:Label runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEReplyNumber %>"> </asp:Label></label> <span class="error-msg">*</span>
                            <asp:TextBox ID="txtReplyNumber" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="BookReply" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE,SCEReplyNumberRequired %>" ControlToValidate="txtReplyNumber" runat="server" CssClass="error-msg"/>
                        </div>
                    </div>
                    <div class="col-md-3 col-md-offset-1">
                        <div class="form-group">
                             <label><asp:Label runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEReplyDate %>"> </asp:Label></label>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtReplyDate" disabled="disabled"></asp:TextBox>
                        </div>
                    </div>



                    <div class="clearfix">
                    </div>
                    <div class="col-xs-12 margin-top-15">
                        <div class="form-group">
                             <label><asp:Label runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEReplySummary %>"></asp:Label></label> <span class="error-msg">*</span>
                            <asp:TextBox ID="txtReplySummary" runat="server" Columns="20" Rows="10" TextMode="MultiLine" CssClass="form-control text-area "></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="BookReply" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE,SCEReplySummaryRequired %>" ControlToValidate="txtReplySummary" runat="server" CssClass="error-msg"/>
                    </div>
                    <div class="clearfix">
                    </div>
                    <div class="margin-top-10 moe-book-upload">
                            <uc1:ClientSideFileUpload runat="server" id="fileUpload2" />
                    </div>


                </div>

            </div>
        </div>
        <!--Book Reply-->

    </div>

    <div class="formBtns clarificatinBtns">
        <div class="row">
            <div class="col-sm-6 col-xs-12 text-left">
                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" CssClass="moe-btn btn" Text="<%$Resources:ITWORX_MOEHEWF_SCE,Back %>" />
            </div>
            <div class="col-sm-6 col-xs-12 text-right">
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" ValidationGroup="BookReply" Text="<%$Resources:ITWORX_MOEHEWF_SCE,Save %>" CssClass="moe-btn btn"></asp:Button>
            </div>
        </div>
    </div>


</asp:Panel>
