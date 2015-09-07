(function ($) {
    $(function () {

        function changeOrInput(e) {
            if (e.srcElement.value === '') {
                var validationMessages = getValidationMessages(e.srcElement);
                e.srcElement.setCustomValidity(validationMessages.required);
            }
            else {
                e.srcElement.setCustomValidity('');
            }
        }

        function invalid(e) {
            var validationMessages = getValidationMessages(e.srcElement);

            if (e.srcElement.validity.valueMissing) {
                e.srcElement.setCustomValidity(validationMessages.required);
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
                var select = $(containers[i]).find('[data-sf-role="dropdown-list-field-select"]')[0];

                if (select) {
                    select.addEventListener('change', changeOrInput);
                    select.addEventListener('invalid', invalid);
                }
            }
        }

        init();
    });
}(jQuery));