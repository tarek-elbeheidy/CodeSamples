<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PAViewStatusandRecommendation.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.PAViewStatusAndRecommendation" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="MOEHE" TagName="FileUpload" %>

<asp:HiddenField ID="hdn_ID" runat="server" />
<asp:HiddenField ID="hdn_RequestNumber" runat="server" />
<div class="row unheighlighted-section margin-bottom-50 flex-display flex-wrap display-mode margin-2">
    <div class="col-md-4 col-sm-6 margin-bottom-15">
        <div class="data-container">
            <h6 class="font-size-16 margin-bottom-15">
                <asp:Label ID="lblEarnedHours" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, EarnedHours %>" Visible="false"></asp:Label>
            </h6>
            <h5 class="font-size-20">
                <asp:Label ID="lblEarnedHoursV" runat="server"></asp:Label>
            </h5>
        </div>
    </div>
    <div class="col-md-4 col-sm-6 margin-bottom-15">
        <div class="data-container">
            <h6 class="font-size-16 margin-bottom-15">
                <asp:Label ID="lblOnlineHours" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, OnlineHours %>" Visible="false"></asp:Label>
            </h6>
            <h5 class="font-size-20">
                <asp:Label ID="lblOnlineHoursV" runat="server"></asp:Label>
            </h5>
        </div>
    </div>
    <div class="col-md-4 col-sm-6 margin-bottom-15">
        <div class="data-container">
            <h6 class="font-size-16 margin-bottom-15">
                <asp:Label ID="lblOnlineHoursPer" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, OnlineHoursPer %>" Visible="false"></asp:Label>
            </h6>
            <h5 class="font-size-20">
                <asp:Label ID="lblOnlineHoursPerV" runat="server"></asp:Label>
            </h5>
        </div>
    </div>

    <div class="col-md-4 col-sm-6 margin-bottom-15">
        <div class="data-container">
            <h6 class="font-size-16 margin-bottom-15">

                <asp:Label ID="lbl_Opinion" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, SearchStatus %>" Visible="false"></asp:Label>
            </h6>
            <h5 class="font-size-20">
                <asp:Label ID="lbl_EmpOpinion" runat="server"></asp:Label>
            </h5>
        </div>
    </div>

    <div class="col-md-4 col-sm-6 margin-bottom-15">
        <div class="data-container">
            <h6 class="font-size-16 margin-bottom-15">

                <asp:Label ID="lbl_EmpRecommend" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, EmpRecommendation %>" Visible="false"></asp:Label>
            </h6>
            <h5 class="font-size-20">
                <asp:Label ID="lbl_EmpRecommendation" runat="server"></asp:Label>
            </h5>
        </div>
    </div>
    <div class="col-md-12 col-sm-12 col-xs-12 margin-bottom-15">
        <div class="data-container">
            <h6 class="font-size-16 margin-bottom-15">

                <asp:Label ID="lbl_Decisiontxt" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, EmpOpinion %>" Visible="false"></asp:Label>
            </h6>
            <h5 class="font-size-20">
                <asp:Label ID="lbl_DecisiontxtForPrintingVal" runat="server"></asp:Label>
            </h5>
        </div>
    </div>
    <div id="attachmentsDiv" class="col-md-12 no-padding" runat="server" visible="false">
        <MOEHE:FileUpload runat="server" id="ViewStatusRecommendAttachements" />
    </div>
    
        <div class="col-md-12 no-padding">
            <asp:Button ID="btn_ReviewDecision" Visible="false" runat="server" OnClick="ReviewDecisionClick" Text="<%$Resources:ITWORX_MOEHEWF_PA, review %>" OnClientClick="return setFormSubmitToFalse();" CssClass="pull-right" />
        </div>
    
</div>

<div class="row no-padding">
    <h4 class="font-size-18 font-weight-600 text-center">
        <asp:Label ID="lbl_NoResults" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, NoResults %>" Visible="false"></asp:Label>
    </h4>
</div>


<script type="text/javascript">
    $(document).ready(function () {
        function setFormSubmitToFalse() {
            setTimeout(function () { _spFormOnSubmitCalled = false; }, 3000);
            return true;
        }
    });
    </script>