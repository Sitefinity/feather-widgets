(function ($) {
    $(function () {

        function processRules(e) {
            if (typeof $.fn.processFormRules === 'function') {
                $(e.target).processFormRules();
            }
        }

        var delayTimer;
        function processRulesWithDelay(e) {
            clearTimeout(delayTimer);
            delayTimer = setTimeout(function () {
                processRules(e);
            }, 300);
        }

        function handleValidation(e) {
            if (typeof e.target.validity === 'undefined')
                return;

            var validationMessages = getValidationMessages(e.target);

            if (e.target.required && e.target.validity.valueMissing) {
                setErrorMessage(e.target, validationMessages.required);
            }
            else if (e.target.validity.tooShort || e.target.validity.tooLong) {
                setErrorMessage(e.target, validationMessages.maxLength);
            }
            else {
                setErrorMessage(e.target, '');
            }
        }

        function onChange(e) {
            handleValidation(e);
            processRules(e);
        }

        function onInput(e) {
            handleValidation(e);
            processRulesWithDelay(e);
        }

        function invalid(e) {
            if (typeof e.target.validity === 'undefined')
                return;

            if (_getErrorMessageContainer(e.target)) {
                e.preventDefault();
            }

            var validationMessages = getValidationMessages(e.target);

            if (e.target.validity.valueMissing) {
                setErrorMessage(e.target, validationMessages.required);
            }
            else if (e.target.validity.tooShort || e.target.validity.tooLong) {
                setErrorMessage(e.target, validationMessages.maxLength);
            }
            else if (e.target.validity.patternMismatch) {
                setErrorMessage(e.target, validationMessages.maxLength);
            }
        }

        function getValidationMessages(input) {
            var container = $(input).parents('[data-sf-role="paragraph-text-field-container"]');
            var validationMessagesInput = $(container).find('[data-sf-role="violation-messages"]');
            var validationMessages = JSON.parse(validationMessagesInput.val());

            return validationMessages;
        }

        function setErrorMessage(input, message) {
            var errorMessagesContainer = _getErrorMessageContainer(input);

            if (errorMessagesContainer) {
                _toggleCustomErrorMessage(errorMessagesContainer, message);
            } else {
                input.setCustomValidity(message);
            }
        }

        function _toggleCustomErrorMessage(container, message) {
            container.innerText = message;

            if (message === '') {
                container.style.display = 'none';
            } else {
                container.style.display = 'block';
            }
        }

        function _getErrorMessageContainer(input) {
            var container = $(input).closest('[data-sf-role="paragraph-text-field-container"]')[0];
            if (container) {
                var errorMessagesContainer = container.querySelector('[data-sf-role="error-message"]');
                return errorMessagesContainer;
            }

            return null;
        }

        function init() {
            var containers = $('[data-sf-role="paragraph-text-field-container"]');

            if (!containers || containers.length < 1)
                return;

            for (var i = 0; i < containers.length; i++) {
                var textarea = $(containers[i]).find('[data-sf-role="paragraph-text-field-textarea"]');

                if (textarea) {
                    textarea.on('change', onChange);
                    textarea.on('input', onInput);
                    textarea.on('invalid', invalid);
                }
            }
        }

        init();
    });
}(jQuery));