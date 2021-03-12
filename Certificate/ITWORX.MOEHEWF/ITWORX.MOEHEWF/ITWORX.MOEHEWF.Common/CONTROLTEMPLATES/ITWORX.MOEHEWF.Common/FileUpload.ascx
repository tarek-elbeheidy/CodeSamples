<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.Common/DropdownWithTextbox.ascx" TagPrefix="MOEHE" TagName="DropdownWithTextbox" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileUpload.ascx.cs" Inherits="ITWORX.MOEHEWF.Common.CONTROLTEMPLATES.ITWORX.MOEHEWF.Common.FileUpload" %>

<style>
	
	.fileUploadOnly .form-control {
		margin-bottom: 0;
	}
</style>

<div class="uploadContainer row margin-bottom-15">

 
   
<!--Title with Input file Type-->
    <div class="col-md-10 no-padding-imp col-sm-9 col-xs-12 fileUploadOnly test">
        <div class="uploadControl data-container table-display moe-full-width margin-bottom-0-imp">
            <div class="form-group form-attachment-item">
				<h6 class="font-size-16 margin-bottom-0 margin-top-0 tempTitle visibility-hidden"  runat="server">
				 hidden
                </h6>
                 <h6 class="font-size-16 margin-bottom-0 margin-top-0 attach-cat-title"  runat="server" id="divUploadLabel">
                        <asp:Label runat="server" ID="lblUploadLabel" CssClass="am title"></asp:Label>
                        <asp:Label runat="server" ID="lblRequiredflag" Style="color: Red; " CssClass="astrik" Visible="false"></asp:Label>
                </h6>
                
				<div class="form" runat="server" id="divUploadControl">
					<div class="box no-js" id="fileUploadContan">
						<asp:FileUpload runat="server" name="file-5[]" onchange="clickTheButton();" ID="upfFile" CssClass="inputfile inputfile-4 uploadFile moe-full-width moe-input-padding moe-select input-height-42"></asp:FileUpload>
						<label for="file-5">
							<span class="trimmed">
                               <asp:Label ID="lblUsername" runat="server" Text="<%$Resources:ITWORX.MOEHEWF.Common, choose %>"></asp:Label>
							</span>
						</label>
					</div>
 
                </div>
            </div>


        </div>

        <div>
            <span class="uploadTemplate" id="divUploadTemplate" runat="server">
                        <asp:HyperLink runat="server" ID="hylnkDownloadTemplate" Target="_blank">
                            <asp:Label runat="server" ID="lblTemplateName" Visible="False"></asp:Label>
                        </asp:HyperLink>
                    </span>
        </div>

        <div class="uploadValidations col-md-12 no-padding table-display moe-width-85" id="divUploadValidations" runat="server" >
 
              <asp:CustomValidator ID="cvFileRequired" runat="server" CssClass="form_error error-msg" EnableClientScript="False"
                Display="Dynamic" OnServerValidate="cvFileRequired_ServerValidate" ValidateEmptyText="True" ValidationGroup="Upload"  />


            <asp:CustomValidator ID="cvFileSize" runat="server" CssClass="form_error error-msg" EnableClientScript="False"
                Display="Dynamic" OnServerValidate="cvFileSize_ServerValidate" ValidateEmptyText="True" ValidationGroup="Upload"  />

            <asp:CustomValidator ID="cvFileExists" runat="server" CssClass="form_error error-msg" EnableClientScript="False"
                Display="Dynamic" OnServerValidate="cvFileExists_ServerValidate" ValidateEmptyText="True" ValidationGroup="Upload" />

            <asp:RegularExpressionValidator ID="revFileExtension" runat="server" ControlToValidate="upfFile"
                CssClass="form_error error-msg" ValidationGroup="Upload" Display="Dynamic">
            </asp:RegularExpressionValidator>
            <asp:Label runat="server" ID="lblCustomErrorMessage" CssClass="form_error error-msg" Visible="False"></asp:Label>
        </div>
    </div>



      
<!--Upload btn-->
    <div class="uploadButton col-md-2 col-sm-3 col-xs-12 no-padding-imp text-right xs-text-right " id="divUploadButton" runat="server">
		<h6 class="font-size-16 margin-bottom-0 margin-top-0 attach-cat-title visibility-hidden"  runat="server">
                       hidden 
                </h6>
        <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Enabled="False"
            ValidationGroup="Upload" CssClass="btn moe-btn" />

    </div>


        <div class="col-md-12 col-xs-12 no-padding-imp">
             <h4 class="noFiles no-margin font-size-14" runat="server" id="divNoFiles">
                        <asp:Label ID="lblNoFiles" runat="server"></asp:Label>
                    </h4>
    </div>
  
   
    <div class="col-md-12 col-xs-12 no-padding-imp ">
        <div runat="server" id="divUploadedRepeater" class="col-md-10 col-sm-8 col-xs-12 no-padding-imp uploaded-file-wrap">
   
      <%--  <asp:Label ID="lblAttachmentNameHeader" runat="server" ></asp:Label>
        <asp:Label ID="lblModifiedHeader" runat="server"></asp:Label>
        <asp:Label ID="lblModifiedByHeader" runat="server"></asp:Label>
        <asp:Label ID="lblAction" runat="server"></asp:Label>--%>
        


        <asp:Panel runat="server" ID="pnlUploadedDocuments" EnableViewState="true" CssClass="file-panel">
        </asp:Panel>

        </div>
    </div>

	<div class="col-md-3 col-sm-6 no-padding-imp uploadControlHeader">
        <div class="data-container table-display moe-width-85">
            <div class="form-group">
                <h6 class="font-size-16 margin-bottom-0 margin-top-0" style="height:auto;">
                    <asp:Label runat="server" ID="lblHeader"></asp:Label>
                </h6>

                <div class="form">
                    

                </div>
            </div>
        </div>
    </div>
</div>


<script type="text/javascript">
    //To handle the freezing of page after downloading a file
    function setFormSubmitToFalse() {
        setTimeout(function () { _spFormOnSubmitCalled = false; }, 3000);
        return true;
	}

	
    function ValidateValue(fileUploadControl, uploadButtonId) {
        if ($(fileUploadControl).val()) {
            $('#' + uploadButtonId).attr('disabled', false);
        }
        else {
            $('#' + uploadButtonId).attr('disabled', true);
        }
	} 

	; (function (document, window, index) {
		var inputs = document.querySelectorAll('.inputfile');
		Array.prototype.forEach.call(inputs, function (input) {
			var label = input.nextElementSibling,
				labelVal = label.innerHTML;

			input.addEventListener('change', function (e) {
				var fileName = '';
				if (this.files && this.files.length > 1)
					fileName = (this.getAttribute('data-multiple-caption') || '').replace('{count}', this.files.length);
				else
					fileName = e.target.value.split('\\').pop();

				if (fileName)
					label.querySelector('span').innerHTML = fileName;
				else
					label.innerHTML = labelVal;


			});

			// Firefox bug fix
			input.addEventListener('focus', function () { input.classList.add('has-focus'); });
			input.addEventListener('blur', function () { input.classList.remove('has-focus'); });
		});
	}(document, window, 0));
	

</script>