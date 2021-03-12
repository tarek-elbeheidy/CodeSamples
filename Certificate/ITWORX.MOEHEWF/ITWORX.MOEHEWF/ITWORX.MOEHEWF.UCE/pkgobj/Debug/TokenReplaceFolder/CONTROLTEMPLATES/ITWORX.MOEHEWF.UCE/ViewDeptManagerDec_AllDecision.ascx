<%@ Assembly Name="ITWORX.MOEHEWF.UCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=883afb4c05a35fe5" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewDeptManagerDec_AllDecision.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.ViewDeptManagerDec_AllDecision" %>

<asp:HiddenField ID="hdBookNum" runat="server" />
<asp:HiddenField ID="hdBookDate" runat="server" />
<div class="row">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div class="data-container">
            <h6 class="font-size-16 margin-bottom-15">
                <asp:Label ID="lblHeadManager" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, HeadManager %>" Visible="false"></asp:Label>
            </h6>
            <h5 class="font-size-18">
                <asp:Label ID="lblDecisionMaker" runat="server"></asp:Label>
            </h5>
        </div>
    </div>
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div class="data-container">
            <h6 class="font-size-16 margin-bottom-15">
                <asp:Label ID="lbl_Decision" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, FinalDecision %>" Visible="false"></asp:Label>
            </h6>
            <h5 class="font-size-18">
                <asp:Label ID="lbl_FinalDecisionVal" runat="server"></asp:Label>
            </h5>
        </div>
    </div>
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div class="data-container">
            <h6 class="font-size-16 margin-bottom-15">
                <asp:Label ID="lbl_RejectionReason" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RejectionReason %>" Visible="false"></asp:Label>
            </h6>
            <h5 class="font-size-18">
                <asp:Label ID="lbl_RejectionReasonVal" runat="server" Visible="false"></asp:Label>
            </h5>
        </div>
    </div>

    <div id="decisionDiv" class="col-md-12 col-sm-12 col-xs-12" runat="server" visible="false">
        <div class="data-container">
            <h6 class="font-size-16 margin-bottom-15">
                <asp:Label ID="lbl_PrintDecision" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, DecisiontxtForPrinting %>" Visible="false"></asp:Label>
            </h6>

            <div class="form recommend-form heighlighted-section">
                <asp:HiddenField ID="hdn_RequestNumber" runat="server" />
                <h1>
                    <label><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","sir") %></label>
                    <asp:Label ID="lbl_OccupationName" runat="server" Text=""></asp:Label>
                    <label><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","recpected") %></label>
                </h1>
                <h1>
                    <asp:Label ID="lbl_EntityNeedsEquivalencyView" runat="server" Text=""></asp:Label>
                </h1>
             
                <h1>
                    <label><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","WelcomText") %></label>
                </h1>
          
                <h1>
                    <asp:Label ID="lbl_decicionBodyView" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, DecisionBody %>"></asp:Label>
                </h1>
                <h1>
                    <asp:Label ID="lbl_DecisiontxtVal" runat="server"></asp:Label>
                </h1>
                <h3>
                    <label><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","AppreciationText") %></label>
                </h3>
                <h4>
                    <asp:Label ID="lbl_headManagerName" runat="server" ></asp:Label> 
                </h4>
                <h4>
                    <label><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","EquivalencManager") %></label>
                </h4>
            </div>
       
        </div>
        </div>
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lbl_Comments" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ProcedureComments %>" Visible="false"></asp:Label>
                </h6>
                <h5 class="font-size-18">
                    <asp:Label ID="lbl_CommentsVal" runat="server"></asp:Label>
                </h5>
            </div>
        </div>

        <div class="col-md-12 no-padding">
            <asp:Button ID="btn_Reject" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ClosedByRejection %>" OnClick="btn_Reject_Click" Visible="false" CssClass="btn moe-btn pull-right clear-btn" />
        </div>

        <div class="col-md-12 no-padding">
            <asp:Button ID="btn_Approve" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ClosedByAcceptance %>" OnClick="btn_Approve_Click" Visible="false" CssClass="btn moe-btn pull-right" />
        </div>

        <div class="col-md-12 no-padding">
            <asp:Button ID="btn_PrintFinalDecision" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PrintFinalDecision %>" OnClick="btn_PrintFinalDecision_Click"
                Visible="false" OnClientClick="return setFormSubmitToFalse();" CssClass="btn moe-btn pull-right" />
        </div>

</div>

<div class="row no-padding margin-top-15">
	<h5 class="col-md-12 font-size-18 font-weight-600 success-msg">
		<asp:Label ID="lbl_SuccessMsg" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RequestClosed %>" Visible="false"></asp:Label>
	</h5>
</div>
<div class="row no-padding margin-top-20">
	<h5 class="col-md-12 font-size-18 font-weight-600 no-padding text-center">
		<asp:Label ID="lbl_NoResults" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NoResults %>" Visible="false"></asp:Label>
	</h5>
</div>

<script type="text/javascript">

    function setFormSubmitToFalse() {
        setTimeout(function () { _spFormOnSubmitCalled = false; }, 3000);
        return true;
    } 
</script>
