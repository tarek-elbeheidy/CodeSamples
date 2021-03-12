$(document).ready(function () {

	//select2
    $('.moe-dropdown').select2();


 $("#s4-workspace").scroll(function ()
{

$('#ui-datepicker-div').hide();
});
 setHeight();


});


$(window).resize(function() {

if ($(window).width() < 992) {
	 $('.navbar-collapse').append('<div class="row footer_cont_nav">' + $(".he-footer .row.flex-display").html() + '</div>');
		
		
	}  
	else{
		
			$('.footer_cont_nav').remove();

	}

setHeight();

});

var setHeight = function () {
    var footer = $("footer").height();
    var windowHeight = $(window).height();
    var header = $("header").height();
    var pageHeight = windowHeight - ( footer + header + 9);
  
    $("#main-content.register").css("min-height", pageHeight);
    console.log("dd");


}

