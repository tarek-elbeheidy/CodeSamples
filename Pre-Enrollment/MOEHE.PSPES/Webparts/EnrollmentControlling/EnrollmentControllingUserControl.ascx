<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnrollmentControllingUserControl.ascx.cs" Inherits="MOEHE.PSPES.Webparts.EnrollmentControlling.EnrollmentControllingUserControl" %>

<link href="/_layouts/15/MOEHE.PSPES/assets/css/chosen.css" rel="stylesheet"  type="text/css" />
<link href="/_layouts/15/MOEHE.PSPES/assets/css/beautiful-checkbox.css" rel="stylesheet"  type="text/css" />
<section class="form-horizontal">
    <div class="container mt-30 mb-30 pt-0 pb-0 bg-white-theme">
        <div class="row">
				<!-- BEGIN EXAMPLE TABLE PORTLET-->
			<div class="portlet light ">
                <div class="portlet-title hidden">
                    <div class="caption font-red">
                        <i class="fa fa-cogs font-red" aria-hidden="true" style="font-size: 20px"></i>
                        <span class="caption-subject bold uppercase">
                            <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:MOEHE.PSPES,EnrollmentControlling%>"></asp:Literal>
                        </span>
                    </div>
                </div>
				<div class="col-md-12 borde-bottom pb-15">
					<h3 class="caption p-0">
						<asp:Literal ID="AdvancedSearchLiteral" runat="server" Text="<%$Resources:MOEHE.PSPES,School%>"></asp:Literal>
					</h3>
					<div class="col-md-12">
						<div class="row">
							<!--Search-->
							<div class="col-md-4">
								<label for="Code">
									<asp:Literal ID="SchoolCodeLiteral" runat="server" Text="<%$Resources:MOEHE.PSPES,SchoolCode%>"></asp:Literal>
								</label>
								 <asp:DropDownList class="chzn-select form-control pt-0" ID="SchoolCodesDropDownList" AutoPostBack="true" OnSelectedIndexChanged="SchoolCodesDropDownList_SelectedIndexChanged" runat="server"></asp:DropDownList>
								<asp:Label ID="lblItemID" runat="server" Width="0" Height="0" style="color: #ffffff; background: transparent; border: none; height: 0px!important; width: 0px;"></asp:Label>
							</div>
						</div>
						<!--End Row-->
					</div>
				</div>
				<asp:Panel ID="pnlData" runat="server" Visible="false">
					<div class="col-md-12 borde-bottom pb-15">
						<h3 class="caption p-0">
						<asp:Literal ID="Literal4" runat="server" Text="<%$Resources:MOEHE.PSPES,EnrollmentDates%>"></asp:Literal>
						</h3>
						<div class="col-md-12">
							<div class="row">
								<!--EnrollmentDateFrom-->
								<div class="col-md-2">
									<div class="form-group">
										<label for="EnrollmentDateFrom">
											<asp:Literal ID="Literal49" runat="server" Text="<%$Resources:MOEHE.PSPES,EnrollmentDateFrom%>"></asp:Literal>
										</label>
										<div id="EnrollmentDateFrom">
											<!-- Datepicker as text field -->
											<div class="input-group date" data-date-format="dd/mm/yyyy">
												<input type="text" class="form-control" onkeydown="return false;" id="dtEnrollmentDateFrom" name="EnrollmentDateFrom" runat="server" placeholder="dd/mm/yyyy">
												<div class="input-group-addon">
													<span class="glyphicon glyphicon-calendar"></span>
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
								</div>
								<!--EnrollmentDateTo-->
								<div class="col-md-2">
									<div class="form-group">
										<label for="EnrollmentDateTo">
											<asp:Literal ID="Literal1" runat="server" Text="<%$Resources:MOEHE.PSPES,EnrollmentDateTo%>"></asp:Literal>
										</label>
										<div id="EnrollmentDateTo">
											<!-- Datepicker as text field -->
											<div class="input-group date" data-date-format="dd/mm/yyyy">
												<input type="text" class="form-control" onkeydown="return false;" id="dtEnrollmentDateTo" name="EnrollmentDateTo" runat="server" placeholder="dd/mm/yyyy">
												<div class="input-group-addon">
													<span class="glyphicon glyphicon-calendar"></span>
												</div>
											</div>
										</div>
									</div>
								</div>
								<script>
                                    $(function () {
                                        $('.input-group.dateto').datepicker({
                                            format: "dd/mm/yyyy"
                                        });
                                    });
								</script>
							</div>
						</div>
					</div>
					<div class="col-md-12 borde-bottom pb-15">
						<h3 class="caption p-0">
						<asp:Literal ID="Literal3" runat="server" Text="<%$Resources:MOEHE.PSPES,EnrollmentGrades%>"></asp:Literal>
						</h3>
						<div class="col-md-12">
							 <div class="row">
								<div class="col-sm-6">
									<div class="panel-body">
										<asp:RadioButtonList ID="rblGrades" Width="100%"  RepeatColumns = "1"  CssClass="input-list" RepeatLayout="UnorderedList" RepeatDirection="Vertical" AutoPostBack="true" runat="server" OnSelectedIndexChanged="rblGrades_SelectedIndexChanged">
											<asp:ListItem Text="<%$Resources:MOEHE.PSPES,All%>" class="radio" Value="ALL"></asp:ListItem>
											<asp:ListItem Text="<%$Resources:MOEHE.PSPES,SelectGrades%>" class="radio" Value="select"></asp:ListItem>
										</asp:RadioButtonList>
									</div>
								</div>
							</div>
							<div class="col-md-12">
								<div id="dvGrades" runat="server" visible="false">
								<!--Grades-->
									<div class="pure-checkbox">
										<asp:CheckBoxList ID="chkLstGrades"   RepeatDirection="Vertical" RepeatLayout="UnorderedList" CssClass="checkbox col-md-2" runat="server" Width="100%"></asp:CheckBoxList>
									</div>
								</div>
							</div>
						</div>
					</div>
					<div class="col-md-12 borde-bottom pb-15">
						<h3 class="caption p-0">
							<asp:Literal ID="Literal6" runat="server" Text="<%$Resources:MOEHE.PSPES,EnrollmentNationalities%>"></asp:Literal>
						</h3>
						<div class="col-md-12">
							<div class="row">
								<div class="col-sm-6">
									<div class="panel-body">
										<div class="hidden">
											<asp:Literal ID="Literal5" runat="server" Text="<%$Resources:MOEHE.PSPES,Nationalities%>"></asp:Literal>
										</div> 
										<asp:RadioButtonList ID="rblNationalities" Width="100%" RepeatColumns = "1"  CssClass="input-list" RepeatLayout="UnorderedList" RepeatDirection="Vertical" AutoPostBack="true" runat="server" OnSelectedIndexChanged="rblNationalities_SelectedIndexChanged">
											<asp:ListItem Text="<%$Resources:MOEHE.PSPES,All%>" class="radio" Value="ALL"></asp:ListItem>
											<asp:ListItem Text="<%$Resources:MOEHE.PSPES,SelectNationalities%>" class="radio" Value="select"></asp:ListItem>
										</asp:RadioButtonList> 
									</div>
								</div>
							</div>
							<div class="row" id="dvNationalities" runat="server" visible="false">
								<!--Nationalities-->
								<div class="col-md-3">
									<div class="form-group">
										<asp:Literal ID="Literal100" runat="server" Text="<%$Resources:MOEHE.PSPES,Nationalities%>"></asp:Literal>
										<asp:ListBox runat="server"   ID="lstNationalities" SelectionMode="Multiple" Style="width: 270px;"></asp:ListBox>
										<i><small><asp:Literal ID="Literal90" runat="server" Text="<%$Resources:MOEHE.PSPES,UseCTRL%>"></asp:Literal></small></i>
									</div>
								</div>
								<div class="col-md-1 text-center">
									<div class="form-group">
										<asp:LinkButton runat="server"   ID="lnkSelectNationalities" CssClass="btn btn-default1" OnClick="lnkSelectNationalities_Click"><span class="glyphicon glyphicon-chevron-right"></span></asp:LinkButton>
										<div class="w-100"></div>
										<div class="w-100"></div>
										<asp:LinkButton runat="server"  ID="lnkDeSelectNationalities" CssClass="btn btn-default1" OnClick="lnkDeSelectNationalities_Click"><span class="glyphicon glyphicon-chevron-left"></span></asp:LinkButton>
									</div>
								</div>
								<div class="col-md-2">
									<div class="form-group">
										<asp:Literal ID="Literal70" runat="server" Text="<%$Resources:MOEHE.PSPES,SelectedNationalities%>"></asp:Literal>
										<asp:ListBox runat="server"  ID="lstSelectedNationalities" SelectionMode="Multiple" Style="width: 270px;"></asp:ListBox>
										<i><small><asp:Literal ID="Literal80" runat="server"  Text="<%$Resources:MOEHE.PSPES,UseCTRL%>"></asp:Literal></small></i>
									</div>
								</div>
							</div>
						</div>
					</div>
					<div class="col-md-12">
						<div class="row">
							<div class="pull-right pt-15">
								<label for="Grade"></label>
								<asp:LinkButton ID="SaveLinkButton" runat="server" CssClass="btn btn-default " OnClick="SaveLinkButton_Click">
									<i class="fa fa-save mr-10"></i>
									<asp:Literal runat="server" ID="Literal71" Text='<%$Resources:MOEHE.PSPES,Save%>'></asp:Literal>
								</asp:LinkButton>
							</div>
						</div>
					</div>
				</asp:Panel>
				<!--End Panel-->
			</div>
		</div>
	</div>
</section>
<script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
<script>
    $(document).ready(function () {
        $(".input-list li span").wrap("<div class='pure-radiobutton'></div>");
        $(".input-list li").addClass("col-md-6 mt-5 mb-5");
        $(".checkbox li").addClass("col-md-2 mt-5 mb-5");
    });
</script>
