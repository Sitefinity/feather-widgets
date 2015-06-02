using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUI.Arrangements.Framework.Attributes;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// SubmitCommentToNewsItemThatRequireAuthentication arrangement class.
    /// </summary>
    public class SubmitCommentToNewsItemThatRequireAuthentication : ITestArrangement
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Sitefinity.TestUtilities.CommonOperations.WidgetOperations.AddControlToPage(System.Guid,System.String,System.String,System.String,System.String,System.String)"), ServerSetUp]
        public void SetUp()
        {
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);

            ServerOperations.Comments().RequireAuthentication(ThreadType, true);
            ServerOperations.News().CreatePublishedNewsItem(NewsTitle, NewsContent, NewsProvider);

            Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            Guid pageId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageName, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerOperationsFeather.Pages().AddNewsWidgetToPage(pageId, "Contentplaceholder1");

            Guid pageIdLogIn = Guid.NewGuid();
            ServerOperations.Pages().CreateTestPage(pageIdLogIn, PageTitleLogin);
            ServerOperations.Widgets().AddControlToPage(pageIdLogIn, ControlTypes.LoginWidget, "Body", "Login");

            ServerOperations.Comments().SetDefaultLoginPageUrl(LoginURL);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);

            ServerOperations.Comments().RequireAuthentication(ThreadType, false);
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.News().DeleteAllNews();
            ServerOperations.Comments().DeleteAllComments(Key);
        }

        private const string PageName = "NewsPage";
        private const string PageTitleLogin = "LogIn";
        private const string LoginURL = "~/login";
        private const string PageTemplateName = "Bootstrap.default";
        private const string NewsContent = "News content";
        private const string NewsTitle = "NewsTitle";
        private const string NewsProvider = "Default News";
        private const string Key = "Telerik.Sitefinity.Modules.News.NewsManager_OpenAccessDataProvider";
        private const string ThreadType = "Telerik.Sitefinity.News.Model.NewsItem";
        private const string AdminUserName = "admin";
        private const string AdminPass = "admin@2";
    }
}
