using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Forums.Web.UI;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.GridSystem;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Mvc.TestUtilities.Helpers;
using Telerik.Sitefinity.TestIntegration.Data.Content;
using Telerik.Sitefinity.TestIntegration.SDK.DevelopersGuide.SitefinityEssentials.Modules.Forms;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.Forms
{
    /// <summary>
    /// This is a class with Forms widget tests.
    /// </summary>
    [TestFixture]
    public class FormsWidgetTests
    {
        /// <summary>
        /// Same grid widget on a form placed on different packages yelds different output.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "FeatherWidgets.TestIntegration.Forms.FormsWidgetTests.AddGridControlToPage(System.Guid,System.String,System.String,System.String)"), Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Same grid widget on a form placed on different packages yelds different output.")]
        public void FormsWidget_WithGridWidget_AdaptsToPackage()
        {
            var gridVirtualPath = "~/Frontend-Assembly/Telerik.Sitefinity.Frontend/GridSystem/Templates/grid-3+9.html";
            var control = new GridControl();
            control.Layout = gridVirtualPath;
            var formId = ServerOperationsFeather.Forms().CreateFormWithWidget(control);

            var pageManager = PageManager.GetManager();

            try
            {
                var semanticTemplate = pageManager.GetTemplates().FirstOrDefault(t => (t.Name == "SemanticUI.default" && t.Title == "default") || t.Title == "SemanticUI.default");
                Assert.IsNotNull(semanticTemplate, "Template was not found");

                var semanticPageId = FeatherServerOperations.Pages().CreatePageWithTemplate(semanticTemplate, "FormsPageSemantic", "forms-page-semantic");
                ServerOperationsFeather.Forms().AddFormControlToPage(semanticPageId, formId);

                string semanticPageContent = ServerOperationsFeather.Pages().GetPageContent(semanticPageId);

                Assert.IsTrue(semanticPageContent.Contains("class=\"sf_colsIn four wide column\""), "SemanticUI grid content not found.");

                var bootstrapTemplate = pageManager.GetTemplates().FirstOrDefault(t => (t.Name == "Bootstrap.default" && t.Title == "default") || t.Title == "Bootstrap.default");
                Assert.IsNotNull(bootstrapTemplate, "Template was not found");

                var bootstrapPageId = FeatherServerOperations.Pages().CreatePageWithTemplate(bootstrapTemplate, "FormsPageBootstrap", "forms-page-bootstrap");
                ServerOperationsFeather.Forms().AddFormControlToPage(bootstrapPageId, formId);

                string bootstrapPageContent = ServerOperationsFeather.Pages().GetPageContent(bootstrapPageId);

                Assert.IsTrue(bootstrapPageContent.Contains("class=\"sf_colsIn col-md-3\""), "Bootstrap grid content not found.");
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
                FormsModuleCodeSnippets.DeleteForm(formId);
            }
        }

        /// <summary>
        /// Ensures that the form markup is updated when a new widget is added to it.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Ensures that the form markup is updated when a new widget is added to it.")]
        public void FormsWidget_AddWidgetToFormDescription_FormIsUpdated()
        {
            var gridVirtualPath = "~/Frontend-Assembly/Telerik.Sitefinity.Frontend/GridSystem/Templates/grid-3+9.html";
            var control = new GridControl();
            control.Layout = gridVirtualPath;
            var formId = ServerOperationsFeather.Forms().CreateFormWithWidget(control);

            var pageManager = PageManager.GetManager();

            try
            {
                var template = pageManager.GetTemplates().FirstOrDefault(t => t.Name == "SemanticUI.default" && t.Title == "default");
                Assert.IsNotNull(template, "Template was not found");

                var pageId = FeatherServerOperations.Pages().CreatePageWithTemplate(template, "FormsPageCacheTest", "forms-page-cache-test");
                ServerOperationsFeather.Forms().AddFormControlToPage(pageId, formId);

                string pageContent = ServerOperationsFeather.Pages().GetPageContent(pageId);

                Assert.IsTrue(pageContent.Contains("class=\"sf_colsIn four wide column\""), "SemanticUI grid content not found.");

                ServerOperationsFeather.Forms().AddFormWidget(formId, new GridControl() { Layout = "<div class=\"sf_colsIn\">Funny widget.</div>" });
                pageContent = ServerOperationsFeather.Pages().GetPageContent(pageId);

                Assert.IsTrue(pageContent.Contains("Funny widget."), "Form did not render the new control.");
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeleteAllPages();
                FormsModuleCodeSnippets.DeleteForm(formId);
            }
        }

        /// <summary>
        /// Ensures that when a form is submited with a forums widget on the same page, on custom hybrid layout, no exception is thrown.
        /// </summary>
        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Ensures that when a form is submited with a forums widget on the same page, on custom hybrid layout, no exception is thrown.")]
        public void FormsWidget_SubmitFormWithForumWidgetOnPageBasedOnCustomHybridPage_NoExceptionIsThrown()
        {
            var testName = MethodInfo.GetCurrentMethod().Name;
            var templateName = testName + "template";
            var pageName = testName + "page";

            var submitButtonControl = new MvcWidgetProxy();
            submitButtonControl.ControllerName = typeof(SubmitButtonController).FullName;
            submitButtonControl.Settings = new ControllerSettings(new SubmitButtonController());
            var formId = ServerOperationsFeather.Forms().CreateFormWithWidget(submitButtonControl);

            var forumControl = new ForumsView();

            var pageManager = PageManager.GetManager();
            Guid templateId = Guid.Empty;
            Guid pageId = Guid.Empty;
            try
            {
                templateId = Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().CreateHybridMVCPageTemplate(templateName);
                pageId = FeatherServerOperations.Pages().CreatePageWithTemplate(pageManager.GetTemplate(templateId), pageName, pageName);
                ServerOperationsFeather.Forms().AddFormControlToPage(pageId, formId, "TestForm", "Body");
                PageContentGenerator.AddControlToPage(pageId, forumControl, "TestForum", "Body");

                var page = pageManager.GetPageNode(pageId);
                var pageUrl = page.GetFullUrl();
                pageUrl = RouteHelper.GetAbsoluteUrl(pageUrl);

                var webRequest = (HttpWebRequest)WebRequest.Create(pageUrl);
                var dataString = "------WebKitFormBoundaryPIB6p73K1Y0L0ha5--";
                var dataBytes = (new ASCIIEncoding()).GetBytes(dataString);
                webRequest.Method = "POST";
                webRequest.ContentLength = dataBytes.Length;
                webRequest.ContentType = "multipart/form-data";
                webRequest.Timeout = 120 * 1000;
                webRequest.GetRequestStream().Write(dataBytes, 0, dataBytes.Length);
                Assert.DoesNotThrow(() => webRequest.GetResponse(), "Submitting a form on custom hybrid page with a forum widget on it throws error");
            }
            finally
            {
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Pages().DeletePage(pageId);
                Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.Templates().DeletePageTemplate(templateId);
                FormsModuleCodeSnippets.DeleteForm(formId);
            }
        }

        [Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Ensures that the NeedsRedirect property is not throwing an exception")]
        public void FormsWidget_Form_NeedsRedirect_NoExceptionIsThrown()
        {
            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(FormController).FullName;
            var controller = new FormController();
            var needsRedirect = controller.Model.NeedsRedirect;
            Type expectedType = typeof(bool);
            Assert.AreEqual(needsRedirect.GetType(), expectedType);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Unpublish"), Test]
        [Category(TestCategories.Forms)]
        [Author(FeatherTeams.SitefinityTeam6)]
        [Description("Ensures that the form widget have correct cache dependency behavior when form is unpublished/published")]
        public void FormsWidget_LoadingMvcForm_UnpublishPublish()
        {
            var testName = MethodInfo.GetCurrentMethod().Name;
            var pageName = testName + "page";
            Guid pageId = Guid.Empty;
            var formId = Guid.Empty;

            try
            {
                var fields = new FeatherWidgets.TestUtilities.CommonOperations.Forms.FormFieldType[] 
                { 
                    FeatherWidgets.TestUtilities.CommonOperations.Forms.FormFieldType.TextField,
                    FeatherWidgets.TestUtilities.CommonOperations.Forms.FormFieldType.SubmitButton
                };

                formId = ServerOperationsFeather.Forms().CreateFormWithWidgets(fields, "NewForm");
                pageId = ServerOperations.Pages().CreatePage(pageName);
                string url = UrlPath.ResolveAbsoluteUrl("~/" + pageName);
                ServerOperationsFeather.Forms().AddFormControlToPage(pageId, formId, FeatherGlobals.FormName, "Body");

                var formManager = FormsManager.GetManager();
                var form = formManager.GetForms().FirstOrDefault(f => f.Id == formId);

                Assert.IsFalse(form == null, "Testing form is missing");

                var errorMessage = new FormsResources().TheSpecifiedFormNoLongerExists;

                string publishFormPageContentFirst = WebRequestHelper.GetPageWebContent(url);
                Assert.IsFalse(publishFormPageContentFirst.Contains(errorMessage), string.Format(CultureInfo.InvariantCulture, "Unexpected message in page: {0}", errorMessage));

                formManager.UnpublishForm(formId);
                formManager.SaveChanges();
                string unpublishFormPageContent = WebRequestHelper.GetPageWebContent(url);
                Assert.IsTrue(unpublishFormPageContent.Contains(errorMessage), string.Format(CultureInfo.InvariantCulture, "Expected message in page: {0}", errorMessage));

                formManager.PublishForm(form);
                formManager.SaveChanges();
                string publishFormPageContentSecond = WebRequestHelper.GetPageWebContent(url);
                Assert.IsFalse(publishFormPageContentSecond.Contains(errorMessage), string.Format(CultureInfo.InvariantCulture, "Unexpected message in page: {0}", errorMessage));
            }
            finally
            {
                ServerOperations.Pages().DeletePage(pageId);
                FormsModuleCodeSnippets.DeleteForm(formId);
            }
        }
    }
}