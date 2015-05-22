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
    /// AddEmbedCodeWidgetToPageAndWriteAndRemoveJavaScript arrangement class.
    /// </summary>
    public class AddEmbedCodeWidgetToPageAndWriteAndRemoveJavaScript : ITestArrangement
    {
        /// <summary>
        /// Server side set up. 
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageName, templateId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
        }

        private const string PageName = "PageWithEmbedCodeWidget";
        private const string PageTemplateName = "Bootstrap.default";
    }
}
