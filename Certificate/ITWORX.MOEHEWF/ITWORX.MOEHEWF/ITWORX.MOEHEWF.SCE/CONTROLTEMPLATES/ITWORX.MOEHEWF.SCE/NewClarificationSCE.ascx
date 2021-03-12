<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/DDLWithTXTWithNoPostback.ascx" TagPrefix="uc1" TagName="DDLWithTXTWithNoPostback" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewClarificationSCE.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE.NewClarificationSCE" %>



<div class="new-clarification">
    <div class="clarifyRequest">
        <div class="">
            <h4 class="pageTitle">
                <asp:Label ID="Label1" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SendClarRequest %>"></asp:Label></h4>
            <div class="dark-bg request-padd margin-top-25">
                <div class="col-md-4  col-sm-6 col-xs-12">
                    <div class="form-group">
                        <label>
                            <asp:Label ID="lbl_RequestDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequestDate %>"></asp:Label></label>
                        <asp:TextBox ID="txt_RequestDate" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <label>
                            <asp:Label ID="lbl_Requester" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequestClarSender %>"></asp:Label></label>
                        <asp:TextBox ID="txt_Requester" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4  col-sm-6 col-xs-12">
                    <div class="form-group">
                         <%-- <label>
                          <asp:Label ID="lbl_Reason" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequestReason %>"></asp:Label></label>--%>
                       <%-- <asp:DropDownList ID="ddl_Reasons" runat="server" CssClass="moe-dropdown"></asp:DropDownList>--%>
                        <uc1:DDLWithTXTWithNoPostback runat="server" id="ddlClarificationReason" />

<%--                        <asp:RequiredFieldValidator ID="rfv_Reasons" runat="server" ControlToValidate="ddl_Reasons"
                            CssClass="error-msg" ValidationGroup="NewClar" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, Required %>"></asp:RequiredFieldValidator>--%>

                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="col-xs-12 margin-top-15">
                    <div class="form-group">
                        <label>
                            <asp:Label ID="lbl_NeededData" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, NeededReason %>"></asp:Label></label>
                        <asp:TextBox ID="txt_NeededData" runat="server" TextMode="MultiLine" CssClass="form-control text-area"   ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_NeededData" 
                            CssClass="error-msg" ValidationGroup="NewClar" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, Required %>"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txt_NeededData" ID="rev_neededData" 
                            CssClass="error-msg moe-full-width"
                            ValidationExpression="^[\s\S]{0,1000}$" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, NeededReasonLength %>"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="clearfix">
                </div>

            </div>

        </div>
    </div>


</div>

<div class="formBtns clarificatinBtns">
		<div class="row">
			<div class="col-sm-6 col-xs-12 text-left">
				<asp:Button ID="btn_Back" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ReturnToRequestDetails %>" OnClick="btn_Back_Click"  CssClass="moe-btn btn"/>
				</div>
			<div class="col-sm-6 col-xs-12 text-right">
			
                <asp:Button ID="btn_Send" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SendRequest %>" ValidationGroup="NewClar" OnClick="btn_Send_Click" CssClass="moe-btn btn"/>
				</div>
		</div>
	</div>
<script type="text/javascript">

    $("#<%=txt_NeededData.ClientID%>").attr('maxLength',1000);
</script>