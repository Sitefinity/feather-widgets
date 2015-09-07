var sf_captchaCallback = null;

(function ($) {
    var initRecaptcha = function (idx, element) {
        var options = {};
        for (var i = 0; i < element.attributes.length; i++) {
            if (element.attributes[i].name.startsWith('data-')) {
                var name = element.attributes[i].name.substring(5, element.attributes[i].name.length);
                options[name] = element.attributes[i].value;
            }
        }

        $(element).attr('sf-role', null);
        grecaptcha.render(element, options);
    };

    sf_captchaCallback = function () {
        $('[sf-role="recaptcha-field"]').each(initRecaptcha);
    };
}(jQuery));