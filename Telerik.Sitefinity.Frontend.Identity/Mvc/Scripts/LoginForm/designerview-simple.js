(function () {
    angular.module('designer').requires.push('expander', 'kendo.directives', 'sfSelectors');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {

        $scope.externalProviderSelector = { selectedItems: {}, classData: {} };
        
        $scope.updateExternalProvider = function (name, cssClass, value) {
            if (value === false) {
                delete $scope.externalProviderSelector.classData[name];
            }
            else {
                $scope.externalProviderSelector.classData[name] = cssClass;
            }
            $scope.properties.SerializedExternalProviders.PropertyValue = JSON.stringify($scope.externalProviderSelector.classData);
        };

        propertyService.get()
            .then(function (data) {
                if (data && data.Items) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);
                    $scope.externalProviderSelector.classData = $.parseJSON($scope.properties.SerializedExternalProviders.PropertyValue || null) || {};

                    angular.forEach($scope.externalProviderSelector.classData, function (value, key) {
                        $scope.externalProviderSelector.selectedItems[key] = true;
                    });
                }
            });
    }]);

})(jQuery);