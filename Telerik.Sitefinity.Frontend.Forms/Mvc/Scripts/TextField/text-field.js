(function ($) {
    var input;
    var validationMessages;

    function changeOrInput() {
        if (input.value === '') {
            input.setCustomValidity(validationMessages.required);
        } else {
            input.setCustomValidity('');
        }
    }

    function invalid() {
        if (input.validity.valueMissing) {
            input.setCustomValidity(validationMessages.required);
        }
        else if (input.validity.patternMismatch) {
            input.setCustomValidity(validationMessages.maxLength);
        }
    }

    function init() {
        var container = $('[data-sf-role="text-field-container"]');
        var validationMessagesInput = container.find('[data-sf-role="violation-messages"]');
        validationMessages = JSON.parse(validationMessagesInput.val());

        input = container.find('[data-sf-role="text-field-input"]')[0];
        if (validationMessages && input) {
            input.addEventListener('change', changeOrInput);
            input.addEventListener('input', changeOrInput);
            input.addEventListener('invalid', invalid);
        }
    }

    init();
}(jQuery));