<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%--<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.PA/PAAdd_ProcedureProgramManager.ascx" TagPrefix="uc1" TagName="Add_ProgramManagerProc" %>--%>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PAProcedures_ProgramManager.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.Procedures_ProgramManager" %>


<div class="row no-padding">
	<asp:LinkButton ID="lnk_AddNewProcedurePopUp" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, AddNewProcedure %>" OnClick="lnk_AddNewProcedurePopUp_Click" CssClass="btn moe-btn pull-right" />
</div>


<asp:GridView ID="grd_Procedures" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="grd_Procedures_PageIndexChanging"
	ShowHeaderWhenEmpty="true" EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>" CssClass="table table-striped moe-full-width moe-table result-table margin-top-25">
	<Columns>
		<asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, Procedure %>">
			<ItemTemplate>
				<asp:HiddenField ID="hdn_Id" runat="server" Value='<%# Eval("ID") %>' />
				<asp:Label ID="lbl_Procedure" runat="server" Text='<%#  Eval("Procedure")%>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, ProcedureDate %>">
			<ItemTemplate>
				<asp:HiddenField ID="hdn_RequestID" runat="server" Value='<%# Eval("RequestID") %>' />
				<asp:Label ID="lbl_ProcedureDate" runat="server" Text='<%#  Eval("ProcedureDate")%>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, ProcedureComments %>">
			<ItemTemplate>
				<asp:Label ID="lbl_ProcedureComments" runat="server" Text='<%#  Eval("ProcedureComments")%>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, ProcedureCreatedBy %>">
			<ItemTemplate>
				<asp:Label ID="lbl_ProcedureCreatedBy" runat="server" Text='<%#  Eval("ProcedureCreatedBy")%>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField>
			<ItemTemplate>
				<asp:LinkButton ID="lnk_View" runat="server" OnClick="lnk_View_Click" Text="<%$Resources:ITWORX_MOEHEWF_PA, View %>" />
			</ItemTemplate>
		</asp:TemplateField>

	</Columns>
</asp:GridView>
<div class="row no-padding">
	<div class="col-md-12">
        <h4 class="font-size-18 font-weight-600 text-right">
		<asp:Label ID="lbl_NoOfRequests" runat="server"></asp:Label>
	</h4>
	</div>
</div>
