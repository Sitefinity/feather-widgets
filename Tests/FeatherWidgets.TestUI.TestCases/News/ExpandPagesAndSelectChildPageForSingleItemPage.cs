using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI
{
    /// <summary>
    /// ExpandPagesAndSelectChildPageForSingleItemPage test class.
    /// </summary>
    [TestClass]
    public class ExpandPagesAndSelectChildPageForSingleItemPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ExpandPagesAndSelectChildPageForSingleItemPage
        /// </summary>
        [TestMethod,
        Owner("Sitefinity Team 7"),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.NewsSelectors)]
        public void ExpandPagesAndSelectChildPageForSingleItemPage()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SingleItemSettingsTab();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectExistingPage();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectItemsInHierarchicalSelector(SingleItemPage);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().VerifySelectedItemsFromHierarchicalSelector(this.selectedItemsFromPageSelector);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
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

        private const string PageName = "News";
        private const string WidgetName = "News";
        private const string SingleItemPage = "ChildPage9";
        private readonly string[] selectedItemsFromPageSelector = new string[] { "News > ChildPage0 > ChildPage1 > ChildPage2 > ChildPage3 > ChildPage4 > ChildPage5 > ChildPage6 > ChildPage7 > ChildPage8 > ChildPage9" };
    }
}
