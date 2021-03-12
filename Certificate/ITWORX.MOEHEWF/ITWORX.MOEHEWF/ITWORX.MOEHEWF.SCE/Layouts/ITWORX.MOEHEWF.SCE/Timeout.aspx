<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Timeout.aspx.cs" Inherits="ITWORX.MOEHEWF.SCE.Layouts.ITWORX.MOEHEWF.SCE.Timeout" DynamicMasterPageFile="~masterurl/default.master" %>


<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">

	<div id="main-content">
		<div class="row heighlighted-section no-padding margin-0 edit-mode">
			<div class="col-md-12 col-xs-12 height-auto">
                <h6 class="font-size-32 font-weight-bold margin-top-0 margin-bottom-0">
                 <asp:Label ID="lblError" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Attention %>"></asp:Label>
				</h6>
                   <h4>
				 <asp:Label ID="lblMsg" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, TimeoutMsg %>"  />
                       </h4>
			</div>

			<div class="col-md-12 col-xs-12 height-auto text-center margin-top-25">
			  <asp:Button ID="btnBackHome" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, BackHome %>" OnClick="btnBackHome_Click" />

			</div>
		</div>

	</div>

</asp:Content>