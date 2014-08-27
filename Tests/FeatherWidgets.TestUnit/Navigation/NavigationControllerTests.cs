using System.Web.Mvc;
using FeatherWidgets.TestUnit.DummyClasses.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Navigation.Mvc.Models;

namespace FeatherWidgets.TestUnit.Navigation
{
    /// <summary>
    /// Tests methods for the NavigationController
    /// </summary>
    [TestClass]
    public class NavigationControllerTests
    {
        /// <summary>
        /// The create navigation_ call the index action_ ensures the model is properly created.
        /// </summary>
        [TestMethod]
        [Owner("EGaneva")]
        [Description("Checks whether the NavigationController properly creates its model and pass it to the Index action result.")]
        public void CreateNavigation_CallTheIndexAction_EnsuresTheModelIsProperlyCreated()
        {
            // Arrange
            using (var controller = new DummyNavigationController())
            {
                controller.CssClass = "myClass";
                controller.LevelsToInclude = 5;
                controller.SelectionMode = PageSelectionMode.CurrentPageChildren;
                controller.ShowParentPage = true;

                // Act
                var view = controller.Index() as ViewResult;
                var model = view.Model;
                var navigationModel = model as NavigationModel;

                // Assert
                Assert.IsNotNull(navigationModel, "The model is created correctly.");
                Assert.AreEqual(controller.CssClass, navigationModel.CssClass, "The CssClass property is not passed correctly.");
                Assert.AreEqual(controller.LevelsToInclude, navigationModel.LevelsToInclude, "The LevelsToInclude property is not passed correctly.");
                Assert.AreEqual(controller.SelectionMode, navigationModel.SelectionMode, "The SelectionMode property is not passed correctly.");
                Assert.AreEqual(controller.ShowParentPage, navigationModel.ShowParentPage, "The ShowParentPage property is not passed correctly.");
            }
        }

        /// <summary>
        /// The create navigation_ call the index action_ ensures the proper view is returned.
        /// </summary>
        [TestMethod]
        [Owner("EGaneva")]
        [Description("Checks whether the Index action returns the proper view.")]
        public void CreateNavigation_CallTheIndexAction_EnsuresTheProperViewIsReturned()
        {
            // Arrange
            using (var controller = new DummyNavigationController())
            {
                controller.TemplateName = "Vertical";

                // Act
                var view = controller.Index() as ViewResult;

                // Assert
                Assert.AreEqual("NavigationView.Vertical", view.ViewName, "The view name is not correct.");
            }
        }

        /// <summary>
        /// The create navigation_ call the get view action_ ensures the view is created properly.
        /// </summary>
        [TestMethod]
        [Owner("EGaneva")]
        [Description("Checks whether the NavigationController GetView method creates view correctly.")]
        public void CreateNavigation_CallTheGetViewAction_EnsuresTheViewIsCreatedProperly()
        {
            // Arrange
            var viewName = "Toggle";
            using (var controller = new DummyNavigationController())
            {
                NavigationModel model = new NavigationModel();
                model.CssClass = "myClass";
                model.LevelsToInclude = 5;

                // Act
                var view = controller.GetView(viewName, model) as PartialViewResult;
                var resultNavigationModel = model as NavigationModel;

                // Assert
                Assert.AreEqual(viewName, view.ViewName, "The view name is not correct.");
                Assert.AreEqual(model, resultNavigationModel, "The model hasn't been passed to the view correctly.");
            }
        }
    }
}
