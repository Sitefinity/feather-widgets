﻿using System;
using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.DynamicWidgets
{
    /// <summary>
    /// SearchAndSelectDynamicItemsByTag test class.
    /// </summary>
    [TestClass]
    public class SearchAndSelectDynamicItemsByTag_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SearchAndSelectDynamicItemsByTag
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.DynamicWidgets)]
        public void SearchAndSelectDynamicItemsByTag()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectWhichItemsToDisplay(WhichNewsToDisplay);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SelectCheckBox(TaxonomyName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SearchItemByTitle(NotExistingTaxonTitle);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().VerifyNoItemsFound();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SearchItemByTitle(TaxonTitle1);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector(TaxonTitle1);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SearchItemByTitle(TaxonTitle2);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector(TaxonTitle2);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromFlatSelector(new[] { TaxonTitle1, TaxonTitle2 });
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            this.VerifyDynamicItemsOnBackend(); 
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().ModuleBuilder().ModuleBuilderWrapper().VerifyDynamicContentPresentOnTheFrontend(new string[] { ItemsTitle + 2, ItemsTitle + 1 }));
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

        /// <summary>
        /// Verifies the dynamic items on backend.
        /// </summary>
        private void VerifyDynamicItemsOnBackend()
        {
            for (int i = 0; i < 5; i++)
            {             
                if (i > 0 && i <= 2)
                {
                    BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, ItemsTitle + i);
                }
                else
                {
                    BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContentForNotExistingContent(WidgetName, ItemsTitle + i);
                }
            }
        }

        private const string NotExistingTaxonTitle = "NotExistingTag";
        private const string TaxonTitle1 = "Tag1";
        private const string TaxonTitle2 = "Tag2";
        private const string ItemsTitle = "Title";
        private const string PageName = "TestPage";
        private const string WidgetName = "Press Articles MVC";     
        private const string WhichNewsToDisplay = "Narrow selection by...";
        private const string TaxonomyName = "Tags";
    }
}