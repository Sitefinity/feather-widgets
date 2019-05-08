(function ($) {
    $(function () {
        function changeOrInput(e) {
            if (e.target.value === '') {
                var validationMessages = getValidationMessages(e.target);
                setErrorMessage(e.target, validationMessages.required);
            }
            else {
                setErrorMessage(e.target, '');
            }

            if (typeof $.fn.processFormRules == 'function') {
                $(e.target).processFormRules();
            }
        }

        function invalid(e) {
            var validationMessages = getValidationMessages(e.target);

            if (_getErrorMessageContainer(e.target)) {
                e.preventDefault();
            }

            if (e.target.validity.valueMissing) {
                setErrorMessage(e.target, validationMessages.required);
            }
        }

        function getValidationMessages(input) {
            var container = $(input).parents('[data-sf-role="dropdown-list-field-container"]');
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
            var container = $(input).closest('[data-sf-role="dropdown-list-field-container"]')[0];
            if (container) {
                var errorMessagesContainer = container.querySelector('[data-sf-role="error-message"]');
                return errorMessagesContainer;
            }

            return null;
        }

        function init() {
            var containers = $('[data-sf-role="dropdown-list-field-container"]');

            if (!containers || containers.length < 1)
                return;

            for (var i = 0; i < containers.length; i++) {
                var select = $(containers[i]).find('[data-sf-role="dropdown-list-field-select"]');

                if (select) {
                    select.on('change', changeOrInput);
                    select.on('invalid', invalid);
                }
            }
        }

        init();
    });
}(jQuery));