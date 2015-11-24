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

                PageManager pageManager = PageManager.GetManager();
                var pageId = pageOperations.CreatePageWithControl(mvcProxy, this.pageNamePrefix, this.pageTitlePrefix, this.urlNamePrefix, this.pageIndex);
                var page = pageManager.GetPageDataList()
         .Where(pd => pd.NavigationNode.Id == pageId && pd.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live)
         .FirstOrDefault();
                page.LockedBy = System.Guid.Empty;
                pageManager.SaveChanges();

                string url = UrlPath.ResolveAbsoluteUrl("~/" + this.urlNamePrefix + this.pageIndex);
                string responseContent = PageInvoker.ExecuteWebRequest(url);
                string responseContentInEdit = PageInvoker.ExecuteWebRequest(url + "/Action/Edit");

                Assert.IsTrue(responseContent.Contains(ModuleUnloadTests.CbContent), "Content was not found!");
                Assert.IsTrue(responseContentInEdit.Contains(ModuleUnloadTests.CbContent), "Content was not found!");

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
                moduleOperations.ActivateFeatherFromDeactivatedState();
            }
        }

        /// <summary>
        /// Checks whether after deactivating Feather the page edit toolbox on hybrid page contains Feather widgets.
        /// </summary>
        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks whether after deactivating Feather the page edit toolbox on hybrid page contains Feather widgets.")]
        public void DeactivateFeather_WidgetsInToolboxHybridPage_VerifyBackend()
        {
            var moduleOperations = Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.FeatherServerOperations.FeatherModule();
            var pageOperations = new PagesOperations();
            Guid pageId = Guid.Empty;

            moduleOperations.EnsureFeatherEnabled();

            try
            {
                var mvcProxy = new MvcControllerProxy() { ControllerName = typeof(ContentBlockController).FullName, Settings = new ControllerSettings(new ContentBlockController()) };
                pageId = pageOperations.CreatePageWithControl(mvcProxy, this.pageNamePrefix, this.pageTitlePrefix, this.urlNamePrefix, this.pageIndex);
                string pageUrl = UrlPath.ResolveAbsoluteUrl("~/" + this.urlNamePrefix + this.pageIndex + "/Action/Edit");
                
                var pageContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + "?t=" + Guid.NewGuid().ToString());
                Assert.IsTrue(pageContentBeforeDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));

                moduleOperations.DeactivateFeather();

                var pageContentAfterDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + "?t=" + Guid.NewGuid().ToString());
                Assert.IsFalse(pageContentAfterDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeletePage(pageId);
                moduleOperations.ActivateFeatherFromDeactivatedState();
            }
        }

        /// <summary>
        /// Checks whether after deactivating Feather the page edit toolbox on pure page contains Feather widgets.
        /// </summary>
        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks whether after deactivating Feather the page edit toolbox on pure page contains Feather widgets.")]
        public void DeactivateFeather_WidgetsInToolboxPurePage_VerifyBackend()
        {
            var moduleOperations = Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.FeatherServerOperations.FeatherModule();
            var pagesOperations = Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.FeatherServerOperations.Pages();
            var pageManager = PageManager.GetManager();
            Guid pageId = Guid.Empty;

            moduleOperations.EnsureFeatherEnabled();

            try
            {
                var bootstrapTemplate = pageManager.GetTemplates().FirstOrDefault(t => (t.Name == "Bootstrap.default" && t.Title == "default") || t.Title == "Bootstrap.default");
                Assert.IsNotNull(bootstrapTemplate, "Bootstrap template was not found");
                pageId = pagesOperations.CreatePageWithTemplate(bootstrapTemplate, "FormsPageBootstrap", "forms-page-bootstrap");
                var pageUrl = RouteHelper.GetAbsoluteUrl(pageManager.GetPageNode(pageId).GetFullUrl()) + "/Action/Edit";

                var pageContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + "?t=" + Guid.NewGuid().ToString());
                Assert.IsTrue(pageContentBeforeDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));

                moduleOperations.DeactivateFeather();

                var pageContentAfterDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + "?t=" + Guid.NewGuid().ToString());
                Assert.IsFalse(pageContentAfterDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeletePage(pageId);
                moduleOperations.ActivateFeatherFromDeactivatedState();
            }
        }

        /// <summary>
        /// Checks whether after uninstalling Feather the page edit toolbox on hybrid page contains Feather widgets.
        /// </summary>
        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks whether after uninstalling Feather the page edit toolbox on hybrid page contains Feather widgets.")]
        public void UninstallFeather_WidgetsInToolboxHybridPage_VerifyBackend()
        {
            var moduleOperations = Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.FeatherServerOperations.FeatherModule();
            var pageOperations = new PagesOperations();
            Guid pageId = Guid.Empty;

            moduleOperations.EnsureFeatherEnabled();

            try
            {
                var mvcProxy = new MvcControllerProxy() { ControllerName = typeof(ContentBlockController).FullName, Settings = new ControllerSettings(new ContentBlockController()) };
                pageId = pageOperations.CreatePageWithControl(mvcProxy, this.pageNamePrefix, this.pageTitlePrefix, this.urlNamePrefix, this.pageIndex);
                string pageUrl = UrlPath.ResolveAbsoluteUrl("~/" + this.urlNamePrefix + this.pageIndex + "/Action/Edit");

                var pageContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + "?t=" + Guid.NewGuid().ToString());
                Assert.IsTrue(pageContentBeforeDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));

                moduleOperations.DeactivateFeather();
                moduleOperations.UninstallFeather();

                var pageContentAfterDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + "?t=" + Guid.NewGuid().ToString());
                Assert.IsFalse(pageContentAfterDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeletePage(pageId);
                moduleOperations.ActivateFeatherFromDeactivatedState();
            }
        }

        /// <summary>
        /// Checks whether after uninstalling Feather the page edit toolbox on pure page contains Feather widgets.
        /// </summary>
        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks whether after uninstalling Feather the page edit toolbox on pure page contains Feather widgets.")]
        public void UninstallFeather_WidgetsInToolboxPurePage_VerifyBackend()
        {
            var moduleOperations = Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.FeatherServerOperations.FeatherModule();
            var pagesOperations = Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.FeatherServerOperations.Pages();
            var pageManager = PageManager.GetManager();
            Guid pageId = Guid.Empty;

            moduleOperations.EnsureFeatherEnabled();

            try
            {
                var bootstrapTemplate = pageManager.GetTemplates().FirstOrDefault(t => (t.Name == "Bootstrap.default" && t.Title == "default") || t.Title == "Bootstrap.default");
                Assert.IsNotNull(bootstrapTemplate, "Bootstrap template was not found");
                pageId = pagesOperations.CreatePageWithTemplate(bootstrapTemplate, "FormsPageBootstrap", "forms-page-bootstrap");
                var pageUrl = RouteHelper.GetAbsoluteUrl(pageManager.GetPageNode(pageId).GetFullUrl()) + "/Action/Edit";

                var pageContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + "?t=" + Guid.NewGuid().ToString());
                Assert.IsTrue(pageContentBeforeDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));

                moduleOperations.DeactivateFeather();
                moduleOperations.UninstallFeather();

                var pageContentAfterDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + "?t=" + Guid.NewGuid().ToString());
                Assert.IsFalse(pageContentAfterDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeletePage(pageId);
                moduleOperations.ActivateFeatherFromUninstalledState();
            }
        }

        private const string CbContent = "Initial CB content";
        private const string FeatherWidgetToolboxItemMarkup = "parameters=\"[{&quot;Key&quot;:&quot;ControllerName&quot;,&quot;Value&quot;:&quot;Telerik.Sitefinity.Frontend.";

        private string pageNamePrefix = "CBPage";
        private string pageTitlePrefix = "CB";
        private string urlNamePrefix = "content-block";
        private int pageIndex = 1;
    }
}
