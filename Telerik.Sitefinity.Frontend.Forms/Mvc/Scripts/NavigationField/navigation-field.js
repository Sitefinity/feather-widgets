; (function ($) {
    if (typeof window.FormData === 'undefined')
        return;

    $(function () {
        var formContainers = $('[data-sf-role="form-container"]');

        var initializeFormContainer = function (element) {
            var formElement = $(element);

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
            updateNavigationFields(navigationFieldContainers, 0);

            formElement.on("form-page-changed", function (e, index, previousIndex) {
                updateNavigationFields(navigationFieldContainers, index);
            });
        };
           

        formContainers.each(function (i, element) {
            initializeFormContainer(element);
        });

        // This implementation is only for the Form preview mode 
        var isPreviewMode = window.location.href.indexOf("/Preview") !== -1;
        if (formContainers.length === 0 && isPreviewMode) {
            var separator = $('[data-sf-role="separator"]');
            if (separator.length > 0) {
                initializeFormContainer(separator.parent());
            }
        }
    });
})(jQuery);