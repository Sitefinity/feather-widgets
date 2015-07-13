/* tests for the PropertyGridModule */
var sitefinity = { pageEditor: {} };
sitefinity.pageEditor.widgetContext = { AppPath: "/", HideSaveAllTranslations: true, Id: "16fcc965-f18a-6d82-8924-ff0000fd783f", MediaType: 0, PageId: "a6f4c965-f18a-6d82-8924-ff0000fd783f", PropertyServiceUrl: "/Sitefinity/Services/Pages/ControlPropertyService.svc/", PropertyValueCulture: null, url: "/Telerik.Sitefinity.Frontend/Designer/Master/DynamicContent" };


describe('Dynamic content designer view tests.',
 function () {
    var dataItems = {
        "Context": null, "IsGeneric": false,
        "Items": [
            { "CategoryName": "Misc", "CategoryNameSafe": "Misc", "ClientPropertyTypeName": "System.Object", "ElementCssClass": null, "InCategoryOrdinal": 5, "IsProxy": true, "ItemTypeName": "System.Object", "NeedsEditor": false, "PropertyId": "00000000-0000-0000-0000-000000000000", "PropertyName": "Settings", "PropertyPath": "/Settings", "PropertyValue": null, "TypeEditor": null },
            { "CategoryName": "Misc", "CategoryNameSafe": "Misc", "ClientPropertyTypeName": "System.String", "ElementCssClass": null, "InCategoryOrdinal": 5, "IsProxy": false, "ItemTypeName": "System.String", "NeedsEditor": false, "PropertyId": "00000000-0000-0000-0000-000000000000", "PropertyName": "ListTemplateName", "PropertyPath": "/Settings/ListTemplateName", "PropertyValue": "TestC", "TypeEditor": null },
            { "CategoryName": "Misc", "CategoryNameSafe": "Misc", "ClientPropertyTypeName": "System.String", "ElementCssClass": null, "InCategoryOrdinal": 5, "IsProxy": false, "ItemTypeName": "System.String", "NeedsEditor": false, "PropertyId": "00000000-0000-0000-0000-000000000000", "PropertyName": "DetailTemplateName", "PropertyPath": "/Settings/DetailTemplateName", "PropertyValue": "TestC", "TypeEditor": null },
            { "CategoryName": "Misc", "CategoryNameSafe": "Misc", "ClientPropertyTypeName": "System.Boolean", "ElementCssClass": null, "InCategoryOrdinal": 5, "IsProxy": false, "ItemTypeName": "System.Boolean", "NeedsEditor": false, "PropertyId": "00000000-0000-0000-0000-000000000000", "PropertyName": "OpenInSamePage", "PropertyPath": "/Settings/OpenInSamePage", "PropertyValue": "True", "TypeEditor": null },
            { "CategoryName": "Misc", "CategoryNameSafe": "Misc", "ClientPropertyTypeName": "System.Guid", "ElementCssClass": null, "InCategoryOrdinal": 5, "IsProxy": false, "ItemTypeName": "System.Guid", "NeedsEditor": false, "PropertyId": "00000000-0000-0000-0000-000000000000", "PropertyName": "DetailsPageId", "PropertyPath": "/Settings/DetailsPageId", "PropertyValue": "00000000-0000-0000-0000-000000000000", "TypeEditor": null },
            { "CategoryName": "Misc", "CategoryNameSafe": "Misc", "ClientPropertyTypeName": "Telerik.Sitefinity.Frontend.DynamicContent.Mvc.Models.IDynamicContentModel", "ElementCssClass": null, "InCategoryOrdinal": 5, "IsProxy": false, "ItemTypeName": "Telerik.Sitefinity.Frontend.DynamicContent.Mvc.Models.IDynamicContentModel", "NeedsEditor": true, "PropertyId": "00000000-0000-0000-0000-000000000000", "PropertyName": "Model", "PropertyPath": "/Settings/Model", "PropertyValue": null, "TypeEditor": null },
            { "CategoryName": "Misc", "CategoryNameSafe": "Misc", "ClientPropertyTypeName": "System.Boolean", "ElementCssClass": null, "InCategoryOrdinal": 5, "IsProxy": false, "ItemTypeName": "System.Boolean", "NeedsEditor": false, "PropertyId": "00000000-0000-0000-0000-000000000000", "PropertyName": "ListMode", "PropertyPath": "/Settings/Model/ListMode", "PropertyValue": "False", "TypeEditor": null },
            { "CategoryName": "Misc", "CategoryNameSafe": "Misc", "ClientPropertyTypeName": "System.String", "ElementCssClass": null, "InCategoryOrdinal": 5, "IsProxy": false, "ItemTypeName": "System.String", "NeedsEditor": false, "PropertyId": "00000000-0000-0000-0000-000000000000", "PropertyName": "ListCssClass", "PropertyPath": "/Settings/Model/ListCssClass", "PropertyValue": "", "TypeEditor": null },
            { "CategoryName": "Misc", "CategoryNameSafe": "Misc", "ClientPropertyTypeName": "System.String", "ElementCssClass": null, "InCategoryOrdinal": 5, "IsProxy": false, "ItemTypeName": "System.String", "NeedsEditor": false, "PropertyId": "00000000-0000-0000-0000-000000000000", "PropertyName": "DetailCssClass", "PropertyPath": "/Settings/Model/DetailCssClass", "PropertyValue": "", "TypeEditor": null },
            { "CategoryName": "Misc", "CategoryNameSafe": "Misc", "ClientPropertyTypeName": "System.String", "ElementCssClass": null, "InCategoryOrdinal": 5, "IsProxy": false, "ItemTypeName": "System.String", "NeedsEditor": false, "PropertyId": "00000000-0000-0000-0000-000000000000", "PropertyName": "SerializedSelectedItemsIds", "PropertyPath": "/Settings/Model/SerializedSelectedItemsIds", "PropertyValue": "[\"dbf0c965-f18a-6d82-8924-ff0000fd783f\"]", "TypeEditor": null },
            { "CategoryName": "Misc", "CategoryNameSafe": "Misc", "ClientPropertyTypeName": "System.Boolean", "ElementCssClass": null, "InCategoryOrdinal": 5, "IsProxy": false, "ItemTypeName": "System.Boolean", "NeedsEditor": false, "PropertyId": "00000000-0000-0000-0000-000000000000", "PropertyName": "EnableSocialSharing", "PropertyPath": "/Settings/Model/EnableSocialSharing", "PropertyValue": "False", "TypeEditor": null },
            { "CategoryName": "Misc", "CategoryNameSafe": "Misc", "ClientPropertyTypeName": "System.String", "ElementCssClass": null, "InCategoryOrdinal": 5, "IsProxy": false, "ItemTypeName": "System.String", "NeedsEditor": false, "PropertyId": "00000000-0000-0000-0000-000000000000", "PropertyName": "ProviderName", "PropertyPath": "/Settings/Model/ProviderName", "PropertyValue": "", "TypeEditor": null },
            { "CategoryName": "Misc", "CategoryNameSafe": "Misc", "ClientPropertyTypeName": "Telerik.Sitefinity.Frontend.Mvc.Models.SelectionMode", "ElementCssClass": null, "InCategoryOrdinal": 5, "IsProxy": false, "ItemTypeName": "Telerik.Sitefinity.Frontend.Mvc.Models.SelectionMode", "NeedsEditor": false, "PropertyId": "00000000-0000-0000-0000-000000000000", "PropertyName": "SelectionMode", "PropertyPath": "/Settings/Model/SelectionMode", "PropertyValue": "AllItems", "TypeEditor": null },
            { "CategoryName": "Misc", "CategoryNameSafe": "Misc", "ClientPropertyTypeName": "Telerik.Sitefinity.Frontend.Mvc.Models.ListDisplayMode", "ElementCssClass": null, "InCategoryOrdinal": 5, "IsProxy": false, "ItemTypeName": "Telerik.Sitefinity.Frontend.Mvc.Models.ListDisplayMode", "NeedsEditor": false, "PropertyId": "00000000-0000-0000-0000-000000000000", "PropertyName": "DisplayMode", "PropertyPath": "/Settings/Model/DisplayMode", "PropertyValue": "Paging", "TypeEditor": null },
            { "CategoryName": "Misc", "CategoryNameSafe": "Misc", "ClientPropertyTypeName": "System.Int32", "ElementCssClass": null, "InCategoryOrdinal": 5, "IsProxy": false, "ItemTypeName": "System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]", "NeedsEditor": false, "PropertyId": "00000000-0000-0000-0000-000000000000", "PropertyName": "ItemsPerPage", "PropertyPath": "/Settings/Model/ItemsPerPage", "PropertyValue": "20", "TypeEditor": null },
            { "CategoryName": "Misc", "CategoryNameSafe": "Misc", "ClientPropertyTypeName": "System.String", "ElementCssClass": null, "InCategoryOrdinal": 5, "IsProxy": false, "ItemTypeName": "System.String", "NeedsEditor": false, "PropertyId": "00000000-0000-0000-0000-000000000000", "PropertyName": "SortExpression", "PropertyPath": "/Settings/Model/SortExpression", "PropertyValue": "PublicationDate DESC", "TypeEditor": null },
            { "CategoryName": "Misc", "CategoryNameSafe": "Misc", "ClientPropertyTypeName": "System.String", "ElementCssClass": null, "InCategoryOrdinal": 5, "IsProxy": false, "ItemTypeName": "System.String", "NeedsEditor": false, "PropertyId": "00000000-0000-0000-0000-000000000000", "PropertyName": "FilterExpression", "PropertyPath": "/Settings/Model/FilterExpression", "PropertyValue": "", "TypeEditor": null },
            { "CategoryName": "Misc", "CategoryNameSafe": "Misc", "ClientPropertyTypeName": "System.String", "ElementCssClass": null, "InCategoryOrdinal": 5, "IsProxy": false, "ItemTypeName": "System.String", "NeedsEditor": false, "PropertyId": "00000000-0000-0000-0000-000000000000", "PropertyName": "SerializedAdditionalFilters", "PropertyPath": "/Settings/Model/SerializedAdditionalFilters", "PropertyValue": "", "TypeEditor": null },
            { "CategoryName": "Misc", "CategoryNameSafe": "Misc", "ClientPropertyTypeName": "System.Boolean", "ElementCssClass": null, "InCategoryOrdinal": 5, "IsProxy": false, "ItemTypeName": "System.Nullable`1[[System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]", "NeedsEditor": false, "PropertyId": "00000000-0000-0000-0000-000000000000", "PropertyName": "DisableCanonicalUrlMetaTag", "PropertyPath": "/Settings/Model/DisableCanonicalUrlMetaTag", "PropertyValue": "", "TypeEditor": null },
            { "CategoryName": "Misc", "CategoryNameSafe": "Misc", "ClientPropertyTypeName": "Telerik.Sitefinity.Frontend.DynamicContent.Mvc.Models.ParentFilterMode", "ElementCssClass": null, "InCategoryOrdinal": 5, "IsProxy": false, "ItemTypeName": "Telerik.Sitefinity.Frontend.DynamicContent.Mvc.Models.ParentFilterMode", "NeedsEditor": false, "PropertyId": "00000000-0000-0000-0000-000000000000", "PropertyName": "ParentFilterMode", "PropertyPath": "/Settings/Model/ParentFilterMode", "PropertyValue": "All", "TypeEditor": null },
            { "CategoryName": "Misc", "CategoryNameSafe": "Misc", "ClientPropertyTypeName": "System.String", "ElementCssClass": null, "InCategoryOrdinal": 5, "IsProxy": false, "ItemTypeName": "System.String", "NeedsEditor": false, "PropertyId": "00000000-0000-0000-0000-000000000000", "PropertyName": "CurrentlyOpenParentType", "PropertyPath": "/Settings/Model/CurrentlyOpenParentType", "PropertyValue": "", "TypeEditor": null },
            { "CategoryName": "Misc", "CategoryNameSafe": "Misc", "ClientPropertyTypeName": "System.String", "ElementCssClass": null, "InCategoryOrdinal": 5, "IsProxy": false, "ItemTypeName": "System.String", "NeedsEditor": false, "PropertyId": "00000000-0000-0000-0000-000000000000", "PropertyName": "SerializedSelectedParentsIds", "PropertyPath": "/Settings/Model/SerializedSelectedParentsIds", "PropertyValue": "[\"dbf0c965-f18a-6d82-8924-ff5400fd783f\", \"dbf0c965-f18a-6d82-8434-ff5400fd783f\"]", "TypeEditor": null },
            { "CategoryName": "Misc", "CategoryNameSafe": "Misc", "ClientPropertyTypeName": "System.String", "ElementCssClass": null, "InCategoryOrdinal": 5, "IsProxy": false, "ItemTypeName": "System.String", "NeedsEditor": false, "PropertyId": "00000000-0000-0000-0000-000000000000", "PropertyName": "RelatedItemType", "PropertyPath": "/Settings/Model/RelatedItemType", "PropertyValue": "", "TypeEditor": null },
            { "CategoryName": "Misc", "CategoryNameSafe": "Misc", "ClientPropertyTypeName": "System.String", "ElementCssClass": null, "InCategoryOrdinal": 5, "IsProxy": false, "ItemTypeName": "System.String", "NeedsEditor": false, "PropertyId": "00000000-0000-0000-0000-000000000000", "PropertyName": "RelatedItemProviderName", "PropertyPath": "/Settings/Model/RelatedItemProviderName", "PropertyValue": "", "TypeEditor": null },
            { "CategoryName": "Misc", "CategoryNameSafe": "Misc", "ClientPropertyTypeName": "System.String", "ElementCssClass": null, "InCategoryOrdinal": 5, "IsProxy": false, "ItemTypeName": "System.String", "NeedsEditor": false, "PropertyId": "00000000-0000-0000-0000-000000000000", "PropertyName": "RelatedFieldName", "PropertyPath": "/Settings/Model/RelatedFieldName", "PropertyValue": "", "TypeEditor": null },
            { "CategoryName": "Misc", "CategoryNameSafe": "Misc", "ClientPropertyTypeName": "Telerik.Sitefinity.RelatedData.RelationDirection", "ElementCssClass": null, "InCategoryOrdinal": 5, "IsProxy": false, "ItemTypeName": "Telerik.Sitefinity.RelatedData.RelationDirection", "NeedsEditor": false, "PropertyId": "00000000-0000-0000-0000-000000000000", "PropertyName": "RelationTypeToDisplay", "PropertyPath": "/Settings/Model/RelationTypeToDisplay", "PropertyValue": "Child", "TypeEditor": null },
            { "CategoryName": "Misc", "CategoryNameSafe": "Misc", "ClientPropertyTypeName": "System.Boolean", "ElementCssClass": null, "InCategoryOrdinal": 5, "IsProxy": false, "ItemTypeName": "System.Boolean", "NeedsEditor": false, "PropertyId": "00000000-0000-0000-0000-000000000000", "PropertyName": "ShowListViewOnEmpyParentFilter", "PropertyPath": "/Settings/Model/ShowListViewOnEmpyParentFilter", "PropertyValue": "False", "TypeEditor": null }
        ],
        "TotalCount": 27
    };

    var errorResponse = {
        Detail: 'Error'
    };
   
    var appPath = 'http://mysite.com:9999/myapp';

    describe('Controllers test.', function () {
        var $httpBackend, $rootScope, createController, propertyService, serverData;

        beforeEach(module(function ($provide) {
            var serverContext = {
                getRootedUrl: function (path) {
                    return appPath + '/' + path;
                },
                getUICulture: function () {
                    return null;
                }
            };
            $provide.value('serverContext', serverContext);
        }));

        beforeEach(module('designer'));

        beforeEach(inject(function ($injector) {
            // Set up the mock http service responses
            $httpBackend = $injector.get('$httpBackend');

            // Get hold of a scope (i.e. the root scope)
            $rootScope = $injector.get('$rootScope');

            // The $controller service is used to create instances of controllers
            var $controller = $injector.get('$controller');

            propertyService = $injector.get("propertyService");
            serverData = $injector.get("serverDataMock");

            createController = function () {
                return $controller('SimpleCtrl', {
                    '$scope': $rootScope, 'propertyService': propertyService, 'serverData': serverData
                });
            };
        }));

        describe('Tests with default data items.', function () {
            beforeEach(inject(function ($injector) {
                $httpBackend.expectGET('/Sitefinity/Services/Pages/ControlPropertyService.svc/16fcc965-f18a-6d82-8924-ff0000fd783f/?pageId=a6f4c965-f18a-6d82-8924-ff0000fd783f')
                    .respond(dataItems);
            }));

            it('[NPetrova] / The selected items ids are populated correctly for the selector.', function () {
                var controller = createController();
                $httpBackend.flush();
                $rootScope.$digest();

                expect($rootScope.itemSelector.selectedItemsIds).toBeDefined();
                expect($rootScope.itemSelector.selectedItemsIds.length).toEqual(1);
                expect($rootScope.itemSelector.selectedItemsIds).toEqual(["dbf0c965-f18a-6d82-8924-ff0000fd783f"]);
            });

            it('[NPetrova] / The selected parents items ids are populated correctly for the selector.', function () {
                var controller = createController();
                $httpBackend.flush();
                $rootScope.$digest();

                expect($rootScope.parentSelector.selectedItemsIds).toBeDefined();
                expect($rootScope.parentSelector.selectedItemsIds.length).toEqual(2);
                expect($rootScope.parentSelector.selectedItemsIds).toEqual(["dbf0c965-f18a-6d82-8924-ff5400fd783f", "dbf0c965-f18a-6d82-8434-ff5400fd783f"]);
            });

            it('[NPetrova] / The SortExpression is correctly changed when the sorting option is changed to Title DESC.', function () {
                var controller = createController();
                $httpBackend.flush();
                $rootScope.$digest();

                expect($rootScope.selectedSortOption).toEqual("PublicationDate DESC");

                var newSortingOption = "Title DESC";
                $rootScope.selectedSortOption = newSortingOption;
                $rootScope.updateSortOption($rootScope.selectedSortOption);
                expect($rootScope.selectedSortOption).toEqual(newSortingOption);
                expect($rootScope.properties.SortExpression.PropertyValue).toEqual(newSortingOption);
            });
        });

        describe('Tests with customised data items.', function () {
            beforeEach(inject(function ($injector) {
                var items = JSON.parse(JSON.stringify(dataItems));
                items.Items[15].PropertyValue = "Content DESC";

                $httpBackend.expectGET('/Sitefinity/Services/Pages/ControlPropertyService.svc/16fcc965-f18a-6d82-8924-ff0000fd783f/?pageId=a6f4c965-f18a-6d82-8924-ff0000fd783f')
                    .respond(items);
            }));

            it('[NPetrova] / The SortExpression is correctly changed when the sorting option is changed to custom sort expression.', function () {
                var controller = createController();
                $httpBackend.flush();
                $rootScope.$digest();

                expect($rootScope.selectedSortOption).toEqual("Custom");
            });
        });

        describe('Tests verifying error mesages.', function () {
            it('[NPetrova] / The error message is populated correctly when property service returns an error.', function () {
                $httpBackend.expectGET('/Sitefinity/Services/Pages/ControlPropertyService.svc/16fcc965-f18a-6d82-8924-ff0000fd783f/?pageId=a6f4c965-f18a-6d82-8924-ff0000fd783f')
                    .respond(500, errorResponse);

                var controller = createController();
                $httpBackend.flush();
                $rootScope.$digest();

                expect($rootScope.feedback.errorMessage).toBeDefined();
                expect($rootScope.feedback.errorMessage).toBe('Error');
            });
        });
    });
});
