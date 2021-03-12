<%@ Assembly Name="MOEHE.PSPES, Version=1.0.0.0, Culture=neutral, PublicKeyToken=677b80a9c9c0da8c" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ScheduleWindowUserControl.ascx.cs" Inherits="MOEHE.PSPES.Webparts.ScheduleWindow.ScheduleWindowUserControl" %>
<section>
    <asp:HiddenField ID="hdnSchoolCode" runat="server" />
    <div class="container bg-white-theme pt-0 pb-10 mt-50 mb-50">
        <div class="row">
            <!-- BEGIN EXAMPLE TABLE PORTLET-->
            <div class="portlet light ">
                <div class="portlet-title">
                    <div class="caption font-red">
                        <i class="fa fa-cogs font-red" aria-hidden="true" style="font-size: 20px"></i>
                        <span class="caption-subject bold uppercase">
                            <asp:Literal ID="Literal6" runat="server" Text="Close Pre-Enrollment and Share data to NSIS"></asp:Literal>
                        </span>
                    </div>
                </div>
                <fieldset class="scheduler-border m-0 mb-5 pb-5 pt-0 col-sm-12">
                    <legend class="scheduler-border  mb-5">
                        <asp:Literal runat="server" ID="ltSchoolDetails" Text='School Details'></asp:Literal>
                    </legend>
                    <div class="row m-0">
                        <!--Term-->
                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Literal runat="server" ID="ltTerm" Text="<%$Resources:MOEHE.PSPES,Term%>"></asp:Literal>
                                <asp:TextBox ID="txtTerm" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <!--Schol-->
                        <div class="col-sm-4">
                            <asp:Literal ID="ltSchool" runat="server" Text="<%$Resources:MOEHE.PSPES,School%>"></asp:Literal>
                            <asp:DropDownList ID="ddlSchool" Enabled="false" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row m-0">
                        <!--Close Date-->
                        <div class='col-sm-2'>
                            <div class="form-group">
                                <label><asp:Literal runat="server" ID="Literal81" Text='Pre-Enrollment Close Date'></asp:Literal></label>
                                <span class="required">* </span>
                                <div id="filterDate2">
                                    <!-- Datepicker as text field -->
                                    <div class="input-group date" data-date-format="dd/mm/yyyy">
                                        <input type="text" runat="server" onkeydown="return false;" id="dtCloseDate" class="form-control CloseDate" placeholder="dd/mm/yyyy">

                                        <div class="input-group-addon">
                                            <span class="glyphicon glyphicon-calendar"></span>
                                        </div>
                                    </div>
                                    <input type="text" id="ApplicationDateID" class="form-control ApplicationDate mb-0" name="CloseDate" style="color: #ffffff; background: transparent; border: none; height: 0px!important; width: 0px;">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row m-0">
                        <div class="col-sm-4"></div>
                        <div class="col-sm-3">
                            <!--User Name-->
                            <div class="form-group">
                                <asp:Literal runat="server" ID="ltUserName" Text='User Name:'></asp:Literal>
                                <asp:Label ID="txtUserName" runat="server" Enabled="false" />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <!--Transaction Date-->
                            <div class="form-group">
                                <asp:Literal runat="server" ID="ltTranDate" Text='Transaction Date:'></asp:Literal>
                                <asp:Label ID="txtDate" runat="server" Enabled="false" />
                            </div>
                        </div>
                    </div>
                    <div class="row m-0">
                        <div class="col-sm-10">
                            <!--Notes-->
                            <asp:Label ID="lblNotes" runat="server" CssClass="btn green mt-50 pr-100 pl-150 pull-center" Text="Close Preenrollment and share data to NSIS" />
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="scheduler-border m-0  mb-20 pb-5 pt-0 col-sm-12">
                <div class="col-md-2">
                    <asp:LinkButton ID="lbSubmit" CssClass="btn btn-xl btn-danger mt-30 pr-40 pl-40  pull-left" runat="server" OnClick="lbSubmit_Click">
                        <i class="fa fa-save mr-10"></i>
                        <asp:Literal runat="server" ID="ltSubmit" Text='<%$Resources:MOEHE.PSPES,Submit%>'></asp:Literal>
                    </asp:LinkButton>
                </div>
                <div class="col-md-2">
                    <asp:LinkButton ID="lbCancel" CssClass="btn btn-xl default mt-30 pr-40 pl-40 center-block width8em" runat="server" OnClick="lbCancel_Click">
                        <i class="fa fa-ban mr-10"></i>
                        <asp:Literal runat="server" ID="ltCancel" Text='<%$Resources:MOEHE.PSPES,Cancel%>'></asp:Literal>
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
</section>
<script>
    $(function () {
        $('.input-group.date').datepicker({
            format: "dd/mm/yyyy"
        });
    });
</script>
<%--<script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>--%>
