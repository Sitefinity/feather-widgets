using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
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
            ServerOperationsFeather.ContentBlockOperations().CreateContentBlock("Content Block 1", "Content 1", DefaultProviderName);
            ServerOperationsFeather.ContentBlockOperations().CreateContentBlock("Content Block 2", "Content 2", SecondProviderName);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.ContentBlocks().DeleteAllContentBlocks(SecondProviderName);
            ServerOperations.ContentBlocks().DeleteAllContentBlocks();
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.ContentBlocks().RemoveSecondDataProvider();
        }

        private const string PageName = "ContentBlock";
        private const string SecondProviderName = "ContentSecondDataProvider";
        private const string DefaultProviderName = "OpenAccessDataProvider";
        private const string Content = "";
    }
}
