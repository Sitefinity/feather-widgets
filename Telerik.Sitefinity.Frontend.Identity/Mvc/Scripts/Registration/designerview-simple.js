(function ($) {
    angular.module('designer').requires.push('expander', 'sfSelectors');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {

        $scope.feedback.showLoadingIndicator = true;
        $scope.rolesSelector = { selectedItems: [] };
        $scope.externalProviderSelector = { selectedItems: {}, classData: {} };        

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

        $scope.updateExternalProvider = function (name, cssClass, value) {
            if (value === false) {
                delete $scope.externalProviderSelector.classData[name];
            }
            else {
                $scope.externalProviderSelector.classData[name] = cssClass;
            }           
            $scope.properties.SerializedExternalProviders.PropertyValue = JSON.stringify($scope.externalProviderSelector.classData);
        }

        propertyService.get()
            .then(function (data) {
                if (data) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);

                    var selectedRoles = $.parseJSON($scope.properties.SerializedSelectedRoles.PropertyValue || null);

                    if (selectedRoles) {
                        $scope.rolesSelector.selectedItems = selectedRoles;
                    }

                    $scope.externalProviderSelector.classData = $.parseJSON($scope.properties.SerializedExternalProviders.PropertyValue || null) || {};
                    
                    angular.forEach($scope.externalProviderSelector.classData, function (value, key) {
                        $scope.externalProviderSelector.selectedItems[key] = true;
                    });                    
                }
            },
            function (data) {
                $scope.feedback.showError = true;
                if (data)
                    $scope.feedback.errorMessage = data.Detail;
            })
            .then(function () {
                $scope.feedback.savingHandlers.push(function () {
                    if ($scope.properties.SuccessfulRegistrationAction.PropertyValue === 'ShowMessage') {
                        $scope.properties.SuccessfulRegistrationPageId.PropertyValue = null;
                    }
                });
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})(jQuery);
