(function ($) {
    angular.module('designer').requires.push('expander', 'sfSelectors');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        var sortOptions = ['PublicationDate DESC', 'LastModified DESC', 'Title ASC', 'Title DESC', 'AsSetManually'];
        $scope.blogSelector = { selectedItemsIds: [] };
        $scope.feedback.showLoadingIndicator = true;

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
