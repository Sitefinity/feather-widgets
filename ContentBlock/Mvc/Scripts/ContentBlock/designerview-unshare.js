(function ($) {
    var shareDialogModule = angular.module('shareDialog', ['sharedContentServices', 'pageEditorServices']);
    angular.module('designer').requires.push('shareDialog');

    shareDialogModule.controller('UnshareCtrl', ['$scope', 'sharedContentService', 'propertyService', 'widgetContext',
        function ($scope, sharedContentService, propertyService, widgetContext) {
            var onError = function (data) {
                $scope.feedback.showError = true;
                if (data)
                    $scope.feedback.errorMessage = data.Detail;

                $scope.feedback.showLoadingIndicator = false;
            };

            var getContentBlock = function (data) {
                $scope.properties = propertyService.toAssociativeArray(data.Items);

                //get the updated content for the current shareContentId
                var checkout = false;
                return sharedContentService.get($scope.properties.SharedContentID.PropertyValue, $scope.properties.ProviderName.PropertyValue, checkout);
            };

            //change SharedContentId and content of the content block widget       
            var updateProperties = function (contentBlock) {
                var EMPTY_GUID = '00000000-0000-0000-0000-000000000000';

                $scope.properties.SharedContentID.PropertyValue = EMPTY_GUID;
                $scope.properties.ProviderName.PropertyValue = '';
                if (contentBlock && contentBlock.Item)
                    $scope.properties.Content.PropertyValue = contentBlock.Item.Content.Value;

                var modifiedProperties = [$scope.properties.SharedContentID, $scope.properties.ProviderName, $scope.properties.Content];
                var currentSaveMode = widgetContext.culture ? 1 : 0;
                return propertyService.save(currentSaveMode, modifiedProperties);
            };

            $scope.feedback.showLoadingIndicator = false;
            $scope.feedback.showError = false;
            $scope.feedback.errorMessage = '';

            $scope.unshareContent = function () {
                $scope.feedback.showLoadingIndicator = true;

                propertyService.get()
                    .then(getContentBlock)
                    .then(updateProperties)
                    .then($scope.close)
                    .catch(onError)
                    .finally(function () {
                        $scope.feedback.showLoadingIndicator = false;
                    });
            };

            $scope.hideError = function () {
                $scope.feedback.showError = false;
                $scope.feedback.errorMessage = null;
            };
        }
    ]);

}) (jQuery);