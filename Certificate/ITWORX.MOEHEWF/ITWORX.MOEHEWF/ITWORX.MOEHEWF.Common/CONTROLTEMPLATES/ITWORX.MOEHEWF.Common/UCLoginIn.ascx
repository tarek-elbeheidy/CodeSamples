<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCLoginIn.ascx.cs" Inherits="ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.UCLoginIn" %>

<asp:Label ID="lblUsername" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, UserName %>"></asp:Label>
<asp:TextBox ID="txtUsername" runat="server" ></asp:TextBox>
<asp:RequiredFieldValidator ID="reqUserName" runat="server" ForeColor="Red" ControlToValidate="txtUsername" Text="<%$Resources:ITWORX.MOEHEWF.Common, RequiredUserName %>"  ValidationGroup="Login"></asp:RequiredFieldValidator>
<br />
<asp:Label ID="lblPassword" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Password %>"  ></asp:Label>
<asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
<asp:RequiredFieldValidator ID="reqPassword" runat="server" ForeColor="Red" ControlToValidate="txtPassword" Text="<%$Resources:ITWORX.MOEHEWF.Common, RequiredPassword %>"  ValidationGroup="Login"></asp:RequiredFieldValidator>
<br />
<asp:Button ID="btnLogin" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Login %>"  ValidationGroup="Login" OnClick="btnLogin_Click" />
<asp:Button ID="btnLogout" runat="server"  Text="<%$Resources:ITWORX.MOEHEWF.Common, Logout %>" OnClick="btnLogout_Click"/>