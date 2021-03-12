<%@ Assembly Name="ITWORX.MOEHEWF.SCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7c6ec0a86ef11fff" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/ITWORX.MOEHEWF.Common/ClientSideFileUpload.ascx" TagPrefix="uc1" TagName="ClientSideFileUpload" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewSCEBooksRequestUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.WebParts.NewSCEBooksRequest.NewSCEBooksRequestUserControl" %>
<asp:ValidationSummary ID="vsGroup" runat="server" ValidationGroup="AddBook" />
<asp:Label ID="lblExceptionMessage" runat="server" visible="false" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ExceptionBooksRequestsMessage %>"></asp:Label>
<asp:Panel ID="pnlRequestDetails" runat="server">
        <!--External Request-->
    <div class="new-extrenal" style="">
        <div class="school-request">
            <div class="">
                <h4 class="pageTitle"><asp:Literal ID="liHeader" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCENewBookHeader %>"></asp:Literal></h4>
                <div class="row dark-bg request-padd">
                    <div class="col-md-3">
                        <div class="form-group">
                            <asp:Label ID="lblBookNumber" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEBookNumber %>"><span class="error-msg">*</span></asp:Label>
                            <asp:TextBox  ID="txtBookNumber" runat="server" cssClass="form-control" placeholder="15/2018"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvBookNumber" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE,SCELetterNumberRequired%>" Display="None" ValidationGroup="AddBook" ControlToValidate="txtBookNumber" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-3 col-md-offset-1">
                        <div class="form-group">
                            <asp:Label ID="lblBookDate" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEBookDate %>" runat="server"></asp:Label>
                            <asp:TextBox cssClass="form-control" ID="txtBookDate" runat="server" disabled="disabled"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-md-4 col-md-offset-1">
                        <div class="form-group">
                            <asp:Label ID="lblBookEditor" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEBookSender %>" runat="server"></asp:Label>
                            <asp:TextBox ID="txtBookEditor" runat="server" cssClass="form-control"  disabled="disabled"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix"></div>

                    <div class="col-md-7 margin-top-15">
                        <div class="form-group">
                            <asp:Label ID="lblBookSubject" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEBookSubject %>" ><span class="error-msg">*</span></asp:Label>
                            <asp:TextBox CssClass="form-control" ID="txtBooksubject" runat="server"> </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvBookSubject" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE,SCELetterSubjectRequired%>" Display="None" ValidationGroup="AddBook" ControlToValidate="txtBooksubject" runat="server" />
                            </div>
                    </div>
                    <div class="col-md-4  col-md-offset-1 margin-top-15">
                        <div class="form-group">
                            <asp:Label ID="lblEntityName" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEAuthority %>"><span class="error-msg">*</span></asp:Label>
                           <asp:DropDownList ID="ddlExternalEntities"  runat="server"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvExternalEntities" InitialValue="none" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE,SCEAgencyRequired%>" Display="None" ValidationGroup="AddBook" ControlToValidate="ddlExternalEntities" runat="server" />
                        </div>
                    </div>

                    <div class="clearfix">
                    </div>

                    <div class="col-md-7 margin-top-15">
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblEntityAddress" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEAuthorityAddress %>" ></asp:Label>
                            <asp:textbox ID="txtEntityAddress" MaxLength="255" runat="server" CssClass="form-control text-area"></asp:textbox>
                        </div>
                    </div>

                    <div class="col-md-4 col-md-offset-1 margin-top-15">
                        <div class="form-group">
                            <asp:Label ID="lblEntityEmail" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEAuthorityEmail %>"><span class="error-msg">*</span></asp:Label>
                            <asp:TextBox ID="txtEntityEmail" runat="server" PlaceHolder="name@domain.com" cssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEntityEmail" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE,SCEEmailRequired%>" Display="None" ValidationGroup="AddBook" ControlToValidate="txtEntityEmail" runat="server" />
                            <asp:RegularExpressionValidator ID="rgeEntityEmail" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE,SCEEmailValidation%>" Display="None" ValidationGroup="AddBook" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" ControlToValidate="txtEntityEmail" runat="server" />    
                        </div>
                    </div>

                    <div class="clearfix">
                    </div>

                    <div class="col-xs-12 margin-top-15">
                        <div class="form-group">
                            <asp:Label ID="lblBookBody" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEBookBody %>"><span class="error-msg">*</span></asp:Label>
                            <asp:TextBox ID="txtBookBody" runat="server" cssClass="form-control text-area"></asp:TextBox> </div>
                        <asp:RequiredFieldValidator ID="rfvBookBody" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE,SCELetterBodyRequired%>" Display="None" ValidationGroup="AddBook" ControlToValidate="txtBookBody" runat="server" />
                    </div>
                    <div class="clearfix">
                    </div>

                    <div class="row fileUploadContainer">
    <div class="col-md-6 col-sm-6 col-xs-8 margin-bottom-10">
        <div class="form-group">
            <label>أسم الملف  <asp:Label ID="lbldropFileUpload" runat="server" CssClass="error-msg" Style="display: none;">*</asp:Label></label>
         <%--   <asp:DropDownList ID="dropFileUpload" runat="server" CssClass="form-control moe-dropdown">
                <asp:ListItem Text="Select" Value=""></asp:ListItem>
                <asp:ListItem Text="CertificateOutside" Value="1"></asp:ListItem>
                <asp:ListItem Text="GeneralOutside" Value="2"></asp:ListItem>
            </asp:DropDownList>--%>
           
            <%--<asp:Label ID="lblRequiredDrop" runat="server" Style="display: none;" ForeColor="Red">You should choose file name </asp:Label>--%>
        </div>
    </div>

    <div class="clearfix"></div>
    <uc1:ClientSideFileUpload runat="server" id="FileUp1" />
    

</div>

                    
                </div>

            </div>
        </div>


    </div>

    <div class="formBtns extrenalBtns" style="">
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
