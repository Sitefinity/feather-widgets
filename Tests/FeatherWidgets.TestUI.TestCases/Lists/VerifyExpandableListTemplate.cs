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
    /// VerifyExpandableListTemplate_ test class.
    /// </summary>
    [TestClass]
    public class VerifyExpandableListTemplate_ : FeatherTestCase
    {
        /// <summary>
        /// UI test verifying Expandable list template, sort Last modified, no filter applied
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Lists),
        TestCategory(FeatherTestCategories.Selectors)]
        public void VerifyExpandableListTemplate()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);

            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton(0);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector(ListTitle);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifySelectedItemsFromFlatSelector(new string[] { ListTitle });

            BATFeather.Wrappers().Backend().Lists().ListsWidgetWrapper().SelectSortingOption(SortingOption);
            BATFeather.Wrappers().Backend().Lists().ListsWidgetWrapper().SelectListTemplate(ListTemplate);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());
            BATFeather.Wrappers().Frontend().Lists().ListsWidgetWrapper().VerifyExpandableListTemplate(ListTitle, this.listItems);
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
        private const string WidgetName = "Lists";
        private const string ListTitle = "Test list";
        private const string SortingOption = "Last modified";
        private const string ListTemplate = "ExpandableList";

        private readonly Dictionary<string, string> listItems = new Dictionary<string, string>()
                                                                {
                                                                    { "edited title", "edited content" },
                                                                    { "list item 3", "list content 3" }, 
                                                                    { "list item 1", "list content 1" }
                                                                };
    }
}
