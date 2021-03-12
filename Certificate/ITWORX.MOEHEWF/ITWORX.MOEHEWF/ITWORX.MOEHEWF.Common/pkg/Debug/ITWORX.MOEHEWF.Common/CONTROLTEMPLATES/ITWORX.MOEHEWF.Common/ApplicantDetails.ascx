<%@ Assembly Name="ITWORX.MOEHEWF.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7b2931724f1d7d1c" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApplicantDetails.ascx.cs" Inherits="ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.ApplicantDetails" %>
<div>
    <asp:Label ID="lbl_QatarID"  runat="server" Text="الرقم الشخصي القطري "></asp:Label> 
    <asp:TextBox ID="txt_QatarID" ClientIDMode="Static" runat="server" Visible="false" ></asp:TextBox>
    <asp:Label ID="lbl_QatarIDVal" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lbl_QatarIDValidat" runat="server" ClientIDMode="Static" ForeColor="Red" ></asp:Label> 
</div>
<div id="div_PassPort" runat="server" visible="false">
    <asp:Label ID="lbl_PassPort" runat="server" Text="رقم جواز السفر"></asp:Label>
    <asp:TextBox ID="txt_PassPort" ClientIDMode="Static" runat="server"></asp:TextBox> 
</div>
<div>
    <asp:Label ID="lbl_Name" runat="server" Text="الاســـــــــــــــم"></asp:Label>
    <asp:TextBox ID="txt_Name" ClientIDMode="Static" runat="server" Visible="false"></asp:TextBox>
    <asp:Label ID="lbl_NameVal" runat="server" Visible="false"></asp:Label>
</div>
<div>
    <asp:Label ID="lbl_Nationality" runat="server" Text="الجنســيـة"></asp:Label>
    <asp:DropDownList ID="ddl_Nationality" runat="server" Visible="false"></asp:DropDownList>  
    <asp:Label ID="lbl_NationalityVal" runat="server" Visible="false"></asp:Label>
</div>
<div>
    <asp:Label ID="lbl_NationalityCat" runat="server" Text="فئة الجنسية"></asp:Label>
    <asp:DropDownList ID="ddl_NatCat" runat="server" Visible="false"></asp:DropDownList>
    <asp:Label ID="lbl_NationalityCatVal" runat="server" Visible="false"></asp:Label>
</div>
<div>
    <asp:Label ID="lbl_Mobile" runat="server" Text="رقم الهاتف المتحرك"></asp:Label>
    <asp:TextBox ID="txt_Mobile" runat="server"></asp:TextBox>
</div>
<div>
    <asp:Label ID="lbl_Mail" runat="server" Text="البريد الإلكتروني"></asp:Label>
    <asp:TextBox ID="txt_Mail" runat="server"></asp:TextBox>
</div>
----------------------------------------------------------
<div>
    <asp:Label ID="lbl_Area" runat="server" Text="المنطقة"></asp:Label>
    <asp:TextBox ID="txt_Area" runat="server"></asp:TextBox>
</div>
<div>
    <asp:Label ID="lbl_Street" runat="server" Text="الشارع"></asp:Label>
    <asp:TextBox ID="txt_street" runat="server"></asp:TextBox>
</div>
<div>
    <asp:Label ID="lbl_BuildingNum" runat="server" Text="رقم المبني"></asp:Label>
    <asp:TextBox ID="txt_BuildingNum" runat="server"></asp:TextBox>
</div>
<div>
    <asp:Label ID="lbl_AppartmentNum" runat="server" Text="رقم الشقة"></asp:Label>
    <asp:TextBox ID="txt_AppartmentNum" runat="server"></asp:TextBox>
</div>
<div>
    <asp:Label ID="lbl_fullArea" runat="server" Text="المنطقة"></asp:Label>
    <asp:TextBox ID="txt_fullArea" runat="server" TextMode="MultiLine" Columns="20" Rows="2" ></asp:TextBox>

</div>
<asp:HiddenField ID="MOIAddress_hdf" ClientIDMode="Static" runat="server" />

<script>
    $("#txt_QatarID").blur(function () {
        //$("#txt_PassPort").val("");
        //$("#txt_Name").prop('disabled', true);
        //$("#txt_Nationality").prop('disabled', true);

        var url = $("#MOIAddress_hdf").val() + $("#txt_QatarID").val();

        $.ajax({
            url: url
            , type: 'GET'
            , success: function (result) {

                if (result != null) {
                    var isAr = window.location.href.toLowerCase().indexOf("ar") >= 0;

                    if (isAr)
                        $("#txt_Name").val(result.ArabicName);
                    else
                        $("#txt_Name").val(result.EnglishName);



                    $.ajax({
                        url: "/_api/web/lists/getbytitle('Nationality')/items?$select=Title,TitleAr&$filter=ISOCode%20eq%20%27" + result.Nationality + "%27"
                        , type: 'GET'
                        , headers: {
                            "accept": "application / json;odata = verbose",
                        }
                        , success: function (nat) {
                            var nationality = nat.d.results[0];
                            if (isAr)
                                $("#txt_Nationality").val(nationality.TitleAr);
                            else
                                $("#txt_Nationality").val(nationality.Title);

                        }
                    });
                    $("#lbl_QatarIDValidat").text("");
                }
                else {
                    $("#txt_Name").val("");
                    $("#txt_Nationality").val(""); 
                    $("#lbl_QatarIDValidat").text("برجاء إعادة أدخال الرقم الشخصى");
                    $("#txt_QatarID").val("");
                }
            }
            , error: function () {
                $("#txt_Name").val("");
                $("#txt_Nationality").val("");
                $("#lbl_QatarIDValidat").text("برجاء إعادة أدخال الرقم الشخصى");
                $("#txt_QatarID").val("");
            }
        });
    });
    $("#txt_PassPort").blur(function () {
        if ($("#txt_PassPort").length > 1) {
            $("#lbl_QatarIDValidat").text("");
            //$("#txt_QatarID").val("");
            //$("#txt_Name").prop('disabled', false);
            //$("#txt_Nationality").prop('disabled', false);
        }
    }); 
</script>
