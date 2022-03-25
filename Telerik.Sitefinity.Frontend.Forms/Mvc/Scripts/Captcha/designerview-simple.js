(function () {

    var simpleViewModule = angular.module('simpleViewModule', ['designer']);

    angular.module('designer').requires.push('expander', 'simpleViewModule');

    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {

        // ------------------------------------------------------------------------
        // scope variables and set up
        // ------------------------------------------------------------------------
        $scope.feedback.showLoadingIndicator = true;

        var onGetPropertiesSuccess = function (data) {
            if (data && data.Items) {
                $scope.properties = propertyService.toHierarchyArray(data.Items);
            }
        };

        propertyService.get()
            .then(onGetPropertiesSuccess)
            .catch(function (errorData) {
                $scope.feedback.showError = true;
                if (errorData && errorData.data)
                    $scope.feedback.errorMessage = errorData.data.Detail;
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})();