﻿using System;
using ArtOfTest.WebAii.Controls.HtmlControls;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;
using Telerik.Sitefinity.TestUI.Framework.Wrappers.Backend.CustomFields;

namespace FeatherWidgets.TestUI.TestCases.Packaging.StaticContent
{
    /// <summary>
    /// Import edited videos structure
    /// </summary>
    [TestClass]
    public class ImportEditedVideosStructure_ : FeatherTestCase
    {
        /// <summary>
        /// Import videos structure
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Packaging)]
        public void ImportEditedVideosStructure()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(200000, () => BAT.Macros().NavigateTo().Modules().Videos(this.Culture));                      
            BAT.Arrange(this.TestName).ExecuteArrangement("ImportNewPackage");
            RuntimeSettingsModificator.ExecuteWithClientTimeout(200000, () => BAT.Macros().NavigateTo().Classifications().AllClassifications()); 
            BAT.Wrappers().Backend().Taxonomies().ClassificationWrapper().VerifyTaxonExistenceInTaxonomyItemsScreen(classifications[0], exists: true);
            BAT.Wrappers().Backend().Taxonomies().ClassificationWrapper().VerifyTaxonExistenceInTaxonomyItemsScreen(classifications[1], exists: true);

            BAT.Macros().NavigateTo().Modules().Videos(this.Culture); 
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().OpenCustomFieldsSection(CustomFieldsLinkID);
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper()
                .VerifyFieldsArePresent(CustomFieldsNames.FieldNamesAllEdited, CustomFieldsNames.FieldTypesAllEdited);
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().VerifyFieldIsPresent(CustomFieldsNames.Classification, classifications[0]);
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().VerifyFieldIsPresent(CustomFieldsNames.Classification, classifications[1]);

            BAT.Macros().NavigateTo().Modules().Videos(this.Culture);
            BAT.Wrappers().Backend().Libraries().LibrariesWrapper().OpenLibraryByTitle(AlbumName);
            BAT.Wrappers().Backend().Libraries().LibrariesWrapper().OpenEditVideoScreen(Name);
            BAT.Wrappers().Backend().Libraries().LibrariesWrapper().AssertFieldsAreVisible(CustomFieldsNames.FieldNamesInItemsScreen);
            BAT.Wrappers().Backend().Images().ImagesEdit().SetTitle(NewName);
            BAT.Wrappers().Backend().Images().ImagesEdit().Publish();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            Assert.IsTrue(BAT.Wrappers().Frontend().Images().ImageGallery().IsImagePresentByAlt(NewNameFrontend));
        }

        /// <summary>
        /// Forces calling initialize methods that will prepare test with data and resources. This method must be overridden if you want
        /// in your test case.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Arrange(this.TestName).ExecuteArrangement("LoadApplication");
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

        private const string CustomFieldsLinkID = "_customFields_ctl00_ctl00_customFields";
        private const string AlbumName = "myTestAlbum2";
        private const string Name = "Wildlife1";
        private const string VideoGalleryWidget = "Video gallery";
        private const string PageName = "TestPage";
        private const string NewName = "New name";
        private const string NewNameFrontend = "Newname";
        private static string[] classifications = new string[] { "v1", "v2" };

    }
}
