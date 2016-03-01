﻿using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestIntegration.SDK.DevelopersGuide.SitefinityEssentials.Modules.Forms;
using Telerik.WebTestRunner.Server.Attributes;

namespace FeatherWidgets.TestIntegration.Forms.Fields
{
    /// <summary>
    /// This class contains ensures Navigation field functionalities work correctly.
    /// </summary>
    [TestFixture]
    public class PageBreakTests
    {
        /// <summary>
        /// Ensures that when a Page break widget is added to form, div separators are presented in the page markup
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Ensures that when a Page break widget is added to form, div separators are presented in the page markup.")]
        public void PageBreak_SeparatorsMarkupIsCorrect()
        {
            var controller = new PageBreakController();

            var control = new MvcWidgetProxy();
            control.ControllerName = typeof(PageBreakController).FullName;
            control.Settings = new ControllerSettings(controller);

            var formId = ServerOperationsFeather.Forms().CreateFormWithWidget(control);

            var pageManager = PageManager.GetManager();

            try
            {
                var template = pageManager.GetTemplates().FirstOrDefault(t => t.Name == "SemanticUI.default" && t.Title == "default");
                Assert.IsNotNull(template, "Template was not found");

                var pageId = FeatherServerOperations.Pages().CreatePageWithTemplate(template, "PageBreakValueTest", "page-break-value-test");
                ServerOperationsFeather.Forms().AddFormControlToPage(pageId, formId);

                var pageContent = ServerOperationsFeather.Pages().GetPageContent(pageId);
                var formMultipageDecorator = ObjectFactory.Resolve<IFormMultipageDecorator>();
                Assert.IsTrue(pageContent.Contains(formMultipageDecorator.SeparatorBegin), "Form did not render page separators");
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
                FormsModuleCodeSnippets.DeleteForm(formId);
            }
        }

        /// <summary>
        /// Ensures that when a Page break widget is added to form, Header and Footer containers are presented.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Ensures that when a Page break widget is added to form, Header and Footer containers are presented.")]
        public void PageBreak_HeaderAndFooterFieldsInitialized()
        {
            var controllerForBody = new PageBreakController();
            var controlForBody = new MvcWidgetProxy();
            controlForBody.ControllerName = typeof(PageBreakController).FullName;
            controlForBody.Settings = new ControllerSettings(controllerForBody);

            var controllerForHeader = new TextFieldController();
            var controlForHeader = new MvcWidgetProxy();
            controlForHeader.ControllerName = typeof(TextFieldController).FullName;
            controlForHeader.Settings = new ControllerSettings(controllerForHeader);

            var controllerForFooter = new ParagraphTextFieldController();
            var controlForFooter = new MvcWidgetProxy();
            controlForFooter.ControllerName = typeof(ParagraphTextFieldController).FullName;
            controlForFooter.Settings = new ControllerSettings(controllerForFooter);

            var formId = ServerOperationsFeather.Forms().CreateFormWithWidgets(new List<Control>() { controlForHeader }, new List<Control>() { controlForBody }, new List<Control>() { controlForFooter }, null);
            var pageManager = PageManager.GetManager();

            try
            {
                var template = pageManager.GetTemplates().FirstOrDefault(t => t.Name == "SemanticUI.default" && t.Title == "default");
                Assert.IsNotNull(template, "Template was not found");

                var pageId = FeatherServerOperations.Pages().CreatePageWithTemplate(template, "PageBreakValueTest", "section-header-field-value-test");
                ServerOperationsFeather.Forms().AddFormControlToPage(pageId, formId);

                var pageContent = ServerOperationsFeather.Pages().GetPageContent(pageId);
                var formMultipageDecorator = ObjectFactory.Resolve<IFormMultipageDecorator>();
                Assert.IsTrue(pageContent.Contains(formMultipageDecorator.SeparatorBegin), "Form did not render page separators");
                Assert.IsTrue(pageContent.Contains("data-sf-role=\"text-field-container\""), "Form did not render page header");
                Assert.IsTrue(pageContent.Contains("data-sf-role=\"paragraph-text-field-container\""), "Form did not render page footer");
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
                FormsModuleCodeSnippets.DeleteForm(formId);
            }
        }
    }
}
