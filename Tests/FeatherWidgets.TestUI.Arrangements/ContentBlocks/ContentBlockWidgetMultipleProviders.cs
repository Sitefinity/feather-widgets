﻿using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// ContentBlockWidgetMultipleProviders arrangement class.
    /// </summary>
    public class ContentBlockWidgetMultipleProviders : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);
            ServerOperations.ContentBlocks().CreateSecondDataProvider();
            Guid page1Id = ServerOperations.Pages().CreatePage(PageName);
            var providerName = ContentManager.GetManager().Provider.Name;
            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(page1Id, Content);
            ServerOperationsFeather.ContentBlockOperations().CreateContentBlock("Content Block 1", "Content 1", providerName);
            ServerOperationsFeather.ContentBlockOperations().CreateContentBlock("Content Block 2", "Content 2", SecondProviderName);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.ContentBlocks().DeleteAllContentBlocks(SecondProviderName);
            ServerOperations.ContentBlocks().DeleteAllContentBlocks();
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.ContentBlocks().RemoveSecondDataProvider();
        }

        private const string AdminUserName = "admin";
        private const string AdminPass = "admin@2";
        private const string PageName = "ContentBlock";
        private const string SecondProviderName = "ContentSecondDataProvider";
        private const string Content = "";
    }
}
