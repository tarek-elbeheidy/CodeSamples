<%@ Assembly Name="MOEHE.PSPES, Version=1.0.0.0, Culture=neutral, PublicKeyToken=677b80a9c9c0da8c" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditApplicationUserControl.ascx.cs" Inherits="MOEHE.PSPES.Webparts.EditApplication.EditApplicationUserControl" %>
<section>
    <div class="container mt-30 mb-30 pt-0 pb-30 bg-white-theme">
        <div class="row">
            <div class="col-md-12">
                <div class="single-service">
                    <div class="portlet light " id="form_wizard_1">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class="fa fa-pencil font-red"></i>
                                <span class="caption-subject font-red bold uppercase">
                                    <asp:Literal runat="server" ID="Literal808" Text='<%$Resources:MOEHE.PSPES,EditApplicationFormTitle%>'></asp:Literal>
                                </span>
                            </div>
                            <div class="actions">
                                <a class="btn btn-circle btn-icon-only btn-default hidden" href="javascript:;">
                                    <i class="icon-cloud-upload"></i>
                                </a>
                                <a class="btn btn-circle btn-icon-only btn-default hidden" href="javascript:;">
                                    <i class="icon-wrench"></i>
                                </a>
                                <a class="btn btn-circle btn-icon-only btn-default hidden" href="javascript:;">
                                    <i class="icon-trash"></i>
                                </a>
                            </div>
                        </div>
                        <div class="portlet-body form">
                            <div class="form-horizontal" id="aspnetForm">
                                <div class="form-wizard">
                                    <div class="form-body pb-0 mb-0">
                                        <ul class="nav nav-pills nav-justified steps">
                                            <li>
                                                <a href="#tab1" data-toggle="tab" style="cursor:default;" class="step">
                                                    <span class="desc">
                                                        <i class="fa fa-check "></i>
                                                        <asp:Literal runat="server" ID="Literal74" Text='<%$Resources:MOEHE.PSPES,PersonalInformation%>'></asp:Literal></span>
                                                </a>
                                            </li>
                                            <li>
                                                <a href="#tab2" style="cursor:default;" data-toggle="tab" class="step">
                                                    <span class="desc">
                                                        <i class="fa fa-check "></i>
                                                        <asp:Literal runat="server" ID="Literal75" Text='<%$Resources:MOEHE.PSPES,GuardianInformation%>'></asp:Literal></span>
                                                </a>
                                            </li>
                                            <li>
                                                <a href="#tab3" data-toggle="tab" style="cursor:default;" class="step">
                                                    <span class="desc">
                                                        <i class="fa fa-check "></i>
                                                        <asp:Literal runat="server" ID="Literal76" Text='<%$Resources:MOEHE.PSPES,HealthInformation%>'></asp:Literal></span>
                                                </a>
                                            </li>
                                            <li>
                                                <a href="#tab4" data-toggle="tab" style="cursor:default;" class="step">
                                                    <span class="desc">
                                                        <i class="fa fa-check "></i>
                                                        <asp:Literal runat="server" ID="Literal77" Text='<%$Resources:MOEHE.PSPES,RequiredDocuments%>'></asp:Literal>
                                                    </span>
                                                </a>
                                            </li>
                                        </ul>
                                        <div id="bar" class="progress progress-striped" role="progressbar">
                                            <div class="progress-bar progress-bar-success"></div>
                                        </div>
                                        <div class="tab-content">
                                            <div class="alert alert-danger display-none">
                                                <button class="close" data-dismiss="alert"></button>
                                                <asp:Literal runat="server" ID="Literal78" Text='<%$Resources:MOEHE.PSPES,SubmissionError%>'></asp:Literal>.
                                            </div>
                                            <div class="alert alert-success display-none">
                                                <button class="close" data-dismiss="alert"></button>
                                                <asp:Literal runat="server" ID="Literal79" Text='<%$Resources:MOEHE.PSPES,SubmissionSuccess%>'></asp:Literal>!
                                            </div>
                                            <div class="tab-pane active" id="tab1">
                                                <fieldset class="scheduler-border m-0 mb-5 pb-5 pt-0 col-sm-12">
                                                    <legend class="scheduler-border  mb-5">
                                                        <asp:Literal runat="server" ID="ltPersonalInformation" Text='<%$Resources:MOEHE.PSPES,PersonalInformation%>'></asp:Literal></legend>
                                                    <form class="p-0 mt-10" role="form">
                                                        <div class="row m-0">
                                                            <!--StudientQID-->
                                                            <div class="col-sm-2">
                                                                <div class="form-group">
                                                                    <label for="StudientQID">
                                                                        <asp:Literal runat="server" ID="Literal1" Text='<%$Resources:MOEHE.PSPES,QID%>'></asp:Literal>
                                                                        <span class="required">* </span>
                                                                    </label>

                                                                    <asp:TextBox ID="txtQID" AutoPostBack="true" CssClass="StudientQID form-control" runat="server" Enabled="false" OnTextChanged="txtQID_TextChanged"></asp:TextBox>
                                                                    <input type="text" id="StudientQID2" class="form-control StudientQID mb-0" name="username" style="color: #ffffff; background: transparent; border: none; height: 0px!important; width: 0px;">
                                                                </div>
                                                            </div>
                                                            <!--StudientName-->
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <label for="StudientName">
                                                                        <asp:Literal runat="server" ID="Literal2" Text='<%$Resources:MOEHE.PSPES,StudentName%>'></asp:Literal></label>
                                                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                     <input type="text" id="englishstudentname" runat="server" class="form-control mb-0"  style="color: #ffffff; background: transparent; border: none; height: 0px!important; width: 0px;">
                                                                    <input type="text" id="arabicstudentname" runat="server" class="form-control mb-0"  style="color: #ffffff; background: transparent; border: none; height: 0px!important; width: 0px;">
                                                                </div>
                                                            </div>
                                                            <!--StudientGender-->
                                                            <div class="col-sm-2">
                                                                <div class="form-group">
                                                                    <label for="StudientGender">
                                                                        <asp:Literal runat="server" ID="Literal3" Text='<%$Resources:MOEHE.PSPES,Gender%>'></asp:Literal></label>
                                                                    <asp:TextBox ID="txtGender" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <!--StudientDOB-->
                                                            <div class="col-sm-2">
                                                                <div class="form-group">
                                                                    <label for="StudientDOB">
                                                                        <asp:Literal runat="server" ID="Literal4" Text='<%$Resources:MOEHE.PSPES,DOB%>'></asp:Literal></label>

                                                                    <asp:TextBox ID="txtDOB" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>

                                                                </div>
                                                            </div>
                                                            <!--StudientCountry-->
                                                            <div class="col-sm-2">
                                                                <div class="form-group">
                                                                    <label for="StudientCountry">
                                                                        <asp:Literal runat="server" ID="Literal5" Text='<%$Resources:MOEHE.PSPES,Nationality%>'></asp:Literal></label>
                                                                    <asp:TextBox ID="txtNationality" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                        </div>
                                                </fieldset>
                                                <fieldset class="scheduler-border m-0 mb-5 pb-5 pt-0 col-sm-12">
                                                    <legend class="scheduler-border mb-5">
                                                        <asp:Literal runat="server" ID="Literal6" Text='<%$Resources:MOEHE.PSPES,ApplicationInformation%>'></asp:Literal></legend>
                                                    <h4 class="line-bottom-theme-colored-2 mt-10 mb-5 ml-20 hidden">
                                                        <asp:Literal runat="server" ID="Literal7" Text='<%$Resources:MOEHE.PSPES,PRVYearsAcademicInfo%>'></asp:Literal></h4>
                                                    <div class="row m-0">

                                                        <!--ApplicationREF-->
                                                        <div class="col-sm-2">
                                                            <div class="form-group">
                                                                <label for="ApplicationREF">
                                                                    <asp:Literal runat="server" ID="Literal8" Text='<%$Resources:MOEHE.PSPES,ApplicationRefNo%>'></asp:Literal></label>
                                                                <asp:TextBox Enabled="False" ID="txtApplicationRefNo" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <!--ApplicationDate-->
                                                        <div class='col-sm-2'>
                                                            <div class="form-group">
                                                                <label for="TimeTestReject"><asp:Literal runat="server" ID="Literal81" Text='<%$Resources:MOEHE.PSPES,ApplicationDate%>'></asp:Literal></label>
                                                                <span class="required">* </span>
                                                                <div id="filterDate2">
                                                                    <!-- Datepicker as text field -->
                                                                    <div class="input-group date" data-date-format="dd/mm/yyyy">
                                                                        <input type="text" disabled  runat="server" onkeydown="return false;" id="dtApplicationDate2" class="form-control ApplicationDate"  placeholder="dd/mm/yyyy">
                                                                        
                                                                        <div class="input-group-addon">
                                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                                        </div>
                                                                    </div>
                                                                    <input type="text" id="ApplicationDateID" class="form-control ApplicationDate mb-0" name="applicationDate" style="color: #ffffff; background: transparent; border: none; height: 0px!important; width: 0px;">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <script>
                                                            $(function () {
                                                                $('.input-group.date').datepicker({
                                                                    format: "dd/mm/yyyy"
                                                                });
                                                            });
                                                        </script>

                                                        <!--PreEnrollmentTerm-->
                                                        <div class="col-sm-2">
                                                            <asp:Literal runat="server" ID="Literal10" Text='<%$Resources:MOEHE.PSPES,Term%>'></asp:Literal>
                                                            <div class="form-group">
                                                                <asp:DropDownList ID="ddlPreEnrollmentTerm" disabled runat="server" CssClass="form-control pt-0">
                                                                </asp:DropDownList>

                                                            </div>
                                                        </div>
                                                        <!--PreEnrollmentSchool-->
                                                        <div class="col-sm-4">
                                                            <div class="form-group">
                                                                <label for="PreEnrollmentSchool">
                                                                    <asp:Literal runat="server" ID="Literal11" Text='<%$Resources:MOEHE.PSPES,School%>'></asp:Literal></label>

                                                                <asp:TextBox ID="txtPreEnrollmentSchool" Enabled="false" runat="server" CssClass="form-control disabled"></asp:TextBox>
                                                                <asp:Label ID="lblPreEnrollmentSchoolCurriculumID" runat="server" CssClass="hidden"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <!--PreEnrollmentGrade-->
                                                        <div class="col-sm-2">
                                                            <div class="form-group">
                                                                <label for="PreEnrollmentTerm">
                                                                    <asp:Literal runat="server" ID="Literal12" Text='<%$Resources:MOEHE.PSPES,RequestedGrade%>'></asp:Literal><span class="required" aria-required="true">* </span></label>
                                                                <asp:DropDownList ID="ddlPreEnrolmentGrade" disabled runat="server" CssClass="form-control preenrolmentgrade pt-0" AutoPostBack="True" OnSelectedIndexChanged="ddlPreEnrolmentGrade_SelectedIndexChanged"></asp:DropDownList>
                                                                <input type="text" id="preenrolmentgradeValid" class="form-control" name="CurrentResult" style="color: #ffffff; background: transparent; border: none; height: 0px!important; width: 0px;">
                                                            </div>
                                                        </div>
                                                        <!--ResidentialArea-->
                                                        <div class="col-sm-2">
                                                            <div class="form-group">
                                                                <label for="ResidentialArea">
                                                                    <asp:Literal runat="server" ID="Literal13" Text='<%$Resources:MOEHE.PSPES,ResidentialArea%>'></asp:Literal><span class="required">* </span></label>
                                                                <asp:TextBox ID="txtResedentialArea" runat="server" CssClass="form-control ResedentialAreaV"></asp:TextBox>
                                                                 <input type="text" id="ResedentialAreaID" class="form-control ResedentialAreaV" name="ResedentialArea" style="color: #ffffff; background: transparent; border: none; height: 0px!important; width: 0px;">
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2 ">
                                                            <div class="form-group">
                                                                <!--TestResult-->
                                                                <label for="TestResult">
                                                                    <asp:Literal runat="server" ID="Literal14" Text='<%$Resources:MOEHE.PSPES,TransportationRequired%>'></asp:Literal></label>
                                                                <asp:DropDownList ID="ddlTransportation" runat="server" CssClass="form-control pt-0">
                                                                    <asp:ListItem Text='<%$Resources:MOEHE.PSPES,PleaseSelect%>' Value="-1"></asp:ListItem>
                                                                    <asp:ListItem Text='<%$Resources:MOEHE.PSPES,Yes%>' Value="true"></asp:ListItem>
                                                                    <asp:ListItem Text='<%$Resources:MOEHE.PSPES,No%>' Value="false"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2" hidden>
                                                            <div class="form-group">
                                                                <label for="Grade-Vacancy">
                                                                    <asp:Literal runat="server" ID="Literal555" Text='<%$Resources:MOEHE.PSPES,AvailableSeatsInformation%>'></asp:Literal></label>
                                                                <input type="text" class="form-control" runat="server" id="txtAvailableSeatsInRequestedGrade" value="" disabled>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2" hidden>
                                                            <div class="form-group">
                                                                <label for="FiltreSchool">
                                                                    <asp:Literal runat="server" ID="Literal17" Text='<%$Resources:MOEHE.PSPES,WaitListNumber%>'></asp:Literal></label>
                                                                <input type="text" class="form-control" value="" runat="server" id="txtWaitListNumber" disabled>
                                                            </div>
                                                        </div>
                                                    </row>
                                                </fieldset>
                                                <fieldset class="scheduler-border m-0 mb-10 col-sm-12">
                                                    <legend class="scheduler-border mb-0">
                                                        <asp:Literal runat="server" ID="Literal18" Text='<%$Resources:MOEHE.PSPES,AcademicInformation%>'></asp:Literal></legend>
                                                    <h4 class="line-bottom-theme-colored-2 mt-5 mb-5 ml-20">
                                                        <asp:Literal runat="server" ID="Literal19" Text='<%$Resources:MOEHE.PSPES,CurrentYearAcademicInformation%>'></asp:Literal></h4>
                                                    <div class="row">
                                                        <div class="row p-0 m-15">
                                                            <!--CurrentSchool-->
                                                            <div class="col-sm-1  width120">
                                                                <div class="form-group">
                                                                    <label for="CurrentSchoolCode">
                                                                        <asp:Literal runat="server" ID="Literal48" Text='<%$Resources:MOEHE.PSPES,SchoolCode%>'></asp:Literal></label>
                                                                    <asp:TextBox ID="txtCurrentSchoolCode" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <div class="form-group">
                                                                    <label for="CurrentSchool">
                                                                        <asp:Literal runat="server" ID="Literal20" Text='<%$Resources:MOEHE.PSPES,CurrentSchool%>'></asp:Literal></label>
                                                                    <asp:TextBox ID="txtCurrentSchool" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <!--CurrentResult-->
                                                            <div class="col-sm-2">
                                                                <div class="form-group">
                                                                    <asp:Literal ID="ltCurrentResult" runat="server" Text="<%$Resources:MOEHE.PSPES,Result%>"></asp:Literal>
                                                                    <asp:DropDownList ID="ddlCurrentYearResult" runat="server" disabled CssClass="form-control pt-0">
                                                                        <asp:ListItem Text='<%$Resources:MOEHE.PSPES,PleaseSelect%>' Value="-1"></asp:ListItem>
                                                                        <asp:ListItem Text='<%$Resources:MOEHE.PSPES,ResultPass%>' Value="True"></asp:ListItem>
                                                                        <asp:ListItem Text='<%$Resources:MOEHE.PSPES,ResultFail%>' Value="False"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <!--CurrentTerm-->
                                                            <div class="col-sm-2">
                                                                <asp:Literal runat="server" ID="Literal21" Text='<%$Resources:MOEHE.PSPES,CurrentTerm%>'></asp:Literal>
                                                                <div class="form-group">
                                                                    <asp:DropDownList ID="ddlCurrentTerm" runat="server" CssClass="form-control pt-0">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <!--CurrentCurriculum-->
                                                            <div class="col-sm-2">
                                                                <asp:Literal runat="server" ID="Literal23" Text='<%$Resources:MOEHE.PSPES,CurrentCurriculum%>'></asp:Literal>
                                                                <div class="form-group">
                                                                    <asp:DropDownList ID="ddlCurrentCurriculum" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCurrentCurriculum_SelectedIndexChanged" CssClass="form-control pt-0">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <!--CurrentGrade-->
                                                            <div class="col-sm-2">
                                                                <asp:Literal runat="server" ID="Literal22" Text='<%$Resources:MOEHE.PSPES,CurrentGrade%>'></asp:Literal>
                                                                <div class="form-group">
                                                                    <asp:DropDownList ID="ddlCurrentGrade" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCurrentGrade_SelectedIndexChanged" CssClass="form-control pt-0">
                                                                        <asp:ListItem Text='<%$Resources:MOEHE.PSPES,PleaseSelect%>' Value="-1"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <h4 class="line-bottom-theme-colored-2 mt-5 mb-5 ml-20">
                                                        <asp:Literal runat="server" ID="Literal24" Text='<%$Resources:MOEHE.PSPES,PRVYearsAcademicInfo%>'></asp:Literal></h4>


                                                    <div class="row">
                                                        <div class="row p-0 m-15">
                                                            <!--PreviousSchool-->
                                                            <div class="col-sm-1  width120">
                                                                <div class="form-group">
                                                                    <label for="CurrentSchoolCode">
                                                                        <asp:Literal runat="server" ID="Literal9" Text='<%$Resources:MOEHE.PSPES,SchoolCode%>'></asp:Literal></label>
                                                                    <asp:TextBox ID="txtPreviousSchoolCode" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <div class="form-group">
                                                                    <label for="CurrentSchool">
                                                                        <asp:Literal runat="server" ID="Literal25" Text='<%$Resources:MOEHE.PSPES,PreviousSchool%>'></asp:Literal></label>
                                                                    <asp:TextBox ID="txtPreviousSchool" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <!--CurrentResult-->
                                                            <div class="col-sm-2">
                                                                <div class="form-group">
                                                                    <asp:Literal ID="Literal16" runat="server" Text="<%$Resources:MOEHE.PSPES,Result%>"></asp:Literal>
                                                                    <asp:DropDownList ID="ddlPreviousYearResult" disabled runat="server" CssClass="form-control pt-0">
                                                                        <asp:ListItem Text='<%$Resources:MOEHE.PSPES,PleaseSelect%>' Value="-1"></asp:ListItem>
                                                                        <asp:ListItem Text='<%$Resources:MOEHE.PSPES,ResultPass%>' Value="True"></asp:ListItem>
                                                                        <asp:ListItem Text='<%$Resources:MOEHE.PSPES,ResultFail%>' Value="False"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <!--PreviousTerm-->
                                                            <div class="col-sm-2">
                                                                <asp:Literal runat="server" ID="Literal26" Text='<%$Resources:MOEHE.PSPES,PreviousTerm%>'></asp:Literal>
                                                                <div class="form-group">
                                                                    <asp:DropDownList ID="ddlPreviousTerm" runat="server" CssClass="form-control pt-0">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <!--PreviousCurriculum-->
                                                            <div class="col-sm-2">
                                                                <asp:Literal runat="server" ID="Literal28" Text='<%$Resources:MOEHE.PSPES,PreviousCurriculum%>'></asp:Literal>
                                                                <div class="form-group">
                                                                    <asp:DropDownList ID="ddlPreviousCurriculum" CssClass="form-control pt-0" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlPreviousCurriculum_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <!--PreviousGrade-->
                                                            <div class="col-sm-2">
                                                                <asp:Literal runat="server" ID="Literal27" Text='<%$Resources:MOEHE.PSPES,PreviousGrade%>'></asp:Literal>
                                                                <div class="form-group">
                                                                    <asp:DropDownList ID="ddlPreviousGrade" CssClass="form-control pt-0" runat="server">
                                                                        <asp:ListItem Text='<%$Resources:MOEHE.PSPES,PleaseSelect%>' Value="-1"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </fieldset>
                                            </div>
                                            <div class="tab-pane" id="tab2">
                                                <fieldset class="scheduler-border m-0 mb-5 pb-5 pt-0 col-sm-12">
                                                    <legend class="scheduler-border mb-0">
                                                        <asp:Literal runat="server" ID="Literal30" Text='<%$Resources:MOEHE.PSPES,PersonalInformation%>'></asp:Literal></legend>
                                                    <form class="p-0 mt-10" role="form">
                                                        <div class="row m-0">
                                                            <!--StudientQID-->
                                                            <div class="col-sm-2">
                                                                <div class="form-group">
                                                                    <label for="StudientQID">
                                                                        <asp:Literal runat="server" ID="Literal29" Text='<%$Resources:MOEHE.PSPES,QID%>'></asp:Literal></label>

                                                                    <asp:TextBox ID="txtQID2" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <!--StudientName-->
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <label for="StudientName">
                                                                        <asp:Literal runat="server" ID="Literal31" Text='<%$Resources:MOEHE.PSPES,StudentName%>'></asp:Literal></label>
                                                                    <asp:TextBox ID="txtName2" runat="server" CssClass="form-control disabled" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <!--StudientGender-->
                                                            <div class="col-sm-2">
                                                                <div class="form-group">
                                                                    <label for="StudientGender">
                                                                        <asp:Literal runat="server" ID="Literal32" Text='<%$Resources:MOEHE.PSPES,Gender%>'></asp:Literal></label>
                                                                    <asp:TextBox ID="txtGender2" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <!--StudientDOB-->
                                                            <div class="col-sm-2">
                                                                <div class="form-group">
                                                                    <label for="StudientDOB">
                                                                        <asp:Literal runat="server" ID="Literal33" Text='<%$Resources:MOEHE.PSPES,DOB%>'></asp:Literal></label>

                                                                    <asp:TextBox ID="txtDOB2" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>

                                                                </div>
                                                            </div>
                                                            <!--StudientCountry-->
                                                            <div class="col-sm-2">
                                                                <div class="form-group">
                                                                    <label for="StudientCountry">
                                                                        <asp:Literal runat="server" ID="Literal34" Text='<%$Resources:MOEHE.PSPES,Nationality%>'></asp:Literal></label>
                                                                    <asp:TextBox ID="txtNationality2" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <!--StudientNationality-->
                                                        </div>
                                                </fieldset>
                                                <fieldset class="scheduler-border m-0  mb-20 pb-5 pt-0 col-sm-12">
                                                    <legend class="scheduler-border mb-0">
                                                        <asp:Literal runat="server" ID="Literal35" Text='<%$Resources:MOEHE.PSPES,GuardianInformation%>'></asp:Literal></legend>

                                                    <div class="row m-0">
                                                        <div class="col-sm-2">
                                                            <!--GuardianQID-->
                                                            <div class="form-group">
                                                                <label for="GuardianQID">
                                                                    <asp:Literal runat="server" ID="Literal36" Text='<%$Resources:MOEHE.PSPES,QID%>'></asp:Literal>
                                                                    <span class="required">* </span>
                                                                </label>
                                                                <asp:TextBox ID="txtGuardianQID" CssClass="GuardianQID form-control" AutoPostBack="true" runat="server" OnTextChanged="txtGuardianQID_TextChanged"></asp:TextBox>
                                                                 <input type="text" id="GuardianQID_Old" runat="server" class="mb-0"  style="color: #ffffff; background: transparent; border: none; height: 0px!important; width: 0px;">
                                                                <input type="text" id="GuardianQID2" class="form-control GuardianQID mb-0" name="guardianusername" style="color: #ffffff; background: transparent; border: none; height: 0px!important; width: 0px;">
                                                            </div>
                                                        </div>
                                                        <!--GuardianName-->
                                                        <div class="col-sm-4">
                                                            <div class="form-group">
                                                                <label for="GuardianName">
                                                                    <asp:Literal runat="server" ID="Literal37" Text='<%$Resources:MOEHE.PSPES,GuardianName%>'></asp:Literal></label>
                                                                <input type="text" class="form-control" runat="server" id="GuardianName" disabled>
                                                                 <input type="text" id="englishguardianname" runat="server" class="form-control mb-0"  style="color: #ffffff; background: transparent; border: none; height: 0px!important; width: 0px;">
                                                                    <input type="text" id="arabicguardianname" runat="server" class="form-control mb-0"  style="color: #ffffff; background: transparent; border: none; height: 0px!important; width: 0px;">
                                                                
                                                            </div>
                                                        </div>
                                                        <!--GuardianGender-->
                                                        <div class="col-sm-2">
                                                            <div class="form-group">
                                                                <label for="GuardianGender">
                                                                    <asp:Literal runat="server" ID="Literal38" Text='<%$Resources:MOEHE.PSPES,Gender%>'></asp:Literal></label>
                                                                    <asp:TextBox runat="server" disabled ID="txtGuardianGender" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <!--GuardianCountry-->
                                                        <div class="col-sm-2">
                                                            <div class="form-group">
                                                                <label for="GuardianCountry">
                                                                    <asp:Literal runat="server" ID="Literal39" Text='<%$Resources:MOEHE.PSPES,Nationality%>'></asp:Literal></label>
                                                                <input type="text" class="form-control" runat="server" id="GuardianCountry" disabled>
                                                            </div>
                                                        </div>
                                                        <!--GuardianRelationship-->
                                                        <div class="col-sm-2">
                                                            <div class="form-group">
                                                                <label for="GuardianRelationship">
                                                                    <asp:Literal runat="server" ID="Literal40" Text='<%$Resources:MOEHE.PSPES,Relationship%>'></asp:Literal></label>
                                                                <asp:DropDownList ID="ddlGuardianRelationship" runat="server" CssClass="form-control pt-0"></asp:DropDownList>

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row m-0">
                                                        <!--GuardianMaritalStatus-->
                                                        <div class="col-sm-3">
                                                            <div class="form-group">
                                                                <label for="GuardianMaritalStatus">
                                                                    <asp:Literal runat="server" ID="Literal41" Text='<%$Resources:MOEHE.PSPES,MaritalStatus%>'></asp:Literal></label>
                                                                <asp:DropDownList ID="ddlGuardianMaritalStatus" runat="server" CssClass="form-control pt-0"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <!--GuardianMobile-->
                                                        <div class="col-sm-3">
                                                            <div class="form-group">
                                                                <label for="GuardianMobile">
                                                                        <asp:Literal runat="server" ID="Literal42" Text='<%$Resources:MOEHE.PSPES,MobileNumber%>'></asp:Literal>
                                                                        <span class="required">* </span>
                                                                    </label>
                                                                <asp:TextBox ID="txtGuardianMobile" CssClass="form-control GuardianMobile" runat="server"></asp:TextBox>

                                                                <input type="text" id="phoneValid" class="form-control GuardianMobile mb-0" name="guardianphone" style="color: #ffffff; background: transparent; border: none; height: 0px!important; width: 0px;">
                                                            </div>
                                                        </div>
                                                        <!--GuardianLandLine-->
                                                        <div class="col-sm-3">
                                                            <div class="form-group">
                                                                <label for="GuardianLandLine">
                                                                    <asp:Literal runat="server" ID="Literal43" Text='<%$Resources:MOEHE.PSPES,Landline%>'></asp:Literal></label>
                                                                <input type="text" class="form-control" runat="server" id="GuardianLandLine">
                                                            </div>
                                                        </div>
                                                        <!--GuardianEmail-->
                                                        <div class="col-sm-3">
                                                            <div class="form-group">
                                                                <label for="GuardianEmail">
                                                                    <asp:Literal runat="server" ID="Literal44" Text='<%$Resources:MOEHE.PSPES,Email%>'></asp:Literal></label>
                                                                <input type="text" class="form-control" runat="server" id="GuardianEmail">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <h4 class="line-bottom-theme-colored-2 mt-5 mb-20 ml-20">
                                                        <asp:Literal runat="server" ID="Literal45" Text='<%$Resources:MOEHE.PSPES,EmploymentInformation%>'></asp:Literal></h4>
                                                    <div class="row m-0">
                                                        <!--GuardianEmployerType-->
                                                        <div class="col-sm-3">
                                                            <div class="form-group">
                                                                <label for="GuardianEmployerType">
                                                                    <asp:Literal runat="server" ID="Literal46" Text='<%$Resources:MOEHE.PSPES,EmploymentType%>'></asp:Literal></label>

                                                                <asp:DropDownList ID="ddlGuardianEmployerType" AutoPostBack="true" runat="server" CssClass="form-control pt-0" OnSelectedIndexChanged="ddlGuardianEmployerType_SelectedIndexChanged"></asp:DropDownList>

                                                            </div>
                                                        </div>
                                                        <!--GuardianEmployer-->
                                                        <div class="col-sm-5">
                                                            <div class="form-group">
                                                                <label for="GuardianEmployer">
                                                                    <asp:Literal runat="server" ID="Literal47" Text='<%$Resources:MOEHE.PSPES,Employer%>'></asp:Literal></label>
                                                                <asp:DropDownList ID="ddlGuardianEmployer" runat="server" CssClass="form-control pt-0"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </fieldset>


                                                <div class="row m-0">

                                                    <asp:GridView ID="gvParents" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" ShowFooter="false" OnRowDeleting="gvParents_RowDeleting" OnRowEditing="EditParent" OnRowUpdating="UpdateParent" OnDataBound="gvParents_DataBound" OnRowCancelingEdit="CancelEdit" OnRowCommand="gvParents_RowCommand" OnRowDataBound="gvParents_RowDataBound">

                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="Literal31" Text='<%$Resources:MOEHE.PSPES,QID%>'></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="ItxtParentID" Enabled="false" Text='<%# decimal.Truncate((decimal)Eval("MOE_RELATED_QID")).ToString() %>' runat="server"></asp:TextBox>
                                                                    <asp:TextBox ID="ItxtParentID_Old" Enabled="false" CssClass="hidden" Text='<%# decimal.Truncate((decimal)Eval("MOE_OLD_RELATED_QID")).ToString() %>' runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="NtxtParentID" runat="server"></asp:TextBox>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="Literal31" Text='<%$Resources:MOEHE.PSPES,Name%>'></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="ItxtParentName" Enabled="false" Text='<%# ((uint)System.Globalization.CultureInfo.CurrentUICulture.LCID == 1025)? Eval("MOE_ARABIC_NAME"): Eval("MOE_ENGLISH_NAME") %>' runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="NtxtParentName" runat="server"></asp:TextBox>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="Literal31" Text='<%$Resources:MOEHE.PSPES,Nationality%>'></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="ItxtCountry" Enabled="false" Text='<%# ((uint)System.Globalization.CultureInfo.CurrentUICulture.LCID == 1025)? Eval("MOE_COUNTRY_ARABIC_NAME"): Eval("MOE_COUNTRY_ENGLISH_NAME")  %>' runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="NtxtCountry" runat="server"></asp:TextBox>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="Literal31" Text='<%$Resources:MOEHE.PSPES,Relationship%>'></asp:Literal>
                                                                </HeaderTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="EddlRelationship">
                                                                    </asp:DropDownList>
                                                                    <asp:Label runat="server" ID="ElblRelationID" Text='<%# Eval("MOE_RELATIONSHIP_TYPE_ID") %>' CssClass="hidden"></asp:Label>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="IddlRelationship">
                                                                    </asp:DropDownList>
                                                                    <asp:Label runat="server" ID="IlblRelationID" Text='<%# Eval("MOE_RELATIONSHIP_TYPE_ID") %>' CssClass="hidden"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="NddlRelationship">
                                                                    </asp:DropDownList>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="Literal31" Text='<%$Resources:MOEHE.PSPES,MobileNumber%>'></asp:Literal>
                                                                </HeaderTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="EtxtParentMobileNumber" Text='<%# Eval("MOE_MOBILE_CONTACT_NBR") %>' runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="ItxtParentMobileNumber" Enabled="false" Text='<%# Eval("MOE_MOBILE_CONTACT_NBR") %>' runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="NtxtParentMobileNumber" Text='<%# Eval("MOE_MOBILE_CONTACT_NBR") %>' runat="server"></asp:TextBox>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="Literal31" Text='<%$Resources:MOEHE.PSPES,Email%>'></asp:Literal>
                                                                </HeaderTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="EtxtParentEmail" Text='<%# Eval("MOE_EMAIL") %>' runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="ItxtParentEmail" Enabled="false" Text='<%# Eval("MOE_EMAIL") %>' runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="NtxtParentEmail" runat="server"></asp:TextBox>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:CommandField ShowEditButton="True" EditText='<%$Resources:MOEHE.PSPES,Edit%>' />



                                                        </Columns>

                                                    </asp:GridView>



                                                </div>
                                            </div>
                                            <div class="tab-pane" id="tab3">
                                                <fieldset class="scheduler-border m-0 mb-5 pb-5 pt-0 col-sm-12">
                                                    <legend class="scheduler-border mb-0">
                                                        <asp:Literal runat="server" ID="Literal52" Text='<%$Resources:MOEHE.PSPES,PersonalInformation%>'></asp:Literal></legend>
                                                    <form class="p-0 mt-10" role="form">
                                                        <div class="row m-0">
                                                            <!--StudientQID-->
                                                            <div class="col-sm-2">
                                                                <div class="form-group">
                                                                    <label for="StudientQID">
                                                                        <asp:Literal runat="server" ID="Literal53" Text='<%$Resources:MOEHE.PSPES,QID%>'></asp:Literal></label>

                                                                    <asp:TextBox ID="txtQID3" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <!--StudientName-->
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <label for="StudientName">
                                                                        <asp:Literal runat="server" ID="Literal54" Text='<%$Resources:MOEHE.PSPES,StudentName%>'></asp:Literal></label>
                                                                    <asp:TextBox ID="txtName3" runat="server" CssClass="form-control disabled" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <!--StudientGender-->
                                                            <div class="col-sm-2">
                                                                <div class="form-group">
                                                                    <label for="StudientGender">
                                                                        <asp:Literal runat="server" ID="Literal55" Text='<%$Resources:MOEHE.PSPES,Gender%>'></asp:Literal></label>
                                                                    <asp:TextBox ID="txtGender3" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <!--StudientDOB-->
                                                            <div class="col-sm-2">
                                                                <div class="form-group">
                                                                    <label for="StudientDOB">
                                                                        <asp:Literal runat="server" ID="Literal56" Text='<%$Resources:MOEHE.PSPES,DOB%>'></asp:Literal></label>

                                                                    <asp:TextBox ID="txtDOB3" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>

                                                                </div>
                                                            </div>
                                                            <!--StudientCountry-->
                                                            <div class="col-sm-2">
                                                                <div class="form-group">
                                                                    <label for="StudientCountry">
                                                                        <asp:Literal runat="server" ID="Literal57" Text='<%$Resources:MOEHE.PSPES,Nationality%>'></asp:Literal></label>
                                                                    <asp:TextBox ID="txtNationality3" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                </fieldset>
                                                <fieldset class="scheduler-border m-0 mb-5 pb-5 pt-0 col-sm-12">
                                                    <legend class="scheduler-border b-5">
                                                        <asp:Literal runat="server" ID="Literal58" Text='<%$Resources:MOEHE.PSPES,HealthInformation%>'></asp:Literal></legend>

                                                    <div class="row m-0">
                                                        <!--StudentHealthCart-->
                                                        <div class="col-sm-2">
                                                            <div class="form-group">
                                                                <label for="StudentHealthCart">
                                                                    <asp:Literal runat="server" ID="Literal59" Text='<%$Resources:MOEHE.PSPES,HCNumber%>'></asp:Literal></label>
                                                                <input type="text" id="txtStudentHealthCard" runat="server" class="form-control">
                                                            </div>
                                                        </div>
                                                        <!--HealthCenterName-->
                                                        <div class="col-sm-4">
                                                            <div class="form-group">
                                                                <label for="HealthCenterName">
                                                                    <asp:Literal runat="server" ID="Literal60" Text='<%$Resources:MOEHE.PSPES,HealthCenter%>'></asp:Literal></label>
                                                                <input type="text" id="txtHealthCenterName" runat="server" class="form-control">
                                                            </div>
                                                        </div>
                                                        <!--SpecialNeed-->
                                                        <div class="col-sm-2">
                                                            <div class="form-group">
                                                                <asp:Literal ID="Literal50" runat="server" Text="<%$Resources:MOEHE.PSPES,SpecialNeed%>"></asp:Literal>
                                                                <asp:DropDownList ID="ddlSpecialNeed" runat="server" CssClass="form-control specialneed pt-0">
                                                                    <asp:ListItem Text='<%$Resources:MOEHE.PSPES,PleaseSelect%>' Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text='<%$Resources:MOEHE.PSPES,Yes%>' Value="true"></asp:ListItem>
                                                                    <asp:ListItem Text='<%$Resources:MOEHE.PSPES,No%>' Value="false"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <input type="text" id="specialneedValid" runat="server" class="form-control specialneedValid" name="specialneed" style="color: #ffffff; background: transparent; border: none; height: 0px!important; width: 0px;">
                                                            </div>
                                                        </div>
                                                        <!--LearningDifficulties-->
                                                        <div class="col-sm-2">
                                                            <div class="form-group">
                                                                <asp:Literal ID="Literal51" runat="server" Text="<%$Resources:MOEHE.PSPES,LearningDifficulties%>"></asp:Literal>
                                                                <asp:DropDownList ID="ddlLearningDifficulties" runat="server" CssClass="form-control learningdifficulties pt-0">
                                                                    <asp:ListItem Text='<%$Resources:MOEHE.PSPES,PleaseSelect%>' Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text='<%$Resources:MOEHE.PSPES,Yes%>' Value="true"></asp:ListItem>
                                                                    <asp:ListItem Text='<%$Resources:MOEHE.PSPES,No%>' Value="false"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <input type="text" id="learningdifficultiesValid" runat="server" class="form-control learningdifficultiesValid" name="learningdifficulties" style="color: #ffffff; background: transparent; border: none; height: 0px!important; width: 0px;">
                                                            </div>
                                                        </div>
                                                        <!--PHCCData-->
                                                        <div class="col-sm-2 ">
                                                            <div class="form-group">
                                                                <label for="PHCCData"><span class="agree"></span></label>
                                                                <div class="sec-filters-item check-item">
                                                                    <input id="PHCCData" type="checkbox" name="chkPHCCData" runat="server" aria-label="Checkbox for following text input" class="form-control check-item-mark">

                                                                    <asp:Literal runat="server" ID="Literal61" Text='<%$Resources:MOEHE.PSPES,FitForSchooling%>'></asp:Literal>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row m-0">
                                                        <!--HealthCenterName-->
                                                        <div class="col-sm-12 mt-0 pt-0">
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <div class="sec-filters-item check-item">
                                                                        <input id="HealthIssue" type="checkbox" name="chkHealthIssue" onclick="EnableText()" runat="server" aria-label="Checkbox for following text input" class="form-control check-item-mark phcccheck mt-0">
                                                                        <label for="HealthIssue" class="agree mt-5">
                                                                            <span class="agree">
                                                                                <asp:Literal runat="server" ID="Literal62" Text='<%$Resources:MOEHE.PSPES,PreviousHealthIssues%>'></asp:Literal>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <textarea id="MentionHealthIssues" disabled="true" runat="server" class="form-control health-area textaria-health" rows="4" placeholder="Files Please"></textarea>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </div>
                                            <div class="tab-pane" id="tab4">
                                                <fieldset class="scheduler-border m-0 mb-5 pb-5 pt-0 col-sm-12">
                                                    <legend class="scheduler-border mb-0">
                                                        <asp:Literal runat="server" ID="Literal63" Text='<%$Resources:MOEHE.PSPES,PersonalInformation%>'></asp:Literal></legend>
                                                    <form class="p-0 mt-10" role="form">
                                                        <div class="row m-0">
                                                            <!--StudientQID-->
                                                            <div class="col-sm-2">
                                                                <div class="form-group">
                                                                    <label for="StudientQID">
                                                                        <asp:Literal runat="server" ID="Literal64" Text='<%$Resources:MOEHE.PSPES,QID%>'></asp:Literal>
                                                                    </label>

                                                                    <asp:TextBox ID="txtQID4" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <!--StudientName-->
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <label for="StudientName">
                                                                        <asp:Literal runat="server" ID="Literal65" Text='<%$Resources:MOEHE.PSPES,StudentName%>'></asp:Literal></label>
                                                                    <asp:TextBox ID="txtName4" runat="server" CssClass="form-control disabled" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <!--StudientGender-->
                                                            <div class="col-sm-2">
                                                                <div class="form-group">
                                                                    <label for="StudientGender">
                                                                        <asp:Literal runat="server" ID="Literal66" Text='<%$Resources:MOEHE.PSPES,Gender%>'></asp:Literal></label>
                                                                    <asp:TextBox ID="txtGender4" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <!--StudientDOB-->
                                                            <div class="col-sm-2">
                                                                <div class="form-group">
                                                                    <label for="StudientDOB">
                                                                        <asp:Literal runat="server" ID="Literal67" Text='<%$Resources:MOEHE.PSPES,DOB%>'></asp:Literal></label>
                                                                    <asp:TextBox ID="txtDOB4" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>

                                                                </div>
                                                            </div>
                                                            <!--StudientCountry-->
                                                            <div class="col-sm-2">
                                                                <div class="form-group">
                                                                    <label for="StudientCountry">
                                                                        <asp:Literal runat="server" ID="Literal68" Text='<%$Resources:MOEHE.PSPES,Nationality%>'></asp:Literal></label>
                                                                    <asp:TextBox ID="txtNationality4" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <!--StudientNationality-->
                                                        </div>
                                                </fieldset>
                                                <fieldset id="template" class="scheduler-border m-0 mb-5 pb-5 pt-0 col-sm-12">
                                                    <legend class="scheduler-border mb-5">
                                                        <asp:Literal runat="server" ID="Literal69" Text='<%$Resources:MOEHE.PSPES,RequiredDocuments%>'></asp:Literal></legend>

                                                    <asp:GridView ID="gvRequiredDocuments" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" runat="server">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="Literal69" Text='<%$Resources:MOEHE.PSPES,DocumentName%>'></asp:Literal></HeaderTemplate>

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRequiredDocumentName" CssClass="control-label ml-10 mb-5" runat="server"></asp:Label>
                                                                    <asp:Label ID="ArabicDocumentTypeLabel" runat="server" Visible='<%# Eval("ShowArabic") %>' Text='<%# Eval("ArabicDocumentType") %>' Enabled="false"></asp:Label>
                                                                    <asp:Label ID="EnglishDocumentTypeLabel" runat="server" Visible='<%# Eval("ShowEnglish") %>' Text='<%# Eval("EnglishDocumentType") %>' Enabled="false"></asp:Label>

                                                                    <asp:HiddenField ID="DocumentTypeIDHiddenField" Value='<%# Eval("DocumentTypeID") %>' runat="server" />

                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="Literal69" Text="View"></asp:Literal></HeaderTemplate>

                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="ArabicDocumentTypeHyperLink" runat="server" Visible='<%# Eval("IsUploadedArabic") %>' Text="عرض" NavigateUrl='<%# Eval("DoumentLocation") %>'></asp:HyperLink>
                                                                    <asp:HyperLink ID="EnglishDocumentTypeHyperLink" runat="server" Visible='<%# Eval("IsUploadedEnglish") %>' Text="View" NavigateUrl='<%# Eval("DoumentLocation") %>'></asp:HyperLink>


                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="Literal69" Text="File"></asp:Literal></HeaderTemplate>

                                                                <ItemTemplate>
                                                                    <asp:FileUpload ID="fuRequiredDocument" runat="server" />
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:Panel ID="pnlNoRequiredDocuments" runat="server" Visible="false">
                                                        <div class="row m-0">
                                                            <div class="alert error-Msgdanger fade in m-0 mb-5 p-10">
                                                                <a href="#" class="close" data-dismiss="alert" aria-label="close" title="close">×</a>
                                                                <div class="glyphicon glyphicon-exclamation-sign m-0"></div>
                                                                <asp:Literal runat="server" ID="Literal1500" Text='<%$Resources:MOEHE.PSPES,NoRequiredDocuments%>'></asp:Literal>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </fieldset>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-actions mt-0 mb-0 pt-0 pb-0">
                                        <div class="row m-0">
                                            <div class="col-md-3">
                                                <a href="javascript:;" class="btn btn-xl btn-theme-colored2 mt-30 pr-40 pl-40 button-previous pull-left">
                                                    <i class=' <asp:Literal ID="literal100" runat="server" Text="<%$Resources:MOEHE.PSPES,BTNClass%>"></asp:Literal>'></i>
                                                    <asp:Literal runat="server" ID="Literal70" Text='<%$Resources:MOEHE.PSPES,Previous%>'></asp:Literal>
                                                </a>
                                            </div>
                                            <div class="col-md-3">

                                                <asp:LinkButton ID="LinkButton1" CssClass="btn btn-xl btn-danger mt-30 pr-40 pl-40  pull-left" runat="server" OnClick="LinkButton1_Click" OnClientClick="resetTab()">
                                                    <i class="fa fa-save mr-10"></i>
                                                    <asp:Literal runat="server" ID="Literal71" Text='<%$Resources:MOEHE.PSPES,Save%>'></asp:Literal>
                                                </asp:LinkButton>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:LinkButton CssClass="btn btn-xl default mt-30 pr-40 pl-40 center-block width8em" runat="server" OnClick="LnkCancel_Click" ID="LnkCancel"><i class="fa fa-ban mr-10"></i>
                                                    <asp:Literal runat="server" ID="Literal72" Text='<%$Resources:MOEHE.PSPES,Cancel%>'></asp:Literal></asp:LinkButton>
                                            </div>
                                            <div class="col-md-3">
                                                <a href="javascript:;" class="btn btn-xl btn-theme-colored2 mt-30 pr-40 pl-40 button-next pull-right">
                                                    <asp:Literal runat="server" ID="Literal73" Text='<%$Resources:MOEHE.PSPES,Next%>'></asp:Literal>
                                                    <i class='<asp:Literal ID="literal80" runat="server" Text="<%$Resources:MOEHE.PSPES,BTNClassNxt%>"></asp:Literal>'></i>
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--<asp:LinkButton ID="testreset" runat="server"  OnClientClick="resetTab()">resettab</asp:LinkButton> -->
        <!--<asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">sendSMS</asp:LinkButton>-->
        <!--<asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton2_Click1">shownow</asp:LinkButton>-->
    </div>
