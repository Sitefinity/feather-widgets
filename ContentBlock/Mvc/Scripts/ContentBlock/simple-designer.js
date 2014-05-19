(function ($) {
    var designerModule = angular.module('designer');

    //basic controller for the simple designer view
    designerModule.controller('simpleCtrl', ['$scope', 'propertyService',
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
                for (var i = 0; i < currentItems.length; i++) {
                    if (currentItems[i].PropertyName === 'Content') {
                        $scope.ContentProperty = currentItems[i];
                        if ($('#contentEditor')) {
                            var kendoEditor = $('#contentEditor').data('kendoEditor');
                            if (kendoEditor)
                                kendoEditor.value($scope.ContentProperty.PropertyValue);
                        }
                    }
                    if (currentItems[i].PropertyName === 'SharedContentID') {
                        var sharedContentIdProperty = currentItems[i];
                        if (sharedContentIdProperty.PropertyValue != '00000000-0000-0000-0000-000000000000')
                            $scope.IsShared = true;
                        else
                            $scope.IsShared = false;                           
                    }
                }

                if(applyScopeChanges)
                    $scope.$apply();
            };

            // ------------------------------------------------------------------------
            // scope variables and set up
            // ------------------------------------------------------------------------

            $scope.IsShared = false;

            $scope.contentChange = function (e) {
                $scope.ContentProperty.PropertyValue = e.sender.value();

                propertyService.set($scope.Items);
            };

            $telerik.$(document).one('controlPropertiesLoaded', function (e, params) {
                if (params.Items)
                    updateScopeVariables(params.Items, true);
            });

            propertyService.get().then(onGetPropertiesSuccess, onGetError);

            $scope.ShowLoadingIndicator = true;

            $scope.$on('$destroy', function () {
                var kendoContent = $('#viewsPlaceholder iframe.k-content');
                if (kendoContent.length > 0)
                    kendoContent[0].src = 'about:blank';
            });
        }
    ]);
})(jQuery);