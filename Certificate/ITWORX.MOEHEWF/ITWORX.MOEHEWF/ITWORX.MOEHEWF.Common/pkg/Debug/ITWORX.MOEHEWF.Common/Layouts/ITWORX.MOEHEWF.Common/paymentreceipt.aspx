<%@ Assembly Name="ITWORX.MOEHEWF.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7b2931724f1d7d1c" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="paymentreceipt.aspx.cs" Inherits=" ITWORX.MOEHEWF.Common.Layouts.ITWORX.MOEHEWF.Common.paymentreceipt"  %>

<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">

	<div id="main-content">
		<div class="row heighlighted-section no-padding margin-0 edit-mode">
			<div class="col-md-12 col-xs-12 height-auto">
				<h6 class="font-size-22 font-weight-bold margin-top-0 margin-bottom-0">
				<asp:Label ID="lbl_Msg" runat="server" Text="Label"></asp:Label>
				</h6>
			</div>

			<div class="col-md-12 col-xs-12 height-auto">
				<asp:Button runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, goDashboard %>" OnClick="btn_goDashboard_Click" CssClass="btn moe-btn pull-right"></asp:Button>
			</div>
		</div>

	</div>

</asp:Content>
