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
    public class NavigationWidgetAllSiblingPagesOfCurrentlyOpenedPage : ITestArrangement
    {
        /// <summary>
        /// Sets up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid templateId = ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            Guid parentPageId = ServerOperations.Pages().CreatePage(PageName, templateId);
            ServerOperations.Pages().CreatePage(SiblingPageName);
            Guid pageNodeId = ServerOperations.Pages().GetPageNodeId(parentPageId);
            Guid childPage1Id = Guid.NewGuid();
            ServerOperations.Pages().CreatePage(ChildPage1, childPage1Id, pageNodeId);
            Guid childPage2Id = Guid.NewGuid();
            ServerOperations.Pages().CreatePage(ChildPage2, childPage2Id, pageNodeId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
        }

        private const string PageName = "ParentPage";
        private const string SiblingPageName = "SiblingPage";
        private const string PageTemplateName = "Bootstrap.default";
        private const string ChildPage1 = "ChildPage1";
        private const string ChildPage2 = "ChildPage2";
    }
}
