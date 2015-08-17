using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Test arrangements for DuplicateAndDeleteDynamicWidgetOnPage
    /// </summary>
    public class DuplicateAndDeleteDynamicWidgetOnPage : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);

            ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(DynamicTitle, DynamicUrl);
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
            ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles());
        }

        private const string AdminUserName = "admin";
        private const string AdminPass = "admin@2";
        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressReleaseWithCategoriesField.zip";
        private const string ResolveType = "Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle";
        private const string WidgetName = "PressArticle";
        private const string WidgetCaptionDynamicWidget = "Press Articles MVC";
        private const string DynamicTitle = "Angel";
        private const string DynamicUrl = "AngelUrl";
        private const string PageName = "TestPage";
    }
}
