<%@ Assembly Name="MOEHE.PSPES, Version=1.0.0.0, Culture=neutral, PublicKeyToken=677b80a9c9c0da8c" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WithdrawApplicationUserControl.ascx.cs" Inherits="MOEHE.PSPES.Webparts.WithdrawApplication.WithdrawApplicationUserControl" %>
<script type="text/javascript">
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
</script>
<section>
    <asp:HiddenField ID="hdnOTP" runat="server" />
    <asp:HiddenField ID="hdnSchoolCode" runat="server" />
    <div class="container bg-white-theme pt-0 pb-10 mt-50 mb-50">
        <div class="row">
            <!-- BEGIN EXAMPLE TABLE PORTLET-->
            <div class="portlet light ">
                <div class="portlet-title">
                    <div class="caption font-red">
                        <i class="fa fa-cogs font-red" aria-hidden="true" style="font-size: 20px"></i>
                        <span class="caption-subject bold uppercase">
                            <asp:Literal ID="ltPageTitle" runat="server" Text="<%$Resources:MOEHE.PSPES,TITLE_PAGE_WITHDRAWAL%>"></asp:Literal>
                        </span>
                    </div>
                </div>
                <fieldset class="scheduler-border m-0 mb-5 pb-5 pt-0 col-sm-12">
                    <legend class="scheduler-border  mb-5">
                        <asp:Literal runat="server" ID="ltPersonalInformation" Text='<%$Resources:MOEHE.PSPES,PersonalInformation%>'></asp:Literal>
                    </legend>
                    <div class="row m-0">
                        <!--StudientQID-->
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Literal runat="server" ID="ltQID" Text='<%$Resources:MOEHE.PSPES,QID%>'></asp:Literal>
                                <span class="required">* </span>
                                <asp:TextBox ID="txtQID" AutoPostBack="true" onkeypress="return isNumberKey(event)" CssClass="StudientQID form-control" runat="server" OnTextChanged="txtQID_TextChanged" MaxLength="11"></asp:TextBox>
                                <input type="text" id="StudientQID2" class="form-control StudientQID mb-0" name="username" style="color: #ffffff; background: transparent; border: none; height: 0px!important; width: 0px;">
                            </div>
                        </div>
                        <!--StudientName-->
                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:Literal runat="server" ID="ltName" Text='<%$Resources:MOEHE.PSPES,StudentName%>'></asp:Literal>
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <!--StudientGender-->
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Literal runat="server" ID="ltGender" Text='<%$Resources:MOEHE.PSPES,Gender%>'></asp:Literal>
                                <asp:TextBox ID="txtGender" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <!--StudientDOB-->
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Literal runat="server" ID="ltDOB" Text='<%$Resources:MOEHE.PSPES,DOB%>'></asp:Literal>
                                <asp:TextBox ID="txtDOB" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <!--StudientCountry-->
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Literal runat="server" ID="ltNationality" Text='<%$Resources:MOEHE.PSPES,Nationality%>'></asp:Literal>
                                <asp:TextBox ID="txtNationality" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </fieldset>
                <fieldset class="scheduler-border m-0 mb-5 pb-5 pt-0 col-sm-12">
                    <legend class="scheduler-border mb-5">
                        <asp:Literal runat="server" ID="ltApplicationInformation" Text='<%$Resources:MOEHE.PSPES,ApplicationInformation%>'></asp:Literal>
                    </legend>
                    <div class="row m-0">
                        <!--PreEnrollmentTerm-->
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Literal runat="server" ID="ltTerm" Text='<%$Resources:MOEHE.PSPES,Term%>'></asp:Literal>
                                <asp:TextBox ID="txtTerm" CssClass="Term form-control" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <!--PreEnrollmentSchool-->
                        <div class="col-sm-3">
                            <div class="form-group">
                                <asp:Literal runat="server" ID="ltSchoolName" Text='<%$Resources:MOEHE.PSPES,School%>'></asp:Literal>
                                <asp:TextBox ID="txtPreEnrollmentSchool" Enabled="false" runat="server" CssClass="form-control disabled"></asp:TextBox>
                            </div>
                        </div>
                        <!--CurrentCurriculum-->
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Literal runat="server" ID="ltCurriculum" Text='<%$Resources:MOEHE.PSPES,CurrentCurriculum%>'></asp:Literal>
                                <asp:TextBox ID="txtCurrentCurriculum" CssClass="CurrentCurriculum form-control" AutoPostBack="true" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <!--ApplicationREF-->
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Literal runat="server" ID="ltApplicationNo" Text='<%$Resources:MOEHE.PSPES,ApplicationRefNo%>'></asp:Literal>
                                <asp:TextBox Enabled="False" ID="txtApplicationRefNo" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <!--ApplicationDate-->
                        <div class='col-sm-2'>
                            <div class="form-group">
                                <asp:Literal runat="server" ID="ltApplicationDate" Text='<%$Resources:MOEHE.PSPES,ApplicationDate%>'></asp:Literal>
                                <div id="filterDate2">
                                    <!-- Datepicker as text field -->
                                    <div class="input-group date" data-date-format="dd/mm/yyyy">
                                        <input type="text" runat="server" id="dtApplicationDate" class="form-control ApplicationDate" disabled="disabled" placeholder="dd/mm/yyyy">
                                        <div class="input-group-addon">
                                            <span class="glyphicon glyphicon-calendar"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row m-0">
                        <!--PreEnrollmentGrade-->
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Literal runat="server" ID="ltRequestedGrade" Text='<%$Resources:MOEHE.PSPES,RequestedGrade%>'></asp:Literal>
                                <asp:TextBox ID="txtPreEnrolmentGrade" CssClass="PreEnrolmentGrade form-control" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </fieldset>
                <fieldset class="scheduler-border m-0  mb-20 pb-5 pt-0 col-sm-12">
                    <legend class="scheduler-border mb-0">
                        <asp:Literal runat="server" ID="ltGuardianInformation" Text='<%$Resources:MOEHE.PSPES,GuardianInformation%>'></asp:Literal>
                    </legend>
                    <div class="row m-0">
                        <div class="col-sm-2">
                            <!--GuardianQID-->
                            <div class="form-group">
                                <asp:Literal runat="server" ID="ltGuardianQID" Text='<%$Resources:MOEHE.PSPES,QID%>'></asp:Literal>
                                <asp:TextBox ID="txtGuardianQID" CssClass="GuardianQID form-control" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <!--GuardianName-->
                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:Literal runat="server" ID="ltGuardianName" Text='<%$Resources:MOEHE.PSPES,GuardianName%>'></asp:Literal>
                                <asp:TextBox ID="txtGuardianName" runat="server" Enabled="false" CssClass="GuardianName form-control"></asp:TextBox>
                            </div>
                        </div>
                        <!--GuardianGender-->
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Literal runat="server" ID="ltGuardianGender" Text='<%$Resources:MOEHE.PSPES,Gender%>'></asp:Literal>
                                <asp:TextBox runat="server" ID="txtGuardianGender" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <!--GuardianCountry-->
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Literal runat="server" ID="ltGuardianNationality" Text='<%$Resources:MOEHE.PSPES,Nationality%>'></asp:Literal>
                                <asp:TextBox ID="txtGuardianCountry" CssClass="form-control GuardianCountry" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <!--GuardianRelationship-->
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Literal runat="server" ID="ltRelationship" Text='<%$Resources:MOEHE.PSPES,Relationship%>'></asp:Literal>
                                <asp:TextBox ID="txtRelationship" CssClass="form-control Relationship" runat="server" Enabled="false"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                    <div class="row m-0">
                        <!--GuardianMobile-->
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Literal runat="server" ID="ltGuardianMobile" Text='<%$Resources:MOEHE.PSPES,MobileNumber%>'></asp:Literal>
                                <asp:TextBox ID="txtGuardianMobile" CssClass="form-control GuardianMobile" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <!--GuardianLandLine-->
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Literal runat="server" ID="ltGuardianLandLine" Text='<%$Resources:MOEHE.PSPES,Landline%>'></asp:Literal>
                                <asp:TextBox ID="txtGuardianLandLine" CssClass="form-control GuardianLandLine" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <!--GuardianEmail-->
                        <div class="col-sm-2">
                            <div class="form-group">
                                <asp:Literal runat="server" ID="ltGuardianEmail" Text='<%$Resources:MOEHE.PSPES,Email%>'></asp:Literal>
                                <asp:TextBox ID="txtGuardianEmail" CssClass="form-control GuardianEmail" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </fieldset>
                <fieldset class="scheduler-border m-0  mb-20 pb-5 pt-0 col-sm-12">
                    <legend class="scheduler-border mb-0">
                        <asp:Literal runat="server" ID="ltWithdrawalInfo" Text='<%$Resources:MOEHE.PSPES,TITLE_PAGE_WITHDRAWALINFO%>'></asp:Literal>
                    </legend>
                    <div class="row m-0">
                        <!--Withdrawal Request Date-->
                        <div class='col-sm-2'>
                            <div class="form-group">
                                <label for="withdrawRequestDate">
                                    <asp:Literal runat="server" ID="ltRequestDate" Text='<%$Resources:MOEHE.PSPES,FIELD_PAGE_WITHDRAWALREQUESTDATE%>'></asp:Literal></label>
                                <span class="required">* </span>
                                <div id="withdrawReqDate">
                                    <!-- Datepicker as text field -->
                                    <div class="input-group date" data-date-format="dd/mm/yyyy">
                                        <input type="text" runat="server" onkeydown="return false;" id="txtwithdrawalRequestDate" class="form-control ApplicationDate" placeholder="dd/mm/yyyy">
                                        <div class="input-group-addon">
                                            <span class="glyphicon glyphicon-calendar"></span>
                                        </div>
                                    </div>
                                    <input type="text" id="withdrawalRequestDate" class="form-control ApplicationDate mb-0" name="withdrawRequestDate" style="color: #ffffff; background: transparent; border: none; height: 0px!important; width: 0px;">
                                </div>
                            </div>
                        </div>
                        <!--Withdrawal Date-->
                        <div class='col-sm-2'>
                            <div class="form-group">
                                <label for="withdrawalDate">
                                    <asp:Literal runat="server" ID="ltWithdrawalDate" Text='<%$Resources:MOEHE.PSPES,FIELD_PAGE_WITHDRAWALTDATE%>'></asp:Literal></label>
                                <span class="required">* </span>
                                <div id="withdrawDate">
                                    <!-- Datepicker as text field -->
                                    <div class="input-group date" data-date-format="dd/mm/yyyy">
                                        <input type="text" runat="server" onkeydown="return false;" id="txtwithdrawalDate" class="form-control ApplicationDate" placeholder="dd/mm/yyyy">
                                        <div class="input-group-addon">
                                            <span class="glyphicon glyphicon-calendar"></span>
                                        </div>
                                    </div>
                                    <input type="text" id="withdrawalDate" class="form-control ApplicationDate mb-0" name="withdrawalDate" style="color: #ffffff; background: transparent; border: none; height: 0px!important; width: 0px;">
                                </div>
                            </div>
                        </div>
                        <!--Withdraw Reason-->
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="ddlWithdrawalReason">
                                    <asp:Literal runat="server" ID="ltReason" Text='<%$Resources:MOEHE.PSPES,FIELD_PAGE_WITHDRAWALTREASON%>'></asp:Literal></label>
                                <span class="required">* </span>
                                <asp:DropDownList ID="ddlWithdrawalReason" runat="server" CssClass="form-control pt-0">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <!--Upload withdrawal form-->
                        <div class="col-md-3">
                            <div class="form-group">
                            <label class="control-label ml-10 mb-5">
                                <asp:Literal ID="ltUploadForm" runat="server" Text="<%$Resources:MOEHE.PSPES,FIELD_PAGE_WITHDRAWALTUPLOAD%>"></asp:Literal>
                            </label>
                            <span class="required">* </span>
                                <asp:FileUpload ID="fUploadForm" runat="server" CssClass="form-control pt-0" />
                            </div>
                        </div>
                    </div>
                </fieldset>
                <%--<fieldset class="scheduler-border m-0  mb-20 pb-5 pt-0 col-sm-12">
                    <legend class="scheduler-border mb-0">
                        <asp:Literal runat="server" ID="ltOTP" Text='OTP'></asp:Literal>
                    </legend>
                    <div class="row m-0">
                        <div class="col-sm-2">
                            <label class="control-label ml-10 mb-5" for="UploadTestResult">
                                <asp:Literal ID="Literal14" runat="server" Text="OTP"></asp:Literal><span class="required"> * </span>
                            </label>
                            <asp:TextBox runat="server" ID="txtSendOtp" CssClass="form-control" />
                        </div>
                        <div class="col-sm-2">
                            <asp:LinkButton ID="lnkSendOtp" class="btn green SendSMSBtn" runat="server" OnClick="lnkSendOtp_Click"><i class="fa fa-paper-plane mr-5"></i>
                            <asp:Literal ID="ltSendOTP" runat="server" Text="Send OTP to Parent"></asp:Literal></asp:LinkButton>
                        </div>
                        <div class="col-sm-2">
                            <asp:LinkButton ID="lnkReSendOTP" class="btn yellow  SendSMSBtn" runat="server" OnClick="lnkReSendOTP_Click"><i class="fa fa-history mr-5"></i>
                            <asp:Literal ID="ltResendOTP" runat="server" Text="Resend OTP to Parent"></asp:Literal></asp:LinkButton>
                        </div>
                    </div>
                    <div class="row m-0">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <label class="control-label ml-10 mb-5">
                                    <asp:Literal ID="ltMsg" runat="server" Text="Entering the OTP, that is sent on the registered mobile at the time of registering the Student is a Concent to Withdraw the student from the School by the Parent/Guardian"></asp:Literal>
                                </label>
                            </div>
                        </div>
                    </div>
                </fieldset>--%>
            </div>
            <div class="scheduler-border m-0  mb-20 pb-5 pt-0 col-sm-12">
                <div class="col-md-2  pull-right" runat="server" id="divSubmit" visible="false"> 
                    <asp:LinkButton ID="lbSubmit" CssClass="btn btn-xl btn-danger mt-30 pr-40 pl-40  pull-left" runat="server" OnClick="lbSubmit_Click">
                        <i class="fa fa-save mr-10"></i>
                        <asp:Literal runat="server" ID="ltSubmit" Text='<%$Resources:MOEHE.PSPES,Submit%>'></asp:Literal>
                    </asp:LinkButton>
                </div>
                <div class="col-md-2  pull-right">
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

