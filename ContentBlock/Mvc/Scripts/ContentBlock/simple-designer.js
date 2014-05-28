(function ($) {
    var designerModule = angular.module('designer');

    //basic controller for the simple designer view
    designerModule.controller('SimpleCtrl', ['$scope', '$q', 'propertyService', 'sharedContentService', 'dialogFeedbackService',
        function ($scope, $q, propertyService, sharedContentService, dialogFeedbackService) {
            // ------------------------------------------------------------------------
            // event handlers
            // ------------------------------------------------------------------------

            var onGetPropertiesSuccess = function (data) {
                if (data.Items) {
                    updateScopeVariables(data.Items, false);
                }

                var deferred = $q.defer();
                if ($scope.IsShared) {
                    var checkOut = true;
                    return sharedContentService.get($scope.Properties.SharedContentID.PropertyValue, $scope.Properties.ProviderName.PropertyValue, checkOut);
                }
                else {
                    deferred.resolve(null);
                    return deferred.promise;
                }
            };

            var onGetContentItemSuccess = function (data) {
                if (data)
                    $scope.Properties.Content.PropertyValue = data.Item.Content.Value;
            };

            var onGetError = function (data) {
                $scope.Feedback.ShowError = true;
                if (data)
                    $scope.Feedback.ErrorMessage = data.Detail;
            };

            // ------------------------------------------------------------------------
            // helper methods
            // ------------------------------------------------------------------------

            var updateScopeVariables = function (currentItems, applyScopeChanges) {
                $scope.Items = currentItems;
                $scope.Properties = propertyService.toAssociativeArray(currentItems);
                $scope.IsShared = $scope.Properties.SharedContentID.PropertyValue != '00000000-0000-0000-0000-000000000000';

                kendo.bind();
            };

            var hideLoadingIndicator = function () {
                $scope.Feedback.ShowLoadingIndicator = false;
            };

            // ------------------------------------------------------------------------
            // scope variables and set up
            // ------------------------------------------------------------------------

            $scope.Feedback = dialogFeedbackService;
            $scope.IsShared = false;
            $scope.Feedback.ShowLoadingIndicator = true;

            propertyService.get()
                .then(onGetPropertiesSuccess, onGetError)
                .then(onGetContentItemSuccess, onGetError)
                .then(hideLoadingIndicator, hideLoadingIndicator);

            //Fixes a bug for modal dialogs with iframe in them for IE.
            $scope.$on('$destroy', function () {
                var kendoContent = $('#viewsPlaceholder iframe.k-content');
                if (kendoContent.length > 0)
                    kendoContent[0].src = 'about:blank';
            });
        }
    ]);
})(jQuery);