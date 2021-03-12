
<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PMDecisionSCEUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.WebParts.PMDecisionSCE.PMDecisionSCEUserControl" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/DDLWithTXTWithNoPostback.ascx" TagPrefix="uc1" TagName="DDLWithTXTWithNoPostback" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.0.30930.28736, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<style>
    .DisplayNone {
         display:none;
    }
    .DisplayBlock {
        display:block;
    }
</style>

<div  role="tabpanel" class="tab-pane  tab-padd">
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <h2 class="tab-title"> 
                                                <asp:Label ID="lblFinalDdecionTitle" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SCEFinalDecision %> "></asp:Label> </h2>
                                        </div>
                                    </div>
                                     <!--Senario-->
                                     <div id="finalconfirm"  runat="server" class="DisplayNone">
                                     <div id="headManagerDecision" class="panel-group" runat="server">
                                    <div class="panel panel-default">
                                            <div class="panel-heading active">
                                                <h4 class="panel-title">
                                                    <a data-toggle="collapse" data-parent="#accordion" href="#test">
                                                        <span>
                                                        <asp:Label ID="Label1" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SCEDDMDecision %> "></asp:Label> 


                                                        </span>
                                                        <em>
                                                        </em>
                                                    </a>
                                                </h4>
                                            </div>
                                            <!-- /.panel-heading -->
                                            <div id="dvNoDecisions" class="panel-collapse collapse in" runat="server">
                                                <div class="panel-body">
                                         
                                                   <div class="tab-padd">
                                    	    
                                    <div class="row margin-top-10">
                                    	<div class="col-xs-12">
                                    		<h4 class="text-center">

                                                
                                                 <asp:Label ID="Label2" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SCENoDecisionTaken %> "></asp:Label>
                                               </h4>
                                    	</div>
                                    </div>
                                    </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                
                                   
                                    
                                    
                                </div>
                                
                                <div class="panel-group">
                                    <div class="panel panel-default">
                                            <div class="panel-heading active">
                                                <h4 class="panel-title">
                                                    <a data-toggle="collapse" data-parent="#accordion" href="#headDecision">
                                                        <span>  <asp:Label ID="Label3" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SCESMDecision %> "></asp:Label>
                                                                                                                </span>
                                                        <em>
                                                        </em>
                                                    </a>
                                                </h4>
                                            </div>
                                            <!-- /.panel-heading -->
                                            <div id="headDecision" class="panel-collapse collapse in">
                                                <div class="panel-body ">
                                                <div class="tab-padd ">
                                                
                                               
                                             			<div class="row">
                                        <div class="col-md-4 col-sm-6">
                                        	<div class="form-group">
                                        		<label><asp:Label ID="Label4" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SCEFinalDecisionSubTitle %> "></asp:Label></label>
                                                
                                                <asp:DropDownList ID="drpFianlDecisonRecommendation" runat="server" CssClass="form-group moe-dropdown" AutoPostBack="true" OnSelectedIndexChanged="drpFianlDecisonRecommendation_SelectedIndexChanged"></asp:DropDownList>
                                                 <asp:RequiredFieldValidator ID="reqFinalDecisioRecomm" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, FinaDecisionRequired %>" CssClass="error-msg" ValidationGroup="Submit" ControlToValidate="drpFianlDecisonRecommendation" ></asp:RequiredFieldValidator>    
                                        		
                                        	</div>
                                        </div>
                                        
                                        <div class="col-md-4 col-sm-6 col-md-offset-1 programManagerHide">
                                        	<div class="form-group" runat="server" id="divFinalDecisionRejectionReasons" visible="false" >
                                        		<%--<asp:Label ID="Label5" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SCERejectionReason %> "></asp:Label>--%>
                                        		
                                                <uc1:DDLWithTXTWithNoPostback runat="server" id="drpFinalDecisionRejectReason" />


                                        	</div>
                                        </div>
                                    </div>
                                    
                                    <div class="row margin-top-10">
                                    	<div class="col-xs-12">
                                    		<div class="form-group">
                                    			<label><asp:Label ID="lblReturnCommentsTitle" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ReturnComments %> "></asp:Label></label><span id="FinalDecisionCommentsRequired" runat="server" class="error-msg" visible="false">*</span>
                                                <asp:TextBox ID="txtFinalDecisionComments" runat="server" TextMode="MultiLine" CssClass="form-control text-area" ></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rqrFinalDecisonCommnets" runat="server" CssClass="error-msg" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE,ReturnValidation %>"  ValidationGroup="Submit" ControlToValidate="txtFinalDecisionComments" Enabled="false"></asp:RequiredFieldValidator>
                                    		</div>
                                    	</div>
                                    </div>
                                    <div class="row margin-top-25 programManagerHide">
                                    	<div class="col-xs-12">
                                    		<div class="form-group">
                                    			<label><asp:Label ID="Label6" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SCESendSMSNote %> "></asp:Label></label>
                                                <asp:Button ID="btnSendSMSFinalConfirm" runat="server" CssClass="btn school-btn" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCESendSMSBtn %>" OnClick="btnSendSMSFinalConfirm_Click" />
                                    		</div>
                                    	</div>
               </div>
                                    	<%--<div class="row margin-top-10">
                                    	<div class="col-xs-12 warningMsg headManagerHide">
                                    		<h5 class="font-size-14 margin-bottom-0 margin-top-0 instruction-details color-black font-family-sans">
                                    		<span>
                                    		<asp:Label ID="Label7" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SCEFinalConfirmNote %> "></asp:Label>

                                    		</span></h5>
                                    	</div>
                                    </div>--%>
                                    
                                    <div class="row margin-top-15">
                                    	<div class="col-xs-12 text-right">
                                    	
                                            <asp:Button ID="btnSaveFinalDecision" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SCESaveBtn %>"  CssClass="school-btn btn"  ValidationGroup="Submit"  OnClick="btnSaveFinalDecision_Click" />

                                    	
                                            <asp:Button ID="btnViewFinalDecision" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SCEViewBtn %>"  CssClass="school-btn btn"  OnClick="btnViewFinalDecision_Click" OnClientClick="LnkDownload(this.id)" />

                                    		
                                              <asp:Button ID="btnSubmitFinalDecision" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SCESubmitBtn %>" CssClass="school-btn btn" ValidationGroup="Submit"  OnClick="btnSubmitFinalDecision_Click" />
                                    	</div>
                                    </div>

                                    </div>
                                    
                                                   
                                                        </div>
                                                    </div>
                                                </div>
                                    
                                    
                                   
                                    
                                   
                                    
                                    
                                     
                                     
                                    </div>
                                <!--Senario-->
                                </div>
                                <!--anotherSenario-->
                                <div id="finalReject" runat="server" class="DisplayNone">
                                	<div id="headManagerDecisionreject" class="panel-group">
                                    <div class="panel panel-default">
                                            <div class="panel-heading active">
                                                <h4 class="panel-title">
                                                    <a data-toggle="collapse" data-parent="#accordion" href="#test1">
                                                        <span><asp:Label ID="Label8" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SCEFinalDecision %> "></asp:Label>
                                                        
                                                        </span>
                                                        <em>
                                                        </em>
                                                    </a>
                                                </h4>
                                            </div>
                                            <!-- /.panel-heading -->
                                            <div id="test1" class="panel-collapse collapse in">
                                                <div class="panel-body">
                                         
                                                   <div class="tab-padd">
                                    	<div class="row margin-top-10">
                                        <div class="col-md-4 col-sm-6">
                                        	<div class="form-group">
                                        		<label>  <asp:Label ID="Label9" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SCEDDMDecision %> "></asp:Label> </label>

                                                <asp:DropDownList ID="drpFinalRejectRecommendReadOnly" runat="server" CssClass="form-group moe-dropdown" Enabled="false"></asp:DropDownList>
                                        
                                        	</div>
                                        </div>
                                        
                                        <div class="col-md-4 col-sm-6 col-md-offset-1 programManagerHide"  id="dvFinalRejectReasonReadOnly" runat="server" visible="false">
                                        	<div class="form-group">
                                        		<label><asp:Label ID="Label10" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SCERejectionReason %> "></asp:Label></label>
                                        	 <uc1:DDLWithTXTWithNoPostback runat="server" id="drpFinalRejectDecisonReasonssReadOnly" />
                                        	</div>
                                        </div>
                                    </div>    
                                    <div class="row margin-top-10">
                                    	<div class="col-xs-12">
                                    		<div class="form-group">
                                    			<label><asp:Label ID="Label11" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ReturnComments %> "></asp:Label></label>
                                    			  <asp:TextBox ID="txtFinalRejectCommentsReadOnly" runat="server" CssClass="form-control text-area" TextMode="MultiLine" Enabled="false" ></asp:TextBox>
                                    		</div>
                                    	</div>
                                    </div>
                                    </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                
                                   
                                    
                                    
                                </div>
                                
                                
                                
                             
                                


