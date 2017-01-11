using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// VerifyNewsItemsWhenOpenDetailsModeOfDynamicItem arragement.
    /// </summary>
    public class VerifyNewsItemsWhenOpenDetailsModeOfDynamicItem : TestArrangementBase
    { 
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            AuthenticationHelper.AuthenticateUser(this.AdminEmail, this.AdminPass, true);
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);
            Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(BootstrapTemplate);
            Guid pageId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageName, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerOperationsFeather.Pages().AddNewsWidgetToPage(pageId, PlaceHolderId);
            ServerOperationsFeather.Pages().AddDynamicWidgetToPage(pageId, "Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle", "PressArticle", "Press Articles MVC", PlaceHolderId);

            var pageId1 = ServerOperations.Pages().CreatePage(PageTitle);
            ServerOperationsFeather.Pages().AddNewsWidgetToPage(pageId1);
            ServerOperationsFeather.Pages().AddDynamicWidgetToPage(pageId1, "Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle", "PressArticle", "Press Articles MVC");

            ServerOperations.News().CreatePublishedNewsItem(NewsTitle1, NewsContent1, null);
            ServerOperations.News().CreatePublishedNewsItem(NewsTitle2, NewsContent2, null);
            ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(ItemTitle, ItemTitle + "Url");
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        /// 
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.News().DeleteAllNews();

            var providerName = string.Empty;
            if (ServerOperations.MultiSite().CheckIsMultisiteMode())
            {
                providerName = "dynamicContentProvider";
            }

            ServerOperationsFeather.DynamicModulePressArticle().DeleteAllDynamicItemsInProvider(providerName);
        }

        private const string BootstrapTemplate = "Bootstrap.default";
        private const string NewsContent1 = "News content1";
        private const string NewsTitle1 = "NewsTitle1";
        private const string NewsContent2 = "News content2";
        private const string NewsTitle2 = "NewsTitle2";
        private const string PlaceHolderId = "Contentplaceholder1";
        private const string ItemTitle = "DynamicItemTitle";
        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressReleaseWithCategoriesField.zip";
        private const string PageName = "TestPage";
        private const string PageTitle = "PageDefaultTemplate";
    }
}
