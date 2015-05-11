using System;
using System.Linq;
using System.Web.Mvc;
using FeatherWidgets.TestUnit.DummyClasses.LanguageSelector;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.LanguageSelector;

namespace FeatherWidgets.TestUnit.LanguageSelector
{
    [TestClass]
    public class LanguageSelectorControllerTests
    {
        /// <summary>
        /// Tests default properties of the calling Index action.
        /// </summary>
        [TestMethod]
        [Description("Tests default properties when calling Index action.")]
        [Owner("NPetrova")]
        public void Index_Action_Test()
        {
            using (var controller = new DummyLanguageSelectorController())
            {
                var viewAction = controller.Index() as ViewResult;

                Assert.IsNotNull(viewAction);
                Assert.IsNotNull(viewAction.Model is ILanguageSelectorModel);
                Assert.IsTrue(viewAction.ViewName == "LanguageSelector.LanguageLinks");
            }
        }

        /// <summary>
        /// Index_s the action_ test_ properties to the view are changed properly.
        /// </summary>
        [TestMethod]
        [Description("Tests default properties when calling Index action.")]
        [Owner("NPetrova")]
        public void Index_Action_Test_PropertiesToTheViewAreChangedProperly()
        {
            using (var controller = new DummyLanguageSelectorController())
            {
                string template = "custom template";
                controller.TemplateName = template;

                var viewAction = controller.Index() as ViewResult;

                Assert.IsNotNull(viewAction);
                Assert.IsNotNull(viewAction.Model is ILanguageSelectorModel);
                Assert.IsTrue(viewAction.ViewName == "LanguageSelector." + template);
            }
        }
    }
}
