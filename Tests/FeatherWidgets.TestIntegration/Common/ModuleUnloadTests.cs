using System.IO;
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
using Telerik.Sitefinity.Services;
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
            var featherWasEnabled = this.IsFeatherEnabled();
            var pageOperations = new PagesOperations();

            try
            {
                if (!featherWasEnabled)
                    this.ActivateFeatherFromDeactivatedState();

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

                this.DeactivateFeather();

                responseContent = PageInvoker.ExecuteWebRequest(url);
                responseContentInEdit = PageInvoker.ExecuteWebRequest(url + "/Action/Edit");

                Assert.IsFalse(responseContent.Contains(ModuleUnloadTests.CbContent), "Content was found after deactivate!");
                Assert.IsFalse(responseContentInEdit.Contains(ModuleUnloadTests.CbContent), "Content was found after deactivate!");
                Assert.IsTrue(responseContentInEdit.Contains("This widget doesn't work, because <strong>Feather</strong> module has been deactivated."), "Error message is not displayed in zone editor!");
            }
            finally
            {
                pageOperations.DeletePages();

                if (featherWasEnabled)
                    this.ActivateFeatherFromDeactivatedState();
            }
        }

        private bool IsFeatherEnabled()
        {
            return SystemManager.GetModule("Feather") != null;
        }

        private void ActivateFeatherFromDeactivatedState()
        {
            var installOperationEndpoint = UrlPath.ResolveUrl(ModuleUnloadTests.FeatherActivateFromDeactivatedStateUrl, true);
            this.MakePutRequest(installOperationEndpoint, JsonRequestPayload);

            // Give time for events to fire
            Thread.Sleep(5000);
        }

        private void DeactivateFeather()
        {
            if (!this.IsFeatherEnabled())
                return;

            var uninstallOperationEndpoint = UrlPath.ResolveUrl(ModuleUnloadTests.FeatherDeactivateUrl, true);
            this.MakePutRequest(uninstallOperationEndpoint, JsonRequestPayload);

            // Give time for events to fire
            Thread.Sleep(5000);
        }

        private void MakePutRequest(string url, string payload)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.CookieContainer = new CookieContainer();
            httpWebRequest.Headers["Authorization"] = HttpContext.Current.Request.Headers["Authorization"];
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "PUT";

            using (var writer = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                writer.Write(payload);
            }

            httpWebRequest.GetResponse();
        }

        private const string JsonRequestPayload = "{\"ClientId\":\"Feather\",\"Description\":\"Modern, intuitive, convention based, mobile-first UI for Telerik Sitefinity\",\"ErrorMessage\":\"\",\"IsModuleLicensed\":true,\"IsSystemModule\":false,\"Key\":\"Feather\",\"ModuleId\":\"00000000-0000-0000-0000-000000000000\",\"ModuleType\":0,\"Name\":\"Feather\",\"ProviderName\":\"\",\"StartupType\":3,\"Status\":1,\"Title\":\"Feather\",\"Type\":\"Telerik.Sitefinity.Frontend.FrontendModule, Telerik.Sitefinity.Frontend\",\"Version\":{\"_Build\":390,\"_Major\":1,\"_Minor\":4,\"_Revision\":0}}";
        private const string CbContent = "Initial CB content";
        private const string FeatherDeactivateUrl = "/Sitefinity/Services/ModulesService/modules?operation=3";
        private const string FeatherActivateFromDeactivatedStateUrl = "/Sitefinity/Services/ModulesService/modules?operation=2";
        private string pageNamePrefix = "CBPage";
        private string pageTitlePrefix = "CB";
        private string urlNamePrefix = "content-block";
        private int pageIndex = 1;
    }
}
