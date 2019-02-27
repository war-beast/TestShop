$(document).ready(function () {
    $.ajaxSetup({ cache: false });

    var location = document.location.href;
    $.each($(".side-menu li"), function () {
        var thisHref = $(this).children("a").attr("href");
        var pos = location.indexOf(thisHref);
        if (pos !== -1) {
            $(this).addClass("active");
        }
    });

    $(".dialog").click(function (e) {
        e.preventDefault();
        $("#contentDialog .modal-title").html($(this).attr("title"));
        $("#contentDialog .modal-body").html("Загрузка...");
        $("#contentDialog .modal-body").load($(this).attr("href"));
        $("#contentDialog").modal("show");
    });
});