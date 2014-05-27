(function ($) {
    var designerModule = angular.module('designer');

    //basic controller for the simple designer view
    designerModule.controller('SimpleCtrl', ['$scope', 'propertyService',
        function ($scope, propertyService) {
            // ------------------------------------------------------------------------
            // event handlers
            // ------------------------------------------------------------------------

            var onGetPropertiesSuccess = function (data) {
                if (data.Items) {
                    updateScopeVariables(data.Items, false);
                }
                $scope.ShowLoadingIndicator = false;
            };

            var onGetError = function (data, status, headers, config) {
                $scope.ShowError = true;
                if (data)
                    $scope.ErrorMessage = data.Detail;

                $scope.ShowLoadingIndicator = false;
            };

            // ------------------------------------------------------------------------
            // helper methods
            // ------------------------------------------------------------------------

            var updateScopeVariables = function (currentItems, applyScopeChanges) {
                $scope.Items = currentItems;
                $scope.Properties = propertyService.toAssociativeArray(currentItems);
                $scope.IsShared = $scope.Properties.SharedContentID.PropertyValue != '00000000-0000-0000-0000-000000000000';

                //k-value does not set the kendo editor value when the value is deferred so we do this manually
                //when we have the data.
                if ($('#contentEditor')) {
                    var kendoEditor = $('#contentEditor').data('kendoEditor');
                    if (kendoEditor)
                        kendoEditor.value($scope.Properties.Content.PropertyValue);
                }
            };

            // ------------------------------------------------------------------------
            // scope variables and set up
            // ------------------------------------------------------------------------

            $scope.IsShared = false;
            $scope.ShowLoadingIndicator = true;

            $scope.contentChange = function (e) {
                $scope.Properties.Content.PropertyValue = e.sender.value();

                propertyService.set($scope.Items);
            };

            $telerik.$(document).one('controlPropertiesLoaded', function (e, params) {
                if (params.Items)
                    updateScopeVariables(params.Items, true);
            });

            propertyService.get().then(onGetPropertiesSuccess, onGetError);

            //Fixes a bug for modal dialogs with iframe in them for IE.
            $scope.$on('$destroy', function () {
                var kendoContent = $('#viewsPlaceholder iframe.k-content');
                if (kendoContent.length > 0)
                    kendoContent[0].src = 'about:blank';
            });
        }
    ]);
})(jQuery);