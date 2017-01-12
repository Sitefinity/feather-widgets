using System;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;
using ArtOfTest.WebAii.Core;


namespace FeatherWidgets.TestUI.TestCases.News
{
    /// <summary>
    /// VerifyNewsItemsWhenOpenDetailsModeOfDynamicItem test class.
    /// </summary>
    [TestClass]
    public class VerifyNewsItemsWhenOpenDetailsModeOfDynamicItem_ : FeatherTestCase
    {    /// <summary>
        /// UI test VerifyNewsItemsWhenOpenDetailsModeOfDynamicItem
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.News),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void VerifyNewsItemsWhenOpenDetailsModeOfDynamicItem()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().User().EnsureAdminLoggedIn());
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/sitefinity/pages", true, null, new HtmlFindExpression("class=~sfMain")));
            //Verify News Items when open detail mode ot dynamic item in frontend Page with MVC template
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().News().NewsWrapper().IsNewsTitlesPresentOnThePageFrontend(this.newsTitle1));
            Assert.IsTrue(BATFeather.Wrappers().Frontend().News().NewsWrapper().IsNewsTitlesPresentOnThePageFrontend(this.newsTitle2));
			Assert.IsTrue(BATFeather.Wrappers().Frontend().ModuleBuilder().ModuleBuilderWrapper().VerifyDynamicContentPresentOnTheFrontend(this.dynamicItemTitle));
            BATFeather.Wrappers().Frontend().CommonWrapper().VerifySelectedAnchorLink(DynamicItemTitle, dynamicItemExpectedUrl);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().News().NewsWrapper().IsNewsTitlesPresentOnThePageFrontend(this.newsTitle1));
            Assert.IsTrue(BATFeather.Wrappers().Frontend().News().NewsWrapper().IsNewsTitlesPresentOnThePageFrontend(this.newsTitle2));
            Assert.IsTrue(BATFeather.Wrappers().Frontend().ModuleBuilder().ModuleBuilderWrapper().VerifyDynamicContentPresentOnTheFrontend(this.dynamicItemTitle));

            //Verify News Items when open detail mode ot dynamic item in frontend Page with Hybrid template
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), false, this.Culture);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().News().NewsWrapper().IsNewsTitlesPresentOnThePageFrontend(this.newsTitle1));
            Assert.IsTrue(BATFeather.Wrappers().Frontend().News().NewsWrapper().IsNewsTitlesPresentOnThePageFrontend(this.newsTitle2));
            Assert.IsTrue(BATFeather.Wrappers().Frontend().ModuleBuilder().ModuleBuilderWrapper().VerifyDynamicContentPresentOnTheFrontend(this.dynamicItemTitle));
            BATFeather.Wrappers().Frontend().CommonWrapper().VerifySelectedAnchorLink(DynamicItemTitle, dynamicItemExpectedUrl1);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().News().NewsWrapper().IsNewsTitlesPresentOnThePageFrontend(this.newsTitle1));
            Assert.IsTrue(BATFeather.Wrappers().Frontend().News().NewsWrapper().IsNewsTitlesPresentOnThePageFrontend(this.newsTitle2));
            Assert.IsTrue(BATFeather.Wrappers().Frontend().ModuleBuilder().ModuleBuilderWrapper().VerifyDynamicContentPresentOnTheFrontend(this.dynamicItemTitle));
        }
        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {

            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// 
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string BootstrapTemplate = "Bootstrap.default";
        private const string PageName = "TestPage";
        private const string PageTitle = "PageDefaultTemplate";
        private const string NewsTitle1 = "NewsTitle1";
        private const string NewsTitle2 = "NewsTitle2";
        private const string DynamicItemTitle = "DynamicItemTitle";
        private readonly string[] newsTitle1 = new string[] { NewsTitle1 };
        private readonly string[] newsTitle2 = new string[] { NewsTitle2 };
        private readonly string[] dynamicItemTitle = new string[] { DynamicItemTitle };
        private readonly string dynamicItemExpectedUrl = string.Format("/TestPage/DynamicItemTitleUrl");
        private readonly string dynamicItemExpectedUrl1 = string.Format("/PageDefaultTemplate/DynamicItemTitleUrl");
    }
}
