<%@ Assembly Name="ITWORX.MOEHEWF.SCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7c6ec0a86ef11fff" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/TermsAndConditions.ascx" TagPrefix="uc1" TagName="TermsAndConditions" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/ApplicantDetails.ascx" TagPrefix="uc1" TagName="ApplicantDetails" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/DDLWithTXTWithNoPostback.ascx" TagPrefix="uc1" TagName="DDLWithTXTWithNoPostback" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.SCE/DisplayRequestDetails.ascx" TagPrefix="uc1" TagName="DisplayRequestDetails" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewRequestUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.WebParts.NewRequest.NewRequestUserControl" %>

<asp:HiddenField ID="OLevel_HF" runat="server" />
<asp:HiddenField ID="ALevel_HF" runat="server" />
<asp:HiddenField ID="IBList_HF" runat="server" />
<asp:HiddenField ID="MOIAddress_hdf" ClientIDMode="Static" runat="server" />
<asp:HiddenField ID="stdNationality_hf" ClientIDMode="Static" runat="server" />
<asp:HiddenField ID="stdGender_hf" ClientIDMode="Static" runat="server" />
<asp:HiddenField ID="hdnBirthDate" ClientIDMode="Static" runat="server" />
<asp:HiddenField ID="hdnStudentName" ClientIDMode="Static" runat="server" />
<asp:HiddenField ID="hdnRequestId" ClientIDMode="Static" runat="server" />



<div id="requestHeaderDiv" runat="server" visible="false" class="school-applicant  dark-bg">
    <div class="row">
        <div class="col-md-4 col-sm-6 col-xs-12">
            <div class="form-group">
                <label>
                    <asp:Label ID="lbl_requestNum" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequestNumber %>"></asp:Label>
                </label>


                <asp:TextBox ID="txt_requestNum" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-4 col-sm-6 col-xs-12">
            <div class="form-group">
                <label>
                    <asp:Label ID="lbl_creationDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequestCreationDate %>"></asp:Label>
                </label>
                <asp:TextBox ID="txt_creationDate" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-4 col-sm-6 col-xs-12">
            <div class="form-group">
                <label>
                    <asp:Label ID="lbl_submitionDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequestSendDate %>"></asp:Label></label>
                    <asp:TextBox ID="txt_submitionDate" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>




    </div>
</div>

