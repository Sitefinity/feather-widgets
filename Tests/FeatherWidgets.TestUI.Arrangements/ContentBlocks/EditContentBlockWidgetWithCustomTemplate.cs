using System.IO;
using System.Linq;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// EditSharedContentBlockFromPage arrangement class.
    /// </summary
    public class EditContentBlockWidgetWithCustomTemplate : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            AuthenticationHelper.AuthenticateUser(this.AdminEmail, this.AdminPass, true);
            this.CreateLayoutFolderAndCopyLayoutFile();
            var templateId = ServerOperations.Templates().GetTemplateIdByTitle(TemplateTitle);
            ServerOperations.Pages().CreatePage(PageName, templateId);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Templates().DeletePageTemplate(TemplateTitle);
            string filePath = this.GetFilePath();
            File.Delete(filePath);
        }

        private void CreateLayoutFolderAndCopyLayoutFile()
        {
            PageManager pageManager = PageManager.GetManager();
            int templatesCount = pageManager.GetTemplates().Count();

            string filePath = this.GetFilePath();

            FeatherServerOperations.ResourcePackages().AddNewResource(FileResource, filePath);
            FeatherServerOperations.ResourcePackages().WaitForTemplatesCountToIncrease(templatesCount, 1);
        }

        private string GetFilePath()
        {
            string folderPath = Path.Combine(FeatherServerOperations.ResourcePackages().SfPath, "MVC", "Views", "Layouts");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, LayoutFileName);

            return filePath;
        }

        private const string PageName = "TestContentBlockPage";
        private const string FileResource = "Telerik.Sitefinity.Frontend.TestUtilities.Data.TestLayoutTwoPlaceholders.cshtml";
        private const string LayoutFileName = "TestLayoutTwoPlaceholders.cshtml";
        private const string TemplateTitle = "TestLayoutTwoPlaceholders";
    }
}
