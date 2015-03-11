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

                         if ($scope.properties.SerializedSocialShareOptionsList &&
                             $scope.properties.SerializedSocialShareOptionsList.PropertyValue) {
                             $scope.socialShareGroups = JSON.parse($scope.properties.SerializedSocialShareOptionsList.PropertyValue || null);
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
                         $scope.properties.SerializedSocialShareOptionsList.PropertyValue = JSON.stringify($scope.socialShareGroups);
                     });
                 })
                 .finally(function () {
                     $scope.feedback.showLoadingIndicator = false;
                 });
         }]);
})(jQuery, angular.module('designer'));
