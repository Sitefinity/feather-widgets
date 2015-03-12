(function ($) {
    angular.module('designer').requires.push('expander', 'sfSelectors', 'sfThumbnailSizeSelection');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        var sortOptions = ['PublicationDate DESC', 'LastModified DESC', 'Title ASC', 'Title DESC'];

        var emptyGuid = '00000000-0000-0000-0000-000000000000';

        $scope.feedback.showLoadingIndicator = true;
        $scope.additionalFilters = {};
        $scope.parentSelector = { selectedItemsIds: [] };
        $scope.thumbnailSizeModel = {};
        $scope.imageSizeModel = {};
        $scope.errors = {};

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
	                $scope.properties.ParentFilterMode.PropertyValue = 'All';
	                $scope.properties.SerializedSelectedParentsIds.PropertyValue = null;
	            }
	        },
	        true
        );

        $scope.$watch(
            'parentSelector.selectedItemsIds',
            function (newSelectedItemsIds, oldSelectedItemsIds) {
                if (newSelectedItemsIds !== oldSelectedItemsIds) {
                    if (newSelectedItemsIds) {
                        $scope.properties.SerializedSelectedParentsIds.PropertyValue = JSON.stringify(newSelectedItemsIds);
                    }
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

        $scope.$watch(
            'thumbnailSizeModel',
            function (newValue, oldValue) {
                if (newValue !== oldValue) {
                    $scope.properties.SerializedThumbnailSizeModel.PropertyValue = JSON.stringify(newValue);
                }
            },
            true
        );

        $scope.$watch(
            'imageSizeModel',
            function (newValue, oldValue) {
                if (newValue !== oldValue) {
                    $scope.properties.SerializedImageSizeModel.PropertyValue = JSON.stringify(newValue);
                }
            },
            true
        );

        $scope.updateSortOption = function (newSortOption) {
            if (newSortOption !== 'Custom') {
                $scope.properties.SortExpression.PropertyValue = newSortOption;
            }
        };

        propertyService.get()
            .then(function (data) {
                if (data) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);

                    var additionalFilters = JSON.parse($scope.properties.SerializedAdditionalFilters.PropertyValue || null);
                    $scope.additionalFilters.value = additionalFilters;

                    var selectedParentsIds = $scope.properties.SerializedSelectedParentsIds.PropertyValue ? JSON.parse($scope.properties.SerializedSelectedParentsIds.PropertyValue) : null;
                    if (selectedParentsIds) {
                        $scope.parentSelector.selectedItemsIds = selectedParentsIds;
                    }

                    if (sortOptions.indexOf($scope.properties.SortExpression.PropertyValue) >= 0) {
                        $scope.selectedSortOption = $scope.properties.SortExpression.PropertyValue;
                    }
                    else {
                        $scope.selectedSortOption = 'Custom';
                    }

                    var thumbnailSizeModel = JSON.parse($scope.properties.SerializedThumbnailSizeModel.PropertyValue || null);
                    if (thumbnailSizeModel) {
                        $scope.thumbnailSizeModel = thumbnailSizeModel;
                    }

                    var imageSizeModel = JSON.parse($scope.properties.SerializedImageSizeModel.PropertyValue || null);
                    if (imageSizeModel) {
                        $scope.imageSizeModel = imageSizeModel;
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
                    if ($scope.properties.OpenInSamePage.PropertyValue && $scope.properties.OpenInSamePage.PropertyValue.toLowerCase() === 'true') {
                        $scope.properties.DetailsPageId.PropertyValue = emptyGuid;
                    }
                    else {
                        if (!$scope.properties.DetailsPageId.PropertyValue ||
                                $scope.properties.DetailsPageId.PropertyValue === emptyGuid) {
                            $scope.properties.OpenInSamePage.PropertyValue = true;
                        }
                    }

                    if ($scope.properties.SelectionMode.PropertyValue === 'FilteredItems' &&
                        $scope.additionalFilters.value &&
                        $scope.additionalFilters.value.QueryItems &&
                        $scope.additionalFilters.value.QueryItems.length === 0) {
                        $scope.properties.SelectionMode.PropertyValue = 'AllItems';
                    }

                    if ($scope.properties.SelectionMode.PropertyValue !== 'FilteredItems') {
                        $scope.properties.SerializedAdditionalFilters.PropertyValue = null;
                    }

                    if ($scope.properties.ParentFilterMode.PropertyValue !== 'Selected') {
                        $scope.properties.SerializedSelectedParentsIds.PropertyValue = null;
                    }
                });
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})(jQuery);
