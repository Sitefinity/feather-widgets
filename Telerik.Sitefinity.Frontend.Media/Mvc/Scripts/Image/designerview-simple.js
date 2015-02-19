(function ($) {

    var simpleViewModule = angular.module('simpleViewModule', ['expander', 'designer', 'kendo.directives', 'sfFields', 'sfSelectors']);
    angular.module('designer').requires.push('simpleViewModule');
    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', 'serverContext', 'sfMediaMarkupService', function ($scope, propertyService, serverContext, mediaMarkupService) {
        $scope.feedback.showLoadingIndicator = true;
        $scope.thumbnailSizeTempalteUrl = serverContext.getEmbeddedResourceUrl('Telerik.Sitefinity.Frontend', 'client-components/selectors/media/sf-thumbnail-size-selection.html');

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
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})(jQuery);
