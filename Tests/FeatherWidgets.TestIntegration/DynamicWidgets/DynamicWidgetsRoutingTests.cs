using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.Frontend.DynamicContent.Mvc.Controllers;
using Telerik.Sitefinity.Frontend.DynamicContent.Mvc.Models;
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
            ServerOperationsFeather.DynamicModules().EnsureModuleIsImported(ModuleName, ModuleResource);

            for (int i = 1; i <= 3; i++)
            {
                string countryName = "Country" + i.ToString(CultureInfo.InvariantCulture);

                var countryId = ServerOperationsFeather.DynamicModuleBooking()
                    .CreateCountry(countryName);

                for (int j = 1; j <= 3; j++)
                {
                    string cityName = countryName + "City" + j.ToString(CultureInfo.InvariantCulture);

                    var cityId = ServerOperationsFeather.DynamicModuleBooking()
                        .CreateCity(countryId, cityName);

                    for (int k = 1; k <= 3; k++)
                    {
                        string hotelName = countryName + cityName + "Hotel" + k.ToString(CultureInfo.InvariantCulture);
                        ServerOperationsFeather.DynamicModuleBooking()
                            .CreateHotel(cityId, hotelName);
                    }
                }
            }
        }

        [SetUp]
        public void TestSetup()
        {
            this.pageId = ServerOperations.Pages().CreatePage(PageName);
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.Team2)]
        [Description("Adds all hierarchical MVC dynamic widgets on page and verifies the proper cities are displayed when selecting a country on the frontend.")]
        public void DynamicWidgets_HierarchicalWidgetsOnPage_DisplayCitiesFromCurrentlyOpenedCountry()
        {
            try
            {
                var countriesWidget = this.CreateMvcWidget(ResolveTypeCountry, WidgetNameCountries);
                var citiesWidget = this.CreateMvcWidget(ResolveTypeCity, WidgetNameCities, ParentFilterMode.CurrentlyOpen, ResolveTypeCountry);
                var hotelsWidget = this.CreateMvcWidget(ResolveTypeHotel, WidgetNameHotels, ParentFilterMode.CurrentlyOpen, ResolveTypeCity);

                var controls = new List<System.Web.UI.Control>();
                controls.Add(countriesWidget);
                controls.Add(citiesWidget);
                controls.Add(hotelsWidget);

                PageContentGenerator.AddControlsToPage(this.pageId, controls);

                string url = UrlPath.ResolveAbsoluteUrl("~/" + PageName + "/country1");
                string responseContent = PageInvoker.ExecuteWebRequest(url);
                string suff = string.Empty;

                for (int i = 1; i <= 3; i++)
                {
                    suff = i.ToString(CultureInfo.InvariantCulture);

                    Assert.IsTrue(responseContent.Contains("Country1City" + suff), "The dynamic item with this title was NOT found: Country1City" + suff);
                    Assert.IsFalse(responseContent.Contains("Country2City" + suff), "The dynamic item with this title was found: Country2City" + suff);
                    Assert.IsFalse(responseContent.Contains("Country3City" + suff), "The dynamic item with this title was found: Country3City" + suff);
                }

                url = UrlPath.ResolveAbsoluteUrl("~/" + PageName + "/country2");
                responseContent = PageInvoker.ExecuteWebRequest(url);

                for (int i = 1; i <= 3; i++)
                {
                    suff = i.ToString(CultureInfo.InvariantCulture);

                    Assert.IsFalse(responseContent.Contains("Country1City" + suff), "The dynamic item with this title was found: Country1City" + suff);
                    Assert.IsTrue(responseContent.Contains("Country2City" + suff), "The dynamic item with this title was NOT found: Country2City" + suff);
                    Assert.IsFalse(responseContent.Contains("Country3City" + suff), "The dynamic item with this title was found: Country3City" + suff);
                }
            }
            finally
            {
                ServerOperations.Pages().DeleteAllPages();
            }
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.Team2)]
        [Description("Adds Cities MVC dynamic widgets on page and verifies the proper cities are displayed when navigating a country on the frontend when no Country widget is added on the page.")]
        public void DynamicWidgets_ChildWidget_DisplayCitiesFromCurrentlyOpenedCountryWithoutCountryWidget()
        {
            string countrySuff;
            string citySuff;
            string url;
            string responseContent;

            try
            {
                var citiesWidget = this.CreateMvcWidget(ResolveTypeCity, WidgetNameCities, ParentFilterMode.CurrentlyOpen, ResolveTypeCountry);

                var controls = new List<System.Web.UI.Control>();
                controls.Add(citiesWidget);

                PageContentGenerator.AddControlsToPage(this.pageId, controls);

                for (int i = 1; i <= 3; i++)
                {
                    countrySuff = i.ToString(CultureInfo.InvariantCulture);
                    url = UrlPath.ResolveAbsoluteUrl("~/" + PageName + "/country" + countrySuff);
                    responseContent = PageInvoker.ExecuteWebRequest(url);

                    for (int j = 1; j <= 3; j++)
                    {
                        citySuff = j.ToString(CultureInfo.InvariantCulture);
                        Assert.IsTrue(responseContent.Contains("Country" + countrySuff + "City" + citySuff), "The dynamic item with this title was NOT found: Country" + countrySuff + "City" + citySuff);
                    }
                }
            }
            finally
            {
                ServerOperations.Pages().DeleteAllPages();
            }
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.Team2)]
        [Description("Adds all hierarchical MVC dynamic widgets on page and verifies the proper hotels are displayed when selecting a city on the frontend.")]
        public void DynamicWidgets_HierarchicalWidgetsOnPage_DisplayHotelsFromCurrentlyOpenedCity()
        {
            string url;
            string responseContent;
            string suff;

            try
            {
                var countriesWidget = this.CreateMvcWidget(ResolveTypeCountry, WidgetNameCountries);
                var citiesWidget = this.CreateMvcWidget(ResolveTypeCity, WidgetNameCities, ParentFilterMode.CurrentlyOpen, ResolveTypeCountry);
                var hotelsWidget = this.CreateMvcWidget(ResolveTypeHotel, WidgetNameHotels, ParentFilterMode.CurrentlyOpen, ResolveTypeCity);

                var controls = new List<System.Web.UI.Control>();
                controls.Add(countriesWidget);
                controls.Add(citiesWidget);
                controls.Add(hotelsWidget);

                PageContentGenerator.AddControlsToPage(this.pageId, controls);

                url = UrlPath.ResolveAbsoluteUrl("~/" + PageName + "/country1/country1city1");
                responseContent = PageInvoker.ExecuteWebRequest(url);

                for (int i = 1; i <= 3; i++)
                {
                    suff = i.ToString(CultureInfo.InvariantCulture);

                    Assert.IsTrue(responseContent.Contains("Country1City1Hotel" + suff), "The dynamic item with this title was NOT found: Country1City1Hotel" + suff);
                    Assert.IsFalse(responseContent.Contains("Country1City2Hotel" + suff), "The dynamic item with this title was found: Country1City2Hotel" + suff);
                    Assert.IsFalse(responseContent.Contains("Country1City3Hotel" + suff), "The dynamic item with this title was found: Country1City3Hotel" + suff);
                }

                url = UrlPath.ResolveAbsoluteUrl("~/" + PageName + "/country1/country1city2");
                responseContent = PageInvoker.ExecuteWebRequest(url);

                for (int i = 1; i <= 3; i++)
                {
                    suff = i.ToString(CultureInfo.InvariantCulture);

                    Assert.IsTrue(responseContent.Contains("Country1City2Hotel" + suff), "The dynamic item with this title was NOT found: Country1City2Hotel" + suff);
                    Assert.IsFalse(responseContent.Contains("Country1City1Hotel" + suff), "The dynamic item with this title was found: Country1City1Hotel" + suff);
                    Assert.IsFalse(responseContent.Contains("Country1City3Hotel" + suff), "The dynamic item with this title was found: Country1City3Hotel" + suff);
                }

                url = UrlPath.ResolveAbsoluteUrl("~/" + PageName + "/country2/country2city2");
                responseContent = PageInvoker.ExecuteWebRequest(url);

                for (int i = 1; i <= 3; i++)
                {
                    suff = i.ToString(CultureInfo.InvariantCulture);

                    Assert.IsFalse(responseContent.Contains("Country2City1Hotel" + suff), "The dynamic item with this title was found: Country2City1Hotel" + suff);
                    Assert.IsTrue(responseContent.Contains("Country2City2Hotel" + suff), "The dynamic item with this title was NOT found: Country2City2Hotel" + suff);
                    Assert.IsFalse(responseContent.Contains("Country2City3Hotel" + suff), "The dynamic item with this title was found: Country2City3Hotel" + suff);
                }
            }
            finally
            {
                ServerOperations.Pages().DeleteAllPages(); 
            }
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.Team2)]
        [Description("Adds Hotels MVC dynamic widgets on page and verifies the proper hotels are displayed when navigating a city on the frontend without Cities widget on page.")]
        public void DynamicWidgets_ChildWidget_DisplayHotelsFromCurrentlyOpenedCityWithoutCityWidget()
        {
            string hotelSuff;
            string citySuff;
            string url;
            string responseContent;

            try
            {
                var hotelsWidget = this.CreateMvcWidget(ResolveTypeHotel, WidgetNameHotels, ParentFilterMode.CurrentlyOpen, ResolveTypeCity);
                var controls = new List<System.Web.UI.Control>();
                controls.Add(hotelsWidget);

                PageContentGenerator.AddControlsToPage(this.pageId, controls);

                for (int i = 1; i <= 3; i++)
                {
                    citySuff = i.ToString(CultureInfo.InvariantCulture);
                    url = UrlPath.ResolveAbsoluteUrl("~/" + PageName + "/country1/country1city" + citySuff);
                    responseContent = PageInvoker.ExecuteWebRequest(url);

                    for (int j = 1; j <= 3; j++)
                    {
                        hotelSuff = j.ToString(CultureInfo.InvariantCulture);
                        Assert.IsTrue(responseContent.Contains("Country1City" + citySuff + "Hotel" + hotelSuff), "The dynamic item with this title was NOT found: Country1City" + citySuff + "Hotel" + hotelSuff);
                    }
                }
            }
            finally
            {
                ServerOperations.Pages().DeleteAllPages();
            }
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.Team2)]
        [Description("Adds countries and hotels MVC dynamic widgets on page and verifies the proper hotels are displayed when selecting a country on the frontend.")]
        public void DynamicWidgets_HierarchicalWidgetsOnPage_DisplayHotelsFromCurrentlyOpenedCountry()
        {
            try
            {
                var countriesWidget = this.CreateMvcWidget(ResolveTypeCountry, WidgetNameCountries);
                var hotelsWidget = this.CreateMvcWidget(ResolveTypeHotel, WidgetNameHotels, ParentFilterMode.CurrentlyOpen, ResolveTypeCountry);

                var controls = new List<System.Web.UI.Control>();
                controls.Add(countriesWidget);
                controls.Add(hotelsWidget);

                PageContentGenerator.AddControlsToPage(this.pageId, controls);

                string url = UrlPath.ResolveAbsoluteUrl("~/" + PageName + "/country1");
                string responseContent = PageInvoker.ExecuteWebRequest(url);
                string citySuff = string.Empty;
                string hotelSuff = string.Empty;

                for (int i = 1; i <= 3; i++)
                {
                    citySuff = i.ToString(CultureInfo.InvariantCulture);

                    for (int j = 1; j <= 3; j++)
                    {
                        hotelSuff = j.ToString(CultureInfo.InvariantCulture);

                        Assert.IsTrue(responseContent.Contains("Country1City" + citySuff + "Hotel" + hotelSuff), "The dynamic item with this title was NOT found Country1City" + citySuff + "Hotel" + hotelSuff);
                        Assert.IsFalse(responseContent.Contains("Country2City" + citySuff + "Hotel" + hotelSuff), "The dynamic item with this title was found Country2City" + citySuff + "Hotel" + hotelSuff);
                        Assert.IsFalse(responseContent.Contains("Country3City" + citySuff + "Hotel" + hotelSuff), "The dynamic item with this title was found Country3City" + citySuff + "Hotel" + hotelSuff);
                    }
                }

                url = UrlPath.ResolveAbsoluteUrl("~/" + PageName + "/country2");
                responseContent = PageInvoker.ExecuteWebRequest(url);

                for (int i = 1; i <= 3; i++)
                {
                    citySuff = i.ToString(CultureInfo.InvariantCulture);

                    for (int j = 1; j <= 3; j++)
                    {
                        hotelSuff = j.ToString(CultureInfo.InvariantCulture);

                        Assert.IsFalse(responseContent.Contains("Country1City" + citySuff + "Hotel" + hotelSuff), "The dynamic item with this title was found Country1City" + citySuff + "Hotel" + hotelSuff);
                        Assert.IsTrue(responseContent.Contains("Country2City" + citySuff + "Hotel" + hotelSuff), "The dynamic item with this title was NOT found Country2City" + citySuff + "Hotel" + hotelSuff);
                        Assert.IsFalse(responseContent.Contains("Country3City" + citySuff + "Hotel" + hotelSuff), "The dynamic item with this title was found Country3City" + citySuff + "Hotel" + hotelSuff);
                    }
                }
            }
            finally
            {
                ServerOperations.Pages().DeleteAllPages();
            }
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.Team2)]
        [Description("Adds cities MVC dynamic widget on page and verifies the proper cities are displayed when selecting a country from the widget.")]
        public void DynamicWidgets_HierarchicalWidgetsOnPage_DisplayCitiesFromSelectedCountry()
        {
            try
            {
                var providerName = string.Empty;
                DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
                Type countryType = TypeResolutionService.ResolveType(ResolveTypeCountry);

                var countryItem = dynamicModuleManager.GetDataItems(countryType).Where("Title = \"Country2\"").First();

                string[] countryItemId = new string[] { countryItem.Id.ToString() };

                var citiesWidget = this.CreateMvcWidget(ResolveTypeCity, WidgetNameCities, ParentFilterMode.Selected, ResolveTypeCountry, countryItemId);

                var controls = new List<System.Web.UI.Control>();
                controls.Add(citiesWidget);

                PageContentGenerator.AddControlsToPage(this.pageId, controls);

                string url = UrlPath.ResolveAbsoluteUrl("~/" + PageName);
                string responseContent = PageInvoker.ExecuteWebRequest(url);

                for (int i = 1; i <= 3; i++)
                {
                    string suff = i.ToString(CultureInfo.InvariantCulture);

                    Assert.IsTrue(responseContent.Contains("Country2City" + suff), "The dynamic item with this title was NOT found Country2City" + suff);
                    Assert.IsFalse(responseContent.Contains("Country1City" + suff), "The dynamic item with this title was found Country1City" + suff);
                    Assert.IsFalse(responseContent.Contains("Country3City" + suff), "The dynamic item with this title was found Country3City" + suff);
                }               
            }
            finally
            {
                ServerOperations.Pages().DeleteAllPages();
            }
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author(FeatherTeams.Team2)]
        [Description("Adds cities MVC dynamic widget on page and verifies the proper cities are displayed when selecting more than 1 country from the widget.")]
        public void DynamicWidgets_HierarchicalWidgetsOnPage_DisplayCitiesFromSelectedCountries()
        {
            try
            {
                var providerName = string.Empty;
                DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);
                Type countryType = TypeResolutionService.ResolveType(ResolveTypeCountry);

                var countryItem = dynamicModuleManager.GetDataItems(countryType).Where("Title = \"Country2\"").First();
                var countryItem2 = dynamicModuleManager.GetDataItems(countryType).Where("Title = \"Country3\"").First();

                string[] parentIds = new string[] { countryItem.Id.ToString(), countryItem2.Id.ToString() };

                var citiesWidget = this.CreateMvcWidget(ResolveTypeCity, WidgetNameCities, ParentFilterMode.Selected, ResolveTypeCountry, parentIds);

                var controls = new List<System.Web.UI.Control>();
                controls.Add(citiesWidget);

                PageContentGenerator.AddControlsToPage(this.pageId, controls);

                string url = UrlPath.ResolveAbsoluteUrl("~/" + PageName);
                string responseContent = PageInvoker.ExecuteWebRequest(url);

                for (int i = 1; i <= 3; i++)
                {
                    string suff = i.ToString(CultureInfo.InvariantCulture);

                    Assert.IsTrue(responseContent.Contains("Country2City" + suff), "The dynamic item with this title was NOT found Country2City" + suff);
                    Assert.IsFalse(responseContent.Contains("Country1City" + suff), "The dynamic item with this title was found Country1City" + suff);
                    Assert.IsTrue(responseContent.Contains("Country3City" + suff), "The dynamic item with this title was NOT found Country3City" + suff);
                }
            }
            finally
            {
                ServerOperations.Pages().DeleteAllPages();
            }
        }

        [FixtureTearDown]
        public void Teardown()
        {           
            ServerOperations.ModuleBuilder().DeleteAllModules(string.Empty, TransactionName);
        }

        private MvcWidgetProxy CreateMvcWidget(string resolveType, string widgetName, ParentFilterMode parentFilter = ParentFilterMode.All, string parentType = null, string[] parentIds = null)
        {
            var mvcProxy = new MvcWidgetProxy();
            mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
            var dynamicController = new DynamicContentController();
            dynamicController.Model.ContentType = TypeResolutionService.ResolveType(resolveType);
            dynamicController.Model.ParentFilterMode = parentFilter;
            dynamicController.Model.CurrentlyOpenParentType = parentType;

            if (parentIds != null)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var id in parentIds)
                {
                    sb.Append("\"");
                    sb.Append(id);
                    sb.Append("\"");

                    if (Array.IndexOf(parentIds, id) < parentIds.Length - 1)
                    {
                        sb.Append(",");
                    }     
                }

                dynamicController.Model.SerializedSelectedParentsIds = "[" + sb.ToString() + "]";
            }

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
        private Guid pageId = Guid.Empty;
    }
}
