using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using FeatherWidgets.TestUtilities.CommonOperations;
using FeatherWidgets.TestUtilities.CommonOperations.Templates;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.FilesMonitoring;
using Telerik.Sitefinity.Frontend.Mvc.Helpers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Models;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.TestIntegration.Data.Content;
using Telerik.Sitefinity.TestIntegration.Helpers;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.Navigation
{
    /// <summary>
    /// This is a class with navigation widget on page template tests.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable"), TestFixture]
    [Description("This is a class with navigation widget on page template.")]
    public class NavigationWidgetPageTemplateTests
    {
        /// <summary>
        /// Navigation widget on page template - All pages under currently opened page
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "FeatherWidgets.TestUtilities.CommonOperations.Templates.TemplateOperations.AddControlToTemplate(System.Guid,System.Web.UI.Control,System.String,System.String)"), Test]
        [Category(TestCategories.Navigation)]
        [Author(FeatherTeams.SitefinityTeam7)]
        public void NavigationWidgetOnPageTemplate_AllPagesUnderCurrentlyOpenedPage()
        {
            Guid templateId = default(Guid);

            try
            {
                templateId = this.templateOperation.DuplicatePageTemplate(OldTemplateName, TemplateName1);

                string url = UrlPath.ResolveAbsoluteUrl("~/" + UrlNamePrefix);

                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(NavigationController).FullName;
                var navigationController = new NavigationController();
                navigationController.TemplateName = "Horizontal";
                navigationController.SelectionMode = PageSelectionMode.CurrentPageChildren;
                mvcProxy.Settings = new ControllerSettings(navigationController);

                this.templateOperation.AddControlToTemplate(templateId, mvcProxy, PlaceHolder, CaptionNavigation);
                Guid pageId = this.locationGenerator.CreatePage(PageNamePrefix, PageTitlePrefix, UrlNamePrefix, null, null);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().SetTemplateToPage(pageId, templateId);

                for (int i = 1; i <= 4; i++)
                {
                    Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(SiblingPage + i);
                    Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageTitlePrefix + i, Guid.NewGuid(), pageId);
                }

                string responseContent = PageInvoker.ExecuteWebRequest(url);

                Assert.IsTrue(responseContent.Contains(Template), "The page template was not found!");

                for (int i = 1; i <= 4; i++)
                {
                    Assert.IsTrue(responseContent.Contains(PageTitlePrefix + i), "The page with this title was not found!");
                    Assert.IsFalse(responseContent.Contains(SiblingPage + i), "The page with this title was found!");
                }
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();           
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().DeletePageTemplate(templateId);                
            }
        }

        /// <summary>
        /// Navigation widget on page template - All sibling pages of currently opened page
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "FeatherWidgets.TestUtilities.CommonOperations.Templates.TemplateOperations.AddControlToTemplate(System.Guid,System.Web.UI.Control,System.String,System.String)"), Test]
        [Category(TestCategories.Navigation)]
        [Author(FeatherTeams.SitefinityTeam7)]
        public void NavigationWidgetOnPageTemplate_AllSiblingPagesOfCurrentlyOpenedPage()
        {
            Guid templateId = default(Guid);

            try
            {
                templateId = this.templateOperation.DuplicatePageTemplate(OldTemplateName, TemplateName);

                string url = UrlPath.ResolveAbsoluteUrl("~/" + UrlNamePrefix);

                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(NavigationController).FullName;
                var navigationController = new NavigationController();
                navigationController.TemplateName = "Horizontal";
                navigationController.SelectionMode = PageSelectionMode.CurrentPageSiblings;
                mvcProxy.Settings = new ControllerSettings(navigationController);

                this.templateOperation.AddControlToTemplate(templateId, mvcProxy, PlaceHolder, CaptionNavigation);
                Guid pageId = this.locationGenerator.CreatePage(PageNamePrefix, PageTitlePrefix, UrlNamePrefix, null, null);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().SetTemplateToPage(pageId, templateId);

                for (int i = 1; i <= 3; i++)
                {
                    Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(SiblingPage + i);
                    Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().CreatePage(PageTitlePrefix + i, Guid.NewGuid(), pageId);
                }

                string responseContent = PageInvoker.ExecuteWebRequest(url);

                Assert.IsTrue(responseContent.Contains(Template), "The page template was not found!");
                Assert.IsTrue(responseContent.Contains(PageTitlePrefix), "The page with this title was not found!");

                for (int i = 1; i <= 3; i++)
                {
                    Assert.IsTrue(responseContent.Contains(SiblingPage + i), "The page with this title was not found!");
                    Assert.IsFalse(responseContent.Contains(PageTitlePrefix + i), "The page with this title was found!");
                }
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();              
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().DeletePageTemplate(templateId);                
            }
        }

        /// <summary>
        /// Navigation widget on page template - Check if jqury exist on page
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "FeatherWidgets.TestUtilities.CommonOperations.Templates.TemplateOperations.AddControlToTemplate(System.Guid,System.Web.UI.Control,System.String,System.String)"), Test]
        [Category(TestCategories.Navigation)]
        [Author(FeatherTeams.SitefinityTeam7)]
        public void NavigationWidgetOnPageTemplate_JQueryExist_Navigation_Pills()
        {
            Guid templateId = default(Guid);
            Guid pageId = Guid.Empty;

            FeatherServerOperations.FeatherModule().EnsureFeatherEnabled();

            try
            {
                templateId = this.templateOperation.DuplicatePageTemplate(OldTemplateName, TemplateName);
                string url = UrlPath.ResolveAbsoluteUrl("~/" + UrlNamePrefix);
                var mvcProxy = CreateNavigationController();

                this.templateOperation.AddControlToTemplate(templateId, mvcProxy, PlaceHolder, CaptionNavigation);
                pageId = this.locationGenerator.CreatePage(PageNamePrefix, PageTitlePrefix, UrlNamePrefix, null, null);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().SetTemplateToPage(pageId, templateId);

                string responseContent = WebRequestHelper.GetPageWebContent(url);

                var httpContextWrapper = GetHttpContextWrapper(url);
                var htmlHelper = GetHtmlHelper(httpContextWrapper);
                var scriptHtml = ResourceHelper.Script(htmlHelper, ScriptRef.JQuery, "top", true).ToHtmlString();

                Assert.IsFalse(!responseContent.Contains(scriptHtml), "The jquery not found!");
            }
            finally
            {
                if (pageId != Guid.Empty)
                {
                    Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeletePage(pageId);
                }

                if (templateId != Guid.Empty)
                {
                    Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().DeletePageTemplate(templateId);
                }
            }
        }

        private static MvcControllerProxy CreateNavigationController()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(NavigationController).FullName;

            var navigationController = new NavigationController();
            navigationController.TemplateName = "Pills";
            navigationController.SelectionMode = PageSelectionMode.CurrentPageSiblings;
            mvcProxy.Settings = new ControllerSettings(navigationController);

            return mvcProxy;
        }

        private static HtmlHelper GetHtmlHelper(System.Web.HttpContextWrapper httpContextWrapper)
        {
            var dummyViewContext = new ViewContext();
            dummyViewContext.HttpContext = httpContextWrapper;
            var dummyViewDataContainer = (IViewDataContainer)new ViewPage();
            var htmlHelper = new System.Web.Mvc.HtmlHelper(dummyViewContext, dummyViewDataContainer);

            return htmlHelper;
        }

        private static System.Web.HttpContextWrapper GetHttpContextWrapper(string url)
        {
            var request = new System.Web.HttpRequest(string.Empty, url, null);
            var response = new System.Web.HttpResponse(null);
            var context = new System.Web.HttpContext(request, response);
            var httpContextWrapper = new System.Web.HttpContextWrapper(context);

            return httpContextWrapper;
        }

        #region Fields and constants

        private const string PageNamePrefix = "NavigationPage";
        private const string PageTitlePrefix = "Navigation Page";
        private const string UrlNamePrefix = "navigation-page";
        private const string SiblingPage = "Sibling Page";
        private const string TemplateName = "Bootstrap.defaultNew2";
        private const string TemplateName1 = "Bootstrap.defaultNew1";
        private const string PlaceHolder = "Contentplaceholder1";
        private const string CaptionNavigation = "Navigation";
        private const string Template = "Bootstrap";
        private const string OldTemplateName = "Bootstrap.default";
        private TemplateOperations templateOperation = new TemplateOperations();
        private PageContentGenerator locationGenerator = new PageContentGenerator();

        #endregion
    }
}
