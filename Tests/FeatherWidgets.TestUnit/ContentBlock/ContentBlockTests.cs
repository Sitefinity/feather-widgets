using System;
using System.Linq;
using System.Web.Mvc;
using FeatherWidgets.TestUnit.DummyClasses;
using FeatherWidgets.TestUnit.DummyClasses.ContentBlock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Data;
using Telerik.Sitefinity.Frontend.ContentBlock.Mvc.Models;
using Telerik.Sitefinity.Frontend.ContentBlock.Mvc.StringResources;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Project.Configuration;
using Telerik.Sitefinity.Services;

namespace FeatherWidgets.TestUnit.ContentBlock
{
    /// <summary>
    /// Tests methods for the Content Block 
    /// </summary>
    [TestClass]
    public class ContentBlockTests
    {
        /// <summary>
        /// The create content block controller_ call the index action_ ensures the model is properly created.
        /// </summary>
        [TestMethod]
        [Owner("Bonchev")]
        [Description("Checks whether the ContentBlockController properly creates its model and pass it to the Index action result.")]
        public void CreateContentBlockController_CallTheIndexAction_EnsuresTheModelIsProperlyCreated()
        {
            // Arrange: Create a dummy controller and set fake property values
            using (new ObjectFactoryContainerRegion())
            {
                this.RegisterResourceClasses();

                using (var controller = new DummyContentBlockController())
                {
                    controller.Content = "Fake controller";
                    controller.ProviderName = "Fake provider";
                    controller.SharedContentID = Guid.NewGuid();
                    controller.EnableSocialSharing = true;

                    // Act: Call the Index action and get the model from the ActionResult 
                    var res = controller.Index() as ViewResult;
                    var model = res.Model;
                    var contentBlockModel = model as IContentBlockModel;

                    // Assert: ensures the model is properly created and all its properties are properly populated
                    Assert.IsNotNull(contentBlockModel, "The model is null or its not implementing the IContentBlockInterface");
                    Assert.AreEqual(controller.Content, contentBlockModel.Content, "The Content property of the model is not properly set");
                    Assert.AreEqual(controller.ProviderName, contentBlockModel.ProviderName, "The provider name is not properly set");
                    Assert.AreEqual(controller.SharedContentID, contentBlockModel.SharedContentID, "The Id of the shared content item is not properly set");
                    Assert.AreEqual(controller.EnableSocialSharing, contentBlockModel.EnableSocialSharing, "The indicator which shows if the content block allows the social share options is not properly set");
                }
            }
        }

        /// <summary>
        /// The create content block controller_ index action_ ensures the actions the right view name.
        /// </summary>
        [TestMethod]
        [Owner("Bonchev")]
        [Description("The test ensures that the Index action of the ContentBlockController uses the right view name")]
        public void CreateContentBlockController_IndexAction_EnsuresTheActionUsesTheRightViewName()
        {
            // Arrange: Create a dummy controller and set fake property values
            using (new ObjectFactoryContainerRegion())
            {
                this.RegisterResourceClasses();
                using (var controller = new DummyContentBlockController())
                {
                    controller.Content = "Fake controller";
                    controller.ProviderName = "Fake provider";
                    controller.SharedContentID = Guid.NewGuid();
                    controller.EnableSocialSharing = true;

                    // Act: Call the Index action and get the model from the ActionResult 
                    var res = controller.Index() as ViewResult;
                    var viewName = res.ViewName;

                    // Assert: the action uses the right view name
                    Assert.AreEqual(viewName, "Default", "The requested view does not have the right name");
                }
            }
        }

        /// <summary>
        /// The dummy content block model_ is shared_ expect false.
        /// </summary>
        [TestMethod]
        [Owner("Bonchev")]
        [Description("The test ensures that the IsShare method of the ContentBlockModel returns false as expected")]
        public void DummyContentBlockModel_IsShared_ExpectFalse()
        {
            // Arrange: Create a dummy model 
            DummyContentBlockModel model = new DummyContentBlockModel();

            // Act: Call the IIsShared method
            bool isShared = model.PublicIsShared();

            // Assert:Tests if the model is not shared as expected
            Assert.IsFalse(isShared, "The model IsShared method returns true when expecting false.");
        }

