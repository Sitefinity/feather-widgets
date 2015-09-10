var sf_captchaCallback = null;

(function ($) {
    var initRecaptcha = function (idx, element) {
        var options = {};
        for (var i = 0; i < element.attributes.length; i++) {
            if (element.attributes[i].name.indexOf('data-') > -1 && element.attributes[i].name.indexOf('data-sf-') < 0) {
                var name = element.attributes[i].name.substring(5, element.attributes[i].name.length);
                options[name] = element.attributes[i].value;
            }
        }

        $(element).attr('data-sf-role', null);
        grecaptcha.render(element, options);
    };

    sf_captchaCallback = function () {
        $('[data-sf-role="recaptcha-field"]').each(initRecaptcha);
    };
}(jQuery));