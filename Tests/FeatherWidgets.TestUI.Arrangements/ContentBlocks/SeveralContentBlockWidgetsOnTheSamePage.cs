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
    /// SeveralContentBlockWidgetsOnTheSamePage arrangement class.
    /// </summary>
    public class SeveralContentBlockWidgetsOnTheSamePage : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperations.ContentBlocks().CreateContentBlock(ContentBlockTitle, ContentBlockContent);
            Guid page1Id = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddSharedContentBlockWidgetToPage(page1Id, ContentBlockTitle);
            ServerOperationsFeather.Pages().AddSharedContentBlockWidgetToPage(page1Id, ContentBlockTitle);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.ContentBlocks().DeleteAllContentBlocks();
        }

        private const string PageName = "ContentBlock";
        private const string ContentBlockContent = "Test content";
        private const string ContentBlockTitle = "ContentBlockTitle";
    }
}
