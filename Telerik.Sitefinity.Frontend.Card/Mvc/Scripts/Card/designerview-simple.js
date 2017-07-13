(function ($) {

    var simpleViewModule = angular.module('simpleViewModule', ['expander', 'designer', 'ngSanitize']);
    angular.module('designer').requires.push('simpleViewModule');
    angular.module('designer').requires.push('sfFields');
    angular.module('designer').requires.push('sfSelectors');

    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        $scope.isPageSelectMode = true;
        $scope.feedback.showLoadingIndicator = true;

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
