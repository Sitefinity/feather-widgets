﻿(function ($) {
    angular.module('designer').requires.push('expander', 'sfSelectors');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        var sortOptions = ['PublicationDate DESC', 'LastModified DESC', 'Title ASC', 'Title DESC', 'AsSetManually'];
        var emptyGuid = '00000000-0000-0000-0000-000000000000';

        $scope.blogSelector = { selectedItemsIds: [] };
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

        $scope.updateSortOption = function (newSortOption) {
            if ($scope.properties && newSortOption !== "Custom") {
                $scope.properties.SortExpression.PropertyValue = newSortOption;
            }
        };

        $scope.$watch(
            'blogSelector.selectedItemsIds',
            function (newVal, oldVal) {
                if (newVal !== oldVal) {
                    if ($scope.properties && newVal) {
                        $scope.properties.SerializedSelectedItemsIds.PropertyValue = JSON.stringify(newVal);
                        if (newVal.length === 1) {
                            $scope.properties.ContentViewDisplayMode.PropertyValue = "Detail";
                        }
                        else {
                            $scope.properties.ContentViewDisplayMode.PropertyValue = "Automatic";
                        }
                    }
                }
            },
            true
        );

        $scope.$watch(
            'properties.SelectionMode.PropertyValue',
            function (newSelectionModeValue, oldSelectionModeValue) {
                if (newSelectionModeValue !== oldSelectionModeValue) {
                    if ($scope.properties.ContentViewDisplayMode.PropertyValue.toLowerCase() === "Detail".toLowerCase() && newSelectionModeValue !== "SelectedItems") {
                        $scope.properties.ContentViewDisplayMode.PropertyValue = "Automatic";
                    }
                }
            },
            true
        );

        propertyService.get()
            .then(function (data) {
                if (data && data.Items) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);

                    var selectedItemsIds = $.parseJSON($scope.properties.SerializedSelectedItemsIds.PropertyValue || null);
                    if (selectedItemsIds) {
                        $scope.blogSelector.selectedItemsIds = selectedItemsIds;
                    }

                    if (sortOptions.indexOf($scope.properties.SortExpression.PropertyValue) >= 0) {
                        $scope.selectedSortOption = $scope.properties.SortExpression.PropertyValue;
                    }
                    else {
                        $scope.selectedSortOption = "Custom";
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
                    if ($scope.properties.DetailPageMode.PropertyValue && $scope.properties.DetailPageMode.PropertyValue != 'SelectedExistingPage') {
                        $scope.properties.DetailsPageId.PropertyValue = emptyGuid;
                    }
                    else {
                        if (!$scope.properties.DetailsPageId.PropertyValue ||
                                $scope.properties.DetailsPageId.PropertyValue === emptyGuid) {
                            $scope.properties.DetailPageMode.PropertyValue = 'SamePage';
                        }
                    }

                    if ($scope.properties.SelectionMode.PropertyValue !== 'SelectedItems') {
                        $scope.properties.SerializedSelectedItemsIds.PropertyValue = null;

                        // If the sorting expression is AsSetManually but the selection mode is AllItems or FilteredItems, this is not a valid combination.
                        // So set the sort expression to the default value: PublicationDate DESC
                        if ($scope.properties.SortExpression.PropertyValue === "AsSetManually") {
                            $scope.properties.SortExpression.PropertyValue = "PublicationDate DESC";
                        }

                        if ($scope.properties.ContentViewDisplayMode.PropertyValue.toLowerCase() === "Detail".toLowerCase()) {
                            $scope.properties.SelectionMode.PropertyValue = "SelectedItems";
                        }
                    }

                    // Set MaxPostsAge to 1 if not used
                    if ($scope.properties.SelectionMode.PropertyValue !== 'FilteredItems' || $scope.properties.FilteredSelectionMode.PropertyValue === 'MinPostsCount') {
                        $scope.properties.MaxPostsAge.PropertyValue = 1;
                    }

                    // Set MinPostsCount to 0 if not used
                    if ($scope.properties.SelectionMode.PropertyValue !== 'FilteredItems' || $scope.properties.FilteredSelectionMode.PropertyValue === 'MaxPostsAge') {
                        $scope.properties.MinPostsCount.PropertyValue = 0;
                    } 
                });
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})(jQuery);
