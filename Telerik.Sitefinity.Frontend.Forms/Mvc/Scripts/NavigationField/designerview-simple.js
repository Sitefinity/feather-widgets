(function () {
    var simpleViewModule = angular.module('simpleViewModule', ['designer']);
    angular.module('designer').requires.push('expander', 'simpleViewModule');
    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', '$q', function ($scope, propertyService, $q) {
        $scope.feedback.showLoadingIndicator = true;
        $scope.currentItems = [];

        propertyService.get()
            .then(function (data) {
                if (data && data.Items) {
                    $scope.properties = propertyService.toHierarchyArray(data.Items);
                    if ($scope.properties.Model.SerializedPages.PropertyValue && $scope.properties.Model.SerializedPages.PropertyValue.length > 0) {
                        $scope.currentItems = JSON.parse($scope.properties.Model.SerializedPages.PropertyValue);
                    }
                }
            })
            .catch(function (errorData) {
                $scope.feedback.showError = true;
                if (errorData && errorData.data) {
                    $scope.feedback.errorMessage = errorData.data.Detail;
                }
            })
            .then(function () {
                $scope.feedback.savingHandlers.push(function () {
                    var deferred = $q.defer();

                    if ($scope.currentItems.length > 0) {
                        $scope.properties.Model.SerializedPages.PropertyValue = angular.toJson($scope.currentItems);
                        deferred.resolve();
                    }
                    else {
                        deferred.reject();
                    }

                    return deferred.promise;
                });
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})();