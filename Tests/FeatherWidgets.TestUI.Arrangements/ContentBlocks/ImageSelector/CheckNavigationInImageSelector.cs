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
    /// Check Navigation in Image Selector arrangement class.
    /// </summary>
    public class CheckNavigationInImageSelector : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid page1Id = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(page1Id);

            var parentId = ServerOperations.Images().CreateLibrary(ImageLibraryTitle);
            var childId = ServerOperations.Images().CreateFolder(ChildLibraryTitle, parentId);
            var nextChildId = ServerOperations.Images().CreateFolder(NextChildLibraryTitle, childId);
            ServerOperations.Images().Upload(ImageLibraryTitle, ImageTitle + 1, ImageResource);

            ServerOperations.Images().UploadInFolder(childId, ImageTitle + 2, ImageResourceChild);

            ServerOperations.Users().CreateUserWithProfileAndRoles("administrator", "password", "Administrator", "User", "administrator@test.test", new List<string> { "BackendUsers", "Administrators" });

            AuthenticationHelper.AuthenticateUser("administrator", "password", true);
            ServerOperations.Images().UploadInFolder(nextChildId, ImageTitle + 3, ImageResourceNextChild);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Users().DeleteUserAndProfile("administrator");
            ServerOperations.Images().DeleteAllLibrariesExceptDefaultOne();
        }

        /// Gets the current libraries provider Url name.
        /// </summary>
        [ServerArrangement]
        public void GetCurrentProviderUrlName()
        {
            string urlName = ServerOperations.Media().GetCurrentProviderUrlName;

            ServerArrangementContext.GetCurrent().Values.Add("CurrentProviderUrlName", urlName);
        }

        private const string PageName = "PageWithImage";
        private const string ImageLibraryTitle = "TestImageLibrary";
        private const string ChildLibraryTitle = "ChildImageLibrary";
        private const string NextChildLibraryTitle = "NextChildImageLibrary";
        private const string ImageTitle = "Image";
        private const string ImageResource = "Telerik.Sitefinity.TestUtilities.Data.Images.1.jpg";
        private const string ImageResourceChild = "Telerik.Sitefinity.TestUtilities.Data.Images.2.jpg";
        private const string ImageResourceNextChild = "Telerik.Sitefinity.TestUtilities.Data.Images.3.jpg";
    }
}