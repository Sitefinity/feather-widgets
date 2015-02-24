using System;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.DynamicWidgets
{
    /// <summary>
    /// SelectItemInDynamicWidgetFromDifferentProvider test class.
    /// </summary>
    [TestClass]
    public class SelectItemInDynamicWidgetFromDifferentProvider_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SelectItemInDynamicWidgetFromDifferentProvider
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.DynamicWidgets)]
        public void SelectItemInDynamicWidgetFromDifferentProvider()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectProvider(SecondProviderName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectWhichItemsToDisplay(WhichItemsToDisplay);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().WaitForItemsToAppear(ItemsCount);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector(this.itemsTitles[0]);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, this.itemsTitles[0]);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            Assert.IsTrue(BATFeather.Wrappers().Frontend().ModuleBuilder().ModuleBuilderWrapper().VerifyDynamicContentPresentOnTheFrontend(this.itemsTitles));
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

        private const string SecondProviderName = "DynamicModulesSecondProvider";
        private const string PageName = "TestPage";
        private const string WidgetName = "Press Articles MVC";
        private const string WhichItemsToDisplay = "Selected PressArticles";
        private const int ItemsCount = 1;
        private readonly string[] itemsTitles = new string[] { "PressArticleSecondProvider" };
    }
}