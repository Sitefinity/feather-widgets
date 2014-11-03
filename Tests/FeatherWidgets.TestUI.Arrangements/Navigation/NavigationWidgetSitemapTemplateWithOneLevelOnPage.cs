using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// NavigationWidgetSitemapTemplateWithOneLevelOnPage arrangement class.
    /// </summary>
    public class NavigationWidgetSitemapTemplateWithOneLevelOnPage : ITestArrangement
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

            FeatherServerOperations.Pages().AddMvcWidgetToPage(parentPageId, ControllerType, WidgetCaption, PlaceHolderId);

            ServerOperations.Pages().CreatePage(SiblingPageName);
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
        private const string WidgetCaption = "Navigation";
        private const string ControllerType = "Navigation.Mvc.Controllers.NavigationController";
        private const string PlaceHolderId = "Contentplaceholder1";
    }
}
