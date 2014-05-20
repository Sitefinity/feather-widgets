using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContentBlock.Mvc.Controllers;
using UnitTests.DummyClasses.ContentBlock;
using System.Web.Mvc;
using ContentBlock.Mvc.Models;

namespace UnitTests.ContentBlock
{
    /// <summary>
    /// Tests methods for the ContentBlockController
    /// </summary>
    [TestClass]
    public class ContentBlockControllerTests
    {
        [TestMethod]
        [Owner("Bonchev")]
        [Description("Checks whether the ContentBlockController properly creates its model and pass it to the Index action result.")]
        public void CreateContentBlockController_CallTheIndexAction_EnsuresTheModelIsProperlyCreated()
        {
            //Arrange: Create a dummy controller and set fake property values
            DummyContentBlockController controller = new DummyContentBlockController();
            controller.Content = "Fake controller";
            controller.ProviderName = "Fake provider";
            controller.SharedContentID = Guid.NewGuid();
            controller.EnableSocialSharing = true;


            //Act: Call the Index action and get the model from the ActionResult 
            var res = controller.Index() as ViewResult;
            var model = res.Model;
            var contentBlockModel = model as IContentBlockModel;


            //Assert: ensures the model is properly created and all its properties are properly populated
            Assert.IsNotNull(contentBlockModel);
            Assert.AreEqual(controller.Content, contentBlockModel.Content);
            Assert.AreEqual(controller.ProviderName, contentBlockModel.ProviderName);
            Assert.AreEqual(controller.SharedContentID, contentBlockModel.SharedContentID);
            Assert.AreEqual(controller.EnableSocialSharing, contentBlockModel.EnableSocialSharing);

        }


        [TestMethod]
        [Owner("Bonchev")]
        [Description("Checks whether the ContentBlockController properly creates its model and pass it to the UseSharedContentItem action result.")]
        public void CreateContentBlockController_CallTheUseSharedContentItemAction_EnsuresTheModelIsProperlyCreated()
        {
            //Arrange: Create a dummy controller and set fake property values
            DummyContentBlockController controller = new DummyContentBlockController();
            controller.Content = "Fake controller";
            controller.ProviderName = "Fake provider";
            controller.SharedContentID = Guid.NewGuid();
            controller.EnableSocialSharing = true;


            //Act: Call the Index action and get the model from the ActionResult 
            var res = controller.UseSharedContentItem() as ViewResult;
            var model = res.Model;
            var contentBlockModel = model as IContentBlockModel;


            //Assert: ensures the model is properly created and all its properties are properly populated
            Assert.IsNotNull(contentBlockModel);
            Assert.AreEqual(controller.Content, contentBlockModel.Content);
            Assert.AreEqual(controller.ProviderName, contentBlockModel.ProviderName);
            Assert.AreEqual(controller.SharedContentID, contentBlockModel.SharedContentID);
            Assert.AreEqual(controller.EnableSocialSharing, contentBlockModel.EnableSocialSharing);

        }

    }
}
