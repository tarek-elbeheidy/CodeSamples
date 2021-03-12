<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegisterationUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.Common.WebParts.Registeration.RegisterationUserControl" %>

<asp:Label ID="lblPersonalIdMsg" runat="server" Font-Bold="true"  Text="<%$Resources:ITWORX.MOEHEWF.Common, PersonalIdMsg %>"></asp:Label>
<br />
<asp:Label ID="lblPhoneNumberEmailMsg" runat="server" Font-Bold="true" Text="<%$Resources:ITWORX.MOEHEWF.Common, PhoneNumberEmailMsg %>"></asp:Label>
<br />
<asp:Label ID="lblPersonalID" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, PersonalID %>" ></asp:Label><span style="color:red"> * </span>
<asp:TextBox ID="txtPersonalID" runat="server"></asp:TextBox>
<asp:RequiredFieldValidator ID="reqPersonalID" runat="server" ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, RequiredPersonalID %>" ControlToValidate="txtPersonalID" ForeColor="Red"  ValidationGroup="Create"></asp:RequiredFieldValidator>
  <asp:RegularExpressionValidator ID="regPersonalID" runat="server"  ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, RegPersonalID %>" ForeColor="Red" ControlToValidate="txtPersonalID"  ValidationExpression="^\d{11}$" ValidationGroup="Create"></asp:RegularExpressionValidator>
<br />
<asp:Label ID="lblBirthDate" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, BirthDate %>"></asp:Label><span style="color:red"> * </span>
 <SharePoint:DateTimeControl ID="dtBirthDate" runat="server" DateOnly="true" />
<asp:RequiredFieldValidator ID="reqBirthDate" runat="server"  ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, RequiredBirthDate %>" ControlToValidate="dtBirthDate$dtBirthDateDate" ForeColor="Red"  ValidationGroup="Create"  ></asp:RequiredFieldValidator>
<asp:CompareValidator ID="compGraduationDate" runat="server"  ForeColor="Red"  Type="Date" Operator="DataTypeCheck" ControlToValidate="dtBirthDate$dtBirthDateDate"  ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, InvalidDate %>" ValidationGroup="Create"></asp:CompareValidator>
<br />
<asp:Label ID="lblEmail" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, EmailAddress %>"></asp:Label><span style="color:red"> * </span>
 <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
 <asp:RequiredFieldValidator ID="reqEmail" runat="server" ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, RequiredEmail %>" ValidationGroup="Create" ForeColor="Red" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
 <asp:RegularExpressionValidator ID="regEmail" ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, WrongEmailAddress %>" ValidationGroup="Create" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" runat="server" ForeColor="Red" ControlToValidate="txtEmail"></asp:RegularExpressionValidator>
<br />
<asp:Label ID="lblMobileNumber" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, MobileNumber %>"></asp:Label><span style="color:red"> * </span>
<asp:TextBox ID="txtMobileNumber" runat="server"></asp:TextBox>
<asp:RequiredFieldValidator ID="reqMobileNumber" runat="server" ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, RequiredMobileNumber %>" ValidationGroup="Create" ForeColor="Red" ControlToValidate="txtMobileNumber"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="regMobileNumber" runat="server"  ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, RegMobileNumber %>" ForeColor="Red" ControlToValidate="txtMobileNumber" ValidationExpression="^\d{8,13}$" ValidationGroup="Create"></asp:RegularExpressionValidator>
<br />
<asp:Label ID="lblNationality" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Nationality %>"></asp:Label><span style="color:red"> * </span>
<asp:DropDownList ID="dropNationailty" runat="server" CssClass="moe-dropdown"></asp:DropDownList>
<asp:RequiredFieldValidator ID="reqNationality" runat="server" ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, RequiredNationality %>" ValidationGroup="Create" ForeColor="Red" ControlToValidate="dropNationailty"></asp:RequiredFieldValidator>
<asp:Label ID="lblNationalityCategory" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, NationalityCategory %>"></asp:Label>
<asp:DropDownList ID="dropNationalityCategory" runat="server" CssClass="moe-dropdown"></asp:DropDownList>
<br />
<asp:Label ID="lblGender" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Gender %>"></asp:Label><span style="color:red"> * </span>
<asp:RadioButtonList ID="rblGender" runat="server"></asp:RadioButtonList>
<asp:RequiredFieldValidator ID="reqGender" runat="server" ErrorMessage="<%$Resources:ITWORX.MOEHEWF.Common, RequiredGender %>" ValidationGroup="Create" ForeColor="Red" ControlToValidate="rblGender"></asp:RequiredFieldValidator>
<br />
<asp:Label ID="lblAttachments" runat="server" Font-Bold="true" Font-Underline="true" Text="<%$Resources:ITWORX.MOEHEWF.Common, Attachments %>"></asp:Label><span style="color:red"> * </span>
<br />
<asp:Label ID="lblAttachmentName" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, AttachmentName %>"></asp:Label>
<asp:DropDownList ID="dropAttachments" runat="server" CssClass="moe-dropdown"></asp:DropDownList>
<asp:FileUpload  ID="uploadAttachments" runat="server"/>
<asp:LinkButton ID="lnkAattachments" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Upload %>"></asp:LinkButton>
<asp:GridView ID="gridAttachments" runat="server" AutoGenerateColumns="false"></asp:GridView>
<asp:Label ID="lblCaptchaMsg" runat="server" ForeColor="Red" Text="<%$Resources:ITWORX.MOEHEWF.Common, CaptchaMsg %>"></asp:Label>
<br />
<asp:Button ID="btnCreateUser" runat="server" ValidationGroup="Create" Text="<%$Resources:ITWORX.MOEHEWF.Common, CreateNewUser %>" OnClick="btnCreateUser_Click" />
<asp:Button ID="btnLogout" runat="server"  Text="<%$Resources:ITWORX.MOEHEWF.Common, Logout %>" OnClick="btnLogout_Click"/>