(function () {
    function init() {
        var FILTER_QUERY_PARAM = "filter";
        var RANGE_SEPARATOR = "__sf-range__";
        var DATE_AND_TIME = "DateAndTime";

        var facetWidgetWrapper = document.getElementById("facetContainer");
        var appliedFiltersContainer = document.getElementById("applied-filters");

        var defaultFacetsCollapseCount = 10;
        if (facetWidgetWrapper) {

            facetWidgetWrapper.addEventListener('change', function (ev) {
                if (ev && ev.target && ev.target.attributes["data-custom-range"]) {
                    return;
                }

                processSelectedFilter(ev.target);
                var filterObject = buildFilterObjectBasedOnPopulatedInputs(facetWidgetWrapper, ev.target);
                searchWithFilter(filterObject);
            });

            var clearAllBtn = document.getElementById("sf-facet-clear-all-btn");
            if (clearAllBtn) {
                clearAllBtn.addEventListener("click", function () {
                    var checkedFilters = facetWidgetWrapper.querySelectorAll('input[type="checkbox"]:checked');
                    checkedFilters.forEach(function (checkedInput) {
                        checkedInput.checked = false;
                    });

                    var customRangeConainterToBeCleared = facetWidgetWrapper.querySelectorAll('input[data-custom-range]');
                    if (customRangeConainterToBeCleared) {
                        customRangeConainterToBeCleared.forEach(function (el) {
                            el.value = "";
                        });
                    }

                    appliedFiltersContainer.innerText = "";
                    var filterObject = buildFilterObjectBasedOnPopulatedInputs(facetWidgetWrapper, null);
                    searchWithFilter(filterObject);
                });
            }

            var showAllFacetsFields = document.querySelectorAll('[id^="show-more-less"]');
            var customSearchButton = document.querySelectorAll('[id^="custom-range"]');

            if (showAllFacetsFields || customSearchButton) {
                facetWidgetWrapper.addEventListener('click', function (ev) {
                    if (ev.target && ev.target.id.startsWith("show-more-less")) {
                        var facetKey = ev.target.attributes["data-facet-type"].value;
                        var showLessText = ev.target.attributes["show-less"].value;
                        var showMoreText = ev.target.attributes["show-more"].value;

                        var ulFacetListId = "facets-group-list-" + facetKey;
                        var facetList = Array.from(document.querySelectorAll("#" + ulFacetListId + ">li"));
                        addOrRemoveHiddenAttributeInCollection(facetList, ev, showLessText, showMoreText);
                    }

                    var customRangeButton = ev.target.id.startsWith("custom-range-btn") ?
                        ev.target :
                        ev.target.closest('[id^="custom-range-btn"]');

                    if (customRangeButton) {
                        customRangeSelectedEventHandler(customRangeButton);
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

                if (ev.target.hasAttribute("data-selected-custom-range")) {
                    var customRangeContainerId = "facets-group-list-" + facetKey;
                    var customRangeConainterToBeCleared = document.querySelectorAll("#" + customRangeContainerId + " input");
                    if (customRangeConainterToBeCleared) {
                        customRangeConainterToBeCleared.forEach(function (el) {
                            el.value = "";
                        });
                    }
                } else {
                    var inputId = "facet-checkbox-" + facetKey + "-" + facetValue;
                    var facetCheckedInputEl = document.getElementById(inputId);
                    if (facetCheckedInputEl) {
                        facetCheckedInputEl.checked = false;
                    }
                }
                facetWidgetWrapper.dispatchEvent(new Event("change"));
            });
        }

        markSelectedInputs(true);

        showHideShowMoreLessButtons();

        function searchWithFilter(filterObject) {
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
        }

        function uncheckCheckboxesFromGroup(facetFieldName) {
            var facetListelement = document.getElementById("facets-group-list-" + facetFieldName);
            if (facetListelement) {
                var checkedFilters = facetListelement.querySelectorAll('input[type="checkbox"]:checked');
                checkedFilters.forEach(function (el) {
                    el.checked = false;
                    processSelectedFilter(el);
                });
            }
        }

        function computeFacetRangeValueForType(fieldType, fromValue, toValue) {
            if (fieldType === DATE_AND_TIME) {
                var fromdate = new Date(fromValue);
                fromdate.setHours(0);
                var toDate = new Date(toValue);
                toDate.setHours(0);

                return fromdate.toISOString() + RANGE_SEPARATOR + toDate.toISOString();
            }

            return fromValue + RANGE_SEPARATOR + toValue;
        }

        function computeFacetRangeLabelForType(fieldType, fromValue, toValue) {
            if (fieldType === DATE_AND_TIME) {
                var fromDateTime = new Date(fromValue);
                var dateOptions = { month: "short", day: "numeric" };
                var fromString = fromDateTime.toLocaleString(undefined, dateOptions) + " " + fromDateTime.getFullYear();

                var toDateTime = new Date(toValue);
                var toString = toDateTime.toLocaleString(undefined, dateOptions) + " " + toDateTime.getFullYear();

                return fromString + " - " + toString;
            }

            return fromValue + " - " + toValue;
        }

        function customRangeSelectedEventHandler(element) {
            var facetFieldAttribute = element.attributes["data-custom-range-name"];
            var facetFieldTypeAttribute = element.attributes["data-custom-range-type"];
            if (facetFieldAttribute && facetFieldTypeAttribute) {
                var facetFieldName = facetFieldAttribute.value;
                var facetFieldType = facetFieldTypeAttribute.value;
                if (facetFieldName && facetFieldType) {
                    var fromInput = document.getElementById("from-" + facetFieldName);
                    var toInput = document.getElementById("to-" + facetFieldName);

                    if (fromInput && toInput) {
                        uncheckCheckboxesFromGroup(facetFieldName);

                        var fromValue = fromInput.value;
                        var toValue = toInput.value;

                        if (fromValue !== null && fromValue !== undefined && fromValue !== "" && toValue !== null && toValue !== undefined && toValue !== "") {
                            var facetChipValue = computeFacetRangeValueForType(facetFieldType, fromValue, toValue);
                            var facetChipLabel = computeFacetRangeLabelForType(facetFieldType, fromValue, toValue);

                            // if the entered custom range exist in the generated facet - select them and don't create Custom range applied element
                            var inputId = "facet-checkbox-" + facetFieldName + "-" + facetChipValue;
                            var isCustomFacetRange = true;
                            var generatedFacetCheckBox = document.getElementById(inputId);
                            if (generatedFacetCheckBox) {
                                isCustomFacetRange = false;
                                generatedFacetCheckBox.checked = true;
                            }

                            appendAppliedFilterElement(facetFieldName, facetChipValue, facetChipLabel, isCustomFacetRange);
                            var checkedFacetInputs = groupAllCheckedFacetInputs();
                            appendCustomRangesToCheckedFacetInputs(checkedFacetInputs);

                            var filterObjectWithCustomFilter = constructFilterObject(checkedFacetInputs, facetFieldName, false);
                            searchWithFilter(filterObjectWithCustomFilter);
                        }
                    }
                }
            }
        }

        function appendCustomRangesToCheckedFacetInputs(checkedFacetInputs) {
            var appliedCustomRangesChips = document.querySelectorAll("[data-selected-custom-range]");
            appliedCustomRangesChips.forEach(function (customRangeContainer) {
                var customRangeValuesAttr = customRangeContainer.attributes["data-facet-value"];
                var customRangeFilterKeyAttr = customRangeContainer.attributes["data-facet-key"];

                if (customRangeValuesAttr && customRangeFilterKeyAttr) {
                    var filterValue = customRangeValuesAttr.value;
                    var filterKey = customRangeFilterKeyAttr.value;

                    var filterValueObj = {
                        filterValue: filterValue,
                        isCustom: true
                    };
                    if (checkedFacetInputs.hasOwnProperty(filterKey)) {
                        checkedFacetInputs[filterKey].push(filterValueObj);
                    } else {
                        checkedFacetInputs[filterKey] = [filterValueObj];
                    }
                }
            });
        }

        function appendAppliedFilterElement(facetKey, facetValue, facetLabel, isCustomRange) {
            var elementId = buildRemoveFacetFilterId(facetKey, facetValue);
            var customFilterAlreadyApplied = document.getElementById(elementId) !== null;
            if (!customFilterAlreadyApplied) {
                // remove other custom filters for the same facet group before adding the new one
                removeCustomRangeChipsForGroup(facetKey);

                var newFilter = createAppliedFilterElementInternal(elementId, facetKey, facetValue, facetLabel, isCustomRange);
                appliedFiltersContainer.appendChild(newFilter);
            }
        }

        function removeCustomRangeChipsForGroup(groupName) {
            var appliedCustomRangesChips = appliedFiltersContainer.querySelectorAll('[data-selected-custom-range][data-facet-key="' + groupName + '"]');
            if (appliedCustomRangesChips) {
                appliedCustomRangesChips.forEach(function (el) {
                    appliedFiltersContainer.removeChild(el.parentElement);
                });
            }
        }

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
            if (!element || element.tagName.toLowerCase() !== "input" || element.attributes["data-custom-range"]) {
                return;
            }

            var facetKey = element.attributes["data-facet-key"].value;
            var facetValue = element.attributes["data-facet-value"].value;
            var facetLabel = element.parentElement.getElementsByTagName('label')[0].textContent;
            var removeFacetSpanElementId = buildRemoveFacetFilterId(facetKey, facetValue);

            var facetFilterEl = document.getElementById(removeFacetSpanElementId);

            if (element.checked) {
                var newFilter = createAppliedFilterElementInternal(removeFacetSpanElementId, facetKey, facetValue, facetLabel, false);
                appliedFiltersContainer.appendChild(newFilter);

                // after we select a facet checkbox the custom range selection must be removed if exists
                removeCustomRangeChip(facetKey);
            } else {
                appliedFiltersContainer.removeChild(facetFilterEl.parentElement);
            }
        }

        function removeCustomRangeChip(facetKey) {
            var customRangeElementFacetKeyAttribute = "[data-facet-key='" + facetKey + "']";
            var customRangeElementQuerySelector = customRangeElementFacetKeyAttribute + "[data-selected-custom-range]";
            var customRangelementToRemove = document.querySelector(customRangeElementQuerySelector);
            if (customRangelementToRemove) {
                appliedFiltersContainer.removeChild(customRangelementToRemove.parentElement);
            }
        }

        function buildRemoveFacetFilterId(facetKey, facetValue) {
            return "remove-facet-filter-" + facetKey + "-" + facetValue;
        }

        function createAppliedFilterElementInternal(removeFilterId, facetKey, filterValue, facetLabel, isCustomRange) {
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
            if (isCustomRange) {
                removeButtonSpan.setAttribute("data-selected-custom-range", true);
            }

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
                    filter.filterValues.forEach(function (fvObj) {
                        var fieldName = decodeURIComponent(filter.fieldName);
                        var filterValue = decodeURIComponent(fvObj.filterValue);
                        var inputId = "facet-checkbox-" + fieldName + "-" + filterValue;
                        var currentInputElement = document.getElementById(inputId);

                        if (!currentInputElement) {
                            // try to find with different decimal point separator
                            inputId = inputId.replace(/\./g, ",");
                            currentInputElement = document.getElementById(inputId);
                        }

                        if (currentInputElement) {
                            currentInputElement.checked = true;
                            if (createAppliedFiltersChips) {
                                processSelectedFilter(currentInputElement);
                            }
                        } else {
                            var facetElementListId = "facets-group-list-" + fieldName;
                            var facetElementList = document.getElementById(facetElementListId);
                            if (facetElementList) {
                                var facetTypeAttr = facetElementList.attributes["data-facet-type"];
                                if (facetTypeAttr) {
                                    var facetType = facetTypeAttr.value;
                                    if (facetType) {
                                        populateCustomRangeInputs(fieldName, filterValue, facetType);
                                    }
                                }
                            }
                        }
                    });
                });
            }
        }

        function convertToDatePickerInputValueFromUtcString(dateString) {
            var dateObject = new Date(dateString);
            var month = dateObject.getMonth() + 1;
            month = month.toString();
            if (month.length !== 2) {
                month = "0" + month;
            }
            var date = dateObject.getDate().toString();
            if (date.length !== 2) {
                date = "0" + date;
            }

            var result = dateObject.getFullYear() + "-" + month + "-" + date;

            return result;
        }

        function populateCustomRangeInputs(fieldName, filterValue, facetType) {
            var fromCustomInput = document.getElementById("from-" + fieldName);
            var toCustomInput = document.getElementById("to-" + fieldName);
            if (fromCustomInput && toCustomInput) {
                var splittedFilterValue = filterValue.split(RANGE_SEPARATOR);
                if (splittedFilterValue && splittedFilterValue.length > 0) {
                    var fromValueToSet = splittedFilterValue[0];
                    var toValueToSet = splittedFilterValue[1];

                    if (fromValueToSet !== null && fromValueToSet !== undefined && toValueToSet !== null && toValueToSet !== undefined) {
                        if (facetType === DATE_AND_TIME) {
                            fromValueToSet = convertToDatePickerInputValueFromUtcString(fromValueToSet);
                            toValueToSet = convertToDatePickerInputValueFromUtcString(toValueToSet);
                        }
                        fromCustomInput.value = fromValueToSet;
                        toCustomInput.value = toValueToSet;

                        var facetChipLabel = computeFacetRangeLabelForType(facetType,fromValueToSet, toValueToSet);
                        appendAppliedFilterElement(fieldName, filterValue, facetChipLabel, true);
                    }
                }
            }
        }

        function buildFilterObjectBasedOnPopulatedInputs(facetWidgetWrapper, eventTargetElement) {
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

                appendCustomRangesToCheckedFacetInputs(groupedFilters);
                var filterObject = constructFilterObject(groupedFilters, lastSelectedElementKey, isDeselected);

                return filterObject;
            }
        }

        function constructFilterObject(groupedFilters, lastSelectedElementKey, isDeselected) {
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

        function addOrRemoveHiddenAttributeInCollection(facetList, ev, showLessText, showMoreText) {
            var isListHasHiddenAttributes = facetList.some(function (listElement) {
                return listElement.hasAttribute("hidden");
            });

            if (isListHasHiddenAttributes) {
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

                    var filterValueObj = {
                        filterValue: filterValue,
                        isCustom: false
                    };

                    if (groupedFilters.hasOwnProperty(filterKey)) {
                        groupedFilters[filterKey].push(filterValueObj);
                    } else {
                        groupedFilters[filterKey] = [filterValueObj];
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
