(function ($) {

    var simpleViewModule = angular.module('simpleViewModule', ['expander', 'designer', 'sfSelectors']);
    angular.module('designer').requires.push('simpleViewModule');

    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        var sortOptions = ['PublicationDate DESC', 'LastModified DESC', 'Title ASC', 'Title DESC', 'Ordinal ASC'];
        var emptyGuid = '00000000-0000-0000-0000-000000000000';

        $scope.feedback.showLoadingIndicator = true;
        $scope.listSelector = { selectedItemsIds: [] };
        $scope.additionalFilters = {};
        $scope.errors = {};
        $scope.dateFilters = {};

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
                  $scope.properties.SerializedSelectedItemsIds.PropertyValue = null;
              }
          },
          true
      );

        $scope.$watch(
            'listSelector.selectedItemsIds',
            function (newSelectedItemsIds, oldSelectedItemsIds) {
                if (newSelectedItemsIds !== oldSelectedItemsIds) {
                    if ($scope.properties && newSelectedItemsIds) {
                        $scope.properties.SerializedSelectedItemsIds.PropertyValue = JSON.stringify(newSelectedItemsIds);
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


                    var selectedListIds = $scope.properties.SerializedSelectedItemsIds.PropertyValue ?
                                                            JSON.parse($scope.properties.SerializedSelectedItemsIds.PropertyValue) :
                                                            null;
                    if (selectedListIds) {
                        $scope.listSelector.selectedItemsIds = selectedListIds;
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
                });
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})(jQuery);
