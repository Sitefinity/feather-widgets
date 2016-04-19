using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Web.Hosting;
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
        [Author(FeatherTeams.SitefinityTeam7)]
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
        [Author(FeatherTeams.SitefinityTeam7)]
        [Description("Ensures that the frontend views of the News widget are precompiled.")]
        public void Frontend_UsesPrecompiledView()
        {
            var viewResult = this.ViewResultFor("Detail.DetailPage");

            Assert.IsNotNull(viewResult, "View result is null.");
            Assert.IsNotNull(viewResult.View, "The view was not found.");
            Assert.IsInstanceOfType<PrecompiledMvcView>(viewResult.View, "The view is not precompiled.");

            viewResult = this.ViewResultFor("List.NewsList");

            Assert.IsNotNull(viewResult, "View result is null.");
            Assert.IsNotNull(viewResult.View, "The view was not found.");
            Assert.IsInstanceOfType<PrecompiledMvcView>(viewResult.View, "The view is not precompiled.");
        }

        /// <summary>
        /// Ensure that updated view template is delivered instead of precompiled one.
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author(FeatherTeams.SitefinityTeam7)]
        [Description("Ensure that updated view template is delivered instead of precompiled one.")]
        public void Frontend_UsesUpdatedView()
        {
            var folder = HostingEnvironment.MapPath("~/Mvc/Views/News/");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            try
            {
                var viewResult = this.ViewResultFor("Detail.DetailPage");

                Assert.IsNotNull(viewResult, "View result is null.");
                Assert.IsNotNull(viewResult.View, "The view was not found.");
                Assert.IsInstanceOfType<PrecompiledMvcView>(viewResult.View, "The view is not precompiled.");

                using (var writer = File.CreateText(Path.Combine(folder, "Detail.DetailPage.cshtml")))
                {
                    writer.WriteLine("@model Telerik.Sitefinity.Frontend.Mvc.Models.ContentDetailsViewModel");
                    writer.WriteLine("<h1>From a file</h1>");
                }

                viewResult = this.ViewResultFor("Detail.DetailPage");

                Assert.IsNotNull(viewResult, "View result is null.");
                Assert.IsNotNull(viewResult.View, "The view was not found.");
                Assert.IsNotInstanceOfType<PrecompiledMvcView>(viewResult.View, "The view is precompiled.");
            }
            finally
            {
                Directory.Delete(folder, true);
            }
        }

        /// <summary>
        /// Ensure that precompiled views are delivered even when physical file is present if config is set.
        /// </summary>
        [Test]
        [Category(TestCategories.News)]
        [Author(FeatherTeams.SitefinityTeam7)]
        [Description("Ensure that precompiled views are delivered even when physical file is present if config is set.")]
        public void Frontend_AlwaysUsePrecompiled_DoesNotUseUpdatedView()
        {
            var folder = HostingEnvironment.MapPath("~/Mvc/Views/News/");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            try
            {
                using (new FeatherConfigRegion(disablePrecompilation: false, alwaysUsePrecompiled: true))
                {
                    var viewResult = this.ViewResultFor("Detail.DetailPage");

                    Assert.IsNotNull(viewResult, "View result is null.");
                    Assert.IsNotNull(viewResult.View, "The view was not found.");
                    Assert.IsInstanceOfType<PrecompiledMvcView>(viewResult.View, "The view is not precompiled.");

                    using (var writer = File.CreateText(Path.Combine(folder, "Detail.DetailPage.cshtml")))
                    {
                        writer.WriteLine("@model Telerik.Sitefinity.Frontend.Mvc.Models.ContentDetailsViewModel");
                        writer.WriteLine("<h1>From a file</h1>");
                    }

                    viewResult = this.ViewResultFor("Detail.DetailPage");

                    Assert.IsNotNull(viewResult, "View result is null.");
                    Assert.IsNotNull(viewResult.View, "The view was not found.");
                    Assert.IsInstanceOfType<PrecompiledMvcView>(viewResult.View, "The view is not precompiled.");
                }
            }
            finally
            {
                Directory.Delete(folder, true);
            }
        }

        /// <summary>
        /// Ensures that precompiled views are not used when precompilation is disabled from config.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Precompilation")]
        [Test]
        [Category(TestCategories.News)]
        [Author(FeatherTeams.SitefinityTeam7)]
        [Description("Ensures that precompiled views are not used when precompilation is disabled from config.")]
        public void Frontend_DisablePrecompilation_DoesNotUsePrecompiledView()
        {
            var viewResult = this.ViewResultFor("Detail.DetailPage");

            Assert.IsNotNull(viewResult, "View result is null.");
            Assert.IsNotNull(viewResult.View, "The view was not found.");
            Assert.IsInstanceOfType<PrecompiledMvcView>(viewResult.View, "The view is not precompiled.");

            using (new FeatherConfigRegion(disablePrecompilation: true, alwaysUsePrecompiled: false))
            {
                viewResult = this.ViewResultFor("Detail.DetailPage");

                Assert.IsNotNull(viewResult, "View result is null.");
                Assert.IsNotNull(viewResult.View, "The view was not found.");
                Assert.IsNotInstanceOfType<PrecompiledMvcView>(viewResult.View, "The view is precompiled.");
            }
        }

        private ViewEngineResult ViewResultFor(string view)
        {
            var controller = new NewsController();
            var routeData = new RouteData();
            routeData.Values["controller"] = "News";
            controller.ControllerContext = new ControllerContext(new RequestContext(SystemManager.CurrentHttpContext, routeData), controller);
            FrontendControllerFactory.EnhanceViewEngines(controller);

            return controller.ViewEngineCollection.FindView(controller.ControllerContext, view, null);
        }
    }
}
