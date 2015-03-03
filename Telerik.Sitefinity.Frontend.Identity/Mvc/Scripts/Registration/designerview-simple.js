(function ($) {
    angular.module('designer').requires.push('expander', 'sfSelectors');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {

        $scope.feedback.showLoadingIndicator = true;
        $scope.showMessageOnSuccess = true;
        $scope.rolesSelector = { selectedItemsIds: [] };

        $scope.$watch(
            'rolesSelector.selectedItemsIds',
            function (newSelectedItemsIds, oldSelectedItemsIds) {
                if (newSelectedItemsIds !== oldSelectedItemsIds) {
                    if (newSelectedItemsIds) {
                        $scope.properties.SerializedSelectedRolesIds.PropertyValue = JSON.stringify(newSelectedItemsIds);
                    }
                }
            },
	        true
        );

        propertyService.get()
            .then(function (data) {
                if (data) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);

                    var selectedRolesIds = $.parseJSON($scope.properties.SerializedSelectedRolesIds.PropertyValue);

                    if (selectedRolesIds) {
                        $scope.rolesSelector.selectedItemsIds = selectedRolesIds;
                    }
                }
            },
            function (data) {
                $scope.feedback.showError = true;
                if (data)
                    $scope.feedback.errorMessage = data.Detail;
            })
            .then(function () {
                $scope.feedback.savingHandlers.push(function () {
                    
                });
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})(jQuery);
