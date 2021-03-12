<%@ Assembly Name="ITWORX.MOEHEWF.UCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=883afb4c05a35fe5" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="UploadUC.ascx" TagPrefix="uc1" TagName="UploadUC" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="MOEHE" TagName="FileUpload" %>



<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchStatusRecommendationUC.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.SearchStatusRecommendationUC" %>

<script>
    function changeDecisionTemp() {
        if ($("#<%=rdbtn_EmpRecommendation.ClientID%> input:checked").val() == "1") {
            $("#lbl_Approve").css("display", "none");
            $("#lbl_Reject").css("display", "block");
        }
        else {
            $("#lbl_Approve").css("display", "block");
            $("#lbl_Reject").css("display", "none");
        }
    }

    $(function () {

        $('#txt_bookDate').attr('readonly', true); 
        $('#txt_bookDate').datepicker({
            dateFormat: "dd/mm/yy", 
            changeYear: true,
            changeMonth: true 

        });
    })

    function setFormSubmitToFalse() {
        setTimeout(function () { _spFormOnSubmitCalled = false; }, 3000);
        return true;
    }
</script> 
<asp:HiddenField ID="hdn_RequestNumber" runat="server" />
<div class="row no-padding margin-bottom-25">

    <div class="col-md-8 col-sm-6 col-xs-12">
        <h5 class="font-size-18 font-weight-600 margin-2 underline">
            <asp:Label ID="lbl_NotificationMsg" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NotifyMsgProcRecommend %>"></asp:Label>
        </h5>
    </div>

    <div class="col-md-4 col-sm-6 col-xs-12">
        <asp:Button ID="btn_ApproveProcedures" ValidationGroup="Save" runat="server" OnClick="btn_ApproveProcedures_Click" OnClientClick="return setFormSubmitToFalse();" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ApproveProcRecommend %>" CssClass="moe-btn btn pull-right" />
    </div>
</div>


