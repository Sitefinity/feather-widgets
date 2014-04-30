(function () {
    //angular controller responsible for the Simple mode logic
    var simpleDesignerModule = angular.module('simpleDesignerModule', ['controlPropertyServices', 'kendo.directives']);

    //basic controller for the simple designer view
    simpleDesignerModule.controller('SimpleDesignerModuleCtrl', ['$scope', 'PropertyDataService',
            function ($scope, PropertyDataService) {
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
                        if (currentItems[i].PropertyName === "Content") {
                            $scope.ContentProperty = currentItems[i];
                            if (jQuery("#contentEditor")) {
                                var kendoEditor = jQuery("#contentEditor").data("kendoEditor");
                                if (kendoEditor)
                                    kendoEditor.value($scope.ContentProperty.PropertyValue);
                            }
                        }
                        if (currentItems[i].PropertyName === "SharedContentID") {
                            var shareContentIdProperty = currentItems[i];
                            if (shareContentIdProperty.PropertyValue != "00000000-0000-0000-0000-000000000000")
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

                    PropertyDataService.setProperties($scope.Items);
                };

                $telerik.$(document).one("controlPropertiesLoaded", function (e, params) {
                    if (params.Items)
                        updateScopeVariables(params.Items, true);
                });

                PropertyDataService.getProperties(onGetPropertiesSuccess, onGetError);

                $scope.ShowLoadingIndicator = true;

                $scope.$on("$destroy", function () {
                    var kendoContent = jQuery("#viewsPlaceholder iframe.k-content");
                    if (kendoContent.length > 0)
                        kendoContent[0].src = "about:blank";
                });
            }]);
})();