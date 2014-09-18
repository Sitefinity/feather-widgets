(function ($) {
    angular.module('designer').requires.push('expander', 'selectors', 'dataProviders');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        $scope.feedback.showLoadingIndicator = true;
        $scope.taxonFilters = {};
        $scope.selectedTaxonomies = [];

        $scope.$watch(
            'taxonFilters',
            function (newTaxonFilters, oldTaxonFilters) {
                if (newTaxonFilters !== oldTaxonFilters) {
                    $scope.properties.SerializedTaxonomyFilter.PropertyValue = JSON.stringify(newTaxonFilters);
                }
            },
            true
        );

        $scope.$watch(
            'selectedTaxonomies',
            function (newSelectedTaxonomies, oldSelectedTaxonomies) {
                if (newSelectedTaxonomies !== oldSelectedTaxonomies) {
                    $scope.properties.SerializedSelectedTaxonomies.PropertyValue = JSON.stringify(newSelectedTaxonomies);
                }
            },
            true
        );

        propertyService.get()
            .then(function (data) {
                if (data) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);

                    $scope.selectedTaxonomies = $.parseJSON($scope.properties.SerializedSelectedTaxonomies.PropertyValue);
                    var taxonFilters = $.parseJSON($scope.properties.SerializedTaxonomyFilter.PropertyValue);

                    if (taxonFilters) {
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
})(jQuery);