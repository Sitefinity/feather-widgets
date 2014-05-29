(function ($) {
    if (typeof ($telerik) != 'undefined') {
        $telerik.$(document).one('dialogRendered', function () {
            angular.bootstrap($('.dialog'), ['contentSelector']);
        });
    }

    var contentSelectorModule = angular.module('contentSelector', ['modalDialog', 'sharedContentServices', 'dataProviders']);

    contentSelectorModule.controller('contentSelectorCtrl', ['$scope', '$modalInstance', 'sharedContentService', 'propertyService', 'widgetContext', 'providerService',
        function ($scope, $modalInstance, sharedContentService, propertyService, widgetContext, providerService) {
            // ------------------------------------------------------------------------
            // Event handlers
            // ------------------------------------------------------------------------

            var onGetPropertiesSuccess = function (data) {
                if (data) {
                    $scope.Properties = propertyService.toAssociativeArray(data.Items);
                    $scope.filter.providerName = $scope.Properties.ProviderName.PropertyValue;

                    providerService.setDefaultProviderName($scope.filter.providerName);
                }
            }

            //invoked when the content blocks for a provider are loaded
            var onLoadedSuccess = function (data) {
                if (data && data.Items) {
                    $scope.ContentItems = data.Items;

                    //select current cotnentBlock if it exists
                    for (var i = 0; i < data.Items.length; i++) {
                        if (data.Items[i].Id == $scope.Properties.SharedContentID.PropertyValue) {
                            $scope.SelectedContentItem = data.Items[i];
                        }
                    }
                }

                $scope.IsListEmpty = $scope.ContentItems.length === 0 && !$scope.filter.search;
            };

            var onError = function () {
                var errorMessage = '';
                if (data)
                    errorMessage = data.Detail;

                $scope.ShowError = true;
                $scope.ErrorMessage = errorMessage;
            };

            // ------------------------------------------------------------------------
            // helper methods
            // ------------------------------------------------------------------------

            var saveProperties = function (data) {
                $scope.Properties.SharedContentID.PropertyValue = data.Item.Id;
                $scope.Properties.ProviderName.PropertyValue = $scope.SelectedContentItem.ProviderName;
                $scope.Properties.Content.PropertyValue = data.Item.Content.Value;

                var modifiedProperties = [$scope.Properties.SharedContentID, $scope.Properties.ProviderName, $scope.Properties.Content];
                var currentSaveMode = widgetContext.culture ? 1 : 0;
                return propertyService.save(currentSaveMode, modifiedProperties);
            };

            var dialogClose = function () {
                if ($modalInstance) {
                    $modalInstance.dismiss('cancel');

                    if (typeof ($telerik) != 'undefined') {
                        $telerik.$(document).trigger('modalDialogClosed');
                    }
                }
            };

            var loadContentItems = function () {
                return sharedContentService.getAll($scope.filter.providerName, $scope.filter.search)
                    .then(onLoadedSuccess, onError);
            };

            var reloadContentItems = function (newValue, oldValue) {
                if (newValue != oldValue) {
                    $scope.ShowLoadingIndicator = true;
                    loadContentItems().finally(hideLoadingIndicator);
                }
            };

            var hideLoadingIndicator = function () {
                $scope.ShowLoadingIndicator = false;
            };

            // ------------------------------------------------------------------------
            // Scope variables and setup
            // ------------------------------------------------------------------------

            $scope.ShowError = false;
            $scope.IsListEmpty = true;
            $scope.ContentItems = [];
            $scope.filter = {
                providerName: null,
                search: null
            };

            $scope.ContentItemClicked = function (index, item) {
                $scope.SelectedContentItem = item;
            };

            $scope.SelectSharedContent = function () {
                if ($scope.SelectedContentItem) {
                    var selectedContentItemId = $scope.SelectedContentItem.Id;
                    var providerName = $scope.SelectedContentItem.ProviderName;
                    var checkout = false;

                    $scope.ShowLoadingIndicator = true;
                    sharedContentService.get(selectedContentItemId, providerName, checkout)
                        .then(saveProperties)
                        .then(dialogClose)
                        .catch(onError)
                        .finally(hideLoadingIndicator);
                }
                else {
                    dialogClose();
                }
            };

            $scope.Cancel = function () {
                dialogClose();
            };

            $scope.HideError = function () {
                $scope.Feedback.ShowError = false;
                $scope.Feedback.ErrorMessage = null;
            };

            $scope.ShowLoadingIndicator = true;
            propertyService.get()
                .then(onGetPropertiesSuccess)
                .then(loadContentItems)
                .then(function () {
                    $scope.$watch('filter.search', reloadContentItems);
                    $scope.$watch('filter.providerName', reloadContentItems);
                })
                .catch(onError)
                .finally(hideLoadingIndicator);
        }
    ]);

})(jQuery);