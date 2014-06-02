(function ($) {
    var EMPTY_GUID = '00000000-0000-0000-0000-000000000000';
    var designerModule = angular.module('designer');

    designerModule.factory('contentBlockService', ['dialogFeedbackService', 'sharedContentService', function (dialogFeedbackService, sharedContentService) {
        var contentItem;
        var properties;

        var unlockContentItem = function () {
            return sharedContentService.deleteTemp(properties.SharedContentID.PropertyValue);
        };

        var updateContentItem = function () {
            return sharedContentService.update(contentItem, properties.Content.PropertyValue, properties.ProviderName.PropertyValue);
        };

        return function (data) {
            properties = data;

            var isShared = properties.SharedContentID.PropertyValue != EMPTY_GUID;

            if (isShared && !contentItem) {
                var checkOut = true;
                return sharedContentService.get(properties.SharedContentID.PropertyValue, properties.ProviderName.PropertyValue, checkOut)
                    .then(function (data) {
                        contentItem = data;
                        if (contentItem) {
                            properties.Content.PropertyValue = contentItem.Item.Content.Value;
                            dialogFeedbackService.SavingPromise = dialogFeedbackService.SavingPromise.then(updateContentItem);
                            dialogFeedbackService.CancelingPromise = dialogFeedbackService.CancelingPromise.then(unlockContentItem);
                        }
                    });
            }
        };
    }]);

    //basic controller for the simple designer view
    designerModule.controller('SimpleCtrl', ['$scope', 'propertyService', 'sharedContentService', 'dialogFeedbackService', 'contentBlockService',
        function ($scope, propertyService, sharedContentService, dialogFeedbackService, contentBlockService) {
            var contentItem;

            // ------------------------------------------------------------------------
            // event handlers
            // ------------------------------------------------------------------------

            var onGetPropertiesSuccess = function (data) {
                $scope.Properties = propertyService.toAssociativeArray(data.Items);
                $scope.IsShared = $scope.Properties.SharedContentID.PropertyValue != EMPTY_GUID;

                kendo.bind();

                if ($scope.IsShared) {
                    return contentBlockService($scope.Properties);
                }
            };

            // ------------------------------------------------------------------------
            // scope variables and set up
            // ------------------------------------------------------------------------

            $scope.Feedback = dialogFeedbackService;
            $scope.Feedback.ShowLoadingIndicator = true;

            $scope.IsShared = false;

            propertyService.get()
                .then(onGetPropertiesSuccess)
                .catch(function (data) {
                    $scope.Feedback.ShowError = true;
                    if (data)
                        $scope.Feedback.ErrorMessage = data.Detail;
                })
                .finally(function () {
                    $scope.Feedback.ShowLoadingIndicator = false;
                });

            //Fixes a bug for modal dialogs with iframe in them for IE.
            $scope.$on('$destroy', function () {
                var kendoContent = $('#viewsPlaceholder iframe.k-content');
                if (kendoContent.length > 0)
                    kendoContent[0].src = 'about:blank';
            });
        }
    ]);
})(jQuery);