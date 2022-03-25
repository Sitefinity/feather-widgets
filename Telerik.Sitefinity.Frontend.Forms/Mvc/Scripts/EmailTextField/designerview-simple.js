﻿(function () {

    var simpleViewModule = angular.module('simpleViewModule', ['designer']);

    angular.module('designer').requires.push('expander', 'simpleViewModule');

    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {

        // ------------------------------------------------------------------------
        // scope variables and set up
        // ------------------------------------------------------------------------

        $scope.$watch(
           'properties.Model.InputType.PropertyValue',
           function (newInputType, oldInputType) {
               if ($scope.properties && newInputType !== oldInputType) {
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
                if ($scope.properties && newDefaultValue !== oldDefaultValue && $scope.properties) {
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
            if (data && data.Items) {
                $scope.properties = propertyService.toHierarchyArray(data.Items);

                if ($scope.properties.Model.ValidatorDefinition.MaxLength.PropertyValue === '0')
                    $scope.properties.Model.ValidatorDefinition.MaxLength.PropertyValue = '';

                $scope.fieldInputType = getInputType($scope.properties.Model.InputType.PropertyValue);
            }
        };

        propertyService.get()
            .then(onGetPropertiesSuccess)
            .catch(function (errorData) {
                $scope.feedback.showError = true;
                if (errorData && errorData.data)
                    $scope.feedback.errorMessage = errorData.data.Detail;
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})();