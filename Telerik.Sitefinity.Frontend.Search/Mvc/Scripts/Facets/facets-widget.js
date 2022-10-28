(function () {
    function init() {
        var FILTER_QUERY_PARAM = "filter";

        var facetWidgetWrapper = document.getElementById("facetContainer");
        var appliedFiltersContainer = document.getElementById("applied-filters");

        var defaultFacetsCollapseCount = 10;
        if (facetWidgetWrapper) {

            facetWidgetWrapper.addEventListener('change', function (ev) {
                processSelectedFilter(ev.target);

                var filterObject = getAllCheckedFacetInputs(facetWidgetWrapper, ev.target);
                var filterString = JSON.stringify(filterObject);
                var query = window.location.search;
                var queryStringParser = new Querystring(query.substring(1, query.length));

                if (filterObject && filterObject.appliedFilters && filterObject.appliedFilters.length > 0) {
                    var encodedFilterString = btoa(filterString);
                    queryStringParser.set(FILTER_QUERY_PARAM, encodedFilterString);
                    toggleClearAllVisibility(true);

                } else {
                    queryStringParser.remove(FILTER_QUERY_PARAM);
                    toggleClearAllVisibility(false);
                }

                var url = buildUrl(queryStringParser);

                loadDataAsync(url);
                window.history.pushState({ path: url }, "", url);
            });

            var clearAllBtn = document.getElementById("sf-facet-clear-all-btn");
            if (clearAllBtn) {
                clearAllBtn.addEventListener("click", function () {
                    var checkedFilters = facetWidgetWrapper.querySelectorAll('input[type="checkbox"]:checked');
                    checkedFilters.forEach(function (checkedInput) {
                        checkedInput.checked = false;
                    });

                    facetWidgetWrapper.dispatchEvent(new Event("change"));

                    appliedFiltersContainer.innerText = "";
                });
            }

            var showAllFacetsFields = document.querySelectorAll('[id^="show-more-less"]');
            if (showAllFacetsFields) {
                facetWidgetWrapper.addEventListener('click', function (ev) {
                    if (ev.target && ev.target.id.startsWith("show-more-less")) {
                        var facetKey = ev.target.attributes["data-facet-type"].value;
                        var showLessText = ev.target.attributes["show-less"].value;
                        var showMoreText = ev.target.attributes["show-more"].value;

                        var ulFacetListId = "facets-group-list-" + facetKey;
                        var facetList = Array.from(document.querySelectorAll("#" + ulFacetListId + ">li"));
                        addOrRemoveHiddenAttributeInCollection(facetList, ev, showLessText, showMoreText);
                    }
                });
            }
        }

        if (appliedFiltersContainer) {
            appliedFiltersContainer.addEventListener('click', function (ev) {
                if (!ev.target || !ev.target.hasAttribute("data-facet-key")) {
                    return;
                }
                var spanToRemove = ev.target.parentElement;
                appliedFiltersContainer.removeChild(spanToRemove);

                var facetKey = ev.target.attributes["data-facet-key"].value;
                var facetValue = ev.target.attributes["data-facet-value"].value;
                var inputId = "facet-checkbox-" + facetKey + "-" + facetValue;
                var facetCheckedInputEl = document.getElementById(inputId);
                facetCheckedInputEl.checked = false;
                facetWidgetWrapper.dispatchEvent(new Event("change"));
            });
        }

        markSelectedInputs(true);

        showHideShowMoreLessButtons();

        function toggleClearAllVisibility(shouldShow) {
            var clearAllBtn = document.getElementById("sf-facet-clear-all-btn");
            if (clearAllBtn) {
                if (shouldShow) {
                    clearAllBtn.removeAttribute('hidden');
                } else {
                    clearAllBtn.setAttribute('hidden', "true");
                }
            }
        }

        function buildUrl(queryStringParser) {
            var currentLocation = window.location.href.split('?')[0];

            // return the pager to 0
            var currentPageUrlElement = document.getElementById("sf-currentPageUrl");
            if (currentPageUrlElement) {
                currentLocation = currentPageUrlElement.value;
            }

            var url = currentLocation + "?" + queryStringParser.toString();

            return url;
        }

        function processSelectedFilter(element) {
            if (!element || element.tagName.toLowerCase() !== "input") {
                return;
            }

            var facetKey = element.attributes["data-facet-key"].value;
            var facetValue = element.attributes["data-facet-value"].value;
            var facetLabel = element.parentElement.getElementsByTagName('label')[0].textContent;
            var removeFilterId = "remove-facet-filter-" + facetKey + "-" + facetValue;
            if (element.checked) {
                var newFilter = createAppliedFilterElement(removeFilterId, facetKey, facetValue, facetLabel);
                appliedFiltersContainer.appendChild(newFilter);
            } else {
                var facetFilterEl = document.getElementById(removeFilterId);
                appliedFiltersContainer.removeChild(facetFilterEl.parentElement);
            }
        }

        function createAppliedFilterElement(removeFilterId, facetKey, filterValue, facetLabel) {
            var filterLabelClass = appliedFiltersContainer.attributes["data-sf-filter-label-css-class"].value;
            var appliedFilterHtmlTag = appliedFiltersContainer.attributes["data-sf-applied-filter-html-tag"].value || "span";
            var filterSpanEl = document.createElement(appliedFilterHtmlTag);

            filterSpanEl.setAttribute("class", filterLabelClass);
            filterSpanEl.innerText = facetLabel;

            var removeFilterClass = appliedFiltersContainer.attributes["data-sf-remove-filter-css-class"].value;
            var removeButtonSpan = document.createElement("span");
            removeButtonSpan.setAttribute("id", removeFilterId);
            removeButtonSpan.setAttribute("role", "button");
            removeButtonSpan.setAttribute("tabindex", "0");
            removeButtonSpan.setAttribute("title", "Remove");
            removeButtonSpan.setAttribute("class", removeFilterClass);
            removeButtonSpan.setAttribute("data-facet-key", facetKey);
            removeButtonSpan.setAttribute("data-facet-value", filterValue);
            removeButtonSpan.innerText = "✕";

            filterSpanEl.appendChild(removeButtonSpan);

            return filterSpanEl;
        }

        function loadDataAsync(url) {
            var xmlHttp = new XMLHttpRequest();
            xmlHttp.open("GET", url, true); // true for asynchronous 
            xmlHttp.setRequestHeader("Content-type", "application/json; charset=utf-8");
            xmlHttp.setRequestHeader("Accept", "application/json");
            document.dispatchEvent(new CustomEvent("beginLoadingSearchResults"));

            xmlHttp.onreadystatechange = function () {
                if (xmlHttp.readyState === 4 && xmlHttp.status === 200 && xmlHttp.responseText) {
                    var parser = new DOMParser();
                    var htmlDoc = parser.parseFromString(xmlHttp.responseText, 'text/html');

                    if (htmlDoc) {
                        document.dispatchEvent(new CustomEvent("searchResultsLoaded", {
                            detail: {
                                searchResultsPageDocument: htmlDoc,
                            }
                        }));

                        rebindSearchFacets(htmlDoc);
                    }
                }
            };

            xmlHttp.onerror = function () {
                document.dispatchEvent(new CustomEvent("searchResultsLoaded", {
                    detail: {
                        searchResultsPageDocument: null,
                    }
                }));
            };

            xmlHttp.send(null);
        }

        function rebindSearchFacets(htmlDoc) {
            var newSearchFacets = htmlDoc.getElementById("facetContent");
            var oldSearchFacetsContent = document.getElementById("facetContent");
            oldSearchFacetsContent.innerHTML = newSearchFacets.innerHTML;

            markSelectedInputs();
            showHideShowMoreLessButtons();
        }

        function markSelectedInputs(createAppliedFiltersChips) {
            var query = window.location.search;
            var queryStringParser = new Querystring(query.substring(1, query.length));
            var filterQuery = queryStringParser.get(FILTER_QUERY_PARAM);

            if (filterQuery) {
                toggleClearAllVisibility(true);
                var decodedFilterParam = atob(filterQuery);
                var jsonFilters = JSON.parse(decodedFilterParam);
                jsonFilters.appliedFilters.forEach(function (filter) {
                    filter.filterValues.forEach(function (filterValue) {
                        var inputId = "facet-checkbox-" + decodeURIComponent(filter.fieldName) + "-" + decodeURIComponent(filterValue);
                        var currentInputElement = document.getElementById(inputId);
                        if (currentInputElement) {
                            currentInputElement.checked = true;
                            if (createAppliedFiltersChips) {
                                processSelectedFilter(currentInputElement);
                            }
                        }
                    });
                });
            }
        }

        function getAllCheckedFacetInputs(facetWidgetWrapper, eventTargetElement) {
            
            if (facetWidgetWrapper) {
                var groupedFilters = groupAllCheckedFacetInputs();

                var lastSelectedElementKey;
                if (eventTargetElement && eventTargetElement.tagName.toLowerCase() === "input") {
                    lastSelectedElementKey = eventTargetElement.attributes["data-facet-key"].value;
                }

                var isDeselected = false;
                if (eventTargetElement && eventTargetElement.tagName.toLowerCase() === "input") {
                    lastSelectedElementKey = eventTargetElement.attributes["data-facet-key"].value;
                    isDeselected = !eventTargetElement.checked;
                }

                var filterObject = {
                    appliedFilters: Object.keys(groupedFilters).map(function (el) {
                        return {
                            fieldName: el,
                            filterValues: groupedFilters[el]
                        };
                    }),
                    lastSelectedFilterGropName: lastSelectedElementKey,
                    isDeselected: isDeselected
                };

                return filterObject;
            }
        }

        function addOrRemoveHiddenAttributeInCollection(facetList, ev, showLessText, showMoreText) {
            var isListHasHiddenAttributes = facetList.some(function (listElement) {
                return listElement.hasAttribute("hidden");
            });

            if (isListHasHiddenAttributes)
            {
                facetList.forEach(function (listElement) {
                    listElement.removeAttribute('hidden');
                });

                ev.target.innerText = showLessText;
            } else {
                facetList.slice(defaultFacetsCollapseCount).forEach(function (listElement) {
                    listElement.setAttribute('hidden', "true");
                });

                ev.target.innerText = showMoreText;
            }
        }

        function showHideShowMoreLessButtons() {

            if (facetWidgetWrapper) {
                var groupedFilters = groupAllCheckedFacetInputs();
                var groupedCheckedFacetsJson = parseGroupedFiltersToJson(groupedFilters);

                // Check if we need to show the 'Show less' or 'Show more' button for particular facet group
                groupedCheckedFacetsJson.forEach(function (facet) {
                    var facetName = facet.fieldName;
                    var button = document.getElementById("show-more-less-" + facetName);

                    if (button) {
                        var buttonText = button.attributes["show-less"].value;
                        var selectedFacetValues = facet.filterValues;
                        var ulFacetListId = "facets-group-list-" + facetName;
                        var facetList = Array.from(document.querySelectorAll("#" + ulFacetListId + ">li"));

                        // Set all facets values for particular group in array
                        var allFacetValuesInGroup = [];
                        facetList.forEach(function (listElement) {
                            var inputEl = listElement.getElementsByTagName('input')[0];
                            var facetValue = inputEl.attributes["data-facet-value"].value;
                            var encodeFacetValue = encodeURIComponent(facetValue);

                            allFacetValuesInGroup.push(encodeFacetValue);
                        });

                        // Remove the hidden attribute from the li elements when there is checked facet after the default hidden position. Which is 10.   
                        for (var selectedFacetIndex = 0; selectedFacetIndex < selectedFacetValues.length; selectedFacetIndex++) {
                            /*jshint loopfunc: true */
                            var index = allFacetValuesInGroup.indexOf(selectedFacetValues[selectedFacetIndex]);

                            if (index > defaultFacetsCollapseCount - 1) {
                                facetList.forEach(function (listElement) {
                                    listElement.removeAttribute('hidden');
                                    button.innerText = buttonText;
                                });

                                break;
                            }
                        }
                    }
                });
            }
        }

        function groupAllCheckedFacetInputs() {
            var groupedFilters = {};

            var checkedFilters = facetWidgetWrapper.querySelectorAll('input[type="checkbox"]:checked');
            if (checkedFilters) {
                checkedFilters.forEach(function (checkedFilter) {
                    var facetKey = checkedFilter.attributes["data-facet-key"].value;
                    var facetValue = checkedFilter.attributes["data-facet-value"].value;

                    var filterKey = encodeURIComponent(facetKey);
                    var filterValue = encodeURIComponent(facetValue);

                    if (groupedFilters.hasOwnProperty(filterKey)) {
                        groupedFilters[filterKey].push(filterValue);
                    } else {
                        groupedFilters[filterKey] = [filterValue];
                    }
                });
            }

            return groupedFilters;
        }

        function parseGroupedFiltersToJson(groupedFilters) {
            var jsonFilter = Object.keys(groupedFilters).map(function (el) {
                return {
                    fieldName: el,
                    filterValues: groupedFilters[el]
                };
            });

            return jsonFilter;
        }
    }

    document.addEventListener('DOMContentLoaded', function () {
        init();
    });
}());


