<%@ Assembly Name="ITWORX.MOEHEWF.SCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7c6ec0a86ef11fff" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClarificationRequestSCE.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE.ClarificationRequestSCE" %>

<asp:Button ID="btn_NewRequest" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, NewClarRequest %>" OnClick="btn_NewRequest_Click" Visible="false" />

<div class="row no-padding">
	<h4 class="font-size-18 font-weight-600 pull-right">
		<asp:Label ID="lbl_NoOfRequests" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, NoOfRequests %>"></asp:Label>
	</h4>    
</div>

<asp:GridView ID="grd_SCEClarRequests" runat="server" AutoGenerateColumns="false" AllowPaging="true" 
    ShowHeaderWhenEmpty="true" OnRowDataBound="grd_SCEClarRequests_RowDataBound" EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>"  CssClass="table moe-table table-striped result-table margin-top-5">
 <Columns>
        
         <asp:TemplateField>
            <ItemTemplate>
                 <asp:HiddenField ID="hdn_ID" runat="server" Value='<%#  Eval("Id")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_ReqId" runat="server" Value='<%#  Eval("RequestIDId")%>'></asp:HiddenField>
                <asp:Label ID="lbl_RequiredClarDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ClarificationDate"))!=DateTime.MinValue ? Convert.ToDateTime(Eval("ClarificationDate")).ToShortDateString():string.Empty %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, RequestSender %>">
            <ItemTemplate>
                <asp:Label ID="lbl_Sender" runat="server" Text='<%#  Eval("Sender")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, RequiredClarification %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ReqClarification" runat="server" Text='<%# Eval("RequiredClarification") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, ClarificationReply %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ClarReply" runat="server" Text='<%#  Eval("ClarificationReply")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, ReplyDate %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ReplyDate" runat="server" Text='<%#  Eval("ReplyDate")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
      
       <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, Action %>">
            <ItemTemplate>
                <asp:LinkButton ID="lnk_ReplytoClarification" runat="server" Tooltip="<%$Resources:ITWORX_MOEHEWF_SCE, ReplytoClarification %>" CssClass="display-icon fa fa-reply" OnClick="lnk_ReplytoClarification_Click"></asp:LinkButton>
                <asp:LinkButton ID="lnk_View" runat="server" ToolTip="<%$Resources:ITWORX_MOEHEWF_SCE, View %>" OnClick="lnk_View_Click" CssClass="display-icon fa fa-eye"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
