<%@ Assembly Name="ITWORX.MOEHEWF.UCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=883afb4c05a35fe5" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.UCE/Procedure_AssistSecretaryListing.ascx" TagPrefix="uc1" TagName="Procedure_AssistSecretaryListing" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.UCE/RolesNavigationLinks.ascx" TagPrefix="uc1" TagName="RolesNavigationLinks" %>


<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Procedures_AssistSecretaryListingUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.WebParts.Procedures_AssistSecretaryListing.Procedures_AssistSecretaryListingUserControl" %>



<div class="row section-container side-nav-container">
    <div class="col-md-3 col-xs-4 no-padding"> <!-- required for floating -->
        <!-- Nav tabs -->
        <%--<ul class="nav nav-tabs tabs-left">
          <li class="active"><a href="#home" data-toggle="tab">Home</a></li>
          <li><a href="#profile" data-toggle="tab">Profile</a></li>
          <li><a href="#messages" data-toggle="tab">Messages</a></li>
          <li><a href="#settings" data-toggle="tab">Settings</a></li>
        </ul>--%>
       <uc1:RolesNavigationLinks runat="server" id="RolesNavigationLinks" />
    </div>
 
    <div class="col-md-9 col-xs-12 no-padding">
        <!-- Tab panes -->
        <%--<div class="tab-content">
          <div class="tab-pane active" id="home">Home Tab.</div>
          <div class="tab-pane" id="profile">Profile Tab.</div>
          <div class="tab-pane" id="messages">Messages Tab.</div>
          <div class="tab-pane" id="settings">Settings Tab.</div>
        </div>--%>
<uc1:Procedure_AssistSecretaryListing runat="server" id="Procedure_AssistSecretaryListing" />
    </div>
</div>
