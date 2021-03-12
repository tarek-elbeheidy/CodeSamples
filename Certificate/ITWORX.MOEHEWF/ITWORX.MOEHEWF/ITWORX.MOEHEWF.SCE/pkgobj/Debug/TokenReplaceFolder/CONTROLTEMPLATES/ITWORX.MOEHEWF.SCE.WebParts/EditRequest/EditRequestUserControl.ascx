<%@ Assembly Name="ITWORX.MOEHEWF.SCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7c6ec0a86ef11fff" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditRequestUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.SCE.WebParts.EditRequest.EditRequestUserControl" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/DDLWithTXTWithNoPostback.ascx" TagPrefix="uc1" TagName="DDLWithTXTWithNoPostback" %>


<asp:HiddenField ID="OLevel_HF" runat="server" />
<asp:HiddenField ID="ALevel_HF" runat="server" />
<asp:HiddenField ID="IBList_HF" runat="server" />
<asp:HiddenField ID="MOIAddress_hdf" ClientIDMode="Static" runat="server" />
<asp:HiddenField ID="stdNationality_hf" ClientIDMode="Static" runat="server" />
<asp:HiddenField ID="stdGender_hf" ClientIDMode="Static" runat="server" />
<asp:HiddenField ID="hdnBirthDate" ClientIDMode="Static" runat="server" />
<asp:HiddenField ID="hdnStudentName" ClientIDMode="Static" runat="server" />


<div id="requestHeaderDiv" runat="server" visible="false">
    <div>
        <asp:Label ID="lbl_requestNum" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequestNumber %>"></asp:Label>
        <asp:TextBox ID="txt_requestNum" Enabled="false" runat="server"></asp:TextBox>
    </div>
    <div>
        <asp:Label ID="lbl_creationDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequestCreationDate %>"></asp:Label>
        <asp:TextBox ID="txt_creationDate" Enabled="false" runat="server"></asp:TextBox>
    </div>
    <div>
        <asp:Label ID="lbl_submitionDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, RequestSendDate %>"></asp:Label>
        <asp:TextBox ID="txt_submitionDate" Enabled="false" runat="server"></asp:TextBox>
    </div>
</div>

<div>
    <asp:Label ID="lblApplicantName" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, NameOfficialRecords %>"></asp:Label>
    <asp:TextBox ID="txtApplicantName" runat="server" ClientIDMode="Static" Enabled="false"></asp:TextBox>
</div>
<div>
    <asp:Label ID="lblMobileNumber" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, MobileNumber %>"></asp:Label>
    <asp:TextBox ID="txtMobileNumber" runat="server" ClientIDMode="Static"></asp:TextBox>
    <asp:RequiredFieldValidator ID="reqMobileNumber" runat="server" ControlToValidate="txtMobileNumber" ForeColor="Red" ValidationGroup="Submit" ErrorMessage="Mobile number is required"></asp:RequiredFieldValidator>
</div>
<div>
    <asp:Label ID="lblEmail" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Email %>"></asp:Label>
    <asp:TextBox ID="txtEmail" runat="server" ClientIDMode="Static"></asp:TextBox>
    <asp:RequiredFieldValidator ID="reqEmail" runat="server" ControlToValidate="txtEmail" ForeColor="Red" ValidationGroup="Submit" ErrorMessage="Email is required"></asp:RequiredFieldValidator>

</div>
<div>
    <asp:Label ID="lbl_QatarID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, QatarID %>"></asp:Label>
    <asp:TextBox ID="txt_QatarID" ClientIDMode="Static" runat="server"></asp:TextBox>
    <asp:Label ID="lbl_QatarIDValidat" runat="server" ClientIDMode="Static" ForeColor="Red"></asp:Label>
    <asp:CustomValidator ID="QatarIDValidator" runat="server" ClientValidationFunction="validateQatarID" ValidationGroup="Submit" ErrorMessage="برجاء إعادة أدخال الرقم الشخصى"></asp:CustomValidator>

    <asp:Label ID="lblTempQatarID" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, TempQatarID %>" Visible="false"></asp:Label>
    <asp:TextBox ID="txtTempQatarID" runat="server" ClientIDMode="Static" Visible="false"></asp:TextBox>
    <%--=================================================--%>
    <asp:Label ID="lbl_PassPort" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Passport %>" Visible="false"></asp:Label>
    <asp:TextBox ID="txt_PassPort" ClientIDMode="Static" runat="server" Visible="false"></asp:TextBox>
