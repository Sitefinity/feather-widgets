using System;
using System.IO;
using System.Threading;
using FeatherWidgets.TestUtilities.CommonOperations;
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
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);

            int templatesCount = ServerOperationsFeather.TemplateOperations().GetTemplatesCount;
            string folderPath = Path.Combine(ServerOperationsFeather.TemplateOperations().SfPath, "MVC", "Views", "Layouts");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, LayoutFileName);
            FeatherServerOperations.ResourcePackages().AddNewResource(LayoutFileResource, filePath);
            FeatherServerOperations.ResourcePackages().WaitForTemplatesCountToIncrease(templatesCount, TemplatesIncrement);            
        }

        [ServerArrangement]
        public void GetTemplateId()
        {
            var templateId = ServerOperationsFeather.TemplateOperations().GetTemplateIdByTitle(TemplateTitle);
            ServerOperations.Pages().CreatePage(PageName, templateId);

            ServerArrangementContext.GetCurrent().Values.Add("templateId", templateId.ToString());
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            string filePath = Path.Combine(ServerOperationsFeather.TemplateOperations().SfPath, "MVC", "Views", "Layouts", LayoutFileName);

            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Templates().DeletePageTemplate(TemplateTitle);
            File.Delete(filePath);
        }

        private const string AdminUserName = "admin";
        private const string AdminPass = "admin@2";
        private const string TemplateTitle = "TestLayout";
        private const string PageName = "FeatherPage";
        private const string LayoutFileResource = "Telerik.Sitefinity.Frontend.TestUtilities.Data.TestLayout.cshtml";
        private const string LayoutFileName = "TestLayout.cshtml";
        private const int TemplatesIncrement = 1;
    }
}
