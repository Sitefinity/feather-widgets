using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Test arrangements for EditDynamicWidgetViaInlineEditing
    /// </summary>
    public class EditDynamicWidgetViaInlineEditing : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            for (int i = 0; i < 2; i++)
            {
                ServerOperations.Taxonomies().CreateCategory(this.categoryName[i]);
                ServerOperations.Taxonomies().CreateTag(this.tagName[i]);
            }

            ServerOperationsFeather.DynamicModules().ImportModule(ModuleResource);
            ServerOperations.ModuleBuilder().ActivateModule(ModuleName, string.Empty, TransactionName);

            ServerOperationsFeather.DynamicModuleAllTypes().CreateAlltypes();

            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            
            ServerOperationsFeather.Pages().AddDynamicWidgetToPage(pageId, ResolveType, WidgetName, WidgetCaptionDynamicWidget);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.ModuleBuilder().DeleteAllModules(string.Empty, TransactionName);
            ServerOperations.Taxonomies().ClearAllCategories(TaxonomiesConstants.CategoriesTaxonomyId);
            ServerOperations.Taxonomies().ClearAllTags(TaxonomiesConstants.TagsTaxonomyId);
        }

        private const string ModuleName = "AllTypesModule";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.AllTypesModule.zip";
        private const string ResolveType = "Telerik.Sitefinity.DynamicTypes.Model.AllTypesModule.Alltypes";
        private const string TransactionName = "Module Installations";
        private const string WidgetName = "AllTypes";
        private const string WidgetCaptionDynamicWidget = "AllTypes MVC";
        private const string PageName = "TestPage";
        private readonly string[] categoryName = { "Categories", "New category" };
        private readonly string[] tagName = { "Tags", "New tag" };
    }
}
