using System;
using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.DynamicContent.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.DynamicWidgets
{
    /// <summary>
    /// This is a test class with tests related to dynamic widgets in Content section.
    /// </summary>
    [TestFixture]
    [Description("This is a test class with tests related to dynamic widgets designer Content section.")]
    public class DynamicWidgetsDesignerContentTests
    {
        [FixtureSetUp]
        public void Setup()
        {
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Verify all items per page.")]
        public void DynamicWidgetsDesignerContent_VerifyAllFunctionality()
        {
            this.pageOperations = new PagesOperations();
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "DynamicPage";
            string pageTitlePrefix = testName + "Dynamic Page";
            string urlNamePrefix = testName + "dynamic-page";
            int index = 1;
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index);
            var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

            try
            {
                for (int i = 0; i < this.dynamicTitles.Length; i++)
                    ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(this.dynamicTitles[i], this.dynamicUrls[i]);

                dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

                var mvcProxy = new MvcWidgetProxy();
                mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
                var dynamicController = new DynamicContentController();
                dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
                dynamicController.Model.SelectionMode = SelectionMode.AllItems;
                dynamicController.Model.ProviderName = FeatherWidgets.TestUtilities.CommonOperations.DynamicModulesOperations.ProviderName;
                mvcProxy.Settings = new ControllerSettings(dynamicController);
                mvcProxy.WidgetName = WidgetName;

                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                string responseContent = PageInvoker.ExecuteWebRequest(url);

                for (int i = 0; i < this.dynamicTitles.Length; i++)
                    Assert.IsTrue(responseContent.Contains(this.dynamicTitles[i]), "The dynamic item with this title was not found!");
            }
            finally
            {
                this.pageOperations.DeletePages();
                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(dynamicCollection);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Taxonomies().DeleteTags(this.tagTitle);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), System.Diagnostics.CodeAnalysis.SuppressMessage("Telerik.Sitefinity", "SF1002:AvoidToListOnIEnumerable"), Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Verify selected items per page.")]
        public void DynamicWidgetsDesignerContent_VerifySelectedItemsFunctionality()
        {
            var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

            try
            {
                for (int i = 0; i < this.dynamicTitles.Length; i++)
                {
                    Guid itemId = ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(this.dynamicTitles[i], this.dynamicUrls[i]).Id;

                    dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

                    var mvcProxy = new MvcWidgetProxy();

                    mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
                    var dynamicController = new DynamicContentController();
                    dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
                    dynamicController.Model.SelectionMode = SelectionMode.SelectedItems;
                    dynamicController.Model.SerializedSelectedItemsIds = "[\"" + itemId.ToString() + "\"]";
                    dynamicController.Model.ProviderName = FeatherWidgets.TestUtilities.CommonOperations.DynamicModulesOperations.ProviderName;
                    mvcProxy.Settings = new ControllerSettings(dynamicController);
                    mvcProxy.WidgetName = WidgetName;

                    var modelItems = dynamicController.Model.CreateListViewModel(taxonFilter: null, page: 1);
                    var dynamicItems = modelItems.Items.ToList();
                    int itemsCount = dynamicItems.Count;

                    Assert.AreEqual(1, itemsCount, "The count of the dynamic item is not as expected");

                    string title = dynamicItems[0].Fields.Title;
                    Assert.IsTrue(title.Equals(this.dynamicTitles[i]), "The dynamic item with this title was not found!");
                }
            }
            finally
            {
                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(dynamicCollection);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Taxonomies().DeleteTags(this.tagTitle);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Telerik.Sitefinity", "SF1002:AvoidToListOnIEnumerable"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Verify dynaimc items by tag.")]
        public void DynamicWidgetsDesignerContent_VerifyDynamicItemsByPublicationDateLastOneDayFunctionality()
        {
            var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

            try
            {
                DateTime earlierPublicationDate = DateTime.UtcNow.AddDays(-10);
                string earlierPublishedItemTitle = this.dynamicTitles[0];
                Guid earlierPublishedItemId = Guid.Empty;
                for (int i = 0; i < this.dynamicTitles.Length; i++)
                {
                    if (i == 0)
                    {
                        earlierPublishedItemId = ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(this.dynamicTitles[i], this.dynamicUrls[i]).Id;
                    }
                    else
                    {
                        ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(this.dynamicTitles[i], this.dynamicUrls[i]);
                    }
                }

                var earlierPublishedItem = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles().First(i => i.Id == earlierPublishedItemId && i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master);
                ServerOperationsFeather.DynamicModulePressArticle().PublishPressArticleWithSpecificDate(earlierPublishedItem, earlierPublicationDate);

                var mvcProxy = new MvcWidgetProxy();
                mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
                var dynamicController = new DynamicContentController();
                dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
                dynamicController.Model.SelectionMode = SelectionMode.FilteredItems;
                dynamicController.Model.ProviderName = FeatherWidgets.TestUtilities.CommonOperations.DynamicModulesOperations.ProviderName;
                dynamicController.Model.SerializedAdditionalFilters = @"{
				""QueryItems"": [
					{
						""IsGroup"":true,
						""Ordinal"":0,
						""Join"":""AND"",
						""ItemPath"":""_0"",
						""Value"":null,
						""Condition"":null,
						""Name"":""PublicationDate""
					},
					{
						""IsGroup"":false,
						""Ordinal"":0,
						""Join"":""AND"",
						""ItemPath"":""_0_0"",
						""Value"":""DateTime.UtcNow.AddDays(-1.0)"",
						""Condition"": {
							""FieldName"":""PublicationDate"",
							""FieldType"":""System.DateTime"",
							""Operator"":"">""
						},
						""Name"":""PublicationDate.DateTime.UtcNow.AddDays(-1.0)""
					}
				]
			}";
                mvcProxy.Settings = new ControllerSettings(dynamicController);
                mvcProxy.WidgetName = WidgetName;

                var modelItems = dynamicController.Model.CreateListViewModel(taxonFilter: null, page: 1);
                var dynamicItems = modelItems.Items.ToList();
                int itemsCount = dynamicItems.Count;

                Assert.AreEqual(2, itemsCount, "The count of the dynamic item is not as expected");

                string title1 = dynamicItems[0].Fields.Title;
                Assert.IsTrue(this.dynamicTitles.Contains(title1), "The dynamic item with this title was not found!");
                Assert.IsFalse(title1.Equals(earlierPublishedItemTitle), "The dynamic item with this title was found!");

                string title2 = dynamicItems[1].Fields.Title;
                Assert.IsTrue(this.dynamicTitles.Contains(title2), "The dynamic item with this title was not found!");
                Assert.IsFalse(title2.Equals(earlierPublishedItemTitle), "The dynamic item with this title was found!");
            }
            finally
            {
                dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();
                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(dynamicCollection);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Taxonomies().DeleteTags(this.tagTitle);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Telerik.Sitefinity", "SF1002:AvoidToListOnIEnumerable"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Verify dynaimc items by tag.")]
        public void DynamicWidgetsDesignerContent_VerifyDynamicItemsByPublicationDateCustomRangeFunctionality()
        {
            var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

            try
            {
                DateTime publicationDate = new DateTime(2014, 10, 23, 12, 00, 00);

                for (int i = 0; i < this.dynamicTitles.Length; i++)
                {
                    Guid itemId = ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(this.dynamicTitles[i], this.dynamicUrls[i]).Id;

                    var masterItem = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles().First(item => item.Id == itemId && item.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master);
                    ServerOperationsFeather.DynamicModulePressArticle().PublishPressArticleWithSpecificDate(masterItem, publicationDate);

                    var itemsToDelete = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles().Where(item => item.Id != itemId && item.OriginalContentId != itemId).ToList();
                    ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(itemsToDelete);

                    var mvcProxy = new MvcWidgetProxy();
                    mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
                    var dynamicController = new DynamicContentController();
                    dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
                    dynamicController.Model.SelectionMode = SelectionMode.FilteredItems;
                    dynamicController.Model.ProviderName = FeatherWidgets.TestUtilities.CommonOperations.DynamicModulesOperations.ProviderName;
                    dynamicController.Model.SerializedAdditionalFilters = @"{
            ""QueryItems"":[
	            {
		            ""IsGroup"":true,
		            ""Ordinal"":0,
		            ""Join"":""AND"",
		            ""ItemPath"":""_0"",
		            ""Value"":null,
		            ""Condition"":null,
		            ""Name"":""PublicationDate""
	            },
	            {
		            ""IsGroup"":false,
		            ""Ordinal"":0,
		            ""Join"":""AND"",
		            ""ItemPath"":""_0_0"",
		            ""Value"":""Wed, 22 Oct 2014 21:00:00 GMT"",
		            ""Condition"":
			            {
				            ""FieldName"":""PublicationDate"",
				            ""FieldType"":""System.DateTime"",
				            ""Operator"":"">""
			            },
		            ""Name"":""PublicationDate.Wed, 22 Oct 2014 21:00:00 GMT""
	            },
	            {
		            ""IsGroup"":false,
		            ""Ordinal"":1,
		            ""Join"":""AND"",
		            ""ItemPath"":""_0_1"",
		            ""Value"":""Fri, 24 Oct 2014 21:00:00 GMT"",
		            ""Condition"":
			            {
				            ""FieldName"":""PublicationDate"",
				            ""FieldType"":""System.DateTime"",
				            ""Operator"":""<""
			            },
		            ""Name"":""PublicationDate.Fri, 24 Oct 2014 21:00:00 GMT""
	            }";

                    mvcProxy.Settings = new ControllerSettings(dynamicController);
                    mvcProxy.WidgetName = WidgetName;

                    var modelItems = dynamicController.Model.CreateListViewModel(taxonFilter: null, page: 1);
                    var dynamicItems = modelItems.Items.ToList();
                    int itemsCount = dynamicItems.Count;

                    Assert.AreEqual(1, itemsCount, "The count of the dynamic item is not as expected");

                    string title1 = dynamicItems[0].Fields.Title;
                    Assert.IsTrue(title1.Equals(this.dynamicTitles[i]), "The dynamic item with this title was not found!");

                    for (int j = 0; j < this.dynamicTitles.Length; j++)
                    {
                        if (j != i)
                        {
                            Assert.IsFalse(title1.Equals(this.dynamicTitles[j]), "The dynamic item with this title was found!");
                        }
                    }
                }
            }
            finally
            {
                dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();
                ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(dynamicCollection);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Taxonomies().DeleteTags(this.tagTitle);
            }
        }

        [FixtureTearDown]
        public void Teardown()
        {
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.ModuleBuilder().DeleteAllModules(string.Empty, TransactionName);
        }

        private Guid[] CreatePressArticleAndReturnTagId()
        {
            Guid[] taxonId = new Guid[3];

            for (int i = 0; i < 3; i++)
            {
                taxonId[i] = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Taxonomies().CreateFlatTaxon(Telerik.Sitefinity.TestUtilities.CommonOperations.TaxonomiesConstants.TagsTaxonomyId, this.tagTitle[i]);
                ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticle(this.dynamicTitles[i], this.dynamicUrls[i], taxonId[i], Guid.Empty);
            }

            return taxonId;
        }

        #region Fields and constants

        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressRelease.zip";
        private const string ResolveType = "Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle";
        private const string TransactionName = "Module Installations";
        private const string WidgetName = "PressArticle";
        private string[] dynamicTitles = { "Angel", "Boat", "Cat" };
        private string[] dynamicUrls = { "AngelUrl", "BoatUrl", "CatUrl" };
        private string[] tagTitle = { "Tag 0", "Tag 1", "Tag 2" };
        private PagesOperations pageOperations;

        #endregion
    }
}
