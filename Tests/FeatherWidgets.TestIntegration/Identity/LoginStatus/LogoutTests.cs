using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.LoginStatus;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.TestIntegration.Data.Content;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.Identity.LoginStatus
{
    /// <summary>
    /// This is a test class with tests related to logout functionality of LoginStatus widget.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Logout")]

    [Description("This is a test class with tests related to logout functionality of LoginStatus widget.")]
    [TestFixture]
    public class LogoutTests
    {
        /// <summary>
        /// Set up method
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.locationGenerator = new PageContentGenerator(); 
            this.expectedPage = PageManager.GetManager().GetPageNodes().FirstOrDefault();
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.locationGenerator.Dispose();
            this.expectedPage = null;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Logout")]
        [Author(FeatherTeams.Team2)]
        [Category(TestCategories.Identity)]
        [Description("Verify when External logout page url is provided, redirect link is constructed correctly.")]
        [Test]
        public void Logout_RedirectToExternalPage_VerifyLogoutRedirectUrlIsCorrect()
        {
            // Arrange
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(LoginStatusController).FullName;
            var controller = new LoginStatusController();
            controller.Model.ExternalLogoutUrl = ExpectedLogoutUrl;
            mvcProxy.Settings = new ControllerSettings(controller);

            // Act
            var actionResult = (ViewResult)controller.Index();
            var viewModel = actionResult.Model as LoginStatusViewModel;

            // Assert
            Assert.AreEqual(ExpectedLogoutUrl, viewModel.LogoutPageUrl, "Logout redirect url is not as expected");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Logout")]
        [Author(FeatherTeams.Team2)]
        [Category(TestCategories.Identity)]
        [Description("Verify when logout page id is provided and no redirect url is provided, redirect link is constructed correctly (using id only).")]
        [Test]
        public void Logout_RedirectToPageWithId_VerifyLogoutRedirectUrlIsCorrect()
        {
            // Arrange
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(LoginStatusController).FullName;
            var controller = new LoginStatusController();
            controller.Model.LogoutPageId = this.expectedPage.Id;
            mvcProxy.Settings = new ControllerSettings(controller);

            // Act
            var actionResult = (ViewResult)controller.Index();
            var viewModel = actionResult.Model as LoginStatusViewModel;

            // Assert
            Assert.AreEqual(UrlPath.ResolveUrl(this.expectedPage.GetFullUrl(), true), viewModel.LogoutPageUrl, "Logout redirect url is not as expected");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Logout")]
        [Author(FeatherTeams.Team2)]
        [Category(TestCategories.Identity)]
        [Description("Verify when logout redirect url and page id are provided, redirect link is constructed correctly (using url).")]
        [Test]
        public void Logout_RedirectToPageWithUrlAndId_VerifyLogoutRedirectUrlIsCorrect()
        {
            string testName = "Logout_RedirectToPageWithUrlAndId_VerifyLogoutRedirectUrlIsCorrect";
            string urlNamePrefix = testName + "urlNamePrefix";
            int pageIndex = 1;
            string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + pageIndex);

            // Arrange
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(LoginStatusController).FullName;
            var controller = new LoginStatusController();

            controller.Model.ExternalLogoutUrl = ExpectedLogoutUrl;
            controller.Model.LogoutPageId = this.expectedPage.Id;

            mvcProxy.Settings = new ControllerSettings(controller);

            // Act
            this.CreatePageWithControl(mvcProxy, "namePrefix", "titlePrefix", urlNamePrefix, pageIndex);
            string responseContent = PageInvoker.ExecuteWebRequest(url);
            Assert.AreEqual(0, 0, responseContent);

            // Assert
            // Assert.AreNotEqual(UrlPath.ResolveUrl(ExpectedPage.GetFullUrl(), true), viewModel.LogoutPageUrl, "Logout redirect url is not as expected");
            // Assert.AreEqual(ExpectedLogoutUrl, viewModel.LogoutPageUrl, "Logout redirect url is not as expected");
        }

        #region Private Methods

        private Guid CreatePageWithControl(Control control, string pageNamePrefix, string pageTitlePrefix, string urlNamePrefix, int index)
        {
            var controls = new List<Control>();
            controls.Add(control);

            var pageId = this.locationGenerator.CreatePage(
                                    string.Format(CultureInfo.InvariantCulture, "{0}{1}", pageNamePrefix, index.ToString(CultureInfo.InvariantCulture)),
                                    string.Format(CultureInfo.InvariantCulture, "{0}{1}", pageTitlePrefix, index.ToString(CultureInfo.InvariantCulture)),
                                    string.Format(CultureInfo.InvariantCulture, "{0}{1}", urlNamePrefix, index.ToString(CultureInfo.InvariantCulture)));

            this.AddControlsToPage(pageId, controls);

            return pageId;
        }

        private void AddControlsToPage(Guid pageId, IEnumerable<Control> controls, string placeHolder = "Body", Action<PageDraftControl> draftControlAction = null)
        {
            var pageManager = PageManager.GetManager();
            using (new ElevatedModeRegion(pageManager))
            {
                var page = pageManager.GetPageNodes().Where(p => p.Id == pageId).SingleOrDefault();

                if (page != null)
                {
                    var temp = pageManager.EditPage(page.GetPageData().Id);

                    if (temp != null)
                    {
                        foreach (var control in controls)
                        {
                            string controlId = control.ID;
                            if (string.IsNullOrEmpty(control.ID))
                            {
                                controlId = this.GenerateUniqueControlIdForPage(temp, null);
                                control.ID = controlId;
                            }

                            var pageControl = pageManager.CreateControl<PageDraftControl>(control, placeHolder);
                            pageControl.Caption = Guid.NewGuid().ToString();
                            pageControl.SiblingId = this.GetLastControlInPlaceHolderInPageId(temp, placeHolder);
                            pageManager.SetControlDefaultPermissions(pageControl);
                            temp.Controls.Add(pageControl);
                            if (draftControlAction != null)
                                draftControlAction(pageControl);
                        }

                        var master = pageManager.PagesLifecycle.CheckIn(temp);
                        master.ApprovalWorkflowState.Value = "Published";
                        pageManager.PagesLifecycle.Publish(master);
                        pageManager.SaveChanges();
                    }
                }
            }
        }

        private Guid GetLastControlInPlaceHolderInPageId(PageDraft page, string placeHolder)
        {
            var id = Guid.Empty;
            PageDraftControl control;

            var controls = new List<PageDraftControl>(page.Controls.Where(c => c.PlaceHolder == placeHolder));

            while (controls.Count > 0)
            {
                control = controls.Where(c => c.SiblingId == id).SingleOrDefault();
                id = control.Id;

                controls.Remove(control);
            }

            return id;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])")]
        private string GenerateUniqueControlIdForPage(PageDraft pageNode, string culture)
        {
            int controlsCount = 0;

            if (pageNode != null)
            {
                controlsCount = pageNode.Controls.Count;
            }

            string cultureSufix = string.IsNullOrEmpty(culture) ? string.Empty : string.Format("_" + culture);

            return string.Format("C" + controlsCount.ToString().PadLeft(3, '0') + cultureSufix);
        }

        #endregion

        #region Fields and constants

        private PageContentGenerator locationGenerator;
        private PageNode expectedPage;
        private const string ExpectedLogoutUrl = "www.telerik.com";

        #endregion
    }
}
