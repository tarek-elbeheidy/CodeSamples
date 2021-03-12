
$(window).click(function() {
  hideAllFomrs();
});
var hideAllFomrs = function() {
  $(".login-form").removeClass("visible");
  $(".register-form").removeClass("visible");
}
$('.login-form.register-form').click(function(event){
    event.stopPropagation();
});
$('.login').click(function(event){
    event.stopPropagation();
});
$('.login > a').click(function(event){
    visibleLogin();
});
var visibleLogin = function() {
$(".register-form").removeClass("visible");
  $(".login-form").toggleClass("visible");
}
$('.register').click(function(event){
    event.stopPropagation();
});
$('.register > a').click(function(event){
    event.stopPropagation();
    visibleRegister();
});
var visibleRegister = function() {
  $(".login-form").removeClass("visible");
  $(".register-form").toggleClass("visible");
}
