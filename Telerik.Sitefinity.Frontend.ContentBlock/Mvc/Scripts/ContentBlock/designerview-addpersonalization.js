(function ($) {
    var personalizationDialogModule = angular.module('personalizationDialog', ['designer', 'personalizationServices']);
    angular.module('designer').requires.push('personalizationDialog');

    personalizationDialogModule.controller('AddPersonalizationCtrl', ['$scope', 'personalizationService',
        function ($scope, personalizationService) {
            var onError = function (data) {
                $scope.feedback.showError = true;
                if (data)
                    $scope.feedback.errorMessage = data.Detail;
            };

            var setSegmentName = function () {
                var selectedSegments = $telerik.$.grep($scope.segments, function (value, index) {
                    return value.Id === $scope.model.segmentId;
                });

                if (selectedSegments && selectedSegments.length === 1) {
                    $scope.model.segmentName = selectedSegments[0].Name;
                }
            };

            $scope.model = {
                controlId: "",
                segmentId: ""
            };

            $scope.segments = [];
            personalizationService.getSegments($scope.model).then(function (data) {
                $scope.segments = data;
            });

            $scope.addPersonalization = function () {
                $scope.feedback.showLoadingIndicator = true;
                setSegmentName();

                personalizationService.personalize($scope.model)
                    .then(function (args) {
                        $scope.close();
                        $telerik.$(document).trigger('personalizationDialogClosed', args);
                    }, onError)
                    .finally(function () {
                        $scope.feedback.showLoadingIndicator = false;
                    });
            };
        }
    ]);
}) (jQuery);