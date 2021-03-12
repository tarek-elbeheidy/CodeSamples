<%@ Assembly Name="ITWORX.MOEHEWF.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7b2931724f1d7d1c" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TermsAndConditions.ascx.cs" Inherits="ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.TermsAndConditions" %>
<%--<asp:Label ID="lblTermsAndConditions" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, TermsAndConditions %>"></asp:Label>--%>

<div class="row margin-2">
     <asp:Label ID="lblTermsAndConditionsText" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, TermsAndConditionsText %>" CssClass="font-size-18 termsandcond"></asp:Label>       
</div>
<div class="download-wrap margin-top-10 margin-bottom-15  margin-2 row">        
    <asp:HyperLink ID="hypTerms" runat="server" CssClass="download-terms font-size-16"  Target="_blank"></asp:HyperLink>
</div>

<div class="row margin-top-15">
    <asp:CheckBox ID="chkTermsAndConditions"  runat="server"  Text="<%$Resources:ITWORX.MOEHEWF.Common, TermsAndConditionAgree %>" CssClass="font-size-18 termsandcond agree" ClientIDMode="Static" />    
       
</div>
<%--<asp:Repeater ID="rpt_Links" runat="server">

    <ItemTemplate>
<div class="download-wrap margin-top-10 margin-bottom-15  margin-2 row">        
    <asp:HyperLink ID="hyp1" runat="server" CssClass="download-terms font-size-16" NavigateUrl='<%# Eval("fileUrl") %>' Text='<%# Eval("Name") %>' Target="_blank"></asp:HyperLink>
</div>
  </ItemTemplate>
</asp:Repeater>--%>

<div style="display:none;">
    <asp:Button ID="btnAgree" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Agree %>" />
</div>
