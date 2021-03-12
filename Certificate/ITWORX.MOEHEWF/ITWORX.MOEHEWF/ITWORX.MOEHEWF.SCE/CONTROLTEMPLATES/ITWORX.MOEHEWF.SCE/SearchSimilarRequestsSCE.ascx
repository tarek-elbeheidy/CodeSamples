<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/DDLWithTXTWithNoPostback.ascx" TagPrefix="uc1" TagName="DDLWithTXTWithNoPostback" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchSimilarRequestsSCE.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE.SearchSimilarRequestsSCE" %>


<style>
    .hidden {
        display: none;
    }
</style>
<%--<asp:Label ID="lbl_search" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SearchIn %>"></asp:Label>
<asp:DropDownList ID="ddl_Search" runat="server"></asp:DropDownList>--%>


<div class="dark-bg tab-pane-wrap">
    <div class="row margin-top-15">
        <div class="col-md-4 col-sm-6">
            <div class="form-group">
                <label>
                    <asp:Label ID="lbl_country" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateOrigin %>"></asp:Label></label>
                <asp:DropDownList ID="ddl_CertificateResource" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
            </div>
        </div>
        <div class="col-md-4 col-sm-6">
            <uc1:DDLWithTXTWithNoPostback runat="server" id="ddl_SchoolType" />
        </div>
        <div class="col-md-4 col-sm-6">
            <div class="form-group">
                <label>
                    <asp:Label ID="lbl_nationality" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Nationality %>"></asp:Label></label>
                <asp:DropDownList ID="ddl_Nationality" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
            </div>
        </div>
    </div>

    <div class="row margin-top-15">
        <div class="col-md-4 col-sm-6">
            <div class="form-group">
                <label>
                    <asp:Label ID="lbl_preferedevel" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, PreferedGrade %>"></asp:Label></label>
                <asp:DropDownList ID="ddl_PreferedSchoolLevel" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
            </div>
        </div>
        <div class="col-md-4 col-sm-6">
            <div class="form-group">
                <label>
                    <asp:Label ID="lbl_certificateType" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateType %>"></asp:Label></label>
                <asp:DropDownList ID="ddl_CertificateType" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
            </div>
        </div>

        <div class="col-md-4 col-sm-6">
            <div class="form-group">
                <label>
                    <asp:Label ID="lbl_lastLevel" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, LastGrade %>"></asp:Label></label>
                <asp:DropDownList ID="ddl_LastSchoolLevel" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="row margin-top-15">
        <div class="col-md-4 col-sm-6">
            <div class="form-group">
                <label>
                    <asp:Label ID="lbl_AcademicYear" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, AcademicYear %>"></asp:Label></label>
                <asp:DropDownList ID="ddl_AcademicYear" runat="server" CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12 text-right">
            <asp:Button ID="btn_Clear" runat="server" OnClientClick="LnkClickSearch(this.id)" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ClearSearch %>" OnClick="btn_Clear_Click" CssClass="btn school-btn clear-btn" />
            <asp:Button ID="Btn_search" runat="server" OnClientClick="LnkClickSearch(this.id)" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Search %>" OnClick="Btn_search_Click"  CssClass="btn school-btn" />
        </div>
    </div>
</div>

<div class="row">
    <%--   <div class="col-xs-6">
            <h2 class="tab-title text-left  margin-top-0  margin-bottom-5">
                <asp:Label ID="Label1" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ClarificationRequests %>"></asp:Label></h2>
        </div>--%>

    <div class="col-xs-12">
        <h4 class="font-weight-600 font-size-18 text-right numOfReuq">
            <asp:Label ID="lbl_NoOfRequests" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, NoOfRequests %>"></asp:Label></h4>
    </div>
</div>

