(function () {
    var simpleViewModule = angular.module('simpleViewModule', ['designer', 'sfSelectors']);
    angular.module('designer').requires.push('expander', 'simpleViewModule');
    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', '$q', function ($scope, propertyService, $q) {
        $scope.feedback.showLoadingIndicator = true;
        $scope.currentItems = [];
        $scope.lastUniqueIndex = 0;
        $scope.defaultValue = null;

        propertyService.get()
            .then(function (data) {
                if (data && data.Items) {
                    $scope.properties = propertyService.toHierarchyArray(data.Items);
                    $scope.currentItems = JSON.parse($scope.properties.Model.SerializedChoices.PropertyValue).map(function (item) { return { key: $scope.lastUniqueIndex++, value: item }; });
                    $scope.defaultValue = $scope.properties.Model.MetaField.DefaultValue.PropertyValue;
                }
            })
            .catch(function (errorData) {
                $scope.feedback.showError = true;
                if (errorData && errorData.data) {
                    $scope.feedback.errorMessage = errorData.data.Detail;
                }
            })
            .then(function () {
                $scope.feedback.savingHandlers.push(function () {
                    var deferred = $q.defer();

                    if (!arrayContainsEmptyValue($scope.currentItems)) {
                        $scope.properties.Model.SerializedChoices.PropertyValue = JSON.stringify($scope.currentItems.map(function (e) { return e.value; }));
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

            $scope.changeRequired();
        };

        $scope.getTrackByValue = function (index) {
            return $scope.currentItems[index].key;
        };

        $scope.itemClicked = function (ev) {
            ev.target.focus();
        };

        $scope.setDefault = function (item) {
            $scope.defaultValue = item;
        };

        $scope.removeItem = function (item, index) {
            $scope.currentItems.splice(index, 1);

            if ($scope.defaultValue == item) {
                $scope.defaultValue = null;
            }
        };

        $scope.addItem = function () {
            $scope.lastUniqueIndex += 1;
            $scope.currentItems.push({key: $scope.lastUniqueIndex, value: null});
        };

        $scope.changeRequired = function () {
            if ($scope.properties.Model.ValidatorDefinition.Required.PropertyValue === 'True' && $scope.currentItems.length) {
                $scope.setDefault($scope.currentItems[0].value);
            }
        };
        
        $scope.sortableOptions = {
            hint: function (element) {
                return $('<div class="sf-backend-wrp"><div class="list-group-item list-group-item-multiselect list-group-item-draggable-2 list-group-item-hint list-group-item-draggable-2--noCheckbox">' +
                            element.html() +
                        '</div></div>');
            },
            placeholder: function (element) {
                return $('<div class="list-group-item list-group-item-placeholder list-group-item-placeholder-2"></div>');
            },
            handler: ".handler",
            axis: "y",
            autoScroll: true
        };

        function arrayContainsEmptyValue(choices) {
            for (var i = 0; i < choices.length; i++) {
                if (choices[i].value === null || choices[i].value === '') {
                    return true;
                }
            }

            return false;
        }
    }]);
})();