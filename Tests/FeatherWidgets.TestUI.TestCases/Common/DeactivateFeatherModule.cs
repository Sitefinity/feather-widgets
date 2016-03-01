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
using Telerik.WebAii.Controls;

namespace FeatherWidgets.TestUI.TestCases.Common
{
    /// <summary>
    /// DeactivateFeatherModule test class.
    /// </summary>
    [TestClass]
    public class DeactivateFeatherModule_ : FeatherTestCase
    {
        /// <summary>
        /// UI test DeactivateFeatherModule
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent)]
        public void DeactivateFeatherModule()
        {
            BAT.Arrange(this.TestName).ExecuteArrangement("DeactivateModule");

            this.VerifyPageBackend(PageName, WidgetName, ContentBlockContent, false);

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            BAT.Wrappers().Backend().ModulesAndServices().FrontendWrapper().VerifyFrontendForNotExistingModule(ContentBlockContent, PageName);


            BAT.Arrange(this.TestName).ExecuteArrangement("ActivateModule");

            this.VerifyPageBackend(PageName, WidgetName, ContentBlockContent, true);

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
        /// <param name="isModuleActvie">if set to <c>true</c> if module is actvie.</param>
        private void VerifyPageBackend(string pageName, string widgetName, string widgetContent, bool isModuleActvie)
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(pageName);
            Assert.AreEqual(isModuleActvie, BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().IsAnyMvcWidgetPersentInToolbox());
            
            if(isModuleActvie)
                BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(widgetName, widgetContent);
            
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
        }

        private const string PageName = "ContentBlock";
        private const string ContentBlockContent = "Test content";
        private const string WidgetName = "ContentBlock";
        private const string ModuleName = "Feather";
    }
}
