using System;
using System.Web.Mvc;
using FeatherWidgets.TestUnit.DummyClasses.SearchBox;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.Search.Mvc.Models;

namespace FeatherWidgets.TestUnit.SearchBox
{
    [TestClass]
    public class SearchBoxControllerTests
    {
        [TestMethod]
        public void CallIndexAction_EnsureTheModelIsProperlyCreated()
        {           
            // Arrange
            using (var controller = new DummySearchBoxController())
            {
                // Act
                var view = controller.Index() as ViewResult;
                var model = view.Model;
                var searchBoxModel = model as SearchBoxModel;

                // Asserts
                Assert.IsNotNull(searchBoxModel, "The model is not created.");
                Assert.AreEqual("Title,Content", searchBoxModel.SuggestionFields, "The suggestion fields are not set correctly.");
                Assert.AreEqual("/restapi/search/suggestions", searchBoxModel.SuggestionsRoute, "The suggestions route is not created correctly.");
                Assert.AreEqual(3, searchBoxModel.MinSuggestionLength, "The minimal suggestions length is not created correctly.");
                Assert.AreEqual("en", searchBoxModel.Language, "The UI language is not set correctly.");
            }
        }
    }
}
