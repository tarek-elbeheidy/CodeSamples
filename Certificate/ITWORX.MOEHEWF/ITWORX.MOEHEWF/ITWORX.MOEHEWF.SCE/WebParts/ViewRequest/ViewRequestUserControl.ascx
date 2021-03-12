<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/ClientSideFileUpload.ascx" TagPrefix="uc1" TagName="ClientSideFileUpload" %>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewRequestUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.WebParts.ViewRequest.ViewRequestUserControl" %>

<asp:HiddenField ID="OLevel_HF" runat="server" />
<asp:HiddenField ID="ALevel_HF" runat="server" />
<asp:HiddenField ID="IBList_HF" runat="server" />



<div class="displayMode tab-pane moe-view-request " role="tabpanel">
    <div class="row">
        <div class="col-xs-12">
            <h2 class="tab-title">
                <asp:Label ID="Label2" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequestDetails %>"></asp:Label>
            </h2>
        </div>
    </div>



    <!--StudentData-->
    <div class="row">
        <div class="accordion panel-group" id="accordion">
            <div class="panel panel-default">
                <div class="panel-heading active">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne1">
                            <asp:Label ID="Label1" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentData %>"></asp:Label><em></em>
                        </a>
                    </h4>
                </div>
                <%-- /.panel-heading --%>
                <div id="collapseOne1" class="panel-collapse collapse in">
                    <div class="panel-body">
                        <div class="row">

                            <div class="col-md-6  col-xs-12 ">
                                <div class="form-group">
                                    <h6>
                                        <asp:Label ID="Label10" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, QatarID %>"></asp:Label></h6>
                                    <h5>
                                        <asp:Label ID="lblQatarIDVal" ClientIDMode="Static" runat="server"></asp:Label></h5>
                                </div>
                            </div>

                              <div class="row" id="divPassportContainer" runat="server" style="display: none">

                                <div class="col-md-6  col-xs-12">
                                    <div class="form-group">
                                        <h6>
                                            <asp:Label ID="lblPassPortDisplay" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Passport %>" Visible="false"></asp:Label>
                                        </h6>
                                        <h5>
                                            <asp:Label ID="lblPassPortVal" ClientIDMode="Static" runat="server" Visible="false"></asp:Label></h5>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-6  col-xs-12">
                                <div class="form-group">
                                    <h6>
                                        <asp:Label ID="Label3" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, BirthDate %>"></asp:Label></h6>
                                    <h5>
                                        <asp:Label ID="lblbirthDateVal" ClientIDMode="Static" runat="server"></asp:Label></h5>
                                </div>
                            </div>
                          
                            <div class="col-md-6  col-xs-12 margin-top-15">
                                <div class="form-group">
                                    <h6>
                                        <asp:Label ID="Label4" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentName %>"></asp:Label>
                                    </h6>
                                    <h5>
                                        <asp:Label ID="lblNameVal" ClientIDMode="Static" runat="server"></asp:Label></h5>
                                </div>
                            </div>
                            <%--  <div id="" class=" qatariContainer">
                                <div class="clearfix"></div>
                            </div>--%>
                            <div class="col-md-6  col-xs-12 margin-top-15">
                                <div class="form-group">
                                    <h6>
                                        <asp:Label ID="Label5" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentNationality %>"></asp:Label>
                                    </h6>
                                    <h5>
                                        <asp:Label ID="lblNationalityVal" runat="server"></asp:Label></h5>
                                </div>
                            </div>

                            <div class="col-md-6  col-xs-12 margin-top-15">
                                <div class="form-group">
                                    <h6>
                                        <asp:Label ID="Label7" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentGender %>"></asp:Label>
                                    </h6>
                                    <h5>
                                        <asp:Label ID="lblGenderVal" runat="server"></asp:Label></h5>
                                </div>
                            </div>

                            <div class="col-md-6  col-xs-12 margin-top-15">
                                <div class="form-group">
                                    <h6>
                                        <asp:Label ID="Label6" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentNationalityType %>"></asp:Label>
                                    </h6>
                                    <h5>
                                        <asp:Label ID="lblNatCatVal" runat="server"></asp:Label></h5>
                                </div>
                            </div>
                            <%-- <div id="" class=" qatariContainer">
                                <div class="clearfix"></div>
                            </div>--%>
                            <div class="col-md-6  col-xs-12 margin-top-15">
                                <div class="form-group">
                                    <h6>
                                        <asp:Label ID="Label8" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentNameCert %>"></asp:Label>
                                    </h6>
                                    <h5>
                                        <asp:Label ID="lblPrintedNameDisplay" runat="server"></asp:Label></h5>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!--CertificateDetails-->
    <div class="row">
        <div class="accordion panel-group" id="accordion">
            <div class="panel panel-default">
                <div class="panel-heading active">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne2">
                            <asp:Label ID="Label11" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateDetails %>"></asp:Label><em></em>
                        </a>
                    </h4>
                </div>
                <%-- /.panel-heading --%>
                <div id="collapseOne2" class="panel-collapse collapse in">
                    <div class="panel-body">

                        <div class="row">
                            <div class="col-md-6  col-xs-12">
                                <div class="form-group">
                                    <h6>
                                        <asp:Label ID="lblCertResource" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateResource %>"></asp:Label></h6>
                                    <h5>
                                        <asp:Label ID="lblcertificateResource" runat="server" /></h5>
                                </div>
                            </div>
                            <div class="col-md-6  col-xs-12">
                                <div class="form-group">
                                    <h6>
                                        <asp:Label ID="lblSchoolType" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SchoolType %>"></asp:Label></h6>
                                    <h5>
                                        <asp:Label ID="lblSchoolTypeVal" runat="server"></asp:Label></h5>
                                </div>
                            </div>

                        </div>

                        <div class="row margin-top-15">
                            <div class="col-md-6  col-xs-12">
                                <div class="form-group">
                                    <h6>
                                        <asp:Label ID="Label12" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, PreviousSchool %>"></asp:Label></h6>
                                    <h5>
                                        <asp:Label ID="lblPrevSchool" runat="server"></asp:Label></h5>
                                </div>
                            </div>
                            <div class="col-md-6  col-xs-12">
                                <div class="form-group">
                                    <h6>
                                        <asp:Label ID="Label14" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SchoolingSystem %>"></asp:Label></h6>
                                    <h5>
                                        <asp:Label ID="lblSchoolSystemVal" runat="server"></asp:Label></h5>
                                </div>
                            </div>

                        </div>
                        <div class="row margin-top-15">
                            <div class="col-md-6  col-xs-12">
                                <div class="form-group">
                                    <h6>
                                        <asp:Label ID="Label15" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, LastSchoolYear %>"></asp:Label></h6>
                                    <h5>
                                        <asp:Label ID="lblScholasticLevel" runat="server"></asp:Label></h5>
                                </div>
                            </div>
                            <div class="col-md-6  col-xs-12">
                                <div class="form-group">
                                    <h6>
                                        <asp:Label ID="Label17" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, AcademicYear %>"></asp:Label></h6>
                                    <h5>
                                        <asp:Label ID="lblLastAcademicYear" runat="server"></asp:Label></h5>
                                </div>
                            </div>
                        </div>

                        <div class="row margin-top-15">
                            <div class="col-md-6  col-xs-12">
                                <div class="form-group">
                                    <h6>
                                        <asp:Label ID="Label19" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, EquivalencyPurpose %>"></asp:Label></h6>
                                    <h5>
                                        <asp:Label ID="lblEquiPurposeVal" runat="server" /></h5>
                                </div>
                            </div>
                            <div class="col-md-6  col-xs-12">
                                <div class="form-group">
                                    <h6>
                                        <asp:Label ID="Label20" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, GoToClass %>"></asp:Label></h6>
                                    <h5>
                                        <asp:Label ID="lblGoingToClass" runat="server"></asp:Label></h5>
                                </div>
                            </div>

                        </div>
                        <div class="row margin-top-15">
                            <div class="col-md-6  col-xs-12">
                                <div class="form-group">
                                    <h6>
                                        <asp:Label ID="lblCertificateType" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateType %>"></asp:Label></h6>
                                    <h5>
                                        <asp:Label ID="lblCertificateTypeVal" runat="server" /></h5>
                                </div>
                            </div>
                        </div>


                        <div class="row">

                            <div id="divIG" class="IGCSE-table-view" style="display: none">
                                <div class="OLevel_Div">
                                    <h4 class="subject-header IGCSE-subject-header">
                                                <asp:Label ID="Label23" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, OLevelMessageReview %>"></asp:Label>
                                                    </h4>
                                    <table id="tblOlevel" class="table moe-table table-striped table-bordered">
                                        <thead>
                                            <tr>
                                                <th class="text-center">
                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectNum %>"></asp:Literal>
                                                </th>
                                                <th class="text-center">
                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectCode %>"></asp:Literal>
                                                </th>
                                                <th class="text-center">
                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectName %>"></asp:Literal>
                                                </th>
                                                <th class="text-center">
                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAverage %>"></asp:Literal>
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                </div>
                                <div class="ALevel_Div margin-top-50">
                                    <h4 class="subject-header IGCSE-subject-header">
                                               <asp:Label ID="Label24" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, ALevelMessageReview %>"></asp:Label>
                                                    </h4>
                                    <table id="tblAlevel" class="table moe-table table-striped table-bordered">
                                        <thead>
                                            <tr>
                                                <th class="text-center">
                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectNum %>"></asp:Literal>
                                                </th>
                                                <th class="text-center">
                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectCode %>"></asp:Literal>
                                                </th>
                                                <th class="text-center">
                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectName %>"></asp:Literal>
                                                </th>
                                                <th class="text-center">
                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAverage %>"></asp:Literal>
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                </div>
                            </div>
                            <div id="divIB" class="IB-table-view" style="display: none">
                                <div>
                                      <h4 class="subject-header IGCSE-subject-header">
                                               <asp:Label ID="Label25" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, IBMessageReview %>"></asp:Label>
                                                    </h4>
                                    <table id="tblIB" class="table moe-table table-striped table-bordered">
                                        <thead>
                                            <tr>
                                                <th class="text-center">
                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectNum %>"></asp:Literal>
                                                </th>
                                                <th class="text-center">
                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectCode %>"></asp:Literal>
                                                </th>
                                                <th class="text-center">
                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectName %>"></asp:Literal>
                                                </th>
                                                <th class="text-center">
                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectPoints %>"></asp:Literal>
                                                </th>
                                                <th class="text-center">
                                                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectLevel %>"></asp:Literal>
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!--Passed Years-->
        <div class="row">
            <div class="accordion panel-group" id="accordion">
                <div class="panel panel-default">
                    <div class="panel-heading active">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseFive1">
                                <asp:Label ID="Label18" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, AcademicData %>"></asp:Label><em></em>
                            </a>
                        </h4>
                    </div>

                    <div id="collapseFive1" class="panel-collapse collapse in">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <h6>
                                            <asp:Label ID="Label21" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, PassedYearsTitle %>"></asp:Label>
                                        </h6>
                                        <h5>
                                            <asp:Label ID="lblTotalPassedYears" runat="server"></asp:Label></h5>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>

                </div>
            </div>
        </div>

        <!-- attachments -->
        <div class="row">
            <div class="accordion panel-group" id="accordion">
                <div class="panel panel-default">
                    <div class="panel-heading active">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo11">
                                <asp:Label ID="Label22" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Attachments %>"></asp:Label><em></em>
                            </a>
                        </h4>
                    </div>
                    <%-- /.panel-heading --%>
                    <div id="collapseTwo11" class="panel-collapse collapse in">
                        <div class="panel-body">
                            <uc1:ClientSideFileUpload runat="server" id="FileUploadDisplay" />
                        </div>
                    </div>
                </div>
            </div>
        </div>


    </div>
