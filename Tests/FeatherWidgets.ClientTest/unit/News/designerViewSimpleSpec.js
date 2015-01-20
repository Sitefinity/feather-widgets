/* tests for the PropertyGridModule */
var sitefinity = { pageEditor: {} };
sitefinity.pageEditor.widgetContext = { AppPath: "/", HideSaveAllTranslations: true, Id: "16fcc965-f18a-6d82-8924-ff0000fd783f", MediaType: 0, PageId: "a6f4c965-f18a-6d82-8924-ff0000fd783f", PropertyServiceUrl: "/Sitefinity/Services/Pages/ControlPropertyService.svc/", PropertyValueCulture: null, url: "/Telerik.Sitefinity.Frontend/Designer/Master/News?package=SemanticUI" };


describe('News designer view tests.', function () {
    var dataItems = {
        "Context": null,
        "IsGeneric": false,
        "Items": [{
            "CategoryName": "Misc",
            "CategoryNameSafe": "Misc",
            "ClientPropertyTypeName": "System.Object",
            "ElementCssClass": null,
            "InCategoryOrdinal": 5,
            "IsProxy": true,
            "ItemTypeName": "System.Object",
            "NeedsEditor": false,
            "PropertyId": "acfcc965-f18a-6d82-8924-ff0000fd783f",
            "PropertyName": "Settings",
            "PropertyPath": "\/Settings",
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
            "PropertyId": "acfcc965-f18a-6d82-8924-ff0000fd783f",
            "PropertyName": "ListTemplateName",
            "PropertyPath": "\/Settings\/ListTemplateName",
            "PropertyValue": "NewsList",
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
            "PropertyId": "acfcc965-f18a-6d82-8924-ff0000fd783f",
            "PropertyName": "DetailTemplateName",
            "PropertyPath": "\/Settings\/DetailTemplateName",
            "PropertyValue": "DetailPage",
            "TypeEditor": null
        }, {
            "CategoryName": "Misc",
            "CategoryNameSafe": "Misc",
            "ClientPropertyTypeName": "System.Boolean",
            "ElementCssClass": null,
            "InCategoryOrdinal": 5,
            "IsProxy": false,
            "ItemTypeName": "System.Nullable`1[[System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]",
            "NeedsEditor": false,
            "PropertyId": "acfcc965-f18a-6d82-8924-ff0000fd783f",
            "PropertyName": "DisableCanonicalUrlMetaTag",
            "PropertyPath": "\/Settings\/DisableCanonicalUrlMetaTag",
            "PropertyValue": "",
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
            "PropertyId": "acfcc965-f18a-6d82-8924-ff0000fd783f",
            "PropertyName": "OpenInSamePage",
            "PropertyPath": "\/Settings\/OpenInSamePage",
            "PropertyValue": "True",
            "TypeEditor": null
        }, {
            "CategoryName": "Misc",
            "CategoryNameSafe": "Misc",
            "ClientPropertyTypeName": "System.Guid",
            "ElementCssClass": null,
            "InCategoryOrdinal": 5,
            "IsProxy": false,
            "ItemTypeName": "System.Guid",
            "NeedsEditor": false,
            "PropertyId": "acfcc965-f18a-6d82-8924-ff0000fd783f",
            "PropertyName": "DetailsPageId",
            "PropertyPath": "\/Settings\/DetailsPageId",
            "PropertyValue": "00000000-0000-0000-0000-000000000000",
            "TypeEditor": null
        }, {
            "CategoryName": "Misc",
            "CategoryNameSafe": "Misc",
            "ClientPropertyTypeName": "Telerik.Sitefinity.Frontend.News.Mvc.Models.INewsModel",
            "ElementCssClass": null,
            "InCategoryOrdinal": 5,
            "IsProxy": false,
            "ItemTypeName": "Telerik.Sitefinity.Frontend.News.Mvc.Models.INewsModel",
            "NeedsEditor": true,
            "PropertyId": "acfcc965-f18a-6d82-8924-ff0000fd783f",
            "PropertyName": "Model",
            "PropertyPath": "\/Settings\/Model",
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
            "PropertyId": "acfcc965-f18a-6d82-8924-ff0000fd783f",
            "PropertyName": "SerializedSelectedItemsIds",
            "PropertyPath": "\/Settings\/Model\/SerializedSelectedItemsIds",
            "PropertyValue": "[\"dbf0c965-f18a-6d82-8924-ff0000fd783f\"]",
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
            "PropertyId": "acfcc965-f18a-6d82-8924-ff0000fd783f",
            "PropertyName": "ListCssClass",
            "PropertyPath": "\/Settings\/Model\/ListCssClass",
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
            "PropertyId": "acfcc965-f18a-6d82-8924-ff0000fd783f",
            "PropertyName": "DetailCssClass",
            "PropertyPath": "\/Settings\/Model\/DetailCssClass",
            "PropertyValue": "",
            "TypeEditor": null
        }, {
            "CategoryName": "Misc",
            "CategoryNameSafe": "Misc",
            "ClientPropertyTypeName": "Telerik.Sitefinity.Frontend.News.Mvc.Models.NewsSelectionMode",
            "ElementCssClass": null,
            "InCategoryOrdinal": 5,
            "IsProxy": false,
            "ItemTypeName": "Telerik.Sitefinity.Frontend.News.Mvc.Models.NewsSelectionMode",
            "NeedsEditor": false,
            "PropertyId": "acfcc965-f18a-6d82-8924-ff0000fd783f",
            "PropertyName": "SelectionMode",
            "PropertyPath": "\/Settings\/Model\/SelectionMode",
            "PropertyValue": "SelectedItems",
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
            "PropertyId": "acfcc965-f18a-6d82-8924-ff0000fd783f",
            "PropertyName": "EnableSocialSharing",
            "PropertyPath": "\/Settings\/Model\/EnableSocialSharing",
            "PropertyValue": "False",
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
            "PropertyId": "acfcc965-f18a-6d82-8924-ff0000fd783f",
            "PropertyName": "ProviderName",
            "PropertyPath": "\/Settings\/Model\/ProviderName",
            "PropertyValue": "OpenAccessDataProvider",
            "TypeEditor": null
        }, {
            "CategoryName": "Misc",
            "CategoryNameSafe": "Misc",
            "ClientPropertyTypeName": "Telerik.Sitefinity.Frontend.News.Mvc.Models.ListDisplayMode",
            "ElementCssClass": null,
            "InCategoryOrdinal": 5,
            "IsProxy": false,
            "ItemTypeName": "Telerik.Sitefinity.Frontend.News.Mvc.Models.ListDisplayMode",
            "NeedsEditor": false,
            "PropertyId": "acfcc965-f18a-6d82-8924-ff0000fd783f",
            "PropertyName": "DisplayMode",
            "PropertyPath": "\/Settings\/Model\/DisplayMode",
            "PropertyValue": "Paging",
            "TypeEditor": null
        }, {
            "CategoryName": "Misc",
            "CategoryNameSafe": "Misc",
            "ClientPropertyTypeName": "System.Int32",
            "ElementCssClass": null,
            "InCategoryOrdinal": 5,
            "IsProxy": false,
            "ItemTypeName": "System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]",
            "NeedsEditor": false,
            "PropertyId": "acfcc965-f18a-6d82-8924-ff0000fd783f",
            "PropertyName": "ItemsPerPage",
            "PropertyPath": "\/Settings\/Model\/ItemsPerPage",
            "PropertyValue": "20",
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
            "PropertyId": "acfcc965-f18a-6d82-8924-ff0000fd783f",
            "PropertyName": "SortExpression",
            "PropertyPath": "\/Settings\/Model\/SortExpression",
            "PropertyValue": "PublicationDate DESC",
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
            "PropertyId": "acfcc965-f18a-6d82-8924-ff0000fd783f",
            "PropertyName": "SerializedTaxonomyFilter",
            "PropertyPath": "\/Settings\/Model\/SerializedTaxonomyFilter",
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
            "PropertyId": "acfcc965-f18a-6d82-8924-ff0000fd783f",
            "PropertyName": "SerializedSelectedTaxonomies",
            "PropertyPath": "\/Settings\/Model\/SerializedSelectedTaxonomies",
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
            "PropertyId": "acfcc965-f18a-6d82-8924-ff0000fd783f",
            "PropertyName": "SerializedAdditionalFilters",
            "PropertyPath": "\/Settings\/Model\/SerializedAdditionalFilters",
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
            "PropertyId": "acfcc965-f18a-6d82-8924-ff0000fd783f",
            "PropertyName": "FilterExpression",
            "PropertyPath": "\/Settings\/Model\/FilterExpression",
            "PropertyValue": "",
            "TypeEditor": null
        }],
        "TotalCount": 20
    };

    var errorResponse = {
        Detail: 'Error'
    };
   
    var appPath = 'http://mysite.com:9999/myapp';

    describe('Controllers test.', function () {
        var $httpBackend, $rootScope, createController, propertyService;

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

            var propertyService = $injector.get("propertyService");

            createController = function () {
                return $controller('SimpleCtrl', {
                    '$scope': $rootScope, 'propertyService': propertyService,
                });
            };
        }));

        describe('Tests with default data items.', function () {
            beforeEach(inject(function ($injector) {
                $httpBackend.expectGET('/Sitefinity/Services/Pages/ControlPropertyService.svc/16fcc965-f18a-6d82-8924-ff0000fd783f/')
                    .respond(dataItems);
            }));

            it('[EGaneva] / The selected items ids are populated correctly for the selector.', function () {
                var controller = createController();
                $httpBackend.flush();
                $rootScope.$digest();

                expect($rootScope.newsSelector.selectedItemsIds).toEqual(["dbf0c965-f18a-6d82-8924-ff0000fd783f"]);
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

                $httpBackend.expectGET('/Sitefinity/Services/Pages/ControlPropertyService.svc/16fcc965-f18a-6d82-8924-ff0000fd783f/')
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
                $httpBackend.expectGET('/Sitefinity/Services/Pages/ControlPropertyService.svc/16fcc965-f18a-6d82-8924-ff0000fd783f/')
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
