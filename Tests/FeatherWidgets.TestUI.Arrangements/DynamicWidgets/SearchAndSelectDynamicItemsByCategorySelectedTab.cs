using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// SearchAndSelectDynamicItemsByCategorySelectedTab arrangement class.
    /// </summary>
    public class SearchAndSelectDynamicItemsByCategorySelectedTab : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);

            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            List<string> categories = new List<string>();

            foreach (var taxonTitle in this.parentCategories)
            {
                ServerOperations.Taxonomies().CreateCategory(taxonTitle + "0");
                categories.Add(taxonTitle + "0");
                
                for (int i = 1; i < 6; i++)
                {
                    ServerOperations.Taxonomies().CreateCategory(taxonTitle + i, taxonTitle + (i - 1));
                    categories.Add(taxonTitle + i);
                }
            }

            TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();
            int index = 0;
            foreach (var category in categories)
            {
                var cat = taxonomyManager.GetTaxa<HierarchicalTaxon>().SingleOrDefault(t => t.Title == category);
                ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticle(ItemsTitle + index, ItemsTitle + index + "Url", Guid.Empty, cat.Id);
                index++;
            }

            ServerOperationsFeather.Pages().AddDynamicWidgetToPage(pageId, "Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle", "PressArticle", "Press Articles MVC");
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles());
            ServerOperations.Taxonomies().ClearAllCategories(TaxonomiesConstants.CategoriesTaxonomyId);
        }

        private const string PageName = "TestPage";
        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressReleaseWithCategoriesField.zip";
        private const string ItemsTitle = "Title";
        private readonly List<string> parentCategories = new List<string> { "Category", "AnotherCategory" };     
    }
}