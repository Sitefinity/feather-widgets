using System;
using System.Linq;
using System.Web.Mvc;
using FeatherWidgets.TestUnit.DummyClasses.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.UsersList;
using Telerik.Sitefinity.Security.Model;

namespace FeatherWidgets.TestUnit.Identity
{
    /// <summary>
    /// Tests the users list controller.
    /// </summary>
    [TestClass]
    public class UsersListControllerTests
    {
        /// <summary>
        /// Tests default properties of the calling Index action.
        /// </summary>
        [TestMethod]
        [Description("Tests default properties when calling Index action.")]
        [Owner("Manev")]
        public void Index_Action_Test()
        {
            using (var controller = new DummyUsersListController())
            {
                var viewAction = controller.Index(null) as ViewResult;

                Assert.IsNotNull(viewAction);
                Assert.IsNotNull(viewAction.Model is IUsersListModel);
                Assert.IsTrue(viewAction.ViewBag.CurrentPageUrl == string.Empty);
                Assert.IsTrue(viewAction.ViewBag.RedirectPageUrlTemplate == "/{0}");
                Assert.IsTrue(viewAction.ViewBag.DetailsPageId == Guid.Empty);
                Assert.IsTrue(viewAction.ViewBag.OpenInSamePage);
                Assert.IsTrue(viewAction.ViewName == "List.UsersList");
            }
        }

        /// <summary>
        /// Index_s the action_ test_ properties to the view are changed properly.
        /// </summary>
        [TestMethod]
        [Description("Tests default properties when calling Index action.")]
        [Owner("Manev")]
        public void Index_Action_Test_PropertiesToTheViewAreChangedProperly()
        {
            using (var controller = new DummyUsersListController())
            {
                var guid = new Guid("28431C80-F251-4A48-BE1A-3970C18DD9F7");

                string template = "custom template";
                controller.ListTemplateName = template;
                controller.OpenInSamePage = false;
                controller.DetailsPageId = guid;

                var viewAction = controller.Index(null) as ViewResult;

                Assert.IsNotNull(viewAction);
                Assert.IsNotNull(viewAction.Model is IUsersListModel);
                Assert.IsTrue(viewAction.ViewBag.CurrentPageUrl == string.Empty);
                Assert.IsTrue(viewAction.ViewBag.RedirectPageUrlTemplate == "/{0}");
                Assert.IsTrue(viewAction.ViewBag.DetailsPageId == guid);
                Assert.IsFalse(viewAction.ViewBag.OpenInSamePage);
                Assert.IsTrue(viewAction.ViewName == "List." + template);
            }
        }

        /// <summary>
        /// Tests default properties when calling Details action.
        /// </summary>
        [TestMethod]
        [Description("Tests default properties when calling Details action.")]
        [Owner("Manev")]
        public void Details_Action_Test()
        {
            using (var controller = new DummyUsersListController())
            {
                var viewAction = controller.Details(new SitefinityProfile()) as ViewResult;

                Assert.IsNotNull(viewAction);
                Assert.IsNotNull(viewAction.Model is IUsersListModel);
                Assert.IsTrue(viewAction.ViewName == "Detail.UserDetails");
            }
        }

        /// <summary>
        /// Details the action test properties to the view are changed properly.
        /// </summary>
        [TestMethod]
        [Description("Tests default properties when calling Index action.")]
        [Owner("Manev")]
        public void Details_Action_Test_PropertiesToTheViewAreChangedProperly()
        {
            using (var controller = new DummyUsersListController())
            {
                string templateName = "Custom user details temlate";

                controller.DetailTemplateName = templateName;

                var viewAction = controller.Details(new SitefinityProfile()) as ViewResult;

                Assert.IsNotNull(viewAction);
                Assert.IsNotNull(viewAction.Model is IUsersListModel);
                Assert.IsTrue(viewAction.ViewName == "Detail." + templateName);
            }
        }
    }
}
