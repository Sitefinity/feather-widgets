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
                                groupItem = scope.sfQueryData.addGroup(groupName, 'OR');

                            //event has already started
                            scope.sfQueryData.addChildToGroup(groupItem, null, 'AND', 'EventStart', 'System.DateTime', '<=', 'DateTime.UtcNow');
                            
                            //event end is after now
                            scope.sfQueryData.addChildToGroup(groupItem, null, 'AND', 'EventEnd', 'System.DateTime', '>=', 'DateTime.UtcNow');                            
                        };

                        var populateSelectedDateFilters = function () {
                            //set AnyTime value as default for upcomming and past events
                            if (scope.sfQueryData && scope.sfQueryData.QueryItems && scope.sfQueryData.QueryItems.length === 0) {
                                scope.upcomingChanged();
                                scope.pastChanged();
                            }
                            if (scope.sfQueryData.QueryItems) {
                                scope.sfQueryData.QueryItems.forEach(function (queryItem) {
                                    {
                                        if (queryItem.IsGroup && queryItem.Name == 'Current')
                                            scope.isCurrentSelected = true;
                                    }
                                });
                            }
                        };

                        scope.upcomingChanged = function () {
                            var upcomingQueryGroup = scope.sfQueryData.getItemByName('Upcoming');
                            if (!upcomingQueryGroup)
                                upcomingQueryGroup = scope.sfQueryData.addGroup('Upcoming', 'OR');

                            scope.sfQueryData.addChildToGroup(upcomingQueryGroup, null, 'AND', 'EventStart', 'System.DateTime', '>=', 'DateTime.UtcNow');
                        };

                        scope.pastChanged = function () {
                            var pastQueryGroup = scope.sfQueryData.getItemByName('Past');
                            if (!pastQueryGroup)
                                pastQueryGroup = scope.sfQueryData.addGroup('Past', 'OR');

                            scope.sfQueryData.addChildToGroup(pastQueryGroup, null, 'AND', 'EventEnd', 'System.DateTime', '<=', 'DateTime.UtcNow');
                        };

                        scope.upcomingClicked = function () {
                            if (element.find('*[sf-group-name="Upcoming"] input[type="checkbox"]')[0].checked) {
                                scope.upcomingChanged();
                            }
                        };

                        scope.pastClicked = function () {
                            if (element.find('*[sf-group-name="Past"] input[type="checkbox"]')[0].checked) {
                                scope.pastChanged();
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

                            scope.isCurrentSelected = !scope.isCurrentSelected;
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