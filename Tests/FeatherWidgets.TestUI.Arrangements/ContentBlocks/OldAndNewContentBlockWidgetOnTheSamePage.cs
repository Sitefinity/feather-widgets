using FeatherWidgets.TestUtilities.CommonOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Sample arrangement that Creates and deletes a page.
    /// </summary>
    public class OldAndNewContentBlockWidgetOnTheSamePage : ITestArrangement
    {
        /// <summary>
        /// Sets up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid page1Id = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(page1Id, Content);
            ServerOperations.Widgets().AddContentBlockToPage(page1Id, Content, PlaceHolder, WidgetName);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
        }

        private const string PageName = "ContentBlock";
        private const string Content = "";
        private const string WidgetName = "Content block";
        private const string PlaceHolder = "Body";
    }
}
