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
    /// SubmitCommentForDynamicItemLoggedUserOnBootstrapPage arrangement class.
    /// </summary>
    public class SubmitCommentForDynamicItemLoggedUserOnBootstrapPage : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            Guid pageId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageName, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);
            ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(DynamicTitle, DynamicUrl);
            ServerOperationsFeather.Pages().AddDynamicWidgetToPage(pageId, ResolveType, WidgetName, WidgetCaptionDynamicWidget, "Contentplaceholder1");
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperationsFeather.DynamicModulePressArticle().DeleteDynamicItems(ServerOperationsFeather.DynamicModulePressArticle().RetrieveCollectionOfPressArticles());
            ServerOperations.ModuleBuilder().DeleteAllModules(string.Empty, TransactionName);  
        }

        private const string PageName = "DynamicPage";
        private const string PageTemplateName = "Bootstrap.default";
        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressReleaseWithCategoriesField.zip";
        private const string ResolveType = "Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle";
        private const string WidgetName = "PressArticle";
        private const string WidgetCaptionDynamicWidget = "Press Articles MVC";
        private const string DynamicTitle = "Angel";
        private const string DynamicUrl = "AngelUrl";
        private const string TransactionName = "Module Installations";
    }
}
