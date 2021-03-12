<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.PA/PAProcedure_TechCommitteListing.ascx" TagPrefix="uc1" TagName="Procedure_TechCommitteListing" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.PA/PARolesNavigationLinks.ascx" TagPrefix="uc1" TagName="PARolesNavigationLinks" %>


<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PAProcedures_TechCommitteeListingUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.WebParts.Procedures_TechCommitteeListing.Procedures_TechCommitteeListingUserControl" %>




<div class="row section-container side-nav-container">
    <div class="col-md-3 col-xs-4 no-padding">
        <!-- required for floating -->
        <uc1:PARolesNavigationLinks runat="server" id="PARolesNavigationLinks" />

    </div>

    <div class="col-md-9 col-xs-12 no-padding test">
        <!-- required for floating -->
     <uc1:Procedure_TechCommitteListing runat="server" id="Procedure_TechCommitteListing" />
    </div>

</div>