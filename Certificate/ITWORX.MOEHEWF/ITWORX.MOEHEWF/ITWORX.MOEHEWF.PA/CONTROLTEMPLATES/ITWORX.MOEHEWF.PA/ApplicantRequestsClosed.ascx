<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApplicantRequestsClosed.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.ApplicantRequestsClosed" %>

<asp:GridView ID="grd_ApplicantRequestsClosed" runat="server" AutoGenerateColumns="false" AllowPaging="true" ShowHeaderWhenEmpty="true"
    EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>" OnPageIndexChanging="grd_ApplicantRequestsClosed_PageIndexChanging" DataKeyNames="ID" CssClass="table table-striped moe-full-width moe-table result-table margin-top-25">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:HiddenField ID="hdn_ID" runat="server" Value='<%#  Eval("ID")%>'></asp:HiddenField>
                 <asp:HiddenField runat="server" ID="hdn_RequestStatusId" Value='<%# Eval("RequestStatusId") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, RequestNumber %>">
            <ItemTemplate>
                <asp:Label ID="lnk_RequestID" runat="server" Text='<%#  Eval("RequestNumber")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, QatariID %>">
            <ItemTemplate>
                <asp:Label ID="lbl_QatariID" runat="server" Text='<%#  Eval("QatariID")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, ApplicantName %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ApplicantName" runat="server" Text='<%# Eval("ApplicantName") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, Nationality %>">
            <ItemTemplate>
                <asp:Label ID="lbl_Nationality" runat="server" Text='<%#  Eval("Nationality")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, WantedAcademicDegree %>">
            <ItemTemplate>
                <asp:Label ID="lbl_AcademicDegree" runat="server" Text='<%#  Eval("AcademicDegree")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, StudySystem %>">
            <ItemTemplate>
                <asp:Label ID="lbl_EntityNeedsCertificate" runat="server" Text='<%# Eval("StudySystem") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, CountryName %>">
            <ItemTemplate>
                <asp:Label ID="lbl_Country" runat="server" Text='<%# Eval("ProgramCountry") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, University %>">
            <ItemTemplate>
                <asp:Label ID="lbl_University" runat="server" Text='<%# Eval("ProgramUniversity") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, FacultyName %>">
            <ItemTemplate>
                <asp:Label ID="lbl_Faculty" runat="server" Text='<%# Eval("Faculty") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, SubmitDate %>">
            <ItemTemplate>
                <asp:Label ID="lbl_RequestSubmitDate" runat="server" Text='<%# Convert.ToDateTime(Eval("SubmitDate"))!=DateTime.MinValue ? Convert.ToDateTime(Eval("SubmitDate")).ToShortDateString():string.Empty %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_PA, Action %>">
            <ItemTemplate>
                <asp:LinkButton ID="lnkDisplay" runat="server" ToolTip="<%$Resources:ITWORX_MOEHEWF_PA, Display %>" OnClick="lnkDisplay_Click" CssClass="display-icon fa fa-eye"></asp:LinkButton>

            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

<div class="row no-padding margin-top-15">
    <h4 class="font-size-18 font-weight-600 text-center success-msg">
        <asp:Label ID="lbl_SuccessMsg" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, SuccessMsg %>"></asp:Label>
    </h4>
</div>

<div class="row no-padding margin-top-15">
    <h4 class="font-size-18 font-weight-600 text-center">
        <asp:Label ID="lbl_NoOfRequests" runat="server"></asp:Label>
    </h4>
</div>