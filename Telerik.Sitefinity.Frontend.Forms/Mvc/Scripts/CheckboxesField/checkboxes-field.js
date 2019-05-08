(function ($) {
    var changeOrInput = function (e) {
        var container = $(e.target).parents('[data-sf-role="checkboxes-field-container"]');
        var inputs = $(container).find('[data-sf-role="checkboxes-field-input"]');
        var hasChecked = $(container).find('input[data-sf-role="checkboxes-field-input"]:checked').length > 0;

        if (hasChecked || container.find('[data-sf-role="required-validator"]').val() === 'False') {
            $(inputs[0]).removeAttr('required');
            setErrorMessage(inputs[0], '');
        }
        else {
            $(inputs[0]).attr('required', 'required');
        }
    };

    var invalid = function (e) {
        var validationMessages = getValidationMessages(e.target);

        if (_getErrorMessageContainer(e.target)) {
            e.preventDefault();
        }

        if (e.target.validity.valueMissing) {
            setErrorMessage(e.target, validationMessages.required);
        }
    };

    var getValidationMessages = function (input) {
        var container = $(input).parents('[data-sf-role="checkboxes-field-container"]');
        var validationMessagesInput = $(container).find('[data-sf-role="violation-messages"]');
        var validationMessages = JSON.parse(validationMessagesInput.val());

        return validationMessages;
    };

    var setErrorMessage = function (input, message) {
        var errorMessagesContainer = _getErrorMessageContainer(input);

        if (errorMessagesContainer) {
            _toggleCustomErrorMessage(errorMessagesContainer, message);
        } else {
            input.setCustomValidity(message);
        }
    };

    var _toggleCustomErrorMessage = function (container, message) {
        container.innerText = message;

        if (message === '') {
            container.style.display = 'none';
        } else {
            container.style.display = 'block';
        }
    };

    var _getErrorMessageContainer = function (input) {
        var container = $(input).closest('[data-sf-role="checkboxes-field-container"]')[0];
        if (container) {
            var errorMessagesContainer = container.querySelector('[data-sf-role="error-message"]');
            return errorMessagesContainer;
        }

        return null;
    };

    $(function () {
        var containers = $('[data-sf-role="checkboxes-field-container"]');

        if (!containers || containers.length < 1)
            return;

        var attachHandlers = function (input) {
            input.on('change', function (e) {
                changeOrInput(e);
                if (typeof $.fn.processFormRules == 'function') {
                    $(e.target).processFormRules();
                }
            });
            input.on('input', changeOrInput);
            input.on('invalid', invalid);
        };

        var checkboxClickHandler = function (e) {
            var container = $(e.target).parents('[data-sf-role="checkboxes-field-container"]');
            var checkboxes = container.find('input[data-sf-role="checkboxes-field-input"]');
            var otherInput = $(container.find('[data-sf-checkboxes-role="other-choice-text"]').first());
            var otherCheckbox = $(container.find('[data-sf-checkboxes-role="other-choice-checkbox"]').first());
            var otherCheckboxIndex = checkboxes.index(otherCheckbox);
            var currentIndex = checkboxes.index($(e.target));
            var isRequired = container.find('[data-sf-role="required-validator"]').val() === 'True';

            if (currentIndex == otherCheckboxIndex && otherCheckbox.is(':checked')) {
                otherInput.attr('type', 'text');

                if (isRequired)
                    otherInput.attr('required', 'required');
                else
                    otherInput.removeAttr('required');
            }
            else {
                otherInput.attr('type', 'hidden');
                otherInput.removeAttr('required');
            }
        };

        var inputChangeHandler = function (e) {
            var container = $(e.target).parents('[data-sf-role="checkboxes-field-container"]');
            var otherCheckbox = $(container.find('[data-sf-checkboxes-role="other-choice-checkbox"]').first());
            otherCheckbox.val($(e.target).val());

            if (typeof $.fn.processFormRules == 'function') {
                $(e.target).processFormRules();
            }
        };

        for (var i = 0; i < containers.length; i++) {
            var container = $(containers[i]);
            var inputs = container.find('[data-sf-role="checkboxes-field-input"]');

            if (container.find('[data-sf-role="required-validator"]').val() === 'True' && !inputs.is(':checked'))
                $(inputs[0]).attr('required', 'required');

            attachHandlers(inputs);

            var checkboxes = container.find('input[data-sf-role="checkboxes-field-input"]');

            var otherInput = $(container.find('[data-sf-checkboxes-role="other-choice-text"]').first());

            checkboxes.click(checkboxClickHandler);
            otherInput.change(inputChangeHandler);
        }
    });
}(jQuery));