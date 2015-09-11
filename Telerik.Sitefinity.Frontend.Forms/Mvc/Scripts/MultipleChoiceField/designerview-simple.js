(function () {
    var simpleViewModule = angular.module('simpleViewModule', ['designer', 'sfSelectors']);
    angular.module('designer').requires.push('expander', 'simpleViewModule');
    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', '$q', function ($scope, propertyService, $q) {
        $scope.feedback.showLoadingIndicator = true;
        $scope.currentItems = [];
        $scope.defaultValue = null;

        $scope.$watch(
            'defaultValue',
            function (newDefaultValue, oldDefaultValue) {
                if (newDefaultValue) {
                    $scope.properties.Model.ValidatorDefinition.Required.PropertyValue = 'False';
                }
            },
            true
        );

        propertyService.get()
            .then(function (data) {
                if (data) {
                    $scope.properties = propertyService.toHierarchyArray(data.Items);
                    $scope.currentItems = JSON.parse($scope.properties.Model.SerializedChoices.PropertyValue);
                    $scope.defaultValue = $scope.properties.Model.MetaField.DefaultValue.PropertyValue;
                }
            })
            .catch(function (data) {
                $scope.feedback.showError = true;
                if (data) {
                    $scope.feedback.errorMessage = data.Detail;
                }
            })
            .then(function () {
                $scope.feedback.savingHandlers.push(function () {
                    var deferred = $q.defer();

                    if ($scope.currentItems.indexOf('') === -1) {
                        $scope.properties.Model.SerializedChoices.PropertyValue = JSON.stringify($scope.currentItems);
                        $scope.properties.Model.MetaField.DefaultValue.PropertyValue = $scope.defaultValue;
                        deferred.resolve();
                    }
                    else {
                        deferred.reject("Please, specify values for all of the choices.");
                    }

                    return deferred.promise;
                });
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });

        $scope.sortItems = function (e) {
            var element = $scope.currentItems[e.oldIndex];
            $scope.currentItems.splice(e.oldIndex, 1);
            $scope.currentItems.splice(e.newIndex, 0, element);
        };

        $scope.itemClicked = function (ev) {
            ev.target.focus();
        };

        $scope.setDefault = function (item) {
            if (item === $scope.defaultValue) {
                $scope.defaultValue = null;
            }
            else {
                $scope.defaultValue = item;
            }
        };

        $scope.removeItem = function (item, index) {
            $scope.currentItems.splice(index, 1);

            if ($scope.defaultValue == item) {
                $scope.defaultValue = null;
            }
        };

        $scope.addItem = function () {
            $scope.currentItems.push('');
        };

        $scope.sortableOptions = {
            hint: function (element) {
                return $('<div class="sf-backend-wrp"><div class="list-group-item list-group-item-multiselect list-group-item-draggable list-group-item-hint">' +
                            element.html() +
                        '</div></div>');
            },
            placeholder: function (element) {
                return $('<div class="list-group-item list-group-item-placeholder"></div>');
            },
            handler: ".handler",
            axis: "y"
        };
    }]);
})();