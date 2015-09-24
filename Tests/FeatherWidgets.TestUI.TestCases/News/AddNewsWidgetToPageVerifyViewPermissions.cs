﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.TestUI.Framework.Framework.ElementMap.Permissions;

namespace FeatherWidgets.TestUI.TestCases.News
{
    /// <summary>
    /// AddNewsWidgetToPageVerifyViewPermissions test class.
    /// </summary>
    [TestClass]
    public class AddNewsWidgetToPageVerifyViewPermissions_ : FeatherTestCase
    {
        /// <summary>
        /// UI test AddNewsWidgetToPageVerifyViewPermissions
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.News)]
        public void AddNewsWidgetToPageVerifyViewPermissions()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationName);
            BAT.Wrappers().Backend().Permissions().PermissionsContentWrapper().ClickChangePermissionsButton(PermissionTypes.View);
            BAT.Wrappers().Backend().Permissions().PermissionsContentWrapper().SelectAndAddRole("Authenticated");
            BAT.Wrappers().Backend().Permissions().PermissionsContentWrapper().ClickBackButton();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().WaitUntilReady();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().User().LogOut();
            ActiveBrowser.RefreshDomTree();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, culture: this.Culture);

            Assert.IsFalse(ActiveBrowser.ContainsText(NewsTitle), "News title was found but it shouldn't");
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

        private const string PageName = "NewsPage";
        private const string OperationName = "Permissions";
        private const string NewsTitle = "NewsTitle";
    }
}
