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
        var container = $(input).parents('[data-sf-role="checkboxes-field-container"]');
        var validationMessagesInput = $(container).find('[data-sf-role="violation-messages"]');
        var validationMessages = JSON.parse(validationMessagesInput.val());

        return validationMessages;
    };

    $(function () {
        var containers = $('[data-sf-role="checkboxes-field-container"]');

        if (!containers || containers.length < 1)
            return;

        for (var i = 0; i < containers.length; i++) {
            var input = $(containers[i]).find('[data-sf-role="checkboxes-field-other-input"]')[0];

            if (input) {
                input.addEventListener('change', changeOrInput);
                input.addEventListener('input', changeOrInput);
                input.addEventListener('invalid', invalid);
            }
        }

        containers.find('label').click(function () {
            $(this).prev().click();
        });

        var otherCheckbox = $(containers.find('[data-sf-checkboxes-role="other-choice-checkbox"]').first());
        var otherInput = $(containers.find('[data-sf-checkboxes-role="other-choice-text"]').first());

        otherInput.change(function () {
            otherCheckbox.val($(this).val());
        });

        var otherSelected = false;
        otherCheckbox.click(function () {
            if (otherSelected) {
                otherInput.attr('type', 'hidden');
            }
            else {
                otherInput.attr('type', 'text');
            }

            otherSelected = !otherSelected;
        });
    });
}(jQuery));