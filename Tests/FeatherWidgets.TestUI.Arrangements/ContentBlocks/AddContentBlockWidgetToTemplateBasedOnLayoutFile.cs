using System;
using System.IO;
using System.Threading;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// AddContentBlockWidgetToTemplateBasedOnLayoutFile arrangement class.
    /// </summary>
    public class AddContentBlockWidgetToTemplateBasedOnLayoutFile : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            string folderPath = Path.Combine(this.SfPath, "MVC", "Views", "Layouts");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, LayoutFileName);
            FeatherServerOperations.ResourcePackages().AddNewResource(LayoutFileResource, filePath);
            Thread.Sleep(1000);
        }

        [ServerArrangement]
        public void GetTemplateId()
        {
            var templateId = ServerOperations.Templates().GetTemplateIdByTitle(TemplateTitle);

            ServerArrangementContext.GetCurrent().Values.Add("templateId", templateId.ToString());
        }

        [ServerArrangement]
        public void CreatePage()
        {
            var templateId = ServerOperations.Templates().GetTemplateIdByTitle(TemplateTitle);
            ServerOperations.Pages().CreatePage(PageName, templateId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            string filePath = Path.Combine(this.SfPath, "MVC", "Views", "Layouts", LayoutFileName);

            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Templates().DeletePageTemplate(TemplateTitle);
            File.Delete(filePath);
        }

        private const string TemplateTitle = "TestLayout";
        private const string PageName = "FeatherPage";
        private const string LayoutFileResource = "Telerik.Sitefinity.Frontend.TestUtilities.Data.TestLayout.cshtml";
        private const string LayoutFileName = "TestLayout.cshtml";

        private string SfPath
        {
            get
            {
                return System.Web.Hosting.HostingEnvironment.MapPath("~/");
            }
        }
    }
}
