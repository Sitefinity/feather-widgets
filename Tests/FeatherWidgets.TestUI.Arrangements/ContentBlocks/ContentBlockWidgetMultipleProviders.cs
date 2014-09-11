using FeatherWidgets.TestUtilities.CommonOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// ContentBlockWidgetMultipleProviders arrangement class.
    /// </summary>
    public class ContentBlockWidgetMultipleProviders : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperations.ContentBlocks().CreateSecondDataProvider();
            Guid page1Id = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(page1Id, Content);
            ServerOperationsFeather.ContentBlockOperations().CreateContentBlock("Content Block 1", "Content 1");
            ServerOperationsFeather.ContentBlockOperations().CreateContentBlock("Content Block 2", "Content 2", SecondProviderName);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.ContentBlocks().DeleteAllContentBlocks(SecondProviderName);

            // this deletes items from the default provider
            ServerOperations.ContentBlocks().DeleteAllContentBlocks();

            ServerOperations.Pages().DeleteAllPages();

            ServerOperations.ContentBlocks().RemoveSecondDataProvider();
        }

        private const string PageName = "ContentBlock";
        private const string SecondProviderName = "ContentSecondDataProvider";
        private const string Content = "";
    }
}
