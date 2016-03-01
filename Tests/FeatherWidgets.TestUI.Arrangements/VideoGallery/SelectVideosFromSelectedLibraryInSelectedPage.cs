﻿using System;
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
    /// Select Images From Selected Library In Selected Page arrangement class.
    /// </summary>
    public class SelectVideosFromSelectedLibraryInSelectedPage : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid page1Id = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddVideoGalleryWidgetToPage(page1Id);

            Guid singleItemPageId = ServerOperations.Pages().CreatePage(SingleItemPage);
            ServerOperationsFeather.Pages().AddVideoGalleryWidgetToPage(singleItemPageId);

            var parentId = ServerOperations.Videos().CreateLibrary(VideoLibraryTitle);
            var childId = ServerOperations.Videos().CreateFolder(ChildLibraryTitle, parentId);

            ServerOperations.Videos().Upload(VideoLibraryTitle, VideoTitle + 1, VideoResource1);
            ServerOperations.Videos().Upload(VideoLibraryTitle, VideoTitle + 2, VideoResource2);
            ServerOperationsFeather.MediaOperations().UploadVideoInFolder(childId, VideoTitle + 3, VideoResource3);
            ServerOperationsFeather.MediaOperations().UploadVideoInFolder(childId, VideoTitle + 4, VideoResource4);
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
            ServerOperations.Videos().DeleteAllLibrariesExceptDefaultOne();
        }

        private const string PageName = "PageWithVideo";
        private const string SingleItemPage = "TestPage";
        private const string VideoLibraryTitle = "TestVideoLibrary";
        private const string VideoTitle = "Video";
        private const string ChildLibraryTitle = "ChildVideoLibrary";
        private const string VideoResource1 = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny1.mp4";
        private const string VideoResource2 = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny2.mp4";
        private const string VideoResource3 = "FeatherWidgets.TestUtilities.Data.MediaFiles.WebM File.webm";
        private const string VideoResource4 = "FeatherWidgets.TestUtilities.Data.MediaFiles.Ogv File.ogv";
    }
}