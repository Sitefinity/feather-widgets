(function () {
    /* Polyfills */

    if (window.NodeList && !NodeList.prototype.forEach) {
        NodeList.prototype.forEach = Array.prototype.forEach;
    }
    
    /* Polyfills end */

    document.addEventListener('DOMContentLoaded', function () {
        //Dropdownlist Selectedchange event
        document.querySelectorAll(".userSortDropdown").forEach(function (result) {
            result.addEventListener('change', function () {
                var selectedValue = result.value;
                var url = getResultsUrl(selectedValue);
                window.location.search = url;
            });
        });

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
            var wordsModeParam = document.querySelector('[data-sf-role="searchResWordsMode"]').value;
            return '?indexCatalogue=' + indexCatalogueParam + '&' +
                'searchQuery=' + searchQueryParam + '&' +
                'wordsMode=' + wordsModeParam +
                orderByParam +
                languageParam;
        }
    });
}());