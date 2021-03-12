<%@ Assembly Name="ITWORX.MOEHEWF.UCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=883afb4c05a35fe5" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Procedures_Receptionist.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.Procedures_Receptionist" %>


<div class="row no-padding">
    <asp:LinkButton ID="lnk_AddNewProcedurePopUp" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, AddNewProcedure %>" OnClick="lnk_AddNewProcedurePopUp_Click" CssClass="btn moe-btn pull-right" Visible="false"/>
</div>


<asp:GridView ID="grd_Procedures" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="grd_Procedures_PageIndexChanging"
    ShowHeaderWhenEmpty="true" EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>" CssClass="table moe-table table-striped result-table margin-top-25">
    <Columns>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, ProcedureDate %>">
            <ItemTemplate>
                <asp:HiddenField ID="hdn_Id" runat="server" Value='<%# Eval("ID") %>' />
                <asp:HiddenField ID="hdn_RequestID" runat="server" Value='<%# Eval("RequestID") %>' />

                <asp:Label ID="lbl_ProcedureDate" runat="server" Text='<%#  Eval("ProcedureDate")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, ProcedureComments %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ProcedureComments" runat="server" Text='<%#  Eval("ProcedureComments")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, ProcedureCreatedBy %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ProcedureCreatedBy" runat="server" Text='<%#  Eval("ProcedureCreatedBy")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="lnk_View" runat="server" OnClick="lnk_View_Click" Tooltip="<%$Resources:ITWORX_MOEHEWF_UCE, View %>" CssClass="display-icon fa fa-eye"/>
            </ItemTemplate>
        </asp:TemplateField>

    </Columns>
</asp:GridView>

<div class="row no-padding">
	<h4 class="font-size-18 font-weight-600 text-center">
		<asp:Label ID="lbl_NoProcedeures" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NoProcedures %>" Visible="false"></asp:Label>
	</h4>
</div>

<div class="row no-padding">
	<h4 class="font-size-18 font-weight-600 text-right">
		<asp:Label ID="lbl_NoOfRequests" runat="server"></asp:Label>
	</h4>
</div>



