<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddSCEStatementUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.WebParts.AddSCEStatement.AddSCEStatementUserControl" %>
<div class="new-clarification">
    <div class="clarifyRequest">
        <div class="">
            <h4 class="pageTitle">
                <asp:Label ID="Label6" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, submitNewStatement %>"></asp:Label>
            </h4>
            <div class="dark-bg request-padd margin-top-25">
                <div class="col-md-4">
                    <div class="form-group">
                        <label>
                            <asp:Label ID="Label3" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StatementDate %>"></asp:Label></label>

                        <asp:TextBox ID="txt_Date" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4 col-md-offset-1">
                    <div class="form-group">
                        <label>
                            <asp:Label ID="Label2" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StatementSender %>"></asp:Label></label>

                        <asp:TextBox ID="txt_Sender" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="clearfix"></div>
                <div class="col-md-4 margin-top-15">
                    <div class="form-group">
                        <label>
                            <asp:Label ID="Label1" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StatementSubject %>"></asp:Label></label><span class="error-msg">*</span>

                        <asp:TextBox ID="txt_Topic" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, StatementSubjectRequired %>" ControlToValidate="txt_Topic" ValidationGroup="statementGroup" CssClass="error-msg"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="col-md-4 col-md-offset-1  margin-top-15">
                    <div class="form-group">
                        <label>
                            <asp:Label ID="Label4" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StatementAgency %>"></asp:Label></label><span class="error-msg">*</span>

                        <asp:DropDownList ID="ddl_Agency" CssClass="moe-dropdown select2-hidden-accessible" data-select2-id="42" TabIndex="-1" aria-hidden="true" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, StatementAgencyRequired %>" ControlToValidate="ddl_Agency" ValidationGroup="statementGroup" CssClass="error-msg" InitialValue="-1"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="clearfix">
                </div>
                <div class="col-xs-12 margin-top-15">
                    <div class="form-group">
                        <label>
                            <asp:Label ID="Label5" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequiredStatement %>"></asp:Label></label><span class="error-msg">*</span>

                        <asp:TextBox ID="txt_RequiredStatment" runat="server" TextMode="MultiLine" Columns="20" Rows="10" CssClass="form-control text-area"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, StatementRequired %>" ControlToValidate="txt_RequiredStatment" ValidationGroup="statementGroup" CssClass="error-msg"></asp:RequiredFieldValidator>
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
            <asp:Button ID="btn_Back" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Back %>" CssClass="moe-btn btn" OnClick="btn_Back_Click" />
        </div>
        <div class="col-sm-6 col-xs-12 text-right">
            <asp:Button ID="btn_Send" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SendStatement %>" CssClass="moe-btn btn" OnClick="btn_Send_Click" ValidationGroup="statementGroup" />

        </div>
    </div>
</div>
