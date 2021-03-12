<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PMRejectRequestSCEUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.WebParts.PMRejectRequestSCE.PMRejectRequestSCEUserControl" %>
   


                                <div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <h2 class="tab-title">
                                                <asp:Label ID="lblRetunRequestTitle" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ReturnRequest %> "></asp:Label>
                                                                                         
                                            </h2>
                                        </div>
                                    </div>
                                    <div class="row margin-top-50">
                                    
                                    	
                                    	<div class="col-md-4 col-sm-6">
                                    		<div class="form-group">
                                    			
                                                <asp:Label ID="lblDateTitle" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Date %> "></asp:Label>
                                                <asp:TextBox ID="txtRequestDate" runat="server" Enabled="false" CssClass="form-control"> </asp:TextBox>
                                    		</div>
                                    	</div>
												<div class="col-md-4 col-md-offset-1 col-sm-6">
                                    		<div class="form-group">
                                                <asp:Label ID="lblReturnReasonTitle" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ReturnReasonPM %> "></asp:Label><span class="error-msg">*</span>
                                    			<asp:DropDownList ID="ddl_RejectionReasons" runat="server" CssClass="form-cotrol moe-dropdown"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rqrReasons" CssClass="error-msg" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE,ReturnValidation %>" ControlToValidate="ddl_RejectionReasons" InitialValue="" ValidationGroup="RetunrRequest"></asp:RequiredFieldValidator>
                                    		</div>
                                    	</div>
                                    </div>
                                    <div class="row margin-top-15">
                                        <div class="col-xs-12">
                                            <div class="form-group">
                                              <asp:Label ID="lblReturnCommentsTitle" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ReturnComments %> "></asp:Label><span class="error-msg">*</span>

                                                <asp:TextBox ID="txtReturnComments" runat="server" CssClass="form-control text-area" TextMode="MultiLine"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rqrReturnComments" CssClass="error-msg" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_SCE,ReturnValidation %>" ControlToValidate="txtReturnComments" InitialValue="" ValidationGroup="RetunrRequest"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                        <div class="col-xs-12 margin-top-15 warningMsg">
                                            <h5 class="font-size-14 margin-bottom-0 margin-top-0 instruction-details color-black font-family-sans">
                                                <span> <asp:Label ID="lblReturnNoteTitle" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ReturnNote %> "></asp:Label> </span>
                                            </h5>
                                        </div>
                                        <div class="col-xs-12 text-right margin-top-15">
                                            <asp:Button ID="btnReturnRequets" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ReturnRequest %> " CssClass="btn school-btn" OnClick="btnReturnRequets_Click" ValidationGroup="RetunrRequest"  />
                                        </div>
                                    </div>
                                </div>
                                
