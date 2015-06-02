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
    /// Arrangement methods for VerifyListTemplatesOnBootstrap
    /// </summary>
    public class VerifyListTemplatesOnBootstrap : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperationsFeather.ListsOperations().CreateList(this.listId, ListTitle, ListDescription);
            ServerOperationsFeather.ListsOperations().CreateListItem(Guid.NewGuid(), this.listId, ListItem1Title, ListItem1Content);
            ServerOperationsFeather.ListsOperations().CreateListItem(Guid.NewGuid(), this.listId, ListItem2Title, ListItem2Content);
            ServerOperationsFeather.ListsOperations().CreateListItem(Guid.NewGuid(), this.listId, ListItem3Title, ListItem3Content);

            Guid templateId = ServerOperationsFeather.TemplateOperations().GetTemplateIdByTitle(PageTemplateName);

            Guid pageId1 = ServerOperations.Pages().CreatePage(AnchorListPage, templateId);
            Guid pageNodeId1 = ServerOperations.Pages().GetPageNodeId(pageId1);
            ServerOperationsFeather.Pages().AddListsWidgetToPage(pageNodeId1, PlaceHolderId);

            Guid pageId2 = ServerOperations.Pages().CreatePage(SimpleListPage, templateId);
            Guid pageNodeId2 = ServerOperations.Pages().GetPageNodeId(pageId2);
            ServerOperationsFeather.Pages().AddListsWidgetToPage(pageNodeId2, this.listId, SimpleList, PlaceHolderId);

            Guid pageId3 = ServerOperations.Pages().CreatePage(PagesListPage, templateId);
            Guid pageNodeId3 = ServerOperations.Pages().GetPageNodeId(pageId3);
            ServerOperationsFeather.Pages().AddListsWidgetToPage(pageNodeId3, this.listId, PagesList, PlaceHolderId);

            Guid pageId4 = ServerOperations.Pages().CreatePage(ExpandedListPage, templateId);
            Guid pageNodeId4 = ServerOperations.Pages().GetPageNodeId(pageId4);
            ServerOperationsFeather.Pages().AddListsWidgetToPage(pageNodeId4, this.listId, ExpandedList, PlaceHolderId);

            Guid pageId5 = ServerOperations.Pages().CreatePage(ExpandableListPage, templateId);
            Guid pageNodeId5 = ServerOperations.Pages().GetPageNodeId(pageId5);
            ServerOperationsFeather.Pages().AddListsWidgetToPage(pageNodeId5, this.listId, ExpandableList, PlaceHolderId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperationsFeather.ListsOperations().DeleteList(this.listId);
        }

        private const string ListTitle = "Test list";
        private const string ListDescription = "Test list description";
        private const string ListItem1Title = "list item 1";
        private const string ListItem1Content = "list content 1";
        private const string ListItem2Title = "list item 2";
        private const string ListItem2Content = "list content 2";
        private const string ListItem3Title = "list item 3";
        private const string ListItem3Content = "list content 3";

        private const string PageTemplateName = "Bootstrap.default";
        private const string PlaceHolderId = "Contentplaceholder1";

        private const string AnchorListPage = "AnchorListPage";
        private const string SimpleListPage = "SimpleListPage";
        private const string PagesListPage = "PagesListPage";
        private const string ExpandedListPage = "ExpandedListPage";
        private const string ExpandableListPage = "ExpandableListPage";

        private const string AnchorList = "AnchorList";
        private const string SimpleList = "SimpleList";
        private const string PagesList = "PagesList";
        private const string ExpandedList = "ExpandedList";
        private const string ExpandableList = "ExpandableList";

        private readonly Guid listId = new Guid("74C3D587-44F6-4D45-8CB4-8F41B29EB03F");
    }
}
