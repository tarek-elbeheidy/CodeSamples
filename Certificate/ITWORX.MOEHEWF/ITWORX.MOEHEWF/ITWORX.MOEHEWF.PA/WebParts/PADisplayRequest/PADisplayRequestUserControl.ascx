<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%--<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.PA/PADisplayApplicantDetails.ascx" TagPrefix="uc1" TagName="DisplayApplicantDetails" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.PA/PADisplayCheckUniversity.ascx" TagPrefix="uc1" TagName="DisplayCheckUniversity" %>--%>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.PA/PADisplayRequestDetails.ascx" TagPrefix="uc1" TagName="PADisplayPARequestDetails" %>
<%--<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/TermsAndConditions.ascx" TagPrefix="uc1" TagName="TermsAndConditions" %>--%>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.PA/PARolesNavigationLinks.ascx" TagPrefix="uc1" TagName="PARolesNavigationLinks" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PADisplayRequestUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.WebParts.PADisplayRequest.PADisplayRequestUserControl" %>


<div class="row section-container side-nav-container">
    <div class="col-md-3 col-xs-4 no-padding"> <!-- required for floating -->
        <!-- Nav tabs -->
        <%--<ul class="nav nav-tabs tabs-left">
          <li class="active"><a href="#home" data-toggle="tab">Home</a></li>
          <li><a href="#profile" data-toggle="tab">Profile</a></li>
          <li><a href="#messages" data-toggle="tab">Messages</a></li>
          <li><a href="#settings" data-toggle="tab">Settings</a></li>
        </ul>--%>
        <uc1:PARolesNavigationLinks runat="server" id="PARolesNavigationLinks" />
    </div>
 
<div class="col-md-9 col-xs-12 no-padding test">
        <!-- Tab panes -->
        <%--<div class="tab-content">
          <div class="tab-pane active" id="home">Home Tab.</div>
          <div class="tab-pane" id="profile">Profile Tab.</div>
          <div class="tab-pane" id="messages">Messages Tab.</div>
          <div class="tab-pane" id="settings">Settings Tab.</div>
        </div>--%>
        <uc1:PADisplayPARequestDetails runat="server" id="PADisplayPARequestDetails" />
    </div>
</div>



 
 

<%--<script type="text/javascript" src='<%= ResolveUrl ("~/Style%20Library/Scripts/jquery.min.js") %>'></script>

<style type="text/css">
    
#wizHeader li .prevStep
{
    background-color: #669966;
}
#wizHeader li .prevStep:after
{
    border-left-color:#669966 !important;
}
#wizHeader li .currentStep
{
    background-color: #C36615;
}
#wizHeader li .currentStep:after
{
    border-left-color: #C36615 !important;
}
#wizHeader li .nextStep
{
    background-color:#C2C2C2;
}
#wizHeader li .nextStep:after
{
    border-left-color:#C2C2C2 !important;
}
#wizHeader
{
    list-style: none;
    overflow: hidden;
    font: 18px Helvetica, Arial, Sans-Serif;
    margin: 0px;
    padding: 0px;
}
#wizHeader li
{
    float: left;
}
#wizHeader li a
{
    color: white;
    text-decoration: none;
    padding: 10px 0 10px 55px;
    background: brown; /* fallback color */
    background: hsla(34,85%,35%,1);
    position: relative;
    display: block;
    float: left;
}
#wizHeader li a:after
{
    content: " ";
    display: block;
    width: 0;
    height: 0;
    border-top: 50px solid transparent; /* Go big on the size, and let overflow hide */
    border-bottom: 50px solid transparent;
    border-left: 30px solid hsla(34,85%,35%,1);
    position: absolute;
    top: 50%;
    margin-top: -50px;
    left: 100%;
    z-index: 2;
}
#wizHeader li a:before
{
    content: " ";
    display: block;
    width: 0;
    height: 0;
    border-top: 50px solid transparent;
    border-bottom: 50px solid transparent;
    border-left: 30px solid white;
    position: absolute;
    top: 50%;
    margin-top: -50px;
    margin-left: 1px;
    left: 100%;
    z-index: 1;
}        
#wizHeader li:first-child a
{
    padding-left: 10px;
}
#wizHeader li:last-child 
{
    padding-right: 50px;
}
#wizHeader li a:hover
{
    background: #FE9400;
}
#wizHeader li a:hover:after
{
    border-left-color: #FE9400 !important;
}
    .content {
        height: 150px;
        padding-top: 75px;
        text-align: center;
        background-color: #F9F9F9;
        font-size: 48px;
    }
