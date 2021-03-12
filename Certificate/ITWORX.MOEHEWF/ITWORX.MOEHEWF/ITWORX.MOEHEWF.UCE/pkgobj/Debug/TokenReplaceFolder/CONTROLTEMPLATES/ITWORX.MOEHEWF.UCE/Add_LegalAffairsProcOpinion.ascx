<%@ Assembly Name="ITWORX.MOEHEWF.UCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=883afb4c05a35fe5" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="MOEHE" TagName="FileUpload" %>


<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Add_LegalAffairsProcOpinion.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.Add_LegalAffairsProcOpinion" %>


<div id="main-content">
    <div class="row section-container side-nav-container">
        <div class="row unheighlighted-section margin-bottom-50 flex-display flex-wrap">
            <div class="col-md-3 col-sm-6 margin-top-10 margin-bottom-10">
                <div class="data-container table-display moe-width-85">
                    <div class="form-group">
                        <MOEHE:FileUpload runat="server" id="legalAffairsAttachements" LabelDisplayName="legalAffairsAttachements" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>


<asp:Panel ID="pnl_Opinion" runat="server" GroupingText="<%$Resources:ITWORX_MOEHEWF_UCE, InitialOpinion %>" CssClass="stateTitle">
    <asp:TextBox ID="txt_InitialOpinion" runat="server" TextMode="MultiLine" CssClass="moe-full-width input-height-42 border-box moe-input-padding text-area"></asp:TextBox>
      <asp:RequiredFieldValidator ID="reqVal_InitialOpinion" runat="server" ForeColor="Red" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredInitialOpinion %>"
        ValidationGroup="Approve" ControlToValidate="txt_InitialOpinion"></asp:RequiredFieldValidator>
</asp:Panel>

<asp:Button ID="btn_ApproveProc" runat="server" ValidationGroup="Approve" OnClick="btn_ApproveProc_Click" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ApproveProcedure %>"/>
