<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Add_ProcedureProgramManager.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.Add_ProcedureProgramManager" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="MOEHE" TagName="FileUpload" %>

<div id="main-content">
    <div class="row unheighlighted-section margin-bottom-50 flex-display flex-wrap updatePanel" id="DialogProc">
        <asp:Panel ID="pnl" runat="server">
            <%--<ContentTemplate>--%>
            <div class="col-md-4 col-sm-6 margin-top-10 margin-bottom-10">
                <div class="data-container table-display moe-width-85">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                            <asp:Label ID="lbl_Procedure" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Procedure %>"></asp:Label>

                            <span class="error-msg astrik">* </span>
                        </h6>

                        <asp:DropDownList ID="drp_Procedure" runat="server" CssClass="moe-dropdown moe-full-width input-height-42 border-box moe-input-padding" OnSelectedIndexChanged="drp_Procedure_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqVal_Procedure" runat="server" ForeColor="Red" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredProcedure %>"
                            ValidationGroup="Approve" ControlToValidate="drp_Procedure" CssClass="error-msg moe-full-width"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>

            <div class="col-md-4 col-sm-6 margin-top-10 margin-bottom-10">
                <div class="data-container table-display moe-width-85">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                            <asp:Label ID="lbl_EmpName" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EmpName %>" Visible="false"></asp:Label>
                        </h6>

                        <asp:DropDownList ID="drp_Employees" runat="server" Visible="false" CssClass="moe-dropdown moe-full-width input-height-42 border-box moe-input-padding"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-md-12 col-sm-12 col-xs-12 no-padding">
                <div class="col-md-6 no-padding">
                    <MOEHE:FileUpload runat="server" id="ProcedureProgramManagerAttachements" />
                </div>
            </div>
            <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10 margin-bottom-10">
                <div class="data-container table-display moe-full-width">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                            <asp:Label ID="lbl_Comments" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ProcedureComments %>"></asp:Label>

                            <span class="error-msg astrik">* </span>
                        </h6>

                        <asp:TextBox ID="txt_Comments" runat="server" TextMode="MultiLine" CssClass="moe-full-width input-height-42 border-box moe-input-padding text-area"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqVal_Comments" runat="server" ForeColor="Red" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredComments %>"
                            ValidationGroup="Approve" ControlToValidate="txt_Comments" CssClass="error-msg moe-full-width"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>

            <div class="col-md-12 no-padding">
                <asp:Button ID="btn_ApproveProc" runat="server" ValidationGroup="Approve" OnClick="btn_ApproveProc_Click" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ApproveProcedure %>" CssClass="btn moe-btn pull-right" />
            </div>

            <%--</ContentTemplate>--%>
            <%--<Triggers>
			<asp:PostBackTrigger ControlID="drp_Procedure" />
			<asp:AsyncPostBackTrigger ControlID="btn_ApproveProc" EventName="Click" />
		</Triggers>--%>
        </asp:Panel>
    </div>
</div>