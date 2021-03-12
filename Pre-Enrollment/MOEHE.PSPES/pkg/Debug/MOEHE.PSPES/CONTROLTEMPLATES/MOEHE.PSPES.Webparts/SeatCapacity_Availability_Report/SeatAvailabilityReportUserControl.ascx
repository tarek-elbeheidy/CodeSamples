<%@ Assembly Name="MOEHE.PSPES, Version=1.0.0.0, Culture=neutral, PublicKeyToken=677b80a9c9c0da8c" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SeatAvailabilityReportUserControl.ascx.cs" Inherits="MOEHE.PSPES.Webparts.SeatCapacity_Availability_Report.SeatCapacity_Availability_ReportUserControl" %>
<link href="/_layouts/15/MOEHE.PSPES/assets/css/chosen.css" rel="stylesheet" type="text/css" />
<link href="/_layouts/15/MOEHE.PSPES/assets/css/beautiful-checkbox.css" rel="stylesheet" type="text/css" />
<script src="/_layouts/15/MOEHE.PSPES/scripts/bootstrap-multiselect.js" type="text/javascript"></script>
<link href="/_layouts/15/MOEHE.PSPES/assets/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
<section>
    <div class="container bg-white-theme pt-0 pb-10 mt-50 mb-50">
        <div class="row">
            <!-- BEGIN EXAMPLE TABLE PORTLET-->
            <div class="portlet light ">
                 <div class="portlet-title">
                    <div class="caption font-red">
                        <i class="fa fa-cogs font-red" aria-hidden="true" style="font-size: 20px"></i>
                        <span class="caption-subject bold uppercase">
                            <asp:Literal ID="ltAllInOne" runat="server" Text="<%$Resources:MOEHE.PSPES,SEAT_CAPACITY_REPORT_PAGE_TITLE%>"></asp:Literal>
                        </span>
                    </div>
                </div>
            </div>
        <!-- END EXAMPLE TABLE PORTLET-->
        </div>
    </div>
</section>
