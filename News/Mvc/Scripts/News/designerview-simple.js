(function ($) {
    angular.module('designer').requires.push('expander', 'selectors', 'dataProviders');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        $scope.feedback.showLoadingIndicator = true;

        $scope.$watch(
            'additionalFilters',
            function (newAdditionalFilters, oldAdditionalFilters) {
                if (newAdditionalFilters !== oldAdditionalFilters) {
                    $scope.properties.SerializedAdditionalFilters.PropertyValue = JSON.stringify(newAdditionalFilters);
                }
            },
            true
        );

        $scope.$watch(
	        'properties.ProviderName.PropertyValue',
	        function (newProviderName, oldProviderName) {
	            if (newProviderName !== oldProviderName) {
	                $scope.properties.SelectionMode.PropertyValue = 'AllItems';
	                $scope.properties.SelectedItemId.PropertyValue = null;
	            }
	        },
	        true
        );

        var translateTaxonFilterData = function (selectedTaxonomies, taxonFilters) {
            var queryData = new Telerik.Sitefinity.Web.UI.QueryData();
            for (var i = 0; i < selectedTaxonomies.length; i++) {
                var taxonomyName = selectedTaxonomies[i];
                var groupItem = queryData.addGroup(taxonomyName, "AND");

                for (var j = 0; j < taxonFilters[taxonomyName].length; j++) {
                    queryData.addChildToGroup(groupItem, taxonomyName, "OR", taxonomyName,
                        'System.Guid', 'Contains', taxonFilters[taxonomyName][j]);
                }
            }
        };

        propertyService.get()
            .then(function (data) {
                if (data) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);

                    var additionalFilters = $.parseJSON($scope.properties.SerializedAdditionalFilters.PropertyValue);

                    if (additionalFilters) {
                        $scope.additionalFilters = additionalFilters;
                    }
                    else {
                        var selectedTaxonomies = $.parseJSON($scope.properties.SerializedSelectedTaxonomies.PropertyValue);
                        var taxonFilters = $.parseJSON($scope.properties.SerializedTaxonomyFilter.PropertyValue);
                        translateTaxonFilterData(selectedTaxonomies, taxonFilters);
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