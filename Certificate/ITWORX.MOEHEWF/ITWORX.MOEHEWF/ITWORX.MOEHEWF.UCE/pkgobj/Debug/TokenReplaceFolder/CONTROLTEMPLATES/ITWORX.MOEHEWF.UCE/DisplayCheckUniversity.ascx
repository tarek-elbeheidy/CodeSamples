<%@ Assembly Name="ITWORX.MOEHEWF.UCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=883afb4c05a35fe5" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DisplayCheckUniversity.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.DisplayCheckUniversity" %>

<div id="uniDetails" class="row heighlighted-section margin-bottom-50 flex-display flex-wrap">

		<div class="col-md-4 col-sm-6">
			<div class="data-container">
				<h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lblYear" runat="server"  Text="<%$Resources:ITWORX_MOEHEWF_UCE, Year  %>" Font-Bold="true"></asp:Label>
				</h6>
				<h5 class="font-size-20">
                    <asp:Label ID="lblYearValue" runat="server" ></asp:Label>

				</h5>
			</div>
		</div>

     <div id="uniValues" runat="server" visible="false">

    
    <div class="col-md-4 col-sm-6">
			<div class="data-container">
				<h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lblCountry" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CountryOfStudy  %>" Font-Bold="true"></asp:Label>
				</h6>
				<h5 class="font-size-20">
                    <asp:Label ID="lblCountryValue" runat="server"></asp:Label>

				</h5>
			</div>
		</div>

    <div class="col-md-4 col-sm-6">
			<div class="data-container">
				<h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lblRequestUniversity" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Universities  %>" Font-Bold="true"></asp:Label>
				</h6>
				<h5 class="font-size-20">
                    <asp:Label ID="lblRequestUniversityValue" runat="server"></asp:Label>

				</h5>
			</div>
		</div>

    <div class="col-md-4 col-sm-6">
			<div class="data-container">
				<h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lblUniversityList" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversitiesLists  %>" Font-Bold="true"></asp:Label>
				</h6>
				<h5 class="font-size-20">
                    <asp:Label ID="lblUniversityListValue" runat="server"></asp:Label>

				</h5>
			</div>
		</div>
      
   </div>
</div>
