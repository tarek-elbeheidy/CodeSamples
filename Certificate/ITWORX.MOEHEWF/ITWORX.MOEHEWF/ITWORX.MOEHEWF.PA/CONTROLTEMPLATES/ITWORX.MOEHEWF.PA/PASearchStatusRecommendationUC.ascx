<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="PAUploadUC.ascx" TagPrefix="uc1" TagName="UploadUC" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="MOEHE" TagName="FileUpload" %>


<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PASearchStatusRecommendationUC.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.PASearchStatusRecommendationUC" %>

 
<asp:HiddenField ID="hdn_RequestNumber" runat="server" />

<div class="row unheighlighted-section margin-bottom-50 flex-display flex-wrap">

    <asp:Panel ID="pnl_EmpRecommendation" runat="server" GroupingText="<%$Resources:ITWORX_MOEHEWF_PA, EmpRecommendation %>" CssClass="stateTitle moe-full-width">
        <asp:HiddenField ID="hdn_ID" runat="server" />
        <div class="row margin-bottom-25">
            
       
        <div class="col-md-8 col-sm-6 col-xs-12 margin-top-15">
                <h5 class="font-size-18 font-weight-600 margin-2 underline">
                    <asp:Label ID="lbl_NotificationMsg" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, NotifyMsgProcRecommend %>"></asp:Label>
                </h5>
         </div>
            <div class="col-md-4 col-sm-6 col-xs-12">
                <asp:Button ID="btn_ApproveProcedures" runat="server" CssClass="pull-right" OnClick="btn_ApproveProcedures_Click" ValidationGroup="Save" Text="<%$Resources:ITWORX_MOEHEWF_PA, ApproveProcRecommend %>" />
            </div>

             </div>

        <div class="row">
            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12 no-padding ">
            <div class="data-container table-display moe-full-width  moe-sm-full-width">
                <div class="form-group">
                    <h6 class="font-size-16 margin-bottom-0 margin-top-0 ">
                        <asp:Label ID="lblEarnedHours" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, EarnedHours%>"></asp:Label>
                    </h6>
                    <div class="form">
                        <asp:TextBox ID="txtEarnedHours" runat="server" TextMode="Number" CssClass="moe-full-width input-height-42 border-box moe-input-padding"></asp:TextBox>
          
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-4 col-md-4  col-sm-12 col-xs-12 no-padding ">
            <div class="data-container table-display moe-full-width  moe-sm-full-width">
                <div class="form-group">
                    <h6 class="font-size-16 margin-bottom-0 margin-top-0 ">
                        <asp:Label ID="lblOnlineHours" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, OnlineHours%>"></asp:Label>
                    </h6>
                    <div class="form">
                        <asp:TextBox ID="txtOnlineHours" runat="server" TextMode="Number" CssClass="moe-full-width input-height-42 border-box moe-input-padding"></asp:TextBox>
                         
                    </div>
                </div>
            </div>
        </div>
         <div class="col-lg-4 col-md-4  col-sm-12 col-xs-12 no-padding ">
            <div class="data-container table-display moe-full-width  moe-sm-full-width">
                <div class="form-group">
                    <h6 class="font-size-16 margin-bottom-0 margin-top-0 ">
                        <asp:Label ID="lblOnlineHoursPer" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, OnlineHoursPer%>"></asp:Label>
                    </h6>
                    <div class="form">
                        <asp:TextBox ID="txtOnlineHoursPer" runat="server" TextMode="Number" CssClass="moe-full-width input-height-42 border-box moe-input-padding"></asp:TextBox>
                 
                    </div>
                </div>
            </div>
        </div>
        </div>

        <div class="row">

      
         
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
            <div class="data-container table-display moe-full-width  moe-sm-full-width">
                <div class="form-group">
                    <h6 class="font-size-16 margin-bottom-0 margin-top-0 ">
                        <asp:Label ID="lbl_AddEmpOpinion" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, SearchStatus%>"></asp:Label>
                        <span class="astrik error-msg">*</span>                    
                    </h6>
                    <div class="form">
                        <asp:TextBox ID="txt_EmpOpinion" runat="server" TextMode="MultiLine" CssClass="moe-full-width input-height-42 border-box moe-input-padding text-area"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqVal_EmpOpinion" runat="server" ForeColor="Red" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RequiredEmpOpinion %>"
                            ValidationGroup="Save" ControlToValidate="txt_EmpOpinion"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
            <div class="data-container table-display moe-full-width moe-sm-full-width">
                <div class="form-group">
                    <h6 class="font-size-16 margin-bottom-0 margin-top-0 ">
                        <asp:Label ID="lbl_DecisiontxtForPrinting" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, EmployeeOpinion %>"></asp:Label>
                        <span class="astrik error-msg">*</span>                    
                    </h6>
                    <div class="form">
                        <asp:TextBox ID="txt_DecisiontxtForPrinting" runat="server" TextMode="MultiLine" CssClass="moe-full-width input-height-42 border-box moe-input-padding text-area"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="req_DecisiontxtForPrinting" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredDecisiontxtForPrinting %>"
                            ValidationGroup="Save" ControlToValidate="txt_DecisiontxtForPrinting" ForeColor="red"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
            <div class="data-container table-display moe-width-85  moe-sm-full-width">
                <div class="form-group">
                    <asp:RadioButtonList ID="rdbtn_EmpRecommendation" runat="server">
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator ID="reqVal_EmpRecommendation" runat="server" ForeColor="Red" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_PA, RequiredEmpRecommendation %>"
                        ValidationGroup="Save" ControlToValidate="rdbtn_EmpRecommendation"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <div class="col-md-8 no-padding">
            <MOEHE:FileUpload runat="server" id="StatusRecommendAttachements" />
        </div>
      <div class="row margin-top-15">
                <div class="col-md-12 no-padding">
                    <asp:Button ID="btn_ReviewDecision" runat="server"  OnClick="ReviewDecisionClick" Text="<%$Resources:ITWORX_MOEHEWF_PA, review %>" OnClientClick="return setFormSubmitToFalse();"  CssClass="pull-right"  />
                           <asp:Button ID="btn_Save" runat="server" ValidationGroup="Save" OnClick="btn_Save_Click" Text="<%$Resources:ITWORX_MOEHEWF_PA, Save %>" CssClass="pull-left" />

                    </div>
            </div>

          </div>
        <asp:Button ID="btn_ViewStatus" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, ViewStatus %>" Style="display: none" />
    </asp:Panel>
