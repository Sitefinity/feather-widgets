using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Arrangement class related to tests for navigation widget and "All pages under particular page" option.
    /// </summary>
    public class NavigationWidgetAllPagesUnderParticularPage : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerArrangement]
        public void SetUp()
        {
            // Gets the template name from the arrangement context
            string templateName = ServerArrangementContext.GetCurrent().Values["templateName"];

            // Create a page with the corresponding template from the test - Bootstrap, Foundation or Semantic
            Guid templateId = ServerOperations.Templates().GetTemplateIdByTitle(templateName);
            Guid parentPageId = ServerOperations.Pages().CreatePage(Page1, templateId);
            Guid pageNodeId1 = ServerOperations.Pages().GetPageNodeId(parentPageId);

            // Adds navigation widget to the page
            FeatherServerOperations.Pages().AddMvcWidgetToPage(pageNodeId1, typeof(NavigationController).FullName, WidgetCaption, PlaceHolderId);

            // Create another page on root level
            Guid pageNodeId2 = ServerOperations.Pages().CreatePage(Page2);

            // Create child page for Page2
            Guid childPageId = Guid.NewGuid();
            ServerOperations.Pages().CreatePage(ChildPage, childPageId, pageNodeId2);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
        }

        private const string Page1 = "PageWithNavigationWidget";
        private const string Page2 = "ParentPage";
        private const string ChildPage = "ChildPage";
        private const string WidgetCaption = "Navigation";
        private const string PlaceHolderId = "Contentplaceholder1";
    }
}
