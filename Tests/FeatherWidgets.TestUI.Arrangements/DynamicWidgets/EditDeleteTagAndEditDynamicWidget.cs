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
    /// EditDeleteTagAndEdiDynamicWidget arrangement class.
    /// </summary>
    public class EditDeleteTagAndEditDynamicWidget : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);

            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            Guid[] taxonId = new Guid[5];

            for (int i = 1; i < 3; i++)
            {
                taxonId[i] = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Taxonomies().CreateFlatTaxon(Telerik.Sitefinity.TestUtilities.CommonOperations.TaxonomiesConstants.TagsTaxonomyId, TaxonTitle + i);
                ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticle(ItemsTitle + i, ItemsTitle + i + "Url", taxonId[i], Guid.Empty);
            }

            ServerOperationsFeather.Pages().AddDynamicWidgetToPage(pageId, "Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle", "PressArticle", "Press Articles MVC");
        }

        /// <summary>
        /// Edits the tag.
        /// </summary>
        [ServerArrangement]
        public void EditTag()
        {
            ServerOperationsFeather.Tags().EditTag(OldTagName, NewTagName);
        }

        /// <summary>
        /// Deletes the tag.
        /// </summary>
        [ServerArrangement]
        public void DeleteTag()
        {
            ServerOperations.Taxonomies().DeleteTags(NewTagName);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles());
            ServerOperations.Taxonomies().ClearAllTags(TaxonomiesConstants.TagsTaxonomyId);
        }

        private const string PageName = "TestPage";
        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressReleaseWithCategoriesField.zip";
        private const string ItemsTitle = "Title";
        private const string TaxonTitle = "Tag";
        private const string OldTagName = "Tag1";
        private const string NewTagName = "Tag1_Edited";
    }
}
