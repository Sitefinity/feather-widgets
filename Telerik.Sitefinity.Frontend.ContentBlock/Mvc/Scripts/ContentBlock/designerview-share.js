﻿(function ($) {
    var shareDialogModule = angular.module('shareDialog', ['designer', 'sharedContentServices']);
    angular.module('designer').requires.push('shareDialog');

    shareDialogModule.controller('ShareCtrl', ['$scope', 'sharedContentService',
        function ($scope, sharedContentService) {
            var onError = function (errorData) {
                $scope.feedback.showError = true;
                if (errorData && errorData.data)
                    $scope.feedback.errorMessage = errorData.data.Detail;
            };

            $scope.model = {
                title: ''
            };

            $scope.isTitleValid = true;

            $scope.shareContent = function () {
                //validate title and send request to share the content block
                if ($.trim($scope.model.title)) {
                    $scope.isTitleValid = true;
                    $scope.feedback.showLoadingIndicator = true;
                    sharedContentService.share($scope.model.title)
                        .then($scope.close, onError)
                        .finally(function () {
                            $scope.feedback.showLoadingIndicator = false;
                        });
                }
                else {
                    $scope.isTitleValid = false;
                }
            };
        }
    ]);
}) (jQuery);