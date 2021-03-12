<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="MOEHE" TagName="FileUpload" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Add_TechCommitteeProcedure.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.Add_TechCommitteeProcedure" %>

<asp:HiddenField ID="hdn_SenderName" runat="server" />
    <asp:Label ID="lbl_Procedure" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Procedure %>"></asp:Label>
 <%--   <asp:DropDownList ID="drp_Procedure" runat="server">
    </asp:DropDownList>--%>
    <asp:RequiredFieldValidator ID="reqVal_Procedure" runat="server" ForeColor="Red" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredProcedure %>"
        ValidationGroup="Approve" ControlToValidate="drp_Procedure"></asp:RequiredFieldValidator>
    <asp:Label ID="lbl_Comments" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ProcedureComments %>"></asp:Label>
    <asp:TextBox ID="txt_Comments" runat="server" TextMode="MultiLine" CssClass="moe-full-width input-height-42 border-box moe-input-padding text-area"></asp:TextBox>
    <asp:RequiredFieldValidator ID="reqVal_Comments" runat="server" ForeColor="Red" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredComments %>"
        ValidationGroup="Approve" ControlToValidate="txt_Comments"></asp:RequiredFieldValidator>

<MOEHE:FileUpload runat="server" id="TechCommitteeAttachements" />

    <asp:Button ID="btn_ApproveProcedures" ValidationGroup="Approve" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ApproveProcedure %>" OnClick="btn_ApproveProcedures_Click" />
