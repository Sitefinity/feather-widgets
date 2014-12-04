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
    /// NoResultsWhenSearchForSingleItemPage arrangement class.
    /// </summary>
    public class NoResultsWhenSearchForSingleItemPage : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid parentPageId = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddNewsWidgetToPage(parentPageId);

            Guid currentChildPageId = Guid.NewGuid();
            for (int i = 0; i < PageHierarchyLevelsCount; i++)
            {
                ServerOperations.Pages().CreatePage(ChildPagesPrefix + i, currentChildPageId, parentPageId);
                parentPageId = currentChildPageId;
                currentChildPageId = Guid.NewGuid();
            }
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
        }

        private const string PageName = "News";
        private const string ChildPagesPrefix = "ChildPage";

        private const int PageHierarchyLevelsCount = 10; 
    }
}
