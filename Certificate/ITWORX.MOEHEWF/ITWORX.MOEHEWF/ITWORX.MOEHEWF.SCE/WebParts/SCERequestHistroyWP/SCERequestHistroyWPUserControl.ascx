<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SCERequestHistroyWPUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.WebParts.SCERequestHistroyWP.SCERequestHistroyWPUserControl" %>
<div class="tab-pane tab-padd" id="history" role="tabpanel">
    <div class="row">
        <div class="col-xs-12">
            <h2 class="tab-title"> <asp:Label ID="lblCertTitle" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateHistoricalRecords %>"></asp:Label>
               </h2>
        </div>
    </div>
    <div class="dark-bg displayMode margin-top-25 tab-pane-wrap">
        <div class="row margin-top-15">
            <div class="col-md-6">
                <div class="form-group">
                    <h6><asp:Label ID="lblReqNo" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequestNumber %>"></asp:Label> :
                      </h6>
                    <h5>
                        <asp:Label ID="lblRequestNo" runat="server"></asp:Label></h5>
                </div>
            </div>

            <div class="col-md-6">
                <div class="form-group">
                    <h6><asp:Label ID="lblCertEquiv" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SchoolCertificateToBeEquivalent %>"></asp:Label> :</h6>
                    <h5>  <asp:Label ID="lblCertEquivalance" runat="server"></asp:Label></h5>
                </div>
            </div>
        </div>

        <div class="row margin-top-15">
            <div class="col-md-6">
                <div class="form-group">
                    <h6><asp:Label ID="lblSendDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SendDate %>"></asp:Label> :</h6>
                    <h5>
                        <asp:Label ID="lblRequestDate" runat="server"></asp:Label>
                    </h5>
                </div>
            </div>
            

            <div class="col-md-6">
                <div class="form-group">
                    <h6><asp:Label ID="lblCertificateHolderNo" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateHolderQatarID %>"></asp:Label> :</h6>
                    <h5> <asp:Label ID="lblStudentNo" runat="server" ></asp:Label></h5>
                </div>
            </div>
        </div>
        <div class="row margin-top-15">


            


            <div class="col-md-6">
                <div class="form-group">
                    <h6><asp:Label ID="lblHolderStudentName" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, HolderStudentName %>"></asp:Label> : </h6>
                    <h5>
                        <asp:Label ID="lblStudentName" runat="server"></asp:Label></h5>
                </div>
            </div>




            

            <div class="col-md-6">
                <div class="form-group">
                    <h6><asp:Label ID="Label1" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ApplicantName %>"></asp:Label> :</h6>
                    <h5> <asp:Label ID="lblApplicantName" runat="server" ></asp:Label> </h5>
                </div>
            </div>
        </div>

    </div>

    <div class="margin-top-5">

  <asp:GridView ID="grd_HistoricalRecords" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" ShowHeaderWhenEmpty="true"
    EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>" OnPageIndexChanging="grd_HistoricalRecords_PageIndexChanging" CssClass="table moe-table table-striped result-table margin-top-25" 
       HeaderStyle-CssClass="text-center">
    <Columns>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, ExecutedProcedure %>">
            <ItemTemplate>
                <asp:HiddenField ID="lbl_RequestID" runat="server" Value='<%#  Eval("RequestID")%>'></asp:HiddenField>
                <asp:Label ID="lbl_ExecutedAction" runat="server" Text='<%#  Eval("ExecutedAction")%>' ToolTip='<%#  Eval("ExecutedAction")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, ActionDate %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ActionDate" runat="server" ToolTip='<%# Convert.ToDateTime(Eval("ActionDate"))!=DateTime.MinValue ? Convert.ToDateTime(Eval("ActionDate")).ToShortDateString():string.Empty %>' Text='<%# Convert.ToDateTime(Eval("ActionDate"))!=DateTime.MinValue ? Convert.ToDateTime(Eval("ActionDate")).ToShortDateString():string.Empty %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, Executor %>">
            <ItemTemplate>
                <asp:Label ID="lbl_Executor" runat="server" Text='<%#  Eval("Executor")%>' ToolTip='<%#  Eval("Executor")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, AuthorityTask %>">
            <ItemTemplate>
                <asp:Label ID="lbl_AuthorityTask" runat="server" Text='<%#  Eval("AuthorityTask")%>' ToolTip='<%#  Eval("AuthorityTask")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, ProcComments %>">
            <ItemTemplate>
                <asp:Label ID="lbl_Comments" runat="server" Text='<%#  Eval("Comments")%>' ToolTip='<%#  Eval("Comments")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField Visible="false">
            <ItemTemplate>
                <asp:HiddenField ID="hdn_RequestId" runat="server" Value='<%# Eval("RequestID") %>'  />
            </ItemTemplate>
        </asp:TemplateField>

    </Columns>
</asp:GridView>


    </div>

</div>