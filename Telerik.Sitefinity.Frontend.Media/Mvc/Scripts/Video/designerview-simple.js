(function ($) {
    var simpleViewModule = angular.module('simpleViewModule', ['expander', 'designer', 'kendo.directives', 'sfFields', 'sfSelectors', 'sfAspectRatioSelection']);
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
            propertyService.get()
                .then(function (data) {
                    if (data) {
                        $scope.properties = propertyService.toAssociativeArray(data.Items);

                        $scope.videoModel = {
                            aspectRatio: $scope.properties.AspectRatio.PropertyValue,
                            width: parseInt($scope.properties.Width.PropertyValue, 10),
                            height: parseInt($scope.properties.Height.PropertyValue, 10)
                        };
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
