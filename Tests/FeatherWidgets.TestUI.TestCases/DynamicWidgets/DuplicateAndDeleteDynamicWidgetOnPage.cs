using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.DynamicWidgets
{
    [TestClass]
    public class DuplicateAndDeleteDynamicWidgetOnPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test DuplicateAndDeleteDynamicWidgetOnPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.DynamicWidgets)]
        public void DuplicateAndDeleteDynamicWidgetOnPage()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationNameDuplicate);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.NavigatePageOnTheFrontend(this.dynamicTitlesDyplicated, DuplicatedCount);
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().SelectExtraOptionForWidget(OperationNameDelete);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.NavigatePageOnTheFrontend(this.dynamicTitles, DeletedCount);
        }

        /// <summary>
        /// Navigate page on the frontend
        /// </summary>
        public void NavigatePageOnTheFrontend(string[] dynamicTitles, int count)
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            Assert.IsTrue(BATFeather.Wrappers().Frontend().ModuleBuilder().ModuleBuilderWrapper().VerifyDynamicContentPresentOnTheFrontend(dynamicTitles));
            Assert.AreEqual(count, BATFeather.Wrappers().Frontend().ModuleBuilder().ModuleBuilderWrapper().ListWithDynamicWidgets().Count);
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

        private const string PageName = "TestPage";
        private const string WidgetName = "Press Articles MVC";
        private string[] dynamicTitlesDyplicated = { "Angel", "Angel" };
        private const int DuplicatedCount = 2;
        private const int DeletedCount = 1;
        private string[] dynamicTitles = { "Angel" };
        private const string OperationNameDuplicate = "Duplicate";
        private const string OperationNameDelete = "Delete";
    }
}
