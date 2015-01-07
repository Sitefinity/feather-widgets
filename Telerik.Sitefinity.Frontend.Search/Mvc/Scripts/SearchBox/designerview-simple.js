(function ($) {
    angular.module('designer').requires.push('expander', 'sfSelectors');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', 'sfSearchService', function ($scope, propertyService, searchService) {
        $scope.feedback.showLoadingIndicator = true;
        $scope.hasSearchIndexes = true;

        $scope.isCollapsed = true;
        $scope.toggle = function () {
            $scope.isCollapsed = $scope.isCollapsed === false ? true : false;
        };

        //$scope.$watch(
	    //    'properties.ProviderName.PropertyValue',
	    //    function (newProviderName, oldProviderName) {
	    //        if (newProviderName !== oldProviderName) {
	    //            $scope.properties.SelectionMode.PropertyValue = 'AllItems';
	    //            $scope.properties.SerializedSelectedItemsIds.PropertyValue = null;
	    //        }
	    //    },
	    //    true
        //);
        $scope.getSearchIndexes = searchService.getSearchIndexes()
            .then(function (data) {
                if (data) {
                    return data.Items;
                }
            }, onError)
            .finally(function () {             
            });

        propertyService.get()
            .then(function (data) {
                if (data) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);
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