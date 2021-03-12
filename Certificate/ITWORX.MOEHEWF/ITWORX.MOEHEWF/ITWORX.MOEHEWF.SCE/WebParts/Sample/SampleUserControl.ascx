<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SampleUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.WebParts.Sample.SampleUserControl" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/ClientSideFileUpload.ascx" TagPrefix="uc1" TagName="ClientSideFileUpload" %>


<div class="row fileUploadContainer">
    <div class="col-md-6 col-sm-6 col-xs-8 margin-bottom-10">
        <div class="form-group">
            <label>أسم الملف  <asp:Label ID="lbldropFileUpload" runat="server" CssClass="error-msg" Style="display: none;">*</asp:Label></label>
            <asp:DropDownList ID="dropFileUpload" runat="server" CssClass="form-control moe-dropdown">
                <asp:ListItem Text="Select" Value=""></asp:ListItem>
                <asp:ListItem Text="CertificateOutside" Value="1"></asp:ListItem>
                <asp:ListItem Text="GeneralOutside" Value="2"></asp:ListItem>
            </asp:DropDownList>
           
            <asp:Label ID="lblRequiredDrop" runat="server" Style="display: none;" ForeColor="Red">You should choose file name </asp:Label>
        </div>
    </div>

    <div class="clearfix"></div>
    <uc1:ClientSideFileUpload runat="server" id="FileUp1" />
    

</div>


<div class="row fileUploadContainer margin-top-15">
    <div class="col-md-6 col-sm-6 col-xs-8 margin-bottom-10">
        <div class="form-group">
            <label>أسم الملف <asp:Label ID="lbldropFileUpload2" runat="server" CssClass="error-msg" Style="display: none;">*</asp:Label></label>
            <asp:DropDownList ID="dropFileUpload2" runat="server" CssClass="form-control moe-dropdown">
                <asp:ListItem Text="Select" Value="" Selected="True"></asp:ListItem>
                <asp:ListItem Text="CertificateOutside2" Value="1"></asp:ListItem>
                <asp:ListItem Text="GeneralOutside2" Value="2"></asp:ListItem>
            </asp:DropDownList>
              <asp:Label ID="lblRequiredDrop2" runat="server" Style="display: none;" CssClass="error-msg">You should choose file name </asp:Label>
        </div>
    </div>

    <div class="clearfix"></div>
    <uc1:ClientSideFileUpload runat="server" id="FileUp2" />
    
  

</div>


<div class="row fileUploadContainer margin-top-15">
    <uc1:ClientSideFileUpload runat="server" id="FileUp3" />
</div>

<div class="row fileUploadContainer margin-top-15">
    <uc1:ClientSideFileUpload runat="server" id="FileUp4" />
</div>



<div class="row">
    <div class="col-xs-12 text-right">
        <asp:Button runat="server" ID="btnSubmit" ValidationGroup="Submit" Text="Submit" OnClick="btnSubmit_Click" CssClass="Sub btn school-btn" />

    </div>
</div>


<asp:ValidationSummary ID="ValidationSummary1" Enabled="true" runat="server" ValidationGroup="Submit" ShowSummary="true" ShowValidationErrors="true" CssClass="validation-summary" />

