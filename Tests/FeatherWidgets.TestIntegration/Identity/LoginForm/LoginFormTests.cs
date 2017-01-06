using System;
using System.Linq;
using System.Web;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Microsoft.Owin.Security;
using Telerik.Sitefinity.Frontend.ContentBlock.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginForm;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.TestIntegration.Helpers;
using Telerik.Sitefinity.Web;
using SitefinityTestUtilities = Telerik.Sitefinity.TestUtilities.CommonOperations;

namespace FeatherWidgets.TestIntegration.Identity.LoginForm
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login"), TestFixture]
    public class LoginFormTests
    {
        /*
        [Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.SitefinityTeam3)]
        [Description("Checks whether authenticating with login form's model will result in current identity with correctly set claim type properties.")]
        public void AuthenticateUser_IdentityHasClaimTypes()
        {
            var owinContext = SystemManager.CurrentHttpContext.Request.GetOwinContext();              
            owinContext.Authentication.SignOut(new AuthenticationProperties(), ClaimsManager.CurrentAuthenticationModule.GetSignOutAuthenticationTypes().ToArray());

            SitefinityTestUtilities.ServerOperations.Users().CreateUser(this.userEmail, this.password, "test", "test", true, "AuthenticateUser", "IdentityHasClaimTypes", SecurityConstants.AppRoles.FrontendUsers);

            try
            {
                var model = new LoginFormModel();
                model.Authenticate(new LoginFormViewModel() { UserName = this.userEmail, Password = this.password }, SystemManager.CurrentHttpContext);
                owinContext.Authentication.AuthenticationResponseChallenge.


                var currentIdentity = ClaimsManager.GetCurrentIdentity();

                Assert.AreEqual(this.userEmail, currentIdentity.Name, "The name of the current identity did not match the user.");
                Assert.IsNotNull(currentIdentity.NameClaimType, "NameClaimType was not set in the current identity.");
                Assert.IsNotNull(currentIdentity.RoleClaimType, "RoleClaimType was not set in the current identity.");
                Assert.AreEqual("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", currentIdentity.NameClaimType, "NameClaimType did not have the expected value.");
                Assert.AreEqual("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", currentIdentity.RoleClaimType, "RoleClaimType did not have the expected value.");
            }
            finally
            {
                owinContext.Authentication.SignOut(new AuthenticationProperties(), ClaimsManager.CurrentAuthenticationModule.GetSignOutAuthenticationTypes().ToArray());
                //// SecurityManager.Logout();
                //// SitefinityTestUtilities.ServerOperations.Users().DeleteUsers(new[] { this.userEmail });
            }
        }*/

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)"), Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.SitefinityTeam3)]
        [Description("Checks whether redirect url is preserved in form action attribute.")]
        public void PostForm_RedirectUrlIsPreserved()
        {
            string loginFormPageUrl = UrlPath.ResolveAbsoluteUrl("~/" + this.urlNamePrefix + this.pageIndex);
            var redirectURL = "http://localhost/";
            var redirectQuery = "?RedirectUrl=";
            var fullQueryString = redirectQuery + redirectURL;
            this.pageOperations = new PagesOperations();

            var userId = ClaimsManager.GetCurrentUserId();
            var user = UserManager.GetManager().GetUser(userId);

            try
            {
                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(LoginFormController).FullName;
                var loginFormController = new LoginFormController();
                mvcProxy.Settings = new ControllerSettings(loginFormController);

                this.pageOperations.CreatePageWithControl(mvcProxy, this.pageNamePrefix, this.pageTitlePrefix, this.urlNamePrefix, this.pageIndex);

                SecurityManager.Logout();
                var responseContent = PageInvoker.ExecuteWebRequest(loginFormPageUrl + fullQueryString);

                int startPosition = responseContent.IndexOf(this.actionSearchString, StringComparison.OrdinalIgnoreCase) + this.actionSearchString.Length;
                Assert.IsTrue(startPosition > 0, "The page is not containing a form with action!");

                int endPosition = responseContent.IndexOf(' ', startPosition);
                string actionValue = responseContent.Substring(startPosition, endPosition - startPosition);
                string searchValue = redirectQuery + HttpUtility.UrlEncode(redirectURL);

                Assert.IsTrue(actionValue.Contains(searchValue), "The action URL no longer contains ReturnUrl");
            }
            finally
            {
                using (new AuthenticateUserRegion(user))
                {
                    this.pageOperations.DeletePages();
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)"),
            System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login"), Test]
        [Category(TestCategories.Identity)]
        [Author(FeatherTeams.SitefinityTeam3)]
        [Ignore] // Failed more than 40 times (passing locally)
        [Description("Checks whether login form will submit only to its post action.")]
        public void PostForm_Login_PostsItselfOnly()
        {
            this.pageOperations = new PagesOperations();
            var pageManager = PageManager.GetManager();
            Guid pageId = Guid.Empty;
            string loginFormPageUrl = UrlPath.ResolveAbsoluteUrl("~/" + this.urlNamePrefix);
            var userId = ClaimsManager.GetCurrentUserId();
            var user = UserManager.GetManager().GetUser(userId);

            try
            {
                var template = pageManager.GetTemplates().Where(t => t.Name == this.templateName).FirstOrDefault();
                Assert.IsNotNull(template, "Template was not found");

                pageId = Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.FeatherServerOperations.Pages().CreatePageWithTemplate(template, this.pageNamePrefix, this.urlNamePrefix);
                this.pageOperations.AddLoginFormWidgetToPage(pageId, "Contentplaceholder1");

                SecurityManager.Logout();

                var responseContent = PageInvoker.ExecuteWebRequest(loginFormPageUrl);

                var expectedActionUrl = "?sf_cntrl_id=";
                Assert.IsTrue(responseContent.Contains(string.Format("action=\"{0}", expectedActionUrl)), "The action URL doesn't contain controller ID.");
            }
            finally
            {
                using (new AuthenticateUserRegion(user))
                {
                    var pageNode = pageManager.GetPageNodes().Where(p => p.Id == pageId).SingleOrDefault();
                    if (pageNode != null)
                    {
                        pageManager.Delete(pageNode);
                        pageManager.SaveChanges();
                    }
                }
            }
        }

        [Test]
        [Author(FeatherTeams.SitefinityTeam3)]
        [Description("Checks whether login form redirects after login on the page set by LoginRedirectPageId.")]
        public void PostForm_LogOnLogOff()
        {
            string loginFormPageUrl = UrlPath.ResolveAbsoluteUrl("~/" + this.urlNamePrefix + this.pageIndex);
            this.pageOperations = new PagesOperations();
            var userId = ClaimsManager.GetCurrentUserId();
            var user = UserManager.GetManager().GetUser(userId);

            try
            {
                ////Create page with login form and set LoginRedirectPageId to the newly created page above
                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(LoginFormController).FullName;
                var loginFormController = new LoginFormController();
                mvcProxy.Settings = new ControllerSettings(loginFormController);
                this.pageOperations.CreatePageWithControl(mvcProxy, this.pageNamePrefix, this.pageTitlePrefix, this.urlNamePrefix, this.pageIndex);

                SecurityManager.Logout();

                ////create new user to Authenticate against newly created login form
                SitefinityTestUtilities.ServerOperations.Users().CreateUser(this.userEmail, this.password, "test", "test", true, "AuthenticateUser", "IdentityHasClaimTypes", SecurityConstants.AppRoles.FrontendUsers);

                string postString = "UserName=" + this.userEmail + "&Password=" + this.password;
                using (PageInvokerRegion region = new PageInvokerRegion())
                {
                    var responseContent = PageInvoker.PostWebRequest(loginFormPageUrl, postString);
                    Assert.IsTrue(responseContent.Contains("You are already logged in"), "The user was not logged in properly on the login form!");

                    ////string logOutUrl = "http://localhost/Sitefinity/SignOut?sts_signout=true&redirect_uri=http://localhost/" + this.urlNamePrefix + this.pageIndex;
                    ////responseContent = PageInvoker.ExecuteWebRequest(logOutUrl, false);
                    ////Assert.IsFalse(responseContent.Contains("You are already logged in"), "User was not logget out!");
                }
            }
            finally
            {
                using (new AuthenticateUserRegion(user))
                {
                    this.pageOperations.DeletePages();
                }

                SecurityManager.Logout();
                SitefinityTestUtilities.ServerOperations.Users().DeleteUserAndProfile(this.userEmail);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)"),
            System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login"), Test]
        [Author(FeatherTeams.SitefinityTeam3)]
        [Description("Checks whether login form redirects after login on a page with property LoginRedirectPageId set.")]
        public void PostForm_LoginRedirectPageId()
        {
            string loginFormPageUrl = UrlPath.ResolveAbsoluteUrl("~/" + this.urlNamePrefix + this.pageIndex);
            this.pageOperations = new PagesOperations();
            var userId = ClaimsManager.GetCurrentUserId();
            var user = UserManager.GetManager().GetUser(userId);

            try
            {
                ////Create simple page with a content block to redirect on it
                var mvcProxyContentBlock = new MvcControllerProxy();
                mvcProxyContentBlock.ControllerName = typeof(ContentBlockController).FullName;
                var contentBlockController = new ContentBlockController();
                contentBlockController.Content = this.searchValueFirst;
                mvcProxyContentBlock.Settings = new ControllerSettings(contentBlockController);
                Guid contentBlockPageID = this.pageOperations.CreatePageWithControl(
                    mvcProxyContentBlock, this.pageNamePrefixContentBlockPage, this.pageTitlePrefixContentBlockPage, this.urlNamePrefixContentBlockPage, this.pageIndexContentBlockFirstPage);

                ////Create page with login form and set LoginRedirectPageId to the newly created page above
                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(LoginFormController).FullName;
                var loginFormController = new LoginFormController();
                loginFormController.Model.LoginRedirectPageId = contentBlockPageID;
                mvcProxy.Settings = new ControllerSettings(loginFormController);
                this.pageOperations.CreatePageWithControl(mvcProxy, this.pageNamePrefix, this.pageTitlePrefix, this.urlNamePrefix, this.pageIndex);

                SecurityManager.Logout();

                ////create new user to Authenticate against newly created login form
                SitefinityTestUtilities.ServerOperations.Users().CreateUser(this.userEmail, this.password, "test", "test", true, "AuthenticateUser", "IdentityHasClaimTypes", SecurityConstants.AppRoles.FrontendUsers);

                string postString = "UserName=" + this.userEmail + "&Password=" + this.password;
                var responseContent = PageInvoker.PostWebRequest(loginFormPageUrl, postString);

                Assert.IsTrue(responseContent.Contains(this.searchValueFirst), "The request was not redirected to the proper page set in LoginRedirectPageId!");
            }
            finally
            {
                using (new AuthenticateUserRegion(user))
                {
                    this.pageOperations.DeletePages();
                }

                SecurityManager.Logout();
                SitefinityTestUtilities.ServerOperations.Users().DeleteUserAndProfile(this.userEmail);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)"),
            System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login"), Test]
        [Author(FeatherTeams.SitefinityTeam3)]
        [Description("Checks whether login form redirects on return url after login.")]
        public void PostForm_LoginRedirectFromQueryString()
        {
            string loginFormPageUrl = UrlPath.ResolveAbsoluteUrl("~/" + this.urlNamePrefix + this.pageIndex);
            this.pageOperations = new PagesOperations();
            var userId = ClaimsManager.GetCurrentUserId();
            var user = UserManager.GetManager().GetUser(userId);

            try
            {
                var mvcProxyContentBlock = new MvcControllerProxy();
                mvcProxyContentBlock.ControllerName = typeof(ContentBlockController).FullName;

                ////Create first simple page with a content block to redirect on it
                var contentBlockController = new ContentBlockController();
                contentBlockController.Content = this.searchValueFirst;
                mvcProxyContentBlock.Settings = new ControllerSettings(contentBlockController);
                this.pageOperations.CreatePageWithControl(
                    mvcProxyContentBlock, this.pageNamePrefixContentBlockPage, this.pageTitlePrefixContentBlockPage, this.urlNamePrefixContentBlockPage, this.pageIndexContentBlockFirstPage);

                ////Create second simple page with a content block to redirect on it
                var contentBlockControllerSecond = new ContentBlockController();
                contentBlockControllerSecond.Content = this.searchValueSecond;
                mvcProxyContentBlock.Settings = new ControllerSettings(contentBlockControllerSecond);
                this.pageOperations.CreatePageWithControl(
                    mvcProxyContentBlock, this.pageNamePrefixContentBlockPage, this.pageTitlePrefixContentBlockPage, this.urlNamePrefixContentBlockPage, this.pageIndexContentBlockSecondPage);

                ////Create page with login form
                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(LoginFormController).FullName;
                var loginFormController = new LoginFormController();
                mvcProxy.Settings = new ControllerSettings(loginFormController);
                this.pageOperations.CreatePageWithControl(mvcProxy, this.pageNamePrefix, this.pageTitlePrefix, this.urlNamePrefix, this.pageIndex);

                SecurityManager.Logout();

                ////create new user to Authenticate against newly created login form
                SitefinityTestUtilities.ServerOperations.Users().CreateUser(this.userEmail, this.password, "test", "test", true, "AuthenticateUser", "IdentityHasClaimTypes", SecurityConstants.AppRoles.FrontendUsers);

                ////There is few ways to redirect to another page
                ////First method is to combine realm param with redirect_uri param to get the full redirect url
                ////Example: ?realm=http://localhost:8086/&redirect_uri=/Sitefinity/Dashboard
                ////Second method is to use only realm or redirect_uri param to get the full redirect url
                ////Example: ?redirect_uri=http://localhost:8086/Sitefinity/Dashboard
                ////Third method is to get ReturnUrl param
                ////Example: ?ReturnUrl=http://localhost:8086/Sitefinity/Dashboard

                string postString = "UserName=" + this.userEmail + "&Password=" + this.password;
                string responseContent;

                using (PageInvokerRegion region = new PageInvokerRegion())
                {
                    string testURL1 = loginFormPageUrl + "?redirect_uri=" + this.urlNamePrefixContentBlockPage + this.pageIndexContentBlockFirstPage + "&realm=http://localhost/"
                        + "&ReturnUrl=http://localhost/" + this.urlNamePrefixContentBlockPage + this.pageIndexContentBlockSecondPage;

                    responseContent = PageInvoker.ExecuteWebRequest(testURL1);
                    responseContent = PageInvoker.PostWebRequest(testURL1, postString);

                    Assert.IsTrue(responseContent.Contains(this.searchValueFirst), "The request was not redirected to the proper page set in request url!");
                }

                using (PageInvokerRegion region = new PageInvokerRegion())
                {
                    string testURL2 = loginFormPageUrl + "?realm=http://localhost/" + this.urlNamePrefixContentBlockPage + this.pageIndexContentBlockFirstPage;
                    responseContent = PageInvoker.ExecuteWebRequest(testURL2);
                    responseContent = PageInvoker.PostWebRequest(testURL2, postString);

                    Assert.IsTrue(responseContent.Contains(this.searchValueFirst), "The request was not redirected to the proper page set in request url!");
                }

                using (PageInvokerRegion region = new PageInvokerRegion())
                {
                    string testURL3 = loginFormPageUrl + "?redirect_uri=http://localhost/" + this.urlNamePrefixContentBlockPage + this.pageIndexContentBlockFirstPage;
                    responseContent = PageInvoker.ExecuteWebRequest(testURL3);
                    responseContent = PageInvoker.PostWebRequest(testURL3, postString);

                    Assert.IsTrue(responseContent.Contains(this.searchValueFirst), "The request was not redirected to the proper page set in request url!");
                }

                using (PageInvokerRegion region = new PageInvokerRegion())
                {
                    string testURL4 = loginFormPageUrl + "?ReturnUrl=http://localhost/" + this.urlNamePrefixContentBlockPage + this.pageIndexContentBlockSecondPage;
                    responseContent = PageInvoker.ExecuteWebRequest(testURL4);
                    responseContent = PageInvoker.PostWebRequest(testURL4, postString);

                    Assert.IsTrue(responseContent.Contains(this.searchValueSecond), "The request was not redirected to the proper page set in request url!");
                }
            }
            finally
            {
                using (new AuthenticateUserRegion(user))
                {
                    this.pageOperations.DeletePages();
                }

                SecurityManager.Logout();
                SitefinityTestUtilities.ServerOperations.Users().DeleteUserAndProfile(this.userEmail);
            }
        }

        private string userEmail = "user_IdentityHasClaimTypes@test.test";
        private string password = "admin@2";

        private string templateName = "Bootstrap.default";
        private string pageNamePrefix = "LoginFormPage";
        private string pageTitlePrefix = "Login Form";
        private string urlNamePrefix = "login-form";
        private string actionSearchString = "action=";
        private int pageIndex = 1;

        private string pageNamePrefixContentBlockPage = "ContentBlockPage";
        private string pageTitlePrefixContentBlockPage = "Content Block";
        private string urlNamePrefixContentBlockPage = "content-block";
        private string searchValueFirst = "Realm Page Text";
        private string searchValueSecond = "ReturnUrl Page Text";
        private int pageIndexContentBlockFirstPage = 1;
        private int pageIndexContentBlockSecondPage = 2;

        private PagesOperations pageOperations;
    }
}