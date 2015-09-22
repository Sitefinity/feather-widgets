﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.TestUI.Framework.Utilities;

namespace FeatherWidgets.TestUI.TestCases.DynamicWidgets
{
    [TestClass]
    public class DuplicateAndDeleteDynamicWidgetOnPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test DuplicateAndDeleteDynamicWidgetOnPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.DynamicWidgets)]
        public void DuplicateAndDeleteDynamicWidgetOnPage()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/sitefinity/pages", false));
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().User().EnsureAdminLoggedIn());
            BAT.Macros().NavigateTo().Pages(this.Culture);   
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationNameDuplicate);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.NavigatePageOnTheFrontend(this.dynamicTitlesDyplicated);
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationNameDelete);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.NavigatePageOnTheFrontend(this.dynamicTitles);
        }

        /// <summary>
        /// Navigate page on the frontend
        /// </summary>
        public void NavigatePageOnTheFrontend(string[] dynamicTitles)
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().ModuleBuilder().ModuleBuilderWrapper().VerifyDynamicContentPresentOnTheFrontend(dynamicTitles));            
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string PageName = "TestPage";
        private const string WidgetName = "Press Articles MVC";
        private string[] dynamicTitlesDyplicated = { "Angel", "Angel" };
        private string[] dynamicTitles = { "Angel" };
        private const string OperationNameDuplicate = "Duplicate";
        private const string OperationNameDelete = "Delete";
    }
}
