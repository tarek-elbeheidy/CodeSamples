<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@Register TagPrefix="PageFieldTextField" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"%>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PAEmployeeDashboardUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.WebParts.PAEmployeeDashboard.PAEmployeeDashboardUserControl" %>

<div class="section-container">
     

    <div class="row margin-bottom-0 ">
        <div class="col-md-7 col-xs-12">
               <h1 class=" section-title font-weight-600 no-margin margin-top-0" style="margin-bottom:30px;font-size: calc(.9em + 1.05vw);">
                     <div data-name="Page Field: Title">
		                    
		                    
		                    <PageFieldTextField:TextField FieldName="fa564e0f-0c70-4ab9-b863-0177e6ddd247" runat="server">
		                    
		                    </PageFieldTextField:TextField>
		                    
		                </div>
               </h1>
           </div>
        <div class="col-md-5 col-xs-12 text-right">
                    <asp:HyperLink ID="hprLnkEquivalence" runat="server" CssClass="moe-btn btn"><asp:Literal runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, BackToHome %>" /></asp:HyperLink>

        </div>
    </div>

<div class="row  margin-bottom-25">
     <asp:Menu ID="Menu_ParentLinks" runat="server" OnMenuItemClick="Menu_ParentLinks_MenuItemClick" Orientation="Horizontal">
            <Items>
                <asp:MenuItem Text="<%$Resources:ITWORX_MOEHEWF_PA, PAMyRequests %>" Value="0"></asp:MenuItem>
                <asp:MenuItem Text="<%$Resources:ITWORX_MOEHEWF_PA, Search %>" Value="1"></asp:MenuItem>
            </Items>
        </asp:Menu>
       

     <asp:PlaceHolder ID="PlaceHolder_Requests_Search" runat="server"></asp:PlaceHolder>
</div>

      
<div class="row">

        <!-- required for floating -->

         <div class="menu-tabs">
        <asp:Menu ID="Menu_Links" runat="server" OnMenuItemClick="Menu_Links_MenuItemClick" Orientation="Horizontal"></asp:Menu>
      
</div>



        <!-- required for floating -->
      
          
      

      
            <asp:PlaceHolder ID="PlaceHolder_RequestsUC" runat="server"></asp:PlaceHolder>
        

 
           



</div>
    
</div>