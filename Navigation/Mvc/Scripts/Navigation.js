(function ($) {
    $('.sfNavToggle').bind("click", function () {
        $(this).siblings(".sfNav").toggle();
    });

    $('.sfNavSelect').change(function () {
        var val = $(this).find("option:selected").val();
        window.location.replace(val);
    });
})(jQuery);
