<%@ Assembly Name="ITWORX.MOEHEWF.SCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7c6ec0a86ef11fff" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DisplayRequestDetails.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.SCE.DisplayRequestDetails" %>


<asp:HiddenField ID="OLevel_HF" runat="server" />
<asp:HiddenField ID="ALevel_HF" runat="server" />
<asp:HiddenField ID="IBList_HF" runat="server" />

<div class="school-collapse displayMode">

    <%--Applicant Details--%>
    <div class="row">
        <div class="accordion panel-group" id="accordion">
            <div class="panel panel-default">
                <div class="panel-heading active">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseFour">
                            <span>بيانات الطالب صاحب الشهادة</span> <em></em></a></h4>
                </div>
                <!-- /.panel-heading -->
                <div id="collapseFour" class="panel-collapse collapse in">
                    <div class="panel-body">

                        <div class="row">
                            <div class="col-md-4  col-xs-12">
                                <div class="form-group">
                                    <h6><asp:Label ID="lbl_QatarID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, QatarID %>"></asp:Label></h6>
                                    <h5><asp:Label ID="lblQatarIDVal" ClientIDMode="Static" runat="server"></asp:Label></h5>
                                </div>
                            </div>

                            <div class="col-md-4  col-xs-12" id="passportContainer">
                                <div class="form-group">
                                    <h6><asp:Label ID="lbl_PassPort" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Passport %>" Visible="false"></asp:Label></h6>
                                    <h5><asp:Label ID="lblPassPortVal" ClientIDMode="Static" runat="server" Visible="false"></asp:Label></h5>
                                </div>
                            </div>
                            <div class="col-md-4 col-xs-12">
                                <div class="form-group">
                                    <h6><asp:Label ID="lbl_birthDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, BirthDate %>"></asp:Label></h6>
                                    <h5><asp:Label ID="lblbirthDateVal" ClientIDMode="Static" runat="server" Visible="false"></asp:Label></h5>
                                </div>
                            </div>
                            <div class="col-md-4  col-xs-12">
                                <div class="form-group">
                                    <h6><asp:Label ID="lbl_Name" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentName %>"></asp:Label></h6>
                                    <h5><asp:Label ID="lblNameVal" ClientIDMode="Static" runat="server"></asp:Label></h5>
                                </div>
                            </div>
                        </div>

                        <div class="row margin-top-15">
                            <div class="col-md-4  col-xs-12">
                                <div class="form-group">
                                    <h6><asp:Label ID="lbl_Nationality" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentNationality %>"></asp:Label></h6>
                                    <h5><asp:Label ID="lblNationalityVal" runat="server"></asp:Label></h5>
                                </div>
                            </div>

                            <div class="col-md-4  col-xs-12">
                                <div class="form-group">
                                    <h6><asp:Label ID="lbl_Gender" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentGender %>"></asp:Label></h6>
                                    <h5><asp:Label ID="lblGenderVal" runat="server" ></asp:Label></h5>
                                </div>
                            </div>

                            <div class="col-md-4  col-xs-12">
                                <div class="form-group">
                                    <h6><asp:Label ID="lbl_NationalityCat" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentNationalityType %>"></asp:Label></h6>
                                    <h5><asp:Label ID="lblNatCatVal" runat="server"></asp:Label></h5>
                                </div>
                            </div>
                        </div>

                        <div class="row margin-top-15">
                            <div class="col-md-4  col-xs-12">
                                <div class="form-group">
                                    <h6> <asp:Label ID="lbl_PrintedName" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentNameCert %>"></asp:Label></h6>
                                    <h5><asp:Label ID="txt_PrintedName" runat="server"></asp:Label></h5>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--Certificate Details--%>
     <div class="row">
        <div class="accordion panel-group" id="accordion">
            <div class="panel panel-default">
                <div class="panel-heading active">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseThree">
                            <span>بيانات الشهادة الدراسية المطلوب معادلتها</span> <em></em></a></h4>
                </div>
                <!-- /.panel-heading -->
                <div id="collapseThree" class="panel-collapse collapse in">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-4  col-xs-12">
                                <div class="form-group">
                                    <h6><asp:Label ID="lblCertResource" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateResource %>"></asp:Label></h6>
                                    <h5><asp:Label runat="server" ID="certificateResource" /></h5>
                                </div>
                            </div>

                            <div class="col-md-4  col-xs-12">
                                <div class="form-group">
                                    <h6><asp:Label ID="lblSchoolType" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SchoolType %>"></asp:Label></h6>
                                    <h5><asp:Label runat="server" ID="schooleType"></asp:Label></h5>
                                </div>
                            </div>

                            <div class="col-md-4  col-xs-12">
                                <div class="form-group">
                                    <h6><asp:Label ID="lbl_PrevSchool" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, PreviousSchool %>"></asp:Label></h6>
                                    <h5><asp:Label ID="txt_PrevSchool" runat="server"></asp:Label></h5>
                                </div>
                            </div>
                        </div>

                        <div class="row margin-top-15">
                            <div class="col-md-4  col-xs-12">
                                <div class="form-group">
                                    <h6><asp:Label ID="lblSchoolingSystem" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SchoolingSystem %>"></asp:Label></h6>
                                    <h5><asp:Label runat="server" ID="lblSchoolSystemVal"></asp:Label></h5>
                                </div>
                            </div>

                            <div class="col-md-4  col-xs-12">
                                <div class="form-group">
                                    <h6><asp:Label ID="lbl_ScholasticLevel" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, LastSchoolYear %>"></asp:Label></h6>
                                    <h5><asp:Label ID="ddl_ScholasticLevel" runat="server"></asp:Label></h5>
                                </div>
                            </div>

                            <div class="col-md-4  col-xs-12">
                                <div class="form-group">
                                    <h6><asp:Label ID="lbl_LastAcademicYear" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, AcademicYear %>"></asp:Label></h6>
                                    <h5><asp:Label ID="ddl_LastAcademicYear" runat="server"></asp:Label></h5>
                                </div>
                            </div>
                        </div>

                        <div class="row margin-top-15">
                            <div class="col-md-4  col-xs-12">
                                <div class="form-group">
                                    <h6><asp:Label ID="lblEquiPurpose" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, EquivalencyPurpose %>"></asp:Label></h6>
                                    <h5><asp:Label runat="server" ID="lblEquiPurposeVal" /></h5>
                                </div>
                            </div>

                            <div class="col-md-4  col-xs-12">
                                <div class="form-group">
                                    <h6><asp:Label ID="lbl_GoingToClass" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, GoToClass %>"></asp:Label></h6>
                                    <h5><asp:Label ID="ddl_GoingToClass" runat="server"></asp:Label></h5>
                                </div>
                            </div>

                            <div class="col-md-4  col-xs-12">
                                <div class="form-group">
                                    <h6><asp:Label ID="lblCertificateType" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, CertificateType %>"></asp:Label></h6>
                                    <h5><asp:Label runat="server" ID="certificateType" /></h5>
                                </div>
                            </div>
                        </div>

                         <div class="IGCSE_Div" style="display: none">
        <div class="OLevel_Div margin-top-50">
           
           
           
            <table class="OLevel_table table school-table table-striped table-bordered">

                <thead>

                    <tr>

                        <th class="text-center">
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectNum %>"></asp:Literal>
                        </th>
                                                                        <th class="text-center">
                                                                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectCode %>"></asp:Literal></th>
                        <th class="text-center">
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectName %>"></asp:Literal></th>
                        <th class="text-center">
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAverage %>"></asp:Literal></th>
                        <th class="text-center">
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAction %>"></asp:Literal></th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
        <div class="ALevel_Div">
            
            <table class="ALevel_table table school-table table-striped table-bordered">
                <thead>
                    <tr>
                        <th class="text-center">
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectNum %>"></asp:Literal></th>
                        <th class="text-center">
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectCode %>"></asp:Literal></th>
                        <th class="text-center">
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectName %>"></asp:Literal></th>
                        <th class="text-center">
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAverage %>"></asp:Literal></th>
                        <th class="text-center">
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAction %>"></asp:Literal></th>
                    </tr>
                </thead>
                <tbody></tbody>
            </table>
        </div>
    </div>
    <div class="IB_Div" style="display: none">
        <div class="margin-top-50">
            <table class="IB_table table school-table table-striped table-bordered">
                <thead>
                    <tr>

                        <th class="text-center">
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectNum %>"></asp:Literal></th>
                        <th class="text-center">
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectName %>"></asp:Literal></th>
                        <th class="text-center">
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectLevel %>"></asp:Literal></th>
                        <th class="text-center">
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectPoints %>"></asp:Literal></th>
                        <th class="text-center">
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAction %>"></asp:Literal></th>

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

    <%--Attachements--%>
     <div class="row">
        <div class="accordion panel-group" id="accordion">
            <div class="panel panel-default">
                <div class="panel-heading active">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseSix">
                            <span>المرفقات</span> <em></em></a></h4>
                </div>
                <!-- /.panel-heading -->
                <div id="collapseSix" class="panel-collapse collapse in">
                    <div class="panel-body">
                    </div>




                </div>
            </div>
        </div>
    </div>

    <%--Approvals--%>
     <div class="row">
        <div class="accordion panel-group" id="accordion">
            <div class="panel panel-default">
                <div class="panel-heading active">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseSeven">
                            <span>الإقرار</span> <em></em></a></h4>
                </div>
                <!-- /.panel-heading -->
                <div id="collapseSeven" class="panel-collapse collapse in">
                    <div class="panel-body">
                    </div>




                </div>
            </div>
        </div>
    </div>
