(function () {
    var simpleViewModule = angular.module('simpleViewModule', ['designer']);

    angular.module('designer').requires.push('expander', 'simpleViewModule');

    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        $scope.feedback.showLoadingIndicator = true;

        var onGetPropertiesSuccess = function (data) {
            if (data && data.Items) {
                $scope.properties = propertyService.toHierarchyArray(data.Items);

                if ($scope.properties.Model.ValidatorDefinition.MaxLength.PropertyValue === '0') {
                    $scope.properties.Model.ValidatorDefinition.MaxLength.PropertyValue = '';
                }
            }
        };

        propertyService.get()
            .then(onGetPropertiesSuccess)
            .catch(function (errorData) {
                $scope.feedback.showError = true;
                if (errorData && errorData.data) {
                    $scope.feedback.errorMessage = errorData.data.Detail;
                }
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})();