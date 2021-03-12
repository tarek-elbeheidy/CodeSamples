<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmpReturnRequestWPUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.WebParts.EmpReturnRequestWP.EmpReturnRequestWPUserControl" %>
<div class="tab-pane tab-padd" role="tabpanel" runat="server" id="returnResquest" visible="true">
                                    <div class="row">
                                    	<div class="col-xs-12">
                                    		<h2 class="tab-title">
                                                <asp:Label ID="lblTitle" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, EmpReturnTitle %>"></asp:Label></h2>
                                    	</div>
                                    </div>
                                    <div class="row margin-top-25">
                                    	<div class="col-xs-12">
                                    		<div class="form-group">
                                               <label> <asp:Label ID="lblreturnnote" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,EmpReturnNotes %>"></asp:Label></label><span class="error-msg">*</span>
                                                <asp:TextBox ID="txtReturnote" runat="server" CssClass="form-control text-area" TextMode="MultiLine"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rqrReturnNote" CssClass="error-msg" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE,ReturnValidation %>" ControlToValidate="txtReturnote" ValidationGroup="Submit"></asp:RequiredFieldValidator>
											</div>
                                    	</div>
                                    	<div class="col-xs-12 margin-top-15 warningMsg">
										        <h5 class="font-size-14 margin-bottom-0 margin-top-0 instruction-details color-black font-family-sans">
										            <span> <asp:Label ID="Label1" runat="server" ></asp:Label></span>										
										        </h5>
                                    	</div>
                                    	<div class="col-xs-12 text-right margin-top-15">
                                            <asp:Button ID="btnReturn" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,ReturnRequest %>" CssClass="btn school-btn" ValidationGroup="Submit" OnClick="btnReturn_Click"/>
                                    	</div>
                                    </div>
                                </div>
<div class="tab-pane tab-padd" role="tabpanel" runat="server" id="dvRetunNote" visible="false">
    <div class="row">
                                    	<div class="col-xs-12">
                                    		<h2 class="tab-title">
                                                <asp:Label ID="lblretunedNote" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE,cantReturnRequest %>"></asp:Label></h2>
                                    	</div>
                                    </div>
    </div>