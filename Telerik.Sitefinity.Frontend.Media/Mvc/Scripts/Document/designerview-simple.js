(function ($) {
    var simpleViewModule = angular.module('simpleViewModule', ['expander', 'designer', 'kendo.directives', 'sfFields', 'sfSelectors', 'ngSanitize']);
    angular.module('designer').requires.push('simpleViewModule');

    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', 'serviceHelper',
        function ($scope, propertyService, serviceHelper) {

            $scope.feedback.showLoadingIndicator = true;

            $scope.$watch('properties.Id.PropertyValue', function (newVal, oldVal) {
                // Cancel is selected with no document selected - close the designer
                if (newVal === null) {
                    $scope.$parent.cancel();
                }
            });
            propertyService.get()
                .then(function (data) {
                    if (data && data.Items) {
                        $scope.properties = propertyService.toAssociativeArray(data.Items);
                    }
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
