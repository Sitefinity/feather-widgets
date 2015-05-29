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
    public class SelectJavaScriptFileInJavaScriptWidget : ITestArrangement
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

            ServerOperationsFeather.Pages().AddProfileWidgetToPage(pageId, PlaceHolderId);
            ServerOperationsFeather.Pages().AddJavaScriptWidgetToPage(pageId, PlaceHolderId);

            string folderPath = Path.Combine(ServerOperationsFeather.TemplateOperations().SfPath, "JavaScriptTest");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, FileName);
            ServerOperationsFeather.DynamicModules().AddNewResource(JavaScriptFileResource, filePath);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();

            string folderPath = Path.Combine(ServerOperationsFeather.TemplateOperations().SfPath, "JavaScriptTest");
            string filePath = Path.Combine(folderPath, FileName);
            File.Delete(filePath);
            Directory.Delete(folderPath);
        }

        private const string PageName = "PageWithJavaScriptWidget";
        private const string PageTemplateName = "Bootstrap.default";
        private const string PlaceHolderId = "Contentplaceholder1";
        private const string JavaScriptFileResource = "FeatherWidgets.TestUtilities.Data.CssFiles.JavaScriptWidgetTest.js";
        private const string FileName = "JavaScriptWidgetTest.js";
    }
}
