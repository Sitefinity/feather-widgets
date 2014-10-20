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
    /// Arrangement methods for NavigationWidgetResponsiveDesignTransformations
    /// </summary>
    public class NavigationWidgetResponsiveDesignTransformations : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid templateId = ServerOperations.Templates().GetTemplateIdByTitle(TemplateTitle);
            Guid pageId = ServerOperations.Pages().CreatePage(PageName, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);

            ServerOperations.Pages().CreatePage(Page1);
            ServerOperations.Pages().CreatePage(Page2);

            FeatherServerOperations.Pages().AddMvcWidgetToPage(pageId, ControllerType, WidgetCaption, PlaceHolderId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
        }

        private const string PageName = "FeatherPage";
        private const string Page1 = "Page1";
        private const string Page2 = "Page2";
        private const string TemplateTitle = "Bootstrap.default";
        private const string WidgetCaption = "Navigation";
        private const string ControllerType = "Navigation.Mvc.Controllers.NavigationController";
        private const string PlaceHolderId = "Contentplaceholder1";
    }
}
