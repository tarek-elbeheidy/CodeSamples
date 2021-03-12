<%@ Assembly Name="MOEHE.PSPES, Version=1.0.0.0, Culture=neutral, PublicKeyToken=677b80a9c9c0da8c" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SetupSupportingDocsUserControl.ascx.cs" Inherits="MOEHE.PSPES.Webparts.SetupSupport.SetupSupportingDocs.SetupSupportingDocsUserControl" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>

 <link href="/_layouts/15/MOEHE.PSPES/assets/css/chosen.css" rel="stylesheet"  type="text/css" />
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>

<section class="form-horizontal">
    <div class="container mt-30 mb-30 pt-0 pb-0 bg-white-theme">
          <div class="row">
            <!-- BEGIN EXAMPLE TABLE PORTLET-->
            <div class="portlet light ">
                <div class="portlet-title hidden">
                    <div class="caption font-red">
                        <i class="fa fa-upload font-red" aria-hidden="true"></i>
                        <span class="caption-subject bold uppercase">
                           <asp:Literal id="SetupDocumentAttachedLiteral" runat="server" Text="<%$Resources:MOEHE.PSPES,SetupDocumentAttachedToSchol%>"></asp:Literal>
                        </span>
                    </div>
                </div>
                <div class="col-md-12 borde-bottom pb-15">
					<h3 class="caption p-0">
                           <asp:Literal id="AdvancedSearchLiteral" runat="server" Text="<%$Resources:MOEHE.PSPES,AdvancedSearch%>"></asp:Literal>
                    </h3>
                    <div class="col-md-12">
                        <!--Start Row-->
                        <div class="row">
                            <!--FiltreTerm-->
                            <div class="col-md-2">
								<div class="form-group">
									<label for="FiltreTerm">
										<asp:Literal id="TermNameLiteral" runat="server" Text="<%$Resources:MOEHE.PSPES,TermName%>"></asp:Literal>
									</label>
									<asp:TextBox ID="TermNameTextBox" class="form-control" runat="server" AutoPostBack="True" ReadOnly="true" OnTextChanged="TermNameTextBox_TextChanged"></asp:TextBox>
									<asp:TextBox ID="FullTermNameTextBox" class="form-control" runat="server" AutoPostBack="True" ReadOnly="true" OnTextChanged="FullTermNameTextBox_TextChanged"></asp:TextBox>
								</div>
								</div>
                            <!--Curriculum-->
                            <div class="col-md-2"> 
                                <label for="Curriculum">
                                    <asp:Literal id="CurriculumLiteral" runat="server" Text="<%$Resources:MOEHE.PSPES,Curriculum%>"></asp:Literal>
                                </label>
                                <asp:DropDownList  class="chzn-select form-control pt-0" ID="CurriculumsDropDownList" AutoPostBack="true" OnSelectedIndexChanged="CurriculumsDropDownList_SelectedIndexChanged" runat="server"></asp:DropDownList>
                            </div>
                            <!--SchoolCode-->
                            <div class="col-md-4"> 
                                <label for="FiltreSchoolCode">
                                    <asp:Literal id="SchoolCodeLiteral" runat="server" Text="<%$Resources:MOEHE.PSPES,SchoolCode%>"></asp:Literal>
                                </label>
                                <asp:DropDownList  class="chzn-select form-control pt-0" ID="SchoolCodesDropDownList" OnSelectedIndexChanged="SchoolCodesDropDownList_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                            </div>
                            <!--Select Grade or All-->
                            <div class="col-md-1">
                                <div class="form-group">
                                    <label for="FiltreCurriculum">
                                        <asp:Literal id="Literal8" runat="server" Text="<%$Resources:MOEHE.PSPES,Grade%>"></asp:Literal>
                                    </label>
                                     <asp:DropDownList  class="form-control pt-0" ID="SchoolGradesDropDownList" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <!--Document Name-->
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="FiltreCurriculum">
                                         <asp:Literal id="Literal3" runat="server" Text="<%$Resources:MOEHE.PSPES,DocumentName%>"> </asp:Literal>
                                    </label>
                                    <asp:DropDownList  class="form-control pt-0" ID="DocumentTypesDropDownList" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <!--Search-->
							<div class="col-md-12 pr-15">
								<div class="btn-group  pull-right">
									<asp:LinkButton  ID="lnkSearchButton" runat="server" class="btn btn-default" Text="" OnClick="SearchButton_Click">
										<i class="fa fa-search"></i>
										البحث									
									</asp:LinkButton>
								</div>
							</div>
                        </div>
                        <!--End Row-->
                    </div>
                  </div>
                <div class="portlet-body pt-15">
                    <table class="table table-striped table-bordered table-hover" id="sample_11">
                        <thead>
                            <tr>
                            <th class="text-center">
                                <asp:Literal id="Literal5" runat="server" Text="<%$Resources:MOEHE.PSPES,Grade%>"></asp:Literal>
                            </th>
                            <th class="text-center">
                                <asp:Literal id="Literal6" runat="server" Text="<%$Resources:MOEHE.PSPES,DocumentName%>"></asp:Literal>
                            </th>
                            <th class="text-center">
                                <asp:CheckBox ID="PSORequiredCheckBox" CssClass="checktable" AutoPostBack="true" Text=""  OnCheckedChanged="PSORequiredCheckBox_CheckedChanged"  runat="server"  />
                                    
                                    <label for="FiltreCurriculum" class="Texttable">
                                        <asp:Literal id="Literal1" runat="server" Text="<%$Resources:MOEHE.PSPES,PSORequired%>"></asp:Literal>
                                </label>
                            </th>
                            <th class="text-center">
                                <asp:CheckBox ID="SchoolRequiredCheckBox" AutoPostBack="true" CssClass="checktable"  Text=""  OnCheckedChanged="SchoolRequiredCheckBox_CheckedChanged" runat="server"  />
                                <label for="FiltreCurriculum" class="Texttable">
                                        <asp:Literal id="Literal2" runat="server" Text="<%$Resources:MOEHE.PSPES,SchoolRequired%>"></asp:Literal>
                                </label>
                            </th>
                            </tr>
                        </thead>
                        <tbody>
                            
                            <asp:Repeater ID="SupportingDocsRepeater" runat="server">

                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>

                                    <tr>
                                        
                                
                                            
                                        <td>
                                                <asp:TextBox ID="TermNameTextBox" Enabled="false"   Text='<%# Eval("Term") %>' Visible="false" runat="server"></asp:TextBox>
                                            
                                            <asp:TextBox ID="SchoolCode" runat="server" Text='<%# Eval("SchoolCode") %>' Visible="false" Enabled="false" ></asp:TextBox>

                                            <asp:TextBox ID="SchoolNameTextBoxGrid" runat="server" Text='<%# Eval("SchoolName") %>'  Visible="false" Enabled="false"></asp:TextBox>
                                            <asp:TextBox ID="EnglishSchoolNameTextBox" runat="server" Text='<%# Eval("ArabicSchoolName") %>'  Visible="false"   Enabled="false"></asp:TextBox>
                                <asp:TextBox ID="GradeTextBox" runat="server" Text='<%# Eval("Grade") %>' Visible="true"  Enabled="false"></asp:TextBox>


                                                        <asp:TextBox ID="CurriculumTextBox" Text='<%# Eval("Curriculum") %>' runat="server" Visible="false"  Enabled="false"></asp:TextBox>
                                                        <asp:TextBox ID="ArabicCurriculumTextBox" Text='<%# Eval("ArabicCurriculum") %>' runat="server" Visible="false"  Enabled="false"></asp:TextBox>
                                            
                                                        
                                                        <asp:HiddenField ID="CurriculumIDHiddenField" Value='<%# Eval("CurriculumID") %>' runat="server" />
                                                        <asp:TextBox ID="CurriculumIDTextBox" Text='<%# Eval("CurriculumID") %>' runat="server" Visible="false" Enabled="false"></asp:TextBox>
                                                        </td>
                                            
                                        <td>
                                            <asp:TextBox ID="ArabicDocumentTypeTextBox" runat="server" Visible='<%# Eval("ShowArabic") %>' Text='<%# Eval("ArabicDocumentType") %>'  Enabled="false"></asp:TextBox>
                                            <asp:TextBox ID="EnglishDocumentTypeTextBox" runat="server"  Visible='<%# Eval("ShowEnglish") %>' Text='<%# Eval("EnglishDocumentType") %>'  Enabled="false"></asp:TextBox>
                                            <asp:TextBox ID="DocumentTypeIDTextBox" runat="server"  Visible="false" Text='<%# Eval("DocumentTypeID") %>'  Enabled="false"></asp:TextBox>

                                            <asp:HiddenField ID="DocumentTypeIDHiddenField" Value='<%# Eval("DocumentTypeID") %>' runat="server" />
                                        </td>
                                        <td class="center ">
                                            <asp:CheckBox ID="PSORequiredRepeaterCheckBox" OnCheckedChanged="PSORequiredRepeaterCheckBox_CheckedChanged" runat="server" Checked='<%# Eval("IsRequiredForPSO") %>'  Enabled='<%# Eval("EnableMinistryRequired") %>' />
                                            
                                        </td>
                                            <td class="center ">
                                            <asp:CheckBox ID="SchoolRequiredRepeaterCheckBox"  OnCheckedChanged="SchoolRequiredRepeaterCheckBox_CheckedChanged" runat="server" Checked='<%# Eval("IsRequiredForSchool") %>' Enabled='<%# Eval("EnableSchoolRequired") %>' />
                                            
                                        </td>
                                    </tr>

                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                            </asp:Repeater>
                            
                        </tbody>
                    </table>
					<div class="col-md-12">
						<div class="btn-group pull-right">
							<asp:LinkButton ID="lnkSaveButton"  class="btn btn-default"  Text="<%$Resources:MOEHE.PSPES,Save%>" OnClick="SaveButton_Click" runat="server"></asp:LinkButton>
						</div>
					</div>
                </div>
            </div>
            <!-- END EXAMPLE TABLE PORTLET-->
          </div>
        </div>
    </section>


	<%--<script src="/_layouts/15/MOEHE.PSPES/assets/js/jquery.min.js" type="text/javascript"></script>
		<script src="/_layouts/15/MOEHE.PSPES/assets/js/chosen.jquery.js" type="text/javascript"></script>--%>
		<script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>



