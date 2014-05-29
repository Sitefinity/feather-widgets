(function ($) {
    $('.nav-toggle').bind("click", function () {
        $(this).siblings(".nav").toggle();
        $(this).toggleClass("active");
    });

    $('.nav-select').change(function () {
        var val = $(this).find("option:selected").val();
        window.location.replace(val);
    });
})(jQuery);
