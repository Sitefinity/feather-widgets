using System;
using System.Linq;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.TestUI.Core.Utilities;

namespace FeatherWidgets.TestUI.TestCases.News
{
    /// <summary>
    /// VerifyPagingWhenPagesAreMoreThanTen test class.
    /// </summary>
    [TestClass]
    public class VerifyPagingWhenPagesAreMoreThanTen_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifyPagingWhenPagesAreMoreThanTen.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam4),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.News)]
        public void VerifyPagingWhenPagesAreMoreThanTen()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToListSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyCheckedRadioButtonOption(WidgetDesignerRadioButtonIds.UsePaging);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ChangePagingOrLimitValue("1", "Paging");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage(PageName.ToLower(), true, this.Culture);
            BATFeather.Wrappers().Frontend().CommonWrapper().NavigateToNextSectionUsingPager();
            Assert.IsTrue(ActiveBrowser.Url.EndsWith(PageName + "/11", StringComparison.OrdinalIgnoreCase));
            Assert.IsTrue(BATFeather.Wrappers().Frontend().News().NewsWrapper().IsNewsTitlesPresentOnThePageFrontend(new string[] { NewsTitle1 }));
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
        private const string WidgetName = "News";
        private const string NewsTitle1 = "NewsTitle1";
    }
}
