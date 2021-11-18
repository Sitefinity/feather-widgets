﻿(function ($) {
    angular.module('designer').requires.push('expander', 'sfSelectors');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        var emptyGuid = '00000000-0000-0000-0000-000000000000';
        var sortOptions = ['PublicationDate DESC', 'LastModified DESC', 'Title ASC', 'Title DESC', 'EventStart ASC', 'EventStart DESC', 'AsSetManually'];

        $scope.eventSelector = {
            selectedItemsIds: []
        };

        $scope.feedback.showLoadingIndicator = true;

        $scope.changeFilteredSelectionMode = function (filteredSelectionMode) {
            if ($scope.properties) {
                $scope.properties.SelectionMode.PropertyValue = 'FilteredItems';
                $scope.properties.FilteredSelectionMode.PropertyValue = filteredSelectionMode;
            }
        };

        $scope.isFilteredSelectionModeChecked = function (filteredSelectionMode) {
            if ($scope.properties && $scope.properties.SelectionMode.PropertyValue == "FilteredItems") {
                return $scope.properties.FilteredSelectionMode.PropertyValue === filteredSelectionMode;
            }
        };

        $scope.$watch(
            'eventSelector.selectedItemsIds',
            function (newVal, oldVal) {
                if (newVal !== oldVal) {
                    if (newVal) {
                        $scope.properties.SerializedSelectedItemsIds.PropertyValue = JSON.stringify(newVal);
                        if (newVal.length === 1) {
                            $scope.properties.ContentViewDisplayMode.PropertyValue = 'Detail';
                        }
                    }
                }
            },
        true
        );

        $scope.$watch(
            'properties.SelectionMode.PropertyValue',
            function (newVal, oldVal) {
                if (newVal !== oldVal) {
                    if (newVal == 'SelectedItems') {
                        $scope.selectedSortOption = 'AsSetManually';
                    }
                    else {
                        $scope.selectedSortOption = 'EventStart ASC';
                    }
                }
            },
        true
        );

        $scope.additionalFilters = {};
        $scope.$watch(
            'additionalFilters.value',
            function (newAdditionalFilters, oldAdditionalFilters) {
                if (newAdditionalFilters !== oldAdditionalFilters) {
                    $scope.properties.SerializedAdditionalFilters.PropertyValue = JSON.stringify(newAdditionalFilters);
                }
            },
            true
        );

        $scope.narrowFilters = {};
        $scope.$watch(
            'narrowFilters.value',
            function (newNarrowFilters, oldNarrowFilters) {
                if (newNarrowFilters !== oldNarrowFilters) {
                    $scope.properties.SerializedNarrowSelectionFilters.PropertyValue = JSON.stringify(newNarrowFilters);
                }
            },
            true
        );

        $scope.updateSortOption = function (newSortOption) {
            if (newSortOption !== "Custom") {
                $scope.selectedSortOption = newSortOption;
                $scope.properties.SortExpression.PropertyValue = newSortOption;
            }
        };

        propertyService.get()
            .then(function (data) {
                if (data) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);

                    var additionalFilters = $.parseJSON($scope.properties.SerializedAdditionalFilters.PropertyValue || null);
                    $scope.additionalFilters.value = additionalFilters;

                    var narrowFilters = $.parseJSON($scope.properties.SerializedNarrowSelectionFilters.PropertyValue || null);
                    $scope.narrowFilters.value = narrowFilters;

                    var selectedItemsIds = $.parseJSON($scope.properties.SerializedSelectedItemsIds.PropertyValue || null);
                    if (selectedItemsIds) {
                        $scope.eventSelector.selectedItemsIds = selectedItemsIds;
                    }

                    if ($scope.properties.SortExpression.PropertyValue === '') {
                        if ($scope.properties.SelectionMode.PropertyValue === 'SelectedItems') {
                            $scope.selectedSortOption = 'AsSetManually';
                        }
                        else {
                            $scope.selectedSortOption = 'EventStart ASC';
                        }
                    }
                    else if (sortOptions.indexOf($scope.properties.SortExpression.PropertyValue) >= 0) {
                        $scope.selectedSortOption = $scope.properties.SortExpression.PropertyValue;
                    }
                    else {
                        $scope.selectedSortOption = 'Custom';
                    }
                }
            },
            function (data) {
                $scope.feedback.showError = true;
                if (data)
                    $scope.feedback.errorMessage = data.Detail;
            })
            .then(function () {
                $scope.feedback.savingHandlers.push(function () {
                    if ($scope.properties.OpenInSamePage.PropertyValue && $scope.properties.OpenInSamePage.PropertyValue.toLowerCase() === 'true') {
                        $scope.properties.DetailsPageId.PropertyValue = emptyGuid;
                    }
                    else if (!$scope.properties.DetailsPageId.PropertyValue || $scope.properties.DetailsPageId.PropertyValue === emptyGuid) {
                        $scope.properties.OpenInSamePage.PropertyValue = true;
                    }

                    if ($scope.properties.SelectionMode.PropertyValue !== 'SelectedItems') {
                        $scope.properties.SerializedSelectedItemsIds.PropertyValue = null;

                        // If the sorting expression is AsSetManually but the selection mode is AllItems or FilteredItems, this is not a valid combination.
                        // So set the sort expression to the default value: PublicationDate DESC
                        if ($scope.properties.SortExpression.PropertyValue === 'AsSetManually') {
                            $scope.properties.SortExpression.PropertyValue = 'EventStart ASC';
                            $scope.selectedSortOption = 'EventStart ASC';
                        }
                    }
                    else {
                        $scope.properties.SerializedAdditionalFilters.PropertyValue = null;
                        $scope.properties.SerializedNarrowSelectionFilters.PropertyValue = null;
                    }
                     
                    if ($scope.properties.ContentViewDisplayMode.PropertyValue === 'Detail' && 
                    		($scope.properties.SelectionMode.PropertyValue !== 'SelectedItems' || $scope.eventSelector.selectedItemsIds.length !== 1)) {
                        $scope.properties.ContentViewDisplayMode.PropertyValue = 'Automatic';
                    }  

                });
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})(jQuery);