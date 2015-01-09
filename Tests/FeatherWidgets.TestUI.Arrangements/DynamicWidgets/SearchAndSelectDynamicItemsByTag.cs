using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// SearchAndSelectDynamicItemsByTag arrangement class.
    /// </summary>
    public class SearchAndSelectDynamicItemsByTag : ITestArrangement
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
            Guid[] taxonId = new Guid[5];

            for (int i = 0; i < 5; i++)
            {
                taxonId[i] = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Taxonomies().CreateFlatTaxon(Telerik.Sitefinity.TestUtilities.CommonOperations.TaxonomiesConstants.TagsTaxonomyId, TaxonTitle + i);
                ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticle(ItemsTitle + i, ItemsTitle + i + "Url", taxonId[i], Guid.Empty);
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
            ServerOperations.ModuleBuilder().DeleteAllModules(string.Empty, TransactionName);
            ServerOperations.Taxonomies().ClearAllTags(TaxonomiesConstants.TagsTaxonomyId);
        }

        private const string PageName = "TestPage";
        private readonly string moduleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressReleaseWithCategoriesField.zip";
        private const string TransactionName = "Module Installations";
        private const string ItemsTitle = "Title";
        private const string TaxonTitle = "Tag";
    }
}
