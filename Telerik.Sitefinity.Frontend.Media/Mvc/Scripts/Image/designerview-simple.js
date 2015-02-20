(function ($) {

    var simpleViewModule = angular.module('simpleViewModule', ['expander', 'designer', 'kendo.directives', 'sfFields', 'sfSelectors']);
    angular.module('designer').requires.push('simpleViewModule');

    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', 'serverContext', 'serviceHelper', 'sfMediaService', '$q', function ($scope, propertyService, serverContext, serviceHelper, mediaService, $q) {

        $scope.feedback.showLoadingIndicator = true;
        $scope.thumbnailSizeTempalteUrl = serverContext.getEmbeddedResourceUrl('Telerik.Sitefinity.Frontend', 'client-components/selectors/media/sf-thumbnail-size-selection.html');

        $scope.$watch('properties.Id.PropertyValue', function (newVal, oldVal) {
            // If controller returns Empty guid - no image is selected
            if (newVal === serviceHelper.emptyGuid()) {
                $scope.properties.Id.PropertyValue = undefined;
            }
                // Cancel is selected with no image selected - close the designer
            else if (newVal === null) {
                $scope.$parent.cancel();
            }
        });

        $scope.$watch('model', function (newVal, oldVal) {
            if (newVal && newVal.item && newVal.item.Id) {
                if (!$scope.properties.Title.PropertyValue && newVal.item.Title && newVal.item.Title.Value) {
                    $scope.properties.Title.PropertyValue = newVal.item.Title.Value;
                }
                if (!$scope.properties.AlternativeText.PropertyValue && newVal.item.AlternativeText && newVal.item.AlternativeText.Value) {
                    $scope.properties.AlternativeText.PropertyValue = newVal.item.AlternativeText.Value;
                }

                $scope.properties.ThumbnailName.PropertyValue = newVal.thumbnail ? newVal.thumbnail.name : null;
                $scope.properties.ThumbnailUrl.PropertyValue = newVal.thumbnail ? newVal.thumbnail.url : null;
                $scope.properties.CustomSize.PropertyValue = JSON.stringify(newVal.customSize);
                $scope.properties.DisplayMode.PropertyValue = newVal.displayMode;

                $scope.openOriginalImageOnClick = $scope.properties.UseAsLink.PropertyValue === 'True' && $scope.properties.LinkedPageId.PropertyValue === serviceHelper.emptyGuid()
            }
        }, true);

        $scope.hasErrors = function () {
            return !$scope.properties.Title.PropertyValue || $scope.properties.Title.PropertyValue.length > 35 || $scope.properties.AlternativeText.PropertyValue.length > 35;
        };

        var updateProperties = function () {
            var savingPromise;

            var parsedCustomSize = JSON.parse($scope.properties.CustomSize.PropertyValue);

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
                    return $scope.properties.ThumbnailUrl.PropertyValue;
                }
                else if (parsedCustomSize && parsedCustomSize.Method) {
                    return mediaService.getCustomThumbnailUrl($scope.properties.Id.PropertyValue, parsedCustomSize);
                }
                else {
                    return '';
                }
            })
            .then(function (thumbnailUrl) {
                return mediaService.getLibrarySettings();
            });
        };

        propertyService.get()
            .then(function (data) {
                if (data) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);

                    if ($scope.properties.Id.PropertyValue !== serviceHelper.emptyGuid()) {
                        mediaService.images.getById($scope.properties.Id.PropertyValue, $scope.properties.ProviderName.PropertyValue).then(function (data) {
                            if (!data || !data.Item || !data.Item.Visible) {
                                $scope.properties.Id.PropertyValue = serviceHelper.emptyGuid();
                            }
                        });
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
                        customSize: $scope.properties.CustomSize.PropertyValue ? JSON.parse($scope.properties.CustomSize.PropertyValue) : null
                    };
                }
            },
            function (data) {
                $scope.feedback.showError = true;
                if (data)
                    $scope.feedback.errorMessage = data.Detail;
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
