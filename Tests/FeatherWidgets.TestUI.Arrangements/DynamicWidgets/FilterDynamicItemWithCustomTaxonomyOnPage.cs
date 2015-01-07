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
    /// FilterNewsItemWithCustomTaxonomyOnPage arrangement class.
    /// </summary>
    public class FilterDynamicItemWithCustomTaxonomyOnPage : ITestArrangement
    {   
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperationsFeather.DynamicModules().ImportModule(ModuleResource);
            ServerOperations.ModuleBuilder().ActivateModule(this.moduleName, string.Empty, TransactionName);
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);

            ServerOperations.Taxonomies().CreateFlatTaxonomy(this.customFlatTaxonomyName);
            ServerOperations.Taxonomies().CreateFlatTaxon(this.customFlatTaxonName1, this.customFlatTaxonomyName);
            ServerOperations.Taxonomies().CreateFlatTaxon(this.customFlatTaxonName2, this.customFlatTaxonomyName);
            ServerOperationsFeather.DynamicModulePressArticle().AddCustomTaxonomyToContext(this.customFlatTaxonomyName, this.isHierarchicalTaxonomy);
            ServerOperations.SystemManager().RestartApplication(false);

            var customTaxonName1 = new List<string> { this.customFlatTaxonName1 };
            var customTaxonName2 = new List<string> { this.customFlatTaxonName2 };
            ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleWithCustomTaxonomy(ItemsTitle1, ItemsTitle1 + "Url", this.customFlatTaxonomyName, customTaxonName1);
            ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleWithCustomTaxonomy(ItemsTitle2, ItemsTitle2 + "Url", this.customFlatTaxonomyName, customTaxonName2);
            ServerOperationsFeather.Pages().AddDynamicWidgetToPage(pageId, "Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle", "PressArticle", "Press Articles MVC");
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {           
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.ModuleBuilder().DeleteAllModules(string.Empty, TransactionName);
            ServerOperations.Taxonomies().DeleteFlatTaxonomy(this.customFlatTaxonomyName);         
        }
        
        private const string PageName = "TestPage";
        private readonly string moduleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressReleaseWithCategoriesField.zip";
        private const string TransactionName = "Module Installations";
        private const string ItemsTitle1 = "Title1";
        private const string ItemsTitle2 = "Title2";

        private readonly string customFlatTaxonomyName = "MyCustomFlatTaxonomy";
        private readonly string customFlatTaxonName1 = "MyFlatTaxon1";
        private readonly string customFlatTaxonName2 = "MyFlatTaxon2";
        private readonly bool isHierarchicalTaxonomy = false;
    }
}