        /// <summary>
        /// The content block controller_ initialize commands for not shared content_ ensure commands are correctly.
        /// </summary>
        [TestMethod]
        [Owner("Bonchev")]
        [Description("The test ensures that the controller properly creates a commands list with all commands for a content block item which is not yet shared.")]
        public void ContentBlockController_InitializeCommandsForNotSharedContent_EnsureCommandsAreCorrectly()
        {
            // Arrange: Register the ContentBlockResources and a dummy Config provider
            using (new ObjectFactoryContainerRegion())
            {
                this.RegisterResourceClasses();

                // Act:Instantiate a controller and initialize the commands
                using (var controller = new DummyContentBlockController())
                {
                    var commands = controller.InitializeCommands();

                    // Assert:Ensures the commands list is properly created
                    Assert.IsTrue(commands.Count >= 5, "Commands are less then expected.");
                    Assert.IsTrue(commands.Where(c => c.Text == Res.Get<Labels>().Delete).Count() == 1, "Commands list does not contain the Delete command.");
                    Assert.IsTrue(commands.Where(c => c.Text == Res.Get<Labels>().Duplicate).Count() == 1, "Commands list does not contain the Duplicate command.");
                    Assert.IsTrue(commands.Where(c => c.Text == Res.Get<ContentBlockResources>().Share).Count() == 1, "Commands list does not contain the Share command.");
                    Assert.IsTrue(commands.Where(c => c.Text == Res.Get<ContentBlockResources>().UseShared).Count() == 1, "Commands list does not contain the Delete command.");
                    Assert.IsTrue(commands.Where(c => c.Text == Res.Get<Labels>().Permissions).Count() == 1, "Commands list does not contain the Delete command.");
                }
            }
        }

        /// <summary>
        /// The content block controller_ initialize commands for shared content_ ensure commands are correctly.
        /// </summary>
        [TestMethod]
        [Owner("Bonchev")]
        [Description("The test ensures that the controller properly creates a commands list with all commands for a content block item which is already shared.")]
        public void ContentBlockController_InitializeCommandsForSharedContent_EnsureCommandsAreCorrectly()
        {
            // Arrange: Register the ContentBlockResources and a dummy Config provider
            using (new ObjectFactoryContainerRegion())
            {
                this.RegisterResourceClasses();

                // Act:Instantiate a controller and initialize the commands
                using (var controller = new DummyContentBlockController())
                {
                    controller.SharedContentID = Guid.NewGuid();
                    var commands = controller.InitializeCommands();

                    // Assert:Ensures the commands list is properly created
                    Assert.IsTrue(commands.Count >= 5, "Commands are less than expected.");
                    Assert.IsTrue(commands.Count(c => c.Text == Res.Get<Labels>().Delete) == 1, "Commands list does not contain the Delete command.");
                    Assert.IsTrue(commands.Count(c => c.Text == Res.Get<Labels>().Duplicate) == 1, "Commands list does not contain the Duplicate command.");
                    Assert.IsTrue(commands.Count(c => c.Text == Res.Get<ContentBlockResources>().Unshare) == 1, "Commands list does not contain the Unshare command.");
                    Assert.IsTrue(commands.Count(c => c.Text == Res.Get<ContentBlockResources>().UseShared) == 1, "Commands list does not contain the Delete command.");
                    Assert.IsTrue(commands.Count(c => c.Text == Res.Get<Labels>().Permissions) == 1, "Commands list does not contain the Delete command.");
                }
            }
        }

        /// <summary>
        /// Registers the resource classes.
        /// </summary>
        private void RegisterResourceClasses()
        {
            var resourceClassType = typeof(ContentBlockResources);
            var labelsClassType = typeof(Labels);

            ObjectFactory.Container.RegisterType<ConfigManager, ConfigManager>(typeof(XmlConfigProvider).Name.ToUpperInvariant(), new InjectionConstructor(typeof(XmlConfigProvider).Name));
            ObjectFactory.Container.RegisterType<XmlConfigProvider, DummyConfigProvider>();
            ObjectFactory.Container.RegisterType<ICacheManager, NoCacheManager>(CacheManagerInstance.LocalizationResources.ToString());
            Config.RegisterSection<ResourcesConfig>();
            Config.RegisterSection<ProjectConfig>();
            Config.RegisterSection<SystemConfig>();
            Res.RegisterResource(resourceClassType);
            Res.RegisterResource(labelsClassType);
        }
    }
}
