(function () {
    var designer = angular.module('designer');
    designer.requires.push('expander', 'sfSelectors');

    designer.controller('SimpleCtrl', ['$scope', '$controller', function ($scope, $controller) {
        $controller('DefaultCtrl', { $scope: $scope });

        $scope.multiPageSelector = { selectedPageIds: null };

        $scope.$watch(
            'multiPageSelector.selectedPageIds',
            function (newSelectedPageIds, oldSelectedPageIds) {
                if (newSelectedPageIds !== oldSelectedPageIds) {
                    $scope.properties.SerializedSelectedPageIds.PropertyValue = JSON.stringify(newSelectedPageIds);
                }
            },
            true
        );

        $scope.$watch(
            'properties.SerializedSelectedPageIds.PropertyValue',
            function (newSelectedPageIds, oldSelectedPageIds) {
                if (newSelectedPageIds !== oldSelectedPageIds || !$scope.multiPageSelector.selectedPageIds) {
                    $scope.multiPageSelector.selectedPageIds = $.parseJSON(newSelectedPageIds);
                }
            },
            true
        );
    }]);
})();