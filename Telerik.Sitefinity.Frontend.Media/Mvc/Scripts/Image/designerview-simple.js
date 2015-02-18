(function ($) {

    var simpleViewModule = angular.module('simpleViewModule', ['designer', 'kendo.directives', 'sfFields', 'sfSelectors']);
    angular.module('designer').requires.push('simpleViewModule');
    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', 'sfMediaMarkupService', function ($scope, propertyService, mediaMarkupService) {
        $scope.feedback.showLoadingIndicator = true;

        $scope.$watch('model', function (newVal, oldVal) {
            if (newVal !== oldVal) {
                $scope.properties.Item.PropertyValue = newVal.item;
                $scope.properties.Title.PropertyValue = newVal.item.Title;
                $scope.properties.AlternativeText.PropertyValue = newVal.item.AlternativeText;
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
