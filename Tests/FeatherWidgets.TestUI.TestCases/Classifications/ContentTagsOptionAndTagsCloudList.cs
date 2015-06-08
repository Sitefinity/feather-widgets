using System;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Classifications;

namespace FeatherWidgets.TestUI.TestCases.Classifications
{
    /// <summary>
    /// ContentTagsOptionAndTagsCloudList test class.
    /// </summary>
    [TestClass]
    public class ContentTagsOptionAndTagsCloudList_ : FeatherTestCase
    {
        /// <summary>
        /// UI test ContentTagsOptionAndTagsCloudList
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Classifications)]
        public void ContentTagsOptionAndTagsCloudList()
        {
             BAT.Macros().NavigateTo().Pages();
             BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
             BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
             BATFeather.Wrappers().Backend().Classifications().TagsWrapper().SelectRadioButtonOption(TagsRadioButtonIds.contentTags);
             BATFeather.Wrappers().Backend().Classifications().TagsWrapper().SelectContentTypeOption("List items");
             BATFeather.Wrappers().Backend().Classifications().TagsWrapper().SelectListTemplate("TagCloud");
             BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
 
             for (int i = 1; i < 4; i++)
             {
                 if (i > 0 && i <= 2)
                 {
                     BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, TagTitle + i);
                 }
                 else
                 {
                     BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContentForNotExistingContent(WidgetName, TagTitle + i);
                 }
             }

             BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetNameList);
             BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton(0);
             BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector(ListTitle);
             BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
             BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();

             BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());

            Assert.IsTrue(BATFeather.Wrappers().Frontend().Classifications().ClassificationsWrapper().IsTagsTitlesPresentOnThePageFrontend(new string[] { TagTitle + 2, TagTitle + 1 }));
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Classifications().ClassificationsWrapper().IsTagsTitlesPresentOnThePageFrontend(new string[] { TagTitle + 3 }));
            BATFeather.Wrappers().Frontend().Classifications().ClassificationsWrapper().VerifCloudListStyle(2, false);
            BATFeather.Wrappers().Frontend().Classifications().ClassificationsWrapper().VerifCloudListStyle(4, false);
            BATFeather.Wrappers().Frontend().Classifications().ClassificationsWrapper().ClickTagsTitle(TagTitle + 2);
            BATFeather.Wrappers().Frontend().Lists().ListsWidgetWrapper().VerifySimpleListTemplate(ListTitle, new string[] { ListItemTitle + 2, ListItemTitle + 3 });
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

        private const string PageName = "TagsPage";
        private const string WidgetName = "Tags";
        private const string TagTitle = "Tag";
        private const string ListItemTitle = "list item";
        private const string WidgetNameList = "Lists";
        private const string ListTitle = "Test list";
    }
}