﻿(function ($) {
    angular.module('designer').requires.push('expander', 'sfSelectors');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        var emptyGuid = '00000000-0000-0000-0000-000000000000';

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
                    if ($scope.properties && newVal) {
                        $scope.properties.SerializedSelectedItemsIds.PropertyValue = JSON.stringify(newVal);
                    }
                }
            },
        true
        );

        $scope.additionalFilters = {};
        $scope.$watch(
            'additionalFilters.value',
            function (newAdditionalFilters, oldAdditionalFilters) {
                if ($scope.properties && newAdditionalFilters !== oldAdditionalFilters) {
                    $scope.properties.SerializedAdditionalFilters.PropertyValue = JSON.stringify(newAdditionalFilters);
                }
            },
            true
        );

        $scope.narrowFilters = {};
        $scope.$watch(
            'narrowFilters.value',
            function (newNarrowFilters, oldNarrowFilters) {
                if ($scope.properties && newNarrowFilters !== oldNarrowFilters) {
                    $scope.properties.SerializedNarrowSelectionFilters.PropertyValue = JSON.stringify(newNarrowFilters);
                }
            },
            true
        );

        propertyService.get()
            .then(function (data) {
                if (data && data.Items) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);

                    var additionalFilters = $.parseJSON($scope.properties.SerializedAdditionalFilters.PropertyValue || null);
                    $scope.additionalFilters.value = additionalFilters;

                    var narrowFilters = $.parseJSON($scope.properties.SerializedNarrowSelectionFilters.PropertyValue || null);
                    $scope.narrowFilters.value = narrowFilters;

                    var selectedItemsIds = $.parseJSON($scope.properties.SerializedSelectedItemsIds.PropertyValue || null);
                    if (selectedItemsIds) {
                        $scope.eventSelector.selectedItemsIds = selectedItemsIds;
                    }
                }
            },
            function (errorData) {
                $scope.feedback.showError = true;
                if (errorData && errorData.data)
                    $scope.feedback.errorMessage = errorData.data.Detail;
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