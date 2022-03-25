(function () {
    var simpleViewModule = angular.module('simpleViewModule', ['designer']);
    angular.module('designer').requires.push('expander', 'simpleViewModule');
    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', '$q', function ($scope, propertyService, $q) {
        $scope.feedback.showLoadingIndicator = true;

        propertyService.get()
            .then(function (data) {
                if (data && data.Items) {
                    $scope.properties = propertyService.toHierarchyArray(data.Items);
                    if ($scope.properties.Model.MetaField.FieldName.PropertyValue !== "") {
                        var nameField = $("#nameField");
                        nameField.attr("disabled", "disabled");
                        nameField.attr("class", "form-control ng-pristine ng-untouched ng-valid ng-scope");
                    }
                }
            })
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