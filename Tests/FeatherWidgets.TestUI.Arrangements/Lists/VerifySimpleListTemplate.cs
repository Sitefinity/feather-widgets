using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lists.Model;
using Telerik.Sitefinity.Modules.Lists;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUtilities.TestConfig;
using Telerik.Sitefinity.Workflow;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Arrangement methods for VerifySimpleListTemplate
    /// </summary>
    public class VerifySimpleListTemplate : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperationsFeather.ListsOperations().CreateList(this.listId, ListTitle, ListDescription);
            Guid listItel1Id = ServerOperationsFeather.ListsOperations().CreateListItemMultilingual(this.mlconfig, this.listId, ListItem1Title, ListItem1Content, false, this.culture);
            ServerOperationsFeather.ListsOperations().CreateListItemMultilingual(this.mlconfig, this.listId, ListItem2Title, ListItem2Content, false, this.culture);
            ServerOperationsFeather.ListsOperations().CreateListItemMultilingual(this.mlconfig, this.listId, ListItem3Title, ListItem3Content, false, this.culture);

            DateTime publicationDate = DateTime.UtcNow.AddDays(-10);
            ServerOperationsFeather.ListsOperations().PublishListItemWithSpecificDate(listItel1Id, publicationDate);

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
        }

        private const string ListTitle = "Test list";
        private const string ListDescription = "Test list description";
        private const string ListItem1Title = "list item 1";
        private const string ListItem1Content = "list content 1";
        private const string ListItem2Title = "list item 2";
        private const string ListItem2Content = "list content 2";
        private const string ListItem3Title = "list item 3";
        private const string ListItem3Content = "list content 3";
        private const string PageName = "TestPage";
        private string culture = ServerOperationsFeather.ListsOperations().GetCurrentCulture();
        private MultilingualTestConfig mlconfig = MultilingualTestConfig.Get();

        private readonly Guid listId = new Guid("0D3937D3-A690-4F19-9DA4-53F0880F5B62");
    }
}
