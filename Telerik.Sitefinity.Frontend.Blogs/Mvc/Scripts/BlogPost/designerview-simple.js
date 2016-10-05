﻿(function ($) {
    angular.module('designer').requires.push('expander', 'sfSelectors');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        var sortOptions = ['PublicationDate DESC', 'LastModified DESC', 'Title ASC', 'Title DESC', 'AsSetManually'];
        var emptyGuid = '00000000-0000-0000-0000-000000000000';

        $scope.blogPostSelector = { selectedItemsIds: [] };
        $scope.parentSelector = { selectedItemsIds: [] };
        $scope.feedback.showLoadingIndicator = true;
        $scope.additionalFilters = {};
        $scope.dateFilters = {};

        $scope.$watch(
            'blogPostSelector.selectedItemsIds',
            function (newVal, oldVal) {
                if (newVal !== oldVal) {
                    if (newVal) {
                        $scope.properties.SerializedSelectedItemsIds.PropertyValue = JSON.stringify(newVal);
                    }
                }
            },
            true
        );

        $scope.$watch(
	        'properties.ProviderName.PropertyValue',
	        function (newProviderName, oldProviderName) {
	            if (newProviderName !== oldProviderName) {
	                $scope.properties.ParentFilterMode.PropertyValue = 'All';
	                $scope.properties.SerializedSelectedParentsIds.PropertyValue = null;
	            }
	        },
	        true
        );

        $scope.$watch(
            'parentSelector.selectedItemsIds',
            function (newSelectedItemsIds, oldSelectedItemsIds) {
                if (newSelectedItemsIds !== oldSelectedItemsIds) {
                    $scope.properties.SerializedSelectedParentsIds.PropertyValue = JSON.stringify(newSelectedItemsIds);
                }
            }, 
            true
            );

        $scope.$watch(
            'additionalFilters.value',
            function (newAdditionalFilters, oldAdditionalFilters) {
                if (newAdditionalFilters !== oldAdditionalFilters) {
                    $scope.properties.SerializedAdditionalFilters.PropertyValue = JSON.stringify(newAdditionalFilters);
                }
            },
            true
        );

        $scope.$watch(
       'dateFilters.value',
        function (newDateFilters, oldDateFilters) {
            if (newDateFilters !== oldDateFilters) {
                $scope.properties.SerializedDateFilters.PropertyValue = JSON.stringify(newDateFilters);
            }
        },
        true
      );

        $scope.$watch(
            'properties.ParentFilterMode.PropertyValue',
            function (newValue, oldValue) {
                if (newValue !== oldValue) {
                    if (newValue == 'NotApplicable') {
                        $scope.properties.SelectionMode.PropertyValue = 'SelectedItems';
                    }
                    else if (oldValue == 'NotApplicable') {
                        $scope.properties.SelectionMode.PropertyValue = 'AllItems';
                    }
                }
            },
            true
        );

        $scope.updateSortOption = function (newSortOption) {
            if (newSortOption !== "Custom") {
                $scope.properties.SortExpression.PropertyValue = newSortOption;
            }
        };

        propertyService.get()
            .then(function (data) {
                if (data) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);

                    $scope.additionalFilters.value = $.parseJSON($scope.properties.SerializedAdditionalFilters.PropertyValue || null);

                    var dateFilters = $.parseJSON($scope.properties.SerializedDateFilters.PropertyValue || null);
                    $scope.dateFilters.value = dateFilters;

                    var selectedItemsIds = $.parseJSON($scope.properties.SerializedSelectedItemsIds.PropertyValue || null);
                    if (selectedItemsIds) {
                        $scope.blogPostSelector.selectedItemsIds = selectedItemsIds;
                    }

                    var selectedParentsIds = $.parseJSON($scope.properties.SerializedSelectedParentsIds.PropertyValue || null);
                    if (selectedParentsIds) {
                        $scope.parentSelector.selectedItemsIds = selectedParentsIds;
                    }

                    if (sortOptions.indexOf($scope.properties.SortExpression.PropertyValue) >= 0) {
                        $scope.selectedSortOption = $scope.properties.SortExpression.PropertyValue;
                    }
                    else {
                        $scope.selectedSortOption = "Custom";
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
                    else {
                        if (!$scope.properties.DetailsPageId.PropertyValue ||
                                $scope.properties.DetailsPageId.PropertyValue === emptyGuid) {
                            $scope.properties.OpenInSamePage.PropertyValue = true;
                        }
                    }

                    if ($scope.properties.SelectionMode.PropertyValue !== 'SelectedItems') {
                        $scope.properties.SerializedSelectedItemsIds.PropertyValue = null;

                        // If the sorting expression is AsSetManually but the selection mode is AllItems or FilteredItems, this is not a valid combination.
                        // So set the sort expression to the default value: PublicationDate DESC
                        if ($scope.properties.SortExpression.PropertyValue === "AsSetManually") {
                            $scope.properties.SortExpression.PropertyValue = "PublicationDate DESC";
                        }
                    }

                    if ($scope.properties.SelectionMode.PropertyValue !== "FilteredItems") {
                        $scope.properties.SerializedAdditionalFilters.PropertyValue = null;
                    }
                });
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})(jQuery);
