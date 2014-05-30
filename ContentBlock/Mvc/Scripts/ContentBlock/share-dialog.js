(function ($) {
    if (typeof ($telerik) != 'undefined') {
        $telerik.$(document).one('dialogRendered', function () {
            angular.bootstrap($('.dialog'), ['shareDialog']);
        });
    }

    //angular controller responsible for the Share dialog module logic
    var shareDialogModule = angular.module('shareDialog', ['modalDialog', 'sharedContentServices', 'pageEditorServices']);

    shareDialogModule.controller('shareDialogCtrl', ['$scope', '$modalInstance', 'sharedContentService',
        function ($scope, $modalInstance, sharedContentService) {
            var dialogClose = function () {
                $modalInstance.close();

                if (typeof ($telerik) != 'undefined') {
                    $telerik.$(document).trigger('modalDialogClosed');
                }
            };

            var onError = function (data) {
                $scope.ShowError = true;
                if (data)
                    $scope.ErrorMessage = data.Detail;
            };

            $scope.model = {
                Title: ''
            };
            $scope.IsTitleValid = true;
            $scope.ShowLoadingIndicator = false;
            $scope.ShowError = false;
            $scope.ErrorMessage = '';

            $scope.ShareContent = function () {
                //validate title and send request to share the content block
                if ($.trim($scope.model.Title)) {
                    $scope.IsTitleValid = true;
                    $scope.ShowLoadingIndicator = true;
                    sharedContentService.share($scope.model.Title)
                        .then(dialogClose, onError)
                        .finally(function () {
                            $scope.ShowLoadingIndicator = false;
                        });
                }
                else {
                    $scope.IsTitleValid = false;
                }

            };

            $scope.Cancel = function () {
                dialogClose();
            };

            $scope.HideError = function () {
                $scope.ShowError = false;
                $scope.ErrorMessage = null;
            };
        }
    ]);

    shareDialogModule.controller('unshareDialogCtrl', ['$scope', '$modalInstance', 'sharedContentService', 'propertyService', 'widgetContext',
        function ($scope, $modalInstance, sharedContentService, propertyService, widgetContext) {

            var dialogClose = function () {
                $modalInstance.close();

                if (typeof ($telerik) != 'undefined') {
                    $telerik.$(document).trigger('modalDialogClosed');
                }
            };

            var onError = function (data) {
                $scope.ShowError = true;
                if (data)
                    $scope.ErrorMessage = data.Detail;

                $scope.ShowLoadingIndicator = false;
            };

            var getContentBlock = function (data) {
                $scope.Properties = propertyService.toAssociativeArray(data.Items);

                //get the updated content for the current shareContentId
                var checkout = false;
                return sharedContentService.get($scope.Properties.SharedContentID.PropertyValue, $scope.Properties.ProviderName.PropertyValue, checkout);
            };

            //change SharedContentId and content of the content block widget       
            var updateProperties = function (contentBlock) {
                var EMPTY_GUID = '00000000-0000-0000-0000-000000000000';

                $scope.Properties.SharedContentID.PropertyValue = EMPTY_GUID;
                $scope.Properties.ProviderName.PropertyValue = '';
                if (contentBlock && contentBlock.Item)
                    $scope.Properties.Content.PropertyValue = contentBlock.Item.Content.Value;

                var modifiedProperties = [$scope.Properties.SharedContentID, $scope.Properties.ProviderName, $scope.Properties.Content];
                var currentSaveMode = widgetContext.culture ? 1 : 0;
                return propertyService.save(currentSaveMode, modifiedProperties);
            };

            $scope.ShowLoadingIndicator = false;
            $scope.ShowError = false;
            $scope.ErrorMessage = '';

            $scope.UnshareContent = function () {
                $scope.ShowLoadingIndicator = true;

                propertyService.get()
                    .then(getContentBlock)
                    .then(updateProperties)
                    .then(dialogClose)
                    .catch(onError)
                    .finally(function () {
                        $scope.ShowLoadingIndicator = false;
                    });
            };

            $scope.Cancel = function () {
                dialogClose();
            };

            $scope.HideError = function () {
                $scope.ShowError = false;
                $scope.ErrorMessage = null;
            };
        }
    ]);

}) (jQuery);