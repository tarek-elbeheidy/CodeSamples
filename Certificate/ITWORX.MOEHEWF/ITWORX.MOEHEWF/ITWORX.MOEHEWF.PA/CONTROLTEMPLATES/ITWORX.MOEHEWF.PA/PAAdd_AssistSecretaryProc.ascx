<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PAAdd_AssistSecretaryProc.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.Add_AssistSecretaryProc" %>

<asp:Panel ID="pnl_AssistSecretaryProc" runat="server" GroupingText="<%$Resources:ITWORX_MOEHEWF_PA, AssistSecretaryProc %>">
    <asp:Label ID="lbl_Procedure" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, Procedure %>"></asp:Label>
    <asp:DropDownList ID="drp_Procedure" runat="server" OnSelectedIndexChanged="drp_Procedure_SelectedIndexChanged" AutoPostBack="true">
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="reqVal_Procedure" runat="server" ForeColor="Red" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RequiredProcedure %>"
        ValidationGroup="Approve" ControlToValidate="drp_Procedure"></asp:RequiredFieldValidator>

    <asp:Label ID="lbl_RejectionReason" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, RejectionReason %>" Visible="false"></asp:Label>
    <asp:DropDownList ID="drp_RejectionReason" runat="server" Visible="false"></asp:DropDownList>
    <asp:Label ID="lbl_Comments" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, ProcedureComments %>"></asp:Label>
    <asp:TextBox ID="txt_Comments" runat="server" TextMode="MultiLine"></asp:TextBox>
    <asp:RequiredFieldValidator ID="reqVal_Comments" runat="server" ForeColor="Red" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RequiredComments %>"
        ValidationGroup="Approve" ControlToValidate="txt_Comments"></asp:RequiredFieldValidator>

    <asp:Button ID="btn_ApproveProcedures" ValidationGroup="Approve" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, ApproveProcedure %>" OnClick="btn_ApproveProcedures_Click" />
</asp:Panel>