using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Test arrangements for InvalidateCacheWhenAddNewItem
    /// </summary>
    public class InvalidateCacheWhenAddNewItem : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperationsFeather.DynamicModules().ImportModule(ModuleResource);
            ServerOperations.ModuleBuilder().ActivateModule(ModuleName, string.Empty, TransactionName);
            ServerOperations.Pages().CreatePage(PageName);

            for (int i = 0; i < this.dynamicTitles.Length; i++)
                ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(this.dynamicTitles[i], this.dynamicUrls[i]);
        }

        /// <summary>
        /// Server arrangement.
        /// </summary>
        [ServerArrangement]
        public void AddNewItem()
        {
            ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(this.newTitles, this.newUrls);
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
        private const string ResolveType = "Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle";
        private const string TransactionName = "Module Installations";
        private const string WidgetName = "PressArticle";
        private string[] dynamicTitles = { "Boat", "Cat", "Dog", "Elephant" };
        private string[] dynamicUrls = { "BoatUrl", "CatUrl", "DogUrl", "ElephantUrl" };
        private const string PageName = "TestPage";
        private string newTitles = "Angel";
        private string newUrls = "AngelUrl";
    }
}
