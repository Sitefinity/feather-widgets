(function ($) {
    $(function () {
        function changeOrInput(e) {
            if (e.target.value === '') {
                var validationMessages = getValidationMessages(e.target);
                e.target.setCustomValidity(validationMessages.required);
            }
            else {
                e.target.setCustomValidity('');
            }
        }

        function invalid(e) {
            var validationMessages = getValidationMessages(e.target);

            if (e.target.validity.valueMissing) {
                e.target.setCustomValidity(validationMessages.required);
            }
        }

        function getValidationMessages(input) {
            var container = $(input).parents('[data-sf-role="dropdown-list-field-container"]');
            var validationMessagesInput = $(container).find('[data-sf-role="violation-messages"]');
            var validationMessages = JSON.parse(validationMessagesInput.val());

            return validationMessages;
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