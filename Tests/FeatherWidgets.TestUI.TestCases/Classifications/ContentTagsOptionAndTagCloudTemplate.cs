using System;
using System.Collections.Generic;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Classifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.TestUI.Framework.Utilities;

namespace FeatherWidgets.TestUI.TestCases.Classifications
{
    /// <summary>
    /// ContentTagsOptionAndTagCloudTemplate test class.
    /// </summary>
    [TestClass]
    public class ContentTagsOptionAndTagCloudTemplate_ : FeatherTestCase
    {
        /// <summary>
        /// UI test verifying used by content type option with TagCloud template in bootstrap
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Classifications)]
        public void ContentTagsOptionAndTagCloudTemplate()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/sitefinity/pages", false));
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().User().EnsureAdminLoggedIn());              
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Classifications().TagsWrapper().SelectRadioButtonOption(TagsRadioButtonIds.contentTags);
            BATFeather.Wrappers().Backend().Classifications().TagsWrapper().SelectUsedByContentTypeOption(SelectedContentType);
            BATFeather.Wrappers().Backend().Classifications().TagsWrapper().SelectTagsTemplate(TagsTemplates.TagCloud);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, TagTitle + 1);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContent(WidgetName, TagTitle + 2);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().CheckWidgetContentForNotExistingContent(WidgetName, TagTitle + 3);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(ListWidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton(0);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector(ListTitle);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Classifications().ClassificationsWrapper().IsTagsTitlesPresentOnTheFrontendPage(new string[] { TagTitle + 2, TagTitle + 1 }));
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Classifications().ClassificationsWrapper().IsTagsTitlesPresentOnTheFrontendPage(new string[] { TagTitle + 3 }));
            BATFeather.Wrappers().Frontend().Classifications().ClassificationsWrapper().VerifyCloudStyleTemplate(this.stylesTags, TagsTemplates.TagCloud);
            BATFeather.Wrappers().Frontend().Lists().ListsWidgetWrapper().VerifySimpleListTemplate(ListTitle, new string[] { ListItemTitle + 1, ListItemTitle + 2, ListItemTitle + 3 });
            BATFeather.Wrappers().Frontend().Classifications().ClassificationsWrapper().ClickTagTitle(TagTitle + 2);
            BATFeather.Wrappers().Frontend().Lists().ListsWidgetWrapper().VerifySimpleListTemplate(ListTitle, new string[] { ListItemTitle + 2, ListItemTitle + 3 });
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
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
        private const string ListWidgetName = "Lists";
        private const string ListTitle = "Test list";
        private const string SelectedContentType = "List items";
        private readonly Dictionary<string, int> stylesTags = new Dictionary<string, int>()
        {
            { TagTitle + 1, 2 },
            { TagTitle + 2, 4 },
        };
    }
}