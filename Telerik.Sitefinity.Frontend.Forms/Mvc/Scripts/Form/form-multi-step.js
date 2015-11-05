; (function ($) {
    if (typeof window.FormData === 'undefined')
        return;

    $(function () {
        var formContainers = $('.separator');
        var formStepIndex = 0;
        var maxFormStepIndex = formContainers.length - 1;

        formContainers.each(function (i, element) {
            $(element).hide();
        });

        formContainers.first().show();
        formContainers.first().find('span[class="prev"]').hide();
        formContainers.last().find('span[class="next"]').hide();

        var separatorsNext = formContainers.find('span[class="next"]');
        separatorsNext.click(function (e) {
            $(e.target).parent().hide();
            formStepIndex++;
            $(formContainers[formStepIndex]).show();
        });

        var separatorsPrev = formContainers.find('span[class="prev"]');
        separatorsPrev.click(function (e) {
            $(e.target).parent().hide();
            formStepIndex--;
            $(formContainers[formStepIndex]).show();
        });
    });
})(jQuery);