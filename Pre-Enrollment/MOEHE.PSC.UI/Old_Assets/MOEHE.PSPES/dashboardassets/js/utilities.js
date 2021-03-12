function detectBrowserVersion() {
    var ua = navigator.userAgent,
	tem,
	M = ua.match(/(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i) || [];

    if (/trident/i.test(M[1])) {
        tem = /\brv[ :]+(\d+)/g.exec(ua) || [];

        return 'IE' + (tem[1] || '');
    }
    if (M[1] === 'Chrome') {
        tem = ua.match(/\bOPR\/(\d+)/)
        if (tem != null) return 'Opera ' + tem[1];
    }
    M = M[2] ? [M[1], M[2]] : [navigator.appName, navigator.appVersion, '-?'];
    if ((tem = ua.match(/version\/(\d+)/i)) != null) M.splice(1, 1, tem[1]);

    return M.join('');
};




$(function () {
    //Detect your browser version and set class name in html tag
    var _detectBrowser = detectBrowserVersion();
    $("html").addClass(_detectBrowser);
    $('.slim-scroll , .years-range').slimScroll({
        position: 'left',
        height: '150px',
        railVisible: true
    });

    var wow = new WOW({
        boxClass: 'wow', // animated element css class (default is wow)
        animateClass: 'animated', // animation css class (default is animated)
        offset: 0, // distance to the element when triggering the animation (default is 0)
        mobile: true, // trigger animations on mobile devices (default is true)
        live: true, // act on asynchronously loaded content (default is true)
        callback: function (box) {
            // the callback is fired every time an animation is started
            // the argument that is passed in is the DOM node being animated
        },
        scrollContainer: null // optional scroll container selector, otherwise use window
    });
    wow.init();

    //$('.menu-toggle').on('click', function() {
    //    $(this).toggleClass('open');
    //});

    var end = 1900;
    var start = new Date().getFullYear();
    var options = "";
    for (var year = start ; year >= end; year--) {
        options += "<a class='dropdown-item' href='#'>" + (year - 1) + " - " + (year) + "</a>";
    }
    document.querySelector(".years-range").innerHTML = options;

    var selectYear = $('.years-range').find('.dropdown-item:first').text();
    console.log(selectYear);
    document.querySelector(".years-range-selected").innerHTML = selectYear;
    $('.years-range .dropdown-item').click(function (e) {
        var selectYear = $(e.target).text();
        //console.log(txt);
        document.querySelector(".years-range-selected").innerHTML = selectYear;
    })

    $('.menu__handle').click(function (e) {
        e.preventDefault();
        $('.menu__handle').toggleClass('menu__handle-open');
    });

    $(".page-header").on("click", ".search-form", function (e) {
        $(this).addClass("open"), $(this).find(".form-control").focus(), $(".page-header .search-form .form-control").on("blur", function (e) {
            $(this).closest(".search-form").removeClass("open"), $(this).unbind("blur")
        })
    }), $(".page-header").on("keypress", ".hor-menu .search-form .form-control", function (e) {
        return 13 == e.which ? ($(this).closest(".search-form").submit(), !1) : void 0
    }), $(".page-header").on("mousedown", ".search-form.open .submit", function (e) {
        e.preventDefault(), e.stopPropagation(), $(this).closest(".search-form").submit()
    });


    var canvas = document.querySelector(".cards").getElementsByTagName('canvas');

    for (var i = 0; i < canvas.length; i++) {
        progressBar(canvas[i].id);
    }

    // load the canvas
    function progressBar(canvasId) {
        var canvas = document.getElementById(canvasId);
        var ctx = canvas.getContext('2d');

        // declare some variables
        var cWidth = canvas.width;
        var cHeight = canvas.height;
        var progressColor = canvas.getAttribute('data-color');
        var circleColor = '#ddd';
        var rawPerc = canvas.getAttribute('data-perc');
        var definition = canvas.getAttribute('data-text');
        var perc = parseInt(rawPerc);
        var minPerc = (parseInt(canvas.getAttribute('data-min-perc')) * 360) / 100;

        var degrees = 0;
        var endDegrees = (360 * perc) / 100;

        var lineWidth = 7; // The 'brush' size

        console.log(canvasId + ' ' + perc);

        function getDegrees() {
            if (degrees < endDegrees) {
                degrees++;
            } else {
                clearInterval(degreesCall);
            }

            drawProgressBar();
        }

        function drawProgressBar() {
            //clear the canvas after every instance
            ctx.clearRect(0, 0, cWidth, cHeight);

            // let's draw the background circle
            ctx.beginPath();
            ctx.strokeStyle = circleColor;
            ctx.lineWidth = lineWidth / 2;
            ctx.arc(cHeight / 2, cWidth / 2, (cWidth / 3) - 4, 0, Math.PI * 2, false);
            ctx.stroke();
            var radians = 0; // We need to convert the degrees to radians

            radians = degrees * Math.PI / 180;


            // let's draw the actual progressBar
            ctx.beginPath();
            ctx.strokeStyle = progressColor;
            ctx.lineWidth = lineWidth;
            //ctx.lineCap = "round";
            ctx.arc(cHeight / 2, cWidth / 2, (cWidth / 3) - 4, 0 - 90 * Math.PI / 180, radians - 90 * Math.PI / 180, false);
            ctx.stroke();
            // let's draw the minPerc
            var minPercRad = (minPerc - 3) * Math.PI / 180;
            var minPercRad2 = (minPerc + 3) * Math.PI / 180;
            ctx.beginPath();
            ctx.strokeStyle = '#71747f';
            ctx.lineWidth = 20;
            ctx.arc(cHeight / 2, cWidth / 2, (cWidth / 3) - 4, minPercRad - 90 * Math.PI / 180, minPercRad2 - 90 * Math.PI / 180, false);
            ctx.stroke();
            var minPercX = cWidth / 2 + (Math.cos((minPerc - 90) * Math.PI / 180) * cWidth / 2);
            var minPercY = cHeight / 2 + (Math.sin((minPerc - 90) * Math.PI / 180) * cWidth / 2);
            var outputTextMinPerc = Math.floor(minPerc / 360 * 100) + '%';
            ctx.fillStyle = '#71747f';
            ctx.font = 'bold 12px OpenSans';
            if (minPerc > 90 && minPerc < 180) {
                minPercX += 3;
                minPercY += 5;
            } else if (minPerc > 180 && minPerc < 270) {
                minPercX += 20;
                minPercY -= 5;
            } else if (minPerc == 270) {
                minPercX += 25;
                minPercY -= 5;
            } else if (minPerc > 270 && minPerc < 360) {
                minPercX += 25;
                minPercY += 10;
            } else if (minPerc == 360) {
                minPercX += 15;
                minPercY += 15;
            } else if (minPerc > 0 && minPerc < 90) {
                minPercX += 10;
                minPercY += 0;
            } else {
                minPercX += 10;
                minPercY -= 5;
            }


            ctx.fillText(outputTextMinPerc, minPercX, minPercY);
            // let's get the text
            ctx.fillStyle = '#71747f';
            ctx.font = 'bold 30px OpenSans';
            var outputTextPerc = Math.floor(degrees / 360 * 100) + '%';
            var outputTextPercWidth = ctx.measureText(outputTextPerc).width;
            var outputTextDefinitionWidth = ctx.measureText(definition).width;
            ctx.fillText(outputTextPerc, cWidth / 2 - outputTextPercWidth / 2 + 60, cHeight / 2 + 10);
            //ctx.fillText(definition, cWidth / 2 - outputTextDefinitionWidth / 4, cHeight / 2 + 15);
        }

        degreesCall = setInterval(getDegrees, 10 / (degrees - endDegrees));
    }



});


$(window).on("load", function () {
    // Animate loader off screen
    $(".preloader").fadeOut("slow");

});