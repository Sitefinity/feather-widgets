﻿(function ($) {
    angular.module('designer').requires.push('expander', 'sfSelectors');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', 'serverData', function ($scope, propertyService, serverData) {
        var sortOptions = ['PublicationDate DESC', 'LastModified DESC', 'Title ASC', 'Title DESC', 'AsSetManually'];
        var emptyGuid = '00000000-0000-0000-0000-000000000000';

        $scope.feedback.showLoadingIndicator = true;
        $scope.additionalFilters = {};
        $scope.itemSelector = { selectedItemsIds: [] };
        $scope.parentSelector = { selectedItemsIds: [] };
        $scope.itemType = serverData.get('itemType');
        $scope.parentTypes = $.parseJSON(serverData.get('parentTypes') || null);
        $scope.dateFilters = {};

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
	        'properties.ProviderName.PropertyValue',
	        function (newProviderName, oldProviderName) {
	            if (newProviderName !== oldProviderName) {
	                $scope.properties.SelectionMode.PropertyValue = 'AllItems';
	                $scope.properties.SerializedSelectedItemsIds.PropertyValue = null;
	            }
	        },
	        true
        );

        $scope.$watch(
            'itemSelector.selectedItemsIds',
            function (newSelectedItemsIds, oldSelectedItemsIds) {
                if (newSelectedItemsIds !== oldSelectedItemsIds) {
                    $scope.properties.SerializedSelectedItemsIds.PropertyValue = JSON.stringify(newSelectedItemsIds);
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

                    var additionalFilters = $.parseJSON($scope.properties.SerializedAdditionalFilters.PropertyValue || null);
                    $scope.additionalFilters.value = additionalFilters;

                    var dateFilters = $.parseJSON($scope.properties.SerializedDateFilters.PropertyValue || null);
                    $scope.dateFilters.value = dateFilters;

                    var selectedItemsIds = $.parseJSON($scope.properties.SerializedSelectedItemsIds.PropertyValue || null);
                    if (selectedItemsIds) {
                        $scope.itemSelector.selectedItemsIds = selectedItemsIds;
                    }

                    var selectedParentsIds = $.parseJSON($scope.properties.SerializedSelectedParentsIds.PropertyValue || null);
                    if (selectedParentsIds) {
                        $scope.parentSelector.selectedItemsIds = selectedParentsIds;
                    }

                    if ($scope.parentTypes.length > 0 && $scope.properties.CurrentlyOpenParentType && !$scope.properties.CurrentlyOpenParentType.PropertyValue) {
                        $scope.properties.CurrentlyOpenParentType.PropertyValue = $scope.parentTypes[0].TypeName;
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

                    if ($scope.properties.SelectionMode.PropertyValue !== "FilteredItems") {
                        $scope.properties.SerializedAdditionalFilters.PropertyValue = null;
                    }

                    if ($scope.properties.SelectionMode.PropertyValue !== 'SelectedItems') {
                        $scope.properties.SerializedSelectedItemsIds.PropertyValue = null;

                        // If the sorting expression is AsSetManually but the selection mode is AllItems or FilteredItems, this is not a valid combination.
                        // So set the sort expression to the default value: PublicationDate DESC
                        if ($scope.properties.SortExpression.PropertyValue === "AsSetManually") {
                            $scope.properties.SortExpression.PropertyValue = "PublicationDate DESC";
                        }
                    }
                });
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})(jQuery);
