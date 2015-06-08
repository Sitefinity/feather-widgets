using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// ContentTagsOptionAndTagsCloudList arrangement class.
    /// </summary>
    public class ContentTagsOptionAndTagsCloudList : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            Guid pageId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageName, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerOperationsFeather.Pages().AddTagsWidgetToPage(pageId, PlaceHolderId);
            ServerOperationsFeather.Pages().AddListsWidgetToPage(pageId, PlaceHolderId);
            ServerOperationsFeather.ListsOperations().CreateList(this.listId, ListTitle, ListDescription);

            for (int i = 1; i < 3; i++)
            {
                ServerOperations.Taxonomies().CreateTag(TaxonTitle + i);
                Guid listItemId = Guid.NewGuid();
                ServerOperationsFeather.ListsOperations().CreateListItem(listItemId, this.listId, ListItemTitle + i, ListItemContent + i);
                ServerOperationsFeather.ListsOperations().AddTaxonomiesToListItem(listItemId, null, new List<string> { TaxonTitle + i });             
            }

            ServerOperations.Taxonomies().CreateTag(TaxonTitle + 3);
            Guid listItemId3 = Guid.NewGuid();
            ServerOperationsFeather.ListsOperations().CreateListItem(listItemId3, this.listId, ListItemTitle + 3, ListItemContent + 3);
            ServerOperationsFeather.ListsOperations().AddTaxonomiesToListItem(listItemId3, null, new List<string> { TaxonTitle + 2 }); 
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperationsFeather.ListsOperations().DeleteList(this.listId);
            ServerOperations.Taxonomies().ClearAllTags(TaxonomiesConstants.TagsTaxonomyId);
        }

        private const string PageName = "TagsPage";
        private const string TaxonTitle = "Tag";
        private const string PageTemplateName = "Bootstrap.default";
        private const string PlaceHolderId = "Contentplaceholder1";
        private const string ListTitle = "Test list";
        private const string ListDescription = "Test list description";
        private const string ListItemTitle = "list item";
        private const string ListItemContent = "list content";

        private readonly Guid listId = new Guid("0D3937D3-A690-4F19-9DA4-53F0880F5B62");
    }
}
