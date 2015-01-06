(function ($, designerModule) {

    designerModule.controller('SimpleCtrl',
        ['$scope',
         'propertyService',
         function ($scope, propertyService) {
             $scope.feedback.showLoadingIndicator = true;

             propertyService.get()
                 .then(function (data) {
                     if (data) {
                         $scope.properties = propertyService.toAssociativeArray(data.Items);

                         if ($scope.properties.SerializeSocialShareSectionMap &&
                             $scope.properties.SerializeSocialShareSectionMap.PropertyValue) {
                             $scope.socialShareSectionMap = JSON.parse($scope.properties.SerializeSocialShareSectionMap.PropertyValue);
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
                         $scope.properties.SerializeSocialShareSectionMap.PropertyValue = JSON.stringify($scope.socialShareSectionMap);
                     })
                 })
                 .finally(function () {
                     $scope.feedback.showLoadingIndicator = false;
                 });
         }]);
})(jQuery, angular.module('designer'));