<asp:Wizard ID="NewRequest_Wizard" runat="server" ActiveStepIndex="0" CssClass="wizardTable" OnNextButtonClick="wizardNewRequest_NextButtonClick" OnPreviousButtonClick="wizardNewRequest_PreviousButtonClick">
    <%--Steps Start--%>
    <WizardSteps>
        <%--First Step (Terms and Conditions)--%>
        <asp:WizardStep ID="terms_Stp" runat="server" Title="Terms And Conditions">
            <div class="termsAndCondition stepOne">
                <uc1:TermsAndConditions runat="server" id="TermsAndConditions" />
            </div>
            <div>
            </div>
        </asp:WizardStep>

        <%--Second Step (Contact Information)--%>
        <asp:WizardStep ID="ContactData_Stp" runat="server" Title="<%$Resources:ITWORX_MOEHEWF_SCE, ContactsData %>">
            <div class="school-collapse stepTwo">
                <div class="row margin-top-15">
                    <div class="accordion panel-group" id="accordion">
                        <div class="panel panel-default">
                            <div class="panel-heading active">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne">
                                        <span>بيانات التواصل</span> <em></em></a></h4>
                            </div>
                            <%-- /.panel-heading --%>
                            <div id="collapseOne" class="panel-collapse collapse in">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lblApplicantName" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, NameOfficialRecords %>"></asp:Label></label>

                                                <asp:TextBox ID="txtApplicantName" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lblMobileNumber" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, MobileNumber %>"></asp:Label><span class="error-msg"> *</span></label>

                                                <asp:TextBox ID="txtMobileNumner" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lblEmail" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Email %>"></asp:Label><span class="error-msg"> *</span>
                                                </label>
                                                <asp:TextBox ID="txtEmail" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>


                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:WizardStep>

        <%--Third Step (Applicant Details)--%>
        <asp:WizardStep ID="student_Stp" runat="server" Title="Student Data">
            <div class="school-collapse stepThree">
                <div class="row margin-top-15">
                    <div class="accordion panel-group" id="accordion">
                        <div class="panel panel-default">
                            <div class="panel-heading active">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseFour">
                                        <span>بيانات الطالب صاحب الشهادة</span> <em></em></a></h4>
                            </div>
                            <%-- /.panel-heading --%>
                            <div id="collapseFour" class="panel-collapse collapse in">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lbl_QatarID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, QatarID %>"></asp:Label><span class="error-msg">*</span>
                                                </label>
                                                <asp:TextBox ID="txt_QatarID" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:Label ID="lbl_QatarIDValidat" runat="server" ClientIDMode="Static" ForeColor="Red"></asp:Label>
                                                <asp:CustomValidator ID="QatarIDValidator" runat="server" CssClass="error-msg" ClientValidationFunction="validateQatarID" ValidationGroup="Submit" ErrorMessage="برجاء إعادة أدخال الرقم الشخصى"></asp:CustomValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-4" id="passportContainer">
 <asp:Label ID="lblTempQatarID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, TempQatarID %>" Visible="false"></asp:Label>
                <asp:TextBox ID="txtTempQatarID" runat="server" ClientIDMode="Static" Visible="false"></asp:TextBox>
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lbl_PassPort" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Passport %>" Visible="false"></asp:Label>
                                                </label>
                                                <asp:TextBox ID="txt_PassPort" ClientIDMode="Static" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lbl_birthDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, BirthDate %>"></asp:Label>
                                                </label>

                                                <asp:TextBox ID="txt_birthDate" ClientIDMode="Static" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <asp:Label ID="lbl_birthDateVal" ClientIDMode="Static" runat="server"></asp:Label>
                                            </div>



                                        </div>

                                         <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lbl_Name" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentName %>"></asp:Label></label>

                                                <asp:TextBox ID="txt_Name" ClientIDMode="Static" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <asp:Label ID="lbl_NameVal" ClientIDMode="Static" runat="server"></asp:Label>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="row margin-top-15">
                                       

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lbl_Nationality" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentNationality %>"></asp:Label>
                                                </label>

                                                <asp:DropDownList ID="ddl_Nationality" runat="server" CssClass="form-control moe-dropdown" Enabled="false"></asp:DropDownList>
                                                <asp:Label ID="lbl_NationalityVal" ClientIDMode="Static" runat="server"></asp:Label>
                                            </div>
                                        </div>

                                       

                                         <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lbl_Gender" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentGender %>"></asp:Label>

                                                    </label>

                                                    <asp:DropDownList ID="ddl_Gender" runat="server" CssClass="form-control moe-dropdown" Enabled="false"></asp:DropDownList>
                                                    <asp:Label ID="lbl_GenderVal" ClientIDMode="Static" runat="server"></asp:Label>
                                            </div>
                                        </div>

                                         <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lbl_NationalityCat" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentNationalityType %>"></asp:Label><span class="error-msg">*</span>
                                                </label>

                                                <asp:DropDownList ID="ddl_NatCat" runat="server" CssClass="form-control moe-dropdown"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="NationalityCatValidator" runat="server" ControlToValidate="ddl_NatCat" InitialValue="-1" CssClass="error-msg" ValidationGroup="Submit" ErrorMessage="فئة جنسية الطالب مطلوبة"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="row margin-top-15">
                                        

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lbl_PrintedName" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentNameCert %>"></asp:Label><span class="error-msg">*</span>
                                                </label>

                                                <asp:TextBox ID="txt_PrintedName" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="PrintedNameValidator" runat="server" ControlToValidate="txt_PrintedName" ForeColor="Red" ValidationGroup="Submit" ErrorMessage="اسم الطالب طبقا للشهادة مطلوبة"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>


                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </asp:WizardStep>

        <%--Fourth Step (Certificate Details)--%>
        <asp:WizardStep ID="certificate_Stp" runat="server" Title="Certificate Details">
            <div class="school-collapse stepFour">
                <div class="row margin-top-15">
                    <div class="accordion panel-group" id="accordion">
                        <div class="panel panel-default">
                            <div class="panel-heading active">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseFive">
                                        <span>بيانات الشهادة الدراسية المطلوب معادلتها</span> <em></em></a></h4>
                            </div>
                            <%-- /.panel-heading --%>
                            <div id="collapseFive" class="panel-collapse collapse in">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-4">

                                            <uc1:DDLWithTXTWithNoPostback runat="server" id="certificateResource" />

                                        </div>
                                        <div class="col-md-4">

                                            <uc1:DDLWithTXTWithNoPostback runat="server" id="schooleType" />

                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lbl_PrevSchool" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, PreviousSchool %>"></asp:Label></label>
                                                <asp:TextBox ID="txt_PrevSchool" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row margin-top-15">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lblSchoolingSystem" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SchoolingSystem %>"></asp:Label>
                                                </label>
                                                <asp:DropDownList ID="ddlSchoolingSystem" runat="server" CssClass="form-control moe-dropdown"></asp:DropDownList>
                                                <%--<uc1:DDLWithTXTWithNoPostback runat="server" id="schoolingSystem" />--%>
                                            </div>
                                        </div>


                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lbl_ScholasticLevel" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, LastSchoolYear %>"></asp:Label><span class="error-msg">*</span></label>

                                                <asp:DropDownList ID="ddl_ScholasticLevel" runat="server" CssClass="form-control moe-dropdown"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lbl_LastAcademicYear" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, AcademicYear %>"></asp:Label><span class="error-msg">*</span></label>

                                                <asp:DropDownList ID="ddl_LastAcademicYear" runat="server" CssClass="form-control moe-dropdown"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row margin-top-15">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lblEquiPurpose" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, EquivalencyPurpose %>"></asp:Label></label>

                                                <asp:DropDownList ID="ddlEquiPurpose" runat="server" CssClass="form-control moe-dropdown"></asp:DropDownList>
                                            </div>
                                            <%--<uc1:DDLWithTXTWithNoPostback runat="server" id="equiPurpose" />--%>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    <asp:Label ID="lbl_GoingToClass" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, GoToClass %>"></asp:Label>
                                                </label>
                                                <asp:DropDownList ID="ddl_GoingToClass" runat="server" CssClass="form-control moe-dropdown"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-md-4">

                                            <uc1:DDLWithTXTWithNoPostback runat="server" id="certificateType" />

                                        </div>
                                    </div>
                                    <div class="row">


                                        <div>



                                            <div class="IGCSE_Div " style="display: none">
                                                <asp:CustomValidator ID="AlevelValidator" runat="server" ClientValidationFunction="validateAlevel" OnServerValidate="serverValidateAlevel" ValidationGroup="Submit" ErrorMessage="لا يمكن إرسال الطلب الا إذا كان عدد المواد 2 أو أكثر"></asp:CustomValidator>
                                                <asp:CustomValidator ID="OlevelValidator" runat="server" ClientValidationFunction="validateOlevel" OnServerValidate="serverValidateOlevel" ValidationGroup="Submit" ErrorMessage="لا يمكن إرسال الطلب الا إذا كان عدد المواد 5 أو أكثر"></asp:CustomValidator>


                                                <div class="OLevel_Div">
                                                    <div class="row margin-top-15">
                                                        <h4 class="subject-header">
                                                            <label>
                                                                أدخل جميع المواد 
								من المستوى العادي O.Level التي تم النجاح فيها بحد 
								أدني 5 مواد <span class="error-msg">*</span></label></h4>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>
                                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectCode %>"></asp:Literal></label>
                                                                <input type="text" class="Ocode_txt form-control" />
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label>
                                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectName %>"></asp:Literal></label>
                                                                <input type="text" class="Otitle_txt form-control" />
                                                            </div>

                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label>
                                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAverage %>"></asp:Literal></label>
                                                                <input type="text" class="OAvrage_txt form-control" />
                                                            </div>


                                                        </div>

                                                        <div class="col-md-2 text-right">
                                                            <div class="form-group">
                                                                <label class="visibility-hidden">
                                                                    المعدل
                                                                </label>
                                                                <input type="button" class="addOLevel_btn btn moe-btn" value="إضافة" />
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="validateAlevel" OnServerValidate="serverValidateAlevel" ValidationGroup="Submit" ErrorMessage="لا يمكن إرسال الطلب الا إذا كان عدد المواد 2 أو أكثر" CssClass="error-msg"></asp:CustomValidator>

                                                    <table class="OLevel_table table school-table table-striped table-bordered">

                                                        <thead>

                                                            <tr>

                                                                <th class="text-center">
                                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectNum %>"></asp:Literal>
                                                                </t>
                                                                        <th class="text-center">
                                                                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectCode %>"></asp:Literal></th>
                                                                <th class="text-center">
                                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectName %>"></asp:Literal></th>
                                                                <th class="text-center">
                                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAverage %>"></asp:Literal></th>
                                                                <th class="text-center">
                                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAction %>"></asp:Literal></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                        </tbody>
                                                    </table>
                                                </div>

                                                <div class="ALevel_Div">

                                                    <div class="row margin-top-50">
                                                        <h4 class="subject-header">
                                                            <label>
                                                                مادتي المستوى 
								الرفيع A.Level التي تم النجاح فيهما أو مواد المستوى 
								المتقدم A.S <span class="error-msg">*</span></label></h4>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>
                                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectCode %>"></asp:Literal></label>

                                                                <input type="text" class="Acode_txt form-control" />

                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label>
                                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectName %>"></asp:Literal></label>

                                                                <input type="text" class="Atitle_txt form-control" />
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label>
                                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAverage %>"></asp:Literal></label>

                                                                <input type="text" class="Aavrage_txt form-control" />

                                                            </div>
                                                        </div>

                                                        <div class="col-md-2 text-right">
                                                            <div class="form-group">
                                                                <label class="visibility-hidden">
                                                                    المعدل
                                                                </label>
                                                                <%--                                                                    <input type="button" class="addALevel_btn btn moe-btn" value="إضافة" />--%>

                                                                <asp:Button runat="server" CssClass="addALevel_btn btn moe-btn" type="button" Text="<%$Resources:ITWORX_MOEHEWF_SCE, OLevelAdd %>" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="validateOlevel" OnServerValidate="serverValidateOlevel" ValidationGroup="Submit" ErrorMessage="لا يمكن إرسال الطلب الا إذا كان عدد المواد 5 أو أكثر" CssClass="error-msg"></asp:CustomValidator>


                                                    <table class="ALevel_table table school-table table-striped table-bordered">

                                                        <thead>
                                                            <tr>
                                                                <th class="text-center">
                                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectNum %>"></asp:Literal></th>
                                                                <th class="text-center">
                                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectCode %>"></asp:Literal></th>
                                                                <th class="text-center">
                                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectName %>"></asp:Literal></th>
                                                                <th class="text-center">
                                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAverage %>"></asp:Literal></th>
                                                                <th class="text-center">
                                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAction %>"></asp:Literal></th>


                                                            </tr>
                                                        </thead>
                                                        <tbody></tbody>
                                                    </table>

                                                </div>
                                            </div>
                                            <div class="IB_Div" style="display: none">


                                                <div class="row margin-top-50 margin-bottom-15">
                                                    <h4 class="subject-header">
                                                        <label>
                                                            أدخل المواد الحاصل عليها  في المستوى HL  في  SL 
 <span class="error-msg">*</span></label></h4>


                                                    <div class="col-md-4">
                                                        <label>
                                                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectName %>"></asp:Literal></label>

                                                        <input type="text" class="IBTitle_txt form-control" />

                                                    </div>
                                                    <div class="col-md-3">
                                                        <label>
                                                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectPoints %>"></asp:Literal></label>

                                                        <input type="number" class="IBPoints_txt form-control" />


                                                    </div>
                                                    <div class="col-md-3">
                                                        <label>
                                                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectLevel %>"></asp:Literal></label>

                                                        <asp:DropDownList ID="ddl_IBLevel" ClientIDMode="Static" runat="server" CssClass="form-control moe-dropdown"></asp:DropDownList>

                                                    </div>
                                                    <div class="col-md-2 text-right">
                                                        <div class="form-group">
                                                            <label class="visibility-hidden">المعدل</label>
                                                            <asp:Button runat="server" CssClass="addIB_btn btn moe-btn" type="button" Text="<%$Resources:ITWORX_MOEHEWF_SCE, OLevelAdd %>" />
                                                        </div>
                                                    </div>
                                                    <div class="clearfix"></div>
                                                    <asp:CustomValidator ID="IBValidator" runat="server" ClientValidationFunction="validateIB" OnServerValidate="serverValidateIB" ValidationGroup="Submit" ErrorMessage="لا يمكن إرسال الطلب الا إذا كان عدد النقاط 24 أو أكثر"></asp:CustomValidator>
                                                    <div class="clearfix"></div>

                                                    <table class="IB_table table school-table table-striped table-bordered">
                                                        <thead>
                                                            <tr>

                                                                <th class="text-center">
                                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectNum %>"></asp:Literal></th>
                                                                <th class="text-center">
                                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectName %>"></asp:Literal></th>
                                                                <th class="text-center">
                                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectLevel %>"></asp:Literal></th>
                                                                <th class="text-center">
                                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectPoints %>"></asp:Literal></th>
                                                                <th class="text-center">
                                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAction %>"></asp:Literal></th>

                                                            </tr>
                                                        </thead>
                                                        <tbody></tbody>

                                                    </table>


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

        </asp:WizardStep>

        <%--Fifth Step (Attachments)--%>
        <asp:WizardStep ID="attachments_Stp" runat="server" Title="<%$Resources:ITWORX_MOEHEWF_SCE, Attachments %>">
            <div class="stepFive">
            </div>
        </asp:WizardStep>

        <%--Sixth Step (Review and Approval)--%>
        <asp:WizardStep ID="review_Stp" runat="server" Title="<%$Resources:ITWORX_MOEHEWF_SCE, Review %>">
            <div class="stepSix">
                <uc1:DisplayRequestDetails runat="server" id="DisplayRequestDetails" />
            </div>
        </asp:WizardStep>
    </WizardSteps>


    <StartNavigationTemplate>
        <div class="margin-lr-30">
            <div class="row">
                <div class="col-sm-6 col-xs-12 text-left"></div>
                <div class="col-sm-6 col-xs-12 text-right">
                    <asp:Button ID="StartNextButton" runat="server" ValidationGroup="Submit" CommandName="MoveNext" Text="Next" />
                </div>
            </div>
        </div>
    </StartNavigationTemplate>

    <StepNavigationTemplate>
        <div class="margin-lr-30">
            <div class="row">
                <div class="col-sm-6 col-xs-12 text-left">
                    <asp:Button ID="StepPreviousButton" runat="server" CausesValidation="False" CommandName="MovePrevious" Text="Previous" />
                </div>
                <div class="col-sm-6 col-xs-12 text-right">
                    <asp:Button ID="StepNextButton" runat="server" ValidationGroup="Submit" CausesValidation="true" CommandName="MoveNext" Text="Next" />
                </div>
            </div>
        </div>
    </StepNavigationTemplate>

    <FinishNavigationTemplate>
        <div class="margin-lr-30">
            <div class="row">
                <div class="col-sm-6 col-xs-12 text-left">
                    <asp:Button ID="FinishPreviousButton" runat="server" CausesValidation="False" CommandName="MovePrevious" Text="Previous" />

                </div>
                <div class="col-sm-6 col-xs-12 text-right">
                    <asp:Button ID="FinishButton" runat="server" ValidationGroup="Submit" CommandName="MoveComplete" Text="Finish" />
                </div>
            </div>
        </div>

    </FinishNavigationTemplate>
