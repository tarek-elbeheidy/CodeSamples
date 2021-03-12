<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HistoricalRecords.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.HistoricalRecords" %>


<div class="cotainer heighlighted-section no-padding test-display">
    <div class="row">
        <div class="col-md-4 col-sm-6 auto-height">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                     <asp:Label ID="lbl_degree" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, DegreeNeedsEquivalency %>"></asp:Label>
    
                </h6>
                <h5 class="font-size-20">
                    <asp:Label ID="lbl_degreeVal" runat="server"></asp:Label>
                </h5>
                    
            </div>
    </div>

    <div class="col-md-4 col-sm-6 auto-height">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                      <asp:Label ID="lbl_RequestID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RequestID %>"></asp:Label>
                </h6>
                <h5 class="font-size-20">
                   
    <asp:Label ID="lbl_RequestIDVal" runat="server"></asp:Label>
                </h5>
                    
            </div>
    </div>

    <div class="col-md-4 col-sm-6 auto-height">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                     <asp:Label ID="lbl_Date" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, SubmitDate %>"></asp:Label>
   
                </h6>
                <h5 class="font-size-20">
                     <asp:Label ID="lbl_DateVal" runat="server"></asp:Label>
                </h5>
                    
            </div>
    </div>
    </div>
   
<div class="row margin-top-15">
       <div class="col-md-4 col-sm-6 auto-height">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                     <asp:Label ID="lbl_QatariID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, QatariID %>"></asp:Label>
   
                </h6>
                <h5 class="font-size-20">
                        <asp:Label ID="lbl_QatariIDVal" runat="server"></asp:Label>

                </h5>
                    
            </div>
    </div>

       <div class="col-md-4 col-sm-6 auto-height">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
    <asp:Label ID="lbl_ApplicantName" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ApplicantName %>"></asp:Label>
                </h6>
                <h5 class="font-size-20">
                        <asp:Label ID="lbl_ApplicantNameVal" runat="server"></asp:Label>

                </h5>
                    
            </div>
    </div>
</div>
     </div>

<asp:GridView ID="grd_HistoricalRecords" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" ShowHeaderWhenEmpty="true"
    EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>" OnPageIndexChanging="grd_HistoricalRecords_PageIndexChanging" CssClass="table moe-table table-striped result-table margin-top-25">
    <Columns>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, ExecutedProcedure %>">
            <ItemTemplate>
                <asp:HiddenField ID="lbl_RequestID" runat="server" Value='<%#  Eval("RequestID")%>'></asp:HiddenField>
                <asp:Label ID="lbl_ExecutedAction" runat="server" Text='<%#  Eval("ExecutedAction")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, ActionDate %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ActionDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ActionDate"))!=DateTime.MinValue ? Convert.ToDateTime(Eval("ActionDate")).ToShortDateString():string.Empty %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, Executor %>">
            <ItemTemplate>
                <asp:Label ID="lbl_Executor" runat="server" Text='<%#  Eval("Executor")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, AuthorityTask %>">
            <ItemTemplate>
                <asp:Label ID="lbl_AuthorityTask" runat="server" Text='<%#  Eval("AuthorityTask")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, ProcComments %>">
            <ItemTemplate>
                <asp:Label ID="lbl_Comments" runat="server" Text='<%#  Eval("Comments")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField Visible="false">
            <ItemTemplate>
                <asp:HiddenField ID="hdn_RequestId" runat="server" Value='<%# Eval("RequestID") %>' />
            </ItemTemplate>
        </asp:TemplateField>

    </Columns>
</asp:GridView>

