(function ($) {
    angular.module('designer').requires.push('expander', 'sfSelectors');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        var emptyGuid = '00000000-0000-0000-0000-000000000000';

        $scope.eventSelector = {
            selectedItemsIds: [],
            narrowSelectionMode: 'All'
        };

        $scope.feedback.showLoadingIndicator = true;

        $scope.isFilteredSelectionModeChecked = function (filteredSelectionMode) {
            if ($scope.properties && $scope.properties.SelectionMode.PropertyValue == "FilteredItems") {
                return $scope.properties.FilteredSelectionMode.PropertyValue === filteredSelectionMode;
            }
        };

        $scope.$watch('eventSelector.selectedItemsIds', function (newVal, oldVal) {
            if (newVal !== oldVal && newVal) {
                $scope.properties.SerializedSelectedItemsIds.PropertyValue = JSON.stringify(newVal);
            }
        }, true);

        $scope.$watch(
            'additionalFilters.value',
            function (newAdditionalFilters, oldAdditionalFilters) {
                if (newAdditionalFilters !== oldAdditionalFilters) {
                    $scope.properties.SerializedAdditionalFilters.PropertyValue = JSON.stringify(newAdditionalFilters);
                }
            },
            true
        );

        propertyService.get()
            .then(function (data) {
                if (data) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);

                    var selectedItemsIds = $.parseJSON($scope.properties.SerializedSelectedItemsIds.PropertyValue || null);
                    if (selectedItemsIds) {
                        $scope.eventSelector.selectedItemsIds = selectedItemsIds;
                    }

                    // Initial sort option
                    if (!$scope.properties.SortExpression.PropertyValue) {
                        if ($scope.properties.SelectionMode.PropertyValue === 'SelectedItems') {
                            $scope.properties.SortExpression.PropertyValue = 'AsSetManually';
                        }
                        else {
                            $scope.properties.SortExpression.PropertyValue = 'EventStart DESC';
                        }
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
                        // So set the sort expression to the default value: EventStart DESC
                        if ($scope.properties.SortExpression.PropertyValue === 'AsSetManually') {
                            $scope.properties.SortExpression.PropertyValue = 'EventStart DESC';
                        }
                    }
                    else {
                        $scope.properties.SerializedAdditionalFilters.PropertyValue = null;
                    }

                });
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})(jQuery);