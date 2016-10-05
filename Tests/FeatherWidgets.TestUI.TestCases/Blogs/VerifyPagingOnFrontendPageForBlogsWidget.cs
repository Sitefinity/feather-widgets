﻿using System;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Widgets;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;

namespace FeatherWidgets.TestUI.TestCases.Blogs
{
    /// <summary>
    /// VerifyPagingOnFrontendPageForBlogsWidget_ test class.
    /// </summary>
    [TestClass]
    public class VerifyPagingOnFrontendPageForBlogsWidget_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifyPagingOnFrontendPageForBlogsWidget
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam7),
        TestCategory(FeatherTestCategories.PagesAndContent), 
        TestCategory(FeatherTestCategories.Blogs)]
        public void VerifyPagingOnFrontendPageForBlogsWidget()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/sitefinity/pages", true, null, new HtmlFindExpression("class=~sfMain")));
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToListSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyCheckedRadioButtonOption(WidgetDesignerRadioButtonIds.UsePaging);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ChangePagingOrLimitValue("2", "Paging");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyPageValue("2", "Paging");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyPageValue("20", "Limit");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToListSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyCheckedRadioButtonOption(WidgetDesignerRadioButtonIds.UsePaging);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyPageValue("2", "Paging");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyPageValue("20", "Limit");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().PressCancelButton();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Blogs().BlogsWrapper().IsBlogTitlesPresentOnThePageFrontend(new string[] { BlogTitle5, BlogTitle4 }));
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Blogs().BlogsWrapper().IsBlogTitlesPresentOnThePageFrontend(new string[] { BlogTitle3, BlogTitle2, BlogTitle1 }));
            BATFeather.Wrappers().Frontend().CommonWrapper().NavigateToPageUsingPager("2", 3);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Blogs().BlogsWrapper().IsBlogTitlesPresentOnThePageFrontend(new string[] { BlogTitle3, BlogTitle2 }));
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Blogs().BlogsWrapper().IsBlogTitlesPresentOnThePageFrontend(new string[] { BlogTitle5, BlogTitle4, BlogTitle1 }));
            BATFeather.Wrappers().Frontend().CommonWrapper().NavigateToPageUsingPager("3", 3);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Blogs().BlogsWrapper().IsBlogTitlesPresentOnThePageFrontend(new string[] { BlogTitle1 }));
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Blogs().BlogsWrapper().IsBlogTitlesPresentOnThePageFrontend(new string[] { BlogTitle5, BlogTitle4, BlogTitle3, BlogTitle2 }));
            BATFeather.Wrappers().Frontend().CommonWrapper().NavigateToPageUsingPager("1", 3);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Blogs().BlogsWrapper().IsBlogTitlesPresentOnThePageFrontend(new string[] { BlogTitle5, BlogTitle4 }));
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Blogs().BlogsWrapper().IsBlogTitlesPresentOnThePageFrontend(new string[] { BlogTitle3, BlogTitle2, BlogTitle1 }));
            BAT.Macros().NavigateTo().Pages(this.Culture);
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

        private const string PageName = "BlogsPage";
        private const string WidgetName = "Blogs";
        private const string BlogTitle1 = "TestBlog1";
        private const string BlogTitle2 = "TestBlog2";
        private const string BlogTitle3 = "TestBlog3";
        private const string BlogTitle4 = "TestBlog4";
        private const string BlogTitle5 = "TestBlog5";
    }
}
