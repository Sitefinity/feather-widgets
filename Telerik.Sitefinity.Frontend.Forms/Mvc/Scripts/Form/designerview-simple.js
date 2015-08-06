(function ($) {
    angular.module('designer').requires.push('expander', 'sfSelectors');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        propertyService.get()
            .then(function (data) {
                if (data) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);
                }
            },
            function (data) {
                $scope.feedback.showError = true;
                if (data)
                    $scope.feedback.errorMessage = data.Detail;
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