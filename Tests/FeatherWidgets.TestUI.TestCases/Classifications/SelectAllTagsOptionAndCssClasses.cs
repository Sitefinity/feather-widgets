using System;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Classifications;

namespace FeatherWidgets.TestUI.TestCases.Classifications
{
    /// <summary>
    /// SelectAllTagsOptionAndCssClasses test class.
    /// </summary>
    [TestClass]
    public class SelectAllTagsOptionAndCssClasses_ : FeatherTestCase
    {
        /// <summary>
        /// UI test SelectAllTagsOptionAndCssClasses
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.Team7),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Classifications)]
        public void SelectAllTagsOptionAndCssClasses()
        {
            BAT.Macros().NavigateTo().Pages();
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddMvcWidgetHybridModePage(WidgetName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Classifications().TagsWrapper().VerifyCheckedRadioButtonOption(TagsRadioButtonIds.allTags);
            BATFeather.Wrappers().Backend().Classifications().TagsWrapper().VerifySelectedSortingOption("By Title (A-Z)");
            BATFeather.Wrappers().Backend().Classifications().TagsWrapper().VerifySelectedListTemplateOption("SimpleList");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ApplyCssClasses(CssClass);
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

            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower());

            Assert.IsTrue(BATFeather.Wrappers().Frontend().Classifications().ClassificationsWrapper().IsTagsTitlesPresentOnThePageFrontend(new string[] { TagTitle + 2, TagTitle + 1 }));
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Classifications().ClassificationsWrapper().IsTagsTitlesPresentOnThePageFrontend(new string[] { TagTitle + 3 }));
            BATFeather.Wrappers().Frontend().Classifications().ClassificationsWrapper().VerifyCssClass(CssClass);
            BATFeather.Wrappers().Frontend().Classifications().ClassificationsWrapper().ClickTagsTitle(TagTitle + 1);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().News().NewsWrapper().IsNewsTitlesPresentOnThePageFrontend(new string[] { NewsTitle + 1 }));
            Assert.IsFalse(BATFeather.Wrappers().Frontend().News().NewsWrapper().IsNewsTitlesPresentOnThePageFrontend(new string[] { NewsTitle + 2}));
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Classifications().ClassificationsWrapper().IsTagsTitlesPresentOnThePageFrontend(new string[] { TagTitle + 2, TagTitle + 1 }));
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Classifications().ClassificationsWrapper().IsTagsTitlesPresentOnThePageFrontend(new string[] { TagTitle + 3 }));
            BATFeather.Wrappers().Frontend().News().NewsWrapper().ClickNewsTitle(NewsTitle + 1);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Classifications().ClassificationsWrapper().IsTagsTitlesPresentOnThePageFrontend(new string[] { TagTitle + 2, TagTitle + 1 }));
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Classifications().ClassificationsWrapper().IsTagsTitlesPresentOnThePageFrontend(new string[] { TagTitle + 3 }));
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
        private const string NewsTitle = "NewsTitle";
        private const string CssClass = "Test";
    }
}