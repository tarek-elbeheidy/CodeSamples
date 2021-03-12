<%@ Assembly Name="MOEHE.PSPES, Version=1.0.0.0, Culture=neutral, PublicKeyToken=677b80a9c9c0da8c" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SeatCapacityAllocateUserControl.ascx.cs" Inherits="MOEHE.PSPES.Webparts.SeatCapacityAllocate.SeatCapacityAllocateUserControl" %>
<%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>--%>

 <link href="/_layouts/15/MOEHE.PSPES/assets/css/chosen.css" rel="stylesheet"  type="text/css" />

<section class="form-horizontal">
	<div class="container mt-30 mb-30 pt-0 pb-0 bg-white-theme">
	    <div class="row">
            <!-- BEGIN EXAMPLE TABLE PORTLET-->
            <div class="portlet light">
				<asp:Panel ID="Pnl" runat="server">
                <div class="col-md-12 borde-bottom">
                    <h3 class="caption p-0"> <asp:Literal id="Literal1" runat="server" Text="<%$Resources:MOEHE.PSPES,SchoolDetails%>"></asp:Literal></h3>
						<div class="row">
							<div class="col-md-3 hidden">
								<div class="form-group">
									<label for="EmpType">
										<asp:Literal ID="Literal41" runat="server" Text="<%$Resources:MOEHE.PSPES,empTP%>"></asp:Literal>
									</label>
									<asp:DropDownList class="form-control" ID="utype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="utype_SelectedIndexChanged">
									   <%-- <asp:ListItem Value="0">-Select-</asp:ListItem>
										<asp:ListItem Value="1">Ministry User</asp:ListItem>
										<asp:ListItem Value="2">School User</asp:ListItem>--%>
									</asp:DropDownList>
								</div>
							</div>
							<div class="col-md-3">
								<div class="form-group">
									 <asp:Panel ID="PanelSclCodeDDL" runat="server"> 
										 <label for="SchCD">
									 <asp:Literal ID="Literal40" runat="server" Text="<%$Resources:MOEHE.PSPES,PleaseschooseSCcode%>"></asp:Literal></label>
									<asp:DropDownList ID="DdlSclCode" class="chzn-select form-control" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="DdlSclCode_SelectedIndexChanged"></asp:DropDownList>  
								</asp:Panel>
								</div>
							</div>
							<!--FiltreTerm-->
							 <%-- 
							<div class="col-md-2">
							    <div class="form-group">
									<label for="FiltreTerm"><asp:Literal id="Literal20" runat="server" Text="<%$Resources:MOEHE.PSPES,NextClass%>"></asp:Literal></label>
							    </div>
							 </div>
							 --%>
							<div class="col-md-3">
								<div class="form-group">
									<label for="FiltreTerm"><asp:Literal id="Literal2" runat="server" Text="<%$Resources:MOEHE.PSPES,NextClass%>"></asp:Literal></label>
									<asp:TextBox ID="nxtTermTxt" runat="server" class="aspNetDisabled form-control" disabled="disabled"></asp:TextBox>
								</div>
							</div>
							<%--
							<div class="col-md-6">
								<!--FiltreCurriculum-->
								<div class="form-group">
									<label for="FiltreCurriculum">المنهج</label>
									<select class="form-control" id="FiltreCurriculum">
										<option> منهج وزارة التعليم و التعليم العالي</option>
										<option>منهج 1</option>
										<option>منهج 2 </option>
									</select>
								</div>
							</div>
							--%>
							<!--FiltreSchoolCode-->
							<div class="col-md-3">
								<div class="form-group">
									<label for="FiltreSchoolCode"><asp:Literal id="Literal4" runat="server" Text="<%$Resources:MOEHE.PSPES,SchoolCode1%>"></asp:Literal></label>
									<asp:TextBox ID="sclCodeTxt" runat="server" class="aspNetDisabled form-control" disabled="disabled"></asp:TextBox>
								</div>
							</div>
							<!--FiltreSchool-->
							<div class="col-md-3">
								<div class="form-group">
									<label for="FiltreSchool"><asp:Literal id="Literal5" runat="server" Text="<%$Resources:MOEHE.PSPES,SchoolName1%>"></asp:Literal></label>
									<asp:TextBox ID="scNameTxt" runat="server" class="aspNetDisabled form-control" disabled="disabled"></asp:TextBox>
								</div>
							</div>
						</div>
						<div class="row">
							<!--LastUpdateByUser-->
							<div class="col-md-6" runat="server" id="dvLastModifiedUser">
								<div class="form-group">
									<label for="LastUpdateByUser"><asp:Literal id="Literal3" runat="server" Text="<%$Resources:MOEHE.PSPES,LastmodifiedUser%>"></asp:Literal></label>
									<asp:TextBox ID="LstUpDTUsrTxt" runat="server" class="aspNetDisabled form-control" disabled="disabled"></asp:TextBox>
								</div>
							</div>
							
							<!--LastUpdateByUser-->
							<div class="col-md-6" runat="server" id="dvLastModifiedDate">
								<div class="form-group">
									<label for="LastUpdateByUser"><asp:Literal id="Literal6" runat="server" Text="<%$Resources:MOEHE.PSPES,Lastmodifieddate%>"></asp:Literal> </label>
									<asp:TextBox ID="lstMdDtTxt" runat="server" class="aspNetDisabled form-control" disabled="disabled"></asp:TextBox>
								</div>
							</div>
						</div>
				</div>
                <div class="col-md-12 borde-bottom pb-20 pt-10">
					<div class="row">
						<div class="col-md-4 borde-left">
							<h3 class="caption"><asp:Literal id="Literal19" runat="server" Text="<%$Resources:MOEHE.PSPES,CurrentCapStatus%>"></asp:Literal></h3>
							<!--CurrentYearSchoolBuildingCapacity-->
							<div class="form-group">
								<label class="control-label col-md-9" for="CurrentYearSchoolBuildingCapacity"><asp:Literal id="Literal7" runat="server" Text="<%$Resources:MOEHE.PSPES,CurrentYearSchoolbuildingCapacity%>"></asp:Literal> </label>
								<div class="col-md-3">
									<asp:TextBox ID="currYearCaptxt" runat="server" class="aspNetDisabled form-control" disabled="disabled"></asp:TextBox>
									
								</div>
							</div>
								<!--CurrentEnrolledStundents-->
							<div class="form-group">
								<label class="control-label col-md-9" for="CurrentEnrolledStundents"><asp:Literal id="Literal8" runat="server" Text="<%$Resources:MOEHE.PSPES,CurrentEnrolledStudents%>"></asp:Literal></label>
								<div class="col-md-3">
									<asp:TextBox ID="currEnrolledtxt" runat="server" class="aspNetDisabled form-control" disabled="disabled"></asp:TextBox>
								   
								</div>
							</div>
							<!--OverCapacitySeatsEnrolledBuildingCapacity-->
							<%--  <div class="form-group">
							 <label class="control-label col-md-9" for="OverCapacitySeatsEnrolledBuildingCapacity">المقاعد الزائدة عن طاقة استيعاب المبنى</label>
							<div class="col-md-3">
							<input type="number" class="form-control" id="OverCapacitySeatsEnrolledBuildingCapacity" disabled>
							</div>
							</div>--%>
							<!--AvailableSeats-->
							<div class="form-group">
								<label class="control-label col-md-9" for="AvailableSeats"><asp:Literal id="Literal9" runat="server" Text="<%$Resources:MOEHE.PSPES,Availableseats%>"></asp:Literal></label>
								<div class="col-md-3">
									<asp:TextBox ID="AvailSeatstxt" runat="server" class="aspNetDisabled form-control" disabled="disabled"></asp:TextBox>
								</div>
							</div>
							<!--LastGradePossingOutSeats-->
							<div  hidden class="form-group" >
								<label class="control-label col-md-9" for="LastGradePossingOutSeats"><asp:Literal id="Literal10" runat="server" Text="<%$Resources:MOEHE.PSPES,LastGradePassing%>"></asp:Literal></label>
								<div class="col-md-3">
							   
									<asp:TextBox ID="lastGrdSeatstxt" runat="server" class="aspNetDisabled form-control" disabled="disabled"></asp:TextBox>
								</div>
							</div>
							<asp:Panel ID="Pnl_AddedGrades" runat="server">
								<div   class="form-group" >
									<label class="control-label col-md-9" for="anynewgrades"><asp:Literal id="Literal20" runat="server" Text="<%$Resources:MOEHE.PSPES,AreThereGradesApproved%>"></asp:Literal></label>
									<div class="col-md-3">
									  <asp:DropDownList ID="DDLnewGrades"  Enabled ="false" runat ="server" AutoPostBack ="True" class="form-control"  OnSelectedIndexChanged="DDLnewGrades_SelectedIndexChanged1" ></asp:DropDownList>
									  
									</div>
								</div>
								<div   class="form-group" >
									<label class="control-label col-md-9" for="howManyGrades"><asp:Literal id="Literal24" runat="server" Text="<%$Resources:MOEHE.PSPES,HowmanyGrades%>"></asp:Literal></label>
									<div class="col-md-3">
										<asp:DropDownList ID="DDLNumbrOfGrades" runat="server" Enabled="false"  class="form-control" OnSelectedIndexChanged="DDLNumbrOfGrades_SelectedIndexChanged" >
											<asp:ListItem Text='<%$Resources:MOEHE.PSPES,PleaseSelect%>' Value="select" Selected="true"></asp:ListItem>
											<asp:ListItem Text="1" Value="1" ></asp:ListItem>
											<asp:ListItem Text="2" Value="2" ></asp:ListItem>
											<asp:ListItem Text="3" Value="3" ></asp:ListItem>
											<asp:ListItem Text="4" Value="4" ></asp:ListItem>
											<asp:ListItem Text="5" Value="5" ></asp:ListItem>
											<asp:ListItem Text="6" Value="6" ></asp:ListItem>
										</asp:DropDownList>
									</div>
									<div class="col-md-3 mt-20 pl-0 pull-right">
										<%--<h4><asp:Literal id="Literal11" runat="server" Text="<%$Resources:MOEHE.PSPES,OverCapacitynotice%>"></asp:Literal></h4>--%>
										<%--  <ul class="list theme-colored2 paper">
										<li></li>
										</ul>--%>
										<asp:LinkButton ID="btn_AddGrades" onclientclick="return ConfirmGrades()" runat="server" Text="<%$Resources:MOEHE.PSPES,AddGrades%>" Enabled="false"  OnClick="btn_AddGrades_Click" CssClass="btn  btn-default pull-right"></asp:LinkButton> 
									</div>
								</div>
							</asp:Panel>
						</div>
						<div class="col-md-4 borde-left">
							<h3 class="caption"><asp:Literal id="Literal12" runat="server" Text="<%$Resources:MOEHE.PSPES,Preenrollmentseatstatus%>"></asp:Literal></h3 class="caption">
							<!--SchoolCapacityforNextYear-->
							<div class="form-group">
								<label class="control-label col-md-9" for="SchoolCapacityforNextYear">
									<asp:Literal id="Literal16" runat="server" Text="<%$Resources:MOEHE.PSPES,SchoolCapacitynextYr%>"></asp:Literal>
								</label>
								<div class="col-md-3">
									<asp:TextBox ID="scCapNxtYrTxt" runat="server"  class="aspNetDisabled form-control" disabled="disabled"></asp:TextBox>
								</div> 
							</div>
							<!--PreEnrollmentSeatsforNextYear-->
							<div class="form-group">
								<label class="control-label col-md-9" for="PreEnrollmentSeatsforNextYear"><asp:Literal id="Literal17" runat="server" Text="<%$Resources:MOEHE.PSPES,PreenrollmentnextYr%>"></asp:Literal></label>
								<div class="col-md-3">
									 <asp:TextBox ID="PreenrollnxtYrTxt" runat="server" class="aspNetDisabled form-control" disabled="disabled"/>
								</div>
							</div>
							<%--<div class="col-md-12">
								<h4><asp:Literal id="Literal21" runat="server" Text="<%$Resources:MOEHE.PSPES,NOTEPreenroll1%>"></asp:Literal></h4>
								<ol class=" theme-colored2 paper m-20 mb-0 mt-0">
									<li><asp:Literal id="Literal22" runat="server" Text="<%$Resources:MOEHE.PSPES,NOTEPreenroll2%>"></asp:Literal></li>
									<li><asp:Literal id="Literal23" runat="server" Text="<%$Resources:MOEHE.PSPES,NOTEPreenroll3%>"></asp:Literal></li>
									<li><asp:Literal id="Literal25" runat="server" Text="<%$Resources:MOEHE.PSPES,NOTEPreenroll4%>"></asp:Literal></li>
								</ol>
							</div>--%>
						</div>
						<div class="col-md-4">	
							<div class="" runat="server" id="fieldsetSeatCapacityPercentage">
								<h3 class="caption" class="scheduler-border mb-5"><asp:Literal id="Literal13" runat="server" Text="<%$Resources:MOEHE.PSPES,SeatsPercentage%>"></asp:Literal></h3 class="caption">			
									<!--IncreaseSchoolSeatcapacityforNextYear%-->
									<div class="form-group">
										<label class="control-label col-md-9" for="IncreaseSchoolSeatcapacityforNextYear%"> <asp:Literal id="Literal14" runat="server" Text="<%$Resources:MOEHE.PSPES,SeatsPercentage%>"></asp:Literal> </label>
										<div class="col-md-3">
										  <asp:TextBox ID="percentTxt" runat="server" class="aspNetDisabled form-control" disabled="disabled"></asp:TextBox>
										</div>
									</div>
									<%-- <div class="col-md-3 col-md-offset-9 text-center">أو</div>--%>
									<!--IncreaseSchoolSeatcapacityforNextYear-->
									<div class="form-group">
										<label class="control-label col-md-9" for="IncreaseSchoolSeatcapacityforNextYear"> <asp:Literal id="Literal15" runat="server" Text="<%$Resources:MOEHE.PSPES,increaseseatcap%>"></asp:Literal> </label>
										<div class="col-md-3">
											<asp:TextBox ID="CapNumTxt" CssClass="aspNetDisabled form-control" runat="server"  /> <%-- OnTextChanged="CapNumTxt_TextChanged"  --%>
											<asp:TextBox ID="HiddenCapNumTxt" class="form-control" runat="server" Visible ="false"  />
										</div>
									</div>
									<div class="col-md-3 mt-20 pl-0 pull-right">
										<%--<h4><asp:Literal id="Literal11" runat="server" Text="<%$Resources:MOEHE.PSPES,OverCapacitynotice%>"></asp:Literal></h4>--%>
										<%--  <ul class="list theme-colored2 paper">
										<li></li>
										</ul>--%>
										<asp:LinkButton ID="btn_AddSeatCapacity" Enabled="false" runat="server" Text="<%$Resources:MOEHE.PSPES,Add%>" onclientclick="return ConfirmSeats()" OnClick="btn_AddSeatCapacity_Click" CssClass="btn  btn-default pull-right"></asp:LinkButton> <%--<asp:Literal id="Literal21" runat="server" Text="<%$Resources:MOEHE.PSPES,AddGrades%>"></asp:Literal>--%>
									</div>
							</div>
						</div>
					</div>
				</div>
				<div class=col-md-12>
					<div class="portlet-body">
						<h3 class="caption caption mb-30">
							<asp:Literal id="Literal18" runat="server" Text="<%$Resources:MOEHE.PSPES,ViewPrepareCap%>"></asp:Literal>
						</h3>
						<asp:GridView ID="GridView1" CssClass="table table-striped table-bordered table-hover" runat="server" AutoGenerateColumns="false" ShowFooter="true">
							<Columns>
								<%-- <asp:BoundField ReadOnly="True" HeaderText="Grade level"  
									DataField="Grade"  >
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundField>--%>
								<asp:TemplateField >
									<HeaderTemplate><asp:Literal id="Literal31" runat="server" Text="<%$Resources:MOEHE.PSPES,GradeLevel%>"></asp:Literal></HeaderTemplate>                                                                
									<ItemTemplate >
								<asp:DropDownList ID="DDLGrades" runat="server" Enabled ="False" AutoPostBack =" true" OnSelectedIndexChanged ="DDLGrades_SelectedIndexChanged" >
								   <asp:ListItem Text='<%$Resources:MOEHE.PSPES,PleaseSelect%>' Value="0" Selected="true"></asp:ListItem>
								   <asp:ListItem Text="PK" Value="PK" ></asp:ListItem>
								   <asp:ListItem Text="KG" Value="KG" ></asp:ListItem>
								   <asp:ListItem Text="N" Value="N" ></asp:ListItem>
								   <asp:ListItem Text="01" Value="01" ></asp:ListItem>
								   <asp:ListItem Text="02" Value="02" ></asp:ListItem>
								   <asp:ListItem Text="03" Value="03" ></asp:ListItem>
								   <asp:ListItem Text="04" Value="04" ></asp:ListItem>
								   <asp:ListItem Text="05" Value="05" ></asp:ListItem>
								   <asp:ListItem Text="06" Value="06" ></asp:ListItem>
								   <asp:ListItem Text="07" Value="07" ></asp:ListItem>
								   <asp:ListItem Text="08" Value="08" ></asp:ListItem>
								   <asp:ListItem Text="09" Value="09" ></asp:ListItem>
								   <asp:ListItem Text="10" Value="10"></asp:ListItem>
								   <asp:ListItem Text="11" Value="11" ></asp:ListItem>
								   <asp:ListItem Text="12" Value="12" ></asp:ListItem>
								   <asp:ListItem Text="13" Value="13"></asp:ListItem>
								</asp:DropDownList>
								<asp:Label ID="GrdLevelLBL" Text='<%# Eval("Grade") %>' runat="server" CssClass="hidden"   ></asp:Label>
								</ItemTemplate></asp:TemplateField>
								<%--<asp:BoundField ReadOnly="True" HeaderText="Current enrolled capacity" 
								DataField="CurrentEnrollments" >
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundField>--%>
								 <asp:TemplateField>
								   <HeaderTemplate><asp:Literal id="Literal32" runat="server" Text="<%$Resources:MOEHE.PSPES,CurrEnrollment%>"></asp:Literal></HeaderTemplate>
									 <ItemTemplate >
									   <asp:Label ID="CrEnCLBL" Text='<%# Eval("CurrentEnrollments") %>'  runat="server"   ></asp:Label> 
									</ItemTemplate>
									<FooterTemplate><asp:Label ID="CurrentEnrollmentsTotal" Text=''  runat="server"   ></asp:Label> </FooterTemplate>
								</asp:TemplateField>
								<%--<asp:BoundField ReadOnly="True" HeaderText="Current grade capacity" DataField="CurrentCapacity">
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundField>--%>
								<asp:TemplateField  Visible ="False" >
									<HeaderTemplate><asp:Literal id="Literal33" runat="server" Text="<%$Resources:MOEHE.PSPES,CurrGradeCap%>"></asp:Literal></HeaderTemplate> <ItemTemplate >
									<asp:Label ID="CrGrCLBL" Text='<%# Eval("CurrentCapacity") %>' runat="server"   ></asp:Label> 
									</ItemTemplate>
								</asp:TemplateField>
							<%--<asp:BoundField ReadOnly="True" HeaderText="Current year available seats" DataField="CurrentYearAvailableSeats" >
								<ItemStyle HorizontalAlign="Center"></ItemStyle>
							</asp:BoundField>--%>
							<asp:TemplateField  Visible ="False">
								<HeaderTemplate><asp:Literal id="Literal34" runat="server" Text="<%$Resources:MOEHE.PSPES,CurryravailSeats%>"></asp:Literal></HeaderTemplate>        
								<ItemTemplate >
									<asp:Label ID="CrYrAvlLBL" Text='<%# Eval("CurrentYearAvailableSeats") %>' runat="server"   ></asp:Label> 
								</ItemTemplate>
							</asp:TemplateField>

				   
						<%--<asp:BoundField ReadOnly="True" HeaderText="Seats after promotion (Next Year)" 
					  DataField="SeatsafterPromotion" >
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundField>--%>

					<asp:TemplateField  Visible ="False" >
						<HeaderTemplate><asp:Literal id="Literal35" runat="server" Text="<%$Resources:MOEHE.PSPES,Seatsafterprom%>"></asp:Literal></HeaderTemplate> 
					   <ItemTemplate >
						   
						   <asp:Label ID="SeatAfterPrLBL" Text='<%# Eval("SeatsafterPromotion") %>' runat="server"   ></asp:Label> 
					   
						</ItemTemplate></asp:TemplateField>


				   <%-- <asp:BoundField ReadOnly="True" HeaderText="Available Seats after promotion (Next Year)" 
					  DataField="AvailableseatsAfterPromotion" >
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundField>--%>

					 <asp:TemplateField  Visible ="False" >
					   <HeaderTemplate><asp:Literal id="Literal36" runat="server" Text="<%$Resources:MOEHE.PSPES,AvailSeatsafterprom%>"></asp:Literal></HeaderTemplate>
						 <ItemTemplate >
						   
						   <asp:Label ID="AvaiSeatAfterPrLBL" Text='<%# Eval("AvailableseatsAfterPromotion") %>' runat="server"   ></asp:Label> 
					   
						</ItemTemplate></asp:TemplateField>
				
					   
					<asp:TemplateField  >
						<HeaderTemplate><asp:Literal id="Literal37" runat="server" Text="<%$Resources:MOEHE.PSPES,Distribut%>"></asp:Literal></HeaderTemplate>
					   <ItemTemplate >
						   
							<asp:TextBox ID="DistTxt" Text="0" runat="server"  OnTextChanged="DistTxt_TextChanged" AutoPostBack= "true" CssClass="TotalDistr"  ></asp:TextBox>
					   <asp:Label ID="lblHiddenDistNum" runat="server" Text="Label" Visible ="False"></asp:Label>
						</ItemTemplate>

					</asp:TemplateField>

					<asp:TemplateField Visible ="False" >
						 <HeaderTemplate><asp:Literal id="Literal38" runat="server" Text="<%$Resources:MOEHE.PSPES,PreenrollmentSeats%>"></asp:Literal></HeaderTemplate>
					   <ItemTemplate >                       
							<asp:Label ID="PreenLbl" Text="0" runat="server" CssClass="TotalPreEnrollment"  ></asp:Label>                   
						</ItemTemplate>

					</asp:TemplateField>
		   
				
		  <%--<asp:BoundField ReadOnly="True" HeaderText="Preenrollment seats for Grade" 
					  >
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</asp:BoundField>--%>

									   
			</Columns>
			</asp:GridView>
						<%--<asp:Label ID="MsgLbl" runat="server" Text="" Visible ="false" ForeColor="Red"></asp:Label>

						<asp:Button ID="BtnSubmt" runat="server" Text="Submit changes" onclientclick="return Confirm()" OnClick="BtnSubmt_Click"  />--%>        
						<div class="row">
							<div class="col-md-10">
								<%--<asp:Label ID="MsgLbl" runat="server" Text="" Visible ="false" class="text-danger"></asp:Label>--%>
							</div>
							<div class="col-md-2 pull-right">
								
						   
							<asp:LinkButton ID="LinkBtnSubmit" Enabled="false" runat="server" Text="<%$Resources:MOEHE.PSPES,Save%>" onclientclick="return ConfirmSave()" OnClick="LinkBtnSubmit_Click" CssClass="pull-right btn btn-default"></asp:LinkButton> 
								
							</div>
						</div>





						 <script type="text/javascript">
                             function ConfirmGrades() {
                                 var yy = $("#" + "<%=btn_AddGrades.ClientID %>").hasClass("aspNetDisabled");
                                 if (yy)
                                 { return false; }
                                 else {
                                     var cult = '<%= System.Globalization.CultureInfo.CurrentUICulture.LCID %>';

                                     if (cult == "1025") {
                                         alert("هذا الاجراء سيقوم بإضافة صفوف جديدة لهذه المدرسة");
                                     }
                                     else {
                                         alert("This action will add new grades to this school");
                                     }
                                 }

                             }

                             function ConfirmSeats() {
                                 var yy = $("#" +"<%=btn_AddSeatCapacity.ClientID %>").hasClass("aspNetDisabled");
                                 if (yy) {
                                     return false;
                                 }
                                 else {

                                     var cult = '<%= System.Globalization.CultureInfo.CurrentUICulture.LCID %>';

                                     if (cult == "1025") {
                                         alert("هذا الاجراء سيقوم بإضافة مقاعد جديدة لهذه المدرسة");
                                     }
                                     else {
                                         alert("This action will add new seats to this school");
                                     }
                                 }
                             }

                             function ConfirmSave() {
                                 var yy = $("#" + "<%=LinkBtnSubmit.ClientID %>").hasClass("aspNetDisabled");
                                 if (yy)
                                 { return false; }
                                 else {
                                     var cult = '<%= System.Globalization.CultureInfo.CurrentUICulture.LCID %>';

                                     if (cult == "1025") {
                                         alert("هذا الاجراء سيقوم بحفظ التغييرات");
                                     }
                                     else {
                                         alert("This action will save the changes");
                                     }
                                 }
                             }
						 </script>

							<script>
                                $(document).ready(function () {
                                    $(document).keyup(function () {
                                        var colTotalD = 0;
                                        $('.TotalDistr').each(function () {
                                            colTotalD += parseInt($(this).val());
                                        });
                                        $('table tr:last-child td:nth-child(3)').html(colTotalD);
                                    }).keyup();
                                    $(document).keyup(function () {
                                        var colTotalP = 0;
                                        $('.TotalPreEnrollment').each(function () {
                                            colTotalP += parseInt($(this).text());
                                        });
                                        $('table tr:last-child td:nth-child(8)').html(colTotalP);
                                    }).keyup();
                                });

							</script>
					</div>
				</div>
            </asp:Panel>
            </div>
            <!-- END EXAMPLE TABLE PORTLET-->
          </div>
        </div>
      </section>


  
	
		<script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
