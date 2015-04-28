(function ($) {
    angular.module('designer').requires.push('expander');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {

        $scope.mediaTypeColumns = [
            [{ checked: false, value: 'braille' }, { checked: false, value: 'embossed' }, { checked: false, value: 'handheld' }],
            [{ checked: false, value: 'print' }, { checked: false, value: 'projection' }, { checked: false, value: 'screen' }],
            [{ checked: false, value: 'speech' }, { checked: false, value: 'tty' }, { checked: false, value: 'tv' }]
        ];

        var updateMediaTypeModel = function () {
            var mediaType = $scope.properties.MediaType.PropertyValue;

            if (mediaType === 'all') {
                $scope.mediaTypeSelection = 'all';
            }
            else {
                $scope.mediaTypeSelection = 'selected';

                var mediaTypes = mediaType.split(',');

                var i;
                for (i = 0; i < mediaTypes.length; i++)
                    mediaTypes[i] = mediaTypes[i].trim().toLowerCase();

                for (i = 0; i < $scope.mediaTypeColumns.length; i++) {
                    for (var j = 0; j < $scope.mediaTypeColumns[i].length; j++) {
                        $scope.mediaTypeColumns[i][j].checked = mediaTypes.indexOf($scope.mediaTypeColumns[i][j].value) > -1;
                    }
                }
            }
        };

        $scope.updateMediaTypeProeprty = function () {
            if ($scope.mediaTypeSelection === 'all') {
                $scope.properties.MediaType.PropertyValue = 'all';
            }
            else {
                var mediaTypes = [];

                for (var i = 0; i < $scope.mediaTypeColumns.length; i++) {
                    for (var j = 0; j < $scope.mediaTypeColumns[i].length; j++) {
                        if ($scope.mediaTypeColumns[i][j].checked) {
                            mediaTypes.push($scope.mediaTypeColumns[i][j].value);
                        }
                    }
                }

                $scope.properties.MediaType.PropertyValue = mediaTypes.join(', ');
            }
        };

        propertyService.get()
            .then(function (data) {
                if (data) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);
                    updateMediaTypeModel();
                }
            },
            function (data) {
                $scope.feedback.showError = true;
                if (data)
                    $scope.feedback.errorMessage = data.Detail;
            })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });
    }]);
})(jQuery);
