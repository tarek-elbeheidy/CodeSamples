<%@ Assembly Name="ITWORX.MOEHEWF.UCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=883afb4c05a35fe5" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="uc1" TagName="FileUpload" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApplicantDetails.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.ApplicantDetails" %>



<div class="row heighlighted-section margin-bottom-50" id="applicantData">
    <div class="col-md-4 col-sm-6 col-xs-12">
        <div class="data-container no-margin-imp">
            <h6 class="font-size-16 margin-bottom-15">
                <asp:Label ID="lblApplicantName" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ApplicantName %>" Font-Bold="true"></asp:Label>
            </h6>
            <h5 class="font-size-20">
                <asp:Label ID="lblApplicantNameValue" runat="server" Text=""></asp:Label>
            </h5>
        </div>
    </div>
    <div class="col-md-3 col-sm-6 col-xs-12">
        <div class="data-container moe-sm-85-width pull-sm-right">
            <h6 class="font-size-16 margin-bottom-15">
                <asp:Label ID="lblPersonalID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PersonalID %>" Font-Bold="true"></asp:Label>
            </h6>
            <h5 class="font-size-20">
                <asp:Label ID="lblPersonalIDValue" runat="server" Text=""></asp:Label>
            </h5>
        </div>
    </div>
    <div class="col-md-3 col-sm-6 col-xs-12">
        <div class="data-container ">
            <h6 class="font-size-16 margin-bottom-15">
                <asp:Label ID="lblBirthDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, BirthDate %>" Font-Bold="true"></asp:Label>
            </h6>
            <h5 class="font-size-20">
                <asp:Label ID="lblBirthDateValue" runat="server" Text=""></asp:Label>
            </h5>
        </div>
    </div>
    <div class="col-md-2 col-sm-6 col-xs-12">
        <div class="data-container moe-sm-85-width pull-sm-right">
            <h6 class="font-size-16 margin-bottom-15">
                <asp:Label ID="lblNationality" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Nationality %>" Font-Bold="true"></asp:Label>
            </h6>
            <h5 class="font-size-20">
                <asp:Label ID="lblNationalityValue" runat="server" Text=""></asp:Label>
            </h5>
        </div>
    </div>

    <div class="col-md-4 col-sm-6 col-xs-12">
        <div class="data-container no-margin-imp">
            <h6 class="font-size-16 margin-bottom-15">
                <asp:Label ID="lblEmail" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EmailAddress %>" Font-Bold="true"></asp:Label>
            </h6>
            <h5 class="font-size-20">
                <asp:Label ID="lblEmailValue" runat="server" Text=""></asp:Label>
            </h5>
        </div>
    </div>


    <div class="col-md-3 col-sm-6 col-xs-12">
        <div class="data-container  moe-sm-85-width pull-sm-right">
            <h6 class="font-size-16 margin-bottom-15">
                <asp:Label ID="lblMobileNumber" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, MobileNumber %>" Font-Bold="true"></asp:Label>
            </h6>
            <h5 class="font-size-20">
                <asp:Label ID="lblMobileNumberValue" runat="server" Text=""></asp:Label>
            </h5>
        </div>
    </div>
    <div class="col-md-3 col-sm-6 col-xs-12">
        <div class="data-container">
            <h6 class="font-size-16 margin-bottom-15">
                <asp:Label ID="lblNationalityCategory" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NationalityCategory %>" Font-Bold="true"></asp:Label>
            </h6>
            <h5 class="font-size-20">
                <asp:Label ID="lblNationalityCategoryValue" runat="server" Text=""></asp:Label>
            </h5>
        </div>
    </div>

</div>




