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
    /// VerifyPagingOnFrontendPageForVideoGalleryWidget arrangement class.
    /// </summary>
    public class VerifyPagingOnFrontendPageForVideoGalleryWidget : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid page1Id = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddVideoGalleryWidgetToPage(page1Id);

            ServerOperations.Videos().CreateLibrary(VideoLibraryTitle);
            ServerOperations.Videos().Upload(VideoLibraryTitle, VideoTitle + 1, VideoResource1);
            ServerOperations.Videos().Upload(VideoLibraryTitle, VideoTitle + 2, VideoResource2);
            ServerOperations.Videos().Upload(VideoLibraryTitle, VideoTitle + 3, VideoResource3);
            ServerOperations.Videos().Upload(VideoLibraryTitle, VideoTitle + 4, VideoResource4);
            ServerOperations.Videos().Upload(VideoLibraryTitle, VideoTitle + 5, VideoResource5);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Videos().DeleteAllLibrariesExceptDefaultOne();
        }

        private const string PageName = "PageWithVideo";
        private const string VideoLibraryTitle = "TestVideoLibrary";
        private const string VideoTitle = "Video";
        private const string VideoResource1 = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny1.mp4";
        private const string VideoResource2 = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny2.mp4";
        private const string VideoResource3 = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny3.mp4";
        private const string VideoResource4 = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny4.mp4";
        private const string VideoResource5 = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny5.mp4";
    }
}