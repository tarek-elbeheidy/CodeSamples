<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApplicationCompletion.ascx.cs" Inherits="MOEHE.PSPES.Webparts.ApplicationCompletion.ApplicationCompletion" %>

     <div>
                <fieldset>
                    <legend style="color: blue; font-weight: bold;">Search criteria </legend>

                    <%--<span>QID:                   
                        <asp:TextBox id = "TxtQID" runat="server"></asp:TextBox>
                       </span>--%>
                    <span>Application Reference Number:                   
                        <asp:TextBox id = "TxtAppRef" runat="server"></asp:TextBox>
                       </span><span>   Student Qatar ID (QID):                   
                        <asp:TextBox id = "TxtQID" runat="server"></asp:TextBox>
                       </span>
                    <br/>
                    <%--<asp:Button ID="BtnSrch" runat="server"   Text="Search" OnClick="BtnSrch_Click"  />--%>
                    <asp:LinkButton ID="BtnSrch" runat="server" Text="Search"  OnClick="BtnSrch_Click" CssClass="ajouter btn btn-xl btn-theme-colored2 pr-40 pl-40 center-block"></asp:LinkButton> 
                    <br />
                    <asp:Label ID="LblappRefNum" runat="server" ForeColor="Red" Visible="false"></asp:Label>

                </fieldset>
            </div>

<br />
 <div>
                <fieldset>
                    <legend style="color: blue; font-weight: bold;">Applicant Data </legend>

                    <span>Student Name:                   
                        <asp:TextBox id = "TxtStudentNm" runat="server"></asp:TextBox>
                       </span>
                    <span>PreEnrollment Grade:                   
                        <asp:TextBox id = "TxtPreEnGrade" runat="server"></asp:TextBox>
                       </span><br/>
                    <span>PreEnrollment School:                   
                        <asp:TextBox id = "TxtPreEnScl" runat="server"></asp:TextBox>
                       </span>
                     <span>Waitlist Number:                   
                        <asp:TextBox id = "TxtWListNum" runat="server"></asp:TextBox>
                       </span><br/>

                </fieldset>
            </div>
<br />
<div>
                <fieldset>
                    <legend style="color: blue; font-weight: bold;">Supporting Documents </legend>

  <fieldset>
                    <legend style="color: blue; font-weight: bold;">Applicant Bio Data</legend>

      <span>Application Date:                   
                        <asp:TextBox id = "TxtAppDT" runat="server" Height="16px" Width="144px"></asp:TextBox>
                       </span><br/>
      <span>Gender:                   
                        <asp:TextBox id = "TxtGender" runat="server"></asp:TextBox>
                       </span><br/>
      <span>Nationality:                   
                        <asp:TextBox id = "TxtNationality" runat="server"></asp:TextBox>
                       </span><br/>



      </fieldset>
</fieldset>
    </div>

<br />
<div>
                <fieldset>
                <legend style="color: blue; font-weight: bold;">Call for Test </legend>

                    <asp:RadioButtonList ID="RBtnListTest" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="RBtnListTest_SelectedIndexChanged" ><%--onselectedindexchanged="RBtnListTest_SelectedIndexChanged"--%>
                    <asp:ListItem Text="Call For Test" Value="1" ></asp:ListItem>
                    <asp:ListItem Text="Rejected" Value="2" ></asp:ListItem>
                     <asp:ListItem Text="No Action Taken" Value="3" Selected="True" ></asp:ListItem>
                    </asp:RadioButtonList>


                    <span>Date:                    
                    
                    <SharePoint:DateTimeControl ID="DTControlTest" FirstDayOfWeek="0" AutoPostBack="True" 
                            ToolTip="DD/MM/YYYY" runat="server" IsRequiredField="false" DateOnly="true" ErrorMessage="Please select a date">
                      </SharePoint:DateTimeControl></span>
                    <br/>

                     <span>Time: <asp:TextBox id = "TxtTime" runat="server"></asp:TextBox>
                     </span><br/>

                    <span>Rejection Reason:                   
                        <asp:TextBox id = "TxtRejctRsn" runat="server" Height="84px" Width="443px"></asp:TextBox>
                       </span><br/>

                     <span>                  
                       <asp:Button ID="btnSendSMStst" runat="server" Text="Send Test SMS" OnClick="btnSendSMStst_Click" /> 
                         <asp:Button ID="btnViewTestSMSHistory" runat="server" Text="View SMS history" OnClick="btnViewTestSMSHistory_Click" />
                       </span><br/>

                    <asp:GridView ID="GrdVTest" runat="server" Visible="False"></asp:GridView>
                    <br />
                </fieldset>
 </div><br />
