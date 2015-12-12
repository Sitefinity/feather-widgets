using System;
using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// FilterDynamicItemWithCategoryOnPage arrangement class.
    /// </summary>
    public class FilterDynamicItemWithCategoryOnPage : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);

            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            ServerOperations.Taxonomies().CreateCategory(TaxonTitle + "0");

            for (int i = 1; i < 7; i++)
            {
                ServerOperations.Taxonomies().CreateCategory(TaxonTitle + i, TaxonTitle + (i - 1));
                var category = TaxonomyManager.GetManager().GetTaxa<HierarchicalTaxon>().SingleOrDefault(t => t.Title == TaxonTitle + i);
                ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticle(ItemsTitle + (i - 1), ItemsTitle + i + "Url", Guid.Empty, category.Id);         
            }

            ServerOperationsFeather.Pages().AddDynamicWidgetToPage(pageId, "Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle", "PressArticle", "Press Articles MVC");
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);
            ServerOperations.Pages().DeleteAllPages();            
            ServerOperations.Taxonomies().ClearAllCategories(TaxonomiesConstants.CategoriesTaxonomyId);

            var providerName = string.Empty;
            if (ServerOperations.MultiSite().CheckIsMultisiteMode())
            {
                providerName = "dynamicContentProvider";
            }

            ServerOperationsFeather.DynamicModulePressArticle().DeleteAllDynamicItemsInProvider(providerName);
        }

        private const string PageName = "TestPage";
        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressReleaseWithCategoriesField.zip";
        private const string ItemsTitle = "Title";
        private const string TaxonTitle = "Category";
        private const string AdminUserName = "admin";
        private const string AdminPass = "admin@2";
    }
}
