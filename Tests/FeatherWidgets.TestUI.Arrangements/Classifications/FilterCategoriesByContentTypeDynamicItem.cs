using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// FilterCategoriesByContentTypeDynamicItem arrangement class.
    /// </summary>
    public class FilterCategoriesByContentTypeDynamicItem : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);
            ServerOperations.Taxonomies().CreateCategory(this.taxonTitleDynamic + "0");
            ServerOperations.Taxonomies().CreateCategory(this.taxonTitleNews + "0");

            for (int i = 1; i < 4; i++)
            {
                ServerOperations.Taxonomies().CreateCategory(this.taxonTitleDynamic + i, this.taxonTitleDynamic + (i - 1));
                ServerOperations.Taxonomies().CreateCategory(this.taxonTitleNews + i, this.taxonTitleNews + (i - 1));
                var category = TaxonomyManager.GetManager().GetTaxa<HierarchicalTaxon>().SingleOrDefault(t => t.Title == this.taxonTitleDynamic + i);
                ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticle(ItemsTitle + (i - 1), ItemsTitle + i + "Url", Guid.Empty, category.Id);
                ServerOperationsFeather.NewsOperations().CreatePublishedNewsItem(NewsTitle + i, NewsContent, AuthorName, SourceName, new List<string> { this.taxonTitleNews + i }, null, null);
            }

            ServerOperationsFeather.Pages().AddDynamicWidgetToPage(pageId, "Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle", "PressArticle", "Press Articles MVC");
            ServerOperationsFeather.Pages().AddNewsWidgetToPage(pageId);
            ServerOperationsFeather.Pages().AddCategoriesWidgetToPage(pageId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            var providerName = string.Empty;
            if (ServerOperations.MultiSite().CheckIsMultisiteMode())
            {
                providerName = "dynamicContentProvider";
            }

            ServerOperationsFeather.DynamicModulePressArticle().DeleteAllDynamicItemsInProvider(providerName);

            for (int i = 0; i < 4; i++)
            {
                ServerOperations.Taxonomies().DeleteCategories(this.taxonTitleDynamic + i, this.taxonTitleNews + i);
            }

            ServerOperations.News().DeleteAllNews();
        }

        private const string PageName = "CategoriesPage";
        private string taxonTitleNews = "CategoryNews";
        private string taxonTitleDynamic = "CategoryDynamic";
        private const string ItemsTitle = "Title";
        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressReleaseWithCategoriesField.zip";
        private const string NewsContent = "News content";
        private const string NewsTitle = "NewsTitle";
        private const string AuthorName = "AuthorName";
        private const string SourceName = "SourceName";
    }
}
