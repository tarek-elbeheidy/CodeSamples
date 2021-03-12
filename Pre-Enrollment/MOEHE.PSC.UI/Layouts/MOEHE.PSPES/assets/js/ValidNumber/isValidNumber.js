var telInput = $("#phone"),
  errorMsg = $("#error-msg"),
  validMsg = $("#valid-msg");

// initialise plugin
telInput.intlTelInput({
  utilsScript: "js/utils.js"
});

var reset = function() {
  telInput.removeClass("error");
  errorMsg.addClass("invisible");
  validMsg.addClass("invisible");
};

// on blur: validate
telInput.blur(function() {
  reset();
  if ($.trim(telInput.val())) {
    if (telInput.intlTelInput("isValidNumber")) {
       validMsg.removeClass("invisible");
    } else {
      telInput.addClass("error");
      errorMsg.removeClass("invisible");
    }
  }
});

// on keyup / change flag: reset
telInput.on("keyup change", reset);