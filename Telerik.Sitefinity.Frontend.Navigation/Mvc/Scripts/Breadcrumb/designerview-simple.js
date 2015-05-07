(function () {
    var designer = angular.module('designer');
    designer.requires.push('expander', 'sfSelectors');

    designer.controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        var emptyGuid = '00000000-0000-0000-0000-000000000000';

        propertyService.get()
           .then(function (data) {
               if (data) {
                   $scope.properties = propertyService.toAssociativeArray(data.Items);
               }
           },
           function (data) {
               $scope.feedback.showError = true;
               if (data)
                   $scope.feedback.errorMessage = data.Detail;
           })
           .then(function () {
               $scope.feedback.savingHandlers.push(function () {
                   if ($scope.properties.BreadcrumbIncludeOption.PropertyValue.toLowerCase() !== 'specificpagepath') {
                       $scope.properties.StartingPageId.PropertyValue = emptyGuid;
                   }
                   if ($scope.properties.StartingPageId.PropertyValue === emptyGuid) {
                       $scope.properties.BreadcrumbIncludeOption.PropertyValue = "CurrentPageFullPath";
                   }
               });
           });
    }]);
})();