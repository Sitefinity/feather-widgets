(function ($) {
    $(function () {
        var validationMessages;

        function changeOrInput(e) {
            if (e.srcElement.value === '') {
                e.srcElement.setCustomValidity(validationMessages.required);
            } else {
                e.srcElement.setCustomValidity('');
            }
        }

        function invalid(e) {
            if (e.srcElement.validity.valueMissing) {
                e.srcElement.setCustomValidity(validationMessages.required);
            }
            else if (e.srcElement.validity.patternMismatch) {
                e.srcElement.setCustomValidity(validationMessages.maxLength);
            }
        }

        function init() {
            var container = $('[data-sf-role="text-field-container"]');
            var validationMessagesInput = container.find('[data-sf-role="violation-messages"]');
            validationMessages = JSON.parse(validationMessagesInput.val());

            inputs = container.find('[data-sf-role="text-field-input"]');

            if (!inputs || inputs.length < 1)
                return;

            for (var i = 0; i < inputs.length; i++) {
                if (validationMessages && inputs[i]) {
                    inputs[i].addEventListener('change', changeOrInput);
                    inputs[i].addEventListener('input', changeOrInput);
                    inputs[i].addEventListener('invalid', invalid);
                }
            }
        }

        init();
    });
}(jQuery));