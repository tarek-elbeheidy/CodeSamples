<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BreadCrumbUserControl.ascx.cs" Inherits="MOEHE.PSPES.Webparts.BreadCrumb.BreadCrumbUserControl" %>
<div class="section-content">
    <div class="row">
        <div class="col-md-12">
            <h2 class="text-theme-colored2-1 font-36 mt-90">
                <asp:Label runat="server" ID="pageTitle"></asp:Label></h2>
            <ol class="breadcrumb text-left mt-10 white">
                <li>
                    <asp:LinkButton ID="lnkPortalHome" CausesValidation="false" runat="server">

                        <asp:Literal runat="server" ID="BC_PortalTitle" Text="" />

                    </asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="lnkParentSite" CausesValidation="false" runat="server">

                        <asp:Literal runat="server" ID="BC_ParentSiteTitle" Text="" />

                    </asp:LinkButton>
                </li>
                <li class="active">
                    <asp:Label runat="server" ID="BC_PageTitle"></asp:Label>
                </li>
            </ol>
        </div>
    </div>
</div>


