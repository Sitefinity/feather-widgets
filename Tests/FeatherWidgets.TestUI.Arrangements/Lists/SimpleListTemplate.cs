using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lists.Model;
using Telerik.Sitefinity.Modules.Lists;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Workflow;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Arrangement methods for SimpleListTemplate
    /// </summary>
    public class SimpleListTemplate : ITestArrangement
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

            ServerOperationsFeather.ListsOperations().CreateList(this.secondListId, SecondListTitle, SecondListDescription);
            ServerOperationsFeather.ListsOperations().CreateListItem(Guid.NewGuid(), this.secondListId, SecondListItem1Title, SecondListItem1Content);
            ServerOperationsFeather.ListsOperations().CreateListItem(Guid.NewGuid(), this.secondListId, SecondListItem2Title, SecondListItem2Content);

            Guid pageId = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddListsWidgetToPage(pageId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperationsFeather.ListsOperations().DeleteList(this.listId);
            ServerOperationsFeather.ListsOperations().DeleteList(this.secondListId);
        }

        private const string PageName = "TestPage";

        private const string ListTitle = "Test list";
        private const string ListDescription = "Test list description";
        private const string ListItem1Title = "list item 1";
        private const string ListItem1Content = "list content 1";
        private const string ListItem2Title = "list item 2";
        private const string ListItem2Content = "list content 2";
        private readonly Guid listId = new Guid("0D3937D3-A690-4F19-9DA4-53F0880F5B62");

        private const string SecondListTitle = "Second list";
        private const string SecondListDescription = "second list description";
        private const string SecondListItem1Title = "second list item 1";
        private const string SecondListItem1Content = "second list content 1";
        private const string SecondListItem2Title = "second list item 2";
        private const string SecondListItem2Content = "second list content 2";
        private readonly Guid secondListId = new Guid("FE7F956E-FE4F-49E2-897E-6145D432C318");
    }
}
