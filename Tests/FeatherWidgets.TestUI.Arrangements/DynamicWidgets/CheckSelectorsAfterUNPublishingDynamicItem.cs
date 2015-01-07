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
    /// CheckSelectorsAfterUNPublishingDynamicItem arragement.
    /// </summary>
    public class CheckSelectorsAfterUNPublishingDynamicItem : ITestArrangement
    {
        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressRelease.zip";
        private const string TransactionName = "Module Installations";
        private const string DynamicItemTitle = "Dynamic Item Title";
        private const string PageName = "TestPage";    
        private static DynamicContent[] items = new DynamicContent[20];
       
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperationsFeather.DynamicModules().ImportModule(ModuleResource);
            ServerOperations.ModuleBuilder().ActivateModule(ModuleName, string.Empty, TransactionName);
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);

            for (int i = 0; i < 20; i++)
            {
                items[i] = ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(DynamicItemTitle + i, DynamicItemTitle + i + "Url");               
            }

            ServerOperationsFeather.Pages().AddDynamicWidgetToPage(pageId, "Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle", "PressArticle", "Press Articles MVC");
        }

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
            ServerOperations.ModuleBuilder().DeleteAllModules(string.Empty, TransactionName);
        }
    }
}
