using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// NavigationWidgetAddChangeRemoveCssClass arrangement class.
    /// </summary>
    public class NavigationWidgetAddChangeRemoveCssClass : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid templateId = ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
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
        private const string PageTemplateName = "Bootstrap.default";
        private const string WidgetCaption = "Navigation";
        private const string PlaceHolderId = "Contentplaceholder1";
    }
}
