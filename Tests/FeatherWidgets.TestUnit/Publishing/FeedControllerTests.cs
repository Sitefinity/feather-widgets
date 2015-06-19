using System.Web.Mvc;
using System.Web.UI;
using FeatherWidgets.TestUnit.DummyClasses.Publishing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.Publishing.Mvc.Models;

namespace FeatherWidgets.TestUnit.Publishing
{
    /// <summary>
    /// Tests methods for the NavigationController
    /// </summary>
    [TestClass]
    public class FeedControllerTests
    {
        /// <summary>
        /// Checks whether the FeedController properly creates its model and pass it to the Index action result.
        /// </summary>
        [TestMethod]
        [Owner("Manev")]
        [Description("Checks whether the FeedController properly creates its model and pass it to the Index action result.")]
        public void CreateFeeds_CallTheIndexAction_EnsuresTheModelIsProperlyCreated()
        {
            // Arrange
            using (var controller = new DummyFeedController())
            {
                // Act
                var view = controller.Index() as ViewResult;
                var model = view.Model;
                var feedViewModel = model as FeedViewModel;

                // Assert
                Assert.IsNotNull(feedViewModel, "The model is created correctly.");
                Assert.IsTrue(view.ViewName == "Feed.FeedLink");
            }
        }

        /// <summary>
        /// Creates the feeds call the index action ensures empty result when module is not activated.
        /// </summary>
        [TestMethod]
        [Owner("Manev")]
        [Description("Creates the feeds call the index action ensures empty result when module is not activated.")]
        public void CreateFeeds_CallTheIndexAction_EnsuresEmptyResultWhenModuleIsNotActivated()
        {
            // Arrange
            using (var controller = new DummyFeedController(false))
            {
                // Act
                var view = controller.Index() as EmptyResult;

                // Assert
                Assert.IsNotNull(view);
            }
        }

        /// <summary>
        /// Creates the feeds call the index action ensures custom template is changed.
        /// </summary>
        [TestMethod]
        [Owner("Manev")]
        [Description("Creates the feeds call the index action ensures custom template is changed.")]
        public void CreateFeeds_CallTheIndexAction_EnsuresCustomTemplateIsChanged()
        {
            // Arrange
            using (var controller = new DummyFeedController())
            {
                controller.TemplateName = "CustomTemplate";

                // Act
                var view = controller.Index() as ViewResult;

                // Assert
                Assert.IsNotNull(view);
                Assert.IsTrue(view.ViewName == "Feed.CustomTemplate");
            }
        }
    }
}
