using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using FeatherWidgets.TestUnit.DummyClasses.SearchBox;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.Frontend.Search.Mvc.Models;
using Telerik.Sitefinity.Services;

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
                controller.Model.IndexCatalogue = "catalogue1";

                // Act
                var view = controller.Index() as ViewResult;
                var model = view.Model;
                var searchBoxModel = model as SearchBoxModel;

                // Asserts
                Assert.IsNotNull(searchBoxModel, "The model is not created.");
                Assert.AreEqual("Title,Content", searchBoxModel.SuggestionFields, "The suggestion fields are not set correctly.");
                Assert.AreEqual("restapi/search/suggestions", searchBoxModel.SuggestionsRoute, "The suggestions route is not created correctly.");
                Assert.AreEqual(3, searchBoxModel.MinSuggestionLength, "The minimal suggestions length is not created correctly.");
                Assert.AreEqual("en", searchBoxModel.Language, "The UI language is not set correctly.");
            }
        }

        [TestMethod]
        public void CallIndexAction_EnsureViewBagSearchQueryIsEscaped()
        {
            const string UnescapedQuery = "\" autofocus onfocus = \"alert('Haxxxor!')\" a = \"";
            const string EscapedQuery = "&quot; autofocus onfocus = &quot;alert(&#39;Haxxxor!&#39;)&quot; a = &quot;";
            const string CatalogName = "catalogue1";

            using (var controller = new DummySearchBoxController())
            {
                // Arrange
                controller.Model.IndexCatalogue = CatalogName;

                var queryString = new NameValueCollection();
                queryString.Add("searchQuery", UnescapedQuery);
                queryString.Add("indexCatalogue", CatalogName);

                var httpContext = new SearchBoxDummyHttpContext(queryString);

                SystemManager.RunWithHttpContext(
                              httpContext,
                              () =>
                              {
                                  // Act
                                  var view = controller.Index() as ViewResult;
                                  string viewBagSearchQuery = view.ViewBag.SearchQuery;

                                  // Asserts
                                  Assert.AreNotEqual(UnescapedQuery, viewBagSearchQuery);
                                  Assert.AreEqual(EscapedQuery, viewBagSearchQuery);
                              });
            }
        }
    }
}
