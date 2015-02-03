using System;
using System.Collections.Generic;
using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// FilterDynamicItemWithCustomTaxonomyOnPage arrangement class.
    /// </summary>
    public class FilterDynamicItemWithCustomTaxonomyOnPage : ITestArrangement
    {   
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);

            Guid pageId = ServerOperations.Pages().CreatePage(PageName);

            ServerOperations.Taxonomies().CreateFlatTaxonomy(CustomFlatTaxonomyName);
            ServerOperations.Taxonomies().CreateFlatTaxon(CustomFlatTaxonName1, CustomFlatTaxonomyName);
            ServerOperations.Taxonomies().CreateFlatTaxon(CustomFlatTaxonName2, CustomFlatTaxonomyName);
            ServerOperationsFeather.DynamicModulePressArticle().AddCustomTaxonomyToContext(CustomFlatTaxonomyName, IsHierarchicalTaxonomy);
            ServerOperations.SystemManager().RestartApplication(false);

            var customTaxonName1 = new List<string> { CustomFlatTaxonName1 };
            var customTaxonName2 = new List<string> { CustomFlatTaxonName2 };
            ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleWithCustomTaxonomy(ItemsTitle1, ItemsTitle1 + "Url", CustomFlatTaxonomyName, customTaxonName1);
            ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleWithCustomTaxonomy(ItemsTitle2, ItemsTitle2 + "Url", CustomFlatTaxonomyName, customTaxonName2);
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
            ServerOperations.Taxonomies().DeleteFlatTaxonomy(CustomFlatTaxonomyName);         
        }
        
        private const string PageName = "TestPage";
        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressReleaseWithCategoriesField.zip";
        private const string ItemsTitle1 = "Title1";
        private const string ItemsTitle2 = "Title2";

        private const string CustomFlatTaxonomyName = "MyCustomFlatTaxonomy";
        private const string CustomFlatTaxonName1 = "MyFlatTaxon1";
        private const string CustomFlatTaxonName2 = "MyFlatTaxon2";
        private const bool IsHierarchicalTaxonomy = false;
    }
}
