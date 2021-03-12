<%@ Assembly Name="Microsoft.SharePoint.IdentityModel, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Assembly Name="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"%>
<%@ Page Language="C#" Inherits="Microsoft.SharePoint.IdentityModel.Pages.FormsSignInPage" MasterPageFile="~/_layouts/15/ITWORX.MOEHEWF.Common/MoeheLogin.master" %> 
<%@ Import Namespace="Microsoft.SharePoint.WebControls" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Content ContentPlaceHolderId="PlaceHolderPageTitle" runat="server">
	<SharePoint:EncodedLiteral runat="server"  EncodeMethod="HtmlEncode" Id="ClaimsFormsPageTitle" />
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderMain" runat="server">
 <div id="SslWarning" style="color:red;display:none">
 <SharePoint:EncodedLiteral runat="server"  EncodeMethod="HtmlEncode" Id="ClaimsFormsPageMessage" />
 </div>
  <script language="javascript" >
	if (document.location.protocol != 'https:')
	{
		var SslWarning = document.getElementById('SslWarning');
		SslWarning.style.display = '';
	}
  </script>
 <asp:login id="signInControl" FailureText="<%$Resources:wss,login_pageFailureText%>" runat="server" width="100%">
	<layouttemplate>
		<asp:label id="FailureText" class="ms-error" runat="server"/>
		<section>
			<div class="user-form">
				<div class="form-header row text-center margin-bottom-25">
					<h3 class="margin-tb-0">
                        <asp:Literal runat="server" Text="&lt;%$Resources:ITWORX.MOEHEWF.Common,WelcomeMag%&gt;"/>
					</h3>
					<img class="form-logo" src="images/logo1.png">
					<h2 class="margin-tb-0">
                        <asp:Literal runat="server" Text="&lt;%$Resources:ITWORX.MOEHEWF.Common,Equivalence%&gt;"/>
					</h2>
				</div>

				<div class="form-content row">
					<%--<h2>إنشاء حساب جديد</h2>--%>
					<div class="col-md-12">
						<div class="form-group">
							<label class="row">
								<SharePoint:EncodedLiteral runat="server" Text="<%$Resources:wss,login_pageUserName%>" EncodeMethod='HtmlEncode' />
							</label>
							<asp:TextBox ID="UserName" autocomplete="off" runat="server" CssClass="form-control" />
						</div>
					</div>

					<div class="col-md-12">
						<div class="form-group">
							<label class="row">
								<SharePoint:EncodedLiteral runat="server" Text="<%$Resources:wss,login_pagePassword%>" EncodeMethod='HtmlEncode' />
							</label>
							<asp:TextBox ID="password" TextMode="Password" autocomplete="off" runat="server" class="ms-inputuserfield" CssClass="form-control" />
						</div>
					</div>

					<div class="col-md-12">
						<div class="form-group flex-display">
							<asp:CheckBox ID="RememberMe" Text="<%$SPHtmlEncodedResources:wss,login_pageRememberMe%>" runat="server" />
						</div>
						
					</div>
					<div class="col-md-12">
						<div class="form-group flex-display">
							<a href="/_layouts/15/ITWORX.MOEHEWF.Common/Register.aspx" >
                            <asp:Literal runat="server" Text="&lt;%$Resources:ITWORX.MOEHEWF.Common,RegisterNow%&gt;"/>
                            </a>
						</div>
						
					</div>
					<div class="col-md-12">
						<div class="form-group text-center">
							<asp:Button ID="login" CommandName="Login" Text="<%$Resources:wss,login_pagetitle%>" CssClass="btn moe-btn" runat="server" />
						</div>
					</div>

				</div>




			</div>
		</section>
		
	</layouttemplate>
 </asp:login>
</asp:Content>
