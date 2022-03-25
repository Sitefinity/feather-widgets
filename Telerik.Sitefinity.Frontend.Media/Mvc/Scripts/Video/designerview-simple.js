(function ($) {
    var simpleViewModule = angular.module('simpleViewModule', ['expander', 'designer', 'kendo.directives', 'sfFields', 'sfSelectors', 'sfAspectRatioSelection', 'ngSanitize']);
    angular.module('designer').requires.push('simpleViewModule');

    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', 'serviceHelper',
        function ($scope, propertyService, serviceHelper) {

            $scope.feedback.showLoadingIndicator = true;
            $scope.videoModel = {};

            $scope.$watch('properties.Id.PropertyValue', function (newVal, oldVal) {
                // Cancel is selected with no document selected - close the designer
                if (newVal === null) {
                    $scope.$parent.cancel();
                }
            });

            $scope.$watch('videoModel', function (newVal, oldVal) {
                if ($scope.properties && newVal) {
                    $scope.properties.AspectRatio.PropertyValue = newVal.aspectRatio;
                    $scope.properties.Width.PropertyValue = newVal.width;
                    $scope.properties.Height.PropertyValue = newVal.height;
                }
            }, true);

            propertyService.get()
                .then(function (data) {
                    if (data && data.Items) {
                        $scope.properties = propertyService.toAssociativeArray(data.Items);

                        $scope.videoModel = {
                            aspectRatio: $scope.properties.AspectRatio.PropertyValue,
                            width: parseInt($scope.properties.Width.PropertyValue, 10),
                            height: parseInt($scope.properties.Height.PropertyValue, 10)
                        };
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
