<%@ Assembly Name="ITWORX.MOEHEWF.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7b2931724f1d7d1c" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DropdownWithTextbox.ascx.cs" Inherits="ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.DropdownWithTextbox" %>

<div id="container" class="row">
<%--    <asp:HiddenField ID="hdnTextNewItem"  runat="server"/>--%>
 
 
                       <asp:HiddenField ID="hdnDropName" runat="server"  />
        
       



    <div class="col-md-12 col-sm-12 col-xs-12 no-padding-imp">
        <div class=" moe-full-width">
            <div class="form-group">
                <h6 class="font-size-16 margin-bottom-0 margin-top-0">
                    <asp:Label ID="lblDropdown" runat="server"></asp:Label>
					<span class="astrik"  id="spandrop" runat="server" visible="false" style="color: red"> *</span>
                </h6>
                <div class="form">
                     <asp:DropDownList ID="dropWithNewOption" runat="server"  CssClass="moe-dropdown moe-full-width moe-input-padding moe-select input-height-42"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqDropWithNewOption" runat="server"  ControlToValidate="dropWithNewOption" CssClass="error-msg" Display="Dynamic" Enabled="false" ></asp:RequiredFieldValidator>

                   <%-- <div id="containerNewOption">--%>
                        <asp:TextBox  ID="txtNewOption" runat="server"  style="display:none;"  CssClass="newTextBox moe-full-width moe-input-padding moe-select input-height-42"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqNewOptionText" runat="server"   ControlToValidate="txtNewOption" CssClass="error-msg" Display="Dynamic" Enabled="false"></asp:RequiredFieldValidator>
                    <%--</div>--%>

                </div>
            </div>
        </div>
    </div>


   


    
</div>



<script type="text/javascript">


   
    $(document).ready(function () {
      //  $('#containerNewOption').hide();
     <%--   if ($('#<%=dropWithNewOption.ClientID %>').is(":hidden") == true) {
            ValidatorEnable(document.getElementById('<%=reqDropWithNewOption.ClientID %>'), false);
            $('#<%=reqDropWithNewOption.ClientID %>').hide();
        }
        else {
            ValidatorEnable(document.getElementById('<%=reqDropWithNewOption.ClientID %>'), true);
            //$('#<%=reqDropWithNewOption.ClientID %>').hide();
        }--%>
        function SetOtherTextBoxValidation() {
            if ($('#<%=dropWithNewOption.ClientID %> option:selected').val() == "New") {

               // $('#containerNewOption').show();  
                $('#<%=txtNewOption.ClientID %>').show();         
                //ValidatorEnable(document.getElementById('<%=reqNewOptionText.ClientID %>'), true);
               // $('#<%=reqNewOptionText.ClientID %>').hide();
            
            }
            else {

                $('#<%=txtNewOption.ClientID %>').hide();
                $('#containerNewOption').hide();
                $('#<%=txtNewOption.ClientID %>').val("");
                //ValidatorEnable(document.getElementById('<%=reqNewOptionText.ClientID %>'), false);
            }
        }
        SetOtherTextBoxValidation();

        $('#<%=dropWithNewOption.ClientID %>').change(function () {


            SetOtherTextBoxValidation();
        });
    });

  
<%--    $('input[type="submit"]').click(function () {

        if ($('#<%=dropWithNewOption.ClientID %> option:selected').text() == $('#<%=hdnTextNewItem.ClientID %>').val()) {
            ValidatorEnable(document.getElementById('<%=reqtxtNewOption.ClientID %>'), true);

        }
        else {
            ValidatorEnable(document.getElementById('<%=reqtxtNewOption.ClientID %>'), false);
        }
    });--%>

//var id = document.getElementById('<%= dropWithNewOption.ClientID %>');



//var dropWithNewOption = $('#<%= dropWithNewOption.ClientID %>');
  <%-- 
alert($('#<%=dropWithNewOption.ClientID %> option:selected').text());

   //var hdnDropName = $("#hdnDropName").val();
   // var hdnTextNew = $("#hdnTextNewItem").val();
   
  $(document).ready(function () {
        if ($("#dropWithNewOption :selected").text() == hdnTextNew) {
            $('#txtNewOption' ).show();

        }
        else {
            $('#txtNewOption').hide();

            ValidatorEnable(document.getElementById('reqtxtNewOption'), false);
        }
    });
   
   $('#<%=dropWithNewOption.ClientID %>').change(function () {
        if ($('#<%=dropWithNewOption.ClientID %> option:selected').text() == "Qatar University") {
		alert("show");
           // $('#txtNewOption' ).show();

        }
        else {
		alert("hide");
         //   $('#txtNewOption' ).hide();
         
          //  ValidatorEnable(document.getElementById('reqtxtNewOption'), false);
        }
    });
	
	
	--%>
	
</script>
