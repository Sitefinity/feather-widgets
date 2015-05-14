using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// SelectCssFileInCssWidget arrangement class.
    /// </summary>
    public class SelectCssFileInCssWidget : ITestArrangement
    {
        /// <summary>
        /// Server side set up. 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "folderPath"), ServerSetUp]
        public void SetUp()
        {
            Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            Guid pageId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageName, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);

            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(pageId, ContentBlockContent, PlaceHolderId);
            ServerOperationsFeather.Pages().AddCssWidgetToPage(pageId, PlaceHolderId);

            string folderPath = Path.Combine(ServerOperationsFeather.TemplateOperations().SfPath, "Css");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, CssFileName);
            ServerOperationsFeather.DynamicModules().AddNewResource(CssFileFileResource, filePath);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();

            string folderPath = Path.Combine(ServerOperationsFeather.TemplateOperations().SfPath, "Css");
            string filePath = Path.Combine(folderPath, CssFileName);
            File.Delete(filePath);
            Directory.Delete(folderPath);
        }

        private const string PageName = "PageWithCssWidget";
        private const string ContentBlockContent = "Test content";
        private const string PageTemplateName = "Bootstrap.default";
        private const string PlaceHolderId = "Contentplaceholder1";
        private const string CssFileFileResource = "FeatherWidgets.TestUtilities.Data.CssFiles.styles.css";
        private const string CssFileName = "styles.css";
    }
}
