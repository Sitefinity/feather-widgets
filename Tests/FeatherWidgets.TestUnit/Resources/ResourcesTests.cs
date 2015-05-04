using System.Linq;
using FeatherWidgets.TestUnit.DummyClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Data;
using Telerik.Sitefinity.Frontend.Blogs.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.ContentBlock.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.DynamicContent.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Identity.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.InlineClientAssets.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Lists.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Media.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.News.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Search.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.SocialShare.Mvc.StringResources;
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
        /// The test ensures that news widget resources are correct.
        /// </summary>
        [TestMethod]
        [Owner("EGaneva")]
        [Description("The test ensures that news widget resources are correct.")]
        public void NewsWidgetResources_IterateTheResources_AssureResourcesAreCorrect()
        {
            // Act & Assert: Iterate over each resource property and verify its correctness 
            this.TestResourceType<NewsWidgetResources>();
        }

        /// <summary>
        /// The test ensures that dynamic widget resources are correct.
        /// </summary>
        [TestMethod]
        [Owner("EGaneva")]
        [Description("The test ensures that dynamic widget resources are correct.")]
        public void DynamicWidgetResources_IterateTheResources_AssureResourcesAreCorrect()
        {
            // Act & Assert: Iterate over each resource property and verify its correctness 
            this.TestResourceType<DynamicContentResources>();
        }

        /// <summary>
        /// The test ensures that Social Share widget resources are correct.
        /// </summary>
        [TestMethod]
        [Owner("EGaneva")]
        [Description("The test ensures that Social Share widget resources are correct.")]
        public void SocialShareWidgetResources_IterateTheResources_AssureResourcesAreCorrect()
        {
            // Act & Assert: Iterate over each resource property and verify its correctness 
            this.TestResourceType<SocialShareResources>();
        }

        /// <summary>
        /// The test ensures that search widget resources are correct.
        /// </summary>
        [TestMethod]
        [Owner("EGaneva")]
        [Description("The test ensures that search widgets resources are correct.")]
        public void SearchWidgetResources_IterateTheResources_AssureResourcesAreCorrect()
        {
            // Act & Assert: Iterate over each resource property and verify its correctness 
            this.TestResourceType<SearchWidgetsResources>();
        }

        /// <summary>
        /// The test ensures that image widget resources are correct.
        /// </summary>
        [TestMethod]
        [Owner("EGaneva")]
        [Description("The test ensures that image widgets resources are correct.")]
        public void ImageWidgetResources_IterateTheResources_AssureResourcesAreCorrect()
        {
            // Act & Assert: Iterate over each resource property and verify its correctness 
            this.TestResourceType<ImageResources>();
        }

        /// <summary>
        /// The test ensures that image gallery widget resources are correct.
        /// </summary>
        [TestMethod]
        [Owner("EGaneva")]
        [Description("The test ensures that image gallery widget resources are correct.")]
        public void ImageGalleryResources_IterateTheResources_AssureResourcesAreCorrect()
        {
            // Act & Assert: Iterate over each resource property and verify its correctness 
            this.TestResourceType<ImageGalleryResources>();
        }

        /// <summary>
        /// The test ensures that document widget resources are correct.
        /// </summary>
        [TestMethod]
        [Owner("EGaneva")]
        [Description("The test ensures that document widget resources are correct.")]
        public void DocumentWidgetResources_IterateTheResources_AssureResourcesAreCorrect()
        {
            // Act & Assert: Iterate over each resource property and verify its correctness 
            this.TestResourceType<DocumentResources>();
        }

        /// <summary>
        /// The test ensures that document list widget resources are correct.
        /// </summary>
        [TestMethod]
        [Owner("EGaneva")]
        [Description("The test ensures that document list widget resources are correct.")]
        public void DocumentListResources_IterateTheResources_AssureResourcesAreCorrect()
        {
            // Act & Assert: Iterate over each resource property and verify its correctness 
            this.TestResourceType<DocumentsListResources>();
        }

        /// <summary>
        /// The test ensures that video widget resources are correct.
        /// </summary>
        [TestMethod]
        [Owner("EGaneva")]
        [Description("The test ensures that video widgets resources are correct.")]
        public void VideoWidgetResources_IterateTheResources_AssureResourcesAreCorrect()
        {
            // Act & Assert: Iterate over each resource property and verify its correctness 
            this.TestResourceType<VideoResources>();
        }

        /// <summary>
        /// The test ensures that video gallery widget resources are correct.
        /// </summary>
        [TestMethod]
        [Owner("EGaneva")]
        [Description("The test ensures that video gallery widget resources are correct.")]
        public void VideoGalleryResources_IterateTheResources_AssureResourcesAreCorrect()
        {
            // Act & Assert: Iterate over each resource property and verify its correctness 
            this.TestResourceType<VideoGalleryResources>();
        }

        /// <summary>
        /// The test ensures that blogs widget resources are correct.
        /// </summary>
        [TestMethod]
        [Owner("EGaneva")]
        [Description("The test ensures that blogs widgets resources are correct.")]
        public void BlogsWidgetResources_IterateTheResources_AssureResourcesAreCorrect()
        {
            // Act & Assert: Iterate over each resource property and verify its correctness 
            this.TestResourceType<BlogListResources>();
        }

        /// <summary>
        /// The test ensures that blog post widget resources are correct.
        /// </summary>
        [TestMethod]
        [Owner("EGaneva")]
        [Description("The test ensures that blog post widget resources are correct.")]
        public void BlogPostResources_IterateTheResources_AssureResourcesAreCorrect()
        {
            // Act & Assert: Iterate over each resource property and verify its correctness 
            this.TestResourceType<BlogPostResources>();
        }

        /// <summary>
        /// The test ensures that CSS widget resources are correct.
        /// </summary>
        [TestMethod]
        [Owner("EGaneva")]
        [Description("The test ensures that CSS widgets resources are correct.")]
        public void CssWidgetResources_IterateTheResources_AssureResourcesAreCorrect()
        {
            // Act & Assert: Iterate over each resource property and verify its correctness 
            this.TestResourceType<StyleSheetResources>();
        }

        /// <summary>
        /// The test ensures that JavaScript widget resources are correct.
        /// </summary>
        [TestMethod]
        [Owner("EGaneva")]
        [Description("The test ensures that JavaScript widgets resources are correct.")]
        public void JavaScriptWidgetResources_IterateTheResources_AssureResourcesAreCorrect()
        {
            // Act & Assert: Iterate over each resource property and verify its correctness 
            this.TestResourceType<JavaScriptResources>();
        }

        /// <summary>
        /// The test ensures that lists widget resources are correct.
        /// </summary>
        [TestMethod]
        [Owner("EGaneva")]
        [Description("The test ensures that lists widgets resources are correct.")]
        public void ListsWidgetResources_IterateTheResources_AssureResourcesAreCorrect()
        {
            // Act & Assert: Iterate over each resource property and verify its correctness 
            this.TestResourceType<ListsWidgetResources>();
        }

        /// <summary>
        /// The test ensures that Login status widget resources are correct.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login"), TestMethod]
        [Owner("EGaneva")]
        [Description("The test ensures that login status widget resources are correct.")]
        public void LoginStatusResources_IterateTheResources_AssureResourcesAreCorrect()
        {
            // Act & Assert: Iterate over each resource property and verify its correctness 
            this.TestResourceType<LoginStatusResources>();
        }

        /// <summary>
        /// The test ensures that Account activation widget resources are correct.
        /// </summary>
        [TestMethod]
        [Owner("EGaneva")]
        [Description("The test ensures that account activation widget resources are correct.")]
        public void AccountActivationResources_IterateTheResources_AssureResourcesAreCorrect()
        {
            // Act & Assert: Iterate over each resource property and verify its correctness 
            this.TestResourceType<AccountActivationResources>();
        }

        /// <summary>
        /// The test ensures that Change password widget resources are correct.
        /// </summary>
        [TestMethod]
        [Owner("EGaneva")]
        [Description("The test ensures that change password widget resources are correct.")]
        public void ChangePasswordResources_IterateTheResources_AssureResourcesAreCorrect()
        {
            // Act & Assert: Iterate over each resource property and verify its correctness 
            this.TestResourceType<ChangePasswordResources>();
        }

        /// <summary>
        /// The test ensures that Profile widget resources are correct.
        /// </summary>
        [TestMethod]
        [Owner("EGaneva")]
        [Description("The test ensures that profile widget resources are correct.")]
        public void ProfileResources_IterateTheResources_AssureResourcesAreCorrect()
        {
            // Act & Assert: Iterate over each resource property and verify its correctness 
            this.TestResourceType<ProfileResources>();
        }

        /// <summary>
        /// The test ensures that Registration widget resources are correct.
        /// </summary>
        [TestMethod]
        [Owner("EGaneva")]
        [Description("The test ensures that registration widget resources are correct.")]
        public void RegistrationResources_IterateTheResources_AssureResourcesAreCorrect()
        {
            // Act & Assert: Iterate over each resource property and verify its correctness 
            this.TestResourceType<RegistrationResources>();
        }

        /// <summary>
        /// The test ensures that Login form widget resources are correct.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Login"), TestMethod]
        [Owner("EGaneva")]
        [Description("The test ensures that form form widget resources are correct.")]
        public void LoginFormResources_IterateTheResources_AssureResourcesAreCorrect()
        {
            // Act & Assert: Iterate over each resource property and verify its correctness 
            this.TestResourceType<LoginFormResources>();
        }

        /// <summary>
        /// The test ensures that CSS widget resources are correct.
        /// </summary>
        [TestMethod]
        [Owner("Boyko-Karadzhov")]
        [Description("The test ensures that CSS widget resources are correct.")]
        public void StyleSheetResources_IterateTheResources_AssureResourcesAreCorrect()
        {
            // Act & Assert: Iterate over each resource property and verify its correctness 
            this.TestResourceType<StyleSheetResources>();
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
