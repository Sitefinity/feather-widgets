(function ($) {
    $(function () {

        function changeOrInput(e) {
            if (typeof e.srcElement.validity == 'undefined')
                return;

            var validationMessages = getValidationMessages(e.srcElement);

            if (e.srcElement.required && e.srcElement.validity.valueMissing) {
                e.srcElement.setCustomValidity(validationMessages.required);
            }
            else if (e.srcElement.validity.tooShort || e.srcElement.validity.tooLong) {
                e.srcElement.setCustomValidity(validationMessages.maxLength);
            }
            else {
                e.srcElement.setCustomValidity('');
            }
        }

        function invalid(e) {
            if (typeof e.srcElement.validity == 'undefined')
                return;

            var validationMessages = getValidationMessages(e.srcElement);
            
            if (e.srcElement.validity.valueMissing) {
                e.srcElement.setCustomValidity(validationMessages.required);
            }
            else if (e.srcElement.validity.tooShort || e.srcElement.validity.tooLong) {
                e.srcElement.setCustomValidity(validationMessages.maxLength);
            }
            else if (e.srcElement.validity.patternMismatch) {
                e.srcElement.setCustomValidity(validationMessages.maxLength);
            }
        }

        function getValidationMessages(input) {
            var container = $(input).parents('[data-sf-role="paragraph-text-field-container"]');
            var validationMessagesInput = $(container).find('[data-sf-role="violation-messages"]');
            var validationMessages = JSON.parse(validationMessagesInput.val());

            return validationMessages;
        }

        function init() {
            var containers = $('[data-sf-role="paragraph-text-field-container"]');

            if (!containers || containers.length < 1)
                return;

            for (var i = 0; i < containers.length; i++) {
                var textarea = $(containers[i]).find('[data-sf-role="paragraph-text-field-textarea"]')[0];

                if (textarea) {
                    textarea.addEventListener('change', changeOrInput);
                    textarea.addEventListener('input', changeOrInput);
                    textarea.addEventListener('invalid', invalid);
                }
            }
        }

        init();
    });
}(jQuery));