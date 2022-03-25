(function ($) {
    angular.module('designer').requires.push('expander', 'sfCodeArea', 'sfBootstrapPopover', 'sfFileUrlField');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        propertyService.get()
            .then(function (data) {
                if (data && data.Items) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);
                }
            },
            function (errorData) {
                $scope.feedback.showError = true;
                if (errorData && errorData.data)
                    $scope.feedback.errorMessage = errorData.data.Detail;
            })
            .then(function () {
                $scope.feedback.savingHandlers.push(function () {
                    if ($scope.properties.Mode.PropertyValue !== 'Inline') {
                        $scope.properties.InlineCode.PropertyValue = '';
                    }

                    if ($scope.properties.Mode.PropertyValue !== 'Reference') {
                        $scope.properties.FileUrl.PropertyValue = null;
                    }
                });
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})(jQuery);
