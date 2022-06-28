(function ($) {

    var simpleViewModule = angular.module('simpleViewModule', ['expander', 'designer', 'kendo.directives', 'sfFields', 'sfSelectors', 'sfThumbnailSizeSelection', 'ngSanitize']);
    angular.module('designer').requires.push('simpleViewModule');

    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', 'serverContext', 'serviceHelper', 'sfMediaService', '$q', function ($scope, propertyService, serverContext, serviceHelper, mediaService, $q) {
        $scope.initialized = false;
        $scope.feedback.showLoadingIndicator = true;
        $scope.thumbnailSizeTempalteUrl = serverContext.getEmbeddedResourceUrl('Telerik.Sitefinity.Frontend', 'client-components/selectors/media/sf-thumbnail-size-selection.html');
        $scope.mediaType = 'images';
        $scope.viewType = 'Telerik.Sitefinity.Frontend.Media.Mvc.Controllers.ImageController';

        var onPostPropertiesInitialized = function () {
            $scope.$watch('properties.Id.PropertyValue', function (newVal, oldVal) {
                // If controller returns Empty guid - no image is selected
                if ($scope.properties && newVal === serviceHelper.emptyGuid()) {
                    $scope.properties.Id.PropertyValue = undefined;
                }
                // Cancel is selected with no image selected - close the designer
                else if (newVal === null) {
                    $scope.$parent.cancel();
                }
            });

            $scope.$watch('model', function (newVal, oldVal) {
                if ($scope.properties && newVal && newVal.item && newVal.item.Id) {
                    // update the title and the alt text only when the widget is new and no image is loaded 
                    // or when replacing the exisitng image
                    if (($scope.properties.Id.PropertyId === serviceHelper.emptyGuid()) ||
                        (oldVal.item.Id && oldVal.item.Id !== newVal.item.Id)) {
                        if (newVal.item.Title && newVal.item.Title.Value) {
                            $scope.properties.Title.PropertyValue = newVal.item.Title.Value;
                        }

                        if (newVal.item.AlternativeText && newVal.item.AlternativeText.Value) {
                            $scope.properties.AlternativeText.PropertyValue = newVal.item.AlternativeText.Value;
                        }
                    }

                    $scope.properties.ThumbnailName.PropertyValue = newVal.thumbnail ? newVal.thumbnail.name : null;
                    $scope.properties.ThumbnailUrl.PropertyValue = newVal.thumbnail ? newVal.thumbnail.url : null;
                    $scope.properties.CustomSize.PropertyValue = JSON.stringify(newVal.customSize);
                    $scope.properties.Responsive.PropertyValue = newVal.responsive;
                    $scope.properties.DisplayMode.PropertyValue = newVal.displayMode;

                    $scope.openOriginalImageOnClick = $scope.properties.UseAsLink.PropertyValue === 'True' && $scope.properties.LinkedPageId.PropertyValue === serviceHelper.emptyGuid();
                }
            }, true);

            $scope.hasErrors = function () {
                return !$scope.properties.Title || !$scope.properties.Title.PropertyValue;
            };
        };

        var updateProperties = function () {
            var savingPromise;

            var parsedCustomSize = JSON.parse($scope.properties.CustomSize.PropertyValue || null);

            if ($scope.openOriginalImageOnClick || $scope.properties.UseAsLink.PropertyValue === 'False') {
                $scope.properties.LinkedPageId.PropertyValue = null;
            }

            if (parsedCustomSize && parsedCustomSize.Method)
                savingPromise = mediaService.checkCustomThumbnailParams(parsedCustomSize.Method, parsedCustomSize);
            else {
                var defer = $q.defer();
                defer.resolve('');
                savingPromise = defer.promise;
            }

            return savingPromise.then(function (errorMessage) {
                if ($scope.properties.ThumbnailUrl.PropertyValue) {
                    return mediaService.getCustomThumbnailUrl($scope.properties.Id.PropertyValue, parsedCustomSize);
                }
                else if (parsedCustomSize && parsedCustomSize.Method) {
                    return mediaService.getCustomThumbnailUrl($scope.properties.Id.PropertyValue, parsedCustomSize);
                }
                else {
                    return '';
                }
            })
            .then(function (thumbnailUrl) {
                if (thumbnailUrl) {
                    $scope.properties.ThumbnailUrl.PropertyValue = thumbnailUrl;
                }

                // return mediaService.getLibrarySettings();
            });
        };

        var showError = function (err) {
            $scope.feedback.showError = true;
            if (err)
                $scope.feedback.errorMessage = err;
        };

        propertyService.get()
            .then(function (data) {
                if (data && data.Items) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);

                    if ($scope.properties.Id.PropertyValue !== serviceHelper.emptyGuid()) {
                        mediaService.images.getById($scope.properties.Id.PropertyValue, $scope.properties.ProviderName.PropertyValue).then(function (data) {
                            if (!data || !data.Item || !data.Item.Visible) {
                                $scope.properties.Id.PropertyValue = serviceHelper.emptyGuid();
                            }
                        });
                    }

                    var responsiveValue;
                    if ($scope.properties.Responsive && $scope.properties.Responsive.PropertyValue && $scope.properties.Responsive.PropertyValue === "True") {
                        responsiveValue = true;
                    }

                    // Needs model population because of the thumbnails
                    $scope.model = $scope.model || {
                        item: {
                            Id: undefined
                        },
                        displayMode: $scope.properties.DisplayMode.PropertyValue,
                        thumbnail: {
                            name: $scope.properties.ThumbnailName.PropertyValue,
                            url: $scope.properties.ThumbnailUrl.PropertyValue
                        },
                        customSize: $scope.properties.CustomSize.PropertyValue ? JSON.parse($scope.properties.CustomSize.PropertyValue) : null,
                        responsive: responsiveValue
                    };

                    $scope.initialized = true;
                } else {
                    throw "Error getting properties data.";
                }
            }, function (errorData) {
                var err = '';
                if (errorData && errorData.data)
                    err = errorData.data.Detail;
                showError(err);
            })
            .then(function () {
                onPostPropertiesInitialized();
            }, function (errorData) {
                var err = '';
                if (errorData && errorData.data)
                    err = errorData.data.Detail;
                showError(err);
            })
            .then(function () {
                $scope.feedback.savingHandlers.push(function () {
                    return updateProperties();
                });
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})(jQuery);