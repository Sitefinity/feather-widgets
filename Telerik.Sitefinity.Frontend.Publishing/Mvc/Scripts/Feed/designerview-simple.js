(function ($) {
    var designerModule = angular.module('designer');
    angular.module('designer').requires.push('sfSelectors', 'expander');

    designerModule.controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        var emptyGuid = '00000000-0000-0000-0000-000000000000';
        var doNotLoadTextToDisplay = false;

        $scope.$watch(
            'feed',
            function (newFeed, oldFeed) {
                if (newFeed !== oldFeed) {
                    if (newFeed && oldFeed && newFeed.Id !== oldFeed.Id) {
                        // new feed is selected so set TextToDisplay to the new feed title
                        doNotLoadTextToDisplay = false;
                    }

                    if ($scope.properties && newFeed && !$scope.properties.TextToDisplay.PropertyValue && !doNotLoadTextToDisplay) {
                        $scope.properties.TextToDisplay.PropertyValue = newFeed.Title;
                    }
                }
            },
            true
        );

        propertyService.get()
            .then(function (data) {
                if (data && data.Items) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);
                    if ($scope.properties.FeedId.PropertyValue &&
                        $scope.properties.FeedId.PropertyValue !== emptyGuid &&
                        !$scope.properties.TextToDisplay.PropertyValue) {
                        // TextToDisplay is set to be empty string so do not set feed title on load
                        doNotLoadTextToDisplay = true;
                    }
                }
            },
            function (errorData) {
                $scope.feedback.showError = true;
                if (errorData && errorData.data)
                    $scope.feedback.errorMessage = errorData.data.Detail;
            })
            .then(function () {
                $scope.feedback.savingHandlers.push(function () {
                    if ($scope.properties.InsertionOption.PropertyValue == 'AddressBarOnly') {
                        $scope.properties.TextToDisplay.PropertyValue = $scope.properties.Tooltip.PropertyValue = null;
                        $scope.properties.OpenInNewWindow.PropertyValue = false;
                    }
                });
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})(jQuery);