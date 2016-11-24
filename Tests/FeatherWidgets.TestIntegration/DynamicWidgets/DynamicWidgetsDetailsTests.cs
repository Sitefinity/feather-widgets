using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.DynamicContent.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.DynamicWidgets
{
    [TestFixture]
    [AssemblyFixture]
    [Description("Integration tests dynamic widget details")]
    [Author(FeatherTeams.SitefinityTeam2)]
    public class DynamicWidgetsDetailsTests
    {
        /// <summary>
        /// Set up method
        /// </summary>
        [FixtureSetUp]
        public void FixtureSetup()
        {
            this.CreateCustomFlatTaxonomyTestData();
            this.CreateCustomHierarchicalTaxonomyTestData();
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);
        }

        [FixtureTearDown]
        public void FixtureTearDown()
        {
            ServerOperations.ModuleBuilder().DeleteModule(ModuleName, string.Empty, "Module Installations");
            this.taxonomyOperations.DeleteFlatTaxonomy(FlatTaxonomyTitle);
            this.taxonomyOperations.DeleteHierarchicalTaxonomy(HierarchicalTaxonomyTitle);
        }

        [TearDown]
        public void TearDown()
        {
            var dynamicModulePressArticle = ServerOperationsFeather.DynamicModulePressArticle();
            dynamicModulePressArticle.DeleteAllDynamicItemsInProvider("dynamicContentProvider");
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.SitefinityTeam2)]
        [Description("Verifies that details of dynamic content item with empty single taxonomy are displayed")]
        public void DynamicWidgets_DisplayContentItemDetailsWithEmptySingleFlatAndHierarchicalTaxonomy()
        {
            var pageId = Guid.Empty;

            try
            {
                string dynamicTitle = "dynamic type title";
                string dynamicUrl = "dynamic-type-url";

                var dynamicModulePressArticle = ServerOperationsFeather.DynamicModulePressArticle();
                dynamicModulePressArticle.CreatePressArticle(dynamicTitle, dynamicUrl);

                var index = 1;
                var mvcWidget = this.CreateDynamicMvcWidget();
                pageId = this.pagesOperations.CreatePageWithControl(mvcWidget, PageName, PageName, PageUrl, index);

                string url = UrlPath.ResolveAbsoluteUrl("~/" + PageUrl + index);
                var article = dynamicModulePressArticle.RetrieveCollectionOfPressArticles().FirstOrDefault(content => content.UrlName == dynamicUrl);
                Assert.IsNotNull(article);
                string detailsUrl = url + article.ItemDefaultUrl;
                string responseContent = PageInvoker.ExecuteWebRequest(detailsUrl);

                Assert.IsNotNull(responseContent);
                Assert.IsTrue(responseContent.Contains(dynamicTitle), "The title of the item was not found");
                Assert.IsTrue(responseContent.Contains(FlatTaxonomyProperty), "The title of the flat taxonomy was not found");
                Assert.IsTrue(responseContent.Contains(HierarchicalTaxonomyProperty), "The title of the hierarchical taxonomy was not found");
                Assert.IsFalse(responseContent.Contains(this.flatTaxonNames[0]), "The flat taxon title is found, but it shouldn't!");
                Assert.IsFalse(responseContent.Contains(this.hierarchicalTaxonNames[0]), "The hierarchical taxon title is found!. but it shouldn't");
            }
            finally
            {
                if (pageId != Guid.Empty)
                {
                    ServerOperations.Pages().DeletePage(pageId);
                }
            }
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.SitefinityTeam2)]
        [Description("Verifies that details of dynamic content item with non empty single flat taxonomy are displayed")]
        public void DynamicWidgets_DisplayContentItemDetailsWithNonEmptySingleFlatTaxonomy()
        {
            var pageId = Guid.Empty;

            try
            {
                string dynamicTitle = "dynamic type title";
                string dynamicUrl = "dynamic-type-url";

                var dynamicModulePressArticle = ServerOperationsFeather.DynamicModulePressArticle();
                dynamicModulePressArticle.CreatePressArticleWithCustomTaxonomy(dynamicTitle, dynamicUrl, FlatTaxonomyProperty, this.flatTaxonNames);

                var index = 1;
                var mvcWidget = this.CreateDynamicMvcWidget();
                pageId = this.pagesOperations.CreatePageWithControl(mvcWidget, PageName, PageName, PageUrl, index);

                string url = UrlPath.ResolveAbsoluteUrl("~/" + PageUrl + index);
                var article = dynamicModulePressArticle.RetrieveCollectionOfPressArticles().FirstOrDefault(content => content.UrlName == dynamicUrl);
                Assert.IsNotNull(article);
                string detailsUrl = url + article.ItemDefaultUrl;
                string responseContent = PageInvoker.ExecuteWebRequest(detailsUrl);

                Assert.IsNotNull(responseContent);
                Assert.IsTrue(responseContent.Contains(dynamicTitle), "The title of the item was not found");
                Assert.IsTrue(responseContent.Contains(FlatTaxonomyProperty), "The title of the flat taxonomy was not found");
                Assert.IsTrue(responseContent.Contains(HierarchicalTaxonomyProperty), "The title of hierarchical taxonomy was not found");
                Assert.IsTrue(responseContent.Contains(this.flatTaxonNames[0]), "The flat taxon title was not found!");
                Assert.IsFalse(responseContent.Contains(this.hierarchicalTaxonNames[0]), "The hierarchical taxon title is found!. but it shouldn't");
            }
            finally
            {
                if (pageId != Guid.Empty)
                {
                    ServerOperations.Pages().DeletePage(pageId);
                }
            }
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.SitefinityTeam2)]
        [Description("Verifies that details of dynamic content item with non empty single hierarchical taxonomy are displayed")]
        public void DynamicWidgets_DisplayContentItemDetailsWithNonEmptySingleHierarchicalTaxonomy()
        {
            var pageId = Guid.Empty;

            try
            {
                string dynamicTitle = "dynamic type title";
                string dynamicUrl = "dynamic-type-url";

                var dynamicModulePressArticle = ServerOperationsFeather.DynamicModulePressArticle();
                dynamicModulePressArticle.CreatePressArticleWithCustomTaxonomy(dynamicTitle, dynamicUrl, HierarchicalTaxonomyProperty, this.hierarchicalTaxonNames, true);

                var index = 1;
                var mvcWidget = this.CreateDynamicMvcWidget();
                pageId = this.pagesOperations.CreatePageWithControl(mvcWidget, PageName, PageName, PageUrl, index);

                string url = UrlPath.ResolveAbsoluteUrl("~/" + PageUrl + index);
                var article = dynamicModulePressArticle.RetrieveCollectionOfPressArticles().FirstOrDefault(content => content.UrlName == dynamicUrl);
                Assert.IsNotNull(article);
                string detailsUrl = url + article.ItemDefaultUrl;
                string responseContent = PageInvoker.ExecuteWebRequest(detailsUrl);

                Assert.IsNotNull(responseContent);
                Assert.IsTrue(responseContent.Contains(dynamicTitle), "The title of the item was not found");
                Assert.IsTrue(responseContent.Contains(FlatTaxonomyProperty), "The title of the flat taxonomy was not found");
                Assert.IsTrue(responseContent.Contains(HierarchicalTaxonomyProperty), "The title of hierarchical taxonomy was not found");
                Assert.IsTrue(responseContent.Contains(this.hierarchicalTaxonNames[0]), "The hierarchical taxon title was not found!");
                Assert.IsFalse(responseContent.Contains(this.flatTaxonNames[0]), "The flat taxon title is found, but it shouldn't!");
            }
            finally
            {
                if (pageId != Guid.Empty)
                {
                    ServerOperations.Pages().DeletePage(pageId);
                }
            }
        }

        private void CreateCustomFlatTaxonomyTestData()
        {
            var taxonomyId = new Guid("3031825E-AA00-654C-9EA2-FF0000531C16");
            this.taxonomyOperations.CreateFlatTaxonomy(FlatTaxonomyTitle, taxonomyId);
            this.taxonomyOperations.CreateFlatTaxon(taxonomyId, this.flatTaxonNames[0]);
        }

        private void CreateCustomHierarchicalTaxonomyTestData()
        {
            var taxonomyId = new Guid("3531825E-AA00-654C-9EA2-FF0000531C16");
            this.taxonomyOperations.CreateHierarchicalTaxonomy(HierarchicalTaxonomyTitle, taxonomyId);
            this.taxonomyOperations.CreateHierarchicalTaxon(taxonomyId, this.hierarchicalTaxonNames[0]);
        }

        private MvcWidgetProxy CreateDynamicMvcWidget()
        {
            var mvcWidget = new MvcWidgetProxy();
            mvcWidget.ControllerName = typeof(DynamicContentController).FullName;
            var dynamicController = new DynamicContentController();
            dynamicController.Model.ContentType = TypeResolutionService.ResolveType(ResolveType);
            dynamicController.Model.ProviderName = FeatherWidgets.TestUtilities.CommonOperations.DynamicModulesOperations.ProviderName;
            mvcWidget.Settings = new ControllerSettings(dynamicController);
            mvcWidget.WidgetName = WidgetName;

            return mvcWidget;
        }

        private readonly TaxonomiesOperations taxonomyOperations = new TaxonomiesOperations();
        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressReleaseCustomTaxonomySingle.zip";
        private readonly List<string> flatTaxonNames = new List<string>() { "flat1" };
        private readonly List<string> hierarchicalTaxonNames = new List<string>() { "hierarchical1" };
        private const string ResolveType = "Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle";
        private const string WidgetName = "PressArticle";
        private readonly FeatherWidgets.TestUtilities.CommonOperations.PagesOperations pagesOperations = new FeatherWidgets.TestUtilities.CommonOperations.PagesOperations();
        private const string PageName = "TestPage";
        private const string PageUrl = "tests-page";
        private const string FlatTaxonomyTitle = "flat custom classification";
        private const string HierarchicalTaxonomyTitle = "hierarchical custom classification";
        private const string FlatTaxonomyProperty = "flatcustomclassification";
        private const string HierarchicalTaxonomyProperty = "hierarchicalcustomclassification";
    }
}
