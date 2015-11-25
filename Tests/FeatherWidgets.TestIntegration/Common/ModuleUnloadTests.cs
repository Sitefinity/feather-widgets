using System;
﻿using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.ContentBlock.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Pages.Model;

namespace FeatherWidgets.TestIntegration.Common
{
    /// <summary>
    /// This class contains tests for unloading of the Feather module.
    /// </summary>
    [TestFixture]
    [Category(TestCategories.Common)]
    [Description("This class contains tests for unloading of the Feather module.")]
    public class ModuleUnloadTests
    {
        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks whether after deactivating Feather the Sitefinity pages doesn't throw errors on frontend.")]
        public void DeactivatingFeather_WidgetOnPage_VerifyFrontend()
        {
            var moduleOperations = new Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.FeatherModuleOperations();
            var pageOperations = new PagesOperations();

            moduleOperations.EnsureFeatherEnabled();
            
            try
            {
                var mvcProxy = new MvcControllerProxy();
                mvcProxy.ControllerName = typeof(ContentBlockController).FullName;
                var contentBlockController = new ContentBlockController();
                contentBlockController.Content = ModuleUnloadTests.CbContent;
                mvcProxy.Settings = new ControllerSettings(contentBlockController);

                var pageId = pageOperations.CreatePageWithControl(mvcProxy, this.pageNamePrefix, this.pageTitlePrefix, this.urlNamePrefix, this.pageIndex);
                

                string url = UrlPath.ResolveAbsoluteUrl("~/" + this.urlNamePrefix + this.pageIndex);
                string responseContent = PageInvoker.ExecuteWebRequest(url);
                string responseContentInEdit = PageInvoker.ExecuteWebRequest(url + "/Action/Edit");

                Assert.IsTrue(responseContent.Contains(ModuleUnloadTests.CbContent), "Content was not found!");
                Assert.IsTrue(responseContentInEdit.Contains(ModuleUnloadTests.CbContent), "Content was not found!");

                this.UnlockPage(pageId);

                moduleOperations.DeactivateFeather();

                responseContent = PageInvoker.ExecuteWebRequest(url);
                responseContentInEdit = PageInvoker.ExecuteWebRequest(url + "/Action/Edit");

                Assert.IsFalse(responseContent.Contains(ModuleUnloadTests.CbContent), "Content was found after deactivate!");
                Assert.IsFalse(responseContentInEdit.Contains(ModuleUnloadTests.CbContent), "Content was found after deactivate!");
                Assert.IsTrue(responseContentInEdit.Contains("This widget doesn't work, because <strong>Feather</strong> module has been deactivated."), "Error message is not displayed in zone editor!");
            }
            finally
            {
                pageOperations.DeletePages();
                moduleOperations.ActivateFeather();
            }
        }

        #region Toolboxes

        #region Page edit

        /// <summary>
        /// Checks whether after deactivating Feather the page edit toolbox on hybrid page doesn't contain Feather widgets.
        /// </summary>
        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks whether after deactivating Feather the page edit toolbox on hybrid page doesn't contain Feather widgets.")]
        public void DeactivateFeather_WidgetsInToolboxHybridPage_VerifyBackend()
        {
            var moduleOperations = Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.FeatherServerOperations.FeatherModule();
            Guid pageId = Guid.Empty;

            moduleOperations.EnsureFeatherEnabled();

            try
            {
                string pageUrl;
                pageId = this.CreatePage(PageTemplateFramework.Hybrid, out pageUrl);

                var pageContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + "/Action/Edit?t=" + Guid.NewGuid().ToString());
                Assert.IsTrue(pageContentBeforeDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));

                moduleOperations.DeactivateFeather();

