using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;
using Telerik.Sitefinity.TestUI.Framework.Wrappers.Backend.CustomFields;
using Telerik.TestUI.Core.Navigation;
using ArtOfTest.WebAii.Core;

namespace FeatherWidgets.TestUI.TestCases.Packaging.StaticContent
{
    /// <summary>
    /// Exports the edited libraries module.
    /// </summary>
    [TestClass]
    public class ExportEditedLibrariesModule_ : FeatherTestCase
    {
        /// <summary>
        /// Exports the edited libraries module.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Packaging)]
        public void ExportEditedLibrariesModule()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(200000, () => BAT.Macros().NavigateTo().Modules().Documents(this.Culture));                   
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().OpenCustomFieldsSection(CustomFieldsLinkID);
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().DeleteField("Short");
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper()
             .AddCustomField(CustomFieldsNames.ShortText, "ShortEdited");
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().SaveCustomFields();
            ActiveBrowser.Refresh();

            this.Images(this.Culture);
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().OpenCustomFieldsSection(CustomFieldsLinkID);
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().DeleteField("Short");
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper()
             .AddCustomField(CustomFieldsNames.ShortText, "ShortEdited");
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().SaveCustomFields();
            ActiveBrowser.Refresh();

            BAT.Macros().NavigateTo().Modules().Videos(this.Culture);
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().OpenCustomFieldsSection(CustomFieldsLinkID);
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().DeleteField("Short");
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper()
             .AddCustomField(CustomFieldsNames.ShortText, "ShortEdited");
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().SaveCustomFields();
            ActiveBrowser.Refresh();

            BAT.Wrappers().Backend().Packaging().PackagingWrapper().ExportStructure();
            BAT.Arrange(this.TestName).ExecuteArrangement("VerifyExportedFiles");    
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

        /// <summary>
        /// Navigates to the images page for the specified culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        private void Images(string culture = null)
        {
            var imagesUrl = culture != null ? "~/sitefinity/content/images/?lang=" + culture : "~/sitefinity/content/images";
            Navigator.Navigate(imagesUrl, true);
            Manager.Current.ActiveBrowser.WaitUntilReady();
            Manager.Current.ActiveBrowser.WaitForAsyncOperations();
            Manager.Current.ActiveBrowser.RefreshDomTree();
        }

        private const string CustomFieldsLinkID = "_customFields_ctl00_ctl00_customFields";
    }
}
