(function () {
    var designer = angular.module('designer');
    designer.requires.push('expander', 'sfSelectors');

    designer.controller('SimpleCtrl', ['$scope', '$controller', function ($scope, $controller) {
        $controller('DefaultCtrl', { $scope: $scope });

        $scope.multiPageSelector = { selectedPages: null, externalPages: null };

        $scope.$watch(
            'multiPageSelector.selectedPages',
            function (newSelectedPages, oldSelectedPages) {
                if (newSelectedPages !== oldSelectedPages) {
                    $scope.properties.SerializedSelectedPages.PropertyValue = JSON.stringify(newSelectedPages);
                }
            },
            true
        );

        $scope.$watch(
            'properties.SerializedSelectedPages.PropertyValue',
            function (newSelectedPages, oldSelectedPages) {
                if (newSelectedPages !== oldSelectedPages || !$scope.multiPageSelector.selectedPages) {
                    $scope.multiPageSelector.selectedPages = $.parseJSON(newSelectedPages);
                }
            },
            true
        );

        $scope.$watch(
    'multiPageSelector.externalPages',
    function (newExternalPages, oldExternalPages) {
        if (newExternalPages !== oldExternalPages) {
            $scope.properties.SerializedExternalPages.PropertyValue = JSON.stringify(newExternalPages);
        }
    },
    true
);

        $scope.$watch(
            'properties.SerializedExternalPages.PropertyValue',
            function (newExternalPages, oldExternalPages) {
                if (newExternalPages !== oldExternalPages || !$scope.multiPageSelector.externalPages) {
                    $scope.multiPageSelector.externalPages = $.parseJSON(newExternalPages);
                }
            },
            true
        );
    }]);
})();