<%@ Assembly Name="ITWORX.MOEHEWF.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=7b2931724f1d7d1c" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClientSideFileUpload.ascx.cs" Inherits="ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.ClientSideFileUpload" %>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

<asp:HiddenField ID="hdnUploadCount" runat="server" Value="0" />
<asp:HiddenField runat="server" ID="hdnGrp" />
<asp:HiddenField runat="server" ID="hdnLookupFieldName" />
<asp:HiddenField runat="server" ID="hdnLookupFieldValue" />
<asp:HiddenField runat="server" ID="hdnHasOptions" />
<asp:HiddenField runat="server" ID="hdnDocLibrary" />
<asp:HiddenField runat="server" ID="hdnDocLibWebUrl" />
<asp:HiddenField runat="server" ID="hdnMaxFileNo" />
<asp:HiddenField runat="server" ID="hdnMaxSize" />
<asp:HiddenField runat="server" ID="hdnDisplayMode" />

<div class="col-md-6 col-sm-6 col-xs-4">
    <div class="form-group">
        <label class="fileUploadLabel">
            <asp:Label ID="lblFileName" runat="server"></asp:Label><asp:Label ID="lblFileUpload" runat="server" ForeColor="Red" Visible="false">*</asp:Label>
        </label>
        <div class="box no-js">

            <asp:FileUpload ID="fileUpload" runat="server" name="file-5[]" CssClass="form-control inputfile inputfile-4 uploadFile" />
            <label for="file-5" runat="server" id="lblChoose">
                <span class="trimmed"><%=Resources.ITWORX.MOEHEWF.Common.ChooseFile %></span>
            </label>
            <span id="<%=ClientID %>_noFiles" style="display: none;"><%=Resources.ITWORX.MOEHEWF.Common.FU_NoFilesUploaded %></span>
        </div>
        <asp:CustomValidator ID="custRequiredFile" runat="server" Display="Dynamic" ForeColor="Red"
            ClientValidationFunction='<%# this.ClientID + "_requiredFileUpload"  %>' OnServerValidate="custRequiredFile_ServerValidate"></asp:CustomValidator>	

        <asp:RegularExpressionValidator ID="regFileExt" runat="server" Display="Dynamic" ControlToValidate="fileUpload" ValidationGroup="Upload" ForeColor="Red"></asp:RegularExpressionValidator>

        <span id="<%=ClientID %>_size" style="display: none; color: red"><%= string.IsNullOrEmpty(FileSizeValidationMsg) ?  string.Format(Resources.ITWORX.MOEHEWF.Common.FU_FileSizeMessage , (float) MaxSize / (1000 * 1024))  :FileSizeValidationMsg %></span>
        <span id="<%=ClientID %>_fileNumbers" style="display: none; color: red"><%= FileNumbersValidationMsg %></span>
        <span id="<%=ClientID %>_fileExistence" style="display: none; color: red"><%= FileExistsValidationMsg %></span>

    </div>
</div>

<div class="col-md-3">
    <label class="FileBtnLabel">hide</label>
    <asp:Button ID="btnUpload" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, FU_Upload %>"  ValidationGroup="Upload" Enabled="false" OnClientClick="return validate()" CssClass="btn moe-btn" />
</div>







<div class="clearfix"></div>
<div class="col-md-8 col-sm-12 col-xs-12">
    <div runat="server" id="header">
    </div>
    <div id="uploadedDiv" runat="server">
    </div>
</div>




<asp:HiddenField ID="hdnFileExist" runat="server" />


