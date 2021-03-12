<%--<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>--%>

<%@ Assembly Name="ITWORX.MOEHEWF.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7b2931724f1d7d1c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UCLogin.aspx.cs" Inherits="ITWORX.MOEHEWF.Common.Layouts.ITWORX.MOEHEWF.Common.UCLogin" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Login Form</title>
    <!--Disable Compatiblety Button-->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

    <!--Meta for mobile-->
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
</head>
<body>
   
       <form class="form" runat="server" autocomplete="on">
            <asp:Label ID="lblUsername" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, UserName %>"></asp:Label>
            <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="reqUserName" runat="server" ForeColor="Red" ControlToValidate="txtUsername" Text="<%$Resources:ITWORX.MOEHEWF.Common, RequiredUserName %>" ValidationGroup="Login"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="lblPassword" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Password %>"></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="reqPassword" runat="server" ForeColor="Red" ControlToValidate="txtPassword" Text="<%$Resources:ITWORX.MOEHEWF.Common, RequiredPassword %>" ValidationGroup="Login"></asp:RequiredFieldValidator>
            <br />
            <asp:Button ID="btnLogin" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Login %>" ValidationGroup="Login" OnClick="btnLogin_Click" />
            <asp:Button ID="btnLogout" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Logout %>" OnClick="btnLogout_Click" />
            <asp:Label ForeColor="Red" runat="server" ID="lblMessage" Text="<%$Resources:ITWORX.MOEHEWF.Common, LoginFailure %>" Visible="false" />
</form>
</body>
</html>