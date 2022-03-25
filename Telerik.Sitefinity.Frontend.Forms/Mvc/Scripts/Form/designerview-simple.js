(function ($) {
    var simpleViewModule = angular.module('simpleViewModule', ['expander', 'designer', 'sfFields', 'sfSelectors', 'sfInfiniteScroll']);
    angular.module('designer').requires.push('simpleViewModule');
    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        propertyService.get()
            .then(function (data) {
                if (data && data.Items) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);

                    var connectorSettings = $scope.properties.ConnectorSettings.PropertyValue;
                    $scope.properties.ConnectorSettings.PropertyValue = connectorSettings === "" ? {} : JSON.parse(connectorSettings);
                }
            },
            function (errorData) {
                $scope.feedback.showError = true;
                if (errorData && errorData.data)
                    $scope.feedback.errorMessage = errorData.data.Detail;
            })
            .then(function () {
                $scope.feedback.savingHandlers.push(function () {
                    if ($scope.properties.UseCustomConfirmation.PropertyValue === 'False' || $scope.properties.CustomConfirmationMode.PropertyValue !== 'ShowMessageForSuccess') {
                        $scope.properties.CustomConfirmationMessage.PropertyValue = '';
                    }
                    if ($scope.properties.UseCustomConfirmation.PropertyValue === 'False' || $scope.properties.CustomConfirmationMode.PropertyValue !== 'RedirectToAPage') {
                        var emptyGuid = '00000000-0000-0000-0000-000000000000';
                        $scope.properties.CustomConfirmationPageId.PropertyValue = emptyGuid;
                    }
                });
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})(jQuery);