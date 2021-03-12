<%@ Assembly Name="ITWORX.MOEHEWF.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7b2931724f1d7d1c" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginIn.ascx.cs" Inherits="ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.LoginIn" %>
<br />
<br />

<%--<asp:Label ForeColor="Red" runat="server" ID="lblMessage" Text="<%$Resources:ITWORX.MOEHEWF.Common, LoginFailure %>" />--%>


<div class="user-form">
            <form>
                <div class="form-header row text-center">
                    <h3 class="margin-tb-0">مرحبا بك في</h3>
                        <img class="form-logo" src="/_catalogs/masterpage/MOEHE/common/img/logo1.png"/>
                        <h2 class="margin-tb-0">إدارة معادلة الشهادات</h2>
                </div>
                <div class="form-content row">
                    <h2>Login</h2>
                    <div class="col-md-12">
                        <div class="form-group">
                            <label class="row">
                                <asp:Label ID="lblUsername" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, UserName %>"></asp:Label>

                            </label>
                         

                            <asp:TextBox ID="txtUsername" runat="server" class="form-control" type="email"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqUserName" runat="server" ForeColor="Red" ControlToValidate="txtUsername" Text="<%$Resources:ITWORX.MOEHEWF.Common, RequiredUserName %>" CssClass="error-msg" ValidationGroup="Login"></asp:RequiredFieldValidator>

                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                            <label class="row">
                                <asp:Label ID="lblPassword" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Password %>"  ></asp:Label>

                            </label>
                            <input >
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
<asp:RequiredFieldValidator ID="reqPassword" runat="server" ForeColor="Red" ControlToValidate="txtPassword" Text="<%$Resources:ITWORX.MOEHEWF.Common, RequiredPassword %>"  CssClass="error-msg" ValidationGroup="Login"></asp:RequiredFieldValidator>

                        </div>
                    </div>
                    
                    
                    
                    
                    

                    

                    

                    <div class="col-md-12 no-padding text-center">
                        <div class="form-group">
                            <button type="submit" class="btn defualt-btn">دخول</button>
                            <asp:Button ID="btnLogin" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, Login %>"  ValidationGroup="Login" OnClick="btnLogin_Click"  CssClass="btn defualt-btn"/>
<asp:Button ID="btnLogout" runat="server"  Text="<%$Resources:ITWORX.MOEHEWF.Common, Logout %>" OnClick="btnLogout_Click1" CssClass="btn defualt-btn"/>
                        </div>
                    </div>

                </div>
            </form>
        </div>
