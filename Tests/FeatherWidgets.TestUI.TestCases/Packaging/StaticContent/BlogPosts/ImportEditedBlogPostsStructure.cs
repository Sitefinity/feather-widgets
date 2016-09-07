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
    /// Import edited blog and posts structure.
    /// </summary>
    [TestClass]
    public class ImportEditedBlogAndBlogPostsStructure_ : FeatherTestCase
    {
        /// <summary>
        /// Import edited blog and posts structure.
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam6),
        TestCategory(FeatherTestCategories.Packaging)]
        public void ImportEditedBlogAndBlogPostsStructure()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(200000, () => BAT.Macros().NavigateTo().Modules().Blogs(this.Culture));          
            BAT.Arrange(this.TestName).ExecuteArrangement("ImportNewPackage");

            RuntimeSettingsModificator.ExecuteWithClientTimeout(200000, () => BAT.Macros().NavigateTo().Classifications().AllClassifications());
          
            BAT.Wrappers().Backend().Taxonomies().ClassificationWrapper().VerifyTaxonExistenceInTaxonomyItemsScreen(classifications[0], exists: true);
            BAT.Wrappers().Backend().Taxonomies().ClassificationWrapper().VerifyTaxonExistenceInTaxonomyItemsScreen(classifications[1], exists: true);
            BAT.Wrappers().Backend().Taxonomies().ClassificationWrapper().VerifyTaxonExistenceInTaxonomyItemsScreen(classificationsBlog[0], exists: true);
            BAT.Wrappers().Backend().Taxonomies().ClassificationWrapper().VerifyTaxonExistenceInTaxonomyItemsScreen(classificationsBlog[1], exists: true);

            BAT.Macros().NavigateTo().Modules().Blogs(this.Culture);
            BAT.Wrappers().Backend().Blogs().BlogsWrapper().NavigateToBlogPostsForBlog("TestBlog", this.Culture);
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().OpenCustomFieldsSection(CustomFieldsLinkID);
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper()
                .VerifyFieldsArePresent(this.fieldNamesWithoutClassifications, this.fieldTypes);
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().VerifyFieldIsPresent(CustomFieldsNames.Classification, classifications[0]);
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().VerifyFieldIsPresent(CustomFieldsNames.Classification, classifications[1]);

            BAT.Macros().NavigateTo().Modules().Blogs(this.Culture);
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().OpenCustomFieldsSection(CustomFieldsLinkIDBlog);
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper()
                .VerifyFieldsArePresent(this.fieldNamesWithoutClassifications, this.fieldTypes);
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().VerifyFieldIsPresent(CustomFieldsNames.Classification, classificationsBlog[0]);
            BAT.Wrappers().Backend().CustomFields().CustomFieldsWrapper().VerifyFieldIsPresent(CustomFieldsNames.Classification, classificationsBlog[1]);
            
            BAT.Macros().NavigateTo().Modules().Blogs(this.Culture);
            BAT.Wrappers().Backend().Blogs().BlogsWrapper().ActionsMenuSelectOption(blogTitle, "Title");
            BAT.Wrappers().Backend().Blogs().BlogsWrapper().AssertFieldsAreVisible(CustomFieldsNames.FieldNamesInItemsScreen);
            BAT.Wrappers().Backend().Blogs().BlogsWrapper().EditBlogTitle(BlogNew);
            BAT.Wrappers().Backend().Blogs().BlogsWrapper().ClickSaveChangesButton();

            BAT.Wrappers().Backend().Blogs().BlogsWrapper().NavigateToBlogPostsForBlog(BlogNew, this.Culture);
            BAT.Wrappers().Backend().Blogs().BlogPostsWrapper().CreateBlogPostFromTopMenu();
            BAT.Wrappers().Backend().Blogs().BlogPostsWrapper().AssertFieldsAreVisible(CustomFieldsNames.FieldNamesInItemsScreen);
            BAT.Wrappers().Backend().Blogs().BlogPostsWrapper().SetTitle(BlogPostNew);
            BAT.Wrappers().Backend().Blogs().BlogPostsWrapper().Publish();

            BAT.Macros().NavigateTo().CustomPage("~/" + pageName.ToLower(), true, this.Culture);
            this.VerifyItemsOnFrontEnd(BlogNew);
            this.VerifyItemsOnFrontEnd(BlogPostNew);
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

        private const string CustomFieldsLinkIDBlog = "_blogsCustomFields_ctl00_ctl00_customFieldsForBlogs";
        private const string CustomFieldsLinkID = "_postsCustomFields_ctl00_ctl00_customFields";
        private static string blogTitle = "TestBlog";
        private static string blogWidget = "Blogs";
        private static string blogPostWidget = "Blog posts";
        private static string pageName = "TestPage";
        private const string BlogPostNew = "BlogPost New";
        private const string BlogNew = "Blog New";

        private readonly string[] fieldNamesWithoutClassifications = new string[] 
                                                   { 
                                                        "Tags", "Pages", "Long",
                                                        "Image", "Video", "Document", "Multiple", "YesNo", 
                                                        "Currency", "Date", "Number", "Events", "BlogPosts", "ShortEdited"
                                                    };

        private static string[] classifications = new string[] { "post1", "post2" };
        private static string[] classificationsBlog = new string[] { "b1", "b2" };

        private readonly string[] fieldTypes = new string[] 
                                                   { 
                                                        CustomFieldsNames.Classification, 
                                                        CustomFieldsNames.RelatedData, CustomFieldsNames.LongText, 
                                                        CustomFieldsNames.RelatedMedia, CustomFieldsNames.RelatedMedia, CustomFieldsNames.RelatedMedia, 
                                                        CustomFieldsNames.MultipleChoices, CustomFieldsNames.YesNo, CustomFieldsNames.Currency, 
                                                        CustomFieldsNames.DateAndTime, CustomFieldsNames.Number, CustomFieldsNames.RelatedData, 
                                                        CustomFieldsNames.RelatedData, CustomFieldsNames.ShortText
                                                   };
    }
}
