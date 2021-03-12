<%@ Assembly Name="ITWORX.MOEHEWF.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7b2931724f1d7d1c" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserVerification.ascx.cs" Inherits="ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.UserVerification" %>

 

<div class="col-md-12 col-xs-12">
    <div class="form-group">
        <label class="row">
            <asp:Label ID="lblEnterVerificationCode" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, EnterVerification%>"></asp:Label>
            <span class="error-msg">* </span>
        </label>

        <asp:TextBox ID="tbVerificationCode" runat="server" CssClass="form-control" type="number" min="0" inputmode="numeric" pattern="[0-9]*"></asp:TextBox>
        <asp:RequiredFieldValidator ID="reqVerificationCode" runat="server" ErrorMessage="RequiredFieldValidator" CssClass="error-msg moe-full-width" ControlToValidate="tbVerificationCode" Text="<%$Resources:ITWORX.MOEHEWF.Common, ReqVerificationCode %>" ValidationGroup="Submit"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="regVerificationCode" Display="dynamic" CssClass="error-msg moe-full-width" runat="server" ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, RegVerificationCode %>" ControlToValidate="tbVerificationCode" ValidationExpression="^\d{1,10}$" ValidationGroup="Submit"></asp:RegularExpressionValidator>

    </div>
</div>

<div class="col-md-12 col-xs-12">
    <div class="form-group">
        <label class="row font-size-14">
            <asp:Label ID="lblVerificationCode" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, VerificationCodeNotification%>"></asp:Label>
        </label>
    </div>
</div>
<div class="col-md-12 col-xs-12">
    <div class="form-group">
        <label class="row font-size-14">
            <asp:LinkButton ID="LinkButton1" CssClass="resend-btn" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, ResendVerificationCodeLink%>" OnClick="lBtnResendCode_Click"></asp:LinkButton>
        </label>
    </div>
</div>
<div ID="lblBlockingTime" ></div>


<%--<asp:RequiredFieldValidator ID="reqVerificationCode" runat="server" ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common,ReqVerificationCode%>"ForeColor="Red" ControlToValidate="tbVerificationCode"
    ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
 
<asp:Panel runat="server" ID="pnlValidation" Visible="false">
    <div class="row no-padding">
    <div class="col-md-12 col-xs-12 verification-msg">
        <h6>
            <asp:Label ID="lblVerificationStatus" runat="server"></asp:Label>
        </h6>
    </div>
    <div class="col-md-12 col-xs-12 verification-msg">
      
            <%--<asp:Label ID="lblBlockingTime" runat="server"></asp:Label>--%>
        
    </div>
</div>
</asp:Panel>
<script type="text/javascript">
    function StartBlockTime(duration) {

        var timer = duration, minutes, seconds;
        var display = document.getElementById('lblBlockingTime');
        
        var enablevc = document.getElementById('<%= tbVerificationCode.ClientID%>');
        var pnlValidationV = document.getElementById('<%= pnlValidation.ClientID%>');
       
        setInterval(function () {
            minutes = parseInt(timer / 60, 10)
            seconds = parseInt(timer % 60, 10);

            minutes = minutes < 10 ? "0" + minutes : minutes;
            seconds = seconds < 10 ? "0" + seconds : seconds;

            display.innerHTML = minutes + ":" + seconds;

            if (--timer < 0) {
                timer = 0;
                enablevc.disabled = false;
                pnlValidationV.style.display = 'none';
                display.style.display = 'none';
             
            }
        }, 1000);
    }

 
     </script>
     

