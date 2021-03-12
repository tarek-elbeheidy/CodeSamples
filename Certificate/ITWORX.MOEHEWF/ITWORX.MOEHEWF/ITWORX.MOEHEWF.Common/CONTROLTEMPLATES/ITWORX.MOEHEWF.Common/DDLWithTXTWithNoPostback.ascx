<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DDLWithTXTWithNoPostback.ascx.cs" Inherits="ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.DDLWithTXTWithNoPostback" %>
    <asp:HiddenField ID="isDepending_HF" runat="server" /> 
    <asp:HiddenField ID="isRequired_HF" runat="server" />
    <asp:HiddenField ID="parentDDL_HF" runat="server" /> 
    <asp:HiddenField ID="lkpARText_HF" runat="server" />  
    <asp:HiddenField ID="lkpENText_HF" runat="server" />  
    <asp:HiddenField ID="lkpVal_HF" runat="server" />  
    <asp:HiddenField ID="lkpList_HF" runat="server" /> 
    <asp:HiddenField ID="hdnTxtWithLbl" runat="server" />

<div class="form-group">

                <label>
                    <asp:Label ID="lblDropdown" runat="server"></asp:Label>
                    <span runat="server" class="astrik error-msg" id="spandrop">*</span>
                </label>
              
                    <asp:DropDownList ID="dropWithNewOption" runat="server" CssClass="moe-dropdown form-control"></asp:DropDownList>
                    
  
    <label class="moe-2nd-label">
                    <asp:Label ID="lblTxtNewOption" runat="server"  ></asp:Label>  
                        </label>
                                          
                    <asp:TextBox ID="txtNewOption" runat="server" CssClass="form-control  margin-bottom-0-imp"></asp:TextBox>
                    
                    <asp:CustomValidator ID="customValidator"
                        runat="server"
                        ErrorMessage="CustomValidator"
                        OnServerValidate="cusCustom_ServerValidate"
                        CssClass="error-msg"
                        Display="Dynamic"></asp:CustomValidator>
              
   
</div>

<script>
    $(function () {

        //====================Preparing Page======================================= 
        var isRequired = $("#<%=isRequired_HF.ClientID %>").val().toLowerCase() == "true";

        if (isRequired)  
            $("#<%=spandrop.ClientID %>").show(); 
        else
            $("#<%=spandrop.ClientID %>").hide();

        if ($("#<%=dropWithNewOption.ClientID %>").val() == "-2") {
            debugger;
            $("#<%=txtNewOption.ClientID %>").show();
            if ($("#<%=hdnTxtWithLbl.ClientID %>").val() == "True") {
              
                $("#<%=lblTxtNewOption.ClientID %>").show();
            
               
            }
            else {
              
                $("#<%=lblTxtNewOption.ClientID %>").hide();
             
                
            }
        }

        else {
            $("#<%=txtNewOption.ClientID %>").hide();
            $("#<%=lblTxtNewOption.ClientID %>").hide();
        }


        //====================Show & Hide Other Text According DDL==================
        $("#<%=dropWithNewOption.ClientID %>").change(function () {
            //debugger;
            if ($("#<%=dropWithNewOption.ClientID %>").val() == "-2") {
               
                $("#<%=txtNewOption.ClientID %>").show();
                if ($("#<%=hdnTxtWithLbl.ClientID %>").val() == "True") {
                
                    $("#<%=lblTxtNewOption.ClientID %>").show();
                 

                    
                }
                else {

                    $("#<%=lblTxtNewOption.ClientID %>").hide();
                  
                }
            }
            else {
                $("#<%=txtNewOption.ClientID %>").val("");
                $("#<%=txtNewOption.ClientID %>").hide();
                $("#<%=lblTxtNewOption.ClientID %>").hide();
           
            }
        });
          
        //====================For Binding===========================================

        if ($("#<%=isDepending_HF.ClientID %>").val().toLowerCase() == "true")
        { 
            var id = $("#<%=parentDDL_HF.ClientID %>").val();
            if (id != "") {
                $("#" + id).change(function () { 
                    $.ajax({
                        url: "/_api/web/lists/getbytitle('" + $("#<%=lkpList_HF.ClientID %>").val()+"')/items?$select=Title,TitleAr,ID&$filter=ISOCode%20eq%20%27" + $("#" + id).val() + "%27"
                        , type: 'GET'
                        , headers: {
                            "accept": "application / json;odata = verbose",
                        }
                        , success: function (data) {
                            var nationality = data.d.results;
                            var isAr = window.location.href.toLowerCase().indexOf("ar") >= 0; 

                            for (var i = 0; i < nationality.length; i++) {
                                $("#<%=dropWithNewOption.ClientID %>").append($("<option/>").val(nationality[i][$("#<%=lkpVal_HF.ClientID %>").val()]).text(isAr ? nationality[i][$("#<%=lkpARText_HF.ClientID %>").val()] : nationality[i][$("#<%=lkpENText_HF.ClientID %>").val()]));
                            }
                        }
                    });
                });
            }
        }
    }) 
</script>
