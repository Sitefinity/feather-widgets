using System;
using System.Linq;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// VerifyPagingOnFrontendPageForDynamicWidget arrangement class.
    /// </summary>
    public class VerifyPagingOnFrontendPageForDynamicWidget : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            AuthenticationHelper.AuthenticateUser(this.AdminEmail, this.AdminPass, true);
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);

            Guid pageId = ServerOperations.Pages().CreatePage(PageName);

            for (int i = 1; i < 6; i++)
            {
                ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticle(ItemsTitle + i, ItemsTitle + i + "Url");         
            }

            ServerOperationsFeather.Pages().AddDynamicWidgetToPage(pageId, "Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle", "PressArticle", "Press Articles MVC");
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            AuthenticationHelper.AuthenticateUser(this.AdminEmail, this.AdminPass, true);
            ServerOperations.Pages().DeleteAllPages();            

            var providerName = string.Empty;
            if (ServerOperations.MultiSite().CheckIsMultisiteMode())
            {
                providerName = "dynamicContentProvider";
            }

            ServerOperationsFeather.DynamicModulePressArticle().DeleteAllDynamicItemsInProvider(providerName);
        }

        private const string PageName = "TestPage";
        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressReleaseWithCategoriesField.zip";
        private const string ItemsTitle = "Title";
    }
}
