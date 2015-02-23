using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.News
{
    /// <summary>
    /// ActivateAndDeactivateNewsModule test class.
    /// </summary>
    [TestClass]
    public class ZActivateAndDeactivateNewsModule_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ActivateAndDeactivateNewsModule
        /// Ignored because of bug ID 145065
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.News)]
        public void ZActivateAndDeactivateNewsModule()
        {
            BAT.Arrange(this.TestName).ExecuteArrangement("DeactivateNewsModule");
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);

            ////This step is missed because of bug
            ////BATFeather.Wrappers().Backend().Widgets().WidgetDesignerContentScreenWrapper().CheckInactiveNewsWidget();

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, string.Empty);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            Assert.IsTrue(BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent().InnerText.Contains(string.Empty), "Page is not empty");
            BAT.Arrange(this.TestName).ExecuteArrangement("ActivateNewsModule");
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, NewsTitle1);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            Assert.IsTrue(BATFeather.Wrappers().Frontend().News().NewsWrapper().IsNewsTitlesPresentOnThePageFrontend(this.newsTitles));
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
        private const string NewsTitle1 = "NewsTitle1";
        private const string WidgetName = "News";
        private readonly string[] newsTitles = new string[] { NewsTitle1 };
    }
}
