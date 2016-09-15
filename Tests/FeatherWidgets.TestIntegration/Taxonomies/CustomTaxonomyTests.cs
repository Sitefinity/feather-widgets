using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.DynamicContent.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using CommonOperationsContext = Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestIntegration.Taxonomies
{
    [TestFixture]
    [AssemblyFixture]
    [Description("Integration tests for the custom taxonomy scenarios.")]
    [Author(FeatherTeams.SitefinityTeam2)]
    public class CustomTaxonomyTests
    {
        /// <summary>
        /// Set up method
        /// </summary>
        [FixtureSetUpAttribute]
        public void Setup()
        {
            this.CreateCustomFlatTaxonomyTestData();
            this.CreateCustomHierarchicalTaxonomyTestData();
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);
        }

        [FixtureTearDown]
        public void TearDown()
        {
            ServerOperations.ModuleBuilder().DeleteModule(ModuleName, string.Empty, "Module Installations");
            this.taxonomyOperations.DeleteFlatTaxonomy(FlatTaxonomyTitle);
            this.taxonomyOperations.DeleteHierarchicalTaxonomy(HierarchicalTaxonomyTitle);
        }

        [Test]
        [Category(TestCategories.Taxonomies)]
        [Author(FeatherTeams.SitefinityTeam2)]
        [Description("Verifies that taxons belonging to a flat taxonomy with spaced title, assigned to a dynamic content type are available at dynamic content mvc detail widget.")]
        public void SpacedTitleFlatTaxonomy_DynamicContentWidget()
        {
            var pageId = Guid.Empty;
           
            try
            {
                string dynamicTitle = "dynamic type title";
                string dynamicUrl = "dynamic-type-url";
               
                var flatTaxonomyName = Regex.Replace(FlatTaxonomyTitle, " ", string.Empty);
                var dynamicModulePressArticle = ServerOperationsFeather.DynamicModulePressArticle();
                dynamicModulePressArticle.CreatePressArticleWithCustomTaxonomy(dynamicTitle, dynamicUrl, flatTaxonomyName, this.flatTaxaNames);
  
                var index = 1;
                var mvcWidget = this.CreateDynamicMvcWidget();
                pageId = this.pagesOperations.CreatePageWithControl(mvcWidget, PageName, PageName, PageUrl, index);

                string url = UrlPath.ResolveAbsoluteUrl("~/" + PageUrl + index);
                var article = dynamicModulePressArticle.RetrieveCollectionOfPressArticles().FirstOrDefault(content => content.UrlName == dynamicUrl);
                Assert.IsNotNull(article);
                string detailsUrl = url + article.ItemDefaultUrl;
                string responseContent = PageInvoker.ExecuteWebRequest(detailsUrl);

                Assert.IsNotNull(responseContent);
                Assert.IsTrue(responseContent.Contains(this.flatTaxaNames[0]), "The taxon with specified spaced taxonomy title was not found!");
                Assert.IsTrue(responseContent.Contains(this.flatTaxaNames[1]), "The taxon with specified spaced taxonomy title was not found!");
            }
            finally
            {
                if (pageId != Guid.Empty)
                {
                    CommonOperationsContext.ServerOperations.Pages().DeletePage(pageId);
                }
            }
        }

        [Test]
        [Category(TestCategories.Taxonomies)]
        [Author(FeatherTeams.SitefinityTeam2)]
        [Description("Verifies that taxons belonging to a hierarchical taxonomy with spaced title, assigned to a dynamic content type are available at dynamic content mvc detail widget.")]
        public void SpacedTitleHierarchicalTaxonomy_DynamicContentWidget()
        {
            var pageId = Guid.Empty;
            try
            {
                string dynamicTitle = "h dynamic type title";
                string dynamicUrl = "h-dynamic-type-url";
                var dynamicModulePressArticle = ServerOperationsFeather.DynamicModulePressArticle();
                var taxonomyName = Regex.Replace(HierarchicalTaxonomyTitle, " ", string.Empty);
                dynamicModulePressArticle.CreatePressArticleWithCustomTaxonomy(dynamicTitle, dynamicUrl, taxonomyName, this.hierarchicalTaxaNames, true);

                var index = 1;
                var mvcWidget = this.CreateDynamicMvcWidget();
                pageId = this.pagesOperations.CreatePageWithControl(mvcWidget, PageName, PageName, PageUrl, index);

                string url = UrlPath.ResolveAbsoluteUrl("~/" + PageUrl + index);
                var article = dynamicModulePressArticle.RetrieveCollectionOfPressArticles().FirstOrDefault(content => content.UrlName == dynamicUrl);
                Assert.IsNotNull(article);
                string detailsUrl = url + article.ItemDefaultUrl;
                string responseContent = PageInvoker.ExecuteWebRequest(detailsUrl);

                Assert.IsNotNull(responseContent);

                Assert.IsTrue(responseContent.Contains(this.hierarchicalTaxaNames[0]), "The parant taxon with specified spaced taxonomy title was not found!");
                Assert.IsTrue(responseContent.Contains(this.hierarchicalTaxaNames[1]), "The first child taxon with specified spaced taxonomy title was not found!");
                Assert.IsTrue(responseContent.Contains(this.hierarchicalTaxaNames[2]), "The second child taxon with specified spaced taxonomy title was not found!");
            }
            finally
            {
                if (pageId != Guid.Empty)
                {
                    CommonOperationsContext.ServerOperations.Pages().DeletePage(pageId);
                }
            }
        }

        private void CreateCustomFlatTaxonomyTestData()
        {
            var taxonomyId = new Guid("3031825E-AA00-654C-9EA2-FF0000531C16");
            this.taxonomyOperations.CreateFlatTaxonomy(FlatTaxonomyTitle, taxonomyId);
            this.taxonomyOperations.CreateFlatTaxon(taxonomyId, this.flatTaxaNames[0]);
            this.taxonomyOperations.CreateFlatTaxon(taxonomyId, this.flatTaxaNames[1]);
        }

        private void CreateCustomHierarchicalTaxonomyTestData()
        {
            var taxonomyId = new Guid("3531825E-AA00-654C-9EA2-FF0000531C16");
            this.taxonomyOperations.CreateHierarchicalTaxonomy(HierarchicalTaxonomyTitle, taxonomyId);
            this.taxonomyOperations.CreateHierarchicalTaxon(taxonomyId, this.hierarchicalTaxaNames[0]);
            this.taxonomyOperations.CreateHierarchicalTaxon(this.hierarchicalTaxaNames[1], this.hierarchicalTaxaNames[0], HierarchicalTaxonomyTitle);
            this.taxonomyOperations.CreateHierarchicalTaxon(this.hierarchicalTaxaNames[2], this.hierarchicalTaxaNames[0], HierarchicalTaxonomyTitle);
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

        #region Private fields and constants
        private readonly List<string> flatTaxaNames = new List<string>() { "f1", "f2" };
        private readonly List<string> hierarchicalTaxaNames = new List<string>() { "h1", "h11", "h12" };
        private readonly TaxonomiesOperations taxonomyOperations = new TaxonomiesOperations();
        private readonly FeatherWidgets.TestUtilities.CommonOperations.PagesOperations pagesOperations = new FeatherWidgets.TestUtilities.CommonOperations.PagesOperations();
        private const string PageName = "TestPage";
        private const string PageUrl = "tests-page";
        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressReleaseCustomTaxonomySpacedTitle.zip";
        private const string ResolveType = "Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle";
        private const string WidgetName = "PressArticle";
        private const string FlatTaxonomyTitle = "flat custom classification";
        private const string HierarchicalTaxonomyTitle = "hierarchical custom classification";

        #endregion
    }
}