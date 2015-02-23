using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.ContentBlocks
{
    /// <summary>
    /// ContentBlockWidgetWithShareButtonsOnPageBasedOnBootstrap test class.
    /// </summary>
    [TestClass]
    public class ContentBlockWidgetWithShareButtonsOnPageBasedOnBootstrap_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ContentBlockWidgetWithShareButtonsOnPageBasedOnBootstrap
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.ContentBlock),
        TestCategory(FeatherTestCategories.SocialShare)]
        public void ContentBlockWidgetWithShareButtonsOnPageBasedOnBootstrap()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().AdvanceButtonSelecting();
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().EnableSocialShareButtons(IsEnabled);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();

            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetNameSocial);
            BATFeather.Wrappers().Backend().SocialShare().SocialShareWidgetEditWrapper().SelectSocialShareOptions(this.optionTitlesToSelect);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().VerifyContentOfContentBlockOnThePageFrontend(ContentBlockContent);
            BATFeather.Wrappers().Frontend().ContentBlock().ContentBlockWrapper().VerifySocialShareOptionsInContentBlockOnFrontend(4, SocialShareOptions.Facebook, SocialShareOptions.Tweeter, SocialShareOptions.GooglePlus, SocialShareOptions.LinkedIn);
            BATFeather.Wrappers().Frontend().SocialShare().SocialShareWrapper().VerifySocialShareOptionsOnFrontend(
                6, 
                SocialShareOptions.Facebook, 
                SocialShareOptions.Tweeter,
                SocialShareOptions.GooglePlus,
                SocialShareOptions.Digg,
                SocialShareOptions.Blogger,
                SocialShareOptions.LinkedIn);
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
        private const string WidgetNameSocial = "Social share";
        private const string ContentBlockContent = "Test content";
        private const string IsEnabled = "True";
        private readonly string[] optionTitlesToSelect = new string[] { "Blogger", "Digg" };
    }
}
