<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%--<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="MOEHE" TagName="ReplyFileUpload" %>--%>
<%--<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="MOEHE" TagName="FileUpload" %>--%>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="uc1" TagName="FileUpload" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Add_NewBook.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.Add_NewBook" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>

<div runat="server" class="row flex-display flex-wrap">
    <asp:Panel ID="pnl_NewBook" runat="server" GroupingText="<%$Resources:ITWORX_MOEHEWF_UCE, NewBook %>" CssClass="stateTitle newbook">

		<div class="addBook container-fluid margin-bottom-50">
			<div class="row heighlighted-section flex-display flex-wrap">

            <div class="col-md-4 col-sm-6 margin-top-10 margin-bottom-10">
                <div class="data-container table-display moe-width-85">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                            <asp:HiddenField ID="hdn_ID" runat="server" />
                            <asp:Label ID="lbl_BookID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, BookID %>"></asp:Label>
                        </h6>
                        <asp:TextBox ID="txt_BookID" runat="server" CssClass="moe-full-width input-height-42 border-box moe-input-padding"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="col-md-4 col-sm-6 margin-top-10 margin-bottom-10">
                <div class="data-container table-display moe-width-85 pull-right">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                            <asp:Label ID="lbl_BookDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, BookDate %> "></asp:Label>
                        </h6>
                        <h5 class="font-size-18">
                            <asp:Label ID="lbl_BookDateVal" runat="server"></asp:Label>
                        </h5>
                    </div>
                </div>
            </div>

            <div class="col-md-4 col-sm-6 margin-top-10 margin-bottom-10">
                <div class="data-container table-display moe-width-85 pull-right">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                            <asp:Label ID="lbl_BookAuthor" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, BookAuthor %>"></asp:Label>
                        </h6>
                        <h5 class="font-size-18">
                            <asp:Label ID="lbl_BookAuthorVal" runat="server"></asp:Label>
                        </h5>
                    </div>
                </div>
            </div>
        </div>

        <div class="row unheighlighted-section flex-display flex-wrap margin-2">
            <div class="col-md-4 col-sm-6 margin-top-10 margin-bottom-10">
                <div class="data-container table-display moe-width-85">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                            <asp:Label ID="lbl_BookSubject" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, BookSubject %>"></asp:Label>
                        </h6>
                        <asp:TextBox ID="txt_BookSubject" runat="server" CssClass="moe-full-width input-height-42 border-box moe-input-padding"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqVal_BookSubject" runat="server" CssClass="error-msg moe-full-width" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredBookSubject %>"
                            ValidationGroup="Save" ControlToValidate="txt_BookSubject"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>

            <div class="col-md-4 col-sm-6 margin-top-10 margin-bottom-10">
                <div class="data-container table-display moe-width-85 pull-right">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                            <asp:Label ID="lbl_BookDirectedTo" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, BookDirectedTo %> "></asp:Label>
                        </h6>

                        <asp:DropDownList ID="drp_BookDirectedTo" runat="server" CssClass="moe-dropdown moe-full-width input-height-42 border-box moe-input-padding"  ></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqVal_BookDirectedTo" runat="server" CssClass="error-msg moe-full-width" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredBookDirectedTo %>"
                            ValidationGroup="Save" ControlToValidate="drp_BookDirectedTo"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txt_BookDirectedTo" runat="server" CssClass="moe-full-width input-height-42 border-box moe-input-padding"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="col-md-4 col-sm-6 margin-top-10 margin-bottom-10">
                <div class="data-container table-display moe-width-85 pull-right">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                            <asp:Label ID="lbl_OrgEmail" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EntityEmail %> "></asp:Label>
                        </h6>
                        <asp:TextBox ID="txt_OrgEmail" runat="server" CssClass="moe-full-width input-height-42 border-box moe-input-padding"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="reqVal_txt_OrgEmail" runat="server" CssClass="error-msg moe-full-width" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredOrgEmail %>"
                            ValidationGroup="Save" ControlToValidate="txt_OrgEmail"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="error-msg moe-full-width"
                            ControlToValidate="txt_OrgEmail" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, InvalidEmailFormat %>"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
             <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10 margin-bottom-10">
                <div class="data-container table-display moe-full-width">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                            <asp:Label ID="lbl_OrgReplyAddress" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EntityAddress %>"></asp:Label>
                        </h6>

                        <asp:TextBox ID="txt_OrgReplyAddress" runat="server" TextMode="MultiLine" CssClass="moe-full-width input-height-42 border-box moe-input-padding text-area"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-md-12 col-sm-12 col-xs-12  margin-top-10 margin-bottom-10">
                <div class="data-container table-display moe-full-width">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                            <asp:Label ID="lbl_BookText" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, BookText %> "></asp:Label>
                        </h6>
                        <cc1:Editor ID="txt_BookText" runat="server" />
                        <%--    <asp:TextBox ID="txt_BookText" runat="server" TextMode="MultiLine" CssClass="moe-full-width input-height-42 border-box moe-input-padding text-area"></asp:TextBox>--%>
                    </div>
                </div>
            </div>

            <div class="col-md-5 col-sm-12 col-xs-12 margin-top-10 margin-bottom-10">
                <uc1:FileUpload runat="server" id="NewBookAttachements" />
            </div>

              <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10 margin-bottom-10">
                <asp:Button ID="btn_SaveBook" runat="server" ValidationGroup="Save" Text="<%$Resources:ITWORX_MOEHEWF_UCE, SaveSendbyMail %>" OnClick="btn_SaveBook_Click" CssClass="btn moe-btn pull-right" />
            </div>
        </div>
		</div>
        

        <%--<MOEHE:FileUpload runat="server" id="NewBookAttachements" />--%>
    </asp:Panel>
