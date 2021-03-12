<%@ Assembly Name="ITWORX.MOEHEWF.UCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=883afb4c05a35fe5" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.UCE/PaymentRecords.ascx" TagPrefix="uc1" TagName="PaymentRecords" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.UCE/RolesNavigationLinks.ascx" TagPrefix="uc1" TagName="RolesNavigationLinks" %>


<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentRecordsUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.WebParts.PaymentRecords.PaymentRecordsUserControl" %>





<div class="row section-container side-nav-container">
    <div class="col-md-3 col-xs-4 no-padding">
        <!-- required for floating -->
        <uc1:RolesNavigationLinks runat="server" id="RolesNavigationLinks" />

    </div>

    <div class="col-md-9 col-xs-12 no-padding test">
        <!-- required for floating -->
      <uc1:PaymentRecords runat="server" id="PaymentRecords" />
    </div>

</div>
