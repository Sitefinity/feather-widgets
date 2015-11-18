using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using System.Web.Routing;
using MbUnit.Framework;
using RazorGenerator.Mvc;
using Telerik.Sitefinity.Frontend.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Frontend.News.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Services;

namespace FeatherWidgets.TestIntegration.News
{
    /// <summary>
    /// Tests whether News widget uses precompiled views.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Precompilation")]
    [TestFixture]
    public class NewsWidgetPrecompilation
    {
        /// <summary>
        /// Ensures that the view of the Designer in News widget is precompiled.
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the view of the Designer in News widget is precompiled.")]
        public void Designer_UsesPrecompiledView()
        {
            var controller = new DesignerController();
            var routeData = new RouteData();
            routeData.Values["controller"] = "Designer";
            routeData.Values["widgetName"] = "News";
            controller.ControllerContext = new ControllerContext(new RequestContext(SystemManager.CurrentHttpContext, routeData), controller);
            FrontendControllerFactory.EnhanceViewEngines(controller);

            var viewResult = controller.ViewEngineCollection.FindView(controller.ControllerContext, "Designer", null);

            Assert.IsNotNull(viewResult, "View result is null.");
            Assert.IsNotNull(viewResult.View, "The view was not found.");
            Assert.IsInstanceOfType<PrecompiledMvcView>(viewResult.View, "The view is not precompiled.");

            viewResult = controller.ViewEngineCollection.FindView(controller.ControllerContext, "DesignerView.Simple", null);

            Assert.IsNotNull(viewResult, "View result is null.");
            Assert.IsNotNull(viewResult.View, "The view was not found.");
            Assert.IsInstanceOfType<PrecompiledMvcView>(viewResult.View, "The view is not precompiled.");
        }

        /// <summary>
        /// Ensures that the frontend views of the News widget are precompiled.
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author(FeatherTeams.FeatherTeam)]
        [Description("Ensures that the frontend views of the News widget are precompiled.")]
        public void Frontend_UsesPrecompiledView()
        {
            var controller = new NewsController();
            var routeData = new RouteData();
            routeData.Values["controller"] = "News";
            controller.ControllerContext = new ControllerContext(new RequestContext(SystemManager.CurrentHttpContext, routeData), controller);
            FrontendControllerFactory.EnhanceViewEngines(controller);

            var viewResult = controller.ViewEngineCollection.FindView(controller.ControllerContext, "Detail.DetailPage", null);

            Assert.IsNotNull(viewResult, "View result is null.");
            Assert.IsNotNull(viewResult.View, "The view was not found.");
            Assert.IsInstanceOfType<PrecompiledMvcView>(viewResult.View, "The view is not precompiled.");

            viewResult = controller.ViewEngineCollection.FindView(controller.ControllerContext, "List.NewsList", null);

            Assert.IsNotNull(viewResult, "View result is null.");
            Assert.IsNotNull(viewResult.View, "The view was not found.");
            Assert.IsInstanceOfType<PrecompiledMvcView>(viewResult.View, "The view is not precompiled.");
        }
    }
}
