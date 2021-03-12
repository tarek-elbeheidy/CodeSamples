<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.PA/PARolesNavigationLinks.ascx" TagPrefix="uc1" TagName="PARolesNavigationLinks" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.PA/PADisplaySimilarPARequestDetails.ascx" TagPrefix="uc1" TagName="DisplaySimilarPARequestDetails" %>


<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PAReviewerDisplaySimilarRequestDetailsUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.WebParts.PAReviewerDisplaySimilarRequestDetails.PAReviewerDisplaySimilarRequestDetailsUserControl" %>



<div class="row section-container side-nav-container">
    <div class="col-md-3 col-xs-4 no-padding"> <!-- required for floating -->
        <!-- Nav tabs -->
        <%--<ul class="nav nav-tabs tabs-left">
          <li class="active"><a href="#home" data-toggle="tab">Home</a></li>
          <li><a href="#profile" data-toggle="tab">Profile</a></li>
          <li><a href="#messages" data-toggle="tab">Messages</a></li>
          <li><a href="#settings" data-toggle="tab">Settings</a></li>
        </ul>--%>
    <uc1:PARolesNavigationLinks runat="server" id="PARolesNavigationLinks" />
    </div>
 
   <div class="col-md-9 col-xs-12 no-padding test">
        <!-- Tab panes -->
        <%--<div class="tab-content">
          <div class="tab-pane active" id="home">Home Tab.</div>
          <div class="tab-pane" id="profile">Profile Tab.</div>
          <div class="tab-pane" id="messages">Messages Tab.</div>
          <div class="tab-pane" id="settings">Settings Tab.</div>
        </div>--%>
<uc1:DisplaySimilarPARequestDetails runat="server" id="DisplaySimilarPARequestDetails" />
    </div>
</div>