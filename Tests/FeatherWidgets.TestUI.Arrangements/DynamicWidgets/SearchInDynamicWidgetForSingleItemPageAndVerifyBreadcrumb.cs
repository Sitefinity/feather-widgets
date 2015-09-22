﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// SearchForSingleItemPageAndVerifyBreadcrumb arrangement class.
    /// </summary>
    public class SearchInDynamicWidgetForSingleItemPageAndVerifyBreadcrumb : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);

            Guid parentPageId = ServerOperations.Pages().CreatePage(PageName);

            Guid currentChildPageId = Guid.NewGuid();
            for (int i = 0; i < PageHierarchyLevelsCount; i++)
            {
                ServerOperations.Pages().CreatePage(ChildPagesPrefix + i, currentChildPageId, parentPageId);
                parentPageId = currentChildPageId;

                currentChildPageId = Guid.NewGuid();
            }
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();

            var providerName = string.Empty;
            if (ServerOperations.MultiSite().CheckIsMultisiteMode())
            {
                providerName = "dynamicContentProvider";
            }

            ServerOperationsFeather.DynamicModulePressArticle().DeleteAllDynamicItemsInProvider(providerName);
        }

        private const string PageName = "TestPage";
        private const string ChildPagesPrefix = "ChildPage";
        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressReleaseWithCategoriesField.zip";

        private const int PageHierarchyLevelsCount = 10; 
    }
}
