(function ($) {
    $(function () {

        function changeOrInput(e) {
            if (typeof e.target.validity === 'undefined')
                return;

            var validationMessages = getValidationMessages(e.target);

            if (e.target.required && e.target.validity.valueMissing) {
                e.target.setCustomValidity(validationMessages.required);
            }
            else if (e.target.validity.tooShort || e.target.validity.tooLong) {
                e.target.setCustomValidity(validationMessages.maxLength);
            }
            else {
                e.target.setCustomValidity('');
            }
        }

        function invalid(e) {
            if (typeof e.target.validity === 'undefined')
                return;

            var validationMessages = getValidationMessages(e.target);
            
            if (e.target.validity.valueMissing) {
                e.target.setCustomValidity(validationMessages.required);
            }
            else if (e.target.validity.tooShort || e.target.validity.tooLong) {
                e.target.setCustomValidity(validationMessages.maxLength);
            }
            else if (e.target.validity.patternMismatch) {
                e.target.setCustomValidity(validationMessages.maxLength);
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
                var textarea = $(containers[i]).find('[data-sf-role="paragraph-text-field-textarea"]');

                if (textarea) {
                    textarea.on('change', changeOrInput);
                    textarea.on('input', changeOrInput);
                    textarea.on('invalid', invalid);
                }
            }
        }

        init();
    });
}(jQuery));