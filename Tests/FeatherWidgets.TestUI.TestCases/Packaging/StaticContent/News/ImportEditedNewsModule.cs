using System;
using ArtOfTest.WebAii.Controls.HtmlControls;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;
using Telerik.Sitefinity.TestUI.Framework.Wrappers.Backend.CustomFields;
using Telerik.TestUI.Core.Navigation;

namespace FeatherWidgets.TestUI.TestCases.Packaging.StaticContent
{
    /// <summary>
    /// Import Edited News Module.
    /// </summary>
    [TestClass]
    public class ImportEditedNewsModule_ : FeatherTestCase
    {
        /// <summary>
        /// Import Edited News Module.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Packaging)]
        public void ImportEditedNewsModule()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(200000, () => BAT.Macros().NavigateTo().Modules().News(this.Culture));
            BAT.Arrange(this.TestName).ExecuteArrangement("ImportNewPackage");
            RuntimeSettingsModificator.ExecuteWithClientTimeout(200000, () => BAT.Macros().NavigateTo().Classifications().AllClassifications());                            
            BAT.Wrappers().Backend().Taxonomies().ClassificationWrapper().VerifyTaxonExistenceInTaxonomyItemsScreen(classifications[0], exists: true);
            BAT.Wrappers().Backend().Taxonomies().ClassificationWrapper().VerifyTaxonExistenceInTaxonomyItemsScreen(classifications[1], exists: true);

            BAT.Macros().NavigateTo().Modules().News(this.Culture);
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().OpenCustomFieldsSection(CustomFieldsLinkID);
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper()
                .VerifyFieldsArePresent(this.fieldNamesWithoutClassifications, this.fieldTypes);
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().VerifyFieldIsPresent(CustomFieldsNames.Classification, classifications[0]);
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().VerifyFieldIsPresent(CustomFieldsNames.Classification, classifications[1]);

            BAT.Macros().NavigateTo().Modules().News(this.Culture);
            BAT.Wrappers().Backend().News().NewsWrapper().ClickCreateANewsItemButton();
            BAT.Wrappers().Backend().News().NewsCreateScreenWrapper().AssertFieldsAreVisible(classifications);
            BAT.Wrappers().Backend().News().NewsCreateScreenWrapper().AssertFieldsAreVisible(CustomFieldsNames.FieldNamesInItemsScreen);
            BAT.Wrappers().Backend().News().NewsCreateScreenWrapper().SetNewsTitle(NewsTitle);
            BAT.Wrappers().Backend().News().NewsCreateScreenWrapper().PublishNewsItem();

            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddMvcWidgetHybridModePage(NewsWidget);
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            this.VerifyItemsOnFrontEnd(NewsTitle);
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

        private const string NewsWidget = "News";
        private const string PageName = "TestPage";
        private const string NewsTitle = "News title";
        private const string CustomFieldsLinkID = "_newsCustomFields_ctl00_ctl00_customFields";
        private readonly string[] fieldNamesWithoutClassifications = new string[] 
                                                   { 
                                                        "Category", "Tags", "Pages", "Long",
                                                        "Image", "Video", "Document", "Multiple", "YesNo", 
                                                        "Currency", "Date", "Number", "Events", "BlogPosts", "ShortEdited"
                                                    };

        private static string[] classifications = new string[] { "n1", "n2" };

        private readonly string[] fieldTypes = new string[] 
                                                   { 
                                                        CustomFieldsNames.Classification, CustomFieldsNames.Classification, 
                                                        CustomFieldsNames.RelatedData, CustomFieldsNames.LongText, 
                                                        CustomFieldsNames.RelatedMedia, CustomFieldsNames.RelatedMedia, CustomFieldsNames.RelatedMedia, 
                                                        CustomFieldsNames.MultipleChoices, CustomFieldsNames.YesNo, CustomFieldsNames.Currency, 
                                                        CustomFieldsNames.DateAndTime, CustomFieldsNames.Number, CustomFieldsNames.RelatedData, 
                                                        CustomFieldsNames.RelatedData, CustomFieldsNames.ShortText
                                                   };
    }
}
