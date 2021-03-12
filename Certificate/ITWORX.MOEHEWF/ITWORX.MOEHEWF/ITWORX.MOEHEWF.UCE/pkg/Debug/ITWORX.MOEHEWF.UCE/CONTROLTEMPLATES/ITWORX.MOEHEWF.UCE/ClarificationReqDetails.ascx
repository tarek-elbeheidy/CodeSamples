<%@ Assembly Name="ITWORX.MOEHEWF.UCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=883afb4c05a35fe5" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClarificationReqDetails.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.ClarificationReqDetails" %>
   <asp:HiddenField ID="hdn_ID" runat="server" Value='<%# Eval("ID") %>' />
   

<div class="container heighlighted-section margin-bottom-50">
           <div class="row">
                <div class="col-md-4 col-sm-6 no-padding">
                <div class="data-container table-display moe-width-85">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                            <asp:Label ID="lbl_RequestID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RequestID %>"></asp:Label>
    
                        </h6>

                        <h5  class="font-size-20">
                            <asp:Label ID="lbl_RequestIDVal" runat="server"></asp:Label>
                        </h5>
                    </div>
                </div>
          </div>

 

    <div class="col-md-4 col-sm-6 no-padding">
                <div class="data-container table-display moe-width-85">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                            <asp:Label ID="lbl_RequestSender" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ClarRequestSender %>"></asp:Label>
    
                        </h6>

                        <h5  class="font-size-20">
                            <asp:Label ID="lbl_RequestSenderVal" runat="server"></asp:Label>
                        </h5>
                    </div>
                </div>
            </div>

    <div class="col-md-4 col-sm-6 no-padding">
                <div class="data-container table-display moe-width-85">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                              <asp:Label ID="lbl_RequestClarificationDate" runat="server" Text=" <%$Resources:ITWORX_MOEHEWF_UCE, ClarRequestedDate %>"></asp:Label>
    
                        </h6>

                        <h5  class="font-size-20">
                            <asp:Label ID="lbl_RequestClarificationDateVal" runat="server"></asp:Label>
                        </h5>
                    </div>
                </div>
            </div>

  

    <div class="col-md-4 col-sm-6 no-padding">
        <div class="data-container table-display moe-width-85">
            <div class="form-group">
                <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                    <asp:Label ID="lbl_ReplyDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ReplyDate %>" Visible="false"></asp:Label>

                </h6>

                <h5 class="font-size-20">
                    <asp:Label ID="lbl_ReplyDateVal" runat="server" Visible="false"></asp:Label>
                </h5>
        </div>
    </div>
</div>

    <div class="col-md-4 col-sm-6 no-padding">
                <div class="data-container table-display moe-width-85">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                             <asp:Label ID="lbl_AssignedTo" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ReplySender %>" Visible="false"></asp:Label>
   
                        </h6>

                        <h5 class="font-size-20">
                            <asp:Label ID="lbl_AssignedToVal" runat="server" Visible="false"></asp:Label>
                        </h5>
                    </div>
                </div>
            </div>
           </div>

      <div class="row">
           <div class="col-md-12 col-sm-12 col-xs-12 no-padding">
                <div class="data-container table-display moe-width-85">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                            <asp:Label ID="lbl_RequestedClarification" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ClarRequested %>"></asp:Label>
  
                        </h6>

                        <h5  class="font-size-20">
                              <asp:Label ID="lbl_RequestedClarificationVal" runat="server"></asp:Label>
                        </h5>
                    </div>
                </div>
            </div>

      <div class="col-md-12 col-sm-12 col-xs-12 no-padding">
                <div class="data-container table-display moe-width-85">
                    <div class="form-group">
                        <h6 class="font-size-16 margin-bottom-10 margin-top-10">
                             <asp:Label ID="lbl_ClarificationReply" runat="server" Text=" <%$Resources:ITWORX_MOEHEWF_UCE, ClarReply %>" Visible="false"></asp:Label>
    
                        </h6>

                        <h5  class="font-size-20">
                            <asp:Label ID="lbl_ClarificationReplyVal" runat="server" Visible="false"></asp:Label>
                        </h5>
                    </div>
                </div>
            </div>
      </div>
  
    </div>


<div class="container no-padding margin-top-10">
<div class="row">
        <asp:Button ID="btn_Close" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Close %>" OnClick="btn_Close_Click" CssClass="btn moe-btn pull-right"/>

</div>
</div>

    