<div>
                <fieldset>
                <legend style="color: blue; font-weight: bold;">Test Result</legend>
 <span>Test Result:  <asp:DropDownList ID="DDLTestResult" runat="server" AutoPostBack="true"></asp:DropDownList>
                     </span><br/>
                    <span>Upload Test Result:  <asp:FileUpload ID="FuploadTestReslt" runat="server" />
                      
                       
                      <asp:HyperLink ID="HlinkTestResult" Visible=" false" runat="server">Test Result</asp:HyperLink>
                      
                       
                      <asp:Button ID="btnTestRSTUpload" runat="server" Text="Upload" OnClick="btnTestRSTUpload_Click" />
                      <asp:Button ID="btnTestRsltDownload" runat="server" Text="Download" Visible="False" />
                     </span>
                    <br />
                    <span>                  
                       <asp:Button ID="btnTestResultSMS" runat="server" Text="Send Test Result SMS" OnClick="btnTestResultSMS_Click" /> 
                       </span><br/>
                </fieldset>
    </div>
<br />
<div>
                <fieldset>
                <legend style="color: blue; font-weight: bold;">Call for InterView </legend>

                    <asp:RadioButtonList ID="RBtnListInterview" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="RBtnListInterview_SelectedIndexChanged" ><%--onselectedindexchanged="RBtnListTest_SelectedIndexChanged"--%>
                    <asp:ListItem Text="Call For Interview" Value="1" ></asp:ListItem>
                    <asp:ListItem Text="Rejected Interview" Value="2" ></asp:ListItem>
                     <asp:ListItem Text="No Action Taken" Value="3" Selected="True" ></asp:ListItem>
                    </asp:RadioButtonList>


                    <span>Date:                    
                    
                    <SharePoint:DateTimeControl ID="DTControlInterview" FirstDayOfWeek="0" AutoPostBack="True" 
                            ToolTip="DD/MM/YYYY" runat="server" IsRequiredField="false" DateOnly="true" ErrorMessage="Please select a date">
                      </SharePoint:DateTimeControl></span>
                    <br/>

                     <span>Time: <asp:TextBox id = "TxtInterTime" runat="server"></asp:TextBox>
                     </span><br/>

                    <span>Rejection Reason:                   
                        <asp:TextBox id = "TxtIntRejReason" runat="server" Height="84px" Width="443px"></asp:TextBox>
                       </span><br/>

                     <span>                  
                       <asp:Button ID="btnSendSMSInterview" runat="server" Text="Send Interview SMS" OnClick="btnSendSMSInterview_Click" /> 
                                              <asp:Button ID="btnViewInterviewSMSHistory" runat="server" Text="View SMS history" OnClick="btnViewInterviewSMSHistory_Click" />  
                     </span><br/>

                    <asp:GridView ID="GrdVInterview" runat="server" Visible="false"></asp:GridView>
                    <br />
                </fieldset>
 </div><br />

<div>
                <fieldset>
                <legend style="color: blue; font-weight: bold;">Interview Result</legend>
 <span>Test Result:  <asp:DropDownList ID="DDLinterviewReslt" runat="server" AutoPostBack="true"></asp:DropDownList>
                     </span><br/>
                    <span>
<SharePoint:DateTimeControl ID="DateTimeControl1" FirstDayOfWeek="0" AutoPostBack="True" 
                            ToolTip="DD/MM/YYYY" runat="server" IsRequiredField="false" DateOnly="true" ErrorMessage="Please select a date">
                      </SharePoint:DateTimeControl>

                     </span>
                    <br />
                    <span>                  
                       <asp:Button ID="BtnSendIntResultSMS" runat="server" Text="Send Interview Result SMS" OnClick="BtnSendIntResultSMS_Click" /> 
                       </span><br/>
                </fieldset>
    </div>


