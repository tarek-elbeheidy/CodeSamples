<%@ Assembly Name="ITWORX.MOEHEWF.UCE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=883afb4c05a35fe5" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UploadUC.ascx.cs" Inherits="ITWORX.MOEHEWF.UCE.CONTROLTEMPLATES.ITWORX.MOEHEWF.UCE.UploadUC" %>


<asp:Panel ID="pnl_Files" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, FilesUploaded %>">
    <asp:Label ID="lbl_FileName" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, FileName %>"></asp:Label>
    <asp:FileUpload ID="uploadFile" runat="server" />
    <asp:Button ID="btn_Upload" runat="server" OnClick="btn_Upload_Click" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Upload %>" />
    <asp:Label ID="lbl_msgNotFound" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, FileMsgNotFound %>"></asp:Label>
    <asp:GridView ID="grd_Files" runat="server" AutoGenerateColumns="false" OnRowCommand="grd_Files_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="اسم المرفق">
                <ItemTemplate>
                    <asp:HyperLink ID="lnk_FileUrl" runat="server"
                        NavigateUrl='<%# Eval("LinkFilename") %>' Text='<%# Eval("Filename") %>' />
                    <asp:HiddenField ID="hdn_FNameExt" runat="server" Value='<%# Eval("FNameExt") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Editor" HeaderText="تم الرفع بواسطة " />
            <asp:BoundField DataField="Modified" HeaderText="تاريخ الرفع" />
            <asp:TemplateField HeaderText="الإجراء">
                <ItemTemplate>
                    <asp:ImageButton ID="btn_Delete" runat="server" CommandName="delete" ImageUrl="/Style library/Images/delete.png"
                        OnClientClick="javascript:if(!confirm('Are you sure you want to delete this File?')){return false;}"
                        CommandArgument='<%# Eval("ID") %>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:HiddenField ID="hdn_ReqId" runat="server" Value='<%# Eval("RequestID") %>' />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>
</asp:Panel>
