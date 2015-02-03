using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// CheckSelectorsAfterSelectUnselectAndUNPublishingDynamicItem arragement.
    /// </summary>
    public class CheckSelectorsAfterSelectUnselectAndUNPublishingDynamicItem : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);
            
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);

            for (int i = 0; i < 20; i++)
            {
                items[i] = ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(DynamicItemTitle + i, DynamicItemTitle + i + "Url");               
            }

            ServerOperationsFeather.Pages().AddDynamicWidgetToPage(pageId, "Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle", "PressArticle", "Press Articles MVC");
        }

        /// <summary>
        /// Unpublish dynamic items arrangement method.
        /// </summary>
        [ServerArrangement]
        public void UNPublishDynamicItem()
        {
            ServerOperationsFeather.DynamicModulePressArticle().UNPublishPressArticle(items[5]);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles());
        }

        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressReleaseWithCategoriesField.zip";
        private const string DynamicItemTitle = "Dynamic Item Title";
        private const string PageName = "TestPage";
        private static DynamicContent[] items = new DynamicContent[20];
    }
}
