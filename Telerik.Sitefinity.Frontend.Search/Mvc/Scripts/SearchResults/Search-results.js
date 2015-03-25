$(document).ready(function () {

    //Dropdownlist Selectedchange event
    $(".userSortDropdown").change(function (value) {
        var selectedValue = $(value.currentTarget).val();
        var url = getResultsUrl(selectedValue);
        window.location.search = url;
    });

    // Returns url with all needed parameters
    function getResultsUrl(orderBy, language) {
        var orderByFieldValue = $('[data-sf-role="searchResOrderBy"]').First().val();
        var orderByValue = orderBy || orderByFieldValue;
        var languageFieldValue = $('[data-sf-role="searchResLanguage"]').First().val();
        var languageValue = language || languageFieldValue;

        var orderByParam = orderByValue ? '&orderBy=' + orderByValue : '';
        var languageParam = languageValue ? '&language=' + languageValue : '';

        var indexCatalogueParam = $('[data-sf-role="searchResIndexCatalogue"]').First().val();
        var searchQueryParam = $('[data-sf-role="searchResQuery"]').First().val();
        var wordsModeParam = $('[data-sf-role="searchResWordsMode"]').First().val();
        return '?indexCatalogue=' + indexCatalogueParam + '&' +
            'searchQuery=' + searchQueryParam + '&' +
            'wordsMode=' + wordsModeParam +
            orderByParam +
            languageParam;
    }
});