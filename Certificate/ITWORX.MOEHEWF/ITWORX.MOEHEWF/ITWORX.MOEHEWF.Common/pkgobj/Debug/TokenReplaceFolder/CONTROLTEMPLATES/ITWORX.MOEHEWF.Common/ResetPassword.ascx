<%@ Assembly Name="ITWORX.MOEHEWF.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7b2931724f1d7d1c" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.ascx.cs" Inherits="ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.ResetPassword" %>
<asp:Label ID="lblPersonalIdMsg" runat="server"  Font-Bold="true" Text="<%$Resources:ITWORX.MOEHEWF.Common, PersonalIdMsg %>"></asp:Label>
<br />
<asp:Label ID="lblPhoneNumberEmailMsg" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX.MOEHEWF.Common, PhoneNumberEmailMsg %>"></asp:Label>
<br />
<asp:Label ID="lblPersonalID" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, PersonalID %>" ></asp:Label><span style="color:red"> * </span>
<asp:TextBox ID="txtPersonalID" runat="server"></asp:TextBox>
<asp:RequiredFieldValidator ID="reqPersonalID" runat="server" ControlToValidate="txtPersonalID" ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, RequiredPersonalID %>"  ForeColor="Red"  ValidationGroup="Reset"></asp:RequiredFieldValidator>
  <asp:RegularExpressionValidator ID="regPersonalID" runat="server"  ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, RegPersonalID %>" ForeColor="Red" ControlToValidate="txtPersonalID"  ValidationExpression="^\d{11}$" ValidationGroup="Reset"></asp:RegularExpressionValidator>
<br />
<asp:Label ID="lblBirthDate" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, BirthDate %>"></asp:Label><span style="color:red"> * </span>
 <SharePoint:DateTimeControl ID="dtBirthDate" runat="server" DateOnly="true" />
<asp:RequiredFieldValidator ID="reqBirthDate" runat="server"  ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, RequiredBirthDate %>" ControlToValidate="dtBirthDate$dtBirthDateDate" ForeColor="Red"  ValidationGroup="Reset"  ></asp:RequiredFieldValidator>
<asp:CompareValidator ID="compGraduationDate" runat="server"  ForeColor="Red"  Type="Date" Operator="DataTypeCheck" ControlToValidate="dtBirthDate$dtBirthDateDate"  ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, InvalidDate %>" ValidationGroup="Reset"></asp:CompareValidator>
<br />
<asp:Label ID="lblEmail" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, EmailAddress %>"></asp:Label><span style="color:red"> * </span>
 <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
 <asp:RequiredFieldValidator ID="reqEmail" runat="server" ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, RequiredEmail %>" ValidationGroup="Reset" ForeColor="Red" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
 <asp:RegularExpressionValidator ID="regEmail" ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, WrongEmailAddress %>" ValidationGroup="Reset" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" runat="server" ForeColor="Red" ControlToValidate="txtEmail"></asp:RegularExpressionValidator>
<br />
<asp:Label ID="lblMobileNumber" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, MobileNumber %>"></asp:Label><span style="color:red"> * </span>
<asp:TextBox ID="txtMobileNumber" runat="server"></asp:TextBox>
<asp:RequiredFieldValidator ID="reqMobileNumber" runat="server" ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, RequiredMobileNumber %>" ValidationGroup="Reset" ForeColor="Red" ControlToValidate="txtMobileNumber"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="regMobileNumber" runat="server"  ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, RegMobileNumber %>" ForeColor="Red" ControlToValidate="txtMobileNumber"  ValidationExpression="^\d{8,13}$" ValidationGroup="Reset"></asp:RegularExpressionValidator>
<br />
<asp:Label ID="lblCaptchaMsg" runat="server" ForeColor="Red" Text="<%$Resources:ITWORX.MOEHEWF.Common, CaptchaMsg %>"></asp:Label>
<br />
<asp:Button ID="btnResetPassword" runat="server" ValidationGroup="Reset" Text="<%$Resources:ITWORX.MOEHEWF.Common, ResetPassword %>" />
<asp:Button ID="btnLogout" runat="server"  Text="<%$Resources:ITWORX.MOEHEWF.Common, Logout %>"/>
