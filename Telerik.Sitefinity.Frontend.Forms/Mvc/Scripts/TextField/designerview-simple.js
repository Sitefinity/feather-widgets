(function () {

    var simpleViewModule = angular.module('simpleViewModule', ['designer']);

    angular.module('designer').requires.push('expander', 'simpleViewModule');

    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', '$q', function ($scope, propertyService, $q) {

        // ------------------------------------------------------------------------
        // scope variables and set up
        // ------------------------------------------------------------------------

        $scope.$watch(
            'properties.Model.InputType.PropertyValue',
            function (newInputType, oldInputType) {
                if (newInputType !== oldInputType) {
                    var inputTypeRegexPatterns = JSON.parse($scope.properties.Model.SerializedInputTypeRegexPatterns.PropertyValue);
                    $scope.properties.Model.ValidatorDefinition.RegularExpression.PropertyValue = inputTypeRegexPatterns[newInputType];

                    $scope.fieldInputType = getInputType(newInputType);
                    $scope.properties.Model.MetaField.DefaultValue.PropertyValue = null;
                }
            },
            true
        );

        $scope.$watch(
            'defaultValue.value',
            function (newDefaultValue, oldDefaultValue) {
                if (newDefaultValue !== oldDefaultValue && $scope.properties) {
                    $scope.properties.Model.MetaField.DefaultValue.PropertyValue = angular.element("#predefinedValue").val();
                }
            },
            true
        );

        $scope.feedback.showLoadingIndicator = true;
        $scope.defaultValue = { value: "" };

        var getInputType = function (textType) {
            if (textType == 'DateTimeLocal')
                return 'datetime-local';
            if (textType == 'Hidden')
                return 'text';
            else
                return textType.toLowerCase();
        };

        var onGetPropertiesSuccess = function (data) {
            if (data) {
                $scope.properties = propertyService.toHierarchyArray(data.Items);

                $scope.fieldInputType = getInputType($scope.properties.Model.InputType.PropertyValue);

                // The changes bellow are needed so that the textbox is cleared when the predefined value of a textbox is removed
                // If changing, please test for all input types (text, date, password, url, etc.)
                var inputType = $scope.properties.Model.InputType.PropertyValue.toLowerCase();
                var defaultValue = inputType === "text" || inputType === "url" || inputType === "password" ? null : "";
                $scope.defaultValue = { value: defaultValue };

                $scope.feedback.savingHandlers.push(function () {
                    var deferred = $q.defer();
                    var maxDbLength = parseInt($scope.properties.Model.MetaField.DBLength.PropertyValue);

                    if (maxDbLength && parseInt($scope.properties.Model.ValidatorDefinition.MaxLength.PropertyValue) > maxDbLength) {
                        deferred.reject();
                        $scope.feedback.errorMessage = "Range max value must be less than " + maxDbLength + " characters";
                    }
                    else {
                        deferred.resolve();
                    }

                    return deferred.promise;
                });
            }
        };

        propertyService.get()
            .then(onGetPropertiesSuccess)
            .catch(function (data) {
                $scope.feedback.showError = true;
                if (data)
                    $scope.feedback.errorMessage = data.Detail;
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})();