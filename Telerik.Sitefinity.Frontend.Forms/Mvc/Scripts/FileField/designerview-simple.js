(function () {
    var simpleViewModule = angular.module('simpleViewModule', ['designer']);

    angular.module('designer').requires.push('expander', 'simpleViewModule');

    simpleViewModule.controller('SimpleCtrl', ['$scope', 'propertyService', function ($scope, propertyService) {
        var onGetPropertiesSuccess = function (data) {
            if (data) {
                $scope.properties = propertyService.toHierarchyArray(data.Items);

                if ($scope.properties.Model.MaxFileSizeInMb.PropertyValue === '0')
                    $scope.properties.Model.MaxFileSizeInMb.PropertyValue = '';

                if ($scope.properties.Model.AllowedFileTypes && $scope.properties.Model.AllowedFileTypes.PropertyValue) {
                    $scope.state.selectedFileTypeCategories = $scope.properties.Model.AllowedFileTypes.PropertyValue.split(',');
                    var idx = $scope.state.selectedFileTypeCategories.indexOf('All');
                    if (idx > -1)
                        $scope.state.selectedFileTypeCategories.splice(idx, 1);

                    if ($scope.state.selectedFileTypeCategories.length > 0)
                        $scope.state.fileTypeRadioSelection = 'Selected';
                    else
                        $scope.state.fileTypeRadioSelection = 'All';
                }

                if ($scope.properties.Model.OtherFileTypes && $scope.properties.Model.OtherFileTypes.PropertyValue) {
                    $scope.state.commaSeparatedFileTypes = $scope.properties.Model.OtherFileTypes.PropertyValue.split(';').join(',');
                }
            }
        };

        $scope.state = {
            fileTypeRadioSelection: 'All',
            selectedFileTypeCategories: [],
            commaSeparatedFileTypes: ''
        };

        $scope.state.fileTypeRadioSelection = 'All';
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
                if (newValue === 'All') {
                    $scope.state.selectedFileTypeCategories = [];
                    $scope.properties.Model.AllowedFileTypes.PropertyValue = 'All';
                }
            },
            true
        );

        $scope.$watch(
            'state.commaSeparatedFileTypes',
            function (newValue, oldValue) {
                if (newValue)
                    $scope.properties.Model.OtherFileTypes.PropertyValue = newValue.split(',').join(';');
            }
        );

        $scope.toggleSelection = function toggleSelection(typeCategory) {
            var idx = $scope.state.selectedFileTypeCategories.indexOf(typeCategory);
            if (idx > -1)
                $scope.state.selectedFileTypeCategories.splice(idx, 1);
            else
                $scope.state.selectedFileTypeCategories.push(typeCategory);

            if ($scope.properties.Model.AllowedFileTypes)
                $scope.properties.Model.AllowedFileTypes.PropertyValue = $scope.state.selectedFileTypeCategories.join(',');
        };

        $scope.feedback.showLoadingIndicator = true;
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