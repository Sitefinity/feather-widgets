using System;
using System.Linq;
using System.Web.UI;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.ContentBlock.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.TestIntegration.Data.Content;
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
        #region Widgets

        #region On page

        /// <summary>
        /// Checks whether after deactivating Feather on hybrid page the Sitefinity pages don't throw errors on frontend and notifies users on backend.
        /// </summary>
        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks whether after deactivating Feather on hybrid page the Sitefinity pages don't throw errors on frontend and notifies users on backend.")]
        public void DeactivatingFeather_WidgetOnHybridPage_VerifyFrontendAndBackend()
        {
            var moduleOperations = new Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.FeatherModuleOperations();
            Guid pageId = Guid.Empty;

            moduleOperations.EnsureFeatherEnabled();

            try
            {
                string pageUrl;
                pageId = this.CreatePageWithControl(PageTemplateFramework.Hybrid, out pageUrl);

                var pageContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendUncacheUrl());
                Assert.IsTrue(pageContentBeforeDeactivate.Contains(ModuleUnloadTests.PageControlContent), "Content was not found!");

                var pageContentBeforeDeactivateInEdit = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendEditUrl());
                Assert.IsTrue(pageContentBeforeDeactivateInEdit.Contains(ModuleUnloadTests.PageControlContent), "Content was not found!");
                Assert.IsFalse(pageContentBeforeDeactivateInEdit.Contains(ModuleUnloadTests.WidgetUnavailableMessage), "Error message is displayed in zone editor!");

                this.UnlockPage(pageId);
                moduleOperations.DeactivateFeather();

                var pageContentAfterDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendUncacheUrl());
                Assert.IsFalse(pageContentAfterDeactivate.Contains(ModuleUnloadTests.PageControlContent), "Content was found after deactivate!");

                var pageContentAfterDeactivateInEdit = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendEditUrl());
                Assert.IsFalse(pageContentAfterDeactivateInEdit.Contains(ModuleUnloadTests.PageControlContent), "Content was found after deactivate!");
                Assert.IsTrue(pageContentAfterDeactivateInEdit.Contains(ModuleUnloadTests.WidgetUnavailableMessage), "Error message is not displayed in zone editor!");
            }
            finally
            {
                moduleOperations.ActivateFeather();
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeletePage(pageId);
            }
        }

        /// <summary>
        /// Checks whether after deactivating Feather on pure page the Sitefinity pages don't throw errors on frontend and notifies users on backend.
        /// </summary>
        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks whether after deactivating Feather on pure page the Sitefinity pages don't throw errors on frontend and notifies users on backend.")]
        public void DeactivatingFeather_WidgetOnPurePage_VerifyFrontendAndBackend()
        {
            var moduleOperations = new Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.FeatherModuleOperations();
            Guid pageId = Guid.Empty;

            moduleOperations.EnsureFeatherEnabled();

            try
            {
                string pageUrl;
                pageId = this.CreatePageWithControl(PageTemplateFramework.Mvc, out pageUrl);

                var pageContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendUncacheUrl());
                Assert.IsTrue(pageContentBeforeDeactivate.Contains(ModuleUnloadTests.PageControlContent), "Content was not found!");

                var pageContentBeforeDeactivateInEdit = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendEditUrl());
                Assert.IsTrue(pageContentBeforeDeactivateInEdit.Contains(ModuleUnloadTests.PageControlContent), "Content was not found!");
                Assert.IsFalse(pageContentBeforeDeactivateInEdit.Contains(ModuleUnloadTests.WidgetUnavailableMessage), "Error message is displayed in zone editor!");

                this.UnlockPage(pageId);
                moduleOperations.DeactivateFeather();

                var pageContentAfterDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendUncacheUrl());
                Assert.IsFalse(pageContentAfterDeactivate.Contains(ModuleUnloadTests.PageControlContent), "Content was found after deactivate!");

                var pageContentAfterDeactivateInEdit = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendEditUrl());
                Assert.IsFalse(pageContentAfterDeactivateInEdit.Contains(ModuleUnloadTests.PageControlContent), "Content was found after deactivate!");
                Assert.IsTrue(pageContentAfterDeactivateInEdit.Contains(ModuleUnloadTests.WidgetUnavailableMessage), "Error message is not displayed in zone editor!");
            }
            finally
            {
                moduleOperations.ActivateFeather();
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeletePage(pageId);
            }
        }
        
        /// <summary>
        /// Checks whether after uninstalling Feather on hybrid page the Sitefinity pages don't throw errors on frontend and notifies users on backend.
        /// </summary>
        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks whether after uninstalling Feather on hybrid page the Sitefinity pages don't throw errors on frontend and notifies users on backend.")]
        public void UninstallingFeather_WidgetOnHybridPage_VerifyFrontendAndBackend()
        {
            var moduleOperations = new Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.FeatherModuleOperations();
            Guid pageId = Guid.Empty;

            moduleOperations.EnsureFeatherEnabled();

            try
            {
                string pageUrl;
                pageId = this.CreatePageWithControl(PageTemplateFramework.Hybrid, out pageUrl);

                var pageContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendUncacheUrl());
                Assert.IsTrue(pageContentBeforeDeactivate.Contains(ModuleUnloadTests.PageControlContent), "Content was not found!");

                var pageContentBeforeDeactivateInEdit = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendEditUrl());
                Assert.IsTrue(pageContentBeforeDeactivateInEdit.Contains(ModuleUnloadTests.PageControlContent), "Content was not found!");
                Assert.IsFalse(pageContentBeforeDeactivateInEdit.Contains(ModuleUnloadTests.WidgetUnavailableMessage), "Error message is displayed in zone editor!");

                this.UnlockPage(pageId);
                moduleOperations.DeactivateFeather();
                moduleOperations.UninstallFeather();

                var pageContentAfterDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendUncacheUrl());
                Assert.IsFalse(pageContentAfterDeactivate.Contains(ModuleUnloadTests.PageControlContent), "Content was found after deactivate!");

                var pageContentAfterDeactivateInEdit = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendEditUrl());
                Assert.IsFalse(pageContentAfterDeactivateInEdit.Contains(ModuleUnloadTests.PageControlContent), "Content was found after deactivate!");
                Assert.IsFalse(pageContentAfterDeactivateInEdit.Contains(ModuleUnloadTests.WidgetUnavailableMessage), "Error message is displayed but must be deleted in zone editor!");
            }
            finally
            {
                moduleOperations.InstallFeather();
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeletePage(pageId);
            }
        }

        /// <summary>
        /// Checks whether after uninstalling Feather on pure page the Sitefinity pages don't throw errors on frontend and notifies users on backend.
        /// </summary>
        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks whether after uninstalling Feather on pure page the Sitefinity pages don't throw errors on frontend and notifies users on backend.")]
        public void UninstallingFeather_WidgetOnPurePage_VerifyFrontendAndBackend()
        {
            var moduleOperations = new Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.FeatherModuleOperations();
            Guid pageId = Guid.Empty;

            moduleOperations.EnsureFeatherEnabled();

            try
            {
                string pageUrl;
                pageId = this.CreatePageWithControl(PageTemplateFramework.Mvc, out pageUrl);

                var pageContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendUncacheUrl());
                Assert.IsTrue(pageContentBeforeDeactivate.Contains(ModuleUnloadTests.PageControlContent), "Content was not found!");

                var pageContentBeforeDeactivateInEdit = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendEditUrl());
                Assert.IsTrue(pageContentBeforeDeactivateInEdit.Contains(ModuleUnloadTests.PageControlContent), "Content was not found!");
                Assert.IsFalse(pageContentBeforeDeactivateInEdit.Contains(ModuleUnloadTests.WidgetUnavailableMessage), "Error message is displayed in zone editor!");

                this.UnlockPage(pageId);
                moduleOperations.DeactivateFeather();
                moduleOperations.UninstallFeather();

                var pageContentAfterDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendUncacheUrl());
                Assert.IsFalse(pageContentAfterDeactivate.Contains(ModuleUnloadTests.PageControlContent), "Content was found after deactivate!");

                var pageContentAfterDeactivateInEdit = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendEditUrl());
                Assert.IsFalse(pageContentAfterDeactivateInEdit.Contains(ModuleUnloadTests.PageControlContent), "Content was found after deactivate!");
                Assert.IsFalse(pageContentAfterDeactivateInEdit.Contains(ModuleUnloadTests.WidgetUnavailableMessage), "Error message is displayed but must be deleted in zone editor!");
            }
            finally
            {
                moduleOperations.InstallFeather();
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeletePage(pageId);
            }
        }

        #endregion

        #region On template

        /// <summary>
        /// Checks whether after deactivating Feather on hybrid page template the Sitefinity pages don't throw errors on frontend and notifies users on backend.
        /// </summary>
        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks whether after deactivating Feather on hybrid page template the Sitefinity pages don't throw errors on frontend and notifies users on backend.")]
        public void DeactivatingFeather_WidgetOnHybridPageTemplate_VerifyFrontendAndBackend()
        {
            var moduleOperations = new Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.FeatherModuleOperations();
            Guid pageId = Guid.Empty;
            Guid templateId = Guid.Empty;

            moduleOperations.EnsureFeatherEnabled();

            try
            {
                templateId = this.CreateTemplateWithControl(PageTemplateFramework.Hybrid);
                var templateUrl = UrlPath.ResolveAbsoluteUrl(ModuleUnloadTests.SitefinityTemplateRoutePrefix + templateId.ToString());
                string pageUrl;
                pageId = this.CreatePageWithTemplate(templateId, out pageUrl);

                var pageContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendUncacheUrl());
                Assert.IsTrue(pageContentBeforeDeactivate.Contains(ModuleUnloadTests.PageControlContent), "Content was not found!");

                var pageContentBeforeDeactivateInEdit = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendEditUrl());
                Assert.IsTrue(pageContentBeforeDeactivateInEdit.Contains(ModuleUnloadTests.PageControlContent), "Content was not found!");
                Assert.IsFalse(pageContentBeforeDeactivateInEdit.Contains(ModuleUnloadTests.WidgetUnavailableMessage), "Error message is displayed in zone editor!");

                var templateContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(templateUrl + this.AppendUncacheUrl());
                Assert.IsTrue(templateContentBeforeDeactivate.Contains(ModuleUnloadTests.PageControlContent), "Content was not found!");
                Assert.IsFalse(pageContentBeforeDeactivateInEdit.Contains(ModuleUnloadTests.WidgetUnavailableMessage), "Error message is displayed in zone editor!");

                this.UnlockPage(pageId);
                this.UnlockPageTemplate(templateId);
                moduleOperations.DeactivateFeather();

                var pageContentAfterDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendUncacheUrl());
                Assert.IsFalse(pageContentAfterDeactivate.Contains(ModuleUnloadTests.PageControlContent), "Content was found after deactivate!");

                var pageContentAfterDeactivateInEdit = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendEditUrl());
                Assert.IsFalse(pageContentAfterDeactivateInEdit.Contains(ModuleUnloadTests.PageControlContent), "Content was found after deactivate!");
                Assert.IsTrue(pageContentAfterDeactivateInEdit.Contains(ModuleUnloadTests.WidgetUnavailableMessage), "Error message is not displayed in zone editor!");

                var templateContentAfterDeactivate = PageInvoker.ExecuteWebRequest(templateUrl + this.AppendUncacheUrl());
                Assert.IsFalse(templateContentAfterDeactivate.Contains(ModuleUnloadTests.PageControlContent), "Content was found after deactivate!");
                Assert.IsTrue(templateContentAfterDeactivate.Contains(ModuleUnloadTests.WidgetUnavailableMessage), "Error message is not displayed in zone editor!");
            }
            finally
            {
                moduleOperations.ActivateFeather();
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeletePage(pageId);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().DeletePageTemplate(templateId);
            }
        }

        /// <summary>
        /// Checks whether after deactivating Feather on pure page template the Sitefinity pages don't throw errors on frontend and notifies users on backend.
        /// </summary>
        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks whether after deactivating Feather on pure page template the Sitefinity pages don't throw errors on frontend and notifies users on backend.")]
        public void DeactivatingFeather_WidgetOnPurePageTemplate_VerifyFrontendAndBackend()
        {
            var moduleOperations = new Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.FeatherModuleOperations();
            Guid pageId = Guid.Empty;
            Guid templateId = Guid.Empty;

            moduleOperations.EnsureFeatherEnabled();

            try
            {
                templateId = this.CreateTemplateWithControl(PageTemplateFramework.Mvc);
                var templateUrl = UrlPath.ResolveAbsoluteUrl(ModuleUnloadTests.SitefinityTemplateRoutePrefix + templateId.ToString());
                string pageUrl;
                pageId = this.CreatePageWithTemplate(templateId, out pageUrl);

                var pageContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendUncacheUrl());
                Assert.IsTrue(pageContentBeforeDeactivate.Contains(ModuleUnloadTests.PageControlContent), "Content was not found!");

                var pageContentBeforeDeactivateInEdit = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendEditUrl());
                Assert.IsTrue(pageContentBeforeDeactivateInEdit.Contains(ModuleUnloadTests.PageControlContent), "Content was not found!");
                Assert.IsFalse(pageContentBeforeDeactivateInEdit.Contains(ModuleUnloadTests.WidgetUnavailableMessage), "Error message is displayed in zone editor!");

                var templateContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(templateUrl + this.AppendUncacheUrl());
                Assert.IsTrue(templateContentBeforeDeactivate.Contains(ModuleUnloadTests.PageControlContent), "Content was not found!");
                Assert.IsFalse(pageContentBeforeDeactivateInEdit.Contains(ModuleUnloadTests.WidgetUnavailableMessage), "Error message is displayed in zone editor!");

                this.UnlockPage(pageId);
                this.UnlockPageTemplate(templateId);
                moduleOperations.DeactivateFeather();

                var pageContentAfterDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendUncacheUrl());
                Assert.IsFalse(pageContentAfterDeactivate.Contains(ModuleUnloadTests.PageControlContent), "Content was found after deactivate!");

                var pageContentAfterDeactivateInEdit = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendEditUrl());
                Assert.IsFalse(pageContentAfterDeactivateInEdit.Contains(ModuleUnloadTests.PageControlContent), "Content was found after deactivate!");
                Assert.IsTrue(pageContentAfterDeactivateInEdit.Contains(ModuleUnloadTests.WidgetUnavailableMessage), "Error message is not displayed in zone editor!");

                var templateContentAfterDeactivate = PageInvoker.ExecuteWebRequest(templateUrl + this.AppendUncacheUrl());
                Assert.IsFalse(templateContentAfterDeactivate.Contains(ModuleUnloadTests.PageControlContent), "Content was found after deactivate!");
                Assert.IsTrue(templateContentAfterDeactivate.Contains(ModuleUnloadTests.WidgetUnavailableMessage), "Error message is not displayed in zone editor!");
            }
            finally
            {
                moduleOperations.ActivateFeather();
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeletePage(pageId);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().DeletePageTemplate(templateId);
            }
        }

        /// <summary>
        /// Checks whether after uninstalling Feather on hybrid page template the Sitefinity pages don't throw errors on frontend and notifies users on backend.
        /// </summary>
        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks whether after uninstalling Feather on hybrid page template the Sitefinity pages don't throw errors on frontend and notifies users on backend.")]
        public void UninstallingFeather_WidgetOnHybridPageTemplate_VerifyFrontendAndBackend()
        {
            var moduleOperations = new Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.FeatherModuleOperations();
            Guid pageId = Guid.Empty;
            Guid templateId = Guid.Empty;

            moduleOperations.EnsureFeatherEnabled();

            try
            {
                templateId = this.CreateTemplateWithControl(PageTemplateFramework.Hybrid);
                var templateUrl = UrlPath.ResolveAbsoluteUrl(ModuleUnloadTests.SitefinityTemplateRoutePrefix + templateId.ToString());
                string pageUrl;
                pageId = this.CreatePageWithTemplate(templateId, out pageUrl);

                var pageContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendUncacheUrl());
                Assert.IsTrue(pageContentBeforeDeactivate.Contains(ModuleUnloadTests.PageControlContent), "Content was not found!");

                var pageContentBeforeDeactivateInEdit = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendEditUrl());
                Assert.IsTrue(pageContentBeforeDeactivateInEdit.Contains(ModuleUnloadTests.PageControlContent), "Content was not found!");
                Assert.IsFalse(pageContentBeforeDeactivateInEdit.Contains(ModuleUnloadTests.WidgetUnavailableMessage), "Error message is displayed in zone editor!");

                var templateContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(templateUrl + this.AppendUncacheUrl());
                Assert.IsTrue(templateContentBeforeDeactivate.Contains(ModuleUnloadTests.PageControlContent), "Content was not found!");
                Assert.IsFalse(pageContentBeforeDeactivateInEdit.Contains(ModuleUnloadTests.WidgetUnavailableMessage), "Error message is displayed in zone editor!");

                this.UnlockPage(pageId);
                this.UnlockPageTemplate(templateId);
                moduleOperations.DeactivateFeather();
                moduleOperations.UninstallFeather();

                var pageContentAfterDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendUncacheUrl());
                Assert.IsFalse(pageContentAfterDeactivate.Contains(ModuleUnloadTests.PageControlContent), "Content was found after deactivate!");

                var pageContentAfterDeactivateInEdit = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendEditUrl());
                Assert.IsFalse(pageContentAfterDeactivateInEdit.Contains(ModuleUnloadTests.PageControlContent), "Content was found after deactivate!");
                Assert.IsFalse(pageContentAfterDeactivateInEdit.Contains(ModuleUnloadTests.WidgetUnavailableMessage), "Error message is displayed but must be deleted in zone editor!");

                var templateContentAfterDeactivate = PageInvoker.ExecuteWebRequest(templateUrl + this.AppendUncacheUrl());
                Assert.IsFalse(templateContentAfterDeactivate.Contains(ModuleUnloadTests.PageControlContent), "Content was found after deactivate!");
                Assert.IsFalse(templateContentAfterDeactivate.Contains(ModuleUnloadTests.WidgetUnavailableMessage), "Error message is displayed but must be deleted in zone editor!");
            }
            finally
            {
                moduleOperations.InstallFeather();
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeletePage(pageId);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().DeletePageTemplate(templateId);
            }
        }

        /// <summary>
        /// Checks whether after uninstalling Feather on pure page template the Sitefinity pages don't throw errors on frontend and notifies users on backend.
        /// </summary>
        [Test]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Checks whether after uninstalling Feather on pure page template the Sitefinity pages don't throw errors on frontend and notifies users on backend.")]
        public void UninstallingFeather_WidgetOnPurePageTemplate_VerifyFrontendAndBackend()
        {
            var moduleOperations = new Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.FeatherModuleOperations();
            Guid pageId = Guid.Empty;
            Guid templateId = Guid.Empty;

            moduleOperations.EnsureFeatherEnabled();

            try
            {
                templateId = this.CreateTemplateWithControl(PageTemplateFramework.Mvc);
                var templateUrl = UrlPath.ResolveAbsoluteUrl(ModuleUnloadTests.SitefinityTemplateRoutePrefix + templateId.ToString());
                string pageUrl;
                pageId = this.CreatePageWithTemplate(templateId, out pageUrl);

                var pageContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendUncacheUrl());
                Assert.IsTrue(pageContentBeforeDeactivate.Contains(ModuleUnloadTests.PageControlContent), "Content was not found!");

                var pageContentBeforeDeactivateInEdit = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendEditUrl());
                Assert.IsTrue(pageContentBeforeDeactivateInEdit.Contains(ModuleUnloadTests.PageControlContent), "Content was not found!");
                Assert.IsFalse(pageContentBeforeDeactivateInEdit.Contains(ModuleUnloadTests.WidgetUnavailableMessage), "Error message is displayed in zone editor!");

                var templateContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(templateUrl + this.AppendUncacheUrl());
                Assert.IsTrue(templateContentBeforeDeactivate.Contains(ModuleUnloadTests.PageControlContent), "Content was not found!");
                Assert.IsFalse(pageContentBeforeDeactivateInEdit.Contains(ModuleUnloadTests.WidgetUnavailableMessage), "Error message is displayed in zone editor!");

                this.UnlockPage(pageId);
                this.UnlockPageTemplate(templateId);
                moduleOperations.DeactivateFeather();
                moduleOperations.UninstallFeather();

                var pageContentAfterDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendUncacheUrl());
                Assert.IsFalse(pageContentAfterDeactivate.Contains(ModuleUnloadTests.PageControlContent), "Content was found after deactivate!");

                var pageContentAfterDeactivateInEdit = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendEditUrl());
                Assert.IsFalse(pageContentAfterDeactivateInEdit.Contains(ModuleUnloadTests.PageControlContent), "Content was found after deactivate!");
                Assert.IsFalse(pageContentAfterDeactivateInEdit.Contains(ModuleUnloadTests.WidgetUnavailableMessage), "Error message is displayed but must be deleted in zone editor!");

                var templateContentAfterDeactivate = PageInvoker.ExecuteWebRequest(templateUrl + this.AppendUncacheUrl());
                Assert.IsFalse(templateContentAfterDeactivate.Contains(ModuleUnloadTests.PageControlContent), "Content was found after deactivate!");
                Assert.IsFalse(templateContentAfterDeactivate.Contains(ModuleUnloadTests.WidgetUnavailableMessage), "Error message is displayed but must be deleted in zone editor!");
            }
            finally
            {
                moduleOperations.InstallFeather();
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeletePage(pageId);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().DeletePageTemplate(templateId);
            }
        }

        #endregion

        #endregion
        
        #region Toolbox

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

                var pageContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendEditUrl());
                Assert.IsTrue(pageContentBeforeDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));

                moduleOperations.DeactivateFeather();

                var pageContentAfterDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendEditUrl());
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

                var pageContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendEditUrl());
                Assert.IsTrue(pageContentBeforeDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));

                moduleOperations.DeactivateFeather();

                var pageContentAfterDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendEditUrl());
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

                var pageContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendEditUrl());
                Assert.IsTrue(pageContentBeforeDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));

                moduleOperations.DeactivateFeather();
                moduleOperations.UninstallFeather();

                var pageContentAfterDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendEditUrl());
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

                var pageContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendEditUrl());
                Assert.IsTrue(pageContentBeforeDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));

                moduleOperations.DeactivateFeather();
                moduleOperations.UninstallFeather();

                var pageContentAfterDeactivate = PageInvoker.ExecuteWebRequest(pageUrl + this.AppendEditUrl());
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
                string templateUrl = UrlPath.ResolveAbsoluteUrl(ModuleUnloadTests.SitefinityTemplateRoutePrefix + templateId.ToString());

                var templateContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(templateUrl + this.AppendUncacheUrl());
                Assert.IsTrue(templateContentBeforeDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));

                moduleOperations.DeactivateFeather();

                var templateContentAfterDeactivate = PageInvoker.ExecuteWebRequest(templateUrl + this.AppendUncacheUrl());
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
                string templateUrl = UrlPath.ResolveAbsoluteUrl(ModuleUnloadTests.SitefinityTemplateRoutePrefix + templateId.ToString());

                var templateContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(templateUrl + this.AppendUncacheUrl());
                Assert.IsTrue(templateContentBeforeDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));

                moduleOperations.DeactivateFeather();

                var templateContentAfterDeactivate = PageInvoker.ExecuteWebRequest(templateUrl + this.AppendUncacheUrl());
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
                string templateUrl = UrlPath.ResolveAbsoluteUrl(ModuleUnloadTests.SitefinityTemplateRoutePrefix + templateId.ToString());

                var templateContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(templateUrl + this.AppendUncacheUrl());
                Assert.IsTrue(templateContentBeforeDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));

                moduleOperations.DeactivateFeather();
                moduleOperations.UninstallFeather();

                var templateContentAfterDeactivate = PageInvoker.ExecuteWebRequest(templateUrl + this.AppendUncacheUrl());
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
                string templateUrl = UrlPath.ResolveAbsoluteUrl(ModuleUnloadTests.SitefinityTemplateRoutePrefix + templateId.ToString());

                var templateContentBeforeDeactivate = PageInvoker.ExecuteWebRequest(templateUrl + this.AppendUncacheUrl());
                Assert.IsTrue(templateContentBeforeDeactivate.Contains(ModuleUnloadTests.FeatherWidgetToolboxItemMarkup));

                moduleOperations.DeactivateFeather();
                moduleOperations.UninstallFeather();

                var templateContentAfterDeactivate = PageInvoker.ExecuteWebRequest(templateUrl + this.AppendUncacheUrl());
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

                pageId = pagesOperations.CreatePageWithTemplate(bootstrapTemplate, "FeatherTestPageBootstrap" + suffix, "feather-test-page-bootstrap" + suffix);
                pageUrl = RouteHelper.GetAbsoluteUrl(pageManager.GetPageNode(pageId).GetFullUrl());
            }

            return pageId;
        }

        private Guid CreatePageWithControl(PageTemplateFramework framework, out string pageUrl)
        {
            var pageId = this.CreatePage(framework, out pageUrl);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(ContentBlockController).FullName;

            var contentBlockController = new ContentBlockController();
            contentBlockController.Content = ModuleUnloadTests.PageControlContent;
            mvcProxy.Settings = new ControllerSettings(contentBlockController);

            PageContentGenerator.AddControlsToPage(pageId, new Control[] { mvcProxy }, framework == PageTemplateFramework.Mvc ? "Contentplaceholder1" : "Body");

            return pageId;
        }

        private Guid CreateTemplateWithControl(PageTemplateFramework framework)
        {
            Guid pageTemplateId = Guid.Empty;

            var templatesOperations = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates();

            if (framework == PageTemplateFramework.Hybrid)
                pageTemplateId = templatesOperations.CreateHybridMVCPageTemplate(ModuleUnloadTests.PageTemplateTitle + Guid.NewGuid().ToString());
            else if (framework == PageTemplateFramework.Mvc)
                pageTemplateId = templatesOperations.CreatePureMVCPageTemplate(ModuleUnloadTests.PageTemplateTitle + Guid.NewGuid().ToString());

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(ContentBlockController).FullName;

            var contentBlockController = new ContentBlockController();
            contentBlockController.Content = ModuleUnloadTests.PageControlContent;
            mvcProxy.Settings = new ControllerSettings(contentBlockController);

            templatesOperations.AddControlToTemplate(pageTemplateId, mvcProxy, "Body", "ContentBlockCaption");

            return pageTemplateId;
        }

        private Guid CreatePageWithTemplate(Guid templateId, out string pageUrl)
        {
            var pageManager = PageManager.GetManager();
            var template = pageManager.GetTemplates().Where(t => t.Id == templateId).FirstOrDefault();
            var pageId = Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations.FeatherServerOperations.Pages().CreatePageWithTemplate(template, "TestPageTitle" + Guid.NewGuid().ToString(), "test-page-url" + Guid.NewGuid().ToString());

            pageUrl = RouteHelper.GetAbsoluteUrl(pageManager.GetPageNode(pageId).GetFullUrl());

            return pageId;
        }

        private void UnlockPage(Guid pageId)
        {
            var pageManager = PageManager.GetManager();
            var page = pageManager.GetPageDataList().Where(pd => pd.NavigationNode.Id == pageId && pd.Status == ContentLifecycleStatus.Live).FirstOrDefault();
            page.LockedBy = Guid.Empty;
            pageManager.SaveChanges();
        }

        private void UnlockPageTemplate(Guid pageTemplateId)
        {
            var pageManager = PageManager.GetManager();
            var pageTemplate = pageManager.GetTemplates().Where(t => t.Id == pageTemplateId).FirstOrDefault();
            pageTemplate.LockedBy = Guid.Empty;
            pageManager.SaveChanges();
        }

        private string AppendEditUrl()
        {
            return "/Action/Edit" + this.AppendUncacheUrl();
        }

        private string AppendUncacheUrl()
        {
            return "?t=" + Guid.NewGuid().ToString();
        }

        private const string SitefinityTemplateRoutePrefix = "~/Sitefinity/Template/";
        private const string PageControlContent = "Initial CB content";
        private const string FeatherWidgetToolboxItemMarkup = "parameters=\"[{&quot;Key&quot;:&quot;ControllerName&quot;,&quot;Value&quot;:&quot;Telerik.Sitefinity.Frontend.";
        private const string PageTemplateTitle = "TestPageTemplate";
        private const string WidgetUnavailableMessage = "This widget doesn't work, because <strong>Feather</strong> module has been deactivated.";

        #endregion
    }
}