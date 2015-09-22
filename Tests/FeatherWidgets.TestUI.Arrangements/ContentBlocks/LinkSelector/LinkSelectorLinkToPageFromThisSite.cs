﻿using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// This is a test class with server site methods for Link selector and page from this site option.
    /// </summary>
    public class LinkSelectorLinkToPageFromThisSite : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);
            Guid selectorPageId = ServerOperations.Pages().CreatePage(LinkSelectorPageName);
            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(selectorPageId);

            Guid parentPageId = ServerOperations.Pages().CreatePage(ParentPageName);

            Guid childPageId = Guid.NewGuid();
            ServerOperations.Pages().CreatePage(ChildPageName, childPageId, parentPageId);    
        }

        [ServerArrangement]
        public void GetSiteMapRootNodeId()
        {
            // We need the siteMapRootNodeId in order to form the sfref later in the UI test.
            Guid siteMapRootNodeId = SystemManager.CurrentContext.CurrentSite.SiteMapRootNodeId;

            ServerArrangementContext.GetCurrent().Values.Add("siteMapRootNodeId", siteMapRootNodeId.ToString());
        }

        [ServerArrangement]
        public void GetChildPageId()
        {
            // We need the child page Id in order to form the sfref later in the UI test.
            Guid childPageId = ServerOperations.Pages().GetPageId(ChildPageName);

            ServerArrangementContext.GetCurrent().Values.Add("childPageId", childPageId.ToString());
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
        }

        private const string AdminUserName = "admin";
        private const string AdminPass = "admin@2";
        private const string LinkSelectorPageName = "PageWithContentBlockAndLinkSelector";
        private const string ParentPageName = "ParentPage";
        private const string ChildPageName = "ChildPage";
    }
}
