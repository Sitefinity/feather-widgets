(function () {

    var sharedContentSelecorModule = angular.module('sharedContentSelecorModule', ['ui.bootstrap', 'shareContentServices', 'providerSelectorModule']);

    //controller for the "$modalInstance"
    var SharedContentSelectorCtrl = function ($scope, $modalInstance, ShareContentService, PropertyDataService, PageControlDataService, ProvidersDataService) {

        var shareContentIdProperty;
        var contentProperty;
        var providerProperty;

        // ------------------------------------------------------------------------
        // Event handlers
        // ------------------------------------------------------------------------

        //invoked when the control properties are extracted
        var onGetPropertiesSuccess = function (xhrData, status, headers, config) {

            if (xhrData) {
                for (var i = 0; i < xhrData.Items.length; i++) {
                    if (xhrData.Items[i].PropertyName === "SharedContentID")
                        shareContentIdProperty = xhrData.Items[i];
                    if (xhrData.Items[i].PropertyName === "Content")
                        contentProperty = xhrData.Items[i];
                    if (xhrData.Items[i].PropertyName === "ProviderName")
                        providerProperty = xhrData.Items[i];

                }

                ProvidersDataService.setDefaultProviderName(providerProperty.PropertyValue);
            }
        }

        //invoked when the content blocks for a provider are extracted
        var onGetSuccess = function (data) {
            if (data && data.Items && data.Items.length != 0) {                
                $scope.IsListEmpty = false;
                $scope.ContentItems = data.Items;
                //select current cotnentBlock if exist
                if (shareContentIdProperty)
                    for (var i = 0; i < data.Items.length; i++) {
                        if (data.Items[i].Id == shareContentIdProperty.PropertyValue) {
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
            var errorMessage = "";
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

                if (typeof ($telerik) != "undefined") {
                    $telerik.$(document).trigger("modalDialogClosed");
                }
            }
        };

        var showError = function (message) {
            $scope.ShowError = true;
            $scope.ErrorMessage = message;
        };

        savePropertiesOnSharedContentIdChange = function (sharedContentId, content, providerName) {

            //change SharedContentID property value
            var modifiedProperties = [];
            shareContentIdProperty.PropertyValue = sharedContentId;
            modifiedProperties.push(shareContentIdProperty);
            providerProperty.PropertyValue = providerName;
            modifiedProperties.push(providerProperty);
            if (content) {
                contentProperty.PropertyValue = content;
                modifiedProperties.push(contentProperty);
            }

            var currentSaveMode = 0;
            if (PageControlDataService.data.PropertyValueCulture) {
                currentSaveMode = 1;
            }
            PropertyDataService.saveProperties(onSavePropertiesSuccess, onError, currentSaveMode, modifiedProperties);
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
                ShareContentService.getContent(selectedContentItemId, providerName, false, onGetContentBlockSuccess, onError);
            }
            else {
                dialogClose();
            }
        };
        $scope.Cancel = function () {
            dialogClose();
        };

        $scope.$on("providerSelectionChanged", function (event, args) {
            $scope.ShowLoadingIndicator = true;
            var providerName;
            if (args)
                providerName = args.providerName;

            ShareContentService.getContentItems(providerName, onGetSuccess, onError);
        });

        $scope.$on("errorOccurred", function (event, args) {
            $scope.ShowLoadingIndicator = false;
            var errorMessage;
            if (args)
                errorMessage = args.message;

            showError(errorMessage);
        });

        //get widget properties to define the selected item
        PropertyDataService.getProperties(onGetPropertiesSuccess, onError);

    }

    sharedContentSelecorModule.controller('SharedContentSelectorDialogCtrl', ['$scope', '$modal',
       function ($scope, $modal) {
           var modalInstance = $modal.open({
               backdrop: 'static',
               templateUrl: 'sharedContentSelector',
               controller: SharedContentSelectorCtrl,
               windowClass: "sf-designer-dlg"
           });
       }]);

})();