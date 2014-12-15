using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using FeatherWidgets.TestUtilities.CommonOperations;
using FeatherWidgets.TestUtilities.CommonOperations.Templates;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Models;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestIntegration.Data.Content;
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
        [Category(TestCategories.Navigation), Ignore]
        [Author("FeatherTeam")]
        public void NavigationWidgetOnPageTemplate_AllPagesUnderCurrentlyOpenedPage()
        {
            string newLayoutTemplatePath = null;
            Guid templateId = default(Guid);

            try
            {
                PageManager pageManager = PageManager.GetManager();
                int templatesCount = pageManager.GetTemplates().Count();

                string layoutTemplatePath = Path.Combine(this.templateOperation.SfPath, "ResourcePackages", "Bootstrap", "MVC", "Views", "Layouts", "default.cshtml");
                newLayoutTemplatePath = Path.Combine(this.templateOperation.SfPath, "ResourcePackages", "Bootstrap", "MVC", "Views", "Layouts", "defaultNew1.cshtml");

                File.Copy(layoutTemplatePath, newLayoutTemplatePath);

                this.templateOperation.WaitForTemplatesCountToIncrease(templatesCount, 1);

                templateId = this.templateOperation.GetTemplateIdByTitle(TemplateName1);

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
                File.Delete(newLayoutTemplatePath);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();           
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().DeletePageTemplate(templateId);                
            }
        }

        /// <summary>
        /// Navigation widget on page template - All sibling pages of currently opened page
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "FeatherWidgets.TestUtilities.CommonOperations.Templates.TemplateOperations.AddControlToTemplate(System.Guid,System.Web.UI.Control,System.String,System.String)"), Test]
        [Category(TestCategories.Navigation), Ignore]
        [Author("FeatherTeam")]
        public void NavigationWidgetOnPageTemplate_AllSiblingPagesOfCurrentlyOpenedPage()
        {
            string newLayoutTemplatePath = null;
            Guid templateId = default(Guid);

            try
            {
                PageManager pageManager = PageManager.GetManager();
                int templatesCount = pageManager.GetTemplates().Count();

                var layoutTemplatePath = Path.Combine(this.templateOperation.SfPath, "ResourcePackages", "Bootstrap", "MVC", "Views", "Layouts", "default.cshtml");
                newLayoutTemplatePath = Path.Combine(this.templateOperation.SfPath, "ResourcePackages", "Bootstrap", "MVC", "Views", "Layouts", "defaultNew2.cshtml");

                File.Copy(layoutTemplatePath, newLayoutTemplatePath);

                this.templateOperation.WaitForTemplatesCountToIncrease(templatesCount, 1);

                templateId = this.templateOperation.GetTemplateIdByTitle(TemplateName);

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
                File.Delete(newLayoutTemplatePath);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();              
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().DeletePageTemplate(templateId);                
            }
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
        private TemplateOperations templateOperation = new TemplateOperations();
        private PageContentGenerator locationGenerator = new PageContentGenerator();

        #endregion
    }
}
