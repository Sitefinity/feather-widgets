﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtOfTest.WebAii.Controls.HtmlControls;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.ContentBlocks
{
    /// <summary>
    /// ShareContentBlockFromPage test class.
    /// </summary>
    [TestClass]
    public class ShareContentBlockWithoutTitleFromPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ShareContentBlockWithoutTitleFromPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.ContentBlock1)]
        public void ShareContentBlockWithoutTitleFromPage()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationName);            
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksShareWrapper().ShareButton();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksShareWrapper().VerifyMessageTitleIsRequired();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksShareWrapper().CancelButton();
            this.VerifyIfSharedLabelExist();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().Modules().ContentBlocks(this.Culture);
            this.VerifyIfContentBlockExist();
        }

        /// <summary>
        /// Verify if shared label exist
        /// </summary>
        public void VerifyIfSharedLabelExist()
        {
            bool isExist = BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().VerifyContentBlockWidgetSharedLabel();
            Assert.IsFalse(isExist, "Shared label exists!");
        }

        /// <summary>
        /// Verify if content block exist
        /// </summary>
        /// <param name="contentBlockName">Content block name</param>
        public void VerifyIfContentBlockExist()
        {
            BAT.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().GetContentBlockDecisionScreen()
                .AssertIsPresent("Content block decision screen");
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

        private const string PageName = "ContentBlock";
        private const string WidgetName = "ContentBlock";
        private const string OperationName = "Share";
    }
}
