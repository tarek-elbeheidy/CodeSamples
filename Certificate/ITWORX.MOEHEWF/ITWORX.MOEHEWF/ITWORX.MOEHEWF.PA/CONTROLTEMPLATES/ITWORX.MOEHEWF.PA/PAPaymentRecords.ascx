<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PAPaymentRecords.ascx.cs" Inherits="ITWORX.MOEHEWF.PA.CONTROLTEMPLATES.ITWORX.MOEHEWF.PA.PAPaymentRecords" %>

<%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>--%>
<script type="text/javascript">
	function Print(a) {
		var row = $(a).closest("tr").clone(true);
		var printWin = window.open('', '', 'left=0", ",top=0,width=1000,height=600,status=0');
		var table = $("[id*=paymentdata]").clone(true);
		$("tr", table).not($("tr:first-child", table)).remove();
		table.append(row);
		$("tr td:last,tr th:last", table).remove();
		var dv = $("<div />");
		dv.append(table);
		printWin.document.write(dv.html());
		printWin.document.close();
		printWin.focus();
		printWin.print();
		printWin.close();
	}
</script>


<asp:HiddenField ID="hdn_RequestId" runat="server" />


<div class="row no-padding">
	<h2 class="section-title font-weight-500 no-margin text-center margin-top-0 margin-bottom-0">
		<asp:Label ID="lbl_grdPayments" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Payments %>"></asp:Label>
	</h2>
</div>
<div class="row heighlighted-section margin-bottom-50 flex-display flex-wrap" id="paymentdata" runat="server">

	<div class="col-md-3 col-sm-6">
		<div class="data-container">
			<h6 class="font-size-16 margin-bottom-15">
				<asp:Label ID="lbl_ReceiptDate" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ReceiptDate %>" />
			</h6>
			<h5 class="font-size-20">
				<asp:Label ID="lbl_ReceiptDateValue" runat="server" />
			</h5>
		</div>
	</div>

	<div class="col-md-3 col-sm-6">
		<div class="data-container">
			<h6 class="font-size-16 margin-bottom-15">
				<asp:Label ID="lbl_ReceiptNumber" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, ReceiptNumber %>" />
			</h6>
			<h5 class="font-size-20">
				<asp:Label ID="lbl_ReceiptNumberValue" runat="server" />
			</h5>
		</div>
	</div>

	<div class="col-md-3 col-sm-6">
		<div class="data-container">
			<h6 class="font-size-16 margin-bottom-15">

				<asp:Label ID="lbl_Statement" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Statement %>" />
			</h6>
			<h5 class="font-size-20">
				<asp:Label ID="lbl_StatementValue" runat="server" />

			</h5>
		</div>
	</div>

	<div id="div_CardType" runat="server"  class="col-md-3 col-sm-6">
		<div class="data-container">
			<h6 class="font-size-16 margin-bottom-15">
				<asp:Label ID="lbl_CardType" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CardType %>" />
			</h6>
			<h5 class="font-size-20">
				<asp:Label ID="lbl_CardTypeValue" runat="server" />

			</h5>
		</div>
	</div>

	<div id="div_CardNumber" runat="server" class="col-md-3 col-sm-6">
		<div class="data-container">
			<h6 class="font-size-16 margin-bottom-15">
				<asp:Label ID="lbl_CardNumber" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, CardNumber %>" />
			</h6>
			<h5 class="font-size-20">
				<asp:Label ID="lbl_CardNumberValue" runat="server" />

			</h5>
		</div>
	</div>

	<div class="col-md-3 col-sm-6">
		<div class="data-container">
			<h6 class="font-size-16 margin-bottom-15">
				<asp:Label ID="lbl_Amount" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, Amount %>" />
			</h6>
			<h5 class="font-size-20">
				<asp:Label ID="lbl_AmountValue" runat="server" />

			</h5>
		</div>
	</div>



	<div class="col-md-12 no-padding">
		<asp:Button ID="btn_Print" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, PrintPaymentReceipt %>" OnClientClick="Print(this)" CssClass="btn moe-btn pull-righ" />

	</div>

</div>

<div class="col-md-12 margin-top-15 margin-bottom-15 row heighlighted-section">
	<h4 class="font-size-18 font-weight-600 text-center">
		<asp:Label ID="lbl_NoPayment" runat="server" Text="<%$Resources:ITWORX_MOEHEWF_UCE, NoPaymentRecord %>"></asp:Label>
	</h4>
</div>