<div class="row unheighlighted-section margin-bottom-50 flex-display flex-wrap applicantDetailsForm">
        <div class="col-md-12 col-xs-12 margin-top-15 margin-bottom-25">
        <h5 class="font-size-18 margin-bottom-0 margin-top-0 instruction-details underline color-black font-family-sans">
            <asp:Label ID="lblAttachmentNote" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, AttachmentNote %>"></asp:Label>

        </h5>
    </div>
    <div class="col-md-3 col-sm-6 col-xs-12 margin-top-10 margin-bottom-10">
        <div class="data-container table-display moe-full-width moe-sm-95-width margin-sm-0">
            <div class="form-group">
                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                    <asp:Label ID="lblRegionNo" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, RegionNo %>"></asp:Label>
                    <span class="error-msg">*</span>
                </h6>

                <asp:TextBox ID="txtRegionNo" runat="server" MaxLength="4" CssClass="moe-full-width input-height-42 border-box moe-input-padding"></asp:TextBox>
                <asp:RegularExpressionValidator ID="regRegionNo" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, Reg4NumbersOnly %>" CssClass="moe-full-width error-msg" ValidationExpression="^[0-9]{1,4}$" ControlToValidate="txtRegionNo" ValidationGroup="Submit" Display="Dynamic"></asp:RegularExpressionValidator>
                <%--<asp:RequiredFieldValidator ID="reqRegionNo" runat="server"  ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredRegionNo  %>" ControlToValidate="txtRegionNo"  CssClass="moe-full-width error-msg"  ValidationGroup="Submit" Display="Dynamic"  ></asp:RequiredFieldValidator>--%>
            </div>
        </div>
    </div>

    <div class="col-md-3 col-sm-6 col-xs-12 margin-top-10 margin-bottom-10">
        <div class="data-container table-display moe-full-width  moe-sm-95-width margin-sm-0">
            <div class="form-group">
                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                    <asp:Label ID="lblStreetNo" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, StreetNo %>"></asp:Label>
                    <span class="error-msg">*</span>
                </h6>

                <asp:TextBox ID="txtStreetNo" runat="server" MaxLength="4" CssClass="moe-full-width input-height-42 border-box moe-input-padding"></asp:TextBox>
                <asp:RegularExpressionValidator ID="regStreetNo" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RegNumbersOnly %>" ValidationExpression="^[0-9]{1,4}$" ControlToValidate="txtStreetNo" CssClass="moe-full-width error-msg" ValidationGroup="Submit" Display="Dynamic"></asp:RegularExpressionValidator>
                <%--<asp:RequiredFieldValidator ID="reqStreetNo" runat="server"  ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredStreetNo  %>" ControlToValidate="txtStreetNo" CssClass="moe-full-width error-msg"  ValidationGroup="Submit" Display="Dynamic"  ></asp:RequiredFieldValidator>--%>
            </div>
        </div>
    </div>
    <div class="col-md-3 col-sm-6 col-xs-12 margin-top-10 margin-bottom-10">
        <div class="data-container table-display moe-full-width  moe-sm-95-width margin-sm-0">
            <div class="form-group">
                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                    <asp:Label ID="lblBuildingNo" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, BuildingNo %>"></asp:Label>
                    <span class="error-msg">*</span>
                </h6>

                <asp:TextBox ID="txtBuildingNo" runat="server" MaxLength="4" CssClass="moe-full-width input-height-42 border-box moe-input-padding"></asp:TextBox>
                <asp:RegularExpressionValidator ID="regBuildingNo" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RegNumbersOnly %>" ValidationExpression="^[0-9]{1,4}$" ControlToValidate="txtBuildingNo" CssClass="moe-full-width error-msg" ValidationGroup="Submit" Display="Dynamic"></asp:RegularExpressionValidator>
                <%--<asp:RequiredFieldValidator ID="reqBuildingNo" runat="server"  ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredBuildingNo  %>" ControlToValidate="txtBuildingNo"  CssClass="moe-full-width error-msg"  ValidationGroup="Submit" Display="Dynamic"  ></asp:RequiredFieldValidator>--%>
            </div>
        </div>
    </div>
    <div class="col-md-3 col-sm-6 col-xs-12 margin-top-10 margin-bottom-10">
        <div class="data-container table-display moe-full-width  moe-sm-95-width margin-sm-0">
            <div class="form-group">
                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                    <asp:Label ID="lblApartmentNo" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ApartmentNo %>"></asp:Label>
                </h6>

                <asp:TextBox ID="txtApartmentNo" MaxLength="4" runat="server" CssClass="moe-full-width input-height-42 border-box moe-input-padding"></asp:TextBox>
                <asp:RegularExpressionValidator ID="regApartmentNo" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RegNumbersOnly %>" ValidationExpression="^[0-9]{1,4}$" ControlToValidate="txtApartmentNo" CssClass="moe-full-width error-msg" ValidationGroup="Submit" Display="Dynamic"></asp:RegularExpressionValidator>
                <%--<asp:RequiredFieldValidator ID="reqApartmentNo" runat="server"  ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredApartmentNo  %>" ControlToValidate="txtApartmentNo"  CssClass="moe-full-width error-msg"  ValidationGroup="Submit" Display="Dynamic"  ></asp:RequiredFieldValidator>--%>
            </div>
        </div>
    </div>

    <div class="col-md-3 col-sm-6 col-xs-12 margin-top-10 margin-bottom-10">
        <div class="data-container table-display moe-full-width  moe-sm-95-width margin-sm-0">
            <div class="form-group">
                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                    <asp:Label ID="lblPostalNumber" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PostalNumber %>"></asp:Label>
                </h6>

                <asp:TextBox ID="txtPostalNumber" runat="server" MaxLength="8" CssClass="moe-full-width input-height-42 border-box moe-input-padding"></asp:TextBox>
                <asp:RegularExpressionValidator ID="regPostalNumber" runat="server" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RegNumbersOnly %>" ValidationExpression="^[0-9]{1,8}$" ControlToValidate="txtPostalNumber" CssClass="moe-full-width error-msg" ValidationGroup="Submit" Display="Dynamic"></asp:RegularExpressionValidator>
                <%--<asp:RequiredFieldValidator ID="reqApartmentNo" runat="server"  ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredApartmentNo  %>" ControlToValidate="txtApartmentNo"  CssClass="moe-full-width error-msg"  ValidationGroup="Submit" Display="Dynamic"  ></asp:RequiredFieldValidator>--%>
            </div>
        </div>
    </div>

    <div class="col-md-3 col-sm-6 col-xs-12 margin-top-10 margin-bottom-10">
        <div class="data-container table-display moe-full-width  moe-sm-95-width margin-sm-0 text-center">
            <img src="/_catalogs/masterpage/MOEHE/common/img/addressDetails.png" class="address-inst">
        </div>
    </div>

    <div class="col-md-12 col-sm-12 col-xs-12  margin-bottom-10">
        <div class="data-container table-display moe-full-width">
            <div class="form-group">
                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                    <asp:Label ID="lblDetailedAddress" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, DetailedAddress %>"></asp:Label>
                </h6>

                <asp:TextBox ID="txtDetailedAddress" runat="server" TextMode="MultiLine" CssClass="moe-full-width input-height-42 border-box moe-input-padding text-area"></asp:TextBox>
                <%--<asp:RequiredFieldValidator ID="reqDetailedAddres" runat="server"  ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredDetailedAddress  %>" ControlToValidate="txtDetailedAddress"  CssClass="moe-full-width error-msg"  ValidationGroup="Submit" Display="Dynamic"  ></asp:RequiredFieldValidator>--%>
            </div>
        </div>

        <asp:CustomValidator ID="custValidateTextBoxes" runat="server" CssClass="moe-full-width error-msg" ValidationGroup="Submit" Display="Dynamic" ErrorMessage="<%$Resources:ITWORX_MOEHEWF_UCE, RequiredApplicantFields %>" OnServerValidate="custValidateTextBoxes_ServerValidate" ValidateEmptyText="true"></asp:CustomValidator>

    </div>



    <div class="col-md-8 col-xs-12 col-sm-12 col-xs-12">
        <uc1:FileUpload runat="server" id="fileUploadNationalID" />
    </div>

    <div class="col-md-8 col-xs-12 col-sm-12 col-xs-12">
        <uc1:FileUpload runat="server" id="fileUploadPassport" />
    </div>