<div class="container unheighlighted-section margin-bottom-50 flex-display flex-wrap" id="DialogProc">
    <asp:Panel ID="pnl_EmpRecommendation" runat="server" GroupingText="<%$Resources:ITWORX_MOEHEWF_UCE, EmpRecommendation %>" CssClass="stateTitle moe-full-width">
        <asp:HiddenField ID="hdn_ID" runat="server" />
         


        <div class="row margin-top-15">
            <div class="col-md-4 col-sm-12 col-xs-12 no-padding">
            <div class="data-container table-display moe-width-85">
                <div class="form-group">
                    <h6 class="font-size-16 margin-bottom-0 margin-top-0">

                        <asp:Label ID="lblGainedHours" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NumberOfHoursGained %>"></asp:Label></h6>
                    <div class="form">
                        <asp:TextBox ID="txtGainedHours" MaxLength="8" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="regGainedHours" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RegNumbersOnly %>" ValidationExpression="^\d+$" ControlToValidate="txtGainedHours" CssClass="moe-full-width error-msg" ValidationGroup="Save" Display="Dynamic"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
        </div>

            <!--10th Field-->
            <div class="col-md-4 col-sm-12 col-xs-12 no-padding">
                <div class="data-container table-display moe-width-85">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-0 margin-top-0">

                            <asp:Label ID="lblOnlineHours" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NumberOfOnlineHours %>"></asp:Label></h6>

                        <div class="form">
                            <asp:TextBox ID="txtOnlineHours" MaxLength="8" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="regOnlineHours" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RegNumbersOnly %>" ValidationExpression="^\d+$" ControlToValidate="txtOnlineHours" CssClass="moe-full-width error-msg" ValidationGroup="Save" Display="Dynamic"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
            </div>
            <!--12th Field-->
            <div class="col-md-4 col-sm-12 col-xs-12 no-padding">
                <div class="data-container table-display moe-width-85">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                            <asp:Label ID="lblOnlinePercentage" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PercentageOfOnlineHours %>"></asp:Label></h6>

                        <div class="form">
                            <asp:TextBox ID="txtOnlinePercentage" MaxLength="8" runat="server" CssClass="moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="regOnlinePercentage" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RegPercNumbersOnly %>" ValidationExpression="^[0-9]*\.?[0-9]*" runat="server" ControlToValidate="txtOnlinePercentage" CssClass="moe-full-width error-msg" ValidationGroup="Save" Display="Dynamic"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
            </div>
        </div>
         
        
       <div class="row margin-top-15">
            <div class="col-md-8 col-xs-12">
                <MOEHE:FileUpload runat="server" id="StatusRecommendAttachements" />
            </div>
      </div>
    
        <div class="row margin-top-15">
            <div class="col-md-6 col-sm-12 col-xs-12 no-padding">
            <div class="data-container table-display moe-width-85">
                <div class="form-group">
                    <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="Label4" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, HonoraryDegree %>"></asp:Label>
                </h6>
                    <div class="form">
                        <asp:RadioButtonList ID="rblHonoraryDegree" runat="server" CssClass="moe-radioBtn">
                            <asp:ListItem Value="0" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Successful %>"></asp:ListItem>
                            <asp:ListItem Value="1" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Ordinary %>"></asp:ListItem>
                            <asp:ListItem Value="2" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Honor %>"></asp:ListItem>
                        </asp:RadioButtonList>
                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, Required %>"
                            ValidationGroup="Save" ControlToValidate="rblOwners" CssClass="moe-full-width error-msg"></asp:RequiredFieldValidator>
   --%>                 </div>
                </div>
            </div>
        </div>
          <div class="col-md-6 col-sm-12 col-xs-12 no-padding">
                <div class="data-container table-display  moe-sm-full-width sm-width-90">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                            <asp:Label ID="Label2" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityName %>"></asp:Label></h6>
                        </h6>

                        <div class="form">
                           <asp:RadioButtonList ID="rblUniversity" runat="server" CssClass="moe-radioBtn">
                                <asp:ListItem Value="0" Text="<%$Resources:ITWORX_MOEHEWF_UCE, universityGovernment %>"></asp:ListItem>
                                <asp:ListItem Value="1" Text="<%$Resources:ITWORX_MOEHEWF_UCE, universityPrivate %>"></asp:ListItem>
                            </asp:RadioButtonList>
                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, Required %>"
                                ValidationGroup="Save" ControlToValidate="rblUniversity" CssClass="moe-full-width error-msg"></asp:RequiredFieldValidator>--%>  
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </div>
    <div class="container">
         
        <div class="row margin-top-15">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
                    <div class="data-container table-display moe-full-width  moe-sm-full-width">
                        <div class="form-group">
                            <h6 class="font-size-16 margin-bottom-0 margin-top-0 ">
                                <asp:Label ID="lbl_AddEmpOpinion" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EmployeeOpinion %>"></asp:Label>
                                <span class="error-msg"> *</span>
                            </h6>
                            <div class="form">
                                <asp:TextBox ID="txt_EmpOpinion" runat="server" TextMode="MultiLine" CssClass="moe-full-width input-height-42 border-box moe-input-padding text-area"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqVal_EmpOpinion" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredEmpOpinion %>"
                                    ValidationGroup="Save" ControlToValidate="txt_EmpOpinion" CssClass="moe-full-width error-msg"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
                    <div class="data-container table-display moe-width-85  moe-sm-full-width">
                        <h6 class="font-size-16 margin-bottom-0 margin-top-0 ">
                                <asp:Label ID="Label7" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EmpRecommendation %>"></asp:Label>
                            <span class="error-msg"> *</span>
                        </h6>
                        <div class="form-group">
                            <asp:RadioButtonList ID="rdbtn_EmpRecommendation" runat="server" CssClass="moe-radioBtn" onclick="changeDecisionTemp()">
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="reqVal_EmpRecommendation" runat="server" ForeColor="Red" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredEmpRecommendation %>"
                                ValidationGroup="Save" ControlToValidate="rdbtn_EmpRecommendation" CssClass="moe-full-width error-msg"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
                    <div class="data-container table-display moe-full-width moe-sm-full-width">
                        <div class="form-group">
                            <h6 class="font-size-16 margin-bottom-0 margin-top-0 ">
                                <asp:Label ID="lbl_DecisiontxtForPrinting" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, DecisiontxtForPrinting %>"></asp:Label>
                            </h6>
                            <div class="form recommend-form heighlighted-section">
                               <h1>
                                    <label><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","sir") %></label>
                                   <span class="error-msg"> *</span>
                                    <asp:TextBox ID="txtOccupationName" runat="server"></asp:TextBox>                      
                                    <label><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","recpected") %></label>
                                  
                               </h1>
                                 <asp:RequiredFieldValidator ID="txtOccupationNameValidator" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredOccupationName %>" Display="Dynamic"
                                    ValidationGroup="Save" ControlToValidate="txtOccupationName" CssClass="moe-full-width error-msg"></asp:RequiredFieldValidator>
                                <h1>
                                    <asp:Label ID="lbl_EntityNeedsEquivalency" runat="server" Text=""></asp:Label> 
                                </h1><br />
                                <h1>
                                    <label><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","WelcomText") %></label>
                                </h1>
                                <br />
                                <h1>
                                    <asp:Label ID="lbl_accordingBook" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, accordingBook %>"></asp:Label> 
                                    <span class="error-msg"> *</span>
                                    <asp:TextBox ID="txt_bookNum"  runat="server"></asp:TextBox>  
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="" Display="Dynamic"
                                    ValidationGroup="Save" ControlToValidate="txt_bookNum" CssClass="moe-full-width error-msg"></asp:RequiredFieldValidator>
                                    <asp:Label ID="lbl_inDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, inDate %>"></asp:Label> 
                                    <span class="error-msg"> *</span>
                                    <asp:TextBox ID="txt_bookDate" ClientIDMode="Static" runat="server"></asp:TextBox>    
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="" Display="Dynamic"
                                    ValidationGroup="Save" ControlToValidate="txt_bookDate" CssClass="moe-full-width error-msg"></asp:RequiredFieldValidator>
                                    <asp:Label ID="lbl_RemainingDecicionBoby" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RemainingDecicionBoby %>"></asp:Label> 
                                </h1>
                                <h1>
                                    <label id="lbl_Approve"><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","approveDecisionText") %></label>
                                    <label id="lbl_Reject" style="display:none"><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","rejectDecisionText") %></label>
                                    <span class="error-msg"> *</span>
                                    </h1>
                                <asp:TextBox ID="txt_DecisiontxtForPrinting" runat="server" TextMode="MultiLine" CssClass="moe-full-width input-height-42 border-box moe-input-padding text-area"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="req_DecisiontxtForPrinting" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredDecisiontxtForPrinting %>"
                                    ValidationGroup="Save" ControlToValidate="txt_DecisiontxtForPrinting" CssClass="moe-full-width error-msg"></asp:RequiredFieldValidator>
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
                </div>
            <div class="row margin-top-15">
                <div class="col-md-12 no-padding">
                    <asp:Button ID="btn_ReviewDecision" runat="server"  OnClick="ReviewDecisionClick" Text="<%$Resources:ITWORX_MOEHEWF_UCE, review %>" OnClientClick="return setFormSubmitToFalse();"  CssClass="pull-right"  />
                </div>
            </div>
        </div>

     <div class="row margin-top-15">
               <div class="col-md-6 col-sm-12 col-xs-12 no-padding">
                <div class="data-container table-display moe-width-85">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-0 margin-top-0" style="visibility: hidden;">
                            hidden
                        </h6>
                        <h6 class="font-size-16 margin-bottom-0 margin-top-0" >
                            <asp:CheckBox ID="ckbHavePA" runat="server" />
                            <asp:Label ID="lblckbHavePA" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, HeHavePA %>" ></asp:Label>
                        </h6>
                            
                        </div>
                    </div>
                </div>
        </div>
         
           <div class="row margin-top-15">
             <div class="col-md-12 no-padding">
                <asp:Button ID="btn_Save" runat="server" ValidationGroup="Save" OnClick="btn_Save_Click" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Save %>" OnClientClick="return setFormSubmitToFalse();" CssClass="pull-right" />
            </div>
        </div>
       
        <%--    <asp:Button ID="btn_ViewStatus" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ViewStatus %>"/>--%>
        <div class="row margin-top-15">
            <h5 class="col-md-12 font-size-18 font-weight-600 success-msg">
                <asp:Label ID="lbl_SaveSuccess" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, SavedSuccessfully %>" Visible="false" ForeColor="Green"></asp:Label>
            </h5>
        </div>

    </asp:Panel>
