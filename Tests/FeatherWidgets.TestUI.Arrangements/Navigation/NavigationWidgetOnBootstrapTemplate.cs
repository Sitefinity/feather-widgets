using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Arrangement methods for NavigationWidgetOnBootstrapTemplate
    /// </summary>
    public class NavigationWidgetOnBootstrapTemplate : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
           ServerOperations.Templates().CreatePureMVCPageTemplate(PageTemplateName);
        }

        /// <summary>
        /// Server arrangement method.
        /// </summary>
        [ServerArrangement]
        public void CreatePages()
        {
            Guid templateId = ServerOperationsFeather.TemplateOperations().GetTemplateIdByTitle(PageTemplateName);
            ServerOperations.Pages().CreatePage(Page1, templateId);
            ServerOperations.Pages().CreatePage(Page2, templateId); 
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Templates().DeletePageTemplate(PageTemplateName);
        }

        private const string Page1 = "BootstrapPage1";
        private const string Page2 = "BootstrapPage2";
        private const string PageTemplateName = "defaultNew";  
    }
}
