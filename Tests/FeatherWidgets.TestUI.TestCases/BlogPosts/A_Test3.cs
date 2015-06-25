using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.BlogPosts
{
    /// <summary>
    /// Test class.
    /// </summary>
    [TestClass]
    public class A_Test3_ : FeatherTestCase
    {
        /// <summary>
        /// Test method
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team2),
        TestCategory(FeatherTestCategories.PagesAndContent)]
        public void A_Test3()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenCreatePageWindow();
            BAT.Wrappers().Backend().Pages().CreatePageWrapper().SetPageTitle(PageName);
            BAT.Wrappers().Backend().Pages().PagesWrapper().SavePageDataAndContinue();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().WaitUntilReady();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Wrappers().Backend().Pages().PagesWrapper().DeleteItemFromActionsMenu(PageName);
            BAT.Wrappers().Backend().Pages().PagesWrapper().DeleteAndConfirm();
        }

        private const string PageName = "TestPage3";
    }
}
