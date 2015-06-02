(function ($) {
    var designer = angular.module('designer');
    designer.requires.push('expander', 'sfSelectors');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        var sortOptions = ['Title ASC', 'Title DESC', 'AsSetManually'];
        var emptyGuid = '00000000-0000-0000-0000-000000000000';
        var defaultDynamicNamespace = "Telerik.Sitefinity.DynamicTypes.Model";
        $scope.taxonSelector = { selectedItemsIds: [] };
        $scope.model = {};
        $scope.feedback.showLoadingIndicator = true;

        $scope.$watch(
             'taxonSelector.selectedItemsIds',
             function (newAdditionalFilters, oldAdditionalFilters) {
                 if (newAdditionalFilters !== oldAdditionalFilters) {
                     $scope.properties.SerializedSelectedTaxaIds.PropertyValue = JSON.stringify(newAdditionalFilters);
                 }
             },
             true
         );

        $scope.$watch(
            'model.proxyContentTypeName',
            function (newValue, oldValue) {
                if (newValue !== oldValue) {
                    if (newValue.startsWith(defaultDynamicNamespace)) {
                        $scope.properties.DynamicContentTypeName.PropertyValue = newValue;
                        $scope.properties.ContentTypeName.PropertyValue = null;
                    }
                    else {
                        $scope.properties.ContentTypeName.PropertyValue = newValue;
                        $scope.properties.DynamicContentTypeName.PropertyValue = null;
                    }
                }
            }
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
                    $scope.model.proxyContentTypeName = $scope.properties.ContentTypeName.PropertyValue || $scope.properties.DynamicContentTypeName.PropertyValue;

                    if (sortOptions.indexOf($scope.properties.SortExpression.PropertyValue) >= 0) {
                        $scope.selectedSortOption = $scope.properties.SortExpression.PropertyValue;
                    }
                    else {
                        $scope.selectedSortOption = "Custom";
                    }

                    var selectedItemsIds = $.parseJSON($scope.properties.SerializedSelectedTaxaIds.PropertyValue || null);

                    if (selectedItemsIds) {
                        $scope.taxonSelector.selectedItemsIds = selectedItemsIds;
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
                    if ($scope.properties.TaxaToDisplay.PropertyValue === 'All') {
                        $scope.properties.DynamicContentTypeName.PropertyValue =
                            $scope.properties.ContentTypeName.PropertyValue =
                               $scope.properties.SerializedSelectedTaxaIds.PropertyValue = null;
                    }
                    else if ($scope.properties.TaxaToDisplay.PropertyValue === 'UsedByContentType') {
                        $scope.properties.SerializedSelectedTaxaIds.PropertyValue = null;
                        // Not taking into account Show empty taxa if filtering by content type is selected
                        $scope.properties.ShowEmptyTaxa.PropertyValue = false;
                    }
                    else if ($scope.properties.TaxaToDisplay.PropertyValue === 'Selected') {
                        $scope.properties.DynamicContentTypeName.PropertyValue =
                            $scope.properties.ContentTypeName.PropertyValue = null;
                    }

                    // If the sorting expression is AsSetManually but the selection mode is All or UsedByContentType, this is not a valid combination.
                    // So set the sort expression to the default value: PublicationDate DESC
                    if ($scope.properties.TaxaToDisplay.PropertyValue !== 'Selected' && $scope.properties.SortExpression.PropertyValue === "AsSetManually") {
                        $scope.properties.SortExpression.PropertyValue = "Title ASC";
                    }
                });
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})(jQuery);
