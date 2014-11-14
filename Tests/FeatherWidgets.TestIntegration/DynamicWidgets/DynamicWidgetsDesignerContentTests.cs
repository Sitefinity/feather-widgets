using System;
using System.Linq;
using DynamicContent.Mvc.Controllers;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
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
            ServerOperationsFeather.DynamicModules().ImportModule(ModuleResource);
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.ModuleBuilder().ActivateModule(ModuleName, string.Empty, "Module Installations");
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author("FeatherTeam")]
        [Description("Verify all items per page.")]
        public void DynamicWidgetsDesignerContent_VerifyAllFunctionality()
        {
            this.CreatePressArticleAndReturnTagId();
            var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

            this.pageOperations = new PagesOperations();
            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "DynamicPage";
            string pageTitlePrefix = testName + "Dynamic Page";
            string urlNamePrefix = testName + "dynamic-page";
            int index = 1;
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index);

            var mvcProxy = new MvcWidgetProxy();
            mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
            var dynamicController = new DynamicContentController();
            dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
            dynamicController.Model.SelectionMode = SelectionMode.AllItems;
            mvcProxy.Settings = new ControllerSettings(dynamicController);
            mvcProxy.WidgetName = WidgetName;

            try
            {
                this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                string responseContent = PageInvoker.ExecuteWebRequest(url);

                for (int i = 0; i < this.dynamicTitles.Length; i++)
                    Assert.IsTrue(responseContent.Contains(this.dynamicTitles[i]), "The dynamic item with this title was not found!");
            }
            finally
            {
                this.pageOperations.DeletePages();
                ServerOperationsFeather.DynamicModulePressArticle().DeletePressArticle(dynamicCollection);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Taxonomies().DeleteTags(this.tagTitle);
            }
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author("FeatherTeam")]
        [Description("Verify selected items per page.")]
        public void DynamicWidgetsDesignerContent_VerifySelectedItemsFunctionality()
        {
            this.CreatePressArticleAndReturnTagId();
            var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

            var mvcProxy = new MvcWidgetProxy();
            mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
            var dynamicController = new DynamicContentController();
            dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
            dynamicController.Model.SelectionMode = SelectionMode.SelectedItems;
            dynamicController.Model.SerializedSelectedItemsIds = "[\"" + dynamicCollection[3].Id.ToString() + "\"]";
            mvcProxy.Settings = new ControllerSettings(dynamicController);
            mvcProxy.WidgetName = WidgetName;

            try
            {
                var modelItems = dynamicController.Model.CreateListViewModel(taxonFilter: null, page: 1);
                var dynamicItems = modelItems.Items.ToList();
                int itemsCount = dynamicItems.Count;

                Assert.AreEqual(1, itemsCount, "The count of the dynamic item is not as expected");

                string title = dynamicItems[0].Title;
                Assert.IsTrue(title.Equals(this.dynamicTitles[1]), "The dynamic item with this title was not found!");
            }
            finally
            {
                ServerOperationsFeather.DynamicModulePressArticle().DeletePressArticle(dynamicCollection);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Taxonomies().DeleteTags(this.tagTitle);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author("FeatherTeam")]
        [Description("Verify dynaimc items by tag.")]
        public void DynamicWidgetsDesignerContent_VerifyDynamicItemsByTagFunctionality()
        {
            TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();
            var taxonId = this.CreatePressArticleAndReturnTagId();

            var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();

            var mvcProxy = new MvcWidgetProxy();
            mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
            var dynamicController = new DynamicContentController();
            dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
            dynamicController.Model.SelectionMode = SelectionMode.FilteredItems;
            mvcProxy.Settings = new ControllerSettings(dynamicController);
            mvcProxy.WidgetName = WidgetName;

            try
            {
                for (int i = 0; i < taxonId.Length; i++)
                {
                    var tag = taxonomyManager.GetTaxa<FlatTaxon>().Where(t => t.Id == taxonId[i]).FirstOrDefault();

                    var modelItems = dynamicController.Model.CreateListViewModel(taxonFilter: tag, page: 1);
                    var dynamicItems = modelItems.Items.ToList();
                    int itemsCount = dynamicItems.Count;

                    Assert.AreEqual(1, itemsCount, "The count of the dynamic item is not as expected");

                    string title = dynamicItems[0].Title;
                    Assert.IsTrue(title.Equals(this.dynamicTitles[i]), "The dynamic item with this title was not found!");
                }
            }
            finally
            {
                ServerOperationsFeather.DynamicModulePressArticle().DeletePressArticle(dynamicCollection);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Taxonomies().DeleteTags(this.tagTitle);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author("FeatherTeam")]
        [Description("Verify dynaimc items by tag.")]
        public void DynamicWidgetsDesignerContent_VerifyDynamicItemsByPublicationDateLastOneDayFunctionality()
        {
            DateTime publicationDate = DateTime.UtcNow.AddDays(-10);

            this.CreatePressArticleAndReturnTagId();

            var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();
            ServerOperationsFeather.DynamicModulePressArticle().PublishPressArticleWithSpecificDate(dynamicCollection[4], publicationDate);

            var mvcProxy = new MvcWidgetProxy();
            mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
            var dynamicController = new DynamicContentController();
            dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
            dynamicController.Model.SelectionMode = SelectionMode.FilteredItems;
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

            try
            {
                var modelItems = dynamicController.Model.CreateListViewModel(taxonFilter: null, page: 1);
                var dynamicItems = modelItems.Items.ToList();
                int itemsCount = dynamicItems.Count;

                Assert.AreEqual(2, itemsCount, "The count of the dynamic item is not as expected");

                string title1 = dynamicItems[0].Title;
                Assert.IsTrue(title1.Equals(this.dynamicTitles[1]), "The dynamic item with this title was not found!");

                string title2 = dynamicItems[1].Title;
                Assert.IsTrue(title2.Equals(this.dynamicTitles[0]), "The dynamic item with this title was not found!");

                for (int i = 0; i < itemsCount; i++)
                {
                    string title3 = dynamicItems[i].Title;
                    Assert.IsFalse(title3.Equals(this.dynamicTitles[2]), "The dynamic item with this title was found!");
                }
            }
            finally
            {
                ServerOperationsFeather.DynamicModulePressArticle().DeletePressArticle(dynamicCollection);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Taxonomies().DeleteTags(this.tagTitle);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author("FeatherTeam")]
        [Description("Verify dynaimc items by tag.")]
        public void DynamicWidgetsDesignerContent_VerifyDynamicItemsByPublicationDateCustomRangeFunctionality()
        {
            DateTime publicationDate = new DateTime(2014, 10, 23, 12, 00, 00);

            this.CreatePressArticleAndReturnTagId();

            var dynamicCollection = ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles();
            ServerOperationsFeather.DynamicModulePressArticle().PublishPressArticleWithSpecificDate(dynamicCollection[4], publicationDate);

            var mvcProxy = new MvcWidgetProxy();
            mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
            var dynamicController = new DynamicContentController();
            dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
            dynamicController.Model.SelectionMode = SelectionMode.FilteredItems;
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

            try
            {
                var modelItems = dynamicController.Model.CreateListViewModel(taxonFilter: null, page: 1);
                var dynamicItems = modelItems.Items.ToList();
                int itemsCount = dynamicItems.Count;

                Assert.AreEqual(1, itemsCount, "The count of the dynamic item is not as expected");

                string title1 = dynamicItems[0].Title;
                Assert.IsTrue(title1.Equals(this.dynamicTitles[2]), "The dynamic item with this title was not found!");

                for (int i = 0; i < itemsCount; i++)
                {
                    string titleNotExist = dynamicItems[i].Title;
                    Assert.IsFalse(titleNotExist.Equals(this.dynamicTitles[0]), "The dynamic item with this title was found!");
                    Assert.IsFalse(titleNotExist.Equals(this.dynamicTitles[1]), "The dynamic item with this title was found!");
                }
            }
            finally
            {
                ServerOperationsFeather.DynamicModulePressArticle().DeletePressArticle(dynamicCollection);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Taxonomies().DeleteTags(this.tagTitle);
            }
        }

        [FixtureTearDown]
        public void Teardown()
        {
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.ModuleBuilder().DeleteModule(ModuleName, string.Empty, TransactionName);
        }

        private Guid[] CreatePressArticleAndReturnTagId()
        {
            Guid[] taxonId = new Guid[3];

            for (int i = 0; i < 3; i++)
            {
                taxonId[i] = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Taxonomies().CreateFlatTaxon(Telerik.Sitefinity.TestUtilities.CommonOperations.TaxonomiesConstants.TagsTaxonomyId, this.tagTitle[i]);
                ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticle(this.dynamicTitles[i], this.dynamicUrls[i], taxonId[i]);
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
