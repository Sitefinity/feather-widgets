(function ($) {

    var simpleViewModule = angular.module('simpleViewModule', ['designer', 'kendo.directives', 'sfFields', 'sfSelectors']);
    angular.module('designer').requires.push('simpleViewModule');
    designerModule.controller('SimpleCtrl', ['$scope', 'propertyService', 'sfMediaMarkupService', function ($scope, propertyService, mediaMarkupService) {
        $scope.feedback.showLoadingIndicator = true;

        $scope.$watch('model', function (newVal, oldVal) {
            if (newVal !== oldVal && newVal && newVal[0]) {
                $scope.properties.Item.PropertyValue = newVal.item;
            }
        });

        propertyService.get()
            .then(function (data) {
                if (data) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);
                    $scope.model = $scope.properties.Item.PropertyValue;
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
