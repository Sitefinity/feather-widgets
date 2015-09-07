(function ($) {
    $(function () {

        function changeOrInput(e) {
            if (!e.srcElement.validity)
                return;

            if (e.srcElement.required && e.srcElement.validity.valueMissing) {
                var validationMessages = getValidationMessages(e.srcElement);
                e.srcElement.setCustomValidity(validationMessages.required);
            } else {
                e.srcElement.setCustomValidity('');
            }
        }

        function invalid(e) {
            if (!e.srcElement.validity)
                return;

            var validationMessages = getValidationMessages(e.srcElement);

            if (e.srcElement.validity.valueMissing) {
                e.srcElement.setCustomValidity(validationMessages.required);
            }
            else if (e.srcElement.validity.patternMismatch) {
                e.srcElement.setCustomValidity(validationMessages.maxLength);
            }
            else if (!e.srcElement.validity.valid) {
                e.srcElement.setCustomValidity(validationMessages.invalid);
            }
        }

        function getValidationMessages(input) {
            var container = $(input).parents('[data-sf-role="text-field-container"]');
            var validationMessagesInput = $(container).find('[data-sf-role="violation-messages"]');
            var validationMessages = JSON.parse(validationMessagesInput.val());

            return validationMessages;
        }

        function init() {
            var containers = $('[data-sf-role="text-field-container"]');

            if (!containers || containers.length < 1)
                return;

            for (var i = 0; i < containers.length; i++) {
                var input = $(containers[i]).find('[data-sf-role="text-field-input"]')[0];

                if (input) {
                    input.addEventListener('change', changeOrInput);
                    input.addEventListener('input', changeOrInput);
                    input.addEventListener('invalid', invalid);
                }
            }
        }

        init();
    });
}(jQuery));