using System;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.ContentBlock.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
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

                pageOperations.CreatePageWithControl(mvcProxy, this.pageNamePrefix, this.pageTitlePrefix, this.urlNamePrefix, this.pageIndex);

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
                Assert.IsTrue(responseContentInEdit.Contains("This widget doesn't work, because Feather module has been deactivated."), "Error message is not displayed in zone editor!");
            }
            finally
            {
                pageOperations.DeletePages();
                moduleOperations.ActivateFeatherFromDeactivatedState();
            }
        }

        private const string CbContent = "Initial CB content";
        private string pageNamePrefix = "CBPage";
        private string pageTitlePrefix = "CB";
        private string urlNamePrefix = "content-block";
        private int pageIndex = 1;
    }
}
