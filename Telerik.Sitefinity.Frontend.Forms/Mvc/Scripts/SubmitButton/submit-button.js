(function ($) {
    $(function () {
        $('button[type="submit"]').each(function () {
            $(this).closest('form').submit(function (ev) {
                if (!ev.isDefaultPrevented()) {
                    $(this).find('button[type="submit"]').prop('disabled', true);
                }
            });
        });
    });
}(jQuery));