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
            var submitButton = null;
            var isSubmitButtonAdded = false;
            var stepNewForm = null;

            formStepsContainers.each(function (i, element) {
                $(element).hide();
            });

            formStepsContainers.first().show();
            formStepsContainers.first().find('[data-sf-btn-role="prev"]').hide();
            formStepsContainers.last().find('[data-sf-btn-role="next"]').hide();

            var navigationFieldContainers = formElement.find('[data-sf-role="navigation-field-container"]');

            var updateNavigationFields = function (navigationElements, index) {

                navigationElements.each(function (navIndex, navigationElement) {
                    var pages = $(navigationElement).find('[data-sf-navigation-index]');
                    pages.each(function (i, page) {
                        var pageIndex = $(page).data("sfNavigationIndex");
                        if (pageIndex !== index) {
                            $(page).removeClass("active");
                        } else {
                            $(page).addClass("active");
                        }
                    });
                });
            };

            // Initialize navigation fields
            updateNavigationFields(navigationFieldContainers, formStepIndex);

            var tryGoToNextStep = function (currentStepContainer, continueFunction) {
                var formContainer = $(currentStepContainer);
                stepNewForm = formContainer.parent().is('form') ? formContainer.parent() : formContainer.parent().find('form');
                if (stepNewForm.length === 0) {
                    formContainer.wrap('<form />');
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

            var separatorsNext = formStepsContainers.find('[data-sf-btn-role="next"]');
            separatorsNext.click(function (e) {
                e.preventDefault();

                var currentStepContainer = $(e.target).closest('.separator');
                tryGoToNextStep(currentStepContainer, function () {
                    currentStepContainer.hide();
                    formStepIndex++;
                    $(formStepsContainers[formStepIndex]).show();
                    updateNavigationFields(navigationFieldContainers, formStepIndex);
                });
            });

            var separatorsPrev = formStepsContainers.find('[data-sf-btn-role="prev"]');
            separatorsPrev.click(function (e) {
                e.preventDefault();

                $(e.target).closest('.separator').hide();
                formStepIndex--;
                $(formStepsContainers[formStepIndex]).show();
                updateNavigationFields(navigationFieldContainers, formStepIndex);
            });
        });
    });
})(jQuery);