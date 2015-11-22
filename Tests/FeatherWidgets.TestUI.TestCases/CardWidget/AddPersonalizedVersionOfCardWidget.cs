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
    /// AddPersonalizedVersionOfCardWidget test class.
    /// </summary>
    [TestClass]
    public class AddPersonalizedVersionOfCardWidget_ : FeatherTestCase
    {
        /// <summary>
        /// UI test AddPersonalizedVersionOfCardWidget
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Card)]
        public void AddPersonalizedVersionOfCardWidget()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Card().CardWrapper().FillHeadingText(NotPersonalizedWidgetContent);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Personalization().PersonalizationInPages().ExpandMoreInWidgetByIndex();
            BAT.Wrappers().Backend().Personalization().PersonalizationInPages().AssertAddPersonalizedVersionLink();
            BAT.Wrappers().Backend().Personalization().PersonalizationInPages().SelectAddPersonalizedVersionLink();
            BATFeather.Wrappers().Backend().Card().CardWrapper().ClickAddButton();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Card().CardWrapper().FillHeadingText(PersonalizedWidgetContent);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            BATFeather.Wrappers().Frontend().Card().CardWrapper().VerifyCardWidgetContentOnFrontend(PersonalizedWidgetContent);
            BAT.Macros().User().LogOut();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            BATFeather.Wrappers().Frontend().Card().CardWrapper().VerifyCardWidgetContentOnFrontend(NotPersonalizedWidgetContent);
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
        private const string NotPersonalizedWidgetContent = "Not personalized widget";
        private const string PersonalizedWidgetContent = "Personalized widget";
        private const string CssClassesToApply = "Css to apply";
        private const string SegmentName = "Test segment";
    }
}
