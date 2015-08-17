using System;
using System.Collections.Generic;
using FeatherWidgets.TestUtilities.CommonOperations;
using FeatherWidgets.TestUtilities.CommonOperations.Pages;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// ContentTagsOptionAndTagCloudTemplate arrangement class.
    /// </summary>
    public class ContentTagsOptionAndTagCloudTemplate : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);
            Guid templateId = ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            Guid pageId = ServerOperations.Pages().CreatePage(PageName, templateId);
            Guid pageNodeId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerOperationsFeather.Pages().AddTagsWidgetToPage(pageNodeId, PlaceHolderId);
            ServerOperationsFeather.Pages().AddListsWidgetToPage(pageNodeId, PlaceHolderId);
            this.listOperations.CreateList(this.listId, ListTitle, ListDescription);

            for (int i = 1; i < 4; i++)
            {
                this.taxonomies.CreateTag(TaxonTitle + i);          
            }

            this.CreateListItemWithTags(ListItemTitle + 1, ListItemContent + 1, new List<string> { TaxonTitle + 1 });
            this.CreateListItemWithTags(ListItemTitle + 2, ListItemContent + 2, new List<string> { TaxonTitle + 2 });
            this.CreateListItemWithTags(ListItemTitle + 3, ListItemContent + 3, new List<string> { TaxonTitle + 2 });

            ServerOperationsFeather.NewsOperations().CreatePublishedNewsItem(NewsTitle + 1, NewsContent, AuthorName, SourceName, null, new List<string> { TaxonTitle + 3 }, null);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            this.listOperations.DeleteList(this.listId);
            this.taxonomies.ClearAllTags(TaxonomiesConstants.TagsTaxonomyId);
            ServerOperations.News().DeleteAllNews();
        }

        private void CreateListItemWithTags(string title, string content, IEnumerable<string> tags)
        {
            Guid listItemId = Guid.NewGuid();
            this.listOperations.CreateListItem(listItemId, this.listId, title, content);
            this.listOperations.AddTaxonomiesToListItem(listItemId, null, tags); 
        }

        private const string AdminUserName = "admin";
        private const string AdminPass = "admin@2";
        private const string PageName = "TagsPage";
        private const string TaxonTitle = "Tag";
        private const string PageTemplateName = "Bootstrap.default";
        private const string PlaceHolderId = "Contentplaceholder1";
        private const string ListTitle = "Test list";
        private const string ListDescription = "Test list description";
        private const string ListItemTitle = "list item";
        private const string ListItemContent = "list content";

        private const string NewsContent = "News content";
        private const string NewsTitle = "NewsTitle";
        private const string AuthorName = "AuthorName";
        private const string SourceName = "SourceName";

        private readonly Guid listId = new Guid("0D3937D3-A690-4F19-9DA4-53F0880F5B62");
        private readonly TaxonomiesOperations taxonomies = ServerOperations.Taxonomies();
        private readonly ListsOperations listOperations = ServerOperationsFeather.ListsOperations();
    }
}
