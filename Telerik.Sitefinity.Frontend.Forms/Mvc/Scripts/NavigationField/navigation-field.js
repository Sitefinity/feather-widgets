; (function ($) {
    if (typeof window.FormData === 'undefined')
        return;

    $(function () {
        var formContainers = $('[data-sf-role="form-container"]');

        var initializeFormContainer = function (element) {
            var formElement = $(element);
            var formStepsContainers = formElement.find('[data-sf-role="separator"]');
            var navigationFieldContainers = formElement.find('[data-sf-role="navigation-field-container"]');
            var srProgressbar = formElement.find('[data-sf-role="sr-progressbar"]');

            // Selects element with class sf-sr-only and adds text: Step # of #
            var modifySrOnlyData = function (currentPage, totalPages, pageTitle) {
                // TODO: Accessibility is not implemented no logic is required
                if (srProgressbar.length <= 0) {
                    return;
                }

                var stepOfResource = $(element).find('[data-sf-role="step-of-resources"]').val();
                var valueText = stepOfResource.replace("{0}", currentPage).replace("{1}", totalPages) + ": " + pageTitle;
                srProgressbar.attr("aria-valuenow", currentPage);
                srProgressbar.attr("aria-valueText", valueText);
            };

            var updateNavigationFields = function (navigationElements, index) {

                navigationElements.each(function (navIndex, navigationElement) {
                    var pages = $(navigationElement).find('[data-sf-navigation-index]');
                    var numberOfAllSteps = formStepsContainers.length;
                    var progressInPercent = Math.round((index / numberOfAllSteps) * 100);

                    var progressBar = formElement.find('[data-sf-role="progress-bar"]');
                    var progressPercent = formElement.find('[data-sf-role="progress-percent"]');

                    if (progressBar && progressBar.length > 0) {
                        progressBar.width(progressInPercent + '%');
                    }

                    if (progressPercent && progressPercent.length > 0) {
                        progressPercent.text(progressInPercent + '%');
                    }

                    if (pages && pages.length > 0) {
                        pages.each(function (i, page) {
                            var navElement = $(navigationElement);
                            var pageIndex = parseInt($(page).data("sfNavigationIndex"));
                            var pageTitleWrp = $(page).find('[data-sf-page-title]');
                            var pageTitle = "";

                            var activeCssClass = $(navElement).attr("data-sf-active-css-class") || "active";
                            var pastCssClass = $(navElement).attr("data-sf-past-css-class") || "past";
                            var futureCssClass = $(navElement).attr("data-sf-future-css-class") || "future";

                            var pastIndicatorPast = $(page).find("[data-sf-progress-indicator='past']");
                            var pastIndicatorIncomplete = $(page).find("[data-sf-progress-indicator='incomplete']");

                            // TODO: Accessibility is implemented
                            if (pageTitleWrp.length > 0) {
                                pageTitle = $(pageTitleWrp).data("sfPageTitle");
                            }
                            $(page).removeClass(activeCssClass).removeClass(futureCssClass).removeClass(pastCssClass);

                            if (pageIndex !== index) {
                                if (pageIndex < index) {
                                    $(page).addClass(pastCssClass);
                                    $(pastIndicatorPast).show();
                                    $(pastIndicatorIncomplete).hide();
                                } else if (pageIndex > index) {
                                    $(page).addClass(futureCssClass);
                                    $(pastIndicatorPast).hide();
                                    $(pastIndicatorIncomplete).show();
                                }
                            } else {
                                $(page).addClass(activeCssClass);
                                $(pastIndicatorPast).hide();
                                $(pastIndicatorIncomplete).show();

                                // Because pageIndex starts from 0 we increase it by 1 so it is simple to read
                                var currentPage = ++pageIndex;
                                modifySrOnlyData(currentPage, pages.length, pageTitle);
                            }
                        });
                    }

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