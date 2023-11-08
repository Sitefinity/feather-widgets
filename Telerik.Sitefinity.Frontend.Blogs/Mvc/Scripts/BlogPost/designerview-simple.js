(function ($) {
    angular.module('designer').requires.push('expander', 'sfSelectors');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        var sortOptions = ['PublicationDate DESC', 'LastModified DESC', 'Title ASC', 'Title DESC', 'AsSetManually'];
        var emptyGuid = '00000000-0000-0000-0000-000000000000';

        $scope.blogPostSelector = { selectedItemsIds: [] };
        $scope.parentSelector = { selectedItemsIds: [] };
        $scope.feedback.showLoadingIndicator = true;
        $scope.additionalFilters = {};
        $scope.dateFilters = {};
        $scope.shouldClearSelectedParentIds = false;
        $scope.feedback.savingHandlers.push(function () {
            if ($scope.shouldClearSelectedParentIds) {
                $scope.properties.SerializedSelectedParentsIds.PropertyValue = '';
            }
        });

        $scope.$watch(
            'blogPostSelector.selectedItemsIds',
            function (newVal, oldVal) {
                if (newVal !== oldVal) {
                    if ($scope.properties && newVal) {
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
	        'properties.ProviderName.PropertyValue',
            function (newProviderName, oldProviderName) {
                newProviderName = newProviderName || "";
                oldProviderName = oldProviderName || "";

                if ($scope.properties && newProviderName !== oldProviderName) {
	                $scope.properties.ParentFilterMode.PropertyValue = 'All';
	                $scope.properties.SerializedSelectedParentsIds.PropertyValue = null;
	            }
	        },
	        true
        );

        $scope.$watch(
            'parentSelector.selectedItemsIds',
            function (newSelectedItemsIds, oldSelectedItemsIds) {
                if ($scope.properties && newSelectedItemsIds !== oldSelectedItemsIds) {
                    $scope.properties.SerializedSelectedParentsIds.PropertyValue = JSON.stringify(newSelectedItemsIds);
                }
            }, 
            true
            );

        $scope.$watch(
            'additionalFilters.value',
            function (newAdditionalFilters, oldAdditionalFilters) {
                if ($scope.properties && newAdditionalFilters !== oldAdditionalFilters) {
                    $scope.properties.SerializedAdditionalFilters.PropertyValue = JSON.stringify(newAdditionalFilters);
                }
            },
            true
        );

        $scope.$watch(
       'dateFilters.value',
        function (newDateFilters, oldDateFilters) {
            if ($scope.properties && newDateFilters !== oldDateFilters) {
                $scope.properties.SerializedDateFilters.PropertyValue = JSON.stringify(newDateFilters);
            }
        },
        true
      );

        $scope.$watch(
            'properties.ParentFilterMode.PropertyValue',
            function (newValue, oldValue) {
                if ($scope.properties && newValue !== oldValue) {
                    if (newValue == 'NotApplicable') {
                        $scope.properties.SelectionMode.PropertyValue = 'SelectedItems';
                    }
                    else if (oldValue == 'NotApplicable') {
                        $scope.properties.SelectionMode.PropertyValue = 'AllItems';
                    }
                    else if (oldValue === 'Selected' && oldValue != newValue) {
                        $scope.shouldClearSelectedParentIds = true;
                    }
                    else if (newValue === 'Selected') {
                        $scope.shouldClearSelectedParentIds = false;
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

        $scope.updateSortOption = function (newSortOption) {
            if ($scope.properties && newSortOption !== "Custom") {
                $scope.properties.SortExpression.PropertyValue = newSortOption;
            }
        };

        propertyService.get()
            .then(function (data) {
                if (data && data.Items) {
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

                        if ($scope.properties.ContentViewDisplayMode.PropertyValue.toLowerCase() === "Detail".toLowerCase()) {
                            $scope.properties.SelectionMode.PropertyValue = "SelectedItems";
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
