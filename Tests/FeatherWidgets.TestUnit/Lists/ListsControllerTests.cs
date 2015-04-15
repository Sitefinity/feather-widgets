using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FeatherWidgets.TestUnit.DummyClasses.Media;
using FeatherWidgets.TestUnit.DummyClasses.Media.DocumentsList;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Lists.Model;
using Telerik.Sitefinity.Model;

namespace FeatherWidgets.TestUnit.Media.DocumentsList
{
    /// <summary>
    /// Tests for ListsControllerTests
    /// </summary>
    [TestClass]
    public class ListsControllerTests
    {
        [TestMethod]
        [Owner("Manev")]
        public void CreateListsController_CallTheIndexAction_EnsuresDefaultModelPropertiesArePresented()
        {
            // Arrange
            using (var controller = new DummyListsController())
            {
                // Act
                var view = controller.Index(null) as EmptyResult;

                // Assert
                Assert.IsNotNull(view);
                Assert.IsTrue(controller.IsEmpty);
                Assert.IsTrue(controller.ViewBag.CurrentPageUrl == string.Empty);
                Assert.IsTrue(controller.ViewBag.RedirectPageUrlTemplate == "/{0}");
            }
        }

        [TestMethod]
        [Owner("Manev")]
        public void CreateListsController_CallTheIndexAction_EnsuresControllerIsNotEmpty()
        {
            // Arrange
            using (var controller = new DummyListsController())
            {
                controller.Model.SerializedSelectedItemsIds = "[CBFF0AA3-BE62-401D-9234-564DB235419D]";

                // Act
                var view = controller.Index(null) as ViewResult;

                // Assert
                Assert.IsNotNull(view);
                Assert.IsFalse(controller.IsEmpty);
                Assert.IsTrue(view.ViewBag.CurrentPageUrl == string.Empty);
                Assert.IsTrue(view.ViewBag.RedirectPageUrlTemplate == "/{0}");
                Assert.IsTrue(view.ViewName == "List.SimpleList");
                Assert.IsNotNull(view.Model);
            }
        }

        [TestMethod]
        [Owner("Manev")]
        public void CreateListsController_CallTheDetailsAction_EnsuresControllerIsNotEmpty()
        {
            // Arrange
            using (var controller = new DummyListsController())
            {
                ////controller.Model.SerializedSelectedItemsIds = "[CBFF0AA3-BE62-401D-9234-564DB235419D]";

                // Act
                var view = controller.Details(new DummyListItem() { Title = "ListItemTitle" }) as ViewResult;

                // Assert
                Assert.IsNotNull(view);
                Assert.IsTrue(controller.IsEmpty);
                Assert.IsTrue(view.ViewBag.Title == "ListItemTitle");
                Assert.IsTrue(view.ViewName == "Detail.DetailPage");
                Assert.IsNotNull(view.Model);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)"), TestMethod]
        [Owner("Manev")]
        public void CreateListsController_CallTheLocationService()
        {
            var guid1 = "CBFF0AA3-BE62-401D-9234-564DB235419D";
            var guid2 = "CBFF0AA3-BE62-401D-9234-564DB235419B";

            // Arrange
            using (var controller = new DummyListsController())
            {
                controller.Model.SerializedSelectedItemsIds = string.Format("[{0}, {1}]", guid1, guid2);

                ((DummyListsModel)controller.Model).Items = new List<FeatherWidgets.TestUnit.DummyClasses.Media.DummyListsModel.DummyList>
            {
                new FeatherWidgets.TestUnit.DummyClasses.Media.DummyListsModel.DummyList("app", new Guid(guid1)),
                new FeatherWidgets.TestUnit.DummyClasses.Media.DummyListsModel.DummyList("app", new Guid(guid2)),
            };

                // Act
                var location = controller.GetLocations();

                Assert.IsNotNull(location);
                Assert.IsTrue(location.Count() == 1);

                var firstLocationFilter = location.First();

                Assert.IsTrue(firstLocationFilter.ContentType == typeof(ListItem));
                Assert.IsTrue(firstLocationFilter.Filters.Count() == 1);
                Assert.IsTrue(firstLocationFilter.Filters.First().Value == "(Parent.Id = cbff0aa3-be62-401d-9234-564db235419d OR Parent.OriginalContentId = cbff0aa3-be62-401d-9234-564db235419d OR Parent.Id = cbff0aa3-be62-401d-9234-564db235419b OR Parent.OriginalContentId = cbff0aa3-be62-401d-9234-564db235419b)");
            }
        }

        private class DummyListItem : ListItem
        {
            public DummyListItem()
            {
            }

            public override Lstring Title
            {
                get;
                set;
            }
        }
    }
}
