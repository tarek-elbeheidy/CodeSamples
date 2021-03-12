<%@ Assembly Name="ITWORX.MOEHEWF.SCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7c6ec0a86ef11fff" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SCEReplyToBookUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.WebParts.SCEReplyToBook.SCEReplyToBookUserControl" %>
<%@ Register Src="~/_controltemplates/ITWORX.MOEHEWF.Common/ClientSideFileUpload.ascx" TagPrefix="uc1" TagName="ClientSideFileUpload" %>

<asp:Panel ID="pnlReplyDetails" runat="server">
    
    <asp:ValidationSummary ID="vsGroup" runat="server" ValidationGroup="BookReply" />
<asp:Label ID="lblExceptionMessage" runat="server" visible="false" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ExceptionBooksRequestsMessage %>"></asp:Label>
<div class="reply-extrenal">
                    <!--Book Details-->
                    	<div class="school-request">
							<div class="">
								<h4 class="pageTitle"><asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEReplyHeader %>"></asp:Literal></h4>
								<div class="row dark-bg request-padd">
				<div class="col-md-3 col-xs-12">
					<div class="form-group">
						<asp:label runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEBookNumber %>"></asp:label>
						<asp:TextBox ID="txtBookNumber" runat="server" CssClass="form-control"  disabled="disabled"></asp:TextBox>
					</div>
				</div>
				<div class="col-md-3 col-md-offset-1 col-xs-12">
					<div class="form-group">
						<asp:label runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEBookDate %>"> </asp:label>
					<asp:TextBox Id="txtBookDate" runat="server" class="form-control" placeholder="12/6/2018" disabled="disabled"></asp:TextBox>				</div>
				</div>
				<div class="col-md-4 col-md-offset-1 col-xs-12">
					<div class="form-group">
						<asp:Label runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEBookSender %>" > </asp:Label>
						<asp:TextBox ID="txtBookSender" CssClass="form-control" runat="server"  disabled="disabled"></asp:TextBox>
					</div>
				</div>
				<div class="clearfix">
				</div>

				<div class="col-md-7 col-xs-12 margin-top-15">
					<div class="form-group">
						<asp:Label runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEBookSubject %>" > </asp:Label>
						<asp:TextBox runat="server" ID="txtBookSubject" CssClass="form-control"  disabled="disabled"></asp:TextBox>
					</div>
				</div>
				<div class="col-md-4 col-md-offset-1 col-xs-12 margin-top-15">
					<div class="form-group">
						<asp:Label runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEAuthority %>">   </asp:Label>
					<asp:TextBox ID="txtAuthority" runat="server" CssClass="form-control"  disabled="disabled">	</asp:TextBox>				</div>
				</div>
				<div class="clearfix">
				</div>
				
				<div class="col-md-7 col-xs-12 margin-top-15">
					
					
					<div class="form-group">
						<asp:Label runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEAuthorityAddress %>">  </asp:Label>
						<asp:TextBox Rows="4" TextMode="MultiLine" ID="txtAuthorityAddress" runat="server" CssClass="form-control text-area" disabled="disabled"></asp:TextBox>
					</div>

				</div>
				<div class="col-md-4 col-md-offset-1 col-xs-12 margin-top-15">
					<div class="form-group">
						<asp:Label runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEAuthorityEmail %>"> </asp:Label>
						<asp:TextBox ID="txtAuthorityEmail" runat="server" CssClass="form-control"  disabled="disabled"></asp:TextBox>
					</div>				</div>



				<div class="clearfix">
				</div>
				<div class="col-xs-12 margin-top-15">
					<div class="form-group">
						<asp:Label runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEBookBody %>">  </asp:Label>
						<asp:TextBox Rows="4" TextMode="MultiLine" ID="txtBookBody" runat="server" CssClass="form-control text-area "  disabled="disabled"></asp:TextBox> </div>
				</div>
				<div class="clearfix">
				</div>
								<div class="col-xs-12 margin-top-15">
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
						<!--Book Details-->
						
						<!--Book Reply-->
						<div class="school-request">
							<div class="">
								<h4 class="pageTitle"><asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEAuthorityReplyHeader %>"></asp:Literal> </h4>
								<div class="row dark-bg request-padd">
									<div class="col-md-3">
										<div class="form-group">
											<asp:Label runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEReplyNumber %>"> </asp:Label>
											<asp:TextBox Id="txtReplyNumber" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="None" ValidationGroup="BookReply" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE,SCEReplyNumberRequired %>" ControlToValidate="txtReplyNumber" runat="server" />
										</div>
									</div>
									<div class="col-md-3 col-md-offset-1">
										<div class="form-group">
											<asp:Label runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEReplyDate %>" > </asp:Label>
											<asp:TextBox runat="server" CssClass="form-control" ID="txtReplyDate" disabled="disabled"></asp:TextBox>
										</div>
									</div>
									
							
									
									<div class="clearfix">
									</div>
									<div class="col-xs-12 margin-top-15">
										<div class="form-group">
											<asp:Label runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCEReplySummary %>"> <span class="error-msg">*</span></asp:Label>
											<asp:TextBox ID="txtReplySummary" runat="server" Rows="4" TextMode="MultiLine" CssClass="form-control text-area "></asp:TextBox> </div>
                                            <asp:RequiredFieldValidator Display="None" ValidationGroup="BookReply" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE,SCEReplySummaryRequired %>" ControlToValidate="txtReplySummary" runat="server" />
                                    </div>
									<div class="clearfix">
									</div>
									<div class="col-md-4 col-sm-6 col-xs-8 margin-bottom-10 margin-top-15">
									<div class="row fileUploadContainer">
    <div class="col-md-6 col-sm-6 col-xs-8 margin-bottom-10">
        <div class="form-group">
            <label>أسم الملف  <asp:Label ID="Label1" runat="server" CssClass="error-msg" Style="display: none;">*</asp:Label></label>
         <%--   <asp:DropDownList ID="dropFileUpload" runat="server" CssClass="form-control moe-dropdown">
                <asp:ListItem Text="Select" Value=""></asp:ListItem>
                <asp:ListItem Text="CertificateOutside" Value="1"></asp:ListItem>
                <asp:ListItem Text="GeneralOutside" Value="2"></asp:ListItem>
            </asp:DropDownList>--%>
           
            <%--<asp:Label ID="lblRequiredDrop" runat="server" Style="display: none;" ForeColor="Red">You should choose file name </asp:Label>--%>
        </div>
    </div>

    <div class="clearfix"></div>
    <uc1:ClientSideFileUpload runat="server" id="fileUpload2" />
    

</div>
                                    </div>
                                    

								</div>
								
							</div>
						</div>
						<!--Book Reply-->
						
                    </div>

<div class="formBtns extrenalBtns" style="">
		<div class="row">
			<div class="col-sm-6 col-xs-12 text-left">
				<asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" CssClass="moe-btn btn" Text="<%$Resources:ITWORX_MOEHEWF_SCE,Back %>" />
				</div>
			<div class="col-sm-6 col-xs-12 text-right">
			 <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" ValidationGroup="BookReply" Text="<%$Resources:ITWORX_MOEHEWF_SCE,Save %>" CssClass="moe-btn btn margin-left-5"></asp:Button>
				</div>
		</div>
    </div>
    

</asp:Panel>
