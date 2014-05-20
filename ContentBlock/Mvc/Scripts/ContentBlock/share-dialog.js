(function ($) {

    //angular controller responsible for the Share dialog module mode logic
    var shareDialogModule = angular.module('shareDialog', ['modalDialog', 'sharedContentServices', 'pageEditorServices']);

    shareDialogModule.controller('shareDialogCtrl', ['$scope', '$modalInstance', '$http', 'sharedContentService',
        function ($scope, $modalInstance, $http, sharedContentService) {
            $scope.Title = '';
            $scope.IsTitleValid = true;
            $scope.ShowLoadingIndicator = false;
            $scope.ShowError = false;
            $scope.ErrorMessage = '';

            $scope.ShareContent = function () {

                var onShareSuccess = function (data) {
                    $scope.ShowLoadingIndicator = false;
                    $scope.ShowError = false;
                    $scope.ErrorMessage = '';

                    $modalInstance.close();

                    if (typeof ($telerik) != 'undefined') {
                        $telerik.$(document).trigger('modalDialogClosed');
                    }
                };

                var onShareError = function (data, status, headers, config) {
                    $scope.ShowLoadingIndicator = false;
                    $scope.ShowError = true;
                    if(data)
                        $scope.ErrorMessage = data.Detail;
                }

                //validate title and send request to share the content block
                if ($.trim(this.Title) != '') {
                    this.IsTitleValid = true;
                    $scope.ShowLoadingIndicator = true;
                    sharedContentService.share(this.Title).then(onShareSuccess, onShareError);
                }
                else
                    this.IsTitleValid = false;

            };

            $scope.Cancel = function () {
                if ($modalInstance) {
                    $modalInstance.dismiss('cancel');

                    if (typeof ($telerik) != 'undefined') {
                        $telerik.$(document).trigger('modalDialogClosed');
                    }
                }
            }
        }
    ]);

    shareDialogModule.controller('unshareDialogCtrl', ['$scope', '$modalInstance', '$http', '$q', 'sharedContentService', 'propertiesService', 'widgetContext',
        function ($scope, $modalInstance, $http, $q, sharedContentService, propertiesService, widgetContext) {

            //change SharedContentId and content of the content block widget       
            var updateWidgetOnSharedContentIdChange = function (sharedContentId) {

                var sharedContentIdProperty,
                    contentProperty,
                    providerProperty,
                    deferred = $q.defer();

                var onGetContentBlockSuccess = function (data) {
                    //change SharedContentID property value
                    var modifiedProperties = [];

                    sharedContentIdProperty.PropertyValue = sharedContentId;
                    modifiedProperties.push(sharedContentIdProperty);
                    providerProperty.PropertyValue = '';
                    modifiedProperties.push(providerProperty);

                    if (data && data.Item)
                        contentProperty.PropertyValue = data.Item.Content.Value;

                    modifiedProperties.push(contentProperty);

                    var currentSaveMode = widgetContext.culture ? 1 : 0;
                    propertiesService.save(currentSaveMode, modifiedProperties).then(function (data) {
                        deferred.resolve(data);
                    }, function (data) {
                        deferred.reject(data);
                    });
                };

                var onGetPropertiesSuccess = function (data) {
                    if (data) {
                        for (var i = 0; i < data.Items.length; i++) {
                            if (data.Items[i].PropertyName === 'SharedContentID')
                                shareContentIdProperty = data.Items[i];
                            if (data.Items[i].PropertyName === 'Content')
                                contentProperty = data.Items[i];
                            if (data.Items[i].PropertyName === 'ProviderName')
                                providerProperty = data.Items[i];
                        }
                    }
                    //get the updated content for the current shareContentId
                    sharedContentService.get(sharedContentIdProperty.PropertyValue, providerProperty.PropertyValue,
                        false).then(onGetContentBlockSuccess, function (data) {
                            deferred.reject(data);
                        });
                };

                propertiesService.get().then(onGetPropertiesSuccess, function (data) {
                    deferred.reject(data);
                });

                return deferred.promise;
            };

            $scope.ShowLoadingIndicator = false;
            $scope.ShowError = false;
            $scope.ErrorMessage = '';

            $scope.UnshareContent = function () {
                $scope.ShowLoadingIndicator = true;

                var onUnshareSuccess = function (data) {
                    $scope.ShowLoadingIndicator = false;
                    $modalInstance.close();

                    if (typeof ($telerik) != 'undefined') {
                        $telerik.$(document).trigger('modalDialogClosed');
                    }
                };

                var onUnshareError = function (data, status, headers, config) {
                    $scope.ShowError = true;
                    if (data)
                        $scope.ErrorMessage = data.Detail;

                    $scope.ShowLoadingIndicator = false;
                };

                updateWidgetOnSharedContentIdChange('00000000-0000-0000-0000-000000000000').then(onUnshareSuccess, onUnshareError);

            };

            $scope.Cancel = function () {
                if ($modalInstance) {
                    $modalInstance.dismiss('cancel');

                    if (typeof ($telerik) != 'undefined') {
                        $telerik.$(document).trigger('modalDialogClosed');
                    }
                }
            }
        }
    ]);

}) (jQuery);