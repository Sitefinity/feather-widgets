(function ($) {
    angular.module('designer').requires.push('expander', 'sfSelectors');

    angular.module('designer').controller('SimpleCtrl', ['$scope', 'propertyService', 'sfSearchService', function ($scope, propertyService, searchService) {
        $scope.feedback.showLoadingIndicator = true;
        $scope.hasSearchIndexes = true;
        $scope.hasScoringSettings = false;
        $scope.loadingScoringProfiles = false;
        $scope.previousScoringProfile = null;
        $scope.disableScoringProfilesDropdown = false;

        searchService.getSearchIndexes()
            .then(function (data) {
                if (data && data.Items) {
                    $scope.searchIndexes = data.Items;
                    $scope.hasSearchIndexes = $scope.searchIndexes.length > 0;

                    // clears the selected search index and its scoring settings if the index is deleted
                    if ($scope.properties && $scope.properties.SearchIndexPipeId) {
                        var indexIds = data.Items.map(function (e) { return e.ID; });
                        if (indexIds.indexOf($scope.properties.SearchIndexPipeId.PropertyValue) < 0) {
                            {
                                $scope.properties.SearchIndexPipeId.PropertyValue = null;
                                $scope.properties.ScoringProfile.PropertyValue = null;
                                $scope.properties.ScoringParameters.PropertyValue = null;
                            }
                        }
                    }
                }
            }, function (errorData) {
                $scope.feedback.showError = true;

                if (errorData && errorData.data)
                    $scope.feedback.errorMessage = errorData.data.Detail;
            })
            .finally(function () {
            });

        propertyService.get()
            .then(function (data) {
                if (data && data.Items) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);
                    $scope.hasScoringSettings = $scope.properties.HasScoringSettings.PropertyValue === "True";
                    setDefaultValuesForEmptyProperties();

                    bindScoringProfiles($scope.properties.SearchIndexPipeId.PropertyValue, true);
                }
            },
                function (errorData) {
                    $scope.feedback.showError = true;
                    if (errorData && errorData.data)
                        $scope.feedback.errorMessage = errorData.data.Detail;
                })
            .finally(function () {
                $scope.feedback.showLoadingIndicator = false;
            });

        $scope.searchIndexChanged = function (e) {
            bindScoringProfiles(e, false);
        };

        $scope.scoringProfileChanged = function (e) {
            // Clear the scoring parameters when the scoring profile is changed
            $scope.properties.ScoringParameters.PropertyValue = "";
        };

        function setDefaultValuesForEmptyProperties() {
            // specifics because of the angular js upgrade. In order for the default option values to be preselected we need to set the selected value to null instead of ''
            if ($scope.properties.SearchIndexPipeId.PropertyValue === '') {
                $scope.properties.SearchIndexPipeId.PropertyValue = null;
            }

            if ($scope.properties.ScoringProfile.PropertyValue === '') {
                $scope.properties.ScoringProfile.PropertyValue = null;
            }
        }

        function bindScoringProfiles(indexId, initialLoad) {
            $scope.disableScoringProfilesDropdown = false;
            $scope.previousScoringProfile = null;

            if (!$scope.hasScoringSettings) {
                $scope.properties.ScoringProfile.PropertyValue = null;
                $scope.properties.ScoringParameters.PropertyValue = null;
                return;
            }

            if (!indexId) {
                // if the index ID is falsy there is no need to load the scoring profiles
                $scope.scoringProfilesNames = [];
                return;
            }

            $scope.loadingScoringProfiles = true;
            searchService.getScoringSettings(indexId)
                .then(function (data) {
                    $scope.loadingScoringProfiles = false;

                    if (data.ScoringSettingsNames) {
                        var scoringProfileNames = data.ScoringSettingsNames;

                        validateScoringSettings(scoringProfileNames, initialLoad);

                        $scope.scoringProfilesNames = scoringProfileNames;
                    }

                }, function (error) {
                    $scope.loadingScoringProfiles = false;
                    $scope.previousScoringProfile = null;
                    $scope.scoringProfilesNames = [];
                })
                .finally(function () {
                    $scope.loadingScoringProfiles = false;
                });
        }

        function validateScoringSettings(scoringProfileNames, initialLoad) {
            if (initialLoad) {
                var currentScorngProfileName = $scope.properties.ScoringProfile.PropertyValue;
                if (currentScorngProfileName && scoringProfileNames.indexOf(currentScorngProfileName) < 0) {
                    $scope.previousScoringProfile = currentScorngProfileName;
                    $scope.properties.ScoringProfile.PropertyValue = null;

                    $scope.disableScoringProfilesDropdown = scoringProfileNames.length === 0;
                }
            }
        }
    }
    ]);
})(jQuery);