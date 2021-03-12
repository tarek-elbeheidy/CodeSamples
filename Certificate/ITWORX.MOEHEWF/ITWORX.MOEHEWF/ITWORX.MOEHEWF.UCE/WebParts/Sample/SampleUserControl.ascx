<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/16/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="MOEHE" TagName="FileUpload" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SampleUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.WebParts.Sample.SampleUserControl" %>

<asp:Label ID="Label1" runat="server"></asp:Label>
<br />
<asp:Label ID="Label2" runat="server"></asp:Label>
<br />
<asp:Label ID="Label3" runat="server"></asp:Label>
<br />
<asp:Label ID="Label4" runat="server"></asp:Label>
<br />
<asp:Label ID="Label5" runat="server"></asp:Label>
<br />
<asp:Label ID="Label6" runat="server"></asp:Label>
<br />
<asp:Label ID="Label7" runat="server"></asp:Label>
<br />
<asp:Label ID="Label8" runat="server"></asp:Label>
<br />
<asp:Label ID="Label9" runat="server"></asp:Label>
<br />
<asp:Label ID="Label10" runat="server"></asp:Label>
<br />
<asp:Label ID="Label11" runat="server"></asp:Label>
<br />
<asp:Label ID="Label12" runat="server"></asp:Label>
<br />
<asp:Label ID="Label13" runat="server"></asp:Label>
<br />

<MOEHE:FileUpload runat="server" id="fileUpload" LabelDisplayName = "fileUpload"/>

 <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, GlobalSample %>"></asp:Literal>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    
<asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />