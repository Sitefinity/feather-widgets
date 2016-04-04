using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Mvc.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// AddCardWidgetOnPageTemplate arrangement class.
    /// </summary>
    public class AddCardWidgetOnPageTemplate : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperations.Templates().CreatePureMVCPageTemplate(PageTemplateName);
            Guid templateId = ServerOperationsFeather.TemplateOperations().GetTemplateIdByTitle(PageTemplateName);
            ServerOperations.Pages().CreatePage(PageName, templateId);

            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Images().CreateLibrary(ImageLibraryTitle);
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Images().Upload(ImageLibraryTitle, ImageTitle, ImageResource1);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Templates().DeletePageTemplate(PageTemplateName);
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Images().DeleteAllLibrariesExceptDefaultOne();
        }

        private const string PageTemplateName = "CardTemplate";
        private const string PageName = "CardPage";
        private const string PageName2 = "Page2";
        private const string Content = "Test content";
        private const string ImageLibraryTitle = "TestImageLibrary";
        private const string ImageTitle = "Image1";
        private const string ImageResource1 = "Telerik.Sitefinity.TestUtilities.Data.Images.1.jpg";
    }
}
