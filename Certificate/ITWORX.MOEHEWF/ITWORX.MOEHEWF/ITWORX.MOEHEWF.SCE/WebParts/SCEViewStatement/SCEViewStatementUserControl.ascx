<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SCEViewStatementUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.WebParts.SCEViewStatement.SCEViewStatementUserControl" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/ClientSideFileUpload.ascx" TagPrefix="uc1" TagName="ClientSideFileUpload" %>

<div class="view-clarification">
<div class="clarifyRequest" style="">
                    	
		
			<h4 class="pageTitle">
                <asp:Label ID="Label1" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StatementRequestDetails %>"></asp:Label>
			</h4>
			<div class="row dark-bg request-padd margin-top-25 view-statement-row">
				<div class="col-md-4 col-xs-12">
					<div class="form-group"> 
                        <label><asp:Label ID="Label2" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StatementDate %>"></asp:Label> </label>
                        <asp:TextBox ID="txt_StatementDate" runat="server" Enabled="false" CssClass="form-control" ></asp:TextBox>
					</div>
				</div>
				<div class="col-md-4 col-xs-12">
					<div class="form-group"> 
                        <label><asp:Label ID="Label3" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StatementSubject %>"></asp:Label> </label>
                        <asp:TextBox ID="txt_statementTopic" runat="server" Enabled="false" CssClass="form-control" ></asp:TextBox>
					</div>
				</div>
				<div class="col-md-4 col-xs-12">
					<div class="form-group"> 
                        <label><asp:Label ID="Label4" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StatementAgency %>"></asp:Label> </label>
                        <asp:TextBox ID="txt_StatementAgency" runat="server" Enabled="false" CssClass="form-control" ></asp:TextBox>
					</div>
				</div>
				<div class="clearfix">
				</div>

				<div class="col-md-4 col-xs-12">
					<div class="form-group"> 
                        <label><asp:Label ID="Label5" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StatementSender %>"></asp:Label></label> 
                        <asp:TextBox ID="txt_Sender" runat="server" Enabled="false" CssClass="form-control" ></asp:TextBox>
                    </div>
				</div>
				<div class="col-md-4 col-xs-12">
					<div class="form-group"> 
                        <label><asp:Label ID="Label6" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, AgencyReplayDate %>"></asp:Label></label> 		 
                        <asp:TextBox ID="txt_ReplayDate" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                        </div>
                </div>
				<div class="col-md-4 col-xs-12">
					<div class="form-group"> 
                        <label><asp:Label ID="Label7" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SenderOfReplay %>"></asp:Label> </label>
                        <asp:TextBox ID="txt_SenderReplay" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                    </div>
				</div>



				<div class="clearfix">
				</div>
				<div class="col-xs-12 margin-top-15">
					<div class="form-group"> 
                        <label><asp:Label ID="Label8" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequiredStatement %>"></asp:Label> </label> 
                        <asp:TextBox ID="txt_Requiredstatement" runat="server" TextMode="MultiLine" Columns="20" Rows="10"  Enabled="false" CssClass="form-control text-area" ></asp:TextBox>
                   </div>
                </div>
				<div class="clearfix">
				</div>
				<div class="col-xs-12 margin-top-15">
					<div class="form-group"> 
                        <label><asp:Label ID="Label9" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StatementReplay %>"></asp:Label> </label>
                    <asp:TextBox ID="txt_StatementReplay" runat="server" TextMode="MultiLine" Columns="20" Rows="10"  Enabled="false" CssClass="form-control text-area"></asp:TextBox>
                 </div>
                    </div>
				<div class="clearfix">
				</div>
				<div class="col-xs-12 margin-top-15">
                    <uc1:ClientSideFileUpload runat="server" id="ViewFile" />    		 
					 
				</div>


			
		
		
	</div>
	
</div>
</div>
<div class="formBtns clarificatinBtns">
		<div class="row">
 
			<div class="col-sm-6 col-xs-12 text-left"> 
                <asp:Button ID="btn_Back" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Back %>" CssClass="moe-btn btn back" OnClick="btn_Back_Click"/>
				</div>
		</div>
</div>
<asp:HiddenField ID="RequestID_HF" runat="server" />
