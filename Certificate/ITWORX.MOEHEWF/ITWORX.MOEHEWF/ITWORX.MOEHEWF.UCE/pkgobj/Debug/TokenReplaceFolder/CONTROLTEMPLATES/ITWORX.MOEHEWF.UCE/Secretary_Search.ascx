<%@ Assembly Name="ITWORX.MOEHEWF.UCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=883afb4c05a35fe5" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Secretary_Search.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.Secretary_Search" %>

<%--<script type="text/javascript" src='<%= ResolveUrl ("~/Style%20Library/Scripts/jquery.min.js") %>'></script>
<link rel="stylesheet" href='<%= ResolveUrl ("~/Style%20Library/CSS/jquery-ui.css") %>'>
<script type="text/javascript" src='<%= ResolveUrl ("~/Style%20Library/Scripts/jquery-1.12.4.js") %>'></script>
<script type="text/javascript" src='<%= ResolveUrl ("~/Style%20Library/Scripts/jquery-ui.js") %>'></script>--%>

<div id="SrchControls" runat="server">
    <div>
        <asp:Label ID="lbl_RequestID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RequestID %>"></asp:Label>
        <asp:TextBox ID="txt_RequestID" runat="server"></asp:TextBox>
        <asp:RegularExpressionValidator ID="regVal_NumbersOnly" ControlToValidate="txt_RequestID" ValidationExpression="\d+" ValidationGroup="Srch"
            Display="Static" EnableClientScript="true" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, NumbersOnly %>" runat="server" />

        <asp:Label ID="lbl_NationalID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, QatariID %>"></asp:Label>
        <asp:TextBox ID="txt_NationalID" runat="server" MaxLength="11"></asp:TextBox>
        <asp:RegularExpressionValidator ID="regVal_NumbersOnlyID" ControlToValidate="txt_NationalID" ValidationExpression="\d+" ValidationGroup="Srch" Display="Static"
            EnableClientScript="true" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, NumbersOnly %>" runat="server" />
    </div>
    <br />
    <div>
        <asp:Label ID="lbl_ApplicantName" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ApplicantName %>"></asp:Label>
        <asp:TextBox ID="txt_ApplicantName" runat="server"></asp:TextBox>

        <asp:Label ID="lbl_EntityNeedsEquivalency" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EntityNeedsEquivalency %> "></asp:Label>
        <asp:DropDownList ID="drp_EntityNeedsEquivalency" runat="server" CssClass="moe-dropdown"></asp:DropDownList>
    </div>

    <br />

    <div>

        <asp:Label ID="lbl_Certificate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Certificate %>"></asp:Label>
        <asp:DropDownList ID="drp_Certificate" runat="server" CssClass="moe-dropdown"></asp:DropDownList>

        <asp:Label ID="lbl_RequestStatus" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RequestStatus %>"></asp:Label>
        <asp:DropDownList ID="drp_RequestStatus" runat="server" CssClass="moe-dropdown"></asp:DropDownList>
    </div>

    <br />

    <br />
    <div>
        <asp:Label ID="lbl_TheEquationSent" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, TheEquationSent %>"></asp:Label>

        <asp:DropDownList runat="server" ID="drp_TheEquationSent" CssClass="moe-dropdown">
            <asp:ListItem Value="-1" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ChooseValue %>"></asp:ListItem>
            <asp:ListItem Value="0" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Yes %>"></asp:ListItem>
            <asp:ListItem Value="1" Text="<%$Resources:ITWORX_MOEHEWF_UCE, No %>"></asp:ListItem>
        </asp:DropDownList>
    </div>

    <asp:Button ID="btn_Search" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Search %>" OnClick="btn_Search_Click" ClientIDMode="Static" />
    <asp:Button ID="btn_Cancel" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, ChangeSearchCriteria %>" OnClick="btn_Cancel_Click" />
</div>
<br />

<asp:GridView ID="grd_Requests" runat="server" AutoGenerateColumns="false" AllowPaging="true" DataKeyNames="ID" OnPageIndexChanging="grd_Requests_PageIndexChanging" 
    ShowHeaderWhenEmpty="true" EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>">
    <Columns>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, SerialNumber %>">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, RequestID %>">
            <ItemTemplate>
                <asp:HiddenField ID="hdn_RequestStatusId" runat="server" Value='<%#  Eval("RequestStatusId")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_IsClosed" runat="server" Value='<%#  Eval("IsRequestClosed")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_AssignedTo" runat="server" Value='<%#  Eval("AssignedTo")%>'></asp:HiddenField>
                <asp:HiddenField ID="hdn_ID" runat="server" Value='<%#  Eval("ID")%>'></asp:HiddenField>
                <asp:Label ID="lbl_RequestID" runat="server" Text='<%#  Eval("RequestID")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, QatariID %>">
            <ItemTemplate>
                <asp:Label ID="lbl_QatariID" runat="server" Text='<%#  Eval("QatariID")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, ApplicantName %>">
            <ItemTemplate>
                <asp:Label ID="lbl_ApplicantName" runat="server" Text='<%# Eval("ApplicantName") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, Nationality %>">
            <ItemTemplate>
                <asp:Label ID="lbl_Nationality" runat="server" Text='<%#  Eval("Nationality")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, Certificate %>">
            <ItemTemplate>
                <asp:Label ID="lbl_AcademicDegree" runat="server" Text='<%#  Eval("AcademicDegree")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, EntityNeedsCertificate %>">
            <ItemTemplate>
                <asp:Label ID="lbl_EntityNeedsCertificate" runat="server" Text='<%# Eval("EntityNeedsEquivalency") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, CountryName %>">
            <ItemTemplate>
                <asp:Label ID="lbl_Country" runat="server" Text='<%# Eval("Country") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, University %>">
            <ItemTemplate>
                <asp:Label ID="lbl_University" runat="server" Text='<%# Eval("University") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, FacultyName %>">
            <ItemTemplate>
                <asp:Label ID="lbl_Faculty" runat="server" Text='<%# Eval("Faculty") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX.MOEHEWF.Common, AttachmentName %>">
            <ItemTemplate>
                <asp:HyperLink ID="hypAttachment" Text='<%#Eval("FileName") %>' runat="server" NavigateUrl='<%#Eval("AttachmentURL") %>' Target="_blank"></asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, TheEquationSent %>">
            <ItemTemplate>
                <asp:Label ID="lbl_TheEquationSent" runat="server" Text='<%# Eval("OrgBookReply") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_UCE, Action %>">
            <ItemTemplate>
                <asp:LinkButton ID="lnk_View" runat="server" Tooltip="<%$Resources:ITWORX_MOEHEWF_UCE, View %>" OnClick="lnk_View_Click" CssClass="display-icon fa fa-eye"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

<div class="row no-padding margin-top-15">
   <h4 class="font-size-18 font-weight">
	    <asp:Label ID="lbl_NoOfRequests" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NoOfRequests %>"></asp:Label>
   </h4>
</div>
