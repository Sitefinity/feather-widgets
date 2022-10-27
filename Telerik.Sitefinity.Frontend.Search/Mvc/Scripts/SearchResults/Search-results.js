(function () {
    /* Polyfills */

    var SEARCH_RESULT_CONTAINER_ID = "sf-search-result-container";
    var SEARCHRESULTS_LOADING_INDICATOR = "sf-search-results-loading-indicator";

    if (window.NodeList && !NodeList.prototype.forEach) {
        NodeList.prototype.forEach = Array.prototype.forEach;
    }

    /* Polyfills end */

    document.addEventListener('DOMContentLoaded', function () {
        init();

    });

    function init() {

        document.addEventListener('searchResultsLoaded', function (ev, o) {
            if (ev.detail && ev.detail.searchResultsPageDocument) {
                var searchResultsPageDocument = ev.detail.searchResultsPageDocument;
                var searchResultsContainer = document.getElementById(SEARCH_RESULT_CONTAINER_ID);

                if (searchResultsContainer) {
                    var newSearchContent = searchResultsPageDocument.getElementById(SEARCH_RESULT_CONTAINER_ID);
                    if (newSearchContent) {
                        searchResultsContainer.innerHTML = newSearchContent.innerHTML;
                        bindSortDropDown();
                    }
                }
            }
            toggleLoadingVisiblity(false);
        });

        bindSortDropDown();
        bindLoadingIndicator();

        // Returns url with all needed parameters
        function getResultsUrl(orderBy, language) {
            var orderByField = document.querySelector('[data-sf-role="searchResOrderBy"]');
            orderByValue = orderBy || orderByField.value;
            var languageField = document.querySelector('[data-sf-role="searchResLanguage"]');
            var languageValue = language || languageField.value;

            var orderByParam = orderByValue ? '&orderBy=' + orderByValue : '';
            var languageParam = languageValue ? '&language=' + languageValue : '';

            var indexCatalogueParam = document.querySelector('[data-sf-role="searchResIndexCatalogue"]').value;
            var searchQueryParam = document.querySelector('[data-sf-role="searchResQuery"]').value;

            var queryString = '?indexCatalogue=' + indexCatalogueParam + '&' +
                'searchQuery=' + searchQueryParam +
                orderByParam +
                languageParam;

            var scoringInfoValue = document.querySelector('[data-sf-role="scoringInfo"]').value;
            var scoringInfoParam = scoringInfoValue ? '&scoringInfo=' + scoringInfoValue : null;
            if (scoringInfoParam) {
                queryString = queryString + scoringInfoParam;
            }

            var filterHiddenElement = document.querySelector('[data-sf-role="filterParameter"]');
            if (filterHiddenElement && filterHiddenElement.value) {
                var filterValue = filterHiddenElement.value;
                var filterQueryParam = filterValue ? '&filter=' + filterValue : null;

                if (filterQueryParam) {
                    queryString = queryString + filterQueryParam;
                }
            }

            var resultsForAllSites = document.querySelector('[data-sf-role="resultsForAllSites"]');
            if (resultsForAllSites) {
                var resultsForAllSitesParam = resultsForAllSites.value ? '&resultsForAllSites=' + resultsForAllSites.value : null;
                if (resultsForAllSitesParam) {
                    queryString = queryString + resultsForAllSitesParam;
                }
            }

            return queryString;
        }

        function bindSortDropDown() {
            //Dropdownlist Selectedchange event
            document.querySelectorAll(".userSortDropdown").forEach(function (result) {
                result.addEventListener('change', function () {
                    var selectedValue = result.value;
                    var url = getResultsUrl(selectedValue);
                    window.location.search = url;
                });
            });
        }

        function bindLoadingIndicator() {
            document.addEventListener('beginLoadingSearchResults', function (ev, o) {
                toggleLoadingVisiblity(true);
            });
        }


        function toggleLoadingVisiblity(showLoading) {
            var searchResultsElements = document.getElementById(SEARCH_RESULT_CONTAINER_ID);
            var elementsToHideWhileLoading = searchResultsElements.querySelectorAll("[data-sf-hide-while-loading='true']");
            if (elementsToHideWhileLoading.length > 0) {
                elementsToHideWhileLoading.forEach(function (element) {
                    element.style.display = showLoading ? "none" : "block";
                });
            } else {
                searchResultsElements.style.display = showLoading ? "none" : "block";
            }

            var loadingElement = document.getElementById(SEARCHRESULTS_LOADING_INDICATOR);
            loadingElement.style.display = showLoading ? "block" : "none";
        }

    }
}());