(function () {
    angular.module('designer').requires.push('expander', 'selectors', 'dataProviders');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        $scope.feedback.showLoadingIndicator = true;
        $scope.taxonFilters = "";
        $scope.allTaxonomies = [{ "Name": "Tags" }];
        $scope.selectedTaxonomies = [];

        $scope.$watch(
            'taxonFilters',
            function (newTaxonFilters, oldTaxonFilters) {
                debugger;
                if (newTaxonFilters !== oldTaxonFilters) {
                    $scope.properties.SerializedTaxonomyFilter.PropertyValue = JSON.stringify(newTaxonFilters);
                }
            },
            true
        );

        $scope.toggleTaxonomySelection = function (taxonomyName) {
            if (!$scope.selectedTaxonomies)
                $scope.selectedTaxonomies = [];
            debugger
            var idx = $scope.selectedTaxonomies.indexOf(taxonomyName);

            // is currently selected
            if (idx > -1) {
                $scope.selectedTaxonomies.splice(idx, 1);
            }

                // is newly selected
            else {
                $scope.selectedTaxonomies.push(taxonomyName);
            }

            $scope.properties.SerializedSelectedTaxonomies.PropertyValue = JSON.stringify($scope.selectedTaxonomies);
        };

        propertyService.get()
            .then(function (data) {
                if (data) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);
                    debugger;
                    $scope.selectedTaxonomies = $.parseJSON($scope.properties.SerializedSelectedTaxonomies.PropertyValue);
                    var taxonFilters = $.parseJSON($scope.properties.SerializedTaxonomyFilter.PropertyValue);

                    if (taxonFilters) {
                        $scope.selectedTaxonKeys = Object.keys(taxonFilters)
                        $scope.taxonFilters = taxonFilters;
                    }
                }
            },
            function (data) {
                $scope.feedback.showError = true;
                if (data)
                    $scope.feedback.errorMessage = data.Detail;
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})();