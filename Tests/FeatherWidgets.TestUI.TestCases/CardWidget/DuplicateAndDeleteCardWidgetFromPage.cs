using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.CardWidget
{
    /// <summary>
    /// DuplicateAndDeleteCardWidgetFromPage test class.
    /// </summary>
    [TestClass]
    public class DuplicateAndDeleteCardWidgetFromPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test DuplicateAndDeleteCardWidgetFromPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Card)]
        public void DuplicateAndDeleteCardWidgetFromPage()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Card().CardWrapper().FillHeadingText(HeadingText);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Card().CardWrapper().FillHeadingText(HeadingText2);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            BATFeather.Wrappers().Frontend().Card().CardWrapper().VerifyCardWidgetContentOnFrontend(HeadingText);
            BATFeather.Wrappers().Frontend().Card().CardWrapper().VerifyCardWidgetContentOnFrontend(HeadingText2);

            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationNameDelete);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            BATFeather.Wrappers().Frontend().Card().CardWrapper().VerifyCardWidgetContentOnFrontend(HeadingText);
            BATFeather.Wrappers().Frontend().Card().CardWrapper().VerifyCardWidgetContentNotPresentedOnFrontend(HeadingText2);
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

        private const string PageName = "CardPage";
        private const string WidgetName = "Card";
        private const string HeadingText = "Heading text1";
        private const string HeadingText2 = "Heading text2";
        private const string OperationName = "Duplicate";
        private const string OperationNameDelete = "Delete";
    }
}
