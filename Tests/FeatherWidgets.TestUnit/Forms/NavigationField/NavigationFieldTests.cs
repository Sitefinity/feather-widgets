using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FeatherWidgets.TestUnit.DummyClasses;
using FeatherWidgets.TestUnit.DummyClasses.Forms.NavigationField;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Data;
using Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.NavigationField;
using Telerik.Sitefinity.Frontend.Forms.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Frontend.TestUtilities.DummyClasses.HttpContext;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Project.Configuration;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Services;

namespace FeatherWidgets.TestUnit.Forms.NavigationField
{
    /// <summary>
    /// Tests methods for the Content Block 
    /// </summary>
    [TestClass]
    public class NavigationFieldTests
    {
        /// <summary>
        /// Ensures the model is properly created when Read action is called.
        /// </summary>
        [TestMethod]
        [Owner(FeatherTeams.SitefinityTeam6)]
        [Description("Ensures the model is properly created when Read action is called.")]
        public void CreateNavigationFieldController_CallTheReadAction_EnsuresTheModelIsProperlyCreated()
        {
            using (new ObjectFactoryContainerRegion())
            {
                this.RegisterResourceClasses();

                using (var controller = new DummyNavigationFieldController())
                {
                    var res = controller.Read(new object()) as ViewResult;
                    var model = res.Model;
                    var navigationFieldModel = model as NavigationFieldViewModel;

                    var pages = navigationFieldModel.Pages;

                    // Assert: ensures model pages value is set correctly
                    Assert.AreEqual(1, pages.Count(), "The SerializedPages property of the model is not properly set");
                    Assert.AreEqual(Res.Get<FieldResources>().PageName + "1", pages.First().Title, "The SerializedPages property of the model is not properly set");
                }
            }
        }

        /// <summary>
        /// Ensures the model is properly created when Read action is called.
        /// </summary>
        [TestMethod]
        [Owner(FeatherTeams.SitefinityTeam6)]
        [Description("Ensures the model is properly created when Read action is called.")]
        public void CreateNavigationFieldController_CallTheWriteAction_CorrectViewIsUsed()
        {
            using (new ObjectFactoryContainerRegion())
            {
                this.RegisterResourceClasses();

                using (var controller = new DummyNavigationFieldController())
                {
                    var httpContext = new NavigationDummyHttpContext();
                    SystemManager.RunWithHttpContext(
                                  httpContext,
                                  () =>
                                  {
                                      var res = controller.Write(new object()) as ViewResult;

                                      // Assert: the action uses the right view name
                                      Assert.AreEqual("Read.Default", res.ViewName, "The requested view does not have the right name");
                                  });
                }
            }
        }

        /// <summary>
        /// Registers the resource classes.
        /// </summary>
        private void RegisterResourceClasses()
        {
            var resourceClassType = typeof(FieldResources);
            var labelsClassType = typeof(Labels);
            var pageResourcesClassType = typeof(PageResources);

            ObjectFactory.Container.RegisterType<ConfigManager, ConfigManager>(typeof(XmlConfigProvider).Name.ToUpperInvariant(), new InjectionConstructor(typeof(XmlConfigProvider).Name));
            ObjectFactory.Container.RegisterType<XmlConfigProvider, DummyConfigProvider>();
            ObjectFactory.Container.RegisterType<ICacheManager, NoCacheManager>(CacheManagerInstance.LocalizationResources.ToString());
            ObjectFactory.Container.RegisterType<ICacheManager, NoCacheManager>(CacheManagerInstance.Users.ToString());

            ObjectFactory.Container.RegisterType<IRelatedDataResolver, DummyRelatedDataResolver>();
            ObjectFactory.Container.RegisterType<IModuleBuilderProxy, DummyModuleBuilderProxy>();

            Config.RegisterSection<ResourcesConfig>();
            Config.RegisterSection<ProjectConfig>();
            Config.RegisterSection<SystemConfig>();
            Config.RegisterSection<SecurityConfig>();
            Res.RegisterResource(resourceClassType);
            Res.RegisterResource(labelsClassType);
            Res.RegisterResource(pageResourcesClassType);
        }
    }
}
