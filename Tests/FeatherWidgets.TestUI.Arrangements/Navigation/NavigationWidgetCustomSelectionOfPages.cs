using System;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Arrangement methods for UI tests related to navigation widget and custom selection of pages. 
    /// </summary>
    public class NavigationWidgetCustomSelectionOfPages : ITestArrangement
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
            Guid navPageId = ServerOperations.Pages().CreatePage(NavigationPage, templateId);
            Guid navPageNodeId = ServerOperations.Pages().GetPageNodeId(navPageId);

            // Adds navigation widget to the page
            FeatherServerOperations.Pages().AddMvcWidgetToPage(navPageNodeId, typeof(NavigationController).FullName, WidgetCaption, PlaceHolderId);

            // Create a page on root level
            Guid pageNodeId = ServerOperations.Pages().CreatePage(Page1);

            // Create child page for Page1
            Guid childPageId = Guid.NewGuid();
            ServerOperations.Pages().CreatePage(ChildPage, childPageId, pageNodeId);

            // Create another page on root level
            ServerOperations.Pages().CreatePage(Page2);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
        }

        private const string NavigationPage = "PageWithNavigationWidget";
        private const string Page1 = "RootPage1";
        private const string ChildPage = "ChildPage1";
        private const string Page2 = "RootPage2";
        private const string WidgetCaption = "Navigation";
        private const string PlaceHolderId = "Contentplaceholder1";
    }
}
