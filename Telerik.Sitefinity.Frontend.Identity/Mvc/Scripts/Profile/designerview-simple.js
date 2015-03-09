(function () {

    var simpleViewModule = angular.module('simpleViewModule', ['designer']);

    angular.module('designer').requires.push('expander', 'kendo.directives', 'sfSelectors', 'simpleViewModule');

    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {

        // ------------------------------------------------------------------------
        // scope variables and set up
        // ------------------------------------------------------------------------

        $scope.feedback.showLoadingIndicator = true;

        var onGetPropertiesSuccess = function (data) {
            if (data) {
                $scope.properties = propertyService.toAssociativeArray(data.Items);

                if ($scope.properties.Mode.PropertyValue === 'EditOnly' &&
                    $scope.properties.SaveChangesAction.PropertyValue === 'SwitchToReadMode') {
                    $scope.properties.SaveChangesAction.PropertyValue = 'ShowMessage';
                }
            }
        };

        propertyService.get()
            .then(onGetPropertiesSuccess)
            .catch(function (data) {
                $scope.feedback.showError = true;
                if (data)
                    $scope.feedback.errorMessage = data.Detail;
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})();