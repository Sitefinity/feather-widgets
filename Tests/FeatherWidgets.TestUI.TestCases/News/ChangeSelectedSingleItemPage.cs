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
    /// ChangeSelectedSingleItemPage test class.
    /// </summary>
    [TestClass]
    public class ChangeSelectedSingleItemPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ChangeSelectedSingleItemPage
        /// </summary>
        [TestMethod,
        Owner("Sitefinity Team 7"),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.NewsSelectors)]
        public void ChangeSelectedSingleItemPage()
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
            //// change single item page and verify selected page in designer
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectItemsInHierarchicalSelector(NewSingleItemPage);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().VerifySelectedItemsFromHierarchicalSelector(this.newSelectedItemsFromPageSelector);
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
        private const string SingleItemPage = "ChildPage0";
        private const string NewSingleItemPage = "ChildPage1";
        private readonly string[] selectedItemsFromPageSelector = new string[] { "News > ChildPage0" };
        private readonly string[] newSelectedItemsFromPageSelector = new string[] { "News > ChildPage0 > ChildPage1" };
    }
}
