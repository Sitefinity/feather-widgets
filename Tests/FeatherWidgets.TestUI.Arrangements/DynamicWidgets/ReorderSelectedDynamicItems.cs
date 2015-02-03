using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// ReorderSelectedDynamicItems arragement.
    /// </summary>
    public class ReorderSelectedDynamicItems : ITestArrangement
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
                ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(ItemTitle + i, ItemTitle + i + "Url");
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
        }

        private const string ItemTitle = "Dynamic Item Title";
        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressRelease.zip";
        private const string PageName = "TestPage";
    }
}