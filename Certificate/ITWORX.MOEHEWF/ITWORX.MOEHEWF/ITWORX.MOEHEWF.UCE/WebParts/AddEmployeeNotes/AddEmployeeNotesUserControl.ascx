<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddEmployeeNotesUserControl.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.WebParts.AddEmployeeNotes.AddEmployeeNotesUserControl" %>
<%@ Register Src="~/_controltemplates/15/ITWORX.MOEHEWF.UCE/RolesNavigationLinks.ascx" TagPrefix="uc1" TagName="RolesNavigationLinks" %>

<div class="row section-container side-nav-container">
    <div class="col-md-3 col-xs-4 no-padding">
        <!-- required for floating -->
        <!-- Nav tabs -->
        <uc1:RolesNavigationLinks runat="server" id="RolesNavigationLinks" />
    </div>
    <div class="row section-container">
        <div class="col-md-9 col-sm-12 col-xs-12 margin-top-10 margin-bottom-10  auto-height">
<SharePoint:InputFormTextBox ID="rftDefaultValue" RichText="true" RichTextMode="FullHtml" runat="server" TextMode="MultiLine" Rows="30" Columns="90"></SharePoint:InputFormTextBox>
               <div runat="server" id="dvSuccessMessage" class="row margin-top-15">
            <h5 class="col-md-12 font-size-18 font-weight-600 success-msg">
                <asp:Label ID="lbl_SuccessMessage" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, SavedSuccessfully %>" Visible="false" ForeColor="Green"></asp:Label>
            </h5>
        </div>
        </div>
     
 <div class="col-md-12 no-padding margin-top-15">
                        <asp:Button ID="btn_Save" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Save %>" OnClick="btn_Save_Click" CssClass="btn moe-btn pull-right" />

                    </div>

        </div>
    </div>

<script type="text/javascript">

    function CreateRichEdit(id) {

        if (browseris.ie5up && browseris.win32 && !IsAccessibilityFeatureEnabled()) {

            g_aToolBarButtons = null;

            g_fRTEFirstTimeGenerateCalled = true;

            RTE_ConvertTextAreaToRichEdit(id, true, true, "", "1033", null, null, null, null, null, "Compatible", "\u002f", null, null, null, null);

            RTE_TextAreaWindow_OnLoad(id);

        }

        else {

            document.write("&nbsp;<br><SPAN class=ms-formdescription><a href='javascript:HelpWindowKey(\"nsrichtext\")'>Click for help about adding basic HTML formatting.</a></SPAN>&nbsp;<br>");

        };

    }

    var id = "<%= rftDefaultValue.ClientID %>";


    "here use your own rich text control id and call the function like below;

    CreateRichEdit(id);
</script>