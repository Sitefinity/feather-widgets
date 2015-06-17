(function ($) {
    var designer = angular.module('designer');
    designer.requires.push('sfSelectors', 'expander', 'sfBootstrapPopover');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        $scope.feedback.showLoadingIndicator = true;
        var emptyGuid = '00000000-0000-0000-0000-000000000000';

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
                    if (!$scope.properties.ListId.PropertyValue || $scope.properties.ListId.PropertyValue === emptyGuid)
                        throw "Please select a mailing list";

                    if ($scope.properties.SuccessfullySubmittedForm.PropertyValue === 'ShowMessage') {
                        $scope.properties.PageId.PropertyValue = null;
                    }
                    if ($scope.properties.UnsubscribeMode.PropertyValue === 'Link') {
                        $scope.properties.PageId.PropertyValue = null;
                        $scope.properties.ListId.PropertyValue = null;
                        $scope.properties.SuccessfullySubmittedForm.PropertyValue = 'ShowMessage';
                    }
                });
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})(jQuery);