<div class="row">
    <div class="col-xs-12">
        <asp:GridView ID="grd_SearchResults" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="grd_SearchResults_PageIndexChanging"
            ShowHeaderWhenEmpty="true" OnRowDataBound="grd_SearchResults_RowDataBound" EmptyDataText="<%$Resources:ITWORX.MOEHEWF.Common, EmptyTrackingRequests %>" CssClass="table moe-table table-striped result-table">
            <Columns>
                
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdn_ID" runat="server" Value='<%#  Eval("Id")%>'></asp:HiddenField>
                          <asp:HiddenField ID="hdnRequestStatus" runat="server" Value='<%#  Eval("RequestStatusId")%>'></asp:HiddenField>
                        <asp:HiddenField ID="hdnRegisteredLevel" runat="server" Value='<%#  Eval("RegisteredSchoolId")%>' />
                        <%--  <asp:HiddenField ID="hdn_ReqId" runat="server" Value='<%#  Eval("RequestIDId")%>'></asp:HiddenField>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, SerialNumber %>">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, RequestNumber %>">
                    <ItemTemplate>
                        <asp:HyperLink ID="lnk_ReqNumber" runat="server" Target="_blank" NavigateUrl='<%#  Eval("RequestUrl")%>' ToolTip=<%#  Eval("RequestNumber")%>><%#  Eval("RequestNumber")%></asp:HyperLink>
                        <%--<asp:Label ID="lbl_RequestNumber" runat="server" Text='<%#  Eval("RequestNumber")%>'></asp:Label>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, EquationDate %>">
                    <ItemTemplate>
                        <asp:Label ID="lbl_RequestDate" runat="server" Text='<%#  Eval("RequestDate")%>' ToolTip='<%#  Eval("RequestDate")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, StudentNumber %>">
                    <ItemTemplate>
                        <asp:Label ID="lbl_StudentNumber" runat="server" Text='<%# Eval("ApplicantNumber") %>' ToolTip='<%# Eval("ApplicantNumber") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, StudentName %>">
                    <ItemTemplate>
                        <asp:Label ID="lbl_StudentName" runat="server" Text='<%#  Eval("ApplicantName")%>' ToolTip='<%#  Eval("ApplicantName")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, Nationality %>">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Nationality" runat="server" Text='<%#  Eval("Nationality")%>' ToolTip='<%#  Eval("Nationality")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateSource %>">
                    <ItemTemplate>
                        <asp:Label ID="lbl_CertificateSource" runat="server" Text='<%#  Eval("CertificateOrigin")%>' ToolTip='<%#  Eval("CertificateOrigin")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, SchoolType %>">
                    <ItemTemplate>
                        <asp:Label ID="lbl_SchoolType" runat="server" Text='<%#  Eval("SchoolType")%>' ToolTip='<%#  Eval("SchoolType")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateType %>">
                    <ItemTemplate>
                        <asp:Label ID="lbl_CertificateType" runat="server" Text='<%#  Eval("CertificateType")%>' ToolTip='<%#  Eval("CertificateType")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, DecisionCopy %>">
                    <ItemTemplate>
                        
                        <%--<asp:HyperLink runat="server"  >HyperLink</asp:HyperLink>--%>
                        <asp:LinkButton ID="lnk_View" runat="server" OnClientClick="LnkDownload(this.id)" ToolTip="<%$Resources:ITWORX_MOEHEWF_SCE, View %>" OnClick="lnk_View_Click" CssClass="display-icon fa fa-eye"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:ITWORX_MOEHEWF_SCE, ChooseAsAttach %>">
                    <ItemTemplate>
                        <asp:CheckBox ID="chk_attach" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div class="text-right">
            <asp:Button ID="btn_Save" runat="server" Visible="false" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SaveAttachedReq %>" OnClick="btn_Save_Click" />
        </div>
    </div>
</div>


<input type="hidden" value="" id="__EventTriggerSearchControl" name="__EventTriggerSearchControl" />
<script type="text/javascript">
<!--
    function LnkClickSearch(eventControl) {
        //  debugger;
        var ctlId = document.getElementById("__EventTriggerSearchControl");
        if (ctlId) {
            ctlId.value = eventControl;
        }

    }
    function LnkDownload(eventControl) {
        //  debugger;
        var ctlId = document.getElementById("__EventTriggerSearchControl");
        if (ctlId) {
            ctlId.value = eventControl;
        }

        window.WebForm_OnSubmit = function () { return true; };
    }
// -->
</script>
