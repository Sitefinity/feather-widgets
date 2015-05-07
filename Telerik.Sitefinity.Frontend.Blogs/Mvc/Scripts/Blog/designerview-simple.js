(function ($) {
    angular.module('designer').requires.push('expander', 'sfSelectors');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', 'sfProviderService', function ($scope, propertyService, sfProviderService) {
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
            if (newSortOption !== "Custom") {
                $scope.properties.SortExpression.PropertyValue = newSortOption;
            }
        };

        $scope.$watch(
            'blogSelector.selectedItemsIds',
            function (newVal, oldVal) {
                if (newVal !== oldVal) {
                    if (newVal) {
                        $scope.properties.SerializedSelectedItemsIds.PropertyValue = JSON.stringify(newVal);
                    }
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
            function (data) {
                $scope.feedback.showError = true;
                if (data)
                    $scope.feedback.errorMessage = data.Detail;
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

        sfProviderService.getAll('Telerik.Sitefinity.Modules.Blogs.BlogsManager').then(
            function (data) {
                $scope.isProviderSelectorVisible = data && data.Items && data.Items.length >= 2;
            },
            function (err) {
                throw 'Error occurred while populating provider list! Please provide value to the managerType attribute!';
            });
        

    }]);
})(jQuery);