</div>


 <div class="row margin-top-15">
            <h5 class="col-md-12 font-size-18 font-weight-600 success-msg">
                <asp:Label ID="lbl_SaveSuccess" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, SavedSuccessfully %>" Visible="false" ForeColor="Green"></asp:Label>
            </h5>
        </div>

<div class="row no-padding margin-top-15">
    <h5 class="col-md-12 font-size-18 font-weight-600 success-msg">
        <asp:Label ID="lbl_Success" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, SuccessMsg %>" Visible="false" ForeColor="Green"></asp:Label>
    </h5>
</div>


<div id="viewControls" runat="server" visible="false">
       <div class="row">
            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12 no-padding ">
			<div class="data-container">
				<h6 class="font-size-16 margin-bottom-15">
					<asp:Label ID="lblEarnedHoursK" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, EarnedHours %>"></asp:Label>
				</h6>
				<h5 class="font-size-22">
					<asp:Label ID="lblEarnedHoursV" runat="server" Text="N/A"></asp:Label>
				</h5>
			</div>
		</div>
       <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12 no-padding ">
			<div class="data-container">
				<h6 class="font-size-16 margin-bottom-15">
					<asp:Label ID="lblOnlineHoursK" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, OnlineHours %>"></asp:Label>
				</h6>
				<h5 class="font-size-22">
					<asp:Label ID="lblOnlineHoursV" runat="server" Text="N/A"></asp:Label>
				</h5>
			</div>
		</div>
       <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12 no-padding ">
			<div class="data-container">
				<h6 class="font-size-16 margin-bottom-15">
					<asp:Label ID="lblOnlineHoursPerK" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, OnlineHoursPer %>"></asp:Label>
				</h6>
				<h5 class="font-size-22">
					<asp:Label ID="lblOnlineHoursPerV" runat="server" Text="N/A"></asp:Label>
				</h5>
			</div>
		</div> 
       </div>
      <div class="row">
           <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
			<div class="data-container">
				<h6 class="font-size-16 margin-bottom-15">
					<asp:Label ID="lblSearchStatus" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, EmployeeOpinion %>"></asp:Label>
				</h6>
				<h5 class="font-size-22">
					<asp:Label ID="lblSearchStatusVal" runat="server" Text="N/A"></asp:Label>
				</h5>
			</div>
		</div>
		<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
			<div class="data-container">
				<h6 class="font-size-16 margin-bottom-15">
					<asp:Label ID="lbl_EmpOpinion" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, SearchStatus %>"></asp:Label>
				</h6>
				<h5 class="font-size-22">
					<asp:Label ID="lbl_EmpOpinionVal" runat="server" Text="N/A"></asp:Label>
				</h5>
			</div>
		</div>
		<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
			<div class="data-container">
				<h6 class="font-size-16 margin-bottom-15">
					<asp:Label ID="lbl_Recommendation" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_PA, EmpRecommendation %>"></asp:Label>
				</h6>
				<h5 class="font-size-22">
					<asp:Label ID="lbl_RecommendationVal" runat="server" Text="N/A"></asp:Label>
				</h5>
			</div>
		</div>
    <div class="col-md-8">
        <MOEHE:FileUpload runat="server" id="ViewStatusRecommendAttachements" /> 
    </div>
      </div>
 
</div>

<script type="text/javascript">
    $(document).ready(function () {
        function setFormSubmitToFalse() {
            setTimeout(function () { _spFormOnSubmitCalled = false; }, 3000);
            return true;
        }
    });
    </script>