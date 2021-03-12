<%@ Assembly Name="MOEHE.PSPES, Version=1.0.0.0, Culture=neutral, PublicKeyToken=677b80a9c9c0da8c" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchApplicationsUserControl.ascx.cs" Inherits="MOEHE.PSPES.Webparts.Search.SearchApplications.SearchApplicationsUserControl" %>

 
 <link href="/_layouts/15/MOEHE.PSPES/assets/css/chosen.css" rel="stylesheet"  type="text/css" />

<section class="form-horizontal">
    <div class="container mt-30 mb-30 pt-0 pb-0 bg-white-theme">
		<div class="row">
        <!-- BEGIN EXAMPLE TABLE PORTLET-->
			<div class="portlet light">
				<div class="col-md-12 borde-bottom pb-15">
					<h3 class="caption p-0">
					<asp:Literal id="AdvancedSearchLiteral" runat="server" Text="<%$Resources:MOEHE.PSPES,AdvancedSearch%>"></asp:Literal>
					</h3>
					<div class="col-md-12">
						<div class="row">
						   <!--QID-->
							<div class="col-sm-4">
								<div class="form-group">   
									<label for="FiltreTerm">
									  <asp:Literal id="Literal1" runat="server" Text="<%$Resources:MOEHE.PSPES,QatariID%>"></asp:Literal>
									</label>
									<asp:TextBox ID="QIDTextBox" class="form-control"    runat="server"></asp:TextBox>
									<asp:RegularExpressionValidator ID="REGEXQID" CssClass="text-danger"  ForeColor="Red" runat="server" ValidationExpression="[0-9]{11}$" ControlToValidate="QIDTextBox" Display="Static" ErrorMessage='<%$Resources:MOEHE.PSPES,NotValidQID%>'></asp:RegularExpressionValidator>
								</div>
							</div>
							<!--ApplicationRefNumberTextBox-->
							<div class="col-sm-4">
								<div class="form-group">
									<label for="FiltreTerm">
									  <asp:Literal id="TermNameLiteral" runat="server" Text="<%$Resources:MOEHE.PSPES,ApplicationReferenceNo%>"></asp:Literal>
									</label>
								<asp:TextBox ID="ApplicationRefNumberTextBox" class="form-control" runat="server"></asp:TextBox>
							  </div>
							</div> 
							<!--Code-->
							<div class="col-sm-4"> 
								<label for="Code">
									<asp:Literal id="SchoolCodeLiteral" runat="server" Text="<%$Resources:MOEHE.PSPES,SchoolCode%>"></asp:Literal>
								</label>
								<asp:DropDownList class="chzn-select" ID="SchoolCodesDropDownList" AutoPostBack="true" OnSelectedIndexChanged="SchoolCodesDropDownList_SelectedIndexChanged" runat="server"></asp:DropDownList>
								<asp:RequiredFieldValidator ID="RVSchool" ForeColor="Red"  CssClass="text-danger" ControlToValidate="SchoolCodesDropDownList" InitialValue="All" Display="Static"  runat="server" ErrorMessage="<%$Resources:MOEHE.PSPES,RVSchool%>"></asp:RequiredFieldValidator>
							</div>
						</div>
					  <!--End Row--> 
					</div>
					<div class="col-md-12">
						<div class="row">
							<!--ApplicationDate-->
							<div class="col-sm-3">
								<div class="form-group">
									<label for="ApplicationDate"><asp:Literal id="Literal49" runat="server" Text="<%$Resources:MOEHE.PSPES,ApplicationDate%>"></asp:Literal> </label>
									<div id="ApplicationDate">
										<!-- Datepicker as text field -->
										<div class="input-group date" data-date-format="dd/mm/yyyy">
											<input type="text" class="form-control" name="PaidFeesDateTime" placeholder="dd/mm/yyyy">
											<div class="input-group-addon">
												<span class="glyphicon glyphicon-calendar"></span>
											</div>
										</div>
									</div>
								</div>
							</div>
							<script>
                                $(function () {
                                    $('.input-group.date').datepicker({
                                        format: "dd/mm/yyyy"
                                    });
                                });
							</script>
							<!--Term-->
							<div class="col-sm-3">
								<div class="form-group">
									<label for="Term">
										<asp:Literal id="Literal3" runat="server" Text="<%$Resources:MOEHE.PSPES,TermName%>"></asp:Literal>
									</label>
									<asp:DropDownList  class=" form-control" ID="TermDropDownList"  runat="server"></asp:DropDownList>
								</div>
							</div>
							<!--Grade-->
							<div class="col-sm-3">
								<div class="form-group">
									<label for="Grade">
										<asp:Literal id="Literal8" runat="server" Text="<%$Resources:MOEHE.PSPES,Grade%>"></asp:Literal>
									</label>
									<asp:DropDownList  class="form-control" ID="SchoolGradesDropDownList" runat="server"></asp:DropDownList>
									<asp:RequiredFieldValidator ID="RVGrades" ForeColor="Red" CssClass="text-danger" ControlToValidate="SchoolGradesDropDownList" InitialValue="All" Display="Static"  runat="server" ErrorMessage="<%$Resources:MOEHE.PSPES,RVSchool%>"></asp:RequiredFieldValidator>
								</div>
							</div>
							<!--AppliactionStatus-->
							<div class="col-sm-3">
								<div class="form-group">   
									<label for="FiltreTerm">
										<asp:Literal id="Literal4" runat="server" Text="<%$Resources:MOEHE.PSPES,ApplicationStatus%>"></asp:Literal>
									</label>
									<asp:DropDownList  class="form-control" ID="ApplicationStatusDropDownList" runat="server"></asp:DropDownList>
								</div>
							</div>
							<div class="col-md-12">
								<div class="pull-right">
									<asp:LinkButton  ID="SearchLinkButton" runat="server" class="btn btn-default mt-10" Text="" OnClick="lnkSearchButton_Click">
										<i class="fa fa-search"></i>
										البحث
									</asp:LinkButton>
								</div>
							</div>
								
						</div>
					</div>
					<!--End 12--> 
				</div>
				<!--Start Table--> 
				<div class="portlet-body pt-15">
					<div class="table-responsive">
						<table class="table table-striped table-bordered table-hover" id="sample_21">
							<thead>
								<tr>
									<th class="text-center"> 
										<asp:Literal runat="server" ID="Literal54" Text='<%$Resources:MOEHE.PSPES,ApplicationRefNo%>'></asp:Literal>
									</th>
									<th class="text-center"> <asp:Literal runat="server" ID="Literal55" Text='<%$Resources:MOEHE.PSPES,ApplicationDate%>'></asp:Literal> </th>
									<th class="text-center"> <asp:Literal runat="server" ID="Literal56" Text='<%$Resources:MOEHE.PSPES,QID%>'></asp:Literal> </th>
									<th class="text-center">  <asp:Literal runat="server" ID="Literal57" Text='<%$Resources:MOEHE.PSPES,StudentName%>'></asp:Literal>  </th>
									<th class="text-center"> <asp:Literal runat="server" ID="Literal58" Text='<%$Resources:MOEHE.PSPES,Grade%>'></asp:Literal> </th>
									<th class="text-center"><asp:Literal runat="server" ID="Literal59" Text='<%$Resources:MOEHE.PSPES,SchoolName%>'></asp:Literal> </th>
									<th class="text-center"><asp:Literal runat="server" ID="Literal5" Text='<%$Resources:MOEHE.PSPES,WaitListNumber%>'></asp:Literal> </th>
									<th class="text-center "><asp:Literal runat="server" ID="Literal60" Text='<%$Resources:MOEHE.PSPES,EditApplicationTitle%>'></asp:Literal> </th>
									<th class="text-center"><asp:Literal runat="server" ID="Literal61" Text='<%$Resources:MOEHE.PSPES,CompleteApplicationTitle%>'></asp:Literal> </th>
								</tr>
							</thead>
							<tbody>
								<asp:Repeater ID="ApplicationsDataRepeater" runat="server" OnDataBinding="ApplicationsDataRepeater_DataBinding">
									<HeaderTemplate></HeaderTemplate>
										<ItemTemplate>
										  <tr>
											<td>
											  <asp:Label ID="ApplicationRefNoLabel" Enabled="false"   Text='<%# Eval("ApplicationRefNo") %>' Visible="true" runat="server"></asp:Label>
											</td>
											<td>
											  <asp:Label ID="ApplicationDateLabel" Enabled="false"   Text='<%# Eval("ApplicationDate") %>' Visible="true" runat="server"></asp:Label>
											</td>
											<td class="center ">
											  <asp:Label ID="QIDLabel" Enabled="false"   Text='<%# Eval("QID") %>' Visible="true" runat="server"></asp:Label>
											</td>
											<td class="center ">
											  <asp:Label ID="StudentNameLabel" Enabled="false"   Text='<%# Eval("StudentName") %>' Visible="true" runat="server"></asp:Label>
											</td>
											<td class="center ">
											  <asp:Label ID="CurriculumLabel" Enabled="false"   Text='<%# Eval("Grade") %>' Visible="true" runat="server"></asp:Label>
											</td>
											<td class="center ">
											  <asp:Label ID="SchoolNameLabel" Enabled="false"   Text='<%# Eval("SchoolName") %>' Visible="true" runat="server"></asp:Label>
											</td>
												<td class="center ">
											  <asp:Label ID="wWitListNumberLabel" Enabled="false"   Text='<%# Eval("MOE_WAITLIST_NUMBER") %>' Visible="true" runat="server"></asp:Label>
											</td>
											<td class="center ">
											  <asp:LinkButton ID="EditApplicationLinkButton" class="btn btn-default" runat="server" OnClick="EditApplicationLinkButton_Click"  Text='<%$Resources:MOEHE.PSPES,EditApplication%>'></asp:LinkButton>
											</td>
											<td class="center ">
											  <asp:LinkButton ID="CompleteApplicationLinkButton" class="btn btn-default" runat="server" OnClick="CompleteApplicationLinkButton_Click" Text='<%$Resources:MOEHE.PSPES,CompleteApplication%>'></asp:LinkButton>
											</td>
										  </tr>
										</ItemTemplate>
									<FooterTemplate></FooterTemplate>
								</asp:Repeater>
							</tbody>
						</table>
					</div>
				</div>
			</div>
				<!--End Table--> 
			</div>
			<!-- END EXAMPLE TABLE PORTLET-->
		</div>
</section>
<%--	<script src="/_layouts/15/MOEHE.PSPES/assets/js/jquery.min.js" type="text/javascript"></script>--%>
<script type="text/javascript"> 

    $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

</script>
