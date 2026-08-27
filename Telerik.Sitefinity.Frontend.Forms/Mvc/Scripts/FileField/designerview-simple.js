(function () {
    var simpleViewModule = angular.module('simpleViewModule', ['designer']);

    angular.module('designer').requires.push('expander', 'simpleViewModule');

    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        var onGetPropertiesSuccess = function (data) {
            if (data && data.Items) {
                $scope.properties = propertyService.toHierarchyArray(data.Items);

                if ($scope.properties.Model.MaxFileSizeInMb.PropertyValue === '0')
                    $scope.properties.Model.MaxFileSizeInMb.PropertyValue = '';

                if ($scope.properties.Model.AllowedFileTypes && $scope.properties.Model.AllowedFileTypes.PropertyValue) {
                    var allowedFileTypesValue = $scope.properties.Model.AllowedFileTypes.PropertyValue;

                    if (allowedFileTypesValue.indexOf('All') >= 0) {
                        $scope.state.fileTypeRadioSelection = 'All';
                        $scope.state.selectedFileTypeCategories = [];
                    } else if (allowedFileTypesValue === 'None') {
                        $scope.state.fileTypeRadioSelection = 'None';
                        $scope.state.selectedFileTypeCategories = [];
                    } else {
                        var categories = allowedFileTypesValue.split(',');

                        $scope.state.selectedFileTypeCategories = [];
                        for (var i = 0; i < categories.length; i++) {
                            var category = categories[i].trim();
                            if (category !== 'All' && category !== 'None' && category !== '') {
                                $scope.state.selectedFileTypeCategories.push(category);
                            }
                        }

                        if ($scope.state.selectedFileTypeCategories.length > 0)
                            $scope.state.fileTypeRadioSelection = 'Selected';
                        else
                            $scope.state.fileTypeRadioSelection = 'None';
                    }
                } else {
                    $scope.state.fileTypeRadioSelection = 'None';
                    $scope.state.selectedFileTypeCategories = [];
                }

                if ($scope.properties.Model.OtherFileTypes && $scope.properties.Model.OtherFileTypes.PropertyValue) {
                    $scope.state.commaSeparatedFileTypes = $scope.properties.Model.OtherFileTypes.PropertyValue.split(';').join(',');
                }
            }
        };

        $scope.state = {
            fileTypeRadioSelection: 'None',
            selectedFileTypeCategories: [],
            commaSeparatedFileTypes: ''
        };

        $scope.fileTypeCategories = [
            {
                value: 'Images',
                title: 'Images',
                description: '(jpg, jpeg, png, gif, bmp)'
            },
            {
                value: 'Documents',
                title: 'Documents',
                description: '(pdf, doc, docx, ppt, pptx, ppsx, xls, xlsx)'
            },
            {
                value: 'Audio',
                title: 'Audio',
                description: '(mp3, ogg, wav, wma)'
            },
            {
                value: 'Video',
                title: 'Video',
                description: '(avi, mpg, mpeg, mov, mp4, wmv)'
            },
            {
                value: 'Other',
                title: 'Other...',
                description: null
            }
        ];

        $scope.$watch(
            'state.fileTypeRadioSelection',
            function (newValue, oldValue) {
                if (!$scope.properties) return;

                if (newValue === 'All') {
                    $scope.state.selectedFileTypeCategories = [];
                    $scope.properties.Model.AllowedFileTypes.PropertyValue = 'All';
                } else if (newValue === 'None') {
                    $scope.state.selectedFileTypeCategories = [];
                    $scope.properties.Model.AllowedFileTypes.PropertyValue = 'None';
                } else if (newValue === 'Selected') {
                    if ($scope.state.selectedFileTypeCategories.length === 0) {
                        $scope.properties.Model.AllowedFileTypes.PropertyValue = 'None';
                    } else {
                        $scope.properties.Model.AllowedFileTypes.PropertyValue = $scope.state.selectedFileTypeCategories.join(',');
                    }
                }
            },
            true
        );

        $scope.$watch(
            'state.commaSeparatedFileTypes',
            function (newValue, oldValue) {
                if (newValue && $scope.properties)
                    $scope.properties.Model.OtherFileTypes.PropertyValue = newValue.split(',').join(';');
            }
        );

        $scope.toggleSelection = function toggleSelection(typeCategory) {
            if ($scope.state.fileTypeRadioSelection !== 'Selected') {
                return;
            }

            var idx = $scope.state.selectedFileTypeCategories.indexOf(typeCategory);
            if (idx > -1)
                $scope.state.selectedFileTypeCategories.splice(idx, 1);
            else
                $scope.state.selectedFileTypeCategories.push(typeCategory);

            if ($scope.properties && $scope.properties.Model.AllowedFileTypes) {
                if ($scope.state.selectedFileTypeCategories.length === 0) {
                    $scope.properties.Model.AllowedFileTypes.PropertyValue = 'None';
                } else {
                    $scope.properties.Model.AllowedFileTypes.PropertyValue = $scope.state.selectedFileTypeCategories.join(',');
                }
            }
        };

        $scope.feedback.showLoadingIndicator = true;
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