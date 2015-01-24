(function ($) {
    var EMPTY_GUID = '00000000-0000-0000-0000-000000000000';

    var simpleViewModule = angular.module('simpleViewModule', ['designer', 'kendo.directives', 'sharedContentServices', 'sfFields', 'sfSelectors']);
    angular.module('designer').requires.push('simpleViewModule');

    simpleViewModule.factory('contentBlockService', ['dialogFeedbackService', 'sharedContentService', function (dialogFeedbackService, sharedContentService) {
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
                            dialogFeedbackService.savingHandlers.push(updateContentItem);
                            dialogFeedbackService.cancelingHandlers.push(unlockContentItem);
                        }
                    }, function () {
                        properties.Content.PropertyValue = '';
                        properties.SharedContentID.PropertyValue = EMPTY_GUID;
                    });
            }
        };
    }]);

    //basic controller for the simple designer view
    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', 'sharedContentService', 'contentBlockService',
        function ($scope, propertyService, sharedContentService, contentBlockService) {
            var contentItem;

            // ------------------------------------------------------------------------
            // event handlers
            // ------------------------------------------------------------------------

            var onGetPropertiesSuccess = function (data) {
                $scope.properties = propertyService.toAssociativeArray(data.Items);
                $scope.isShared = $scope.properties.SharedContentID.PropertyValue != EMPTY_GUID;

                kendo.bind();

                if ($scope.isShared) {
                    return contentBlockService($scope.properties);
                }
            };

            // ------------------------------------------------------------------------
            // scope variables and set up
            // ------------------------------------------------------------------------

            $scope.feedback.showLoadingIndicator = true;

            $scope.isShared = false;

            propertyService.get()
                .then(onGetPropertiesSuccess)
                .catch(function (data) {
                    $scope.feedback.showError = true;
                    if (data)
                        $scope.feedback.errorMessage = data.Detail;
                })
                .finally(function () {
                    $scope.isShared = $scope.properties.SharedContentID.PropertyValue != EMPTY_GUID;
                    $scope.feedback.showLoadingIndicator = false;
                });
        }
    ]);
})(jQuery);
