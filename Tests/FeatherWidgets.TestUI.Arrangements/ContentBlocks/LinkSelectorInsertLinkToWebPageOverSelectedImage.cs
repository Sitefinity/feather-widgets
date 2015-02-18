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
    /// LinkSelectorInsertLinkToWebPageOverSelectedImage arrangement class.
    /// </summary>
    public class LinkSelectorInsertLinkToWebPageOverSelectedImage : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid page1Id = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(page1Id, ContentBlockHtml);
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
        private const string ContentBlockHtml = "<img src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTQOrSSvhefLVAXo3OOoMGYGS232bfHFnZyA9Jk24KeefYuau8c\">";
    }
}