</div>
<script type="text/javascript">
    LoadSubjects();
    function LoadSubjects() {
        if ($("#<%=OLevel_HF.ClientID%>").val() != "") {
            $('#divIG').show();
            oLevel_IGCSEList = JSON.parse($("#<%=OLevel_HF.ClientID%>").val());
            if (oLevel_IGCSEList.length > 0) {
                for (var i = 0; i < oLevel_IGCSEList.length; i++) {
                    var tr;
                    tr = $('<tr id="' + (parseInt(i) + 1) + '" />');
                    tr.append("<td class='text-center'>" + (parseInt(i) + 1) + "</td>");
                    tr.append("<td class='text-center'>" + oLevel_IGCSEList[i].Code + "</td>");
                    tr.append("<td class='text-center'>" + oLevel_IGCSEList[i].Title + "</td>");
                    tr.append("<td class='text-center'>" + oLevel_IGCSEList[i].Avrage + "</td>");
                    //tr.append("<td><input type='button' class='DeleteOLevel_btn' value='حذف' onclick='DeleteOLevel(" + (parseInt(i) + 1) + ")'/></td>");
                    $('#tblOlevel').append(tr);
                }
            }
        }
        if ($("#<%=ALevel_HF.ClientID%>").val() != "") {
            $('#divIG').show();
            aLevel_IGCSEList = JSON.parse($("#<%=ALevel_HF.ClientID%>").val());
            if (aLevel_IGCSEList.length > 0) {
                for (var i = 0; i < aLevel_IGCSEList.length; i++) {
                    var tr;
                    tr = $('<tr id="' + (parseInt(i) + 1) + '" />');
                    tr.append("<td class='text-center'>" + (parseInt(i) + 1) + "</td>");
                    tr.append("<td class='text-center'>" + aLevel_IGCSEList[i].Code + "</td>");
                    tr.append("<td class='text-center'>" + aLevel_IGCSEList[i].Title + "</td>");
                    tr.append("<td class='text-center'>" + aLevel_IGCSEList[i].Avrage + "</td>");
                    //tr.append("<td><input type='button' class='DeleteALevel_btn' value='حذف' onclick='DeleteALevel(" + (parseInt(i) + 1) + ")'/></td>");
                    $('#tblAlevel').append(tr);
                }
            }
        }
        if ($("#<%=IBList_HF.ClientID%>").val() != "") {
            $('#divIB').show();
            var sumPoints = 0;
            IBList = JSON.parse($("#<%=IBList_HF.ClientID%>").val());
             if (IBList.length > 0) {
                 for (var i = 0; i < IBList.length; i++) {
                     var tr;
                     tr = $('<tr id="' + (parseInt(i) + 1) + '" />');
                     tr.append("<td class='text-center'>" + (parseInt(i) + 1) + "</td>");
                     tr.append("<td class='text-center'>" + IBList[i].Code + "</td>");
                     tr.append("<td class='text-center'>" + IBList[i].Title + "</td>");
                     tr.append("<td class='text-center'>" + IBList[i].Points + "</td>");
                     tr.append("<td class='text-center'>" + IBList[i].Level + "</td>");
                     //tr.append("<td><input type='button' class='DeleteIBItem_btn' value='حذف' onclick='DeleteIBItem(" + (parseInt(i) + 1) + ")'/></td>");
                     $('#tblIB').append(tr);
                     sumPoints += parseInt(IBList[i].Points);
                 }
             }
             $('#tblIB').append(
                 '<tfoot><tr>' +
                 '<td id="footerIbTxt" class="text-center" colspan="3">' + "<%=Resources.ITWORX_MOEHEWF_SCE.Total %>" + '</td>' +
                '<td id="footerIb" class="text-center">' + sumPoints + '</td>' +
                '</tfoot></tr>'
            );
        }
    }

</script>
