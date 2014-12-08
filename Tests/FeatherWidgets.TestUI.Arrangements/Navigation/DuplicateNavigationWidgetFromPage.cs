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
    /// DuplicateNavigationWidgetFromPage arrangement class.
    /// </summary>
    public class DuplicateNavigationWidgetFromPage : ITestArrangement
    {
        /// <summary>
        /// Server side set up. 
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            string templateName = ServerArrangementContext.GetCurrent().Values["templateName"];

            Guid templateId = ServerOperations.Templates().GetTemplateIdByTitle(templateName);
            Guid parentPageId = ServerOperations.Pages().CreatePage(PageName, templateId);
            parentPageId = ServerOperations.Pages().GetPageNodeId(parentPageId);

            FeatherServerOperations.Pages().AddMvcWidgetToPage(parentPageId, typeof(NavigationController).FullName, WidgetCaption, PlaceHolderId);
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
        private const string WidgetCaption = "Navigation";
        private const string PlaceHolderId = "Contentplaceholder1";
    }
}