</div>
<div>
    <asp:Label ID="lbl_birthDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, BirthDate %>"></asp:Label>
    <asp:TextBox ID="txt_birthDate" ClientIDMode="Static" runat="server" Visible="false"></asp:TextBox>
    <asp:Label ID="lbl_birthDateVal" ClientIDMode="Static" runat="server"></asp:Label>
</div>
<div>
    <asp:Label ID="lbl_Name" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentName %>"></asp:Label>
    <asp:TextBox ID="txt_Name" ClientIDMode="Static" runat="server" Visible="false"></asp:TextBox>
    <asp:Label ID="lbl_NameVal" ClientIDMode="Static" runat="server"></asp:Label>
</div>
<div>
    <asp:Label ID="lbl_Nationality" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentNationality %>"></asp:Label>
    <asp:DropDownList ID="ddl_Nationality" runat="server" Visible="false"></asp:DropDownList>
    <asp:Label ID="lbl_NationalityVal" ClientIDMode="Static" runat="server"></asp:Label>
</div>
<div>
    <asp:Label ID="lbl_NationalityCat" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentNationalityType %>"></asp:Label>
    <asp:DropDownList ID="ddl_NatCat" runat="server"></asp:DropDownList>
    <asp:RequiredFieldValidator ID="NationalityCatValidator" runat="server" ControlToValidate="ddl_NatCat" InitialValue="-1" ForeColor="Red" ValidationGroup="Submit" ErrorMessage="فئة جنسية الطالب مطلوبة"></asp:RequiredFieldValidator>
</div>
<div>
    <asp:Label ID="lbl_Gender" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentGender %>"></asp:Label>
    <asp:DropDownList ID="ddl_Gender" runat="server" Visible="false"></asp:DropDownList>
    <asp:Label ID="lbl_GenderVal" ClientIDMode="Static" runat="server"></asp:Label>
</div>
<div>
    <asp:Label ID="lbl_PrintedName" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, StudentNameCert %>"></asp:Label>
    <asp:TextBox ID="txt_PrintedName" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="PrintedNameValidator" runat="server" ControlToValidate="txt_PrintedName" ForeColor="Red" ValidationGroup="Submit" ErrorMessage="اسم الطالب طبقا للشهادة مطلوبة"></asp:RequiredFieldValidator>
</div>

<div>
    <uc1:DDLWithTXTWithNoPostback runat="server" id="certificateResource" />
</div>
<div>
    <uc1:DDLWithTXTWithNoPostback runat="server" id="schooleType" />
</div>
<div>
    <asp:Label ID="lbl_PrevSchool" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, PreviousSchool %>"></asp:Label>
    <asp:TextBox ID="txt_PrevSchool" runat="server"></asp:TextBox>
</div>
<div>
    <asp:Label ID="lblSchoolingSystem" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SchoolingSystem %>"></asp:Label>
    <asp:DropDownList ID="ddlSchoolingSystem" runat="server"></asp:DropDownList>
</div>
<div>
    <asp:Label ID="lbl_ScholasticLevel" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, LastSchoolYear %>"></asp:Label>
    <asp:DropDownList ID="ddl_ScholasticLevel" runat="server"></asp:DropDownList>
</div>
<div>
    <asp:Label ID="lbl_LastAcademicYear" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, AcademicYear %>"></asp:Label>
    <asp:DropDownList ID="ddl_LastAcademicYear" runat="server"></asp:DropDownList>
</div>
<div>
    <asp:Label ID="lblEquiPurpose" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, EquivalencyPurpose %>"></asp:Label>
    <asp:DropDownList ID="ddlEquiPurpose" runat="server"></asp:DropDownList>
</div>
<div>
    <asp:Label ID="lbl_GoingToClass" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, GoToClass %>"></asp:Label>
    <asp:DropDownList ID="ddl_GoingToClass" runat="server"></asp:DropDownList>
</div>
<div>
    <uc1:DDLWithTXTWithNoPostback runat="server" id="certificateType" />
