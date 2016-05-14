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
    /// Import edited pages structure.
    /// </summary>
    [TestClass]
    public class ImportEditedPages_ : FeatherTestCase
    {
        /// <summary>
        /// Import edited pages structure.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Packaging)]
        public void ImportEditedPages()
        {
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Arrange(this.TestName).ExecuteArrangement("ImportNewPackage");

            BAT.Macros().NavigateTo().Classifications().AllClassifications();
            BAT.Wrappers().Backend().Taxonomies().ClassificationWrapper().VerifyTaxonExistenceInTaxonomyItemsScreen(classifications[0], exists: true);
            BAT.Wrappers().Backend().Taxonomies().ClassificationWrapper().VerifyTaxonExistenceInTaxonomyItemsScreen(classifications[1], exists: true);

            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().OpenCustomFieldsSection(CustomFieldsLinkID);
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper()
                .VerifyFieldsArePresent(this.fieldNamesWithoutClassifications, this.fieldTypes);
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().VerifyFieldIsPresent(CustomFieldsNames.Classification, classifications[0]);
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().VerifyFieldIsPresent(CustomFieldsNames.Classification, classifications[1]);

            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().CreatePageLinkFromTopMenu();
            BAT.Wrappers().Backend().Pages().CreatePageWrapper().AssertFieldsAreVisible(CustomFieldsNames.FieldNamesInItemsScreen);
            BAT.Wrappers().Backend().Pages().CreatePageWrapper().SetPageTitle(PageName);
            BAT.Wrappers().Backend().Pages().CreatePageWrapper().ClickCreateAndGoToAddContentButton();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddMvcWidgetHybridModePage(WidgetName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().FillContentToContentBlockWidget(ContentBlockText);
            BATFeather.Wrappers().Backend().ContentBlocks().ContentBlocksWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            this.VerifyItemsOnFrontEnd(ContentBlockText);
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

        private const string CustomFieldsLinkID = "_customFields_ctl00_ctl00_customFields";
        private const string WidgetName = "Content block";
        private const string PageName = "TestPageNew";
        private const string ContentBlockText = "This is test content";

        private readonly string[] fieldNamesWithoutClassifications = new string[] 
                                                   { 
                                                        "Pages", "Long", "Image", "Video",
                                                        "Document", "Multiple", "YesNo", "Currency", 
                                                        "Date", "Number", "Events", "BlogPosts", "ShortEdited"
                                                    };

        private readonly string[] fieldTypes = new string[] 
                                                   { 
                                                        CustomFieldsNames.RelatedData, CustomFieldsNames.LongText, 
                                                        CustomFieldsNames.RelatedMedia, CustomFieldsNames.RelatedMedia, CustomFieldsNames.RelatedMedia, 
                                                        CustomFieldsNames.MultipleChoices, CustomFieldsNames.YesNo, CustomFieldsNames.Currency, 
                                                        CustomFieldsNames.DateAndTime, CustomFieldsNames.Number, CustomFieldsNames.RelatedData, 
                                                        CustomFieldsNames.RelatedData, CustomFieldsNames.ShortText
                                                   };

        private static string[] classifications = new string[] { "p1", "p2" };
    }
}
