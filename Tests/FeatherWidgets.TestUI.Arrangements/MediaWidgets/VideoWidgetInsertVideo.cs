using System;
using System.Collections.Generic;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// VideoWidgetInsertDocument arrangement class.
    /// </summary>
    public class VideoWidgetInsertVideo : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperations.Pages().CreatePage(PageName);
            ServerOperations.Videos().CreateLibrary(LibraryTitle);
            ServerOperations.Videos().Upload(LibraryTitle, VideoTitle1, VideoResource1);
            ServerOperations.Videos().Upload(LibraryTitle, VideoTitle2, VideoResource2);
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

        /// <summary>
        /// Gets the current libraries provider Url name.
        /// </summary>
        [ServerArrangement]
        public void GetCurrentProviderUrlName()
        {
            string urlName = ServerOperations.Media().GetCurrentProviderUrlName;

            ServerArrangementContext.GetCurrent().Values.Add("CurrentProviderUrlName", urlName);
        }

        private const string PageName = "PageWithVideo";
        private const string LibraryTitle = "TestVideoLibrary";
        private const string VideoTitle1 = "big_buck_bunny1";
        private const string VideoTitle2 = "big_buck_bunny2";
        private const string VideoResource1 = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny1.mp4";
        private const string VideoResource2 = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny2.mp4";
    }
}