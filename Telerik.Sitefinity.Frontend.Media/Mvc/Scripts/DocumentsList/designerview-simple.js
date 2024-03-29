﻿(function ($) {

    var simpleViewModule = angular.module('simpleViewModule', ['expander', 'designer', 'sfSelectors', 'ngSanitize']);
    angular.module('designer').requires.push('simpleViewModule');

    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        var sortOptions = ['PublicationDate DESC', 'LastModified DESC', 'Title ASC', 'Title DESC'];
        var emptyGuid = '00000000-0000-0000-0000-000000000000';

        $scope.feedback.showLoadingIndicator = true;
        $scope.parentSelector = { selectedItemsIds: [] };
        $scope.additionalFilters = {};
        $scope.errors = {};
        $scope.dateFilters = {};
        $scope.shouldClearSelectedParentIds = false;
        $scope.feedback.savingHandlers.push(function () {
            if ($scope.shouldClearSelectedParentIds) {
                $scope.properties.SerializedSelectedParentsIds.PropertyValue = '';
            }
        });

        $scope.updateSortOption = function (newSortOption) {
            if (newSortOption !== 'Custom') {
                $scope.properties.SortExpression.PropertyValue = newSortOption;
            }
        };

        $scope.$watch(
          'properties.ProviderName.PropertyValue',
            function (newProviderName, oldProviderName) {
                newProviderName = newProviderName || "";
                oldProviderName = oldProviderName || "";

                if ($scope.properties && newProviderName !== oldProviderName) {
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
                    if ($scope.properties && newSelectedItemsIds) {
                        $scope.properties.SerializedSelectedParentsIds.PropertyValue = JSON.stringify(newSelectedItemsIds);
                    }
                }
            },
            true
        );

        $scope.$watch(
            'properties.ParentFilterMode.PropertyValue',
            function (newValue, oldValue) {
                if ($scope.properties && newValue !== oldValue) {
                    if (newValue == 'NotApplicable') {
                        $scope.properties.SelectionMode.PropertyValue = 'SelectedItems';
                    }
                    else if (oldValue == 'NotApplicable') {
                        $scope.properties.SelectionMode.PropertyValue = 'AllItems';
                    }
                    else if (oldValue === 'Selected' && oldValue != newValue) {
                        $scope.shouldClearSelectedParentIds = true;
                    }
                    else if (newValue === 'Selected') {
                        $scope.shouldClearSelectedParentIds = false;
                    }
                }
            },
            true
        );

        $scope.$watch(
           'additionalFilters.value',
           function (newAdditionalFilters, oldAdditionalFilters) {
               if ($scope.properties && newAdditionalFilters !== oldAdditionalFilters) {
                   $scope.properties.SerializedAdditionalFilters.PropertyValue = JSON.stringify(newAdditionalFilters);
               }
           },
           true
       );

        $scope.$watch(
            'dateFilters.value',
            function (newDateFilters, oldDateFilters) {
                if ($scope.properties && newDateFilters !== oldDateFilters) {
                    $scope.properties.SerializedDateFilters.PropertyValue = JSON.stringify(newDateFilters);
                }
            },
            true
            );

        propertyService.get()
            .then(function (data) {
                if (data && data.Items) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);

                    var additionalFilters = JSON.parse($scope.properties.SerializedAdditionalFilters.PropertyValue || null);
                    $scope.additionalFilters.value = additionalFilters;

                    var dateFilters = $.parseJSON($scope.properties.SerializedDateFilters.PropertyValue || null);
                    $scope.dateFilters.value = dateFilters;

                    var selectedParentsIds = $scope.properties.SerializedSelectedParentsIds.PropertyValue ?
                                                            JSON.parse($scope.properties.SerializedSelectedParentsIds.PropertyValue) :
                                                            null;
                    if (selectedParentsIds) {
                        $scope.parentSelector.selectedItemsIds = selectedParentsIds;
                    }

                    if (sortOptions.indexOf($scope.properties.SortExpression.PropertyValue) >= 0) {
                        $scope.selectedSortOption = $scope.properties.SortExpression.PropertyValue;
                    }
                    else {
                        $scope.selectedSortOption = 'Custom';
                    }
                }
            },
            function (errorData) {
                $scope.feedback.showError = true;
                if (errorData && errorData.data)
                    $scope.feedback.errorMessage = errorData.data.Detail;
            })
            .then(function () {
                $scope.feedback.savingHandlers.push(function () {

                    $scope.properties.SerializedAdditionalFilters.PropertyValue = JSON.stringify($scope.additionalFilters.value);

                    if ($scope.properties.OpenInSamePage.PropertyValue && $scope.properties.OpenInSamePage.PropertyValue.toLowerCase() === 'true') {
                        $scope.properties.DetailsPageId.PropertyValue = emptyGuid;
                    }
                    else {
                        if (!$scope.properties.DetailsPageId.PropertyValue ||
                                $scope.properties.DetailsPageId.PropertyValue === emptyGuid) {
                            $scope.properties.OpenInSamePage.PropertyValue = true;
                        }
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
