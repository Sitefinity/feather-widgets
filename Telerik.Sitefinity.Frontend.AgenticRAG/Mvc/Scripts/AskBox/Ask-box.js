(function () {
    var init = function () {
        var askBoxContainers = document.querySelectorAll('[data-sf-role="askBoxContainer"]');

        for (var i = 0; i < askBoxContainers.length; i++) {
            var container = askBoxContainers[i];
            var controlServerData = {
                knowledgeBoxName: getHiddenValue(container, "knowledgeBoxName"),
                searchConfigurationName: getHiddenValue(container, "searchConfigurationName"),
                resultsPageUrl: getHiddenValue(container, "resultsPageUrl"),
                suggestionsTriggerCharCount: parseInt(getHiddenValue(container, "suggestionsTriggerCharCount"), 10) || 0,
                suggestionsServicePath: getHiddenValue(container, "suggestionsServicePath"),
                searchTextBoxSelector: getHiddenValue(container, "askBoxTextBoxId"),
                searchButtonSelector: getHiddenValue(container, "askBoxButtonId"),
                contentTypes: getHiddenValue(container, "contentTypes"),
                lastModified: getHiddenValue(container, "lastModified")
            };
            featherAskBoxWidget(container, controlServerData);
        }

        function getHiddenValue(container, role) {
            var el = container.querySelector('[data-sf-role="' + role + '"]');
            return el ? el.value : "";
        }

        function featherAskBoxWidget(container, serverData) {
            var searchTextBox = document.getElementById(serverData.searchTextBoxSelector.replace(/^#/, ''));
            var searchButton = document.getElementById(serverData.searchButtonSelector.replace(/^#/, ''));

            if (!searchTextBox || !searchButton) return;

            var debounceTimeout = null;
            var debounceDelay = 300;
            var autocompleteDropdown = initAutocomplete(container, searchTextBox, navigateToResults);

            searchButton.addEventListener("click", navigateToResults);
            searchTextBox.addEventListener("keypress", keypressHandler);
            searchTextBox.addEventListener("keyup", keyupHandler);

            // Bind suggestion pills - handler defined outside loop
            function pillClickHandler(e) {
                var suggestionText = e.target.textContent;
                if (searchTextBox) {
                    searchTextBox.value = suggestionText;
                }
                navigateToResults(e);
            }

            var pills = container.querySelectorAll(".badge.rounded-pill");
            for (var p = 0; p < pills.length; p++) {
                pills[p].addEventListener("click", pillClickHandler);
            }

            /* Event handlers */
            function keypressHandler(e) {
                if (!e) e = window.event;

                var keyCode = e.keyCode || e.charCode;

                if (keyCode === 13) {
                    navigateToResults(e);
                }
            }

            function keyupHandler(e) {
                if (e.keyCode != 38 &&  // up arrow
                    e.keyCode != 40 &&  // down arrow
                    e.keyCode != 27) {  // esc

                    var searchText = e.target.value.trim();

                    if (serverData.suggestionsTriggerCharCount && searchText.length >= serverData.suggestionsTriggerCharCount) {
                        getSuggestions(e.target);
                    } else {
                        if (autocompleteDropdown) {
                            autocompleteDropdown.hide();
                        }
                    }
                }

                if (e.keyCode == 40) {
                    autocompleteDropdown.focus();
                } else if (e.keyCode == 27) {
                    autocompleteDropdown.hide();
                }
            }

            function getSuggestions(input) {
                // Clear any pending debounce timeout
                if (debounceTimeout) {
                    clearTimeout(debounceTimeout);
                }

                // Set a new debounce timeout
                debounceTimeout = setTimeout(function () {
                    var requestUrl = serverData.suggestionsServicePath +
                        "/Default.GetPARAGSuggestions()" +
                        "?knowledgeBoxName=" + encodeURIComponent(serverData.knowledgeBoxName) +
                        "&searchQuery=" + encodeURIComponent(input.value);

                    fetch(requestUrl)
                        .then(function (res) {
                            if (res.status === 200) {
                                res.json().then(function (suggestions) {
                                    autocompleteDropdown.source(suggestions.value);
                                });
                            } else {
                                autocompleteDropdown.hide();
                            }
                        })
                        .catch(function () {
                            autocompleteDropdown.hide();
                        });
                }, debounceDelay);
            }

            /* Helper methods */
            function navigateToResults(e) {
                if (!e) e = window.event;

                if (e.stopPropagation) {
                    e.stopPropagation();
                } else {
                    e.cancelBubble = true;
                }

                if (e.preventDefault) {
                    e.preventDefault();
                } else {
                    e.returnValue = false;
                }

                var query = searchTextBox.value;

                if (query && query.trim() && serverData.knowledgeBoxName) {
                    sendAnalytics(query);
                    window.location = buildResultsUrl(query);
                }
            }

            function buildResultsUrl(query) {
                var knowledgeBoxName = serverData.knowledgeBoxName || "";
                var searchConfigurationName = serverData.searchConfigurationName;
                var contentTypes = serverData.contentTypes;
                var lastModified = serverData.lastModified;
                var trimmedQuery = query.trim();
                var baseUrl;

                if (serverData.resultsPageUrl) {
                    baseUrl = serverData.resultsPageUrl;
                    var separator = baseUrl.indexOf("?") === -1 ? "?" : "&";
                    var url = baseUrl + separator + "knowledgeBoxName=" + encodeURIComponent(knowledgeBoxName);
                    url += "&searchQuery=" + encodeURIComponent(trimmedQuery);

                    if (searchConfigurationName) {
                        url += "&searchConfigurationName=" + encodeURIComponent(searchConfigurationName);
                    }

                    if (contentTypes) {
                        url += "&contentTypes=" + encodeURIComponent(contentTypes);
                    }

                    if (lastModified) {
                        url += "&lastModified=" + encodeURIComponent(lastModified);
                    }

                    return url;
                } else {
                    var params = new URLSearchParams(window.location.search);
                    params.set("knowledgeBoxName", knowledgeBoxName);
                    params.set("searchQuery", trimmedQuery);

                    if (searchConfigurationName) {
                        params.set("searchConfigurationName", searchConfigurationName);
                    } else {
                        params.delete("searchConfigurationName");
                    }

                    if (contentTypes) {
                        params.set("contentTypes", contentTypes);
                    } else {
                        params.delete("contentTypes");
                    }

                    if (lastModified) {
                        params.set("lastModified", lastModified);
                    } else {
                        params.delete("lastModified");
                    }

                    params.delete("page");
                    return window.location.pathname + "?" + params.toString();
                }
            }

            function sendAnalytics(query) {
                if (window.DataIntelligenceSubmitScript) {
                    DataIntelligenceSubmitScript._client.fetchClient.sendInteraction({
                        P: "Search for",
                        O: query,
                        OM: {
                            PageUrl: location.href
                        }
                    });
                }
            }
        }

        function initAutocomplete(container, inputField, handleSearch) {
            var activeAttribute = "data-sf-active";
            var dataSfItemAttribute = "data-sfitem";
            var activeClassElement = findInContainerOrSelf(container, "[data-sf-active-class]");
            var activeClass = activeClassElement && activeClassElement.dataset && isNotEmpty(activeClassElement.dataset.sfActiveClass) ? activeClassElement.dataset.sfActiveClass : null;
            var activeClassProcessed = activeClass ? processCssClass(activeClass) : null;
            var visibilityClassElement = findInContainerOrSelf(container, "[data-sf-visibility-hidden]");
            var visibilityClassHidden = visibilityClassElement && visibilityClassElement.dataset ? visibilityClassElement.dataset.sfVisibilityHidden : null;
            var searchAutocompleteItemElement = findInContainerOrSelf(container, "[data-sf-search-autocomplete-item-class]");
            var autocompleteItemClass = searchAutocompleteItemElement && searchAutocompleteItemElement.dataset ? searchAutocompleteItemElement.dataset.sfSearchAutocompleteItemClass : null;
            var autocompleteItemClassProcessed = autocompleteItemClass ? processCssClass(autocompleteItemClass) : null;
            var suggestionsDropdown = container.querySelector("ul[data-sf-role='suggestionsDropdown']");
            var suggestions = [];

            function findInContainerOrSelf(el, selector) {
                if (el.matches && el.matches(selector)) return el;
                return el.querySelector(selector);
            }

            function processCssClass(str) {
                var classList = str.split(" ");
                return classList;
            }

            function isNotEmpty(attr) {
                return (attr && attr !== "");
            }

            function setSource(items) {
                items = Array.isArray(items) ? items : [];
                suggestions = generateDropdownItems(items);

                clearDropdown();

                for (var i = 0; i < suggestions.length; i++) {
                    if (suggestionsDropdown) {
                        suggestionsDropdown.appendChild(suggestions[i]);
                    }
                }

                if (suggestions.length) {
                    show();
                } else {
                    hide();
                }
            }

            function clearDropdown() {
                if (suggestionsDropdown) {
                    var child = suggestionsDropdown.lastElementChild;
                    while (child) {
                        suggestionsDropdown.removeChild(child);
                        child = suggestionsDropdown.lastElementChild;
                    }
                }
            }

            function generateDropdownItems(suggestions) {
                var dropDownItems = [];

                if (Array.isArray(suggestions) && suggestions.length > 0) {
                    for (var i = 0; i < suggestions.length; i++) {
                        var dropdownItem = document.createElement("LI");
                        dropdownItem.setAttribute("role", "option");
                        var item = document.createElement("BUTTON");

                        if (autocompleteItemClassProcessed) {
                            item.classList.add.apply(item.classList, autocompleteItemClassProcessed);
                        }

                        item.setAttribute(dataSfItemAttribute, "");
                        item.innerText = suggestions[i];
                        item.title = suggestions[i];
                        dropdownItem.appendChild(item);
                        dropDownItems.push(dropdownItem);
                    }
                }

                return dropDownItems;
            }

            function suggestionsKeyupHandler(e) {
                var key = e.keyCode;
                var activeLinkSelector = "[" + dataSfItemAttribute + "][" + activeAttribute + "]";
                var activeLink = suggestionsDropdown.querySelector(activeLinkSelector);
                if (!activeLink) {
                    return;
                }

                var previousParent = activeLink.parentElement.previousElementSibling;
                var nextParent = activeLink.parentElement.nextElementSibling;
                if (key == 38 && previousParent) {
                    focusItem(previousParent);
                } else if (key == 40 && nextParent) {
                    focusItem(nextParent);
                } else if (key == 13) {
                    inputField.value = activeLink.innerText;
                    handleSearch(e);
                    hide();
                    inputField.focus();
                } else if (key == 27) {
                    resetActiveClass();
                    hide(false);
                    inputField.focus();
                }
            }

            function resetActiveClass() {
                var activeLink = suggestionsDropdown.querySelector("[" + activeAttribute + "]");

                if (activeLink && activeClassProcessed) {
                    activeLink.classList.remove.apply(activeLink.classList, activeClassProcessed);
                    activeLink.removeAttribute(activeAttribute);
                }
            }

            function suggestionsClickHandler(e) {
                var target = e.target;
                var content = target.innerText;
                inputField.value = content;
                handleSearch(e);
                hide();
            }

            function inputKeyupHandler(e) {
                if (e.keyCode == 40 && suggestions.length) {
                    show();
                    focusItem(suggestions[0]);
                }
            }

            function show() {
                setAutocompleteDropdownWidth();

                if (suggestionsDropdown) {
                    if (visibilityClassHidden) {
                        suggestionsDropdown.classList.remove(visibilityClassHidden);
                    } else {
                        suggestionsDropdown.style.display = "";
                    }
                }
            }

            function hide(clear) {
                if (clear === undefined) {
                    clear = true;
                }

                if (clear) {
                    clearDropdown();
                }
                clearAutocompleteDropdownWidth();

                if (suggestionsDropdown) {
                    if (visibilityClassHidden) {
                        suggestionsDropdown.classList.add(visibilityClassHidden);
                    } else {
                        if (suggestionsDropdown !== null)
                            suggestionsDropdown.style.display = "none";
                    }
                }
            }

            function dropdownFocusout(e) {
                if (suggestionsDropdown !== null && !suggestionsDropdown.contains(e.relatedTarget)) {
                    hide(false);
                }
            }

            function focus() {
                if (suggestionsDropdown && suggestionsDropdown.children.length) {
                    suggestionsDropdown.children[0].querySelector("[" + dataSfItemAttribute + "]").focus();
                }
            }

            function focusItem(item) {
                resetActiveClass();

                var link = item.querySelector("[" + dataSfItemAttribute + "]");

                if (link && activeClassProcessed) {
                    link.classList.add.apply(link.classList, activeClassProcessed);
                }

                // set data attribute, to be used in queries instead of class
                link.setAttribute(activeAttribute, "");

                link.focus();
            }

            function setAutocompleteDropdownWidth() {
                var inputWidth = inputField.clientWidth;
                var offset = 20; // to account for padding

                if (suggestionsDropdown) {
                    suggestionsDropdown.style.width = (inputWidth - offset) + "px";
                    suggestionsDropdown.style.marginLeft = (offset / 2) + "px";
                }
            }

            function clearAutocompleteDropdownWidth() {
                if (suggestionsDropdown !== null)
                    suggestionsDropdown.style.width = "";
            }

            inputField.addEventListener("keyup", inputKeyupHandler);
            inputField.addEventListener("focusout", dropdownFocusout);

            if (suggestionsDropdown) {
                suggestionsDropdown.addEventListener("focusout", dropdownFocusout);
                suggestionsDropdown.addEventListener("keyup", suggestionsKeyupHandler);
                suggestionsDropdown.addEventListener("click", suggestionsClickHandler);
            }

            return {
                source: setSource,
                show: show,
                hide: hide,
                clear: clearDropdown,
                focus: focus
            };
        }
    };

    if (window.personalizationManager) {
        window.personalizationManager.addPersonalizedContentLoaded(function () {
            init();
        });
    } else {
        document.addEventListener('DOMContentLoaded', function () {
            init();
        });
        document.addEventListener('widgetLoaded', function () {
            init();
        });
    }
}());
