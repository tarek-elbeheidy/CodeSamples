<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewContactUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.WebParts.ViewContact.ViewContactUserControl" %>



<div class="displayMode tab-pane tab-padd" role="tabpanel">
    <div class="row">
        <div class="col-xs-12">
            <h2 class="tab-title"> 
                <asp:Label ID="Label1" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ContactsData %>"></asp:Label>
            </h2>
        </div>
    </div>
    <div class="dark-bg displayMode margin-top-25 margin-bottom-25 tab-pane-wrap">
        <div class="row">
            <div class="col-md-6 col-xs-12">
                <div class="form-group">
                    <h6><asp:Label ID="lblApplicantName" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, NameOfficialRecords %>"></asp:Label></h6>
                    <h5><asp:Label ID="lblApplicantNameValue" runat="server" ></asp:Label></h5>

                </div>
            </div>
            <div class="col-md-6 col-xs-12">
                <div class="form-group">
                    <h6><asp:Label ID="lblMobileNumber" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, MobileNumber %>"></asp:Label></h6>
                    <h5><asp:Label ID="lblMobileNumberValue" runat="server" ></asp:Label></h5>
                </div>
            </div>
            <div class="col-md-6  col-xs-12">
                <div class="form-group">
                    <h6><asp:Label ID="lblEmail" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Email %>"></asp:Label></h6>
                    <h5><asp:Label ID="lblEmailValue" runat="server" ></asp:Label></h5>
                </div>
            </div>
        </div>
    </div>
</div>
