<%@ Assembly Name="MOEHE.PSPES, Version=1.0.0.0, Culture=neutral, PublicKeyToken=677b80a9c9c0da8c" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangeLanguageUserControl.ascx.cs" Inherits="MOEHE.PSPES.Webparts.MultiLanguageSwitcher.ChangeLanguage.ChangeLanguageUserControl" %>



 <div class="col-md-5">
                                        <ul class="list-inline sm-pull-none sm-text-center text-right text-white mb-sm-20 mt-10 ">
                                            <li class="m-0 pl-10">
                                                <a href="#" data-toggle="modal" class="text-white">
                                                    <i class="fa fa-language ml-5 text-white">
                                                    </i>

<asp:LinkButton ID="ArabicHyperLink"  CausesValidation="false" runat="server" class="text-white" OnClick="ArabicHyperLink_Click">
                    <span class="username username-hide-on-mobile">
                        <asp:Literal ID="ArabicLiteral" runat="server" Text="العربية" />
                    </span>
                </asp:LinkButton> <asp:LinkButton ID="EnglishHyperLink" CausesValidation="false" runat="server" class="text-white" OnClick="EnglishHyperLink_Click">
                <span class="username username-hide-on-mobile">
                    <asp:Literal runat="server" ID="EnglishLiteral" Text="English" />
                </span>
            </asp:LinkButton> |                                                                                                </a>
                                            </li>
                                           
                                            <li class="m-0 pl-0 pr-10">
											 <asp:Panel ID="UserDetailsPanel"  runat="server">
                                                <a href="#" data-toggle="modal" class="text-white">
                                                    <i class="fa fa-user ml-5">
                                                    </i>

  <asp:Literal ID="ltusername" runat="server" Text="" /> |                                                                                                </a>
                                            </asp:Panel></li>
                                            
<li class="m-0 pl-0 pr-10">

                                                <a href="" id="SignLnk" runat="server" on  data-toggle="modal" class="text-white">
                                                    <i class="fa fa-sign-out  ml-5">
                                                    </i>

<asp:Literal ID="SignOutLiteral" runat="server" Text='<%$Resources:MOEHE.PSPES,SignOut%>' />   
<asp:Literal ID="SignInLiteral" runat="server" Text='<%$Resources:MOEHE.PSPES,SignIn%>' />                                                                                               </a>

                                                </a>
                                            </li>
                                        </ul>
                                    </div>



