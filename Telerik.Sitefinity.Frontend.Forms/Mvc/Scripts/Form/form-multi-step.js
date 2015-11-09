; (function ($) {
    if (typeof window.FormData === 'undefined')
        return;

    $(function () {
        var formContainers = $('[data-sf-role="form-container"]');
        formContainers.each(function (i, element) {
            var formElement = $(element);
            var formStepsContainers = formElement.find('.separator');
            var formStepIndex = 0;
            var maxFormStepIndex = formStepsContainers.length - 1;

            formStepsContainers.each(function (i, element) {
                $(element).hide();
            });

            formStepsContainers.first().show();
            formStepsContainers.first().find('span[class="prev"]').hide();
            formStepsContainers.last().find('span[class="next"]').hide();

            var tryGoToNextStep = function (currentStepContainer, continueFunction) {
                var formContainer = $(currentStepContainer);
                var newForm = formContainer.find('form');
                var wrapped = false;
                var isSubmitButtonAdded = false;
                if (newForm.length === 0) {
                    wrapped = true;
                    formContainer.wrap('<form />');
                    newForm = formContainer.parent();
                }

                newForm.one('submit', function (e) {
                    e.preventDefault();

                    var inputs = newForm.find('input');
                    var isValid = true;
                    for (var i = 0; i < inputs.length; i++) {
                        var jInput = $(inputs[i]);
                        if (typeof (jInput.data('sfvalidator')) === 'function')
                            isValid = jInput.data('sfvalidator')() && isValid;
                    }

                    if (!isValid) {
                        return false;
                    }

                    if (wrapped) {
                        formContainer.unwrap();
                    }

                    if (isSubmitButtonAdded) {
                        submitButton.remove();
                    }

                    continueFunction();
                });

                var submitButton = formContainer.find('button[type="submit"],input[type="submit"]');
                
                if (submitButton.length === 0) {
                    // If we do not have submit button in this step we need to add a hidden submit button in order to click it and trigger the native HTML 5
                    // browser validation
                    submitButton = $('<input style="display:none;" type="submit"/>');
                    formContainer.append(submitButton);
                    isSubmitButtonAdded = true;
                }

                submitButton.click();
            };

            var separatorsNext = formStepsContainers.find('span[class="next"]');
            separatorsNext.click(function(e){
                var currentStepContainer = $(e.target).closest('.separator');
                tryGoToNextStep(currentStepContainer, function () {
                    currentStepContainer.hide();
                    formStepIndex++;
                    $(formStepsContainers[formStepIndex]).show();
                });
            });

            var separatorsPrev = formStepsContainers.find('span[class="prev"]');
            separatorsPrev.click(function (e) {
                $(e.target).closest('.separator').hide();
                formStepIndex--;
                $(formStepsContainers[formStepIndex]).show();
            });
        });
    });
})(jQuery);