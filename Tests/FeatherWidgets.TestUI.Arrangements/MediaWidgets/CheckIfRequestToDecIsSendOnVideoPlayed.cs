using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Server;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    public class CheckIfRequestToDecIsSendOnVideoPlayed : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperations.Pages().CreatePage(PageName);
            ServerOperations.Videos().CreateLibrary(LibraryTitle);
            ServerOperations.Videos().Upload(LibraryTitle, VideoTitle, VideoResource);
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
        private const string VideoTitle = "big_buck_bunny1";
        private const string VideoResource = "Telerik.Sitefinity.TestUtilities.Data.Videos.big_buck_bunny1.mp4";
    }
}
