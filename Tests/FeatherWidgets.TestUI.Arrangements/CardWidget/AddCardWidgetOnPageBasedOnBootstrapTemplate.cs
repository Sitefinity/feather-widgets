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
    /// AddCardWidgetOnPageBasedOnBootstrapTemplate arrangement class.
    /// </summary>
    public class AddCardWidgetOnPageBasedOnBootstrapTemplate : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            ServerOperations.Pages().CreatePage(PageName, templateId);

            Guid page1Id = ServerOperations.Pages().CreatePage(PageName2);
            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(page1Id, Content);

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
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Images().DeleteAllLibrariesExceptDefaultOne();
        }

        private const string PageName = "CardPage";
        private const string PageTemplateName = "Bootstrap.default";
        private const string PageName2 = "Page2";
        private const string Content = "Test content";
        private const string ImageLibraryTitle = "TestImageLibrary";
        private const string ImageTitle = "Image1";
        private const string ImageResource1 = "Telerik.Sitefinity.TestUtilities.Data.Images.1.jpg";
    }
}