</div>

<div runat="server" id="ReplyControlsdiv">
    <asp:Panel ID="pnl_OrgReply" runat="server" GroupingText="<%$Resources:ITWORX_MOEHEWF_UCE, EntityReply %>" CssClass="stateTitle newbook">
		<div class="addBook container-fluid">
			<div class="row heighlighted-section flex-display flex-wrap">
                
				  <div class="col-md-4 col-sm-6 margin-top-10 margin-bottom-10">
                <div class="data-container table-display moe-width-85">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                            <asp:Label ID="lbl_OrgBookID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, BookID %>"></asp:Label>
                        </h6>
                        <asp:TextBox ID="txt_OrgBookID" runat="server" CssClass="moe-full-width input-height-42 border-box moe-input-padding"></asp:TextBox>
                    </div>
                </div>
            </div>
                  <div class="col-md-4 col-sm-6 margin-top-10 margin-bottom-10">
                <div class="data-container table-display moe-width-85">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                            <asp:Label ID="lbl_OrgReplyBookSubject" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, BookSubject %>"></asp:Label>
                        </h6>
                        <asp:TextBox ID="txt_OrgReplyBookSubjectVal" runat="server" CssClass="moe-full-width input-height-42 border-box moe-input-padding"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqVal_OrgReplyBookSubject" runat="server" CssClass="error-msg moe-full-width" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredBookSubject %>"
                            ValidationGroup="SaveOrgBook" ControlToValidate="txt_OrgReplyBookSubjectVal" Enabled="false"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div class="col-md-4 col-sm-6 margin-top-10 margin-bottom-10">
                <div class="data-container table-display moe-width-85 pull-right">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                            <asp:Label ID="lbl_OrgBookDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, BookDate %> "></asp:Label>
                        </h6>
                        <h5 class="font-size-18">
                            <asp:Label ID="lbl_OrgBookDateVal" runat="server"></asp:Label>
                        </h5>
                    </div>
                </div>
            </div>
			</div>
			 <div class="row unheighlighted-section flex-display flex-wrap margin-2">
          

            <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10 margin-bottom-10">
                <div class="data-container table-display moe-full-width">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                            <asp:Label ID="lbl_OrgReply" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EntityReplySummary %> "></asp:Label>
                        </h6>
                        <asp:TextBox ID="txt_OrgReply" runat="server" TextMode="MultiLine" CssClass="moe-full-width input-height-42 border-box moe-input-padding text-area"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqVal_OrgReply" runat="server" CsssClass="error-msg" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredOrgReply %>"
                            ValidationGroup="SaveOrgBook" ControlToValidate="txt_OrgReply" Enabled="false"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>

            <%--<MOEHE:FileUpload runat="server" id="NewBookReplyAttachements" />--%>
            <div class="col-md-5 col-sm-12 col-xs-12 no-padding">
                <uc1:FileUpload runat="server" id="NewBookReplyAttachements" />
            </div>

            <div class="col-md-12 no-padding">
                <asp:Button ID="btn_SaveOrgReplyBook" runat="server" ValidationGroup="SaveOrgBook" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Save %>" OnClick="btn_SaveOrgReplyBook_Click" CssClass="btn moe-btn pull-right" />
            </div>
        </div>
		</div>
    </asp:Panel>
</div>