﻿using System;
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
    /// This is test class for SearchForSingleItemPageAndVerifyBreadcrumb.
    /// </summary>
    [TestClass]
    public class SearchForSingleItemPageAndVerifyBreadcrumb_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SearchForSingleItemPageAndVerifyBreadcrumb
        /// </summary>
        [TestMethod,
        Owner("Sitefinity Team 7"),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.NewsSelectors)]
        public void SearchForSingleItemPageAndVerifyBreadcrumb()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SingleItemSettingsTab();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectExistingPage();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SearchItemByTitle(SearchText);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().WaitForItemsToAppear(SearchResultsCount);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().CheckBreadcrumbAfterSearchInFlatSelector(SearchText, ResultPageBreadcrumb);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectItemsInFlatSelector(SearchText);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().VerifySelectedItemsFromHierarchicalSelector(this.selectedItemsFromPageSelector);
            //// save changes and reopen widget designer to ensure selection is saved
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SingleItemSettingsTab();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().VerifySelectedItemsFromHierarchicalSelector(this.selectedItemsFromPageSelector);
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
        private const string SearchText = "ChildPage6";
        private const string ResultPageBreadcrumb = "Under News > ChildPage0 > ChildPage1 > ChildPage2 > ChildPage3 > ChildPage4 > ChildPage5";
        private const int SearchResultsCount = 1;

        private readonly string[] selectedItemsFromPageSelector = new string[] { "News > ChildPage0 > ChildPage1 > ChildPage2 > ChildPage3 > ChildPage4 > ChildPage5 > ChildPage6" };
    }
}
