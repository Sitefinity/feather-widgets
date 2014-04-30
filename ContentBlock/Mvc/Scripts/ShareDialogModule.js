(function () {

    //angular controller responsible for the Share dialog module mode logic
    var shareDialogModule = angular.module('shareDialog', ['ui.bootstrap', 'shareContentServices', 'controlPropertyServices']);

    //controller for the share "$modalInstance"
    var ShareDialogContentCtrl = function ($scope, $modalInstance, $http, ShareContentService) {
        $scope.Title = "";
        $scope.IsTitleValid = true;
        $scope.ShowLoadingIndicator = false;
        $scope.ShowError = false;
        $scope.ErrorMessage = "";

        $scope.ShareContent = function () {

            var onShareSuccess = function (data) {
                $scope.ShowLoadingIndicator = false;
                $scope.ShowError = false;
                $scope.ErrorMessage = "";

                $modalInstance.close();

                if (typeof ($telerik) != "undefined") {
                    $telerik.$(document).trigger("modalDialogClosed");
                }
            };

            var onShareError = function (data, status, headers, config) {
                $scope.ShowLoadingIndicator = false;
                $scope.ShowError = true;
                if(data)
                    $scope.ErrorMessage = data.Detail;
            }

            //validate title and send request to share the content block
            if (jQuery.trim(this.Title) != "") {
                this.IsTitleValid = true;
                $scope.ShowLoadingIndicator = true;
                ShareContentService.shareContent(this.Title, onShareSuccess, onShareError);
            }
            else
                this.IsTitleValid = false;

        };
        $scope.Cancel = function () {
            if ($modalInstance) {
                $modalInstance.dismiss('cancel');

                if (typeof ($telerik) != "undefined") {
                    $telerik.$(document).trigger("modalDialogClosed");
                }
            }
        }
    };

    //controller for the unshare "$modalInstance"
    var UnshareDialogContentCtrl = function ($scope, $modalInstance, $http, ShareContentService, PropertyDataService, PageControlDataService) {

        //change SharedContentId and content of the content block widget       
        var updateWidgetOnSharedContentIdChange = function (sharedContentId, onsuccess, onerror) {
            var shareContentIdProperty;
            var contentProperty;
            var providerProperty;

            var onGetContentBlockSuccess = function (xhrData, status, headers, config) {
                //change SharedContentID property value
                var modifiedProperties = [];
                shareContentIdProperty.PropertyValue = sharedContentId;
                modifiedProperties.push(shareContentIdProperty);
                providerProperty.PropertyValue = "";
                modifiedProperties.push(providerProperty);
                if (xhrData && xhrData.Item)
                    contentProperty.PropertyValue = xhrData.Item.Content.Value;
                modifiedProperties.push(contentProperty);

                var currentSaveMode = 0;
                if (PageControlDataService.data.PropertyValueCulture) {
                    currentSaveMode = 1;
                }
                PropertyDataService.saveProperties(onsuccess, onerror, currentSaveMode, modifiedProperties);
            };

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
                }
                //get the updated content for the current shareContentId
                ShareContentService.getContent(shareContentIdProperty.PropertyValue, providerProperty.PropertyValue,
                    false, onGetContentBlockSuccess, onerror);
            };

            PropertyDataService.getProperties(onGetPropertiesSuccess, onerror);
        };

        $scope.ShowLoadingIndicator = false;
        $scope.ShowError = false;
        $scope.ErrorMessage = "";

        $scope.UnshareContent = function () {
            $scope.ShowLoadingIndicator = true;

            var onUnshareSuccess = function (data) {
                $scope.ShowLoadingIndicator = false;
                $modalInstance.close();

                if (typeof ($telerik) != "undefined") {
                    $telerik.$(document).trigger("modalDialogClosed");
                }
            };

            var onUnshareError = function (data, status, headers, config) {
                $scope.ShowError = true;
                if (data)
                    $scope.ErrorMessage = data.Detail;

                $scope.ShowLoadingIndicator = false;
            };

            updateWidgetOnSharedContentIdChange("00000000-0000-0000-0000-000000000000", onUnshareSuccess, onUnshareError);

        };
        $scope.Cancel = function () {
            if ($modalInstance) {
                $modalInstance.dismiss('cancel');

                if (typeof ($telerik) != "undefined") {
                    $telerik.$(document).trigger("modalDialogClosed");
                }
            }
        }
    };

    //basic controller for the Share dialog view
    shareDialogModule.controller('ShareDialogCtrl', ['$scope', '$modal',
    function ($scope, $modal) {

        openModal($modal, 'shareDialogContent', ShareDialogContentCtrl);

    }]);

    //basic controller for the Unshare dialog view
    shareDialogModule.controller('UnshareDialogCtrl', ['$scope', '$modal',
    function ($scope, $modal) {

        openModal($modal, 'unshareDialogContent', UnshareDialogContentCtrl);

    }]);


    // ------------------------------------------------------------------------
    // Helper methods
    // ------------------------------------------------------------------------

    var openModal = function (modal, template, controllerName) {
        var modalInstance = modal.open({
            templateUrl: template,
            controller: controllerName,
            windowClass: "sf-designer-dlg",
            backdrop: "static"
        });
    };


}) (jQuery);