<!--End other-->
                                    
                                    
                                    <div id="reject" class="panel-group">
                                    <div class="panel panel-default">
                                            <div class="panel-heading active">
                                                <h4 class="panel-title">
                                                    <a data-toggle="collapse" data-parent="#accordion" href="#headDecision1">
                                                        <span><asp:Label ID="Label12" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SCESMDecision %> "></asp:Label>
                                                                                                                </span>
                                                        <em>
                                                        </em>
                                                    </a>
                                                </h4>
                                            </div>
                                            <!-- /.panel-heading -->
                                            <div id="headDecision1" class="panel-collapse collapse in">
                                                <div class="panel-body ">
                                                <div class="tab-padd ">
                                                
                                     <%--<div class="row margin-top-25 programManagerHide">
                                    	<div class="col-xs-12">
                                    		<div class="form-group">
                                    			<label><asp:Label ID="Label13" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SCESendSMSNote %> "></asp:Label></label>
                                                <asp:Button ID="btnSendSMSFinalReject" runat="server" CssClass="btn school-btn" Text="<%$Resources:ITWORX_MOEHEWF_SCE,SCESendSMSBtn %>" OnClick="btnSendSMSFinalReject_Click" />
                                    		</div>
                                    	</div>
                                        </div>--%>
                                    	<%--<div class="row margin-top-10">
                                    	<div class="col-xs-12 warningMsg headManagerHide">
                                    		<h5 class="font-size-14 margin-bottom-0 margin-top-0 instruction-details color-black font-family-sans">
                                    		<span>
                                    		<asp:Label ID="Label14" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SCEFinalConfirmNote %> "></asp:Label> </span></h5>
                                    	</div>
                                    </div>--%>
                                    
                                    <div class="row margin-top-15">
                                    	<div class="col-xs-12 text-right">
 
                                    	
                                            <asp:Button ID="btnFinalRejectView" runat="server" CssClass="school-btn btn" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SCEViewBtn %>" OnClick="btnFinalRejectView_Click" OnClientClick="LnkDownload(this.id)" />
                                             <asp:Button ID="btnFinalRejectSubmit" runat="server" CssClass="school-btn btn" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SCESubmitBtn %>" OnClick="btnFinalRejectSubmit_Click" />
                                    		
                                    	</div>
                                    </div>

                                    </div>
                                    
                                                   
                                                        </div>
                                                    </div>
                                                </div>
                                    
                                    
                                   
                                    
                                   
                                    
                                    
                                     
                                     
                                    </div>
                                       </div>
                                     <!--Senario-->
                                     
                                     <div id="managerDecision" class="panel-group DisplayNone"  runat="server">
                                    
                                    <div class="tab-padd">
                                    	<div class="row margin-top-10">
                                        <div class="col-md-4 col-sm-6">
                                        	<div class="form-group">
                                        		<label><asp:Label ID="Label15" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SCEFinalDecisionSubTitle %> "></asp:Label></label><span  class="error-msg">*</span>
                                        		  <asp:DropDownList ID="drpHMDecisionRecommend" runat="server" CssClass="form-group moe-dropdown" OnSelectedIndexChanged="drpHMDecisionRecommend_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    
                                            <asp:RequiredFieldValidator ID="requiredDecisionRecommend" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, FinaDecisionRequired %>" CssClass="error-msg" ValidationGroup="Submit" ControlToValidate="drpHMDecisionRecommend"></asp:RequiredFieldValidator>    
                                            </div>
                                        </div>
                                                                  
                                        <div class="col-md-4 col-sm-6 col-md-offset-1" id="dvFinalDecisionRejection" runat="server" visible="false">
                                        	<div class="form-group"> 
                                        		<uc1:DDLWithTXTWithNoPostback runat="server" id="drpManagerDecision" />
                                        	</div>
                                        </div>
                                        
                                    </div>    
                                    <div class="row margin-top-10">
                                    	<div class="col-xs-12">
                                    		<div class="form-group">
                                    			<label><asp:Label ID="Label17" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ReturnComments %>"></asp:Label></label> <span id="dvFinalDecisionSpan" runat="server" visible="false" class="error-msg">*</span>
                                    			 <asp:TextBox ID="txtManagerDecisionComments" runat="server" CssClass="form-control text-area" TextMode="MultiLine" ></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rqrtxtManagerDecisionComments" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE, RequiredNotes %>" CssClass="error-msg" ControlToValidate="txtManagerDecisionComments" Enabled="False" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    		</div>
                                    	</div>
                                    </div>
                                    
                                    <div class="row margin-top-15">
                                    	<div class="col-xs-12 text-right">
                                    	
                                            <asp:Button ID="btnManagerDecisionSave" runat="server"   CssClass="school-btn btn" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SCESaveBtn %>" OnClick="btnManagerDecisionSave_Click" ValidationGroup="Submit" />
                                    	
                                            <asp:Button ID="btnManagerDecisionView" runat="server"  CssClass="school-btn btn" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SCEViewBtn %>"  OnClick="btnManagerDecisionView_Click" OnClientClick="LnkDownload(this.id)"/>

                                             <asp:Button ID="btnManagerDecisionSubmit" runat="server"  CssClass="school-btn btn" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SCESubmitBtn %>" OnClick="btnManagerDecisionSubmit_Click" ValidationGroup="Submit"  />

                                    	</div>
                                    </div>
                                    
                                    </div>
                                     
                                     
                                    </div>






      <!--finalDecision-->
                    			<div class="DisplayNone" id="readOnLyView" role="tabpanel" runat="server">
                                    <div class="row">
                                        <div class="col-xs-12">
                                           
                                        </div>
                                    </div>
                                    <div class="row margin-top-25">
                                        <div class="col-md-4 col-sm-6">
                                        	<div class="form-group">
                                        		<label><asp:Label ID="Label18" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SCEFinalDecisionSubTitle %> "></asp:Label></label>
                                                
                                                <asp:DropDownList ID="drpRecommendationViewOnly" runat="server" CssClass="form-group moe-dropdown" Enabled="false" ></asp:DropDownList>

                                        	</div>
                                        </div>
                                        
                                        <div class="col-md-4 col-sm-6 col-md-offset-1 programManagerHide" id="dvviewOnlyRejectionReason" runat="server" visible="false">
                                        	<div class="form-group">
                                        			<label><asp:Label ID="Label19" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SCERejectionReason %> "></asp:Label></label>
                                        		
                                                <uc1:DDLWithTXTWithNoPostback runat="server" id="drpViewOnlyRejectionReason" />
                                        	</div>
                                        </div>
                                    </div>
                                    
                                    <div class="row margin-top-10">
                                    	<div class="col-xs-12">
                                    		<div class="form-group">
                                    			<label><asp:Label ID="Label20" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ReturnComments %> "></asp:Label></label>
                                                <asp:TextBox ID="txtCommentsViewOnly" runat="server" TextMode="MultiLine" CssClass="form-control text-area" Enabled="false" ></asp:TextBox>
                                    		</div>
                                    	</div>
                                    </div>
                                    
                                    <div class="row margin-top-10">
                                    	<div class="col-md-12 text-right">
                                    		
                                            <asp:Button ID="btnDownload" runat="server" CssClass="btn school-btn" Text="<%$Resources:ITWORX_MOEHEWF_SCE, downloadDecision %> " OnClick="btnDownload_Click" />
                                    	</div>
                                    </div>
                                    
                                    
                                    
                                </div>
                                
                                <!--END finalDecision-->



                                </div>
                                <!--END finalDecision-->
