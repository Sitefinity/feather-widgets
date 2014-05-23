(function ($) {
    if (typeof ($telerik) != 'undefined') {
        $telerik.$(document).one('dialogRendered', function () {
            angular.bootstrap($('.dialog'), ['contentSelector']);
        });
    }

    var contentSelectorModule = angular.module('contentSelector', ['modalDialog', 'sharedContentServices', 'dataProviders']);

    contentSelectorModule.controller('contentSelectorCtrl', ['$scope', '$modalInstance', 'sharedContentService', 'propertyService', 'widgetContext', 'providerService',
        function ($scope, $modalInstance, sharedContentService, propertyService, widgetContext, providerService) {

            var sharedContentIdProperty,
                contentProperty,
                providerProperty;

            // ------------------------------------------------------------------------
            // Event handlers
            // ------------------------------------------------------------------------

            //invoked when the control properties are extracted
            var onGetPropertiesSuccess = function (data) {
                if (data) {
                    for (var i = 0; i < data.Items.length; i++) {
                        if (data.Items[i].PropertyName === 'SharedContentID')
                            sharedContentIdProperty = data.Items[i];
                        if (data.Items[i].PropertyName === 'Content')
                            contentProperty = data.Items[i];
                        if (data.Items[i].PropertyName === 'ProviderName')
                            providerProperty = data.Items[i];
                    }

                    providerService.setDefaultProviderName(providerProperty.PropertyValue);
                }
            }

            //invoked when the content blocks for a provider are extracted
            var onGetSuccess = function (data) {
                if (data && data.Items && data.Items.length != 0) {
                    $scope.IsListEmpty = false;
                    $scope.ContentItems = data.Items;
                    //select current cotnentBlock if exist
                    if (sharedContentIdProperty)
                        for (var i = 0; i < data.Items.length; i++) {
                            if (data.Items[i].Id == sharedContentIdProperty.PropertyValue) {
                                $scope.SelectedContentItem = data.Items[i];
                                $scope.SelectedCBId = data.Items[i].Id;
                            }
                        }
                }
                else {
                    $scope.IsListEmpty = true;
                }
                $scope.ShowLoadingIndicator = false;
            };

            var onError = function () {
                $scope.ShowLoadingIndicator = false;
                var errorMessage = '';
                if (data)
                    errorMessage = data.Detail;

                showError(errorMessage);
            };

            //invoked after the widget properties are persisted
            var onSavePropertiesSuccess = function () {
                $scope.ShowLoadingIndicator = false;
                dialogClose();
            };

            //invoked after the user selects content block item and press save
            var onGetContentBlockSuccess = function (data) {
                $scope.ShowLoadingIndicator = true;
                var contentItem = data.Item
                var providerName = $scope.SelectedContentItem.ProviderName;
                if (contentItem)
                    savePropertiesOnSharedContentIdChange(contentItem.Id, contentItem.Content.Value, providerName);
            };

            // ------------------------------------------------------------------------
            // helper methods
            // ------------------------------------------------------------------------

            var dialogClose = function () {
                if ($modalInstance) {
                    $modalInstance.dismiss('cancel');

                    if (typeof ($telerik) != 'undefined') {
                        $telerik.$(document).trigger('modalDialogClosed');
                    }
                }
            };

            var showError = function (message) {
                $scope.ShowError = true;
                $scope.ErrorMessage = message;
            };

            var savePropertiesOnSharedContentIdChange = function (sharedContentId, content, providerName) {

                //change SharedContentID property value
                var modifiedProperties = [];
                sharedContentIdProperty.PropertyValue = sharedContentId;
                modifiedProperties.push(sharedContentIdProperty);
                providerProperty.PropertyValue = providerName;
                modifiedProperties.push(providerProperty);
                if (content) {
                    contentProperty.PropertyValue = content;
                    modifiedProperties.push(contentProperty);
                }

                var currentSaveMode = widgetContext.culture ? 1 : 0;
                propertyService.save(currentSaveMode, modifiedProperties).then(onSavePropertiesSuccess, onError);
            };

            // ------------------------------------------------------------------------
            // Scope variables and setup
            // ------------------------------------------------------------------------

            $scope.ShowLoadingIndicator = true;
            $scope.ShowError = false;
            $scope.IsListEmpty = true;

            $scope.ContentItemClicked = function (index, item) {
                $scope.SelectedContentItem = item;
                $scope.SelectedCBId = item.Id;
            };

            $scope.SelectSharedContent = function () {
                if ($scope.SelectedContentItem) {
                    var selectedContentItemId = $scope.SelectedContentItem.Id;
                    var providerName = $scope.SelectedContentItem.ProviderName;
                    sharedContentService.get(selectedContentItemId, providerName, false).then(onGetContentBlockSuccess, onError);
                }
                else {
                    dialogClose();
                }
            };
            $scope.Cancel = function () {
                dialogClose();
            };

            $scope.$on('providerSelectionChanged', function (event, args) {
                $scope.ShowLoadingIndicator = true;
                var providerName;
                if (args)
                    providerName = args.providerName;

                sharedContentService.getAll(providerName).then(onGetSuccess, onError);
            });

            $scope.$on('errorOccurred', function (event, args) {
                $scope.ShowLoadingIndicator = false;
                var errorMessage;
                if (args)
                    errorMessage = args.message;

                showError(errorMessage);
            });

            //get widget properties to define the selected item
            propertyService.get().then(onGetPropertiesSuccess, onError);

        }
    ]);

})(jQuery);