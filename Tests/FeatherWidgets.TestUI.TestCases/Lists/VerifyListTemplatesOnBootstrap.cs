using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;

namespace FeatherWidgets.TestUI.TestCases.Lists
{
    /// <summary>
    /// VerifyListTemplatesOnBootstrap_ test class.
    /// </summary>
    [TestClass]
    public class VerifyListTemplatesOnBootstrap_ : FeatherTestCase
    {
        /// <summary>
        /// UI test verifying list templates on Bootstrap pages
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Lists),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void VerifyListTemplatesOnBootstrap()
        {
            BAT.Macros().NavigateTo().CustomPage("~/" + AnchorListPage.ToLower());
            BATFeather.Wrappers().Frontend().Lists().ListsWidgetWrapper().VerifyAnchorListTemplate(ListTitle, this.listItems);

            BAT.Macros().NavigateTo().CustomPage("~/" + SimpleListPage.ToLower());
            BATFeather.Wrappers().Frontend().Lists().ListsWidgetWrapper().VerifySimpleListTemplate(ListTitle, this.listItemTitles);

            BAT.Macros().NavigateTo().CustomPage("~/" + PagesListPage.ToLower());
            BATFeather.Wrappers().Frontend().Lists().ListsWidgetWrapper().VerifyPagesListTemplate(ListTitle, this.listItems);
            
            BAT.Macros().NavigateTo().CustomPage("~/" + ExpandedListPage.ToLower());
            BATFeather.Wrappers().Frontend().Lists().ListsWidgetWrapper().VerifyExpandedListTemplateOnBootstrap(ListTitle, this.listItems);

            BAT.Macros().NavigateTo().CustomPage("~/" + ExpandableListPage.ToLower());
            BATFeather.Wrappers().Frontend().Lists().ListsWidgetWrapper().VerifyExpandableListTemplateOnBootstrap(ListTitle, this.listItems);
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

        private const string AnchorListPage = "AnchorListPage";
        private const string SimpleListPage = "SimpleListPage";
        private const string PagesListPage = "PagesListPage";
        private const string ExpandedListPage = "ExpandedListPage";
        private const string ExpandableListPage = "ExpandableListPage";

        private const string ListTitle = "Test list";
        private readonly string[] listItemTitles = new string[] { "list item 1", "list item 2", "list item 3" };

        private readonly Dictionary<string, string> listItems = new Dictionary<string, string>()
                                                                {
                                                                    { "list item 1", "list content 1" },
                                                                    { "list item 2", "list content 2" },
                                                                    { "list item 3", "list content 3" }
                                                                };
    }
}
