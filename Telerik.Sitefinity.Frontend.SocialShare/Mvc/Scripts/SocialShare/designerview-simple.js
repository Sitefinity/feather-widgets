(function ($, designerModule) {

    designerModule.requires.push('expander');

    designerModule.controller('SimpleCtrl',
        ['$scope',
         'propertyService',
         function ($scope, propertyService) {
             $scope.feedback.showLoadingIndicator = true;

             propertyService.get()
                 .then(function (data) {
                     if (data) {
                         $scope.properties = propertyService.toAssociativeArray(data.Items);

                         if ($scope.properties.SerializedSocialShareSectionMap &&
                             $scope.properties.SerializedSocialShareSectionMap.PropertyValue) {
                             $scope.socialShareSectionMap = JSON.parse($scope.properties.SerializedSocialShareSectionMap.PropertyValue);
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
                         $scope.properties.SerializedSocialShareSectionMap.PropertyValue = JSON.stringify($scope.socialShareSectionMap);
                     })
                 })
                 .finally(function () {
                     $scope.feedback.showLoadingIndicator = false;
                 });
         }]);
})(jQuery, angular.module('designer'));
