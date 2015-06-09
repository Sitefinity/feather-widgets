(function ($) {
    var designerModule = angular.module('designer');
    angular.module('designer').requires.push('sfSelectors', 'expander');

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
                    if ($scope.properties.InsertionOption.PropertyValue == 'AddressBarOnly') {
                        $scope.properties.TextToDisplay.PropertyValue = $scope.properties.Tooltip.PropertyValue = null;
                        $scope.properties.OpenInNewWindow.PropertyValue = false;
                    }
                });
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})(jQuery);