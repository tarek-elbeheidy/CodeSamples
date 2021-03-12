<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SendVerification.ascx.cs" Inherits="ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.SendVerification" %>


<div class="col-md-12 col-xs-12">
	<div class="form-group">
		<label class="row">
			<asp:Label ID="lblMobileNumber" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, MobileNumber %>"></asp:Label>
			<span style="color: red"> * </span>
		</label>
		<asp:TextBox ID="tbMobileNumber" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="reqMobileNumber" Display="dynamic" runat="server" ErrorMessage="RequiredFieldValidator" CssClass="error-msg moe-full-width" ControlToValidate="tbMobileNumber" Text="<%$Resources:ITWORX.MOEHEWF.Common, RequiredMobileNumber %>" ValidationGroup="Submit"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="dynamic" CssClass="error-msg moe-full-width" runat="server" ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, RegMobileNumber %>" ControlToValidate="tbMobileNumber" ValidationExpression="^\d{8,13}$" ValidationGroup="Submit"></asp:RegularExpressionValidator>

	</div>
</div>



<asp:Label ID="lblVerificationStatus" runat="server" Text=""></asp:Label>

