<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavigationLinksSCE.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE.NavigationLinksSCE" %>

<asp:Repeater ID="rep_Main" runat="server" OnItemDataBound="rep_Main_ItemDataBound">
    <HeaderTemplate>
        <ul class="sideLinkWrap margin-bottom-0">
    </HeaderTemplate>
    <ItemTemplate>
        <li class="sideNavLinks mainItem sideMenuItem">

            <asp:HyperLink ID="lnk_Main" runat="server"></asp:HyperLink>

        </li>
    </ItemTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
</asp:Repeater>

<asp:Repeater ID="rep_Action" runat="server" OnItemDataBound="rep_Action_ItemDataBound">
    <HeaderTemplate>

        <h6 data-toggle="collapse" data-target="#actions" class="sideNavLinks general-item margin-top-0 margin-bottom-0">
            <a>
                <asp:Label ID="lbl_Action" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ActionNavigation %>"></asp:Label></a>
        </h6>
        <ul  class="sub-menu collapse in" id="actions">
    </HeaderTemplate>
    <ItemTemplate>
        <li class="sideNavLink sideMenuItem">

            <asp:HyperLink ID="lnk_Action" runat="server"></asp:HyperLink>

        </li>
    </ItemTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
</asp:Repeater>

<asp:Repeater ID="rep_View" runat="server" OnItemDataBound="rep_View_ItemDataBound">
    <HeaderTemplate>
        <h6 data-toggle="collapse" data-target="#viewOnlyItems" class="sideNavLinks general-item margin-top-0 margin-bottom-0">
            <a> <asp:Label ID="lbl_View" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ViewNavigation %>"></asp:Label></a>
        </h6>
        <ul  class="sub-menu collapse in" id="viewOnlyItems">
    </HeaderTemplate>
    <ItemTemplate>
        <li class="sideNavLink sideMenuItem">

            <asp:HyperLink ID="lnk_View" runat="server"></asp:HyperLink>

        </li>
    </ItemTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
</asp:Repeater>
