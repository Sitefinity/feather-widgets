using System;
using System.Web;
using System.Web.Hosting;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestUtilities.Utilities;

namespace FeatherWidgets.TestUI.Arrangements
{
    /// <summary>
    /// Test arrangement methods for DeactivateAndActivateDynamicModuleVerifyPageToolbox
    /// </summary>
    public class DeactivateAndActivateDynamicModuleVerifyPageToolbox : TestArrangementBase
    {
        /// <summary>
        /// Server side set up.
        /// </summary>
        [ServerSetUp]
        public void SetUp()
        {
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);
            ServerOperations.Pages().CreatePage(PageName);
        }

        /// <summary>
        /// Server arrangement.
        /// </summary>
        [ServerArrangement]
        public void DeactivateModule()
        {
            ServerOperations.ModuleBuilder().DeactivateModule(ModuleName, string.Empty, TransactionName);

            WaitUtils.WaitForSitefinityToStart(HttpContext.Current.Request.Url
              .GetLeftPart(UriPartial.Authority) + (HostingEnvironment.ApplicationVirtualPath.TrimEnd('/') ?? string.Empty));
        }

        /// <summary>
        /// Server arrangement.
        /// </summary>
        [ServerArrangement]
        public void ActivateModule()
        {
            ServerOperations.ModuleBuilder().ActivateModule(ModuleName, string.Empty, TransactionName);

            WaitUtils.WaitForSitefinityToStart(HttpContext.Current.Request.Url
              .GetLeftPart(UriPartial.Authority) + (HostingEnvironment.ApplicationVirtualPath.TrimEnd('/') ?? string.Empty));
        }

        /// <summary>
        /// Server arrangement.
        /// </summary>
        [ServerArrangement]
        public void VerifyToolboxConfigBeforeDeactivate()
        {
            var section = ServerOperationsFeather.Pages().GetDynamicWidgetToolboxSection(ModuleName);
            int expectedCount = 2;

            Assert.IsTrue(section.Tools.Count.Equals(expectedCount), "Widgets count is unexpected.");
            Assert.IsTrue(ServerOperationsFeather.Pages().IsWidgetPresentInToolbox(section, DynamicWidgetTitle, isMvcWidget: true), "MVC Widget not found: " + DynamicWidgetTitle);
            Assert.IsTrue(ServerOperationsFeather.Pages().IsWidgetPresentInToolbox(section, DynamicWidgetTitle, isMvcWidget: false), "Web forms Widget not found: " + DynamicWidgetTitle);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.ModuleBuilder().DeleteAllModules(string.Empty, TransactionName);          
        }

        private const string ModuleName = "Press Release";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.PressReleaseWithCategoriesField.zip";
        private const string TransactionName = "Module Installations";
        private const string DynamicWidgetTitle = "Press Articles";
        private const string PageName = "TestPage";
    }
}