</style>


<asp:Wizard ID="wizardPADisplayRequest" runat="server" DisplaySideBar="false" OnPreRender="wizardPADisplayRequest_PreRender" >
     <HeaderTemplate>
               <ul id="wizHeader">
                   <asp:Repeater ID="SideBarList" runat="server">
                       <ItemTemplate>
                           <li><a class="<%# GetClassForWizardStep(Container.DataItem) %>" title="<%#Eval("Name")%>">
                               <%# Eval("Name")%></a> </li>
                       </ItemTemplate>
                   </asp:Repeater>
               </ul>
           </HeaderTemplate>
    <WizardSteps>
        <asp:WizardStep ID="wizardStepCheckUniversity" runat="server" Title="<%$Resources:ITWORX_MOEHEWF_PA, CheckUniversity  %>" >
            <asp:Label runat="server"> Step1</asp:Label>
            <br />
             <uc1:DisplayCheckUniversity runat="server" id="DisplayCheckUniversity" />
        </asp:WizardStep>
        <asp:WizardStep ID="wizardStepConditions" runat="server" Title="<%$Resources:ITWORX_MOEHEWF_PA, TermsAndConditions  %>">
              <asp:Label runat="server"> Step2</asp:Label>
            <br />
            <uc1:TermsAndConditions runat="server" id="TermsAndConditions" />
        </asp:WizardStep>
           <asp:WizardStep ID="wizardStepApplicantDetails" runat="server" Title="<%$Resources:ITWORX_MOEHEWF_PA, ApplicantDetails  %>">
              <asp:Label runat="server"> Step3</asp:Label>
               <br />
               <uc1:DisplayApplicantDetails runat="server" id="DisplayApplicantDetails" />
               </asp:WizardStep>
               <asp:WizardStep ID="wizardStepPARequestDetails" runat="server" Title="<%$Resources:ITWORX_MOEHEWF_PA, PARequestDetails  %>">
              <asp:Label runat="server"> Step4</asp:Label>
               
                   <br />
                   
        </asp:WizardStep>
    </WizardSteps>
      
     <StartNavigationTemplate >
      <asp:Button ID="StartNextButton" runat="server"  CommandName="MoveNext" Text="<%$Resources:ITWORX_MOEHEWF_PA, Next %>"     />
   </StartNavigationTemplate>
    <StepNavigationTemplate>
      <asp:Button ID="StepPreviousButton" runat="server" CommandName="MovePrevious" Text="<%$Resources:ITWORX_MOEHEWF_PA, Previous %>" />
      <asp:Button ID="StepNextButton" runat="server"  CommandName="MoveNext" Text="<%$Resources:ITWORX_MOEHEWF_PA, Next %>"  />
   </StepNavigationTemplate>
 <FinishNavigationTemplate>
 <asp:Button ID="FinishPreviousButton" runat="server"  CommandName="MovePrevious" Text="<%$Resources:ITWORX_MOEHEWF_PA, Previous %>" />
 <asp:Button ID="FinishButton" runat="server" CommandName="MoveComplete"   Text="<%$Resources:ITWORX_MOEHEWF_PA, Submit %>" />
 </FinishNavigationTemplate>

</asp:Wizard>--%>
<%--<asp:Label ID="lblNoRequest" runat="server" Font-Bold="true"  Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_PA, YouHaveNoRequests %>"></asp:Label>--%>
