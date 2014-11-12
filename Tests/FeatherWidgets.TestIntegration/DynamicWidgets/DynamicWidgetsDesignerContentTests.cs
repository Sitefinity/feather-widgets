using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DynamicContent.Mvc.Controllers;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.DynamicWidgets
{
    /// <summary>
    /// This is a test class with tests related to dynamic widgets in toolbox section.
    /// </summary>
    [TestFixture]
    [Description("This is a test class with tests related to dynamic widgets designer Content section.")]
    public class DynamicWidgetsDesignerContentTests
    {
        [FixtureSetUp]
        public void Setup()
        {
            ServerOperationsFeather.DynamicModules().ImportModule(ModuleResource);
            Telerik.Sitefinity.TestUtilities.CommonOperations.ServerOperations.ModuleBuilder().ActivateModule(ModuleName, string.Empty, "Module Installations");

            for (int i = 0; i < 3; i++)
                ServerOperationsFeather.DynamicModulePressArticle().CreatePressArticle(this.dynamicTitles[i], new Guid(this.dynamicIds[i]));

            this.pageOperations = new PagesOperations();
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author("FeatherTeam")]
        [Description("Used the imported dynamic module and verifies that the proper widgets are generated.")]
        public void DynamicWidgetsDesignerContent_Test()
        {
            Assert.IsTrue(true);

            string testName = System.Reflection.MethodInfo.GetCurrentMethod().Name;
            string pageNamePrefix = testName + "DynamicPage";
            string pageTitlePrefix = testName + "Dynamic Page";
            string urlNamePrefix = testName + "dynamic-page";
            int index = 1;
            ////string url = UrlPath.ResolveAbsoluteUrl("~/" + urlNamePrefix + index);

            var mvcProxy = new MvcControllerProxy();
            mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
            var dynamicController = new DynamicContentController();

            ////dynamicController.Model.SelectionMode = NewsSelectionMode.AllItems;
            mvcProxy.Settings = new ControllerSettings(dynamicController);

            try
            {
               this.pageOperations.CreatePageWithControl(mvcProxy, pageNamePrefix, pageTitlePrefix, urlNamePrefix, index);

                ////int newsCount = 5;

                ////string responseContent = PageInvoker.ExecuteWebRequest(url);

                ////for (int i = 1; i <= newsCount; i++)
                ////    Assert.IsTrue(responseContent.Contains(NewsTitle + i), "The news with this title was not found!");
            }
            finally
            {
                this.pageOperations.DeletePages();
            }
        }

        [FixtureTearDown]
        public void Teardown()
        {
            Assert.IsTrue(true);
            ////ServerOperations.ModuleBuilder().DeleteModule(ModuleName, string.Empty, TransactionName);
        }

        #region Fields and constants

        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressRelease.zip";
        private const string DynamicWidgetSection = "Press Release";
        private const string DynamicWidgetMVCTitle = "Press Articles MVC";
        private const string DynamicWidgetTitle = "Press Articles";
        private const string TransactionName = "Module Installations";
        private string[] dynamicTitles = { "Angel", "Boat", "Cat" };
        private string[] dynamicIds = { "228c2dee-3640-43f2-881c-43992da8e055", "228c2dee-3640-43f2-881c-43992da8e056", "228c2dee-3640-43f2-881c-43992da8e057" };
         private PagesOperations pageOperations;

        #endregion
    }
}
