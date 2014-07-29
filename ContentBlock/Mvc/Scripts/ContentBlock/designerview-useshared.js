(function ($) {
    var contentSelectorModule = angular.module('contentSelector', ['sharedContentServices', 'dataProviders']);
    angular.module('designer').requires.push('contentSelector');

    contentSelectorModule.controller('UseSharedCtrl', ['$scope', 'sharedContentService', 'propertyService', 'widgetContext', 'providerService',
        function ($scope, sharedContentService, propertyService, widgetContext, providerService) {
            // ------------------------------------------------------------------------
            // Event handlers
            // ------------------------------------------------------------------------

            var onGetPropertiesSuccess = function (data) {
                if (data) {
                    $scope.properties = propertyService.toAssociativeArray(data.Items);
                    $scope.filter.providerName = $scope.properties.ProviderName.PropertyValue;

                    providerService.setDefaultProviderName($scope.filter.providerName);
                }
            };

            //invoked when the content blocks for a provider are loaded
            var onLoadedSuccess = function (data) {
                if (data && data.Items) {
                    $scope.contentItems = data.Items;
                    $scope.filter.paging.set_totalItems(data.TotalCount);

                    //select current cotnentBlock if it exists
                    for (var i = 0; i < data.Items.length; i++) {
                        if (data.Items[i].Id == $scope.properties.SharedContentID.PropertyValue) {
                            $scope.selectedContentItem = data.Items[i];
                        }
                    }
                }

                $scope.isListEmpty = $scope.contentItems.length === 0 && !$scope.filter.search;
            };

            var onError = function () {
                var errorMessage = '';
                if (data)
                    errorMessage = data.Detail;

                $scope.feedback.showError = true;
                $scope.feedback.errorMessage = errorMessage;
            };

            // ------------------------------------------------------------------------
            // helper methods
            // ------------------------------------------------------------------------

            var saveProperties = function (data) {
                $scope.properties.SharedContentID.PropertyValue = data.Item.Id;
                $scope.properties.ProviderName.PropertyValue = $scope.selectedContentItem.ProviderName;
                $scope.properties.Content.PropertyValue = data.Item.Content.Value;

                var modifiedProperties = [$scope.properties.SharedContentID, $scope.properties.ProviderName, $scope.properties.Content];
                var currentSaveMode = widgetContext.culture ? 1 : 0;
                return propertyService.save(currentSaveMode, modifiedProperties);
            };

            var loadContentItems = function () {
                var skip = $scope.filter.paging.get_itemsToSkip();
                var take = $scope.filter.paging.itemsPerPage;

                return sharedContentService.getItems($scope.filter.providerName, skip, take, $scope.filter.search)
                    .then(onLoadedSuccess, onError);
            };

            var reloadContentItems = function (newValue, oldValue) {
                if (newValue != oldValue) {
                    loadContentItems();
                }
            };

            var hideLoadingIndicator = function () {
                $scope.feedback.showLoadingIndicator = false;
            };

            // ------------------------------------------------------------------------
            // Scope variables and setup
            // ------------------------------------------------------------------------

            $scope.feedback.showError = false;
            $scope.isListEmpty = false;
            $scope.contentItems = [];
            $scope.filter = {
                providerName: null,
                search: null,
                paging: {
                    totalItems: 0,
                    currentPage: 1,
                    itemsPerPage: 50,
                    get_itemsToSkip: function () {
                        return (this.currentPage - 1) * this.itemsPerPage;
                    },
                    set_totalItems: function (itemsCount) {
                        this.totalItems = itemsCount;
                        this.isVisible = this.totalItems > this.itemsPerPage;
                    },
                    isVisible: false
                }
            };

            $scope.contentItemClicked = function (index, item) {
                $scope.selectedContentItem = item;
            };

            $scope.selectSharedContent = function () {
                if ($scope.selectedContentItem) {
                    var selectedContentItemId = $scope.selectedContentItem.Id;
                    var providerName = $scope.selectedContentItem.ProviderName;
                    var checkout = false;

                    $scope.feedback.showLoadingIndicator = true;
                    sharedContentService.get(selectedContentItemId, providerName, checkout)
                        .then(saveProperties)
                        .then($scope.close)
                        .catch(onError)
                        .finally(hideLoadingIndicator);
                }
                else {
                    $scope.close();
                }
            };

            $scope.hideError = function () {
                $scope.feedback.showError = false;
                $scope.feedback.errorMessage = null;
            };

            $scope.feedback.showLoadingIndicator = true;
            propertyService.get()
                .then(onGetPropertiesSuccess)
                .then(loadContentItems)
                .then(function () {
                    $scope.$watch('filter.search', reloadContentItems);
                    $scope.$watch('filter.providerName', reloadContentItems);
                    $scope.$watch('filter.paging.currentPage', reloadContentItems);
                })
                .catch(onError)
                .finally(hideLoadingIndicator);
        }
    ]);

})(jQuery);