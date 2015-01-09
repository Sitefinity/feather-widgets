(function ($) {
    angular.module('designer').requires.push('expander', 'sfSelectors');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', 'sfSearchService', function ($scope, propertyService, searchService) {
        $scope.feedback.showLoadingIndicator = true;
        $scope.hasSearchIndexes = true;
        
        searchService.getSearchIndexes()
            .then(function (data) {
                if (data) {
                    $scope.searchIndexes = data.Items;
                    $scope.hasSearchIndexes = $scope.searchIndexes.length > 0;
                }
            }, function (data) {
                $scope.feedback.showError = true;
                if (data)
                    $scope.feedback.errorMessage = data.Detail;
            })
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