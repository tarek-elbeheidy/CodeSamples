<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Add_FinalDecision.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.Add_FinalDecision" %>

   

    
<script> 
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

<div id="main-content">
	<div class="row section-container side-nav-container">


		
<div class="row unheighlighted-section margin-bottom-50 flex-display flex-wrap">

    <div class="col-md-3 col-sm-6 margin-top-10 margin-bottom-10">
        <div class="data-container table-display moe-width-85">
            <div class="form-group">
                <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                    
     <asp:Label ID="lbl_Decision" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, FinalDecision %>"></asp:Label><span class="error-msg"> *</span>

  
                </h6>
                
                 <asp:DropDownList ID="drp_FinalDecision" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drp_FinalDecision_SelectedIndexChanged" CssClass="moe-dropdown moe-full-width input-height-42 border-box moe-input-padding">
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="reqVal_FinalDecision" runat="server" ForeColor="Red" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredProcedure %>"
        ValidationGroup="Approve" ControlToValidate="drp_FinalDecision" CssClass="error-msg moe-full-width"></asp:RequiredFieldValidator>
                    


                
            </div>
        </div>
    </div>

	 <div class="col-md-3 col-sm-6 margin-top-10 margin-bottom-10">
        <div class="data-container table-display moe-width-85">
            <div class="form-group">
                <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                    
<asp:Label ID="lbl_RejectionReason" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RejectionReason %>" Visible="false"></asp:Label>
    
  
                </h6>
    <asp:DropDownList ID="drp_RejectionReason" runat="server" Visible="false" CssClass="moe-dropdown moe-full-width input-height-42 border-box moe-input-padding"></asp:DropDownList>

                    


                
            </div>
        </div>
    </div>


	 <div class="col-md-12 margin-top-10 margin-bottom-10">
        <div class="data-container table-display moe-full-width">
            <div class="form-group">
                <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                    
    <asp:Label ID="lbl_Comments" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ProcedureComments %>"></asp:Label><span class="error-msg"> *</span>
    

  
                </h6>
                

				<asp:TextBox ID="txt_Comments" runat="server" TextMode="MultiLine"  CssClass="moe-full-width input-height-42 border-box moe-input-padding text-area"></asp:TextBox>
    <asp:RequiredFieldValidator ID="reqVal_Comments" runat="server" CssClass="error-msg moe-full-width" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredComments %>"
        ValidationGroup="Approve" ControlToValidate="txt_Comments"></asp:RequiredFieldValidator>


                
            </div>
        </div>
    </div>

    	 <div class="col-md-12 margin-top-10 margin-bottom-10" id="div_DecisionforPrint" runat="server">
        <div class="data-container table-display moe-full-width">
            <div class="form-group">
                <h6 class="font-size-16 margin-bottom-10 margin-top-15">
                    <asp:HiddenField ID="hdn_ProcID" runat="server" />          
    <asp:Label ID="lbl_DecisionforPrint" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, DecisiontxtForPrinting %>"></asp:Label><span class="error-msg"> *</span>
    

  
                </h6>
                

                   <div class="form recommend-form heighlighted-section">
                       <h1>
                           <asp:Label ID="lbl_SirValue" runat="server" Text=""></asp:Label> 
                            
                            <asp:TextBox ID="txtOccupationName" runat="server"></asp:TextBox>          
                           <asp:Label ID="lbl_RespectedValue" runat="server" Text=""></asp:Label>
                            
                       </h1>
                         <asp:RequiredFieldValidator ID="txtOccupationNameValidator" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredOccupationName %>" Display="Dynamic"
                            ValidationGroup="Approve" ControlToValidate="txtOccupationName" CssClass="moe-full-width error-msg"></asp:RequiredFieldValidator>
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
                            ValidationGroup="Approve" ControlToValidate="txt_bookNum" CssClass="moe-full-width error-msg"></asp:RequiredFieldValidator>
                            <asp:Label ID="lbl_inDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, inDate %>"></asp:Label> 
                            <span class="error-msg"> *</span>
                            <asp:TextBox ID="txt_bookDate" ClientIDMode="Static" runat="server"></asp:TextBox>    
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="" Display="Dynamic"
                            ValidationGroup="Approve" ControlToValidate="txt_bookDate" CssClass="moe-full-width error-msg"></asp:RequiredFieldValidator>
                            <asp:Label ID="lbl_RemainingDecicionBoby" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RemainingDecicionBoby %>"></asp:Label> 
                        </h1>
                        <h1>
                            <asp:Label ID="lbl_decisionText" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, approveDecisionText %>"></asp:Label> 
                        </h1>
                        <asp:TextBox ID="txt_DecisiontxtForPrinting" runat="server" TextMode="MultiLine" CssClass="moe-full-width input-height-42 border-box moe-input-padding text-area"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="req_DecisiontxtForPrinting" runat="server" ForeColor="Red" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredDecisiontxtForPrinting %>"
                            ValidationGroup="Approve" ControlToValidate="txt_DecisiontxtForPrinting" CssClass="error-msg moe-full-width"></asp:RequiredFieldValidator>

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

	<div class="col-md-12 no-padding margin-top-10">
    <asp:Button ID="btn_ApproveDecision" ValidationGroup="Approve" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ApproveDecision %>" OnClick="btn_ApproveDecision_Click" CssClass="btn moe-btn pull-right"/>
		</div>
</div>


		


</div>
</div>