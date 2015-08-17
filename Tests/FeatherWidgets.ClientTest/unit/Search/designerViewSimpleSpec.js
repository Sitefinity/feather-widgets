/* tests for the PropertyGridModule */
var sitefinity = { pageEditor: {} };
sitefinity.pageEditor.widgetContext = { AppPath: "/", HideSaveAllTranslations: true, Id: "16fcc965-f18a-6d82-8924-ff0000fd783f", MediaType: 0, PageId: "a6f4c965-f18a-6d82-8924-ff0000fd783f", PropertyServiceUrl: "/Sitefinity/Services/Pages/ControlPropertyService.svc/", PropertyValueCulture: null, url: "/Telerik.Sitefinity.Frontend/Designer/Master/SearchBox" };


describe('Search designer view tests.', function () {
    var dataItems = {
        "Context": null,
        "IsGeneric": false,
        "Items": [
            {
                "CategoryName": "Misc",
                "CategoryNameSafe": "Misc",
                "ClientPropertyTypeName": "System.Object",
                "ElementCssClass": null,
                "InCategoryOrdinal": 5,
                "IsProxy": true,
                "ItemTypeName": "System.Object",
                "NeedsEditor": false,
                "PropertyId": "00000000-0000-0000-0000-000000000000",
                "PropertyName": "Settings",
                "PropertyPath": "/Settings",
                "PropertyValue": null,
                "TypeEditor": null
            }, {
                "CategoryName": "Misc",
                "CategoryNameSafe": "Misc",
                "ClientPropertyTypeName": "System.String",
                "ElementCssClass": null,
                "InCategoryOrdinal": 5,
                "IsProxy": false,
                "ItemTypeName": "System.String",
                "NeedsEditor": false,
                "PropertyId": "00000000-0000-0000-0000-000000000000",
                "PropertyName": "TemplateName",
                "PropertyPath": "/Settings/TemplateName",
                "PropertyValue": "SearchBox",
                "TypeEditor": null
            }, {
                "CategoryName": "Misc",
                "CategoryNameSafe": "Misc",
                "ClientPropertyTypeName": "Telerik.Sitefinity.Frontend.Search.Mvc.Models.ISearchBoxModel",
                "ElementCssClass": null,
                "InCategoryOrdinal": 5,
                "IsProxy": false,
                "ItemTypeName": "Telerik.Sitefinity.Frontend.Search.Mvc.Models.ISearchBoxModel",
                "NeedsEditor": true,
                "PropertyId": "00000000-0000-0000-0000-000000000000",
                "PropertyName": "Model",
                "PropertyPath": "/Settings/Model",
                "PropertyValue": null,
                "TypeEditor": null
            }, {
                "CategoryName": "Misc",
                "CategoryNameSafe": "Misc",
                "ClientPropertyTypeName": "Telerik.Sitefinity.Frontend.Search.WordsMode",
                "ElementCssClass": null,
                "InCategoryOrdinal": 5,
                "IsProxy": false,
                "ItemTypeName": "Telerik.Sitefinity.Frontend.Search.WordsMode",
                "NeedsEditor": false,
                "PropertyId": "00000000-0000-0000-0000-000000000000",
                "PropertyName": "WordsMode",
                "PropertyPath": "/Settings/Model/WordsMode",
                "PropertyValue": "AllWords",
                "TypeEditor": null
            }, {
                "CategoryName": "Misc",
                "CategoryNameSafe": "Misc",
                "ClientPropertyTypeName": "System.String",
                "ElementCssClass": null,
                "InCategoryOrdinal": 5,
                "IsProxy": false,
                "ItemTypeName": "System.String",
                "NeedsEditor": false,
                "PropertyId": "00000000-0000-0000-0000-000000000000",
                "PropertyName": "ResultsUrl",
                "PropertyPath": "/Settings/Model/ResultsUrl",
                "PropertyValue": "",
                "TypeEditor": null
            }, {
                "CategoryName": "Misc",
                "CategoryNameSafe": "Misc",
                "ClientPropertyTypeName": "System.String",
                "ElementCssClass": null,
                "InCategoryOrdinal": 5,
                "IsProxy": false,
                "ItemTypeName": "System.String",
                "NeedsEditor": false,
                "PropertyId": "00000000-0000-0000-0000-000000000000",
                "PropertyName": "ResultsPageId",
                "PropertyPath": "/Settings/Model/ResultsPageId",
                "PropertyValue": "",
                "TypeEditor": null
            }, {
                "CategoryName": "Misc",
                "CategoryNameSafe": "Misc",
                "ClientPropertyTypeName": "System.String",
                "ElementCssClass": null,
                "InCategoryOrdinal": 5,
                "IsProxy": false,
                "ItemTypeName": "System.String",
                "NeedsEditor": false,
                "PropertyId": "00000000-0000-0000-0000-000000000000",
                "PropertyName": "SiteRootName",
                "PropertyPath": "/Settings/Model/SiteRootName",
                "PropertyValue": "",
                "TypeEditor": null
            }, {
                "CategoryName": "Misc",
                "CategoryNameSafe": "Misc",
                "ClientPropertyTypeName": "System.String",
                "ElementCssClass": null,
                "InCategoryOrdinal": 5,
                "IsProxy": false,
                "ItemTypeName": "System.String",
                "NeedsEditor": false,
                "PropertyId": "00000000-0000-0000-0000-000000000000",
                "PropertyName": "SearchIndexPipeId",
                "PropertyPath": "/Settings/Model/SearchIndexPipeId",
                "PropertyValue": "dbf0c965-f18a-6d82-8924-ff0000fd783f",
                "TypeEditor": null
            }, {
                "CategoryName": "Misc",
                "CategoryNameSafe": "Misc",
                "ClientPropertyTypeName": "System.String",
                "ElementCssClass": null,
                "InCategoryOrdinal": 5,
                "IsProxy": false,
                "ItemTypeName": "System.String",
                "NeedsEditor": false,
                "PropertyId": "00000000-0000-0000-0000-000000000000",
                "PropertyName": "IndexCatalogue",
                "PropertyPath": "/Settings/Model/IndexCatalogue",
                "PropertyValue": "",
                "TypeEditor": null
            }, {
                "CategoryName": "Misc",
                "CategoryNameSafe": "Misc",
                "ClientPropertyTypeName": "System.String",
                "ElementCssClass": null,
                "InCategoryOrdinal": 5,
                "IsProxy": false,
                "ItemTypeName": "System.String",
                "NeedsEditor": false,
                "PropertyId": "00000000-0000-0000-0000-000000000000",
                "PropertyName": "SuggestionFields",
                "PropertyPath": "/Settings/Model/SuggestionFields",
                "PropertyValue": "Title,Content",
                "TypeEditor": null
            }, {
                "CategoryName": "Misc",
                "CategoryNameSafe": "Misc",
                "ClientPropertyTypeName": "System.String",
                "ElementCssClass": null,
                "InCategoryOrdinal": 5,
                "IsProxy": false,
                "ItemTypeName": "System.String",
                "NeedsEditor": false,
                "PropertyId": "00000000-0000-0000-0000-000000000000",
                "PropertyName": "SuggestionsRoute",
                "PropertyPath": "/Settings/Model/SuggestionsRoute",
                "PropertyValue": "/restapi/search/suggestions",
                "TypeEditor": null
            }, {
                "CategoryName": "Misc",
                "CategoryNameSafe": "Misc",
                "ClientPropertyTypeName": "System.Boolean",
                "ElementCssClass": null,
                "InCategoryOrdinal": 5,
                "IsProxy": false,
                "ItemTypeName": "System.Boolean",
                "NeedsEditor": false,
                "PropertyId": "00000000-0000-0000-0000-000000000000",
                "PropertyName": "DisableSuggestions",
                "PropertyPath": "/Settings/Model/DisableSuggestions",
                "PropertyValue": "False",
                "TypeEditor": null
            }, {
                "CategoryName": "Misc",
                "CategoryNameSafe": "Misc",
                "ClientPropertyTypeName": "System.Int32",
                "ElementCssClass": null,
                "InCategoryOrdinal": 5,
                "IsProxy": false,
                "ItemTypeName": "System.Int32",
                "NeedsEditor": false,
                "PropertyId": "00000000-0000-0000-0000-000000000000",
                "PropertyName": "MinSuggestionLength",
                "PropertyPath": "/Settings/Model/MinSuggestionLength",
                "PropertyValue": "3",
                "TypeEditor": null

            }, {
                "CategoryName": "Misc",
                "CategoryNameSafe": "Misc",
                "ClientPropertyTypeName": "System.String",
                "ElementCssClass": null,
                "InCategoryOrdinal": 5,
                "IsProxy": false,
                "ItemTypeName": "System.String",
                "NeedsEditor": false,
                "PropertyId": "00000000-0000-0000-0000-000000000000",
                "PropertyName": "Language",
                "PropertyPath": "/Settings/Model/Language",
                "PropertyValue": "",
                "TypeEditor": null
            }, {
                "CategoryName": "Misc",
                "CategoryNameSafe": "Misc",
                "ClientPropertyTypeName": "System.String",
                "ElementCssClass": null,
                "InCategoryOrdinal": 5,
                "IsProxy": false,
                "ItemTypeName": "System.String",
                "NeedsEditor": false,
                "PropertyId": "00000000-0000-0000-0000-000000000000",
                "PropertyName": "CssClass",
                "PropertyPath": "/Settings/Model/CssClass",
                "PropertyValue": "",
                "TypeEditor": null
            }],
        "TotalCount": 15
    };

    var errorResponse = {
        Detail: 'Error'
    };

    var appPath = 'http://mysite.com:9999/myapp';

    describe('Controllers test.', function () {
        var $httpBackend, $rootScope, createController, propertyService, searchService;

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
            searchService = $injector.get("sfSearchServiceMock");

            createController = function () {
                return $controller('SimpleCtrl', {
                    '$scope': $rootScope, 'propertyService': propertyService, 'sfSearchService': searchService,
                });
            };
        }));

        it('[NPetrova] / The search indexes are populated correctly for the dropdown.', function () {
            $httpBackend.expectGET('/Sitefinity/Services/Pages/ControlPropertyService.svc/16fcc965-f18a-6d82-8924-ff0000fd783f/?pageId=a6f4c965-f18a-6d82-8924-ff0000fd783f')
                .respond(dataItems);

            var controller = createController();
            $httpBackend.flush();
            $rootScope.$digest();

            expect($rootScope.searchIndexes).toBeDefined();
            expect($rootScope.searchIndexes.length).toBe(1);
            expect($rootScope.hasSearchIndexes).toBe(true);
        });

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
