<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExceptionsUserControl.ascx.cs" Inherits="MOEHE.PSPES.Webparts.Exceptions.ExceptionsUserControl" %>

<link href="/_layouts/15/MOEHE.PSPES/assets/css/chosen.css" rel="stylesheet" type="text/css" />

<section class="form-horizontal">
    <div class="container mt-30 mb-30 pt-0 pb-0 bg-white-theme">
        <div class="row">
            <!-- BEGIN EXAMPLE TABLE PORTLET-->
            <div class="portlet light ">
                <div class="col-md-12 borde-bottom">
                    <h3 class="caption p-0">
                        <asp:Literal runat="server" ID="ltPersonalInformation" Text='<%$Resources:MOEHE.PSPES,PersonalInformation%>'></asp:Literal></h3>

                    <div class="row">
                        <!--StudientQID-->
                        <div class="col-sm-2">
                            <div class="form-group">
                                <label for="StudientQID">
                                    <asp:Literal runat="server" ID="Literal1" Text='<%$Resources:MOEHE.PSPES,QID%>'></asp:Literal>
                                    <span class="required">* </span>
                                </label>

                                <asp:TextBox ID="txtQID" AutoPostBack="true" CssClass="StudientQID form-control" runat="server" OnTextChanged="txtQID_TextChanged"></asp:TextBox>
                                <input type="text" id="StudientQID2" class="form-control StudientQID mb-0" name="username" style="color: #ffffff; background: transparent; border: none; height: 0px!important; width: 0px;">
                            </div>
                        </div>
                        <!--StudientName-->
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label for="StudientName">
                                    <asp:Literal runat="server" ID="Literal2" Text='<%$Resources:MOEHE.PSPES,StudentName%>'></asp:Literal></label>
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                <input type="text" id="englishstudentname" runat="server" class="form-control mb-0" style="color: #ffffff; background: transparent; border: none; height: 0px!important; width: 0px;">
                                <input type="text" id="arabicstudentname" runat="server" class="form-control mb-0" style="color: #ffffff; background: transparent; border: none; height: 0px!important; width: 0px;">
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
                </div>
                <asp:Panel ID="pnlExceptionDetails" runat="server" Visible="false">
                    <div class="col-md-12 borde-bottom">
						<h3 class="caption p-0">
                            <asp:Literal ID="AdvancedSearchLiteral" runat="server" Text="<%$Resources:MOEHE.PSPES,ExceptionDetails%>"></asp:Literal>
                        </h3>

                        <div class="row">
                            <!--Code-->
                            <!--PreEnrollmentTerm-->
                            <div class="col-sm-2">
                                <div class="form-group">
									<label for="1">
										<asp:Literal runat="server" ID="Literal10" Text='<%$Resources:MOEHE.PSPES,Term%>'></asp:Literal>
									</label>
                                    <asp:DropDownList ID="ddlPreEnrollmentTerm" runat="server" CssClass="form-control">
                                    </asp:DropDownList>

                                </div>
                            </div>
                            <div class="col-sm-4">
								<div class="form-group">
									<label for="2">
										<asp:Literal ID="SchoolCodeLiteral" runat="server" Text="<%$Resources:MOEHE.PSPES,School%>"></asp:Literal>
									</label>
                                    <asp:DropDownList class="chzn-select form-control" ID="SchoolCodesDropDownList" AutoPostBack="true" OnSelectedIndexChanged="SchoolCodesDropDownList_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                    <asp:Label ID="lblItemID" runat="server" Width="0" Height="0" Style="color: #ffffff; background: transparent; border: none; height: 0px!important; width: 0px;"></asp:Label>
                                </div>
                            </div>
                            <!--CurrentCurriculum-->
                            <div class="col-sm-2">
								<div class="form-group">
									<label for="3">
									<asp:Literal runat="server" ID="Literal23" Text='<%$Resources:MOEHE.PSPES,CurrentCurriculum%>'></asp:Literal>
									</label>
                                    <asp:TextBox ID="txtSchoolCurriculum" runat="server" CssClass="form-control" Enabled="false" disabled></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <div class="form-group">
                                    <label for="Grade">
                                        <asp:Literal ID="Literal8" runat="server" Text="<%$Resources:MOEHE.PSPES,Grade%>"></asp:Literal>
                                    </label>
                                    <asp:DropDownList class="form-control " ID="SchoolGradesDropDownList" AutoPostBack="true" OnSelectedIndexChanged="SchoolGradesDropDownList_SelectedIndexChanged" runat="server"></asp:DropDownList>

                                </div>
                            </div>

                            <!--ApplicationDate-->
                            <div class='col-sm-2'>
                                <div class="form-group">
                                    <label for="TimeTestReject">

                                        <asp:Literal runat="server" ID="Literal81" Text='<%$Resources:MOEHE.PSPES,ExceptionValidUntil%>'></asp:Literal><span class="required">* </span>
                                    </label>
                                    <div id="filterDate2">
                                        <!-- Datepicker as text field -->
                                        <div class="input-group date" data-date-format="dd/mm/yyyy">
                                            <input type="text" runat="server" onkeydown="return false;" id="dtExceptionExpiryDate" class="form-control ApplicationDate" placeholder="dd/mm/yyyy">

                                            <div class="input-group-addon">
                                                <span class="glyphicon glyphicon-calendar"></span>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <script>
                                $(function () {
                                    var date = new Date();
                                    date.setDate(date.getDate());
                                    $('.input-group.date').datepicker({
                                        format: "dd/mm/yyyy",
                                        startDate: date
                                    });
                                });
                            </script>


                            <!--Search-->

                        </div>
                        <!--End Row-->
                        
                        <asp:Panel ID="pnlExceptionBoxes" class="DivUpload" runat="server" Visible="false">
							<h3 class="caption p-0">Exception Type</h3>
                            <div class="row">
                                <div class="col-sm-12">
                                <!-- Age-->
                                    <div class="pure-checkbox">
                                        <div id="ctl00_ctl52_g_41f78195_ffc5_4dd7_be24_11ce0379b442_ctl00_chkLstGrades" class="checkbox m-5 p-5">
                                            <asp:CheckBox ID="chkAgeException" class="Age" Text='<%$Resources:MOEHE.PSPES,Age%>'  onclick=" toggleAgeAttachment(); toggleSaveBTN()" runat="server" />
                                            <div class="neptuneRising">
                                                <div id="AgeExceptionAttachmentRow" runat="server" class=" row neptuneRising1">
                                                    <!--
                                                        <div class="fileinput fileinput-new" data-provides="fileinput">
                                                                <div class="input-group input-large">
                                                                    <div class="form-control uneditable-input input-fixed input-medium-edit col-md-12" data-trigger="fileinput">
                                                                        <i class="fa fa-file fileinput-exists"></i>&nbsp;
                                                                        <span class="fileinput-filename"> </span>
                                                                    </div>
                                                                    <span class="input-group-addon btn default btn-file upload-edit">
                                                                        <span class="fileinput-new "> <asp:Literal runat="server" ID="Literal7" Text='<%$Resources:MOEHE.PSPES,Selectfile%>'></asp:Literal> </span>
                                                                        <span class="fileinput-exists "><asp:Literal runat="server" ID="Literal9" Text='<%$Resources:MOEHE.PSPES,Change%>'></asp:Literal></span>
                                                                    <asp:FileUpload ID="fuAgeAttachment1" class="fileinputAge" runat="server" />
                                                                    </span>
                                                                    <asp:HyperLink ID="AgeExceptionAttachmentLink1" class="attachementLink-2"  runat="server" Target="_blank" CssClass="input-group-addon btn blue fileinput-exists upload"  Text='<%$Resources:MOEHE.PSPES,View%>'></asp:HyperLink>
                                                                    <!--<a href="javascript:;"  class="input-group-addon btn red fileinput-exists upload" data-dismiss="fileinput"> Remove </a>
                                                                </div>
                                                            </div>
                                                    -->
                                                    <asp:FileUpload ID="fuAgeAttachment" class="fileinputAge col-sd-4" runat="server" />
                                                    <asp:HyperLink ID="AgeExceptionAttachmentLink" class="attachementLink-2"  runat="server" Target="_blank" CssClass="btn btn-default  View-Upload col-sd-1" >  <i class="fa fa-link mr-5"></i>
                                                        <asp:Literal runat="server" ID="Literal721" Text='<%$Resources:MOEHE.PSPES,View%>'></asp:Literal>
                                                   </asp:HyperLink>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- Allow Reg-->
                                    <div class="pure-checkbox">
                                        <div id="ctl00_ctl52_g_41f78195_ffc5_4dd7_be24_11ce0379b442_ctl00_chkLstGrades" class="checkbox m-5 p-5">
                                            <asp:CheckBox ID="chkAllowRegWhileCloseException"  Text='<%$Resources:MOEHE.PSPES,AllowRegistrationWhileEnrollmentClosed%>' onclick="toggleAllowRegisterWhileCloseAttachment(); toggleSaveBTN()" runat="server" />
                                            <div class="neptuneRising">
                                                <div id="AllowRegWhileCloseExceptionAttachmentRow" runat="server" class=" row neptuneRising1">
                                                    <asp:FileUpload ID="fuAllowRegistrationWhileEnrollmentClosedAttachment" class="fileinputAge col-sd-4" runat="server" />
                                                    <asp:HyperLink ID="AllowRegistrationWhileEnrollmentClosedAttachmentLink" class="attachementLink-2"  runat="server" Target="_blank" CssClass="btn btn-default  View-Upload col-sd-1" >  <i class="fa fa-link mr-5"></i>
                                                        <asp:Literal runat="server" ID="Literal722" Text='<%$Resources:MOEHE.PSPES,View%>'></asp:Literal>
                                                   </asp:HyperLink>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- Nationality-->
                                    <div class="pure-checkbox">
                                        <div id="ctl00_ctl52_g_41f78195_ffc5_4dd7_be24_11ce0379b442_ctl00_chkLstGrades" class="checkbox m-5 p-5">
                                            <asp:CheckBox ID="chkNationalityException"  Text='<%$Resources:MOEHE.PSPES,Nationality%>' onclick="toggleNationalityAttachment(); toggleSaveBTN()" runat="server" />
                                            <div class="neptuneRising">
                                                <div id="NationalityExceptionAttachmentRow" runat="server" class=" row neptuneRising1">
                                                    <asp:FileUpload ID="fuNationalityExceptionAttachment" class="fileinputAge col-sd-4" runat="server" />
                                                    <asp:HyperLink ID="NationalityExceptionAttachmentLink" class="attachementLink-2"  runat="server" Target="_blank" CssClass="btn btn-default View-Upload col-sd-1" >  <i class="fa fa-link mr-5"></i>
                                                        <asp:Literal runat="server" ID="Literal723" Text='<%$Resources:MOEHE.PSPES,View%>'></asp:Literal>
                                                   </asp:HyperLink>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- Allow Rep-->
                                    <div class="pure-checkbox">
                                        <div id="ctl00_ctl52_g_41f78195_ffc5_4dd7_be24_11ce0379b442_ctl00_chkLstGrades" class="checkbox m-5 p-5">
                                            <asp:CheckBox ID="chkAllowRepeatYearException"  Text='<%$Resources:MOEHE.PSPES,AllowRepeatYear%>' onclick="toggleAllowRepeatYearAttachment(); toggleSaveBTN()" runat="server" />
                                            <div class="neptuneRising">
                                                <div id="AllowRepeatYearExceptionAttachmentRow" runat="server" class=" row neptuneRising1">
                                                    <asp:FileUpload ID="fuAllowRepeatYearExceptionAttachment" class="fileinputAge col-sd-4" runat="server" />
                                                    <asp:HyperLink ID="AllowRepeatYearExceptionAttachmentLink" class="attachementLink-2"  runat="server" Target="_blank" CssClass="btn btn-default View-Upload col-sd-1" >  <i class="fa fa-link mr-5"></i>
                                                        <asp:Literal runat="server" ID="Literal724" Text='<%$Resources:MOEHE.PSPES,View%>'></asp:Literal>
                                                   </asp:HyperLink>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- Gender-->
                                     <div class="pure-checkbox">
                                        <div id="ctl00_ctl52_g_41f78195_ffc5_4dd7_be24_11ce0379b442_ctl00_chkLstGrades" class="checkbox m-5 p-5">
                                            <asp:CheckBox ID="ChkGenderException"  Text='<%$Resources:MOEHE.PSPES,Gender%>' onclick="toggleGenderAttachment(); toggleSaveBTN()" runat="server" />
                                            <div class="neptuneRising">
                                                <div id="GenderExceptionAttachmentRow" runat="server" class=" row neptuneRising1">
                                                    <asp:FileUpload ID="fuGenderExceptionAttachment" class="fileinputAge col-sd-4" runat="server" />
                                                    <asp:HyperLink ID="GenderExceptionAttachmentLink" class="attachementLink-2"  runat="server" Target="_blank" CssClass="btn btn-default  View-Upload col-sd-1" >  <i class="fa fa-link mr-5"></i>
                                                        <asp:Literal runat="server" ID="Literal725" Text='<%$Resources:MOEHE.PSPES,View%>'></asp:Literal>
                                                   </asp:HyperLink>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            <div class="col-md-12">
                                <div class=" pull-right">
                                    <div class="btn-group Research  pb-15">
                                        <label for="Grade"></label>
                                        <asp:LinkButton ID="SaveLinkButton" Style="display:none ;" runat="server" OnClientClick="return CheckExpiryDate(); return checkUploads();" CssClass="btn btn-default" OnClick="SaveLinkButton_Click">
                                            <i class="fa fa-save mr-10"></i>
                                            <asp:Literal runat="server" ID="Literal71" Text='<%$Resources:MOEHE.PSPES,Save%>'></asp:Literal>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdnAgeAttachmentRequired" runat="server" />
                            <asp:HiddenField ID="hdnAllowEnrollmentWhileCloseAttachmentRequired" runat="server" />
                            <asp:HiddenField ID="hdnNationalityAttachmentRequired" runat="server" />
                            <asp:HiddenField ID="hdnAllowRepeatYearAttachmentRequired" runat="server" />
                            <asp:HiddenField ID="hdnGenderAttachmentRequired" runat="server" />
                            <asp:HiddenField ID="hdnAgeAttachmentTypeID" runat="server" />
                            <asp:HiddenField ID="hdnAllowEnrollmentWhileCloseAttachmentTypeID" runat="server" />
                            <asp:HiddenField ID="hdnNationalityAttachmentTypeID" runat="server" />
                            <asp:HiddenField ID="hdnAllowRepeatYearAttachmentTypeID" runat="server" />
                            <asp:HiddenField ID="hdnGenderAttachmentTypeID" runat="server" />
                        </asp:Panel>
                        <!--End Row-->

                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</section>
