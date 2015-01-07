using System;
using System.Web.Mvc;
using FeatherWidgets.TestUnit.DummyClasses.SocialShare;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUnit.SocialShare
{
    /// <summary>
    /// Tests methods for the SocialShareController
    /// </summary>
    [TestClass]
    public class SocialShareControllerTests
    {
        /// <summary>
        /// The create social share_ call the index action_ ensures the default view is returned.
        /// </summary>
        [TestMethod]
        [Owner("NPetrova")]
        [Description("Checks whether the Index action returns the default view.")]
        public void CreateSocialShare_CallTheIndexAction_EnsuresTheDefaultViewIsReturned()
        {
            // Arrange
            using (var controller = new DummySocialShareController())
            {
                // Act
                var view = controller.Index() as ViewResult;

                // Assert
                Assert.AreEqual("SocialShare", view.ViewName, "The view name is not correct.");
            }
        }

        /// <summary>
        /// The create social share_ call the index action_ ensures the changed view is returned.
        /// </summary>
        [TestMethod]
        [Owner("NPetrova")]
        [Description("Checks whether the Index action returns the changed view.")]
        public void CreateSocialShare_CallTheIndexAction_EnsuresTheChangedViewIsReturned()
        {
            // Arrange
            using (var controller = new DummySocialShareController())
            {
                controller.TemplateName = "SocialShareIconsWithText";

                // Act
                var view = controller.Index() as ViewResult;

                // Assert
                Assert.AreEqual("SocialShareIconsWithText", view.ViewName, "The view name is not correct.");
            }
        }
    }
}