</div>
<div>
    <div class="IGCSE_Div" style="display: none">
        <asp:CustomValidator ID="AlevelValidator" runat="server" ClientValidationFunction="validateAlevel" OnServerValidate="serverValidateAlevel" ValidationGroup="Submit" ErrorMessage="لا يمكن إرسال الطلب الا إذا كان عدد المواد 2 أو أكثر"></asp:CustomValidator>
        <asp:CustomValidator ID="OlevelValidator" runat="server" ClientValidationFunction="validateOlevel" OnServerValidate="serverValidateOlevel" ValidationGroup="Submit" ErrorMessage="لا يمكن إرسال الطلب الا إذا كان عدد المواد 5 أو أكثر"></asp:CustomValidator>

        <div class="OLevel_Div">
            <div>
                <div>
                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectCode %>"></asp:Literal>
                </div>
                <div>
                    <input type="text" class="Ocode_txt" />
                </div>
            </div>
            <div>
                <div>
                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectName %>"></asp:Literal>
                </div>
                <div>
                    <input type="text" class="Otitle_txt" />
                </div>
            </div>
            <div>
                <div>
                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAverage %>"></asp:Literal>
                </div>
                <div>
                    <input type="text" class="OAvrage_txt" />
                </div>
            </div>

            <%--<input type="button" class="addOLevel_btn" value="إضافة"/>--%>
            <asp:Button runat="server" CssClass="addOLevel_btn" type="button" Text="<%$Resources:ITWORX_MOEHEWF_SCE, OLevelAdd %>" />
            <table class="OLevel_table">
                <thead>
                    <tr>
                        <%--<td>م</td>
                                    <td>رمز المادة	</td> 
                                    <td>اسم المادة	</td> 
                                    <td>المعدل</td> 
                                    <td>الاجراء</td>--%>
                        <td>
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectNum %>"></asp:Literal></td>
                        <td>
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectCode %>"></asp:Literal></td>
                        <td>
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectName %>"></asp:Literal></td>
                        <td>
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAverage %>"></asp:Literal></td>
                        <td>
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAction %>"></asp:Literal></td>
                    </tr>
                </thead>
            </table>
        </div>
        <div class="ALevel_Div">
            <div>
                <div>
                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectCode %>"></asp:Literal>
                </div>
                <div>
                    <input type="text" class="Acode_txt" />
                </div>
            </div>
            <div>
                <div>
                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectName %>"></asp:Literal>
                </div>
                <div>
                    <input type="text" class="Atitle_txt" />
                </div>
            </div>
            <div>
                <div>
                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAverage %>"></asp:Literal>
                </div>
                <div>
                    <input type="text" class="Aavrage_txt" />
                </div>
            </div>
            <%--<input type="button" class="addALevel_btn" value="إضافة"/>--%>
            <asp:Button runat="server" CssClass="addALevel_btn" type="button" Text="<%$Resources:ITWORX_MOEHEWF_SCE, OLevelAdd %>" />
            <table class="ALevel_table">
                <thead>
                    <tr>
                        <%--<td>م</td>
                                    <td>رمز المادة	</td>
                                    <td>اسم المادة	</td>
                                    <td>المعدل</td>
                                    <td>الاجراء</td>--%>
                        <td>
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectNum %>"></asp:Literal></td>
                        <td>
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectCode %>"></asp:Literal></td>
                        <td>
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectName %>"></asp:Literal></td>
                        <td>
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAverage %>"></asp:Literal></td>
                        <td>
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAction %>"></asp:Literal></td>

                    </tr>
                </thead>
            </table>
        </div>
    </div>
    <div class="IB_Div" style="display: none">
        <asp:CustomValidator ID="IBValidator" runat="server" ClientValidationFunction="validateIB" OnServerValidate="serverValidateIB" ValidationGroup="Submit" ErrorMessage="لا يمكن إرسال الطلب الا إذا كان عدد النقاط 24 أو أكثر"></asp:CustomValidator>

        <div>
            <div>
                <div>
                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectName %>"></asp:Literal>
                </div>
                <div>
                    <input type="text" class="IBTitle_txt" />
                </div>
            </div>
            <div>
                <div>
                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectLevel %>"></asp:Literal>
                </div>
                <div>
                    <asp:DropDownList ID="ddl_IBLevel" ClientIDMode="Static" runat="server"></asp:DropDownList>
                </div>
            </div>
            <div>
                <div>
                    <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectPoints %>"></asp:Literal>
                </div>
                <div>
                    <input type="number" class="IBPoints_txt" />
                </div>
            </div>
            <%--<input type="button" class="addIB_btn" value="إضافة"/>--%>
            <asp:Button runat="server" CssClass="addIB_btn" type="button" Text="<%$Resources:ITWORX_MOEHEWF_SCE, OLevelAdd %>" />

            <table class="IB_table">
                <thead>
                    <tr>
                        <%--<td>م</td>
                                    <td>اسم المادة	</td> 
                                    <td>المستوى</td> 
                                    <td>عدد النقاط</td> 
                                    <td>الاجراء</td>--%>
                        <td>
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectNum %>"></asp:Literal></td>
                        <td>
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectName %>"></asp:Literal></td>
                        <td>
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectLevel %>"></asp:Literal></td>
                        <td>
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectPoints %>"></asp:Literal></td>
                        <td>
                            <asp:Literal runat="server" Text="<%$Resources:ITWORX_MOEHEWF_SCE, SubjectAction %>"></asp:Literal></td>

                    </tr>
                </thead>
            </table>
        </div>
    </div>
