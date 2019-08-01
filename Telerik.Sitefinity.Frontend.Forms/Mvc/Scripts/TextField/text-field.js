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

            if (e.target.required && e.target.validity.valueMissing) {
                var validationMessages = getValidationMessages(e.target);
                setErrorMessage(e.target, validationMessages.required);
            } else {
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
            var validationRestrictions = getValidationRestrictions(e.target);
            var isValidLength = e.target.value.length >= validationRestrictions.minLength;

            if (validationRestrictions.maxLength > 0)
                isValidLength &= e.target.value.length <= validationRestrictions.maxLength;

            if (e.target.validity.valueMissing) {
                setErrorMessage(e.target, validationMessages.required);
            }
            else if (e.target.validity.patternMismatch && !isValidLength) {
                setErrorMessage(e.target, validationMessages.maxLength);
            }
            else if (e.target.validity.patternMismatch && isValidLength) {
                setErrorMessage(e.target, validationMessages.regularExpression);
            }
            else if (!e.target.validity.valid) {
                setErrorMessage(e.target, validationMessages.invalid);
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
            var container = $(input).closest('[data-sf-role="text-field-container"]')[0];
            if (container) {
                var errorMessagesContainer = container.querySelector('[data-sf-role="error-message"]');
                return errorMessagesContainer;
            }

            return null;
        }

        function init() {
            var containers = $('[data-sf-role="text-field-container"]');

            if (!containers || containers.length < 1)
                return;

            for (var i = 0; i < containers.length; i++) {
                var input = $(containers[i]).find('[data-sf-role="text-field-input"]');

                if (input) {
                    input.on('change', onChange);
                    input.on('input', onInput);
                    input.on('invalid', invalid);
                }
            }
        }

        init();
    });
}(jQuery));