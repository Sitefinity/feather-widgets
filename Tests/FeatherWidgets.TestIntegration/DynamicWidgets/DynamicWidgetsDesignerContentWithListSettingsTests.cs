using System;
using System.Collections.Generic;
using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Frontend.DynamicContent.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using TaxonomiesOperationsContext = Telerik.Sitefinity.TestUtilities.CommonOperations.TaxonomiesOperations;

namespace FeatherWidgets.TestIntegration.DynamicWidgets
{
    /// <summary>
    /// This is a test class with tests related to dynamic widgets in Content section.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), TestFixture]
    [Description("This is a test class with tests related to dynamic widgets designer Content section.")]
    public class DynamicWidgetsDesignerContentWithListSettingsTests
    {
        [FixtureSetUp]
        public void Setup()
        {
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);
            ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();
        }

        /// <summary>
        /// Dynamics the widgets_ verify selected items functionality with sort descending.
        /// </summary>
        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.Team7)]
        public void DynamicWidgets_VerifySelectedItemsFunctionalityWithSortDescending()
        {
            string sortExpession = "Title DESC";
            string[] selectedDynamicTitles = { "Title2", "Title7", "Title5" };
            string[] expectedDynamicTitles = { "Title7", "Title5", "Title2" };
 
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem("Title" + i, "Title" + i + "Url");
                }

                var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles(); 

                var dynamicController = new DynamicContentController();
                dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
                dynamicController.Model.SelectionMode = SelectionMode.SelectedItems;
                dynamicController.Model.SortExpression = sortExpession;

                var selectedDynamicItems = new DynamicContent[3];

                for (int i = 0; i < selectedDynamicTitles.Count(); i++)
                {
                    selectedDynamicItems[i] = dynamicCollection.FirstOrDefault<DynamicContent>(n => n.UrlName == (selectedDynamicTitles[i] + "Url"));
                }

                //// SerializedSelectedItemsIds string should appear in the following format: "[\"ca782d6b-9e3d-6f9e-ae78-ff00006062c4\",\"66782d6b-9e3d-6f9e-ae78-ff00006062c4\"]"
                dynamicController.Model.SerializedSelectedItemsIds =
                                                                    "[\"" + selectedDynamicItems[0].Id.ToString() + "\"," +
                                                                    "\"" + selectedDynamicItems[1].Id.ToString() + "\"," +
                                                                    "\"" + selectedDynamicItems[2].Id.ToString() + "\"]";

                var modelItems = dynamicController.Model.CreateListViewModel(taxonFilter: null, page: 1);
                var dynamicItems = modelItems.Items.ToList();
                int itemsCount = dynamicItems.Count;

                for (int i = 0; i < itemsCount; i++)
                {
                    Assert.IsTrue(dynamicItems[i].Fields.Title.Equals(expectedDynamicTitles[i]), "The item with this title was not found!");
                }

                dynamicController.Model.SelectionMode = SelectionMode.AllItems;

                modelItems = dynamicController.Model.CreateListViewModel(taxonFilter: null, page: 1);
                dynamicItems = modelItems.Items.ToList();

                int lastIndex = 9;
                for (int i = 0; i < 10; i++)
                {
                    Assert.IsTrue(dynamicItems[i].Fields.Title.Equals(this.dynamicTitle + lastIndex), "The item with this title was not found!");
                    lastIndex--;
                }
            }
            finally
            {
                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles());           
            }
        }

        /// <summary>
        /// Dynamics the widgets_ verify selected items functionality with paging.
        /// </summary>
        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.Team7)]
        public void DynamicWidgets_VerifySelectedItemsFunctionalityWithPaging()
        { 
            this.pageOperations = new PagesOperations();
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "DynamicPage";
            string pageTitlePrefix = testName + "Dynamic Page";
            string urlNamePrefix = testName + "dynamic-page";
            int index = 1;
            string index2 = "/2";
            string index3 = "/3";
            int itemsPerPage = 3;
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index);
            string url2 = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index + index2);
            string url3 = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index + index3);

            try
            {
                for (int i = 0; i < 20; i++)
                {
                    ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem("Title" + i, "Title" + i + "Url");
                }

                var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

                var dynamicController = new DynamicContentController();
                dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
                dynamicController.Model.SelectionMode = SelectionMode.SelectedItems;
                string[] selectedDynamicTitles = { "Title7", "Title15", "Title11", "Title3", "Title5", "Title8", "Title2", "Title16", "Title6" };
                var selectedDynamicItems = new DynamicContent[9];

                var mvcProxy = new MvcWidgetProxy();
                mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
                mvcProxy.Settings = new ControllerSettings(dynamicController);
                mvcProxy.WidgetName = WidgetName;
                dynamicController.Model.ItemsPerPage = itemsPerPage;
                dynamicController.Model.SortExpression = AsSetManuallySortingOption;

                for (int i = 0; i < selectedDynamicTitles.Count(); i++)
                {
                    selectedDynamicItems[i] = dynamicCollection.FirstOrDefault<DynamicContent>(n => n.UrlName == (selectedDynamicTitles[i] + "Url"));
                }

                //// SerializedSelectedItemsIds string should appear in the following format: "[\"ca782d6b-9e3d-6f9e-ae78-ff00006062c4\",\"66782d6b-9e3d-6f9e-ae78-ff00006062c4\"]"
                dynamicController.Model.SerializedSelectedItemsIds =
                                                                    "[\"" + selectedDynamicItems[0].Id.ToString() + "\"," +
                                                                    "\"" + selectedDynamicItems[1].Id.ToString() + "\"," +
                                                                    "\"" + selectedDynamicItems[2].Id.ToString() + "\"," +
                                                                    "\"" + selectedDynamicItems[3].Id.ToString() + "\"," +
                                                                    "\"" + selectedDynamicItems[4].Id.ToString() + "\"," +
                                                                    "\"" + selectedDynamicItems[5].Id.ToString() + "\"," +
                                                                    "\"" + selectedDynamicItems[6].Id.ToString() + "\"," +
                                                                    "\"" + selectedDynamicItems[7].Id.ToString() + "\"," +
                                                                    "\"" + selectedDynamicItems[8].Id.ToString() + "\"]";

                this.VerifyCorrectItemsOnPages(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index, url, url2, url3, selectedDynamicTitles);
            }
            finally
            {
                this.pageOperations.DeletePages();
                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles());
            }
        }
       
        /// <summary>
        /// Dynamics the widgets_ verify selected items functionality with use limit.
        /// </summary>
        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.Team7)]
        public void DynamicWidgets_VerifySelectedItemsFunctionalityWithUseLimit()
        {
            this.pageOperations = new PagesOperations();
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "DynamicPage";
            string pageTitlePrefix = testName + "Dynamic Page";
            string urlNamePrefix = testName + "dynamic-page";
            int index = 1;
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index);

            try
            {
                for (int i = 0; i < 20; i++)
                {
                    ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem("Title" + i, "Title" + i + "Url");
                }

                var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

                var dynamicController = new DynamicContentController();
                dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
                dynamicController.Model.SelectionMode = SelectionMode.SelectedItems;
                dynamicController.Model.DisplayMode = ListDisplayMode.Limit;
                dynamicController.Model.ItemsPerPage = 5;
                dynamicController.Model.SortExpression = AsSetManuallySortingOption;

                var mvcProxy = new MvcWidgetProxy();
                mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
                mvcProxy.Settings = new ControllerSettings(dynamicController);
                mvcProxy.WidgetName = WidgetName;

                string[] selectedDynamicTitles = { "Title7", "Title15", "Title11", "Title3", "Title5", "Title8", "Title2", "Title16", "Title6" };
                var selectedDynamicItems = new DynamicContent[9];

                for (int i = 0; i < selectedDynamicTitles.Count(); i++)
                {
                    selectedDynamicItems[i] = dynamicCollection.FirstOrDefault<DynamicContent>(n => n.UrlName == (selectedDynamicTitles[i] + "Url"));
                }

                //// SerializedSelectedItemsIds string should appear in the following format: "[\"ca782d6b-9e3d-6f9e-ae78-ff00006062c4\",\"66782d6b-9e3d-6f9e-ae78-ff00006062c4\"]"
                dynamicController.Model.SerializedSelectedItemsIds =
                                                                    "[\"" + selectedDynamicItems[0].Id.ToString() + "\"," +
                                                                    "\"" + selectedDynamicItems[1].Id.ToString() + "\"," +
                                                                    "\"" + selectedDynamicItems[2].Id.ToString() + "\"," +
                                                                    "\"" + selectedDynamicItems[3].Id.ToString() + "\"," +
                                                                    "\"" + selectedDynamicItems[4].Id.ToString() + "\"," +
                                                                    "\"" + selectedDynamicItems[5].Id.ToString() + "\"," +
                                                                    "\"" + selectedDynamicItems[6].Id.ToString() + "\"," +
                                                                    "\"" + selectedDynamicItems[7].Id.ToString() + "\"," +
                                                                    "\"" + selectedDynamicItems[8].Id.ToString() + "\"]";

                this.VerifyCorrectItemsOnPageWithUseLimitsOption(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index, url, selectedDynamicTitles);
            }
            finally
            {
                this.pageOperations.DeletePages();
                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles());
            }
        }

        /// <summary>
        /// Dynamics the widgets_ verify selected items functionality with no limit.
        /// </summary>
        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.Team7)]
        public void DynamicWidgets_VerifySelectedItemsFunctionalityWithNoLimit()
        {
            this.pageOperations = new PagesOperations();
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "DynamicPage";
            string pageTitlePrefix = testName + "Dynamic Page";
            string urlNamePrefix = testName + "dynamic-page";
            int index = 1;
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index);

            string[] itemsNames = new string[25];

            try
            {
                for (int i = 0; i < 25; i++)
                {
                    ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem("Title" + i, "Title" + i + "Url");
                    itemsNames[i] = "Title" + i;
                }

                var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

                var dynamicController = new DynamicContentController();
                dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
                dynamicController.Model.SelectionMode = SelectionMode.SelectedItems;
                dynamicController.Model.DisplayMode = ListDisplayMode.All;

                var mvcProxy = new MvcWidgetProxy();
                mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
                mvcProxy.Settings = new ControllerSettings(dynamicController);
                mvcProxy.WidgetName = WidgetName;

                string[] selectedDynamicTitles = { "Title7", "Title15", "Title11", "Title3", "Title5", "Title8", "Title2", "Title16", "Title6" };
                var selectedDynamicItems = new DynamicContent[9];

                for (int i = 0; i < selectedDynamicTitles.Count(); i++)
                {
                    selectedDynamicItems[i] = dynamicCollection.FirstOrDefault<DynamicContent>(n => n.UrlName == (selectedDynamicTitles[i] + "Url"));
                }

                //// SerializedSelectedItemsIds string should appear in the following format: "[\"ca782d6b-9e3d-6f9e-ae78-ff00006062c4\",\"66782d6b-9e3d-6f9e-ae78-ff00006062c4\"]"
                dynamicController.Model.SerializedSelectedItemsIds =
                                                                    "[\"" + selectedDynamicItems[0].Id.ToString() + "\"," +
                                                                    "\"" + selectedDynamicItems[1].Id.ToString() + "\"," +
                                                                    "\"" + selectedDynamicItems[2].Id.ToString() + "\"," +
                                                                    "\"" + selectedDynamicItems[3].Id.ToString() + "\"," +
                                                                    "\"" + selectedDynamicItems[4].Id.ToString() + "\"," +
                                                                    "\"" + selectedDynamicItems[5].Id.ToString() + "\"," +
                                                                    "\"" + selectedDynamicItems[6].Id.ToString() + "\"," +
                                                                    "\"" + selectedDynamicItems[7].Id.ToString() + "\"," +
                                                                    "\"" + selectedDynamicItems[8].Id.ToString() + "\"]";

                this.VerifyCorrectItemsOnPageWithNoLimitsOption(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index, url, selectedDynamicTitles);

                dynamicController.Model.SelectionMode = SelectionMode.AllItems;

                this.VerifyCorrectItemsOnPageWithNoLimitsOption(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index, url, itemsNames);
            }
            finally
            {
                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles());
            }
        }

        /// <summary>
        /// Dynamics the widgets designer content_ verify dynamic items by category functionality.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Telerik.Sitefinity", "SF1002:AvoidToListOnIEnumerable"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.Team7)]
        [Description("Verifies dynamic items by category.")]
        public void DynamicWidgetsDesignerContent_VerifyDynamicItemsByCategoryFunctionality()
        {
            string[] categoryTitles = { "Category 0", "Category 1", "Category 2" };
            try
            {
                TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();
                var taxonId = this.CreatePressArticleAndReturnCategoryId(categoryTitles);

                var dynamicController = new DynamicContentController();
                dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
                dynamicController.Model.SelectionMode = SelectionMode.FilteredItems;

                for (int i = 0; i < taxonId.Length; i++)
                {
                    var category = taxonomyManager.GetTaxa<HierarchicalTaxon>().Where(t => t.Id == taxonId[i]).FirstOrDefault();

                    var modelItems = dynamicController.Model.CreateListViewModel(taxonFilter: category, page: 1);
                    var dynamicItems = modelItems.Items.ToList();
                    int itemsCount = dynamicItems.Count;

                    Assert.AreEqual(1, itemsCount, "The count of the dynamic item is not as expected");

                    string title = dynamicItems[0].Fields.Title;
                    Assert.IsTrue(title.Equals(this.dynamicTitles[i]), "The dynamic item with this title was not found!");
                }
            }
            finally
            {
                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles());
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Taxonomies().DeleteCategories(categoryTitles);
            }
        }

        /// <summary>
        /// Dynamics the widgets_ select by tag and sort functionality.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Telerik.Sitefinity", "SF1002:AvoidToListOnIEnumerable"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.Team7)]
        public void DynamicWidgets_SelectByTagAndSortFunctionality()
        {
            int tagsCount = 3;
            Guid[] taxonId = new Guid[tagsCount];
            string sortExpession = "Title ASC";
            string[] itemsTitles = { "Boat", "Cat", "Angel", "Kitty", "Dog" };
            string[] urls = { "AngelUrl", "BoatUrl", "CatUrl", "KittyUrl", "DogUrl" };
            string[] sortedTitles = { "Angel", "Boat", "Cat" };         

            var dynamicController = new DynamicContentController();
            dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
            dynamicController.Model.SortExpression = sortExpession;

            try
            {
                for (int i = 0; i < tagsCount; i++)
                {
                    taxonId[i] = this.serverOperationsTaxonomies.CreateFlatTaxon(Telerik.Sitefinity.TestUtilities.CommonOperations.TaxonomiesConstants.TagsTaxonomyId, this.tagTitles[i]);
                }

                for (int i = 0; i < itemsTitles.Count(); i++)
                {
                    if (i <= 2)
                    {
                        ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticle(itemsTitles[i], urls[i], taxonId[0], Guid.Empty);                       
                    }
                    else
                    {
                        ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticle(itemsTitles[i], urls[i], taxonId[i - 2], Guid.Empty);
                    }
                }

                TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();
                    
                for (int i = 0; i < tagsCount; i++)
                {
                    var tag = taxonomyManager.GetTaxa<FlatTaxon>().Where(t => t.Id == taxonId[i]).FirstOrDefault();
                    var modelItems = dynamicController.Model.CreateListViewModel(taxonFilter: tag, page: 1);
                    var dynamicItems = modelItems.Items.ToList();
                    int itemsCount = dynamicItems.Count;     
   
                    if (i == 0)
                    {
                        for (int j = 0; j < itemsCount; j++)
                            Assert.IsTrue(dynamicItems[j].Fields.Title.Equals(sortedTitles[j], StringComparison.CurrentCulture), "The item with this title was not found!");
                    }
                    else
                    {
                        for (int j = 0; j < itemsCount; j++)
                            Assert.IsTrue(dynamicItems[j].Fields.Title.Equals(itemsTitles[i + 2], StringComparison.CurrentCulture), "The item with this title was not found!");
                    }
                }
            }
            finally
            {
                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles());
                this.serverOperationsTaxonomies.DeleteTags(this.tagTitles);
            }
        }
   
        /// <summary>
        /// Dynamic widget_ select by category functionality and paging.
        /// </summary>
        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.Team7)]
        public void DynamicWidgets_SelectByCategoryFunctionalityAndPaging()
        {
            var dynamicController = new DynamicContentController();
            dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
            dynamicController.Model.DisplayMode = ListDisplayMode.Paging;
            int itemsPerPage = 3;
            dynamicController.Model.ItemsPerPage = itemsPerPage;
            string categoryTitle = "Category";
            string[] itemsTitles = { "Boat", "Cat", "Angel", "Kitty", "Dog" };
            string[] urls = { "AngelUrl", "BoatUrl", "CatUrl", "KittyUrl", "DogUrl" };

            try
            {
                this.serverOperationsTaxonomies.CreateCategory(categoryTitle + "0");
                this.serverOperationsTaxonomies.CreateCategory(categoryTitle + "1", categoryTitle + "0");

                TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();
                var category0 = taxonomyManager.GetTaxa<HierarchicalTaxon>().SingleOrDefault(t => t.Title == categoryTitle + "0");
                var category1 = taxonomyManager.GetTaxa<HierarchicalTaxon>().SingleOrDefault(t => t.Title == categoryTitle + "1");

                for (int i = 0; i < itemsTitles.Count(); i++)
                {
                    if (i == 0)
                    {
                        ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticle(itemsTitles[i], urls[i], Guid.Empty, category0.Id); 
                    }
                    else if (i > 0 && i <= 3)
                    {
                        ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticle(itemsTitles[i], urls[i], Guid.Empty, category0.Id);
                        ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticle(itemsTitles[i], urls[i], Guid.Empty, category1.Id); 
                    }
                    else
                    {
                        ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticle(itemsTitles[i], urls[i], Guid.Empty, category1.Id);
                    }
                }

                this.VerifyCorrectItemsOnPageWithCategoryFilterAndPaging(category0, category1, dynamicController, itemsTitles);
            }
            finally
            {
                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles());
                this.serverOperationsTaxonomies.DeleteCategories("Category0", "Category1");
            }
        }

        /// <summary>
        /// Dynamics the widgets_ select by category functionality and limits.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Telerik.Sitefinity", "SF1002:AvoidToListOnIEnumerable"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.Team7)]
        public void DynamicWidgets_SelectByCategoryFunctionalityAndLimits()
        {
            var dynamicController = new DynamicContentController();
            dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
            dynamicController.Model.DisplayMode = ListDisplayMode.Limit;
            int itemsPerPage = 3;
            dynamicController.Model.ItemsPerPage = itemsPerPage;
            string categoryTitle = "Category";
            string[] itemsTitles = { "Boat", "Cat", "Angel", "Kitty", "Dog" };
            string[] urls = { "AngelUrl", "BoatUrl", "CatUrl", "KittyUrl", "DogUrl" };

            try
            {
                this.serverOperationsTaxonomies.CreateCategory(categoryTitle + "0");
                this.serverOperationsTaxonomies.CreateCategory(categoryTitle + "1", categoryTitle + "0");

                TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();
                var category0 = taxonomyManager.GetTaxa<HierarchicalTaxon>().SingleOrDefault(t => t.Title == categoryTitle + "0");
                var category1 = taxonomyManager.GetTaxa<HierarchicalTaxon>().SingleOrDefault(t => t.Title == categoryTitle + "1");

                for (int i = 0; i < itemsTitles.Count(); i++)
                {
                    if (i <= 3)
                    {
                        ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticle(itemsTitles[i], urls[i], Guid.Empty, category0.Id); 
                    }
                    else
                    {
                        ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticle(itemsTitles[i], urls[i], Guid.Empty, category1.Id); 
                    }
                }

                var modelItems = dynamicController.Model.CreateListViewModel(taxonFilter: category0, page: 1);
                var dynamicItems = modelItems.Items.ToList();
                int itemsCount = dynamicItems.Count;

                Assert.IsTrue(itemsCount.Equals(3), "Number of items is not correct");
                for (int i = 0; i <= 2; i++)
                {
                    Assert.IsTrue(dynamicItems[i].Fields.Title.Equals(itemsTitles[3 - i], StringComparison.CurrentCulture), "The items with this title was found!");
                }

                modelItems = dynamicController.Model.CreateListViewModel(taxonFilter: category1, page: 1);
                dynamicItems = modelItems.Items.ToList();
                itemsCount = dynamicItems.Count;

                Assert.IsTrue(itemsCount.Equals(1), "Number of items is not correct");
                Assert.IsTrue(dynamicItems[0].Fields.Title.Equals(itemsTitles[4], StringComparison.CurrentCulture), "The items with this title was found!");

                dynamicController.Model.DisplayMode = ListDisplayMode.All;

                modelItems = dynamicController.Model.CreateListViewModel(taxonFilter: category0, page: 1);
                dynamicItems = modelItems.Items.ToList();
                itemsCount = dynamicItems.Count;

                Assert.IsTrue(itemsCount.Equals(4), "Number of items is not correct");
                for (int i = 0; i <= 3; i++)
                {
                    Assert.IsTrue(dynamicItems[i].Fields.Title.Equals(itemsTitles[3 - i], StringComparison.CurrentCulture), "The items with this title was found!");
                }

                modelItems = dynamicController.Model.CreateListViewModel(taxonFilter: category1, page: 1);
                dynamicItems = modelItems.Items.ToList();
                itemsCount = dynamicItems.Count;

                Assert.IsTrue(itemsCount.Equals(1), "Number of items is not correct");
                Assert.IsTrue(dynamicItems[0].Fields.Title.Equals(itemsTitles[4], StringComparison.CurrentCulture), "The items with this title was found!");
            }
            finally
            {
                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles());
                this.serverOperationsTaxonomies.DeleteCategories("Category0", "Category1");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Telerik.Sitefinity", "SF1002:AvoidToListOnIEnumerable"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.Team7)]
        [Description("Verifies dynamic items by tag.")]
        public void DynamicWidgetsDesignerContent_VerifyDynamicItemsByTagFunctionality()
        {
            try
            {
                TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();
                var taxonId = this.CreatePressArticleAndReturnTagId(this.tagTitles);
              
                var dynamicController = new DynamicContentController();
                dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);             

                for (int i = 0; i < taxonId.Length; i++)
                {
                    var tag = taxonomyManager.GetTaxa<FlatTaxon>().Where(t => t.Id == taxonId[i]).FirstOrDefault();

                    var modelItems = dynamicController.Model.CreateListViewModel(taxonFilter: tag, page: 1);
                    var dynamicItems = modelItems.Items.ToList();
                    int itemsCount = dynamicItems.Count;

                    Assert.AreEqual(1, itemsCount, "The count of the dynamic item is not as expected");

                    string title = dynamicItems[0].Fields.Title;
                    Assert.IsTrue(title.Equals(this.dynamicTitles[i]), "The dynamic item with this title was not found!");
                }
            }
            finally
            {
                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles());
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Taxonomies().DeleteTags(this.tagTitles);
            }
        }

         /// <summary>
        /// Verifies dynamic items sorted by a valid As set in Advanced mode option.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.IndexOf(System.String)"), Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.Team7)]
        [Description("Verifies dynamic items sorted by a valid As set in Advanced mode option.")]
        public void DynamicWidgetDesignerContent_VerifyValidSortingOptionAsSetInAdvancedMode()
        {
            string sortExpession = "PublishedBy ASC";
            var itemsCount = 5;
            string[] publishers = { "Ivan", "George", "Steve", "Ana", "Tom" };
            string[] expectedSortedItemsTitles = { "Title3", "Title1", "Title0", "Title2", "Title4" };

            try
            {
                var dynamicController = new DynamicContentController();
                dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
                dynamicController.Model.SortExpression = sortExpession;

                for (int i = 0; i < itemsCount; i++)
                    ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticle("Title" + i, "Title" + i + "Url", publishers[i]);

                var items = dynamicController.Model.CreateListViewModel(null, 1).Items.ToArray();

                for (int i = 0; i < itemsCount; i++)
                {
                    Assert.AreEqual(expectedSortedItemsTitles[i], items[i].Fields.Title, "The dynamic item with this title was not found!");
                }
            }
            finally
            {
                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles());
            }
        }

        /// <summary>
        /// Verifies dynamic items sorted by an invalid As set in Advanced mode option.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.IndexOf(System.String)"), Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.Team7)]
        [Description("Verifies dynamic items sorted by an invalid As set in Advanced mode option.")]
        public void DynamicWidgetDesignerContent_VerifyInvalidSortingOptionAsSetInAdvancedMode()
        {
            string sortExpession = "InvalidSortingExpression";
            var itemsCount = 5;
            string[] publishers = { "Ivan", "George", "Steve", "Ana", "Tom" };
            string[] expectedSortedItemsTitles = { "Title4", "Title3", "Title2", "Title1", "Title0" };

            try
            {
                var dynamicController = new DynamicContentController();
                dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
                dynamicController.Model.SortExpression = sortExpession;

                for (int i = 0; i < itemsCount; i++)
                    ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticle("Title" + i, "Title" + i + "Url", publishers[i]);

                var items = dynamicController.Model.CreateListViewModel(null, 1).Items.ToArray();

                for (int i = 0; i < itemsCount; i++)
                {
                    Assert.AreEqual(expectedSortedItemsTitles[i], items[i].Fields.Title, "The dynamic item with this title was not found!");
                }
            }
            finally
            {
                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles());
            }
        }

        [FixtureTearDown]
        public void Teardown()
        {
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.ModuleBuilder().DeleteAllModules(string.Empty, TransactionName);
        }

        private Guid[] CreatePressArticleAndReturnTagId(string[] titles)
        {
            int count = titles.Count();
            Guid[] taxonId = new Guid[count];

            for (int i = 0; i < count; i++)
            {
                taxonId[i] = this.serverOperationsTaxonomies.CreateFlatTaxon(Telerik.Sitefinity.TestUtilities.CommonOperations.TaxonomiesConstants.TagsTaxonomyId, titles[i]);
                ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticle(this.dynamicTitles[i], this.dynamicUrls[i], taxonId[i], Guid.Empty);
            }

            return taxonId;
        }

        private Guid[] CreatePressArticleAndReturnCategoryId(string[] titles)
        {
            int count = titles.Count();
            Guid[] taxonId = new Guid[count];

            for (int i = 0; i < count; i++)
            {
                taxonId[i] = this.serverOperationsTaxonomies.CreateHierarchicalTaxon(Telerik.Sitefinity.TestUtilities.CommonOperations.TaxonomiesConstants.CategoriesTaxonomyId, titles[i]);
                ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticle(this.dynamicTitles[i], this.dynamicUrls[i], Guid.Empty, taxonId[i]);
            }

            return taxonId;
        }

        private void VerifyCorrectItemsOnPages(MvcControllerProxy mvcProxy, string pageNamePrefix, string pageTitlePrefix, string urlNamePrefix, int index, string url, string url2, string url3, string[] selectedDynamicTitles)
        {
            this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

            string responseContent = PageInvoker.ExecuteWebRequest(url);
            string responseContent2 = PageInvoker.ExecuteWebRequest(url2);
            string responseContent3 = PageInvoker.ExecuteWebRequest(url3);

            for (int i = 0; i < selectedDynamicTitles.Count(); i++)
            {
                if (i <= 2)
                {
                    Assert.IsTrue(responseContent.Contains(selectedDynamicTitles[i]), "The items with this title was not found!");
                    Assert.IsFalse(responseContent2.Contains(selectedDynamicTitles[i]), "The items with this title was found!");
                    Assert.IsFalse(responseContent3.Contains(selectedDynamicTitles[i]), "The items with this title was found!");
                }
                else if (i > 2 && i <= selectedDynamicTitles.Count() - 4)
                {
                    Assert.IsTrue(responseContent2.Contains(selectedDynamicTitles[i]), "The items with this title was not found!");
                    Assert.IsFalse(responseContent.Contains(selectedDynamicTitles[i]), "The items with this title was found!");
                    Assert.IsFalse(responseContent3.Contains(selectedDynamicTitles[i]), "The items with this title was found!");
                }
                else
                {
                    Assert.IsTrue(responseContent3.Contains(selectedDynamicTitles[i]), "The items with this title was not found!");
                    Assert.IsFalse(responseContent.Contains(selectedDynamicTitles[i]), "The items with this title was found!");
                    Assert.IsFalse(responseContent2.Contains(selectedDynamicTitles[i]), "The items with this title was found!");
                }
            }
        }

        private void VerifyCorrectItemsOnPageWithUseLimitsOption(MvcWidgetProxy mvcProxy, string pageNamePrefix, string pageTitlePrefix, string urlNamePrefix, int index, string url, string[] selectedItemsTitles)
        {
            this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

            string responseContent = PageInvoker.ExecuteWebRequest(url);

            for (int i = 0; i < selectedItemsTitles.Count(); i++)
            {
                if (i <= 4)
                {
                    Assert.IsTrue(responseContent.Contains(selectedItemsTitles[i]), "The items with this title was not found!");
                }
                else
                {
                    Assert.IsFalse(responseContent.Contains(selectedItemsTitles[i]), "The items with this title was found!");
                }
            }
        }

        private void VerifyCorrectItemsOnPageWithNoLimitsOption(MvcWidgetProxy mvcProxy, string pageNamePrefix, string pageTitlePrefix, string urlNamePrefix, int index, string url, string[] selectedItemsTitles)
        {
            this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

            string responseContent = PageInvoker.ExecuteWebRequest(url);

            for (int i = 0; i < selectedItemsTitles.Count(); i++)
            {
                Assert.IsTrue(responseContent.Contains(selectedItemsTitles[i]), "The items with this title was not found!");
            }

            this.pageOperations.DeletePages();
        }

        private void VerifyCorrectItemsOnPageWithCategoryFilterAndPaging(HierarchicalTaxon category0, HierarchicalTaxon category1, DynamicContentController dynamicController, string[] itemsTitles)
        {
            var modelItems = dynamicController.Model.CreateListViewModel(taxonFilter: category0, page: 1);
            var dynamicItems = modelItems.Items.ToList();
            int itemsCount = dynamicItems.Count;

            Assert.IsTrue(itemsCount.Equals(3), "Number of items is not correct");
            for (int i = 0; i <= 2; i++)
            {
                Assert.IsTrue(dynamicItems[i].Fields.Title.Equals(itemsTitles[3 - i], StringComparison.CurrentCulture), "The items with this title was found!");
            }

            modelItems = dynamicController.Model.CreateListViewModel(taxonFilter: category0, page: 2);
            dynamicItems = modelItems.Items.ToList();
            itemsCount = dynamicItems.Count;

            Assert.IsTrue(itemsCount.Equals(1), "Number of items is not correct");
            Assert.IsTrue(dynamicItems[0].Fields.Title.Equals(itemsTitles[0], StringComparison.CurrentCulture), "The items with this title was found!");

            modelItems = dynamicController.Model.CreateListViewModel(taxonFilter: category1, page: 1);
            dynamicItems = modelItems.Items.ToList();
            itemsCount = dynamicItems.Count;

            Assert.IsTrue(itemsCount.Equals(3), "Number of items is not correct");
            for (int i = 0; i <= 2; i++)
            {
                Assert.IsTrue(dynamicItems[i].Fields.Title.Equals(itemsTitles[4 - i], StringComparison.CurrentCulture), "The items with this title was found!");
            }

            modelItems = dynamicController.Model.CreateListViewModel(taxonFilter: category1, page: 2);
            dynamicItems = modelItems.Items.ToList();
            itemsCount = dynamicItems.Count;

            Assert.IsTrue(itemsCount.Equals(1), "Number of items is not correct");
            Assert.IsTrue(dynamicItems[0].Fields.Title.Equals(itemsTitles[1], StringComparison.CurrentCulture), "The items with this title was found!");
        }

        #region Fields and constants

        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressReleaseWithCategoriesField.zip";
        private const string ResolveType = "Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle";
        private const string TransactionName = "Module Installations";
        private const string WidgetName = "PressArticle";
        private const string AsSetManuallySortingOption = "AsSetManually";
        private readonly string dynamicTitle = "Title";
        private readonly string[] dynamicTitles = { "Angel", "Boat", "Cat" };
        private readonly string[] dynamicUrls = { "AngelUrl", "BoatUrl", "CatUrl" };
        private readonly string[] tagTitles = { "Tag 0", "Tag 1", "Tag 2" };
        private PagesOperations pageOperations;
        private readonly TaxonomiesOperationsContext serverOperationsTaxonomies = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Taxonomies();
        
        #endregion
    }
}