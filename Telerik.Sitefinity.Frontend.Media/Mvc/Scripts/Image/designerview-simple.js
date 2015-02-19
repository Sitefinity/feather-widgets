(function ($) {

    var simpleViewModule = angular.module('simpleViewModule', ['expander', 'designer', 'kendo.directives', 'sfFields', 'sfSelectors']);
    angular.module('designer').requires.push('simpleViewModule');
    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', 'serverContext', 'sfMediaService', 'sfMediaMarkupService', '$q', function ($scope, propertyService, serverContext, mediaService, mediaMarkupService, $q) {
        $scope.feedback.showLoadingIndicator = true;
        $scope.thumbnailSizeTempalteUrl = serverContext.getEmbeddedResourceUrl('Telerik.Sitefinity.Frontend', 'client-components/selectors/media/sf-thumbnail-size-selection.html');

        $scope.$watch('model.item.Title.Value', function (newVal, oldVal) {
            if ($scope.model.item && $scope.model.item.Title && (oldVal === $scope.model.title || !$scope.model.title))
                $scope.model.title = $scope.model.item.Title.Value;
        });

        $scope.$watch('model.item.AlternativeText.Value', function (newVal, oldVal) {
            if ($scope.model.item && $scope.model.item.AlternativeText && (oldVal === $scope.model.alternativeText || !$scope.model.alternativeText))
                $scope.model.alternativeText = $scope.model.item.AlternativeText.Value;
        });

        $scope.$watch('model.item.Id', function (newVal, oldVal) {
            if (newVal !== oldVal) {
                $scope.properties.Id.PropertyValue = newVal;
            }
        });

        $scope.$watch('model.provider', function (newVal, oldVal) {
            if (newVal !== oldVal) {
                $scope.properties.ProviderName.PropertyValue = newVal;
            }
        });

        propertyService.get()
            .then(function (data) {
                if (data) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);
                    var markup = $scope.properties.Markup.PropertyValue || '';
                    $scope.model = mediaMarkupService.image.properties(markup);
                }
            },
            function (data) {
                $scope.feedback.showError = true;
                if (data)
                    $scope.feedback.errorMessage = data.Detail;
            })
            .then(function () {
                $scope.feedback.savingHandlers.push(function () {
                    var savingPromise;

                    if ($scope.model.customSize && $scope.model.customSize.Method)
                        savingPromise = mediaService.checkCustomThumbnailParams($scope.model.customSize.Method, $scope.model.customSize);
                    else {
                        var defer = $q.defer();
                        defer.resolve('');
                        savingPromise = defer.promise;
                    }

                    savingPromise.then(function (errorMessage) {
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
                        var markup = mediaMarkupService.image.markup($scope.model, settings, wrapIt);
                        $scope.properties.Markup.PropertyValue = markup;
                    });
                });
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})(jQuery);