</section>

<!-- end main-content -->

<script>
    $(document).ready(function () {
        $(".GuardianMobile").autocomplete({
            change: function (event, ui) { }
        });

        $('#preenrolmentgradeValid').val($('.preenrolmentgrade').val());
        $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {
            localStorage.setItem('activeTab', $(e.target).attr('href'));
        });
        $(".StudientQID").keyup(function () {

            var QID = $(this).val();
            $("#StudientQID2").val(QID);
        }).keyup();

        $(".ResedentialAreaV").keyup(function () {

            var ResedentialAreaV = $(this).val();
            $("#ResedentialAreaID").val(ResedentialAreaV);
        }).keyup();
        $(".ApplicationDate").focusout(function () {

            var ApplicationDate = $(this).val();
            $("#ApplicationDateID").val(ApplicationDate);
        }).focusout();

        $(".preenrolmentgrade").keyup(function () {
            if ($('.preenrolmentgrade').val() == "0") {
                $('#preenrolmentgradeValid').val('');
            }
        }).keyup();
        $(".GuardianMobile").keyup(function () {
            var phonenumber = $(this).val();
            $("#phoneValid").val(phonenumber);
        }).keyup();

        $(".GuardianQID").keyup(function () {

            var GQID = $(this).val();
            $("#GuardianQID2").val(GQID);
        }).keyup();

        $('.specialneed').bind('change click keyup', function () {
            if ($('.specialneed').val() == "0") {
                $('#specialneedValid').val('');
            }
            else if ($('.specialneed').val() !== "0") {
                $('#specialneedValid').val($(this).val());
            }
        });

        $('.learningdifficulties').bind('change click keyup', function () {
            if ($('.learningdifficulties').val() == "0") {
                $('#learningdifficultiesValid').val('');
            }
            else if ($('.learningdifficulties').val() !== "0") {
                $('#learningdifficultiesValid').val($(this).val());
            }
        });
        var activeTab = localStorage.getItem('activeTab');
        var total = 3;
        $(".button-next").on("click", function () {


            $("#s4-workspace").scrollTo(activeTab);
            if ($('#phoneValid-error').length > 0)
                $('.GuardianMobile').closest(".form-group").addClass("has-error")
            else if ($('#phoneValid-error').length == 0)
                $('.GuardianMobile').closest(".form-group").removeClass("has-error")
        });
        $(".btn-danger").on("click", function () {

            $("#s4-workspace").scrollTo(activeTab);
            if ($('#phoneValid-error').length > 0)
                $('.GuardianMobile').closest(".form-group").addClass("has-error")
            else if ($('#phoneValid-error').length == 0)
                $('.GuardianMobile').closest(".form-group").removeClass("has-error")

        });

        if (activeTab) {
            $('#form_wizard_1 a[href="' + activeTab + '"]').tab('show');

            if (activeTab == 1) {
                $('.button-previous').attr('style', 'display:none');
            } else {
                $('.button-previous').removeAttr('style');
            }

            if (activeTab >= total) {
                $('.button-next').removeAttr('style');
                $('.button-submit').attr('style', 'display:none');
            } else {
                $('.button-next').removeAttr('style');
                $('.button-submit').attr('style', 'display:none');
            }
        }
        else {
            $('#form_wizard_1 a:first').tab('show');

            if (activeTab == 1) {
                $('.button-previous').attr('style', 'display:none');
            } else {
                $('.button-previous').attr('style', 'display:none');
            }

            if (activeTab >= total) {
                $('.button-next').removeAttr('style');
                $('.button-submit').attr('style', 'display:none');
            } else {
                $('.button-next').removeAttr('style');
                $('.button-submit').attr('style', 'display:none');
            }
        }
        if ($(".StudientQID").val() == "") {
            resetTab();
        }
    });

    function resetTab() {
        $('#form_wizard_1').bootstrapWizard('show', 0);
        $('.cancelapp').click();


    }


    function EnableText() {

        if ($('.phcccheck').prop('checked')) {
            $('.health-area').prop('disabled', false);
        }
        else {
            $('.health-area').prop('disabled', true);
        }
    }

    $(window).load(function () {
        if ($('#phoneValid-error').length > 0)
            $('.GuardianMobile').closest(".form-group").addClass("has-error")
        else if ($('#phoneValid-error').length == 0)
            $('.GuardianMobile').closest(".form-group").removeClass("has-error")
        if ($('.StudientQID').val() == "") {
            $('#form_wizard_1').bootstrapWizard('show', 0);
        }
        EnableText();

        $('.specialneed').bind('change click keyup', function () {
            if ($('.specialneed').val() == "0") {
                $('#specialneedValid').val('');
            }
            else if ($('.specialneed').val() !== "0") {
                $('#specialneedValid').val($(this).val());
            }
        });

        $('.learningdifficulties').bind('change click keyup', function () {
            if ($('.learningdifficulties').val() == "0") {
                $('#learningdifficultiesValid').val('');
            }
            else if ($('.learningdifficulties').val() !== "0") {
                $('#learningdifficultiesValid').val($(this).val());
            }
        });

    });



</script>