<script type="text/javascript">

    SP.SOD.executeFunc('sp.js', 'SP.ClientContext');

    $("#<%=fileUpload.ClientID%>").on('change', function () {
        debugger;
        $("#<%=ClientID %>_fileExistence").hide();
        $("#<%=ClientID %>_fileNumbers").hide();
        $("#<%=ClientID %>_size").hide();
      
       
       // var upBtn = $("#<%=fileUpload.ClientID%>").nextAll('input')[0];
       //var upBtn = <%--("#<%=btnUpload.ClientID%>"); $("#<%# this.ClientID + "_btnUpload"  %>");$--%>

        var upBtn = $("#<%=ClientID %>_btnUpload");

        if ($("#<%=fileUpload.ClientID%>").val() != "") {
            // $("#" + upBtn.id).attr("disabled", false);
            $(upBtn).attr("disabled", false);
        }
        else {
            // $("#" + upBtn.id).attr("disabled", true);
            $(upBtn).attr("disabled", true);
        }
    });
   

    if ($('#<%=hdnHasOptions.ClientID%>').val() === "True") {
        debugger;
        $("#" + "<%=DropClientID%>").on('change', function () {
            if (parseInt($("#" + "<%=DropClientID%>").val()) > 0) {
                $("#" + "<%=ReqDropClientID%>").hide();
            }
        });
    }
    debugger;
    var maxFileNumbers = parseInt($('#<%=hdnMaxFileNo.ClientID%>').val());
    //alert(maxFileNumbers);
    function <%=ClientID %>_requiredFileUpload(source, arguments) {
        debugger;
        var maxFileNumbers = parseInt($('#<%=hdnMaxFileNo.ClientID%>').val());
        var fileUpload = $('#<%=fileUpload.ClientID%>');
        console.log(maxFileNumbers);

        if (maxFileNumbers != 0 && parseInt($('#<%=hdnUploadCount.ClientID%>').val()) < maxFileNumbers) {
            arguments.IsValid = false;

        } else {
            arguments.IsValid = true;
            

        }
    }


    $("#<%=ClientID %>_btnUpload").click(function () {

        //Validate max file numbers 
        var maxFileNumbers = parseInt($('#<%=hdnMaxFileNo.ClientID%>').val());

        var fileUpload = $('#<%=fileUpload.ClientID%>');
        var validNum = false;
        if (parseInt($('#<%=hdnUploadCount.ClientID%>').val()) < maxFileNumbers || maxFileNumbers === 0) {

            validNum = true;
            $("#<%=ClientID %>_fileNumbers").hide();
        }
        else {

            validNum = false;
            $("#<%=ClientID %>_fileNumbers").show();
        }

        //Validate file size 
        var validSize = false;
        var maxFileSize = parseInt($('#<%=hdnMaxSize.ClientID%>').val());
        var fileUpload = $('#<%=fileUpload.ClientID%>');
        debugger;
        if (fileUpload.val() === "") {

            validSize = true;
            $("#<%=ClientID %>_size").hide();
        }
      
        else
            if (fileUpload.val() != "" && fileUpload[0].files[0].size  < maxFileSize) {

                validSize = true;
                $("#<%=ClientID %>_size").hide();
            }
            else {
                // arguments.IsValid = false;
                validSize = false;
                $("#<%=ClientID %>_size").show();
            }

        //Validate file existence 
        var dataLength = 0;
        var hasOptions = $('#<%=hdnHasOptions.ClientID%>').val();
        var fileTitle = "";


        if (hasOptions === "True" && $("#" + "<%=DropClientID%>" + " option:selected").val() === "-2") {
            fileTitle = $("#" + "<%=TextBoxClientID%>").val();
          }
       else if (hasOptions === "True") {
            fileTitle = $("#" + "<%=DropClientID%>" + " option:selected").text();
        }



        else {
            var filename = $('#<%=fileUpload.ClientID%>').get(0).files[0].name;
            fileTitle = $('#<%=fileUpload.ClientID%>').get(0).files[0].name.substr(0, filename.lastIndexOf('.'));

        }

        var requestUri = $('#<%=hdnDocLibWebUrl.ClientID%>').val() +
            "/_api/web/lists/getByTitle('" + $('#<%=hdnDocLibrary.ClientID%>').val() + "')/items?$select=EncodedAbsUrl,ID,Title&$filter=MOEHEDocumentGroup eq '" + $('#<%=hdnGrp.ClientID%>').val() + "' and " + $('#<%=hdnLookupFieldName.ClientID%>').val() + " eq " + $('#<%=hdnLookupFieldValue.ClientID%>').val() + " and Title eq '" + fileTitle + "' and MOEHEDocumentStatus ne 'Deleted'";
        requestUri = encodeURI(requestUri);

        var requestHeaders = {
            "accept": "application/json;odata=verbose"
        }


        $.ajax({
            url: requestUri,
            type: 'GET',
            dataType: 'json',
            headers: requestHeaders,
            async: false,
            success: function (data) {


                dataLength = data.d.results.length;



            },
            error: function ajaxError(response) {
                console.log(response.status + ' ' + response.statusText);
                //alert(response.status + ' ' + response.statusText);
            }
        });


    
        var validExist = false;
        if (dataLength === 0) {

            validExist = true;
            $("#<%=ClientID %>_fileExistence").hide();
           }
           else {

               validExist = false;
               $("#<%=ClientID %>_fileExistence").show();
        }
        if (validSize === false || validExist === false || validNum === false) {
            return false;
        }

        else {
            Page_ClientValidate();
        }

           
            if (Page_IsValid) {
            <%--  var upBtn = $("#<%=fileUpload.ClientID%>").nextAll('input')[0];
            $("#" + upBtn.id).attr("disabled", true);--%>

               var upBtn = $("#<%=ClientID %>_btnUpload");<%-- $("#<%# this.ClientID + "_btnUpload"  %>");--%> <%--$("#<%=btnUpload.ClientID%>");--%>
               $(upBtn).attr("disabled", true);

               if (hasOptions === "True") {
                   var selectedVal = $("#" + "<%= DropClientID  %>" + " option:selected").val();

                   if (parseInt(selectedVal) === 0 || parseInt(selectedVal) === -1 || selectedVal === "") {



                    $("#" + "<%=ReqDropClientID%>").show();
                    return false;
                }
                else {

                    $("#" + "<%=ReqDropClientID%>").hide();

                }


            }

            var fileElement = $('#<%=fileUpload.ClientID%>');

            var parts = fileElement.get(0).value.split("\\");
            var filename = parts[parts.length - 1];
            var fileReader = new FileReader();
            //File loaded  
            //debugger;
            fileReader.onloadend = function (event) {
                if (event.target.readyState == FileReader.DONE) {
                    var filecontent = event.target.result;
                    // alert('Hi');
                    //Code to upload image in Images Library  

                    doclibraryURL = $('#<%=hdnDocLibrary.ClientID%>').val();


                    var completeDocLibraryUrl = $('#<%=hdnDocLibWebUrl.ClientID%>').val()
                        + "/_api/web/lists/getByTitle('" + $('#<%=hdnDocLibrary.ClientID%>').val() + "')/RootFolder/Files/add(url='" + filename + "',overwrite=true)?$expand=ListItemAllFields";


                    $.ajax({
                        url: completeDocLibraryUrl,
                        type: "POST",
                        data: filecontent,
                        async: false,
                        processData: false,
                        headers: {
                            "accept": "application/json;odata=verbose",
                            "X-RequestDigest": $("#__REQUESTDIGEST").val(),
                        },
                        complete: function (data) {
                            var clientContext = new SP.ClientContext($('#<%=hdnDocLibWebUrl.ClientID%>').val());
                              var web = clientContext.get_web();
                              var fileurl = doclibraryURL + "/" + filename;
                              console.log(fileurl);
                              console.log(_spPageContextInfo.webAbsoluteUrl);

                              var imagetopublish = web.getFileByServerRelativeUrl(fileurl);

                              imagetopublish.checkIn();
                              imagetopublish.publish();



                              clientContext.executeQueryAsync();
                              //  alert('data');
                              debugger;
                              console.log(data)
                              console.log(data.d);

                              var jsonData = JSON.parse(data.responseText);
                              var itemID = jsonData.d.ListItemAllFields.ID;



                              var hasOptions = $('#<%=hdnHasOptions.ClientID%>').val();
                              var fileTitle = "";

                              if (hasOptions === "True" && $("#" + "<%=DropClientID%>" + " option:selected").val() === "-2") {
                                  fileTitle = $("#" + "<%=TextBoxClientID%>").val();
                                }
                            else if (hasOptions === "True") {

                                fileTitle = $("#" + "<%= DropClientID  %>" + " option:selected").text();
                            }
                            else {

                                fileTitle = filename.substr(0, filename.lastIndexOf('.'));
                            }

                            var updated = updateFile(itemID, fileTitle, $('#<%=hdnGrp.ClientID%>').val(), $('#<%=hdnLookupFieldName.ClientID%>').val() + "Id", $('#<%=hdnLookupFieldValue.ClientID%>').val(), $('#<%=hdnDocLibrary.ClientID%>').val(), $('#<%=hdnDocLibWebUrl.ClientID%>').val());

                            if (updated === true) {
                                var upBtn = $("#<%=ClientID %>_btnUpload");  <%-- $("#<%=fileUpload.ClientID%>").nextAll('input')[0];--%>
                                $(upBtn).attr("disabled", true);
                                console.log(parseInt($('#<%=hdnUploadCount.ClientID%>').val()));
                                debugger;
                                if (parseInt($('#<%=hdnUploadCount.ClientID%>').val()) === 0) {

                                  <%--  $('#<%=header.ClientID%>').attr("style", "width: 500px; height: 25px; clear: both");
                                   $('#<%=header.ClientID%>').css('background-color', '#93c9ff');--%>

                                    <%--  $('#<%=header.ClientID%>').append("<div class='testTable' style='width: 175px; float: left; margin-left: 40px;'>" +'<%=Resources.ITWORX.MOEHEWF.Common.FileName %>' + "</div>" +
                                        "<div style='width: 180px; float: left'>" +'<%=Resources.ITWORX.MOEHEWF.Common.UploadedDate %>' + "</div>" +
                                       "<div style='width: 50px; float: left'>" +'<%=Resources.ITWORX.MOEHEWF.Common.Actions %>' + "</div>");--%>
                                    debugger;
                                    $('#<%=header.ClientID%>').append("<table class='FileTable table moe-table table-striped'><tbody><th class='text-center'>" + '<%=Resources.ITWORX.MOEHEWF.Common.FileName %>' + "</th>" +
                                        "<th class='text-center'>" + '<%=Resources.ITWORX.MOEHEWF.Common.UploadedDate %>' + "</th>" +
                                        "<th class='text-center'>" + '<%=Resources.ITWORX.MOEHEWF.Common.Actions %>' + "</th></tbody></table>");


                                    if (parseInt($('#<%=hdnUploadCount.ClientID%>').val()) === 0) {
                                        $('#<%=header.ClientID%>').attr("style", "width: 500px; height: 25px; clear: both");
                                        $('#<%=header.ClientID%>').css('background-color', '#93c9ff');

                                    }
                                }
                                    //added

                               <%=ClientID %>_ShowUploadedFiles(fileElement.get(0).files[0].name, fileTitle, itemID, jsonData.d.ListItemAllFields.Created);
                                $('#<%=hdnUploadCount.ClientID%>').val(parseInt($('#<%=hdnUploadCount.ClientID%>').val()) + 1);

                               $('#<%=fileUpload.ClientID%>').val("");
                                if (hasOptions === "True") {
                                    $("#" + "<%= DropClientID  %>").val("-1");
                                    $("#" + "<%=TextBoxClientID%>").val("");
                                    $("#" + "<%=TextBoxClientID%>").hide();
                               }
                               
                            }
                          }, //complete  
                        error: function (err) {
                            console.log(err + err.message + err.stacktrace);
                            // alert(err + err.message + err.stacktrace);
                          }//error  
                      });
                }


            };//else
            fileReader.readAsArrayBuffer(fileElement.get(0).files[0]);


            return false;
           }
           else {
               return false;
           }

    });





    //show uploaded file 
    function  <%=ClientID %>_ShowUploadedFiles(file, fileName, fileID, createdDate) {
        debugger;

        var hdnid = fileID;

        var lblfilename = fileName + fileID;

        var ext = file.substr((file.lastIndexOf('.') + 1));

      
        debugger;


        <%--$('#<%=header.ClientID%> table').append("<span id='" + '<%=ClientID%>_' + hdnid + "''><td id='" + '<%=ClientID%>_' + hdnid + "''><span id='" + '<%=ClientID%>_' + hdnid + "'>" + fileName +
            "</span><span id='" + '<%=ClientID%>_' + hdnid + "''>" + new Date(createdDate).localeFormat() +
              "</span><span id='" + '<%=ClientID%>_' + hdnid + "' ' >" +
              "<a href='#' id='" + '<%=ClientID%>_' + hdnid + "' class='dellink fa fa-times delete-icon' title=" + '<%=Resources.ITWORX.MOEHEWF.Common.Delete %>' + " onclick='deleteDraftRow(\"#" + '<%=ClientID%>_' + hdnid + "," + hdnid + "\")'></a>" + // for deleting file

            "<span' id='" + '<%=ClientID%>_' + hdnid + "' ><a class='dellink fa fa-eye display-icon' id= '" + '<%=ClientID%>_' + hdnid + "'  title=" + '<%=Resources.ITWORX.MOEHEWF.Common.View %>' + "   href= '#' onclick='view(\"" + file + "\")' ></a ></span ></span ></span>");--%>

        debugger;

        if ($('#<%=header.ClientID%> table').length == 0) {
            $('#<%=header.ClientID%>').append("<table class='FileTable table moe-table table-striped'><tbody><th class='text-center'>" +'<%=Resources.ITWORX.MOEHEWF.Common.FileName %>' + "</th>" +
                  "<th class='text-center'>" +'<%=Resources.ITWORX.MOEHEWF.Common.UploadedDate %>' + "</th>" +
                    "<th class='text-center'>" + '<%=Resources.ITWORX.MOEHEWF.Common.Actions %>' + "</th></tbody></table>");
          }

        if ($('#<%=hdnDisplayMode.ClientID%>').val() == "False") {

          
            $('#<%=header.ClientID%> table.FileTable').append("<tr id='" + '<%=ClientID%>_' + hdnid + "''><td id='" + '<%=ClientID%>_' + hdnid + "''><span id='" + '<%=ClientID%>_' + hdnid + "'>" + fileName +
                "</td><td id='" + '<%=ClientID%>_' + hdnid + "''>" + String.format("{0:d}", new Date(createdDate)) + " " + String.format("{0:t}", new Date(createdDate)) +
                 "</td><td id='" + '<%=ClientID%>_' + hdnid + "' ' >" +
                 "<span' id='" + '<%=ClientID%>_' + hdnid + "' ><a class='dellink fa fa-eye display-icon' id= '" + '<%=ClientID%>_' + hdnid + "'  title=" + '<%=Resources.ITWORX.MOEHEWF.Common.View %>' + "   href= '#' onclick='<%=ClientID%>_view(\"" + file + "\")' ></a ></span ></td ></tr>");


        }
        else {

          
            $('#<%=header.ClientID%> table.FileTable').append("<tr id='" + '<%=ClientID%>_' + hdnid + "''><td id='" + '<%=ClientID%>_' + hdnid + "''><span id='" + '<%=ClientID%>_' + hdnid + "'>" + fileName +
                "</td><td id='" + '<%=ClientID%>_' + hdnid + "''>" + String.format("{0:d}", new Date(createdDate)) + " " + String.format("{0:t}", new Date(createdDate)) +
                "</td><td id='" + '<%=ClientID%>_' + hdnid + "' ' >" +
                "<a href='#' id='" + '<%=ClientID%>_' + hdnid + "' class='dellink fa fa-times delete-icon' title=" + '<%=Resources.ITWORX.MOEHEWF.Common.Delete %>' + " onclick='<%=ClientID%>_deleteDraftRow(\"#" + '<%=ClientID%>_' + hdnid + "," + hdnid + "\")'></a>" + // for deleting file
                "<span' id='" + '<%=ClientID%>_' + hdnid + "' ><a class='dellink fa fa-eye display-icon' id= '" + '<%=ClientID%>_' + hdnid + "'  title=" + '<%=Resources.ITWORX.MOEHEWF.Common.View %>' + "   href= '#' onclick='<%=ClientID%>_view(\"" + file + "\")' ></a ></span ></td ></tr>");

        }
        return false;

    }
    function GetItemTypeForListName(name) {
        // debugger;
        return "SP.Data." + name.charAt(0).toUpperCase() + name.slice(1) + "ListItem";

    }
    function CreateGuid() {
        function _p8(s) {
            var p = (Math.random().toString(16) + "000000000").substr(2, 8);
            return s ? "-" + p.substr(0, 4) + "-" + p.substr(4, 4) : p;
        }
        return _p8() + _p8(true) + _p8(true) + _p8();
    }


    function updateFile(itemId, fileName, Group, LookupFieldName, LookupFieldValue, LibraryName, LibraryUrl) {

        var itemType = GetItemTypeForListName(LibraryName);
        var updated = false;
        //debugger;
        var itemObj = '{"__metadata": { "type": "' + itemType + '" },"MOEHEDocumentGroup": "' + Group + '","MOEHEDocumentStatus": "Uploaded","' + LookupFieldName + '": ' + parseInt(LookupFieldValue) + ',"FileLeafRef": "' + CreateGuid() + '","Title": "' + fileName + '"}';

        var item = JSON.parse(itemObj);
        $.ajax({
            url: LibraryUrl + "/_api/web/lists/GetByTitle('" + LibraryName + "')/items(" + itemId + ")",
            type: "POST",
            headers: {
                "accept": "application/json;odata=verbose",
                "X-RequestDigest": $("#__REQUESTDIGEST").val(),
                "content-Type": "application/json;odata=verbose",
                "IF-MATCH": "*",
                "X-HTTP-Method": "MERGE"
            },
            data: JSON.stringify(item),
            async: false,

            success: function (data) {
                updated = true;

            },
            error: function (error) {
                console.log(JSON.stringify(error));
                //alert(JSON.stringify(error));
            }
        });
        return updated;
    }


    function <%=ClientID%>_deleteDraftRow(divrow) {
        //debugger;
        var str = divrow.split(",");
        var row = str[0];
        var itemId = str[1];

        if (confirm("<%= ConfirmationDeleteMsg %>")) {
            var LibraryName = $('#<%=hdnDocLibrary.ClientID%>').val();
            var itemType = GetItemTypeForListName(LibraryName);

            // debugger;

            var LibraryUrl = $('#<%=hdnDocLibWebUrl.ClientID%>').val();



            var item = {
                "__metadata": { "type": itemType },
                "MOEHEDocumentStatus": 'Deleted'

            };

            $.ajax({
                url: LibraryUrl + "/_api/web/lists/GetByTitle('" + LibraryName + "')/items(" + itemId + ")",
                type: "POST",
                headers: {
                    "accept": "application/json;odata=verbose",
                    "X-RequestDigest": $("#__REQUESTDIGEST").val(),
                    "content-Type": "application/json;odata=verbose",
                    "IF-MATCH": "*",
                    "X-HTTP-Method": "MERGE"
                },
                data: JSON.stringify(item),
                async: false,

                success: function (data) {

                    $(row).remove();
                  <%--  console.log($(parseInt('#<%=hdnUploadCount.ClientID%>').val()));--%>
                    $('#<%=hdnUploadCount.ClientID%>').val(parseInt($('#<%=hdnUploadCount.ClientID%>').val()) - 1);
                    console.log($('#<%=hdnUploadCount.ClientID%>').val());
                    //debugger;
                    if (parseInt($('#<%=hdnUploadCount.ClientID%>').val()) === 0) {

                        $('#<%=header.ClientID%> table').empty();
                        $('#<%=header.ClientID%>').empty();
                        //$('#<%=header.ClientID%>').hide();
                    }
                },
                error: function (error) {
                    console.log(JSON.stringify(error));
                   // alert(JSON.stringify(error));
                }
            });


        }
        return false;
    }



    function deleterow(divrow) {
        //debugger;
        var str = divrow.split(",");
        var row = str[0];
        var file = str[1];

        if (confirm("<%= ConfirmationDeleteMsg %>")) {
            $.ajax(
                {
                    url: $('#<%=hdnDocLibWebUrl.ClientID%>').val()
                    + "/_api/web/lists/getByTitle('" + $('#<%=hdnDocLibrary.ClientID%>').val() + "')/items('" + file + "')",
                    type: "DELETE",
                    async: false,
                    headers: {
                        "accept": "application/json;odata=verbose",
                        "X-RequestDigest": $("#__REQUESTDIGEST").val(),
                        "IF-MATCH": "*"
                    },
                    success: function (data) {
                        //debugger;
                        $(row).remove();
                        $('#<%=hdnUploadCount.ClientID%>').val(parseInt($('#<%=hdnUploadCount.ClientID%>').val()) - 1);

                        //update hdn field when delete
                        console.log("Success");
                    },
                    error: function (err) {
                        console.log(JSON.stringify(err));
                        //alert(JSON.stringify(err));
                    }
                }
            );

        }
        return false;
    }
    function <%=ClientID%>_view(externallink) {
        window.open(externallink, target = '_blank')
    }
    function viewFile(divrow) {
        var str = divrow.split(",");
        var Id = str[0];
        var file = str[1];
        var ext = str[2];
        var param = {};
        param.itemid = Id;
        param.filename = file;
        param.extension = ext;
        $.ajax({
            type: "POST",
            url: _spPageContextInfo.siteAbsoluteUrl + "/_layouts/15/FileUpload/ViewFiles.aspx/ViewFile",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(param),
            dataType: "json",
            success: function (/*data*/) {
                console.log("Success");
               // alert("Success");
            },
            error: function (xhr, status, error) {
                console.log(xhr.status);
                console.log(xhr.responseText);
               // alert(xhr.status);
               // alert(xhr.responseText);
            }
        });
    }


    $(document).ready(function () {

        debugger;
        if ($('#<%=hdnHasOptions.ClientID%>').val() === "True") {
            $('#<%=LabelRequiredDrop%>').show();
        }

        var requestUri = $('#<%=hdnDocLibWebUrl.ClientID%>').val() +
            "/_api/web/lists/getByTitle('" + $('#<%=hdnDocLibrary.ClientID%>').val() + "')/items?$select=EncodedAbsUrl,ID,Title,Created&$filter=MOEHEDocumentGroup eq '" + $('#<%=hdnGrp.ClientID%>').val() + "' and " + $('#<%=hdnLookupFieldName.ClientID%>').val() + " eq " + $('#<%=hdnLookupFieldValue.ClientID%>').val() + " and MOEHEDocumentStatus ne 'Deleted'";
        requestUri = encodeURI(requestUri);

        var requestHeaders = {
            "accept": "application/json;odata=verbose"
        }

        $.ajax({
            url: requestUri,
            type: 'GET',
            dataType: 'json',
            headers: requestHeaders,
            success: function (data) {
                debugger;
                var retRs = data.d.results;
                $('#<%=uploadedDiv.ClientID%>').empty();
                $('#<%=hdnUploadCount.ClientID%>').val(retRs.length);

                if (retRs.length === 0 && $('#<%=hdnDisplayMode.ClientID%>').val() == "False") {
                    $("#<%=ClientID %>_noFiles").show();
                  }
                  else {
                      $("#<%=ClientID %>_noFiles").hide();
                  }
                if (retRs.length != 0) {
                    $('#<%=header.ClientID%>').show();
                    //debugger;
                 //  $('#<%=header.ClientID%>').attr("style", "width: 500px; height: 25px; clear: both");
                    // $('#<%=header.ClientID%>').css('background-color', '#93c9ff');
                    debugger;
                    <%--$('#<%=header.ClientID%>').append("<table class='FileTable table moe-table table-striped'><tbody><th class='text-center'>" +'<%=Resources.ITWORX.MOEHEWF.Common.FileName %>' + "</th>" +
                        "<th class='text-center'>" +'<%=Resources.ITWORX.MOEHEWF.Common.UploadedDate %>' + "</th>" +
                        "<th class='text-center'>" + '<%=Resources.ITWORX.MOEHEWF.Common.Actions %>' + "</th></tbody></table>");--%>

                }
               
                for (var i = 0; i < retRs.length; i++) {


                    debugger;
                   <%=ClientID %>_ShowUploadedFiles(retRs[i].EncodedAbsUrl, retRs[i].Title, retRs[i].Id, retRs[i].Created);


                }
               
             
            },
            error: function ajaxError(response) {
                console.log(response.status + ' ' + response.statusText);
              //  alert(response.status + ' ' + response.statusText);
            }
        });
    });

    //To handle the freezing of page after downloading a file
    function setFormSubmitToFalse() {
        setTimeout(function () { _spFormOnSubmitCalled = false; }, 3000);
        return true;
    }


    function ViewMode(viewLink) {


        debugger;


        window.document.forms[0].target = '_blank';
        setTimeout(function () { window.document.forms[0].target = ''; }, 500);
        setFormSubmitToFalse();
    }
</script>











