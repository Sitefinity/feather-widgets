; (function ($) {
    if (typeof window.FormData === 'undefined')
        return;

    $(function () {
        var selectors = {
            separator: '[data-sf-role="separator"]',
            formContainer: '[data-sf-role="form-container"]',
            previousButton: '[data-sf-btn-role="prev"]',
            nextButton: '[data-sf-btn-role="next"]'
        };
        var formContainers = $(selectors.formContainer);

        var initializeFormContainer = function (element) {
            var formElement = $(element);
            var formStepsContainers = formElement.find(selectors.separator);
            var formStepIndex = 0;
            var submitButton = null;
            var isSubmitButtonAdded = false;
            var stepNewForm = null;

            formElement.on("form-page-changed", function (e, index, previousIndex) {
                formStepIndex = index;
            });

            formStepsContainers.each(function (i, element) {
                $(element).hide();
            });

            formStepsContainers.first().show();
            formStepsContainers.first().find(selectors.previousButton).hide();
            formStepsContainers.last().find(selectors.nextButton).hide();

            var tryGoToNextStep = function (currentStepContainer, continueFunction) {
                var formContainer = $(currentStepContainer);
                stepNewForm = $('form#stepNewForm');
                if (stepNewForm.length === 0) {
                    formContainer.wrap('<form id="stepNewForm"></form>');
                    stepNewForm = formContainer.parent();
                }

                stepNewForm.one('submit', function (e) {
                    e.preventDefault();
                    if (e.target.innerHTML.length > 0) {
                        var inputs = stepNewForm.find('input');
                        var isValid = true;
                        for (var i = 0; i < inputs.length; i++) {
                            var jInput = $(inputs[i]);
                            if (typeof (jInput.data('sfvalidator')) === 'function')
                                isValid = jInput.data('sfvalidator')() && isValid;
                        }

                        if (!isValid) {
                            return false;
                        }

                        formContainer.unwrap();

                        if (isSubmitButtonAdded) {
                            submitButton.remove();
                        }

                        continueFunction();
                    }
                });

                submitButton = formContainer.find('button[type="submit"],input[type="submit"]');

                if (submitButton.length === 0) {
                    // If we do not have submit button in this step we need to add a hidden submit button in order to click it and trigger the native HTML 5
                    // browser validation
                    submitButton = $('<input style="display:none;" type="submit"/>');
                    formContainer.append(submitButton);
                    isSubmitButtonAdded = true;
                }

                submitButton.click();
            };

            var separatorsNext = formStepsContainers.find(selectors.nextButton);
            separatorsNext.click(function (e) {
                e.preventDefault();

                var currentStepContainer = $(e.target).closest(selectors.separator);
                tryGoToNextStep(currentStepContainer, function () {
                    var previousIndex = formStepIndex;
                    currentStepContainer.hide();
                    formStepIndex++;
                    $(formStepsContainers[formStepIndex]).show();
                    formElement.trigger("form-page-changed", [formStepIndex, previousIndex]);
                });
            });

            var separatorsPrev = formStepsContainers.find(selectors.previousButton);
            separatorsPrev.click(function (e) {
                e.preventDefault();

                var previousIndex = formStepIndex;
                $(e.target).closest(selectors.separator).hide();
                formStepIndex--;
                $(formStepsContainers[formStepIndex]).show();
                formElement.trigger("form-page-changed", [formStepIndex, previousIndex]);
            });
        };

        formContainers.each(function (i, element) {
            initializeFormContainer(element);
        });

        // This implementation is only for the Form preview mode 
        var isPreviewMode = window.location.href.indexOf("/Preview") !== -1;
        if (formContainers.length === 0 && isPreviewMode) {
            var separator = $(selectors.separator);
            if (separator.length > 0) {
                initializeFormContainer(separator.parent());
            }
        }
    });
})(jQuery);