using System;
using System.Linq;
using System.Web.Mvc;
using FeatherWidgets.TestUnit.DummyClasses.Taxonomies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.Taxonomies.Mvc.Models.FlatTaxonomy;

namespace FeatherWidgets.TestUnit.Taxonomies
{
    /// <summary>
    /// Unit tests for the FlatTaxonomyController.
    /// </summary>
    [TestClass]
    public class FlatTaxonomyControllerTests
    {
        /// <summary>
        /// Tests default properties of the calling Index action.
        /// </summary>
        [TestMethod]
        [Description("Tests default properties when calling Index action.")]
        [Owner("NPetrova")]
        public void Index_Action_Test()
        {
            using (var controller = new DummyFlatTaxonomyController())
            {
                var viewAction = controller.Index() as ViewResult;

                Assert.IsNotNull(viewAction);
                Assert.IsNotNull(viewAction.Model is IFlatTaxonomyModel);
                Assert.IsTrue(viewAction.ViewName == "FlatTaxonomy.SimpleList");
            }
        }

        /// <summary>
        /// Tests that properties to the view are changed properly.
        /// </summary>
        [TestMethod]
        [Description("Tests default properties when calling Index action.")]
        [Owner("NPetrova")]
        public void Index_Action_Test_PropertiesToTheViewAreChangedProperly()
        {
            using (var controller = new DummyFlatTaxonomyController())
            {
                string template = "custom template";
                controller.TemplateName = template;

                var viewAction = controller.Index() as ViewResult;

                Assert.IsNotNull(viewAction);
                Assert.IsNotNull(viewAction.Model is IFlatTaxonomyModel);
                Assert.IsTrue(viewAction.ViewName == "FlatTaxonomy." + template);
            }
        }
    }
}
