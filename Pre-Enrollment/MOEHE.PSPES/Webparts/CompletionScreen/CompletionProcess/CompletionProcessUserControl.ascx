<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompletionProcessUserControl.ascx.cs" Inherits="MOEHE.PSPES.Webparts.CompletionScreen.CompletionProcess.CompletionProcessUserControl" %>


  <!-- Section: Services Details -->
    <section>
        <div class="container bg-white-theme mt-50 mb-50 p-0">
          <div class="row">
            <!-- BEGIN EXAMPLE TABLE PORTLET-->
            <div class="portlet light ">
                <div class="portlet-title">
                    <div class="caption font-red">
                        <i class="fa fa-upload font-red" aria-hidden="true"></i>
                        <span class="caption-subject bold uppercase">إعداد الوثائق المرفقة للمدارس</span>
                    </div>
                    <div class="tools"> 
                    </div>
                </div>
                <div class="portlet-body p-15">
                  <div class="row p-15">
                    <fieldset class="scheduler-border m-0 mb-30 col-sm-12">
                      <legend class="scheduler-border">معايير البحث</legend>
                        <div class="row mr-20 ml-20">
                          <!--StudientQID-->
                          <div class="col-sm-2">
                              <div class="form-group">
                                <label for="StudientQID">رقم البطاقة الشخصية <span class="required"> * </span></label>
                        <asp:TextBox id = "TxtAppRef" class="form-control" runat="server"></asp:TextBox>

                              </div>
                            </div>
                          <!--ApplicationRefNbr-->
                          <div class="col-sm-2">
                              <div class="form-group">
                                <label for="ApplicationRefNbr">رقم الطلب <span class="required"> * </span></label>
                        <asp:TextBox id = "TxtQID" runat="server"  class="form-control"></asp:TextBox>

                                
                              </div>
                            </div>
                          <div class="col-md-2 mt-30">


                              <asp:LinkButton ID="SearchLinkButton" runat="server" OnClick="SearchLinkButton_Click"  class="btn grey-salsa btn-outline">Search</asp:LinkButton>
                            
                          </div>
                        </div>
                    </fieldset>
                    
                      <fieldset class="scheduler-border m-0 mb-30 col-sm-12">
                          <legend class="scheduler-border">البيانات الشخصية</legend>
                          <form class="p-15 mt-10" role="form">
                            <div class="row">
                          
                              <!--StudientName-->
                              <div class="col-sm-4">
                                <div class="form-group">
                                  <label for="StudientName">اسم الطالب</label>
                        <asp:TextBox id = "TxtStudentNm" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>

                                 
                                </div>
                              </div>

                              <!--PreEnrollmentSchool-->
                              <div class="col-sm-4">
                                <div class="form-group">
                                  <label for="PreEnrollmentSchool">المدرسة السابقة</label>
                        <asp:TextBox id = "TxtPreEnScl" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>

                                 
                                </div>
                              </div>

                                                              <!--Grade-->
                              <div class="col-sm-4">
                                <div class="form-group">
                                  <label for="PreEnrollmentSchool">المدرسة السابقة</label>
                        <asp:TextBox id = "TxtPreEnGrade" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>

                                 
                                </div>
                              </div>

                            </div>
                            <!--StudientGender-->
                            <div class="row">
                              <div class="col-sm-3">
                                <div class="form-group">
                                  <label for="StudientGender">ذكر - انثى</label>
                                     <asp:TextBox id = "TxtGender" runat="server"  class="form-control"></asp:TextBox>

                                 
                                </div>
                              </div>
                            
                           
                              <!--StudientNationality-->
                              <div class="col-sm-3">
                                <div class="form-group">
                                  <label for="StudientNationality">الجنسية</label>
                        <asp:TextBox id = "TxtNationality" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>

                               
                                </div>
                              </div>
                                <div class="col-sm-3">
                                <div class="form-group">
                                  <label for="StudientNationality">Waitlist Number</label>
                        <asp:TextBox id = "TxtWListNum" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>

                               
                                </div>
                              </div>
                                <div class="col-sm-3">
                                <div class="form-group">
                                  <label for="StudientNationality">Application Date</label>
                        <asp:TextBox id = "TxtAppDT" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>

                               
                                </div>
                              </div>

                            </div>
                        </fieldset>
                        <div class="form-group form-md-checkboxes">
                          <h3 for="form_control_1 mt-15 mb-15">متطلبات التسجيل  لهذا الطلب </h3>
                          <div class="md-checkbox-inline">
                              <div class="md-checkbox">
                                  <input type="checkbox" id="checkbox_1" name="checkboxes1[]" value="1" class="md-check" checked>
                                  <label for="checkbox_1">
                                    <span class="inc"></span>
                                    <span class="check"></span>
                                    <span class="box"></span> استكمال الوثائق والمستندات المطلوبة  </label>
                              </div>
                              <div class="md-checkbox">
                                  <input type="checkbox" id="checkbox_2" name="checkboxes2[]" value="1" class="md-check" checked>
                                  <label for="checkbox_2">
                                    <span class="inc"></span>
                                    <span class="check"></span>
                                    <span class="box"></span> اختبار القبول </label>
                              </div>
                              <div class="md-checkbox">
                                <input type="checkbox" id="checkbox_3" name="checkboxes3[]" value="1" class="md-check" checked>
                                <label for="checkbox_3">
                                    <span class="inc"></span>
                                    <span class="check"></span>
                                    <span class="box"></span> مقابلة الطالب للتقييم  </label>
                              </div>
                          </div>
                      </div>  
                    <fieldset class="scheduler-border m-0 mb-30 p-30 pt-0 col-sm-12">
                      <legend class="scheduler-border">الوثائق الداعمة</legend>
                      
                            <%--MohammedAlhanafi 18-02-2019 supporitng document part--%>

                      <fieldset class="checkbox1 scheduler-border m-0 mb-30 col-sm-12 p-10 pb-30">
                          <legend class="scheduler-border mb-0">
                              
                                
                           <asp:Literal id="SupportingDocumentsLiteral" runat="server" Text="SupportingDocuments"></asp:Literal>

                          </legend>

                           <asp:GridView ID="gvRequiredDocuments" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" runat="server">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate><asp:Literal runat="server" ID="Literal69" Text="DocumentName"></asp:Literal></HeaderTemplate>

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRequiredDocumentName" CssClass="control-label ml-10 mb-5" runat="server"></asp:Label>
                                                                    <asp:Label ID="ArabicDocumentTypeLabel" runat="server" Visible='<%# Eval("ShowArabic") %>' Text='<%# Eval("ArabicDocumentType") %>' Enabled="false"></asp:Label>
                                                                    <asp:Label ID="EnglishDocumentTypeLabel" runat="server" Visible='<%# Eval("ShowEnglish") %>' Text='<%# Eval("EnglishDocumentType") %>' Enabled="false"></asp:Label>
                                                                   
                                                                    <asp:HiddenField ID="DocumentTypeIDHiddenField" Value='<%# Eval("DocumentTypeID") %>' runat="server" />
                                                                   
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                                     <asp:TemplateField>
                                                                <HeaderTemplate><asp:Literal runat="server" ID="Literal69" Text="View"></asp:Literal></HeaderTemplate>

                                                                <ItemTemplate>
                                                                   <asp:HyperLink ID="ArabicDocumentTypeHyperLink" runat="server"  Visible='<%# Eval("IsUploadedArabic") %>' Text="عرض" NavigateUrl='<%# Eval("DoumentLocation") %>'></asp:HyperLink>
                                                                    <asp:HyperLink ID="EnglishDocumentTypeHyperLink" runat="server"  Visible='<%# Eval("IsUploadedEnglish") %>' Text="View" NavigateUrl='<%# Eval("DoumentLocation") %>'></asp:HyperLink>
                                                                    
                                                                   
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate><asp:Literal runat="server" ID="Literal69" Text="File"></asp:Literal></HeaderTemplate>

                                                                <ItemTemplate>
                                                                    <asp:FileUpload ID="fuRequiredDocument"  runat="server" />
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>

                             <div class="row mr-20 ml-20">
                        <div class="col-md-2 mt-10">
                          
                            <asp:LinkButton ID="SaveDocumentsLinkButton" class="btn green" runat="server" OnClick="SaveDocumentsLinkButton_Click">Save</asp:LinkButton>

                        </div>
                                 

                      </fieldset>
                   
                        <%--MohammedAlhanafi 18-02-2019 supporitng document part--%>
                        
                        <fieldset class="checkbox2 scheduler-border m-0 mb-30 col-sm-12 p-10 pb-30">
                          <legend class="scheduler-border mb-0">دعوة للاختبار</legend>
                          <div class="row mr-20 ml-20">
                            <div class="form-group form-md-radios mb-20">
                                <div class="md-radio-inline">
                                  <!--CallTest-->
                                  <div class="md-radio">
                                    <input type="radio" id="radio6" name="radioTest" onclick="TestRadioCheck();"  class="md-radiobtn">
                                      <asp:HiddenField ID="InviationTestaRadioHiddenField" runat="server" />
                                    <label for="radio6">
                                        <span class="inc"></span>
                                        <span class="check"></span>
                                        <span class="box"></span>دعوة للاختبار</label>
                                  </div>
                                  <!--Rejected-->
                                  <div class="md-radio">
                                    <input type="radio" id="radio7" name="radioTest" onclick="TestRadioCheck();"  class="md-radiobtn" checked="">
                                      <asp:HiddenField ID="RejecttionTestRadioHiddenField" runat="server" />

                                    <label for="radio7">
                                        <span class="inc"></span>
                                        <span class="check"></span>
                                        <span class="box"></span>مرفوض</label>
                                  </div>
                                  <!--No action-->
                                  <div class="md-radio">
                                    <input type="radio" id="radio8" name="radioTest" onclick="TestRadioCheck();"  class="md-radiobtn">
                                      <asp:HiddenField ID="NoactionTestRadioHiddenField" runat="server" />

                                    <label for="radio8">
                                        <span class="inc"></span>
                                        <span class="check"></span>
                                        <span class="box"></span>لم يتم اتخاذ أي إجراء بعد</label>
                                  </div>
                                </div>
                            </div>
                          </div>
                          <div class="row mr-20 ml-20">
                            <!--TimeTestReject-->
                            <div class='col-sm-3'>
                              <div class="form-group">
                                <label for="TimeTestReject">Date</label>
                                <div id="filterDate2">
                                  <!-- Datepicker as text field -->
                                  <div class="input-group date" data-date-format="dd/mm/yyyy">
                                    <input type="text" class="form-control" name="TestDateTextBox" placeholder="dd/mm/yyyy">
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
                            <!--TimeTest-->
                            <div class="col-sm-3 col-md-offset-1 ">
                              <div class="form-group">
                                <label for="StudientCountry">Time</label>
                                <input type="text" id="TestTimeText" name="TestTimeText" class="form-control">
                              </div>
                            </div>
                            
                              
                          <div class="col-sm-3 col-md-offset-1 ">
                                <asp:LinkButton ID="SendTestInvatationLinkButton" OnClick="SendTestInvatationLinkButton_Click" class="btn grey-salsa btn-outline" runat="server">Send SMS For Test </asp:LinkButton>
                           
                            </div>
                                 <div class="col-sm-3 col-md-offset-1 ">
                                <asp:LinkButton ID="ShowHideTestSMSHistoryLinkButton" OnClick="ShowHideTestSMSHistoryLinkButton_Click" class="btn grey-salsa btn-outline" runat="server">عرض سجل الرسائل</asp:LinkButton>
                              </div> 
                          </div>
                          <div class="row mr-20 ml-20">
                            <div class="col-sm-11">
                              <!--RejectionReason-->
                              <div class="form-group">
                                  <asp:Panel ID="TestRejectionReasonPanel" runat="server" Visible="false">
                                  <label for="RejectionReason">سبب الرفض</label>
                                  <textarea class="form-control" rows="3" id="TestRejectionReasonText"  name="TestRejectionReasonText" placeholder="الرجاء إدخال سبب الرفض. يتم تمكين هذا الحقل عندما تكون قيمة الحقلمرفوض في الاختبار المطلوب"></textarea>
                                      </asp:Panel>
                              </div>
                            </div>
                          
                          
                          
                          </div>
                           
                          <fieldset class="scheduler-border mr-30 ml-30">
                              <legend class="scheduler-border mb-0">نتيجة الاختبار</legend>
                              <div class="row mr-20 ml-20">
                              <div class="col-sm-3 ">
                                  <div class="form-group">
                                    <!--TestResult-->
                                    <label for="TestResult">نتيجة الاختبار</label>
                                   <asp:DropDownList ID="DDLTestResult" runat="server" AutoPostBack="true"></asp:DropDownList>
                                  </div>
                                </div>
                                <div class="col-md-3 col-md-offset-1">
                                    <!--UploadTestResult-->
                                    <label class="control-label ml-10 mb-5" for="UploadTestResult">رفع نتيجة الاختبار</label>
                                    <div class="form-group">
                                        <asp:FileUpload ID="FuploadTestReslt" runat="server" />
                                      
                                    </div>
                                  </div>
                                <div class="col-md-2 mt-30  col-md-offset-1">
                                <asp:LinkButton ID="SendTestResultLinkButton" OnClick="SendTestResultLinkButton_Click" class="btn grey-salsa btn-outline" runat="server">SendTestResultConfirmation</asp:LinkButton>

                                </div>

                                 <asp:GridView ID="gvTestRst" runat="server" AutoGenerateColumns="false" Visible="false">
                                          <Columns>
                                              <asp:TemplateField>
                                                  <HeaderTemplate>
                                                      <asp:Literal ID="Literal69" runat="server" Text="By"></asp:Literal>
                                                  </HeaderTemplate>
                                                  <ItemTemplate>
                                                      <asp:Label ID="lblRequiredDocumentName" runat="server" CssClass="control-label ml-10 mb-5" Text="By"></asp:Label>
                                                      <asp:TextBox ID="TxttstResultBy" runat="server" Text='<%# Eval("USERID") %>'></asp:TextBox>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                              <asp:TemplateField>
                                                  <HeaderTemplate>
                                                      <asp:Label ID="lbl1" runat="server" CssClass="control-label ml-10 mb-5" Text="Result Document"></asp:Label>
                                                  </HeaderTemplate>
                                                  <ItemTemplate>
                                                      <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("ResultDocLocation") %>' Text="Download">Test Result</asp:HyperLink>
                                                  </ItemTemplate>
                                              </asp:TemplateField>
                                          </Columns>
                                      </asp:GridView>


                                  
                              </div>
                              <asp:GridView ID="TestResultSMSGridView" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" runat="server" Visible="False">
                                      <Columns>

                                          <asp:TemplateField>
                                              <HeaderTemplate>
                                                  <asp:Literal runat="server" ID="Literal69" Text="Message Text"></asp:Literal></HeaderTemplate>

                                              <ItemTemplate>
                                                  <asp:Label ID="MessageTextLabel" runat="server" Text='<%# Eval("MsgText") %>' Enabled="false"></asp:Label>


                                              </ItemTemplate>

                                          </asp:TemplateField>

                                          <asp:TemplateField>
                                              <HeaderTemplate>
                                                  <asp:Literal runat="server" ID="Literal69" Text="Number"></asp:Literal></HeaderTemplate>

                                              <ItemTemplate>
                                                  <asp:Label ID="MessageTextLabel" runat="server" Text='<%# Eval("MobileNumber") %>' Enabled="false"></asp:Label>
                                              </ItemTemplate>

                                          </asp:TemplateField>

                                          <asp:TemplateField>
                                              <HeaderTemplate>
                                                  <asp:Literal runat="server" ID="Literal69" Text="Date"></asp:Literal></HeaderTemplate>

                                              <ItemTemplate>
                                                  <asp:Label ID="StatusLabel" runat="server" Text='<%# Eval("MsgTime") %>' Enabled="false"></asp:Label>
                                              </ItemTemplate>

                                          </asp:TemplateField>

                                          <asp:TemplateField>
                                              <HeaderTemplate>
                                                  <asp:Literal runat="server" ID="Literal69" Text="Sender"></asp:Literal></HeaderTemplate>

                                              <ItemTemplate>
                                                  <asp:Label ID="StatusLabel" runat="server" Text='<%# Eval("MsgSender") %>' Enabled="false"></asp:Label>
                                              </ItemTemplate>

                                          </asp:TemplateField>

                                          <asp:TemplateField>
                                              <HeaderTemplate>
                                                  <asp:Literal runat="server" ID="Literal69" Text="Status"></asp:Literal></HeaderTemplate>

                                              <ItemTemplate>
                                                  <asp:Label ID="StatusLabel" runat="server" Text='<%# Eval("MsgStatus") %>' Enabled="false"></asp:Label>
                                              </ItemTemplate>

                                          </asp:TemplateField>

                                      </Columns>
                                  </asp:GridView>
                          </fieldset>

                        </fieldset>



                        <fieldset class="checkbox3 scheduler-border m-0 mb-30 col-sm-12 p-10 pb-30">
                            <legend class="scheduler-border mb-0">معلومات المقابلة</legend>
                            <div class="row mr-20 ml-20">
                                <div class="form-group form-md-radios mb-20">
                                    <div class="md-radio-inline">
                                        <!--CallInterview-->
                                        <div class="md-radio">
                                            <input type="radio" id="radio1" onclick=" InterviewRadioCheck();"  name="radioInterview" class="md-radiobtn">
                                             <asp:HiddenField ID="InterviewInvitationRadioHiddenField" runat="server" />
                                            <label for="radio1">
                                                <span class="inc"></span>
                                                <span class="check"></span>
                                                <span class="box"></span>دعوة للمقابلة</label>
                                        </div>
                                        <!--Rejected-->
                                        <div class="md-radio">
                                            <input type="radio" id="radio2" onclick=" InterviewRadioCheck();"  name="radioInterview" class="md-radiobtn" checked="">
                                             <asp:HiddenField ID="InterviewRejectionRadioHiddenField" runat="server" />
                                            <label for="radio2">
                                                <span class="inc"></span>
                                                <span class="check"></span>
                                                <span class="box"></span>مرفوض</label>
                                        </div>
                                        <!--Noaction-->
                                        <div class="md-radio">
                                            <input type="radio" id="radio3" onclick=" InterviewRadioCheck();"  name="radioInterview" class="md-radiobtn">
                                             <asp:HiddenField ID="InterviewNoactionRadioHiddenField" runat="server" />
                                            <label for="radio3">
                                                <span class="inc"></span>
                                                <span class="check"></span>
                                                <span class="box"></span>لم يتم اتخاذ أي إجراء بعد</label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mr-20 ml-20">
                                <!--TimeTestReject2-->
                                <div class='col-sm-3'>
                                    <div class="form-group">
                                        <label for="TimeTestReject">التاريخ اختبار / رفض *</label>
                                        <div id="TimeTestReject2">
                                            <!-- Datepicker as text field -->
                                            <div class="input-group date" data-date-format="dd/mm/yyyy">
                                                <input type="text" class="form-control" name="InterviewDateText" placeholder="dd/mm/yyyy">
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
                                <!--TimeTest-->
                                <div class="col-sm-3 col-md-offset-1 ">
                                    <div class="form-group">
                                        <label for="StudientCountry">وقت الاختبار</label>
                                        <input type="text" id="StudientCountry" name="InterviewTimeText" class="form-control">
                                    </div>
                                </div>

                                 <div class="col-sm-3 col-md-offset-1 ">
                                                                           <asp:LinkButton ID="SendInterviewSMSLinkButton1"  class="btn grey-salsa btn-outline"  OnClick="SendInterviewSMSLinkButton1_Click" runat="server">إرسال تاريخ الاختبار / الرفض رسالة نصية</asp:LinkButton>

                                </div>

                                 <div class="col-sm-3 col-md-offset-1 ">
                                                                    <asp:LinkButton ID="ShowHideInterviewSMSHistoryLinkButton" OnClick="ShowHideInterviewSMSHistoryLinkButton_Click" class="btn grey-salsa btn-outline" runat="server">عرض سجل الرسائل</asp:LinkButton>

                                </div>
                                
                               
                                  
                            <div class="row mr-20 ml-20">
                                <div class="col-sm-11">
                                    <!--RejectionReason-->
                                    <div class="form-group">
                                        <asp:Panel ID="InterviewRejectionPanel" runat="server" Visible="false">
                                        <label for="RejectionReason">سبب الرفض</label>
                                        <textarea class="form-control" rows="3" id="RejectionReason" name="InterviewRejectionReasonText" placeholder="الرجاء إدخال سبب الرفض. يتم تمكين هذا الحقل عندما تكون قيمة الحقلمرفوض في الاختبار المطلوب"></textarea>
                                    </asp:Panel>
                                            </div>
                                </div>
                            </div>
                            <fieldset class="scheduler-border mr-30 ml-30">
                                <legend class="scheduler-border mb-0">نتيجة الاختبار</legend>
                                <div class="row mr-20 ml-20">
                                    <div class="col-sm-3 ">
                                        <div class="form-group">
                                            <!--TestResult-->
                                            <label for="TestResult">نتيجة الاختبار</label>
                                            <asp:DropDownList ID="DDLinterviewReslt" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>
                                           
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

                                    <div class="col-md-2 mt-30  col-md-offset-1">
                                        <asp:LinkButton ID="SendInterviewResultLinkButton1"  class="btn grey-salsa btn-outline"  OnClick="SendInterviewResultLinkButton1_Click" runat="server">إرسال نتيجة الاختبار</asp:LinkButton>

                                    </div>

                                </div>
                                <asp:GridView ID="InterviewSMSHistoryGridView" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" runat="server" Visible="False">
                                      <Columns>

                                          <asp:TemplateField>
                                              <HeaderTemplate>
                                                  <asp:Literal runat="server" ID="Literal69" Text="Message Text"></asp:Literal></HeaderTemplate>

                                              <ItemTemplate>
                                                  <asp:Label ID="MessageTextLabel" runat="server" Text='<%# Eval("MsgText") %>' Enabled="false"></asp:Label>


                                              </ItemTemplate>

                                          </asp:TemplateField>

                                          <asp:TemplateField>
                                              <HeaderTemplate>
                                                  <asp:Literal runat="server" ID="Literal69" Text="Number"></asp:Literal></HeaderTemplate>

                                              <ItemTemplate>
                                                  <asp:Label ID="MessageTextLabel" runat="server" Text='<%# Eval("MobileNumber") %>' Enabled="false"></asp:Label>
                                              </ItemTemplate>

                                          </asp:TemplateField>

                                          <asp:TemplateField>
                                              <HeaderTemplate>
                                                  <asp:Literal runat="server" ID="Literal69" Text="Date"></asp:Literal></HeaderTemplate>

                                              <ItemTemplate>
                                                  <asp:Label ID="StatusLabel" runat="server" Text='<%# Eval("MsgTime") %>' Enabled="false"></asp:Label>
                                              </ItemTemplate>

                                          </asp:TemplateField>

                                          <asp:TemplateField>
                                              <HeaderTemplate>
                                                  <asp:Literal runat="server" ID="Literal69" Text="Sender"></asp:Literal></HeaderTemplate>

                                              <ItemTemplate>
                                                  <asp:Label ID="StatusLabel" runat="server" Text='<%# Eval("MsgSender") %>' Enabled="false"></asp:Label>
                                              </ItemTemplate>

                                          </asp:TemplateField>

                                          <asp:TemplateField>
                                              <HeaderTemplate>
                                                  <asp:Literal runat="server" ID="Literal69" Text="Status"></asp:Literal></HeaderTemplate>

                                              <ItemTemplate>
                                                  <asp:Label ID="StatusLabel" runat="server" Text='<%# Eval("MsgStatus") %>' Enabled="false"></asp:Label>
                                              </ItemTemplate>

                                          </asp:TemplateField>

                                      </Columns>
                                  </asp:GridView>
                            </fieldset>
                        </fieldset>
                            <%--MohammedAlhanafi 19-02-2019 pay fees part--%>
                      <fieldset class="scheduler-border m-0 mb-30 col-sm-12 p-10 pb-30">
                          <legend class="scheduler-border mb-0">معلومات حجز المقاعد</legend>
                            
                                <fieldset class="scheduler-border mr-30 ml-30">
                                    <legend class="scheduler-border mb-0">دعوة لدفع رسوم الحجز مقعد</legend>
                              <div class="row mr-20 ml-20">
                              <div class="form-group form-md-radios mb-20">
                                  <div class="md-radio-inline">
                                    <!--CallInterview-->
                                    <div class="md-radio">
                                      <input type="radio" id="radio10" onclick="PayFeesRadioCheck();"  name="radioAgree" class="md-radiobtn">
                                      <label for="radio10">
                                          <span class="inc"></span>
                                          <span class="check"></span>
                                          <span class="box"></span>لم يتم اتخاذ أي إجراء بعد</label>
                                        <asp:HiddenField ID="NoActionTakenRadioHiddenField" runat="server" />
                                    </div>
                                    <!--Rejected-->
                                    <div class="md-radio">
                                      <input type="radio" id="radio11" name="radioAgree"  onclick="PayFeesRadioCheck();" class="md-radiobtn">
                                      <label for="radio11">
                                          <span class="inc"></span>
                                          <span class="check"></span>
                                          <span class="box"></span>طلب الدفع</label>
                                        <asp:HiddenField ID="PayFeesRequestRadioHiddenField" runat="server" />

                                    </div>
                                  </div>
                                  <div class="row mr-20 ml-20">
                                      <!--TimePay-->
                                       <div class="col-sm-2">
                                  <div class="form-group">
                                    <label for="ReservationFees">رسوم الحجز <span class="required"> * </span></label>
                                    <input type="text" id="ReservationFeesTextBoxForSMS" class="form-control" name="ReservationFeesTextBoxForSMS">
                                  </div>
                                </div>
                                      <div class='col-sm-3'>
                                        <div class="form-group">
                                          <label for="TimeTestReject">دفع رسوم في </label>
                                          <div id="TimeTestReject2">
                                            <!-- Datepicker as text field -->
                                            <div class="input-group date" data-date-format="dd/mm/yyyy">
                                              <input type="text" class="form-control" name="PayFeesOnDateTime" placeholder="dd/mm/yyyy">
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
                                      <div class="col-md-3 mt-30  col-md-offset-1 ">
                                          <asp:LinkButton ID="PayFeesSMSLinkButton" OnClick="PayFeesSMSLinkButton_Click" class="btn grey-salsa btn-outline" runat="server">ارسال رساله بتاريخ وقميه دفع المصاريف</asp:LinkButton>
                                       
                                      </div>
                                      <div class="col-md-2 mt-30  col-md-offset-1">
                                          <asp:LinkButton ID="ViewPayFeesSMSLinkButton1" OnClick="ViewPayFeesSMSLinkButton1_Click" class="btn grey-salsa btn-outline" runat="server">عرض سجل الرسائل</asp:LinkButton>

                                   
                                  </div>
                                    </div>
                                  <asp:GridView ID="PayFessMessageHistoryGridView" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" runat="server" Visible="False">
                                                        <Columns>
                                                           
                                                                     <asp:TemplateField>
                                                                <HeaderTemplate><asp:Literal runat="server" ID="Literal69" Text="Message Text"></asp:Literal></HeaderTemplate>

                                                                <ItemTemplate>
                                                                     <asp:Label ID="MessageTextLabel" runat="server" Text='<%# Eval("MsgText") %>' Enabled="false"></asp:Label>
                                                                 
                                                                   
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate><asp:Literal runat="server" ID="Literal69" Text="Number"></asp:Literal></HeaderTemplate>

                                                                <ItemTemplate>
                                                                     <asp:Label ID="MessageTextLabel" runat="server" Text='<%# Eval("MobileNumber") %>' Enabled="false"></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                               <asp:TemplateField>
                                                                <HeaderTemplate><asp:Literal runat="server" ID="Literal69" Text="Status"></asp:Literal></HeaderTemplate>

                                                                <ItemTemplate>
                                                                     <asp:Label ID="StatusLabel" runat="server" Text='<%# Eval("MsgStatus") %>' Enabled="false"></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                              </div>


                                
                            </div>
                            </fieldset>
                            <fieldset class="scheduler-border mr-30 ml-30">
                                <legend class="scheduler-border mb-0">نتيجة الاختبار</legend>
                                <div class="row mr-20 ml-20">
                                <!--ReservationFees-->
                                <div class="col-sm-2">
                                  <div class="form-group">
                                    <label for="ReservationFees">رسوم الحجز <span class="required"> * </span></label>
                                  
                                      <input type="number" id="PaidFeesTextBox" class="form-control" name="PaidFeesTextBox">
                                  
                                  </div>
                                </div>
                                <div class='col-sm-3'>
                                  <div class="form-group">
                                    <label for="TimeTestReject">دفع رسوم في </label>
                                    <div id="TimeTestReject2">
                                      <!-- Datepicker as text field -->
                                      <div class="input-group date" data-date-format="dd/mm/yyyy">
                                        <input type="text" class="form-control" name="PaidFeesDateTime" placeholder="dd/mm/yyyy">
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
                                  <div class="col-md-2 mt-30  col-md-offset-1">
                                          <asp:LinkButton ID="CLinkButton" OnClick="FeesPaidConfirmationLinkButton_Click" class="btn grey-salsa btn-outline" runat="server">تاكيد اتمام الحجز والدفع</asp:LinkButton>

                                   
                                  </div>
                                    <div class="col-md-2 mt-30  col-md-offset-1">
                                          <asp:LinkButton ID="ViewFeesPaidConfirmationLLinkButton" OnClick="ViewFeesPaidConfirmationLLinkButton_Click" class="btn grey-salsa btn-outline" runat="server">عرض سجل الرسائل</asp:LinkButton>

                                   
                                  </div>
                                    <asp:GridView ID="PayFessConfirmationMessageHistoryGridView" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" runat="server" Visible="False">
                                                        <Columns>
                                                           
                                                                     <asp:TemplateField>
                                                                <HeaderTemplate><asp:Literal runat="server" ID="Literal69" Text="Message Text"></asp:Literal></HeaderTemplate>

                                                                <ItemTemplate>
                                                                     <asp:Label ID="MessageTextLabel" runat="server" Text='<%# Eval("MsgText") %>' Enabled="false"></asp:Label>
                                                                 
                                                                   
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate><asp:Literal runat="server" ID="Literal69" Text="Number"></asp:Literal></HeaderTemplate>

                                                                <ItemTemplate>
                                                                     <asp:Label ID="MessageTextLabel" runat="server" Text='<%# Eval("MobileNumber") %>' Enabled="false"></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            
                                                               <asp:TemplateField>
                                                                <HeaderTemplate><asp:Literal runat="server" ID="Literal69" Text="Date"></asp:Literal></HeaderTemplate>

                                                                <ItemTemplate>
                                                                     <asp:Label ID="StatusLabel" runat="server" Text='<%# Eval("MsgTime") %>' Enabled="false"></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                                                     <asp:TemplateField>
                                                                <HeaderTemplate><asp:Literal runat="server" ID="Literal69" Text="Sender"></asp:Literal></HeaderTemplate>

                                                                <ItemTemplate>
                                                                     <asp:Label ID="StatusLabel" runat="server" Text='<%# Eval("MsgSender") %>' Enabled="false"></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                               <asp:TemplateField>
                                                                <HeaderTemplate><asp:Literal runat="server" ID="Literal69" Text="Status"></asp:Literal></HeaderTemplate>

                                                                <ItemTemplate>
                                                                     <asp:Label ID="StatusLabel" runat="server" Text='<%# Eval("MsgStatus") %>' Enabled="false"></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                </div>
                                --------
                            </fieldset>
                      </fieldset>

                        <%--MohammedAlhanafi 19-02-2019 pay fees part--%>

                      
                    </fieldset>
                  </div>
                </div>
            </div>
            <!-- END EXAMPLE TABLE PORTLET-->
          </div>
        </div>
      </section>



  <script>

                                  window.onload = function () {
                                      if (document.getElementById('<%=NoActionTakenRadioHiddenField.ClientID %>').value == "true")
                                      {
                                          document.getElementById('radio10').checked = true;

                                      }
                                      else if (document.getElementById('<%=PayFeesRequestRadioHiddenField.ClientID %>').value == "true")
                                      {
                                          document.getElementById('radio11').checked = true;

                                      }



                                      if (document.getElementById('<%=NoactionTestRadioHiddenField.ClientID %>').value == "true") {
                                          document.getElementById('radio6').checked = true;

                                      }
                                      else if (document.getElementById('<%=InviationTestaRadioHiddenField.ClientID %>').value == "true") {
                                          document.getElementById('radio7').checked = true;

                                      }
                                      else if (document.getElementById('<%=RejecttionTestRadioHiddenField.ClientID %>').value == "true") {
                                          document.getElementById('radio8').checked = true;

                                      }

                                      if (document.getElementById('<%=InterviewInvitationRadioHiddenField.ClientID %>').value == "true") {
                                          document.getElementById('radio1').checked = true;

                                      }
                                      else if (document.getElementById('<%=InterviewNoactionRadioHiddenField.ClientID %>').value == "true") {
                                           document.getElementById('radio3').checked = true;

                                       }
                                       else if (document.getElementById('<%=InterviewRejectionRadioHiddenField.ClientID %>').value == "true") {
                                          document.getElementById('radio2').checked = true;

                                      }




                                      
                                  };
                                  function send() {
                                      var genders = document.getElementsByName("gender");
                                      if (genders[0].checked == true) {
                                          alert("Your gender is male");
                                      } else if (genders[1].checked == true) {
                                          alert("Your gender is female");
                                      } else {
                                          // no checked
                                          var msg = '<span style="color:red;">You must select your gender!</span><br /><br />';
                                          document.getElementById('msg').innerHTML = msg;
                                          return false;
                                      }
                                      return true;
                                  }

                                  function PayFeesRadioCheck() {
                                      if (document.getElementById('radio10').checked) {
                                          // NoActionTakenRadioHiddenField checked
                                          document.getElementById('<%=NoActionTakenRadioHiddenField.ClientID %>').value = "true";
                                          document.getElementById('<%=PayFeesRequestRadioHiddenField.ClientID %>').value = "false";
                                     


                                      } else if (document.getElementById('radio11').checked) {
                                          document.getElementById('<%=NoActionTakenRadioHiddenField.ClientID %>').value = "false";
                                          document.getElementById('<%=PayFeesRequestRadioHiddenField.ClientID %>').value = "true";
                                         


                                         
                                      }



                                  }


                                  function TestRadioCheck() {
                                      if (document.getElementById('radio6').checked) {
                                          // NoActionTakenRadioHiddenField checked
                                          document.getElementById('<%=InviationTestaRadioHiddenField.ClientID %>').value = "false";
                                          document.getElementById('<%=RejecttionTestRadioHiddenField.ClientID %>').value = "false";
                                          document.getElementById('<%=NoactionTestRadioHiddenField.ClientID %>').value = "true";



                                      } else if (document.getElementById('radio7').checked) {
                                          document.getElementById('<%=InviationTestaRadioHiddenField.ClientID %>').value = "true";
                                          document.getElementById('<%=RejecttionTestRadioHiddenField.ClientID %>').value = "false";
                                          document.getElementById('<%=NoactionTestRadioHiddenField.ClientID %>').value = "false";




                                      }
                                      else if (document.getElementById('radio8').checked) {
                                          document.getElementById('<%=InviationTestaRadioHiddenField.ClientID %>').value = "false";
                                          document.getElementById('<%=RejecttionTestRadioHiddenField.ClientID %>').value = "true";
                                          document.getElementById('<%=NoactionTestRadioHiddenField.ClientID %>').value = "false";




                                      }



                                  }



                                  function InterviewRadioCheck() {
                                      if (document.getElementById('radio3').checked) {
                                          // NoActionTakenRadioHiddenField checked
                                          document.getElementById('<%=InterviewInvitationRadioHiddenField.ClientID %>').value = "false";
                                          document.getElementById('<%=InterviewRejectionRadioHiddenField.ClientID %>').value = "false";
                                          document.getElementById('<%=InterviewNoactionRadioHiddenField.ClientID %>').value = "true";



                                      } else if (document.getElementById('radio1').checked) {
                                          document.getElementById('<%=InterviewInvitationRadioHiddenField.ClientID %>').value = "true";
                                          document.getElementById('<%=InterviewRejectionRadioHiddenField.ClientID %>').value = "false";
                                          document.getElementById('<%=InterviewNoactionRadioHiddenField.ClientID %>').value = "false";




                                      }
                                      else if (document.getElementById('radio2').checked) {
                                          document.getElementById('<%=InterviewInvitationRadioHiddenField.ClientID %>').value = "false";
                                          document.getElementById('<%=InterviewRejectionRadioHiddenField.ClientID %>').value = "true";
                                          document.getElementById('<%=InterviewNoactionRadioHiddenField.ClientID %>').value = "false";




                                      }



                                  }
        </script>