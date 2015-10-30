(function ($) {
    $(function () {

        function changeOrInput(e) {
            if (typeof e.target.validity == 'undefined')
                return;

            if (e.target.required && e.target.validity.valueMissing) {
                var validationMessages = getValidationMessages(e.target);
                e.target.setCustomValidity(validationMessages.required);
            } else {
                e.target.setCustomValidity('');
            }
        }

        function invalid(e) {
            if (typeof e.target.validity == 'undefined')
                return;

            var validationMessages = getValidationMessages(e.target);
            var validationRestrictions = getValidationRestrictions(e.target);
            var isValidLength = e.target.value.length >= validationRestrictions.minLength;

            if(validationRestrictions.maxLength > 0)
                isValidLength &= e.target.value.length <= validationRestrictions.maxLength;

            if (e.target.validity.valueMissing) {
                e.target.setCustomValidity(validationMessages.required);
            }
            else if (e.target.validity.patternMismatch && !isValidLength) {
                e.target.setCustomValidity(validationMessages.maxLength);
            }
            else if (e.target.validity.patternMismatch && isValidLength) {
                e.target.setCustomValidity(validationMessages.regularExpression);
            }
            else if (!e.target.validity.valid) {
                e.target.setCustomValidity(validationMessages.invalid);
            }
        }

        function getValidationMessages(input) {
            var container = $(input).parents('[data-sf-role="text-field-container"]');
            var validationMessagesInput = $(container).find('[data-sf-role="violation-messages"]');
            var validationMessages = JSON.parse(validationMessagesInput.val());

            return validationMessages;
        }

        function getValidationRestrictions(input) {
            var container = $(input).parents('[data-sf-role="text-field-container"]');
            var validationRestrictionsInput = $(container).find('[data-sf-role="violation-restrictions"]');
            var validationRestrictions = JSON.parse(validationRestrictionsInput.val());

            return validationRestrictions;
        }

        function init() {
            var containers = $('[data-sf-role="text-field-container"]');

            if (!containers || containers.length < 1)
                return;

            for (var i = 0; i < containers.length; i++) {
                var input = $(containers[i]).find('[data-sf-role="text-field-input"]');

                if (input) {
                    input.on('change', changeOrInput);
                    input.on('input', changeOrInput);
                    input.on('invalid', invalid);
                }
            }
        }

        init();
    });
}(jQuery));