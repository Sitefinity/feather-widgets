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
            ServerOperationsFeather.DynamicModules().ImportModule(ModuleResource);
            ServerOperations.ModuleBuilder().ActivateModule(ModuleName, string.Empty, TransactionName);
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);

            for (int i = 0; i < this.dynamicTitles.Length; i++)
                ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(this.dynamicTitles[i], this.dynamicUrls[i]);
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
        }

        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressRelease.zip";
        private const string TransactionName = "Module Installations";
        private readonly string[] dynamicTitles = { "Boat", "Cat", "Dog", "Elephant" };
        private readonly string[] dynamicUrls = { "BoatUrl", "CatUrl", "DogUrl", "ElephantUrl" };
        private const string PageName = "TestPage";
    }
}
