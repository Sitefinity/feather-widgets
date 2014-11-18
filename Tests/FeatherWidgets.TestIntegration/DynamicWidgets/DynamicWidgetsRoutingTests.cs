using System.Collections.Generic;
using System.Globalization;
using DynamicContent.Mvc.Controllers;
using DynamicContent.Mvc.Models;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.TestIntegration.Data.Content;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;

namespace FeatherWidgets.TestIntegration.DynamicWidgets
{
    /// <summary>
    /// This is a test class with tests related to dynamic widgets routing functionality on the frontend.
    /// </summary>
    [TestFixture]
    public class DynamicWidgetsRoutingTests
    {
        [FixtureSetUp]
        public void Setup()
        {
            ServerOperationsFeather.DynamicModules().ImportModule(ModuleResource);
            ServerOperations.ModuleBuilder().ActivateModule(ModuleName, string.Empty, TransactionName);

            for (int i = 1; i <= 3; i++)
            {
                var countryId = ServerOperationsFeather.DynamicModuleBooking()
                    .CreateCountry("Country" + i.ToString(CultureInfo.InvariantCulture));

                for (int j = 1; j <= 3; j++)
                {
                    var cityId = ServerOperationsFeather.DynamicModuleBooking()
                        .CreateCity(countryId, "Country" + i.ToString(CultureInfo.InvariantCulture) + "City" + j.ToString(CultureInfo.InvariantCulture));

                    for (int k = 1; k <= 3; k++)
                    {
                        ServerOperationsFeather.DynamicModuleBooking()
                            .CreateHotel(cityId, "Country" + i.ToString(CultureInfo.InvariantCulture) + "City" + j.ToString(CultureInfo.InvariantCulture) + "Hotel" + k.ToString(CultureInfo.InvariantCulture));
                    }
                }
            }
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author("FeatherTeam")]
        [Description("Adds all hierarchical dynamic widgets on page and verifies the proper routing on the frontend when use from currently opened option.")]
        public void DynamicWidgets_HierarchicalWidgetsOnPage_VerifyFromCurrentlyOpenedOption()
        {
            var countriesWidget = this.CreateMvcWidget(ResolveTypeCountry, WidgetNameCountries);
            var citiesWidget = this.CreateMvcWidget(ResolveTypeCity, WidgetNameCities, ParentFilterMode.CurrentlyOpen, ResolveTypeCountry);
            var hotelsWidget = this.CreateMvcWidget(ResolveTypeHotel, WidgetNameHotels, ParentFilterMode.CurrentlyOpen, ResolveTypeCity);

            var controls = new List<System.Web.UI.Control>();
            controls.Add(countriesWidget);
            controls.Add(citiesWidget);
            controls.Add(hotelsWidget);

            var pageId = ServerOperations.Pages().CreatePage(PageName);
            PageContentGenerator.AddControlsToPage(pageId, controls);

            string url = UrlPath.ResolveAbsoluteUrl("~/" + PageName + "/country1");
            string responseContent = PageInvoker.ExecuteWebRequest(url);

            for (int i = 1; i <= 3; i++)
            {
                Assert.IsTrue(responseContent.Contains("Country1City" + i.ToString(CultureInfo.InvariantCulture)), "The dynamic item with this title was NOT found!");
                Assert.IsFalse(responseContent.Contains("Country2City" + i.ToString(CultureInfo.InvariantCulture)), "The dynamic item with this title was found!");
                Assert.IsFalse(responseContent.Contains("Country3City" + i.ToString(CultureInfo.InvariantCulture)), "The dynamic item with this title was found!");
            }

            url = UrlPath.ResolveAbsoluteUrl("~/" + PageName + "/country2");
            responseContent = PageInvoker.ExecuteWebRequest(url);

            for (int i = 1; i <= 3; i++)
            {
                Assert.IsFalse(responseContent.Contains("Country1City" + i.ToString(CultureInfo.InvariantCulture)), "The dynamic item with this title was found!");
                Assert.IsTrue(responseContent.Contains("Country2City" + i.ToString(CultureInfo.InvariantCulture)), "The dynamic item with this title was NOT found!");
                Assert.IsFalse(responseContent.Contains("Country3City" + i.ToString(CultureInfo.InvariantCulture)), "The dynamic item with this title was found!");
            }
        }

        [FixtureTearDown]
        public void Teardown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.ModuleBuilder().DeleteModule(ModuleName, string.Empty, TransactionName);
        }

        private MvcWidgetProxy CreateMvcWidget(string resolveType, string widgetName, ParentFilterMode parentFilter = ParentFilterMode.All, string parentType = null)
        {
            var mvcProxy = new MvcWidgetProxy();
            mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
            var dynamicController = new DynamicContentController();
            dynamicController.Model.ContentType = TypeResolutionService.ResolveType(resolveType);
            dynamicController.Model.ParentFilterMode = parentFilter;
            dynamicController.Model.CurrentlyOpenParentType = parentType;
            mvcProxy.Settings = new ControllerSettings(dynamicController);
            mvcProxy.WidgetName = widgetName;

            return mvcProxy; 
        }

        private const string ModuleName = "Booking";
        private const string ModuleResource = "FeatherWidgets.TestUtilities.Data.DynamicModules.Booking.zip";
        private const string TransactionName = "Module Installations";
        private const string PageName = "TestPage";
        private const string ResolveTypeCountry = "Telerik.Sitefinity.DynamicTypes.Model.Booking.Country";
        private const string ResolveTypeCity = "Telerik.Sitefinity.DynamicTypes.Model.Booking.City";
        private const string ResolveTypeHotel = "Telerik.Sitefinity.DynamicTypes.Model.Booking.Hotel";
        private const string WidgetNameCountries = "Country";
        private const string WidgetNameCities = "City";
        private const string WidgetNameHotels = "Hotel";
    }
}
