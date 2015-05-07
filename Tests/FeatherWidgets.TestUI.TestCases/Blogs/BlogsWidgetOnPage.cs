using System;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.Blogs
{
    /// <summary>
    /// This is a test class with UI tests for Blogs widget on page.
    /// </summary>
    [TestClass]
    public class BlogsWidgetOnPage : FeatherTestCase
    {
        /// <summary>
        /// Drags and drops blogs widget to page and verifies on the frontend.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Blogs)]
        public void DragAndDropBlogsWidgetToPage()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName);

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower());

            BATFeather.Wrappers().Frontend().CommonWrapper().VerifySelectedAnchorLink(BlogTitle, this.blogExpectedUrl);
            BATFeather.Wrappers().Frontend().CommonWrapper().VerifySelectedAnchorLink(PostTitle, this.postExpectedUrl);
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

        private const string WidgetName = "Blogs";
        private const string PageTitle = "PageWithBlogsWidget";
        private const string DefaultPageTitle = "BlogsDefaultPage";
        private const string BlogTitle = "TestBlog";
        private const string PostTitle = "post1";
        private readonly string blogExpectedUrl = string.Format("/BlogsDefaultPage/TestBlog");
        private readonly string postExpectedUrl = string.Format("/BlogsDefaultPage/TestBlog/{0}/{1:00}/{2:00}/post1", DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);
    }
}
