using System;
using System.Web.Mvc;
using FeatherWidgets.TestUnit.DummyClasses.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.AccountActivation;

namespace FeatherWidgets.TestUnit.Identity
{
    [TestClass]
    public class AccountActivationControllerTests
    {
        [TestMethod]
        [Owner("manev")]
        public void Index_TestDefaultViewName()
        {
            using (var controller = new DummyAccountActivationController())
            {
                controller.TemplateName = "MyTestTemplate";

                var result = (ViewResult)controller.Index();

                Assert.AreEqual("AccountActivation.MyTestTemplate", result.ViewName);
            }
        }

        [TestMethod]
        [Owner("manev")]
        public void Index_TestDefaultTemplateViewName()
        {
            using (var controller = new DummyAccountActivationController())
            {
                var result = (ViewResult)controller.Index();

                Assert.AreEqual("AccountActivation.Default", result.ViewName);
            }
        }

        [TestMethod]
        [Owner("manev")]
        public void Index_TestDefaultValuesOfViewModel()
        {
            using (var controller = new DummyAccountActivationController())
            {
                controller.TemplateName = "MyTestTemplate";

                var result = (ViewResult)controller.Index();

                var model = result.Model as AccountActivationViewModel;

                Assert.IsNotNull(model);
                Assert.IsNull(model.CssClass);
                Assert.IsNull(model.ProfilePageUrl);
                Assert.IsFalse(model.Activated);
            }
        }

        [TestMethod]
        [Owner("manev")]
        public void Index_TestDefaultValuesOfControllerModel()
        {
            using (var controller = new DummyAccountActivationController())
            {
                controller.TemplateName = "MyTestTemplate";

                var model = controller.Model;

                Assert.IsNotNull(model);
                Assert.IsNull(model.CssClass);
                Assert.IsTrue(model.MembershipProvider.Length == 0);
                Assert.IsNull(model.ProfilePageId);
            }
        }
    }
}
