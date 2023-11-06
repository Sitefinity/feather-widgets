(function ($) {
    var EMPTY_GUID = '00000000-0000-0000-0000-000000000000';

    var simpleViewModule = angular.module('simpleViewModule', ['designer', 'kendo.directives', 'sharedContentServices', 'sfFields', 'sfSelectors', 'ngSanitize', 'serverDataModule']);
    angular.module('designer').requires.push('simpleViewModule');

    simpleViewModule.factory('contentBlockService', ['dialogFeedbackService', 'sharedContentService', function (dialogFeedbackService, sharedContentService) {
        var contentItem;
        var properties;

        var unlockContentItem = function () {
            return sharedContentService.deleteTemp(properties.SharedContentID.PropertyValue);
        };

        var updateContentItem = function () {
            return sharedContentService.update(contentItem, properties.Content.PropertyValue, properties.ProviderName.PropertyValue);
        };

        return function (data) {
            properties = data;

            var isShared = properties.SharedContentID.PropertyValue != EMPTY_GUID;

            if (isShared && !contentItem) {
                var checkOut = true;
                return sharedContentService.get(properties.SharedContentID.PropertyValue, properties.ProviderName.PropertyValue, checkOut)
                    .then(function (data) {
                        contentItem = data;
                        if (contentItem) {
                            properties.Content.PropertyValue = contentItem.Item.Content.Value;
                            dialogFeedbackService.savingHandlers.push(updateContentItem);
                            dialogFeedbackService.cancelingHandlers.push(unlockContentItem);
                        }
                    }, function () {
                        properties.Content.PropertyValue = '';
                        properties.SharedContentID.PropertyValue = EMPTY_GUID;
                    });
            }
        };
    }]);

    //basic controller for the simple designer view
    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', 'sharedContentService', 'contentBlockService', 'serverData',
        function ($scope, propertyService, sharedContentService, contentBlockService, serverData) {
            var contentItem;

            // ------------------------------------------------------------------------
            // event handlers
            // ------------------------------------------------------------------------

            var onGetPropertiesSuccess = function (data) {
                if (data && data.Items) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);
                    $scope.isShared = $scope.properties.SharedContentID.PropertyValue != EMPTY_GUID;

                    var mergeTags = serverData.get("mergeTags");
                    if (mergeTags) {
                        $scope.mergeTags = JSON.parse(mergeTags);
                        $scope.selectedMergeTag = $scope.mergeTags[0];
                    }

                    kendo.bind();

                    if ($scope.isShared) {
                        return contentBlockService($scope.properties);
                    }
                }
            };

            // ------------------------------------------------------------------------
            // scope variables and set up
            // ------------------------------------------------------------------------

            $scope.feedback.showLoadingIndicator = true;

            $scope.isShared = false;

            $scope.insertMergeTag = function () {
                if ($scope.editor) {
                    var range = $scope.editor.getRange();
                    $scope.editor.exec("insertHtml", { html: $scope.selectedMergeTag.ComposedTag, split: true, range: range });
                }
            };

            $scope.$on('kendoWidgetCreated', function (event, widget) {
                if (widget.wrapper && widget.wrapper.is('.k-editor')) {
                    $scope.editor = widget;

                    $("[ng-click=\"openVideoSelector()\"]").parent().hide();
                }
            });

            propertyService.get()
                .then(onGetPropertiesSuccess)
                .catch(function (errorData) {
                    $scope.feedback.showError = true;
                    if (errorData && errorData.data)
                        $scope.feedback.errorMessage = errorData.data.Detail;
                })
                .finally(function () {
                    $scope.isShared = $scope.properties.SharedContentID.PropertyValue != EMPTY_GUID;
                    $scope.feedback.showLoadingIndicator = false;
                });
        }
    ]);
})(jQuery);

function saveProperties(saveToAllTranslations) {
    var scope = angular.element('#viewsPlaceholder').scope();
    scope.$apply(function () {
        scope.$broadcast('close');
    });
    scope.save(saveToAllTranslations);
}
