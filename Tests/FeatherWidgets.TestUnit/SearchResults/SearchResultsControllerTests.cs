using System;
using System.Web;
using System.Web.Mvc;
using FeatherWidgets.TestUnit.DummyClasses;
using FeatherWidgets.TestUnit.DummyClasses.SearchBox;
using FeatherWidgets.TestUnit.DummyClasses.SearchResults;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Data;
using Telerik.Sitefinity.Frontend.Search.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Search.Mvc.Models;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Project.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Search.Configuration;

namespace FeatherWidgets.TestUnit.SearchResults
{
    [TestClass]
    public class SearchResultsControllerTests
    {
        [TestMethod]
        [Owner("EGaneva")]
        [Description("Checks whether the Index action displays no results when no parameters are provided.")]
        public void CallIndexAction_WithoutParams_EnsureTheModelIsProperlyCreated()
        {
            // Arrange
            using (var controller = new SearchResultsController())
            {
                // Act
                var view = controller.Index(null);

                // Asserts
                Assert.IsNull(view, "No results are displayed.");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), TestMethod]
        [Owner("EGaneva")]
        [Description("Checks whether Index action populates its model correctly.")]
        public void CallIndexAction_WithParams_EnsureTheModelIsProperlyCreated()
        {
            // Arrange
            var searchQuery = "searchString";
            var indexCatalogue = "catalogue1";
            var language = "en";
            var orderBy = "Oldest";

            using (new ObjectFactoryContainerRegion())
            {
                ObjectFactory.Container.RegisterType<ConfigManager, ConfigManager>(typeof(XmlConfigProvider).Name.ToUpperInvariant(), new InjectionConstructor(typeof(XmlConfigProvider).Name));
                ObjectFactory.Container.RegisterType<XmlConfigProvider, DummyConfigProvider>();
                ObjectFactory.Container.RegisterType<IRelatedDataResolver, DummyRelatedDataResolver>();
                ObjectFactory.Container.RegisterType<IModuleBuilderProxy, DummyModuleBuilderProxy>();
                Config.RegisterSection<ResourcesConfig>();
                Config.RegisterSection<SearchConfig>();
                Config.RegisterSection<ProjectConfig>();

                using (var controller = new DummySearchResultsController())
                {
                    var context =
                    new HttpContextWrapper(
                        new HttpContext(
                            new HttpRequest(null, "http://tempuri.org", "package=testPackageName"),
                            new HttpResponse(null)));

                    // Act
                    ViewResult view = null;
                    SystemManager.RunWithHttpContext(context, () => { view = controller.Index(null, searchQuery, indexCatalogue, null, language, orderBy) as ViewResult; });

                    var model = view.Model;
                    var searchResultsModel = model as SearchResultsModel;

                    // Asserts
                    Assert.IsNotNull(searchResultsModel, "The model is not created.");
                    Assert.AreEqual(0, searchResultsModel.CurrentPage, "The default value of the current page is 0.");
                    Assert.IsNotNull(searchResultsModel.Results, "No results are displayed.");
                    Assert.AreEqual(3, searchResultsModel.Results.TotalCount, "No items are found");
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), TestMethod]
        [Owner("EGaneva")]
        [Description("Checks whether the Results action displays no results when no parameters are provided.")]
        public void CallResultsAction_WithoutParams_EnsureTheModelIsProperlyCreated()
        {
            using (new ObjectFactoryContainerRegion())
            {
                ObjectFactory.Container.RegisterType<ConfigManager, ConfigManager>(typeof(XmlConfigProvider).Name.ToUpperInvariant(), new InjectionConstructor(typeof(XmlConfigProvider).Name));
                ObjectFactory.Container.RegisterType<XmlConfigProvider, DummyConfigProvider>();
                ObjectFactory.Container.RegisterType<IRelatedDataResolver, DummyRelatedDataResolver>();
                ObjectFactory.Container.RegisterType<IModuleBuilderProxy, DummyModuleBuilderProxy>();
                Config.RegisterSection<ResourcesConfig>();
                Config.RegisterSection<SearchConfig>();
                Config.RegisterSection<ProjectConfig>();

                // Arrange
                using (var controller = new SearchResultsController())
                {
                    var context =
                    new HttpContextWrapper(
                        new HttpContext(
                            new HttpRequest(null, "http://tempuri.org", "package=testPackageName"),
                            new HttpResponse(null)));

                    // Act
                    JsonResult jsonResult = null;
                    SystemManager.RunWithHttpContext(context, () => { jsonResult = controller.Results() as JsonResult; });

                    // Asserts
                    Assert.IsNotNull(jsonResult, "No results are displayed.");
                    var resultModel = jsonResult.Data as ResultModel;
                    Assert.AreEqual(0, resultModel.TotalCount, "No items are found");
                    Assert.AreEqual(0, resultModel.Data.Count, "The results collection is empty");
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"), TestMethod]
        [Owner("EGaneva")]
        [Description("Checks whether Results action populates the Results collection correctly.")]
        public void CallResultsAction_WithParams_EnsureTheModelIsProperlyCreated()
        {
            // Arrange
            var searchQuery = "searchString";
            var indexCatalogue = "catalogue1";
            var language = "en";
            var orderBy = "Oldest";

            using (new ObjectFactoryContainerRegion())
            {
                ObjectFactory.Container.RegisterType<ConfigManager, ConfigManager>(typeof(XmlConfigProvider).Name.ToUpperInvariant(), new InjectionConstructor(typeof(XmlConfigProvider).Name));
                ObjectFactory.Container.RegisterType<XmlConfigProvider, DummyConfigProvider>();
                ObjectFactory.Container.RegisterType<IRelatedDataResolver, DummyRelatedDataResolver>();
                ObjectFactory.Container.RegisterType<IModuleBuilderProxy, DummyModuleBuilderProxy>();
                Config.RegisterSection<ResourcesConfig>();
                Config.RegisterSection<SearchConfig>();
                Config.RegisterSection<ProjectConfig>();

                using (var controller = new DummySearchResultsController())
                {
                    var context =
                    new HttpContextWrapper(
                        new HttpContext(
                            new HttpRequest(null, "http://tempuri.org", "package=testPackageName"),
                            new HttpResponse(null)));

                    // Act
                    JsonResult jsonResult = null;
                    SystemManager.RunWithHttpContext(context, () => { jsonResult = controller.Results(searchQuery, indexCatalogue, language, orderBy, null) as JsonResult; });

                    ISearchResultsModel model = controller.Model;

                    // Asserts
                    Assert.IsNotNull(jsonResult, "No results are displayed.");
                    var resultModel = jsonResult.Data as ResultModel;
                    Assert.AreEqual(3, resultModel.TotalCount, "No items are found");
                    Assert.IsNotNull(model, "The model is not created.");
                }
            }
        }
    }
}
