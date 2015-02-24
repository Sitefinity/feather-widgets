using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Test arrangements for SelectItemInDynamicWidgetFromDifferentProvider
    /// </summary>
    public class SelectItemInDynamicWidgetFromDifferentProvider : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            string itemUrl = ItemName.ToLower();

            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);
            ServerOperations.Configuration().AddOpenAccessDynamicModuleProvider(ProviderName);
            ServerOperations.SystemManager().RestartApplication(false);

            ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticle(ItemName, itemUrl, Guid.Empty, Guid.Empty, null, ProviderName);

            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddDynamicWidgetToPage(pageId, ContentType, WidgetName, WidgetCaption);         
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles(ProviderName), ProviderName);
            ServerOperations.Configuration().RemoveOpenAccessDynamicModuleProvider(ProviderName);
            ServerOperations.SystemManager().RestartApplication(false);
        }

        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressReleaseWithCategoriesField.zip";
        private const string PageName = "TestPage";
        private const string ProviderName = "DynamicModulesSecondProvider";
        private const string ItemName = "PressArticleSecondProvider";
        private const string ContentType = "Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle";
        private const string WidgetName = "PressArticle";
        private const string WidgetCaption = "Press Articles MVC";
    }
}
