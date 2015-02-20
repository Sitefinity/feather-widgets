(function ($) {

    var simpleViewModule = angular.module('simpleViewModule', ['expander', 'designer', 'kendo.directives', 'sfFields', 'sfSelectors']);
    angular.module('designer').requires.push('simpleViewModule');

    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', 'serverContext', 'serviceHelper', 'sfMediaService', '$q', function ($scope, propertyService, serverContext, serviceHelper, mediaService, $q) {

        $scope.feedback.showLoadingIndicator = true;
        $scope.thumbnailSizeTempalteUrl = serverContext.getEmbeddedResourceUrl('Telerik.Sitefinity.Frontend', 'client-components/selectors/media/sf-thumbnail-size-selection.html');

        $scope.$watch('model.item.Title.Value', function (newVal, oldVal) {
            if ($scope.model && $scope.model.item && $scope.model.item.Title && (oldVal === $scope.model.title || !$scope.model.title))
                $scope.model.title = $scope.model.item.Title.Value;
        });

        $scope.$watch('model.item.AlternativeText.Value', function (newVal, oldVal) {
            if ($scope.model && $scope.model.item && $scope.model.item.AlternativeText && (oldVal === $scope.model.alternativeText || !$scope.model.alternativeText))
                $scope.model.alternativeText = $scope.model.item.AlternativeText.Value;
        });

        $scope.$watch('model.item.Id', function (newVal, oldVal) {
            // If controller returns Empty guid - no image is selected
            if (newVal === serviceHelper.emptyGuid()) {
                $scope.model.item = { Id: undefined };
            }
            // Cancel is selected with no image selected - close the designer
            else if (newVal === null) {
                $scope.$parent.cancel();
            }
        });

        var updateModel = function () {
            $scope.model = {
                title: $scope.properties.Title.PropertyValue,
                alternativeText: $scope.properties.AlternativeText.PropertyValue,

                item: { Id: $scope.properties.Id.PropertyValue },
                provider: $scope.properties.ProviderName.PropertyValue,
                cssClass: $scope.properties.CssClass.PropertyValue,
                displayMode: $scope.properties.DisplayMode.PropertyValue,
                thumbnail: {
                    name: $scope.properties.ThumbnailName.PropertyValue,
                    url: $scope.properties.ThumbnailUrl.PropertyValue
                },
                openOriginalImageOnClick: $scope.properties.UseAsLink.PropertyValue === 'True' && $scope.properties.LinkedPageId.PropertyValue === serviceHelper.emptyGuid(),
                customSize: $scope.properties.CustomSize.PropertyValue ? JSON.parse($scope.properties.CustomSize.PropertyValue) : null
            };
        };

        var updateProperties = function () {
            var savingPromise;
            if ($scope.model.openOriginalImageOnClick || $scope.properties.UseAsLink.PropertyValue === 'False') {
                $scope.properties.LinkedPageId.PropertyValue = null;
            }

            if ($scope.model.customSize && $scope.model.customSize.Method)
                savingPromise = mediaService.checkCustomThumbnailParams($scope.model.customSize.Method, $scope.model.customSize);
            else {
                var defer = $q.defer();
                defer.resolve('');
                savingPromise = defer.promise;
            }

            return savingPromise.then(function (errorMessage) {
                if ($scope.model.thumbnail && $scope.model.thumbnail.url) {
                    return $scope.model.thumbnail.url;
                }
                else if ($scope.model.customSize && $scope.model.customSize.Method) {
                    return mediaService.getCustomThumbnailUrl($scope.model.item.Id, $scope.model.customSize);
                }
                else {
                    return '';
                }
            })
            .then(function (thumbnailUrl) {
                if (thumbnailUrl) {
                    $scope.model.thumbnail = $scope.model.thumbnail || {};
                    $scope.model.thumbnail.url = thumbnailUrl;
                }

                return mediaService.getLibrarySettings();
            })
            .then(function (settings) {
                var wrapIt = true;

                $scope.properties.Id.PropertyValue = $scope.model.item ? $scope.model.item.Id : null;
                $scope.properties.ProviderName.PropertyValue = $scope.model.provider;
                $scope.properties.Title.PropertyValue = $scope.model.title;
                $scope.properties.AlternativeText.PropertyValue = $scope.model.alternativeText;
                $scope.properties.CssClass.PropertyValue = $scope.model.cssClass;
                $scope.properties.DisplayMode.PropertyValue = $scope.model.displayMode;

                $scope.properties.ThumbnailName.PropertyValue = $scope.model.thumbnail ? $scope.model.thumbnail.name : null;
                $scope.properties.ThumbnailUrl.PropertyValue = $scope.model.thumbnail ? $scope.model.thumbnail.url : null;
                $scope.properties.CustomSize.PropertyValue = JSON.stringify($scope.model.customSize);
            });
        };

        propertyService.get()
            .then(function (data) {
                if (data) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);
                    updateModel();
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
