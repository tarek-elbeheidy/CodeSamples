<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.PA/PAClarificationRequests.ascx" TagPrefix="uc1" TagName="PAClarificationRequests" %>

<%--<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.PA/PANewRequests.ascx" TagPrefix="uc1" TagName="PANewRequests" %>--%>
<%--<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.PA/PARejectedRequests.ascx" TagPrefix="uc1" TagName="RejectedRequests" %>--%>


<%--<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.PA/PALateRequests.ascx" TagPrefix="uc1" TagName="LateRequests" %>--%>


<%--<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.PA/PASearchRequests.ascx" TagPrefix="uc2" TagName="SearchRequests" %>--%>
<%--<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.PA/PASimilarRequests.ascx" TagPrefix="uc2" TagName="SimilarRequests" %>--%>




<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TestWebPartUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.WebParts.TestWebPart.TestWebPartUserControl" %>

<%--<uc1:LateRequests runat="server" id="LateRequests" />--%>

<%--<uc2:SearchRequests runat="server" id="SearchRequests" />--%>

<%--<uc2:SimilarRequests runat="server" id="SimilarRequests" />--%>
<%--<uc1:PANewRequests runat="server" id="PANewRequests" />--%>
<%--<uc1:RejectedRequests runat="server" id="RejectedRequests" />--%>
<uc1:PAClarificationRequests runat="server" id="PAClarificationRequests" />