<!-- save popup -->
<cc1:ModalPopupExtender ID="modalSavePopup" runat="server"
    TargetControlID="btnSaveHdn"
    PopupControlID="pnlSaveConfirmation" BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlSaveConfirmation" runat="server" Style="display: none;" CssClass="modalPopup">
    <asp:Label ID="lblSaveSuccess" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>
    <br />
    <br />
    <asp:Button ID="btnSaveOk" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Ok %>" OnClick="btnSaveOk_Click" />
</asp:Panel>
<asp:Button ID="btnSaveHdn" runat="server" Text="Button" Style="display: none;" />


<!-- submit popup -->
<cc1:ModalPopupExtender ID="modalPopUpConfirmation" runat="server"
    TargetControlID="btnHdn"
    PopupControlID="pnlConfirmation" BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlConfirmation" runat="server" Style="display: none;" CssClass="modalPopup">
    <asp:Label ID="lblSuccess" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>
    <br />
    <br />
    <asp:Button ID="btnModalOK" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Ok %>" OnClick="btnModalOK_Click" />
</asp:Panel>
<asp:Button ID="btnHdn" runat="server" Text="Button" Style="display: none;" />


<!-- Confirm Decision -->

<cc1:ModalPopupExtender ID="ModalPopupConfirmDecision" runat="server"
    TargetControlID="btnConfirmDecisionHdn"
    PopupControlID="PanelConfirmDecision" BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
<asp:Panel ID="PanelConfirmDecision" runat="server" Style="display: none;" CssClass="modalPopup">
    <asp:Label ID="lbl_ConfirmDecision" runat="server" ForeColor="Green" Font-Bold="true" ></asp:Label>
    <br />
    <br />
    <asp:Button ID="btnConfirmDecisionOK" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Ok %>" OnClick="btnConfirmDecisionOK_Click" />
    <asp:Button ID="btnConfirmDecisionCancel" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Cancel %>" OnClick="btnConfirmDecisionCancel_Click" />
</asp:Panel>
<asp:Button ID="btnConfirmDecisionHdn" runat="server" Text="Button" Style="display: none;" />

<input type="hidden" value="" id="__EventTriggerRecommendControl" name="__EventTriggerRecommendControl" /> 

<script>
    function LnkDownload(eventControl) {
        //  debugger;
        var ctlId = document.getElementById("__EventTriggerSearchControl");
        if (ctlId) {
            ctlId.value = eventControl;
        }

        window.WebForm_OnSubmit = function () { return true; };
    }
</script>