(function ($) {
    var changeOrInput = function (e) {
        if (e.srcElement.value === '') {
            var validationMessages = getValidationMessages(e.srcElement);
            e.srcElement.setCustomValidity(validationMessages.required);
        }
        else {
            e.srcElement.setCustomValidity('');
        }
    };

    var invalid = function (e) {
        var validationMessages = getValidationMessages(e.srcElement);

        if (e.srcElement.validity.valueMissing) {
            e.srcElement.setCustomValidity(validationMessages.required);
        }
    };

    var getValidationMessages = function (input) {
        var container = $(input).parents('[data-sf-role="multiple-choice-field-container"]');
        var validationMessagesInput = $(container).find('[data-sf-role="violation-messages"]');
        var validationMessages = JSON.parse(validationMessagesInput.val());

        return validationMessages;
    };

    $(function () {
        var containers = $('[data-sf-role="multiple-choice-field-container"]');

        if (!containers || containers.length < 1)
            return;

        for (var i = 0; i < containers.length; i++) {
            var input = $(containers[i]).find('[data-sf-role="multiple-choice-field-input"]');

            if (input) {
                input.addEventListener('change', changeOrInput);
                input.addEventListener('input', changeOrInput);
                input.addEventListener('invalid', invalid);
            }
        }

        var radios = containers.find('input[data-sf-role="multiple-choice-field-input"]');

        var hiddenRadio = $(radios.last());
        var hiddenRadioIndex = radios.index(hiddenRadio);

        var otherRadio = $(containers.find('[data-sf-multiple-choice-role="other-choice-radio"]').first());
        var otherInput = $(containers.find('[data-sf-multiple-choice-role="other-choice-text"]').first());
        var otherRadioIndex = radios.index(otherRadio);

        var currentIndex = -1;

        radios.click(function () {
            var index = radios.index($(this));
            if (index != hiddenRadioIndex) {
                if (index == currentIndex) {
                    currentIndex = -1;
                    otherInput.attr('type', 'hidden');
                    hiddenRadio.click();
                }
                else {
                    currentIndex = index;
                    if (currentIndex == otherRadioIndex) {
                        otherInput.attr('type', 'text');
                    }
                    else {
                        otherInput.attr('type', 'hidden');
                    }
                }
            }
        });

        otherInput.change(function () {
            otherRadio.val($(this).val());
        });

        containers.find('label').click(function () {
            $(this).prev().click();
        });
    });
}(jQuery));