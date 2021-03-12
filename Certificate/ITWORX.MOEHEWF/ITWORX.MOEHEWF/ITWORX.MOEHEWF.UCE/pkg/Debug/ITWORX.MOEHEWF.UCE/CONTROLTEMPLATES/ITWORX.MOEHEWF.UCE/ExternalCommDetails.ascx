<%@ Assembly Name="ITWORX.MOEHEWF.UCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=883afb4c05a35fe5" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="MOEHE" TagName="FileUpload" %>
<%--<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="MOEHE" TagName="ReplyFileUpload" %>--%>


<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExternalCommDetails.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.ExternalCommDetails" %>



<div class="section-container panel_main_title">
	<asp:Panel ID="pnl_SubmitOrgReply" runat="server" GroupingText="<%$Resources:ITWORX_MOEHEWF_UCE, SubmitEntityReply %>" CssClass="stateTitle">

			<asp:Panel ID="pnl_BookDetails" runat="server" GroupingText="<%$Resources:ITWORX_MOEHEWF_UCE, BookDetails %>" CssClass="container margin-bottom-25 field_width">
				<asp:HiddenField ID="hdn_ID" runat="server" Value='<%# Eval("ID") %>' />

                <div class="row">

               
				<div class="col-md-4 col-sm-6">
					<div class="data-container">
						<h6 class="font-size-16 margin-bottom-15">
							<asp:Label ID="lbl_BookID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, BookID %>"></asp:Label>
						</h6>
						<h5 class="font-size-22">
							<asp:Label ID="lbl_BookIDVal" runat="server"></asp:Label>
						</h5>
					</div>
				</div>
				<div class="col-md-4 col-sm-6">
					<div class="data-container">
						<h6 class="font-size-16 margin-bottom-15">

							<asp:Label ID="lbl_BookDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, BookDate %>"></asp:Label>
						</h6>
						<h5 class="font-size-22">
							<asp:Label ID="lbl_BookDateVal" runat="server"></asp:Label>
						</h5>
					</div>
				</div>
				<div class="col-md-4 col-sm-6">
					<div class="data-container">
						<h6 class="font-size-16 margin-bottom-15">

							<asp:Label ID="lbl_BookAuthor" runat="server" Text=" <%$Resources:ITWORX_MOEHEWF_UCE, BookAuthor %>"></asp:Label>
						</h6>
						<h5 class="font-size-22">
							<asp:Label ID="lbl_BookAuthorVal" runat="server"></asp:Label>
						</h5>
					</div>
				</div>
                 </div>
                <div class="row">

               
				<div class="col-md-4 col-sm-6">
					<div class="data-container">
						<h6 class="font-size-16 margin-bottom-15">
							<asp:Label ID="lbl_BookSubject" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, BookSubject %>"></asp:Label>
						</h6>
						<h5 class="font-size-22">
							<asp:Label ID="lbl_BookSubjectVal" runat="server"></asp:Label>
						</h5>
					</div>
				</div>
				<div class="col-md-4 col-sm-6">
					<div class="data-container">
						<h6 class="font-size-16 margin-bottom-15">

							<asp:Label ID="lbl_DirectedTo" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, BookDirectedTo %>"></asp:Label>
						</h6>
						<h5 class="font-size-22">
							<asp:Label ID="lbl_DirectedToVal" runat="server"></asp:Label>
						</h5>
					</div>
				</div>
                <div class="col-md-4 col-sm-6">
					<div class="data-container">
						<h6 class="font-size-16 margin-bottom-15">
							<asp:Label ID="lbl_OrgAddress" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EntityAddress %>"></asp:Label>
						</h6>
						<h5 class="font-size-22">
							<asp:Label ID="lbl_OrgAddressVal" runat="server"></asp:Label>
						</h5>
					</div>
				</div>
                 </div>

                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12">
					<div class="data-container">
						<h6 class="font-size-16 margin-bottom-15">
							<asp:Label ID="lbl_OrgEmail" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EntityEmail %> "></asp:Label>
						</h6>
						<h5 class="font-size-22">
							<asp:Label ID="lbl_OrgEmailVal" runat="server"></asp:Label>
						</h5>
					</div>
				</div>
                </div>
                	

                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12">
					<div class="data-container">
						<h6 class="font-size-16 margin-bottom-15">

							<asp:Label ID="lbl_BookText" runat="server" Text=" <%$Resources:ITWORX_MOEHEWF_UCE, BookText %>"></asp:Label>
						</h6>
						<h5 class="font-size-22">
							<asp:Label ID="lbl_BookTextVal" runat="server"></asp:Label>
						</h5>
					</div>
				</div>
                </div>
				
				
			
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10 margin-bottom-10">

				<MOEHE:FileUpload runat="server" id="NewBookAttachements" />
</div>
                </div>
   
			</asp:Panel>




				<asp:Panel ID="pnl_OrgReply" runat="server" GroupingText="<%$Resources:ITWORX_MOEHEWF_UCE, EntityReply %>" CssClass="container field_width">
					<div class="row">
                        <div class="col-md-4 col-sm-6">
						<div class="data-container">
							<h6 class="font-size-16 margin-bottom-15">
								<asp:Label ID="lbl_OrgBookID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, BookID %>"></asp:Label>
							</h6>
							<h5 class="font-size-22">
								<asp:Label ID="lbl_OrgBookIDVal" runat="server"></asp:Label>
							</h5>
						</div>
					</div>
                    <div class="col-md-4 col-sm-6">
						<div class="data-container">
							<h6 class="font-size-16 margin-bottom-15">
								<asp:Label ID="lbl_OrgBookSubject" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, BookSubject %>"></asp:Label>
							</h6>
							<h5 class="font-size-22">
								<asp:Label ID="lbl_OrgBookSubjectVal" runat="server"></asp:Label>
							</h5>
						</div>
					</div>
					<div class="col-md-4 col-sm-6">
						<div class="data-container">
							<h6 class="font-size-16 margin-bottom-15">
								<asp:Label ID="lbl_OrgBookDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, BookDate %> "></asp:Label>
							</h6>
							<h5 class="font-size-22">
								<asp:Label ID="lbl_OrgBookDateVal" runat="server"></asp:Label>
							</h5>
						</div>
					</div>
					</div>
                    
                    

                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10 margin-bottom-10">
						<div class="data-container">
							<h6 class="font-size-16 margin-bottom-15">
								<asp:Label ID="lbl_OrgReply" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EntityReplySummary %> "></asp:Label>
							</h6>
							<h5 class="font-size-22">
								<asp:Label ID="lbl_OrgReplyVal" runat="server"></asp:Label>
							</h5>

						</div>
					</div>
					</div>
					


                    <div class="row">
                         <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10 margin-bottom-10">
                        <MOEHE:FileUpload runat="server" id="NewBookReplyAttachements" />
                    </div>
					</div>
                   
					

                    <div class="row margin-bottom-25">

					
					<div class="col-md-12 text-right">
						<asp:Button ID="btn_Close" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Close %>" OnClick="btn_Close_Click" />
					</div>
                    </div>
				</asp:Panel>
	</asp:Panel>
</div>
