(function ($) {
    angular.module('sfSelectors')
        .directive('sfEventsDateFilterSelector', function () {
            return {
                restrict: 'EA',
                scope: {
                    sfQueryData: '='
                },
                templateUrl: function (elem, attrs) {
                    var assembly = attrs.sfTemplateAssembly || 'Telerik.Sitefinity.Frontend.Events';
                    var url = attrs.sfTemplateUrl || 'client-components/sf-events-date-filter-selector.sf-cshtml';
                    return sitefinity.getEmbeddedResourceUrl(assembly, url);
                },
                link: {
                    pre: function (scope, element, attrs, ctrl) {

                        var addCurrentDateQueryItem = function (groupName) {
                            var groupItem = scope.sfQueryData.getItemByName(groupName);

                            if (!groupItem)
                                groupItem = scope.sfQueryData.addGroup(groupName, scope.sfGroupLogicalOperator);

                            //event has already started
                            scope.sfQueryData.addChildToGroup(groupItem, null, 'AND', 'EventStart', 'System.DateTime', '<=', 'DateTime.UtcNow');
                            
                            //eventend is after now
                            scope.sfQueryData.addChildToGroup(groupItem, null, 'AND', 'EventEnd', 'System.DateTime', '>=', 'DateTime.UtcNow');                            
                        };

                        var populateSelectedDateFilters = function () {
                            if (scope.sfQueryData.QueryItems) {
                                scope.sfQueryData.QueryItems.forEach(function (queryItem) {
                                    {
                                        if (queryItem.IsGroup && queryItem.Name == 'Current')
                                            scope.isCurrentSelected = true;
                                    }
                                });
                            }
                        };

                        scope.toggleEventFilterSelection = function (filterName) {
                            // is currently selected
                            if (scope.isCurrentSelected) {
                                var groupToRemove = scope.sfQueryData.getItemByName(filterName);

                                if (groupToRemove)
                                    scope.sfQueryData.removeGroup(groupToRemove);
                            }

                                // is newly selected
                            else {
                                addCurrentDateQueryItem(filterName);
                            }

                            scope.isCurrentSelected == !scope.isCurrentSelected;
                        };


                        if (scope.sfQueryData && scope.sfQueryData.QueryItems)
                            scope.sfQueryData = new Telerik.Sitefinity.Web.UI.QueryData(scope.sfQueryData);
                        else
                            scope.sfQueryData = new Telerik.Sitefinity.Web.UI.QueryData();

                        scope.today = Date.now();
                        populateSelectedDateFilters();
                    }
                }
            };
        });
})(jQuery);