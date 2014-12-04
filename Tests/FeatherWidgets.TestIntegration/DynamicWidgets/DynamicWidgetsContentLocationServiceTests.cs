using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using DynamicContent.Mvc.Controllers;
using FeatherWidgets.TestUtilities.CommonOperations;
using MbUnit.Framework;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.TestIntegration.Data.Content;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace FeatherWidgets.TestIntegration.DynamicWidgets
{
    /// <summary>
    /// This is a test class with tests related to dynamic widgets and content location service.
    /// </summary>
    [TestFixture]
    public class DynamicWidgetsContentLocationServiceTests
    {
        [FixtureSetUp]
        public void Setup()
        {
            ServerOperationsFeather.DynamicModules().ImportModule(ModuleResource);
            ServerOperations.ModuleBuilder().ActivateModule(ModuleName, string.Empty, TransactionName);            
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author("FeatherTeam")]
        [Description("Create a page with dynamic widget and verify content location of the dynamic item.")]
        public void DynamicWidgets_ContentLocationService_VerifyDynamicItemLocation()
        {
            int expectedLocationsCount;

            try
            {
                var countryId = ServerOperationsFeather.DynamicModuleBooking().CreateCountry(CountryName);
                Type countryType = TypeResolutionService.ResolveType(ResolveTypeCountry);
 
                DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager();

                var locationsService = SystemManager.GetContentLocationService();
                var dynamicItemLocations = locationsService.GetItemLocations(countryType, dynamicModuleManager.Provider.Name, countryId);

                expectedLocationsCount = 0;

                Assert.AreEqual(expectedLocationsCount, dynamicItemLocations.Count(), "There should be no locations yet");

                var pageId = ServerOperations.Pages().CreatePage(PageName);

                var countriesWidget = this.CreateMvcWidget(ResolveTypeCountry, WidgetNameCountries);
                var controls = new List<System.Web.UI.Control>();
                controls.Add(countriesWidget);

                PageContentGenerator.AddControlsToPage(pageId, controls);

                dynamicItemLocations = locationsService.GetItemLocations(countryType, dynamicModuleManager.Provider.Name, countryId);

                expectedLocationsCount = 1;

                Assert.AreEqual(expectedLocationsCount, dynamicItemLocations.Count(), "Unexpected locations count");
            }
            finally
            {
                ServerOperations.Pages().DeleteAllPages();
                ServerOperationsFeather.DynamicModuleBooking().DeleteCountry(CountryName);
            }
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author("FeatherTeam")]
        [Description("Create 2 pages with the same dynamic widget and verify content locations of the dynamic item.")]
        public void DynamicWidgets_ContentLocationService_VerifyDynamicItemLocationOnMoreThanOnePage()
        {
            int expectedLocationsCount;
            var page2Name = PageName + "2";

            try
            {
                var countryId = ServerOperationsFeather.DynamicModuleBooking().CreateCountry(CountryName);
                Type countryType = TypeResolutionService.ResolveType(ResolveTypeCountry);

                DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager();                

                var page1Id = ServerOperations.Pages().CreatePage(PageName);
                var page2Id = ServerOperations.Pages().CreatePage(page2Name);

                var countriesWidget = this.CreateMvcWidget(ResolveTypeCountry, WidgetNameCountries);
                var controls = new List<System.Web.UI.Control>();
                controls.Add(countriesWidget);

                PageContentGenerator.AddControlsToPage(page1Id, controls);
                PageContentGenerator.AddControlsToPage(page2Id, controls);

                var locationsService = SystemManager.GetContentLocationService();
                var dynamicItemLocations = locationsService.GetItemLocations(countryType, dynamicModuleManager.Provider.Name, countryId);

                expectedLocationsCount = 2;

                Assert.AreEqual(expectedLocationsCount, dynamicItemLocations.Count(), "Unexpected locations count");
            }
            finally
            {
                ServerOperations.Pages().DeleteAllPages();
                ServerOperationsFeather.DynamicModuleBooking().DeleteCountry(CountryName);
            }
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author("FeatherTeam")]
        [Description("Create page with dynamic widget and selected to display one item and verify content location of the dynamic item.")]
        public void DynamicWidgets_ContentLocationService_SingleItemSelectedFromWidget()
        {
            int expectedLocationsCount;

            try
            {
                for (int i = 1; i <= 3; i++)
                {
                    ServerOperationsFeather.DynamicModuleBooking().CreateCountry(CountryName + i.ToString(CultureInfo.InvariantCulture));
                }

                Type countryType = TypeResolutionService.ResolveType(ResolveTypeCountry);

                DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager();
                var countryItemId = dynamicModuleManager.GetDataItems(countryType).Where("Title = \"" + CountryName + "1\"").First().Id;

                string[] itemIds = new string[] { countryItemId.ToString() };

                var pageId = ServerOperations.Pages().CreatePage(PageName);

                var countriesWidget = this.CreateMvcWidget(ResolveTypeCountry, WidgetNameCountries, itemIds);
                var controls = new List<System.Web.UI.Control>();
                controls.Add(countriesWidget);

                PageContentGenerator.AddControlsToPage(pageId, controls);

                var locationsService = SystemManager.GetContentLocationService();
                var dynamicItemLocations = locationsService.GetItemLocations(countryType, dynamicModuleManager.Provider.Name, countryItemId);

                expectedLocationsCount = 1;

                Assert.AreEqual(expectedLocationsCount, dynamicItemLocations.Count(), "Unexpected locations count");
            }
            finally
            {
                ServerOperations.Pages().DeleteAllPages();

                for (int i = 1; i <= 3; i++)
                {
                    ServerOperationsFeather.DynamicModuleBooking().DeleteCountry(CountryName + i.ToString(CultureInfo.InvariantCulture));
                }
            }
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author("FeatherTeam")]
        [Description("Create page with dynamic widget and selected to display several items and verify content location of the dynamic item.")]
        public void DynamicWidgets_ContentLocationService_MultipleItemsSelectedFromWidget()
        {
            int expectedLocationsCount;

            try
            {
                for (int i = 1; i <= 3; i++)
                {
                    ServerOperationsFeather.DynamicModuleBooking().CreateCountry(CountryName + i.ToString(CultureInfo.InvariantCulture));
                }

                Type countryType = TypeResolutionService.ResolveType(ResolveTypeCountry);

                DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager();
                var countryItem1Id = dynamicModuleManager.GetDataItems(countryType).Where("Title = \"" + CountryName + "1\"").First().Id;
                var countryItem2Id = dynamicModuleManager.GetDataItems(countryType).Where("Title = \"" + CountryName + "2\"").First().Id;

                string[] itemIds = new string[] { countryItem1Id.ToString(), countryItem2Id.ToString() };

                var pageId = ServerOperations.Pages().CreatePage(PageName);

                var countriesWidget = this.CreateMvcWidget(ResolveTypeCountry, WidgetNameCountries, itemIds);
                var controls = new List<System.Web.UI.Control>();
                controls.Add(countriesWidget);

                PageContentGenerator.AddControlsToPage(pageId, controls);

                var locationsService = SystemManager.GetContentLocationService();
                var dynamicItemLocations = locationsService.GetItemLocations(countryType, dynamicModuleManager.Provider.Name, countryItem1Id);

                expectedLocationsCount = 1;

                Assert.AreEqual(expectedLocationsCount, dynamicItemLocations.Count(), "Unexpected locations count");

                dynamicItemLocations = locationsService.GetItemLocations(countryType, dynamicModuleManager.Provider.Name, countryItem2Id);
                Assert.AreEqual(expectedLocationsCount, dynamicItemLocations.Count(), "Unexpected locations count");
            }
            finally
            {
                ServerOperations.Pages().DeleteAllPages();

                for (int i = 1; i <= 3; i++)
                {
                    ServerOperationsFeather.DynamicModuleBooking().DeleteCountry(CountryName + i.ToString(CultureInfo.InvariantCulture));
                }
            }
        }

        [Test]
        [Category(TestCategories.DynamicWidgets)]
        [Author("FeatherTeam")]
        [Description("Create page with child widget and verify content location of the child dynamic item.")]
        public void DynamicWidgets_ContentLocationService_VerifyChildItemLocation()
        {
            int expectedLocationsCount;

            try
            {
                var countryId = ServerOperationsFeather.DynamicModuleBooking().CreateCountry(CountryName);
                
                string cityName = CountryName + CityName;
                
                var cityId = ServerOperationsFeather.DynamicModuleBooking().CreateCity(countryId, cityName);               
                Type cityType = TypeResolutionService.ResolveType(ResolveTypeCity);

                DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager();

                var pageId = ServerOperations.Pages().CreatePage(PageName);

                var countriesWidget = this.CreateMvcWidget(ResolveTypeCity, WidgetNameCities);
                var controls = new List<System.Web.UI.Control>();
                controls.Add(countriesWidget);

                PageContentGenerator.AddControlsToPage(pageId, controls);

                var locationsService = SystemManager.GetContentLocationService();
                var dynamicItemLocations = locationsService.GetItemLocations(cityType, dynamicModuleManager.Provider.Name, cityId);

                expectedLocationsCount = 1;

                Assert.AreEqual(expectedLocationsCount, dynamicItemLocations.Count(), "Unexpected locations count");
            }
            finally
            {
                ServerOperations.Pages().DeleteAllPages();
                ServerOperationsFeather.DynamicModuleBooking().DeleteCountry(CountryName);
            }
        }

        [FixtureTearDown]
        public void Teardown()
        {
            ServerOperations.ModuleBuilder().DeleteAllModules(string.Empty, TransactionName);
        }

        private MvcWidgetProxy CreateMvcWidget(string resolveType, string widgetName, string[] selectedItemsIds = null)
        {
            var mvcProxy = new MvcWidgetProxy();
            mvcProxy.ControllerName = typeof(DynamicContentController).FullName;
            var dynamicController = new DynamicContentController();
            dynamicController.Model.ContentType = TypeResolutionService.ResolveType(resolveType);

            if (selectedItemsIds != null)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var id in selectedItemsIds)
                {
                    sb.Append("\"");
                    sb.Append(id);
                    sb.Append("\"");

                    if (Array.IndexOf(selectedItemsIds, id) < selectedItemsIds.Length - 1)
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
        private const string WidgetNameCountries = "Country";
        private const string WidgetNameCities = "City";
        private const string CountryName = "Country";
        private const string CityName = "City";
    }
}
