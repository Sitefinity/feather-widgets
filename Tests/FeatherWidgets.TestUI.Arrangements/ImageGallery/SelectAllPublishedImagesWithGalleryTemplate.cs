using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// SelectAllPublishedImagesWithGalleryTemplate arrangement class.
    /// </summary>
    public class SelectAllPublishedImagesWithGalleryTemplate : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            string templateName = ServerArrangementContext.GetCurrent().Values["templateName"];

            Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(templateName);
            Guid pageId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageName, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);

            ServerOperationsFeather.Pages().AddImageGalleryWidgetToPage(pageId, PlaceHolderId);

            ServerOperations.Images().CreateLibrary(ImageLibraryTitle);
            ServerOperations.Images().Upload(ImageLibraryTitle, ImageTitle + 1, ImageResource1);

            ServerOperations.Images().Upload(ImageLibraryTitle, ImageTitle + 2, ImageResource2);

            ServerOperations.Images().Upload(ImageLibraryTitle, ImageTitle + 3, ImageResource3);
        }

        /// <summary>
        /// Gets the current libraries provider Url name.
        /// </summary>
        [ServerArrangement]
        public void GetCurrentProviderUrlName()
        {
            string urlName = ServerOperations.Media().GetCurrentProviderUrlName;

            ServerArrangementContext.GetCurrent().Values.Add("CurrentProviderUrlName", urlName);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Images().DeleteAllLibrariesExceptDefaultOne();
        }

        private const string PageName = "PageWithImage";
        private const string ImageLibraryTitle = "TestImageLibrary";
        private const string ImageTitle = "Image";
        private const string ImageResource1 = "Telerik.Sitefinity.TestUtilities.Data.Images.1.jpg";
        private const string ImageResource2 = "Telerik.Sitefinity.TestUtilities.Data.Images.2.jpg";
        private const string ImageResource3 = "Telerik.Sitefinity.TestUtilities.Data.Images.3.jpg";
        private const string PlaceHolderId = "Contentplaceholder1";
    }
}