<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/ClientSideFileUpload.ascx" TagPrefix="uc1" TagName="ClientSideFileUpload" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewSCEBooksRequestUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.WebParts.NewSCEBooksRequest.NewSCEBooksRequestUserControl" %>
<style>
    .moe-book-upload .moe-file-btn{
        padding-top: 19px;
    }
</style>
<asp:ValidationSummary ID="vsGroup" runat="server" ValidationGroup="AddBook"  Visible="false"/>
<asp:Label ID="lblExceptionMessage" runat="server" visible="false" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ExceptionBooksRequestsMessage %>"></asp:Label>
<asp:Panel ID="pnlRequestDetails" runat="server">
        <!--External Request-->
    <div class="new-clarification" >
        <div class="clarifyRequest">
            <div class="">
                <h4 class="pageTitle"><asp:Literal ID="liHeader" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCENewBookHeader %>"></asp:Literal></h4>
                <div class="dark-bg request-padd margin-top-25">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label><asp:Label ID="lblBookNumber" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEBookNumber %>"></asp:Label></label><span class="error-msg">*</span>
                            <asp:TextBox  ID="txtBookNumber" runat="server" cssClass="form-control" placeholder="15/2018"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvBookNumber" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE,SCELetterNumberRequired%>" Display="Dynamic" ValidationGroup="AddBook" ControlToValidate="txtBookNumber" runat="server" CssClass="error-msg" />
                        </div>
                    </div>
                    <div class="col-md-3 col-md-offset-1">
                        <div class="form-group">
                            <label><asp:Label ID="lblBookDate" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEBookDate %>" runat="server"></asp:Label></label>
                            <input type="text" runat="server" readonly="readonly"  id="txtBookDate" class="form-control"/>
                            <%--<asp:TextBox cssClass="form-control" ID="txtBookDate" runat="server" disabled="disabled"></asp:TextBox>--%>
                        </div>
                    </div>

                    <div class="col-md-4 col-md-offset-1">
                        <div class="form-group">
                            <label><asp:Label ID="lblBookEditor" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEBookSender %>" runat="server"></asp:Label></label>
                            <asp:TextBox ID="txtBookEditor" runat="server" CssClass="form-control"  disabled="disabled"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix"></div>

                    <div class="col-md-7 margin-top-15">
                        <div class="form-group">
                            <label><asp:Label ID="lblBookSubject" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEBookSubject %>" ></asp:Label></label><span class="error-msg">*</span>
                            <asp:TextBox CssClass="form-control" ID="txtBooksubject" runat="server"> </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvBookSubject" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE,SCELetterSubjectRequired%>" Display="Dynamic" ValidationGroup="AddBook" ControlToValidate="txtBooksubject" runat="server"  CssClass="error-msg"/>
                            </div>
                    </div>
                    <div class="col-md-4  col-md-offset-1 margin-top-15">
                        <div class="form-group">
                            <label><asp:Label ID="lblEntityName" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEAuthority %>"></asp:Label></label><span class="error-msg">*</span>
                           <asp:DropDownList ID="ddlExternalEntities"  runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42" AutoPostBack="true" OnSelectedIndexChanged="ddlExternalEntities_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvExternalEntities" InitialValue="0" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE,SCEAgencyRequired%>" Display="Dynamic" ValidationGroup="AddBook" ControlToValidate="ddlExternalEntities" runat="server"  CssClass="error-msg" />
                        </div>
                    </div>

                    <div class="clearfix">
                    </div>

                    <div class="col-md-7 margin-top-15">
                        <div class="form-group">
                            <label><asp:Label runat="server" ID="lblEntityAddress" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEAuthorityAddress %>" ></asp:Label></label>
                            <asp:textbox ID="txtEntityAddress" MaxLength="255" runat="server" CssClass="form-control text-area" TextMode="MultiLine" Columns="20" Rows="10"></asp:textbox>
                        </div>
                    </div>

                    <div class="col-md-4 col-md-offset-1 margin-top-15">
                        <div class="form-group">
                            <label><asp:Label ID="lblEntityEmail" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEAuthorityEmail %>"></asp:Label></label><span class="error-msg">*</span>
                            <asp:TextBox ID="txtEntityEmail" runat="server" PlaceHolder="name@domain.com" cssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEntityEmail" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE,SCEEmailRequired%>" Display="Dynamic" ValidationGroup="AddBook" ControlToValidate="txtEntityEmail" runat="server"  CssClass="error-msg"/>
                            <asp:RegularExpressionValidator ID="rgeEntityEmail" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE,SCEEmailValidation%>" Display="Dynamic" ValidationGroup="AddBook" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" ControlToValidate="txtEntityEmail" runat="server"  CssClass="error-msg"/>    
                        </div>
                    </div>

                    <div class="clearfix">
                    </div>

                    <div class="col-xs-12 margin-top-15">
                        <div class="form-group">
                            <label><asp:Label ID="lblBookBody" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEBookBody %>"></asp:Label></label><span class="error-msg">*</span>
                            <asp:TextBox ID="txtBookBody" runat="server" cssClass="form-control text-area" TextMode="MultiLine" Columns="20" Rows="10"></asp:TextBox> </div>
                        <asp:RequiredFieldValidator ID="rfvBookBody" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE,SCELetterBodyRequired%>" Display="Dynamic" ValidationGroup="AddBook" ControlToValidate="txtBookBody" runat="server"  CssClass="error-msg"/>
                    </div>
                    <div class="clearfix">
                    </div>

                    <div class="margin-top-10 moe-book-upload">
 

                        
                        <uc1:ClientSideFileUpload runat="server" id="FileUp1" />
    

                    </div>

                    
                </div>

            </div>
        </div>


    </div>

    <div class="formBtns clarificatinBtns">
        <div class="row">
            <div class="col-sm-6 col-xs-12 text-left">
				<asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" CssClass="moe-btn btn" Text="<%$Resources:ITWORX_MOEHEWF_SCE,Back %>" />

            </div>
            <div class="col-sm-6 col-xs-12 text-right">
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" ValidationGroup="AddBook" Text="<%$Resources:ITWORX_MOEHEWF_SCE,Save %>" CssClass="moe-btn btn margin-left-5"></asp:Button>
                <asp:Button ID="btnSendEmail" runat="server" OnClick="btnSendEmail_Click" ValidationGroup="AddBook" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SendEmail %>"  CssClass="moe-btn btn"></asp:Button>
            </div>
        </div>
    </div>



</asp:Panel>

