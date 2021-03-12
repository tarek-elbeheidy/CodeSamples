<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MOEHE_TC.ascx.cs" Inherits="ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.MOEHE_TC" %>


<style>
    .download-terms{
        display: block!important;
        margin-bottom: 10px;
    }
</style>

		<div class="row">
			<div class="col-xs-12 checkbox">
			<h4 class="aboutServiceTitle"> <%=Resources.ITWORX.MOEHEWF.Common.SchoolingCertificateTitle %></h4>
			<h6 class="aboutService"><%=Resources.ITWORX.MOEHEWF.Common.SchoolingCertificateDescription %>	</h6>

<h4 class="aboutServiceTitle"><%=Resources.ITWORX.MOEHEWF.Common.Reminder %></h4>
<h5 class="font-size-18 margin-bottom-50 margin-top-0 instruction-details color-black font-family-sans">
            <span><%=Resources.ITWORX.MOEHEWF.Common.InstructionsDetails %></span>

        </h5>

<div class="download-wrap margin-bottom-15">
					

                    <asp:Repeater ID="repTermsAndConditions" runat="server"  >
        <ItemTemplate>
            <asp:HyperLink ID="hypTerms" runat="server" CssClass="download-terms"  Target="_blank" NavigateUrl='<%#Eval("FileURL") %>' Text='<%#Eval("FileName") %>'></asp:HyperLink>
   
        </ItemTemplate>
    </asp:Repeater>
				</div>

<h4 class="aboutServiceTitle"><%=Resources.ITWORX.MOEHEWF.Common.TermsAndCond %></h4>
							<h5 class="termsandcond"><asp:Label ID="lblTermsAndConditionsText" runat="server"></asp:Label></h5>
				
				<div class=" termsandcond agree margin-top-10">
					    <asp:CheckBox ID="chkTermsAndConditions"  runat="server"  Text="<%$Resources:ITWORX.MOEHEWF.Common, TermsAndConditionAgree %>" CssClass="font-size-18 termsandcond agree" ClientIDMode="Static" />    
 </div>
			</div>
		</div>