</div>
<!-- attachment step -->
<div>
</div>

<asp:Button ID="btnFinishButton" runat="server" ValidationGroup="Submit" CommandName="MoveComplete" Text="<%$Resources:ITWORX_MOEHEWF_SCE, Finish %>" OnClick="btnFinishButton_Click" />


<script>
    var oLevel_IGCSEList = [];
    var aLevel_IGCSEList = [];
    var IBList = [];

    $(function () {


        LoadForm();

        //getApplicantNameFromService();

        $("#txt_QatarID").blur(function () {
            getStudentData();
        });

        $(".addIB_btn").click(function (e) {
            e.preventDefault();
            if ($(".IBTitle_txt").val() != "" && $("#ddl_IBLevel").val() != "-1" && $(".IBPoints_txt").val() != "") {
                var id = IBList.length > 0 ? parseInt(IBList[IBList.length - 1].ID) + 1 : 1;

                IBList.push({ ID: id, Title: $(".IBTitle_txt").val(), Level: $("#ddl_IBLevel").val(), LevelTitle: $("#ddl_IBLevel option:selected").text(), Points: $(".IBPoints_txt").val() })

                var tr;
                tr = $('<tr id="' + id + '" />');
                tr.append("<td>" + id + "</td>");
                tr.append("<td>" + $(".IBTitle_txt").val() + "</td>");
                tr.append("<td>" + $("#ddl_IBLevel option:selected").text() + "</td>");
                tr.append("<td>" + $(".IBPoints_txt").val() + "</td>");
                tr.append("<td><input type='button' class='DeleteIBItem_btn' value='حذف' onclick='DeleteIBItem(" + id + ")'/></td>");

                $('.IB_table').append(tr);

                $("#<%=IBList_HF.ClientID%>").val(JSON.stringify(IBList));
                validateDivs("IB_Div");
                if ($("#ddl_IBLevel").val() == "-1") {
                    $("#ddl_IBLevel").css('border-color', 'red');
                }
                else {
                    $("#ddl_IBLevel").css('border-color', '');
                }

                $(".IBTitle_txt").val("");
                $("#ddl_IBLevel").val("-1");
                $(".IBPoints_txt").val("");
            }
            else {
                validateDivs("IB_Div");
                if ($("#ddl_IBLevel").val() == "-1") {
                    $("#ddl_IBLevel").css('border-color', 'red');
                }
                else {
                    $("#ddl_IBLevel").css('border-color', '');
                }
            }

        })
        $(".addALevel_btn").click(function (e) {
            e.preventDefault();
            if ($(".Acode_txt").val() != "" && $(".Atitle_txt").val() != "" && $(".Aavrage_txt").val() != "") {

                var id = aLevel_IGCSEList.length > 0 ? parseInt(aLevel_IGCSEList[aLevel_IGCSEList.length - 1].ID) + 1 : 1;

                aLevel_IGCSEList.push({ ID: id, Code: $(".Acode_txt").val(), Title: $(".Atitle_txt").val(), Avrage: $(".Aavrage_txt").val() })

                var tr;
                tr = $('<tr id="2' + id + '" />');
                tr.append("<td>" + id + "</td>");
                tr.append("<td>" + $(".Acode_txt").val() + "</td>");
                tr.append("<td>" + $(".Atitle_txt").val() + "</td>");
                tr.append("<td>" + $(".Aavrage_txt").val() + "</td>");
                tr.append("<td><input type='button' class='DeleteALevel_btn' value='حذف' onclick='DeleteALevel(2" + id + ")'/></td>");

                $('.ALevel_table').append(tr);

                $("#<%=ALevel_HF.ClientID%>").val(JSON.stringify(aLevel_IGCSEList));
                validateDivs("ALevel_Div");

                $(".Acode_txt").val("");
                $(".Atitle_txt").val("");
                $(".Aavrage_txt").val("");
            } else {
                validateDivs("ALevel_Div");
            }
        })


        $(".addOLevel_btn").click(function (e) {
            e.preventDefault();
            if ($(".Ocode_txt").val() != "" && $(".Otitle_txt").val() != "" && $(".OAvrage_txt").val() != "") {

                var id = oLevel_IGCSEList.length > 0 ? parseInt(oLevel_IGCSEList[oLevel_IGCSEList.length - 1].ID) + 1 : 1;

                oLevel_IGCSEList.push({ ID: id, Code: $(".Ocode_txt").val(), Title: $(".Otitle_txt").val(), Avrage: $(".OAvrage_txt").val() })

                var tr;
                tr = $('<tr id="1' + id + '" />');
                tr.append("<td>" + id + "</td>");
                tr.append("<td>" + $(".Ocode_txt").val() + "</td>");
                tr.append("<td>" + $(".Otitle_txt").val() + "</td>");
                tr.append("<td>" + $(".OAvrage_txt").val() + "</td>");
                tr.append("<td><input type='button' class='DeleteOLevel_btn' value='حذف' onclick='DeleteOLevel(1" + id + ")'/></td>");

                $('.OLevel_table').append(tr);

                $("#<%=OLevel_HF.ClientID%>").val(JSON.stringify(oLevel_IGCSEList));
                validateDivs("OLevel_Div");

                $(".Ocode_txt").val("");
                $(".Otitle_txt").val("");
                $(".OAvrage_txt").val("");
            } else {
                validateDivs("OLevel_Div");
            }
        })

        $("#<%=CerTypeClientID%>").change(function () {
            viewHideCert();
        })
    })
    function validateDivs(control) {
        $("." + control + " input[type='text']").each(function () {
            if ($(this).val() == "")
                $(this).css('border-color', 'red');
            else
                $(this).css('border-color', '');

        });
    }
    function DeleteALevel(ID) {

        $('table.ALevel_table tr#' + ID).remove();
        for (var i = 0; i < aLevel_IGCSEList.length; i++) {
            if (aLevel_IGCSEList[i].ID == ID)
                aLevel_IGCSEList.splice($.inArray(aLevel_IGCSEList[i], aLevel_IGCSEList), 1);

        }
        $("#<%=ALevel_HF.ClientID%>").val(JSON.stringify(aLevel_IGCSEList));
    }


    function DeleteOLevel(ID) {

        $('table.OLevel_table tr#' + ID).remove();
        for (var i = 0; i < oLevel_IGCSEList.length; i++) {
            if (oLevel_IGCSEList[i].ID == ID)
                oLevel_IGCSEList.splice($.inArray(oLevel_IGCSEList[i], oLevel_IGCSEList), 1);

        }
        $("#<%=OLevel_HF.ClientID%>").val(JSON.stringify(oLevel_IGCSEList));
    }

    function DeleteIBItem(ID) {

        $('table.IB_table tr#' + ID).remove();
        for (var i = 0; i < IBList.length; i++) {
            if (IBList[i].ID == ID)
                IBList.splice($.inArray(IBList[i], IBList), 1);

        }
        $("#<%=IBList_HF.ClientID%>").val(JSON.stringify(IBList));
    }

    function validateAlevel(sender, arguments) {
        var crtTypeVal = $("#<%=CerTypeClientID%>").val();


        if (crtTypeVal == 1 && (parseInt(aLevel_IGCSEList.length) < 2)) {
            arguments.IsValid = false;

        }
    }
    function validateOlevel(sender, arguments) {
        var crtTypeVal = $("#<%=CerTypeClientID%>").val();


        if (crtTypeVal == 1 && (parseInt(oLevel_IGCSEList.length) < 5)) {
            arguments.IsValid = false;

        }
    }

    function validateIB(sender, arguments) {
        var crtTypeVal = $("#<%=CerTypeClientID%>").val();

        if (crtTypeVal == 2) {
            var points = 0;

            for (var i = 0; i < IBList.length; i++) {
                points = points + parseInt(IBList[i].Points);
            }
            if (points < 24)
                arguments.IsValid = false;

        }
    }

    function validateQatarID(sender, argument) {
        if ($("#lbl_birthDateVal").text() == "" || $("#lbl_NameVal").text() == "" || $("#lbl_NationalityVal").text() == "" || $("#lbl_GenderVal").text() == "") {
            argument.IsValid = false;
        }
    }


    function LoadForm() {
        if ($("#lbl_NameVal").text() == "") {
            getStudentData();
        }
        viewHideCert();
        if ($("#<%=OLevel_HF.ClientID%>").val() != "") {
            oLevel_IGCSEList = JSON.parse($("#<%=OLevel_HF.ClientID%>").val());

            if (oLevel_IGCSEList.length > 0) {
                for (var i = 0; i < oLevel_IGCSEList.length; i++) {
                    var tr;
                    tr = $('<tr id="' + oLevel_IGCSEList[i].ID + '" />');
                    tr.append("<td>" + (parseInt(i) + 1) + "</td>");
                    tr.append("<td>" + oLevel_IGCSEList[i].Code + "</td>");
                    tr.append("<td>" + oLevel_IGCSEList[i].Title + "</td>");
                    tr.append("<td>" + oLevel_IGCSEList[i].Avrage + "</td>");
                    tr.append("<td><input type='button' class='DeleteOLevel_btn' value='حذف' onclick='DeleteOLevel(" + oLevel_IGCSEList[i].ID + ")'/></td>");

                    $('.OLevel_table').append(tr);
                }
            }
        }
        if ($("#<%=ALevel_HF.ClientID%>").val() != "") {
            aLevel_IGCSEList = JSON.parse($("#<%=ALevel_HF.ClientID%>").val());

            if (aLevel_IGCSEList.length > 0) {
                for (var i = 0; i < aLevel_IGCSEList.length; i++) {
                    var tr;
                    tr = $('<tr id="' + aLevel_IGCSEList[i].ID + '" />');
                    tr.append("<td>" + (parseInt(i) + 1) + "</td>");
                    tr.append("<td>" + aLevel_IGCSEList[i].Code + "</td>");
                    tr.append("<td>" + aLevel_IGCSEList[i].Title + "</td>");
                    tr.append("<td>" + aLevel_IGCSEList[i].Avrage + "</td>");
                    tr.append("<td><input type='button' class='DeleteALevel_btn' value='حذف' onclick='DeleteALevel(" + aLevel_IGCSEList[i].ID + ")'/></td>");

                    $('.ALevel_table').append(tr);
                }
            }
        }

        if ($("#<%=IBList_HF.ClientID%>").val() != "") {
            IBList = JSON.parse($("#<%=IBList_HF.ClientID%>").val());

            if (IBList.length > 0) {
                for (var i = 0; i < IBList.length; i++) {
                    var tr;
                    tr = $('<tr id="' + IBList[i].ID + '" />');
                    tr.append("<td>" + (parseInt(i) + 1) + "</td>");
                    tr.append("<td>" + IBList[i].Title + "</td>");
                    tr.append("<td>" + IBList[i].Level + "</td>");
                    tr.append("<td>" + IBList[i].Points + "</td>");
                    tr.append("<td><input type='button' class='DeleteIBItem_btn' value='حذف' onclick='DeleteIBItem(" + IBList[i].ID + ")'/></td>");

                    $('.IB_table').append(tr);
                }
            }
        }

    }
    function viewHideCert() {
        var crtTypeVal = $("#<%=CerTypeClientID%>").val();
        if (crtTypeVal == 1) {
            $(".IGCSE_Div").show();
            $(".IB_Div").hide();
        }
        else if (crtTypeVal == 2) {
            $(".IB_Div").show();
            $(".IGCSE_Div").hide();
        }
        else {
            $(".IB_Div").hide();
            $(".IGCSE_Div").hide();
        }
    }

    function getStudentData() {
        if ($("#txt_QatarID").val() != "") {
            var url = $("#MOIAddress_hdf").val() + $("#txt_QatarID").val();

            $.ajax({
                url: url
                , type: 'GET'
                , success: function (result) {
                    //debugger;
                    if (result != null) {
                        var isAr = window.location.href.toLowerCase().indexOf("ar") >= 0;

                        if (isAr) {
                            $("#lbl_NameVal").text(result.ArabicName);
                            $("#hdnStudentName").val(result.ArabicName);
                        } else {
                            $("#lbl_NameVal").text(result.EnglishName);
                            $("#hdnStudentName").val(result.EnglishName);
                        }

                        $("#lbl_birthDateVal").text(result.BirthDate);
                        $("#hdnBirthDate").val(result.BirthDate);
                        if (result.Gender.toLowerCase() == 'm') {
                            $("#lbl_GenderVal").text("ذكر");
                            $("#stdGender_hf").val('M');
                        }
                        else {
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
                            , success: function (nat) {
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
                , error: function () {
                    console.log("MOI Error");
                    $("#lbl_NameVal").val("");
                    $("#lbl_NationalityVal").val("");
                    $("#lbl_QatarIDValidat").text("برجاء إعادة أدخال الرقم الشخصى");
                    $("#txt_QatarID").val("");
                }
            });
        }
    }

    function getApplicantNameFromService() {
        $.ajax({
            url: "/_api/web/currentuser",
            type: 'GET',
            headers: {
                "accept": "application / json;odata = verbose",
            },
            success: function (currentUser) {
                //console.log(currentUser.d.Title);
                $.ajax({
                    url: "/_api/web/lists/getbytitle('Applicants')/items?$select=ApplicantName,PersonalID,MobileNumber,ApplicantEmail,ArabicName,EnglishName&$filter=ApplicantName%20eq%20%27" + currentUser.d.Title + "%27",
                    type: 'GET',
                    headers: {
                        "accept": "application / json;odata = verbose",
                    },
                    success: function (applicantData) {
                        //var nationality = nat.d.results[0];
                        //if (nationality != undefined) {
                        //    if (isAr)
                        //        $("#lbl_NationalityVal").text(nationality.TitleAr);
                        //    else
                        //        $("#lbl_NationalityVal").text(nationality.Title);
                        //}
                        //console.log(applicantData.d.results[0].PersonalID);
                        var isAr = window.location.href.toLowerCase().indexOf("ar") >= 0;
                        if (isAr)
                            $("#txtApplicantName").val(applicantData.d.results[0].ArabicName);
                        else
                            $("#txtApplicantName").val(applicantData.d.results[0].EnglishName);

                        $('#txtMobileNumber').val(applicantData.d.results[0].MobileNumber);
                        $('#txtEmail').val(applicantData.d.results[0].ApplicantEmail);
                        var url = $("#MOIAddress_hdf").val() + applicantData.d.results[0].PersonalID;
                        //$.ajax({
                        //    url: url,
                        //    type: 'GET',
                        //    success: function (result) {
                        //        //console.log(result);
                        //        if (result != null) {
                        //            var isAr = window.location.href.toLowerCase().indexOf("ar") >= 0;
                        //            if (isAr)
                        //                $("#txtApplicantName").val(result.ArabicName);
                        //            else
                        //                $("#txtApplicantName").val(result.EnglishName);

                        //            $('#txtMobileNumber').val(applicantData.d.results[0].MobileNumber);
                        //            $('#txtEmail').val(applicantData.d.results[0].ApplicantEmail);
                        //            //$("#lbl_birthDateVal").text(result.BirthDate);

                        //            //if (result.Gender.toLowerCase() == 'm') {
                        //            //    $("#lbl_GenderVal").text("ذكر");
                        //            //    $("#stdGender_hf").val('M');
                        //            //}
                        //            //else {
                        //            //    $("#lbl_GenderVal").text("أنثى");
                        //            //    $("#stdGender_hf").val('F');
                        //            //}
                        //            //$("#stdNationality_hf").val(result.Nationality)




                        //            //$("#lbl_QatarIDValidat").text("");
                        //        }
                        //        //else {
                        //        //    $("#lbl_NameVal").val("");
                        //        //    $("#lbl_NationalityVal").val("");
                        //        //    $("#lbl_QatarIDValidat").text("برجاء إعادة أدخال الرقم الشخصى");
                        //        //    $("#txt_QatarID").val("");
                        //        //}
                        //    },
                        //    error: function (error) {
                        //        //$("#lbl_NameVal").val("");
                        //        //$("#lbl_NationalityVal").val("");
                        //        //$("#lbl_QatarIDValidat").text("برجاء إعادة أدخال الرقم الشخصى");
                        //        //$("#txt_QatarID").val("");
                        //        console.log(error);
                        //    }
                        //});
                    },
                    error: function (error) {
                        console.log(error);
                    }
                });
            }
        });

        //if ($("#txtApplicantName").val() != "") {



        //}
    }
</script>
