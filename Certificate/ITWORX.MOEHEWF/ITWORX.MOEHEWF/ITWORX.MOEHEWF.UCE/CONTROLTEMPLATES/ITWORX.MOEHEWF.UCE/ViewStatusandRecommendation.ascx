<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewStatusandRecommendation.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.ViewStatusandRecommendation" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/FileUpload.ascx" TagPrefix="MOEHE" TagName="FileUpload" %>

<asp:HiddenField ID="hdn_ID" runat="server" />
<div class="row unheighlighted-section margin-bottom-50 flex-display flex-wrap display-mode margin-2">
    <div class="col-md-12 col-sm-12 col-xs-12  margin-bottom-15">
        <div class="data-container">
            <h6 class="font-size-16 margin-bottom-15">

                <asp:Label ID="lbl_Opinion" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EmployeeOpinion %>" Visible="false"></asp:Label>
            </h6>
            <h5 class="font-size-20">
                <asp:Label ID="lbl_EmpOpinion" runat="server"></asp:Label>
            </h5>
        </div>
    </div>
 
    <div class="col-md-12 col-sm-12 col-xs-12  margin-bottom-15">
        <div class="data-container">
            <h6 class="font-size-16 margin-bottom-15">
                <asp:Label ID="lbl_EmpRecommend" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, EmpRecommendation %>" Visible="false"></asp:Label>
            </h6>
            <h5 class="font-size-20">
                <asp:Label ID="lbl_EmpRecommendation" runat="server"></asp:Label>
            </h5>
        </div>
    </div>
    <div class="col-md-12 no-padding">
        <MOEHE:FileUpload runat="server" id="ViewStatusRecommendAttachements" />
    </div> 
     <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12  no-padding ">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="Label1" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NumberOfHoursGained %>"></asp:Label>
                </h6>
                <h5 class="font-size-22">
                    <asp:Label ID="lbltxtGainedHours" Text="N/A" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12  no-padding ">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="Label3" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NumberOfOnlineHours %>"></asp:Label>
                </h6>
                <h5 class="font-size-22">
                    <asp:Label ID="lbltxtOnlineHours" Text="N/A" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12  no-padding ">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="Label5" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PercentageOfOnlineHours %>"></asp:Label>
                </h6>
                <h5 class="font-size-22">
                    <asp:Label ID="lbltxtOnlinePercentage" Text="N/A" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
         <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12  no-padding ">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lblHonoraryDegree" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, HonoraryDegree %>"></asp:Label>
                </h6>
                <h5 class="font-size-22">
                    <asp:Label ID="lblrblHonoraryDegree" Text="N/A" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
         <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12  no-padding ">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="Label6" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, HeHavePA %>"></asp:Label>
                </h6>
                <h5 class="font-size-22">
                    <asp:Label ID="valckbHavePA" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
         <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12  no-padding ">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="Label8" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, UniversityName %>"></asp:Label>
                </h6>
                <h5 class="font-size-22">
                    <asp:Label ID="lblrblUniversity" Text="N/A" runat="server"></asp:Label>
                </h5>
            </div>
        </div>
    
    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12  no-padding ">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                    <asp:Label ID="lblckbHaveException" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, HaveException %>"  ></asp:Label>
                </h6>
                <h5 class="font-size-22">
                  
                    <asp:Label ID="lbl_HaveException" Text="N/A"  runat="server"></asp:Label>
                </h5>
            </div>
        </div>
    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12  no-padding ">
            <div class="data-container">
                <h6 class="font-size-16 margin-bottom-15">
                        <asp:Label ID="lblExceptionFrom" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ExceptionFrom %>"></asp:Label>
                </h6>
                <h5 class="font-size-22">
                   <asp:Label ID="lblExceptionFromValue"  runat="server"  Text="N/A" ></asp:Label>
                </h5>
            </div>
        </div>


       <div class="col-md-12 col-sm-12 col-xs-12  margin-bottom-15">
        <div class="data-container">
            <h6 class="font-size-16 margin-bottom-15">

                <asp:Label ID="lbl_Decisiontxt" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, DecisiontxtForPrinting %>" Visible="false"></asp:Label>
            </h6>
            <div class="form recommend-form heighlighted-section">
                <h1>
                    <asp:Label ID="lbl_SirValue" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lbl_OccupationName" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lbl_RespectedValue" runat="server" Text=""></asp:Label>
                </h1>
                <h1>
                    <asp:Label ID="lbl_EntityNeedsEquivalencyView" runat="server" Text=""></asp:Label>
                </h1>
                <br />
                <h1>
                    <label><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","WelcomText") %></label>
                </h1>
                <br />
                <h1>
                    <asp:Label ID="lbl_decicionBobyView" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, DecisionBody %>"></asp:Label>
                </h1>
                <h1>
                    <asp:Label ID="lbl_DecisiontxtVal" runat="server"></asp:Label>
                </h1>
                <h3>
                    <label><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","AppreciationText") %></label>
                </h3>
                <h4>
                    <asp:Label ID="lbl_headManagerName" runat="server" ></asp:Label> 
                    
                </h4>
                <h4>
                    <label><%=HttpContext.GetGlobalResourceObject("ITWORX_MOEHEWF_UCE","EquivalencManager") %></label>
                </h4>
            </div>
        </div>
    </div>
</div>

<div class="row no-padding">
    <h4 class="font-size-18 font-weight-600 text-center">
        <asp:Label ID="lbl_NoResults" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NoResults %>" Visible="false"></asp:Label></h4>
</div>