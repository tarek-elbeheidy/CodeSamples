<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="uc1" TagName="FileUpload" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReviewerDisplayApplicantDetails.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.ReviewerDisplayApplicantDetails" %>


<%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>--%>
<script type="text/javascript">
    function Print(a) {
        var row = $(a).closest("tr").clone(true);
        var printWin = window.open('', '', 'left=0", ",top=0,width=1000,height=600,status=0');
        var table = $("[id*=applicantDetails]").clone(true);
        $("tr", table).not($("tr:first-child", table)).remove();
        table.append(row);
        $("tr td:last,tr th:last", table).remove();
        var dv = $("<div />");
        dv.append(table);
        printWin.document.write(dv.html());
        printWin.document.close();
        printWin.focus();
        printWin.print();
        printWin.close();
    }
</script>

<div id="applicantDetails">

    <div class="container heighlighted-section margin-bottom-50">
        <div class="row">

      
        <div class="col-md-4 col-sm-6 auto-height">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lblName" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Name %>" Font-Bold="true"></asp:Label>

                </h6>
                <h5 class="font-size-20">
                    <asp:Label ID="lblNameValue" runat="server"></asp:Label>
                </h5>
            </div>
        </div>

        <div class="col-md-3 col-sm-6 auto-height">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lblPersonalID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PersonalID %>" Font-Bold="true"></asp:Label>

                </h6>
                <h5 class="font-size-20">
                    <asp:Label ID="lblPersonalIDValue" runat="server"></asp:Label>
                </h5>
            </div>
        </div>

        <div class="col-md-3 col-sm-6 auto-height">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lblBirthDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, BirthDate %>" Font-Bold="true"></asp:Label>

                </h6>
                <h5 class="font-size-20">
                    <asp:Label ID="lblBirthDateValue" runat="server"></asp:Label>
                </h5>
            </div>
        </div>



        <div class="col-md-2 col-sm-6 auto-height">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lblNationality" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Nationality %>" Font-Bold="true"></asp:Label>

                </h6>
                <h5 class="font-size-20">
                    <asp:Label ID="lblNationalityValue" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
  </div>
        <div class="row margin-top-15">

 
        <div class="col-md-4 col-sm-6 auto-height">
            <div class="data-container no-margin-imp">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lblEmail" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EmailAddress %>" Font-Bold="true"></asp:Label>
                </h6>
                <h5 class="font-size-20">

                    <asp:Label ID="lblEmailValue" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
        <div class="col-md-3 col-sm-6 auto-height">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lblMobileNumber" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, MobileNumber %>" Font-Bold="true"></asp:Label>
                </h6>
                <h5 class="font-size-20">
                    <asp:Label ID="lblMobileNumberValue" runat="server"></asp:Label>

                </h5>
            </div>
        </div>
        <div class="col-md-3 col-sm-6 auto-height">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lblNationalityCategory" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NationalityCategory %>" Font-Bold="true"></asp:Label>

                </h6>
                <h5 class="font-size-20">
                    <asp:Label ID="lblNationalityCategoryValue" runat="server"></asp:Label>

                </h5>
            </div>
        </div>


        </div>




    </div>
    <!--End Of Highlighted Section-->


    <div class="container unheighlighted-section margin-bottom-50">
        <div class="row">

        
        <div class="col-md-6 col-sm-6 auto-height">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lblRegionNo" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RegionNo %>" Font-Bold="true"></asp:Label>

                </h6>
                <h5 class="font-size-20">
                    <asp:Label ID="lblRegionNoValue" runat="server"></asp:Label>
                </h5>
            </div>
        </div>

        <div class="col-md-6 col-sm-6 auto-height">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lblStreetNo" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, StreetNo %>" Font-Bold="true"></asp:Label>

                </h6>
                <h5 class="font-size-20">
                    <asp:Label ID="lblStreetNoValue" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
        </div>

        <div class="row margin-top-15">

        <div class="col-md-6 col-sm-6 auto-height">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lblBuildingNo" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, BuildingNo %>" Font-Bold="true"></asp:Label>

                </h6>
                <h5 class="font-size-20">
                    <asp:Label ID="lblBuildingNoValue" runat="server"></asp:Label>
                </h5>
            </div>
        </div>

        <div class="col-md-6 col-sm-6 auto-height">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lblApartmentNo" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ApartmentNo %>" Font-Bold="true"></asp:Label>

                </h6>
                <h5 class="font-size-20">
                    <asp:Label ID="lblApartmentNoValue" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
        
</div>
        <div class="row margin-top-15">

       
        <div class="col-md-6 col-sm-6 auto-height">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lblPostalNumber" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PostalNumber %>" Font-Bold="true"></asp:Label>

                </h6>
                <h5 class="font-size-20">
                    <asp:Label ID="lblPostalNumberValue" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
         </div>

        <div class="row margin-top-15">
            <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lblDetailedAddress" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, DetailedAddress %>" Font-Bold="true"></asp:Label>

                </h6>
                <h5 class="font-size-20">
                    <asp:Label ID="lblDetailedAddressValue" runat="server" TextMode="MultiLine"></asp:Label>
                </h5>
            </div>
        </div>

        </div>
        
        <div class="row margin-top-15">
            <div class="col-md-7 col-sm-12 col-xs-12">
        <uc1:FileUpload runat="server" id="fileUploadNationalID" />
    </div>


        </div>

        <div class="row margin-top-15">
                <div class="col-md-7 col-sm-12 col-xs-12">
        <uc1:FileUpload runat="server" id="fileUploadPassport" />
    </div>
        </div>
    </div>
    <!--End Of UnHighlighted Section-->

    


    <div class="row no-padding">
        <asp:Button ID="btn_Print" Visible="false" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Print %>" OnClientClick="Print(this)" CssClass="btn moe-btn pull-right" />
    </div>
</div>