</div>


<div class="row no-padding margin-top-15">
    <h5 class="font-size-18 font-weight-600 success-msg">
        <asp:Label ID="lbl_Success" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, SuccessMsg %>" Visible="false" ForeColor="Green"></asp:Label>
    </h5>
</div>


<div class="container unheighlighted-section margin-bottom-50 flex-display flex-wrap" id="DialogProc">
    <div id="viewControls" runat="server" visible="false">
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lbl_EmpOpinion" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EmployeeOpinion %>"></asp:Label>
                </h6>
                <h5 class="font-size-22">
                    <asp:Label ID="lbl_EmpOpinionVal" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lbl_Decisiontxt" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, DecisiontxtForPrinting %>"></asp:Label>
                </h6>
 
                 
                <div class="form recommend-form heighlighted-section">
                       <h1>
                            <label><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","sir") %></label>
                            <asp:Label ID="lbl_OccupationName" runat="server" Text=""></asp:Label>                       
                            <label><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","recpected") %></label>
                       </h1>
                        <h1>
                            <asp:Label ID="lbl_EntityNeedsEquivalencyView" runat="server" Text=""></asp:Label> 
                        </h1><br />
                        <h1>
                            <label><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","WelcomText") %></label>
                        </h1>
                        <br />
                        <h1>
                            <asp:Label ID="lbl_decicionBobyView" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, DecisionBody %>"></asp:Label> 
                        </h1>
                        <h1>
                            <asp:Label ID="lbl_DecisiontxtVal" runat="server"></asp:Label>
                        </h1>
                        <h3>
                            <label><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","AppreciationText") %></label>
                        </h3> 
                        <h4>
                            <asp:Label ID="lbl_headManagerNameView" runat="server" ></asp:Label> 
                        </h4>
                        <h4>
                            <label><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","EquivalencManager") %></label>
                        </h4> 
                    </div>
            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lbl_Recommendation" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EmpRecommendation %>"></asp:Label>
                </h6>
                <h5 class="font-size-22">
                    <asp:Label ID="lbl_RecommendationVal" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
        <div class="col-md-12 no-padding">
            <MOEHE:FileUpload runat="server" id="ViewStatusRecommendAttachements" />

        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="Label1" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NumberOfHoursGained %>"></asp:Label>
                </h6>
                <h5 class="font-size-22">
                    <asp:Label ID="lbltxtGainedHours" Text="N/A" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="Label3" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NumberOfOnlineHours %>"></asp:Label>
                </h6>
                <h5 class="font-size-22">
                    <asp:Label ID="lbltxtOnlineHours" Text="N/A" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="Label5" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PercentageOfOnlineHours %>"></asp:Label>
                </h6>
                <h5 class="font-size-22">
                    <asp:Label ID="lbltxtOnlinePercentage" Text="N/A" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
         <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lblHonoraryDegree" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, HonoraryDegree %>"></asp:Label>
                </h6>
                <h5 class="font-size-22">
                    <asp:Label ID="lblrblHonoraryDegree" Text="N/A" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
         <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="Label6" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, HeHavePA %>"></asp:Label>
                </h6>
                <h5 class="font-size-22">
                    <asp:Label ID="valckbHavePA" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
         <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 no-padding ">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="Label8" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityName %>"></asp:Label>
                </h6>
                <h5 class="font-size-22">
                    <asp:Label ID="lblrblUniversity" Text="N/A" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
        </div>
    </div>
</div>
