using System;
using System.Linq;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Lists.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Lists.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Modules.Lists;
using ListsOperationsContext = Telerik.Sitefinity.TestUtilities.CommonOperations.ListOperations;

namespace FeatherWidgets.TestIntegration.Lists
{
    /// <summary>
    /// This is a class with Lists tests.
    /// </summary>
    [TestFixture]
    [Description("This is a class with Lists tests.")]
    public class ListWidgetTests
    {
        /// <summary>
        /// Tears down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.serverOperationsLists.DeleteAllLists();
        }

        /// <summary>
        /// List widget - test items functionality
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.List)]
        [Author(FeatherTeams.SitefinityTeam7)]
        public void ListsWidget_VerifyMoreThan20Items()
        {
            var list = this.serverOperationsLists.CreateList(ListTitle);
            int listItemsCount = 21;

            for (int i = 0; i < listItemsCount; i++)
            {
                this.serverOperationsLists.CreateListItem(list, ItemTitle + i, ItemTitle + i);
            }

            var listsController = new ListsController();                  

            var listsManager = ListsManager.GetManager();
            var selectedList = listsManager.GetLists().FirstOrDefault();
            listsController.Model.SerializedSelectedItemsIds = "[\"" + selectedList.Id.ToString() + "\"]";

            var lists = listsController.Model.CreateListViewModel(null, 1).Items.ToArray();
            Assert.AreEqual(1, lists.Length, "The count of lists is not as expected");

            var listItems = ((ListViewModel)lists.FirstOrDefault()).ListItemViewModel.Items.ToArray();
            for (int i = 0; i < listItemsCount; i++)
            {
                Assert.IsTrue(listItems[i].Fields.Title.Equals("Item" + i, StringComparison.CurrentCulture), "The item with this title was not found!");
            }         
        }
        #region Fields and constants

        private const string ListTitle = "Title";
        private const string ItemTitle = "Item";

        private readonly ListsOperationsContext serverOperationsLists = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Lists();
        
        #endregion
    }
}