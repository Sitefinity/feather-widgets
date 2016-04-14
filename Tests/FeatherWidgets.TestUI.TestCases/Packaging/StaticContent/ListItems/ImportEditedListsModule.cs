using System;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;
using Telerik.Sitefinity.TestUI.Framework.Wrappers.Backend.CustomFields;
using Telerik.Sitefinity.TestUI.ModuleBuilder.Framework;
using Telerik.TestUI.Core.Navigation;

namespace FeatherWidgets.TestUI.TestCases.Packaging.StaticContent
{
    /// <summary>
    /// Import edited lists module.
    /// </summary>
    [TestClass]
    public class ImportEditedListsModule_ : FeatherTestCase
    {
        /// <summary>
        /// Import edited lists module.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Packaging)]
        public void ImportEditedListsModule()
        {
            BAT.Macros().NavigateTo().Modules().Lists(this.Culture);
            BAT.Wrappers().Backend().ListItems().ListItemsGridWrapper().NavigateToListItem("TestList");
            BAT.Arrange(this.TestName).ExecuteArrangement("ImportNewPackage");

            BAT.Macros().NavigateTo().Classifications().AllClassifications();
            BAT.Wrappers().Backend().Taxonomies().ClassificationWrapper().VerifyTaxonExistenceInTaxonomyItemsScreen(classifications[0], exists: true);
            BAT.Wrappers().Backend().Taxonomies().ClassificationWrapper().VerifyTaxonExistenceInTaxonomyItemsScreen(classifications[1], exists: true);

            BAT.Macros().NavigateTo().Modules().Lists(this.Culture);
            BAT.Wrappers().Backend().ListItems().ListItemsGridWrapper().NavigateToListItem("TestList");
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().OpenCustomFieldsSection(CustomFieldsLinkID);
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper()
                .VerifyFieldsArePresent(this.fieldNamesWithoutClassifications, this.fieldTypes);
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().VerifyFieldIsPresent(CustomFieldsNames.Classification, classifications[0]);
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().VerifyFieldIsPresent(CustomFieldsNames.Classification, classifications[1]);

            BAT.Macros().NavigateTo().Modules().Lists(this.Culture);
            BAT.Wrappers().Backend().ListItems().ListItemsGridWrapper().NavigateToListItem("TestList");
            BAT.Wrappers().Backend().ListItems().ListItemsGridWrapper().CreateListItemFromTopMenu();
            BAT.Wrappers().Backend().ListItems().ListItemsGridWrapper().AssertFieldsAreVisible(this.fieldNamesWithoutClassificationsInEdit);
            BAT.Wrappers().Backend().ListItems().ListItemsGridWrapper().SetTitle(ListItemTitleNew);
            BAT.Wrappers().Backend().ListItems().ListItemsGridWrapper().Publish();

            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddMvcWidgetHybridModePage(ListWidget);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(ListWidget);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton(0);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector("TestList");
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            this.VerifyItemsOnFrontEnd(ListItemTitleNew);
        }

        /// <summary>
        /// Forces calling initialize methods that will prepare test with data and resources. This method must be overridden if you want
        /// in your test case.
        /// </summary>
        protected override void ServerSetup()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(200000, () => BAT.Macros().User().EnsureAdminLoggedIn());
            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// Forces cleanup of the test data. This method is thrown if test setup fails. This method must be overridden in your test case.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        /// <summary>
        /// Verifies the items on front end.
        /// </summary>
        /// <param name="item">The item.</param>
        private void VerifyItemsOnFrontEnd(string item)
        {
            HtmlDiv frontendPageMainDiv = BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent();
            Assert.IsTrue(frontendPageMainDiv.InnerText.Contains(item));
        }

        private const string CustomFieldsLinkID = "_listsCustomFields_ctl00_ctl00_customFieldsForListItems";

        private readonly string[] fieldNamesWithoutClassifications = new string[] 
                                                   { 
                                                        "Category", "Tags", "Pages", "Long",
                                                        "Image", "Video", "Document", "Multiple", "YesNo", 
                                                        "Currency", "Date", "Number", "Events", "BlogPosts", "ShortEdited"
                                                    };

        private readonly string[] fieldNamesWithoutClassificationsInEdit = new string[] 
                                                   { 
                                                        "ShortEdited", "Long", "Multiple", "YesNo", "Currency", 
                                                        "Date", "Number", "Tags", "Related news", "Related pages", "Related events",
                                                        "Related blog posts",  "Related images", "Related video", "Related documents or other files"
                                                    };

        private readonly string[] fieldTypes = new string[] 
                                                   { 
                                                        CustomFieldsNames.Classification, CustomFieldsNames.Classification, 
                                                        CustomFieldsNames.RelatedData, CustomFieldsNames.LongText, 
                                                        CustomFieldsNames.RelatedMedia, CustomFieldsNames.RelatedMedia, CustomFieldsNames.RelatedMedia, 
                                                        CustomFieldsNames.MultipleChoices, CustomFieldsNames.YesNo, CustomFieldsNames.Currency, 
                                                        CustomFieldsNames.DateAndTime, CustomFieldsNames.Number, CustomFieldsNames.RelatedData, 
                                                        CustomFieldsNames.RelatedData, CustomFieldsNames.ShortText
                                                   };

        private static string[] classifications = new string[] { "l1", "l2" };
        private const string ListItemTitleNew = "ListItem New";
        private const string ListWidget = "List";
        private const string PageName = "TestPage";
    }
}
