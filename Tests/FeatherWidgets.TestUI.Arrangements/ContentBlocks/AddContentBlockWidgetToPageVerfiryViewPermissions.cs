using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Arrangement methods for AddContentBlockWidgetToPageVerifyViewPermissions
    /// </summary>
    public class AddContentBlockWidgetToPageVerifyViewPermissions : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);
            var pageId = ServerOperations.Pages().CreatePage(PageTitle);
            ServerOperationsFeather.Pages().AddContentBlockWidgetToPage(pageId, ContentBlockText);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
        }

        private const string AdminUserName = "admin";
        private const string AdminPass = "admin@2";
        private const string PageTitle = "FeatherPage";
        private const string ContentBlockText = "TestContent";
    }
}
