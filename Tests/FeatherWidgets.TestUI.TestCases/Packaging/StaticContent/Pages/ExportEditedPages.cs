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
    /// Exports edited pages structure.
    /// </summary>
    [TestClass]
    public class ExportEditedPages_ : FeatherTestCase
    {
        /// <summary>
        /// Exports edited pages structure.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Packaging)]
        public void ExportEditedPages()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(200000, () => BAT.Macros().NavigateTo().Pages(this.Culture));            
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
    }
}
