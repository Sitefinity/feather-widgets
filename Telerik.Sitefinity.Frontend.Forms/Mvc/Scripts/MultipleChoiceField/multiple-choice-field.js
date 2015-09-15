(function ($) {
    var changeOrInput = function (e) {
        var container = $(e.srcElement).parents('[data-sf-role="multiple-choice-field-container"]');
        var inputs = $(container).find('[data-sf-role="multiple-choice-field-input"]');
        inputs.each(function (index, input) {
            input.validity.valueMissing = false;
            input.setCustomValidity('');
        });
    };

    var invalid = function (e) {
        var validationMessages = getValidationMessages(e.srcElement);

        if (e.srcElement.validity.valueMissing) {
            e.srcElement.setCustomValidity(validationMessages.required);
        }
    };

    var getValidationMessages = function (input) {
        var container = $(input).parents('[data-sf-role="multiple-choice-field-container"]');
        var validationMessagesInput = $(container).find('[data-sf-role="violation-messages"]');
        var validationMessages = JSON.parse(validationMessagesInput.val());

        return validationMessages;
    };

    $(function () {
        var containers = $('[data-sf-role="multiple-choice-field-container"]');

        if (!containers || containers.length < 1)
            return;

        var attachHandlers = function (index, input) {
            input.addEventListener('change', changeOrInput);
            input.addEventListener('input', changeOrInput);
            input.addEventListener('invalid', invalid);
        };

        var radioClickHandler = function (e) {
            var container = $(e.target).parents('[data-sf-role="multiple-choice-field-container"]');
            var radios = container.find('input[data-sf-role="multiple-choice-field-input"]');
            var otherInput = $(container.find('[data-sf-multiple-choice-role="other-choice-text"]').first());
            var otherRadio = $(container.find('[data-sf-multiple-choice-role="other-choice-radio"]').first());
            var otherRadioIndex = radios.index(otherRadio);
            var currentIndex = radios.index($(e.target));
            var isRequired = $(radios).first().attr('required');

            if (currentIndex == otherRadioIndex) {
                otherInput.attr('type', 'text');

                if (isRequired)
                    otherInput.attr('required', 'required');
            }
            else {
                otherInput.attr('type', 'hidden');
                otherInput.removeAttr('required');
            }
        };

        var inputChangeHandler = function (e) {
            var container = $(e.srcElement).parents('[data-sf-role="multiple-choice-field-container"]');
            var otherRadio = $(container.find('[data-sf-multiple-choice-role="other-choice-radio"]').first());
            otherRadio.val($(e.srcElement).val());
        };

        for (var i = 0; i < containers.length; i++) {
            var container = $(containers[i]);
            var inputs = container.find('[data-sf-role="multiple-choice-field-input"]');

            inputs.each(attachHandlers);

            var radios = container.find('input[data-sf-role="multiple-choice-field-input"]');

            var otherInput = $(container.find('[data-sf-multiple-choice-role="other-choice-text"]').first());

            radios.click(radioClickHandler);
            otherInput.change(inputChangeHandler);
        }
    });
}(jQuery));