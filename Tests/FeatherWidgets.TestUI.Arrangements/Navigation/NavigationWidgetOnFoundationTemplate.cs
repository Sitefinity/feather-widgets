using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Arrangement methods for NavigationWidgetOnFoundationTemplate
    /// </summary>
    public class NavigationWidgetOnFoundationTemplate : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            string layoutTemplatePath = this.LayoutFilePath(DefaultLayoutFile);
            string newLayoutTemplatePath = this.LayoutFilePath(NewLayoutFile);

            File.Copy(layoutTemplatePath, newLayoutTemplatePath);
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
            string filePath = this.LayoutFilePath(NewLayoutFile);

            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Templates().DeletePageTemplate(PageTemplateName);
            File.Delete(filePath);
        }

        private string LayoutFilePath(string fileName)
        {
            return Path.Combine(FeatherServerOperations.ResourcePackages().SfPath, "ResourcePackages", "Foundation", "MVC", "Views", "Layouts", fileName);          
        }

        private const string Page1 = "FoundationPage1";
        private const string Page2 = "FoundationPage2";
        private const string PageTemplateName = "defaultNew";
        private const string DefaultLayoutFile = "default.cshtml";
        private const string NewLayoutFile = "defaultNew.cshtml";       
    }
}
