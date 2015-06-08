(function ($) {
    var designerModule = angular.module('designer');
    angular.module('designer').requires.push('sfSelectors');

    designerModule.controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
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
            .then(function () {
                $scope.feedback.savingHandlers.push(function () {
                });
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})(jQuery);