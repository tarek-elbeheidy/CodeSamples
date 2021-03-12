<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="MOEHE" TagName="FileUpload" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PAAdd_LegalAffairsProcOpinion.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.Add_LegalAffairsProcOpinion" %>

<MOEHE:FileUpload runat="server" id="legalAffairsAttachements" LabelDisplayName="legalAffairsAttachements" />

<asp:Panel ID="pnl_Opinion" runat="server" GroupingText="<%$Resources:ITWORX_MOEHEWF_PA, InitialOpinion %>">
    <asp:TextBox ID="txt_InitialOpinion" runat="server" TextMode="MultiLine"></asp:TextBox>
    <asp:RequiredFieldValidator ID="reqVal_InitialOpinion" runat="server" ForeColor="Red" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RequiredInitialOpinion %>"
        ValidationGroup="Approve" ControlToValidate="txt_InitialOpinion"></asp:RequiredFieldValidator>
</asp:Panel>

<asp:Button ID="btn_ApproveProc" runat="server" ValidationGroup="Approve" OnClick="btn_ApproveProc_Click" Text="<%$Resources:ITWORX_MOEHEWF_PA, ApproveProcedure %>" />
