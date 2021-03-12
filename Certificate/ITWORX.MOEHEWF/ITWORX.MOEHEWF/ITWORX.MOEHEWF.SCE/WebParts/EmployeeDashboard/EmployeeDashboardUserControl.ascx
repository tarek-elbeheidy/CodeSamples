<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeDashboardUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.WebParts.EmployeeDashboard.EmployeeDashboardUserControl" %>

<%@Register TagPrefix="PageFieldTextField" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"%>



     

<div class="row margin-top-25 margin-bottom-25 flex-display">
        <div class="col-md-6 col-xs-12">
               <h1 class=" page-title margin-top-0 margin-bottom-0">
                     <div data-name="Page Field: Title">
		                    
		                    
		                    <PageFieldTextField:TextField FieldName="fa564e0f-0c70-4ab9-b863-0177e6ddd247" runat="server">
		                    
		                    </PageFieldTextField:TextField>
		                    
		                </div>
               </h1>
           </div>
        <div class="col-md-6 col-xs-12 text-right">
                    <asp:HyperLink ID="hprLnkEquivalence" runat="server" CssClass="school-btn btn"><asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, BackHome %>" /></asp:HyperLink>

        </div>
    </div>

<div class="row  margin-bottom-25 no-margin-imp">
    <div class="clearfix"></div>
     <asp:Menu ID="Menu_ParentLinks" runat="server" OnMenuItemClick="Menu_ParentLinks_MenuItemClick" Orientation="Horizontal" CssClass="col-xs-12">
            <Items>
                <asp:MenuItem Text="<%$Resources:ITWORX_MOEHEWF_SCE, SCEMyRequests %>" Value="0" ></asp:MenuItem>
                   <asp:MenuItem Text="<%$Resources:ITWORX_MOEHEWF_SCE, Search %>" Value="1"></asp:MenuItem>
            
                  <asp:MenuItem Text="<%$Resources:ITWORX_MOEHEWF_SCE, SCEExceptionRequests %>" Value="2" ></asp:MenuItem>
            </Items>
        </asp:Menu>
       

     <asp:PlaceHolder ID="PlaceHolder_Requests_Search" runat="server"></asp:PlaceHolder>
   
</div>

      
<div class="row">

 

    <div class="menu-tabs">
        <asp:Menu ID="Menu_Links" runat="server" OnMenuItemClick="Menu_Links_MenuItemClick" Orientation="Horizontal"></asp:Menu>

    </div>

        <div class="tab-padd">
    <asp:PlaceHolder ID="PlaceHolder_RequestsUC" runat="server"></asp:PlaceHolder>

    </div>

</div>

