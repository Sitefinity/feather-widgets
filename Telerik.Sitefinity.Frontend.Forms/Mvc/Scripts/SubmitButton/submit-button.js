(function ($) {
    $(function () {
        $('button[type="submit"]').each(function () {
            $(this).closest('form').submit(function (ev) {
                if (!ev.isDefaultPrevented()) {
                    var button = $(this).find('button[type="submit"]');
                    button.prop('disabled', true);

                    //we need first to validate all inputs which require server side validation (cannot be validated client-side)
                    //before the form is submitted and submit the form only if all are valid - e.g. captcha is such control
                    var allInputs = ev.target.querySelectorAll('input, textarea', 'select');
                    var widgetValidators = [];

                    for (var i = 0; i < allInputs.length; i++) {
                        if ($(allInputs[i]).data('widget-validator')) {
                            widgetValidators.push($(allInputs[i]).data('widget-validator'));
                        }
                    }

                    if (widgetValidators.length) {
                        ev.preventDefault();
                        var deferreds = [];
                        for (i = 0; i < widgetValidators.length; i++) {
                            deferreds.push(widgetValidators[i]());
                        }

                        $.when.apply($, deferreds).done(function () {
                            for (var i = 0; i < widgetValidators.length; i++) {
                                if (!arguments[i]) {
                                    return false;
                                }
                            }

                            ev.target.submit();
                        }).always(function () {
                            button.prop('disabled', false);
                        });
                    }
                }
            });
        });
    });
}(jQuery));