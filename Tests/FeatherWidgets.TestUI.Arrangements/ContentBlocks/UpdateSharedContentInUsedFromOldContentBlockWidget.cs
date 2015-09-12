using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// UpdateSharedContentInUsedFromOldContentBlockWidget arrangement class.
    /// </summary>
    public class UpdateSharedContentInUsedFromOldContentBlockWidget : TestArrangementBase
    {
        /// <summary>
        /// Server side set up. 
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            var providerName = ContentManager.GetManager().Provider.Name;
            ServerOperationsFeather.ContentBlockOperations().CreateContentBlock(ContentBlockTitle, ContentBlockContent, providerName);
            Guid page1Id = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddSharedContentBlockWidgetToPage(page1Id, ContentBlockTitle);
            ServerOperations.ContentBlocks().AddSharedContentToPage(page1Id, ContentBlockTitle);
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
