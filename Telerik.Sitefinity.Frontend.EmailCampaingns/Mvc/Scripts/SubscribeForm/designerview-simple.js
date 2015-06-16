(function ($) {
    var designer = angular.module('designer');
    designer.requires.push('sfSelectors', 'expander');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        $scope.feedback.showLoadingIndicator = true;

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
                    if ($scope.properties.SuccessfullySubmittedForm.PropertyValue === 'ShowMessage') {
                        $scope.properties.PageId.PropertyValue = null;
                    }
                });
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})(jQuery);
