using System;
using System.Collections.Generic;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// 
    /// </summary>
    public class CheckNavigationInImageSelector : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            Guid page1Id = ServerOperations.Pages().CreatePage(PageName);
            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(page1Id);

            var parentId = ServerSideUpload.CreateAlbum(ImageLibraryTitle);
            var childId = ServerSideUpload.CreateFolder(ChildLibraryTitle, parentId);
            var nextChildId = ServerSideUpload.CreateFolder(NextChildLibraryTitle, childId);
            ServerSideUpload.UploadImage(ImageLibraryTitle, ImageTitle + 1, ImageResource);

            ServerSideUpload.UploadImageInFolder(childId, ImageTitle + 2, ImageResourceChild);

            ServerOperations.Users().CreateUserWithProfileAndRoles("administrator", "password", "Administrator", "User", "administrator@test.test", new List<string> { "BackendUsers", "Administrators" });

            AuthenticationHelper.AuthenticateUser("administrator", "password", true);
            ServerSideUpload.UploadImageInFolder(nextChildId, ImageTitle + 3, ImageResourceNextChild);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.Users().DeleteUserAndProfile("administrator");
            ServerOperations.Libraries().DeleteLibraries(false, "Image");
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