                var pageContentAfterDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + "/Action/Edit?t=" + Guid.NewGuid().ToString());
                Assert.IsFalse(pageContentAfterDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeletePage(pageId);
                moduleOperations.ActivateFeather();
            }
        }

        /// <summary>
        /// Checks whether after deactivating Feather the page edit toolbox on pure page doesn't contain Feather widgets.
        /// </summary>
        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks whether after deactivating Feather the page edit toolbox on pure page doesn't contain Feather widgets.")]
        public void DeactivateFeather_WidgetsInToolboxPurePage_VerifyBackend()
        {
            var moduleOperations = Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.FeatherServerOperations.FeatherModule();
            Guid pageId = Guid.Empty;

            moduleOperations.EnsureFeatherEnabled();

            try
            {
                string pageUrl;
                pageId = this.CreatePage(PageTemplateFramework.Mvc, out pageUrl);

                var pageContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + "/Action/Edit?t=" + Guid.NewGuid().ToString());
                Assert.IsTrue(pageContentBeforeDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));

                moduleOperations.DeactivateFeather();

                var pageContentAfterDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + "/Action/Edit?t=" + Guid.NewGuid().ToString());
                Assert.IsFalse(pageContentAfterDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));
            }
            finally
            {
                moduleOperations.ActivateFeather();
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeletePage(pageId);
            }
        }

        /// <summary>
        /// Checks whether after uninstalling Feather the page edit toolbox on hybrid page doesn't contain Feather widgets.
        /// </summary>
        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks whether after uninstalling Feather the page edit toolbox on hybrid page doesn't contain Feather widgets.")]
        public void UninstallFeather_WidgetsInToolboxHybridPage_VerifyBackend()
        {
            var moduleOperations = Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.FeatherServerOperations.FeatherModule();
            Guid pageId = Guid.Empty;

            moduleOperations.EnsureFeatherEnabled();

            try
            {
                string pageUrl;
                pageId = this.CreatePage(PageTemplateFramework.Hybrid, out pageUrl);

                var pageContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + "/Action/Edit?t=" + Guid.NewGuid().ToString());
                Assert.IsTrue(pageContentBeforeDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));

                moduleOperations.DeactivateFeather();
                moduleOperations.UninstallFeather();

                var pageContentAfterDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + "/Action/Edit?t=" + Guid.NewGuid().ToString());
                Assert.IsFalse(pageContentAfterDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));
            }
            finally
            {
                moduleOperations.InstallFeather();
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeletePage(pageId);
            }
        }

        /// <summary>
        /// Checks whether after uninstalling Feather the page edit toolbox on pure page doesn't contain Feather widgets.
        /// </summary>
        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks whether after uninstalling Feather the page edit toolbox on pure page doesn't contain Feather widgets.")]
        public void UninstallFeather_WidgetsInToolboxPurePage_VerifyBackend()
        {
            var moduleOperations = Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.FeatherServerOperations.FeatherModule();
            Guid pageId = Guid.Empty;

            moduleOperations.EnsureFeatherEnabled();

            try
            {
                string pageUrl;
                pageId = this.CreatePage(PageTemplateFramework.Mvc, out pageUrl);

                var pageContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + "/Action/Edit?t=" + Guid.NewGuid().ToString());
                Assert.IsTrue(pageContentBeforeDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));

                moduleOperations.DeactivateFeather();
                moduleOperations.UninstallFeather();

                var pageContentAfterDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + "/Action/Edit?t=" + Guid.NewGuid().ToString());
                Assert.IsFalse(pageContentAfterDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));
            }
            finally
            {
                moduleOperations.InstallFeather();
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeletePage(pageId);
            }
        }

        #endregion

        #region Template edit

        /// <summary>
        /// Checks whether after deactivating Feather the page template edit toolbox on hybrid page doesn't contain Feather widgets.
        /// </summary>
        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks whether after deactivating Feather the page template edit toolbox on hybrid page doesn't contain Feather widgets.")]
        public void DeactivateFeather_WidgetsInToolboxHybridPageTemplate_VerifyBackend()
        {
            var moduleOperations = Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.FeatherServerOperations.FeatherModule();
            var templatesOperations = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates();
            Guid templateId = Guid.Empty;

            moduleOperations.EnsureFeatherEnabled();

            try
            {
                templateId = templatesOperations.CreateHybridMVCPageTemplate(ModuleUnloadTests.PageTemplateTitle + Guid.NewGuid().ToString("N"));
                string templateUrl = UrlPath.ResolveAbsoluteUrl("~/Sitefinity/Template/" + templateId.ToString());

                var templateContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(templateUrl + "?t=" + Guid.NewGuid().ToString());
                Assert.IsTrue(templateContentBeforeDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));

                moduleOperations.DeactivateFeather();

                var templateContentAfterDeactivate = PageInvoker.ExecuteWebRequest(templateUrl + "?t=" + Guid.NewGuid().ToString());
                Assert.IsFalse(templateContentAfterDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));
            }
            finally
            {
                moduleOperations.ActivateFeather();
                templatesOperations.DeletePageTemplate(templateId);
            }
        }

        /// <summary>
        /// Checks whether after deactivating Feather the page template edit toolbox on pure page template doesn't contain Feather widgets.
        /// </summary>
        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks whether after deactivating Feather the page template edit toolbox on pure page template doesn't contain Feather widgets.")]
        public void DeactivateFeather_WidgetsInToolboxPurePageTemplate_VerifyBackend()
        {
            var moduleOperations = Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.FeatherServerOperations.FeatherModule();
            var templatesOperations = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates();
            Guid templateId = Guid.Empty;

            moduleOperations.EnsureFeatherEnabled();

            try
            {
                templateId = templatesOperations.CreatePureMVCPageTemplate(ModuleUnloadTests.PageTemplateTitle + Guid.NewGuid().ToString("N"));
                string templateUrl = UrlPath.ResolveAbsoluteUrl("~/Sitefinity/Template/" + templateId.ToString());

                var templateContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(templateUrl + "?t=" + Guid.NewGuid().ToString());
                Assert.IsTrue(templateContentBeforeDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));

                moduleOperations.DeactivateFeather();

                var templateContentAfterDeactivate = PageInvoker.ExecuteWebRequest(templateUrl + "?t=" + Guid.NewGuid().ToString());
                Assert.IsFalse(templateContentAfterDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));
            }
            finally
            {
                moduleOperations.ActivateFeather();
                templatesOperations.DeletePageTemplate(templateId);
            }
        }

        /// <summary>
        /// Checks whether after uninstalling Feather the page template edit toolbox on hybrid page doesn't contain Feather widgets.
        /// </summary>
        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks whether after uninstalling Feather the page template edit toolbox on hybrid page doesn't contain Feather widgets.")]
        public void UninstallFeather_WidgetsInToolboxHybridPageTemplate_VerifyBackend()
        {
            var moduleOperations = Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.FeatherServerOperations.FeatherModule();
            var templatesOperations = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates();
            Guid templateId = Guid.Empty;

            moduleOperations.EnsureFeatherEnabled();

            try
            {
                templateId = templatesOperations.CreateHybridMVCPageTemplate(ModuleUnloadTests.PageTemplateTitle + Guid.NewGuid().ToString("N"));
                string templateUrl = UrlPath.ResolveAbsoluteUrl("~/Sitefinity/Template/" + templateId.ToString());

                var templateContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(templateUrl + "?t=" + Guid.NewGuid().ToString());
                Assert.IsTrue(templateContentBeforeDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));

                moduleOperations.DeactivateFeather();
                moduleOperations.UninstallFeather();

                var templateContentAfterDeactivate = PageInvoker.ExecuteWebRequest(templateUrl + "?t=" + Guid.NewGuid().ToString());
                Assert.IsFalse(templateContentAfterDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));
            }
            finally
            {
                moduleOperations.InstallFeather();
                templatesOperations.DeletePageTemplate(templateId);
            }
        }

        /// <summary>
        /// Checks whether after uninstalling Feather the page template edit toolbox on pure page template doesn't contain Feather widgets.
        /// </summary>
        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks whether after uninstalling Feather the page template edit toolbox on pure page template doesn't contain Feather widgets.")]
        public void UninstallFeather_WidgetsInToolboxPurePageTemplate_VerifyBackend()
        {
            var moduleOperations = Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.FeatherServerOperations.FeatherModule();
            var templatesOperations = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates();
            Guid templateId = Guid.Empty;

            moduleOperations.EnsureFeatherEnabled();

            try
            {
                templateId = templatesOperations.CreatePureMVCPageTemplate(ModuleUnloadTests.PageTemplateTitle + Guid.NewGuid().ToString("N"));
                string templateUrl = UrlPath.ResolveAbsoluteUrl("~/Sitefinity/Template/" + templateId.ToString());

                var templateContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(templateUrl + "?t=" + Guid.NewGuid().ToString());
                Assert.IsTrue(templateContentBeforeDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));

                moduleOperations.DeactivateFeather();
                moduleOperations.UninstallFeather();

                var templateContentAfterDeactivate = PageInvoker.ExecuteWebRequest(templateUrl + "?t=" + Guid.NewGuid().ToString());
                Assert.IsFalse(templateContentAfterDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));
            }
            finally
            {
                moduleOperations.InstallFeather();
                templatesOperations.DeletePageTemplate(templateId);
            }
        }

        #endregion

        #endregion

        #region Private members

        private Guid CreatePage(PageTemplateFramework framework, out string pageUrl)
        {
            Guid pageId = Guid.Empty;
            pageUrl = string.Empty;
            var suffix = Guid.NewGuid().ToString("N");

            if (framework == PageTemplateFramework.Hybrid)
            {
                var namePrefix = "TestPageName";
                var titlePrefix = "TestPageTitle";
                var urlPrefix = "test-page-url";
                var index = 1;
                var pageOperations = new FeatherWidgets.TestUtilities.CommonOperations.PagesOperations();
                var mvcProxy = new MvcControllerProxy() { ControllerName = typeof(ContentBlockController).FullName, Settings = new ControllerSettings(new ContentBlockController()) };
                pageId = pageOperations.CreatePageWithControl(mvcProxy, namePrefix + suffix, titlePrefix + suffix, urlPrefix + suffix, index);
                pageUrl = UrlPath.ResolveAbsoluteUrl("~/" + urlPrefix + suffix + index);
            }
            else if (framework == PageTemplateFramework.Mvc)
            {
                var pagesOperations = Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.FeatherServerOperations.Pages();
                var pageManager = PageManager.GetManager();
                
                var bootstrapTemplate = pageManager.GetTemplates().FirstOrDefault(t => (t.Name == "Bootstrap.default" && t.Title == "default") || t.Title == "Bootstrap.default");
                if (bootstrapTemplate == null)
                    throw new ArgumentException("Bootstrap template not found");


                pageId = pagesOperations.CreatePageWithTemplate(bootstrapTemplate, "FormsPageBootstrap" + suffix, "forms-page-bootstrap" + suffix);
                pageUrl = RouteHelper.GetAbsoluteUrl(pageManager.GetPageNode(pageId).GetFullUrl());
            }

            return pageId;
        }

        private void UnlockPage(Guid pageId)
        {
            PageManager pageManager = PageManager.GetManager();
            var page = pageManager.GetPageDataList().Where(pd => pd.NavigationNode.Id == pageId && pd.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live)
             .FirstOrDefault();
                    page.LockedBy = System.Guid.Empty;
                    pageManager.SaveChanges();
        }

        private const string CbContent = "Initial CB content";
        private const string FeatherWidgetToolboxItemMarkup = "parameters=\"[{&quot;Key&quot;:&quot;ControllerName&quot;,&quot;Value&quot;:&quot;Telerik.Sitefinity.Frontend.";
        private const string PageTemplateTitle = "TestPageTemplate";

        private string pageNamePrefix = "CBPage";
        private string pageTitlePrefix = "CB";
        private string urlNamePrefix = "content-block";
        private int pageIndex = 1;

        #endregion
    }
}
