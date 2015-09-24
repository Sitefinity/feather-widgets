﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.WebAii.Controls;

namespace FeatherWidgets.TestUI.TestCases.ContentBlocks
{
    /// <summary>
    /// DeactivateContentBlockModule test class.
    /// </summary>
    [TestClass]
    public class DeactivateContentBlockModule_ : FeatherTestCase
    {
        /// <summary>
        /// UI test DeactivateContentBlockModule
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.ContentBlock)]
        public void DeactivateContentBlockModule()
        {
            BAT.Arrange(this.TestName).ExecuteArrangement("DeactivateModule");

            this.VerifyPageBackend(PageName, WidgetName, ContentBlockContent);
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().VerifyContentOfContentBlockOnThePageFrontend(ContentBlockContent);

            BAT.Arrange(this.TestName).ExecuteArrangement("ActivateModule");

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().VerifyContentOfContentBlockOnThePageFrontend(ContentBlockContent);
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        /// <summary>
        /// Verify page backend
        /// </summary>
        /// <param name="pageName">Page name</param>
        /// <param name="widgetName">Widget name</param>
        /// <param name="widgetContent">Widget content</param>
        private void VerifyPageBackend(string pageName, string widgetName, string widgetContent)
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(pageName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(widgetName, widgetContent);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
        }

        private const string PageName = "ContentBlock";
        private const string ContentBlockContent = "Test content";
        private const string WidgetName = "ContentBlock";
        private const string ModuleName = "Content blocks";
    }
}
