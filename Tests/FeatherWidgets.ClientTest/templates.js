angular.module('templates', []).run(['$templateCache', function($templateCache) {
  $templateCache.put("../Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/common/sf-bubbles-selection.html",
    "<div ng-repeat=\"item in sfSelectedItems | limitTo:5\" class=\"label label-taxon label-full\">\n" +
    "    <span data-index=\"{{$index}}\"\n" +
    "          sf-shrinked-breadcrumb=\"{{hierarchical ? item.TitlesPath : bindIdentifierField(item)}}\" sf-max-length=\"45\">\n" +
    "    </span>\n" +
    "</div>\n" +
    "\n" +
    "<a class=\"small\" ng-click=\"open()\" ng-show=\"sfSelectedItems.length > 5\">and {{sfSelectedItems.length - 5}} more</a>\n" +
    "\n" +
    "<button class=\"btn btn-xs btn-default openSelectorBtn\" ng-click=\"open()\">\n" +
    "    <span ng-hide=\"isItemSelected()\">Select</span>\n" +
    "    <span ng-show=\"isItemSelected()\">Change</span>\n" +
    "</button>\n" +
    "");
  $templateCache.put("../Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/common/sf-filter-selector.html",
    "<sf-taxon-filter sf-query-data=\"sfQueryData\" \n" +
    "    sf-taxonomy-fields=\"sfTaxonomyFields\"\n" +
    "    sf-provider=\"sfProvider\"\n" +
    "    sf-group-logical-operator=\"AND\"\n" +
    "    sf-item-logical-operator=\"OR\"></sf-taxon-filter>\n" +
    "\n" +
    " <sf-date-filter sf-query-data=\"sfQueryData\"\n" +
    "     sf-group-logical-operator=\"AND\"\n" +
    "     sf-item-logical-operator=\"AND\"\n" +
    "     sf-query-field-name=\"PublicationDate\"></sf-date-filter>");
  $templateCache.put("../Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/common/sf-items-tree.html",
    "<div kendo-tree-view=\"treeView\"\n" +
    "         k-data-source=\"itemsDataSource\"\n" +
    "         k-template=\"itemTemplate\"\n" +
    "         k-checkboxes=\"sfMultiselect ? checkboxes : false\"\n" +
    "         k-load-on-demand=\"true\"\n" +
    "         class=\"k-treeview--selection\"/>");
  $templateCache.put("../Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/common/sf-list-group-selection.html",
    "<div class=\"list-group list-group-sm m-bottom-sm\" ng-show=\"sfSelectedItems.length > 0\">\n" +
    "    <div ng-repeat=\"item in sfSelectedItems | limitTo:5\"\n" +
    "         data-index=\"{{$index}}\"\n" +
    "         class=\"list-group-item\">\n" +
    "         <span sf-shrinked-breadcrumb=\"{{hierarchical ? item.TitlesPath : bindIdentifierField(item)}}\" sf-max-length=\"70\"></span>\n" +
    "    </div>\n" +
    "\n" +
    "    <a class=\"u-dib m-top-xs small\" ng-click=\"open()\" ng-show=\"sfSelectedItems.length > 5\">and {{sfSelectedItems.length - 5}} more</a>\n" +
    "</div>\n" +
    "\n" +
    "<button class=\"btn btn-xs btn-default openSelectorBtn\" ng-click=\"open()\">\n" +
    "    <span ng-hide=\"isItemSelected()\">Select</span>\n" +
    "    <span ng-show=\"isItemSelected()\">Change</span>\n" +
    "</button>\n" +
    "");
  $templateCache.put("../Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/common/sf-list-selector.html",
    "<div id=\"selectedItemsPlaceholder\">\n" +
    "    <alert type=\"danger\" ng-show=\"showError\">{{errorMessage}}</alert>\n" +
    "\n" +
    "    <div ng-hide=\"showError\">\n" +
    "        <div ng-include=\"getClosedDialogTemplate()\"></div>\n" +
    "    </div>\n" +
    "</div>\n" +
    "<div class=\"contentSelector\" modal template-url=\"{{dialogTemplateId}}\" window-class=\"sf-designer-dlg selector-dlg\" existing-scope=\"true\">\n" +
    "    <div ng-include=\"getDialogTemplate()\">\n" +
    "\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
  $templateCache.put("../Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/common/sf-provider-selector.html",
    "<div style=\"margin-top: 0\" class=\"dropdown s-bg-source-wrp\" ng-show=\"isProviderSelectorVisible\" is-open=\"isOpen\">\n" +
    "    <a class=\"btn btn-default dropdown-toggle\" >\n" +
    "        {{selectedProvider.Title}} <span class=\"caret\"></span>\n" +
    "    </a>\n" +
    "    <ul class=\"dropdown-menu\">\n" +
    "        <li>\n" +
    "            <a href=\"\" style=\"color: #ccc; background: #fff; cursor: default;\">{{providerLabel}}</a>\n" +
    "        </li>\n" +
    "        <li ng-repeat=\"provider in providers\">\n" +
    "            <a href=\"\" ng-click=\"selectProvider(provider);\">{{provider.Title}}</a>\n" +
    "        </li>\n" +
    "    </ul>\n" +
    "</div>");
  $templateCache.put("../Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/common/sf-selected-items-view.html",
    "<div ng-show=\"isListEmpty()\" class=\"alert alert-info\">No items have been selected yet.</div>\n" +
    "\n" +
    "<alert type=\"danger\" ng-show=\"showError\">{{errorMessage}}</alert>\n" +
    "\n" +
    "<div ng-hide=\"isListEmpty() || showError\">\n" +
    "    <div class=\"input-group m-bottom-sm\">\n" +
    "        <span class=\"input-group-addon\">\n" +
    "            <i class=\"glyphicon glyphicon-search\"></i>\n" +
    "        </span>\n" +
    "        <sf-items-filter sf-filter='filter'></sf-items-filter>\n" +
    "    </div>\n" +
    "    <div class=\"list-group list-group-endless\" kendo-sortable k-options=\"sortableOptions\" k-on-change=\"sortItems(kendoEvent)\">\n" +
    "        <div class=\"list-group-item list-group-item-multiselect list-group-item-draggable\"\n" +
    "            ng-click=\"itemClicked(item.item)\"\n" +
    "            ng-repeat=\"item in currentItems\">\n" +
    "\n" +
    "            <span class=\"handler list-group-item-drag\" ng-hide=\"!!filter.searchString || !sfSortable\"></span>\n" +
    "\n" +
    "            <input type=\"checkbox\" ng-checked=\"item.isChecked\">\n" +
    "\n" +
    "            <div><span sf-shrinked-breadcrumb=\"{{bindIdentifierField(item.item)}}\" sf-max-length=\"60\"></span></div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "    <div ng-hide=\"currentItems.length\"><i>No items found</i></div>\n" +
    "</div>\n" +
    "");
  $templateCache.put("../Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/date-time/sf-date-filter.html",
    "<div class=\"checkbox\" >\n" +
    "    <label for=\"dateInput\">\n" +
    "        <input id=\"dateInput\" type=\"checkbox\"\n" +
    "            ng-click=\"toggleDateSelection(sfQueryFieldName)\"\n" +
    "            ng-checked=\"selectedDateFilters.hasOwnProperty(sfQueryFieldName)\"  />\n" +
    "\n" +
    "            Date...\n" +
    "    </label>\n" +
    "\n" +
    "    <sf-timespan-selector class=\"label-content\" sf-change=\"change\" sf-selected-item=\"selectedDateFilters[sfQueryFieldName]\"\n" +
    "    ng-show=\"selectedDateFilters.hasOwnProperty(sfQueryFieldName)\"></sf-timespan-selector>\n" +
    "</div>\n" +
    "\n" +
    "");
  $templateCache.put("../Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/date-time/sf-date-time-picker.html",
    "<div class=\"row row-xs\">\n" +
    "    <div class=\"col-xs-5\">\n" +
    "        <p class=\"input-group\">\n" +
    "            <input id=\"fromInput\" type=\"text\" class=\"form-control input-sm\"\n" +
    "            datepicker-popup=\"dd-MMMM-yyyy\" show-button-bar=\"false\" datepicker-options=\"{'show-weeks' : 'false', 'starting-day': '1'}\"\n" +
    "            ng-model=\"ngModel\" is-open=\"isOpen\" close-text=\"Close\" />\n" +
    "\n" +
    "            <span class=\"input-group-btn\">\n" +
    "                <button type=\"button\" class=\"btn btn-default btn-sm\" ng-click=\"openDatePicker($event)\">\n" +
    "                    <i class=\"glyphicon glyphicon-calendar\"></i>\n" +
    "                </button>\n" +
    "            </span>\n" +
    "        </p>\n" +
    "    </div>\n" +
    "\n" +
    "    <div class=\"col-xs-3\">\n" +
    "        <a class=\"btn btn-link btn-sm\" ng-show=\"!showMinutesField\" ng-click=\"updateHours(0)\">Add hour</a>\n" +
    "\n" +
    "        <select class=\"form-control input-sm\" ng-model=\"hstep\" ng-change=\"updateHours(hstep)\" ng-options=\"opt.value as opt.label for opt in hsteps\" ng-show=\"showMinutesField\"></select>\n" +
    "    </div>\n" +
    "\n" +
    "    <div class=\"col-xs-3\">\n" +
    "        <a class=\"btn btn-link btn-sm\" ng-show=\"!showMinutesDropdown && showMinutesField\"  ng-click=\"updateMinutes(0)\">Add minutes</a>\n" +
    "\n" +
    "        <select class=\"form-control input-sm\" ng-model=\"mstep\" ng-change=\"updateMinutes(mstep)\" ng-options=\"opt.value as opt.label for opt in msteps\" ng-show=\"showMinutesDropdown && showMinutesField\"></select>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
  $templateCache.put("../Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/date-time/sf-timespan-selector.html",
    "<div>\n" +
    "    <div id=\"selectedItemsPlaceholder\">\n" +
    "        <alert type=\"danger\" ng-show=\"showError\">{{errorMessage}}</alert>\n" +
    "\n" +
    "        <div ng-hide=\"showError\">\n" +
    "            <span class=\"label label-taxon label-full\" ng-bind=\"sfSelectedItem.displayText\"></span>\n" +
    "            <button class=\"btn btn-xs btn-default openSelectorBtn\" ng-click=\"open()\">\n" +
    "                <span ng-hide=\"isItemSelected()\">Select</span>\n" +
    "                <span ng-show=\"isItemSelected()\">Change</span>\n" +
    "            </button>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "    <div class=\"contentSelector\" template-url =\"timespan-selector-content\" modal window-class=\"sf-designer-dlg sf-timespan-selector-dlg\" existing-scope=\"true\">\n" +
    "\n" +
    "    </div>\n" +
    "</div>\n" +
    "\n" +
    "<script type=\"text/ng-template\" id=\"timespan-selector-content\">\n" +
    "    <div class=\"modal-header\">\n" +
    "        <h3 class=\"modal-title\">Select dates</h3>\n" +
    "    </div>\n" +
    "    <div class=\"modal-body\">\n" +
    "\n" +
    "\n" +
    "        <alert type=\"danger\" ng-show=\"showError\">{{errorMessage}}</alert>\n" +
    "\n" +
    "        <h4>Display items published in...</h4>\n" +
    "        <form name=\"periodSelection\">\n" +
    "            <div class=\"radio\">\n" +
    "                <label for=\"anyTime\">\n" +
    "                    <input id=\"anyTime\" type=\"radio\" value=\"anyTime\" ng-model=\"selectedItemInTheDialog.periodType\" />\n" +
    "                    Any time\n" +
    "                </label>\n" +
    "            </div>\n" +
    "            <div class=\"radio form-inline\">\n" +
    "                <label for=\"periodToNow\">\n" +
    "                    <input id=\"periodToNow\" type=\"radio\" value=\"periodToNow\" ng-model=\"selectedItemInTheDialog.periodType\"/>\n" +
    "                    Last\n" +
    "                </label>\n" +
    "\n" +
    "                <input type=\"number\" min=\"1\" ng-model=\"selectedItemInTheDialog.timeSpanValue\"\n" +
    "                        ng-disabled=\"selectedItemInTheDialog.periodType!='periodToNow'\"\n" +
    "                        style=\"width: 70px\" class=\"form-control input-sm\" />\n" +
    "\n" +
    "                <select ng-model=\"selectedItemInTheDialog.timeSpanInterval\"\n" +
    "                        ng-disabled=\"selectedItemInTheDialog.periodType!='periodToNow'\"\n" +
    "                        class=\"form-control input-sm\">\n" +
    "                    <option value=\"days\">day(s)</option>\n" +
    "                    <option value=\"weeks\">week(s)</option>\n" +
    "                    <option value=\"months\">month(s)</option>\n" +
    "                    <option value=\"years\">year(s)</option>\n" +
    "                </select>\n" +
    "            </div>\n" +
    "            <div class=\"radio\">\n" +
    "                <label for=\"customRange\">\n" +
    "                    <input id=\"customRange\" type=\"radio\" value=\"customRange\" ng-model=\"selectedItemInTheDialog.periodType\" />\n" +
    "                    Custom range...\n" +
    "                </label>\n" +
    "\n" +
    "                <div class=\"label-content\" ng-show=\"selectedItemInTheDialog.periodType=='customRange'\">\n" +
    "                    <strong>From</strong>\n" +
    "                    <sf-date-time-picker ng-model=\"selectedItemInTheDialog.fromDate\" sf-show-meridian=\"true\"></sf-date-time-picker >\n" +
    "\n" +
    "                    <strong>To</strong>\n" +
    "                    <sf-date-time-picker ng-model=\"selectedItemInTheDialog.toDate\" sf-show-meridian=\"true\"></sf-date-time-picker >\n" +
    "                </div>\n" +
    "\n" +
    "            </div>\n" +
    "        </form>\n" +
    "    </div>\n" +
    "    <div class=\"modal-footer\">\n" +
    "        <button type=\"button\" class=\"btn btn-primary pull-left\" ng-click=\"selectItem()\">Done selecting</button>\n" +
    "        <button type=\"button\" class=\"btn btn-link pull-left\" ng-click=\"cancel()\">Cancel</button>\n" +
    "    </div>\n" +
    "</script>\n" +
    "\n" +
    "");
  $templateCache.put("../Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/dynamic-modules/sf-dynamic-items-selector.html",
    "<script type=\"text/ng-template\" id=\"sf-dynamic-items-selector-template\">\n" +
    "    <div class=\"modal-header\">\n" +
    "        <button type=\"button\" class=\"close\" ng-click=\"cancel()\">&times;</button>\n" +
    "        <h3 class=\"modal-title\">Select content</h3>\n" +
    "    </div>\n" +
    "    <div class=\"modal-body\">\n" +
    "        <div kendo-tab-strip class=\"k-tabstrip--selection\">\n" +
    "            <ul ng-show=\"multiselect\">\n" +
    "                <li ng-class=\"{true:'k-state-active', false:''}[!isItemSelected() || multiselect === false]\" ng-click=\"removeUnselectedItems()\">\n" +
    "                    All\n" +
    "                </li>\n" +
    "                <li ng-class=\"{true:'k-state-active', false:''}[isItemSelected() && multiselect === true]\" ng-click=\"collectSelectedItems()\">\n" +
    "\n" +
    "                    Selected <span class=\"badge\">{{getSelectedItemsCount()}}</span>\n" +
    "                </li>\n" +
    "            </ul>\n" +
    "            <div>\n" +
    "                <div ng-show=\"noItemsExist\" class=\"alert alert-info\">No items have been created yet.</div>\n" +
    "\n" +
    "                <alert type=\"danger\" ng-show=\"showError\">{{errorMessage}}</alert>\n" +
    "\n" +
    "                <div ng-hide=\"noItemsExist || showError\">\n" +
    "                    <div class=\"input-group m-bottom-sm\">\n" +
    "                        <span class=\"input-group-addon\">\n" +
    "                            <i class=\"glyphicon glyphicon-search\"></i>\n" +
    "                        </span>\n" +
    "                        <sf-items-filter sf-filter='filter'></sf-items-filter>\n" +
    "                    </div>\n" +
    "                    <div sf-endless-scroll sf-paging='paging' class=\"list-group list-group-endless\">\n" +
    "                        <div ng-repeat=\"item in items\"\n" +
    "                            ng-class=\"{'list-group-item':true, 'active': isItemSelectedInDialog(item), 'list-group-item-multiselect': multiselect }\"\n" +
    "                            ng-click=\"itemClicked($index, item)\">\n" +
    "\n" +
    "                            <input type=\"checkbox\" ng-checked=\"isItemSelectedInDialog(item)\" ng-show=\"multiselect\">\n" +
    "\n" +
    "                            <div ng-bind=\"bindIdentifierField(item)\"></div>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                    <div ng-hide=\"items.length || showLoadingIndicator\"><i>No items found</i></div>\n" +
    "                    <div class=\"s-loading\" ng-show=\"showLoadingIndicator\" style=\"display: inline-block; width: 30px;\"></div>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "            <div>\n" +
    "                <sf-selected-items-view sf-items='selectedItemsViewData' sf-selected-items='selectedItemsInTheDialog' sf-search-identifier-field='searchIdentifierField'\n" +
    "                    sf-identifier-field='sfIdentifierField' sf-sortable='{{sfSortable}}'>\n" +
    "                </selected-items-view>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "    <div class=\"modal-footer\">\n" +
    "        <button type=\"button\" ng-hide=\"noItemsExist || showError\" class=\"btn btn-primary  pull-left\" ng-click=\"doneSelecting()\">Done selecting</button>\n" +
    "        <button type=\"button\" class=\"btn btn-link  pull-left\" ng-click=\"cancel()\">Cancel</button>\n" +
    "    </div>\n" +
    "</script>\n" +
    "");
  $templateCache.put("../Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/news/sf-news-selector.html",
    "<script type=\"text/ng-template\" id=\"sf-news-selector-template\">\n" +
    "    <div class=\"modal-header\">\n" +
    "        <button type=\"button\" class=\"close\" ng-click=\"cancel()\">&times;</button>\n" +
    "        <h3 class=\"modal-title\">Select news</h3>\n" +
    "    </div>\n" +
    "    <div class=\"modal-body\">\n" +
    "        <div kendo-tab-strip k-animation=\"false\" class=\"k-tabstrip--selection\">\n" +
    "            <ul ng-show=\"multiselect\">\n" +
    "                <li ng-class=\"{true:'k-state-active', false:''}[!isItemSelected() || multiselect === false]\" ng-click=\"removeUnselectedItems()\">\n" +
    "                    All\n" +
    "                </li>\n" +
    "                <li ng-class=\"{true:'k-state-active', false:''}[isItemSelected() && multiselect === true]\" ng-click=\"collectSelectedItems()\">\n" +
    "                    Selected <span class=\"badge\">{{getSelectedItemsCount()}}</span>\n" +
    "                </li>\n" +
    "            </ul>\n" +
    "            <div>\n" +
    "                <div ng-show=\"noItemsExist\" class=\"alert alert-info\">No items have been created yet.</div>\n" +
    "\n" +
    "                <alert type=\"danger\" ng-show=\"showError\">{{errorMessage}}</alert>\n" +
    "\n" +
    "                <div ng-hide=\"noItemsExist || showError\">\n" +
    "                    <div class=\"input-group m-bottom-sm\">\n" +
    "                        <span class=\"input-group-addon\">\n" +
    "                            <i class=\"glyphicon glyphicon-search\"></i>\n" +
    "                        </span>\n" +
    "                        <sf-items-filter sf-filter='filter'></sf-items-filter>\n" +
    "                    </div>\n" +
    "                    <div sf-endless-scroll sf-paging='paging' class=\"list-group list-group-endless\">\n" +
    "                        <div ng-repeat=\"item in items\"\n" +
    "                             ng-class=\"{'list-group-item': true, 'active': isItemSelectedInDialog(item), 'list-group-item-multiselect' : multiselect }\"\n" +
    "                             ng-click=\"itemClicked($index, item)\">\n" +
    "\n" +
    "                            <input type=\"checkbox\" ng-checked=\"isItemSelectedInDialog(item)\" ng-show=\"multiselect\">\n" +
    "\n" +
    "                            <div ng-bind=\"bindIdentifierField(item)\"></div>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                    <div ng-hide=\"items.length || showLoadingIndicator\"><i>No items found</i></div>\n" +
    "                    <sf-loading ng-show=\"showLoadingIndicator\"></sf-loading>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "            <div>\n" +
    "                <sf-selected-items-view sf-items='selectedItemsViewData' sf-selected-items='selectedItemsInTheDialog' sf-search-identifier-field='searchIdentifierField'\n" +
    "                    sf-identifier-field='sfIdentifierField' sf-sortable='{{sfSortable}}'>\n" +
    "                </selected-items-view>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "    <div class=\"modal-footer\">\n" +
    "        <button type=\"button\" ng-hide=\"noItemsExist || showError\" class=\"btn btn-primary pull-left\" ng-click=\"doneSelecting()\">Done selecting</button>\n" +
    "        <button type=\"button\" ng-hide=\"noItemsExist || showError\" class=\"btn btn-link pull-left\" ng-click=\"cancel()\">Cancel</button>\n" +
    "\n" +
    "        <button type=\"button\" ng-show=\"noItemsExist\" class=\"btn btn-primary pull-left\" ng-click=\"cancel()\">Close</button>\n" +
    "\n" +
    "    </div>\n" +
    "</script>\n" +
    "");
  $templateCache.put("../Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/pages/sf-page-selector.html",
    "<script type=\"text/ng-template\" id=\"sf-page-selector-template\">\n" +
    "    <div class=\"modal-header\">\n" +
    "        <button type=\"button\" class=\"close\" ng-click=\"cancel()\">&times;</button>\n" +
    "        <h3 class=\"modal-title\">Select a page</h3>\n" +
    "    </div>\n" +
    "    <div class=\"modal-body\">\n" +
    "        <div kendo-tab-strip class=\"k-tabstrip--selection\">\n" +
    "            <ul ng-show=\"multiselect\">\n" +
    "                <li ng-class=\"{true:'k-state-active', false:''}[!isItemSelected() || multiselect === false]\" ng-click=\"removeUnselectedItems()\">\n" +
    "                    All\n" +
    "                </li>\n" +
    "                <li ng-class=\"{true:'k-state-active', false:''}[isItemSelected() && multiselect === true]\" ng-click=\"collectSelectedItems()\">\n" +
    "                    Selected <span class=\"badge\">{{getSelectedItemsCount()}}</span>\n" +
    "                </li>\n" +
    "            </ul>\n" +
    "            <div>\n" +
    "                <div ng-show=\"noItemsExist\" class=\"alert alert-info\">No items have been created yet.</div>\n" +
    "\n" +
    "                <alert type=\"danger\" ng-show=\"showError\">{{errorMessage}}</alert>\n" +
    "\n" +
    "                <div ng-hide=\"noItemsExist || showError\">\n" +
    "                    <div class=\"input-group m-bottom-sm\">\n" +
    "                        <span class=\"input-group-addon\">\n" +
    "                            <i class=\"glyphicon glyphicon-search\"></i>\n" +
    "                        </span>\n" +
    "                        <sf-items-filter sf-filter='filter'></sf-items-filter>\n" +
    "                    </div>\n" +
    "                    <div class=\"list-group list-group-endless\">\n" +
    "                        <sf-items-tree ng-show=\"filter.isEmpty\"\n" +
    "                                       sf-multiselect=\"multiselect\"\n" +
    "                                       sf-expand-selection=\"expandSelection\"\n" +
    "                                       sf-items-promise=\"itemsPromise\"\n" +
    "                                       sf-selected-ids=\"sfSelectedIds\"\n" +
    "                                       sf-get-predecessors=\"getPredecessors(itemId)\"\n" +
    "                                       sf-select-item=\"itemClicked(dataItem)\"\n" +
    "                                       sf-item-selected=\"isItemSelectedInDialog(dataItem)\"\n" +
    "                                       sf-item-disabled=\"itemDisabled(dataItem)\"\n" +
    "                                       sf-get-children=\"getChildren(parentId)\"\n" +
    "                                       sf-identifier-field-value=\"bindIdentifierField(dataItem)\"\n" +
    "                                       class=\"k-treeview--selection\"></sf-items-tree>\n" +
    "\n" +
    "                        <div ng-repeat=\"item in items\"\n" +
    "                             ng-hide=\"filter.isEmpty\"\n" +
    "                             ng-class=\"{'list-group-item':true, 'active': isItemSelectedInDialog(item), 'list-group-item-multiselect': multiselect }\"\n" +
    "                             ng-click=\"itemClicked($index, item)\">\n" +
    "\n" +
    "                            <input type=\"checkbox\" ng-checked=\"isItemSelectedInDialog(item)\" ng-hide=\"itemDisabled(item) || !multiselect\">\n" +
    "\n" +
    "                            <div>\n" +
    "                                <div ng-bind=\"bindIdentifierField(item)\" />\n" +
    "\n" +
    "                                <div ng-show=\"itemDisabled(item)\">(not translated)</div>\n" +
    "                            </div>\n" +
    "\n" +
    "                            <div ng-class=\"{'text-muted': !isItemSelectedInDialog(item)}\">\n" +
    "                                <span sf-shrinked-breadcrumb=\"{{item.ParentTitlesPath}}\" sf-max-length=\"60\"></span>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                    <div ng-hide=\"items.length || showLoadingIndicator\"><i>No items found</i></div>\n" +
    "                    <sf-loading ng-show=\"showLoadingIndicator\"></sf-loading>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "            <div>\n" +
    "                <sf-selected-items-view sf-items='selectedItemsViewData' sf-selected-items='selectedItemsInTheDialog' sf-search-identifier-field='searchIdentifierField'\n" +
    "                                        sf-identifier-field='sfIdentifierField' sf-sortable='{{sfSortable}}'>\n" +
    "                    </selected-items-view>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "    <div class=\"modal-footer\">\n" +
    "        <button type=\"button\" ng-hide=\"noItemsExist || showError\" class=\"btn btn-primary  pull-left\" ng-click=\"doneSelecting()\">Done selecting</button>\n" +
    "        <button type=\"button\" class=\"btn btn-link  pull-left\" ng-click=\"cancel()\">Cancel</button>\n" +
    "    </div>\n" +
    "</script>\n" +
    "");
  $templateCache.put("../Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/taxonomies/sf-hierarchical-taxon-selector.html",
    "<script type=\"text/ng-template\" id=\"sf-hierarchical-taxon-selector\">\n" +
    "    <div class=\"modal-header\">\n" +
    "        <button type=\"button\" class=\"close\" ng-click=\"cancel()\">&times;</button>\n" +
    "        <h3 class=\"modal-title\">Select {{sfDialogHeader}}</h3>\n" +
    "    </div>\n" +
    "    <div class=\"modal-body\">\n" +
    "        <div kendo-tab-strip class=\"k-tabstrip--selection\">\n" +
    "            <ul ng-show=\"multiselect\">\n" +
    "                <li ng-class=\"{true:'k-state-active', false:''}[!isItemSelected() || multiselect === false]\" ng-click=\"removeUnselectedItems()\">\n" +
    "                    All\n" +
    "                </li>\n" +
    "                <li ng-class=\"{true:'k-state-active', false:''}[isItemSelected() && multiselect === true]\" ng-click=\"collectSelectedItems()\">\n" +
    "                    Selected <span class=\"badge\">{{getSelectedItemsCount()}}</span>\n" +
    "                </li>\n" +
    "            </ul>\n" +
    "            <div>\n" +
    "                <div ng-show=\"noItemsExist\" class=\"alert alert-info\">No items have been created yet.</div>\n" +
    "\n" +
    "                <alert type=\"danger\" ng-show=\"showError\">{{errorMessage}}</alert>\n" +
    "\n" +
    "                <div ng-hide=\"noItemsExist || showError\">\n" +
    "                    <div class=\"input-group m-bottom-sm\">\n" +
    "                        <span class=\"input-group-addon\">\n" +
    "                            <i class=\"glyphicon glyphicon-search\"></i>\n" +
    "                        </span>\n" +
    "                        <sf-items-filter sf-filter='filter'></sf-items-filter>\n" +
    "                    </div>\n" +
    "\n" +
    "                    <div class=\"list-group list-group-endless\">\n" +
    "                        <sf-items-tree ng-show=\"filter.isEmpty\"\n" +
    "                                       sf-multiselect=\"multiselect\"\n" +
    "                                       sf-expand-selection=\"expandSelection\"\n" +
    "                                       sf-items-promise=\"itemsPromise\"\n" +
    "                                       sf-items=\"items\"\n" +
    "                                       sf-get-predecessors=\"getPredecessors(itemId)\"\n" +
    "                                       sf-select-item=\"itemClicked(dataItem)\"\n" +
    "                                       sf-item-selected=\"isItemSelectedInDialog(dataItem)\"\n" +
    "                                       sf-item-disabled=\"itemDisabled(dataItem)\"\n" +
    "                                       sf-get-children=\"getChildren(parentId)\"\n" +
    "                                       sf-identifier-field-value=\"bindIdentifierField(dataItem)\"\n" +
    "                                       class=\"k-treeview--selection\"></sf-items-tree>\n" +
    "\n" +
    "                        <div ng-repeat=\"item in items\"\n" +
    "                             ng-hide=\"filter.isEmpty\"\n" +
    "                             ng-class=\"{'list-group-item':true, 'active': isItemSelectedInDialog(item), 'list-group-item-multiselect': multiselect }\"\n" +
    "                             ng-click=\"itemClicked($index, item)\">\n" +
    "\n" +
    "                            <input type=\"checkbox\" ng-checked=\"isItemSelectedInDialog(item)\" ng-show=\"multiselect\" ng-hide=\"itemDisabled(item)\">\n" +
    "\n" +
    "                            <div>\n" +
    "                                <div ng-bind=\"bindIdentifierField(item)\" />\n" +
    "\n" +
    "                                <div ng-class=\"{'text-muted': !isItemSelectedInDialog(item)}\">\n" +
    "                                    <span sf-shrinked-breadcrumb=\"{{item.RootPath}}\" sf-max-length=\"60\"></span>\n" +
    "                                </div>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "\n" +
    "                    </div>\n" +
    "                    <div ng-hide=\"items.length || showLoadingIndicator\"><i>No items found</i></div>\n" +
    "                    <div class=\"s-loading\" ng-show=\"showLoadingIndicator\" style=\"display: inline-block; width: 30px;\"></div>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "            <div>\n" +
    "                <sf-selected-items-view sf-items='selectedItemsViewData'\n" +
    "                                        sf-selected-items='selectedItemsInTheDialog'\n" +
    "                                        sf-search-identifier-field='searchIdentifierField'\n" +
    "                                        sf-identifier-field='sfIdentifierField'\n" +
    "                                        sf-sortable='{{sfSortable}}'></sf-selected-items-view>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "    <div class=\"modal-footer\">\n" +
    "        <button type=\"button\" ng-hide=\"noItemsExist || showError\" class=\"btn btn-primary pull-left\" ng-click=\"doneSelecting()\">Done selecting</button>\n" +
    "        <button type=\"button\" class=\"btn btn-link pull-left\" ng-click=\"cancel()\">Cancel</button>\n" +
    "    </div>\n" +
    "</script>\n" +
    "\n" +
    "");
  $templateCache.put("../Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/taxonomies/sf-taxon-filter.html",
    "<div class=\"checkbox\" ng-repeat=\"taxonomy in sfTaxonomyFields\">\n" +
    "    <label for=\"{{taxonomy.Name}}\" class=\"full-width\">\n" +
    "        <input id=\"{{taxonomy.Name}}\" type=\"checkbox\"\n" +
    "               ng-click=\"toggleTaxonomySelection(taxonomy.Name)\" value=\"{{taxonomy.Name}}\"\n" +
    "               ng-checked=\"selectedTaxonomies.hasOwnProperty(taxonomy.Name)\" />\n" +
    "        {{taxonomy.Title}}...\n" +
    "    </label>\n" +
    "\n" +
    "    <sf-list-selector class=\"label-content\"\n" +
    "                      sf-taxon-selector\n" +
    "                      sf-dialog-header=\"{{taxonomy.Title}}\"\n" +
    "                      ng-if=\"selectedTaxonomies.hasOwnProperty(taxonomy.Name) && taxonomy.TaxonomyType === 0\"\n" +
    "                      sf-multiselect=\"true\"\n" +
    "                      sf-selected-ids=\"selectedTaxonomies[taxonomy.Name]\"\n" +
    "                      sf-provider=\"sfProvider\"\n" +
    "                      sf-taxonomy-id=\"{{taxonomy.Id}}\"\n" +
    "                      sf-change=\"change\"></sf-list-selector>\n" +
    "\n" +
    "    <sf-list-selector class=\"label-content\"\n" +
    "                      sf-hierarchical-taxon-selector\n" +
    "                      sf-dialog-header=\"{{taxonomy.Title}}\"\n" +
    "                      ng-if=\"selectedTaxonomies.hasOwnProperty(taxonomy.Name) && taxonomy.TaxonomyType === 1\"\n" +
    "                      sf-multiselect=\"true\"\n" +
    "                      sf-selected-ids=\"selectedTaxonomies[taxonomy.Name]\"\n" +
    "                      sf-provider=\"sfProvider\"\n" +
    "                      sf-taxonomy-id=\"{{taxonomy.Id}}\"\n" +
    "                      sf-change=\"change\">\n" +
    "    </sf-list-selector>\n" +
    "\n" +
    "</div>\n" +
    "");
  $templateCache.put("../Tests/FeatherWidgets.ClientTest/helpers/Telerik.Sitefinity.Frontend/client-components/selectors/taxonomies/sf-taxon-selector.html",
    "<script type=\"text/ng-template\" id=\"sf-taxon-selector-template\">\n" +
    "    <div class=\"modal-header\">\n" +
    "        <button type=\"button\" class=\"close\" ng-click=\"cancel()\">&times;</button>\n" +
    "        <h3 class=\"modal-title\">Select {{sfDialogHeader}}</h3>\n" +
    "    </div>\n" +
    "    <div class=\"modal-body\">\n" +
    "        <div kendo-tab-strip k-animation=\"false\" class=\"k-tabstrip--selection\">\n" +
    "            <ul ng-show=\"multiselect\">\n" +
    "                <li ng-class=\"{true:'k-state-active', false:''}[!isItemSelected() || multiselect === false]\" ng-click=\"removeUnselectedItems()\">\n" +
    "                    All\n" +
    "                </li>\n" +
    "                <li ng-class=\"{true:'k-state-active', false:''}[isItemSelected() && multiselect === true]\" ng-click=\"collectSelectedItems()\">\n" +
    "                    Selected <span class=\"badge\">{{getSelectedItemsCount()}}</span>\n" +
    "                </li>\n" +
    "            </ul>\n" +
    "            <div>\n" +
    "                <div ng-show=\"noItemsExist\" class=\"alert alert-info\">No items have been created yet.</div>\n" +
    "\n" +
    "                <alert type=\"danger\" ng-show=\"showError\">{{errorMessage}}</alert>\n" +
    "\n" +
    "                <div ng-hide=\"noItemsExist || showError\">\n" +
    "                    <div class=\"input-group m-bottom-sm\">\n" +
    "                        <span class=\"input-group-addon\">\n" +
    "                            <i class=\"glyphicon glyphicon-search\"></i>\n" +
    "                        </span>\n" +
    "                        <sf-items-filter sf-filter='filter'></sf-items-filter>\n" +
    "                    </div>\n" +
    "                    <div sf-endless-scroll sf-paging='paging' class=\"list-group list-group-endless\">\n" +
    "                        <div ng-repeat=\"item in items\"\n" +
    "                             ng-class=\"{'list-group-item': true, 'active': isItemSelectedInDialog(item), 'list-group-item-multiselect': multiselect }\"\n" +
    "                             ng-click=\"itemClicked($index, item)\">\n" +
    "\n" +
    "                            <input type=\"checkbox\" ng-checked=\"isItemSelectedInDialog(item)\" ng-show=\"multiselect\">\n" +
    "\n" +
    "                            <div ng-bind=\"bindIdentifierField(item)\"></div>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                    <div ng-hide=\"items.length || showLoadingIndicator\"><i>No items found</i></div>\n" +
    "                    <sf-loading ng-show=\"showLoadingIndicator\"></sf-loading>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "            <div>\n" +
    "                <sf-selected-items-view sf-items='selectedItemsViewData' sf-selected-items='selectedItemsInTheDialog' sf-search-identifier-field='searchIdentifierField'\n" +
    "                    sf-identifier-field='sfIdentifierField' sf-sortable='{{sfSortable}}'>\n" +
    "                </selected-items-view>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "    <div class=\"modal-footer\">\n" +
    "        <button type=\"button\" ng-hide=\"noItemsExist || showError\" class=\"btn btn-primary pull-left\" ng-click=\"doneSelecting()\">Done selecting</button>\n" +
    "        <button type=\"button\" class=\"btn btn-link pull-left\" ng-click=\"cancel()\">Cancel</button>\n" +
    "    </div>\n" +
    "</script>\n" +
    "\n" +
    "");
}]);