</asp:Wizard>

<script>
    var oLevel_IGCSEList = [];
    var aLevel_IGCSEList = [];
    var IBList = [];

    $(function () {


        LoadForm();

        //getApplicantNameFromService();

        $("#txt_QatarID").blur(function () {
            getStudentData();
        }); 

        $(".addIB_btn").click(function (e) {
            e.preventDefault();
            if ($(".IBTitle_txt").val() != "" && $("#ddl_IBLevel").val() != "-1" && $(".IBPoints_txt").val() != "") {
                var id = IBList.length > 0 ? parseInt(IBList[IBList.length - 1].ID) + 1 : 1;

                IBList.push({ ID: id, Title: $(".IBTitle_txt").val(), Level: $("#ddl_IBLevel").val(), LevelTitle: $("#ddl_IBLevel option:selected").text(), Points: $(".IBPoints_txt").val() })

                var tr;
                tr = $('<tr id="' + id + '" />');
                tr.append("<td>" + id + "</td>");
                tr.append("<td>" + $(".IBTitle_txt").val() + "</td>");
                tr.append("<td>" + $("#ddl_IBLevel option:selected").text() + "</td>");
                tr.append("<td>" + $(".IBPoints_txt").val() + "</td>");
                tr.append("<td><input type='button' class='DeleteIBItem_btn' value='حذف' onclick='DeleteIBItem(" + id + ")'/></td>");

                $('.IB_table').append(tr);

                $("#<%=IBList_HF.ClientID%>").val(JSON.stringify(IBList));
                validateDivs("IB_Div"); 
                if ($("#ddl_IBLevel").val() == "-1") {
                    $("#ddl_IBLevel").css('border-color', 'red');
                }
                else {
                    $("#ddl_IBLevel").css('border-color', '');
                }

                $(".IBTitle_txt").val("");
                $("#ddl_IBLevel").val("-1");
                $(".IBPoints_txt").val("");
            }
            else {  
                validateDivs("IB_Div");  
                if ($("#ddl_IBLevel").val() == "-1") {
                    $("#ddl_IBLevel").css('border-color', 'red');
                }
                else {
                    $("#ddl_IBLevel").css('border-color', '');
                }
            }

        })
        $(".addALevel_btn").click(function (e) {
            e.preventDefault();
            if ($(".Acode_txt").val() != "" && $(".Atitle_txt").val() != "" && $(".Aavrage_txt").val() != "") {

                var id = aLevel_IGCSEList.length > 0 ? parseInt(aLevel_IGCSEList[aLevel_IGCSEList.length-1].ID) + 1 :1;

                aLevel_IGCSEList.push({ ID: id, Code: $(".Acode_txt").val(), Title: $(".Atitle_txt").val(), Avrage: $(".Aavrage_txt").val() })

                var tr;
                tr = $('<tr id="2' + id + '" />');
                tr.append("<td>" + id + "</td>");
                tr.append("<td>" + $(".Acode_txt").val() + "</td>");
                tr.append("<td>" + $(".Atitle_txt").val() + "</td>");
                tr.append("<td>" + $(".Aavrage_txt").val() + "</td>");
                tr.append("<td><input type='button' class='DeleteALevel_btn' value='حذف' onclick='DeleteALevel(2" + id + ")'/></td>");

                $('.ALevel_table').append(tr);

                $("#<%=ALevel_HF.ClientID%>").val(JSON.stringify(aLevel_IGCSEList));
                validateDivs("ALevel_Div"); 

                $(".Acode_txt").val("");
                $(".Atitle_txt").val("");
                $(".Aavrage_txt").val("");
            } else {
                validateDivs("ALevel_Div"); 
            }
        })


        $(".addOLevel_btn").click(function (e) {
            e.preventDefault();
            if ($(".Ocode_txt").val() != "" && $(".Otitle_txt").val() != "" && $(".OAvrage_txt").val() != "") {

                var id = oLevel_IGCSEList.length > 0 ? parseInt(oLevel_IGCSEList[oLevel_IGCSEList.length - 1].ID) + 1 : 1;

                oLevel_IGCSEList.push({ ID: id, Code: $(".Ocode_txt").val(), Title: $(".Otitle_txt").val(), Avrage: $(".OAvrage_txt").val() })

                var tr;
                tr = $('<tr id="1' + id + '" />');
                tr.append("<td>" + id + "</td>");
                tr.append("<td>" + $(".Ocode_txt").val() + "</td>");
                tr.append("<td>" + $(".Otitle_txt").val() + "</td>");
                tr.append("<td>" + $(".OAvrage_txt").val() + "</td>");
                tr.append("<td><input type='button' class='DeleteOLevel_btn' value='حذف' onclick='DeleteOLevel(1" + id + ")'/></td>");

                $('.OLevel_table').append(tr);

                $("#<%=OLevel_HF.ClientID%>").val(JSON.stringify(oLevel_IGCSEList));
                validateDivs("OLevel_Div");

                $(".Ocode_txt").val("");
                $(".Otitle_txt").val("");
                $(".OAvrage_txt").val("");
            } else { 
                validateDivs("OLevel_Div");
            }
        })

        $("#<%=CerTypeClientID%>").change(function () { 
            viewHideCert();
        })
    })
    function validateDivs(control) {
        $("." + control + " input[type='text']").each(function () {
            if ($(this).val() == "")
                $(this).css('border-color', 'red');
            else
                $(this).css('border-color', '');

        });
    }
    function DeleteALevel(ID) {

        $('table.ALevel_table tr#' + ID).remove();
        for (var i = 0; i < aLevel_IGCSEList.length; i++) {
            if (aLevel_IGCSEList[i].ID == ID)
                aLevel_IGCSEList.splice($.inArray(aLevel_IGCSEList[i], aLevel_IGCSEList), 1);

        }
        $("#<%=ALevel_HF.ClientID%>").val(JSON.stringify(aLevel_IGCSEList));
        }


    function DeleteOLevel(ID) {

        $('table.OLevel_table tr#' + ID).remove();
        for (var i = 0; i < oLevel_IGCSEList.length; i++) {
            if (oLevel_IGCSEList[i].ID == ID)
                oLevel_IGCSEList.splice($.inArray(oLevel_IGCSEList[i], oLevel_IGCSEList), 1);

        } 
        $("#<%=OLevel_HF.ClientID%>").val(JSON.stringify(oLevel_IGCSEList));
    }

    function DeleteIBItem(ID) {

        $('table.IB_table tr#' + ID).remove();
        for (var i = 0; i < IBList.length; i++) {
            if (IBList[i].ID == ID)
                IBList.splice($.inArray(IBList[i], IBList), 1);

        }
        $("#<%=IBList_HF.ClientID%>").val(JSON.stringify(IBList));
    }

    function validateAlevel(sender, arguments) {
        var crtTypeVal = $("#<%=CerTypeClientID%>").val();


        if (crtTypeVal == 1 && (parseInt(aLevel_IGCSEList.length) < 2)) { 
                arguments.IsValid = false;

        } 
    }
    function validateOlevel(sender, arguments) {
        var crtTypeVal = $("#<%=CerTypeClientID%>").val();


            if (crtTypeVal == 1 && (parseInt(oLevel_IGCSEList.length) < 5)) {
                arguments.IsValid = false;

            }
        }

    function validateIB(sender, arguments) {
        var crtTypeVal = $("#<%=CerTypeClientID%>").val();

        if (crtTypeVal == 2) {
            var points = 0;

            for (var i = 0; i < IBList.length; i++) {
                points = points + parseInt(IBList[i].Points);
            }
            if (points < 24)
                arguments.IsValid = false;

        }  
    }

    function validateQatarID(sender, argument) {
        if ($("#lbl_birthDateVal").text() == "" || $("#lbl_NameVal").text() == "" || $("#lbl_NationalityVal").text() == "" || $("#lbl_GenderVal").text()=="")
        {
            argument.IsValid = false; 
        }
    }


    function LoadForm() {
        if ($("#lbl_NameVal").text() == "") {
            getStudentData();
        }
        viewHideCert();
        if ($("#<%=OLevel_HF.ClientID%>").val() != "") {
            oLevel_IGCSEList = JSON.parse($("#<%=OLevel_HF.ClientID%>").val());

            if (oLevel_IGCSEList.length > 0) {
                for (var i = 0; i < oLevel_IGCSEList.length; i++) {
                    var tr;
                    tr = $('<tr id="' + oLevel_IGCSEList[i].ID + '" />');
                    tr.append("<td>" + (parseInt(i) + 1) + "</td>");
                    tr.append("<td>" + oLevel_IGCSEList[i].Code + "</td>");
                    tr.append("<td>" + oLevel_IGCSEList[i].Title + "</td>");
                    tr.append("<td>" + oLevel_IGCSEList[i].Avrage + "</td>");
                    tr.append("<td><input type='button' class='DeleteOLevel_btn' value='حذف' onclick='DeleteOLevel(" + oLevel_IGCSEList[i].ID + ")'/></td>");

                    $('.OLevel_table').append(tr);
                }
            }
        }
        if ($("#<%=ALevel_HF.ClientID%>").val() != "") {
            aLevel_IGCSEList = JSON.parse($("#<%=ALevel_HF.ClientID%>").val());

            if (aLevel_IGCSEList.length > 0) {
                for (var i = 0; i < aLevel_IGCSEList.length; i++) {
                    var tr;
                    tr = $('<tr id="' + aLevel_IGCSEList[i].ID + '" />');
                    tr.append("<td>" + (parseInt(i) + 1) + "</td>");
                    tr.append("<td>" + aLevel_IGCSEList[i].Code + "</td>");
                    tr.append("<td>" + aLevel_IGCSEList[i].Title + "</td>");
                    tr.append("<td>" + aLevel_IGCSEList[i].Avrage + "</td>");
                    tr.append("<td><input type='button' class='DeleteALevel_btn' value='حذف' onclick='DeleteALevel(" + aLevel_IGCSEList[i].ID + ")'/></td>");

                    $('.ALevel_table').append(tr);
                }
            }
        }

        if ($("#<%=IBList_HF.ClientID%>").val() != "") {
            IBList = JSON.parse($("#<%=IBList_HF.ClientID%>").val()); 

            if (IBList.length > 0) {
                for (var i = 0; i < IBList.length; i++) {
                    var tr;
                    tr = $('<tr id="' + IBList[i].ID + '" />');
                    tr.append("<td>" + (parseInt(i) + 1) + "</td>");
                    tr.append("<td>" + IBList[i].Title + "</td>");
                    tr.append("<td>" + IBList[i].Level + "</td>");
                    tr.append("<td>" + IBList[i].Points + "</td>");
                    tr.append("<td><input type='button' class='DeleteIBItem_btn' value='حذف' onclick='DeleteIBItem(" + IBList[i].ID + ")'/></td>");

                    $('.IB_table tbody').append(tr);
                }
            }
        }

    }
    function viewHideCert() {
        var crtTypeVal = $("#<%=CerTypeClientID%>").val();
        if (crtTypeVal == 1) {
            $(".IGCSE_Div").show();
            $(".IB_Div").hide();
        }
        else if (crtTypeVal == 2) {
            $(".IB_Div").show();
            $(".IGCSE_Div").hide();
        }
        else {
            $(".IB_Div").hide();
            $(".IGCSE_Div").hide();
        }
    }

    function getStudentData() {
        if ($("#txt_QatarID").val() != "") {
            var url = $("#MOIAddress_hdf").val() + $("#txt_QatarID").val();

            $.ajax({
                url: url
                , type: 'GET'
                , success: function (result) {
                    //debugger;
                    if (result != null) {
                        var isAr = window.location.href.toLowerCase().indexOf("ar") >= 0;

                        if (isAr) {
                            $("#lbl_NameVal").text(result.ArabicName);
                            $("#hdnStudentName").val(result.ArabicName);
                        } else {
                            $("#lbl_NameVal").text(result.EnglishName);
                            $("#hdnStudentName").val(result.EnglishName);
                        }

                        $("#lbl_birthDateVal").text(result.BirthDate);
                        $("#hdnBirthDate").val(result.BirthDate);
                        if (result.Gender.toLowerCase() == 'm') {
                            $("#lbl_GenderVal").text("ذكر");
                            $("#stdGender_hf").val('M'); 
                        }
                        else {
                            $("#lbl_GenderVal").text("أنثى");
                            $("#stdGender_hf").val('F'); 
                        }

                        $("#stdNationality_hf").val(result.Nationality)

                        $.ajax({
                            url: "/_api/web/lists/getbytitle('Nationality')/items?$select=Title,TitleAr&$filter=ISOCode%20eq%20%27" + result.Nationality + "%27"
                            , type: 'GET'
                            , headers: {
                                "accept": "application / json;odata = verbose",
                            }
                            , success: function (nat) {
                                var nationality = nat.d.results[0];
                                if (nationality != undefined) {
                                    if (isAr)
                                        $("#lbl_NationalityVal").text(nationality.TitleAr);
                                    else
                                        $("#lbl_NationalityVal").text(nationality.Title);
                                }

                            }
                        });
                        $("#lbl_QatarIDValidat").text("");
                    }
                    else {
                        $("#lbl_NameVal").val("");
                        $("#lbl_NationalityVal").val("");
                        $("#lbl_QatarIDValidat").text("برجاء إعادة أدخال الرقم الشخصى");
                        $("#txt_QatarID").val("");
                    }
                }
                , error: function () {
                    console.log("MOI Error");
                    $("#lbl_NameVal").val("");
                    $("#lbl_NationalityVal").val("");
                    $("#lbl_QatarIDValidat").text("برجاء إعادة أدخال الرقم الشخصى");
                    $("#txt_QatarID").val("");
                }
            });
        }
    }

    function getApplicantNameFromService() {
        $.ajax({
            url: "/_api/web/currentuser",
            type: 'GET',
            headers: {
                "accept": "application / json;odata = verbose",
            },
            success: function (currentUser) {
                //console.log(currentUser.d.Title);
                $.ajax({
                    url: "/_api/web/lists/getbytitle('Applicants')/items?$select=ApplicantName,PersonalID,MobileNumber,ApplicantEmail,ArabicName,EnglishName&$filter=ApplicantName%20eq%20%27" + currentUser.d.Title + "%27",
                    type: 'GET',
                    headers: {
                        "accept": "application / json;odata = verbose",
                    },
                    success: function (applicantData) {
                        //var nationality = nat.d.results[0];
                        //if (nationality != undefined) {
                        //    if (isAr)
                        //        $("#lbl_NationalityVal").text(nationality.TitleAr);
                        //    else
                        //        $("#lbl_NationalityVal").text(nationality.Title);
                        //}
                        //console.log(applicantData.d.results[0].PersonalID);
                        var isAr = window.location.href.toLowerCase().indexOf("ar") >= 0;
                        if (isAr)
                            $("#txtApplicantName").val(applicantData.d.results[0].ArabicName);
                        else
                            $("#txtApplicantName").val(applicantData.d.results[0].EnglishName);

                        $('#txtMobileNumber').val(applicantData.d.results[0].MobileNumber);
                        $('#txtEmail').val(applicantData.d.results[0].ApplicantEmail);
                        var url = $("#MOIAddress_hdf").val() + applicantData.d.results[0].PersonalID;
                        //$.ajax({
                        //    url: url,
                        //    type: 'GET',
                        //    success: function (result) {
                        //        //console.log(result);
                        //        if (result != null) {
                        //            var isAr = window.location.href.toLowerCase().indexOf("ar") >= 0;
                        //            if (isAr)
                        //                $("#txtApplicantName").val(result.ArabicName);
                        //            else
                        //                $("#txtApplicantName").val(result.EnglishName);

                        //            $('#txtMobileNumber').val(applicantData.d.results[0].MobileNumber);
                        //            $('#txtEmail').val(applicantData.d.results[0].ApplicantEmail);
                        //            //$("#lbl_birthDateVal").text(result.BirthDate);

                        //            //if (result.Gender.toLowerCase() == 'm') {
                        //            //    $("#lbl_GenderVal").text("ذكر");
                        //            //    $("#stdGender_hf").val('M');
                        //            //}
                        //            //else {
                        //            //    $("#lbl_GenderVal").text("أنثى");
                        //            //    $("#stdGender_hf").val('F');
                        //            //}
                        //            //$("#stdNationality_hf").val(result.Nationality)




                        //            //$("#lbl_QatarIDValidat").text("");
                        //        }
                        //        //else {
                        //        //    $("#lbl_NameVal").val("");
                        //        //    $("#lbl_NationalityVal").val("");
                        //        //    $("#lbl_QatarIDValidat").text("برجاء إعادة أدخال الرقم الشخصى");
                        //        //    $("#txt_QatarID").val("");
                        //        //}
                        //    },
                        //    error: function (error) {
                        //        //$("#lbl_NameVal").val("");
                        //        //$("#lbl_NationalityVal").val("");
                        //        //$("#lbl_QatarIDValidat").text("برجاء إعادة أدخال الرقم الشخصى");
                        //        //$("#txt_QatarID").val("");
                        //        console.log(error);
                        //    }
                        //});
                    },
                    error: function (error) {
                        console.log(error);
                    }
                });
            }
        });

        //if ($("#txtApplicantName").val() != "") {



        //}
    }
</script>
