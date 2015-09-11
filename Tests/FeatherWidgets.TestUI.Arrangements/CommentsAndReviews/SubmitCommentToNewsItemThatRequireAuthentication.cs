using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Modules.News;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// SubmitCommentToNewsItemThatRequireAuthentication arrangement class.
    /// </summary>
    public class SubmitCommentToNewsItemThatRequireAuthentication : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Sitefinity.TestUtilities.CommonOperations.WidgetOperations.AddControlToPage(System.Guid,System.String,System.String,System.String,System.String,System.String)"), ServerSetUp]
        public void SetUp()
        {
            AuthenticationHelper.AuthenticateUser(AdminUserName, AdminPass, true);

            ServerOperations.Comments().RequireAuthentication(ThreadType, true);
            ServerOperations.News().CreatePublishedNewsItemLiveId(NewsTitle, NewsContent, NewsAuthor, NewsSource);

            Guid templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().GetTemplateIdByTitle(PageTemplateName);
            Guid pageId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageName, templateId);
            pageId = ServerOperations.Pages().GetPageNodeId(pageId);
            ServerOperationsFeather.Pages().AddNewsWidgetToPage(pageId, "Contentplaceholder1");

            if (ServerOperations.MultiSite().CheckIsMultisiteMode())
            {
                Guid pageId2 = Guid.NewGuid();
                ServerOperations.Multilingual().Pages().CreatePageMultilingual(pageId2, PageTitleLogin, true, "en");
                ServerOperations.Widgets().AddControlToPage(pageId2, ControlTypes.LoginWidget, "Body", "Login");
                ServerOperations.MultiSite().AddPublicLoginPageToSite(pageId2, "SecondSite");
            }
            else
            {
                Guid pageIdLogIn = Guid.NewGuid();
                ServerOperations.Pages().CreateTestPage(pageIdLogIn, PageTitleLogin);
                ServerOperations.Widgets().AddControlToPage(pageIdLogIn, ControlTypes.LoginWidget, "Body", "Login");
                ServerOperations.Comments().SetDefaultLoginPageUrl(LoginURL);
            } 
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
            ServerOperations.Comments().DeleteAllComments(this.key);
        }

        private const string PageName = "NewsPage";
        private const string PageTitleLogin = "LogIn";
        private const string LoginURL = "~/login";
        private const string PageTemplateName = "Bootstrap.default";
        private const string NewsContent = "News content";
        private const string NewsTitle = "NewsTitle";
        private const string NewsAuthor = "TestNewsAuthor";
        private const string NewsSource = "TestNewsSource";
        private string key = "Telerik.Sitefinity.Modules.News.NewsManager_" + NewsManager.GetManager().Provider.Name;
        private const string ThreadType = "Telerik.Sitefinity.News.Model.NewsItem";
        private const string AdminUserName = "admin";
        private const string AdminPass = "admin@2";
    }
}
