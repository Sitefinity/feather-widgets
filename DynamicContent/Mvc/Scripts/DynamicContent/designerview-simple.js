(function ($) {
    angular.module('designer').requires.push('expander', 'sfSelectors');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', 'serverData', function ($scope, propertyService, serverData) {
        $scope.feedback.showLoadingIndicator = true;
        $scope.additionalFilters = {};
        $scope.itemSelector = { selectedItemsIds: [] };
        $scope.parentSelector = { selectedItemsIds: [] };
        $scope.itemType = serverData.get('itemType');
        $scope.parentTypes = $.parseJSON(serverData.get('parentTypes'));

        $scope.$watch(
            'additionalFilters.value',
            function (newAdditionalFilters, oldAdditionalFilters) {
                if (newAdditionalFilters !== oldAdditionalFilters) {
                    $scope.properties.SerializedAdditionalFilters.PropertyValue = JSON.stringify(newAdditionalFilters);
                }
            },
            true
        );

        $scope.$watch(
	        'properties.ProviderName.PropertyValue',
	        function (newProviderName, oldProviderName) {
	            if (newProviderName !== oldProviderName) {
	                $scope.properties.SelectionMode.PropertyValue = 'AllItems';
	                $scope.properties.SerializedSelectedItemsIds.PropertyValue = null;
	            }
	        },
	        true
        );

        $scope.$watch(
            'itemSelector.selectedItemsIds',
            function (newSelectedItemsIds, oldSelectedItemsIds) {
                if (newSelectedItemsIds !== oldSelectedItemsIds) {
                    $scope.properties.SerializedSelectedItemsIds.PropertyValue = JSON.stringify(newSelectedItemsIds);
	            }
	        },
	        true
        );

        $scope.$watch(
            'parentSelector.selectedItemsIds',
            function (newSelectedItemsIds, oldSelectedItemsIds) {
                if (newSelectedItemsIds !== oldSelectedItemsIds) {
                    $scope.properties.SerializedSelectedParentsIds.PropertyValue = JSON.stringify(newSelectedItemsIds);
                }
            },
            true
        );

        $scope.$watch(
            'properties.ParentFilterMode.PropertyValue',
            function (newValue, oldValue) {
                if (newValue !== oldValue) {
                    if (newValue == 'NotApplicable') {
                        $scope.properties.SelectionMode.PropertyValue = 'SelectedItems';
                    }
                    else if (oldValue == 'NotApplicable') {
                        $scope.properties.SelectionMode.PropertyValue = 'AllItems';
                    }
                }
            },
            true
        );

        propertyService.get()
            .then(function (data) {
                if (data) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);

                    var additionalFilters = $.parseJSON($scope.properties.SerializedAdditionalFilters.PropertyValue);
                    $scope.additionalFilters.value = additionalFilters;

                    var selectedItemsIds = $.parseJSON($scope.properties.SerializedSelectedItemsIds.PropertyValue);
                    if (selectedItemsIds) {
                        $scope.itemSelector.selectedItemsIds = selectedItemsIds;
                    }

                    var selectedParentsIds = $.parseJSON($scope.properties.SerializedSelectedParentsIds.PropertyValue);
                    if (selectedParentsIds) {
                        $scope.parentSelector.selectedItemsIds = selectedParentsIds;
                    }

                    if ($scope.parentTypes.length > 0 && $scope.properties.CurrentlyOpenParentType && !$scope.properties.CurrentlyOpenParentType.PropertyValue) {
                        $scope.properties.CurrentlyOpenParentType.PropertyValue = $scope.parentTypes[0].TypeName;
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
                    if (!$scope.properties.DetailsPageId.PropertyValue ||
                            $scope.properties.DetailsPageId.PropertyValue === '00000000-0000-0000-0000-000000000000') {
                        $scope.properties.OpenInSamePage.PropertyValue = true;
                    }
                    if ($scope.properties.SelectionMode.PropertyValue === "FilteredItems" &&
                        $scope.additionalFilters.value &&
                        $scope.additionalFilters.value.QueryItems &&
                        $scope.additionalFilters.value.QueryItems.length === 0) {
                        $scope.properties.SelectionMode.PropertyValue = 'AllItems';
                    }
                })
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})(jQuery);