<script>
    $(document).ready(function () {
        $(".StudientQID").keyup(function () {

            var QID = $(this).val();
            $("#StudientQID2").val(QID);
        }).keyup();
        $(".uneditable-input").keyup(function () {
            if ($('.Age input').is(':checked') == true) {
                $(".neptuneRising1").show();
            } else {
                $(".neptuneRising1").hide();
            }
            var filePath = $(this).val();
            console.log(filePath);
        }).keyup();
        $('.fileinputAge').on('change', function () {
            var filePath = $(this).val();
            console.log(filePath);
        });
    });


    function CheckExpiryDate() {

        if ($('.ApplicationDate').val() == "") {
            var cult = '<%= System.Globalization.CultureInfo.CurrentUICulture.LCID %>';

            if (cult == "1025") {
                alert("الرجاء تحديد تاريخ صلاحية الاستثناء");
                return false;
            }
            else {
                alert("Please enter Exception Expiry Date");
                return false;
            }
        }

        else {


        }
    }



    function toggleSaveBTN() {
        var savebtn = document.getElementById('<%=SaveLinkButton.ClientID%>');
        var chkAge = document.getElementById('<%=chkAgeException.ClientID%>');
        var chkAllowRegWhileClose = document.getElementById('<%=chkAllowRegWhileCloseException.ClientID%>');
        var chkNationality = document.getElementById('<%=chkNationalityException.ClientID%>');
        var chkAllowRepeatYear = document.getElementById('<%=chkAllowRepeatYearException.ClientID%>');
        var ChkGender = document.getElementById('<%=ChkGenderException.ClientID%>');

        if (chkAge.checked == true || chkAllowRegWhileClose.checked == true || chkNationality.checked == true || chkAllowRepeatYear.checked == true || ChkGender.checked == true) {
            savebtn.style.display = 'block';
        }
        else
        { savebtn.style.display = 'none'; }


    }

    function toggleAgeAttachment() {

        var chkAge = document.getElementById('<%=chkAgeException.ClientID%>');
        var AgeAttachmentRow = document.getElementById('<%=AgeExceptionAttachmentRow.ClientID%>');
        var hdnAgeAttachmentRequired = document.getElementById('<%=hdnAgeAttachmentRequired.ClientID%>');
       <%-- var chkAllowRegWhileClose = document.getElementById('<%=chkAllowRegWhileCloseException.ClientID%>');
        var chkNationality = document.getElementById('<%=chkNationalityException.ClientID%>');
        var chkAllowRepeatYear = document.getElementById('<%=chkAllowRepeatYearException.ClientID%>');
        var ChkGender = document.getElementById('<%=ChkGenderException.ClientID%>');--%>

        if (hdnAgeAttachmentRequired.value.toLowerCase() == "true" && chkAge.checked == true) {
            AgeAttachmentRow.style.display = 'block';
        }
        else
        { AgeAttachmentRow.style.display = 'none'; }


    }

    function toggleAllowRegisterWhileCloseAttachment() {


        var AllowRegWhileCloseAttachmentRow = document.getElementById('<%=AllowRegWhileCloseExceptionAttachmentRow.ClientID%>');
        var chkAllowRegWhileClose = document.getElementById('<%=chkAllowRegWhileCloseException.ClientID%>');
        var hdnAllowEnrollmentWhileCloseAttachmentRequired = document.getElementById('<%=hdnAllowEnrollmentWhileCloseAttachmentRequired.ClientID%>');
       <%--  var chkNationality = document.getElementById('<%=chkNationalityException.ClientID%>');
        var chkAllowRepeatYear = document.getElementById('<%=chkAllowRepeatYearException.ClientID%>');
        var ChkGender = document.getElementById('<%=ChkGenderException.ClientID%>');--%>


        if (hdnAllowEnrollmentWhileCloseAttachmentRequired.value.toLowerCase() == "true" && chkAllowRegWhileClose.checked == true) {
            AllowRegWhileCloseAttachmentRow.style.display = 'block';
        }
        else
        { AllowRegWhileCloseAttachmentRow.style.display = 'none'; }


    }
    function toggleNationalityAttachment() {


        var NationalityAttachmentRow = document.getElementById('<%=NationalityExceptionAttachmentRow.ClientID%>');

        var chkNationality = document.getElementById('<%=chkNationalityException.ClientID%>');
        var hdnNationalityAttachmentRequired = document.getElementById('<%=hdnNationalityAttachmentRequired.ClientID%>');
       <%-- var chkAllowRepeatYear = document.getElementById('<%=chkAllowRepeatYearException.ClientID%>');
        var ChkGender = document.getElementById('<%=ChkGenderException.ClientID%>');--%>

        if (hdnNationalityAttachmentRequired.value.toLowerCase() == "true" && chkNationality.checked == true) {
            NationalityAttachmentRow.style.display = 'block';
        }
        else
        { NationalityAttachmentRow.style.display = 'none'; }


    }


    function toggleAllowRepeatYearAttachment() {


        var AllowRepeatYearAttachmentRow = document.getElementById('<%=AllowRepeatYearExceptionAttachmentRow.ClientID%>');


        var chkAllowRepeatYear = document.getElementById('<%=chkAllowRepeatYearException.ClientID%>');
        var hdnAllowRepeatYearAttachmentRequired = document.getElementById('<%=hdnAllowRepeatYearAttachmentRequired.ClientID%>');
       <%--  var ChkGender = document.getElementById('<%=ChkGenderException.ClientID%>');--%>

        if (hdnAllowRepeatYearAttachmentRequired.value.toLowerCase() == "true" && chkAllowRepeatYear.checked == true) {
            AllowRepeatYearAttachmentRow.style.display = 'block';
        }
        else
        { AllowRepeatYearAttachmentRow.style.display = 'none'; }


    }

    function toggleGenderAttachment() {


        var GenderAttachmentRow = document.getElementById('<%=GenderExceptionAttachmentRow.ClientID%>');



        var ChkGender = document.getElementById('<%=ChkGenderException.ClientID%>');
        var hdnGenderAttachmentRequired = document.getElementById('<%=hdnGenderAttachmentRequired.ClientID%>');

        if (hdnGenderAttachmentRequired.value.toLowerCase() == "true" && ChkGender.checked == true) {
            GenderAttachmentRow.style.display = 'block';
        }
        else
        { GenderAttachmentRow.style.display = 'none'; }


    }

    function checkUploads() {

        var cult = '<%= System.Globalization.CultureInfo.CurrentUICulture.LCID %>';

        if (cult == "1025") {
            alert("الرجاء تحديد تاريخ صلاحية الاستثناء");
            return false;
        }
        else {
            alert("Please enter Exception Expiry Date");
            return false;
        }

        var AgeAttachment = document.getElementById('<%=fuAgeAttachment.ClientID%>');
        var AllowEnrollmentWhileCloseAttachment = document.getElementById('<%=fuAllowRegistrationWhileEnrollmentClosedAttachment.ClientID%>');
        var NationalityAttachment = document.getElementById('<%=fuNationalityExceptionAttachment.ClientID%>');
        var AllowRepeatYearAttachment = document.getElementById('<%=fuAllowRepeatYearExceptionAttachment.ClientID%>');
        var GenderAttachment = document.getElementById('<%=fuGenderExceptionAttachment.ClientID%>');

        var hdnAgeAttachmentRequired = document.getElementById('<%=hdnAgeAttachmentRequired.ClientID%>');
        var hdnAllowEnrollmentWhileCloseAttachmentRequired = document.getElementById('<%=hdnAllowEnrollmentWhileCloseAttachmentRequired.ClientID%>');
        var hdnNationalityAttachmentRequired = document.getElementById('<%=chkNationalityException.ClientID%>');
        var hdnAllowRepeatYearAttachmentRequired = document.getElementById('<%=hdnAllowRepeatYearAttachmentRequired.ClientID%>');
        var hdnGenderAttachmentRequired = document.getElementById('<%=hdnGenderAttachmentRequired.ClientID%>');


        if (hdnAgeAttachmentRequired.value.toLowerCase() == "true" && AgeAttachment.files.length == 0) {
            if (cult == "1025") {
                alert('المرفق الخاص بالعمر مطلوب');
                return false;
            }
            else {
                alert('Age Attachment Required');
                return false;
            }
        }
        if (hdnAllowEnrollmentWhileCloseAttachmentRequired.value.toLowerCase() == "true" && AllowEnrollmentWhileCloseAttachment.files.length == 0) {
            if (cult == "1025") {
                alert('المرفق الخاص بالسماح بالتسجيل في فترة الإغلاق مطلوب');
                return false;
            }
            else {
                alert('Allow Registration While Enrollment Closed Attachment Required');
                return false;
            }
        }
        if (hdnNationalityAttachmentRequired.value.toLowerCase() == "true" && NationalityAttachment.files.length == 0) {
            if (cult == "1025") {
                alert('المرفق الخاص بالسماح بالجنسية مطلوب');
                return false;
            }
            else {
                alert('Nationality Attachment Required');
                return false;
            }
        }
        if (hdnAllowRepeatYearAttachmentRequired.value.toLowerCase() == "true" && AllowRepeatYearAttachment.files.length == 0) {
            if (cult == "1025") {
                alert('المرفق الخاص بالسماح بإعادة السنة مطلوب');
                return false;
            }
            else {
                alert('Allow Repeat Year Attachment Required');
                return false;
            }
        }
        if (hdnGenderAttachmentRequired.value.toLowerCase() == "true" && GenderAttachment.files.length == 0) {
            if (cult == "1025") {
                alert('المرفق الخاص بالنوع مطلوب');
                return false;
            }
            else {
                alert('Gender Attachment Required');
                return false;
            }
        }



    }

</script>

<script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>

<style>
    .neptuneRising1{background: #eaf2f3;
    padding: 10px;
    margin: 10px 0;
}    
    
    .input-medium-edit{width: 400px!important;height:33px!important}
    .upload-edit{
  height: 13px!important;
  padding: 0px 10px!important;
}
    .attachementLink-2{    display: inherit;
    float: left;
    height: 40px;
    line-height: 31px;
    margin-left: 23px;}
</style>