</div>
               
        




<div>
   
</div>
<script>
    $(function () {
    });
    LoadForm();

    function LoadForm() {
        //getStudentData();
        //viewHideCert();
        if ($("#<%=OLevel_HF.ClientID%>").val() != "") {
            $('#divIG').show();
            oLevel_IGCSEList = JSON.parse($("#<%=OLevel_HF.ClientID%>").val());
            if (oLevel_IGCSEList.length > 0) {
                for (var i = 0; i < oLevel_IGCSEList.length; i++) {
                    var tr;
                    tr = $('<tr id="' + (parseInt(i) + 1) + '" />');
                    tr.append("<td>" + (parseInt(i) + 1) + "</td>");
                    tr.append("<td>" + oLevel_IGCSEList[i].Code + "</td>");
                    tr.append("<td>" + oLevel_IGCSEList[i].Title + "</td>");
                    tr.append("<td>" + oLevel_IGCSEList[i].Avrage + "</td>");
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
                    tr.append("<td>" + (parseInt(i) + 1) + "</td>");
                    tr.append("<td>" + aLevel_IGCSEList[i].Code + "</td>");
                    tr.append("<td>" + aLevel_IGCSEList[i].Title + "</td>");
                    tr.append("<td>" + aLevel_IGCSEList[i].Avrage + "</td>");
                    //tr.append("<td><input type='button' class='DeleteALevel_btn' value='حذف' onclick='DeleteALevel(" + (parseInt(i) + 1) + ")'/></td>");
                    $('#tblAlevel').append(tr);
                }
            }
        }
        //debugger;
        if ($("#<%=IBList_HF.ClientID%>").val() != "") {
            $('#divIB').show();
            IBList = JSON.parse($("#<%=IBList_HF.ClientID%>").val());
            if (IBList.length > 0) {
                for (var i = 0; i < IBList.length; i++) {
                    var tr;
                    tr = $('<tr id="' + (parseInt(i) + 1) + '" />');
                    tr.append("<td>" + (parseInt(i) + 1) + "</td>");
                    tr.append("<td>" + IBList[i].Title + "</td>");
                    tr.append("<td>" + IBList[i].Level + "</td>");
                    tr.append("<td>" + IBList[i].Points + "</td>");
                    //tr.append("<td><input type='button' class='DeleteIBItem_btn' value='حذف' onclick='DeleteIBItem(" + (parseInt(i) + 1) + ")'/></td>");
                    $('#tblIB').append(tr);
                }
            }
        }
    }

    function viewHideCert() {
        //
        if (crtTypeVal == 1) {
            $(".IGCSE_Div").show();
            $(".IB_Div").hide();
        } else if (crtTypeVal == 2) {
            $(".IB_Div").show();
            $(".IGCSE_Div").hide();
        } else {
            $(".IB_Div").hide();
            $(".IGCSE_Div").hide();
        }
    }

    function getStudentData() {
        if ($("#txt_QatarID").val() != "") {
            var url = $("#MOIAddress_hdf").val() + $("#txt_QatarID").val();
            $.ajax({
                url: url
                ,type: 'GET'
                ,success: function (result) {
                    if (result != null) {
                        var isAr = window.location.href.toLowerCase().indexOf("ar") >= 0;

                        if (isAr)
                            $("#lbl_NameVal").text(result.ArabicName);
                        else
                            $("#lbl_NameVal").text(result.EnglishName);

                        $("#lbl_birthDateVal").text(result.BirthDate);

                        if (result.Gender.toLowerCase() == 'm') {
                            $("#lbl_GenderVal").text("ذكر");
                            $("#stdGender_hf").val('M');
                        } else {
                            $("#lbl_GenderVal").text("أنثى");
                            $("#stdGender_hf").val('F');
                        }
                        $("#stdNationality_hf").val(result.Nationality)
                        $.ajax({
                            url: "/_api/web/lists/getbytitle('Nationality')/items?$select=Title,TitleAr&$filter=ISOCode%20eq%20%27" + result.Nationality + "%27"
                            , type: 'GET'
                            , headers: {
                                "accept": "application / json;odata = verbose",
                            }
                            ,success: function (nat) {
                                var nationality = nat.d.results[0];
                                if (nationality != undefined) {
                                    if (isAr)
                                        $("#lbl_NationalityVal").text(nationality.TitleAr);
                                    else
                                        $("#lbl_NationalityVal").text(nationality.Title);
                                }
                            }
                        });
                        $("#lbl_QatarIDValidat").text("");
                    }
                    else {
                        $("#lbl_NameVal").val("");
                        $("#lbl_NationalityVal").val("");
                        $("#lbl_QatarIDValidat").text("برجاء إعادة أدخال الرقم الشخصى");
                        $("#txt_QatarID").val("");
                    }
                }
                ,error: function () {
                    $("#lbl_NameVal").val("");
                    $("#lbl_NationalityVal").val("");
                    $("#lbl_QatarIDValidat").text("برجاء إعادة أدخال الرقم الشخصى");
                    $("#txt_QatarID").val("");
                }
            });
        }
    }

</script>
