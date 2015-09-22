﻿using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// CheckSelectorsAfterSelectUnselectAndUNPublishingDynamicItem arragement.
    /// </summary>
    public class CheckSelectorsAfterSelectUnselectAndUNPublishingDynamicItem : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);
            
            Guid pageId = ServerOperations.Pages().CreatePage(PageName);

            for (int i = 0; i < 20; i++)
            {
                items[i] = ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticleItem(DynamicItemTitle + i, DynamicItemTitle + i + "Url");               
            }

            ServerOperationsFeather.Pages().AddDynamicWidgetToPage(pageId, "Telerik.Sitefinity.DynamicTypes.Model.PressRelease.PressArticle", "PressArticle", "Press Articles MVC");
        }

        /// <summary>
        /// Unpublish dynamic items arrangement method.
        /// </summary>
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
            var providerName = string.Empty;
            if (ServerOperations.MultiSite().CheckIsMultisiteMode())
            {
                providerName = "dynamicContentProvider";
            }

            ServerOperationsFeather.DynamicModulePressArticle().DeleteAllDynamicItemsInProvider(providerName);

            ServerOperations.Pages().DeleteAllPages();           
        }

        private const string AdminUserName = "admin";
        private const string AdminPass = "admin@2";
        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressReleaseWithCategoriesField.zip";
        private const string DynamicItemTitle = "Dynamic Item Title";
        private const string PageName = "TestPage";
        private static DynamicContent[] items = new DynamicContent[20];
    }
}
