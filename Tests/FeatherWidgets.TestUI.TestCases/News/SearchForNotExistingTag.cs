using Feather.Widgets.TestUI.Framework;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatherWidgets.TestUI
{
    /// <summary>
    /// SearchForNotExistingTag test class.
    /// </summary>
    [TestClass]
    public class SearchForNotExistingTag_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SearchForNotExistingTag
        /// </summary>
        [TestMethod,
       Microsoft.VisualStudio.TestTools.UnitTesting.Owner("Feather team"),
       TestCategory(FeatherTestCategories.PagesAndContent)]
        public void SearchForNotExistingTag()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectWhichNewsToDisplay(WhichNewsToDisplay);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SelectTaxonomy(TaxonomyName);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SearchTagByTitle(TaxonTitle);
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().NoItemsFound();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().DoneSelectingButton();
            BATFeather.Wrappers().Backend().News().NewsWidgetEditContentScreenWrapper().SaveChanges();
            this.VerifyNewsOnTheBackend();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            this.VerifyNewsOnTheFrontend();
        }

        /// <summary>
        /// Verify news widget on the backend
        /// </summary>
        public void VerifyNewsOnTheBackend()
        {
            for (int i = 0; i < newsTitles.Length; i++)
            {
                BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, newsTitles[i]);
            }
        }

        /// <summary>
        /// Verify news widget on the frontend
        /// </summary>
        public void VerifyNewsOnTheFrontend()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            ActiveBrowser.WaitUntilReady();
            BATFeather.Wrappers().Frontend().News().NewsWrapper().VerifyNewsTitlesOnThePageFrontend(newsTitles);
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
        private const string TaxonTitle = "NotExistingTag";
        private string[] newsTitles = new string[] { "NewsTitle4", "NewsTitle3", "NewsTitle2", "NewsTitle1", "NewsTitle0" };
        private const string WidgetName = "News";
        private const string WhichNewsToDisplay = "Narrow selection by...";
        private const string TaxonomyName = "Tags";
    }
}
