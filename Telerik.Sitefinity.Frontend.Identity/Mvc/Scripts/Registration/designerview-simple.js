(function ($) {
    angular.module('designer').requires.push('expander', 'sfSelectors');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {

        $scope.feedback.showLoadingIndicator = true;
        $scope.showMessageOnSuccess = true;
        $scope.rolesSelector = { selectedItems: [] };

        $scope.$watch(
            'rolesSelector.selectedItems',
            function (newSelectedItems, oldSelectedItems) {
                if (newSelectedItems !== oldSelectedItems) {
                    if (newSelectedItems) {
                        $scope.properties.SerializedSelectedRoles.PropertyValue = JSON.stringify(newSelectedItems);
                    }
                }
            },
	        true
        );

        propertyService.get()
            .then(function (data) {
                if (data) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);

                    var selectedRoles = $.parseJSON($scope.properties.SerializedSelectedRoles.PropertyValue);

                    if (selectedRoles) {
                        $scope.rolesSelector.selectedItems = selectedRoles;
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
