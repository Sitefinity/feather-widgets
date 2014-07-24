using Microsoft.VisualStudio.TestTools.UnitTesting;
using Navigation.Mvc.Controllers;
using Navigation.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using UnitTests.DummyClasses.Navigation;

namespace UnitTests.Navigation
{
    /// <summary>
    /// Tests methods for the NavigationController
    /// </summary>
    [TestClass]
    public class NavigationControllerTests
    {
        [TestMethod]
        [Owner("EGaneva")]
        [Description("Checks whether the Index action returns the proper view.")]
        public void CreateNavigation_CallTheIndexAction_EnsuresTheProperViewIsReturned()
        {
            //Arrange
            DummyNavigationController controller = new DummyNavigationController();
            controller.TemplateName = "Vertical";

            //Act
            var view = controller.Index() as ViewResult;

            //Assert
            Assert.AreEqual("NavigationView.Vertical", view.ViewName, "The view name is not correct.");
        }

        [TestMethod]
        [Owner("EGaneva")]
        [Description("Checks whether the NavigationController GetView method creates view correctly.")]
        public void CreateNavigation_CallTheGetViewAction_EnsuresTheViewIsCreatedProperly()
        {
            //Arrange
            var viewName = "Toggle";
            DummyNavigationController controller = new DummyNavigationController();
            NavigationModel model = new NavigationModel();
            model.CssClass = "myClass";
            model.LevelsToInclude = 5;

            //Act
            var view = controller.GetView(viewName, model) as PartialViewResult;
            var resultModel = view.Model;
            var resultNavigationModel = model as NavigationModel;

            //Assert
            Assert.AreEqual(viewName, view.ViewName, "The view name is not correct.");
            Assert.AreEqual(model, resultNavigationModel, "The model hasn't been passed to the view correctly.");
        }
    }
}
