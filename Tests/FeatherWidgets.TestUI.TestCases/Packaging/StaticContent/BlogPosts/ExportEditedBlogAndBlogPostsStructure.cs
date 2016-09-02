using System;
using ArtOfTest.WebAii.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;
using Telerik.Sitefinity.TestUI.Framework.Wrappers.Backend.CustomFields;
using Telerik.Sitefinity.TestUI.ModuleBuilder.Framework;
using Telerik.TestUI.Core.Navigation;

namespace FeatherWidgets.TestUI.TestCases.Packaging.StaticContent
{
    /// <summary>
    /// Exports edited blog and posts structure.
    /// </summary>
    [TestClass]
    public class ExportEditedBlogAndBlogPostsStructure_ : FeatherTestCase
    {
        /// <summary>
        /// Exports edited blog and posts structure.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Packaging)]
        public void ExportEditedBlogAndBlogPostsStructure()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(200000, () => BAT.Macros().NavigateTo().Modules().Blogs(this.Culture));       
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().OpenCustomFieldsSection(CustomFieldsLinkIDBlog);
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().DeleteField("Short");
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper()
             .AddCustomField(CustomFieldsNames.ShortText, "ShortEdited");
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().SaveCustomFields();

            BAT.Macros().NavigateTo().Modules().Blogs(this.Culture);
            BAT.Wrappers().Backend().Blogs().BlogsWrapper().NavigateToBlogPostsForBlog("TestBlog", this.Culture);
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

        private const string CustomFieldsLinkIDBlog = "_blogsCustomFields_ctl00_ctl00_customFieldsForBlogs";
        private const string CustomFieldsLinkID = "_postsCustomFields_ctl00_ctl00_customFields";
    }
}
