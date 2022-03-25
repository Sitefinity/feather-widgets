(function ($) {

    var simpleViewModule = angular.module('simpleViewModule', ['expander', 'designer', 'ngSanitize']);
    angular.module('designer').requires.push('simpleViewModule');
    angular.module('designer').requires.push('sfFields');
    angular.module('designer').requires.push('sfSelectors');

    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        $scope.feedback.showLoadingIndicator = true;

        propertyService.get()
            .then(function (data) {
                if (!data || !data.Items) {
                    return;
                }

                $scope.properties = propertyService.toAssociativeArray(data.Items);

                var isPageSelectMode = $scope.properties.IsPageSelectMode.PropertyValue;
                $scope.properties.IsPageSelectMode.PropertyValue = isPageSelectMode.toLowerCase() === "true";
            },
            function (errorData) {
                $scope.feedback.showError = true;
                if (errorData && errorData.data)
                    $scope.feedback.errorMessage = errorData.data.Detail;
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})(jQuery);
