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

            var separatorsNext = formStepsContainers.find('span[class="next"]');
            separatorsNext.click(function (e) {
                $(e.target).closest('.separator').hide();
                formStepIndex++;
                $(formStepsContainers[formStepIndex]).show();
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