﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// NavigationWidgetAllPagesUnderCurrentlyOpenedPage arrangement class.
    /// </summary>
    public class NavigationWidgetAllPagesUnderCurrentlyOpenedPage : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            string templateName = ServerArrangementContext.GetCurrent().Values["templateName"];

            Guid templateId = ServerOperations.Templates().GetTemplateIdByTitle(templateName);
            Guid parentPageId = ServerOperations.Pages().CreatePage(PageName, templateId);
            Guid pageNodeId = ServerOperations.Pages().GetPageNodeId(parentPageId);

            FeatherServerOperations.Pages().AddMvcWidgetToPage(pageNodeId, typeof(NavigationController).FullName, WidgetCaption, PlaceHolderId);

            Guid childPage1Id = Guid.NewGuid();
            ServerOperations.Pages().CreatePage(ChildPage1, childPage1Id, pageNodeId);
            Guid childPage2Id = Guid.NewGuid();
            ServerOperations.Pages().CreatePage(ChildPage2, childPage2Id, pageNodeId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
        }

        private const string PageName = "ParentPage";
        private const string ChildPage1 = "ChildPage1";
        private const string ChildPage2 = "ChildPage2";
        private const string WidgetCaption = "Navigation";
        private const string PlaceHolderId = "Contentplaceholder1";
    }
}
