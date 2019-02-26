$(document).ready(function () {
    var location = document.location.href;
    $.each($(".side-menu li"), function () {
        var thisHref = $(this).children("a").attr("href");
        var pos = location.indexOf(thisHref);
        if (pos !== -1) {
            $(this).addClass("active");
        }
    });
});