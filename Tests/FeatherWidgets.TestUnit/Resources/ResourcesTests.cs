using System.Linq;
using ContentBlock.Mvc.StringResources;
using FeatherWidgets.TestUnit.DummyClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Navigation.Mvc.StringResources;

using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Project.Configuration;

namespace FeatherWidgets.TestUnit.Resources
{
    /// <summary>
    /// Resources Tests
    /// </summary>
    [TestClass]
    public class ResourcesTests
    {
        /// <summary>
        /// Contents the block resources_ iterate the resources_ assure resources are correct.
        /// </summary>
        [TestMethod]
        [Owner("Bonchev")]
        [Description("The test ensures that content block resources are correct.")]
        public void ContentBlockResources_IterateTheResources_AssureResourcesAreCorrect()
        {
            // Act & Assert: Iterate over each resource property and verify its correctness 
            this.TestResourceType<ContentBlockResources>();
        }

        /// <summary>
        /// Navigations the resources_ iterate the resources_ assure resources are correct.
        /// </summary>
        [TestMethod]
        [Owner("Bonchev")]
        [Description("The test ensures that navigation widget resources are correct.")]
        public void NavigationResources_IterateTheResources_AssureResourcesAreCorrect()
        {
            // Act & Assert: Iterate over each resource property and verify its correctness 
            this.TestResourceType<NavigationResources>();
        }

        /// <summary>
        /// Tests a given type of resource.
        /// </summary>
        /// <typeparam name="TRes">The type of the resource.</typeparam>
        private void TestResourceType<TRes>() where TRes : Telerik.Sitefinity.Localization.Resource, new()
        {
            // Arrange: Use the  getResourceClassDelegate to register and obtain a resource class instance, get the resource class type, register a dummy Config provider
            using (new ObjectFactoryContainerRegion())
            {
                ObjectFactory.Container.RegisterType<ConfigManager, ConfigManager>(typeof(XmlConfigProvider).Name.ToUpperInvariant(), new InjectionConstructor(typeof(XmlConfigProvider).Name));
                ObjectFactory.Container.RegisterType<XmlConfigProvider, DummyConfigProvider>();
                Config.RegisterSection<ResourcesConfig>();
                Config.RegisterSection<ProjectConfig>();

                var resourceClassType = typeof(TRes);
                    Res.RegisterResource(resourceClassType);
                    var resourceClass = Res.Get<TRes>();
                Assert.IsNotNull(resourceClass, "The resource class cannot be instantiated.");

                // Act & Assert: Iterate over each resource and verify if its resource attribute is correct and if the resource value is correct 
                var properties = resourceClassType.GetProperties().Where(p => p.GetCustomAttributes(typeof(ResourceEntryAttribute), false).Count() == 1);
                foreach (var prop in properties)
                {
                    var attribute = prop.GetCustomAttributes(typeof(ResourceEntryAttribute), false).FirstOrDefault() as ResourceEntryAttribute;
                    Assert.IsNotNull(attribute, "The resource property does not have the required resource attribute.");
                    var resource = prop.GetValue(resourceClass) as string;
                    Assert.IsFalse(string.IsNullOrEmpty(resource), string.Format(System.Globalization.CultureInfo.InvariantCulture, "The resource string for the {0} property cannot be found,", prop.Name));
                    Assert.AreEqual(prop.Name, attribute.Key, "The resource key does not match the property name,", System.Globalization.CultureInfo.InvariantCulture);
                    Assert.AreEqual(resource, attribute.Value, string.Format(System.Globalization.CultureInfo.InvariantCulture, "The resource string for the {0} property cannot be found,", prop.Name));
                    Assert.IsFalse(string.IsNullOrEmpty(attribute.Description), "The description of the resource cannot be empty string.", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
        }
    }
}
