(function ($) {
    angular.module('designer').requires.push('expander', 'sfSelectors', 'dataProviders');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', 'serverData', function ($scope, propertyService, serverData) {
        $scope.feedback.showLoadingIndicator = true;
        $scope.additionalFilters = {};
        $scope.itemSelector = { selectedItemsIds: [] };
        $scope.itemType = serverData.get('itemType');

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

        propertyService.get()
            .then(function (data) {
                if (data) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);

                    var additionalFilters = $.parseJSON($scope.properties.SerializedAdditionalFilters.PropertyValue);

                    $scope.additionalFilters.value = additionalFilters;

                    var selectedItemsIds = $.parseJSON($scope.properties.SerializedSelectedItemsIds.PropertyValue);

                    if (selectedItemsIds) {
                        $scope.itemSelector.selectedItemsIds = selectedItemsIds;
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
