using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Test arrangements for InvalidateCacheWhenAddNewItem
    /// </summary>
    public class InvalidateCacheWhenAddNewItem : TestArrangementBase
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

            for (int i = 0; i < this.dynamicTitles.Length; i++)
                ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(this.dynamicTitles[i], this.dynamicUrls[i]);

            ServerOperationsFeather.Pages().AddDynamicWidgetToPage(pageId, "Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle", "PressArticle", "Press Articles MVC");
        }

        /// <summary>
        /// Server arrangement.
        /// </summary>
        [ServerArrangement]
        public void AddNewItem()
        {
            ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(NewTitles, NewUrls);
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

        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressReleaseWithCategoriesField.zip";
        private readonly string[] dynamicTitles = { "Boat", "Cat", "Dog", "Elephant" };
        private readonly string[] dynamicUrls = { "BoatUrl", "CatUrl", "DogUrl", "ElephantUrl" };
        private const string PageName = "TestPage";
        private const string NewTitles = "Angel";
        private const string NewUrls = "AngelUrl";
    }
}
