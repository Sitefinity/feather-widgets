﻿(function ($) {
    $(document).ready(function () {
        var searchBoxIdFields = $('[data-sf-role="searchTextBoxId"]');

        for (var i = 0; i < searchBoxIdFields.length; i++) {
            var searchBoxIdField = $(searchBoxIdFields[i]);
            var controlServerData = {
                resultsUrl: searchBoxIdField.siblings('[data-sf-role="resultsUrl"]').first().val(),
                indexCatalogue: searchBoxIdField.siblings('[data-sf-role="indexCatalogue"]').first().val(),
                wordsMode: searchBoxIdField.siblings('[data-sf-role="wordsMode"]').first().val(),
                disableSuggestions: $.parseJSON(searchBoxIdField.siblings('[data-sf-role="disableSuggestions"]').first().val()),
                minSuggestionLength: searchBoxIdField.siblings('[data-sf-role="minSuggestionLength"]').first().val(),
                suggestionFields: searchBoxIdField.siblings('[data-sf-role="suggestionFields"]').first().val(),
                language: searchBoxIdField.siblings('[data-sf-role="language"]').first().val(),
                siteId: searchBoxIdField.siblings('[data-sf-role="siteId"]').first().val(),
                suggestionsRoute: searchBoxIdField.siblings('[data-sf-role="suggestionsRoute"]').first().val(),
                searchTextBoxSelector: searchBoxIdField.val(),
                searchButtonSelector: searchBoxIdField.siblings('[data-sf-role="searchButtonId"]').first().val()
            };
            featherSearchBoxWidget(controlServerData);
        }

        function featherSearchBoxWidget(serverData) {
            var searchTextBox = $(serverData.searchTextBoxSelector),
                    searchButton = $(serverData.searchButtonSelector);

            searchButton.click(navigateToResults);
            searchTextBox.keypress(keypressHandler);

            /* Initialization */
            if (!serverData.disableSuggestions) {
                searchTextBox.keyup(keyupHandler);

                try {
                    searchTextBox.autocomplete({
                        source: [],
                        messages:
                            {
                                noResults: '',
                                results: function () { }
                            },
                        select: function (event, ui) {
                            searchTextBox.val(ui.item.value);
                            navigateToResults(event);
                        },
                    }).autocomplete("widget").addClass("sf-autocomplete");
                } catch (e) {
                    // Fixes jQuery bug, causing IE7 to throw error "script3 member not found".
                    // The try/catch can be removed when the bug is fixed.
                }
            }

            /* Event handlers */
            function keypressHandler(e) {
                if (!e)
                    e = window.event;

                var keyCode = null;
                if (e.keyCode) {
                    keyCode = e.keyCode;
                }
                else {
                    keyCode = e.charCode;
                }

                if (keyCode == 13) {
                    navigateToResults(e);
                }
            }

            function suggestionsSuccessHandler(result, args) {
                var dataSource = result.Suggestions;
                searchTextBox.autocomplete('option', 'source', dataSource);

                searchTextBox.autocomplete("search", searchTextBox.val().trim());
            }

            function keyupHandler(e) {
                if (e.keyCode != 38 &&  // up arrow
                    e.keyCode != 40 && // down arrow
                    e.keyCode != 27) { // esc
                    // When the auto complete menu is shown, only this event is detected
                    if (e.keyCode == 13) {
                        // when enter is pressed
                        navigateToResults(e);
                    }

                    var request = {};
                    var searchText = searchTextBox.val().trim();
                    if (searchText.length >= serverData.minSuggestionLength) {
                        request.IndexName = serverData.indexCatalogue;
                        request.SuggestionFields = serverData.suggestionFields;
                        request.Text = searchText;
                        request.Language = serverData.language;
                        request.SiteId = serverData.siteId;

                        $.ajax({
                            type: "GET",
                            url: serverData.suggestionsRoute,
                            dataType: 'json',
                            data: request,
                            success: suggestionsSuccessHandler
                        });
                    }
                }
            }

            /* Helper methods */
            function navigateToResults(e) {
                if (!e)
                    e = window.event;

                if (e.stopPropagation) {
                    e.stopPropagation();
                }
                else {
                    e.cancelBubble = true;
                }
                if (e.preventDefault) {
                    e.preventDefault();
                }
                else {
                    e.returnValue = false;
                }

                var query = searchTextBox.val();

                if (query && query.trim() && serverData.indexCatalogue) {
                    sendSentence();
                    window.location = getLocation();
                }
            }

            function getLocation() {
                var query = searchTextBox.val().trim();

                var separator = (serverData.resultsUrl.indexOf("?") == -1) ? "?" : "&";

                var catalogueParam = separator + "indexCatalogue=" + encodeURIComponent(serverData.indexCatalogue),
                    searchQueryParam = "&searchQuery=" + encodeURIComponent(query),
                    wordsModeParam = "&wordsMode=" + serverData.wordsMode;

                var url = serverData.resultsUrl + catalogueParam + searchQueryParam + wordsModeParam;

                return url;
            }

            function sendSentence() {
                if (window.DataIntelligenceSubmitScript) {
                    DataIntelligenceSubmitScript._client.sentenceClient.writeSentence({
                        predicate: "Search for",
                        object: searchTextBox.val(),
                        objectMetadata: [{
                            'K': 'PageUrl',
                            'V': location.href
                        }]
                    });
                }
            }
        }
    });
}(jQuery));
