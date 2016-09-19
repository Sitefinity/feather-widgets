using System;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;
using ArtOfTest.WebAii.Core;

namespace FeatherWidgets.TestUI.TestCases.Identity
{
    /// <summary>
    /// VerifyLoginWidgetToTemplateWhenOpenItemDetailViewAndForgotPassword test class.
    /// </summary>
    [TestClass]
    public class VerifyLoginWidgetToTemplateWhenOpenItemDetailViewAndForgotPassword_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifyLoginWidgetToTemplateWhenOpenItemDetailViewAndForgotPassword
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.FeatherTeam),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.LoginForm),
        TestCategory(FeatherTestCategories.Bootstrap)]
        public void VerifyLoginWidgetToTemplateWhenOpenItemDetailViewAndForgotPassword()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/sitefinity/pages", false));
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().User().EnsureAdminLoggedIn());
            BAT.Macros().NavigateTo().Design().PageTemplates(this.Culture);
            BAT.Wrappers().Backend().PageTemplates().PageTemplateMainScreen().OpenTemplateEditor(TemplateTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(LoginStatusWidget, "Body");
            BAT.Wrappers().Backend().PageTemplates().PageTemplateModifyScreen().PublishTemplate();
            
            //Show "Forgotten Password" link
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageTitle);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(LoginFormWidget);
            BATFeather.Wrappers().Backend().Identity().LoginFormWrapper().SelectAllowUserToResetPasswordCheckbox();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            //Verify Login widet when open post item
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), false, this.Culture);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Blogs().BlogsWrapper().IsBlogPostTitlesPresentOnThePageFrontend(this.postTitle));
            Assert.IsTrue(ActiveBrowser.ContainsText(LoggedInText), "Text was not found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText(LogoutText), "Text was not found on the page");
            BATFeather.Wrappers().Frontend().CommonWrapper().VerifySelectedAnchorLink(PostTitle, this.expectedUrl);
            Assert.IsTrue(ActiveBrowser.ContainsText(LoggedInText), "Text was not found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText(LogoutText), "Text was not found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText(PostTitle), "Text was not found on the page");

            BAT.Macros().User().LogOut();
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), false, this.Culture, new HtmlFindExpression("data-sf-role=~sf-logged-out-view"));
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Blogs().BlogsWrapper().IsBlogPostTitlesPresentOnThePageFrontend(this.postTitle));
            Assert.IsTrue(ActiveBrowser.ContainsText(LoginText), "Text was not found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText(RegisterText), "Text was not found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText(ForgottenPasswordText), "Text was not found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText(UsernameText), "Text was not found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText(PasswordText), "Text was not found on the page");

            BATFeather.Wrappers().Frontend().CommonWrapper().VerifySelectedAnchorLink(PostTitle, this.expectedUrl);
            Assert.IsTrue(ActiveBrowser.ContainsText(LoginText), "Text was not found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText(RegisterText), "Text was not found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText(ForgottenPasswordText), "Text was not found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText(UsernameText), "Text was not found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText(PasswordText), "Text was not found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText(PostTitle), "Text was not found on the page");

            //Verify Login widget when open Forgotten password link
            BAT.Macros().NavigateTo().CustomPage("~/" + PageTitle.ToLower(), false, this.Culture);
            BATFeather.Wrappers().Frontend().Identity().LoginFormWrapper().PressForgottenPasswordLink();
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Blogs().BlogsWrapper().IsBlogPostTitlesPresentOnThePageFrontend(this.postTitle));
            Assert.IsTrue(ActiveBrowser.ContainsText(LoginText), "Text was not found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText(RegisterText), "Text was not found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText("Forgot your password?"), "Text was not found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText("Enter your login email address and you will receive email with a link to reset your password."), "Text was not found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText("Email"), "Text was not found on the page");
            Assert.IsTrue(ActiveBrowser.ContainsText("Send"), "Text was not found on the page");
        }
        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// 
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string TemplateTitle = "TestTemplatePureMVC";
        private const string PageTitle = "TestPageWithBlogPostsWidget";
        private const string BlogTitle = "TestBlog";
        private const string PostTitle = "post1";
        private const string LogoutText = "Logout";
        private const string LoginText = "Login";
        private const string RegisterText = "Register now";
        private const string ForgottenPasswordText = "Forgotten Password";
        private const string UsernameText = "Username";
        private const string PasswordText = "Password";
        private const string LoggedInText = "You are already logged in";
        private const string LoginStatusWidget = "Login / Logout button";
        private const string LoginFormWidget = "Login form";
        private readonly string[] postTitle = new string[] { PostTitle };
        private readonly string expectedUrl = string.Format("/TestPageWithBlogPostsWidget/TestBlog/{0}/{1:00}/{2:00}/post1", DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);
    }
}
